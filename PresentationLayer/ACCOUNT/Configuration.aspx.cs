//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CONFIGURATION SETING
// CREATION DATE : 03-NOVEMBER-2009                                               
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;
using System.Data.SqlClient;

public partial class Configuration : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    string back = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        { back = Request.QueryString["obj"].ToString().Trim(); }
       
        
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            txtconfigdesc.Focus();
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");

                }
                else
                {
                    Session["comp_set"] = "";
                }
                    

                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDown();

            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AccountConfigurationController objMGC = new AccountConfigurationController();
            AccountConfiguration objMainGroup = new AccountConfiguration();
            objMainGroup.ConfiguraionDesc = txtconfigdesc.Text.Trim().ToUpper();
            objMainGroup.ConfigId = Convert.ToInt16(ViewState["id"]);
            objMainGroup.ConfigValue = txtconfigvalue.Text.Trim().ToUpper();
            string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objMGC.AddUpdateConfiguration(objMainGroup,Session["comp_code"].ToString(),Session["fin_yr"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        PopulateDropDown();
                        lblStatus.Text = "Record Saved Successfully!!!";
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        Clear();
                        PopulateDropDown();
                        lblStatus.Text = "Record Updated Successfully!!!";
                    }
                    else
                    {
                        lblStatus.Text = "Server Error!!!";
                    }
                }
                else
                {
                    if (ViewState["id"] != null)
                    {
                        objMainGroup.ConfigId = Convert.ToInt16(ViewState["id"]);

                        CustomStatus cs = (CustomStatus)objMGC.AddUpdateConfiguration(objMainGroup, Session["comp_code"].ToString(), Session["fin_yr"].ToString());
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            PopulateDropDown();
                            lblStatus.Text = "Record Updated Successfully!!!";
                        }
                        else
                            lblStatus.Text = "Server Error!!!";

                    }
                }
            }
            Clear();
            PopulateDropDown();
            txtconfigdesc.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Configuration.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        txtconfigdesc.Enabled = true;
        PopulateDropDown();
        lblStatus.Text = "";
        //Response.Redirect(Request.Url.ToString());
        //txtAccCode.Text="";
        //txtGroupName.Text = "";
        //ddlFAHead.SelectedIndex = 0;
        //ddlParentGroup.SelectedIndex = 0;
        //txtGroupName.Focus();
    }

    protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtconfigdesc.Enabled = false;
            //very important 
            string id = Request.Form[lstGroup.UniqueID].ToString();

            if (id != "" | id != string.Empty)
            {
                Clear();
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                AccountConfigurationController objMGC = new AccountConfigurationController();
                string code_year = Session["comp_code"].ToString() + "_" + Session["fin_yr"].ToString();

                DataTableReader dtr = objMGC.GetConfigurationDetails(Convert.ToInt32(id), code_year);
                if (dtr.Read())
                {
                    txtconfigdesc.Text = dtr["CONFIGDESC"].ToString();
                    txtconfigvalue.Text = dtr["PARAMETER"].ToString();
                                       
                }
                dtr.Close();
                
            }
            else
            {
                ViewState["action"] = "add";
                ViewState["id"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_maingroup.lstGroup_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/Configuration.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }
    

    private void PopulateDropDown()
    {
        try
        {
          DataSet ds=  objCommon.FillDropDown("ACC_REF_CONFIG", "CONFIGID", "CONFIGDESC", string.Empty, "CONFIGID");
          if (ds.Tables.Count > 0)
          {
              if (ds.Tables[0].Rows.Count > 0)
              {
                  lstGroup.DataTextField = "CONFIGDESC";
                  lstGroup.DataValueField = "CONFIGID";
                  lstGroup.DataSource = ds.Tables[0];
                  lstGroup.DataBind();
              }
          }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ConfigurationPage.PopulateDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtconfigdesc.Text = string.Empty;
        txtconfigvalue.Text = string.Empty;
        lstGroup.SelectedIndex = -1;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
        ViewState["id"] = null;
        txtconfigdesc.Focus();
    }
    #endregion

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        DataSet ds = objCommon.FillDropDown("ACC_REF_CONFIG", "CONFIGID", "CONFIGDESC", "CONFIGDESC like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "CONFIGID");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstGroup.DataTextField = "CONFIGDESC";
                lstGroup.DataValueField = "CONFIGID";
                lstGroup.DataSource = ds.Tables[0];
                lstGroup.DataBind();
                
            }
        }

        txtSearch.Focus();


    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(back + ".aspx?obj=config");
    }
}
