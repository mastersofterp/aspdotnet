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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic.BusinessLogicLayer.BusinessLogic.Academic;
public partial class ModerationResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }

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
                BindDropDown();
                Page.Title = Session["coll_name"].ToString();
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
                Response.Redirect("~/notauthorized.aspx?page=ModerationResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ModerationResult.aspx");
        }
    }
    private void BindDropDown()
    {
        string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();

        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");

        objCommon.FillDropDownList(ddlAcdBatch, "ACD_ACADEMICBATCH", "ACADEMICBATCHNO", "ACADEMICBATCH", "ACADEMICBATCHNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "ACADEMICBATCHNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
       // objCommon.FillDropDownList(ddlDegreeType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND ISNULL(ACTIVESTATUS,0)=1", "UA_SECTION");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        LstCourse.ClearSelection();
        ddlAcdBatch.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        if (ddlCollege.SelectedIndex > 0)
        {
            PanelList.Visible = false;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_idOVER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idOVER"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            }
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            LstCourse.ClearSelection();
            ddlAcdBatch.SelectedIndex = 0;
            ddlStudentType.SelectedIndex = 0;
        }
    }
    private void BindData()
    {
        string coursno = "";

        foreach (ListItem item in LstCourse.Items)
        {
            if (item.Selected == true)
            {
                coursno += item.Value + '$';
            }

        }

        if (!string.IsNullOrEmpty(coursno))
        {
            coursno = coursno.Substring(0, coursno.Length - 1);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Select Course Name", this.Page);
            return;
        }
        int minmarks = 0;
        int MaxMarks = 0;
        if (txtMinMarks.Text == "")
        {
            minmarks = 0;
        }
        else
        {
            minmarks = Convert.ToInt32(txtMinMarks.Text);
        }
        if (txtMaxMarks.Text == "")
        {
            MaxMarks = 0;
        }
        else
        {
            MaxMarks = Convert.ToInt32(txtMaxMarks.Text);
        }
        
        string SP_Name2 = "PKG_ACD_MODERATION_ANALYSIS_REPORT_RAJAGIRI";
        string SP_Parameters2 = "@P_SCHEMENO,@P_ACADEMICBATCH,@P_COURSENO,@P_COLLEGE_ID,@P_SESSIONNO,@P_STUDENT_TYPE,@P_SEMESTERNO,@P_MIN_MARKS,@P_MAX_MARKS";
        string Call_Values2 = "" + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlAcdBatch.SelectedValue.ToString()) + "," + coursno + "," + Convert.ToInt32(ViewState["college_idOVER"].ToString()) + "," +
                Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudentType.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlSemester.SelectedValue.ToString()) + "," + minmarks + "," + MaxMarks;
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[1].Rows.Count > 0)
        {
            table1.Visible = true;
            lblTotalStudent.Text = dsStudList.Tables[1].Rows[0]["TOTAL_COUNT"].ToString();
            lblPass.Text = dsStudList.Tables[1].Rows[0]["PASSCOUNT"].ToString();
            lblFailed.Text = dsStudList.Tables[1].Rows[0]["FAIL_COUNT"].ToString();
            lblCurrentPassPer.Text = dsStudList.Tables[1].Rows[0]["PASS_PERCENTAGE"].ToString();
            
        }
        if (dsStudList.Tables[2].Rows.Count > 0)
        {
            PanelList.Visible = true;
            LvModerationResult.DataSource = dsStudList.Tables[2];
            LvModerationResult.DataBind();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            LvModerationResult.DataSource = null;
            LvModerationResult.DataBind();
        }
    }
    private void BindModeration()
    {
        string degreename = objCommon.LookUp("ACD_DEGREE", "CODE", "DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]));
        string admbatchname = objCommon.LookUp("ACD_ACADEMICBATCH", "ACADEMICBATCH", "ACADEMICBATCHNO=" + Convert.ToInt32(ddlAcdBatch.SelectedValue));
        string year = objCommon.LookUp("ACD_SEMESTER s inner join ACD_YEAR y on y.YEAR=s.yearno", "YEARNAME", "SEMESTERNO=" +Convert.ToInt32(ddlSemester.SelectedValue));
        string name = year + " " + degreename + "(" + admbatchname + ")" + "Degree Examination -";
        if (ViewState["college_idOVER"] == null)
        {
            ViewState["college_idOVER"] = 0;
        }
        string coursno = "";

        foreach (ListItem item in LstCourse.Items)
        {
            if (item.Selected == true)
            {
                coursno += item.Value + '$';
            }

        }

        if (!string.IsNullOrEmpty(coursno))
        {
            coursno = coursno.Substring(0, coursno.Length - 1);
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Select Course", this.Page);
            return;
        }
        int minmarks = 0;
        int MaxMarks = 0;
        if (txtMinMarks.Text == "")
        {
            minmarks = 0;
        }
        else
        {
            minmarks = Convert.ToInt32(txtMinMarks.Text);
        }
        if (txtMaxMarks.Text == "")
        {
            MaxMarks = 0;
        }
        else
        {
            MaxMarks = Convert.ToInt32(txtMaxMarks.Text);
        }
        
        string SP_Name2 = "PKG_ACD_MODERATION_ANALYSIS_REPORT_RAJAGIRI";
        string SP_Parameters2 = "@P_SCHEMENO,@P_ACADEMICBATCH,@P_COURSENO,@P_COLLEGE_ID,@P_SESSIONNO,@P_STUDENT_TYPE,@P_SEMESTERNO,@P_MIN_MARKS,@P_MAX_MARKS";
        string Call_Values2 = "" + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlAcdBatch.SelectedValue.ToString()) + "," + coursno + "," + Convert.ToInt32(ViewState["college_idOVER"].ToString()) + "," +
                 Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudentType.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlSemester.SelectedValue.ToString()) + "," + minmarks + "," + MaxMarks;
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[2].Rows.Count > 0)
        {
            
            string yourHTMLstring = ""; string passStud = ""; string PassPer = ""; string MARKS = "";
            for (int i = 0; i < dsStudList.Tables[2].Rows.Count; i++)
            {
                passStud += Convert.ToString(dsStudList.Tables[2].Rows[i]["STUD_DIFFERENCE"]) + ',' + ' ';
                PassPer += Convert.ToString(dsStudList.Tables[2].Rows[i]["PASS_PERCENTAGE"]) + ',' + ' ';
                MARKS += Convert.ToString(dsStudList.Tables[2].Rows[i]["RANGE_OF_MARKS"]) + ',' + ' ';
            }
            passStud = passStud.TrimEnd(' ');
            passStud = passStud.TrimEnd(',');
            PassPer = PassPer.TrimEnd(' ');
            PassPer = PassPer.TrimEnd(',');
            MARKS = MARKS.TrimEnd(' ');
            MARKS = MARKS.TrimEnd(',');
            yourHTMLstring = passStud + '#' + PassPer + '#' + MARKS + '#' + name ;
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Popup", "ShowGraph('" + yourHTMLstring + "');", true);
        }
        else
        {
            PanelList.Visible = false;
            objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            LvModerationResult.DataSource = null;
            LvModerationResult.DataBind();
        }
        
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindData();
        BindModeration();
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["GET_DYNAMIC_DATA"]; 
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "GetGraph", "$(document).ready(function () {$('#GetGraph').modal();});", true);
    }
    private void Clear()
    {
        txtMaxMarks.Text = "";
        txtMinMarks.Text = "";
        ddlAcdBatch.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = -1;
        table1.Visible = false;
        PanelList.Visible = false;
        LvModerationResult.DataSource = null;
        LvModerationResult.DataBind();
        LstCourse.ClearSelection();
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillListBox(LstCourse, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON C.COURSENO=O.COURSENO", "C.COURSENO", "C.CCODE +' - '+ C.COURSE_NAME COLLATE DATABASE_DEFAULT AS COURESNAME", "SESSIONNO=" + ddlSession.SelectedValue + "AND O.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND O.SEMESTERNO=" + ddlSemester.SelectedValue, "C.COURSENO");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.SelectedIndex = 0;
        LstCourse.ClearSelection();
        ddlAcdBatch.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
    }
    protected void ddlAcdBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStudentType.SelectedIndex = 0;
    }

}