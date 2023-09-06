<%@ WebService Language="C#" Class="ACDOnlinePostAdmission" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AjaxControlToolkit;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;                     //Data Access Layer
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class ACDOnlinePostAdmission  : System.Web.Services.WebService {

 
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();

    //ConnectionStrings
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    public ACDOnlinePostAdmission()
    {
    }

    //[System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetParentLinks(string knownCategoryValues, string category)
    {
        // Split the knownCategoryValues on ":" with ";" delimiter 
        // For the first dropdownlist, the values will be "undefined: id of the dropdownelement"
        // ex: "undefined: 13;"
        // The string at index 1 will be the CarId selected in the dropdownlist.
        string[] _categoryValues = knownCategoryValues.Split(':', ';');

        // Convert the element at index 1 in the string[] to get the ID
        int as_no = System.Convert.ToInt32(_categoryValues[1]);

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

        //string sql = "select al_no,al_link from access_link where al_asno=" + as_no + " and mastno = al_no and al_url is null";

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_AL_ASNO", as_no);

        SqlDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN_SP_SUBDOMAIN", objParams);

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        int i = 0;
        while (dr.Read())
        {
            values.Add(new CascadingDropDownNameValue(dr["al_link"].ToString(), dr["al_no"].ToString()));
            if (Session["mastno"] != null)
                if (dr["al_no"].ToString() == Session["mastno"].ToString())
                    values[i].isDefaultValue = true;
            i++;
        }
        //closing connections
        if (dr != null) dr.Close();

        return values.ToArray();
    }

    ////This method is for filling the Semester in assignFacultyAdvisor Page on Branch Click
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetSemByBranch(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');

        // Convert the element at index 1 in the string[] to get the ID
        int batchno = System.Convert.ToInt32(_categoryValues[1]);

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_BATCHNO", batchno);

        DataTableReader dr = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_RET_SEMESTER_BYBATCHNO", objParams).Tables[0].CreateDataReader();

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        while (dr.Read())
        {
            values.Add(new CascadingDropDownNameValue(dr["SEMESTERNAME"].ToString(), dr["SEMESTERNO"].ToString()));
        }
        //closing connections
        if (dr != null) dr.Close();

        return values.ToArray();
    }


    /// <summary>
    /// This method is for filling the Batch in assignFacultyAdvisor Page on Branch Click
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetBranchByBatch(string knownCategoryValues, string category)
    {
        string[] _categoryValues = knownCategoryValues.Split(':', ';');

        // Convert the element at index 1 in the string[] to get the ID
        string batch = _categoryValues[1];

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

        SqlParameter[] objParams = new SqlParameter[1];
        objParams[0] = new SqlParameter("@P_BATCHNO", batch);

        DataTableReader dr = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_RET_BRANCH_BYBATCH", objParams).Tables[0].CreateDataReader();

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

        while (dr.Read())
        {
            values.Add(new CascadingDropDownNameValue(dr["LONGNAME"].ToString(), dr["BRANCHNO"].ToString()));
        }
        //closing connections
        if (dr != null) dr.Close();

        return values.ToArray();
    }


    [WebMethod]
    public List<object> GetData(string data, string tablename, string col1, string col2, string col3)
    {
        //old code
        //var emp = new Employee();
        //var fetchEmail = emp.GetEmployeeList()
        //.Where(m => m.Email.ToLower().StartsWith(mail.ToLower()));

        DataTable dt = null;

        if (data.Trim() == string.Empty)
        {
            if (col3 != string.Empty)
            {
                col3 = col3.Substring(4);
                dt = objCommon.FillDropDown(tablename, "TOP 10 " + col1, col2, col3, col2).Tables[0];
            }
            else
            {
                dt = objCommon.FillDropDown(tablename, "TOP 10 " + col1, col2, string.Empty, col2).Tables[0];
            }

        }
        else
            dt = objCommon.FillDropDown(tablename, col1, col2, col2 + " LIKE '%" + data.Trim() + "%'" + col3, col2).Tables[0];

        List<object> list = new List<object>();
        string item = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {
            //if 3 columns
            if (dt.Columns.Count > 2)
                item = dr[1].ToString() + (dr[2].ToString() == string.Empty ? "  [" + dr[0].ToString() + "] " : " (" + dr[2].ToString() + ")  [" + dr[0].ToString() + "]");
            else
                //if 2 columns
                item = dr[1].ToString() + " [" + dr[0].ToString() + "]";

            list.Add(item);
        }

        return list.ToList();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetInterviewData(int BatchNo, int DegreeNo)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);


        SqlParameter[] objParams = new SqlParameter[2];
        objParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);

        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_ALL_INTERVIEW_TEST_CONFIG", objParams);

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }

    public string errmsg(Exception ex)
    {
        return "[['ERROR','" + ex.Message + "']]";
    }

   
    [WebMethod(EnableSession = true)]
    public int SaveMarks(string marks, string columnValue)
    {


        //var unQuotedString = marks.TrimStart('"').TrimEnd('"');

        var MK = marks;
        var PNL = columnValue;

        List<PanelIns> MarkDtl = JsonConvert.DeserializeObject<List<PanelIns>>(MK);

        //List<PanelInfo> PanelDtl = JsonConvert.DeserializeObject<List<PanelInfo>>(PNL);

        string json1 = JsonConvert.SerializeObject(MarkDtl);
        DataTable MKINS = JsonConvert.DeserializeObject<DataTable>(json1);

        //string json2 = JsonConvert.SerializeObject(PanelDtl);
        //DataTable PnlDtls = JsonConvert.DeserializeObject<DataTable>(json2);

       
        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
        int status = 0;
        try
        {
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];
            {
                objParams[0] = new SqlParameter("@P_INTANDCONFIG", MKINS);
                objParams[1] = new SqlParameter("@P_CREATEDBY", Session["userno"]);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
            };
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_UPDATE_INTERVIEW_CONFIG", objParams, false);

            if (Convert.ToInt32(ret) == -99)
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
            else
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);

            return status;                   

        }
        catch (Exception ex)
        {
           
        }
        return status;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetDataForMarkEntry(int BatchNo, int BranchNo)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);


        SqlParameter[] objParams = new SqlParameter[2];
        objParams[0] = new SqlParameter("@P_ADMBATCH", BatchNo);
        objParams[1] = new SqlParameter("@P_BRANCHNO", BranchNo);

        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADMP_MARKS_ENTRY", objParams);

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }

    [WebMethod(EnableSession = true)]
    public int SavePanelMarkEntry(string marks)
    {


        //var unQuotedString = marks.TrimStart('"').TrimEnd('"');

        var MK = marks;


        List<InsertMarks> MarkDtl = JsonConvert.DeserializeObject<List<InsertMarks>>(MK);

        //List<PanelInfo> PanelDtl = JsonConvert.DeserializeObject<List<PanelInfo>>(PNL);

        string json1 = JsonConvert.SerializeObject(MarkDtl);
        DataTable MKINS = JsonConvert.DeserializeObject<DataTable>(json1);

        //string json2 = JsonConvert.SerializeObject(PanelDtl);
        //DataTable PnlDtls = JsonConvert.DeserializeObject<DataTable>(json2);


        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
        int status = 0;
        try
        {
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];
            {
                objParams[0] = new SqlParameter("@P_MARKENTRY", MKINS);
                objParams[1] = new SqlParameter("@P_CREATEDBY", Session["userno"]);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
            };
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_UPDATE_MARKENTRY_FINAL", objParams, false);

            if (Convert.ToInt32(ret) == -99)
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
            else
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);

            return status;

        }
        catch (Exception ex)
        {

        }
        return status;
    }



    [WebMethod(EnableSession = true)]
    public int SavePanelFor(string PANELNAME, string ACTIVESTATUS, int PANELFORID)
    {

     
        //var PNL = PNlFor;

        //List<PanelFor> PNlDESC = JsonConvert.DeserializeObject<List<PanelFor>>(PNL);


        //string json1 = JsonConvert.SerializeObject(PNlDESC);
        //DataTable PNlDESC1 = JsonConvert.DeserializeObject<DataTable>(json1);


        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
        int status = 0;
        try
        {
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[5];
            {
                objParams[0] = new SqlParameter("@P_PANELFORNAME", PANELNAME);
                objParams[1] = new SqlParameter("@P_PANELFORID", PANELFORID);
                objParams[2] = new SqlParameter("@P_ACTIVESTATUS", ACTIVESTATUS);
                objParams[3] = new SqlParameter("@P_CREATEDBY", Session["userno"]);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
            };
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_UPDATE_PANELFOR", objParams, false);

            if (Convert.ToInt32(ret) == -99)
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
            else
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);

            return status;



        }
        catch (Exception ex)
        {

        }
        //return status;
        return 0;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GetDataPanelFor()
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);


        //SqlParameter[] objParams = new SqlParameter[2];
        //objParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
        //objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);

        ds = objSQLHelper.ExecuteDataSet("ACD_ADMP_GET_NAME_PANELFOR");

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }

    public class PanelIns
    {
        public int BRANCHNO { get; set; }
        public int BATCHNO { get; set; }
        public int DEGREENO { get; set; }       
        public int MaxMarks { get; set; }
        public string PanelFor { get; set; }
        
    }

    public class PanelInfo
    {
        public int PANELFORID { get; set; }
    }

    public class InsertMarks
    {
        public int UserNo { get; set; }
        public int PANELFORID { get; set; }
        public string EnteredMark { get; set; }
        public int BATCHNO { get; set; }
        public int BRANCHNO { get; set; }
        public int ScheduleNo { get; set; }
       
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void StatusResult(int ADMBatch, int ADHOCID)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;

        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);


        SqlParameter[] objParams = new SqlParameter[2];
        objParams[0] = new SqlParameter("@P_ADMBATCH", ADMBatch);
        objParams[1] = new SqlParameter("@P_ADHOCID", ADHOCID);


        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_DAIICT_DASHBOARD", objParams);

        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }
    

    public class Param
    {
        public string ParameterName { get; set; }
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void GenerateAllReports(string ControllerName1, int Cnrtl1Value, string ControllerName2, int Cnrtl2Value, string ControllerName3, int Cnrtl3Value, string ControllerName4, int Cnrtl4Value, string ControllerName5, int Cnrtl5Value)
    {
        ControllerName1 = ControllerName1.Replace("ctl00_ContentPlaceHolder1_", "");
        ControllerName2 = ControllerName2.Replace("ctl00_ContentPlaceHolder1_", "");
        ControllerName3 = ControllerName3.Replace("ctl00_ContentPlaceHolder1_", "");
        ControllerName4 = ControllerName4.Replace("ctl00_ContentPlaceHolder1_", "");
        ControllerName5 = ControllerName5.Replace("ctl00_ContentPlaceHolder1_", "");
        
        
        
        
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataSet ds = null;


        string ProcName = objCommon.LookUp("ACD_ADMP_Adhoc_Report_Configuration", "PROCEDURENAME", "ADHOCID=" + Cnrtl1Value);

        //DataSet dtparam = new DataSet();

        //List<Param> objprm = new List<Param>();

        DataSet ds1 = null;
        
        

        SQLHelper objSQLHelper1 = new SQLHelper(_uaims_constr);

        SqlParameter[] objParams1 = new SqlParameter[1];
        objParams1[0] = new SqlParameter("@P_ADHOCID", Cnrtl1Value);
        ds1 = objSQLHelper1.ExecuteDataSetSP("PKG_ADMP_GetParameter_AllReport", objParams1);
        
        
        
        //SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
        
        //SqlParameter[] objParams = new SqlParameter[2];
        //objParams[0] = new SqlParameter("@P_ADMBATCH", ADMBatch);
        //objParams[1] = new SqlParameter("@P_ADHOCID", ADHOCID);


        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_DAIICT_DASHBOARD", objParams);
        
        //Added By Abhijit 

     
        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

        SqlParameter[] objParams = new SqlParameter[ds1.Tables[0].Rows.Count];
    
        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
        {
            if (ControllerName1.ToString() == ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString())
            {
                objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PROCEDUREPARAMNAME"].ToString(), Cnrtl1Value);
            }

            if (ControllerName2.ToString() == ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString())
            {
                objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PROCEDUREPARAMNAME"].ToString(), Cnrtl2Value);
            }
            if (ControllerName3.ToString() == ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString())
            {
                objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PROCEDUREPARAMNAME"].ToString(), Cnrtl3Value);
            }
            if (ControllerName4.ToString() == ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString())
            {
                objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PROCEDUREPARAMNAME"].ToString(), Cnrtl4Value);
            }
            if (ControllerName5.ToString() == ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString())
            {
                objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PROCEDUREPARAMNAME"].ToString(), Cnrtl5Value);
            }
            
            //objParams[j] = new SqlParameter(ds1.Tables[0].Rows[j]["PAGECONTROLID"].ToString(),ADMBatch);
           
        }
      
        ds = objSQLHelper.ExecuteDataSetSP(ProcName, objParams);
        

        DataTable dt1 = new DataTable();
        String HeaderName = "";
        dt1.Columns.Add("HeadName");
        
          for (int i=0; i < ds.Tables[0].Columns.Count;i++ )
          {

              HeaderName = ds.Tables[0].Columns[i].ColumnName;
              dt1.Rows.Add(HeaderName);
              
          }

          ds.Tables.Add(dt1);
        
        ArrayList root = new ArrayList();
        List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
        Dictionary<string, object> data = null;
        foreach (DataTable dt in ds.Tables)
        {
            table = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                data = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    data.Add(col.ColumnName, dr[col]);
                }
                table.Add(data);
            }
            root.Add(table);
        }

        this.Context.Response.ContentType = "application/json; charset=utf-8";
        this.Context.Response.Write(serializer.Serialize(root));
    }
}