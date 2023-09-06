using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ACADEMIC_AcademicRoomMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    User_AccController objUACC = new User_AccController();
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
                //Page Authorization
                this.CheckPageAuthorization();
                bindlist();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDowns();
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DepartmentMapping.aspx");
        }
    }

    private void PopulateDropDowns()
    {
        objCommon.FillDropDownList(ddldept, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNO");
        objCommon.FillDropDownList(ddlClgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SCHEMENO=SC.SCHEMENO)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (SC.DEPTNO IN(" + Session["userdeptno"].ToString() + "))", "COSCHNO");
        DataSet ds = objCommon.FillDropDown("ACD_ACADEMIC_ROOMMASTER", "Roomno", "Roomname", "Roomno>0 AND ISNULL(ACTIVESTATUS,0)=1", "Roomno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lstRoom.DataSource = ds;
            lstRoom.DataTextField = ds.Tables[0].Columns["Roomname"].ToString();
            lstRoom.DataValueField = ds.Tables[0].Columns["Roomno"].ToString();
            lstRoom.DataBind();
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SESSIONNO", typeof(int)));
            dt.Columns.Add(new DataColumn("SCHEMENO", typeof(int)));
            dt.Columns.Add(new DataColumn("SECTIONNO", typeof(int)));
            dt.Columns.Add(new DataColumn("COURSENO", typeof(int)));
            dt.Columns.Add(new DataColumn("ROOMNO", typeof(int)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(int)));
            dt.Columns.Add(new DataColumn("ORGANIZATIONID", typeof(int)));

            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            int deptno = Convert.ToInt32(ddldept.SelectedValue);
            int Batchno = ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0;
            string roomno = string.Empty;
            foreach (ListItem item in lstRoom.Items)
            {
                if (item.Selected == true)
                {
                    //roomno = roomno + item.Value.ToString() + ",";
                    dt.Rows.Add(sessionno, schemeno, sectionno, courseno, Convert.ToInt32(item.Value), Batchno, OrgId);
                }
            }
            //if (roomno != "")
            //{
            //    if (roomno.Substring(roomno.Length - 1) == ",")
            //        roomno = roomno.Substring(0, roomno.Length - 1);
            //}
            //if (roomno == "")
            //{
            //    objCommon.DisplayMessage(this.updAcademroom, "Please Select Atleast one Room", this.Page);
            //}

            int ret = objUACC.Insert_TimeTable_RoomConfig(dt);
            if (ret == 1)
            {
                bindlist();
                objCommon.DisplayMessage(this.updAcademroom, "Time Table Room Mapping Saved Successfully.", this.Page);
                this.clear();
                //bindchkdash();
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(this.updAcademroom, "Record Updated Successfully", this.Page);
                //bindchkdash();
            }
            else
            {
                objCommon.DisplayMessage(this.updAcademroom, "Failed to save the Record", this.Page);
            }

        }
        catch
        {
            throw;
        }
    }

    private void clear()
    {
        ddlClgScheme.SelectedIndex = -1;
        ddlSession.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        lstRoom.SelectedIndex = -1;
        ddlBatch.SelectedIndex = -1;
    }
    private void bindchkdash()
    {

        lstRoom.ClearSelection();
        DataSet ds = objCommon.FillDropDown("acd_department_room", "distinct deptno", "roomno", "deptno=" + ddldept.SelectedValue, "roomno");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string dashboard = ds.Tables[0].Rows[0][1].ToString();
            foreach (string value in dashboard.Split(','))
            {
                int val = Convert.ToInt32(value);
                DataSet ds1 = objCommon.FillDropDown("acd_academic_roommaster", "roomno", "roomname", "roomno=" + value, "roomname");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    int i;
                    int j;
                    for (j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        for (i = 0; i < lstRoom.Items.Count; i++)
                        {
                            if (lstRoom.Items[i].Value == ds1.Tables[0].Rows[j]["roomno"].ToString())
                            {
                                lstRoom.Items[i].Selected = true;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void bindlist()
    {
        DataSet ds = objUACC.getlistrooms();
        ViewState["ds"] = ds;
        this.bindListView();
    }

    private void bindListView()
    {
        DataSet ds=(DataSet)ViewState["ds"]; 
        DataTable dt = new DataTable();
        DataTable dt1 = null;
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            dt = ds.Tables[0];
            var dataRow = dt.AsEnumerable().Where(x => (x.Field<int>("SESSIONNO") == Convert.ToInt32(ddlSession.SelectedValue) || ddlSession.SelectedValue == "0") &&
                                                       (x.Field<int>("SCHEMENO") == Convert.ToInt32(ViewState["schemeno"]) || ddlClgScheme.SelectedValue == "0") &&
                                                       (x.Field<int>("SECTIONNO") == Convert.ToInt32(ddlSection.SelectedValue) || ddlSection.SelectedValue == "0") &&
                                                       (x.Field<int>("COURSENO") == Convert.ToInt32(ddlCourse.SelectedValue) || ddlCourse.SelectedValue == "0") &&
                                                       (x.Field<int>("BATCHNO") == (ddlBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlBatch.SelectedValue) : 0) ||
                                                       ddlBatch.SelectedValue == "0"));
            dt1 = dataRow.Count() > 0 ? dataRow.CopyToDataTable<DataRow>() : null;

        }
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            lvlist.DataSource = dt1;
            lvlist.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvlist);//Set label 
        }
    }
    
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        lvlist.DataSource = null;
        lvlist.DataBind();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindchkdash();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Room", "rptBranchwiseDetails.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["username"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updAcademroom, this.updAcademroom.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch 
        {
            throw;
        }
    }
    protected void ddlClgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSession.SelectedIndex = -1;
            ddlSection.SelectedIndex = -1;
            ddlCourse.SelectedIndex = -1;
            lstRoom.SelectedIndex = -1;
            ddlBatch.SelectedIndex = -1;
            lvlist.DataSource = null;
            lvlist.DataBind();
            if (ddlClgScheme.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgScheme.SelectedValue));
                //ViewState["degreeno"]

                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                }
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COURSE_TEACHER CT WITH (NOLOCK) ON (CT.SESSIONNO=SM.SESSIONNO) ", "DISTINCT SM.SESSIONNO", "SESSION_PNAME", "SM.SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND ISNULL(CT.CANCEL,0)=0 AND SM.COLLEGE_ID=" + ViewState["college_id"].ToString(), "SM.SESSIONNO desc");
                ddlSession.Focus();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSection.SelectedIndex = -1;
            ddlCourse.SelectedIndex = -1;
            lstRoom.SelectedIndex = -1;
            ddlBatch.SelectedIndex = -1;
            lvlist.DataSource = null;
            lvlist.DataBind();
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_SECTION SC WITH (NOLOCK) ON (CT.SECTIONNO=SC.SECTIONNO)", "DISTINCT SC.SECTIONNO", "SECTIONNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(CT.CANCEL,0)=0 AND COLLEGE_ID=" + ViewState["college_id"] + " AND SCHEMENO=" + ViewState["schemeno"], "SC.SECTIONNO");
                ddlSection.Focus();
               this.bindListView();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourse.SelectedIndex = -1;
            lstRoom.SelectedIndex = -1;
            ddlBatch.SelectedIndex = -1;
            lvlist.DataSource = null;
            lvlist.DataBind();

            if (ddlSection.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlBatch, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_BATCH B WITH (NOLOCK) ON (CT.BATCHNO=B.BATCHNO AND B.SECTIONNO=CT.SECTIONNO)", "DISTINCT CT.BATCHNO", "BATCHNAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND CT.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(CT.CANCEL,0)=0 AND B.SECTIONNO=" + ddlSection.SelectedValue, "CT.BATCHNO");
                ddlBatch.Focus();
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (CT.COURSENO=C.COURSENO AND C.SCHEMENO=CT.SCHEMENO)", "DISTINCT C.COURSENO", "(C.CCODE+'-'+COURSE_NAME) COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND C.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(CT.CANCEL,0)=0 AND SECTIONNO=" + ddlSection.SelectedValue, "C.COURSENO");
                //ddlCourse.Focus();
                this.bindListView();
            }
        }
        catch
        {
            throw;
        }

    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstRoom.SelectedIndex = -1;
        lvlist.DataSource = null;
        lvlist.DataBind();
        if (ddlCourse.SelectedIndex > 0)
        {
            this.bindListView();
        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstRoom.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        lvlist.DataSource = null;
        lvlist.DataBind();
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE_TEACHER CT WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON (CT.COURSENO=C.COURSENO AND C.SCHEMENO=CT.SCHEMENO)", "DISTINCT C.COURSENO", "COURSE_NAME", "SESSIONNO=" + ddlSession.SelectedValue + " AND COLLEGE_ID=" + ViewState["college_id"] + " AND C.SCHEMENO=" + ViewState["schemeno"] + " AND ISNULL(CT.CANCEL,0)=0 AND SECTIONNO=" + ddlSection.SelectedValue + " AND (ISNULL(BATCHNO,0)=" + ddlBatch.SelectedValue + " OR " + ddlBatch.SelectedValue + "=0)", "C.COURSENO");
        ddlCourse.Focus();
        this.bindListView();
    }
}