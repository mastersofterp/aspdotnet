//======================================================================================
// PROJECT NAME  : UAIMS / RFC                                                             
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PAPER SET DETAILS
// CREATION DATE : 30-08-2012
// ADDED BY      : PRIYANKA KABADE                                                  
// ADDED DATE    : 
// MODIFIED BY   : SHUBHAM BARKE (FOR RFC COMMON)
// MODIFIED DESC :                                                    
//======================================================================================

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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_PaperSetDetails : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    int SessionNo;

    #region Page Events
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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                ////Page Authorization
                 CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
               

                //ddlSession FILL
                string deptno = string.Empty;
                if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                {
                    deptno = "0";
                }
                else
                {
                    deptno = Session["userdeptno"].ToString();
                }
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
                }
                else
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
               

            }
        }
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ThirdSem_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ThirdSem_Registration.aspx");
        }
    }
    #endregion

    #region ddl Events
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "S.SESSIONID DESC"); //--AND FLOCK = 1
            }

            ddlSession.Focus();
        }
        else
        {
            ddlClgname.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));
            ViewState["cursession"] = SessionNo;
            //DDLDEGREE FILL
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_COURSE C ON (S.SEMESTERNO = C.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0 AND C.SUBID =1  AND C.MAXMARKS_E > 0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), " S.SEMESTERNO");
            ddlSemester.Focus();
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlList.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        ddlSemester.Focus();
        btnunlock.Visible = false;
    }

    #endregion

    #region Private Methods
    private void BindListView()
    {

        
        SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));

       // DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON S.SCHEMENO = C.SCHEMENO LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.CCODE = C.CCODE) LEFT OUTER JOIN ACD_PAPERSET_DETAILS P ON (c.CCODE = P.CCODE AND c.SEMESTERNO = P.SEMESTERNO AND (p.CANCEL IS NULL OR p.CANCEL = 0)) ", "DISTINCT C.CCODE ,COURSE_NAME", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE  WHERE SCHEMETYPENO = S.SCHEMETYPE)SCHEMETYPE,COUNT(DISTINCT IDNO)TOTAL_STUDENT ,REQ_LEVEL ", "C.SUBID = 1 AND MAXMARKS_E > 0 AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND C.REQ_LEVEL != 0 GROUP BY C.CCODE,COURSE_NAME,REQ_LEVEL,S.SCHEMETYPE ", "CCODE"); //MODIFIED by SHUBHAM on 12/04/23
        DataSet ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME S ON S.SCHEMENO = C.SCHEMENO LEFT OUTER JOIN ACD_STUDENT_RESULT SR ON (SR.CCODE = C.CCODE AND SR.SESSIONNO = " + Convert.ToInt32(SessionNo) + ") LEFT OUTER JOIN ACD_PAPERSET_DETAILS P ON (c.CCODE = P.CCODE AND c.SEMESTERNO = P.SEMESTERNO AND (p.CANCEL IS NULL OR p.CANCEL = 0) AND P.SESSIONNO= SR.SESSIONNO)", "DISTINCT C.CCODE ,COURSE_NAME", "(SELECT SCHEMETYPE FROM ACD_SCHEMETYPE  WHERE SCHEMETYPENO = S.SCHEMETYPE)SCHEMETYPE,COUNT(DISTINCT IDNO)TOTAL_STUDENT ,REQ_LEVEL ", "C.SUBID = 1 AND MAXMARKS_E > 0 AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND C.REQ_LEVEL != 0 GROUP BY C.CCODE,COURSE_NAME,REQ_LEVEL,S.SCHEMETYPE ", "CCODE"); //MODIFIED by SHUBHAM on 12/04/23
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds;
                lvCourse.DataBind();
                pnlList.Visible = true;
                btnSave.Visible = true;
                btnunlock.Visible = true;
                ////btnCancel.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updPaperStock, "No Record found!", this.Page);
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlList.Visible = false;
                btnSave.Visible = false;
                btnunlock.Visible = false;
                btnCancel.Visible = false;
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            pnlList.Visible = false;
            btnSave.Visible = false;
            btnunlock.Visible = false;
            btnCancel.Visible = false;
        }
    }
    #endregion

    #region Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        PStaffController PsatffCont = new PStaffController();
        // ADDED BY SHUBHAM ON 02/03/2023
        int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
        foreach (ListViewItem item in lvCourse.Items)
        {
            Label lblCCode = item.FindControl("lblCCode") as Label;
            CheckBox cbchk = item.FindControl("cbchk") as CheckBox;

            
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));
            int ret;
            int COLLEGEID = Convert.ToInt32(ViewState["college_id"]);
           // string str = objCommon.LookUp("ACD_PAPERSET_DETAILS P INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "COUNT(SEMESTERNO)", "(DEAN_LOCK = 1 OR APPROVED IS not NULL OR BOS_LOCK = 1) AND BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND CCODE = '" + lblCCode.Text + "' AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SM.SESSIONID = " + sessionno);
            string str = objCommon.LookUp("ACD_PAPERSET_DETAILS P ", "COUNT(SEMESTERNO)", "(DEAN_LOCK = 1 OR APPROVED IS not NULL OR BOS_LOCK = 1) AND BOS_DEPTNO = " + Deptno + " AND CCODE = '" + lblCCode.Text + "' AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SESSIONNO = " + SessionNo);

            if (str == "" || str == "0")
            {
                if (cbchk.Checked)
                {
                    ret = PsatffCont.UpdateCourseBal(SessionNo, Deptno, Convert.ToInt16(ddlSemester.SelectedValue), lblCCode.Text, COLLEGEID);
                }
                else
                {
                    ret = PsatffCont.DeleteCourseBal(SessionNo, Deptno, Convert.ToInt16(ddlSemester.SelectedValue), lblCCode.Text, COLLEGEID);
                }
                if (ret == 99)
                {
                    objCommon.DisplayMessage("Data Not Saved", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updPaperStock, "No further stock request allowed. Entry is locked!", this.Page);
                return;
            }
        }
        objCommon.DisplayMessage(this.updPaperStock, "Paper Set Details Saved!", this.Page);
        BindListView();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlList.Visible = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            if (ddlSession.SelectedIndex > 0)
            {
                if (ddlSemester.SelectedIndex > 0)
                {
                    BindListView();
                }
                else
                {
                    objCommon.DisplayMessage(this.updPaperStock, "Please Select Semester", this.Page);
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    pnlList.Visible = false;

                }
            }
            else
            {
                if (ddlSession.Items.Count == 1)
                    objCommon.DisplayMessage(this.updPaperStock, "Please Select Session and  Semester", this.Page);
                else
                    objCommon.DisplayMessage(this.updPaperStock, "Please Select Session", this.Page);

                lvCourse.DataSource = null;
                lvCourse.DataBind();
                pnlList.Visible = false;

            }
        }
        else
        {
            objCommon.DisplayMessage(this.updPaperStock, "Please Select College & Scheme", this.Page);
        }
        btnShow.Focus();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
       // ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlReport.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlList.Visible = false;
        btnSave.Visible = false;
        btnunlock.Visible = false;
        btnCancel.Visible = false;
        //btnShow.Focus();
    }

    protected void btnunlock_Click(object sender, EventArgs e)
    {
        PStaffController PsatffCont = new PStaffController();
        int COLLEGEID = Convert.ToInt32(ViewState["college_id"]);
        int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
        int a = PsatffCont.UnlockPaperset(Convert.ToInt32(ViewState["cursession"].ToString()), Deptno, Convert.ToInt32(ddlSemester.SelectedValue), COLLEGEID);
        if (a == 1)
        {
            objCommon.DisplayMessage(this.updPaperStock, "Paper Setter Details unlock !!", this.Page);
        }
        else
        {
        }
    }

    #endregion

    #region Report Events
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));
            int clg_id = Convert.ToInt32(ViewState["college_id"]);
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["cursession"]) + ",@P_COLLEGEID=" + clg_id; ;
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["cursession"]) + ",@P_COLLEGEID=" + clg_id;
            }
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPaperStock, updPaperStock.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportNotAllot(string reportTitle, string rptFileName)
    {
        try
        {
            int clg_id = Convert.ToInt32(ViewState["college_id"]);
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                //int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT COLLEGE_ID", "FLOCK = 1 AND IS_ACTIVE = 1 AND SESSIONID= " + ddlSession.SelectedValue));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["cursession"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["cursession"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]);
            }
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPaperStock, updPaperStock.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportNotSet(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ViewState["cursession"].ToString());
            string Script = string.Empty;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPaperStock, updPaperStock.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            DataSet ds = new DataSet();
            PStaffController objPStaff = new PStaffController();
            GridView GVDayWiseAtt = new GridView();
            ds = objPStaff.GetPaperSetterStudCount1(Convert.ToInt32(ViewState["cursession"]), Convert.ToInt32(ViewState["college_id"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Columns.Contains("EXAM"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["EXAM"]);
                }
                if (ds.Tables[0].Columns.Contains("STAFFNO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["STAFFNO"]);
                }
                if (ds.Tables[0].Columns.Contains("DEGREENO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["DEGREENO"]);
                }
                if (ds.Tables[0].Columns.Contains("BRANCHNO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["BRANCHNO"]);
                }
                if (ds.Tables[0].Columns.Contains("SCHEMENO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["SCHEMENO"]);
                }
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=" + rptFileName + ".xls";
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
                objCommon.DisplayMessage(this.updPaperStock, "Data not found", this.Page);
                return;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    public void ShowReportinFormateFacultyNotSet(string exporttype, string rptFileName)
    {
        try
        {
            DataSet ds = new DataSet();
            PStaffController objPStaff = new PStaffController();
            GridView GVDayWiseAtt = new GridView();
            ds = objPStaff.GetPaperSetterNotDone(Convert.ToInt32(ViewState["cursession"]), Convert.ToInt32(ViewState["schemeno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Columns.Contains("SESSIONNO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["SESSIONNO"]);
                }
                if (ds.Tables[0].Columns.Contains("CCODE"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["CCODE"]);
                }
                if (ds.Tables[0].Columns.Contains("SCHEMENO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["SCHEMENO"]);
                }
                if (ds.Tables[0].Columns.Contains("SEMESTERNO"))
                {
                    ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["SEMESTERNO"]);
                }

                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=" + rptFileName + ".xls";
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
                objCommon.DisplayMessage(this.updPaperStock, "Data not found", this.Page);
                return;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        PStaffController objPStaff = new PStaffController();
        try
        {
            if (ddlReport.SelectedValue == "1")
            {
                ds = objPStaff.GetPaperSetterStudCount1(Convert.ToInt32(ViewState["cursession"]), Convert.ToInt32(ViewState["college_id"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReport("Paper_Set_Faculty_List", "TeacherNotAllotedCount.rpt");
                }
                else
                {
                    objCommon.DisplayMessage(this.updPaperStock, "No Record found!", this.Page);
                }
            }
            else if (ddlReport.SelectedValue == "2")
            {
                ds = objPStaff.GetPaperSetterNotDone(Convert.ToInt32(ViewState["cursession"]), Convert.ToInt32(ViewState["schemeno"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ShowReportNotAllot("Paper_Not_Set_Course_List", "rptPaperSetterNotAlloted.rpt");
                }
                else
                {
                    objCommon.DisplayMessage(this.updPaperStock, "No Record found!", this.Page);
                }
            }
            else if (ddlReport.SelectedValue == "3")
            {
                ShowReportinFormate("xls", "TeacherNotAllotedCount.rpt");
            }
            else if (ddlReport.SelectedValue == "4")
            {
                ShowReportinFormateFacultyNotSet("xls", "FacultyNotSet.rpt");
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PaperSetFacultyAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

}
