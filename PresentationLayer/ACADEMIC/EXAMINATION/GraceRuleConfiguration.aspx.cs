//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : EXAMINATION
// CREATION DATE : 23-02-2024
// ADDED BY      :                                                  
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :     
//=====================================================================================  
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_Exam_Configue : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamController objEC = new ExamController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Exam objExam = new Exam();
    //protected string a = string.Empty;
    int GraceMarks = 0;
    int MaxCourse = 0;
    int PerCourseMarks = 0;
    int PercentMarks = 0;
    int Status = 0;

    #region
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Check User Authority                     
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlGrace, "ACD_GRACE_CATEGORY", "GRACENO", "GRACE_NAME", "GRACENO> 0", "GRACENO");
                    BindListView();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                }
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Exam_Configue.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Exam_Configue.aspx");
        }
    }
    #endregion

    #region btn events
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        hdfGMaxMarks.Value = "";
        hdfMCourse.Value = "";
        hdfPGMarks.Value = "";
        hdfPTMarks.Value = "";
        hdfActive.Value = "";
        ddlGrace.SelectedIndex = 0;
        txtGMaxMarks.Text = string.Empty;
        txtMCourse.Text = string.Empty;
        txtPGMarks.Text = string.Empty;
        txtPTMarks.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int SrNo = Convert.ToInt32(ViewState["srno"]);
        try
        {
            //validation 
            if (ddlGrace.SelectedIndex > 0)
            {
                

                string GraceType = ddlGrace.SelectedValue;
                decimal GraceMarksdata = Convert.ToDecimal(0);
                if (Convert.ToString(txtGMaxMarks.Text) != "")
                {
                    GraceMarksdata = Convert.ToDecimal(txtGMaxMarks.Text);
                }
                decimal MaxCoursedata = Convert.ToDecimal(0);
                if (Convert.ToString(txtMCourse.Text) != "")
                {
                    MaxCoursedata = Convert.ToDecimal(txtMCourse.Text);
                }
                decimal PerCourseMarksdata = Convert.ToDecimal(0);
                if (Convert.ToString(txtPGMarks.Text) != "")
                {
                    PerCourseMarksdata = Convert.ToDecimal(txtPGMarks.Text);
                }
                decimal PercentMarksdata = Convert.ToDecimal(0);
                if (Convert.ToString(txtPTMarks.Text) != "")
                {
                    PercentMarksdata = Convert.ToDecimal(txtPTMarks.Text);
                }

                if (GraceMarksdata < PerCourseMarksdata)
                {
                    objCommon.DisplayMessage(this.Page, "Invalid Per Course Grace Marks", this.Page);
                }

                //for Checkbox
                
                if (hdfGMaxMarks.Value == "true")
                {
                    GraceMarks = 1;
                }
                if (hdfMCourse.Value == "true")
                {
                    MaxCourse = 1;
                }
                if (hdfPGMarks.Value == "true")
                {
                    PerCourseMarks = 1;
                }
                if (hdfPTMarks.Value == "true")
                {
                    PercentMarks = 1;
                }
                if (hdfActive.Value == "true")
                {
                    Status = 1;
                }

                //duplicate record
                string ifexists = objCommon.LookUp("ACD_EXAM_GRACE_CONFIGURATION", "COUNT(1)", "GRACE_CATEGORY=" + ddlGrace.SelectedValue);
                if (ifexists == "1" && SrNo == 0)
                {
                    objCommon.DisplayMessage(this.Page, "Record already exists", this.Page);
                    clear();
                    return;
                }
                CustomStatus cs = (CustomStatus)objEC.Add_ExamGraceRule(SrNo, GraceType, GraceMarksdata, GraceMarks, MaxCourse, MaxCoursedata, PerCourseMarks, PerCourseMarksdata, PercentMarks, PercentMarksdata, Status);
                
                //Insert
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["action"] = "add";
                    clear();
                    objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this.Page);
                    BindListView();
                }
                string input1 = txtGMaxMarks.Text.Trim();
                bool isValid1 = true;
                foreach (char c in input1)
                {
                    if (!char.IsDigit(c) && c != '.')
                    {
                        isValid1 = false;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(input1) || !isValid1)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Only Numbers.", this.Page);
                    txtGMaxMarks.Focus();
                    ShowAllDetail(SrNo);
                }
                string input2 = txtMCourse.Text.Trim();
                bool isValid2 = true;
                foreach (char c in input2)
                {
                    if (!char.IsDigit(c) && c != '.')
                    {
                        isValid2 = false;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(input2) || !isValid2)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Only Numbers.", this.Page);
                    txtMCourse.Focus();
                    ShowAllDetail(SrNo);
                }
                string input3 = txtPGMarks.Text.Trim();
                bool isValid3 = true;
                foreach (char c in input3)
                {
                    if (!char.IsDigit(c) && c != '.')
                    {
                        isValid3 = false;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(input3) || !isValid3)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Only Numbers.", this.Page);
                    txtPGMarks.Focus();
                    ShowAllDetail(SrNo);
                }
                string input4 = txtPTMarks.Text.Trim();
                bool isValid4 = true;

                foreach (char c in input4)
                {
                    if (!char.IsDigit(c) && c != '.')
                    {
                        isValid4 = false;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(input4) || !isValid4)
                {
                    objCommon.DisplayMessage(this.Page, "Please Enter Only Numbers.", this.Page);
                    txtPTMarks.Focus();
                    ShowAllDetail(SrNo);
                }
                //update
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    if (hdfGMaxMarks.Value == "true")
                    {
                        if (txtGMaxMarks.Text == "0.00" || txtGMaxMarks.Text == "" || txtGMaxMarks.Text == "0")
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Grace Max Marks", this.Page);
                            txtGMaxMarks.Focus();
                        }
                        else if (GraceMarksdata < 0 || GraceMarksdata > 10)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Grace Max Marks", this.Page);
                            txtGMaxMarks.Focus();
                        }
                    }
                    else
                    {
                        if (hdfGMaxMarks.Value == "false")
                        {
                            if (txtGMaxMarks.Text != "0.00")
                            {
                                objCommon.DisplayMessage(this.Page, "Please Select Checkbox", this.Page);
                                ShowAllDetail(SrNo);
                                txtGMaxMarks.Focus();
                                //return;
                            }
                        }
                    }
                    if (hdfMCourse.Value == "true")
                    {
                        if (txtMCourse.Text == "0.00" || txtMCourse.Text == "" || txtMCourse.Text == "0")
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Max Course", this.Page);
                            txtMCourse.Focus();
                        }
                        else if (MaxCoursedata < 0 || MaxCoursedata > 10)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Max Course", this.Page);
                            txtMCourse.Focus();
                        }
                    }
                    else
                    {
                        if (hdfMCourse.Value == "false")
                        {
                            if (txtMCourse.Text != "0.00")
                            {
                                objCommon.DisplayMessage(this.Page, "Please Select Checkbox", this.Page);
                                ShowAllDetail(SrNo);
                                txtMCourse.Focus();
                               // return;
                            }
                        }
                    }
                    if (hdfPGMarks.Value == "true")
                    {
                        if (txtPGMarks.Text == "0.00" || txtPGMarks.Text == "" || txtPGMarks.Text == "0")
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Per Course Grace Marks", this.Page);
                            txtPGMarks.Focus();
                        }
                        else if (PerCourseMarksdata < 0 || PerCourseMarksdata > 10)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Per Course Grace Marks ", this.Page);
                            txtGMaxMarks.Focus();
                        }
                    }
                    else
                    {
                        if (hdfPGMarks.Value == "false")
                        {
                            if (txtPGMarks.Text != "0.00")
                            {
                                objCommon.DisplayMessage(this.Page, "Please Select Checkbox", this.Page);
                                ShowAllDetail(SrNo);
                                txtPGMarks.Focus();
                                // return;
                            }
                        }
                    }
                    if (hdfPTMarks.Value == "true")
                    {
                        if (txtPTMarks.Text == "0.00" || txtPTMarks.Text == "" || txtPTMarks.Text == "0")
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Percentage of total Max Marks", this.Page);
                            txtPTMarks.Focus();
                        }
                        else if (PercentMarksdata < 0 || PercentMarksdata > 10)
                        {
                            objCommon.DisplayMessage(this.Page, "Please Enter Valid Percentage of total Max Marks", this.Page);
                            txtPTMarks.Focus();
                        }
                    }
                    else
                    {
                        if (hdfPTMarks.Value == "false")
                        {
                            if (txtPTMarks.Text != "0.00")
                            {
                                objCommon.DisplayMessage(this.Page, "Please Select Checkbox", this.Page);
                                ShowAllDetail(SrNo);
                                txtPTMarks.Focus();
                                //return;
                            }
                        }
                    }
                    ShowAllDetail(SrNo);
                    objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this.Page);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                    BindListView();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Something went wrong", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select Grace", this.Page);
            }
        }
        catch (Exception ex)
        {
        }
        objCommon.DisplayMessage(this.Page, "Please Enter Valid Marks", this.Page);
        ShowAllDetail(SrNo);
    }
    protected void ddlGrace_SelectedIndexChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ViewState["srno"] = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int SrNo = int.Parse(btnEdit.CommandArgument);
            ShowAllDetail(SrNo);
            ViewState["action"] = "edit";
            ViewState["srno"] = SrNo;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowAllDetail(int SrNo)
    {
        ExamController objEC = new ExamController();
        SqlDataReader dr = objEC.GetAllGraceData(SrNo);
        if (dr != null)
        {
            if (dr.Read())
            {
                ddlGrace.Text = dr["GRACE_CATEGORY"] == null ? string.Empty : dr["GRACE_CATEGORY"].ToString();
                txtGMaxMarks.Text = dr["GRACE_MARKS_DATA"] == null ? string.Empty : dr["GRACE_MARKS_DATA"].ToString();
                txtMCourse.Text = dr["MAX_GRACE_MARKS_DATA"] == null ? string.Empty : dr["MAX_GRACE_MARKS_DATA"].ToString();
                txtPGMarks.Text = dr["PER_COURSE_GRACE_MARKS_DATA"] == null ? string.Empty : dr["PER_COURSE_GRACE_MARKS_DATA"].ToString();
                txtPTMarks.Text = dr["PERCENT_OF_MAX_MARKS_DATA"] == null ? string.Empty : dr["PERCENT_OF_MAX_MARKS_DATA"].ToString();

                //set checkbox 
                if (dr["GRACE_MARKS"].ToString().Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "GraceMarks", "setCheckbox('chk_GMaxMarks', true);", true);
                    GraceMarks = 1;
                    hdfGMaxMarks.Value = "true";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "GraceMarks", "setCheckbox('chk_GMaxMarks', false);", true);
                    GraceMarks = 0;
                    if (hdfGMaxMarks.Value == "false")
                    {
                        txtGMaxMarks.Text = "0.00";
                    }
                }

                if (dr["MAX_GRACE_MARKS"].ToString().Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MaxGraceMarks", "setCheckbox('chk_MCourse', true);", true);
                    MaxCourse = 1;
                    hdfMCourse.Value = "true";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MaxGraceMarks", "setCheckbox('chk_MCourse', false);", true);
                    MaxCourse = 0;
                    if (hdfMCourse.Value == "false")
                    {
                        txtMCourse.Text = "0.00";
                    }
                }
                if (dr["PER_COURSE_GRACE_MARKS"].ToString().Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PerCourseGraceMarks", "setCheckbox('chk_PGMarks', true);", true);
                    PerCourseMarks = 1;
                    hdfPGMarks.Value = "true";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PerCourseGraceMarks", "setCheckbox('chk_PGMarks', false);", true);
                    PerCourseMarks = 0;
                    if (hdfPGMarks.Value == "false")
                    {
                        txtPGMarks.Text = "0.00";
                    }
                }
                if (dr["PERCENT_OF_MAX_MARKS"].ToString().Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PercentOfMaxMarks", "setCheckbox('chk_PTMarks', true);", true);
                    PercentMarks = 1;
                    hdfPTMarks.Value = "true";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "PercentOfMaxMarks", "setCheckbox('chk_PTMarks', false);", true);
                    PercentMarks = 0;
                    if (hdfPTMarks.Value == "false")
                    {
                        txtPTMarks.Text = "0.00";
                    }
                }
                if (dr["STATUS"].ToString() == "Active" || dr["STATUS"].ToString() == "ACTIVE")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Active(true);", true);
                    Status = 1;
                    hdfActive.Value = "true";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Active(false);", true);
                    Status = 0;
                    hdfActive.Value = "false";
                }
            }
            if (dr != null) dr.Close();
        }
    }
    #endregion

    #region listview
    protected void BindListView()
    {
        DataSet dslist = null;
        dslist = objEC.Get_Grace_Data();
        if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
        {
            lvGraceRule.Visible = true;
            lvGraceRule.DataSource = dslist;
            lvGraceRule.DataBind();
        }
        else
        {
            lvGraceRule.Visible = false;
            lvGraceRule.DataSource = null;
            lvGraceRule.DataBind();
        }
    }
    #endregion
}

