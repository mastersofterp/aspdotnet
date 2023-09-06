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

public partial class ACADEMIC_SemesterConfigForAuditMode : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSFBC = new StudentRegistration();

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

                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ACTIVESTATUS = 1 AND SEMESTERNO <> 0", "SEMESTERNO");
                    objCommon.FillListBox(lboScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "", "SCHEMENO");

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
            DataSet ds = objSFBC.GetAllSemConfig(0);
            lvSem.DataSource = ds;
            lvSem.DataBind();

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
        int count = 0;
        string Schemeno = string.Empty;
        foreach (ListItem Item in lboScheme.Items)
        {
            if (Item.Selected)
            {
                Schemeno += Item.Value + ",";
                count++;
            }
        }
        Schemeno = Schemeno.Substring(0, Schemeno.Length - 1);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            int id = Convert.ToInt32(ViewState["id"]);

            CustomStatus cs = (CustomStatus)objSFBC.UpdateSemesterconfig(Convert.ToInt32(ddlSemester.SelectedValue.ToString()), Schemeno, id);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updGrade, "Record Updated Successfully !", this.Page);
                ddlSemester.SelectedValue = "0";
                lboScheme.ClearSelection();
                ViewState["action"] = null;
            }
            else
            {
                objCommon.DisplayMessage(updGrade, "Record Already Exists !", this.Page);
                ViewState["action"] = null;
            }
        }
        else
        {
            CustomStatus cs = (CustomStatus)objSFBC.InsertSemesterconfig(Convert.ToInt32(ddlSemester.SelectedValue.ToString()), Schemeno);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updGrade, "Record Saved Successfully !", this.Page);
                ddlSemester.SelectedValue = "0";
                lboScheme.ClearSelection();
            }
            else
            {
                objCommon.DisplayMessage(updGrade, "Record Already Exists !", this.Page);
            }
        }
       

        
        BindListView();
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
            int id = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(id);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PbiConfiguration.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowDetail(int id)
    {
        DataSet ds = objSFBC.GetAllSemConfig(id);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlSemester.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
            lboScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
            
        }
    }
}