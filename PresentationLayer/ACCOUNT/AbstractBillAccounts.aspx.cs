//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : DAY BOOK REPORT                                                    
// CREATION DATE : 24-May-2010                                               
// CREATED BY    : ASHISH THAKRE                                                
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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

public partial class AbstractBillAccounts : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    static int AccountId = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["comp_code"] == null)
        {
            Response.Redirect("~/Account/selectCompany.aspx");
        }
        // To Set the MasterPage
        else if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            //Response.Redirect("Default.aspx");
            Response.Redirect("~/Default.aspx");

        }
        objCommon = new Common();
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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);

                    Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    // Page.Title = Session["coll_name"].ToString();



                    //DataSet ds = new DataSet();

                    //if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
                    //{

                    //    ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
                    //}
                    //else
                    //{
                    //    ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
                    //    DataView dv = ds.Tables[0].DefaultView;
                    //    dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
                    //    ds.Tables.Remove("Table");
                    //    ds.Tables.Add(dv.ToTable());

                    //}
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
                    //}
                    //else
                    //{
                    //    lblHelp.Text = "No Help Present!";

                    //}
                }
            }
            btnSubmit.Text = "Add Account";
            ViewState["action"] = "add";
        }
        ShowAllAccount();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        AccountId = 0;
        txtAccountName.Text = string.Empty;
        btnSubmit.Text = "Add Account";
        ViewState["action"] = "add";
        btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AccountTransactionController objAccount = new AccountTransactionController();
        int status = 0;
        string AccountName = txtAccountName.Text;
        //string database = Session["Database"].ToString();
        string comp_code = Session["comp_code"].ToString();
        try
        {
            int look = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT", "count(ACC_ID)", "Acc_Name='" + AccountName + "'"));

            if (look == 1)
            {
                if (Convert.ToInt32(ViewState["action"]) != Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_ABSTRACT_BILL_ACCOUNT", "ACC_ID", "Acc_Name='" + AccountName + "'")))
                {
                    //txtBankName.Text = string.Empty;
                    objCommon.DisplayUserMessage(updBank, "Account Already Exist...!", this);
                    btnSubmit.Text = "Add Account";
                    ViewState["action"] = "add";
                    //ClearControl();
                    return;
                }
                else
                    look = 0;


            }
            if (look == 0)
            {
                if (ViewState["action"].ToString() != null && ViewState["action"].ToString() != "add")
                {
                    status = objAccount.UpdatAbstractAccount(Convert.ToInt32(ViewState["action"].ToString()), AccountName, comp_code, IsTransfer.Checked);
                     
                }
                else if (ViewState["action"].ToString() != null && ViewState["action"].ToString() == "add")
                {
                    status = objAccount.AddAccountName(AccountName, Session["comp_code"].ToString(),IsTransfer.Checked);
                }

                if (status == 1)
                {
                    objCommon.DisplayUserMessage(updBank, "Account Added Successfully", this);
                    btnSubmit.Text = "Add Account";
                    ViewState["action"] = "add";
                    btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "green");
                    ClearControl();
                }
                if (status == 2)
                {
                    objCommon.DisplayUserMessage(updBank, "Account Updated Successfully", this);
                    btnSubmit.Text = "Add Account";
                    ViewState["action"] = "add";
                    btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "");
                    ClearControl();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AbstractBillAccounts.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
        AccountId = 0;
        ShowAllAccount();
    }
    protected void ClearControl()
    {
        txtAccountName.Text = string.Empty;
    }
    protected void ImageButtonEdit_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update Account";
            btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "red");
            AccountTransactionController objAccount = new AccountTransactionController();
            ImageButton btnEdit = sender as ImageButton;
            AccountId = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = AccountId;
            DataSet ds = null;
            ds = objAccount.GetAccountByAccountNo(AccountId, Session["comp_code"].ToString());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtAccountName.Text = ds.Tables[0].Rows[0]["Acc_Name"].ToString();
                    IsTransfer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTransferable"]);
                }
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AbstractBillAccounts.ImageButtonEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, Common.Message.ExceptionOccured, Common.MessageType.Error);
        }
    }
    private void ShowAllAccount()
    {
        AccountTransactionController objAccount = new AccountTransactionController();
        DataSet ds = null;
        ds = objAccount.GetAbstractBillAccounts(Session["comp_code"].ToString());
        try
        {
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvAccount.DataSource = ds;
                    lvAccount.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AbstractBillAccounts.ShowAllAccount -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    public void DisplayMessage(Control UpdatePanelId, string msg, Page pg)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, msg);

        ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", message, false);
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
}



