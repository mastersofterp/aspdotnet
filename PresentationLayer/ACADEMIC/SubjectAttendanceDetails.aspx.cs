//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Faculty wise Attendance Report
// CREATION DATE : 06-DECEMBER-2021                                                    
// CREATED BY    : JAY S. TAKALKHEDE                                                      
//=======================================================================================
//----------------------------------------------------------------------------------------------------------------------------------------------
//-- Version      Modified On        Modified By        Purpose
//-----------------------------------------------------------------------------------------------------------------------------------------------
//--  RFC.1.1    18-March-2024       Jay Takalkhede      Add vistate session condition in Excel report  (Tktno. 56508)                                                                  (Tktno. 57393)[TGPCET]  
//----------------------------------------------------------------------------------------------------------------------------------------------
//--  RFC.1.2    16-April-2024       Jay Takalkhede      Add OD related Colummn in Excel report and Listview  (Tktno.56806)                                                                  (Tktno. 57393)[TGPCET]  
//----------------------------------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_SubjectAttendanceDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController objAtt = new StudentAttendanceController();

    #region Page Load 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();
            }
            PopulateDropDown();

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 04/01/2022
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 04/01/2022
        }
    }
    private void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONID DESC"); // ADDED BY Nehal N. ON DATED 30.06.2023
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT WITH (NOLOCK)", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }
    #endregion

    #region Page Load
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_SESSION_MASTER SM ON (C.COLLEGE_ID = SM.COLLEGE_ID)", "C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND SM.SESSIONID= " + Convert.ToInt32(ddlSession.SelectedValue) + " AND C.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "C.COLLEGE_ID");

            //this.objCommon.FillDropDownList(ddlCollege, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlCollege.Focus();
        }
        else
        {
            ddlCollege.Items.Clear();
            ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlFaculty, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 AND UA_DEPTNO=" + ddlDepartment.SelectedValue, "UA_NO");
            objCommon.FillDropDownList(ddlFaculty, "USER_ACC WITH (NOLOCK)", "UA_NO", "UA_FULLNAME", "UA_TYPE=3 AND '" + ddlDepartment.SelectedValue + "' in (select value from dbo.Split(UA_DEPTNO, ',')) AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "UA_FULLNAME");
        }
        else
        {
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCollege.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID = "+ Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO DESC");
        //}
        //else
        //{
        //    ddlSession.Items.Clear();
        //    ddlSession.Items.Add(new ListItem("Please Select", "0"));
        //}
    }
    #endregion

    #region BindListview
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            // Added By Jay Takalkhede On dated 18/03/2024 (TkNo.56508)
            ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));

            DataSet ds = GetStudAttDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), Convert.ToInt32(ddlFaculty.SelectedValue), txtFromDate.Text, txtToDate.Text);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudAttendance.DataSource = ds;
                    lvStudAttendance.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudAttendance);//Set label -
                    divlvStudentHeading.Visible = true;
                }
                else
                {
                    objCommon.DisplayUserMessage(updProg, "No Record Found.", this.Page);
                    divlvStudentHeading.Visible = false;
                }

            }
            else
            {
                objCommon.DisplayUserMessage(updProg, "No Record Found.", this.Page);
                divlvStudentHeading.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updProg, "Please Select School/Institute.", this.Page);
                    divlvStudentHeading.Visible = false;
           
        }
    }
    #endregion BindListview

    #region ShowReport
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //char ch = '/';
            //string[] Fromdate = txtAttDate.Text.Split(ch);
            //string[] Todate = txtRecDate.Text.Split(ch);
            //string fdate = Fromdate[1] + "/" + Fromdate[0] + "/" + Fromdate[2];
            //string tdate = Todate[1] + "/" + Todate[0] + "/" + Todate[2];

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
                ",@P_UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) +
                ",@P_BRANCH=" + Convert.ToString(ddlDepartment.SelectedItem.Text) +
                ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["sessionno"].ToString()) +
                ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text +
                ",@P_SESSIONNAME=" + Convert.ToString(ddlSession.SelectedItem.Text);

            // +",@P_PERIODNO=" + ddlPeriod.SelectedValue
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_FROMDATE=" + txtFromDate.Text + ",@P_TODATE=" + txtToDate.Text; // +",@P_PERIODNO=" + ddlPeriod.SelectedValue
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion ShowReport

    #region Cancel
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Cancel

    #region Excel
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ViewState["sessionno"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue));
        GridView GVDayWiseAtt = new GridView();
        DataSet ds = objAtt.GetStudAttDetails(Convert.ToInt32(ViewState["sessionno"].ToString()), Convert.ToInt32(ddlFaculty.SelectedValue), txtFromDate.Text, txtToDate.Text);

        if (ds != null && ds.Tables.Count > 0)
        {
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename="+"Faculty wise Attendance Report" + "_" + txtFromDate.Text.Trim() + "_" + txtToDate.Text.Trim() + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Attendance Not Found", this.Page);
        }
    }
    #endregion

    #region Method
    /// <summary>
    /// Added by S.Patil = 29082019
    /// </summary>
    /// <param name="session"></param>
    /// <param name="uaNo"></param>
    /// <param name="fdate"></param>
    /// <param name="todate"></param>
    /// <returns></returns>
     string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    public DataSet GetStudAttDetails(int session, int uaNo, string fdate, string todate)
    {
        DataSet ds = null;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(connectionString);

            SqlParameter[] objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_SESSIONNO", session);
            objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
            objParams[2] = new SqlParameter("@P_FROMDATE", fdate);
            objParams[3] = new SqlParameter("@P_TODATE", todate);
            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_FACULTY_SUBJECTWISE_Listview", objParams);
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudAttDetails-> " + ex.ToString());
        }

        return ds;
    }
    #endregion
}