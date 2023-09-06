using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_MASTERS_FeedBackMode : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFBC = new StudentFeedBackController();
    static int modeno;

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
                    //Check for Activity On/Off

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    //if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                BindListView();
                ViewState["action"] = "add";
            }

            //divMsg.InnerHtml = string.Empty;
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
        }
        catch { }
    }

    //to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeedBackMode.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeedBackMode.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objSFBC.GetAllFeedbackMode(0);
            lvFeedback.DataSource = ds;
            lvFeedback.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_FeedbackMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objSFBC.InsertStudentFeedBackMode(txtFeedbackMode.Text, Session["colcode"].ToString());
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updGrade, "Record already exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(updGrade, "FeedBack Mode Saved Successfully !", this.Page);
                        txtFeedbackMode.Text = string.Empty;
                        ViewState["action"] = "add";
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGrade, "Something Went Wrong !", this.Page);
                    }
                    BindListView();
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objSFBC.UpdateStudentFeedBackMode(txtFeedbackMode.Text, Session["colcode"].ToString(), modeno);
                    if (cs.Equals(CustomStatus.DuplicateRecord))  
                    {
                        objCommon.DisplayMessage(updGrade, "Record already exist", this.Page); 
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ViewState["action"] = null;
                        btnSave.Text = "Submit";
                        btnSave.CssClass = "btn btn-primary";
                        txtFeedbackMode.Focus();
                        objCommon.DisplayMessage(this.updGrade, "Record Updated Successfully!", this.Page);
                        txtFeedbackMode.Text = string.Empty;
                    }
                    BindListView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Activity_SessionActivityDefinition.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
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
            ImageButton btnEdit = sender as ImageButton;
            modeno = int.Parse(btnEdit.CommandArgument);

            ShowDetail(modeno);
            ViewState["action"] = "edit";

            btnSave.Text = "Update";
            btnSave.CssClass = "btn btn-primary";
            txtFeedbackMode.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_FeedbackMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetail(int feedbackNo)
    {
        DataSet ds = objSFBC.GetAllFeedbackMode(feedbackNo);

        ViewState["modeno"] = feedbackNo.ToString();
        txtFeedbackMode.Text = ds.Tables[0].Rows[0]["FEEDBACK_MODE_NAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["FEEDBACK_MODE_NAME"].ToString();

    }
}