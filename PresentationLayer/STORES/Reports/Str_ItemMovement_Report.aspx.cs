//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                              
// CREATION DATE : 26/07/2021                                                     
// CREATED BY    : GOPAL ANTHATI                                                      
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Collections;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;


public partial class STORES_Reports_Str_ItemMovement_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            FillDropDownList();
           // divSRNO.Visible = false;

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


    private void FillDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO <> 0", "MIGNAME");//and ITEM_TYPE='F'            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Stores_Transactions_Str_user_Requisition.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////ddlCategory.Items.Clear();
        //objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
        //if (ddlCategory.SelectedValue == "2")
        //{
        //    if (ddlCategory.Items.Count == 1)
        //    {
        //        MessageBox("Cosumable Items are not Added.");
        //    }
        //}

        //ddlCategory.Items.Clear();
        objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=" + ddlCategory.SelectedValue, "MISGNAME");
        if (ddlCategory.SelectedValue == "2")
        {
            if (ddlCategory.Items.Count == 1)
            {
                MessageBox("Cosumable Items are not Added.");
                return;
            }
            divItemSrNo.Visible = false;
        }
        else
        {
            divItemSrNo.Visible = true;
        }


    }


    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlItem, "store_item", "ITEM_NO", "ITEM_NAME", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        
        
        //objCommon.FillDropDownList(ddlItem, "store_item", "ITEM_NO", "ITEM_NAME", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        //objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + "", "ITEM_NAME");
        //if (ddlItem.SelectedIndex == 0)
        //{
        //    objCommon.FillDropDownList(ddlItemSrNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        //}
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////objCommon.FillDropDownList(ddlItemSrNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) +  " AND ITEM_NO=" +Convert.ToInt32(ddlItem.SelectedValue)+"", "");//and ITEM_TYPE='F'            
        //objCommon.FillDropDownList(ddlItemSrNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DSR_NUMBER is not null", "");//and ITEM_TYPE='F'            

        //if (ddlItemSrNo.Items.Count == 1)
        //{
        //    MessageBox("Item Serial No. is not Generated.");
        //}


        if (ddlCategory.SelectedValue == "1")
        {

            //objCommon.FillDropDownList(ddlItemSrNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "MIGNO=" + Convert.ToInt32(ddlCategory.SelectedValue) + " AND MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) +  " AND ITEM_NO=" +Convert.ToInt32(ddlItem.SelectedValue)+"", "");//and ITEM_TYPE='F'            
            objCommon.FillDropDownList(ddlItemSrNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + " AND DSR_NUMBER is not null", "");//and ITEM_TYPE='F'            

            if (ddlItemSrNo.Items.Count == 1)
            {
                MessageBox("Item Serial No. is not Generated.");
            }
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue == "1")
            {
                if (ddlItemSrNo.SelectedIndex == 0)
                {
                    MessageBox("Please Selct Item Sr.No.");
                    return;
                }
            }
            ShowReport("Report", "StrItemMovementReport.rpt");


           // ShowReport("Report", "StrItemMovementReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Reports_Stock_Reports.btnLICReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {

        try
        {


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_DSR_NUMBER=" + ddlItemSrNo.SelectedItem.Text + ",@P_ITEM_NO=" + ddlItem.SelectedValue + ",@P_ITEM_TYPE=" + ddlCategory.SelectedValue;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);



            //string Script = string.Empty;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            //url += "Reports/commonreport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,STORES," + rptFileName;
            ////url += "&param=@P_DSR_NUMBER=" + ddlItemSrNo.SelectedItem.Text;
            //url += "&param=@P_DSR_NUMBER=" + ddlItemSrNo.SelectedItem.Text + ",@P_ITEM_NO=" + ddlItem.SelectedValue + ",@P_ITEM_TYPE=" + ddlCategory.SelectedValue;
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Reports_Stock_Reports.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlItemSrNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlItemSrNo.SelectedIndex = 0;



    }
}