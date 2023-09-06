//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Stock Maintenance)     
// CREATION DATE : 27-FEB-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class Health_StockMaintenance_ItemMaster : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StockMaster objStock = new StockMaster();
    StockMaintnance objSController = new StockMaintnance();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["action"] = "add";
                BindListViewGroupMaster();
                BindListViewsSubGroupMaster();
                BindListViewItemMaster();
                FillGroupNames();
                FillSubGroupNames();
                FillItemTypes();
                //Tabs.ActiveTabIndex = 0;
                objCommon.FillDropDownList(ddlUnit, "HEALTH_UNIT", "UNIT_ID", "UNIT_NAME", "", "UNIT_ID");
            }
            //Set Item Group Report Parameters
            //objCommon.ReportPopUp(btnshorptitemgrp, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Item_grp_Master.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            //Set Item sub Group Report Parameters
            //objCommon.ReportPopUp(btnshowsubgrprpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Item_Subgrp_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            //Set Items Master Report Parameters
            //objCommon.ReportPopUp(btnshowrptItems, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Items_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
    }

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


    #region HEALTH_MAIN_ITEM_GROUP

    private void BindListViewGroupMaster()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllMainItemGroup();
            ds = objCommon.FillDropDown("HEALTH_MAIN_ITEM_GROUP", " MIGNAME,SNAME", "MIGNO", "", "");
            lvItemGroupMaster.DataSource = ds;
            lvItemGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objSController.AddMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtItemGroupName.Text = string.Empty;
                            txtShortName.Text = string.Empty;
                            MessageBox("Record saved Successfully.");
                            BindListViewGroupMaster();
                            FillGroupNames();
                        }
                    }
                    else
                    {
                        MessageBox("Record already exist");
                    }
                }
                else
                {

                    if (ViewState["gprNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "' and MIGNO <> '" + ViewState["gprNo"].ToString() + "'"));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus csupd = (CustomStatus)objSController.UpdateMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Convert.ToInt32(ViewState["gprNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                txtItemGroupName.Text = string.Empty;
                                txtShortName.Text = string.Empty;
                                ViewState["action"] = "add";
                                MessageBox("Record Updated Successfully.");
                                BindListViewGroupMaster();
                                FillGroupNames();
                            }
                        }
                        else
                        {
                            MessageBox("Record alreay exist.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                MessageBox("Record Already Exist");
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.butGroupSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnEditGroup_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["gprNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsGroupMaster(Convert.ToInt32(ViewState["gprNo"].ToString()));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.btnEditGroup_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsGroupMaster(int gprNo)
    {
        DataSet ds = null;
        try
        {
            //  ds = objStrMaster.GetSingleRecordMainItemGroup(gprNo);
            ds = objCommon.FillDropDown("HEALTH_MAIN_ITEM_GROUP", "MIGNAME,SNAME", "MIGNO", "MIGNO=" + gprNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtItemGroupName.Text = ds.Tables[0].Rows[0]["MIGNAME"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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
        //Response.Redirect(Request.Url.ToString());
        txtItemGroupName.Text = string.Empty;
        txtShortName.Text = string.Empty;
        ViewState["gprNo"] = null;
        ViewState["action"] = "add";
    }

    protected void dpPagerGroupMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewGroupMaster();
    }

    #endregion

    #region HEALTH_MAIN_ITEM_SUBGROUP

    private void BindListViewsSubGroupMaster()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllMainSubItemGroup();
            ds = objCommon.FillDropDown("HEALTH_MAIN_ITEM_SUBGROUP A INNER JOIN HEALTH_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME,A.SNAME,A.MIGNO,MIGNAME", "MISGNO", "", "MIGNAME, MISGNAME");
            lvItemSubGroupMaster.DataSource = ds;
            lvItemSubGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "'"));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objSController.AddMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtItemSubGroupName.Text = string.Empty;
                            txtSubShortname.Text = string.Empty;
                            ddlItemGroupName.SelectedValue = "0";
                            MessageBox("Record Saved Successfully.");
                            BindListViewsSubGroupMaster();
                            FillSubGroupNames();
                        }
                    }
                    else
                    {
                        MessageBox("Record Already Exist.");
                    }
                }
                else
                {

                    if (ViewState["subGrpNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "' and MISGNO <>" + Convert.ToInt32(ViewState["subGrpNo"])));

                        if (duplicateCkeck == 0)
                        {

                            CustomStatus csupd = (CustomStatus)objSController.UpdateMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["subGrpNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                txtItemSubGroupName.Text = string.Empty;
                                txtSubShortname.Text = string.Empty;
                                ddlItemGroupName.SelectedValue = "0";
                                ViewState["action"] = "add";
                                MessageBox("Record Updated Successfully.");
                                BindListViewsSubGroupMaster();
                                FillSubGroupNames();
                            }
                        }
                        else
                        {
                            MessageBox("Record Already Exist.");
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                MessageBox("Record Already Exist");
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.butSubItemSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void butSubItemcancel_Click(object sender, EventArgs e)
    {

        txtItemSubGroupName.Text = string.Empty;
        ddlItemGroupName.SelectedValue = "0";
        txtSubShortname.Text = string.Empty;
        ViewState["subGrpNo"] = null;
        ViewState["action"] = "add";
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
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.btnEditSubGroup_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsSubGroupMaster(int subGrpNo)
    {
        DataSet ds = null;

        try
        {
            // ds = objStrMaster.GetSingleRecordMainSubItemGroup(subGrpNo);
            ds = objCommon.FillDropDown("HEALTH_MAIN_ITEM_SUBGROUP A   INNER JOIN HEALTH_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME,A.SNAME,A.MIGNO,MIGNAME", "MISGNO", "MISGNO=" + subGrpNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemGroupName.SelectedValue = ds.Tables[0].Rows[0]["MIGNO"].ToString();
                txtItemSubGroupName.Text = ds.Tables[0].Rows[0]["MISGNAME"].ToString();
                txtSubShortname.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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

    #endregion

    #region HEALTH_ITEM_MASTER

    private void BindListViewItemMaster()
    {
        try
        {
            DataSet ds = objSController.GetAllItemMaster();
            lvItemMaster.DataSource = ds;
            lvItemMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.BindListViewItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butItemMasterSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            objStock.ITEM_CODE = txtItemCode.Text;
            objStock.ITEM_DETAILS = txtCommonDescriptionOfItem.Text;
            objStock.ITEM_NAME = txtItemName.Text;
            objStock.MISGNO = Convert.ToInt32(ddlItemSubGroup.SelectedValue);
            objStock.MIGNO = Convert.ToInt32(objCommon.LookUp("HEALTH_MAIN_ITEM_SUBGROUP", "MIGNO", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue)));
            objStock.ITEM_MAX_QTY = Convert.ToInt32(txtMaxQuantity.Text);
            objStock.ITEM_MIN_QTY = Convert.ToInt32(txtMinQuantity.Text);
            objStock.ITEM_CUR_QTY = 0;
            objStock.ITEM_OB_QTY = Convert.ToInt32(txtOpeningBalanceQuantity.Text);
            objStock.ITEM_OB_VALUE = Convert.ToInt32(txtOpeningBalanceQuantity.Text);
            //objStock.ITEM_BUD_QTY = Convert.ToInt32(txtBudgetQuantity.Text);
            objStock.ITEM_REORDER_QTY = Convert.ToInt32(txtReorderLevel.Text);
            objStock.COLLEGE_CODE = Session["colcode"].ToString();
            objStock.ITEM_UNIT = ddlUnit.SelectedValue; // txtUnit.Text;
            objStock.ITEM_APPROVAL = "";
            //if (rdbapproval.SelectedIndex == 0)
            //{
            //    objStock.ITEM_APPROVAL = "Y";
            //}
            //else
            //{
            //    objStock.ITEM_APPROVAL = "N";
            //}
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_ITEM", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " '"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objSController.AddItemMaster(objStock, Session["userfullname"].ToString(), Convert.ToInt32(ddlItmType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            MessageBox("Record Saved Successfully.");
                            BindListViewItemMaster();
                        }
                    }
                    else
                    {
                        MessageBox("Record Already exist.");
                    }
                }
                else
                {
                    if (ViewState["itemNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("HEALTH_ITEM", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'AND ITEM_NO <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                        if (duplicateCkeck == 0)
                        {
                            objStock.ITEM_NO = Convert.ToInt32(ViewState["itemNo"].ToString());
                            CustomStatus cs = (CustomStatus)objSController.UpdateItemMaster(objStock, Session["userfullname"].ToString(), Convert.ToInt32(ddlItmType.SelectedValue));
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                clear();
                                ViewState["action"] = "add";
                                MessageBox("Record Updated Successfully.");
                                BindListViewItemMaster();
                            }
                        }
                        else
                        {
                            MessageBox("Record Already exist.");
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                MessageBox("Record Already Exist.");
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.butItemMasterSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
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
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.btnEditItemMaster_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsItemMaster(int itemNo)
    {
        DataSet ds = null;
        try
        {
            ds = objSController.GetSingleRecordItemMaster(itemNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemSubGroup.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();
                //txtBudgetQuantity.Text = ds.Tables[0].Rows[0]["ITEM_BUD_QTY"].ToString();
                txtCommonDescriptionOfItem.Text = ds.Tables[0].Rows[0]["ITEM_DETAILS"].ToString();
                txtItemCode.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                txtMaxQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString();
                txtMinQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MIN_QTY"].ToString();
                txtOpeningBalanceQuantity.Text = ds.Tables[0].Rows[0]["ITEM_OB_QTY"].ToString();
               // txtOpeningBalanceValue.Text = ds.Tables[0].Rows[0]["ITEM_OB_VALUE"].ToString();
               // txtUnit.Text = ds.Tables[0].Rows[0]["ITEM_UNIT"].ToString();
                ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["ITEM_UNIT"].ToString();
                txtReorderLevel.Text = ds.Tables[0].Rows[0]["ITEM_REORDER_QTY"].ToString();
                ddlItmType.SelectedValue = ds.Tables[0].Rows[0]["ITPNO"].ToString();

                //string approvalitm = ds.Tables[0].Rows[0]["APPROVAL"].ToString();
                //if (approvalitm == "Y")
                //{
                //    rdbapproval.SelectedIndex = 0;
                //}
                //else
                //{
                //    rdbapproval.SelectedIndex = 1;
                //}
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.ShowEditDetailsItemMaster-> " + ex.Message + " " + ex.StackTrace);
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
            objCommon.FillDropDownList(ddlItemGroupName, "HEALTH_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.FillGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillItemTypes()
    {
        try
        {
            objCommon.FillDropDownList(ddlItmType, "HEALTH_ITEMTYPE", "ITPNO", "ITPNAME", "", "ITPNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.FillGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillSubGroupNames()
    {
        try
        {
            //objCommon.FillDropDownList(ddlItemSubGroup, "HEALTH_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME+ ' [ '+dbo.FN_DESC('GROUP',MIGNO)+' ]' ", "", "MISGNAME");
            objCommon.FillDropDownList(ddlItemSubGroup, "HEALTH_MAIN_ITEM_SUBGROUP SG INNER JOIN HEALTH_MAIN_ITEM_GROUP MG ON(MG.MIGNO = SG.MIGNO)", "SG.MISGNO", "MISGNAME+ ' [ '+ MG.MIGNAME +' ]'", "", "SG.MISGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_StockMaintenance_ItemMaster.FillSubGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clear()
    {
        ddlItemSubGroup.SelectedValue = "0";
        //txtBudgetQuantity.Text = "0";
        txtCommonDescriptionOfItem.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtMaxQuantity.Text = "0";
        txtMinQuantity.Text = "0";
        txtOpeningBalanceQuantity.Text = "0";
       // txtOpeningBalanceValue.Text = "0";
       // txtUnit.Text = string.Empty;
        ddlUnit.SelectedIndex = 0;
        txtReorderLevel.Text = "0";
        ddlItmType.SelectedValue = "0";
        ViewState["itemNo"] = null;
        ViewState["action"] = "add";
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

            ShowReport("ItemWiseSummary", "rptItemSummaryReport.rpt");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Report_ItemSummary.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_ITEMNO=0"; //+ Convert.ToInt32(ddlItem.SelectedValue);

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Report_ItemSummary.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnshowrptItems_Click(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnshowrptItems, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Items_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        ShowItemReport("ItemMaster", "Items_Master_Report.rpt");
    }

    private void ShowItemReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(); //+ Convert.ToInt32(ddlItem.SelectedValue);

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_Report_ItemSummary.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnshowsubgrprpt_Click(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnshowsubgrprpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Item_Subgrp_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        ShowItemReport("ItemSubGroupMaster", "Item_Subgrp_Report.rpt");
    }

    protected void btnshorptitemgrp_Click(object sender, EventArgs e)
    {
        //objCommon.ReportPopUp(btnshorptitemgrp, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Health" + "," + "Item_grp_Master.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        ShowItemReport("ItemSubGroupMaster", "Item_grp_Master.rpt");
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}