using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PBIDashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PBIConfigurationController objPCC = new PBIConfigurationController();

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

                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                PopulateDropDownList();

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
                Response.Redirect("~/notauthorized.aspx?page=PBI_Configuration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PBI_Configuration.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        try
        {

            ////objCommon.FillDropDownList(ddlWorkspace, "ACD_PBI_CONFIGURATION_WORKSPACE_MASTER", "DISTINCT WORKSPACE_ID", "WORKSPACE_NAME", "WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1", "WORKSPACE_NAME");
            objCommon.FillDropDownList(ddlWorkspace, "ACD_ANALYTICS_DASHBOARD_USER_CONFIGURATION AD INNER JOIN ACD_PBI_CONFIGURATION_WORKSPACE_MASTER WM ON AD.MODULENO = WM.WORKSPACE_ID", "DISTINCT WORKSPACE_ID", "WORKSPACE_NAME", "WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1 AND AD.UA_NO = " + Convert.ToInt32(Session["userno"]), "WORKSPACE_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResult.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlWorkspace_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ////objCommon.FillDropDownList(ddlSubWorkspace, "ACD_PBI_CONFIGURATION_SUB_WORKSPACE", "DISTINCT SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", "SUB_WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1 AND WORKSPACE_ID = " + Convert.ToInt32(ddlWorkspace.SelectedValue), "SUB_WORKSPACE_NAME");
            objCommon.FillDropDownList(ddlSubWorkspace, "ACD_ANALYTICS_DASHBOARD_USER_CONFIGURATION AD INNER JOIN ACD_PBI_CONFIGURATION_SUB_WORKSPACE SW ON SW.SUB_WORKSPACE_ID = AD.DASHBOARDNO", "DISTINCT SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", "SUB_WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1 AND MODULENO = " + Convert.ToInt32(ddlWorkspace.SelectedValue) + " AND AD.UA_NO= " + Convert.ToInt32(Session["userno"]), "SUB_WORKSPACE_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResult.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlSubWorkspace_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objPCC.GetPbiDashobardLink(Convert.ToInt32(ddlWorkspace.SelectedValue),Convert.ToInt32(ddlSubWorkspace.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            divdashboard.Visible = true;
            string link = ds.Tables[0].Rows[0]["PBI_LINK_NAME"].ToString();
            this.myIframe.Attributes.Add("src", link);
        }
        else
        {
            divdashboard.Visible = false;
        }

    }
}