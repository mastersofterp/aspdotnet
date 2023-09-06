using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_Quotation_Tender_Controller
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                /// 
                
                
                
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetIndentForQuotationTender(int mdCode,char qT,char Flag)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MDNO", mdCode );
                        objParams[1] = new SqlParameter("@P_TQSTATUS", qT );
                        objParams[2] = new SqlParameter("@P_FLAG", Flag );
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_RET_INDENT_FOR_QUOTATION_TENDER", objParams);
                        
                        
                        
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetIndentForQuotationTender-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetItemsForQuotationTender(string INDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INDNO", INDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_ITEMS_QUOTATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsForQuotationTender-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetFieldsQuotationTender()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_RET_FIELD_QUOTATION_TENDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetFieldsQuotationTender-> " + ex.ToString());
                    }
                    return ds;
                }
                
                
                public DataSet GetVendorsForQuotation(string categoryNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CATEGORYNO", categoryNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VENDOR_FOR_QUOTATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }


                public int AddQuotationTender(Str_Quotation_Tender objQt,string colcode,string authority)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try 
                    { 
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_QNO", SqlDbType.Int );
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_QUOTNO", objQt.QUOTNO );
                        objParams[2] = new SqlParameter("@P_REFNO", objQt.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objQt.MDNO);
                        objParams[4] = new SqlParameter("@P_ODATE", objQt.ODATE);
                        objParams[5] = new SqlParameter("@P_LDATE", objQt.LDATE);

                        if (objQt.OTIME == DateTime.MinValue)
                        {
                            objParams[6] = new SqlParameter("@P_OTIME", DBNull.Value);
                        }
                        else
                        {
                            objParams[6] = new SqlParameter("@P_OTIME", objQt.OTIME);
                        }

                        if (objQt.LTIME == DateTime.MinValue)
                        {
                            objParams[7] = new SqlParameter("@P_LTIME", DBNull.Value);                            
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_LTIME", objQt.LTIME);
                        }



                        objParams[8] = new SqlParameter("@P_SDATE", objQt.SDATE);
                        if (objQt.MATTER == null)
                        {
                            objQt.MATTER = "-";
                        }

                        objParams[9] = new SqlParameter("@P_MATTER", objQt.MATTER);
                        objParams[10] = new SqlParameter("@P_SUBJECT", objQt.SUBJECT);
                        objParams[11] = new SqlParameter("@P_FLAG", objQt.FLAG);
                        objParams[12] = new SqlParameter("@P_BHALNO", objQt.BHALNO);
                        if (objQt.TOPSPECI == null)
                        {
                            objQt.TOPSPECI = "-";
                        }
                        objParams[13] = new SqlParameter("@P_TOPSPECI", objQt.TOPSPECI );
                        if (objQt.TERM == null)
                        {
                            objQt.TERM = "-";
                        }
                        objParams[14] = new SqlParameter("@P_TERMS", objQt.TERM);
                        objParams[15] = new SqlParameter("@P_RTVALID", objQt.RTVALID );
                        objParams[16] = new SqlParameter("@P_RTVALIDUNIT", objQt.RTVALIDUNIT );
                        objParams[17] = new SqlParameter("@P_QUOTAMT", objQt.QUOTAMT );
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", colcode );
                        objParams[19] = new SqlParameter("@P_INDNO", objQt.INDNO);
                        objParams[20] = new SqlParameter("@P_AUTHORITY", authority);
                        objSQLHelper.ExecuteNonQuerySP ("PKG_STR_QUOTATIONENTRY_INSERT", objParams,true );
                        retStatus=(int)CustomStatus.RecordSaved ;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateQuotationFieldEntry(string  quotno,int fieldno,string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_QFNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_FNO", fieldno );
                        objParams[2] = new SqlParameter("@P_QUOTNO", quotno );
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode );

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_QUOTFIELDENTRY_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateQuotationPartyEntry(string quotno, int pno,int mdno,string flag, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_QPNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_QUOTNO", quotno);
                        objParams[2] = new SqlParameter("@P_PNO", pno);
                        objParams[3] = new SqlParameter("@P_FLAG", flag);
                        objParams[4] = new SqlParameter("@P_MDNO", mdno);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYENTRY_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetSingleQuotation(string quotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUOTNO", quotno );
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;  
                }

                public int UpdateQuotationEntry(Str_Quotation_Tender objQt, string colcode,string authority)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New BUDGETHEAD
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_QUOTNO", objQt.QUOTNO);
                        objParams[1] = new SqlParameter("@P_REFNO", objQt.REFNO);
                        objParams[2] = new SqlParameter("@P_INDNO", objQt.INDNO);
                        objParams[3] = new SqlParameter("@P_ODATE", objQt.ODATE);
                        objParams[4] = new SqlParameter("@P_LDATE", objQt.LDATE);
                        //objParams[5] = new SqlParameter("@P_OTIME", objQt.OTIME);
                        //objParams[6] = new SqlParameter("@P_LTIME", objQt.LTIME);

                        if (objQt.OTIME == DateTime.MinValue)
                        {
                            objParams[5] = new SqlParameter("@P_OTIME", DBNull.Value);
                        }
                        else
                        {
                            objParams[5] = new SqlParameter("@P_OTIME", objQt.OTIME);
                        }

                        if (objQt.LTIME == DateTime.MinValue)
                        {
                            objParams[6] = new SqlParameter("@P_LTIME", DBNull.Value);
                        }
                        else
                        {
                            objParams[6] = new SqlParameter("@P_LTIME", objQt.LTIME);
                        }


                        objParams[7] = new SqlParameter("@P_SDATE", objQt.SDATE);
                        if (objQt.MATTER != null)
                        
                            objParams[8] = new SqlParameter("@P_MATTER", objQt.MATTER);
                        else
                            objParams[8] = new SqlParameter("@P_MATTER", "");

                        objParams[9] = new SqlParameter("@P_SUBJECT", objQt.SUBJECT);
                        objParams[10] = new SqlParameter("@P_FLAG", objQt.FLAG);
                        objParams[11] = new SqlParameter("@P_BHALNO", objQt.BHALNO);
                        if (objQt.TOPSPECI  == null)
                            objQt.TOPSPECI = "";
                        objParams[12] = new SqlParameter("@P_TOPSPECI", objQt.TOPSPECI);
                             if(objQt.TERM==null)
                                 objQt.TERM = "";
                        objParams[13] = new SqlParameter("@P_TERMS", objQt.TERM);
                        objParams[14] = new SqlParameter("@P_RTVALID", objQt.RTVALID);
                        objParams[15] = new SqlParameter("@P_RTVALIDUNIT", objQt.RTVALIDUNIT);
                        objParams[16] = new SqlParameter("@P_QUOTAMT", objQt.QUOTAMT);
                        objParams[17] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[18] =new SqlParameter("@P_MDNO", objQt.MDNO);
                        objParams[19] = new SqlParameter("@P_AUTHORITY", authority); 
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_QUOTATIONENTRY_UPDATE", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddTender(Str_Quotation_Tender objQt,double PSAmt,int Npno, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_TNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_TENDERNO", objQt.TENDERNO);
                        objParams[2] = new SqlParameter("@P_TDRNO", objQt.TDRNO);
                        objParams[3] = new SqlParameter("@P_LDATESALE", objQt.LDATESALE );
                        objParams[4] = new SqlParameter("@P_LTIMESALE", objQt.LTIMESALE );
                        objParams[5] = new SqlParameter("@P_SUBMITDATE", objQt.SUBMITDATE );
                        objParams[6] = new SqlParameter("@P_SUBMITTIME", objQt.SUBMITTIME );
                        objParams[7] = new SqlParameter("@P_TDODATE", objQt.TDODATE );
                        objParams[8] = new SqlParameter("@P_TDOTIME", objQt.TDOTIME);
                        objParams[9] = new SqlParameter("@P_SDATE", objQt.SDATE );
                        objParams[10] = new SqlParameter("@P_BHALNO", objQt.BHALNO );
                        objParams[11] = new SqlParameter("@P_EMD", objQt.EMD);
                        objParams[12] = new SqlParameter("@P_STAX", objQt.STAX);
                        objParams[13] = new SqlParameter("@P_TDAMT", objQt.TDAMT);
                        objParams[14] = new SqlParameter("@P_TOTAMT", objQt.TOTAMT );
                        objParams[15] = new SqlParameter("@P_INDNO", objQt.INDENTNO );

                        if (objQt.SPECI == null)
                        {
                            objQt.SPECI = "this is test";
                        }
                        objParams[16] = new SqlParameter("@P_SPECI", objQt.SPECI);
                        objParams[17] = new SqlParameter("@P_SUBJECT", objQt.SUBJECT );
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[19] = new SqlParameter("@P_NPNO", Npno);
                        objParams[20] = new SqlParameter("@P_PSAmt", PSAmt);
                        objParams[21] = new SqlParameter("@P_TNPNO", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public int UpdateTenderNewsPaper(string tenderno, int Npno,  string colcode)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New BUDGETHEAD
                //        objParams = new SqlParameter[4];
                //        objParams[0] = new SqlParameter("@P_TNPNO", SqlDbType.Int);
                //        objParams[0].Direction = ParameterDirection.Output;
                //        objParams[1] = new SqlParameter("@P_NPNO", Npno );
                //        objParams[2] = new SqlParameter("@P_TENDERNO", tenderno );
                //        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                //        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYENTRY_INSERT", objParams, true));

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                public int UpdateTenderFieldEntry(string tenderno, int fieldno, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                      
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TFNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_FNO", fieldno);
                        objParams[2] = new SqlParameter("@P_TENDERNO", tenderno );
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERFIELDENTRY_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSingleTender(int  tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                //

                public DataSet GetSingleTenderByTenderNo(string tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_DETAILS_GETBY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }




                ///


                public int UpdateTenderEntry(Str_Quotation_Tender objQt, double PSAmt, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_TENDERNO", objQt.TENDERNO);
                        objParams[1] = new SqlParameter("@P_TDRNO", objQt.TDRNO);
                        objParams[2] = new SqlParameter("@P_LDATESALE", objQt.LDATESALE);
                        objParams[3] = new SqlParameter("@P_LTIMESALE", objQt.LTIMESALE);
                        objParams[4] = new SqlParameter("@P_SUBMITDATE", objQt.SUBMITDATE);
                        objParams[5] = new SqlParameter("@P_SUBMITTIME", objQt.SUBMITTIME);
                        objParams[6] = new SqlParameter("@P_TDODATE", objQt.TDODATE);
                        objParams[7] = new SqlParameter("@P_TDOTIME", objQt.TDOTIME);
                        objParams[8] = new SqlParameter("@P_SDATE", objQt.SDATE);
                        objParams[9] = new SqlParameter("@P_BHALNO", objQt.BHALNO);
                        objParams[10] = new SqlParameter("@P_EMD", objQt.EMD);
                        objParams[11] = new SqlParameter("@P_STAX", objQt.STAX);
                        objParams[12] = new SqlParameter("@P_TDAMT", objQt.TDAMT);
                        objParams[13] = new SqlParameter("@P_TOTAMT", objQt.TOTAMT);
                        objParams[14] = new SqlParameter("@P_INDNO", objQt.INDENTNO);
                       
                        if (objQt.SPECI == null)
                            objQt.SPECI = "";
                        objParams[15] = new SqlParameter("@P_SPECI", objQt.SPECI);
                        objParams[16] = new SqlParameter("@P_SUBJECT", objQt.SUBJECT);
                        objParams[17] = new SqlParameter("@P_COLLEGE_CODE", colcode);

                        objParams[18] = new SqlParameter("@P_PSAmt", PSAmt);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_UPDATE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetFieldsByTenderNo(string TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo );
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_FIELDS_BY_TENDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;  
                }
                public void  DeleteTenderFieldEntry(string tenderno, int fieldno)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TFNO", fieldno );
                       
                       

                     objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERFIELDENTRY_DELETE ", objParams, true);

                    }
                    catch (Exception ex)
                    {
                       // retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                   // return retStatus;
                }
                public void DeleteTenderNewsPaper(string tenderno, int Tnpno)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNPNO", Tnpno );



                        objSQLHelper.ExecuteNonQuerySP ("PKG_STR_TENDERNEWSENTRY_DELETE",objParams,true );

                    }
                    catch (Exception ex)
                    {
                        // retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    // return retStatus;
                }
                public DataSet GetNewsPaperByTenderNo(string TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_NEWSPAPER_BY_TENDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateTenderNewsPaper(string tenderno, int npno, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TNPNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_NPNO", npno );
                        objParams[2] = new SqlParameter("@P_TENDERNO", tenderno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_NEWSPAPER_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int ChangeLockForQuot(int qno, char flag)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_QNO", qno  );
                        objParams[1] = new SqlParameter("@P_ISLOCK", flag );
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_QUOTATION_UPDATE_LOCK", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllQuotationEntry(int mdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", mdno );
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetBudgetHead(string INDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INDNO", INDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_BUDGET_BY_REQTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public int ChangeLockForTender(int tno, char flag)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TNO", tno);
                        objParams[1] = new SqlParameter("@P_ISLOCK", flag);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_UPDATE_LOCK", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllTender()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       // SqlParameter[] objParams = null; ;
                        SqlParameter[] objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_MDNO", mdno);
                        ds = objSQLHelper.ExecuteDataSetSP ("PKG_STR_TENDER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SaveQuotItemEntry(string quotno, int itemno, string indentno, int qty, string colcode, string ItemSpecification, int QINO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_QINO", QINO);                     
                        objParams[1] = new SqlParameter("@P_QUOTNO", quotno );
                        objParams[2] = new SqlParameter("@P_INDNO", indentno);
                        objParams[3] = new SqlParameter("@P_ITEM_NO", itemno );
                        objParams[4] = new SqlParameter("@P_QTY", qty);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[6] = new SqlParameter("@P_ITEM_SPECIFICATION", ItemSpecification);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_QUOTATION_ITEMENTRY_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.SaveQuotItemEntry-> " + ex.ToString());
                        throw;
                    }
                    return retStatus;
                }
                
                public string ReadText(System.Web.UI.WebControls.FileUpload fld)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fld.PostedFile.FileName  );
                    
                    string text = sr.ReadToEnd();
                    sr.Close();
                    return text;

                }

                //public DataSet GenrateQuotNo(int MDNO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_QUOTNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateQuotNo-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                //11/03/2022 Gayatri
                public DataSet GenrateQuotNo(int MDNO, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_OrgId", OrgId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_QUOTNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GenrateQuotRefNo(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_REFNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateQuotRefNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GenrateTenderRefNo(int MDNO, char status, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_OrgId", OrgId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_TENDERREFNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateTenderRefNo-> " + ex.ToString());
                    }
                    return ds;
                }
                //public DataSet GenrateTenderNo(int MDNO,char status)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                //        objParams[1] = new SqlParameter("@P_STATUS",status);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_TENDERNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateTenderNo-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                // 11/03/2022 gayatri
                public DataSet GenrateTenderNo(int MDNO, char status, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_OrgId", OrgId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateTenderNo-> " + ex.ToString());
                    }
                    return ds;

                }

                // Code for Getting the Quotation locked information. 
                // Date 09 Jan 2014

                public DataSet GetAllTenderEntryLocked()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        //objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_MDNO", mdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_ALL_LOCKED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFieldEntryForQuotation(string Quotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_QUOTFIELDENTRY_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetFieldEntryForQuotation-> " + ex.ToString());
                    }
                    return ds;
                }



                //------------------------Added by shabina to get Quotation item details on modify------------22/11/2022
                public DataSet GetItemsForQuotationOnModify(string INDNO,char M)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_INDNO", INDNO);
                        objParams[1] = new SqlParameter("@P_MODIFY", M);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_ITEMS_QUOTATION_ON_MODIFY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsForQuotationTender-> " + ex.ToString());
                    }
                    return ds;
                }




            }
        }
    }
}
