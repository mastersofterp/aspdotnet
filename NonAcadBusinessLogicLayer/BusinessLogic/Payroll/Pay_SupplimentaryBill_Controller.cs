//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[SupplimentaryBill_Controller]                                  
// CREATION DATE : 02-NOV-2009                                                        
// CREATED BY    : KIRAN GVS                                       
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Pay_SupplimentaryBill_Controller
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                
                //public int AddSupplimentaryBill(Pay_Supplimentary_Bill objsupbill)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        //AddSupplimentaryBill
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //        {
                //            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                //            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                //            new SqlParameter("@P_SUPLSTATUS",objsupbill.SUPLSTATUS),
                //            new SqlParameter("@P_MONYEAR",objsupbill.MONYEAR),
                //            new SqlParameter("@P_IDNO",objsupbill.IDNO),
                //            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                //            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),
                //            new SqlParameter("@P_ADDIT",objsupbill.ADDIT),
                //            new SqlParameter("@P_PSTATUS",objsupbill.PSTATUS),
                //            new SqlParameter("@P_APPOINTNO",objsupbill.APPOINTNO),
                //            new SqlParameter("@P_SCALENO",objsupbill.SCALENO),
                //            new SqlParameter("@P_STAFFNO",objsupbill.STAFFNO),
                //            new SqlParameter("@P_SUPLIPAY",objsupbill.SUPLIPAY),
                //            new SqlParameter("@P_I1",objsupbill.I1),
                //            new SqlParameter("@P_I2",objsupbill.I2),
                //            new SqlParameter("@P_I3",objsupbill.I3),
                //            new SqlParameter("@P_I4",objsupbill.I4),
                //            new SqlParameter("@P_I5",objsupbill.I5),
                //            new SqlParameter("@P_I6",objsupbill.I6),
                //            new SqlParameter("@P_I7",objsupbill.I7),
                //            new SqlParameter("@P_I8",objsupbill.I8),
                //            new SqlParameter("@P_I9",objsupbill.I9),
                //            new SqlParameter("@P_I10",objsupbill.I10),
                //            new SqlParameter("@P_I11",objsupbill.I11),
                //            new SqlParameter("@P_I12",objsupbill.I12),
                //            new SqlParameter("@P_I13",objsupbill.I13),
                //            new SqlParameter("@P_I14",objsupbill.I14),
                //            new SqlParameter("@P_I15",objsupbill.I15),
                //            new SqlParameter("@P_GS",objsupbill.GS),
                //            new SqlParameter("@P_GS1",objsupbill.GS1),
                //            new SqlParameter("@P_D1",objsupbill.D1),
                //            new SqlParameter("@P_D2",objsupbill.D2),
                //            new SqlParameter("@P_D3",objsupbill.D3),
                //            new SqlParameter("@P_D4",objsupbill.D4),
                //            new SqlParameter("@P_D5",objsupbill.D5),
                //            new SqlParameter("@P_D6",objsupbill.D6),
                //            new SqlParameter("@P_D7",objsupbill.D7),
                //            new SqlParameter("@P_D8",objsupbill.D8),
                //            new SqlParameter("@P_D9",objsupbill.D9),
                //            new SqlParameter("@P_D10",objsupbill.D10),
                //            new SqlParameter("@P_D11",objsupbill.D11),
                //            new SqlParameter("@P_D12",objsupbill.D12),
                //            new SqlParameter("@P_D13",objsupbill.D13),
                //            new SqlParameter("@P_D14",objsupbill.D14),
                //            new SqlParameter("@P_D15",objsupbill.D15),
                //            new SqlParameter("@P_D16",objsupbill.D16),
                //            new SqlParameter("@P_D17",objsupbill.D17),
                //            new SqlParameter("@P_D18",objsupbill.D18),
                //            new SqlParameter("@P_D19",objsupbill.D19),
                //            new SqlParameter("@P_D20",objsupbill.D20),
                //            new SqlParameter("@P_D21",objsupbill.D21),
                //            new SqlParameter("@P_D22",objsupbill.D22),
                //            new SqlParameter("@P_D23",objsupbill.D23),
                //            new SqlParameter("@P_D24",objsupbill.D24),
                //            new SqlParameter("@P_D25",objsupbill.D25),
                //            new SqlParameter("@P_D26",objsupbill.D26),
                //            new SqlParameter("@P_D27",objsupbill.D27),
                //            new SqlParameter("@P_D28",objsupbill.D28),
                //            new SqlParameter("@P_D29",objsupbill.D29),
                //            new SqlParameter("@P_D30",objsupbill.D30),
                //            new SqlParameter("@P_TOT_DED",objsupbill.TOT_DED),
                //            new SqlParameter("@P_NET_PAY",objsupbill.NET_PAY),
                //            new SqlParameter("@P_HP",objsupbill.HP),
                //            new SqlParameter("@P_PAYDAYS",objsupbill.PAYDAYS),
                //            new SqlParameter("@P_REMARK",objsupbill.REMARK),
                //            new SqlParameter("@P_PAY",objsupbill.PAY),
                //            new SqlParameter("@P_TITLE",objsupbill.TITLE),
                //            new SqlParameter("@P_EXPAMT",objsupbill.EXPAMT),
                //            new SqlParameter("@P_DPAMT",objsupbill.DPAMT),
                //            new SqlParameter("@P_GRADEPAY",objsupbill.GRADEPAY),
                //            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE), 
                //            new SqlParameter("@P_SUPLTRXID",objsupbill.SUPLTRXID) 
                //        };

                //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_SUPLIMENTARY_BILL",sqlParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_SupplimentaryBill_Controller.AddSupplimentaryBill -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddSupplimentaryBill(Pay_Supplimentary_Bill objsupbill,string earningsAmt, string deductionAmt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddSupplimentaryBill
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                            new SqlParameter("@P_SUPLSTATUS",objsupbill.SUPLSTATUS),
                            new SqlParameter("@P_MONYEAR",objsupbill.MONYEAR),
                            new SqlParameter("@P_IDNO",objsupbill.IDNO),
                            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),
                            new SqlParameter("@P_ADDIT",objsupbill.ADDIT),
                            new SqlParameter("@P_PSTATUS",objsupbill.PSTATUS),
                            new SqlParameter("@P_APPOINTNO",objsupbill.APPOINTNO),
                            new SqlParameter("@P_SCALENO",objsupbill.SCALENO),
                            new SqlParameter("@P_STAFFNO",objsupbill.STAFFNO),
                            new SqlParameter("@P_SUPLIPAY",objsupbill.SUPLIPAY),
                            new SqlParameter("@P_GS",objsupbill.GS),
                            new SqlParameter("@P_GS1",objsupbill.GS1),
                            new SqlParameter("@P_TOT_DED",objsupbill.TOT_DED),
                            new SqlParameter("@P_NET_PAY",objsupbill.NET_PAY),
                            new SqlParameter("@P_HP",objsupbill.HP),
                            new SqlParameter("@P_PAYDAYS",objsupbill.PAYDAYS),
                            new SqlParameter("@P_REMARK",objsupbill.REMARK),
                            new SqlParameter("@P_PAY",objsupbill.PAY),
                            new SqlParameter("@P_TITLE",objsupbill.TITLE),
                            new SqlParameter("@P_EXPAMT",objsupbill.EXPAMT),
                            new SqlParameter("@P_DPAMT",objsupbill.DPAMT),
                            new SqlParameter("@P_GRADEPAY",objsupbill.GRADEPAY),
                            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE), 
                            new SqlParameter("@P_EARNINGSAMT",earningsAmt) ,
                            new SqlParameter("@P_DEDUCTIONAMT",deductionAmt),
                            new SqlParameter("@P_ADDOTHINCOME",objsupbill.ADDOTHINCOME),
                            
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_SUPLIMENTARY_BILL", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_SupplimentaryBill_Controller.AddSupplimentaryBill -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdateSupplimentaryBill(Pay_Supplimentary_Bill objsupbill)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        //UpdateSupplimentaryBill
                //        SqlParameter[] sqlParams = new SqlParameter[]
                //        {   
                //            new SqlParameter("@P_SUPLTRXID",objsupbill.SUPLTRXID),
                //            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                //            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                //            new SqlParameter("@P_SUPLSTATUS",objsupbill.SUPLSTATUS),
                //            new SqlParameter("@P_MONYEAR",objsupbill.MONYEAR),
                //            new SqlParameter("@P_IDNO",objsupbill.IDNO),
                //            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                //            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),
                //            new SqlParameter("@P_ADDIT",objsupbill.ADDIT),
                //            new SqlParameter("@P_PSTATUS",objsupbill.PSTATUS),
                //            new SqlParameter("@P_APPOINTNO",objsupbill.APPOINTNO),
                //            new SqlParameter("@P_SCALENO",objsupbill.SCALENO),
                //            new SqlParameter("@P_STAFFNO",objsupbill.STAFFNO),
                //            new SqlParameter("@P_SUPLIPAY",objsupbill.SUPLIPAY),
                //            new SqlParameter("@P_I1",objsupbill.I1),
                //            new SqlParameter("@P_I2",objsupbill.I2),
                //            new SqlParameter("@P_I3",objsupbill.I3),
                //            new SqlParameter("@P_I4",objsupbill.I4),
                //            new SqlParameter("@P_I5",objsupbill.I5),
                //            new SqlParameter("@P_I6",objsupbill.I6),
                //            new SqlParameter("@P_I7",objsupbill.I7),
                //            new SqlParameter("@P_I8",objsupbill.I8),
                //            new SqlParameter("@P_I9",objsupbill.I9),
                //            new SqlParameter("@P_I10",objsupbill.I10),
                //            new SqlParameter("@P_I11",objsupbill.I11),
                //            new SqlParameter("@P_I12",objsupbill.I12),
                //            new SqlParameter("@P_I13",objsupbill.I13),
                //            new SqlParameter("@P_I14",objsupbill.I14),
                //            new SqlParameter("@P_I15",objsupbill.I15),
                //            new SqlParameter("@P_GS",objsupbill.GS),
                //            new SqlParameter("@P_GS1",objsupbill.GS1),
                //            new SqlParameter("@P_D1",objsupbill.D1),
                //            new SqlParameter("@P_D2",objsupbill.D2),
                //            new SqlParameter("@P_D3",objsupbill.D3),
                //            new SqlParameter("@P_D4",objsupbill.D4),
                //            new SqlParameter("@P_D5",objsupbill.D5),
                //            new SqlParameter("@P_D6",objsupbill.D6),
                //            new SqlParameter("@P_D7",objsupbill.D7),
                //            new SqlParameter("@P_D8",objsupbill.D8),
                //            new SqlParameter("@P_D9",objsupbill.D9),
                //            new SqlParameter("@P_D10",objsupbill.D10),
                //            new SqlParameter("@P_D11",objsupbill.D11),
                //            new SqlParameter("@P_D12",objsupbill.D12),
                //            new SqlParameter("@P_D13",objsupbill.D13),
                //            new SqlParameter("@P_D14",objsupbill.D14),
                //            new SqlParameter("@P_D15",objsupbill.D15),
                //            new SqlParameter("@P_D16",objsupbill.D16),
                //            new SqlParameter("@P_D17",objsupbill.D17),
                //            new SqlParameter("@P_D18",objsupbill.D18),
                //            new SqlParameter("@P_D19",objsupbill.D19),
                //            new SqlParameter("@P_D20",objsupbill.D20),
                //            new SqlParameter("@P_D21",objsupbill.D21),
                //            new SqlParameter("@P_D22",objsupbill.D22),
                //            new SqlParameter("@P_D23",objsupbill.D23),
                //            new SqlParameter("@P_D24",objsupbill.D24),
                //            new SqlParameter("@P_D25",objsupbill.D25),
                //            new SqlParameter("@P_D26",objsupbill.D26),
                //            new SqlParameter("@P_D27",objsupbill.D27),
                //            new SqlParameter("@P_D28",objsupbill.D28),
                //            new SqlParameter("@P_D29",objsupbill.D29),
                //            new SqlParameter("@P_D30",objsupbill.D30),
                //            new SqlParameter("@P_TOT_DED",objsupbill.TOT_DED),
                //            new SqlParameter("@P_NET_PAY",objsupbill.NET_PAY),
                //            new SqlParameter("@P_HP",objsupbill.HP),
                //            new SqlParameter("@P_PAYDAYS",objsupbill.PAYDAYS),
                //            new SqlParameter("@P_REMARK",objsupbill.REMARK),
                //            new SqlParameter("@P_PAY",objsupbill.PAY),
                //            new SqlParameter("@P_TITLE",objsupbill.TITLE),
                //            new SqlParameter("@P_EXPAMT",objsupbill.EXPAMT),
                //            new SqlParameter("@P_DPAMT",objsupbill.DPAMT),
                //            new SqlParameter("@P_GRADEPAY",objsupbill.GRADEPAY),
                //            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE)                              
                //        };

                //        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_SUPLIMENTARY_BILL", sqlParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_SupplimentaryBill_Controller.UpdateSupplimentaryBill -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int UpdateSupplimentaryBill(Pay_Supplimentary_Bill objsupbill, string earningsAmt, string deductionAmt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateSupplimentaryBill
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {   
                            new SqlParameter("@P_SUPLTRXID",objsupbill.SUPLTRXID),
                            new SqlParameter("@P_SUPLHNO",objsupbill.SUPLHNO),
                            new SqlParameter("@P_SUPLHEAD",objsupbill.SUPLHEAD),
                            new SqlParameter("@P_SUPLSTATUS",objsupbill.SUPLSTATUS),
                            new SqlParameter("@P_MONYEAR",objsupbill.MONYEAR),
                            new SqlParameter("@P_IDNO",objsupbill.IDNO),
                            new SqlParameter("@P_ORDNO",objsupbill.ORDNO),
                            new SqlParameter("@P_SBDATE",objsupbill.SBDATE),
                            new SqlParameter("@P_ADDIT",objsupbill.ADDIT),
                            new SqlParameter("@P_PSTATUS",objsupbill.PSTATUS),
                            new SqlParameter("@P_APPOINTNO",objsupbill.APPOINTNO),
                            new SqlParameter("@P_SCALENO",objsupbill.SCALENO),
                            new SqlParameter("@P_STAFFNO",objsupbill.STAFFNO),
                            new SqlParameter("@P_SUPLIPAY",objsupbill.SUPLIPAY),
                            new SqlParameter("@P_GS",objsupbill.GS),
                            new SqlParameter("@P_GS1",objsupbill.GS1),
                            new SqlParameter("@P_TOT_DED",objsupbill.TOT_DED),
                            new SqlParameter("@P_NET_PAY",objsupbill.NET_PAY),
                            new SqlParameter("@P_HP",objsupbill.HP),
                            new SqlParameter("@P_PAYDAYS",objsupbill.PAYDAYS),
                            new SqlParameter("@P_REMARK",objsupbill.REMARK),
                            new SqlParameter("@P_PAY",objsupbill.PAY),
                            new SqlParameter("@P_TITLE",objsupbill.TITLE),
                            new SqlParameter("@P_EXPAMT",objsupbill.EXPAMT),
                            new SqlParameter("@P_DPAMT",objsupbill.DPAMT),
                            new SqlParameter("@P_GRADEPAY",objsupbill.GRADEPAY),
                            new SqlParameter("@P_COLLEGE_CODE",objsupbill.COLLEGE_CODE), 
                            new SqlParameter("@P_EARNINGSAMT",earningsAmt) ,
                            new SqlParameter("@P_DEDUCTIONAMT",deductionAmt),
                            new SqlParameter("@P_ADDOTHINCOME",objsupbill.ADDOTHINCOME), 
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_SUPLIMENTARY_BILL", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Pay_SupplimentaryBill_Controller.UpdateSupplimentaryBill -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllSupplimentaryBill()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_SUPLIMENTARY_BILL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSingleSupplimentaryBill(int suplTrxId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                         new SqlParameter("@P_SUPLTRXID",suplTrxId),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_SUPLIMENTARY_BILL", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllSuplimentaryBillOrderDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_PAY_SUPLIMENTARYBILL_ORDER_DETAILS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllSuplimentaryBillOrderDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSupplimentaryBillByOrderNo(string ordNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                         new SqlParameter("@P_ORDNO",ordNo),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SUPLIMENTARYBILL_BYORDNO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetSupplimentaryBillByOrderNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllEarningHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_PAY_ALL_EARNINGHEADS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllPayHeads() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllDeductionHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_PAY_ALL_DEDUCTIONHEADS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllDeductionHeads() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //added by vidisha
                public DataSet GetPensionSupplementaryExcel(string Fdate, string Ldate, int HeadId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@FROMDATE", Fdate);
                        objParams[1] = new SqlParameter("@TODATE", Ldate);
                        objParams[2] = new SqlParameter("@HEAD", HeadId);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_PENSION_SUPPLIMENTARY_BILL_EXCEL_BY_HEADNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSupplementaryTDSExcel(string Fdate, string Ldate, int HeadId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@FROMDATE", Fdate);
                        objParams[1] = new SqlParameter("@TODATE", Ldate);
                        objParams[2] = new SqlParameter("@HEAD", HeadId);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SUPPLIMENTARY_BILL_BY_HEADNO_TDS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPensionSupplementaryTDSExcel(string Fdate, string Ldate, int HeadId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@FROMDATE", Fdate);
                        objParams[1] = new SqlParameter("@TODATE", Ldate);
                        objParams[2] = new SqlParameter("@HEAD", HeadId);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_PENSION_SUPPLIMENTARY_BILL_BY_HEADNO_TDS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GeAllSupplimentaryBill() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int DeleteSupplimentaryBill(int supltrxid, int createdby)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SUPLTRXID", supltrxid);
                        objParams[1] = new SqlParameter("@P_CREATEDBY", createdby);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_SUPPLIMENTARY_BILL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.Pay_SupplimentaryBill_Controller.DeleteSupplimentaryBill.-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllSuplimentaryBillOrderDetailsById(int SUPLHNO, string Fdate, string Ldate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SUPLHNO", SUPLHNO);
                        objParams[1] = new SqlParameter("@FROMDATE", Fdate);
                        objParams[2] = new SqlParameter("@TODATE", Ldate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SUPLIMENTARYBILL_ORDER_DETAILS_BYSUPPHEADNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_SupplimentaryBill_Controller.GetAllSuplimentaryBillOrderDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
