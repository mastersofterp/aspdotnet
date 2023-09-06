using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_Degree_Completion_Criteria : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController ObjMark = new MarksEntryController();

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
                this.PopulateDropDown();
            }
        }
    }

    protected void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "", "");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMICDegree_Completion_CriteriaConfig.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlStudentType.SelectedIndex = -1;
        lvstudentlist.DataSource = null;
        lvstudentlist.DataBind();
        lvstudentlist.Visible = false;
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO  DESC");

        }

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlStudentType.SelectedIndex = -1;
        lvstudentlist.DataSource = null;
        lvstudentlist.DataBind();
        lvstudentlist.Visible = false;
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStudentType.SelectedIndex = -1;
        lvstudentlist.DataSource = null;
        lvstudentlist.DataBind();
        lvstudentlist.Visible = false;
    }

    protected void ddlStudentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvstudentlist.DataSource = null;
        lvstudentlist.DataBind();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string proc_ = "PKG_GET_DEGREE_CRITERIA_STUDENT_DATA";
        string para_ = "@P_SESSIONNO,@P_DEGREENO,@P_SCHEMENO,@P_SEMESTERNO,@P_COLLEGEID,@P_STUDENTTYPE";
        string value = Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(ddlStudentType.SelectedValue);
        DataSet ds = null;
        ds = objCommon.DynamicSPCall_Select(proc_, para_, value);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvstudentlist.DataSource = ds;
            lvstudentlist.DataBind();
            lvstudentlist.Visible = true;
        }
        else
        {
            objCommon.DisplayMessage(updstudenteligibility, "No Student Record Found For Given Selection", this.Page);
            lvstudentlist.DataSource = ds;
            lvstudentlist.DataBind();
            lvstudentlist.Visible = false;
        }
    }
    protected void lnkbtncoursecategory_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            LinkButton btnEdit = sender as LinkButton;
            string idno = (btnEdit.CommandArgument).ToString();
            ds = ObjMark.GetStudentEligibilityStatus(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvcoursecatelist.DataSource = ds;
                lvcoursecatelist.DataBind();
                lvcoursecatelist.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updstudenteligibility, "No Student Record Found For Given Selection", this.Page);
                lvcoursecatelist.DataSource = ds;
                lvcoursecatelist.DataBind();
                lvcoursecatelist.Visible = false;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#CouserCategorymodel').modal('show');</script>", false);
            updPopUp.Update();
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlClgname.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        lvstudentlist.DataSource = null;
        lvstudentlist.DataBind();
        lvcoursecatelist.DataSource = null;
        lvcoursecatelist.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            string idno = GetStudents();
            ds = ObjMark.GetStudentEligibilityStatus(idno);
            if (ds.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                objCommon.DisplayMessage(updstudenteligibility, "No Student Record Found For Given Selection", this.Page);
                lvcoursecatelist.DataSource = ds;
                lvcoursecatelist.DataBind();
                lvcoursecatelist.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Degree_Completion_Criteria.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem items in lvstudentlist.Items)
            {
                CheckBox chkBox = items.FindControl("chkbody") as CheckBox;
                if (chkBox.Checked)
                {
                    int idno = Convert.ToInt32((items.FindControl("lblstudentname") as Label).ToolTip);
                    int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                    int degreeno = Convert.ToInt32(ViewState["degreeno"]);
                    int status = Convert.ToInt32(items.FindControl("lbllockstatus") as Label);
                    int lock_status = 1;
                    int ret = ObjMark.LockUnlockDegreeCritera(idno, schemeno, degreeno, status, lock_status);
                    if (ret == 1)
                    {
                        objCommon.DisplayMessage(updstudenteligibility, "Student Degree Completion Eligibility lock Saved", this.Page);
                    }
                    else if (ret == 2)
                    {
                        objCommon.DisplayMessage(updstudenteligibility, "Student Degree Completion Eligibility lock Updated", this.Page);
                    }
                    else
                    { objCommon.DisplayMessage(updstudenteligibility, "Failed To Save Record...", this.Page); }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Degree_Completion_Criteria.btnLock_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem items in lvstudentlist.Items)
            {
                CheckBox chkBox = items.FindControl("chkbody") as CheckBox;
                if (chkBox.Checked)
                {
                    int idno = Convert.ToInt32((items.FindControl("lblstudentname") as Label).ToolTip);
                    int schemeno = Convert.ToInt32(ViewState["schemeno"]);
                    int degreeno = Convert.ToInt32(ViewState["degreeno"]);
                    int status = Convert.ToInt32(items.FindControl("lbllockstatus") as Label);
                    int lock_status = 0;
                    int ret = ObjMark.LockUnlockDegreeCritera(idno, schemeno, degreeno, status, lock_status);
                    if (ret == 1)
                    {
                        objCommon.DisplayMessage(updstudenteligibility, "Student Degree Completion Eligibility lock Saved", this.Page);
                    }
                    else if (ret == 2)
                    {
                        objCommon.DisplayMessage(updstudenteligibility, "Student Degree Completion Eligibility lock Updated", this.Page);
                    }
                    else
                    { objCommon.DisplayMessage(updstudenteligibility, "Failed To Save Record...", this.Page); }
                }
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Degree_Completion_Criteria.btnUnlock_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected string GetStudents()
    {
        string idnos = "";
        try
        {
            foreach (ListViewDataItem items in lvstudentlist.Items)
            {
                CheckBox chk = items.FindControl("chkbody") as CheckBox;
                Label lblStudname = items.FindControl("lblstudentname") as Label;
                if (chk.Checked)
                {
                    if (idnos.Length == 0) idnos = lblStudname.ToolTip.ToString();
                    else
                        idnos += "," + lblStudname.ToolTip.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Degree_Completion_Criteria.GetStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return idnos;
    }
}