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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Text;
using System.Data.OleDb;
using System.Data.Common;

public partial class ACADEMIC_FacultyWorkLoad : System.Web.UI.Page
{

    #region Page Events

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                    CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();


                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    this.PopulateDropDown();

                    //Add by Rita M.. 
                    //hide the validation and star mark for admin.......
                    if (Session["usertype"].ToString().Equals("1"))
                    {
                        //For Reqired field
                        rfvcollege.Visible = false;
                        rfvSemester1.Visible = false;
                        rfvSubjectType1.Visible = false;
                        //rfvToDate.Visible = false;
                        //rfvFromDate.Visible = false;
                        //For Span star.................
                        clgspan.Visible = false;
                        //todtspan.Visible = false;
                        //frmdtspan.Visible = false;
                        subspan.Visible = false;
                        semspan.Visible = false;
                    }
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

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
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TeachingPlan.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=TeachingPlan.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING CSM INNER JOIN ACD_COURSE_TEACHER CT ON(CSM.COLLEGE_ID = CT.COLLEGE_ID AND CSM.SCHEMENO = CT.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND (UA_NO=" + Convert.ToInt32(Session["userno"]) + "OR ADTEACHER = " + Convert.ToInt32(Session["userno"]) + ")", "CSM.COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0", "COLLEGE_ID");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN ACD_COURSE_TEACHER CT ON(SM.SESSIONNO=CT.SESSIONNO)", "DISTINCT SM.SESSIONNO", "SM.SESSION_PNAME", "SM.SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND CT.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND CT.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SM.SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SEMESTER SEM ON (SEM.SEMESTERNO = CT.SEMESTERNO)", "DISTINCT CT.SEMESTERNO", "SEM.SEMESTERNAME", "CT.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "CT.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            ddlSubjectType.Focus();
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SUBJECTTYPE S ON (CT.SUBID = S.SUBID)", "DISTINCT CT.SUBID", "S.SUBNAME", "CT.SESSIONNO=" + ddlSession.SelectedValue + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND CT.SEMESTERNO = " + ddlSemester.SelectedValue, "CT.SUBID");
        }
        else
        {
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedIndex > 0)
        {
            txtFromDate.Focus();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("FacultyWorkLoad", "rptFacultyWorkLoadReport.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //int ua_no = 0;
            //if (Session["usertype"].ToString() != "1")
            //{
            //    ua_no = Convert.ToInt32(Session["userno"]);
            //}
            //else
            //{
            //    ua_no = Convert.ToInt32(ddlTeacher.SelectedValue);
            //}
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() //+ ",@P_UA_NO=" + ua_no
                    + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                //+ ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue)
                      + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue)
                      + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"])
                      + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"])
                      + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue)
                      + ",@P_FROMDATE=" + txtFromDate.Text.Trim()
                      + ",@P_TODATE=" + txtToDate.Text.Trim();

            //+ ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
            //+ ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTech, this.updTech.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
    }

    private void ClearFields()
    {
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
}