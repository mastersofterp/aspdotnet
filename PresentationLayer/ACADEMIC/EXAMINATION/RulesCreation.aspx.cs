//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Rules Creation                                 
// CREATION DATE : 08-04-2018
// CREATED BY    : M. REHBAR SHEIKH                                   
// MODIFIED DATE : 04-09-2018
// MODIFIED BY   : M. REHBAR SHEIKH
// MODIFIED DESC : ADDED YEAR PATTERN RULES                                                   
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_RulesCreation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RulesCreationController objRC = new RulesCreationController();
    RulesCreation objR = new RulesCreation();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                this.PopulateDropDownList();
                //GetNextRuleId();
               // BindListView();
            }
            ViewState["action"] = null;
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 ", "C.COLLEGE_ID"); //AND CD.UGPGOT IN (" + Session["ua_section"] + ")
            // objCommon.FillDropDownList(ddlYearCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            //ddlSession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
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
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

   

    //private void GetNextRuleId()
    //{
    //    try
    //    {
    //        //txtRuleID.Text = objCommon.LookUp("ACD_RULES_CREATION", "ISNULL(MAX(RULE_ID)+1,100)", string.Empty);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_RulesCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {          
            if (ddlColg.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(upddetails, "Please Select College.", this.Page);
                return;
            }

            if (ddlDegree.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(upddetails, "Please Select Degree.", this.Page);
                return;
            }

            if (ddlBranch.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(upddetails, "Please Select Branch.", this.Page);
                return;
            }

            if (ddlScheme.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(upddetails, "Please Select Scheme.", this.Page);
                return;
            }

            if (txtSpanPeriod.Text == string.Empty)
            {
                objCommon.DisplayMessage(upddetails, "Please Enter Span Period.", this.Page);
                return;
            }

            objR.college_Id = Convert.ToInt32(ddlColg.SelectedValue);
            objR.degreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objR.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objR.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            //objR.academicPattern = Convert.ToInt32(ddlCoursePattern.SelectedValue);
            objR.academicPattern = 1;
            objR.durationRegular = txtDurationRegular.Text != string.Empty ? Convert.ToInt32(txtDurationRegular.Text.Trim()) : Convert.ToInt32(-100);
            objR.spanPeriod = txtSpanPeriod.Text != string.Empty ? Convert.ToInt32(txtSpanPeriod.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem1_Sem2 = txtSEM1_SEM2.Text != string.Empty ? Convert.ToInt32(txtSEM1_SEM2.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem2_Sem3 = txtSEM2_SEM3.Text != string.Empty ? Convert.ToInt32(txtSEM2_SEM3.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem3_Sem4 = txtSEM3_SEM4.Text != string.Empty ? Convert.ToInt32(txtSEM3_SEM4.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem4_Sem5 = txtSEM4_SEM5.Text != string.Empty ? Convert.ToInt32(txtSEM4_SEM5.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem5_Sem6 = txtSEM5_SEM6.Text != string.Empty ? Convert.ToInt32(txtSEM5_SEM6.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem6_Sem7 = txtSEM6_SEM7.Text != string.Empty ? Convert.ToInt32(txtSEM6_SEM7.Text.Trim()) : Convert.ToInt32(-100);
            objR.Backlog_Sem7_Sem8 = txtSEM7_SEM8.Text != string.Empty ? Convert.ToInt32(txtSEM7_SEM8.Text.Trim()) : Convert.ToInt32(-100);
            objR.remark = txtRemark.Text;
            objR.IPaddress = Session["ipAddress"].ToString();
            objR.UA_NO = Convert.ToInt32(Session["userno"]);
            int ret = 0;
            //Check for add or edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit 
                int RuleNo = Convert.ToInt32(ViewState["RuleNo"]);
                ret = objRC.InsertRules(objR, RuleNo);
                if (ret != 0 && ret != -99 && ret == 2)
                {
                    objCommon.DisplayMessage(upddetails,"Rules Updated Successfully.", this.Page);
                    BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
                    ClearForSaveUp();
                   // BindListView();
                }
            }
            else
            {
                ret = objRC.InsertRules(objR, 0);
                if (ret != 0 && ret != -99 && ret == 1)
                {
                    objCommon.DisplayMessage(upddetails,"Rules Successfully Created.", this.Page);
                    BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
                    ClearForSaveUp();
                    //BindListView();
                }
            }
            // }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RulesCreation.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void clear()
    {
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        txtDurationRegular.Text = string.Empty;
        txtSpanPeriod.Text = string.Empty;
        txtSEM1_SEM2.Text = string.Empty;
        txtSEM2_SEM3.Text = string.Empty;
        txtSEM3_SEM4.Text = string.Empty;
        txtSEM4_SEM5.Text = string.Empty;
        txtSEM5_SEM6.Text = string.Empty;
        txtSEM6_SEM7.Text = string.Empty;
        txtSEM7_SEM8.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["RuleNo"] = null;
       // BindListView();
        //  this.GetNextRuleId();

    }

    private void ClearForSaveUp()
    {
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        txtDurationRegular.Text = string.Empty;
        txtSpanPeriod.Text = string.Empty;
        txtSEM1_SEM2.Text = string.Empty;
        txtSEM2_SEM3.Text = string.Empty;
        txtSEM3_SEM4.Text = string.Empty;
        txtSEM4_SEM5.Text = string.Empty;
        txtSEM5_SEM6.Text = string.Empty;
        txtSEM6_SEM7.Text = string.Empty;
        txtSEM7_SEM8.Text = string.Empty;
        txtRemark.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["RuleNo"] = null;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
        lvSemProRules.DataSource = null;
        lvSemProRules.DataBind();
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlColg.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", " D.DEGREENAME", " CD.COLLEGE_ID=" + ddlColg.SelectedValue + " ", "D.DEGREENO"); //AND CDB.UGPGOT IN (" + Session["ua_section"] + ")
                ddlDegree.Focus();
                BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
            }
            else
            {
                lvSemProRules.DataSource = null;
                lvSemProRules.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
                ddlBranch.Focus();
                BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
                //BindListViewByDegree(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue));
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
                lvSemProRules.DataSource = null;
                lvSemProRules.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAM_RULESCREATION.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + "", "SCHEMENO");
            ddlScheme.Focus();
            BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));

            if (lvSemProRules.Items.Count == 0)
            {
                lvSemProRules.DataSource = null;
                lvSemProRules.DataBind();
            }
        }
        else
        {
            lvSemProRules.DataSource = null;
            lvSemProRules.DataBind();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {         
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(DURATION,0) AS DURATION", "COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)));
            txtDurationRegular.Text = duration.ToString();

            BindListViewByFilters(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
        }
        else
        {
            lvSemProRules.DataSource = null;
            lvSemProRules.DataBind();
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objRC.GetAllSemPromotionRules();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSemProRules.DataSource = ds;
                lvSemProRules.DataBind();
            }
            else
            {
                lvSemProRules.DataSource = null;
                lvSemProRules.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Exam.BindListViewByDegreeBranch-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewByFilters(int college_id, int degreeno, int branchno, int schemeno)
    {
        try
        {          
            DataSet ds = new DataSet();
           
                ds = objRC.GetAllSemPromotionRulesByFilters(college_id, degreeno, branchno, schemeno);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvSemProRules.DataSource = ds;
                    lvSemProRules.DataBind();
                    
                }
                else
                {
                    lvSemProRules.DataSource = null;
                    lvSemProRules.DataBind();
                }
            }           
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Exam.BindListViewByDegreeBranch-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewByDegree(int college_id, int degreeno)
    {
        try
        {          
            DataSet ds = new DataSet();
          
                ds = objRC.GetAllSemPromotionRulesByDegree(college_id, degreeno);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvSemProRules.DataSource = ds;
                    lvSemProRules.DataBind();
                }
                else
                {
                    lvSemProRules.DataSource = null;
                    lvSemProRules.DataBind();
                }
                     
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Exam.BindListViewByDegreeBranch-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btneditSW = sender as ImageButton;
        int ruleid = int.Parse(btneditSW.CommandArgument);
        ViewState["action"] = "edit";
        ViewState["RuleNo"] = int.Parse(btneditSW.CommandArgument);
        this.ShowDetails(ruleid);
    }
    private void ShowDetails(int RuleId)
    {
        try
        {
            SqlDataReader dr = objRC.GetRuleDetailsToEdit(RuleId);
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (Convert.ToInt32(dr["COLLEGE_ID"]) == 0)
                    {
                        ddlColg.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlColg.SelectedValue = dr["COLLEGE_ID"].ToString();
                    }

                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", " D.DEGREENAME", " CD.COLLEGE_ID=" + ddlColg.SelectedValue + "AND CDB.UGPGOT IN (" + Session["ua_section"] + ")", "D.DEGREENO");
                    if (Convert.ToInt32(dr["DEGREENO"]) == 0)
                    {
                        ddlDegree.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlDegree.SelectedValue = dr["DEGREENO"].ToString();
                    }

                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
                    if (Convert.ToInt32(dr["BRANCHNO"]) == 0)
                    {
                        ddlBranch.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlBranch.SelectedValue = dr["BRANCHNO"].ToString();
                    }
                    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + "", "SCHEMENO");
                    if (Convert.ToInt32(dr["SCHEMENO"]) == 0)
                    {
                        ddlScheme.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlScheme.SelectedValue = dr["SCHEMENO"].ToString();
                    }

                    //if (Convert.ToInt32(dr["ACADEMIC_PATTERN"]) == 0)
                    //{
                    //    ddlCoursePattern.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlCoursePattern.SelectedValue = dr["ACADEMIC_PATTERN"].ToString();
                    //}

                    if (dr["DURATION_REGULAR"] == "" || dr["DURATION_REGULAR"] == "0")
                    {
                        txtDurationRegular.Text = "0";
                    }
                    else
                    {
                        txtDurationRegular.Text = dr["DURATION_REGULAR"].ToString();
                    }

                    if (dr["SPAN_PERIOD"] == "")
                    {
                        txtSpanPeriod.Text = "0";
                    }
                    else
                    {
                        txtSpanPeriod.Text = dr["SPAN_PERIOD"].ToString();
                    }

                    txtSEM1_SEM2.Text = dr["BACKLOG_SEM1_SEM2"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM1_SEM2"])).ToString();
                    txtSEM2_SEM3.Text = dr["BACKLOG_SEM2_SEM3"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM2_SEM3"])).ToString();
                    txtSEM3_SEM4.Text = dr["BACKLOG_SEM3_SEM4"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM3_SEM4"])).ToString();
                    txtSEM4_SEM5.Text = dr["BACKLOG_SEM4_SEM5"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM4_SEM5"])).ToString();
                    txtSEM5_SEM6.Text = dr["BACKLOG_SEM5_SEM6"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM5_SEM6"])).ToString();
                    txtSEM6_SEM7.Text = dr["BACKLOG_SEM6_SEM7"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM6_SEM7"])).ToString();
                    txtSEM7_SEM8.Text = dr["BACKLOG_SEM7_SEM8"] == DBNull.Value ? string.Empty : (Convert.ToInt32(dr["BACKLOG_SEM7_SEM8"])).ToString();
                    txtRemark.Text = dr["REMARKS"].ToString();

                    //if (dr["BACKLOG_SEM7_SEM8"] == "" || dr["BACKLOG_SEM7_SEM8"] == "0")
                    //{
                    //    txtSEM7_SEM8.Text = "0";
                    //}
                    //else
                    //{
                    //    txtSEM7_SEM8.Text = dr["BACKLOG_SEM7_SEM8"].ToString();
                    //}                   
                }
            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}