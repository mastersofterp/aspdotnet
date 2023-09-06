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

public partial class Acc_Validations : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    static int AccountId = 0;
    static DataSet ds1 = null;
    static string sessionComp = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
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
                    //CheckPageAuthorization();

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
            // btnSubmit.Text = "Add Account";
            ViewState["action"] = "add";
        }
        //ShowAllAccount();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //AccountId = 0;
        ////txtAccountName.Text = string.Empty;
        //btnSubmit.Text = "Add Account";
        //ViewState["action"] = "add";
        //btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ddlError_Type.SelectedIndex == 1)
        {
            ds = DEBIT_CREDIT_TOTALMISMATCH(Session["comp_code"].ToString());
        }
        else if (ddlError_Type.SelectedIndex == 2)
        {
            ds = PARTY_MISMATCH(Session["comp_code"].ToString());
        }
        else if (ddlError_Type.SelectedIndex == 3)
        {
            ds = OPARTY_MISMATCH(Session["comp_code"].ToString());

        }
        else if (ddlError_Type.SelectedIndex == 4)
        {
            ds = DATE_MISMATCH(Session["comp_code"].ToString());
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            GvError.DataSource = ds.Tables[0];
            GvError.DataBind();
        }
    }
    protected void ClearControl()
    {
        //txtAccountName.Text = string.Empty;
    }
    protected void ImageButtonEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ////btnSubmit.Text = "Update Account";
            ////btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "red");
            //AccountTransactionController objAccount = new AccountTransactionController();
            //ImageButton btnEdit = sender as ImageButton;
            //AccountId = int.Parse(btnEdit.CommandArgument);
            //ViewState["action"] = AccountId;
            //DataSet ds = null;
            //ds = objAccount.GetAccountByAccountNo(AccountId, Session["comp_code"].ToString());
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        //txtAccountName.Text = ds.Tables[0].Rows[0]["Acc_Name"].ToString();
            //        //IsTransfer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsTransferable"]);
            //    }
            //}


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
                    //lvAccount.DataSource = ds;
                    //lvAccount.DataBind();
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



    private DataSet DATE_MISMATCH(string sessionComp)
    {

        AccountTransactionController Atc = new AccountTransactionController();
        DataSet ds1 = new DataSet();
        ds1 = Atc.GetError_TypeWise(sessionComp, 4);
        return ds1;
    }

    private DataSet OPARTY_MISMATCH(string sessionComp)
    {
        AccountTransactionController Atc = new AccountTransactionController();
        DataSet ds1 = new DataSet();
        ds1 = Atc.GetError_TypeWise(sessionComp, 3);
        return ds1;
    }

    private DataSet PARTY_MISMATCH(string sessionComp)
    {
        AccountTransactionController Atc = new AccountTransactionController();
        DataSet ds1 = new DataSet();
        ds1 = Atc.GetError_TypeWise(sessionComp, 2);
        return ds1;
    }

    private DataSet DEBIT_CREDIT_TOTALMISMATCH(string sessionComp)
    {
        AccountTransactionController Atc = new AccountTransactionController();
        DataSet ds1 = new DataSet();
        ds1 = Atc.GetError_TypeWise(sessionComp, 1);
        return ds1;
    }


    public void DisplayMessage(Control UpdatePanelId, string msg, Page pg)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, msg);

        ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", message, false);
    }
}



