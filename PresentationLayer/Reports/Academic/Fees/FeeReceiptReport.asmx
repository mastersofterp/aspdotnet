1473    147<%@ WebService Language="C#" Class="FeeReceiptReport" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Collections.Generic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Web.Script.Services;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;


//[WebService(Namespace = "http://tempuri.org/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class FeeReceiptReport : System.Web.Services.WebService
{
    static string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    //public DailyCollectionRegister GetReceiptDetailsToPrint() //string Idno, int DcrNo, Boolean Cancel)
    //{
    //    try
    //    {
    //        string myId = HttpContext.Current.Request.QueryString["Idno"];
    //        int Idno = 7934, DcrNo = 11; byte Cancel = 1;
    //        //string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //        DataSet ds = new DataSet();
    //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[3];

    //        objParams[0] = new SqlParameter("@P_IDNO", Idno);
    //        objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
    //        objParams[2] = new SqlParameter("@P_CANCEL", Cancel);

    //        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);
    //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        DailyCollectionRegister dcr = new DailyCollectionRegister();

    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            dcr.StudentName = ds.Tables[0].Rows[0]["NAME"].ToString();
    //            dcr.EnrollmentNo = ds.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
    //            dcr.YearName = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
    //            dcr.SemesterName = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
    //            dcr.DegreeName = ds.Tables[0].Rows[0]["DEGREE"].ToString();
    //            dcr.ReceiptNo = ds.Tables[0].Rows[0]["REC_NO"].ToString();
    //            dcr.TransReffNo = ds.Tables[0].Rows[0]["TRANS_REFNO"].ToString();
    //            dcr.PaymentType = ds.Tables[0].Rows[0]["PAY_TYPE"].ToString();
    //            dcr.TotalAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString());
    //            dcr.DemandDraftAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["DD_AMT"].ToString());
    //            dcr.RecDate = ds.Tables[0].Rows[0]["REC_DT"].ToString();
    //            dcr.TotAmtInWord = ds.Tables[0].Rows[0]["TOTAL_AMT_IN_WORDS"].ToString();
    //            dcr.CashAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["CASH_AMT"].ToString());
    //            dcr.Remark = ds.Tables[0].Rows[0]["REMARK"].ToString();
    //            dcr.ExcessAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["EXCESS_AMOUNT"].ToString());
    //            dcr.UA_FullName = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
    //            dcr.BranchName = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
    //            dcr.PayTypeName = ds.Tables[0].Rows[0]["PAYTYPENAME"].ToString();
    //            dcr.Currency = Convert.ToInt16(ds.Tables[0].Rows[0]["CUR_NO"].ToString());
    //            dcr.AdjustedAmt = Convert.ToDouble(ds.Tables[0].Rows[0]["ADJUSTED_AMT"].ToString());
    //            dcr.AdjustedAmtInWord = ds.Tables[0].Rows[0]["ADJUSTED_AMT_INWORDS"].ToString();
    //            dcr.BankName = ds.Tables[0].Rows[0]["BANKNAME"].ToString();
    //            dcr.PayMode = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

    //            List<Feehead> feeHeadAmtsDetails = new List<Feehead>();
    //            foreach (DataRow dtrow in ds.Tables[0].Rows)
    //            {
    //                Feehead feeHeadAmts = new Feehead();
    //                feeHeadAmts.Fee_LongName = dtrow["FEE_LONGNAME"].ToString();
    //                feeHeadAmts.FeeAmount = Convert.ToDouble(dtrow["AMOUNT"].ToString());
    //                feeHeadAmtsDetails.Add(feeHeadAmts);
    //            }
    //            dcr.FeeHeadsWithAmount = feeHeadAmtsDetails;
    //        }
    //        return dcr;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetReceiptDetailsToPrint() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //}



    //[WebMethod(EnableSession = true)]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public void  GetReceiptDetails()
    //{
    //    try
    //    {
    //        string myId = HttpContext.Current.Request.QueryString["id"];

    //        int Idno = 7934;
    //        int DcrNo = 11; byte Cancel = 1;
    //        DataSet ds = new DataSet();
    //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[3];

    //        objParams[0] = new SqlParameter("@P_IDNO", Idno);
    //        objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
    //        objParams[2] = new SqlParameter("@P_CANCEL", Cancel);
    //        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);

    //        var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
    //       // return jsonstring;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
    //    }
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDetails()
    {
        try
        {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());
            byte Cancel = Convert.ToByte(HttpContext.Current.Request.QueryString["Cancel"].ToString());

            // int Idno = 7934; int DcrNo = 11; byte Cancel = 1;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            objParams[2] = new SqlParameter("@P_CANCEL", Cancel);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);

            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstring;

        }
        catch (Exception ex)
        {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
        }
    }






    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDetailsRcpit()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());
            byte Cancel = Convert.ToByte(HttpContext.Current.Request.QueryString["Cancel"].ToString());

            // int Idno = 7934; int DcrNo = 11; byte Cancel = 1;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            objParams[2] = new SqlParameter("@P_CANCEL", Cancel);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);

            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstring;

            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDetailsRcpiper()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());
            byte Cancel = Convert.ToByte(HttpContext.Current.Request.QueryString["Cancel"].ToString());

            // int Idno = 7934; int DcrNo = 11; byte Cancel = 1;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            objParams[2] = new SqlParameter("@P_CANCEL", Cancel);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);

            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstring;

            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }





    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDetailsCpukota()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());
            byte Cancel = Convert.ToByte(HttpContext.Current.Request.QueryString["Cancel"].ToString());

            // int Idno = 7934; int DcrNo = 11; byte Cancel = 1;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            objParams[2] = new SqlParameter("@P_CANCEL", Cancel);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_FEERECIEPT", objParams);

            var jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstring;

            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

    
    
    
    
    
    
    
    
    
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDDDetails()
    {
        try
        {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString()); 
            
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_SUBREPORT_DD_DETAILS", objParams);

            var jsonstringDD = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstringDD;
        }
        catch (Exception ex)
        {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
        }
    }






    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDDDetailsCpukota()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());

            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_SUBREPORT_DD_DETAILS", objParams);

            var jsonstringDD = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstringDD;
            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDDDetailsRcpit()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());

            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_SUBREPORT_DD_DETAILS", objParams);

            var jsonstringDD = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstringDD;
            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetFeeReceiptDDDetailsRcpiper()
        {
        try
            {
            int Idno = Convert.ToInt32(HttpContext.Current.Request.QueryString["Idno"].ToString());
            int DcrNo = Convert.ToInt32(HttpContext.Current.Request.QueryString["DcrNo"].ToString());

            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[2];

            objParams[0] = new SqlParameter("@P_IDNO", Idno);
            objParams[1] = new SqlParameter("@P_DCRNO", DcrNo);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_SUBREPORT_DD_DETAILS", objParams);

            var jsonstringDD = Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]);
            return jsonstringDD;
            }
        catch (Exception ex)
            {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetFeeReceiptDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
        }
    
    
    
    

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public College GetCollegeLogo()
    {
        try
        {
            College clg = new College();
            int CollegeID = Convert.ToInt32(HttpContext.Current.Request.QueryString["CollegeID"].ToString());
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_COLLEGE_CODE", CollegeID);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_COLLEGE_INFO", objParams);
            string clgLogo = string.Empty;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                clg.Name = ds.Tables[0].Rows[0]["COLLEGENAME"].ToString();
                clg.Address = ds.Tables[0].Rows[0]["COLLEGE_ADDRESS"].ToString();
                byte[] logo = ds.Tables[0].Rows[0]["COLLEGE_LOGO"] as byte[];
                clgLogo = Convert.ToBase64String(logo);
                clg.CollegeLogo = clgLogo;
            }
            return clg;
        }
        catch (Exception ex)
        {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetCollegeLogo() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public College GetCollegeBanner()
    {
        try
        {
            College clg = new College();
            int CollegeID = Convert.ToInt32(HttpContext.Current.Request.QueryString["CollegeID"].ToString());
            DataSet ds = null;
            string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_COLLEGE_CODE", CollegeID);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_COLLEGE_INFO", objParams);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                clg.Name = ds.Tables[0].Rows[0]["COLLEGENAME"].ToString();
                clg.Address = ds.Tables[0].Rows[0]["COLLEGE_ADDRESS"].ToString();
            }
            return clg;

        }
        catch (Exception ex)
        {
            throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetCollegeBanner() --> " + ex.Message + " " + ex.StackTrace);
        }

    }
}