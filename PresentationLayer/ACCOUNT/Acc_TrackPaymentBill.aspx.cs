//=================================================================================
// PROJECT NAME  :UAIMS                                                  
// MODULE NAME   :Track PaymentBill                                                   
// CREATION DATE :30-SEP-2020                                              
// CREATED BY    :VIJAY ANDOJU                                       
// MODIFIED BY   :
// MODIFIED DESC :
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Xml;
using System.Web.Services;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class ACCOUNT_Acc_TrackPaymentBill : System.Web.UI.Page
{

    Common objCommon = new Common();
    RaisingPaymentBill ObjRPB = new RaisingPaymentBill();
    RaisingPaymentBillController objRPBController = new RaisingPaymentBillController();
    public string back = string.Empty;
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
        Menu aa = (Menu)Page.Master.FindControl("mainMenu");
        aa.Visible = false;
        if (Request.QueryString["obj"] != null)   /// Eknath
        { back = Request.QueryString["obj"].ToString().Trim(); }
        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {

            BindBillList();
        }

    }
    private void BindBillList()
    {
        DataSet ds = null;
        lable1.Text = "Transaction No:" + back;
        string ISSINGLEAUTHORITY = objCommon.LookUp("ACC_" + Session["Comp_Code"].ToString() + "_CONFIG", "PARAMETER", "CONFIGDESC ='IS SINGLE AUTHORITY'");
        if (ISSINGLEAUTHORITY == "Y")
        {
             ds = objRPBController.GetBillStatusNew(Convert.ToInt32(back));
        }
        else
        {
            ds = objRPBController.GetBillStatus(Convert.ToInt32(back));
        }
        
        lvBillList.DataSource = ds;
        lvBillList.DataBind();
    }

    protected void lvBillList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            Label lblNatureService =(Label) e.Item.FindControl("lblNatureService");
            if (lblNatureService.Text == "PENDING")
            {
                lblNatureService.ForeColor = System.Drawing.Color.CornflowerBlue;
            }
            if (lblNatureService.Text == "APPROVED")
            {
                lblNatureService.ForeColor = System.Drawing.Color.Green;
            }
            if (lblNatureService.Text == "REJECTED")
            {
                lblNatureService.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}