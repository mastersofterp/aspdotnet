using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class PAYROLL_MASTERS_ITRuleYear : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    ITRule objIT = new ITRule();


    string UsrStatus = string.Empty;
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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                BindListView();
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
                Response.Redirect("~/notauthorized.aspx?page=Pay_ITRules.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_ITRules.aspx");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        AddUpdate();
    }
    protected void AddUpdate()
    {
        ITRule objIT = new ITRule();
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                objIT.ITRULEID = 0;
                objIT.ITRULENAME = txtrulename.Text.Trim();
                objIT.COLLEGECODE = Session["colcode"].ToString();
                objIT.COLLEGENO = 0;
                if (chkisactive.Checked == true)
                {
                    objIT.IsActive = true;
                }
                else
                {
                    objIT.IsActive = false;
                }
                if (rblschemetype.SelectedValue == "Old Scheme")
                {
                    objIT.SchemeType = 0;
                }
                else if (rblschemetype.SelectedValue == "New Scheme")
                {
                    objIT.SchemeType = 1;
                }
                CustomStatus cs = (CustomStatus)objITMas.InsertITRuleName(objIT);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    BindListView();
                    Clear();
                    objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updpanel, "Record Already Exists", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }

            }
            else
            {
                objIT.ITRULEID = Convert.ToInt32(Session["lblITNO"].ToString().Trim());
                objIT.ITRULENAME = txtrulename.Text.Trim();
                objIT.COLLEGECODE = Session["colcode"].ToString();
                objIT.COLLEGENO = 0;
                if (chkisactive.Checked == true)
                {
                    objIT.IsActive = true;
                }
                else
                {
                    objIT.IsActive = false;
                }
                if (rblschemetype.SelectedValue == "Old Scheme")
                {
                    objIT.SchemeType = 0;
                }
                else if (rblschemetype.SelectedValue == "New Scheme")
                {
                    objIT.SchemeType = 1;
                }
                CustomStatus cs = (CustomStatus)objITMas.UpdateITRuleName(objIT);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    Clear();
                    objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updpanel, "Record Already Exists", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
        }

    }
    private void Clear()
    {
        txtrulename.Text = "";
        ViewState["action"] = "add";
        chkisactive.Checked = false;
        rblschemetype.SelectedIndex = -1;

    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            Session["lblITNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblitrulename = lst.FindControl("lblITRuleName") as Label;
            txtrulename.Text = lblitrulename.Text;
            Label lblisactive = lst.FindControl("lblisactive") as Label;
            if (lblisactive.Text == "IsActive")
            {
                chkisactive.Checked = true;
            }
            else
            {
                chkisactive.Checked = false;
            }

            Label lblscheme = lst.FindControl("lblscheme") as Label;
            if (lblscheme.Text == "Old Scheme")
            {
                rblschemetype.SelectedValue = "Old Scheme";
            }
            else if (lblscheme.Text == "New Scheme")
            {
                rblschemetype.SelectedValue = "New Scheme";
            }

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_ITRuleMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = objITMas.GetITRulesName();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvITRule.DataSource = ds;
                lvITRule.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_ITRuleMaster.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}