//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Sports  (Stock Maintenance)     
// CREATION DATE : 18-MAY-2017
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Sports_StockMaintanance_ItemMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventKitEnt objEK = new EventKitEnt();
    KitController objKCon = new KitController();

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
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["action"] = "add";
               
                BindListViewGroupMaster();
                BindListViewsSubGroupMaster();
                BindListViewItemMaster();
                FillGroupNames();
                FillSubGroupNames();

                //Tabs.ActiveTabIndex = 0;               
            }
        }
    }

    // This method is used to check the page authority.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    #region  Item Group

    private void BindListViewGroupMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_MAIN_ITEM_GROUP", "MIGNAME, SNAME", "MIGNO", "", "");
            lvItemGroupMaster.DataSource = ds;
            lvItemGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butGroupSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objKCon.AddMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtItemGroupName.Text = string.Empty;
                            txtShortName.Text = string.Empty;
                            objCommon.DisplayMessage(this.Page, "Record saved Successfully.", this);
                            BindListViewGroupMaster();
                            FillGroupNames();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record already exist", this);
                    }
                }
                else
                {

                    if (ViewState["gprNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "' and MIGNO <> '" + ViewState["gprNo"].ToString() + "'"));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus csupd = (CustomStatus)objKCon.UpdateMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Convert.ToInt32(ViewState["gprNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                txtItemGroupName.Text = string.Empty;
                                txtShortName.Text = string.Empty;
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this);
                                BindListViewGroupMaster();
                                FillGroupNames();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Record already exist.", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.butGroupSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnEditGroup_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["gprNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            pnlItemSubGroupMaster.Visible = false;
            ShowEditDetailsGroupMaster(Convert.ToInt32(ViewState["gprNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.btnEditGroup_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsGroupMaster(int gprNo)
    {
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("SPRT_MAIN_ITEM_GROUP", "MIGNAME, SNAME", "MIGNO", "MIGNO=" + gprNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemGroupName.Text = ds.Tables[0].Rows[0]["MIGNAME"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void butGroupCancel_Click(object sender, EventArgs e)
    {
        txtItemGroupName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        ViewState["gprNo"] = null;
        ViewState["action"] = "add";
    }

    protected void dpPagerGroupMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewGroupMaster();
    }

    protected void btnshorptitemgrp_Click(object sender, EventArgs e)
    {
        ShowItemReport("ItemGroupList", "ItemGroup.rpt");
    }
    #endregion

    #region Item Sub Group

    private void BindListViewsSubGroupMaster()
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("SPRT_MAIN_ITEM_SUBGROUP A INNER JOIN SPRT_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME, A.SNAME, A.MIGNO, MIGNAME", "MISGNO", "", "MIGNAME, MISGNAME");
            lvItemSubGroupMaster.DataSource = ds;
            lvItemSubGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubItemSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "'"));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objKCon.AddMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtItemSubGroupName.Text = string.Empty;
                            txtSubShortname.Text = string.Empty;
                            ddlItemGroupName.SelectedValue = "0";
                            objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this);
                            BindListViewsSubGroupMaster();
                            FillSubGroupNames();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record Already Exist.", this);
                    }
                }
                else
                {

                    if (ViewState["subGrpNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "' and MISGNO <>" + Convert.ToInt32(ViewState["subGrpNo"])));

                        if (duplicateCkeck == 0)
                        {

                            CustomStatus csupd = (CustomStatus)objKCon.UpdateMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["subGrpNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                txtItemSubGroupName.Text = string.Empty;
                                txtSubShortname.Text = string.Empty;
                                ddlItemGroupName.SelectedValue = "0";
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this);
                                BindListViewsSubGroupMaster();
                                FillSubGroupNames();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Record Already Exist.", this);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.butSubItemSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubItemcancel_Click(object sender, EventArgs e)
    {
        //txtItemSubGroupName.Text = string.Empty;
        //ddlItemGroupName.SelectedValue = "0";
        //txtSubShortname.Text = string.Empty;
        //ViewState["subGrpNo"] = null;
        //ViewState["action"] = "add";
        clear();
    }

    protected void btnEditSubGroup_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["subGrpNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            
            ShowEditDetailsSubGroupMaster(Convert.ToInt32(ViewState["subGrpNo"].ToString()));
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.btnEditSubGroup_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsSubGroupMaster(int subGrpNo)
    {
       
        DataSet ds = null;
        try
        {
            ds = objCommon.FillDropDown("SPRT_MAIN_ITEM_SUBGROUP A INNER JOIN SPRT_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME, A.SNAME, A.MIGNO, MIGNAME", "MISGNO", "MISGNO=" + subGrpNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemGroupName.SelectedValue = ds.Tables[0].Rows[0]["MIGNO"].ToString();
                txtItemSubGroupName.Text = ds.Tables[0].Rows[0]["MISGNAME"].ToString();
                txtSubShortname.Text = ds.Tables[0].Rows[0]["SNAME"].ToString().Trim();  //Shaikh juned(11-05-2022)  Add Trim() Resolved the cursor and text issue  
                
                                
            }
            DataSet dss = objCommon.FillDropDown("SPRT_MAIN_ITEM_SUBGROUP A INNER JOIN SPRT_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME, A.SNAME, A.MIGNO, MIGNAME", "MISGNO", "MISGNO=" + subGrpNo, "");
            string misgno = dss.Tables[0].Rows[0]["MIGNO"].ToString();
            if (misgno == "0")
            {
                updatePanel5.Visible = false;

                return;
            }
            else
            {
                updatePanel5.Visible = true;
                

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void dpPagerSubGroupMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewsSubGroupMaster();
    }

    protected void btnshowsubgrprpt_Click(object sender, EventArgs e)
    {
        ShowItemReport("ItemSubGroupList", "ItemSubGroup.rpt");
    }

    #endregion

    #region Item Master

    private void BindListViewItemMaster()
    {
        try
        {
            DataSet ds = objKCon.GetAllItemMaster();
            lvItemMaster.DataSource = ds;
            lvItemMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.BindListViewItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butItemMasterSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objEK.ITEM_CODE = txtItemCode.Text;
            objEK.ITEM_DETAILS = txtCommonDescriptionOfItem.Text;
            objEK.ITEM_NAME = txtItemName.Text;
            objEK.MISGNO = Convert.ToInt32(ddlItemSubGroup.SelectedValue);
            objEK.MIGNO = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_SUBGROUP", "MIGNO", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue)));
            objEK.ITEM_MAX_QTY = Convert.ToInt32(txtMaxQuantity.Text);
            objEK.ITEM_MIN_QTY = Convert.ToInt32(txtMinQuantity.Text);
            objEK.ITEM_CUR_QTY = 0;
            objEK.ITEM_OB_QTY = Convert.ToInt32(txtOpeningBalanceQuantity.Text);
            objEK.ITEM_OB_VALUE = Convert.ToInt32(txtOpeningBalanceQuantity.Text);
            objEK.ITEM_REORDER_QTY = Convert.ToInt32(txtReorderLevel.Text);
            objEK.COLLEGE_CODE = Session["colcode"].ToString();
            objEK.ITEM_UNIT = "0"; // ddlUnit.SelectedValue;
            objEK.ITEM_APPROVAL = "";

            if (Convert.ToInt32(txtMaxQuantity.Text) < Convert.ToInt32(txtMinQuantity.Text))
            {
                objCommon.DisplayMessage(this.Page, "Maximum Quentity Should Be Greater Than Minimum Quentity.", this);
                return;
            }


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_ITEM", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " '"));
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_ITEM", " count(*)", "ITEM_CODE= '" + Convert.ToString(txtItemCode.Text) + " '"));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objKCon.AddItemMaster(objEK, Session["userfullname"].ToString(), 0);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this);
                            BindListViewItemMaster();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "This Item Code Already exist.", this);
                        return;
                    }
                }
                else
                {
                    if (ViewState["itemNo"] != null)
                    {
                        //  int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_ITEM", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'AND ITEM_NO <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                        //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_ITEM", " count(*)", "ITEM_CODE= '" + Convert.ToString(txtItemCode.Text) + " '"));

                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " count(*)", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'and item_no <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                        //Shaikh Juned(10-05-2022)
                        if (duplicateCkeck == 0)
                        {
                            objEK.ITEM_NO = Convert.ToInt32(ViewState["itemNo"].ToString());
                            CustomStatus cs = (CustomStatus)objKCon.UpdateItemMaster(objEK, Session["userfullname"].ToString(), 0);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                clear();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this);
                                BindListViewItemMaster();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "This Item Code Already exist.", this);
                            return;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.butItemMasterSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butItemMasterCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnEditItemMaster_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["itemNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsItemMaster(Convert.ToInt32(ViewState["itemNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.btnEditItemMaster_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsItemMaster(int itemNo)
    {
        DataSet ds = null;
        try
        {
            ds = objKCon.GetSingleRecordItemMaster(itemNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemSubGroup.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();
                txtCommonDescriptionOfItem.Text = ds.Tables[0].Rows[0]["ITEM_DETAILS"].ToString();
                txtItemCode.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString().Trim();         //Shaikh juned(11-05-2022)  Add Trim() Resolved the cursor and text issue  
                txtItemName.Text = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                txtMaxQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString();
                txtMinQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MIN_QTY"].ToString();
                txtOpeningBalanceQuantity.Text = ds.Tables[0].Rows[0]["ITEM_OB_QTY"].ToString();
                txtReorderLevel.Text = ds.Tables[0].Rows[0]["ITEM_REORDER_QTY"].ToString();
                //ddlItmType.SelectedValue = ds.Tables[0].Rows[0]["ITPNO"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.ShowEditDetailsItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    protected void dpPagerItemMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewItemMaster();
    }

    protected void FillGroupNames()
    {
        try
        {
            objCommon.FillDropDownList(ddlItemGroupName, "SPRT_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.FillGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillSubGroupNames()
    {
        try
        {
            objCommon.FillDropDownList(ddlItemSubGroup, "SPRT_MAIN_ITEM_SUBGROUP SG INNER JOIN SPRT_MAIN_ITEM_GROUP MG ON(MG.MIGNO = SG.MIGNO)", "SG.MISGNO", "MISGNAME+ ' [ '+ MG.MIGNAME +' ]'", "", "SG.MISGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.FillSubGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clear()
    {
        ddlItemSubGroup.SelectedValue = "0";
        txtCommonDescriptionOfItem.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtMaxQuantity.Text = "0";
        txtMinQuantity.Text = "0";
        txtOpeningBalanceQuantity.Text = "0";
        txtReorderLevel.Text = "0";
        // ddlItmType.SelectedValue = "0";
        ViewState["itemNo"] = null;
        ViewState["action"] = "add";
    }

    protected void btnshowrptItems_Click(object sender, EventArgs e)
    {
        ShowItemReport("SportsItemList", "SportsItemList.rpt");
    }

    #endregion

    protected void tab_activetabindexchanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("ItemWiseSummaryReport", "SportsItemSummaryReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=" + reportTitle + ".pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_ITEMNO=0";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // Use for Item Reports
    private void ShowItemReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=" + reportTitle + ".pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_SportsVendor.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlItemGroupName.SelectedIndex = 0;
        txtItemSubGroupName.Text = string.Empty;
        txtSubShortname.Text = string.Empty;
        
    }
   protected void btnSubGroupSubmit_Click(object sender, EventArgs e)
{
    try
    {
        if (ViewState["action"] != null)
        {

            if (ViewState["action"].ToString().Equals("add"))
            {
                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "'"));
                if (duplicateCkeck == 0)
                {
                    CustomStatus cs = (CustomStatus)objKCon.AddMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        txtItemSubGroupName.Text = string.Empty;
                        txtSubShortname.Text = string.Empty;
                        ddlItemGroupName.SelectedValue = "0";
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully.", this);
                        BindListViewsSubGroupMaster();
                        FillSubGroupNames();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record Already Exist.", this);
                }
            }
            else
            {

                if (ViewState["subGrpNo"] != null)
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("SPRT_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "' and MISGNO <>" + Convert.ToInt32(ViewState["subGrpNo"])));

                    if (duplicateCkeck == 0)
                    {

                        CustomStatus csupd = (CustomStatus)objKCon.UpdateMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["subGrpNo"].ToString()), Session["userfullname"].ToString());
                        if (csupd.Equals(CustomStatus.RecordUpdated))
                        {
                            txtItemSubGroupName.Text = string.Empty;
                            txtSubShortname.Text = string.Empty;
                            ddlItemGroupName.SelectedValue = "0";
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.Page, "Record Updated Successfully.", this);
                            BindListViewsSubGroupMaster();
                            FillSubGroupNames();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Record Already Exist.", this);
                    }
                }
            }
        }
    }

    catch (Exception ex)
    {
        if (Convert.ToBoolean(Session["error"]) == true)
            objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemMaster.butSubItemSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
        else
            objUCommon.ShowError(Page, "Server UnAvailable");
    }
}
}
