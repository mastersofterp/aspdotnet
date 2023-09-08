//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Examination                                                             
// PAGE NAME     : Degree Completion Criteria Configuration
// CREATION DATE : 31-AUGUST-2023
// CREATED BY    : Injamam Ansari
// DISCRIPTION   : Degree Completion Configuration Master Page
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;
using SendGrid;



public partial class Degree_Completion_CriteriaConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController ObjMark = new MarksEntryController();

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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                }
                ViewState["dccc_no"] = 0;
                ViewState["action"] = 0;
                ViewState["schemeno"] = 0;
                this.PopulateListView();
                this.PopulateDropDown();
                hfdadconfig.Value = "false";
            }
        }
    }

    protected void PopulateListView()
    {
        DataSet ds = null;
        try
        {
            ds = ObjMark.GetDegreeCompletionCriteria();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvdegreeconfig.DataSource = ds;
                lvdegreeconfig.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMICDegree_Completion_CriteriaConfig.PopulateListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMICDegree_Completion_CriteriaConfig.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        ddlExemptedSubject.Items.Clear();
        ddlpreSubjects.Items.Clear();
        txtminCredit.Text = string.Empty;
        txtMinGradePoints.Text = string.Empty;
        PopulateListView();
        lvGrade.DataSource = null;
        lvGrade.DataBind();
        lvGrade.Visible = false;
        Div2.Visible = false;
        ViewState["dccc_no"] = 0;
        ViewState["schemeno"] = 0;
        ViewState["action"] = 0;
        ddlScheme.Enabled = true;
        hfdadconfig.Value = "false";
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int dcccno = Convert.ToInt32(ViewState["dccc_no"]);
            int schemeno = Convert.ToInt32(ViewState["schemeno"]);
            decimal min_creadit = Convert.ToDecimal(txtminCredit.Text.ToString());
            string grade = txtMinGradePoints.Text.ToString();
            decimal min_garde = ((grade.ToString() != string.Empty) ? Convert.ToDecimal(grade) : 0);
            string prereqsub = GetSelectedPrereqSub();
            string exemptsub = GetSelectedExemptSub();
            int college_id = Convert.ToInt32(ViewState["college_id"]);
            int adconfig = 0;
            if (rdConfig.Checked == true) { adconfig = 1; }
            if (checksubject() == false)
            {
                return;
            }
            if (ViewState["action"].ToString().Equals("edit"))
            {
                int status = ObjMark.InsertDegreeCriteriaConfig(dcccno, schemeno, min_creadit, min_garde, prereqsub, exemptsub, college_id, adconfig);
                if (status == 1)
                {
                    GradeConfig();
                    objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Criteria is Saved", this.Page);
                    clear();
                }
                else if (status == 2)
                {
                    GradeConfig();
                    objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Criteria is Updated", this.Page);
                    clear();
                }
                else
                { objCommon.DisplayMessage(updDegreeConfig, "Failed To Save Record...", this.Page); }
                PopulateListView();
            }
            else
            {
                int exist = Convert.ToInt32(objCommon.LookUp("ACD_DEGREE_COMPLETION_CRITERIA_CONFIG", "COUNT(SCHEMENO)", "SCHEMENO=" + schemeno));
                if (exist == 0)
                {
                    int status = ObjMark.InsertDegreeCriteriaConfig(dcccno, schemeno, min_creadit, min_garde, prereqsub, exemptsub, college_id, adconfig);
                    if (status == 1)
                    {
                        objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Criteria is Saved", this.Page);
                        clear();
                    }
                    else if (status == 2)
                    {
                        objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Criteria is Updated", this.Page);
                        clear();
                    }
                    else
                    { objCommon.DisplayMessage(updDegreeConfig, "Failed To Save Record...", this.Page); }
                    PopulateListView();
                }
                else
                {
                    objCommon.DisplayMessage(updDegreeConfig, "Selected Scheme Data Already Exists", this.Page);
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMICDegree_Completion_CriteriaConfig.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExemptedSubject.Items.Clear();
        ddlpreSubjects.Items.Clear();
        txtminCredit.Text = string.Empty;
        txtMinGradePoints.Text = string.Empty;
        Div2.Visible = false;
        lvGrade.DataSource = null;
        lvGrade.DataBind();
        lvGrade.Visible = false;
        ViewState["degreeno"] = 0;
        ViewState["branchno"] = 0;
        ViewState["college_id"] = 0;
        ViewState["schemeno"] = 0;
        if (ddlScheme.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlScheme.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            DataSet dsp = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "(CCODE+' - '+COURSE_NAME) AS COURSENAME", "ISNULL(OFFERED,0)=1 AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]), "COURSENO,SEMESTERNO");
            if (dsp.Tables[0].Rows.Count > 0)
            {
                ddlpreSubjects.DataSource = dsp;
                ddlpreSubjects.DataValueField = dsp.Tables[0].Columns[0].ToString();
                ddlpreSubjects.DataTextField = dsp.Tables[0].Columns[1].ToString();
                ddlpreSubjects.DataBind();
            }
            DataSet dse = objCommon.FillDropDown("ACD_COURSE", "COURSENO", "(CCODE+' - '+COURSE_NAME) AS COURSENAME", "ISNULL(OFFERED,0)=1 AND SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]), "COURSENO,SEMESTERNO");
            if (dse.Tables[0].Rows.Count > 0)
            {
                ddlExemptedSubject.DataSource = dse;
                ddlExemptedSubject.DataValueField = dse.Tables[0].Columns[0].ToString();
                ddlExemptedSubject.DataTextField = dse.Tables[0].Columns[1].ToString();
                ddlExemptedSubject.DataBind();
            }
        }

        CheckExisting();
    }

    private string GetSelectedPrereqSub()
    {
        string PrereqSub = string.Empty;
        foreach (ListItem items in ddlpreSubjects.Items)
        {
            if (items.Selected == true)
            {
                PrereqSub += (items.Value).Split('-')[0] + ',';
            }
        }
        if (PrereqSub.Length > 1)
        {
            PrereqSub = PrereqSub.Remove(PrereqSub.Length - 1);
        }
        return PrereqSub;
    }

    private string GetSelectedExemptSub()
    {
        string ExemptSub = string.Empty;
        foreach (ListItem items in ddlExemptedSubject.Items)
        {
            if (items.Selected == true)
            {
                ExemptSub += (items.Value).Split('-')[0] + ',';
            }
        }
        if (ExemptSub.Length > 1)
        {
            ExemptSub = ExemptSub.Remove(ExemptSub.Length - 1);
        }
        return ExemptSub;
    }

    protected void clear()
    {
        ddlScheme.SelectedIndex = 0;
        txtminCredit.Text = string.Empty;
        txtMinGradePoints.Text = string.Empty;
        ddlExemptedSubject.Items.Clear();
        ddlpreSubjects.Items.Clear();
        ViewState["schemeno"] = 0;
        ViewState["dccc_no"] = 0;
        lvGrade.DataSource = null;
        lvGrade.DataBind();
        lvGrade.Visible = false;
        Div2.Visible = false;
        ddlScheme.Enabled = true;
        ViewState["action"] = 0;
        hfdadconfig.Value = "false";
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
    }

    protected void checkedpresub(string presub)
    {
        string[] presubject = presub.Split(',');
        for (int i = 0; i < presubject.Length; i++)
            for (int j = 0; j < ddlpreSubjects.Items.Count; j++)
            {
                if (presubject[i] == ddlpreSubjects.Items[j].Value)
                {
                    ddlpreSubjects.Items[j].Selected = true;
                }
            }
    }

    protected void checkedexempsub(string exemptsub)
    {
        string[] exemptsubject = exemptsub.Split(',');
        for (int i = 0; i < exemptsubject.Length; i++)
            for (int j = 0; j < ddlExemptedSubject.Items.Count; j++)
            {
                if (exemptsubject[i] == ddlExemptedSubject.Items[j].Value)
                {
                    ddlExemptedSubject.Items[j].Selected = true;
                }
            }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        ViewState["dccc_no"] = int.Parse(btnEdit.CommandArgument);
        ViewState["action"] = "edit";
        ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
        int scheme = Convert.ToInt32((lst.FindControl("lblscheme") as Label).ToolTip);
        int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + scheme));
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + scheme));
        string coscno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "SCHEMENO= " + scheme + " AND DEGREENO =" + degreeno + " AND BRANCHNO=" + branchno);
        ddlScheme.SelectedValue = coscno;
        ddlScheme.Enabled = false;
        ddlScheme_SelectedIndexChanged(sender, e);
        txtminCredit.Text = (lst.FindControl("lblmincredit") as Label).Text.ToString();
        txtMinGradePoints.Text = (lst.FindControl("lblmingarde") as Label).Text.ToString();
        string presub = (lst.FindControl("lblpreque") as Label).ToolTip.ToString();
        string exemptsub = (lst.FindControl("lblexmpted") as Label).ToolTip.ToString();
        checkedpresub(presub);
        checkedexempsub(exemptsub);
        int adconfig = Convert.ToInt32((lst.FindControl("lblmincredit") as Label).ToolTip);
        if (adconfig == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
        }
        DataSet ds = ObjMark.GetDegreeCompletionGradeCriteria(Convert.ToInt32(ViewState["dccc_no"]));
        if (ds.Tables[0].Rows.Count > 0)
        {
            Div2.Visible = true;
            lvGrade.DataSource = ds;
            lvGrade.DataBind();
            SetFocus(lvGrade);
            lvGrade.Visible = true;
        }
        else
        {
            lvGrade.DataSource = null;
            lvGrade.DataBind();
            lvGrade.Visible = false;
            Div2.Visible = false;
        }

    }

    protected void GradeConfig()
    {
        try
        {
            if (lvGrade.Visible == true)
            {
                int status = 0;
                int dccc_no = Convert.ToInt32(ViewState["dccc_no"]);
                int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                foreach (ListViewDataItem items in lvGrade.Items)
                {
                    int course_cate_no = Convert.ToInt32((items.FindControl("lblcoursecateno") as Label).ToolTip);
                    string min_grade = (items.FindControl("lblmingarde1") as TextBox).Text;
                    string max_garde = (items.FindControl("lblmaxgarde") as TextBox).Text;
                    if (string.IsNullOrEmpty(min_grade) && string.IsNullOrEmpty(min_grade) || (min_grade == "0" && max_garde == "0")) { }
                    else
                    {
                        status = Convert.ToInt32(ObjMark.InsertDegreeGradeCriteriaConfig(dccc_no, schemeno, course_cate_no, Convert.ToDecimal(min_grade), Convert.ToDecimal(max_garde)));
                    }
                }
                if (status == 1)
                {
                    objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Grade Criteria is Saved", this.Page);
                }
                else if (status == 2)
                {
                    objCommon.DisplayMessage(updDegreeConfig, "Degree Completion Grade Criteria is Updated", this.Page);
                }
                else if (status != 0)
                { objCommon.DisplayMessage(updDegreeConfig, "Failed To Save Grade Criteria...", this.Page); }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMICDegree_Completion_CriteriaConfig.btnsubmit1_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void CheckExisting()
    {

        DataSet existing = objCommon.FillDropDown("ACD_DEGREE_COMPLETION_CRITERIA_CONFIG", "DCCC_NO", "SCHEMENO,MIN_CREDIT,MIN_GRADEPOINT,PREREQ_COURSENO,EXEMPT_COURSENO,ADVANCE_CONFIG", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "");
        if (existing.Tables[0].Rows.Count > 0)
        {
            ViewState["dccc_no"] = Convert.ToInt32(existing.Tables[0].Rows[0]["DCCC_NO"]);
            txtminCredit.Text = existing.Tables[0].Rows[0]["MIN_CREDIT"].ToString();
            txtMinGradePoints.Text = existing.Tables[0].Rows[0]["MIN_GRADEPOINT"].ToString();
            string presub = existing.Tables[0].Rows[0]["PREREQ_COURSENO"].ToString();
            string exemptsub = existing.Tables[0].Rows[0]["EXEMPT_COURSENO"].ToString();
            checkedpresub(presub);
            checkedexempsub(exemptsub);
            ViewState["action"] = "edit";
            int adconfig = Convert.ToInt32(existing.Tables[0].Rows[0]["ADVANCE_CONFIG"]);
            if (adconfig == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
            }
            DataSet ds = ObjMark.GetDegreeCompletionGradeCriteria(Convert.ToInt32(ViewState["dccc_no"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                Div2.Visible = true;
                lvGrade.DataSource = ds;
                lvGrade.DataBind();
                SetFocus(lvGrade);
                lvGrade.Visible = true;
            }
            else
            {
                lvGrade.DataSource = null;
                lvGrade.DataBind();
                lvGrade.Visible = false;
                Div2.Visible = false;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatMandat(false);", true);
        }
    }

    protected bool checksubject()
    {
        bool stat = true;
        string course = "";
        if (rdConfig.Checked == true)
        {
            foreach (ListItem items in ddlpreSubjects.Items)
            {
                if (items.Selected == true)
                {
                    foreach (ListItem items1 in ddlExemptedSubject.Items)
                    {
                        if (items1.Selected == true)
                        {
                            if (items.Value == items1.Value)
                            {
                                course += items.ToString() + " ,";
                                stat = false;
                            }
                        }
                    }
                }
            }
        }
        if (stat == false)
        {
            course = course.Remove(course.Length - 1);
            objCommon.DisplayMessage(updDegreeConfig, @"Same Courses Selected in both list " + course + "", this.Page);
            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "ShowDropDown();", true);
        }
        return stat;
    }

}