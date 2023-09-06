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
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_EXAMINATION_SemPromotionRules : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RulesCreationController objRCC = new RulesCreationController();

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
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            //Populate the Drop Down Lists
            PopulateDropDown();
            ViewState["ruleid"] = null;
            ViewState["action"] = null;
            BindListView();
            ddlScheme.Enabled = true;
            ddlSemester.Enabled = true;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SemPromotionRules.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SemPromotionRules.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            //FILL DROPDOWN 
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "DISTINCT SCHEMENO", "SCHEMENAME", "SCHEMENO > 0", "SCHEMENO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0 AND SEMESTERNO <> 1", "SEMESTERNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SemPromotionRules.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ShowDetails(int ruleid)
    {
        SqlDataReader dr = null;
        dr = objRCC.GetDetailsForSemPromotionRulesByRuleID(ruleid);

        if (dr != null)
        {
            if (dr.Read())
            {
                ddlScheme.SelectedValue = dr["SCHEMENO"] == DBNull.Value ? "0" : dr["SCHEMENO"].ToString();
                ddlSemester.SelectedValue = dr["TO_ENROLL_FOR_SEM"] == DBNull.Value ? "0" : dr["TO_ENROLL_FOR_SEM"].ToString();
                txtMinEarnedCreditsPer.Text = dr["MIN_EARNED_CREDITS_PER"] == DBNull.Value ? string.Empty : dr["MIN_EARNED_CREDITS_PER"].ToString();
                txtPrevSemCourseCleared.Text = dr["PREV_SEM_COURSE_CLEARED"] == DBNull.Value ? string.Empty : dr["PREV_SEM_COURSE_CLEARED"].ToString();

                ddlScheme.Enabled = false;
                ddlSemester.Enabled = false;
            }
        }
        dr.Close();
    }

    public void BindListView()
    {
        try
        {
            DataSet ds = objRCC.GetDetailsForSemPromotionRules(Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlDetails.Visible = true;
                lvDetails.DataSource = ds;
                lvDetails.DataBind();
            }
            else
            {
                pnlDetails.Visible = false;
                lvDetails.DataSource = null;
                lvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SemPromotionRules.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    public void Clear()
    {
        txtMinEarnedCreditsPer.Text = string.Empty;
        txtPrevSemCourseCleared.Text = string.Empty;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
            int min_earned_credits_per = Convert.ToInt32(txtMinEarnedCreditsPer.Text);
            int prev_sem_course_cleared = Convert.ToInt32(txtPrevSemCourseCleared.Text);


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].Equals("edit"))
                {
                    CustomStatus cs = (CustomStatus)objRCC.UpdSemPromotionRules(Convert.ToInt32(ViewState["ruleid"]), schemeno, semesterno, min_earned_credits_per, prev_sem_course_cleared);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.updSemPromotion, "Exam Promotion Rules Updated Successfully !!!!", this.Page);
                        Clear();
                        BindListView();
                        ddlScheme.Enabled = true;
                        ddlSemester.Enabled = true;
                    }
                    ViewState["action"] = null;
                    ViewState["ruleid"] = null;
                }
            }
            else
            {
                int count = Convert.ToInt32(objCommon.LookUp("ACD_SEM_PROMOTION_RULES_CREATION WITH (NOLOCK)", "COUNT(1)", "SCHEMENO = " + schemeno + " AND TO_ENROLL_FOR_SEM = " + semesterno));
                if (count > 0)
                {
                    objCommon.DisplayMessage(this.updSemPromotion, "Exam Promotion Rule is already defined for this scheme & semester !!!!", this.Page);
                    Clear();
                    BindListView();                  
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objRCC.InsSemPromotionRules(schemeno, semesterno, min_earned_credits_per, prev_sem_course_cleared);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this.updSemPromotion, "Exam Promotion Rules Saved Successfully !!!!", this.Page);
                        Clear();
                        BindListView();
                        ddlScheme.Enabled = true;
                        ddlSemester.Enabled = true;
                        
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SemPromotionRules.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton img = sender as ImageButton;
            int ruleid = Convert.ToInt32(img.CommandArgument);
            ViewState["ruleid"] = ruleid;

            ShowDetails(ruleid);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SemPromotionRules.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
}