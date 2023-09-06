//======================================================================================
// PROJECT NAME  : RFC Common code                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Offered Course Session Wise                                    
// CREATION DATE : 26/05/2021
// CREATED BY    : Sneha G                                              
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_OfferedCourseforBacklog : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    CourseController ObjCous = new CourseController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    CheckPageAuthorization();
                    PopulateDropDown();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }
            }
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
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OfferedCourseforBacklog.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OfferedCourseforBacklog.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            this.BindListView();
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            ddlSession.Focus();
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet dsfaculty = null;

            dsfaculty = ObjCous.GetBacklogCourseOfferedSessionwise(Convert.ToInt32(ddlSession.SelectedValue));
            if (dsfaculty != null && dsfaculty.Tables.Count > 0 && dsfaculty.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = dsfaculty;
                lvCourse.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
                pnlCourse.Visible = true;
                btnAd.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                btnAd.Visible = false;
                btnCancel.Visible = false;
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                ddlSession.SelectedIndex = 0;
                objCommon.DisplayMessage(this.updpnl, "Record Not Found!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnAd_Click(object sender, EventArgs e)
    {
        string offcourse = string.Empty;
        string Degree = string.Empty;
        int count = 0;
        try
        {
            foreach (ListViewDataItem dataitem in lvCourse.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkoffered") as CheckBox;
                HiddenField hfcourse = dataitem.FindControl("hf_course") as HiddenField;
                HiddenField hfdegree = dataitem.FindControl("hfdegree") as HiddenField;

                if (chkBox.Checked == true)
                {
                    count++;
                    offcourse += hfcourse.Value + ",";
                    Degree += hfdegree.Value + ",";
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updpnl, "Please Select Atleast one Course form List.", this.Page);
                return;
            }

            if (offcourse != string.Empty)
            {
                CustomStatus cs = (CustomStatus)ObjCous.OfferedBacklogCourses(Convert.ToInt32(ddlSession.SelectedValue), offcourse, Degree);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updpnl, "Offered Courses Saved Successfully.", this.Page);
                    BindListView();
                    return;
                }
                else
                    objCommon.DisplayMessage(this.updpnl, "Error Occurred!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please Offer Atleast one Course.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        btnAd.Visible = false;
    }

    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        lvCourse.DataSource = null;
        lvCourse.DataBind();
    }

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        CheckBox ChkOffer = dataitem.FindControl("chkoffered") as CheckBox;
        HiddenField hf = dataitem.FindControl("hf_offered") as HiddenField;
        if (hf.Value == "1")
        {
            ChkOffer.Checked = true;
            ChkOffer.Enabled = false;
        }
        else
        {
            ChkOffer.Checked = false;
            ChkOffer.Enabled = true;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            this.objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            ddlSession.Focus();
            lvCourse.DataSource = null;
            lvCourse.DataBind();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }
    }
}