﻿//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 07-MAY-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_Report_MedicineIssueReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    objCommon.FillDropDownList(ddlItem, "HEALTH_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NO");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Report_MedicineIssueReport.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtFDate.Text = string.Empty;
        ddlItem.SelectedIndex = 0;
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("MedicineIssueDetails", "rptMedicineIssueDetailsReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Report_MedicineIssueReport.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_ITEMNO=" + Convert.ToInt32(ddlItem.SelectedValue);
         
            if (txtFDate.Text.Trim() != string.Empty)
            {
                url += ",@P_FDATE=" +  Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd");
                //+ Convert.ToDateTime(txtFDate.Text).ToString("yyyy-MM-dd") 
            }
            else
            {
                url += ",@P_FDATE=null";
            }
            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Report_MedicineIssueReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}