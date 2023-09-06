//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :COMPANY ACCOUNT MASTER                                              
// CREATION DATE :22-Nov-2018                                              
// CREATED BY    :Nokhlal Kumar                                        
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

//using IITMS;
using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class ACCOUNT_ACC_Account_Master : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    AccountTransactionController CompAcc = new AccountTransactionController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!Page.IsPostBack)
        {
            ViewState["id"] = "0";
            txtAccountName.Focus();

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    objCommon.DisplayMessage(UPDLedger, "Select company/cash book.", this);
                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {
                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                    PopulateListBox();
                }
            }
        }
    }

    private void PopulateListBox()
    {
        objCommon = new Common();
        try
        {
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_COMP_ACCOUNT_MASTER", "ACC_ID", "UPPER(ACCOUNT_NAME) AS ACCOUNTNAME", "", "ACC_ID");// "PARTY_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstAccountName.Items.Clear();
                    lstAccountName.DataTextField = "ACCOUNTNAME";
                    lstAccountName.DataValueField = "ACC_ID";
                    lstAccountName.DataSource = ds.Tables[0];
                    lstAccountName.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AccountMaster.PopulateListBox()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ACC_Account_Master.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ACC_Account_Master.aspx");
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
                    Response.Redirect("~/notauthorized.aspx?page=ACC_Account_Master.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ACC_Account_Master.aspx");
            }
        }
    }
    protected void lstAccountName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //very important 
            string id = Request.Form[lstAccountName.UniqueID].ToString();

            if (id != "" | id != string.Empty)
            {
                txtAccountName.Text = string.Empty;
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                PartyController objPC = new PartyController();
                string code_year = Session["comp_code"].ToString().Trim();// +"_" + Session["fin_yr"].ToString();

                DataSet dscnt = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_COMP_ACCOUNT_MASTER", "*", "", "ACC_ID = '" + Convert.ToString(id.ToString()).Trim().ToUpper() + "' ", string.Empty);

                if (dscnt != null)
                {
                    if (dscnt.Tables[0].Rows.Count > 0)
                    {
                        //int ACC_Id = 0;
                        txtAccountName.Text = dscnt.Tables[0].Rows[0]["Account_Name"].ToString();
                    }
                }
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
                objUCommon.ShowError(Page, "ACC_Account_Master.lstAccountName_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.NotSaved, Common.MessageType.Error);
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        objCommon = new Common();

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_COMP_ACCOUNT_MASTER", "ACC_ID", "UPPER(ACCOUNT_NAME) AS ACCOUNTNAME", "ACCOUNT_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "ACCOUNT_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstAccountName.DataTextField = "ACCOUNTNAME";
                lstAccountName.DataValueField = "ACC_ID";
                lstAccountName.DataSource = ds.Tables[0];
                lstAccountName.DataBind();

            }
        }

        txtSearch.Focus();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int Acc_id = 0;
        if (txtAccountName.Text == string.Empty || txtAccountName.Text == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter Account Name!", this.Page);
            return;
        }
        string Acc_Name = txtAccountName.Text.Trim().ToString();
        if (ViewState["id"].ToString() == "0")
        {
            Acc_id = 0;
        }
        else
        {
            Acc_id = Convert.ToInt32(ViewState["id"].ToString());
        }

        int res = CompAcc.AddUpdateCompAccountMaster(Acc_id, Acc_Name);
        if (res == 1)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Added successfully", this);
            txtAccountName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ViewState["id"] = "0";
            PopulateListBox();
            txtAccountName.Focus();
            return;
        }
        else if (res == 2)
        {
            objCommon.DisplayMessage(UPDLedger, "Record Updated successfully", this);
            txtAccountName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            ViewState["id"] = "0";
            PopulateListBox();
            txtAccountName.Focus();
            return;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtAccountName.Text = string.Empty;
        txtSearch.Text = string.Empty;
        ViewState["id"] = "0";
        PopulateListBox();
    }
}