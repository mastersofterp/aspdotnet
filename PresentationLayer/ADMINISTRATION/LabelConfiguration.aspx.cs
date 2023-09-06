using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;



public partial class ADMINISTRATION_LabelConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ConfigController objConfig = new ConfigController();
    Config objCon = new Config();

    //USED FOR INITIALSING THE MASTER PAGE
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
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    FillDropDown();
                    BindListView();
                    ViewState["action"] = "add";
                    CheckPageAuthorization();
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ADMINISTRATION_LabelConfiguration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=LabelConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LabelConfiguration.aspx");
        }
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "ActiveStatus=1 AND  OrganizationId="+ Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])+"", "COLLEGE_ID");
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objConfig.GetLabelConfigList(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLabelConfig.DataSource = ds;
                lvLabelConfig.DataBind();
            }
            else
            {
                lvLabelConfig.DataSource = null;
                lvLabelConfig.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_LabelConfiguration.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCon.LabelId = txtlblId.Text.Trim();
            objCon.LabelName = txtlblName.Text.Trim();
            objCon.ColgId = Convert.ToInt32(ddlCollege.SelectedValue);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    objCon.RecId = Convert.ToInt32(ViewState["RecId"]);
                }
                CustomStatus cs = (CustomStatus)objConfig.AddLabelConfig(objCon);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.Page, "Record Updated sucessfully", this.Page);
                    BindListView();
                    ClearAll();
                }
                else if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.Page, "Record Added sucessfully", this.Page);
                    BindListView();
                    ClearAll();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                    BindListView();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ADMINISTRATION_LabelConfiguration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ClearAll()
    {
        txtlblId.Text = string.Empty;
        txtlblName.Text = string.Empty;
        ddlCollege.SelectedIndex = 0;
        ViewState["RecId"] = null;
        BindListView();
        btnSubmit.Text = "Submit";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        btnSubmit.Text = "Update";
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int recno = int.Parse(btnEdit.CommandArgument);
            ViewState["RecId"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(recno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_LabelConfiguration.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowDetails(int recno)
    {
        try
        {
            DataSet ds = null;
            ds = objConfig.GetLabelConfigList(recno);
            if (ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtlblId.Text = ds.Tables[0].Rows[0]["LabelId"] == null ? string.Empty : ds.Tables[0].Rows[0]["LabelId"].ToString();
                    txtlblName.Text = ds.Tables[0].Rows[0]["LabelName"] == null ? string.Empty : ds.Tables[0].Rows[0]["LabelName"].ToString();
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["CollegeId"] == null ? string.Empty : ds.Tables[0].Rows[0]["CollegeId"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ADMINISTRATION_LabelConfiguration.ShowDetails-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
}