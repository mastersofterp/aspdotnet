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
            public class ITDeclarationController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// TO RETRIVE EMP INFO FOR INCOME TAX DECLARATION FORM.
                /// </summary>
                /// <param name="idno"></param>
                /// <returns></returns>
                public DataTableReader ShowEmpDetails(int idno)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_GET_EMPLOYEE_BYID", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public DataTableReader ShowEmpCHAPVIAmt(int idno, string fdate, string tdate)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EMPNO", idno);
                        objParams[1] = new SqlParameter("@P_FROMDATE", fdate);
                        objParams[2] = new SqlParameter("@P_TODATE", tdate);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_GET_EMPLOYEE_CHVIA_AMOUNT", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.ShowEmpDetails->" + ex.ToString());
                    }
                    return dtr;
                }


                //public int AddITEmplyeeInfo(ITDeclaration objITDec, decimal hravalue, decimal houseint, decimal nscint)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Employee
                //        objParams = new SqlParameter[149];
                //        objParams[0] = new SqlParameter("@P_IDNO", objITDec.IDNO);
                //        objParams[1] = new SqlParameter("@P_ITNO", objITDec.ITNO);
                //        if (!objITDec.FROMDATE.Equals(DateTime.MinValue))
                //            objParams[2] = new SqlParameter("@P_FROMDATE", objITDec.FROMDATE);
                //        else
                //            objParams[2] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                //        if (!objITDec.TODATE.Equals(DateTime.MinValue))
                //            objParams[3] = new SqlParameter("@P_TODATE", objITDec.TODATE);
                //        else
                //            objParams[3] = new SqlParameter("@P_TODATE", DBNull.Value);

                //        objParams[4] = new SqlParameter("@P_VIA1", objITDec.VIA1);
                //        objParams[5] = new SqlParameter("@P_VIA2", objITDec.VIA2);
                //        objParams[6] = new SqlParameter("@P_VIA3", objITDec.VIA3);
                //        objParams[7] = new SqlParameter("@P_VIA4", objITDec.VIA4);
                //        objParams[8] = new SqlParameter("@P_VIA5", objITDec.VIA5);
                //        objParams[9] = new SqlParameter("@P_VIA6", objITDec.VIA6);
                //        objParams[10] = new SqlParameter("@P_VIA7", objITDec.VIA7);
                //        objParams[11] = new SqlParameter("@P_VIA8", objITDec.VIA8);
                //        objParams[12] = new SqlParameter("@P_VIA9", objITDec.VIA9);
                //        objParams[13] = new SqlParameter("@P_VIA10", objITDec.VIA10);
                //        objParams[14] = new SqlParameter("@P_VIA11", objITDec.VIA11);
                //        objParams[15] = new SqlParameter("@P_VIA12", objITDec.VIA12);
                //        objParams[16] = new SqlParameter("@P_VIA13", objITDec.VIA13);

                //        objParams[17] = new SqlParameter("@P_VIB1", objITDec.VIB1);
                //        objParams[18] = new SqlParameter("@P_VIB2", objITDec.VIB2);
                //        objParams[19] = new SqlParameter("@P_VIB3", objITDec.VIB3);
                //        objParams[20] = new SqlParameter("@P_VIB4", objITDec.VIB4);
                //        objParams[21] = new SqlParameter("@P_VIB5", objITDec.VIB5);
                //        objParams[22] = new SqlParameter("@P_VIB6", objITDec.VIB6);
                //        objParams[23] = new SqlParameter("@P_VIB7", objITDec.VIB7);
                //        objParams[24] = new SqlParameter("@P_VIB8", objITDec.VIB8);
                //        objParams[25] = new SqlParameter("@P_VIB9", objITDec.VIB9);
                //        objParams[26] = new SqlParameter("@P_VIB10", objITDec.VIB10);
                //        objParams[27] = new SqlParameter("@P_VIB11", objITDec.VIB11);
                //        objParams[28] = new SqlParameter("@P_VIB12", objITDec.VIB12);
                //        objParams[29] = new SqlParameter("@P_VIB13", objITDec.VIB13);

                //        objParams[30] = new SqlParameter("@P_VIAMT1", objITDec.VIAMT1);
                //        objParams[31] = new SqlParameter("@P_VIAMT2", objITDec.VIAMT2);
                //        objParams[32] = new SqlParameter("@P_VIAMT3", objITDec.VIAMT3);
                //        objParams[33] = new SqlParameter("@P_VIAMT4", objITDec.VIAMT4);
                //        objParams[34] = new SqlParameter("@P_VIAMT5", objITDec.VIAMT5);
                //        objParams[35] = new SqlParameter("@P_VIAMT6", objITDec.VIAMT6);
                //        objParams[36] = new SqlParameter("@P_VIAMT7", objITDec.VIAMT7);
                //        objParams[37] = new SqlParameter("@P_VIAMT8", objITDec.VIAMT8);
                //        objParams[38] = new SqlParameter("@P_VIAMT9", objITDec.VIAMT9);
                //        objParams[39] = new SqlParameter("@P_VIAMT10", objITDec.VIAMT10);
                //        objParams[40] = new SqlParameter("@P_VIAMT11", objITDec.VIAMT11);
                //        objParams[41] = new SqlParameter("@P_VIAMT12", objITDec.VIAMT12);
                //        objParams[42] = new SqlParameter("@P_VIAMT13", objITDec.VIAMT13);

                //        objParams[43] = new SqlParameter("@P_REBP1", objITDec.REBP1);
                //        objParams[44] = new SqlParameter("@P_REBP2", objITDec.REBP2);
                //        objParams[45] = new SqlParameter("@P_REBP3", objITDec.REBP3);
                //        objParams[46] = new SqlParameter("@P_REBP4", objITDec.REBP4);
                //        objParams[47] = new SqlParameter("@P_REBP5", objITDec.REBP5);
                //        objParams[48] = new SqlParameter("@P_REBP6", objITDec.REBP6);
                //        objParams[49] = new SqlParameter("@P_REBP7", objITDec.REBP7);
                //        objParams[50] = new SqlParameter("@P_REBP8", objITDec.REBP8);
                //        objParams[51] = new SqlParameter("@P_REBP9", objITDec.REBP9);
                //        objParams[52] = new SqlParameter("@P_REBP10", objITDec.REBP10);
                //        objParams[53] = new SqlParameter("@P_REBP11", objITDec.REBP11);
                //        objParams[54] = new SqlParameter("@P_REBP12", objITDec.REBP12);
                //        objParams[55] = new SqlParameter("@P_REBP13", objITDec.REBP13);
                //        objParams[56] = new SqlParameter("@P_REBP14", objITDec.REBP14);
                //        objParams[57] = new SqlParameter("@P_REBP15", objITDec.REBP15);
                //        objParams[58] = new SqlParameter("@P_REBP16", objITDec.REBP16);
                //        objParams[59] = new SqlParameter("@P_REBP17", objITDec.REBP17);
                //        objParams[60] = new SqlParameter("@P_REBP18", objITDec.REBP18);
                //        objParams[61] = new SqlParameter("@P_REBP19", objITDec.REBP19);
                //        objParams[62] = new SqlParameter("@P_REBP20", objITDec.REBP20);

                //        objParams[63] = new SqlParameter("@P_REBM1", objITDec.REBM1);
                //        objParams[64] = new SqlParameter("@P_REBM2", objITDec.REBM2);
                //        objParams[65] = new SqlParameter("@P_REBM3", objITDec.REBM3);
                //        objParams[66] = new SqlParameter("@P_REBM4", objITDec.REBM4);
                //        objParams[67] = new SqlParameter("@P_REBM5", objITDec.REBM5);
                //        objParams[68] = new SqlParameter("@P_REBM6", objITDec.REBM6);
                //        objParams[69] = new SqlParameter("@P_REBM7", objITDec.REBM7);
                //        objParams[70] = new SqlParameter("@P_REBM8", objITDec.REBM8);
                //        objParams[71] = new SqlParameter("@P_REBM9", objITDec.REBM9);
                //        objParams[72] = new SqlParameter("@P_REBM10", objITDec.REBM10);
                //        objParams[73] = new SqlParameter("@P_REBM11", objITDec.REBM11);
                //        objParams[74] = new SqlParameter("@P_REBM12", objITDec.REBM12);
                //        objParams[75] = new SqlParameter("@P_REBM13", objITDec.REBM13);
                //        objParams[76] = new SqlParameter("@P_REBM14", objITDec.REBM14);
                //        objParams[77] = new SqlParameter("@P_REBM15", objITDec.REBM15);
                //        objParams[78] = new SqlParameter("@P_REBM16", objITDec.REBM16);
                //        objParams[79] = new SqlParameter("@P_REBM17", objITDec.REBM17);
                //        objParams[80] = new SqlParameter("@P_REBM18", objITDec.REBM18);
                //        objParams[81] = new SqlParameter("@P_REBM19", objITDec.REBM19);
                //        objParams[82] = new SqlParameter("@P_REBM20", objITDec.REBM20);


                //        objParams[83] = new SqlParameter("@P_REBT1", objITDec.REBT1);
                //        objParams[84] = new SqlParameter("@P_REBT2", objITDec.REBT2);
                //        objParams[85] = new SqlParameter("@P_REBT3", objITDec.REBT3);
                //        objParams[86] = new SqlParameter("@P_REBT4", objITDec.REBT4);
                //        objParams[87] = new SqlParameter("@P_REBT5", objITDec.REBT5);
                //        objParams[88] = new SqlParameter("@P_REBT6", objITDec.REBT6);
                //        objParams[89] = new SqlParameter("@P_REBT7", objITDec.REBT7);
                //        objParams[90] = new SqlParameter("@P_REBT8", objITDec.REBT8);
                //        objParams[91] = new SqlParameter("@P_REBT9", objITDec.REBT9);
                //        objParams[92] = new SqlParameter("@P_REBT10", objITDec.REBT10);
                //        objParams[93] = new SqlParameter("@P_REBT11", objITDec.REBT11);
                //        objParams[94] = new SqlParameter("@P_REBT12", objITDec.REBT12);
                //        objParams[95] = new SqlParameter("@P_REBT13", objITDec.REBT13);
                //        objParams[96] = new SqlParameter("@P_REBT14", objITDec.REBT14);
                //        objParams[97] = new SqlParameter("@P_REBT15", objITDec.REBT15);
                //        objParams[98] = new SqlParameter("@P_REBT16", objITDec.REBT16);
                //        objParams[99] = new SqlParameter("@P_REBT17", objITDec.REBT17);
                //        objParams[100] = new SqlParameter("@P_REBT18", objITDec.REBT18);
                //        objParams[101] = new SqlParameter("@P_REBT19", objITDec.REBT19);
                //        objParams[102] = new SqlParameter("@P_REBT20", objITDec.REBT20);

                //        objParams[103] = new SqlParameter("@P_REBD1", objITDec.REBD1);
                //        objParams[104] = new SqlParameter("@P_REBD2", objITDec.REBD2);
                //        objParams[105] = new SqlParameter("@P_REBD3", objITDec.REBD3);
                //        objParams[106] = new SqlParameter("@P_REBD4", objITDec.REBD4);
                //        objParams[107] = new SqlParameter("@P_REBD5", objITDec.REBD5);
                //        objParams[108] = new SqlParameter("@P_REBD6", objITDec.REBD6);
                //        objParams[109] = new SqlParameter("@P_REBD7", objITDec.REBD7);
                //        objParams[110] = new SqlParameter("@P_REBD8", objITDec.REBD8);
                //        objParams[111] = new SqlParameter("@P_REBD9", objITDec.REBD9);
                //        objParams[112] = new SqlParameter("@P_REBD10", objITDec.REBD10);
                //        objParams[113] = new SqlParameter("@P_REBD11", objITDec.REBD11);
                //        objParams[114] = new SqlParameter("@P_REBD12", objITDec.REBD12);
                //        objParams[115] = new SqlParameter("@P_REBD13", objITDec.REBD13);
                //        objParams[116] = new SqlParameter("@P_REBD14", objITDec.REBD14);
                //        objParams[117] = new SqlParameter("@P_REBD15", objITDec.REBD15);
                //        objParams[118] = new SqlParameter("@P_REBD16", objITDec.REBD16);
                //        objParams[119] = new SqlParameter("@P_REBD17", objITDec.REBD17);
                //        objParams[120] = new SqlParameter("@P_REBD18", objITDec.REBD18);
                //        objParams[121] = new SqlParameter("@P_REBD19", objITDec.REBD19);
                //        objParams[122] = new SqlParameter("@P_REBD20", objITDec.REBD20);

                //        objParams[123] = new SqlParameter("@P_BALAMT", objITDec.BALAMT);
                //        objParams[124] = new SqlParameter("@P_INCP1", objITDec.INCP1);
                //        objParams[125] = new SqlParameter("@P_INCP2", objITDec.INCP2);
                //        objParams[126] = new SqlParameter("@P_INCP3", objITDec.INCP3);
                //        objParams[127] = new SqlParameter("@P_INCP4", objITDec.INCP4);
                //        objParams[128] = new SqlParameter("@P_INCP5", objITDec.INCP5);

                //        objParams[129] = new SqlParameter("@P_INCAMT1", objITDec.INCAMT1);
                //        objParams[130] = new SqlParameter("@P_INCAMT2", objITDec.INCAMT2);
                //        objParams[131] = new SqlParameter("@P_INCAMT3", objITDec.INCAMT3);
                //        objParams[132] = new SqlParameter("@P_INCAMT4", objITDec.INCAMT4);
                //        objParams[133] = new SqlParameter("@P_INCAMT5", objITDec.INCAMT5);

                //        if (!objITDec.INCDATE.Equals(DateTime.MinValue))
                //            objParams[134] = new SqlParameter("@P_INCDATE", objITDec.INCDATE);
                //        else
                //            objParams[134] = new SqlParameter("@P_INCDATE", DBNull.Value);

                //        objParams[135] = new SqlParameter("@P_REMARK", objITDec.REMARK);
                //        objParams[136] = new SqlParameter("@P_COLLEGE_CODE", objITDec.COLLEGE_CODE);
                //        objParams[137] = new SqlParameter("@P_HRA", hravalue);
                //        objParams[138] = new SqlParameter("@P_HOUSEINST", houseint);
                //        objParams[139] = new SqlParameter("@P_OTHERINCTDS", objITDec.OTHERINCTDS);
                //        objParams[140] = new SqlParameter("@P_SUMPERQUISITE", objITDec.SUMPERQUISITE);
                //        objParams[141] = new SqlParameter("@NSCINT", nscint);
                //        objParams[142] = new SqlParameter("@P_CCDNPS", objITDec.CCDNPS);
                //        objParams[143] = new SqlParameter("@P_RGESS80CCG", objITDec.RGESS80CCG);

                //        objParams[144] = new SqlParameter("@IsMailSend", objITDec.IsMailSend);
                //        objParams[145] = new SqlParameter("@IsFinalSub", objITDec.IsFinalSubmit);
                //        objParams[146] = new SqlParameter("@IsLock", objITDec.IsLock);

                //        objParams[147] = new SqlParameter("@IT_RULE_ID", objITDec.IT_RULE_ID);
                //        objParams[148] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[148].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_IT_EMPINFO", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else if (Convert.ToInt32(ret) == 1)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else if (Convert.ToInt32(ret) == 2)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpMasController.AddITEmplyeeInfo -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

               // Add new Function by Purva Raut Date:19-04-2022

                public int AddITEmplyeeInfo(ITDeclaration objITDec, decimal hravalue, decimal houseint, decimal nscint)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[33];
                        objParams[0] = new SqlParameter("@P_IDNO", objITDec.IDNO);
                        objParams[1] = new SqlParameter("@P_ITNO", objITDec.ITNO);
                        if (!objITDec.FROMDATE.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_FROMDATE", objITDec.FROMDATE);
                        else
                            objParams[2] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objITDec.TODATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_TODATE", objITDec.TODATE);
                        else
                            objParams[3] = new SqlParameter("@P_TODATE", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_BALAMT", objITDec.BALAMT);
                        objParams[5] = new SqlParameter("@P_INCP1", objITDec.INCP1);
                        objParams[6] = new SqlParameter("@P_INCP2", objITDec.INCP2);
                        objParams[7] = new SqlParameter("@P_INCP3", objITDec.INCP3);
                        objParams[8] = new SqlParameter("@P_INCP4", objITDec.INCP4);
                        objParams[9] = new SqlParameter("@P_INCP5", objITDec.INCP5);

                        objParams[10] = new SqlParameter("@P_INCAMT1", objITDec.INCAMT1);
                        objParams[11] = new SqlParameter("@P_INCAMT2", objITDec.INCAMT2);
                        objParams[12] = new SqlParameter("@P_INCAMT3", objITDec.INCAMT3);
                        objParams[13] = new SqlParameter("@P_INCAMT4", objITDec.INCAMT4);
                        objParams[14] = new SqlParameter("@P_INCAMT5", objITDec.INCAMT5);

                        if (!objITDec.INCDATE.Equals(DateTime.MinValue))
                            objParams[15] = new SqlParameter("@P_INCDATE", objITDec.INCDATE);
                        else
                            objParams[15] = new SqlParameter("@P_INCDATE", DBNull.Value);

                        objParams[16] = new SqlParameter("@P_REMARK", objITDec.REMARK);
                        objParams[17] = new SqlParameter("@P_COLLEGE_CODE", objITDec.COLLEGE_CODE);
                        objParams[18] = new SqlParameter("@P_HRA", hravalue);
                        objParams[19] = new SqlParameter("@P_HOUSEINST", houseint);
                        objParams[20] = new SqlParameter("@P_OTHERINCTDS", objITDec.OTHERINCTDS);
                        objParams[21] = new SqlParameter("@P_SUMPERQUISITE", objITDec.SUMPERQUISITE);
                        objParams[22] = new SqlParameter("@NSCINT", nscint);
                        objParams[23] = new SqlParameter("@P_CCDNPS", objITDec.CCDNPS);
                        objParams[24] = new SqlParameter("@P_RGESS80CCG", objITDec.RGESS80CCG);
                        objParams[25] = new SqlParameter("@IsMailSend", objITDec.IsMailSend);
                        objParams[26] = new SqlParameter("@IsFinalSub", objITDec.IsFinalSubmit);
                        objParams[27] = new SqlParameter("@IsLock", objITDec.IsLock);
                        objParams[28] = new SqlParameter("@IT_RULE_ID", objITDec.IT_RULE_ID);
                        objParams[29] = new SqlParameter("@VIA_HEADS_TABLE", objITDec.VIA_HEADS_Table);
                        objParams[30] = new SqlParameter("@FINYEAR", objITDec.FINYEAR);
                        objParams[31] = new SqlParameter("@REBATE_HEADS_TABLE", objITDec.REBATE_HEADS_Table);
                        objParams[32] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[32].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_IT_EMPINFO_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpMasController.AddITEmplyeeInfo -> " + ex.ToString());
                    }
                    return retStatus;
                }
                
                
                
                public DataTableReader GetCalculativeValues(string fromDate, string toDate, int idNo)
                {
                    DataTableReader dtr;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Fetch Salary Details For Declaration From Salary Table
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_EMPNO", idNo);
                      //  dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_DECLARATION_CAL", objParams).Tables[0].CreateDataReader();
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_DECLARATION_CAL_NEW", objParams).Tables[0].CreateDataReader();
                        
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.GetCalculativeValues->" + ex.ToString());
                    }
                    return dtr;
                }


                public DataTableReader GetCHAPVIValues(string fromDate, string toDate, int idNo)
                {
                    DataTableReader dtr;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Fetch Salary Details For Declaration From Salary Table
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_EMPNO", idNo);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_DECLARATION_RET_CHAPVI", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.GetCalculativeValues->" + ex.ToString());
                    }
                    return dtr;
                }
                public DataSet GetRebateHeadsToBindNew(string fromDate, string toDate, int idNo)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Fetch Salary Details For Declaration From Salary Table
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_EMPNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_REBATE_HEADS_VALUES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.GetRebateHeadsToBind->" + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetRebateHeadsToBind(int idNo, string FinYear)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_REBATE_HEADS_VALUES_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.GetRebateHeadsToBind->" + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetITVIAHeadsToModify(int idno, string FinYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_IT_VIA_HEAD_LIST_BY_EMPID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetITVIAHeadsToModify-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataTableReader GetDetailsFromITMaster(string fromDate, string toDate, int idNo)
                {
                    DataTableReader dtr;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Fetch Salary Details For Declaration From Salary Table
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_EMPNO", idNo);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_DECLARATION_RET_DYNAMIC", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.GetCalculativeValues->" + ex.ToString());
                    }
                    return dtr;
                }

                public DataTable RetrieveNatureofPerquisite(int idno, DateTime fdate, DateTime tdate, int collegeNo, decimal gs)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FDATE", fdate);
                        objParams[2] = new SqlParameter("@P_TDATE", tdate);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[4] = new SqlParameter("@P_GS", gs);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_NATUREOFPERQUISITE", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.RetrieveNatureofPerquisite-> " + ex.ToString());
                    }
                    return dt;
                }

                public int AddITNatureofPerquisite(ITDeclaration objITDec)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Nature of Perquisites
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objITDec.IDNO);
                        objParams[1] = new SqlParameter("@P_PERQUISITEID", objITDec.PERQUISITEID);
                        objParams[2] = new SqlParameter("@P_PERQUISITES", objITDec.PERQUISITE);
                        objParams[3] = new SqlParameter("@P_FDATE", objITDec.FROMDATE);
                        objParams[4] = new SqlParameter("@P_TDATE", objITDec.TODATE);
                        objParams[5] = new SqlParameter("@P_VALUE3", objITDec.VALUE3);
                        objParams[6] = new SqlParameter("@P_VALUE4", objITDec.VALUE4);
                        objParams[7] = new SqlParameter("@P_VALUE5", objITDec.VALUE5);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO", objITDec.COLLEGENO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_NATUREOFPERQUISITE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.AddITNatureofPerquisite-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEmployeesByStaff(int Staff, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_Staff", Staff);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EMPLOYEE_BY_STAFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }


                public int ITBulkDeclaration(string fromDate, string toDate, int idNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", toDate);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        objParams[3] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IT_BULK_DECLARATION", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITDeclarationController.ITBulkDeclaration-> " + ex.ToString());
                    }
                    return retStatus;
                }




                #region 23 Mar 2018


                public int Add_ChapterVI(ITDeclaration objITDec)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Nature of Perquisites
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objITDec.IDNO);
                        objParams[1] = new SqlParameter("@P_CNO", objITDec.CNO);
                        objParams[2] = new SqlParameter("@P_HouseOwnerName", objITDec.HouseOwnerName);
                        objParams[3] = new SqlParameter("@P_PanNumber", objITDec.PanNumber);

                        if (!objITDec.DeclarationDate.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_DeclarationDate", objITDec.DeclarationDate);
                        else
                            objParams[4] = new SqlParameter("@P_DeclarationDate", DBNull.Value);

                        // objParams[4] = new SqlParameter("@P_DeclarationDate", objITDec.DeclarationDate);
                        objParams[5] = new SqlParameter("@P_Amount", objITDec.Amount);
                        objParams[6] = new SqlParameter("@P_FinYear", objITDec.FinYear);
                        objParams[7] = new SqlParameter("@P_CollegeCode", objITDec.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_CHAPVI_ID", objITDec.CHAPVI_ID);
                        objParams[9] = new SqlParameter("@P_Remark", objITDec.REMARK);
                        objParams[10] = new SqlParameter("@P_EmpDocumentName", objITDec.EmpDocumentName);
                        objParams[11] = new SqlParameter("@P_EmpDocumentUrl", objITDec.EmpDocumentUrl);
                        objParams[12] = new SqlParameter("@P_DocumentType", objITDec.DocumentType);
                        objParams[13] = new SqlParameter("@P_CollegeNo", objITDec.CollegeNo);

                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_CHAPER_VI", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.AddITNatureofPerquisite-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataTable GET_CHAPVI_HEAD_DATA(int idno, int CNO, string FinYear)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_CNO", CNO);
                        objParams[2] = new SqlParameter("@P_FinYear", FinYear);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_GET_CHAPVI_HEAD_DATA", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.RetrieveNatureofPerquisite-> " + ex.ToString());
                    }
                    return dt;
                }


                public DataTable GET_CHAPVI_HEAD_DATA_BY_ID(int CHAPVI_ID)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_CHAPVI_ID", CHAPVI_ID);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_GET_CHAPVI_HEAD_DATA_BY_ID", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.RetrieveNatureofPerquisite-> " + ex.ToString());
                    }
                    return dt;
                }



                public int DELETE_CHAPVI_HEAD_DATA_BY_ID(int CHAPVI_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Nature of Perquisites
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CHAPVI_ID", CHAPVI_ID);

                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_CHAPVI_HEAD_DATA_BY_ID", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.AddITNatureofPerquisite-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GET_CHAPVI_HEAD_AMOUNT_BY_ID(int idno, string FinYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CHAPVI_HEAD_AMOUNT_BY_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetBySrNo-> " + ex.ToString());
                    }
                    return ds;
                }





                /* Rebate */


                public int Add_Rebate(ITDeclaration objITDec)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Nature of Perquisites
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objITDec.IDNO);
                        objParams[1] = new SqlParameter("@P_FNO", objITDec.FNO);


                        if (!objITDec.DeclarationDate.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_DeclarationDate", objITDec.DeclarationDate);
                        else
                            objParams[2] = new SqlParameter("@P_DeclarationDate", DBNull.Value);

                        //   objParams[2] = new SqlParameter("@P_DeclarationDate", objITDec.DeclarationDate);
                        objParams[3] = new SqlParameter("@P_Amount", objITDec.Amount);
                        objParams[4] = new SqlParameter("@P_FinYear", objITDec.FinYear);
                        objParams[5] = new SqlParameter("@P_Remark", objITDec.REMARK);
                        objParams[6] = new SqlParameter("@P_CollegeCode", objITDec.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_RebateHeadId", objITDec.RebateHeadId);

                        objParams[8] = new SqlParameter("@P_EmpDocumentName", objITDec.EmpDocumentName);
                        objParams[9] = new SqlParameter("@P_EmpDocumentUrl", objITDec.EmpDocumentUrl);
                        objParams[10] = new SqlParameter("@P_DocumentType", objITDec.DocumentType);
                        objParams[11] = new SqlParameter("@P_CollegeNo", objITDec.CollegeNo);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_CHAPER_REBATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.AddITNatureofPerquisite-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataTable GET_REBATE_HEAD_DATA(int idno, int CNO, string FinYear)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FNO", CNO);
                        objParams[2] = new SqlParameter("@P_FinYear", FinYear);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_GET_REBATE_HEAD_DATA", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.RetrieveNatureofPerquisite-> " + ex.ToString());
                    }
                    return dt;
                }



                public DataTable GET_REBATE_HEAD_DATA_BY_ID(int RebateHeadId)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_RebateHeadId", RebateHeadId);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_GET_REBATE_HEAD_DATA_BY_ID", objParams).Tables[0];

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.RetrieveNatureofPerquisite-> " + ex.ToString());
                    }
                    return dt;
                }



                public int DELETE_REBATE_HEAD_DATA_BY_ID(int CHAPVI_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Nature of Perquisites
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CHAPVI_ID", CHAPVI_ID);

                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DELETE_REBATE_HEAD_DATA_BY_ID", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.AddITNatureofPerquisite-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public DataSet GET_REBATE_HEAD_AMOUNT_BY_ID(int idno, string FinYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_REBATE_HEAD_AMOUNT_BY_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetBySrNo-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion



                // This method is used to get login credentails for sending mail.
                public DataSet GetFromDataForEmail()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                      
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_GET_CREDENTIALS_FOR_EMAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ITDeclarationController.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }



                public int LockUnlockITDeclaration(int idno, DateTime FromDate, DateTime ToDate, int IsLock)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FROMDATE", FromDate);
                        objParams[2] = new SqlParameter("@P_TODATE", ToDate);
                        objParams[3] = new SqlParameter("@P_ISLOCK", IsLock);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IT_UPDATE_LOCK_UNLOCK_STATUS", objParams, true));
                        
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ITDeclarationController.LockUnlockITDeclaration-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GET_CHAPVI_AMOUNT_BY_EMPID(int idno, string FinYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CHAPVI_AMOUNT_BY_EMPID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetBySrNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GET_REBEATE_AMOUNT_BY_EMPIDNO(int idno, string FinYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FinYear", FinYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_REBEATE_AMOUNT_BY_EMPIDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.GetBySrNo-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}

                
