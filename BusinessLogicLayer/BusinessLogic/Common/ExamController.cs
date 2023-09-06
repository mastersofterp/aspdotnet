//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC CONTROLLER [EXAM CREATION]                     
// CREATION DATE : 22-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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
            public class ExamController
            {
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method is used to get all exam name.
                /// </summary>
                /// <param name="spname">Gets exam name as per this spname.</param>
                /// <returns>DataSet</returns>
                public DataSet GetAllExamName(int patternNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PATTERNNO", patternNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ALL_EXAM_HEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet CheckDuplicateDate(int sessionno, DateTime exdate, int slot)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO ", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMDATE ", exdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REGIST_SP_CHECK_EXAM_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.CheckDuplicateDate-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }
                /// <summary>
                /// This method is used to add new exam name in Exam_Name table.
                /// </summary>
                /// <param name="objExam">objExam is the object of Exam class.</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                /// 

                public DataSet GetStudentAttendanceData(int sessionno, int collegeno, int degreeno, int branchno, int semesterno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_TOSENDSMS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetStudentAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentIAMarksForSMS(int sessionno, int collegeno, int degreeno, int branchno, int semesterno, string examname)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_MARK", examname);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_STUDENT_MARKS_IA_FORSMS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }


                public int INSERTPARENTSMSLOG(int userno, string message, string parentmobileno, int usertype, int idno, int MSGTYPE)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO",userno),
                    new SqlParameter("@P_MSG_CONTENT", message),
                    new SqlParameter("@P_PARENTMOBILENO", parentmobileno),
                    new SqlParameter("@P_USERTYPE",usertype),
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_MSGTYPE",MSGTYPE),               
                    new SqlParameter("@P_MSGID", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_BULKSMS_INSERT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                //*************added on 22112022************
                public int FeeCredit(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string Creditfee, int FeesStructure, string Semname, int userno, bool FeeCertificate, string CertificateFee, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee, int Payment_Mode)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CREDITFEE", Creditfee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_CertiFeesApplicable",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_PaymentMode", Payment_Mode),
                    new SqlParameter("@P_OUT", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_CREDIT", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                //------------------------------------------------------

                public int FeeCourse(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string CourseFee, int FeesStructure, string Semname, int userno, bool FeeCertificate, string CertificateFee, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee, int Payment_Mode)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CourseFee", CourseFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CertiFeesApplicable",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_PaymentMode", Payment_Mode),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COURSE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                //------------------------------------------------------------------------

                public int FeeConfig(int College, int Session, int ExamType, int SUBID, string SubjectName, string FeeAmt, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, int FeesStructure, string Semname, int userno, bool FeeCertificate, string CertificateFee, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee, int Payment_Mode)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_SUBID", SUBID),
                    new SqlParameter("@P_SubjectName", SubjectName),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CertiFeesApplicable ",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_PaymentMode", Payment_Mode),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COFIG", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }

                //---------------------------------------------------------

                public int SemFeeConfig(int College, int Session, int ExamType, string TempSem, String Semestername, string FeeAmt, int degreeno, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, int FeesStructure, int userno, bool FeeCertificate, string CertificateFee, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee, int Payment_Mode)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_TEMPSEM", TempSem),
                    new SqlParameter("@P_SEMNAME", Semestername),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CertiFeesApplicable",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_PaymentMode", Payment_Mode),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACD_FEE_DEFINATION_CONFIG_SEM", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }
                //------------------------------------------------------------

                public int CreditRange(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string MINRANGE, string MAXRANGE, string AMOUNT, int FeesStructure, string Semname, int userno, bool FeeCertificate, string CertificateFee, int hfd, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee, int Payment_Mode)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_MINRANGE", MINRANGE),
                    new SqlParameter("@P_MAXRANGE", MAXRANGE),
                    new SqlParameter("@P_AMOUNT", AMOUNT),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CertiFeesApplicable",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_FID",hfd),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_PaymentMode", Payment_Mode),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEE_DEFINATION_CREDITRANGE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.CreditRange() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }



                //************END******************************





                public int Add(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_EXAMNAME", objExam.ExaminationName);
                        objParams[1] = new SqlParameter("@P_FLDNAME", objExam.FldName);
                        objParams[2] = new SqlParameter("@P_FORMULA", objExam.Formula);// not final
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[4] = new SqlParameter("P_EXAMNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMNAME_SP_INS_EXAMNAME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to update existing exam name from Exam_Name table.
                /// </summary>
                /// <param name="objExam">objExam is the object of Exam class.</param>
                /// <returns>Integer CustomStatus-Record Updated or Error</returns>
                public int Update(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty ExamName
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);
                        objParams[1] = new SqlParameter("@P_EXAMNAME", objExam.ExaminationName);
                        objParams[2] = new SqlParameter("@P_FLDNAME", objExam.FldName); // not final
                        objParams[3] = new SqlParameter("@P_FORMULA", objExam.Formula);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMNAME_SP_UPD_EXAMNAME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteExamDay(int EXDTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EXDTNO", EXDTNO);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_DELETE_EXAM_DAY", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteExamday-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// This method is used to retrieve single exam name.
                /// </summary>
                /// <param name="srno">Gets single exam name as per this examno.</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleExamName(int examno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EXAMNO", examno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_EXAMNAME_SP_RET_EXAMNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GetSingleExamName-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// ALL BELOW METHOD BELONG TO EXAM CONFIG TABLE
                /// </summary>
                /// <param name="srno">Gets single exam name as per this sessionno.</param>
                /// <returns>dataset</returns>

                public DataSet GetAllConfigExam(int sessionno)
                {
                    DataSet dsExam = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        dsExam = objSQLHelper.ExecuteDataSetSP("PKG_EXAMCONFIG_SP_ALL_EXAMDETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsExam;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GetAllExam-> " + ex.ToString());
                    }
                    return dsExam;
                }

                /// <summary>
                /// This method is used to get single exam configuration from Examconfig table.
                /// </summary>
                /// <param name="econid">Get single exam configuration as per this econid.</param>
                /// <returns> SqlDataReader</returns>
                public SqlDataReader GetSingleConfigExam(int econid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ECONID", econid);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_EXAMCONFIG_SP_RET_EXAMDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSingleExam -> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This method is used to add new exam configuration in Examconfig table.
                /// </summary>
                /// <param name="objExam">objExam is the object of EConfig class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                public int AddConfig(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add New Exam Configuration
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_EXAM_NAME", objExam.ConfigExamName);
                        objParams[1] = new SqlParameter("@P_FROMDATE", objExam.FromDate);
                        objParams[2] = new SqlParameter("@P_TODATE", objExam.ToDate);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[4] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[6] = new SqlParameter("@P_ECONID", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMCONFIG_SP_INS_EXAMCONFIG", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddConfig -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to update existing exam configuration from Examconfig table.
                /// </summary>
                /// <param name="objExam">objExam is the object of EConfig class</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>
                public int UpdateConfig(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        //UpdateFaculty Existing Exam Configuration
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ECONID", objExam.EconId);
                        objParams[1] = new SqlParameter("@P_EXAM_NAME", objExam.ConfigExamName);
                        objParams[2] = new SqlParameter("@P_FROMDATE", objExam.FromDate);
                        objParams[3] = new SqlParameter("@P_TODATE", objExam.ToDate);
                        objParams[4] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMCONFIG_SP_UPD_EXAMCONFIG", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateConfig -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateExamHead(string fldName, string examName, int Patternno, int ExamType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        // objParams[0] = new SqlParameter("@P_EXAMNO", lblExamNo);
                        objParams[0] = new SqlParameter("@P_FLDNAME ", fldName);
                        objParams[1] = new SqlParameter("@P_EXAMNAME", examName);
                        objParams[2] = new SqlParameter("@P_PATTERNNO", Patternno);
                        objParams[3] = new SqlParameter("@p_EXAMTYPE", ExamType);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMHEAD_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamName.UpdateExamHead-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region Exam Invigilator

                //public DataSet GetAllExamInvigilator()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_INVIGILATORS_GET_ALL", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamInvigilator-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public int Add_Update_ExamInvigilator(string ua_nos, string classnos, string distancenos, string status, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_UA_NOS", ua_nos);
                        objParams[1] = new SqlParameter("@P_CLASSNOS", classnos);
                        objParams[2] = new SqlParameter("@P_DISTANCENOS", distancenos);
                        objParams[3] = new SqlParameter("@P_STATUS", status);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_EXAM_INVIGILATOR", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add_Update_ExamInvigilator -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdateExamInvigilator(Exam objExam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_EXMINVNO", objExam.EXMINVNO);
                //        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Invigilator);
                //        objParams[2] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                //        objParams[3] = new SqlParameter("@P_EXDTNO", objExam.Exdtno);                        
                //        objParams[4] = new SqlParameter("@P_EXSHIFTNO", objExam.Shiftno);
                //        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_INVIGILATORS_UPDATE", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateExamInvigilator-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                //public DataSet GetSingleExamInvigilator(int EXMINVNO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_EXMINVNO", EXMINVNO);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_INVIGILATORS_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSingleExamInvigilator -> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetExamDatesBySesNo(int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATES_GET_BY_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetExamDatesBySesNo -> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion

                #region Exam Time Table
                public int GenerateTestTimeTable(int sessionno, int timetable_type, string col_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", timetable_type);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", col_code);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_SP_INS_TIMETABLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GenerateTestTimeTable -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTestTimeTable(int sessionno, int timetable_type)
                {
                    DataSet dsExam = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", timetable_type);

                        dsExam = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_SP_GET_TIMETABLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsExam;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GetTestTimeTable-> " + ex.ToString());
                    }
                    return dsExam;
                }
                #endregion

                #region Exam Test Seating Arrangement
                public DataSet GetTestSeatArrangement(int sessionno, int dayno, int slotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slotno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEST_SEATING_ARRANGEMENT_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GetTestSeatArrangement-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Exam Time Table Slot
                public int AddExamSlot(string slotname, string timefrom, string timeto, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[1] = new SqlParameter("@P_TIMEFROM", timefrom);
                        objParams[2] = new SqlParameter("@P_TIMETO", timeto);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[4] = new SqlParameter("@P_SLOTNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_TT_SLOT_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamSlot -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateExamSlot(int slotno, string slotname, string timefrom, string timeto)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[1] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[2] = new SqlParameter("@P_TIMEFROM", timefrom);
                        objParams[3] = new SqlParameter("@P_TIMETO", timeto);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_TT_SLOT_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateExamSlot -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSingleExamSlot(int slotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_EXAM_TT_SLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSingleExamSlot -> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                public int UpdateESEMTimeTable(int sessionno, string ccode, int dayno, int slotno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_CCODE", ccode);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[3] = new SqlParameter("@P_DAYNO", dayno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_SP_UPD_ESEM_TIMETABLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateESEMTimeTable -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDaySlotESEMTimeTable(int sessionno, int dayno1, int slotno1, int dayno2, int slotno2)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SLOTNO1", slotno1);
                        objParams[2] = new SqlParameter("@P_DAYNO1", dayno1);
                        objParams[3] = new SqlParameter("@P_SLOTNO2", slotno2);
                        objParams[4] = new SqlParameter("@P_DAYNO2", dayno2);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_SP_UPD_DAY_SLOT_ESEM_TIMETABLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateDaySlotESEMTimeTable -> " + ex.ToString());
                    }
                    return retStatus;
                }

                ////public int AddAbsentStudentsRecord(Exam objExam, int idno, int lck, int stat)//, string ufm_idno )
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);

                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                ////        SqlParameter[] objParams = null;

                ////        objParams = new SqlParameter[7];
                ////        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.Sessionno);

                ////        objParams[1] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);

                ////        objParams[2] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                ////        objParams[3] = new SqlParameter("@P_IDNOS", idno);
                ////        objParams[4] = new SqlParameter("@P_LOCK", lck);
                ////        objParams[5] = new SqlParameter("@P_stat", stat);
                ////        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);

                ////        objParams[6].Direction = ParameterDirection.Output;


                ////        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD", objParams, true));
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        retStatus = Convert.ToInt32(CustomStatus.Error);
                ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
                ////    }

                ////    return retStatus;
                ////}

                public int AddAbsentStudentsRecord(Exam objExam, int idno, int lck, int stat)//, string ufm_idno )
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.Sessionno);

                        objParams[1] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);

                        objParams[2] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[3] = new SqlParameter("@P_IDNOS", idno);
                        objParams[4] = new SqlParameter("@P_LOCK", lck);
                        objParams[5] = new SqlParameter("@P_stat", stat);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[6].Direction = ParameterDirection.Output;


                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // sycronus process get data

                public DataSet GetStudentsCompareData(int SessionNo, int operatorNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_OPERATOR", operatorNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RESULT_OPRTABLE_DEFECIT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetStudentsCompareData-> " + ex.ToString());
                    }

                    return ds;
                }
                public int AddCompareRecord(int sessionNo, int operatorNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_OPERATOR", operatorNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_RESULT_OPRTABLE_SYNCHRONIZE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region ExamDay

                public DataSet GetAllExamDay(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllExamDay(int sessionno, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        // objParams[5] = new SqlParameter("@P_EXAMTYPE", examtype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteSingleExamDay(int EXDTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EXDTNO", EXDTNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_DELETE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.DeleteSingleExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddExamDay(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[7] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[8] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[10] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[11] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[12] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateExamDay(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[3] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[5] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[6] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[8] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[9] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[12] = new SqlParameter("@P_EXDTNO", objExam.Exdtno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateExamDay-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetCourses(int schemeno, int semesterno, int sessionno, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSingleExamDay(int EXDTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EXDTNO", EXDTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSingleExamDay -> " + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet GetAttendanceData(int sessionno, int collegeno, int degreeno, int branchno, int semesterno, int schemeno, double fromper, string condition, double toper)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[9];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeno);
                //        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                //        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[6] = new SqlParameter("@P_PERCENTAGE_FROM", fromper);
                //        objParams[7] = new SqlParameter("@P_CONDITIONS", condition);
                //        objParams[8] = new SqlParameter("@P_PERCENTAGE_TO", toper);


                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA_FOR_DETAIN", objParams);

                //    }
                //    catch (Exception ex)
                //    {

                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetAttendanceData(int sessionno, int collegeno, int degreeno, int branchno, int semesterno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA_FOR_DETAIN", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet UpdateDetailBulkDe(string hdnval, string courselist, int sessionno, int degreeno, int branchno, int semesterno, int schemeno, int uano, string ipaddress)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_REGNO", hdnval);
                        objParams[1] = new SqlParameter("@P_COURSELIST", courselist);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_SCHEME", schemeno);
                        objParams[7] = new SqlParameter("@P_UANO", uano);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_DETAIN_RESULT", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudendetaintion(int session, int college, int degree, int sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", sem);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_DETAIN_RESULT_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }

                public int GradeAllotment(int sessionno, int maxmark, int minmark, int courseno, string ccode, int sectionno)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_MAXMARK", maxmark),
                            new SqlParameter("@P_MINMARK", minmark),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_EXAM_GRADE_ALLOTMENT_AUTONOMOUS", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ExamController.GradeAllotment() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateMarkTot(int sessionno, string ccode, int sectionno)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SECTIONNO", sectionno)
                        };

                        status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_EXAM_UPDATE_MARKTOT_AUTONOMOUS", sqlParams, true);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ExamController.GradeAllotment() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int GradeAllotmentNew(int sessionno, int courseno, string ccode, int sectionno, int offset)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_OFFSET", offset),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        //sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        //sqlParams[sqlParams.Length - 1].Size = 10000;
                        //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        SqlConnection conn = new SqlConnection(_uaims_constr);
                        SqlCommand cmd = new SqlCommand("PKG_ACAD_EXAM_GRADE_ALLOTMENT_AUTONOMOUS_NEW", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = Convert.ToInt32(cmd.Parameters[5].Value);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResultProcessing.ResultProcess() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;


                }

                #endregion

                #region Invigilator
                /// <summary>
                /// This method is used to make the faculty applicable for invigilation duty
                /// </summary>
                /// <param name="objExam">objExam is the object of Exam class.</param>
                /// <returns>Integer CustomStatus - Record Added or or record Error</returns>
                public int Add_Update_Exam_Invigilator_Status(string ua_nos, string status, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NOS", ua_nos);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_EXAM_INVIGILATOR", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.ApplyInvigilatorApplication-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //TO ADD INVIGILATION DUTY
                public int AddInvigilationDuty(int sessionno, int dayno, int slotno, int inv_cnt, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[3] = new SqlParameter("@P_INV_CNT", inv_cnt);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colcode);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GENERATE_INVIGILATION_DUTY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.AddInvigilationDuty-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                public DataSet ViewDate(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        // objParams[1] = new SqlParameter("@P_EXAM_TYPE", examtype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_GET_DATE", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet BindDate(int dayno, int sessionno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_EXAM_TYPE", examtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_GET_DAY", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public int AssignValuer(int sessionno, int schemeno, int courseno, int ua_no)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ASSIGN_VALUER", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssignValuer --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ret;
                }

                public int PracticalValuer(int sessionno, int schemeno, int courseno, int extua_no, string ua_name, int intua_no, int batchno, int sectionno)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_EXTUA_NO", extua_no);
                        objParams[4] = new SqlParameter("@P_UA_NAME", ua_name);
                        objParams[5] = new SqlParameter("@P_INTUA_NO", intua_no);
                        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[7] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ASSIGN_PRACTICAL_FACULTY", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssignValuer --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ret;
                }
                //ADDED
                public DataSet GetAllExamDayForChange(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int Slot, int CourseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        //objParams[5] = new SqlParameter("@P_DAYNO", DayNo);
                        objParams[5] = new SqlParameter("@P_SLOTNO", Slot);
                        objParams[6] = new SqlParameter("@P_COURSENO", CourseNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_FOR_PRE_POST_EXAM_TIMETABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet ViewDateForSelection(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_GET_DATE_FOR_SELECTION", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public DataSet EBindDate(int dayno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //  objParams[2] = new SqlParameter("@P_EXAM_TYPE", examtype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_GET_DAY1", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //public int IssueBundle(int sessionno, int courseno, string ccode, int set, int prev_status, int valuer_ua_no, string seatfrom, string seatnoto, string bundle)
                //{
                //    int ret = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[10];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SET", set);
                //        objParams[4] = new SqlParameter("@P_PREV_STATUS", prev_status);
                //        objParams[5] = new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no);
                //        objParams[6] = new SqlParameter("@P_SEATNOFROM", seatfrom);
                //        objParams[7] = new SqlParameter("@P_SEATNOTO", seatnoto);
                //        objParams[8] = new SqlParameter("@P_BUNDLE", bundle);
                //        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //        objParams[9].Direction = ParameterDirection.Output;

                //        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_BUNDLE_CREATION", objParams, true));
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IssueBunle --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ret;
                //}

                //added on 24 sept 2013
                public int AddEquivalanceCourses(int Old_schemeno, int New_Schemeno, string New_Ccode, string New_Courseno, string Old_Ccode, int Old_Courseno, string college_code, string ipAddress, int ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_OLD_SCHEMENO", Old_schemeno);
                        objParams[1] = new SqlParameter("@P_NEW_SCHEMENO", New_Schemeno);
                        objParams[2] = new SqlParameter("@P_NEW_CCODE", New_Ccode);
                        objParams[3] = new SqlParameter("@P_NEW_COURSENO", New_Courseno);
                        objParams[4] = new SqlParameter("@P_OLD_CCODE", Old_Ccode);
                        objParams[5] = new SqlParameter("@P_OLD_COURSENO", Old_Courseno);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[8] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[9] = new SqlParameter("P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_EQUIVALANCE_COURSES", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddEquivalanceCourses-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEquivalanceCourse(int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_EQUIVALENCE_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetEquivalanceCourse -> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteEquivalence(int schemeno, int courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_OLD_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_OLD_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_EQUIVALANCE_COURSES", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteEquivalance-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int IssueBundle(int sessionno, int courseno, string ccode, int set, int prev_status, int valuer_ua_no, int schemeno, string seatfrom, string seatnoto, string bundle)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SET", set);
                        objParams[4] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[5] = new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[7] = new SqlParameter("@P_SEATNOFROM", seatfrom);
                        objParams[8] = new SqlParameter("@P_SEATNOTO", seatnoto);
                        objParams[9] = new SqlParameter("@P_BUNDLE", bundle);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_BUNDLE_CREATION", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IssueBunle --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ret;
                }

                public int ReceiveBundle(int sessionno, int courseno, string ccode, int set, int prev_status, int valuer_ua_no, int bundleid)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SET", set);
                        objParams[4] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[5] = new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no);
                        objParams[6] = new SqlParameter("@P_BUNDLEID", bundleid);

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_BUNDLE_RECEIVE_STATUS", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IssueBunle --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ret;
                }

                public int UFMCategoryInsUpd(int UfmNo, string Category, string CateDesc, string CatePunishemnt, string col_code, bool Extermark, bool s1, bool s2, bool s3, bool s4, bool s5, bool s6, bool s7, bool s8, bool s9, bool s10, int debarred)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];

                        objParams[0] = new SqlParameter("@P_UFMNO", UfmNo);
                        objParams[1] = new SqlParameter("@P_UFM_NAME", Category);
                        objParams[2] = new SqlParameter("@P_UFM_DESC", CateDesc);
                        objParams[3] = new SqlParameter("@P_UFM_PUNISHMENT", CatePunishemnt);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", col_code);
                        objParams[5] = new SqlParameter("@P_EXTERMARK", Extermark);
                        objParams[6] = new SqlParameter("@P_S1", s1);
                        objParams[7] = new SqlParameter("@P_S2", s2);
                        objParams[8] = new SqlParameter("@P_S3", s3);
                        objParams[9] = new SqlParameter("@P_S4", s4);
                        objParams[10] = new SqlParameter("@P_S5", s5);
                        objParams[11] = new SqlParameter("@P_S6", s6);
                        objParams[12] = new SqlParameter("@P_S7", s7);
                        objParams[13] = new SqlParameter("@P_S8", s8);
                        objParams[14] = new SqlParameter("@P_S9", s9);
                        objParams[15] = new SqlParameter("@P_S10", s10);
                        objParams[16] = new SqlParameter("@P_DEBARRED", debarred);
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UFM_CATEGORY_INSERT_UPDATE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UFMCategoryInsUpd --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ret;
                }

                public DataTableReader GetIssueBundle(int sessionno, string bundle)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BUNDLE", bundle);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ISSUE_BUNDLE", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public DataSet GetPresntAbsUFM_Student(int sessionno, int degreeno, int branchno, int schemeno, int sem, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_ABSENT_STATEMENT_REPORT", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetPresntAbsUFM_Student() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //ADDED on 24 feb 2014
                public int ProcessSemesterwise(int idno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        //objParams[2] = new SqlParameter("P_EXAMNO", SqlDbType.Int);
                        //objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PROCESS_ALL_SEM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateIssueBundle(int sessionno, int courseno, int schemeno, int set, int valuer_ua_no, string bundle)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SET", set);
                        objParams[4] = new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no);
                        objParams[5] = new SqlParameter("@P_BUNDLE", bundle);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_BUNDLE_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateIssueBundle -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateExamDayForSelection(string Exdtno, Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EXDTNO", Exdtno);
                        objParams[1] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_UPDATE_FOR_SELECTION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateExamDay-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet ViewDateForSelection(int sessionno, int examtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_TYPE", examtype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_GET_DATE_FOR_SELECTION", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //public DataSet GetAllExamDayForChange(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int examtype, int DayNo, int Slot, int CourseNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[9];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                //        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[5] = new SqlParameter("@P_EXAMTYPE", examtype);
                //        objParams[6] = new SqlParameter("@P_DAYNO", DayNo);
                //        objParams[7] = new SqlParameter("@P_SLOTNO", Slot);
                //        objParams[8] = new SqlParameter("@P_COURSENO", CourseNo);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_FOR_PRE_POST_EXAM_TIMETABLE", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetEmpByStaffno(int patternNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PATTERNNO ", patternNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_ALL_EMP_BY_PATTERNNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }


                public DataSet GetAllExamName(Exam patternNo)
                {
                    throw new NotImplementedException();
                }



                public int deleteExamtimeRecord(int col_Exdtno)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EXDTNO", col_Exdtno);

                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_EXAM_TIME_ENTRY", objParams, true);


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




                public int MarkUpdate(int idNo, string courseno, int sem, string mark, string strings, int session)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        // objParams[0] = new SqlParameter("@P_EXAMNO", lblExamNo);
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_COURSE_STR", courseno);
                        objParams[2] = new SqlParameter("@P_EXAM_NAME", strings);
                        objParams[3] = new SqlParameter("@P_MARKS_STR", mark);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[5] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARK_ENTRY_MIGRATED_DATA", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamName.UpdateExamHead-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int LOCKUpdate(int idno, int sem, string STRING, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        // objParams[0] = new SqlParameter("@P_EXAMNO", lblExamNo);
                        objParams[0] = new SqlParameter("@P_IDNO ", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[2] = new SqlParameter("@P_STRING", STRING);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TR_LOCKUPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamName.LOCKUpdate-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int TrMarkUpdate(string lblRegNo, int totreg, int eran, string result, double sgpa, double cgpa, int IDNO, int SEM, double totmrkobt, int outmark, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        // objParams[0] = new SqlParameter("@P_EXAMNO", lblExamNo);
                        objParams[0] = new SqlParameter("@P_REGNO ", lblRegNo);
                        objParams[1] = new SqlParameter("@P_TOTREG", totreg);
                        objParams[2] = new SqlParameter("@P_EARN", eran);
                        objParams[3] = new SqlParameter("@P_RESULT", result);
                        objParams[4] = new SqlParameter("@P_SGPA", sgpa);
                        objParams[5] = new SqlParameter("@P_CGPA", cgpa);
                        objParams[6] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[7] = new SqlParameter("@P_SEMESTER", SEM);
                        objParams[8] = new SqlParameter("@P_TOTMARKOBT", totmrkobt);
                        objParams[9] = new SqlParameter("@P_OUTOFMARKS", outmark);
                        objParams[10] = new SqlParameter("@p_sessionno", sessionno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TR_MARKUPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamName.UpdateExamHead-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetAllStudentList(string obj, int sessionNo, int collegeNo, int semesterNo, int courseNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FLDNAME", obj);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGEID", collegeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_STUDENTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED by reena on 17_10_16
                //public int UnlockPaperset(int Session, int deptno, int sem)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New ExamName
                //        objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                //        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                //        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[3].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_PAPER_SET_DETAILS", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                //CHANGES DONE ON 20/02/2023 BY SHUBHAM
                public int UnlockPaperset(int SessionId, int deptno, int sem, int CollegeId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONID", SessionId);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[3] = new SqlParameter("@P_COLLEGEID", CollegeId);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_PAPER_SET_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddPaperBillAmount(int uano, int rmno, int noofex, int amount, int session, string remfor, string uaname, string mobno, int deptno, string sbiaccno, string address, string course, string ccode, string bankbranch, string ifsc, string email, string schemetype, string semester, string branch)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[20];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_EXMRUNO", uano);
                        objParams[1] = new SqlParameter("@P_RMNO", rmno);
                        objParams[2] = new SqlParameter("@P_NOOFEXAM", noofex);
                        objParams[3] = new SqlParameter("@P_AMOUNT", amount);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[5] = new SqlParameter("@P_REMFOR", remfor);
                        objParams[6] = new SqlParameter("@P_EXMRNAME", uaname);
                        objParams[7] = new SqlParameter("@P_MOBNO", mobno);
                        objParams[8] = new SqlParameter("@P_EXMNRDEPTNO", deptno);
                        objParams[9] = new SqlParameter("@P_SBIACNO", sbiaccno);
                        objParams[10] = new SqlParameter("@P_ADDRESS", address);
                        if (course != null && course != "")
                            objParams[11] = new SqlParameter("@P_COURSE", course);
                        else
                            objParams[11] = new SqlParameter("@P_COURSE", DBNull.Value);
                        if (ccode != null)
                            objParams[12] = new SqlParameter("@P_CCODE", ccode);
                        else
                            objParams[12] = new SqlParameter("@P_CCODE", DBNull.Value);
                        objParams[13] = new SqlParameter("@P_BANKBRANCH", bankbranch);
                        objParams[14] = new SqlParameter("@P_IFSCCODE", ifsc);
                        objParams[15] = new SqlParameter("@P_EMAIL", email);
                        if (schemetype != null)
                            objParams[16] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                        else
                            objParams[16] = new SqlParameter("@P_SCHEMETYPE", DBNull.Value);
                        if (semester != null)
                            objParams[17] = new SqlParameter("@P_SEMESTERNO", semester);
                        else
                            objParams[17] = new SqlParameter("@P_SEMESTERNO", DBNull.Value);
                        if (branch != null)
                            objParams[18] = new SqlParameter("@P_BRANCH", branch);
                        else
                            objParams[18] = new SqlParameter("@P_BRANCH", DBNull.Value);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACAD_REMUNERTAION_BILL_AMOUNT", objParams, true);


                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //LOCK THE BILL ENTRY
                //Update phd student
                public int LockPaperBillAmount(int uano, int session)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[3];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_EXMRUNO", uano);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACAD_REMUNERTAION_BILL_AMOUNT_LOCK", objParams, true);


                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                public DataSet RemunerationBillExcelReport(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REMUNERATION_NOT_FILL_RPT", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubmitTicketController.add() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                ////public DataSet GetAllSeatPlan(int roomno, int session, int course)
                ////{
                ////    DataSet ds = null;
                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                ////        SqlParameter[] objParams = new SqlParameter[3];
                ////        objParams[0] = new SqlParameter("@P_ROOMNO", roomno);
                ////        objParams[1] = new SqlParameter("@p_SESSION", session);
                ////        objParams[2] = new SqlParameter("@p_COURSENO", course);


                ////        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SEATING_PLAN_CONFIGURATION_FOR_ABSENT_STUDENT", objParams);
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        return ds;
                ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                ////    }
                ////    return ds;
                ////}



                public DataSet GetAllSeatPlan(int roomno, int session, int course)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[1] = new SqlParameter("@p_SESSION", session);
                        objParams[2] = new SqlParameter("@p_COURSENO", course);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SEATING_PLAN_CONFIGURATION_FOR_ABSENT_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetConsolidatedMarksandAttendanceList(int sessionno, int collegeid, int degreeno, int branchno, int schemeno, int semesterno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA_FOR_CONSOLIDATED_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetConsolidatedMarksandAttendanceList-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet UpdateDetailcondonanceBulkDe(string hdnval, string courselist, int sessionno, int degreeno, int branchno, int semesterno, int schemeno, int uano, string ipaddress)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_REGNO", hdnval);
                        objParams[1] = new SqlParameter("@P_COURSELIST", courselist);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_SCHEME", schemeno);
                        objParams[7] = new SqlParameter("@P_UANO", uano);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_DETAIN_RESULT_CONDONANCE", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// added by s.patil on date 12-11-2019
                /// </summary>
                /// <param name="degree"></param>
                /// <param name="Branch"></param>
                /// <param name="shift"></param>
                /// <returns></returns>
                public DataSet GetStudAttendanceSheetData(int session, int degree, int Branch, int sem, int examno, int scheme, int prev, int elect)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Branch);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[4] = new SqlParameter("@P_EXAM_NO", examno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", prev);
                        objParams[7] = new SqlParameter("@P_ELECT", elect);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_ATTENDANCE_SHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetTotalExamDetailsReport-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// added by s.patil on date 12-11-2019
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>
                //public int UpdateSubExam(Exam objExam)
                //{
                //    int status = -99;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //    {                    

                //       new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),
                //       new SqlParameter("@P_SUBEXAMNO", objExam.SubExamNo),
                //       new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                //       new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                //       new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                //       new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                //       new SqlParameter("@P_MAXMARK",objExam.MAXMARKS),
                //       new SqlParameter("@P_OUTPUT",status)
                //    };
                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_UPDATE", sqlParams, true);
                //        status = (Int32)obj;
                //    }
                //    catch (Exception ex)
                //    {
                //        status = -99; //Added By Abhinay Lad [14-06-2019]
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateSubExam() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return status;
                //}
                /// <summary>
                /// Added by S.Patil - 24012020
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>
                public int AddSubExam(Exam objExam, DataTable dtExamPattern)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                      {
                        //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                        new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),
                        new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                        new SqlParameter("@P_SUBEXAM_SUBID", objExam.CourseType),
                        new SqlParameter("@P_BULKSUBEXAMPATTERN" ,dtExamPattern),
                        //new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                        //new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                        //new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                        //new SqlParameter("@P_MAXMARK",objExam.MAXMARKS),
                        new SqlParameter("@P_OUTPUT", status)
                      };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_NAME_INSERT", sqlParams, true);
                        //status = (Int32)obj;
                        if (Convert.ToInt32(obj) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(obj) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        //status = -99;
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddSubExam() --> " + ex.Message + " " + ex.StackTrace);
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }
                /// <summary>
                /// Added by S.Patil - 24012020
                /// </summary>
                /// <param name="subexamno"></param>
                /// <returns></returns>
                /// 
                public int AddSubExam(Exam objExam, int Sub_Fixed)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                      {
                        //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                        new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                        new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                        new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                       // new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                        new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),   
                        new SqlParameter("@P_ORGID", objExam.OrgId),
                        new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                        new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                        new SqlParameter("@P_SUBID", objExam.Subid),
                        new SqlParameter("@P_FIXED",Sub_Fixed),
                        new SqlParameter("@P_OUTPUT", status)

                        
                      };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_NAME_INSERT", sqlParams, true);
                        status = (Int32)obj;

                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateSubExam(Exam objExam, int Sub_Fixed)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                    {                    
                   
                    new SqlParameter("@P_SUBEXAMNO", objExam.SubExamNo),
                    new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                    new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                    new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                    //new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                    new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),
                    new SqlParameter("@P_ORGID", objExam.OrgId),
                    new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                    new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                    new SqlParameter("@P_SUBID", objExam.Subid),
                    new SqlParameter("@P_FIXED",Sub_Fixed),
                    new SqlParameter("@P_OUTPUT",status)
                    
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99; //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public SqlDataReader GetSubExambyNo(int subexamno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", subexamno) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_SUBEXAM_GET_BY_SUBEXAMNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetSubExambyNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }
                /// <summary>
                /// Added by S.Patil on date 24012020
                /// </summary>
                /// <returns></returns>
                public DataSet GetAllSubExam(Exam objExam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO);
                        objParams[1] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SUBEXAM_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                /// <summary>
                /// Added Mahesh On Dated 12/02/2020
                /// </summary>
                /// <returns></returns>
                //public DataSet GetPatternWithExamWiseSubExam(Exam objExam)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO);
                //        objParams[1] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PATTERNWISE_SUBEXAM_HEAD", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetPatternWithExamWiseSubExam() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}
                public DataSet GetPatternWithExamWiseSubExam(Exam objExam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO);
                        objParams[1] = new SqlParameter("@P_EXAMNO", objExam.ExamNo);
                        objParams[2] = new SqlParameter("@P_SUBEXAM_SUBID", objExam.CourseType);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PATTERNWISE_SUBEXAM_HEAD", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PATTERNWISE_SUBEXAM_HEAD_IA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetPatternWithExamWiseSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                /// <summary>
                /// Added By Dileep 
                /// Date:09032020
                /// </summary>
                /// <param name="Sessionno"></param>
                /// <param name="Examno"></param>
                /// <returns></returns>

                public DataSet GetStudentExamTimeTable(int Sessionno, int Examno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[3];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_EXAMNO", Examno);
                        objParam[2] = new SqlParameter("@P_IDNO", idno);
                        ds = objHelp.ExecuteDataSetSP("PKG_GET_STUDENT_EXAMTIMETABLE", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetStudentExamTimeTable()->" + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Deepali G. Dated on 11022021
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="examdate"></param>
                /// <param name="branchno"></param>
                /// <param name="StudPerBundle"></param>
                /// <returns></returns>
                //public DataSet GetBundleNo_SeatNoDetails(int sessionno, int courseno, DateTime examdate, int branchno, int StudPerBundle)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[2] = new SqlParameter("@P_EXAMDATE", examdate);
                //        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                //        objParams[4] = new SqlParameter("@P_BUNDLENO", StudPerBundle);
                //        //  objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BUNDLENO_SEATNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.GetBundleNo_SeatNoDetails-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GetBundleNo_SeatNoDetails(int sessionid, int courseno, DateTime examdate, int branchno, int StudPerBundle)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_BUNDLENO", StudPerBundle);
                        //  objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BUNDLENO_SEATNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.GetBundleNo_SeatNoDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Deepali G. Dated on 11022021
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="examdate"></param>
                /// <param name="branchno"></param>
                /// <param name="StudPerBundle"></param>
                /// <returns></returns>
                //public int CreateBundle(int bundleno, int courseno, string regfrom, string regto, int sessionno, int branchno)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objDataAccess = new SQLHelper(_uaims_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //        { 
                //            new SqlParameter("@P_BUNDLENO", bundleno),
                //            new SqlParameter("@P_COURSENO", courseno),
                //            new SqlParameter("@P_REGNOFROM", regfrom),
                //            new SqlParameter("@P_REGNOTO",regto),
                //            new SqlParameter("@P_SESSIONNO",sessionno),
                //            new SqlParameter("@P_BRANCHNO",branchno),
                //            new SqlParameter("@P_OUT", SqlDbType.Int) 
                //        };
                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                //        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_BUNDLE", sqlParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else if (Convert.ToInt32(ret) == 1)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else if (Convert.ToInt32(ret) == 2)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.Error);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.CreateBundle-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                public int CreateBundle(int bundleno, int courseno, string regfrom, string regto, int sessionid, int branchno, int collegeid, int Count)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_BUNDLENO", bundleno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_REGNOFROM", regfrom),
                            new SqlParameter("@P_REGNOTO",regto),
                            new SqlParameter("@P_SESSIONID",sessionid),
                            new SqlParameter("@P_BRANCHNO",branchno),
                            new SqlParameter("@P_COLLEGEID",collegeid),
                            new SqlParameter("@P_COUNT",Count),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output; object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_BUNDLE", sqlParams, true); if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.CreateBundle-> " + ex.ToString());
                    } return retStatus;
                }

                /// <summary>
                /// Added by Deepali G. Dated on 11022021
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="examdate"></param>
                /// <param name="branchno"></param>
                /// <param name="StudPerBundle"></param>
                /// <returns></returns>
                public DataSet GetOnlineEvalutionReport(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_ONSCREEN_EVALUATION_BUNDLE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Deepali G. Dated on 11022021
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="examdate"></param>
                /// <param name="branchno"></param>
                /// <param name="StudPerBundle"></param>
                /// <returns></returns>
                /// Updated by Shubham B Dated on 20/02/2023
                public int UpdateValuerWithBundle(int sessionId, int Courseno, int bundleno, int valuer, DateTime dtIssue)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONID",sessionId),
                            new SqlParameter("@P_COURSENO", Courseno),
                            new SqlParameter("@P_BUNDLENO", bundleno),
                            new SqlParameter("@P_UA_NO", valuer),
                            new SqlParameter("@P_ISSUE_DATE",dtIssue),
                            new SqlParameter("@P_OUT",SqlDbType.Int) 
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_VALUER_TO_BUNDLE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.UpdateValuerWithBundle-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Deepali G. Dated on 11022021
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="examdate"></param>
                /// <param name="branchno"></param>
                /// <param name="StudPerBundle"></param>
                /// <returns></returns>
                /// Updated by Shubham B Dated on 20/02/2023
                public DataSet GetValuerInfoWithBundleNo(int sessionId, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        //objParams[2] = new SqlParameter("@P_BUNDLENO", bundleno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_VALUER_TO_BUNDLE_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PreExamController.GetValuerInfoWithBundleNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDateWiseTimeTable(int SessionNo, DateTime FromDate, DateTime Todate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[] {
                      new SqlParameter("@P_SESSIONNO", SessionNo),
                      new SqlParameter("@P_FROMDATE", FromDate),
                      new SqlParameter("@P_TODATE", Todate)
                    };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_DATE_WISE_TIME_TABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.RetrieveStudentAttDetailsExcel-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Dileep Kare
                /// Date:05.08.2021
                /// </summary>
                /// <param name="Sessionno"></param>
                /// <param name="Examno"></param>
                /// <returns></returns>

                public DataSet GetFacultyExamTimeTable(int Sessionno, int Examno, int ua_no, int Schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_EXAMNO", Examno);
                        objParam[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParam[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        ds = objHelp.ExecuteDataSetSP("PKG_GET_FACULTY_EXAMTIMETABLE", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetFacultyExamTimeTable()->" + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By S.P -09082021
                /// </summary>
                /// <param name="Sessionno"></param>
                /// <param name="ua_no"></param>
                /// <returns></returns>
                public DataSet GetExamTimeTableDashboard(int Sessionno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[2];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objHelp.ExecuteDataSetSP("PKG_GET_FACULTY_EXAMTIMETABLE_DASHBOARD", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetExamTimeTableDashboard()->" + ex.ToString());
                    }
                    return ds;
                }
                // ONLINE FEES PAYMENT METHODS ADDED BY NARESH BEERLA ON 23112021
                public DataSet GetStudentDetail(int Idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_ONLINE_PAYMENT_WITHOUT_FEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Examination Rules Added BY Sneha G on 01/01/2022

                //public DataSet GetCourseExamRule(int sessionno, int schemeno, int semno, int Subid)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                //        objParams[3] = new SqlParameter("@P_SUBID", Subid);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_RULES_COURSE_WISE", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRule-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //public int AddCourseExamRule(StudentRegist objSR, int OrgId)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[16];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                //        objParams[1] = new SqlParameter("@P_DEGREENO", objSR.DEGREENO);
                //        objParams[2] = new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO);
                //        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                //        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //        objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENNO.Remove(objSR.COURSENNO.Length - 1, 1));
                //        objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODE.Remove(objSR.CCODE.Length - 1, 1));
                //        objParams[7] = new SqlParameter("@P_COURSENAME", objSR.COURSENAME.Remove(objSR.COURSENAME.Length - 1, 1));
                //        objParams[8] = new SqlParameter("@P_EXAMNO", objSR.CATEGORY3.Remove(objSR.CATEGORY3.Length - 1, 1));
                //        objParams[9] = new SqlParameter("@P_RULE1", objSR.Rule11.Remove(objSR.Rule11.Length - 1, 1));
                //        objParams[10] = new SqlParameter("@P_RULE2", objSR.Rule22.Remove(objSR.Rule22.Length - 1, 1));
                //        objParams[11] = new SqlParameter("@P_ISLOCK", objSR.START_NO);
                //        objParams[12] = new SqlParameter("@P_ORGID", OrgId);
                //        objParams[13] = new SqlParameter("@P_OTYPE", objSR.USERTTYPE);
                //        objParams[14] = new SqlParameter("@P_SUBID", objSR.SUBID);
                //        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[15].Direction = ParameterDirection.Output;
                //        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_RULE", objParams, true);
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_INSERT", objParams, true);
                //        retStatus = (int)ret;
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseExamRule-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                public int UpdateExamHead(string fldName, string examName, int Patternno, int ExamType, int orgid, bool Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        // objParams[0] = new SqlParameter("@P_EXAMNO", lblExamNo);
                        objParams[0] = new SqlParameter("@P_FLDNAME ", fldName);
                        objParams[1] = new SqlParameter("@P_EXAMNAME", examName);
                        objParams[2] = new SqlParameter("@P_PATTERNNO", Patternno);
                        objParams[3] = new SqlParameter("@p_EXAMTYPE", ExamType);
                        objParams[4] = new SqlParameter("@P_ORGID", orgid);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_EXAMHEAD_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamName.UpdateExamHead-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //public int AddSubExam(Exam objExam)
                //{
                //    int status = -99;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //      {
                //        //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                //        new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                //        new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                //        new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                //        new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                //        new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),   
                //        new SqlParameter("@P_ORGID", objExam.OrgId),
                //        new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                //        new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                //        new SqlParameter("@P_OUTPUT", status)


                //      };
                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_NAME_INSERT", sqlParams, true);
                //        status = (Int32)obj;

                //    }
                //    catch (Exception ex)
                //    {
                //        status = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddSubExam() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return status;
                //}





                //public int UpdateSubExam(Exam objExam)
                //{
                //    int status = -99;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //    {                    

                //    new SqlParameter("@P_SUBEXAMNO", objExam.SubExamNo),
                //    new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                //    new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                //    new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                //    new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                //    new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),
                //    new SqlParameter("@P_ORGID", objExam.OrgId),
                //    new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                //     new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                //    new SqlParameter("@P_OUTPUT",status)

                //    };
                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_UPDATE", sqlParams, true);
                //        status = (Int32)obj;
                //    }
                //    catch (Exception ex)
                //    {
                //        status = -99; //Added By Abhinay Lad [14-06-2019]
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateSubExam() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return status;
                //}



                public DataSet GetAllSubExam()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SUBEXAM_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Exam Time Table Slot
                public int AddExamSlot(string slotname, string timefrom, string timeto, string colcode, int OrgID, bool Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[1] = new SqlParameter("@P_TIMEFROM", timefrom);
                        objParams[2] = new SqlParameter("@P_TIMETO", timeto);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[4] = new SqlParameter("@P_ORG_ID", OrgID);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        objParams[6] = new SqlParameter("@P_SLOTNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_TT_SLOT_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamSlot -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateExamSlot(int slotno, string slotname, string timefrom, string timeto, int OrgID, bool Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[1] = new SqlParameter("@P_SLOTNAME", slotname);
                        objParams[2] = new SqlParameter("@P_TIMEFROM", timefrom);
                        objParams[3] = new SqlParameter("@P_TIMETO", timeto);
                        objParams[4] = new SqlParameter("@P_ORG_ID", OrgID);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_TT_SLOT_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateExamSlot -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetSingleExamSlot(int slotno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_EXAM_TT_SLOT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSingleExamSlot -> " + ex.ToString());
                //    }
                //    return ds;
                //}
                #endregion

                #region Examination Rules Added BY Sneha G on 01/01/2022

                //public DataSet GetCourseExamRule(int sessionno, int schemeno, int semno, int Subid)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                //        SqlParameter[] objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                //        objParams[3] = new SqlParameter("@P_SUBID", Subid);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_RULES_COURSE_WISE", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRule-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //public int AddCourseExamRule(StudentRegist objSR, int OrgId)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[16];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                //        objParams[1] = new SqlParameter("@P_DEGREENO", objSR.DEGREENO);
                //        objParams[2] = new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO);
                //        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                //        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //        objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENNO.Remove(objSR.COURSENNO.Length - 1, 1));
                //        objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODE.Remove(objSR.CCODE.Length - 1, 1));
                //        objParams[7] = new SqlParameter("@P_COURSENAME", objSR.COURSENAME.Remove(objSR.COURSENAME.Length - 1, 1));
                //        objParams[8] = new SqlParameter("@P_EXAMNO", objSR.CATEGORY3.Remove(objSR.CATEGORY3.Length - 1, 1));
                //        objParams[9] = new SqlParameter("@P_RULE1", objSR.Rule11.Remove(objSR.Rule11.Length - 1, 1));
                //        objParams[10] = new SqlParameter("@P_RULE2", objSR.Rule22.Remove(objSR.Rule22.Length - 1, 1));
                //        objParams[11] = new SqlParameter("@P_ISLOCK", objSR.START_NO);
                //        objParams[12] = new SqlParameter("@P_ORGID", OrgId);
                //        objParams[13] = new SqlParameter("@P_OTYPE", objSR.USERTTYPE);
                //        objParams[14] = new SqlParameter("@P_SUBID", objSR.SUBID);
                //        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[15].Direction = ParameterDirection.Output;
                //        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_RULE", objParams, true);
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_INSERT", objParams, true);
                //        retStatus = (int)ret;
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseExamRule-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddExamDay(Exam objExam, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[7] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[8] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[10] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[11] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[13] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTransactionEntryList(int sessionno, int semno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);



                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TRANS_APPLY_LIST", objParams);



                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetTransactionEntryList-> " + ex.ToString());
                    }



                    return ds;
                }
                #endregion
                #region Exam Transaction Details By Sneha G added on 18/01/2022

                public SqlDataReader GetStudDetailsForExamTransaction(int idno, int orgid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ORGID", orgid);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_EXAM_TRANSACTION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.GetStudDetailsForExamTransaction-> " + ex.ToString());
                    }
                    return dr;
                }


                public int AddExamTransactionDetails(Exam objSR, int ua_type)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_IDNO", objSR.Idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.Sessionno);
                        objParams[2] = new SqlParameter("@P_TRANSACTION_NO", objSR.Transaction_no);
                        objParams[3] = new SqlParameter("@P_TRANS_DATE", objSR.Transaction_date);
                        objParams[4] = new SqlParameter("@P_TRANSACTION_AMT", objSR.trans_amt);
                        objParams[5] = new SqlParameter("@P_DOC_NAME", objSR.file_name);
                        objParams[6] = new SqlParameter("@P_DOC_PATH", objSR.file_path);
                        objParams[7] = new SqlParameter("@P_APPROVAL_STATUS", objSR.Approvedstatus);
                        objParams[8] = new SqlParameter("@P_APPROVED_BY", objSR.Approvedby);
                        objParams[9] = new SqlParameter("@P_REMARK", objSR.Remark);
                        objParams[10] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[11] = new SqlParameter("@P_ORGID", objSR.OrgId);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXAM_TRANSACTION_DETAILS", objParams, true);
                        retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddExamTransactionDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                public DataSet GetAllFeeDef(int Sessionno, int ClgId, int UA_SECTION)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                        { 
                             new SqlParameter("@P_SESSIONNO",Sessionno),
                             new SqlParameter("@P_CLGID ",ClgId),
                             new SqlParameter("@UA_SECTION", UA_SECTION),
                        };
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_EXAM_FEE_DEFINATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllFeeDef()->" + ex.ToString());
                    }
                    return ds;
                }






                public int AddFeeEntry(int Subid, String Subname, int regular, int Backlog, int OrgId, int ClgId, int Sessionno, int Sectionno, int Subid_new)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                              {
                             new SqlParameter("@P_SubId",Subid),
                             new SqlParameter("@P_SubName", Subname),
                             new SqlParameter("@P_Regular", regular),
                             new SqlParameter("@P_Backlog", Backlog),
                            new SqlParameter("@P_OrgId", OrgId),
                               new SqlParameter("@P_CLGID ", ClgId),
                            new SqlParameter("@P_SESSIONNO", Sessionno),
                             new SqlParameter("@UA_SECTION ", Sectionno),
                              new SqlParameter("@P_SubId_new ", Subid_new),
                             new SqlParameter("@P_OUT", status)
                              };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_FEE_DEFINATION_SAVE ", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)

                            status = Convert.ToInt32(CustomStatus.RecordUpdated);

                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);




                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.AddfeeEntry()->" + ex.ToString());
                    }

                    return status;


                }

                //added by prafull

                //public int Add_ExamConfiguration(int examrule, int garcerule ,int latefee ,int improvement  ,int exampattern ,int revaluation,int result,int condonation,int feetype )
                //public int Add_ExamConfiguration(int examrule, int garcerule, int latefee, int improvement, int exampattern, int revaluation, int result, int condonation, int feetype, int passrule, int examreg)
                //{
                //    int status = 0;
                //    try
                //    {


                //        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParam = new SqlParameter[11];
                //        objParam[0] = new SqlParameter("@P_EXAM_RULE", examrule);
                //        objParam[1] = new SqlParameter("@P_GRACE_RULE", garcerule);
                //        objParam[2] = new SqlParameter("@P_LATE_FEE", latefee);
                //        objParam[3] = new SqlParameter("@P_IMPROVEMENT", improvement);
                //        objParam[4] = new SqlParameter("@P_EXAM_PATTERN", exampattern);
                //        objParam[5] = new SqlParameter("@P_REVALUATION", revaluation);
                //        objParam[6] = new SqlParameter("@P_RESULT_OTP", result);
                //        objParam[7] = new SqlParameter("@P_CONDONATION", condonation);
                //        objParam[8] = new SqlParameter("@P_FEE_TYPE", feetype);
                //        objParam[9] = new SqlParameter("@P_PASS_RULE", passrule);
                //        objParam[10] = new SqlParameter("@P_EXAM_REG", examreg);


                //        //objParam[objParam.Length - 1].Direction = ParameterDirection.InputOutput;

                //        object obj = objHelp.ExecuteNonQuerySP("PKG_INS_EXAM_CONFIGURATION", objParam, true);
                //        //object obj = objHelp.ExecuteScalarSP("PKG_INS_EXAM_CONFIGURATION", objParam);

                //        if (obj != null)
                //            status = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else
                //            status = Convert.ToInt32(CustomStatus.Error);
                //    }
                //    catch (Exception ex)
                //    {
                //        status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return status;
                //}
                //added the temp parameter because controller contains the two functions with the same name and parameters
                public int Add_ExamConfiguration(int examrule, int garcerule, int latefee, int improvement, int exampattern, int revaluation, int result, int condonation, int feetype, int passrule, int examreg, int decode, int seat, int temp)
                {
                    int status = 0;
                    try
                    {


                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[13];
                        objParam[0] = new SqlParameter("@P_EXAM_RULE", examrule);
                        objParam[1] = new SqlParameter("@P_GRACE_RULE", garcerule);
                        objParam[2] = new SqlParameter("@P_LATE_FEE", latefee);
                        objParam[3] = new SqlParameter("@P_IMPROVEMENT", improvement);
                        objParam[4] = new SqlParameter("@P_EXAM_PATTERN", exampattern);
                        objParam[5] = new SqlParameter("@P_REVALUATION", revaluation);
                        objParam[6] = new SqlParameter("@P_RESULT_OTP", result);
                        objParam[7] = new SqlParameter("@P_CONDONATION", condonation);
                        objParam[8] = new SqlParameter("@P_FEE_TYPE", feetype);
                        objParam[9] = new SqlParameter("@P_PASS_RULE", passrule);
                        objParam[10] = new SqlParameter("@P_EXAM_REG", examreg);
                        objParam[11] = new SqlParameter("@P_DECODE_NUMBER", decode);
                        objParam[12] = new SqlParameter("@P_SEAT_NUMBER", seat);


                        //objParam[objParam.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objHelp.ExecuteNonQuerySP("PKG_INS_EXAM_CONFIGURATION", objParam, true);
                        //object obj = objHelp.ExecuteScalarSP("PKG_INS_EXAM_CONFIGURATION", objParam);

                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public int Add_SubjectWisePassingRule(string subid, string subname, string internalm, string external)
                {
                    int status = 0;
                    try
                    {


                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SUBID", subid);
                        objParam[1] = new SqlParameter("@P_SUBNAME", subname);
                        objParam[2] = new SqlParameter("@P_INTERNAL_MARK", internalm);
                        objParam[3] = new SqlParameter("@P_EXTERNAL_MARK", external);


                        //objParam[objParam.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objHelp.ExecuteNonQuerySP("PKG_INS_SUBJECT_WISE_PASSING_RULE", objParam, true);
                        //object obj = objHelp.ExecuteScalarSP("PKG_INS_EXAM_CONFIGURATION", objParam);

                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw;
                        // throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
                public DataSet GetSubjectType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUBJECT_TYPE", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }


                #region Examination Rules Added BY Sneha G on 01/01/2022

                public DataSet GetCourseExamRule(int sessionno, int schemeno, int semno, int Subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                        objParams[3] = new SqlParameter("@P_SUBID", Subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_RULES_COURSE_WISE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRule-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddCourseExamRule(StudentRegist objSR, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[16];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objSR.DEGREENO);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENNO.Remove(objSR.COURSENNO.Length - 1, 1));
                        objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODE.Remove(objSR.CCODE.Length - 1, 1));
                        objParams[7] = new SqlParameter("@P_COURSENAME", objSR.COURSENAME.Remove(objSR.COURSENAME.Length - 1, 1));
                        objParams[8] = new SqlParameter("@P_EXAMNO", objSR.CATEGORY3.Remove(objSR.CATEGORY3.Length - 1, 1));
                        objParams[9] = new SqlParameter("@P_RULE1", objSR.Rule11.Remove(objSR.Rule11.Length - 1, 1));
                        objParams[10] = new SqlParameter("@P_RULE2", objSR.Rule22.Remove(objSR.Rule22.Length - 1, 1));
                        objParams[11] = new SqlParameter("@P_ISLOCK", objSR.START_NO);
                        objParams[12] = new SqlParameter("@P_ORGID", OrgId);
                        objParams[13] = new SqlParameter("@P_OTYPE", objSR.USERTTYPE);
                        objParams[14] = new SqlParameter("@P_SUBID", objSR.SUBID);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_RULE", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_INSERT", objParams, true);
                        retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseExamRule-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSubexamheader(int OrgId, int Schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrgId);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SUBEXAM_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRule-> " + ex.ToString());
                    }

                    return ds;
                }

                #endregion

                public int AddSubExam(Exam objExam)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                      {
                        //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                        new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                        new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                        new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                       // new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                        new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),   
                        new SqlParameter("@P_ORGID", objExam.OrgId),
                        new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                        new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                        new SqlParameter("@P_SUBID", objExam.Subid),
                        new SqlParameter("@P_OUTPUT", status)

                        
                      };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_NAME_INSERT", sqlParams, true);
                        status = (Int32)obj;

                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public int UpdateSubExam(Exam objExam)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                    {                    
                   
                    new SqlParameter("@P_SUBEXAMNO", objExam.SubExamNo),
                    new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode),
                    new SqlParameter("@P_EXAMNO", objExam.ExamNo),
                    new SqlParameter("@P_SUBEXAM_NAME",objExam.SubExamname),
                    //new SqlParameter("@P_FIELD_NAME",objExam.FieldName),
                    new SqlParameter("@P_PATTERNNO", objExam.PATTERNNO),
                    new SqlParameter("@P_ORGID", objExam.OrgId),
                    new SqlParameter("@P_STATUS",objExam.ActiveStatus),
                    new SqlParameter("@P_MAX_MARK", objExam.MAXMARKS),
                    new SqlParameter("@P_SUBID", objExam.Subid),
                    new SqlParameter("@P_OUTPUT",status)
                    
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SUBEXAM_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99; //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.UpdateSubExam() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public DataSet GetAbsentEntryDetails(int Sessionno, int Semesterno, int Courseno, int Schemeno, string ExamName, string SubExamName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParam[2] = new SqlParameter("@P_COURSENO", Courseno);
                        objParam[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParam[4] = new SqlParameter("@P_EXAM", ExamName);
                        objParam[5] = new SqlParameter("@P_SUB_EXAM", SubExamName);

                        ds = objHelp.ExecuteDataSetSP("PKG_ACD_GET_ABSENT_STUD_LIST_INEXAM", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAbsentEntryDetails()->" + ex.ToString());
                    }
                    return ds;
                }


                public int GetUpdateAbsentEntry(int Sessionno, int Schemeno, int Semesterno, int Courseno, int IDNO)
                {
                    //DataSet ds = null;
                    int ret = 0;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParam[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParam[3] = new SqlParameter("@P_COURSENO", Courseno);
                        objParam[4] = new SqlParameter("@P_IDNO", IDNO);
                        objParam[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[5].Direction = ParameterDirection.Output;
                        //ds = objHelp.ExecuteDataSetSP("PKG_ACD_UPDATE_ABSENT_STUD_LOG", objParam);

                        ret = Convert.ToInt32(objHelp.ExecuteNonQuerySP("PKG_ACD_UPDATE_ABSENT_STUD_LOG", objParam, true));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetUpdateAbsentEntry()->" + ex.ToString());
                    }
                    return ret;
                }

                public int AddExamTransactionDetails_Admin(int idno, int sessionno, int Approvedstatus, int approveby, string remark, int orgid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_APPROVAL_STATUS", Approvedstatus);
                        objParams[3] = new SqlParameter("@P_APPROVED_BY", approveby);
                        objParams[4] = new SqlParameter("@P_REMARK", remark);
                        objParams[5] = new SqlParameter("@P_ORGID", orgid);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXAM_TRANSACTION_DETAILS_ADMIN", objParams, true);
                        retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddExamTransactionDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourses(int schemeno, int semesterno, int sessionno, int College_id, int Subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[4] = new SqlParameter("@P_SUBID", Subid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //------------------------------------------------------------------------------

                public int AddBundleCreationOpenElec(int collegeno, int sessionno, string date, int courseno, int roomno, string bundleNo, string idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_College", collegeno);
                        objParams[1] = new SqlParameter("@P_Session", sessionno);
                        objParams[2] = new SqlParameter("@P_Date", date);
                        objParams[3] = new SqlParameter("@P_Course", courseno);
                        objParams[4] = new SqlParameter("@P_Room", roomno);
                        objParams[5] = new SqlParameter("@P_BUNDLENO", bundleNo);
                        objParams[6] = new SqlParameter("@P_IDNOS", idno);

                        //objParams[4].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_INSERT_BUNDLENO", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-------------------------------------------------------------------------------------

                public DataSet GetFacultyForAssignBundle(int SESSIONNO, int COURSENO, string BUNDLENO, string LETTERTYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[2] = new SqlParameter("@P_BUNDLENAME", BUNDLENO);
                        objParams[3] = new SqlParameter("@P_LETTERTYPE", LETTERTYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTY_FOR_ASSINGBUNDLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetEnvelope-> " + ex.ToString());
                    }

                    return ds;
                }
                //public int AddExamTransactionDetails_Admin(int idno, int sessionno, int Approvedstatus, int approveby, string remark, int orgid)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[7];

                //        objParams[0] = new SqlParameter("@P_IDNO", idno);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[2] = new SqlParameter("@P_APPROVAL_STATUS", Approvedstatus);
                //        objParams[3] = new SqlParameter("@P_APPROVED_BY", approveby);
                //        objParams[4] = new SqlParameter("@P_REMARK", remark);
                //        objParams[5] = new SqlParameter("@P_ORGID", orgid);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_EXAM_TRANSACTION_DETAILS_ADMIN", objParams, true);
                //        retStatus = (int)ret;
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddExamTransactionDetails-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                //---------------------------------------------------------------------------------------------------
                public int AssignBundleToFaculty(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate, string bundletype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        objParams[6] = new SqlParameter("@P_BUNDLETYPE", bundletype);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_VALUER", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-----------------------------------------------------------------------------------------
                public int AssignBundleToModerator(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_MODERATOR", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-------------------------------------------------------------------------------
                public int AssignBundleToScrutinizer(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_SCRUTINIZER", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet CheckDate(Exam objExam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            
                            //SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                            //SqlParameter[] objParams = new SqlParameter[5];
                        {
                            new SqlParameter("@P_Date", objExam.Examdate),
                            new SqlParameter("@P_SEMESTERNO",objExam.SemesterNo),
                            new SqlParameter("@P_SLOTNO",objExam.Slot),  
                            new SqlParameter("@P_SESSIONNO",objExam.SessionNo)
                        };
                        //  ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_TIMETABLE_CHECKDATE", objParams);          //PKG_ACD_EXAM_CHECKDATE
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_CHECKDATE", objParams);
                        return ds;
                    }
                    catch (Exception ex)
                    {
                        // this._uaims_constr = null;                          
                        ds = null;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ExamSchedulingController.PKG_ACD_EXAM_TIMETABLE_CHECKDATE() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    finally
                    {
                        //objSQLHelper = null;
                        // objParams = null;
                        ds = null;
                    }

                }

                public int FeeStructureCourse(int rdl, int degreeno, int SUBID, String SubjectName, String FeeAmt)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_FEETYPE",rdl),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SUBID", SUBID),
                    new SqlParameter("@P_SubjectName", SubjectName),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_OUT", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_ENTRY_COURSE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeStructureCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;




                }
                public int FeeStructureSemester(int rdl, int degreeno, int Semesterno, String FeeAmt)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_FEETYPE",rdl),
                    new SqlParameter("@P_DEGREENO", degreeno),
                //  new SqlParameter("@P_SUBID", SUBID),
                    new SqlParameter("@P_SEMESTERNO", Semesterno),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_OUT", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_ENTRY_SEMESTER", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeStructureSemester() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;




                }
                public DataSet GetFeeStructureCourse(int rdl, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FEETYPE", rdl);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_FEE_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetFeeStructure-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetFeeStructureSemester(int rdl, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FEETYPE", rdl);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);



                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_FEE_ENTRY_SEM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetFeeStructure-> " + ex.ToString());
                    }

                    return ds;
                }
                public int FeeRedoRegistration(string RedoFee)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REDOFEE",RedoFee),
                   
                    new SqlParameter("@P_OUT", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_REDO_REG_FEE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeRedoRegistration() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;




                }

                public DataSet GetStudentCATMarksForSMS(int sessionno, int degreeno, int branchno, int semesterno, int sectionno, string examname)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_MARK", examname);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_STUDENT_CATMARKS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetCourseExamRuleSubjectwise(int sessionno, int schemeno, int semno, int Subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                        objParams[3] = new SqlParameter("@P_SUBID", Subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_RULES_COURSE_WISE_subject_type_wise", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRuleSubjectwise-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddCourseExamRuleforcoursewise(StudentRegist objSR, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[16];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objSR.DEGREENO);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENNO.Remove(objSR.COURSENNO.Length - 1, 1));
                        objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODE.Remove(objSR.CCODE.Length - 1, 1));
                        objParams[7] = new SqlParameter("@P_COURSENAME", objSR.COURSENAME.Remove(objSR.COURSENAME.Length - 1, 1));
                        objParams[8] = new SqlParameter("@P_EXAMNO", objSR.CATEGORY3.Remove(objSR.CATEGORY3.Length - 1, 1));
                        objParams[9] = new SqlParameter("@P_RULE1", objSR.Rule11.Remove(objSR.Rule11.Length - 1, 1));
                        objParams[10] = new SqlParameter("@P_RULE2", objSR.Rule22.Remove(objSR.Rule22.Length - 1, 1));
                        objParams[11] = new SqlParameter("@P_ISLOCK", objSR.START_NO);
                        objParams[12] = new SqlParameter("@P_ORGID", OrgId);
                        objParams[13] = new SqlParameter("@P_OTYPE", objSR.USERTTYPE);
                        objParams[14] = new SqlParameter("@P_SUBID", objSR.SUBID);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_RULE", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_INSERT_FOR_COMMON_CODE", objParams, true);
                        retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseExamRule-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int Add_ExamConfiguration(int examrule, int garcerule, int latefee, int improvement, int exampattern, int revaluation, int result, int condonation, int feetype, int passrule, int examreg, int markentry_pattern, int orgid)
                {
                    int status = 0;
                    try
                    {


                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[13];
                        objParam[0] = new SqlParameter("@P_EXAM_RULE", examrule);
                        objParam[1] = new SqlParameter("@P_GRACE_RULE", garcerule);
                        objParam[2] = new SqlParameter("@P_LATE_FEE", latefee);
                        objParam[3] = new SqlParameter("@P_IMPROVEMENT", improvement);
                        objParam[4] = new SqlParameter("@P_EXAM_PATTERN", exampattern);
                        objParam[5] = new SqlParameter("@P_REVALUATION", revaluation);
                        objParam[6] = new SqlParameter("@P_RESULT_OTP", result);
                        objParam[7] = new SqlParameter("@P_CONDONATION", condonation);
                        objParam[8] = new SqlParameter("@P_FEE_TYPE", feetype);
                        objParam[9] = new SqlParameter("@P_PASS_RULE", passrule);
                        objParam[10] = new SqlParameter("@P_EXAM_REG", examreg);
                        objParam[11] = new SqlParameter("@P_MARK_ENTRY", markentry_pattern);

                        objParam[12] = new SqlParameter("@P_ORG_ID", orgid);


                        //objParam[objParam.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objHelp.ExecuteNonQuerySP("PKG_INS_EXAM_CONFIGURATION", objParam, true);
                        //object obj = objHelp.ExecuteScalarSP("PKG_INS_EXAM_CONFIGURATION", objParam);

                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAbsentEntryDetailsByAdmin(int Sessionno, int collegeid, int schemeno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParam[1] = new SqlParameter("@P_COLLEGEID", collegeid);
                        objParam[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParam[3] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParam[4] = new SqlParameter("@P_EXAM", ExamName);
                        //objParam[5] = new SqlParameter("@P_SUB_EXAM", SubExamName);

                        ds = objHelp.ExecuteDataSetSP("PKG_ACD_GET_ABSENT_STUD_LIST_INEXAM_ADMIN", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAbsentEntryDetails()->" + ex.ToString());
                    }
                    return ds;
                }
                //Added By Tejas Thakre on 24-08-2022
                public DataSet GetStudentByExam(int Schemeno, int Sessiono, int Semester, int Courseno, string AbIGEntry)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessiono);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", Semester);
                        objParams[2] = new SqlParameter("@P_COUSRENO", Courseno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParams[4] = new SqlParameter("@P_ABIGENTRY", AbIGEntry);
                        //objParams[5] = new SqlParameter("@P_EXAMNO", Examno);
                        //objParams[6] = new SqlParameter("@P_SUBEXAMNO", Subexamno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_BY_EXAM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRuleSubjectwise-> " + ex.ToString());
                    }

                    return ds;
                }


                //Added By Tejas Thakre on 25-08-2022
                public int UpdateStudentByGrade(int Schemeno, int Sessionno, int Semester, int Courseno, string Ccode, string Idno, string ABgrade, string Igrade, string OldGrade, int UA_NO)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objHelp = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", Semester);
                        objParams[3] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[4] = new SqlParameter("@P_CCODE", Ccode);
                        objParams[5] = new SqlParameter("@P_IDNO", Idno);
                        objParams[6] = new SqlParameter("@P_ABGrade", ABgrade);
                        objParams[7] = new SqlParameter("@P_IGrade", Igrade);
                        objParams[8] = new SqlParameter("@P_OLD_GRADE", OldGrade);
                        objParams[9] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object obj = objHelp.ExecuteNonQuerySP("PKG_ACD_UPDENT_STUDENT_BY_GRADE", objParams, true);

                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseExamRuleSubjectwise-> " + ex.ToString());
                    }

                    return status;
                }

                public int AddExamDay(Exam objExam, int OrgID, int Modeexam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[7] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[8] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[10] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[11] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[13] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[14] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetExamRegStud(int schemeno, int semesterno, int sessionno, int Examreg)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_STUD_EXAM_REGISTERED", Examreg);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_REGISTER_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int GetViewOnStudentLock(int examtype, int semesterno, int sessionno, int Schemano, int Subid)
                {
                    int retStatus = 0;
                    // DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", Schemano);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[4] = new SqlParameter("@P_SUBID", Subid);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_VIEW_STUDENT_LOG_EXAM_DATE", objParams);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        //retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetViewOnStudentLock-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // added by ashish dt on 030102022

                #region
                //added for Exam_Fee_Config Page


                public int FeeCredit(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string Creditfee, int FeesStructure, string Semname)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CREDITFEE", Creditfee),
                     new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                      new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_CREDIT", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                //FeeCourse
                public int FeeCourse(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string CourseFee, int FeesStructure, string Semname)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CourseFee", CourseFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COURSE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                public int FeeConfig(int College, int Session, int ExamType, int SUBID, string SubjectName, string FeeAmt, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, int FeesStructure, string Semname)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_SUBID", SUBID),
                    new SqlParameter("@P_SubjectName", SubjectName),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                     new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COFIG", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                //NEW 03102022

                public SqlDataReader GetFeeDetails(int FID)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@F_ID", FID) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_FEE_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetFeeDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }



                public DataSet GetFeeConfig()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[0];


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_FEE_SHOW", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetFeeConfig-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetFeeConfig(int College, int degreeno, int ExamType, int Sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_FEETYPE", ExamType);
                        objParams[3] = new SqlParameter("@P_Sessionno", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_FEE_CONFIG", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetFeeStructure-> " + ex.ToString());
                    }

                    return ds;
                }





                #endregion





                //public DataSet GetAdminExamTimeTable(int CollegeScheme, int Sessionno, int Examno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParam = new SqlParameter[3];
                //        objParam[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                //        objParam[1] = new SqlParameter("@P_EXAMNO", Examno);
                //        //objParam[2] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParam[2] = new SqlParameter("@P_SCHEMENO", CollegeScheme);
                //        ds = objHelp.ExecuteDataSetSP("PKG_GET_ADMIN_EXAMTIMETABLE", objParam);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetFacultyExamTimeTable()->" + ex.ToString());
                //    }
                //    return ds;
                //}


                //Added dt on 09112022
                public int FeeDelete(int ClgId, int Sessionno, int FeeType, int Degreeno, int FeeStru)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",ClgId),
                    new SqlParameter("@P_SESSIONNO", Sessionno),
                    new SqlParameter("@P_FEETYPE", FeeType),
                    new SqlParameter("@P_DEGREENO", Degreeno),
                    new SqlParameter("@P_FEESTRUCTURE", FeeStru),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_CANCEL_FEE_DEFINATION", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeDelete() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }
                public int FeeCredit(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string Creditfee, int FeesStructure, string Semname, int userno)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CREDITFEE", Creditfee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_CREDIT", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }

                public int FeeCourse(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string CourseFee, int FeesStructure, string Semname, int userno)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_CourseFee", CourseFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                     new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COURSE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }

                public int FeeConfig(int College, int Session, int ExamType, int SUBID, string SubjectName, string FeeAmt, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, int FeesStructure, string Semname, int userno)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_SUBID", SUBID),
                    new SqlParameter("@P_SubjectName", SubjectName),
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                     new SqlParameter("@P_SEMNAME", Semname),
                     new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_FEE_DEFINATION_COFIG", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                public DataSet GetSemFeeConfig(int College, int degreeno, int ExamType, int Sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_FEETYPE", ExamType);
                        objParams[3] = new SqlParameter("@P_Sessionno", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FEE_DEFINATION_CONFIG_SEM", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetSemFeeConfig-> " + ex.ToString());
                    }

                    return ds;
                }


                public int SemFeeConfig(int College, int Session, int ExamType, string TempSem, String Semestername, string FeeAmt, int degreeno, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, int FeesStructure, int userno)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                 
                    new SqlParameter("@P_FeeAmt", FeeAmt),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_TEMPSEM", TempSem),
                      new SqlParameter("@P_SEMNAME", Semestername),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACD_FEE_DEFINATION_CONFIG_SEM", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.FeeConfig() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }


                public int CreditRange(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string MINRANGE, string MAXRANGE, string AMOUNT, int FeesStructure, string Semname, int userno)
                {

                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_MINRANGE", MINRANGE),
                    new SqlParameter("@P_MAXRANGE", MAXRANGE),
                     new SqlParameter("@P_AMOUNT", AMOUNT),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEE_DEFINATION_CREDITRANGE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.CreditRange() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;



                }

                //Added by Lalit G dt on 09112022
                public DataSet GetIDGRADESYSTEM(int COS, int SCHEMANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_level", COS);
                        objParams[1] = new SqlParameter("@P_SCHEMANO", SCHEMANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ID_DIRECT_GRADE_SYSTEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Add_DirectGradeSystem(int Schemano, int level, int Grade, decimal mini, decimal max, int grandpoint, string indicator, int active)
                {
                    int status = 0;
                    try
                    {


                        SQLHelper objHelp = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[8];

                        objParam[0] = new SqlParameter("@P_SCHEMANO", Schemano);
                        objParam[1] = new SqlParameter("@P_LEVELNO", level);
                        objParam[2] = new SqlParameter("@P_GRADENO", Grade);
                        objParam[3] = new SqlParameter("@P_MINIRANGE", mini);
                        objParam[4] = new SqlParameter("@P_MAXIRANGE", max);
                        objParam[5] = new SqlParameter("@P_GRADEPOINT", grandpoint);
                        objParam[6] = new SqlParameter("@P_INDICATOR", indicator);
                        objParam[7] = new SqlParameter("@P_ACTIVESTATUS", active);
                        object obj = objHelp.ExecuteNonQuerySP("PKG_INS_DIRECT_GRADE_SYSTEM", objParam, true);
                        if (obj != null)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetIDGRADESYSTEM2(int COS, int SCHEMANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_level", COS);
                        objParams[1] = new SqlParameter("@P_SCHEMANO", SCHEMANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ID2_DIRECT_GRADE_SYSTEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCGPAGrade()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADE_DIRECT_SYSTEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetGradeRelease(int courseid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SELECTDROPDOWN_GRADE_RELEASE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetGradeTable(int SESSIONNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_@SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_@COURSENO", COURSENO);
                        // objParams[2] = new SqlParameter("@P_@SCHEMENO",SCHEMENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADETABLE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet Show_Grade_Release(int sessionno, int courseid, int schemano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseid);
                        objParams[2] = new SqlParameter("@P_SCHEMANO", schemano);
                        //objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_INTERMIDEATE_RELEASE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GradeAllotment123(int sessionid, int courseno, int schemano, int sem, int uano, int prev, string compo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONO", sessionid);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemano);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);
                        objParams[5] = new SqlParameter("@P_PREV_STATUS", prev);
                        objParams[6] = new SqlParameter("@P_COMPONENT_NAME", compo);
                        objParams[7] = new SqlParameter("@P_out", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_CUTTOFF_CAL_RELATIVE_COMPONENTWISE_ATLAS", objParams, true));
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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConfigConroller.Insert_Exam_Components_Details-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public DataSet GetGradename(string courseid)
                //              {
                //                  DataSet ds = null;
                //                  try
                //                  {
                //                      SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //                      SqlParameter[] objParams = new SqlParameter[1];
                //                      objParams[0] = new SqlParameter("@P_GRADERELEASE", courseid);
                //                      ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADE_BY_USER_ONEBYONE", objParams);
                //                  }
                //                  catch (Exception ex)
                //                  {
                //                      return ds;
                //                      throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                //                  }
                //                  return ds;
                //              }
                public DataSet GradeAllotmentbyuser(int sessionno, int courseid, int schemano, int semester, string regno, string Graderealse)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseid);
                        objParams[2] = new SqlParameter("@P_SCHEMANO", schemano);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[4] = new SqlParameter("@P_REGNO", regno);
                        objParams[5] = new SqlParameter("@P_GRADE_NAME", Graderealse);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRADE_UPD_GRADE_RELEASE_INTERMIDEATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                    }
                    return ds;
                }
                public int CopyExamRuleToSession(int ClgScheme, int session, int SubType, int sem, int CopySession)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSION_NO",session),
                            new SqlParameter("@P_SUB_ID",SubType),
                            new SqlParameter("@P_SCHEMENO",ClgScheme),
                            new SqlParameter("@P_SEMESTER_NO",sem),
                            new SqlParameter("@P_SESSION_COPY",CopySession),
                            new SqlParameter("@P_OUTPUT",status)

                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_COPY_TO_SESSION", sqlParams, true);
                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(obj) == 1)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.CopyExamRuleToSession()->" + ex.ToString());
                    }
                    return status;

                }

                public int CopyToSession(int College, int session, int Degree, int FeesStructure, int CopySession)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_COLLEGE_ID",College),
                            new SqlParameter("@P_SESSION_NO",session),
                            new SqlParameter("@P_FEETYPE",FeesStructure),
                            new SqlParameter("@P_DEGREENO",Degree),
                            new SqlParameter("@P_SESSION_COPY",CopySession),
                            new SqlParameter("@P_OUTPUT",status)
                        };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_FEE_CONFIG_COPY_TO_SESSION", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(obj) == 1)
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.CopyExamRuleToSession()->" + ex.ToString());
                    }

                    return status;
                }

                public DataSet GetStudentFailExamSubList(int idno, int sessionno, int schemeno, int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        //SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_SINGLE_STUD_NEW", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_SUBJECTS_LIST_FOR_REEXAM_REGISTARTION_ATLAS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return ds;
                }


                public int AddStudentResitExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                        objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                        objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;



                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_RESIT_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_ATLAS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public DataSet GetCourses(int schemeno, int semesterno, int sessionno, int College_id, int Subid, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[4] = new SqlParameter("@P_SUBID", Subid);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int GetViewOnStudentLock(int examtype, int semesterno, int sectionno, int sessionno, int Schemano, int Subid)
                {
                    int retStatus = 0;
                    // DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", Schemano);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_SUBID", Subid);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_VIEW_STUDENT_LOG_EXAM_DATE", objParams);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        //retStatus = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetViewOnStudentLock-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddExamDay(Exam objExam, int OrgID, int Modeexam, int sectionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[6] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[8] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[9] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[11] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[12] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[14] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[15] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddStudentExamRegistrationDetails(StudentRegist objSR, string Amt, string order_id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details

                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                        objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_EXAM_FEES", Amt);
                        objParams[10] = new SqlParameter("@P_ORDER_ID", order_id);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_DETAILS_FOR_STUDENT_ATLAS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetGradename(string courseid)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRADERELEASE", courseid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GRADE_BY_USER_ONEBYONE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetRealtiveGrade(int sessionno, int schemeno, int semesterno, int courseno, int output)
                {
                    DataSet ds = null;

                    try
                    {
                        //SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_OP", output);
                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_SINGLE_STUD_NEW", objParams);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RELATIVE_GRADE_ANALYSIS_PBI_ATLAS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }

                    return ds;
                }

                //public int UpdateGradeRange(int session, string ccode, int semester, string maxmark, string minmark, string grade, int lockstatus)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New eXAM Registered Subject Details
                //        objParams = new SqlParameter[8];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                //        objParams[1] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semester);
                //        objParams[3] = new SqlParameter("@P_MAXMARK", maxmark);
                //        objParams[4] = new SqlParameter("@P_MINMARK", minmark);
                //        objParams[5] = new SqlParameter("@P_GRADE", grade);
                //        objParams[6] = new SqlParameter("@P_LOCK", lockstatus);
                //        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[7].Direction = ParameterDirection.Output;

                //        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_UPDATE_GRADE_RANGE", objParams, true);
                //        //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                public int UpdateGradeRange(int session, string ccode, int semester, string maxmark, string minmark, string grade, int lockstatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_CCODE", ccode);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[3] = new SqlParameter("@P_MAXMARK", maxmark);
                        objParams[4] = new SqlParameter("@P_MINMARK", minmark);
                        objParams[5] = new SqlParameter("@P_GRADE", grade);
                        objParams[6] = new SqlParameter("@P_LOCK", lockstatus);
                        //   objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_UPDATE_GRADE_RANGE", objParams, true);
                        //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int CreditRange(int College, int Session, int ExamType, int degreeno, string Sem, bool ActiveStatus, bool FeeProcAppli, string ApplicableFee, string MINRANGE, string MAXRANGE, string AMOUNT, int FeesStructure, string Semname, int userno, bool FeeCertificate, string CertificateFee, int hfd, bool CheckLateFee, int FeeMode, DateTime lateFeeDate, decimal lateFeeAmount, decimal valuationFee, decimal valuationMaxFee)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_ID",College),
                    new SqlParameter("@P_SESSIONNO", Session),
                    new SqlParameter("@P_FEETYPE", ExamType),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SEM", Sem),
                    new SqlParameter("@P_ActiveStatus", ActiveStatus),
                    new SqlParameter("@P_FeeProcAppli", FeeProcAppli),
                    new SqlParameter("@P_APPLICABLEFEE", ApplicableFee),
                    new SqlParameter("@P_MINRANGE", MINRANGE),
                    new SqlParameter("@P_MAXRANGE", MAXRANGE),
                    new SqlParameter("@P_AMOUNT", AMOUNT),
                    new SqlParameter("@P_FEESTRUCTURE", FeesStructure),
                    new SqlParameter("@P_SEMNAME", Semname),
                    new SqlParameter("@P_UANO", userno),
                    new SqlParameter("@P_CertiFeesApplicable",FeeCertificate),
                    new SqlParameter("@P_CertificateFee",CertificateFee),
                    new SqlParameter("@P_FID",hfd),
                    new SqlParameter("@P_CheckLateFeesApplicable", CheckLateFee),
                    new SqlParameter("@P_FeeMode", FeeMode),
                    new SqlParameter("@P_LateFeeDate", lateFeeDate),
                    new SqlParameter("@P_LateFeeAmount", lateFeeAmount),
                    new SqlParameter("@P_ValuationFee", valuationFee),
                    new SqlParameter("@P_ValuationMaxFee", valuationMaxFee),
                    new SqlParameter("@P_OUT", status)

                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEE_DEFINATION_CREDITRANGE", sqlParams, true);

                        if (Convert.ToInt32(obj) == 2)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.CreditRange() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                public DataSet GetRange(int College, int degreeno, int ExamType, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_FEETYPE", ExamType);
                        objParams[3] = new SqlParameter("@P_Sessionno", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_RANGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetRange-> " + ex.ToString());
                    }

                    return ds;
                }

                public int DeleteTimeTable(int exdtno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int ret = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]                   
                        {
                               new SqlParameter("@P_EXDTNO", exdtno),                             
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAM_TIME_TABLE", objParams, true));

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteTimeTable-> " + ex.ToString());
                    }

                    return ret;
                }
                public int GenerateDecodeNumber(int sessionNo, int branchNo, int courseNo, int DigitsNo, string ipAddress, int userId, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    Object ret = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[3] = new SqlParameter("@P_DIGIT", DigitsNo);
                        objParams[4] = new SqlParameter("@P_IP_ADDRESS", ipAddress);
                        objParams[5] = new SqlParameter("@P_USER_ID", userId);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DECODENO_RANDOM", objParams, true);

                        //  if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DECODENO_RANDOM", objParams, false) != null)
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GenerateDecodeNumber->" + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateLockDecodeNo(int sessionno, int courseno, int lck)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                    {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", courseno),
                        new SqlParameter("@P_LOCK", lck)
                    };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DECODE_NUMBER_LOCK", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.UpdateLockDecodeNo->" + ex.ToString());
                    }

                    return retStatus;
                }

                public int GenerateFalseNumber(int sessionno, int schemeno, int semesterno, int courseno, int numSeriese, int branchno, string ipadd, int uano, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        object ret = 0;
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objparams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objparams[2] = new SqlParameter("@P_SEMESTER", semesterno);
                        objparams[3] = new SqlParameter("@P_COURSENO", courseno);

                        objparams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                        objparams[5] = new SqlParameter("@P_IP_ADDRESS", ipadd);
                        objparams[6] = new SqlParameter("@P_USER_ID ", uano);
                        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", colcode);

                        objparams[8] = new SqlParameter("@P_FALSE_NUM_SEREIRSE", numSeriese);
                        objparams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[9].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GENERATE_SEAT_NUMBER", objparams, true);
                        if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GenerateFalseNumber->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourses(int schemeno, int semesterno, int sessionno, int College_id, int Subid, int sectionno,int examno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[4] = new SqlParameter("@P_SUBID", Subid);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_EXAMNO", examno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //Added By Injamam on 15-4-23
                public int AddExamDay1(Exam objExam, int OrgID, int Modeexam, string ccode, int sessionid, int subexamno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionid);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[6] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[7] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        objParams[9] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT_MULTIPLE_SCHEME", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }
                #region Added by gaurav 20_04_2023
                public int AddExamDay(Exam objExam, int OrgID, int Modeexam, int sectionno, int batchno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);

                        objParams[5] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[6] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[8] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[9] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[11] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[12] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[14] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[15] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[16] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT_BATCHWISE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #region added by gaurav for crescent 20_04_2023
                public int AddExamDay(Exam objExam, int OrgID, int Modeexam, int sectionno, string ccode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        // objParams[2] = new SqlParameter("@P_DAYNO", objExam.Dayno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objExam.SemesterNo);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[6] = new SqlParameter("@P_DEGREENO", objExam.DegreeNo);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objExam.BranchNo);
                        objParams[8] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[9] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        //objParams[10] = new SqlParameter("@P_EXAMID", objExam.Courseno );
                        // objParams[10] = new SqlParameter("@P_DAYNAME", objExam.DayName);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objExam.CollegeCode);
                        objParams[11] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[12] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);
                        objParams[14] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[15] = new SqlParameter("@P_CCODE", ccode);
                        objParams[16] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                // added by nehal on dated 02052023
                public DataSet GetStudentCATMarksForSMSFaculty(int sessionno, int degreeno, int branchno, int semesterno, int sectionno, string examname, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_MARK", examname);
                        objParams[6] = new SqlParameter("@P_FAC_ADVISOR", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_STUDENT_CATMARKS_FACULTY", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetStudentCATMarksForSMSFaculty-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentIAMarksForSMSFaculty(int sessionno, int collegeno, int degreeno, int branchno, int semesterno, string examname, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_MARK", examname);
                        objParams[6] = new SqlParameter("@P_FAC_ADVISOR", uano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FOR_STUDENT_MARKS_IA_FORSMS_FACULTY", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetStudentIAMarksForSMSFaculty-> " + ex.ToString());
                    }
                    return ds;
                }
                #region added By Injamam For JECRC
                //Delete Exam Time Table For JECRC Added By Injamam on 09-05-2023
                public int DeleteTimeTable_JECRC(string allexdtno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int ret = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]                   
                        {
                               new SqlParameter("@P_EXDTNO", allexdtno),                             
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAM_TIME_TABLE_JECRC", objParams, true));

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteTimeTable-> " + ex.ToString());
                    }

                    return ret;
                }
                #endregion

                #region Global_electiv Subjects Added by Injamam 08_06_2023
                public DataSet GetCoursesGlobleElectiv(int sessionid, int subjecttype, int subexamno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[1] = new SqlParameter("@P_SUBJECTTYPE", subjecttype);
                        objParams[2] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_ALL_GLOBALELE_COURSE_JECRC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetCourses-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region INSERT Global elective Sub Timetable Added by Injamam 08_06_2023
                public int AddExamDayElect(Exam objExam, int OrgID, int Modeexam, string ccode, int sessionid, int subexamno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionid);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[6] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[7] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[8] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        objParams[9] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT_ELECTIVE_CC_JECRC", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteTimeTableElectiv(string ccode, int sessionid, int subexamno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int ret = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]                   
                        {
                               new SqlParameter("@P_CCODE", ccode),                             
                               new SqlParameter("@P_SESSIONID", sessionid),                             
                               new SqlParameter("@P_SUBEXAMNO", subexamno),                             
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAM_TIME_TABLE_ELECTIVE_JECRC", objParams, true));

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteTimeTable-> " + ex.ToString());
                    }

                    return ret;
                }


                public int GetViewOnStudentLock(int sessionid, int subexamno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[1] = new SqlParameter("@P_SUBEXAMNO", subexamno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VIEW_STUDENT_LOG_GLOBALE_EXAM_DATE_JECRC", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetViewOnStudentLock-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region INSERT Global elective Sub Timetable Common Code Added by Injamam 08_06_2023
                public int AddExamDayElect(Exam objExam, int OrgID, int Modeexam, string ccode, int sessionid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    object ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionid);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", objExam.Exam_TT_Type);
                        objParams[2] = new SqlParameter("@P_SLOTNO", objExam.Slot);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_EXAMDATE", objExam.Examdate);
                        objParams[5] = new SqlParameter("@P_STATUS", objExam.Status);
                        objParams[6] = new SqlParameter("@P_ORGID", OrgID);
                        objParams[7] = new SqlParameter("@P_ModeEXAMNO", Modeexam);
                        objParams[8] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_DATE_INSERT_GLOBALE_ELECTIVE_CC", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.AddExamDay -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteTimeTableElectiv_CC(string ccode, int sessionid, int examno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int ret = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]                   
                        {
                               new SqlParameter("@P_CCODE", ccode),                             
                               new SqlParameter("@P_SESSIONID", sessionid),                             
                               new SqlParameter("@P_EXAMNO", examno),                             
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_EXAM_TIME_TABLE_ELECTIVE_CC", objParams, true));

                        if (!string.IsNullOrEmpty(ret.ToString()) && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.DeleteTimeTable-> " + ex.ToString());
                    }

                    return ret;
                }

                public int GetViewOnStudentLock(int sessionid, int examno, string ccode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[1] = new SqlParameter("@P_SUBEXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VIEW_STUDENT_LOG_GLOBALE_EXAM_DATE_CC", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetViewOnStudentLock-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                // ADDED BY PRAFULL ON DT-26062023
                public int AddExamRegisteredBacklaog_All_LOG(StudentRegist objSR, string Backlogsems)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", Backlogsems);
                        objParams[3] = new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_IDNOS", objSR.IDNO);
                        //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                        objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG_ALL_LOG", objParams, true);
                        //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

		//added by prafull on dt-05-07-2023

  public int UpdateGradeRange(int session, string ccode, int semester, string maxmark, string minmark, string grade, int lockstatus, int ua_no)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);
                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                         SqlParameter[] objParams = null;

                         //Add New eXAM Registered Subject Details
                         objParams = new SqlParameter[9];

                         objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                         objParams[1] = new SqlParameter("@P_CCODE", ccode);
                         objParams[2] = new SqlParameter("@P_SEMESTERNO", semester);
                         objParams[3] = new SqlParameter("@P_MAXMARK", maxmark);
                         objParams[4] = new SqlParameter("@P_MINMARK", minmark);
                         objParams[5] = new SqlParameter("@P_GRADE", grade);
                         objParams[6] = new SqlParameter("@P_LOCK", lockstatus);
                         objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                         objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                         objParams[8].Direction = ParameterDirection.Output;

                         //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                         object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_UPDATE_GRADE_RANGE", objParams, true);
                         //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                         if (Convert.ToInt32(ret) == -99)
                             retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                         else
                             retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                     }
                     catch (Exception ex)
                     {
                         retStatus = Convert.ToInt32(CustomStatus.Error);
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                     }

                     return retStatus;

                 }


                 public int UpdatePowerFactor(int session, string ccode ,int schemeno, int semester, decimal Upper_range, decimal Lower_range, int ua_no)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);
                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                         SqlParameter[] objParams = null;

                         //Add New eXAM Registered Subject Details
                         objParams = new SqlParameter[8];

                         objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                         objParams[1] = new SqlParameter("@P_CCODE", ccode);
                         objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                         objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                         objParams[4] = new SqlParameter("@P_UPPER_MARKS", Upper_range);
                         objParams[5] = new SqlParameter("@P_LOWER_MARK", Lower_range);
                         objParams[6] = new SqlParameter("@P_UA_NO", ua_no);
                         objParams[7] = new SqlParameter("@P_OP", SqlDbType.Int);
                         objParams[7].Direction = ParameterDirection.Output;

                         //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_RESULT_FAIL_LIST_BACKLOG_REG", objParams, true);

                         object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_POWER_FACTOR_CALCULATION_CPU_REGENERATE", objParams, true);
                         //PKG_EXAM_RESULT_INSERT_FAIL_LIST_BACKLOG_REG
                         if (Convert.ToInt32(ret) == -99)
                             retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                         else
                             retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                     }
                     catch (Exception ex)
                     {
                         retStatus = Convert.ToInt32(CustomStatus.Error);
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                     }

                     return retStatus;

                 }


                 //FOR CREATION OF TIMETABLE SECTIONWISE & BATCHWISE TIME TABLE ADDED BY INJAMAM ANSARI 16-08-2023
                 public int Add_ExamConfiguration(int examrule, int garcerule, int latefee, int improvement, int exampattern, int revaluation, int result, int condonation, int feetype, int passrule, int examreg, int decode, int seat, int temp, int excel, int sec, int batch)
                 {
                     int status = 0;
                     try
                     {


                         SQLHelper objHelp = new SQLHelper(_uaims_constr);
                         SqlParameter[] objParam = new SqlParameter[16];
                         objParam[0] = new SqlParameter("@P_EXAM_RULE", examrule);
                         objParam[1] = new SqlParameter("@P_GRACE_RULE", garcerule);
                         objParam[2] = new SqlParameter("@P_LATE_FEE", latefee);
                         objParam[3] = new SqlParameter("@P_IMPROVEMENT", improvement);
                         objParam[4] = new SqlParameter("@P_EXAM_PATTERN", exampattern);
                         objParam[5] = new SqlParameter("@P_REVALUATION", revaluation);
                         objParam[6] = new SqlParameter("@P_RESULT_OTP", result);
                         objParam[7] = new SqlParameter("@P_CONDONATION", condonation);
                         objParam[8] = new SqlParameter("@P_FEE_TYPE", feetype);
                         objParam[9] = new SqlParameter("@P_PASS_RULE", passrule);
                         objParam[10] = new SqlParameter("@P_EXAM_REG", examreg);
                         objParam[11] = new SqlParameter("@P_DECODE_NUMBER", decode);
                         objParam[12] = new SqlParameter("@P_SEAT_NUMBER", seat);
                         objParam[13] = new SqlParameter("@P_EXCEL_MARK_ENTRY", excel);
                         objParam[14] = new SqlParameter("@P_SECTIONWISE", sec);
                         objParam[15] = new SqlParameter("@P_BATCHWISE", batch);


                         //objParam[objParam.Length - 1].Direction = ParameterDirection.InputOutput;

                         object obj = objHelp.ExecuteNonQuerySP("PKG_INS_EXAM_CONFIGURATION", objParam, true);
                         //object obj = objHelp.ExecuteScalarSP("PKG_INS_EXAM_CONFIGURATION", objParam);

                         if (obj != null)
                             status = Convert.ToInt32(CustomStatus.RecordSaved);
                         else
                             status = Convert.ToInt32(CustomStatus.Error);
                     }
                     catch (Exception ex)
                     {
                         status = Convert.ToInt32(CustomStatus.Error);
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ElectionController.AddElectionCategoryPostName() --> " + ex.Message + " " + ex.StackTrace);
                     }
                     return status;
                 }



            }
        }
    }
}
