//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : CASH BOOK ENTRY
// CREATION DATE : 13-OCTOBER-2009                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;

public partial class CashBookEntry : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    DataSet ds;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!Page.IsPostBack)
        {
            upd_ModalPopupExtender.Show(); 


            if (Request.QueryString["obj"].ToString().Trim() == "'Cash'")
            {
                Session["EntryType"] = "Cash";
                tdlabel.InnerHtml = "CASH BOOK ENTRY";
            }
            else if (Request.QueryString["obj"].ToString().Trim() == "'Bank'")
            {
                Session["EntryType"] = "Bank";
                tdlabel.InnerHtml = "BANK BOOK ENTRY";
            }
            else
            {
                Session["EntryType"] = "";
            
            }

            
          
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
                else { Session["comp_set"] = ""; }
                   
                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                                              
            }

            FillddHeads();

        }
       
        
    }

    private void FillddHeads()
    {
        if (Session["EntryType"] == "Cash")
        {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('1')", "PARTY_NO");
        }
        else if (Session["EntryType"] == "Bank")
        {

            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('2')", "PARTY_NO");
        }
        else {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('1','2')", "PARTY_NO");
        
        }
       



        if (ds != null)
        {
            lvGrp.DataSource = ds;
            lvGrp.DataBind();
            if (ds.Tables[0].Rows.Count > 10)
            {
                pnl.ScrollBars = ScrollBars.None;

            }
            else
            {
                pnl.ScrollBars = ScrollBars.Vertical;
            }


        }
        else
        {
            objCommon.DisplayMessage("Record Not Found.", this);
            txtname.Focus();
        
        }

    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CashBankGroup.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CashBankGroup.aspx");
        }
    }




    #endregion


    
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        txtname.Text = "";
        txtname.Focus();
        FillddHeads();
    }




    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        upd_ModalPopupExtender.Show(); 
        

        if (Session["EntryType"] == "Cash")
        {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('1') and PARTY_NAME like '%" + Convert.ToString(txtname.Text).Trim().ToUpper() + "%' ", "PARTY_NO");
        }
        else if (Session["EntryType"] == "Bank")
        {

            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('2') and PARTY_NAME like '%" + Convert.ToString(txtname.Text).Trim().ToUpper() + "%' ", "PARTY_NO");
        }
        else
        {
            ds = objCommon.FillDropDown("ACC_PARTY_" + Session["comp_code"].ToString().Trim() + "_" + Session["fin_yr"].ToString().Trim(), "PARTY_NO", "PARTY_NAME", "PAYMENT_TYPE_NO IN ('1','2') and PARTY_NAME like '%" + Convert.ToString(txtname.Text).Trim().ToUpper() + "%' ", "PARTY_NO");

        }




        if (ds != null)
        {
            lvGrp.DataSource = ds;
            lvGrp.DataBind();
            txtname.Focus();
            if (ds.Tables[0].Rows.Count > 10)
            {
                pnl.ScrollBars = ScrollBars.None;

            }
            else
            {
                pnl.ScrollBars = ScrollBars.Vertical;
            }


        }
        else
        {
            objCommon.DisplayMessage("Record Not Found.", this);
            txtname.Focus();

        }

    }
    protected void lvGrp_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        HiddenField hdnid = e.Item.FindControl("hdnPcd") as HiddenField;
        LinkButton lnk = e.Item.FindControl("lnkHead") as LinkButton;

        if (hdnid != null && lnk != null)
        {
            Session["BookTran"] = Convert.ToString(hdnid.Value).Trim() + "¤" + Convert.ToString(lnk.Text).Trim() + "¤" + Session["EntryType"];
            Response.Redirect("BookEntry.aspx?send=" + Convert.ToString(Session["BookTran"]).Trim());
            
        }
        else
        {
            Session["BookTran"] = null;
        
        }
    }
}

