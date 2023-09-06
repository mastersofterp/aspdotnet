using BusinessLogicLayer.BusinessLogic.PostAdmission;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_POSTADMISSION_ACDStateMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    ACDStateMasterController objSMC = new ACDStateMasterController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            BindDDLNationality_Country();
            BindDDLCountry_State();
            BindListView_Country();
            BindListView_State();
        }
    }


    #region Tab-1 Country
    public void BindDDLNationality_Country()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_NATIONALITY", "NATIONALITYNO", "NATIONALITY", "NATIONALITYNO > 0 AND ISNULL(ACTIVESTATUS, 0) = 1", "NATIONALITY");

            ddlNationality_Country.Items.Clear();
            ddlNationality_Country.DataSource = ds;
            ddlNationality_Country.DataValueField = "NATIONALITYNO";
            ddlNationality_Country.DataTextField = "NATIONALITY";
            ddlNationality_Country.DataBind();
            ddlNationality_Country.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDStateMaster.BindDDLNationality_Country() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlNationality_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlNationality_Country.SelectedIndex > 0)
            {
                BindListView_Country();
            }
            else
            {
                pnlCountry.Visible = false;
                lvCountry.DataSource = null;
                lvCountry.DataBind();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDStateMaster.ddlCountry_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindListView_Country()
    {
       
        try
        {
            DataSet ds = objSMC.GetCountryDataList(Convert.ToInt32(ddlNationality_Country.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlCountry.Visible = true;
                lvCountry.DataSource = ds;
                lvCountry.DataBind();

            }
            else
            {

                pnlCountry.Visible = false;
                lvCountry.DataSource = null;
                lvCountry.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvCountry.Items)
            {
                Label Status = dataitem.FindControl("lblStatus_Country") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDStateMaster.BindListView_Country() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Country_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit_Country = sender as ImageButton;
            DataTable dt;

            string id = btnEdit_Country.CommandArgument.ToString();
            dt = objSMC.GetSingleCountryInformation(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                Session["CountryNo"] = id;
                txtCountryName.Text = dt.Rows[0]["COUNTRYNAME"].ToString();
                ddlNationality_Country.SelectedValue = (dt.Rows[0]["NATIONALITYNO"]).ToString();

                if (dt.Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Set_ActiveStatus_Country(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Set_ActiveStatus_Country(false);", true);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDStateMaster.btnEdit_Country_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmitCountry_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int CountryNo = 0;
            string CountryName = txtCountryName.Text.Trim();
            int NationalityNo = Convert.ToInt32(ddlNationality_Country.SelectedValue);
            int ActiveStatus;
            if (hfd_ActiveStatus.Value == "true")
            {
                ActiveStatus = 1;
            }
            else
            {
                ActiveStatus = 0;
            }

            string College_id = Session["colcode"].ToString();

            int ret = 0;
            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["CountryNo"].ToString()))
                {
                    CountryNo = Convert.ToInt32(Session["CountryNo"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objSMC.InsertUpdateCountry(CountryNo, CountryName, NationalityNo, ActiveStatus, College_id));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearData_Country();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
        }
        else
            Response.Redirect("~/default.aspx");

    }

    protected void btnCancelCountry_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        ClearData_Country();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_1');</script>", false);
    }

    protected void ClearData_Country()
    {   
        ViewState["action"] = "add";
        txtCountryName.Text = string.Empty;
        ddlNationality_Country.SelectedIndex = 0;
        BindListView_Country();
    }

    #endregion

    #region Tab-2 State
    public void BindDDLCountry_State()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0 AND ISNULL(ACTIVE_STATUS, 0) = 1", "COUNTRYNAME");

            ddlCountry_State.Items.Clear();
            ddlCountry_State.DataSource = ds;
            ddlCountry_State.DataValueField = "COUNTRYNO";
            ddlCountry_State.DataTextField = "COUNTRYNAME";
            ddlCountry_State.DataBind();
            ddlCountry_State.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDStateMaster.BindDDLCountry_State() --> " + ex.Message + " " + ex.StackTrace);
        }
        
    }

    protected void ddlCountry_State_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry_State.SelectedIndex > 0)
            {
                BindListView_State();
            }
            else
            {
                pnlState.Visible = false;
                lvState.DataSource = null;
                lvState.DataBind();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "ACDStateMaster.ddlCountry_State_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);

    }

    protected void btnEditState_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit_State = sender as ImageButton;
            DataTable dt;

            string id = btnEdit_State.CommandArgument.ToString();
            dt = objSMC.GetSingleStateInformation(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                Session["StateNo"] = id;
                txtStateName.Text = dt.Rows[0]["STATENAME"].ToString();
                ddlCountry_State.SelectedValue = (dt.Rows[0]["COUNTRYNO"]).ToString();

                if (dt.Rows[0]["ACTIVESTATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(false);", true);
                }
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDStateMaster.btnEditState_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_State()
    {
        try
        {

            DataSet ds = objSMC.GetStateDataList(Convert.ToInt32(ddlCountry_State.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlState.Visible = true;
                lvState.DataSource = ds;
                lvState.DataBind();

            }
            else
            {

                pnlState.Visible = false;
                lvState.DataSource = null;
                lvState.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvState.Items)
            {
                Label Status = dataitem.FindControl("lblStatus_State") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACDStateMaster.BindListView_State() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmitState_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int StateNo = 0;
            string StateName = txtStateName.Text.Trim();
            int CountryNo = Convert.ToInt32(ddlCountry_State.SelectedValue);
            bool Active = hfdActive.Value == "true" ? true : false;


            string College_id = Session["colcode"].ToString();

            int ret = 0;
            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["StateNo"].ToString()))
                {
                    StateNo = Convert.ToInt32(Session["StateNo"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objSMC.InsertUpdateState(CountryNo, StateNo, StateName, Active, College_id));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearData_State();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
        }
        else
            Response.Redirect("~/default.aspx");
    }

    protected void btnCancelState_Click(object sender, EventArgs e)
    {
        Session["StateNo"] = "";
        ViewState["action"] = "add";
        ClearData_State();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }

    private void ClearData_State()
    {
        ViewState["action"] = "add";
        Session["StateNo"] = "";

        ddlCountry_State.SelectedIndex = 0;
        txtStateName.Text = string.Empty;
        BindListView_State();
    }

    #endregion    
}












