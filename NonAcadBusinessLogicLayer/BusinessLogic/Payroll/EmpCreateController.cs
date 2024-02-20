using System;
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
            public class EmpCreateController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// Generate New Idno for new employee.
                /// </summary>
                /// <returns></returns>
                public int GenerateId()
                {
                    int idno = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        object ret = objSQLHelper.ExecuteScalarSP("PKG_PAY_RETURN_ID_EMPMAS", objParams);
                        if (ret != null)
                            idno = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GenerateId -> " + ex.ToString());
                    }
                    return idno;
                }

                /// <summary>
                /// Generate New RFID NO for new employee.
                /// </summary>
                /// <returns></returns>
                public int GenerateRFId()
                {
                    int idno = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        object ret = objSQLHelper.ExecuteScalarSP("PKG_PAY_RETURN_RFIDNO_EMPMAS", objParams);
                        if (ret != null)
                            idno = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GenerateId -> " + ex.ToString());
                    }
                    return idno;
                }


                /// <summary>
                /// AddNewEmployee method is used to add new Employee.
                /// </summary>
                /// <param name="objEM">objEM is the object of EmpMaster class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                /// 
                public int AddNewEmployee_CRES(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)//, int DESIG_COLLNO, int DESIG_UNIVNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[98];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IDNO);
                        objParams[1] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                        objParams[2] = new SqlParameter("@P_TITLE", objEM.TITLE);
                        objParams[3] = new SqlParameter("@P_FNAME", objEM.FNAME);
                        objParams[4] = new SqlParameter("@P_MNAME", objEM.MNAME);
                        objParams[5] = new SqlParameter("@P_LNAME", objEM.LNAME);
                        objParams[6] = new SqlParameter("@P_SEX", objEM.SEX);
                        objParams[7] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                        if (!objEM.DOB.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);
                        else
                            objParams[8] = new SqlParameter("@P_DOB", DBNull.Value);

                        //objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);

                        if (!objEM.DOJ.Equals(DateTime.MinValue))
                            objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        else
                            objParams[9] = new SqlParameter("@P_DOJ", DBNull.Value);

                        if (!objEM.DOI.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                        else
                            objParams[10] = new SqlParameter("@P_DOI", DBNull.Value);

                        if (!objEM.RDT.Equals(DateTime.MinValue))
                            objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);
                        else
                            objParams[11] = new SqlParameter("@P_RDT", DBNull.Value);

                        // objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        // objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                        // objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);

                        objParams[12] = new SqlParameter("@P_HP", objEM.HP);
                        objParams[13] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                        objParams[14] = new SqlParameter("@P_SUBDESIGNO", objEM.SUBDESIGNO);
                        objParams[15] = new SqlParameter("@P_STNO", objEM.STNO);
                        objParams[16] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                        objParams[17] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                        objParams[18] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                        objParams[19] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                        objParams[20] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                        objParams[21] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                        objParams[22] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                        objParams[23] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                        objParams[24] = new SqlParameter("@P_REMARK", objEM.REMARK);
                        objParams[25] = new SqlParameter("@P_ANFN", objEM.ANFN);
                        objParams[26] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                        objParams[27] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                        objParams[28] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                        objParams[29] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                        objParams[30] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                        objParams[31] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                        objParams[32] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                        objParams[33] = new SqlParameter("@P_BASIC", objPM.BASIC);
                        objParams[34] = new SqlParameter("@P_TA", objPM.TA);
                        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);
                        objParams[36] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                        objParams[37] = new SqlParameter("@P_PHOTO", objEM.Photo);
                        objParams[38] = new SqlParameter("@P_PFNO", objEM.PFNO);
                        objParams[39] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                        objParams[40] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                        objParams[41] = new SqlParameter("@P_CLNO", objEM.CLNO);
                        objParams[42] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                        objParams[43] = new SqlParameter("@P_RFID", rfid);
                        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                        //objParams[44] = new SqlParameter("@P_DESIG_COLLNO", DESIG_COLLNO);
                        //objParams[45] = new SqlParameter("@P_DESIG_UNIVNO", DESIG_UNIVNO);
                        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                        if (!objEM.STDATE.Equals(DateTime.MinValue))
                            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                        else
                            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                        objParams[55] = new SqlParameter("@P_MOTHERNAME", mothername);

                        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);
                        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                        else
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                        else
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);

                        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                        objParams[65] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[66] = new SqlParameter("@P_USER_UATYPE", objEM.USER_UATYPE);
                        objParams[67] = new SqlParameter("@P_IsbusFas", objEM.IsBusFac);

                        objParams[68] = new SqlParameter("@UANNO", objEM.UAN1);
                        objParams[69] = new SqlParameter("@EMPLOYEEID", objEM.EmployeeId);
                        objParams[70] = new SqlParameter("@I8", objPM.I8);

                        objParams[71] = new SqlParameter("@P_BLOODGRPNO", objEM.BLOODGRPNO);
                        objParams[72] = new SqlParameter("@P_ISMARITALSTATUS", objEM.MaritalStatus);
                        objParams[73] = new SqlParameter("@P_CHILDFEMALE", objEM.ChildFemale);
                        objParams[74] = new SqlParameter("@P_CHILDMALE", objEM.ChildMale);
                        objParams[75] = new SqlParameter("@P_HANDICAPTYPEID", objEM.HandicapTypeID);
                        objParams[76] = new SqlParameter("@P_ISPHYSICALLYCHALLENGED", objEM.IsPhysicallyChallenged);
                        objParams[77] = new SqlParameter("@P_COLLEGEROOMNO", objEM.CollegeRoomNo);
                        objParams[78] = new SqlParameter("@P_COLLEGEINTERCOMNO", objEM.CollegeIntercomNo);
                        objParams[79] = new SqlParameter("@P_QUALFORDISPLAY", objEM.QualForDisplay);
                        objParams[80] = new SqlParameter("@P_EMPLOYMENT", objEM.Employment);
                        if (!objEM.QuartersAllotmentDate.Equals(DateTime.MinValue))
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        else
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", DBNull.Value);
                        objParams[82] = new SqlParameter("@Age", objEM.Age);

                        objParams[83] = new SqlParameter("@isCabFac", objEM.IsCabfac);
                        objParams[84] = new SqlParameter("@isTelguMin", objEM.IsTelguMin);
                        objParams[85] = new SqlParameter("@isDrugAlrg", objEM.IsDrugAlrg);
                        objParams[86] = new SqlParameter("@IFSC_CODE", objEM.IFSC_CODE);
                        objParams[87] = new SqlParameter("@ALTERNATE_EMAILID", objEM.ALTERNATEEMAILID);
                        objParams[88] = new SqlParameter("@ALTERNATE_PHONENO", objEM.ALTERNATEPHONENO);
                        objParams[89] = new SqlParameter("@IS_SHIFT_MANAGMENT", objEM.IS_SHIFT_MANAGMENT);
                        objParams[90] = new SqlParameter("@ESICNO", objEM.ESICNO);
                        objParams[91] = new SqlParameter("@P_ISNEFT", objEM.IsNEFT);
                        //objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);
                        if (objEM.Photo == null)
                            objParams[92] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);

                        objParams[92].SqlDbType = SqlDbType.Image;
                        objParams[93] = new SqlParameter("@P_MAINDEPTNO ", objEM.MAINDEPTNO);
                        objParams[94] = new SqlParameter("@P_DAHEADID", objEM.DAHEADID);
                        objParams[95] = new SqlParameter("@P_IsServicemen", objEM.ExServicemen);
                        objParams[96] = new SqlParameter("@P_IsBioAuthorityPerson", objEM.IsBioAuthorityPerson);
                        objParams[97] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[97].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_INS_EMP_CRESCENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.AddNewEmployee -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddNewEmployee(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)//, int DESIG_COLLNO, int DESIG_UNIVNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[111];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IDNO);
                        objParams[1] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                        objParams[2] = new SqlParameter("@P_TITLE", objEM.TITLE);
                        objParams[3] = new SqlParameter("@P_FNAME", objEM.FNAME);
                        objParams[4] = new SqlParameter("@P_MNAME", objEM.MNAME);
                        objParams[5] = new SqlParameter("@P_LNAME", objEM.LNAME);
                        objParams[6] = new SqlParameter("@P_SEX", objEM.SEX);
                        objParams[7] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                        if (!objEM.DOB.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);
                        else
                            objParams[8] = new SqlParameter("@P_DOB", DBNull.Value);

                        //objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);

                        if (!objEM.DOJ.Equals(DateTime.MinValue))
                            objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        else
                            objParams[9] = new SqlParameter("@P_DOJ", DBNull.Value);

                        if (!objEM.DOI.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                        else
                            objParams[10] = new SqlParameter("@P_DOI", DBNull.Value);

                        if (!objEM.RDT.Equals(DateTime.MinValue))
                            objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);
                        else
                            objParams[11] = new SqlParameter("@P_RDT", DBNull.Value);

                        // objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        // objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                        // objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);

                        objParams[12] = new SqlParameter("@P_HP", objEM.HP);
                        objParams[13] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                        objParams[14] = new SqlParameter("@P_SUBDESIGNO", objEM.SUBDESIGNO);
                        objParams[15] = new SqlParameter("@P_STNO", objEM.STNO);
                        objParams[16] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                        objParams[17] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                        objParams[18] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                        objParams[19] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                        objParams[20] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                        objParams[21] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                        objParams[22] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                        objParams[23] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                        objParams[24] = new SqlParameter("@P_REMARK", objEM.REMARK);
                        objParams[25] = new SqlParameter("@P_ANFN", objEM.ANFN);
                        objParams[26] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                        objParams[27] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                        objParams[28] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                        objParams[29] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                        objParams[30] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                        objParams[31] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                        objParams[32] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                        objParams[33] = new SqlParameter("@P_BASIC", objPM.BASIC);
                        objParams[34] = new SqlParameter("@P_TA", objPM.TA);
                        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);
                        objParams[36] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                        if (objEM.Photo == null)
                        {
                            objParams[37] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        }
                        else
                        {
                            objParams[37] = new SqlParameter("@P_PHOTO", objEM.Photo);
                        }
                        objParams[37].SqlDbType = SqlDbType.Image;
                        objParams[38] = new SqlParameter("@P_PFNO", objEM.PFNO);
                        objParams[39] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                        objParams[40] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                        objParams[41] = new SqlParameter("@P_CLNO", objEM.CLNO);
                        objParams[42] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                        objParams[43] = new SqlParameter("@P_RFID", rfid);
                        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                        //objParams[44] = new SqlParameter("@P_DESIG_COLLNO", DESIG_COLLNO);
                        //objParams[45] = new SqlParameter("@P_DESIG_UNIVNO", DESIG_UNIVNO);
                        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                        if (!objEM.STDATE.Equals(DateTime.MinValue))
                            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                        else
                            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                        objParams[55] = new SqlParameter("@P_MOTHERNAME", mothername);

                        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);
                        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                        else
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                        else
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);

                        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                        objParams[65] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[66] = new SqlParameter("@P_USER_UATYPE", objEM.USER_UATYPE);
                        objParams[67] = new SqlParameter("@P_IsbusFas", objEM.IsBusFac);

                        objParams[68] = new SqlParameter("@UANNO", objEM.UAN1);
                        objParams[69] = new SqlParameter("@EMPLOYEEID", objEM.EmployeeId);
                        objParams[70] = new SqlParameter("@I8", objPM.I8);

                        objParams[71] = new SqlParameter("@P_BLOODGRPNO", objEM.BLOODGRPNO);
                        objParams[72] = new SqlParameter("@P_ISMARITALSTATUS", objEM.MaritalStatus);
                        objParams[73] = new SqlParameter("@P_CHILDFEMALE", objEM.ChildFemale);
                        objParams[74] = new SqlParameter("@P_CHILDMALE", objEM.ChildMale);
                        objParams[75] = new SqlParameter("@P_HANDICAPTYPEID", objEM.HandicapTypeID);
                        objParams[76] = new SqlParameter("@P_ISPHYSICALLYCHALLENGED", objEM.IsPhysicallyChallenged);
                        objParams[77] = new SqlParameter("@P_COLLEGEROOMNO", objEM.CollegeRoomNo);
                        objParams[78] = new SqlParameter("@P_COLLEGEINTERCOMNO", objEM.CollegeIntercomNo);
                        objParams[79] = new SqlParameter("@P_QUALFORDISPLAY", objEM.QualForDisplay);
                        objParams[80] = new SqlParameter("@P_EMPLOYMENT", objEM.Employment);
                        if (!objEM.QuartersAllotmentDate.Equals(DateTime.MinValue))
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        else
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", DBNull.Value);
                        objParams[82] = new SqlParameter("@Age", objEM.Age);

                        objParams[83] = new SqlParameter("@isCabFac", objEM.IsCabfac);
                        objParams[84] = new SqlParameter("@isTelguMin", objEM.IsTelguMin);
                        objParams[85] = new SqlParameter("@isDrugAlrg", objEM.IsDrugAlrg);
                        objParams[86] = new SqlParameter("@IFSC_CODE", objEM.IFSC_CODE);
                        objParams[87] = new SqlParameter("@ALTERNATE_EMAILID", objEM.ALTERNATEEMAILID);
                        objParams[88] = new SqlParameter("@ALTERNATE_PHONENO", objEM.ALTERNATEPHONENO);
                        objParams[89] = new SqlParameter("@IS_SHIFT_MANAGMENT", objEM.IS_SHIFT_MANAGMENT);
                        objParams[90] = new SqlParameter("@ESICNO", objEM.ESICNO);
                        objParams[91] = new SqlParameter("@P_ISNEFT", objEM.IsNEFT);
                        //objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);
                        if (objEM.PhotoSign == null)
                            objParams[92] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);

                        objParams[92].SqlDbType = SqlDbType.Image;
                        objParams[93] = new SqlParameter("@P_MAINDEPTNO ", objEM.MAINDEPTNO);
                        //objParams[94] = new SqlParameter("@P_DAHEADID", objEM.DAHEADID);
                        objParams[94] = new SqlParameter("@P_DIVIDNO", objPM.DIVISIONMASTER);  //add amol
                        // Added on 16-01-2023 
                        objParams[95] = new SqlParameter("@P_IsOnNotice", objEM.IsNoticePeriod);

                        if (!objEM.RESIGNATIONDATE.Equals(DateTime.MinValue))
                          objParams[96] = new SqlParameter("@P_DateOfResignation", objEM.RESIGNATIONDATE);
                        else
                          objParams[96] = new SqlParameter("@P_DateOfResignation", DBNull.Value);
                       
                        objParams[97] = new SqlParameter("@P_AttritionTypeNo", objEM.AttritionTypeNo);

                        objParams[98] = new SqlParameter("@P_ExitReason", objEM.RESIGNATIONRESASON);

                        objParams[99] = new SqlParameter("@P_EnityNo", objEM.EnityNo);
                        if (!objEM.GROUPOFDOJ.Equals(DateTime.MinValue))
                            objParams[100] = new SqlParameter("@P_GroupOfDOJ", objEM.GROUPOFDOJ);
                        else
                            objParams[100] = new SqlParameter("@P_GroupOfDOJ", DBNull.Value);

                        objParams[101] = new SqlParameter("@P_STATE", objEM.STATE);
                        objParams[102] = new SqlParameter("@P_COUNTRY", objEM.COUNTRY);
                        objParams[103] = new SqlParameter("@P_CITY", objEM.CITY);
                        if (!objEM.EXITDATE.Equals(DateTime.MinValue))
                        objParams[104] = new SqlParameter("@P_EXIT_DATE", objEM.EXITDATE);

                        else
                        objParams[104] = new SqlParameter("@P_EXIT_DATE", DBNull.Value);

                        objParams[105] = new SqlParameter("@P_PAYLEVELNO", objEM.PaylevelId);
                        objParams[106] = new SqlParameter("@P_CELLNO", objEM.CellNumber);
                        objParams[107] = new SqlParameter("@P_IsBioAuthorityPerson", objEM.IsBioAuthorityPerson);
                        // Added on 07-08-2023
                        objParams[108] = new SqlParameter("@P_HRA_HEADID", objEM.HRA_HEADID);
                        objParams[109] = new SqlParameter("@P_DAHEADID", objEM.DAHEADID);
                        objParams[110] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[110].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_INS_EMP", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.AddNewEmployee -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int AddNewEmployee(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)//, int DESIG_COLLNO, int DESIG_UNIVNO)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Employee
                //        objParams = new SqlParameter[73];

                //        objParams[0] = new SqlParameter("@P_IDNO", objEM.IDNO);
                //        objParams[1] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                //        objParams[2] = new SqlParameter("@P_TITLE", objEM.TITLE);
                //        objParams[3] = new SqlParameter("@P_FNAME", objEM.FNAME);
                //        objParams[4] = new SqlParameter("@P_MNAME", objEM.MNAME);
                //        objParams[5] = new SqlParameter("@P_LNAME", objEM.LNAME);
                //        objParams[6] = new SqlParameter("@P_SEX", objEM.SEX);
                //        objParams[7] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                //        if (!objEM.DOB.Equals(DateTime.MinValue))
                //            objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);
                //        else
                //            objParams[8] = new SqlParameter("@P_DOB", DBNull.Value);

                //        //objParams[8] = new SqlParameter("@P_DOB", objEM.DOB);

                //        if (!objEM.DOJ.Equals(DateTime.MinValue))
                //            objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                //        else
                //            objParams[9] = new SqlParameter("@P_DOJ", DBNull.Value);

                //        if (!objEM.DOI.Equals(DateTime.MinValue))
                //            objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                //        else
                //            objParams[10] = new SqlParameter("@P_DOI", DBNull.Value);

                //        if (!objEM.RDT.Equals(DateTime.MinValue))
                //            objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);
                //        else
                //            objParams[11] = new SqlParameter("@P_RDT", DBNull.Value);

                //        // objParams[9] = new SqlParameter("@P_DOJ", objEM.DOJ);
                //        // objParams[10] = new SqlParameter("@P_DOI", objEM.DOI);
                //        // objParams[11] = new SqlParameter("@P_RDT", objEM.RDT);

                //        objParams[12] = new SqlParameter("@P_HP", objEM.HP);
                //        objParams[13] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                //        objParams[14] = new SqlParameter("@P_SUBDESIGNO", objEM.SUBDESIGNO);
                //        objParams[15] = new SqlParameter("@P_STNO", objEM.STNO);
                //        objParams[16] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                //        objParams[17] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                //        objParams[18] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                //        objParams[19] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                //        objParams[20] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                //        objParams[21] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                //        objParams[22] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                //        objParams[23] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                //        objParams[24] = new SqlParameter("@P_REMARK", objEM.REMARK);
                //        objParams[25] = new SqlParameter("@P_ANFN", objEM.ANFN);
                //        objParams[26] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                //        objParams[27] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                //        objParams[28] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                //        objParams[29] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                //        objParams[30] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                //        objParams[31] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                //        objParams[32] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                //        objParams[33] = new SqlParameter("@P_BASIC", objPM.BASIC);
                //        objParams[34] = new SqlParameter("@P_TA", objPM.TA);
                //        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);
                //        objParams[36] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE);
                //        objParams[37] = new SqlParameter("@P_PHOTO", objEM.Photo);
                //        objParams[38] = new SqlParameter("@P_PFNO", objEM.PFNO);
                //        objParams[39] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                //        objParams[40] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                //        objParams[41] = new SqlParameter("@P_CLNO", objEM.CLNO);
                //        objParams[42] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                //        objParams[43] = new SqlParameter("@P_RFID", rfid);
                //        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                //        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                //        //objParams[44] = new SqlParameter("@P_DESIG_COLLNO", DESIG_COLLNO);
                //        //objParams[45] = new SqlParameter("@P_DESIG_UNIVNO", DESIG_UNIVNO);
                //        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                //        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                //        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                //        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                //        if (!objEM.STDATE.Equals(DateTime.MinValue))
                //            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                //        else
                //            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                //        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                //        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                //        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                //        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                //        objParams[55] = new SqlParameter("@P_AddNewEmployeeMOTHERNAME", mothername);

                //        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                //        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                //        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);
                //        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                //            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                //        else
                //            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                //        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                //        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                //            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                //        else
                //            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                //        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                //        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                //        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                //        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                //        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);

                //        objParams[65] = new SqlParameter("@P_LocationId", objEM.LocationId);
                //        objParams[66] = new SqlParameter("@P_ESICNO", objEM.EsicNo);

                //        if (!objEM.TransferDate.Equals(DateTime.MinValue))
                //            objParams[67] = new SqlParameter("@TransferDate", objEM.TransferDate);
                //        else
                //            objParams[67] = new SqlParameter("@TransferDate", DBNull.Value);


                //        objParams[68] = new SqlParameter("@AlternateMobileNo", objEM.AlternateMobileNo);
                //        objParams[69] = new SqlParameter("@OfficialMail", objEM.OfficialMail);
                //        objParams[70] = new SqlParameter("@EmgName", objEM.EmgName);
                //        objParams[71] = new SqlParameter("@EmgContactNo", objEM.EmgContactNo);

                //        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                //        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                //        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                //        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                //        objParams[72] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[72].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_INS_EMP", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.AddNewEmployee -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                public DataTableReader TermsCondtionDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_TERMSCONDTION_DETAILS_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                public DataTableReader UserDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_USER_DETAILS_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                public DataTable RetrieveEmpDetails(string search, string category, string collegeno)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }

                public DataTableReader ShowEmpDetails(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_Idno", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_RET_EMPLOYEE_BYID", objParams).Tables[0].CreateDataReader();
                        // dtr = objSQLHelper.ExecuteReaderSP("PKG_EMP_SP_RET_EMPLOYEE_BYID", objParams)as SqlDataReader ;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //public int UpdateEmployee(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        //Update New Employee
                //        objParams = new SqlParameter[73];

                //        objParams[0] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                //        objParams[1] = new SqlParameter("@P_TITLE", objEM.TITLE);
                //        objParams[2] = new SqlParameter("@P_FNAME", objEM.FNAME);
                //        objParams[3] = new SqlParameter("@P_MNAME", objEM.MNAME);
                //        objParams[4] = new SqlParameter("@P_LNAME", objEM.LNAME);
                //        objParams[5] = new SqlParameter("@P_SEX", objEM.SEX);
                //        objParams[6] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                //        if (!objEM.DOB.Equals(DateTime.MinValue))
                //            objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                //        else
                //            objParams[7] = new SqlParameter("@P_DOB", DBNull.Value);

                //        if (!objEM.DOJ.Equals(DateTime.MinValue))
                //            objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                //        else
                //            objParams[8] = new SqlParameter("@P_DOJ", DBNull.Value);

                //        if (!objEM.DOI.Equals(DateTime.MinValue))
                //            objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                //        else
                //            objParams[9] = new SqlParameter("@P_DOI", DBNull.Value);

                //        if (!objEM.RDT.Equals(DateTime.MinValue))
                //            objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);
                //        else
                //            objParams[10] = new SqlParameter("@P_RDT", DBNull.Value);


                //        //objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                //        //objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                //        //objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                //        //objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);

                //        objParams[11] = new SqlParameter("@P_HP", objEM.HP);
                //        objParams[12] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                //        objParams[13] = new SqlParameter("@P_DESIG_COLLNO", objPM.SUBDESIGNO);
                //        objParams[14] = new SqlParameter("@P_STNO", objEM.STNO);
                //        objParams[15] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                //        objParams[16] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                //        objParams[17] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                //        objParams[18] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                //        objParams[19] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                //        objParams[20] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                //        objParams[21] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                //        objParams[22] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                //        objParams[23] = new SqlParameter("@P_REMARK", objEM.REMARK);
                //        objParams[24] = new SqlParameter("@P_ANFN", objEM.ANFN);
                //        objParams[25] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                //        objParams[26] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                //        objParams[27] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                //        objParams[28] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                //        objParams[29] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                //        objParams[30] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                //        objParams[31] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                //        objParams[32] = new SqlParameter("@P_BASIC", objPM.BASIC);
                //        objParams[33] = new SqlParameter("@P_TA", objPM.TA);
                //        objParams[34] = new SqlParameter("@P_IDNO", objEM.IDNO);
                //        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);

                //        if (objEM.Photo == null)
                //            objParams[36] = new SqlParameter("@P_PHOTO", DBNull.Value);
                //        else
                //            objParams[36] = new SqlParameter("@P_PHOTO", objEM.Photo);

                //        objParams[36].SqlDbType = SqlDbType.Image;
                //        objParams[37] = new SqlParameter("@P_PFNO", objEM.PFNO);

                //        objParams[38] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                //        objParams[39] = new SqlParameter("@P_CLNO", objEM.CLNO);

                //        objParams[40] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                //        objParams[41] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                //        objParams[42] = new SqlParameter("@P_RFID", rfid);

                //        objParams[43] = new SqlParameter("@P_DESIG_UNIVNO", objEM.SUBDESIGNO);
                //        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                //        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                //        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                //        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                //        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                //        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                //        if (!objEM.STDATE.Equals(DateTime.MinValue))
                //            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                //        else
                //            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                //        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                //        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                //        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                //        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                //        objParams[55] = new SqlParameter("@P_MOTHERNAME", mothername);
                //        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                //        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                //        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);

                //        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                //            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                //        else
                //            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                //        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                //        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                //            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                //        else
                //            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                //        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                //        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                //        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                //        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                //        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);

                //        objParams[65] = new SqlParameter("@P_LocationId", objEM.LocationId);
                //        objParams[66] = new SqlParameter("@P_ESICNO", objEM.EsicNo);


                //        if (!objEM.TransferDate.Equals(DateTime.MinValue))
                //            objParams[67] = new SqlParameter("@TransferDate", objEM.TransferDate);
                //        else
                //            objParams[67] = new SqlParameter("@TransferDate", DBNull.Value);

                //        objParams[68] = new SqlParameter("@AlternateMobileNo", objEM.AlternateMobileNo);
                //        objParams[69] = new SqlParameter("@OfficialMail", objEM.OfficialMail);
                //        objParams[70] = new SqlParameter("@EmgName", objEM.EmgName);
                //        objParams[71] = new SqlParameter("@EmgContactNo", objEM.EmgContactNo);

                //        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                //        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                //        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                //        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                //        objParams[72] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[72].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_UPD_EMP", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                //    }
                //    return retStatus;

                //}
                public int UpdateEmployee(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update New Employee

                        objParams = new SqlParameter[111];

                        objParams[0] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                        objParams[1] = new SqlParameter("@P_TITLE", objEM.TITLE);
                        objParams[2] = new SqlParameter("@P_FNAME", objEM.FNAME);
                        objParams[3] = new SqlParameter("@P_MNAME", objEM.MNAME);
                        objParams[4] = new SqlParameter("@P_LNAME", objEM.LNAME);
                        objParams[5] = new SqlParameter("@P_SEX", objEM.SEX);
                        objParams[6] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                        if (!objEM.DOB.Equals(DateTime.MinValue))
                            objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                        else
                            objParams[7] = new SqlParameter("@P_DOB", DBNull.Value);

                        if (!objEM.DOJ.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        else
                            objParams[8] = new SqlParameter("@P_DOJ", DBNull.Value);

                        if (!objEM.DOI.Equals(DateTime.MinValue))
                            objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                        else
                            objParams[9] = new SqlParameter("@P_DOI", DBNull.Value);

                        if (!objEM.RDT.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);
                        else
                            objParams[10] = new SqlParameter("@P_RDT", DBNull.Value);


                        //objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                        //objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        //objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                        //objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);

                        objParams[11] = new SqlParameter("@P_HP", objEM.HP);
                        objParams[12] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                        objParams[13] = new SqlParameter("@P_DESIG_COLLNO", objPM.SUBDESIGNO);
                        objParams[14] = new SqlParameter("@P_STNO", objEM.STNO);
                        objParams[15] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                        objParams[16] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                        objParams[17] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                        objParams[18] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                        objParams[19] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                        objParams[20] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                        objParams[21] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                        objParams[22] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                        objParams[23] = new SqlParameter("@P_REMARK", objEM.REMARK);
                        objParams[24] = new SqlParameter("@P_ANFN", objEM.ANFN);
                        objParams[25] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                        objParams[26] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                        objParams[27] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                        objParams[28] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                        objParams[29] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                        objParams[30] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                        objParams[31] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                        objParams[32] = new SqlParameter("@P_BASIC", objPM.BASIC);
                        objParams[33] = new SqlParameter("@P_TA", objPM.TA);
                        objParams[34] = new SqlParameter("@P_IDNO", objEM.IDNO);
                        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);

                        if (objEM.Photo == null)
                            objParams[36] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[36] = new SqlParameter("@P_PHOTO", objEM.Photo);

                        objParams[36].SqlDbType = SqlDbType.Image;
                        objParams[37] = new SqlParameter("@P_PFNO", objEM.PFNO);

                        objParams[38] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                        objParams[39] = new SqlParameter("@P_CLNO", objEM.CLNO);

                        objParams[40] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                        objParams[41] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                        objParams[42] = new SqlParameter("@P_RFID", rfid);

                        objParams[43] = new SqlParameter("@P_DESIG_UNIVNO", objEM.SUBDESIGNO);
                        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                        if (!objEM.STDATE.Equals(DateTime.MinValue))
                            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                        else
                            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                        objParams[55] = new SqlParameter("@P_MOTHERNAME", mothername);
                        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);

                        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                        else
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                        else
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);


                        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                        objParams[65] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[66] = new SqlParameter("@P_USER_UATYPE", objEM.USER_UATYPE);
                        objParams[67] = new SqlParameter("@P_IsbusFas", objEM.IsBusFac);

                        objParams[68] = new SqlParameter("@UANNO", objEM.UAN1);
                        objParams[69] = new SqlParameter("@EMPLOYEEID", objEM.EmployeeId);
                        objParams[70] = new SqlParameter("@I8", objPM.I8);

                        objParams[71] = new SqlParameter("@P_BLOODGRPNO", objEM.BLOODGRPNO);
                        objParams[72] = new SqlParameter("@P_ISMARITALSTATUS", objEM.MaritalStatus);
                        objParams[73] = new SqlParameter("@P_CHILDFEMALE", objEM.ChildFemale);
                        objParams[74] = new SqlParameter("@P_CHILDMALE", objEM.ChildMale);
                        objParams[75] = new SqlParameter("@P_HANDICAPTYPEID", objEM.HandicapTypeID);
                        objParams[76] = new SqlParameter("@P_ISPHYSICALLYCHALLENGED", objEM.IsPhysicallyChallenged);
                        objParams[77] = new SqlParameter("@P_COLLEGEROOMNO", objEM.CollegeRoomNo);
                        objParams[78] = new SqlParameter("@P_COLLEGEINTERCOMNO", objEM.CollegeIntercomNo);
                        objParams[79] = new SqlParameter("@P_QUALFORDISPLAY", objEM.QualForDisplay);
                        objParams[80] = new SqlParameter("@P_EMPLOYMENT", objEM.Employment);
                        if (!objEM.QuartersAllotmentDate.Equals(DateTime.MinValue))
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        else
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", DBNull.Value);
                        // objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        objParams[82] = new SqlParameter("@Age", objEM.Age);

                        objParams[83] = new SqlParameter("@isCabFac", objEM.IsCabfac);
                        objParams[84] = new SqlParameter("@isTelguMin", objEM.IsTelguMin);
                        objParams[85] = new SqlParameter("@isDrugAlrg", objEM.IsDrugAlrg);
                        objParams[86] = new SqlParameter("@IFSC_CODE", objEM.IFSC_CODE);
                        objParams[87] = new SqlParameter("@ALTERNATE_EMAILID", objEM.ALTERNATEEMAILID);
                        objParams[88] = new SqlParameter("@ALTERNATE_PHONENO", objEM.ALTERNATEPHONENO);
                        objParams[89] = new SqlParameter("@IS_SHIFT_MANAGMENT", objEM.IS_SHIFT_MANAGMENT);
                        objParams[90] = new SqlParameter("@ESICNO", objEM.ESICNO);
                        objParams[91] = new SqlParameter("@P_ISNEFT", objEM.IsNEFT);
                        // objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);

                        // if (objEM.Photo == null)
                        if (objEM.PhotoSign == null)
                            objParams[92] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);

                        objParams[92].SqlDbType = SqlDbType.Image;

                        objParams[93] = new SqlParameter("@P_MAINDEPTNO", objEM.MAINDEPTNO);
                        //objParams[94] = new SqlParameter("@P_DAHEADID ", objEM.DAHEADID);
                        objParams[94] = new SqlParameter("@P_DIVIDNO", objPM.DIVISIONMASTER);   //add amol 

                        objParams[95] = new SqlParameter("@P_IsOnNotice", objEM.IsNoticePeriod);

                        if (!objEM.RESIGNATIONDATE.Equals(DateTime.MinValue))
                            objParams[96] = new SqlParameter("@P_DateOfResignation", objEM.RESIGNATIONDATE);
                        else
                            objParams[96] = new SqlParameter("@P_DateOfResignation", DBNull.Value);

                        objParams[97] = new SqlParameter("@P_AttritionTypeNo", objEM.AttritionTypeNo);

                        objParams[98] = new SqlParameter("@P_ExitReason", objEM.RESIGNATIONRESASON);

                        objParams[99] = new SqlParameter("@P_EnityNo", objEM.EnityNo);
                        if (!objEM.GROUPOFDOJ.Equals(DateTime.MinValue))
                            objParams[100] = new SqlParameter("@P_GroupOfDOJ", objEM.GROUPOFDOJ);
                        else
                            objParams[100] = new SqlParameter("@P_GroupOfDOJ", DBNull.Value);

                        objParams[101] = new SqlParameter("@P_STATE", objEM.STATE);
                        objParams[102] = new SqlParameter("@P_COUNTRY", objEM.COUNTRY);
                        objParams[103] = new SqlParameter("@P_CITY", objEM.CITY);
                        if (!objEM.EXITDATE.Equals(DateTime.MinValue))
                            objParams[104] = new SqlParameter("@P_EXIT_DATE", objEM.EXITDATE);

                        else
                            objParams[104] = new SqlParameter("@P_EXIT_DATE", DBNull.Value);

                        objParams[105] = new SqlParameter("@P_PAYLEVELNO", objEM.PaylevelId);
                        objParams[106] = new SqlParameter("@P_CELLNO", objEM.CellNumber);
                        objParams[107] = new SqlParameter("@P_IsBioAuthorityPerson", objEM.IsBioAuthorityPerson);
                        objParams[108] = new SqlParameter("@P_HRA_HEADID", objEM.HRA_HEADID);
                        objParams[109] = new SqlParameter("@P_DAHEADID ", objEM.DAHEADID);
                        objParams[110] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[110].Direction = ParameterDirection.Output;
                        
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_UPD_EMP", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                    }
                    return retStatus;
                }
                //Bulk Photo Upload
                public int EmployeeBulkPhotoUpdate(int ddlValue, DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        // if (!(photo == null))
                        // {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ID", ddlValue);
                        objParams[1] = new SqlParameter("@P_EMP_BULK_UPDATE", dt);

                        if (objSQLHelper.ExecuteNonQuerySP("EMP_BULK_PHOTO_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        // }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateEmployee_CRES(EmpMaster objEM, PayMaster objPM, ITMaster objIT, int rfid, string mothername)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update New Employee
                        objParams = new SqlParameter[99];
                        objParams[0] = new SqlParameter("@P_SEQ_NO", objEM.SEQ_NO);
                        objParams[1] = new SqlParameter("@P_TITLE", objEM.TITLE);
                        objParams[2] = new SqlParameter("@P_FNAME", objEM.FNAME);
                        objParams[3] = new SqlParameter("@P_MNAME", objEM.MNAME);
                        objParams[4] = new SqlParameter("@P_LNAME", objEM.LNAME);
                        objParams[5] = new SqlParameter("@P_SEX", objEM.SEX);
                        objParams[6] = new SqlParameter("@P_FATHERNAME", objEM.FATHERNAME);

                        if (!objEM.DOB.Equals(DateTime.MinValue))
                            objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                        else
                            objParams[7] = new SqlParameter("@P_DOB", DBNull.Value);

                        if (!objEM.DOJ.Equals(DateTime.MinValue))
                            objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        else
                            objParams[8] = new SqlParameter("@P_DOJ", DBNull.Value);

                        if (!objEM.DOI.Equals(DateTime.MinValue))
                            objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                        else
                            objParams[9] = new SqlParameter("@P_DOI", DBNull.Value);

                        if (!objEM.RDT.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);
                        else
                            objParams[10] = new SqlParameter("@P_RDT", DBNull.Value);


                        //objParams[7] = new SqlParameter("@P_DOB", objEM.DOB);
                        //objParams[8] = new SqlParameter("@P_DOJ", objEM.DOJ);
                        //objParams[9] = new SqlParameter("@P_DOI", objEM.DOI);
                        //objParams[10] = new SqlParameter("@P_RDT", objEM.RDT);

                        objParams[11] = new SqlParameter("@P_HP", objEM.HP);
                        objParams[12] = new SqlParameter("@P_SUBDEPTNO", objEM.SUBDEPTNO);
                        objParams[13] = new SqlParameter("@P_DESIG_COLLNO", objPM.SUBDESIGNO);
                        objParams[14] = new SqlParameter("@P_STNO", objEM.STNO);
                        objParams[15] = new SqlParameter("@P_STAFFNO", objEM.STAFFNO);
                        objParams[16] = new SqlParameter("@P_BANKACC_NO", objEM.BANKACC_NO);
                        objParams[17] = new SqlParameter("@P_GPF_NO", objEM.GPF_NO);
                        objParams[18] = new SqlParameter("@P_PPF_NO ", objEM.PPF_NO);
                        objParams[19] = new SqlParameter("@P_PAN_NO", objEM.PAN_NO);
                        objParams[20] = new SqlParameter("@P_BANKNO", objEM.BANKNO);
                        objParams[21] = new SqlParameter("@P_QUARTER", objEM.QUARTER);
                        objParams[22] = new SqlParameter("@P_QTRNO", objEM.QTRNO);
                        objParams[23] = new SqlParameter("@P_REMARK", objEM.REMARK);
                        objParams[24] = new SqlParameter("@P_ANFN", objEM.ANFN);
                        objParams[25] = new SqlParameter("@P_QRENT_YN", objEM.QRENT_YN);
                        objParams[26] = new SqlParameter("@P_PSTATUS", objPM.PSTATUS);
                        objParams[27] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                        objParams[28] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                        objParams[29] = new SqlParameter("@P_SCALENO", objPM.SCALENO);
                        objParams[30] = new SqlParameter("@P_DESIGNATURENO", objPM.DESIGNATURENO);
                        objParams[31] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                        objParams[32] = new SqlParameter("@P_BASIC", objPM.BASIC);
                        objParams[33] = new SqlParameter("@P_TA", objPM.TA);
                        objParams[34] = new SqlParameter("@P_IDNO", objEM.IDNO);
                        objParams[35] = new SqlParameter("@P_GRADEPAY", objPM.GRADEPAY);

                        if (objEM.Photo == null)
                            objParams[36] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[36] = new SqlParameter("@P_PHOTO", objEM.Photo);

                        objParams[36].SqlDbType = SqlDbType.Image;
                        objParams[37] = new SqlParameter("@P_PFNO", objEM.PFNO);

                        objParams[38] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                        objParams[39] = new SqlParameter("@P_CLNO", objEM.CLNO);

                        objParams[40] = new SqlParameter("@P_BANKCITYNO", objEM.BANKCITYNO);
                        objParams[41] = new SqlParameter("@P_NUNIQUEID", objEM.NUNIQUEID);
                        objParams[42] = new SqlParameter("@P_RFID", rfid);

                        objParams[43] = new SqlParameter("@P_DESIG_UNIVNO", objEM.SUBDESIGNO);
                        objParams[44] = new SqlParameter("@P_SHIFTNO", objEM.SHIFTNO);
                        objParams[45] = new SqlParameter("@P_ACTIVE", objEM.SACTIVE);
                        objParams[46] = new SqlParameter("@P_NUDESIGNO", objEM.NUDESIGNO);
                        objParams[47] = new SqlParameter("@P_MAIDENNAME", objEM.MAIDENNAME);
                        objParams[48] = new SqlParameter("@P_HUSBANDNAME", objEM.HUSBANDNAME);
                        objParams[49] = new SqlParameter("@P_STATUSNO", objEM.STATUSNO);
                        if (!objEM.STDATE.Equals(DateTime.MinValue))
                            objParams[50] = new SqlParameter("@P_STDATE", objEM.STDATE);
                        else
                            objParams[50] = new SqlParameter("@P_STDATE", DBNull.Value);
                        objParams[51] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                        objParams[52] = new SqlParameter("@P_RESADD", objEM.RESADD1);
                        objParams[53] = new SqlParameter("@P_TOWNADD", objEM.TOWNADD1);

                        objParams[54] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                        objParams[55] = new SqlParameter("@P_MOTHERNAME", mothername);
                        objParams[56] = new SqlParameter("@P_EPF_EXTRA", objEM.EPF_EXTRA);
                        objParams[57] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[58] = new SqlParameter("@P_SENIOR_CITIZEN", objEM.SENIOR_CIIZEN);

                        if (!objEM.RELIEVINGDATE.Equals(DateTime.MinValue))
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);
                        else
                            objParams[59] = new SqlParameter("@P_RELIEVING_DATE", DBNull.Value);
                        //objParams[59] = new SqlParameter("@P_RELIEVING_DATE", objEM.RELIEVINGDATE);

                        if (!objEM.EXPDATEOFEXT.Equals(DateTime.MinValue))
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);
                        else
                            objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", DBNull.Value);
                        //objParams[60] = new SqlParameter("@P_EXPDATEOFEXT", objEM.EXPDATEOFEXT);

                        objParams[61] = new SqlParameter("@P_EMPLOYEE_LOCK", objEM.EMPLOYEE_LOCK);
                        objParams[62] = new SqlParameter("@P_UA_TYPE", objEM.UA_TYPE);

                        objParams[63] = new SqlParameter("@P_EMPTYPENO", objEM.EMPTYPENO);
                        objParams[64] = new SqlParameter("@P_PGSUBDEPTNO", objEM.PGDEPTNO);


                        //objParams[65] = new SqlParameter("@P_FNAME_UNICODE", objEM.FNAME_UNICODE);
                        //objParams[66] = new SqlParameter("@P_MNAME_UNICODE", objEM.MNAME_UNICODE);
                        //objParams[67] = new SqlParameter("@P_LNAME_UNICODE", objEM.LNAME_UNICODE);
                        //objParams[68] = new SqlParameter("@P_FATHERNAME_UNICODE", objEM.FATHERNAME_UNICODE);

                        objParams[65] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[66] = new SqlParameter("@P_USER_UATYPE", objEM.USER_UATYPE);
                        objParams[67] = new SqlParameter("@P_IsbusFas", objEM.IsBusFac);

                        objParams[68] = new SqlParameter("@UANNO", objEM.UAN1);
                        objParams[69] = new SqlParameter("@EMPLOYEEID", objEM.EmployeeId);
                        objParams[70] = new SqlParameter("@I8", objPM.I8);

                        objParams[71] = new SqlParameter("@P_BLOODGRPNO", objEM.BLOODGRPNO);
                        objParams[72] = new SqlParameter("@P_ISMARITALSTATUS", objEM.MaritalStatus);
                        objParams[73] = new SqlParameter("@P_CHILDFEMALE", objEM.ChildFemale);
                        objParams[74] = new SqlParameter("@P_CHILDMALE", objEM.ChildMale);
                        objParams[75] = new SqlParameter("@P_HANDICAPTYPEID", objEM.HandicapTypeID);
                        objParams[76] = new SqlParameter("@P_ISPHYSICALLYCHALLENGED", objEM.IsPhysicallyChallenged);
                        objParams[77] = new SqlParameter("@P_COLLEGEROOMNO", objEM.CollegeRoomNo);
                        objParams[78] = new SqlParameter("@P_COLLEGEINTERCOMNO", objEM.CollegeIntercomNo);
                        objParams[79] = new SqlParameter("@P_QUALFORDISPLAY", objEM.QualForDisplay);
                        objParams[80] = new SqlParameter("@P_EMPLOYMENT", objEM.Employment);
                        if (!objEM.QuartersAllotmentDate.Equals(DateTime.MinValue))
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        else
                            objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", DBNull.Value);
                        // objParams[81] = new SqlParameter("@P_QUARTERSALLOTMENTDATE", objEM.QuartersAllotmentDate);
                        objParams[82] = new SqlParameter("@Age", objEM.Age);

                        objParams[83] = new SqlParameter("@isCabFac", objEM.IsCabfac);
                        objParams[84] = new SqlParameter("@isTelguMin", objEM.IsTelguMin);
                        objParams[85] = new SqlParameter("@isDrugAlrg", objEM.IsDrugAlrg);
                        objParams[86] = new SqlParameter("@IFSC_CODE", objEM.IFSC_CODE);
                        objParams[87] = new SqlParameter("@ALTERNATE_EMAILID", objEM.ALTERNATEEMAILID);
                        objParams[88] = new SqlParameter("@ALTERNATE_PHONENO", objEM.ALTERNATEPHONENO);
                        objParams[89] = new SqlParameter("@IS_SHIFT_MANAGMENT", objEM.IS_SHIFT_MANAGMENT);
                        objParams[90] = new SqlParameter("@ESICNO", objEM.ESICNO);
                        objParams[91] = new SqlParameter("@P_ISNEFT", objEM.IsNEFT);
                        // objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);

                        if (objEM.Photo == null)
                            objParams[92] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[92] = new SqlParameter("@P_PHOTOSign", objEM.PhotoSign);
                        objParams[92].SqlDbType = SqlDbType.Image;
                        objParams[93] = new SqlParameter("@P_MAINDEPTNO", objEM.MAINDEPTNO);
                        objParams[94] = new SqlParameter("@P_DAHEADID", objEM.DAHEADID);
                        objParams[95] = new SqlParameter("@P_USERSTATUS", objEM.UserStatus);
                        objParams[96] = new SqlParameter("@P_IsServicemen", objEM.ExServicemen);
                        objParams[97] = new SqlParameter("@P_IsBioAuthorityPerson", objEM.IsBioAuthorityPerson);
                        objParams[98] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[98].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_SP_UPD_EMP_CRESCENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DateTime RetirementDate(int staffNo, DateTime birthDate)
                {
                    object ret = null;
                    DateTime retireDate = DateTime.Now;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        if (!birthDate.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_DOB", birthDate);
                        else
                            objParams[1] = new SqlParameter("@P_DOB", DBNull.Value);

                        ret = objSQLHelper.ExecuteScalarSP("PKG_EMP_SP_RET_RETIREMENTAGE", objParams);

                        if (ret != null)
                            retireDate = Convert.ToDateTime(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetirementDate -> " + ex.ToString());
                    }
                    return retireDate;
                }

                //ADDED FOR EMPLOYEE PROFILE INFO SELECT 
                public DataTableReader GetEmpInfo(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_EMP_PROFILE", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;

                }

                public DataTableReader GetStudInfo(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUD_PROFILE", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                //public int UpdateStuInfo(int idno, string Phno, string EmailId, string UName)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;

                //        //Update New Employee
                //        objParams = new SqlParameter[4];

                //        objParams[0] = new SqlParameter("@P_UA_NO", idno);
                //        objParams[1] = new SqlParameter("@P_PHONNO", Phno);
                //        objParams[2] = new SqlParameter("@P_EMAILID", EmailId);
                //        objParams[3] = new SqlParameter("@P_UNAME", UName);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STUDINFO", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                //    }
                //    return retStatus;

                //}

                public int UpdateStuInfo(int idno, string Phno, string EmailId)
                //, string UName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update New Employee
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_UA_NO", idno);
                        objParams[1] = new SqlParameter("@P_PHONNO", Phno);
                        objParams[2] = new SqlParameter("@P_EMAILID", EmailId);
                        //objParams[3] = new SqlParameter("@P_UNAME", UName);        Modified By Nikhil Lambe on 16/03/2021
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STUDINFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                    }
                    return retStatus;

                }
                //TO UPDATE EMPLOYEE PROFILE INFO
                public int UpdateEmpInfo(int idno, string Phno, string EmailId, string BlockNo1, string BlockNo2, string RoomNo1, string RoomNo2, string CabinNo1, string CabinNo2, string IntercomNo1, string IntercomNo2, string sp1, string sp2, string sp3, string sp4, string sp5, byte[] Photo, int IdNoAcc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update New Employee
                        objParams = new SqlParameter[18];

                        objParams[0] = new SqlParameter("@P_UA_NO", idno);
                        objParams[1] = new SqlParameter("@P_PHONNO", Phno);
                        objParams[2] = new SqlParameter("@P_EMAILID", EmailId);
                        objParams[3] = new SqlParameter("@P_BLOCKNO1", BlockNo1);
                        objParams[4] = new SqlParameter("@P_BLOCKNO2", BlockNo2);
                        objParams[5] = new SqlParameter("@P_ROOMNO1", RoomNo1);
                        objParams[6] = new SqlParameter("@P_ROOMNO2", RoomNo2);
                        objParams[7] = new SqlParameter("@P_CABINNO1", CabinNo1);
                        objParams[8] = new SqlParameter("@P_CABINNO2", CabinNo2);
                        objParams[9] = new SqlParameter("@P_INTERCOMNO1", IntercomNo1);
                        objParams[10] = new SqlParameter("@P_INTERCOMNO2", IntercomNo2);
                        objParams[11] = new SqlParameter("@P_SPEC1", sp1);
                        objParams[12] = new SqlParameter("@P_SPEC2", sp2);
                        objParams[13] = new SqlParameter("@P_SPEC3", sp3);
                        objParams[14] = new SqlParameter("@P_SPEC4", sp4);
                        objParams[15] = new SqlParameter("@P_SPEC5", sp5);
                        //objParams[16] = new SqlParameter("@P_PHOTO", Photo);
                        if (Photo == null)
                            objParams[16] = new SqlParameter("@P_PHOTO", DBNull.Value);
                        else
                            objParams[16] = new SqlParameter("@P_PHOTO", Photo);
                        objParams[16].SqlDbType = SqlDbType.Image;
                        objParams[17] = new SqlParameter("@P_IDNO", IdNoAcc);
                        
                        
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_EMPINFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployee -> " + ex.ToString());
                    }
                    return retStatus;

                }


                public int AddBankManagment(string bankname, string bankcode, string bankaddress, string branchname, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Qualification
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_BANKNAME", bankname);
                        objParams[1] = new SqlParameter("@P_BANKCODE", bankcode);
                        objParams[2] = new SqlParameter("@P_BANKADDR", bankaddress);
                        objParams[3] = new SqlParameter("@P_BRANCHNAME", branchname);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_PAY_BANK_MANAGEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddBankManagment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateBank(string BANKNAME, string BANKCODE, string BANKADDR, string BRANCHNAME, int BANKNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Qualification
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_BANKNAME", BANKNAME);
                        objParams[1] = new SqlParameter("@P_BANKCODE", BANKCODE);
                        objParams[2] = new SqlParameter("@P_BANKADDR", BANKADDR);
                        objParams[3] = new SqlParameter("@P_BRANCHNAME", BRANCHNAME);
                        objParams[4] = new SqlParameter("@P_BANKNO", BANKNO);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_PAY_BANK_MANAGMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateBank-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetBANKID(int BANKNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BANKNO", BANKNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_RET_BANK_MANAGMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetBANKID-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetEmployeeCode()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEECODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetEmployeeCode-> " + ex.ToString());
                    }
                    return ds;
                }
                #region
                public int InsertDAHEADMaster(int DAHEADID, string DAHEADDescription, int CollegeNo, int CreatedBy, int OrganizationId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DA_HEADID", DAHEADID);
                        objParams[1] = new SqlParameter("@P_DA_HEAD_DESCRIPTION", DAHEADDescription);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                        objParams[3] = new SqlParameter("@P_CREATED_BY ", CreatedBy);
                        objParams[4] = new SqlParameter("@P_OrganizationId", OrganizationId);
                        objParams[5] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_DA_HEAD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetBANKID-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateDAHEADMaste(int DAHEADID, string DAHEADDescription, int CollegeNo, int CreatedBy, int OrganizationId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DA_HEADID", DAHEADID);
                        objParams[1] = new SqlParameter("@P_DA_HEAD_DESCRIPTION", DAHEADDescription);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                        objParams[3] = new SqlParameter("@P_MODIFIED_BY", CreatedBy);
                        objParams[4] = new SqlParameter("@P_OrganizationId", OrganizationId);
                        objParams[5] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_DA_HEAD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetBANKID-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetDAHeadDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_DA_HEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetBANKID-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GETDAHEADCalculation(int DAHEADID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[1];

                        objParam[0] = new SqlParameter("@P_DAHEADID", DAHEADID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_DA_HEAD_CALCULATION", objParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.GetBANKID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateDAHeadCalculationDetails(int DAHeadId, int HRAHeadCalPer, string DAHeadCalDate, int Collegeno, int OrganizationId, int CreatedBy, int DAHeadCalPer, DataTable Dtg, bool iSDetailCal)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_DA_HEADID", DAHeadId);
                        objParams[1] = new SqlParameter("@P_DA_PER", DAHeadCalPer);
                        objParams[2] = new SqlParameter("@P_HRA_PER", HRAHeadCalPer);
                        objParams[3] = new SqlParameter("@P_DA_HEAD_CALCULATION_DATE", DAHeadCalDate);
                        objParams[4] = new SqlParameter("@P_TBL_DAHRACAL", Dtg);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", Collegeno);
                        objParams[6] = new SqlParameter("@P_OrganizationId", OrganizationId);
                        objParams[7] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                        objParams[8] = new SqlParameter("@P_IS_DETAILCAL", iSDetailCal);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_DA_HEAD_CALCULATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.UpdateDAHeadCalculationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region
                //17-10-2022
                //add for duplicate record all read exits message 
                public string GetPayrollCheckFieldsAlreadyExists(EmpMaster objEM, int rfid)
                {
                    // DataSet ds = null;
                    string retStatus = "";
                    string ret = "";
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PFILENO", objEM.PFILENO);
                        objParams[1] = new SqlParameter("@P_PHONENO", objEM.PHONENO);
                        objParams[2] = new SqlParameter("@P_EMAILID", objEM.EMAILID);
                        objParams[3] = new SqlParameter("@P_RFIDNO", rfid);
                        objParams[4] = new SqlParameter("@P_EmployeeId ", objEM.EmployeeId);
                        objParams[5] = new SqlParameter("@P_IDNO ", objEM.IDNO);

                        ret = Convert.ToString(objSQLHelper.ExecuteScalarSP("PAYROLL_CHECK_FIELDS_ALREADY_EXISTS", objParams));
                        if (ret == "")
                        {
                            retStatus = "";
                        }
                        else
                        {
                            retStatus = ret.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetPayHeadsforbulkupdate-> " + ex.ToString());
                    }
                    finally
                    {


                    }
                    return retStatus;

                }

                #endregion



                #region ServiceBookMaster
                public DataTable MenuDetails(int UsertypeId, int Parentmenuid)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@UserTypeId", UsertypeId);
                        objParams[1] = new SqlParameter("@ParentMenuId", Parentmenuid);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_BIND_MENU_DATA", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }

                #endregion



                public DataTableReader RetrieveEmpDetailsNoDues(string search, string category)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_FOR_NODUES", objParams).Tables[0].CreateDataReader();
                        // dtr = objSQLHelper.ExecuteReaderSP("PKG_EMP_SP_RET_EMPLOYEE_BYID", objParams)as SqlDataReader ;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }


                public DataTable RetrieveEmpDetailsNoDuesNew(string search, string category)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_FOR_NODUES", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }

                public DataTable RetrieveEmpDetailJoining(string search, string category)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_FOR_JOINING", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return dt;
                }


                public DataSet GetAllNoDuesDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];


                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_NODUES_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllNoDuesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIDNOWiseNoDuesDetails(int idno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EMP_WISE_NODUES_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetNoDuesNOWiseDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetUANOWiseNoDuesDetails(int UA_NO, int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_UANO_WISE_NODUES_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetUANOWiseNoDuesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetNoDuesNOWiseDetails(int NODUES_NO)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NODUES_NO", NODUES_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_NODUES_NO_WISE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetNoDuesNOWiseDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public string InsertNoDuesEntry(EmpMaster objEM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference

                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_NODUES_STATUS", objEM.Nodues_Status);
                        objParams[2] = new SqlParameter("@P_REMARK", objEM.Remark);
                        objParams[3] = new SqlParameter("@P_CREATED_BY", objEM.Created_By);
                        objParams[4] = new SqlParameter("@P_IP_ADDRESS", objEM.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_AUTHO_TYP_ID", objEM.AUTHO_TYPE_ID);
                        objParams[6] = new SqlParameter("@P_AUTHO_ID", objEM.AUTHO_ID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_NO_DUES_ENTRY", objParams, true);

                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.InsertNoDuesEntry-> " + ex.ToString());
                    }
                    //return retStatus;
                }

                public string UpdateNoDuesEntry(EmpMaster objEM)
                {

                    string retun_status = string.Empty;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[9];
                        //First Add Student Parameter

                        objParams[0] = new SqlParameter("@P_NODUES_NO", objEM.Nodues_No);
                        objParams[1] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[2] = new SqlParameter("@P_NODUES_STATUS", objEM.Nodues_Status);
                        objParams[3] = new SqlParameter("@P_REMARK", objEM.Remark);
                        objParams[4] = new SqlParameter("@P_CREATED_BY", objEM.Created_By);
                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", objEM.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_AUTHO_TYP_ID", objEM.AUTHO_TYPE_ID);
                        objParams[7] = new SqlParameter("@P_AUTHO_ID", objEM.AUTHO_ID);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_NO_DUES_ENTRY", objParams, true);

                        retun_status = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retun_status = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateNoDuesEntry-> " + ex.ToString());
                    }

                    return retun_status;

                }

                #region Payroll_AssetAllotmentofEmployee
                // FUNCTION ADDED BY PURVA RAUT ON 9_06-2021 for ASset add
                public DataTable RetrieveEmpDetailsForaSsetAllotment(string search, string category)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCH", search);
                        objParams[1] = new SqlParameter("@P_CATEGORY", category);

                        dt = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_SEARCH_EMPLOYEE_FOR_ASSETALLOTMENT", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetailsForaSsetAllotment-> " + ex.ToString());
                    }
                    return dt;
                }
                public int CHeckAssetRecordAlreadyAvailable(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_ASSETID", objEM.ASSETID);
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_EMPLOYEE_ASSETALLOTMENT_CHECK", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.CHeckAssetRecordAlreadyAvailable->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int SaveEmployeeAssetAllotment(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_ASSETID", objEM.ASSETID);
                        objParams[3] = new SqlParameter("@P_ASSETREMARK", objEM.ASSETREMARK);
                        // objParams[4] = new SqlParameter("@P_ISAPPROVED", objEM.ISAPPROVED);
                        objParams[4] = new SqlParameter("@P_ASSETNAME", objEM.ASSETNAME);
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_EMPLOYEE_ASSETALLOTMENT", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeAssetAllotment->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int UpdateEmployeeAssetAllotment(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_ASSETID", objEM.ASSETID);
                        objParams[3] = new SqlParameter("@P_ASSETREMARK", objEM.ASSETREMARK);
                        objParams[4] = new SqlParameter("@P_ASSETALLOTID", objEM.ASSETALLOTID);
                        objParams[5] = new SqlParameter("@P_ASSETNAME", objEM.ASSETNAME);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMPLOYEE_ASSETALLOTMENT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployeeAssetAllotment->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int ApprovedEmpAssetAllotment(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_ASSETID", objEM.ASSETID);
                        objParams[3] = new SqlParameter("@P_ASSETREMARK", objEM.ASSETREMARK);
                        objParams[4] = new SqlParameter("@P_ASSETALLOTID", objEM.ASSETALLOTID);
                        objParams[5] = new SqlParameter("@P_ASSETNAME", objEM.ASSETNAME);
                        objParams[6] = new SqlParameter("@P_ISAPPROVED", objEM.ISAPPROVED);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMP_ASSETAPPROVED", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.ApprovedEmpAssetAllotment->" + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllEmpAssetDetail(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPASSET_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpAssetDetail-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllEmpAssetDetailIDWISE(int Assetallotid)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ASSETALLOTID", Assetallotid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPASSETALLOTID_WISE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpAssetDetailIDWISE-> " + ex.ToString());
                    }
                    return ds;
                }
                // FOR APPROVAL OF ASSET DETAILE OF EMPLOYEE on 10_06_2021
                public DataSet GetAllEmpAssetDetailALL()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPASSET_DETAILS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpAssetDetailALL-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllEmpAssetDetailDatewise(DateTime fromdate, DateTime todate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[1] = new SqlParameter("@P_TODATE", todate);
                        //objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        //objParams[2].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPASSET_DETAIL_DATEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpAssetDetailDatewise-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Employee Mail Authority
                public int SaveEmployeeMailAuthoriy(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_COLLEGEID", objEM.COLLEGEID);
                        objParams[1] = new SqlParameter("@P_EMPNAME", objEM.EMPNAME);
                        objParams[2] = new SqlParameter("@P_MAILID", objEM.EMAILID);
                        objParams[3] = new SqlParameter("@P_PASSWORD", objEM.PASSWORD);
                        objParams[4] = new SqlParameter("@P_ISACTIVE", objEM.IsActive);
                        objParams[5] = new SqlParameter("@P_NOTIFICATIONDAYS", objEM.NOTIFICATIONDAYS);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_EMPLOYEE_MAILAUTHORITY", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeMailAuthoriy->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int UpdateEmployeeMailAuthority(EmpMaster objEM)
                {

                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_EMPMAILAUTHID", objEM.EMPAUTHMAILID);
                        objParams[1] = new SqlParameter("@P_COLLEGEID", objEM.COLLEGEID);
                        objParams[2] = new SqlParameter("@P_EMPNAME", objEM.EMPNAME);
                        objParams[3] = new SqlParameter("@P_MAILID", objEM.EMAILID);
                        objParams[4] = new SqlParameter("@P_PASSWORD", objEM.PASSWORD);
                        objParams[5] = new SqlParameter("@P_ISACTIVE", objEM.IsActive);
                        objParams[6] = new SqlParameter("@P_NOTIFICATIONDAYS", objEM.NOTIFICATIONDAYS);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMPLOYEE_MAILAUTHORITY", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployeeMailAuthority->" + ex.ToString());
                    }
                    return retStatus;

                }
                public DataSet GetAllEmpMailAUthority()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPLOYEEMAILAUTHORITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpMailAUthority-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllEmplMailAuthorityIDWISE(int EMPMAILAUTHID)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPMAILAUTHID", EMPMAILAUTHID);
                        objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPLOYEEMAILAUTHORITYIDWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmplMailAuthorityIDWISE-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Payroll_EmployeeResignation
                public int SaveEmployeeResignation(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_RESIGNATIONDATE", objEM.RESIGNATIONDATE);
                        objParams[3] = new SqlParameter("@P_RESIGNATIONREMARK", objEM.RESIGNATIONREMARK);
                        objParams[4] = new SqlParameter("@P_EXITTYPE", objEM.EXITTYPEID);
                        objParams[5] = new SqlParameter("@P_REG_NOTICE_PERIOD", objEM.PNOTICEPERIOD);
                        objParams[6] = new SqlParameter("@P_PASSTYPE", objEM.PASSTYPE);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_EMPLOYEE_RESIGNATION", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateEmployeeResignationForPassingPath(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_PASSTYPE", objEM.PASSTYPE);
                        objParams[3] = new SqlParameter("@P_Remark", objEM.REMARK);

                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_EMPLOYEE_RESIGNATION_PASSING_PATH", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateEmployeeResignation(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_RESIGNATIONDATE", objEM.RESIGNATIONDATE);
                        objParams[3] = new SqlParameter("@P_RESIGNATIONREMARK", objEM.RESIGNATIONREMARK);
                        objParams[4] = new SqlParameter("@P_EXITTYPE", objEM.EXITTYPEID);
                        objParams[5] = new SqlParameter("@P_EMPRESIGNATIONID", objEM.RESIGNATIONEMPID);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMPLOYEE_RESIGNATION", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int UpdateEmployeeResignationNew(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_RESIGNATIONDATE", objEM.RESIGNATIONDATE);
                        objParams[3] = new SqlParameter("@P_RESIGNATIONREMARK", objEM.RESIGNATIONREMARK);
                        objParams[4] = new SqlParameter("@P_REGSTATUS", objEM.REGSTATUS);
                        objParams[5] = new SqlParameter("@P_EMPRESIGNATIONID", objEM.RESIGNATIONEMPID);
                        objParams[6] = new SqlParameter("@P_HR_RESIGNATION_REMARK", objEM.PHRRESIGNATIONREMARK);
                        objParams[7] = new SqlParameter("@P_REG_NOTICE_PERIOD", objEM.PNOTICEPERIOD);
                        //objParams[8] = new SqlParameter("@P_REG_RELEVING_DATE", objEM.REGRELEVINGDATE);
                        objParams[8] = new SqlParameter("@P_ISNODUES", objEM.ISNODUES);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMPLOYEE_RESIGNATION_NEW", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;


                }

                public int UpdateEmployeeResignationUpdated(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_IDNO", objEM.IdNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        objParams[2] = new SqlParameter("@P_RESIGNATIONDATE", objEM.RESIGNATIONDATE);
                        objParams[3] = new SqlParameter("@P_RESIGNATIONREMARK", objEM.RESIGNATIONREMARK);
                        objParams[4] = new SqlParameter("@P_REGSTATUS", objEM.REGSTATUS);
                        objParams[5] = new SqlParameter("@P_EMPRESIGNATIONID", objEM.RESIGNATIONEMPID);
                        objParams[6] = new SqlParameter("@P_HR_RESIGNATION_REMARK", objEM.PHRRESIGNATIONREMARK);
                        objParams[7] = new SqlParameter("@P_REG_NOTICE_PERIOD", objEM.PNOTICEPERIOD);
                        objParams[8] = new SqlParameter("@P_COMMANDTYPE", objEM.COMMANDTYPE);
                        objParams[9] = new SqlParameter("@P_ISNODUES", objEM.ISNODUES);
                        objParams[10] = new SqlParameter("@P_EXITTYPEID", objEM.EXITTYPEID);

                        // objParams[11] = new SqlParameter("@P_RELEVINGDATE", objEM.REGRELEVINGDATE);
                        if (objEM.REGRELEVINGDATE != DateTime.MinValue)
                            objParams[11] = new SqlParameter("@P_RELEVINGDATE", objEM.REGRELEVINGDATE);
                        else
                            objParams[11] = new SqlParameter("@P_RELEVINGDATE", DBNull.Value);

                        if (objEM.FINALAMOUNT != null)
                        {
                            objParams[12] = new SqlParameter("@P_FINALAMOUNT", objEM.FINALAMOUNT);
                        }
                        else
                        {
                            objParams[12] = new SqlParameter("@P_FINALAMOUNT", DBNull.Value);
                        }
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UP_EMPLOYEE_RESIGNATION_UPDATED", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.UpdateEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;


                }


                public DataSet GetAllEmpResignationDetail(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPID_RESIGNATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationDetail-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllEmpResignationIDWISE(int EmpResignationId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EMPRESIGNATIONID", EmpResignationId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMPRESIGNATIONID_WISE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationIDWISE-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllEmpResignationDetailALL()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPID_RESIGNATION_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationDetailALL-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllEmpResignationDetailALLForPassingPath(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPID_RESIGNATION_ALL_PASSING_AUTHO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationDetailALL-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllEmpResignationDetailDatewise(DateTime fromdate, DateTime todate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[1] = new SqlParameter("@P_TODATE", todate);
                        //objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        //objParams[2].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPID_RESIGNATION_DATEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationDetailDatewise-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataTableReader ShowEmpDetailsForResignation(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_Idno", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_EMP_SP_RET_EMPLOYEE_BYID_RESIGNATION", objParams).Tables[0].CreateDataReader();
                        // dtr = objSQLHelper.ExecuteReaderSP("PKG_EMP_SP_RET_EMPLOYEE_BYID", objParams)as SqlDataReader ;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }


                #endregion

                #region Employee Resignation Passing Pass Type


                public DataSet GetAllEmpResigntionPassingDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPPASSINGPASS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpDocumentsDetailALL-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmpRegPassingPassByID(int RegPassId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_REGPASSID", RegPassId);
                        objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPPASSINGPASS_BYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetEmpRegPassingPassByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int SaveEmpResignationPassingPass(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PANAME", objEM.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[2] = new SqlParameter("@P_PASSTYPE ", objEM.PASSTYPE);

                        if (objEM.COLLEGE_NO != 0)
                            objParams[3] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        else
                            objParams[3] = new SqlParameter("@P_COLLEGE_NO", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_INS_EMP_REG_PASSING_PASS", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmpResignationPassingPass->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateEmpResignationPassingPass(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PANAME", objEM.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objEM.UA_NO);
                        objParams[2] = new SqlParameter("@P_PASSTYPE ", objEM.PASSTYPE);

                        if (objEM.COLLEGE_NO != 0)
                            objParams[3] = new SqlParameter("@P_COLLEGE_NO", objEM.COLLEGE_NO);
                        else
                            objParams[3] = new SqlParameter("@P_COLLEGE_NO", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_REGPASSID", objEM.REGPASSID);
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_UP_EMP_REG_PASSING_PASS", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmpResignationPassingPass->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetAllEmpResigntionPassingDetailsByCollege(int CollegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", CollegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMPPASSINGPASS_BYCOLLEGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResigntionPassingDetailsByCollege-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                public DataTableReader getUserProfileImage(string idno, string type)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Idno", idno);
                        objParams[1] = new SqlParameter("@P_TYPE", type);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_STUDEMP_SP_RET_PHOTO", objParams).Tables[0].CreateDataReader();
                        

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.getUserProfileImage->" + ex.ToString());
                    }
                    return dtr;
                }

                   #region Employee Transfer
                public int SaveEmployeeTransfertoCollege(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@EmployeeTransferEntryId", objEM.EMPLOYEETRANSFERID);
                        objParams[1] = new SqlParameter("@EmployeeOldCollegeID", objEM.OLDEMPCOLLEGENO);
                        objParams[2] = new SqlParameter("@EmployeeNewCollegeId", objEM.NEWEMPCOLLEGENO);
                        objParams[3] = new SqlParameter("@EmployeeTransferDate", objEM.EMPTRANSFERDATE);
                        objParams[4] = new SqlParameter("@Transfer_Remark", objEM.EMPTRANSFERRESASON);
                        objParams[5] = new SqlParameter("@CreatedBy", objEM.UA_NO);
                        objParams[6] = new SqlParameter("@Employee_IDNO", objEM.EMPTRANSFERIDNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_INS_EMP_TRANSFER_TOCOLLEGE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                        if (retStatus == 1)
                        {
                            retStatus = 1;
                        }
                        else if (retStatus == 2627)
                        {
                            retStatus = 2627;
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int UpdateEmployeeTransfertoCollege(EmpMaster objEM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@EmployeeTransferEntryId", objEM.EMPLOYEETRANSFERID);
                        objParams[1] = new SqlParameter("@EmployeeOldCollegeID", objEM.OLDEMPCOLLEGENO);
                        objParams[2] = new SqlParameter("@EmployeeNewCollegeId", objEM.NEWEMPCOLLEGENO);
                        objParams[3] = new SqlParameter("@EmployeeTransferDate", objEM.EMPTRANSFERDATE);
                        objParams[4] = new SqlParameter("@Transfer_Remark", objEM.EMPTRANSFERRESASON);
                        objParams[5] = new SqlParameter("@CreatedBy", objEM.UA_NO);
                        objParams[6] = new SqlParameter("@Employee_IDNO", objEM.EMPTRANSFERIDNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMP_UPD_EMP_TRANSFER_TOCOLLEGE", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.SaveEmployeeResignation->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllEmpTransferDetail(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_EMP_TRANSFERTOCOLLEGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationDetail-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllEmpTransferDetailByID(int EmpTransferID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EMPTRANSFERD", EmpTransferID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_EMP_TRANSFERID_WISE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetAllEmpResignationIDWISE-> " + ex.ToString());
                    }
                    return ds;
                }
                   #endregion




                #region daily wages staff
                public DataSet GetDailyWagesEmployeesNew(int collegeNo, int staffNo, string month, int DeptID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGENO ", collegeNo);
                        objParams[1] = new SqlParameter("@P_STAFFNO ", staffNo);
                        objParams[2] = new SqlParameter("@P_MONTH", month);
                        objParams[3] = new SqlParameter("@P_DEPTID", DeptID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_DAILY_WAGES_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
                    }

                    return ds;
                }
                public string CheckMonYearExists(string MonYear)
                {
                    string retun_status = string.Empty;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[2];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@MONYEAR", MonYear);
                        objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_CHECK_SALARY_MONYEAR", objParams, true);

                        retun_status = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retun_status = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateNoDuesEntry-> " + ex.ToString());
                    }

                    return retun_status;
                }
                //  private int UpdateDailyWagesBasicNew(int collegeNo, double idNo, double Basic, double dailyDays, double dailyamt, int StaffId, DateTime MONTHYEAR, double TotalDaysinMonth, double TotalPayabledays,double FixedRemuneration,double NewBasic,bool Status,double AdmissibleWorkingDays,double PayableHolidays,double TotalDays,double TotalPayableDays,int DeptId)
                public int UpdateDailyWagesBasicNew(int collegeNo, double idNo, int StaffId, DateTime MONTHYEAR, double TotalDaysinMonth, double Payabledays, double FixedRemuneration, double NewBasic, bool Status, double AdmissibleWorkingDays, double PayableHolidays, double TotalDays, double TotalPayableDays, int DeptId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        objParams[2] = new SqlParameter("@P_BASIC ", NewBasic);
                        objParams[3] = new SqlParameter("@P_DAILYAMT", FixedRemuneration);
                        objParams[4] = new SqlParameter("@P_DAILYDAYS", TotalDaysinMonth);
                        objParams[5] = new SqlParameter("@P_STAFFID", StaffId);
                        objParams[6] = new SqlParameter("@P_MONTHYEAR", MONTHYEAR);
                        objParams[7] = new SqlParameter("@P_TotalDaysinMonth", TotalDaysinMonth);
                        objParams[8] = new SqlParameter("@P_Payabledays", Payabledays);
                        objParams[9] = new SqlParameter("@P_FixedRemuneration", FixedRemuneration);
                        objParams[10] = new SqlParameter("@P_NewBasic", NewBasic);
                        objParams[11] = new SqlParameter("@P_Attedence_Lock", Status);
                        objParams[12] = new SqlParameter("@P_Admissible_Weekly_Off", AdmissibleWorkingDays);
                        objParams[13] = new SqlParameter("@P_Payable_Holiday", PayableHolidays);
                        objParams[14] = new SqlParameter("@P_Total_Payable_Days", TotalPayableDays);
                        objParams[15] = new SqlParameter("@P_Total_Days", TotalDays);
                        objParams[16] = new SqlParameter("@P_DEPTID", DeptId);
                        objParams[17] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_DAILY_WAGES_BASIC", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateIncerment-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

            }
        }
    }
}