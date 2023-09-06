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



/// <summary>
/// WebService used for Ajax Filling of Drop Down List
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService()]

public class WebService : System.Web.Services.WebService
{

    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();

    //ConnectionStrings
    private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    public WebService()
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


    //////This method is for filling the Branch in SchemeMaster Page on Department Click
    ////[WebMethod(EnableSession = true)]
    ////public CascadingDropDownNameValue[] GetBranch(string knownCategoryValues, string category)
    ////{
    ////    string[] _categoryValues = knownCategoryValues.Split(':', ';');

    ////    // Convert the element at index 1 in the string[] to get the ID
    ////    int deptno = System.Convert.ToInt32(_categoryValues[1]);

    ////    SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

    ////    SqlParameter[] objParams = new SqlParameter[2];
    ////    objParams[0] = new SqlParameter("V_DEPT", OracleDbType.Int32, deptno, System.Data.ParameterDirection.Input);
    ////    objParams[1] = new SqlParameter("COMMON_CURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

    ////    OracleDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN.SP_ALL_BRANCH", objParams);

    ////    List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

    ////    while (dr.Read())
    ////    {
    ////        values.Add(new CascadingDropDownNameValue(dr["LONGNAME"].ToString(), dr["FACULTY"].ToString()));
    ////    }
    ////    //closing connections
    ////    if (dr != null) dr.Close();

    ////    return values.ToArray();
    ////}

    //////This method is for filling the Semester in SchemeMaster Page on Branch Click
    ////[WebMethod(EnableSession = true)]
    ////public CascadingDropDownNameValue[] GetSemester(string knownCategoryValues, string category)
    ////{
    ////    string[] _categoryValues = knownCategoryValues.Split(':', ';');

    ////    // Convert the element at index 1 in the string[] to get the ID
    ////    int id = System.Convert.ToInt32(_categoryValues[1]);

    ////    SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

    ////    SqlParameter[] objParams = new SqlParameter[1];
    ////    objParams[0] = new SqlParameter("COMMON_CURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

    ////    OracleDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN.SP_ALL_SEMESTER", objParams);

    ////    List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();

    ////    while (dr.Read())
    ////        values.Add(new CascadingDropDownNameValue(dr["SEMFULLNAME"].ToString(), dr["SEMESTERNO"].ToString()));

    ////    //closing connections
    ////    if (dr != null) dr.Close();

    ////    return values.ToArray();
    ////}

    //////This method is for filling the Degree in CourseMaster Page on Department Click
    ////[WebMethod(EnableSession = true)]
    ////public CascadingDropDownNameValue[] GetDegreeByDept(string knownCategoryValues, string category)
    ////{
    ////    string[] _categoryValues = knownCategoryValues.Split(':', ';');

    ////    // Convert the element at index 1 in the string[] to get the ID
    ////    int id = System.Convert.ToInt32(_categoryValues[1]);

    ////    SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

    ////    SqlParameter[] objParams = new SqlParameter[2];
    ////    objParams[0] = new SqlParameter("V_OFFDEPT", OracleDbType.Int32, id, System.Data.ParameterDirection.Input);
    ////    objParams[1] = new SqlParameter("COMMON_CURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

    ////    OracleDataReader dr = objSQLHelper.ExecuteReaderSP("PKG_DROPDOWN.SP_ALL_DEGREE", objParams);

    ////    List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
    ////    int i = 0;
    ////    while (dr.Read())
    ////    {
    ////        values.Add(new CascadingDropDownNameValue(dr["COLLEGENAME"].ToString(), dr["COLLEGENO"].ToString()));
    ////    }
    ////    //closing connections
    ////    if (dr != null) dr.Close();

    ////    return values.ToArray();
    ////}



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

    //public static string dataSetToJSON(DataSet ds)
    //{
    //    ArrayList root = new ArrayList();
    //    List<Dictionary<string, object>> table;
    //    Dictionary<string, object> data;

    //    foreach (DataTable dt in ds.Tables)
    //    {
    //        table = new List<Dictionary<string, object>>();
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            data = new Dictionary<string, object>();
    //            foreach (DataColumn col in dt.Columns)
    //            {
    //                data.Add(col.ColumnName, dr[col]);
    //            }
    //            table.Add(data);
    //        }
    //        root.Add(table);
    //    }
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    return serializer.Serialize(root);
    //}


    //For 
    [WebMethod]
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
            objParams = new SqlParameter[2];
            {
                objParams[0] = new SqlParameter("@P_INTANDCONFIG", MKINS);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
            };
            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_UPDATE_INTERVIEW_CONFIG", objParams, false);

            if (Convert.ToInt32(ret) == -99)
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
            else
                status = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);

            return status;
            //object obj = 1;
            //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("1"))
            //    status = Convert.ToInt32(CustomStatus.RecordSaved);
            //else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("2"))
            //    status = Convert.ToInt32(CustomStatus.RecordUpdated);
            //else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("2627"))
            //    status = Convert.ToInt32(CustomStatus.RecordExist);
            //else
            //    status = Convert.ToInt32(CustomStatus.Error);


            object optval = 0;
            // ACADEMIC_EXAMINATION_Rajagiri_End_SemMark objEndsem = new ACADEMIC_EXAMINATION_Rajagiri_End_SemMark();          

        }
        catch (Exception ex)
        {
           
        }
        return status;
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
}
