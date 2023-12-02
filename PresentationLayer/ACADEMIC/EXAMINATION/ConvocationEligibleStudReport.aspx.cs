using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Net;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_TabulationChart : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
    CourseController objCC = new CourseController();
    bool IsDataPresent = false;

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //TO SET THE MASTERPAGE
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //CHECK SESSION

            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //PAGE AUTHORIZATION
                // this.CheckPageAuthorization();

                //SET THE PAGE TITLE
                this.Page.Title = Session["coll_name"].ToString();

                //LOAD PAGE HELP
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.FillDropdownList();
                this.PopulateSessionDropDown();

                string passoutflag = (objCommon.LookUp("ACD_CONVOCATION_CONFIGUARATION_LEVEL", "TOP 1 ISNULL(PASSOUT_RPT,0)", ""));
                string convfeedback = (objCommon.LookUp("ACD_CONVOCATION_CONFIGUARATION_LEVEL", "TOP 1 ISNULL(FEEDBACK_RPT,0)", ""));

                if (passoutflag == "1")
                {
                    btnPassStudList.Visible = true;
                }
                if (convfeedback == "1")
                {
                    btnConvocationExcelReport.Visible = true;
                }
            }
        }
    }
    #endregion

    #region Click Events

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region User Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
        }
    }

    private void FillDropdownList()
    {
        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + "))", "");
        //Fill College&Scheme Dropdown
        //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");

        //**objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"]) + ") OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");

        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();

        objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");

    }
    private void PopulateSessionDropDown()   //Added by Sachin A dt on 27022023 as per requirement
    {
        try
        {
            //Fill Dropdown Session 
            string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            DataSet dsCollegeSession = objCC.GetCollegeSession(1, college_IDs);
            ddlCollege.Items.Clear();
            //ddlCollege.Items.Add("Please Select");
            ddlCollege.DataSource = dsCollegeSession;
            ddlCollege.DataValueField = "SESSIONNO";
            ddlCollege.DataTextField = "COLLEGE_SESSION";
            ddlCollege.DataBind();
            //  rdbReport.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
 

    string admbatch = string.Empty; 
    public void clear()
    { 
        ddlSession.SelectedIndex = 0;
    }

    #endregion

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common objCommon = new Common();

        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0  AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
            }
        } 
    }
    protected void btnConvocationEligible_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsCollege;
            string sessionno = string.Empty;
            for (int k = 0; k < ddlCollege.Items.Count; k++)
            {
                if (ddlCollege.Items[k].Selected == true)
                    sessionno += ddlCollege.Items[k].Value + "$";
            }

            GridView GDProgession = new GridView();
            string ContentType = string.Empty;
            DataSet ds = null;  
            string proc_name = "PKG_ACD_DEGREE_COMPLETE_CONVOCATION_EXCEL_CRESCENT";  //"PKG_ACD_DEGREE_COMPLETE_CONVOCATION_EXCEL";
            string param = "@P_SESSIONNO";
            string call_values = "" + sessionno + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GDProgession.DataSource = ds;
                GDProgession.DataBind();

                string attachment = "attachment; filename=PassoutStudentListReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GDProgession.RenderControl(htw);
                //lvStudApplied.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
     
    protected void btnConvocationExcelReport_Click1(object sender, EventArgs e)     
    {
        bool CompleteRequest = false;
        try
        {
            SQLHelper objsql = new SQLHelper(_nitprm_constr);
            SqlParameter[] objParams = new SqlParameter[0];
            DataSet ds = objsql.ExecuteDataSetSP("PKG_CONVOCATION_REPORT_NEW", objParams);

            GridView GVStatus = new GridView();
            string ContentType = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                GVStatus.DataSource = ds;
                GVStatus.DataBind();

                string attachment = "attachment; filename= ConvocationExcelReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVStatus.RenderControl(htw);
                Response.Write(sw.ToString());

                CompleteRequest = true;
            }
            else
            {
                GVStatus.DataSource = null;
                GVStatus.DataBind();
                objCommon.DisplayMessage("No record found...", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDataSessionWise_btnExcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

        if (CompleteRequest)
            Response.End();
    }
}

