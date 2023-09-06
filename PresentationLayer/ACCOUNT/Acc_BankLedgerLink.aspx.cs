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
using IITMS.NITPRM;

public partial class ACCOUNT_Acc_BankLedgerLink : System.Web.UI.Page
{
    Common objCommon = new Common();
    CustomStatus CS = new CustomStatus();
    static int bankId = 0;
    Party objParty = new Party();
    PartyController objPartyController = new PartyController();
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
            string TableName = "ACC_" + Session["comp_code"] + "_PARTY";
            objCommon.FillDropDownList(ddlLedgerName, TableName, "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO=2", "PARTY_NO");
            objCommon.FillDropDownList(ddlAccountName, "acc_" + Session["comp_code"] + "_bankac", "ACCNO", "ACCNAME", "", "ACCNAME");
            Clear();
            ViewState["action"] = "add";
        }


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string comp_code = Session["comp_code"].ToString();
            int status = 0;
            objParty.Party_No = Convert.ToInt32(ddlLedgerName.SelectedValue);
            objParty.Bank_Account_No = ddlAccountName.SelectedValue.ToString().Trim();
            if (ViewState["action"].ToString() != null && ViewState["action"].ToString() == "add")
            {
                status = objPartyController.SetBankLedgerLinking(objParty, comp_code);
            }
            if (status == 1)
            {
                objCommon.DisplayUserMessage(updBank, "Bank Linking Successfully Done ", this);

                ViewState["action"] = "add";
                //btnSubmit.Style.Add(HtmlTextWriterStyle.Color, "green");
                Clear();
            }
        }
        catch (Exception Ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Acc_BankMaster.btnSubmit_Click -> " + Ex.Message + " " + Ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    void Clear()
    {
        ddlLedgerName.SelectedValue = "0";
        ddlAccountName.SelectedValue = "0";
    }

    protected void ddlLedgerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAccountName.SelectedValue = Convert.ToString(objCommon.LookUp("ACC_" + Session["comp_code"] + "_PARTY", "BANKACCOUNTNO", "PARTY_NO=" + Convert.ToInt32(ddlLedgerName.SelectedValue)));
    }
}
