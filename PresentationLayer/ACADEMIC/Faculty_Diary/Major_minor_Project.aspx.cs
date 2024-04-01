//=================================================================================
// PROJECT NAME  : RFC Common Code                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : Major_minor_Project.aspx.cs                                          
// CREATION DATE : 05/01/2024                                                
// CREATED BY    : Vipul Tichakule                             
// MODIFIED BY   : GUNESH MOHANE
// MODIFIED DATE : 30/03/2024 
// MODIFIED DESC :  
//=================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;

public partial class ACADEMIC_Major_minor_Project : System.Web.UI.Page
{

    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Session objSession = new Session();
    TeachingPlanController objTeachingPlanController = new TeachingPlanController();

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
                // CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //if (Session["usertype"].ToString() == "3")
                //{
                //        //  ddlSession.Items.Clear();


                // }
                PopulateAcademicYear();
                objCommon.FillDropDownList(ddlsessionNew, "ACD_SESSION_MASTER SM INNER JOIN  ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (SM.SESSIONNO = S.SESSIONNO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SM.SESSION_NAME", "S.SESSIONNO > 0 AND ISNULL(SM.IS_ACTIVE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "S.SESSIONNO DESC");
                objCommon.FillDropDownList(ddlReportSession, "ACD_SESSION_MASTER SM INNER JOIN  ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (SM.SESSIONNO = S.SESSIONNO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT S.SESSIONNO", "SM.SESSION_NAME", "S.SESSIONNO > 0 AND ISNULL(SM.IS_ACTIVE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]), "S.SESSIONNO DESC");

                BindListView();

                divProject.Visible = false;
                divNewProject.Visible = false;
                divNewStage.Visible = false;
                ddlsessionNew.Enabled = true;
                ddlCollegeNew.Enabled = true;
                ddlDegreeNew.Enabled = true;
                ddlBranchNew.Enabled = true;

            }

        }

    }

    protected void PopulateAcademicYear()
    {
        objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "IS_CURRENT_FY = 1", "ACADEMIC_YEAR_ID");
    }

    protected void BindListView()
    {
        DataSet ds = objTeachingPlanController.GetProjectTitleData();
        if (ds != null)
        {
            lvProject.DataSource = ds;
            lvProject.DataBind();

        }
        else
        {
            objCommon.DisplayMessage(this.UpdProjectMs, "Record Not Found", this.Page);
        }
    }
    #endregion

    #region Tab-1 Project Master

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtProject.Text != string.Empty)
            {
                int clgcode = Convert.ToInt32(objCommon.LookUp("Reff", "College_code", "College_code >0"));
                objSession.ProjectName = txtProject.Text;

                if (hfdActive.Value == "true")
                {
                    objSession.IsActive = true;
                }
                else
                {
                    objSession.IsActive = false;
                }
                objSession.Selection = ddlSelection.SelectedValue;
                if (btnSubmit.Text.ToString().Equals("Submit"))
                {
                    CustomStatus cs = (CustomStatus)objTeachingPlanController.InsertPeojectTitle(objSession, clgcode);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record Inserted Successfully", this.Page);
                        BindListView();
                        ClearControl();
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record Not Inserted", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateProjectTitle(objSession, Convert.ToInt32(ViewState["ID"]));
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record update successfully", this.Page);
                        btnSubmit.Text = "Submit";
                        ClearControl();
                        BindListView();
                    }
                    else if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdProjectMs, "Record not update ", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.UpdProjectMs, "Please Enter Name Of Major / Minor Project", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btn_editt_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = int.Parse(btnEdit.CommandArgument);
            ViewState["ID"] = ID;
            ShowDetail(ID);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btn_editt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ShowDetail(int ID)
    {
        try
        {
            SqlDataReader dr = objTeachingPlanController.GetProjectTitleData(ID);
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (dr["PROJECT_TITLE"] == null | dr["PROJECT_TITLE"].ToString().Equals(""))
                        txtProject.Text = string.Empty;
                    else
                        txtProject.Text = dr["PROJECT_TITLE"].ToString();

                    if (dr["IS_ACTIVE"].ToString() == "Active")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                    }

                    ddlSelection.SelectedValue = dr["SELECTION"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ShowDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancell_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    protected void ClearControl()
    {
        txtProject.Text = string.Empty;
        ddlSelection.SelectedValue = "0";
        btnSubmit.Text = "Submit";
    }
    #endregion

    #region Tab-2 Assign

    #region Dropdown Events
    protected void ddlAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_COURSE_TEACHER CT INNER JOIN ACD_SESSION_MASTER C ON (CT.SESSIONNO=C.SESSIONNO) INNER JOIN ACD_ACADEMIC_YEAR AY ON (AY.ACADEMIC_YEAR_NAME = C.ACADEMIC_YEAR)", "DISTINCT C.SESSIONNO", "C.SESSION_NAME", "C.SESSIONNO > 0 AND ISNULL(C.IS_ACTIVE,0)=1 AND CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ACADEMIC_YEAR = '" + ddlAcademicYear.SelectedItem.Text + "'", "C.SESSIONNO DESC");
            ddlSession.Focus();
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
            ddlCollege.ClearSelection();
            ddlDegree.ClearSelection();
            ddlBranch.ClearSelection();
            ddlYear.ClearSelection();
            ddlSemester.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlAcademicYear_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COURSE_TEACHER CT INNER JOIN ACD_COLLEGE_MASTER CM ON (CT.COLLEGE_ID=CM.COLLEGE_ID)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND CT.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "CM.COLLEGE_NAME");
            }
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
            ddlDegree.ClearSelection();
            ddlBranch.ClearSelection();
            ddlYear.ClearSelection();
            ddlSemester.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, " ACD_STUDENT S INNER JOIN  USER_ACC A ON (A.UA_NO = S.FAC_ADVISOR) INNER JOIN ACD_DEGREE D ON (D.DEGREENO=S.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "A.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "D.DEGREENAME");
            }
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
            ddlBranch.ClearSelection();
            ddlYear.ClearSelection();
            ddlSemester.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlCollege_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_STUDENT S INNER JOIN  USER_ACC A ON (A.UA_NO = S.FAC_ADVISOR) INNER JOIN ACD_BRANCH D ON (D.BRANCHNO=S.BRANCHNO)", "DISTINCT D.BRANCHNO", "D.LONGNAME", "A.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "D.LONGNAME");
            }
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
            ddlYear.ClearSelection();
            ddlSemester.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0 AND COLLEGE_CODE = " + clgcode, "YEAR");
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
            ddlSemester.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string clgcode = objCommon.LookUp("Reff", "College_code", "College_code >0");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND COLLEGE_CODE = " + clgcode, "SEMESTERNO");
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlYear_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProject, "ACD_FD_PROJECT_TITLE_MASTER", "ID", "Project_Title", "id > 0 AND IS_ACTIVE = 1", "");
            }
            lvStudent.Visible = false;
            divProject.Visible = false;
            btnShow.Text = "Show";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlSemester_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Button Events
    protected void BindStudentListView()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
            int degree = Convert.ToInt32(ddlDegree.SelectedValue);
            int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

            DataSet ds = objTeachingPlanController.BindStudData(sessionno, collegeid, degree, branchno, year, semesterno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Columns.Contains("PROJECT_ID"))
                {
                    DataTable originalTable = ds.Tables[0];
                    DataView dv = originalTable.DefaultView;
                    dv.Sort = "PROJECT_ID DESC";
                    DataTable sortedTable = dv.ToTable();
                    ds.Tables.Remove(originalTable);
                    ds.Tables.Add(sortedTable);
                }

                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                lvStudent.Visible = true;
                divProject.Visible = true;
                btnShow.Text = "Submit";

                DataSet dsCheck = objCommon.FillDropDown("ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT", "IDNO", "STUDNAME", "IDNO>0", "IDNO");
                Session["check"] = dsCheck;
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                    Label IDno = item.FindControl("lblIDno") as Label;

                    for (int i = 0; i < dsCheck.Tables[0].Rows.Count; i++)
                    {
                        if (IDno.Text == dsCheck.Tables[0].Rows[i]["IDNO"].ToString())
                        {

                            chkstud.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "Record Not Found ", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.BindStudentListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmitt_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnShow.Text.ToString().Equals("Show"))
            {
                BindStudentListView();
            }
            else
            {
                bool msg = false;
                int checkbox = 0;
                bool check = false;
                int result = 0;
                bool Exist = false;

                if (ddlProject.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this.updStudent, "Please Select Project", this.Page);
                    return;
                }
                int Srno = 0;
                string Groupid = objCommon.LookUp("ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT", "MAX(SRNO)", "IDNO>0");

                if (Groupid != "")
                {
                    Srno = Convert.ToInt32(Groupid);
                    Srno = Srno + 1;
                }
                else
                {
                    Srno = 1;
                }
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                    Label StudName = item.FindControl("lblName") as Label;
                    Label Degreeno = item.FindControl("lblDegree") as Label;
                    Label Branchno = item.FindControl("lblBranch") as Label;
                    Label Session = item.FindControl("lblSession") as Label;
                    Label IDno = item.FindControl("lblIDno") as Label;
                    Label AcademicYear = item.FindControl("lblAcademicYear") as Label;
                    Label Semesterno = item.FindControl("lblAcademicYear") as Label;
                    Label Year = item.FindControl("lblYear") as Label;
                    ViewState["checkbox"] = chkstud.Checked;
                    objSession.ProjectName = ddlProject.SelectedItem.Text;
                    objSession.ProjectID = ddlProject.SelectedValue;
                    string stage = ddlStage.SelectedItem.Text;

                    if (chkstud.Checked == false && checkbox == 0)
                    {
                        //objCommon.DisplayMessage(this.updStudent, "Please select checkbox", this.Page);
                        check = true;
                    }
                    else if (chkstud.Checked == true)
                    {
                        checkbox++;
                        check = false;
                        CustomStatus cs = (CustomStatus)objTeachingPlanController.InsertProjectTitleData(objSession, Convert.ToInt32(IDno.Text), Convert.ToInt32(Session.Text), Convert.ToInt32(Degreeno.ToolTip), Convert.ToInt32(Branchno.ToolTip), Srno, stage, Convert.ToInt32(AcademicYear.Text), Convert.ToInt32(Year.Text), Convert.ToInt32(Semesterno.Text));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            msg = true;
                            // objCommon.DisplayMessage(this.updStudent, "Record inserted succesfully", this.Page);
                        }
                        else if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Exist = true;
                        }
                        else
                        {
                            msg = false;
                        }
                    }
                }
                if (check == true)
                {
                    objCommon.DisplayMessage(this.updStudent, "Please Select Atleast One Checkbox", this.Page);
                    return;
                }
                if (msg == true)
                {
                    objCommon.DisplayMessage(this.updStudent, "Record Inserted Successfully", this.Page);
                    foreach (ListViewDataItem item in lvStudent.Items)
                    {
                        CheckBox chkstud = item.FindControl("ChkBox") as CheckBox;
                        if (chkstud.Checked)
                        {
                            chkstud.Checked = false;
                        }
                    }
                    BindStudentListView();
                    // ClearControll();
                }
                else if (Exist == true)
                {
                    objCommon.DisplayMessage(this.UpdProjectMs, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updStudent, "Record Not Inserted", this.Page);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btnSubmitt_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControll();
        lvStudent.Visible = false;
        divProject.Visible = false;
        btnShow.Text = "Show";
    }

    protected void ClearControll()
    {
        ddlAcademicYear.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlProject.SelectedIndex = 0;
        ddlStage.SelectedIndex = 0;

        ddlsessionNew.SelectedIndex = 0;
        ddlCollegeNew.SelectedIndex = 0;
        ddlDegreeNew.SelectedIndex = 0;
        ddlDegreeNew.SelectedIndex = 0;
        ddlBranchNew.SelectedIndex = 0;
        ddlProejctNew.SelectedIndex = 0;
        ddlStageNew.SelectedIndex = 0;
        divStudentName.Visible = false;
        ddlsessionNew.Enabled = true;
        ddlCollegeNew.Enabled = true;
        ddlDegreeNew.Enabled = true;
        ddlBranchNew.Enabled = true;

        ddlReportSession.SelectedIndex = 0;
        ddlReportCollege.SelectedIndex = 0;
        ddlReportDegree.SelectedIndex = 0;
        ddlReportBranch.SelectedIndex = 0;
        ddlReportProject.SelectedIndex = 0;
        ddlReportStage.SelectedIndex = 0;

    }
    #endregion

    #endregion

    #region Tab-3 Edit Assign Project

    #region Dropdown Events
    protected void ddlsessionNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlsessionNew.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlCollegeNew, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (CM.COLLEGE_ID = S.COLLEGE_ID) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.SESSIONNO=" + Convert.ToInt32(ddlsessionNew.SelectedValue), "CM.COLLEGE_NAME");
            }
            lvStudentAssign.Visible = false;
            divNewProject.Visible = false;
            divNewStage.Visible = false;
            ddlDegreeNew.ClearSelection();
            ddlBranchNew.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlsessionNew_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlCollegeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollegeNew.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegreeNew, " ACD_DEGREE D INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (D.DEGREENO= S.DEGREENO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue), "D.DEGREENAME");
            }
            lvStudentAssign.Visible = false;
            divNewProject.Visible = false;
            divNewStage.Visible = false;
            ddlBranchNew.ClearSelection();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlCollegeNew_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegreeNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreeNew.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranchNew, " ACD_BRANCH B INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollegeNew.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlDegreeNew.SelectedValue), "B.LONGNAME");
            }
            lvStudentAssign.Visible = false;
            divNewProject.Visible = false;
            divNewStage.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlDegreeNew_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlBranchNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranchNew.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProejctNew, "ACD_FD_PROJECT_TITLE_MASTER", "ID", "Project_Title", "id > 0", "");
            }
            lvStudentAssign.Visible = false;
            divNewProject.Visible = false;
            divNewStage.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlBranchNew_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Button Events
    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmitNew.Text.ToString().Equals("Show"))
            {

                BindProjectAssignStudentData();

            }
            else if (btnSubmitNew.Text.ToString().Equals("Update"))
            {
                if (ddlProejctNew.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this.updEditAssignStudent, "Please Select Project", this.Page);
                    return;
                }

                int StudentId = Convert.ToInt32(ViewState["IDNO"].ToString());
                objSession.ProjectName = ddlProejctNew.SelectedItem.Text;
                CustomStatus cs = (CustomStatus)objTeachingPlanController.UpdateAssignProjectData(objSession, StudentId, Convert.ToInt32(ddlProejctNew.SelectedValue), ddlStageNew.SelectedValue);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {

                    objCommon.DisplayMessage(this.updEditAssignStudent, "Record Update Successfully", this.Page);
                    BindProjectAssignStudentData();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updEditAssignStudent, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updEditAssignStudent, "Record Not Update", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btnSubmitNew_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void BindProjectAssignStudentData()
    {
        try
        {
            int sessionno = Convert.ToInt32(ddlsessionNew.SelectedValue);
            int collegeid = Convert.ToInt32(ddlCollegeNew.SelectedValue);
            int degree = Convert.ToInt32(ddlDegreeNew.SelectedValue);
            int branchno = Convert.ToInt32(ddlBranchNew.SelectedValue);

            DataSet ds = objTeachingPlanController.BindStudentAssignprojectData(sessionno, collegeid, degree, branchno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudentAssign.DataSource = ds;
                lvStudentAssign.DataBind();
                //divNewProject.Visible = true;
                //btnSubmitNew.Text = "Show";
                //ClearControll();
                lvStudentAssign.Visible = true;
                divNewProject.Visible = true;
                divNewStage.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.updEditAssignStudent, "Record Not Found ", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.BindProjectAssignStudentData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelNew_Click(object sender, EventArgs e)
    {
        lvStudentAssign.Visible = false;
        divNewProject.Visible = false;
        divNewStage.Visible = false;


        btnSubmitNew.Text = "Show";
        ClearControll();
    }

    protected void btn_EditAssignStudent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int studentId = int.Parse(btnEdit.CommandArgument);
            ViewState["IDNO"] = studentId;
            EditProjectAssignStudent(studentId);
            ViewState["action"] = "edit";
            btnSubmitNew.Text = "Update";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btn_EditAssignStudent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void EditProjectAssignStudent(int studentId)
    {
        try
        {
            SqlDataReader dr = objTeachingPlanController.EditAssignDataOfStudent(studentId);
            if (dr != null)
            {
                if (dr.Read())
                {

                    ddlsessionNew.SelectedValue = dr["SESSIONNO"].ToString();
                    ddlCollegeNew.SelectedValue = dr["COLLEGE_ID"].ToString();
                    ddlDegreeNew.SelectedValue = dr["DEGREENO"].ToString();
                    ddlBranchNew.SelectedValue = dr["BRANCHNO"].ToString();
                    ddlProejctNew.SelectedValue = dr["PROJECT_ID"].ToString();
                    if (dr["STAGE"].ToString() != string.Empty)
                        ddlStageNew.SelectedValue = dr["STAGE"].ToString();
                    else
                        ddlStageNew.SelectedIndex = 0;

                    lblStudentName.Text = dr["STUDNAME"].ToString();

                    divStudentName.Visible = true;
                    ddlsessionNew.Enabled = false;
                    ddlCollegeNew.Enabled = false;
                    ddlDegreeNew.Enabled = false;
                    ddlBranchNew.Enabled = false;


                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.EditProjectAssignStudent-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #endregion

    #region Tab-4 Assign Student Report

    #region Dropdown Events
    protected void ddlReportSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlReportCollege, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (CM.COLLEGE_ID = S.COLLEGE_ID) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT CM.COLLEGE_ID", "CM.COLLEGE_NAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.SESSIONNO=" + Convert.ToInt32(ddlReportSession.SelectedValue), "CM.COLLEGE_NAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlReportSession_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlReportCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlReportDegree, " ACD_DEGREE D INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (D.DEGREENO= S.DEGREENO) INNER JOIN  ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlReportCollege.SelectedValue), "D.DEGREENAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlReportCollege_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlReportDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlReportBranch, " ACD_BRANCH B INNER JOIN ACD_MINOR_MAJOR_ALLOT_PROJECT_STUDENT S ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_COURSE_TEACHER CT ON (CT.SESSIONNO= S.SESSIONNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "CT.UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND S.COLLEGE_ID=" + Convert.ToInt32(ddlReportCollege.SelectedValue) + "AND S.DEGREENO=" + Convert.ToInt32(ddlReportDegree.SelectedValue), "B.LONGNAME");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlReportDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlReportBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlReportProject, "ACD_FD_PROJECT_TITLE_MASTER", "ID", "Project_Title", "id > 0", "");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ddlReportBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Button Events
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Records_Of_Major_Minor_Project", "Major_MinorProject_Details.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int clgcode = Convert.ToInt32(objCommon.LookUp("Reff", "College_code", "College_code >0"));
            string title = objCommon.LookUp("ACD_FD_PROJECT_TITLE_MASTER", "PROJECT_TITLE", "id>0");
            int sessionno = Convert.ToInt32(ddlReportSession.SelectedValue);
            int degreeno = Convert.ToInt32(ddlReportDegree.SelectedValue);
            int branchno = Convert.ToInt32(ddlReportBranch.SelectedValue);
            int projectid = Convert.ToInt32(ddlReportProject.SelectedValue);
            string stage = ddlReportStage.SelectedValue;

            DataSet ds = objTeachingPlanController.CheckDatainReport(sessionno, degreeno, branchno, clgcode, projectid, stage);
            if (title != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    url += "&param=@P_COLLEGE_CODE=" + clgcode + ",@P_SESSIONNO=" + sessionno + ",@P_DEGREENO=" + degreeno + ",@P_BRANCHNO=" + branchno + ",@P_PROJECT_ID=" + projectid + ",@P_STAGE=" + stage; // ViewState["college_id"].ToString();
                    //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                    //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                    //divMsg.InnerHtml += " </script>";


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");
                    ScriptManager.RegisterClientScriptBlock(this.updEditAssignStudent, this.updEditAssignStudent.GetType(), "controlJSScript", sb.ToString(), true);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updEditAssignStudent, "Record Not Found ", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.ShowReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btn_DeleteAssignStudent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btn_DeleteAssignStudent = sender as ImageButton;
            int id = int.Parse(btn_DeleteAssignStudent.CommandArgument);
            int result = objTeachingPlanController.DeleteAssignDataOfStudent(id);
            if (result == 3)
                objCommon.DisplayMessage(this.UpdProjectMs, "Record Deleted Successfully", this.Page);

            BindProjectAssignStudentData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Major_minor_Project.btn_DeleteAssignStudent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnReportCancel_Click(object sender, EventArgs e)
    {
        ClearControll();
    }
    #endregion

    #endregion

}