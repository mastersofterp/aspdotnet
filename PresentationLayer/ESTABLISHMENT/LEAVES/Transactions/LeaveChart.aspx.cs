using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ESTABLISHMENT_LEAVES_Transactions_LeaveChart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<GraphData> GetCourseGraph(int collgeid, int Staff, int Dept)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            // DataTable dt1 = new DataTable();

            // List<MarkDetails> details = new List<MarkDetails>();
            List<GraphData> detailsnew = new List<GraphData>();
            SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];


            objParams[0] = new SqlParameter("@P_COLLEGE_ID", collgeid);
            objParams[1] = new SqlParameter("@P_STAFF_NO", Staff);
            objParams[2] = new SqlParameter("@P_DEPT_NO", Dept); 
            ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTABLISHMENT_EMPLOYEE_LEAVECHARTS", objParams);
            dt1 = ds.Tables[0];

            foreach (DataRow dtrow in dt1.Rows)
            {
                GraphData us = new GraphData();
                //	COLLEGE_NO,CODE COLLEGE_CODE,STAFF,SUBDEPT,SUBDESIG,RULENAME
                us.IDNO = dtrow["EMPNO"].ToString();
                us.COLLEGE_CODE = dtrow["COLLEGE_CODE"].ToString();
                us.STAFF = dtrow["StaffType"].ToString();
                us.SUBDEPT = dtrow["Department"].ToString();
                us.letrno = dtrow["letrno"].ToString();
                us.LeaveName = dtrow["LeaveName"].ToString();
                us.Mname = dtrow["Mname"].ToString();
                us.Status = dtrow["Status"].ToString();
                detailsnew.Add(us);

            }
            return detailsnew;
        }
        catch (Exception ex)
        {
            return null;


        }
    }


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static GraphData[] GetCourseGraph1(int collgeid, int degree, int branch, int studylevel, int boardclass)
    //{

    //}

    public class GraphData
    {
        //      //	COLLEGE_NO,CODE COLLEGE_CODE,STAFF,SUBDEPT,SUBDESIG,RULENAME
        public string IDNO { get; set; }
        public string COLLEGE_CODE { get; set; }
        public string STAFF { get; set; }
        public string SUBDEPT { get; set; }
        public string SUBDESIG { get; set; }
        public string RULENAME { get; set; }
        public string NETPAY { get; set; }
        public string letrno { get; set; }
        public string LeaveName { get; set; }
        public string Mname { get; set; }
        public string Status { get; set; }



    }
}