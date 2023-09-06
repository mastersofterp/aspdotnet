//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : Account                                             
// CREATION DATE : 10-JUNE-2014                                               
// CREATED BY    : Nitin Meshram                                                 
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

using System.Data.SqlClient;
using IITMS.NITPRM;

public partial class ACCOUNT_PaymentBillFormat : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");

            }
            else
            {

                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {

            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            
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


                

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

               

            }
            CheckPageAuthorization();
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



    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVoucherType.SelectedValue == "1")
        {
            trTDS.Visible = true;
            trRecovered.Visible = false;
            trDeduct.Visible = false;
            trBillAmount.Visible = false;
            trAmountToPaid.Visible = false;
            trAlreadyDeposite.Visible = false;
            

        }

        else if (ddlVoucherType.SelectedValue == "2")
        {
            trTDS.Visible = false;
            trRecovered.Visible = false;
            trDeduct.Visible = true;
            trBillAmount.Visible = false;
            trAmountToPaid.Visible = false;
            trAlreadyDeposite.Visible = false;
        }
        else if (ddlVoucherType.SelectedValue == "3")
        {
            trTDS.Visible = false;
            trRecovered.Visible = true;
            trDeduct.Visible = false;
            trBillAmount.Visible = true;
            trAmountToPaid.Visible = true;
            trAlreadyDeposite.Visible = true;
        }
        else if (ddlVoucherType.SelectedValue == "4")
        {
            trTDS.Visible = false;
            trRecovered.Visible = false;
            trDeduct.Visible = true;
            trBillAmount.Visible = false;
            trAmountToPaid.Visible = false;
            trAlreadyDeposite.Visible = false;
        }
    }
}
