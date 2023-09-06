using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ACDAnalyticsDashboardUserConfig : System.Web.UI.Page
{
    Common objCommon = new Common();
    ACDAnalyticsDashboardConfigController objADUC = new ACDAnalyticsDashboardConfigController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            PopulateDropDownList();
            PopulateListBox();
            Session["UaNo"] = null;

        }

    }
    public void PopulateDropDownList()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID<>0", "USERTYPEID");

            ddlUserType.Items.Clear();
            ddlUserType.DataSource = ds;
            ddlUserType.DataValueField = "USERTYPEID";
            ddlUserType.DataTextField = "USERDESC";
            ddlUserType.DataBind();
            ddlUserType.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.BindDDLCountry() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserType.SelectedIndex > 0)
            {
                BindListView();
            }
            else
            {
                pnlDashboard.Visible = false;
                lvDashboard.DataSource = null;
                lvDashboard.DataBind();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.ddlCountry_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void PopulateListBox()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_PBI_CONFIGURATION_WORKSPACE_MASTER", "DISTINCT WORKSPACE_ID", "WORKSPACE_NAME", "WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1", "WORKSPACE_NAME");

            lstModule.Items.Clear();
            lstModule.DataSource = ds;
            lstModule.DataValueField = "WORKSPACE_ID";
            lstModule.DataTextField = "WORKSPACE_NAME";
            lstModule.DataBind();
            lstModule.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.PopulateListBox() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void lstModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstDashboard.Items.Clear();
            if (lstModule.Items.Count > 0)
            {
                BindDashboard();
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "lstModule_SelectedIndexChanged.PopulateListBox() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindDashboard()
    {
        try
        {
            string Module = string.Empty;
            foreach (ListItem item in lstModule.Items)
            {
                if (item.Selected)
                {
                    Module += item.Value + ",";
                }
            }
            Module = Module.Substring(0, (Module.Length - 1));

            DataSet ds = objCommon.FillDropDown("ACD_PBI_CONFIGURATION_SUB_WORKSPACE", "DISTINCT SUB_WORKSPACE_ID", "SUB_WORKSPACE_NAME", "SUB_WORKSPACE_ID>0 AND ISNULL(STATUS,0)=1 AND WORKSPACE_ID IN(" + Module + ")", "SUB_WORKSPACE_NAME");

            lstDashboard.DataSource = ds;
            lstDashboard.DataValueField = "SUB_WORKSPACE_ID";
            lstDashboard.DataTextField = "SUB_WORKSPACE_NAME";
            lstDashboard.DataBind();
            lstDashboard.SelectedIndex = -1;


        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            //ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID INNER JOIN ACD_ANALYTICS_DASHBOARD_USER_CONFIGURATION D ON A.UA_NO = D.UA_NO", "UA_NO,UA_FULLNAME,UA_NAME", "UA_PWD,UA_TYPE,UA_STATUS,USERDESC", "UA_NO IS NOT NULL AND  UA_STATUS = 0 AND  UA_TYPE=" + ddlUserType.SelectedValue, "UA_TYPE,UA_NO");
            ds = objADUC.GetUserList(Convert.ToInt32(ddlUserType.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {


                lvDashboard.DataSource = ds;
                lvDashboard.DataBind();
                foreach (ListViewDataItem item in lvDashboard.Items)
                {
                    Label Status = item.FindControl("lblUStatus") as Label;
                    if (Status.Text.Trim().ToUpper() == "0")
                    {
                        Status.Text = "Active";
                        Status.Style.Add("color", "Green");
                    }
                    if (Status.Text.Trim().ToUpper() == "1")
                    {
                        Status.Text = "Blocked";
                        Status.Style.Add("color", "Red");
                    }
                }
                pnlDashboard.Visible = true;

            }
            else
            {

                pnlDashboard.Visible = false;
                lvDashboard.DataSource = null;
                lvDashboard.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.BindListView() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            divUserName.Style.Add("display", "block");
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objADUC.GetSingleUserInformation(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";

            lstModule.Items.Clear();
            lstDashboard.Items.Clear();
            if (dt.Rows.Count > 0)
            {

                Session["UaNo"] = id;

                txtUserName.Text = dt.Rows[0]["UA_FULLNAME"].ToString() == null ? string.Empty : dt.Rows[0]["UA_FULLNAME"].ToString();
                ddlUserType.SelectedValue = dt.Rows[0]["UA_TYPE"].ToString() == null ? string.Empty : dt.Rows[0]["UA_TYPE"].ToString();
                PopulateListBox();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (ListItem item in lstModule.Items)
                    {
                        if (dt.Rows[i]["MODULENO"].ToString().Equals(item.Value))
                        {
                            item.Selected = true;
                        }

                    }
                }
                BindDashboard();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    foreach (ListItem item in lstDashboard.Items)
                    {
                        if (dt.Rows[j]["DASHBOARDNO"].ToString().Equals(item.Value))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
            {
                string UaNo = "0";
                int UserNo = Convert.ToInt32(Session["userno"]);
                int UaType = Convert.ToInt32(ddlUserType.SelectedValue);
                string Dashboard = string.Empty;
                foreach (ListItem item in lstDashboard.Items)
                {
                    if (item.Selected == true)
                    {
                        Dashboard += item.Value + ",";
                    }
                }
                if (Dashboard.Contains(','))
                {
                    Dashboard = Dashboard.Remove(Dashboard.Length - 1);
                }
                string ret = "";
                int countForCheked = 0;
                string displaymsg = "";
                foreach (ListViewDataItem lv in lvDashboard.Items)
                {
                    CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                    HiddenField hfdUaNo = lv.FindControl("hfdUaNo") as HiddenField;

                    if (chkIsActive.Checked)
                    {
                        countForCheked++;
                    }
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        if (!string.IsNullOrEmpty(Session["UaNo"].ToString()))
                        {
                            UaNo = (Session["UaNo"]).ToString();
                            ret = objADUC.UpdateAnalyticsDashbord(UaNo, UaType, Dashboard, UserNo);
                            displaymsg = "Record updated successfully.";
                        }

                    }
                    else if (ViewState["action"].ToString().Equals("add"))
                    {
                        if (chkIsActive.Checked)
                        {
                            ret = objADUC.InsertAnalyticsDashbord(hfdUaNo.Value.ToString(), UaType, Dashboard, UserNo);
                            displaymsg = "Record added successfully.";
                        }

                    }
                }
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (countForCheked <= 0 )
                    {
                        objCommon.DisplayMessage(this, "Please Select User..!", this.Page);
                        return;
                    }
                }
                objCommon.DisplayMessage(this, displaymsg, this.Page);
                ClearData();
            }
            //else
            //{
            //    Response.Redirect("~/default.aspx");
            //}
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDAnalyticsDashboardUserConfig.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();

    }
    private void ClearData()
    {
        ViewState["action"] = "add";
        //Session["userno"] = null;
        //Session["UaNo"] = null;
        divUserName.Style.Add("display", "none");
        ddlUserType.SelectedIndex = 0;

        lstDashboard.ClearSelection();
        lstModule.ClearSelection();

        pnlDashboard.Visible = false;
        lvDashboard.DataSource = null;
        lvDashboard.DataBind();


    }
}