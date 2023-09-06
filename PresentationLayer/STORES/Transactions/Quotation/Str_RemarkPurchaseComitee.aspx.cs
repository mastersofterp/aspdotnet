
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_RemarkPurchaseComitee.aspx                                      
// CREATION DATE : 31-Aug-2015                                                    
// CREATED BY    : Nitin Meshram
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

public partial class STORES_Transactions_Quotation_Str_RemarkPurchaseComitee : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    PurchaseComiteeController objPurchase = new PurchaseComiteeController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        ViewState["action"] = "add";


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillQuotNo();
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    private void FillQuotNo()
    {
        objCommon.FillDropDownList(ddlQno, "STORE_QUOTENTRY a inner join STORE_PURCHASE_COMITEE b on (a.QNO=b.QNO)", "distinct a.QNO", "a.QUOTNO", "a.ISLOCK=1 and b.uno=" + Session["userno"].ToString(), "a.QNO");
        if (ddlQno.Items.Count <= 1)
            objCommon.DisplayUserMessage(updpnlMain, "You are not a member of any Purchase Comitee", this.Page);
    }

    protected void btnShowComp_Click(object sender, EventArgs e)
    {
        string Script = string.Empty;

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
        url += "Reports/STORES/ComparativeReport.aspx?";

        url += "quotno=" + ddlQno.SelectedItem.Text;
        Script += " window.open('" + url + "','Comparative Statement','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.updpnlMain, updpnlMain.GetType(), "Report", Script, true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = objPurchase.InsertremarkpurchaseComitee(Convert.ToInt32(ddlQno.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), txtRemark.Text.Trim());
            if (ret > 0)
            {
                objCommon.DisplayUserMessage(updpnlMain, "Your Remark Saved Successfully", this.Page);
                //ddlQno.SelectedValue = "0";
                txtRemark.Text = string.Empty;
                fillRemark();
            }
            else
            {
                objCommon.DisplayUserMessage(updpnlMain, "Transaction Failed", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlQno.SelectedValue = "0";
        txtRemark.Text = string.Empty;
        grdComiteeRemark.DataSource = null;
        grdComiteeRemark.DataBind();
    }
    protected void ddlQno_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillRemark();
    }

    private void fillRemark()
    {
        DataSet ds = objCommon.FillDropDown("STORE_PURCHASE_COMITEE a inner join User_Acc b on (a.UNO=b.UA_NO)", "b.UA_FULLNAME", "isnull(a.REMARK,'') REMARK", "QNO=" + ddlQno.SelectedValue, "b.UA_TYPE,REMARK desc");
        grdComiteeRemark.DataSource = ds;
        grdComiteeRemark.DataBind();
    }
}