//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ACTIVITY MASTER FORM                                                 
// CREATION DATE : 15-JUN-2009                                                          
// CREATED BY    : AMIT YADAV AND SANJAY RATNAPARKHI                                    
// MODIFIED DATE : 07-OCT-2009 (NIRAJ D. PHALKE)                                        
// MODIFIED DESC : PAGELINK, PREREQUISITES, EXAMNO ADDED IN MASTER                      
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Activity_ActivityMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form mode equals to -1(New Mode).
                    ViewState["activityno"] = "0";

                    this.PopulateUserTypes();
                    this.PopulateAcademicPageLinks();
                    this.PopulateActivity();
                    //this.PopulateExam();

                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlExamPattern, "ACD_EXAM_PATTERN WITH (NOLOCK)", "PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0", "PATTERNNO");
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            }

            txtActivityCode.Focus();
            this.LoadActivities();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ActivityMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ActivityMaster.aspx");
        }
    }

    private void LoadActivities()
    {
        try
        {
            ActivityController activityController = new ActivityController();
            DataSet ds = activityController.GetActivities();
            if (ds != null && ds.Tables.Count > 0)
            {
                lvActivities.DataSource = ds;
                lvActivities.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.LoadActivities() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ActivityController activityController = new ActivityController();
            Activity activity = new Activity();
            activity = this.BindData();

            if (activity.UserTypes.Length == 0)
            {
                objCommon.DisplayMessage(this.updActivity, "Please select atleast one user type", this.Page);
                return;
            }

            if (ViewState["activityno"].ToString() != string.Empty && ViewState["activityno"].ToString() == "0")
            {
                CustomStatus cs = (CustomStatus)activityController.AddSessionActivity(activity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updActivity, "Activity Added Successfully!", this.Page);
                    Clear();
                    LoadActivities();
                    PopulateActivity();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayUserMessage(this.updActivity, "Record Already Exists!", this.Page);
                }
                else
                    objCommon.DisplayMessage(this.updActivity, "Error Adding Activity!", this.Page);
            }
            else
            {
                CustomStatus cs = (CustomStatus)activityController.UpdateSessionActivity(activity);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updActivity, "Activity Updated Successfully!", this.Page);
                    Clear();
                    LoadActivities();
                    PopulateActivity();
                }
                else
                    objCommon.DisplayMessage(this.updActivity, "Error Updating Activity!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private Activity BindData()
    {
        Activity activity = new Activity();
        try
        {
            activity.ActivityNo = int.Parse(ViewState["activityno"].ToString());
            activity.ActivityCode = txtActivityCode.Text;
            activity.ActivityName = txtActivity.Text;
            activity.Exam_No = Convert.ToInt32(ddlExamNo.SelectedValue);
            activity.SubExam_No = Convert.ToInt32(ddlSubExamNo.SelectedValue);
            activity.ActivityTemplate = txtActivityTemplate.Text;
            activity.AssignFlag = Convert.ToInt32(ddlassign.SelectedValue);   //Added by Injamam 28-2-23
            foreach (ListItem item in chkListUserTypes.Items)
            {
                if (item.Selected)
                {
                    if (activity.UserTypes.Length > 0)
                        activity.UserTypes += ",";

                    activity.UserTypes += item.Value;
                }
            }

            foreach (ListItem item in chkPageLink.Items)
            {
                if (item.Selected)
                {
                    if (activity.Page_links.Length > 0)
                        activity.Page_links += ",";

                    activity.Page_links += item.Value;
                }
            }

            foreach (ListItem item in chkPreActNo.Items)
            {
                if (item.Selected)
                {
                    if (activity.Prereq_Act_No.Length > 0)
                        activity.Prereq_Act_No += ",";

                    activity.Prereq_Act_No += item.Value;
                }
            }

            activity.Exam_No = Convert.ToInt32(ddlExamNo.SelectedValue);
            //Added By Rishabh On 30/10/2021
            if (hfdActive.Value == "true")
            {
                activity.ActiveStatus = true;
            }
            else
            {
                activity.ActiveStatus = false;
            }
            //
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return activity;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            ActivityController activityController = new ActivityController();
            DataSet ds = activityController.GetActivities(int.Parse(btnEditRecord.CommandArgument));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                ViewState["activityno"] = dr["ACTIVITY_NO"].ToString();
                txtActivityCode.Text = dr["ACTIVITY_CODE"].ToString();
                txtActivity.Text = dr["ACTIVITY_NAME"].ToString();
                string userTypes = dr["UA_TYPE"] == DBNull.Value ? string.Empty : dr["UA_TYPE"].ToString();
                string[] userTypeIds = userTypes.Split(',');
                ddlassign.SelectedValue = dr["ASSIGN_TO"] == DBNull.Value ? "0" : dr["ASSIGN_TO"].ToString();   //Addes by Injamam on 28-2-23
                for (int i = 0; i < userTypeIds.Length; i++)
                {
                    foreach (ListItem item in chkListUserTypes.Items)
                    {
                        if (item.Value == userTypeIds[i])
                            item.Selected = true;
                    }
                }

                string pageLink = dr["PAGE_LINK"] == DBNull.Value ? string.Empty : dr["PAGE_LINK"].ToString();
                string[] pageLinks = pageLink.Split(',');

                for (int i = 0; i < pageLinks.Length; i++)
                {
                    foreach (ListItem item in chkPageLink.Items)
                    {
                        if (item.Value == pageLinks[i])
                            item.Selected = true;
                    }
                }

                string prereqActNo = dr["PREREQ_ACT_NO"] == DBNull.Value ? string.Empty : dr["PREREQ_ACT_NO"].ToString();
                string[] prereqActNos = prereqActNo.Split(',');

                for (int i = 0; i < prereqActNos.Length; i++)
                {
                    foreach (ListItem item in chkPreActNo.Items)
                    {
                        if (item.Value == prereqActNos[i])
                            item.Selected = true;
                    }
                }
                ddlExamPattern.SelectedValue = dr["PATTERNNO"] == DBNull.Value ? "0" : dr["PATTERNNO"].ToString();
                PopulateExam();
                ddlExamNo.SelectedValue = dr["EXAMNO"] == DBNull.Value ? "0" : dr["EXAMNO"].ToString();
                PopulateSubExam();
                ddlSubExamNo.SelectedValue = dr["SUBEXAMNO"] == DBNull.Value ? "0" : dr["SUBEXAMNO"].ToString();
                txtActivityTemplate.Text = dr["ActivityTemplate"].ToString();
                //Added By Rishabh On 30/10/2021
                if (dr["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void PopulateUserTypes()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("USER_RIGHTS WITH (NOLOCK)", "USERTYPEID", "USERDESC", string.Empty, "USERTYPEID");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkListUserTypes.DataTextField = "USERDESC";
                    chkListUserTypes.DataValueField = "USERTYPEID";
                    chkListUserTypes.DataSource = ds.Tables[0];
                    chkListUserTypes.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.PopulateUserTypes --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateAcademicPageLinks()
    {
        try
        {
            DataSet ds = objCommon.GetDropDownData("PKG_SESSION_GET_ACADEMIC_LINKS");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkPageLink.DataTextField = "AL_LINK";
                    chkPageLink.DataValueField = "AL_NO";
                    chkPageLink.DataSource = ds.Tables[0];
                    chkPageLink.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.PopulateAcademicPageLinks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateActivity()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACTIVITY_MASTER WITH (NOLOCK)", "ACTIVITY_NO", "ACTIVITY_NAME", "ACTIVITY_NO>0", "ACTIVITY_NO");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkPreActNo.DataTextField = "ACTIVITY_NAME";
                    chkPreActNo.DataValueField = "ACTIVITY_NO";
                    chkPreActNo.DataSource = ds.Tables[0];
                    chkPreActNo.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Activity_ActivityMaster.PopulateActivity --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulateSubExam()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubExamNo, "ACD_SUBEXAM_NAME", "SUBEXAMNO", "SUBEXAMNAME", "EXAMNO = " + Convert.ToInt32(ddlExamNo.SelectedValue) + " AND SUBEXAMNAME <> ''", "SUBEXAMNO");
            ddlSubExamNo.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Clear()
    {
        ddlExamPattern.SelectedIndex = 0;
        txtActivity.Text = string.Empty;
        txtActivityCode.Text = string.Empty;
        ddlExamNo.SelectedIndex = 0;
        ddlSubExamNo.SelectedIndex = 0;
        txtActivityTemplate.Text = string.Empty;

        foreach (ListItem item in chkListUserTypes.Items)
        {
            item.Selected = false;
        }

        foreach (ListItem item in chkPageLink.Items)
        {
            item.Selected = false;
        }

        foreach (ListItem item in chkPreActNo.Items)
        {
            item.Selected = false;
        }

        ViewState["activityno"] = "0";
        ddlassign.SelectedIndex = 0;
        ddlSubExamNo.Items.Clear();
        ddlSubExamNo.Items.Add(new ListItem("Please Select", "0"));
        ddlExamNo.Items.Clear();
        ddlExamNo.Items.Add(new ListItem("Please Select", "0"));
    }

    protected void ddlExamNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateSubExam();
        //objCommon.FillDropDownList(ddlSubExamNo, "ACD_SUBEXAM_NAME WITH (NOLOCK)", "SUBEXAMNO", "SUBEXAMNAME", "EXAMNO=0 AND ", "SUBEXAMNO");
    }


    private void BindDropDown(DropDownList ddl, DataSet ds)
    {
        ddl.Items.Clear();
        ddl.Items.Add("Please Select");
        ddl.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl.DataSource = ds;
            ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }
    }


    protected void ddlExamPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateExam();
    }

    private void PopulateExam()
    {
        try
        {
            if (ddlExamPattern.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlExamNo, "ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + ddlExamPattern.SelectedValue + "AND EXAMNAME <> ''", "EXAMNO");
                ddlExamNo.Focus();
                ViewState["ExamPattern"] = Convert.ToInt32(ddlExamPattern.SelectedValue);
            }
            else
            {
                ddlSubExamNo.Items.Clear();
                ddlSubExamNo.Items.Add(new ListItem("Please Select", "0"));
                ddlExamNo.Items.Clear();
                ddlExamNo.Items.Add(new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}