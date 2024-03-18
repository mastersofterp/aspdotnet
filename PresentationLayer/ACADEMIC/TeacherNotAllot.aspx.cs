//================================================================================================================
// PROJECT NAME   : NITRAIPUR                                                        
// MODULE NAME    : ACADEMIC                                                             
// PAGE NAME      : Teacher Not Alloted
// CREATION DATE  : 06-JULY-2011                                                          
// CREATED BY     : ASHISH DHAKATE                                                   
// MODIFIED DATE  : 31/12/2021 - Rishabh Bajirao                                                                    
// MODIFIED DESC  : Added Excel Report 
// MODIFIED DATE  : 27/03/2024 - Jay Takalkhede                                                         
// MODIFIED DESC  : Added Excel Report 1) Teacher Not Tagged 2) Programs/Branches Not Yet Registered  (TkNo. 55317)                                                                     
//================================================================================================================

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
using ClosedXML.Excel;
using System.IO;


public partial class Academic_REPORTS_MarksEntryNotDone : System.Web.UI.Page
{
    Common objCommon = new Common();
    CourseTeacherAllotController objAllot = new CourseTeacherAllotController();
    UAIMS_Common objUCommon = new UAIMS_Common();

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
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();


                    if (Session["usertype"].ToString() != "1")
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC");
                        objCommon.FillDropDownList(ddlSessionH, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC");

                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC");
                        objCommon.FillDropDownList(ddlSessionH, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "ISNULL(IS_ACTIVE,0)=1", "SESSIONID DESC");
                        // objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                        // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "D.DEGREENAME");
                    }
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 16/12/2021
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 16/12/2021
            }

            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            divMsg.InnerHtml = string.Empty;
        }
        catch
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
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }

    #endregion Page Load

    #region ShowReport

    protected void btnReport1_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.btnReport1_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport2_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=REPORT";
            url += "&path=~,Reports,Academic," + "rptTestMarksEntryNotDone.rpt";
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeacher, this.updTeacher.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport()
    {
<<<<<<< HEAD
        try
        {
            //Added By Rishabh on 31/12/2021
            //if (rblAllotment.SelectedValue != "1" && rblAllotment.SelectedValue != "2" && rblAllotment.SelectedValue != "3")
            //{
            //    objCommon.DisplayMessage(this.Page, "Please select option.", this.Page);
            //    return;
            //}
            DataSet ds = null;
            string filename = string.Empty;
            if (ddlReport.SelectedValue == "1")
            {
                ds = objAllot.GetCourseTeachernotAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["colcode"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));
                ds.Tables[0].TableName = "Course_Teacher_Not_Allot";
                filename = "Course_Teacher_Not_Allot";
            }
            else if (ddlReport.SelectedValue == "2")
=======
        //Added By Rishabh on 31/12/2021
        //if (rblAllotment.SelectedValue != "1" && rblAllotment.SelectedValue != "2" && rblAllotment.SelectedValue != "3")
        //{
        //    objCommon.DisplayMessage(this.Page, "Please select option.", this.Page);
        //    return;
        //}
        DataSet ds = null;
        string filename = string.Empty;
        if (ddlReport.SelectedValue == "1")
        {
            ds = objAllot.GetCourseTeachernotAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["colcode"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));
            ds.Tables[0].TableName = "Course_Teacher_Not_Allot";
            filename = "Course_Teacher_Not_Allot";
        }
        else if (ddlReport.SelectedValue == "2")
        {
            ds = objAllot.GetTeachernotAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["colcode"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));
           
            ds.Tables[0].TableName = "Teacher Not Allot";
            filename = "Teacher Not Allot";
        }
        else if (ddlReport.SelectedValue == "3")
        {
            ds = objAllot.GetCourseTeacherAllotmentDoneExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue));
            ds.Tables[0].TableName = "Course Teacher Allot";
            ds.Tables[1].TableName = "Summery Course Teacher Report";
            filename = "Course Teacher Allot";
        }
        else if (ddlReport.SelectedValue == "4")
        {
            ds = objAllot.GetFacultyNotTagToCourse();
            ds.Tables[0].TableName = "Teacher Not Tag";
            filename = "Teacher Not Alloted to any Course";
        }
        if (ds.Tables[0].Rows.Count < 1)
        {
            objCommon.DisplayMessage(this.updTeacher, "Record Not Found", this.Page);
            return;
        }
        GridView gv = new GridView();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //gv.DataSource = ds;
            //gv.DataBind();
            //string attachment = rblAllotment.SelectedValue != "3" ? "attachment ; filename=Teacher_Not_Allot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls" : "attachment ; filename=Teacher_Alloted_Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", attachment);
            //Response.ContentType = "application/ms-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //gv.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.Flush();
            //Response.End();
            string attachment = "attachment ; filename=" + filename + ".xls";

            //if (rblAllotment.SelectedValue == "3")
            //{
            //    ds.Tables[1].TableName = "Summery Course Teacher Report";
            //}
            using (XLWorkbook wb = new XLWorkbook())
>>>>>>> eb55f393 ([ENHANCEMENT] [54221] [TEACHER NOT TAGGED])
            {
                ds = objAllot.GetTeachernotAllot(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["colcode"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ddlSemester.SelectedValue));

                ds.Tables[0].TableName = "Teacher Not Allot";
                filename = "Teacher Not Allot";
            }
            else if (ddlReport.SelectedValue == "3")
            {
                ds = objAllot.GetCourseTeacherAllotmentDoneExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSemester.SelectedValue));
                ds.Tables[0].TableName = "Course Teacher Allot";
                ds.Tables[1].TableName = "Summery Course Teacher Report";
                filename = "Course Teacher Allot";
            }
            // Added By Jay T. On dated 27032024 (TkNo.55317)  Add Session Condition In Excel 
            else if (ddlReport.SelectedValue == "4")
            {
                ds = objAllot.GetFacultyNotTagToCourse(Convert.ToInt32(ddlSession.SelectedValue));
                ds.Tables[0].TableName = "Teacher Not Tag";
                filename = "Teacher Not Alloted to any Course";
            }
            //Added By Jay T. On dated 27032024 (TkNo.55317) Add New Report "Programs/Branches Not Yet Registered"
            else if (ddlReport.SelectedValue == "5")
            {
                ds = objAllot.GetBranchNotYetRegister(Convert.ToInt32(ddlSession.SelectedValue));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].TableName = "Branches Not Yet Registered";
                }
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[0] ! =null)
                //{
                //    ds.Tables[1].TableName = "Programs/Branches Not Yet Registered";
                //}
                filename = "Branches Not Yet Registered";
            }
            if (ds.Tables[0].Rows.Count < 1)
            {
                objCommon.DisplayMessage(this.updTeacher, "Record Not Found", this.Page);
                return;
            }
            GridView gv = new GridView();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment ; filename=" + filename + ".xls";

                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                        wb.Worksheets.Add(dt);    //Add System.Data.DataTable as Worksheet.

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", attachment);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion ShowReport

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Cancel

    #region DDL
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //  objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) , "BRANCHNO");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlClgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.SelectedIndex = 0;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgScheme.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

            }
            //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ViewState["college_id"], "SESSIONNO DESC");
            //   ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.ddlClgScheme_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlClgScheme.SelectedIndex = 0;
            if (ddlSession.SelectedIndex > 0)
            {

                if (Session["usertype"].ToString() == "1")
                {
                    AcademinDashboardController AcadDash = new AcademinDashboardController(); // add by maithili [07-09-2022]
                    DataSet ds = null;
                    ds = AcadDash.Get_CollegeID_BySession(Convert.ToInt32(ddlSession.SelectedValue));

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        ddlClgScheme.DataSource = ds.Tables[1];
                        ddlClgScheme.DataValueField = ds.Tables[1].Columns[0].ToString();
                        ddlClgScheme.DataTextField = ds.Tables[1].Columns[1].ToString();
                        ddlClgScheme.DataBind();
                    }
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)  INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)inner join ACD_SESSION_MASTER  CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_SESSION S ON (S.SESSIONID = CM.SESSIONID)", "COSCHNO", "COL_SCHEME_NAME", "S.SESSIONID=" + ddlSession.SelectedValue + "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
                }
            }
            else
            {
                ddlClgScheme.Items.Clear();
                ddlClgScheme.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.ddlSession_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
<<<<<<< HEAD
    #endregion DDL

    #region ddlReport
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSession.SelectedIndex = 0;
            ddlClgScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;

            if (Convert.ToInt32(ddlReport.SelectedValue) > 0)
            {
                pnlfooter.Visible = true;
                if (Convert.ToInt32(ddlReport.SelectedValue) == 1 || Convert.ToInt32(ddlReport.SelectedValue) == 2 || Convert.ToInt32(ddlReport.SelectedValue) == 3)
                {
                    pnlHideShow1.Visible = false;
                    pnlHideShow.Visible = true;
                }
                // Added By Jay T. On dated 27032024 (TkNo.55317)
                else if (Convert.ToInt32(ddlReport.SelectedValue) == 4 || Convert.ToInt32(ddlReport.SelectedValue) == 5)
                {
                    pnlHideShow1.Visible = true;
                    pnlHideShow.Visible = false;
                }
                else
                {
                    pnlHideShow1.Visible = false;
                    pnlHideShow.Visible = false;
                }
            }
            else
            {
                pnlfooter.Visible = false;
                pnlHideShow.Visible = false;
                pnlHideShow1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeacherNotAllot.aspx.ddlReport_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion ddlReport
=======
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlClgScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

        if (Convert.ToInt32(ddlReport.SelectedValue) > 0)
        {
            pnlfooter.Visible = true;
            if (Convert.ToInt32(ddlReport.SelectedValue) == 1 || Convert.ToInt32(ddlReport.SelectedValue) == 2 || Convert.ToInt32(ddlReport.SelectedValue) == 3)
            {
                pnlHideShow.Visible = true;
            }
            else
            {
                pnlHideShow.Visible = false;
            }
        }
        else
        {
            pnlfooter.Visible = false;
            pnlHideShow.Visible = false;
        }
    }
>>>>>>> eb55f393 ([ENHANCEMENT] [54221] [TEACHER NOT TAGGED])
}
