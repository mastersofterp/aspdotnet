using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;

public partial class Dashboard_Configuration_Online_Adm : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //ConnectionStrings
    private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                CheckPageAuthorization();
                objCommon.FillDropDownList(ddlusertype, "User_Rights", "USERTYPEID", "USERDESC", "USERTYPEID>0", "USERDESC");

                DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_MASTER_ONLINE_ADM", "ID", "NAME", "ID>0 and status=1", "NAME");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkdashboard.DataSource = ds;
                    chkdashboard.DataTextField = ds.Tables[0].Columns["NAME"].ToString();
                    chkdashboard.DataValueField = ds.Tables[0].Columns["ID"].ToString();
                    chkdashboard.DataBind();
                }
                //ddlusertype.ToolTip = objCommon.LookUp("ACD_DASHBOARD_MASTER","SESSIONNO","status=1");
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
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            User_AccController objUACC = new User_AccController();
            int usertype = Convert.ToInt32(ddlusertype.SelectedValue);
            string dashboard = string.Empty;
            foreach (ListItem item in chkdashboard.Items)
            {
                if (item.Selected == true)
                {
                    dashboard = dashboard + item.Value.ToString() + ",";

                }
            }
            if (dashboard != "")
            {
                if (dashboard.Substring(dashboard.Length - 1) == ",")
                    dashboard = dashboard.Substring(0, dashboard.Length - 1);
            }

            int ret = objUACC.InsertDashboardconfigdetails_Onlineadm(usertype, dashboard);
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.upd1, "Record Inserted Successfully", this.Page);
                bindchkdash();
            }
            else if (ret == 2)
            {
                objCommon.DisplayMessage(this.upd1, "Record Updated Successfully", this.Page);
                bindchkdash();
            }
            else
            {
                objCommon.DisplayMessage(this.upd1, "Failed to save the Record", this.Page);
            }
        }
        catch (Exception)
        {

        }
    }

    private void bindchkdash()
    {

        chkdashboard.ClearSelection();
        DataSet ds = objCommon.FillDropDown("ACD_DASHBOARD_CONFIGURATION_ONLINE_ADM", "USERTYPE", "DASHBOARD", "USERTYPE=" + ddlusertype.SelectedValue, "DASHBOARD");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string dashboard = ds.Tables[0].Rows[0][1].ToString();
            foreach (string value in dashboard.Split(','))
            {
                int val = Convert.ToInt32(value);
                DataSet ds1 = objCommon.FillDropDown("ACD_DASHBOARD_MASTER_ONLINE_ADM", "ID", "NAME", "ID=" + value + " and status=1", "NAME");

                if (ds1.Tables[0].Rows.Count > 0)
                {

                    int i;
                    int j;
                    for (j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        for (i = 0; i < chkdashboard.Items.Count; i++)
                        {
                            if (chkdashboard.Items[i].Value == ds1.Tables[0].Rows[j]["ID"].ToString())
                            {
                                chkdashboard.Items[i].Selected = true;

                                break;
                            }
                        }
                    }
                }
            }

        }

    }
    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindchkdash();
        }
        catch (Exception ex)
        {
        }
    }
}