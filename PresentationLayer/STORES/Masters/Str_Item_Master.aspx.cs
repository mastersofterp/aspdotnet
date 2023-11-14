//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Item_Master.ASPX                                                    
// CREATION DATE : 05-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.NITPRM;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Stores_Masters_Str_Item_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();

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
                ViewState["action1"] = "add";
                ViewState["action2"] = "add";
                ViewState["action3"] = "add";

                BindListViewGroupMaster();
                BindListViewsSubGroupMaster();

                FillGroupNames();
                FillSubGroupNames();
                FillItemTypes();


                //this.CheckMainStoreUser();
                String Dept_Wise_Item = objCommon.LookUp("STORE_REFERENCE", "DEPT_WISE_ITEM", "");
                ViewState["Dept_Wise_Item"] = Dept_Wise_Item;

                if (ViewState["Dept_Wise_Item"].ToString() == "1")
                {
                    FillDepartment();

                    divDept.Visible = true;
                    //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                    //{
                    //    BindListViewItemMaster();
                    //}


                    //if (ViewState["StoreUser"].ToString() != "NormalUser")
                    //{
                    //    BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue), ViewState["StoreUser"].ToString());
                    //}
                    //

                    //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
                    //{
                    BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue)); //, ViewState["StoreUser"].ToString());
                    //}
                }
                else
                {
                    divDept.Visible = false;
                    BindListViewItemMaster();
                    FillItemSubGroup();
                    BindListViewDepriciation();
                }
            }
            //Set Item Group Report Parameters
            objCommon.ReportPopUp(btnshorptitemgrp, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Item_grp_Master.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            //Set Item sub Group Report Parameters
            objCommon.ReportPopUp(btnshowsubgrprpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Item_Subgrp_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            //Set Items Master Report Parameters
            objCommon.ReportPopUp(btnshowrptItems, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Items_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
        String hiddenFieldValue = hidLastTab.Value;
        System.Text.StringBuilder js = new System.Text.StringBuilder();
        js.Append("<script type='text/javascript'>");
        js.Append("var previouslySelectedTab = ");
        js.Append(hiddenFieldValue);
        js.Append(";</script>");
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "acttab", js.ToString());
    }
    public void FillItemSubGroup()
    {
        objCommon.FillDropDownList(ddlItemSubgroupdep, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNO DESC");
    }

    private void FillDepartment()
    {
        try
        {
            //main store user
            //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            //{
            //    objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "MDNO", "MDNAME", "", "MDNAME");  // MDNO=1               
            //}
            //else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            //{
            // Departmental user                
            objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "MDNO", "MDNAME", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "MDNAME DESC");
            ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORES_Masters_STR_PASSING_AUTHORITY_PATH.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND APLNO=" + deptStoreUser);

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
        }
    }

    #region Tax

    protected void ChkTaxChckedChanged(object sender, EventArgs e)
    {

        if (chkTax.Checked == true)
        {

            if (ViewState["action"].ToString().Equals("add"))
            {
                BindListViewTax();
            }
            else
            {
                string TAXID = (objCommon.LookUp("STORE_TAX_ITEM_MAP", "TAXID", "ITEM_NO=" + Convert.ToInt32(ViewState["itemNo"].ToString())));
                if (TAXID != "")
                {
                    ShowEditDetailsTaxItemMaster(Convert.ToInt32(ViewState["itemNo"].ToString()));
                }
                else
                {
                    BindListViewTax();
                }
            }

            lvTaxFields.Visible = true;

        }
        else
        {
            lvTaxFields.Visible = false;
            lvTaxFields.DataSource = null;
            lvTaxFields.DataBind();
        }
    }

    private void BindListViewTax()
    {
        try
        {

            DataSet ds = objStrMaster.GetStoreTax();

            lvTaxFields.DataSource = ds;
            lvTaxFields.DataBind();
            lvTaxFields.Visible = true;


            //for (int i = 0; i < lvTaxFields.Items.Count; i++)
            //{
            //    Label lblActive = lvTaxFields.Items[i].FindControl("lblSTATETAX") as Label;
            //    if (ds.Tables[0].Rows[i]["IS_STATE_TAX"].ToString().Equals("1"))
            //    {
            //        lblActive.Text = "State Tax";
            //    }
            //    else
            //    {
            //        lblActive.Text = "City Tax";
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewTax-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion

    #region STORE_MAIN_ITEM_GROUP


    private void BindListViewGroupMaster()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllMainItemGroup();
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_GROUP", " MIGNAME,SNAME,ITEM_TYPE", "MIGNO", "", "");
            lvItemGroupMaster.DataSource = ds;
            lvItemGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butGroupSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action2"] != null)
            {
                if (ViewState["action2"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString(), Convert.ToChar(rdbItemType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtItemGroupName.Text = string.Empty;
                            txtShortName.Text = string.Empty;
                            objCommon.DisplayMessage(updatePanel3, "Record saved Successfully", this);
                            BindListViewGroupMaster();
                            FillGroupNames();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel3, "Record already exist", this);
                    }
                }
                else
                {

                    if (ViewState["gprNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_GROUP", " count(*)", "MIGNAME='" + Convert.ToString(txtItemGroupName.Text) + "' and MIGNO <> '" + ViewState["gprNo"].ToString() + "'"));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateMainItemGroup(txtItemGroupName.Text, txtShortName.Text, Session["colcode"].ToString(), Convert.ToInt32(ViewState["gprNo"].ToString()), Session["userfullname"].ToString(), Convert.ToChar(rdbItemType.SelectedValue));
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                txtItemGroupName.Text = string.Empty;
                                txtShortName.Text = string.Empty;
                                ViewState["action2"] = "add";
                                objCommon.DisplayMessage(updatePanel3, "Record Updated Successfully", this);
                                BindListViewGroupMaster();
                                FillGroupNames();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel3, "Record alreay exist", this);
                        }

                    }


                }
            }
        }

        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                objCommon.DisplayMessage(updatePanel1, "Record Already Exist", Page);
            }
            else
            {

                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.butGroupSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ViewState["action2"] = "edit";
            ShowEditDetailsGroupMaster(Convert.ToInt32(ViewState["gprNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.btnEditGroup_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_GROUP", "MIGNAME,SNAME,ITEM_TYPE", "MIGNO", "MIGNO=" + gprNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtItemGroupName.Text = ds.Tables[0].Rows[0]["MIGNAME"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SNAME"].ToString();
                rdbItemType.SelectedValue = ds.Tables[0].Rows[0]["ITEM_TYPE"].ToString();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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
        rdbItemType.SelectedValue = "F";


    }

    protected void dpPagerGroupMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewGroupMaster();

    }

    #endregion

    #region STORE_MAIN_ITEM_SUBGROUP

    private void BindListViewsSubGroupMaster()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllMainSubItemGroup();
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_SUBGROUP A INNER JOIN STORE_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME,A.SNAME,A.MIGNO,MIGNAME", "MISGNO", "", "MIGNAME, MISGNAME");
            lvItemSubGroupMaster.DataSource = ds;
            lvItemSubGroupMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewGroupMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubItemSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action1"] != null)
            {

                if (ViewState["action1"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "'"));
                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updatePanel2, "Record Saved Successfully", this);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel2, "Record Already Exist", this);
                    }
                }
                else
                {

                    if (ViewState["subGrpNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_SUBGROUP", " count(*)", "MIGNO=" + Convert.ToInt32(ddlItemGroupName.SelectedValue) + " and MISGNAME='" + Convert.ToString(txtItemSubGroupName.Text) + "' and MISGNO <>" + Convert.ToInt32(ViewState["subGrpNo"])));

                        if (duplicateCkeck == 0)
                        {

                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateMainSubItemGroup(txtItemSubGroupName.Text, txtSubShortname.Text, Convert.ToInt32(ddlItemGroupName.SelectedValue), Session["colcode"].ToString(), Convert.ToInt32(ViewState["subGrpNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action1"] = "add";
                                objCommon.DisplayMessage(updatePanel2, "Record Updated Successfully", this);

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel2, "Record Already Exist", this);
                        }

                    }


                }
                FillItemSubGroup();
                BindListViewsSubGroupMaster();
                FillSubGroupNames();
                txtItemSubGroupName.Text = string.Empty;
                txtSubShortname.Text = string.Empty;
                ddlItemGroupName.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                objCommon.DisplayMessage(updatePanel1, "Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.butSubItemSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
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
        //Response.Redirect(Request.Url.ToString());
        ViewState["action1"] = "add";
    }

    protected void btnEditSubGroup_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["subGrpNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action1"] = "edit";
            ShowEditDetailsSubGroupMaster(Convert.ToInt32(ViewState["subGrpNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.btnEditSubGroup_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_SUBGROUP A   INNER JOIN STORE_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "MISGNAME,A.SNAME,A.MIGNO,MIGNAME", "MISGNO", "MISGNO=" + subGrpNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemGroupName.SelectedValue = ds.Tables[0].Rows[0]["MIGNO"].ToString();
                txtItemSubGroupName.Text = ds.Tables[0].Rows[0]["MISGNAME"].ToString();
                txtSubShortname.Text = ds.Tables[0].Rows[0]["SNAME"].ToString().Trim();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowEditDetailsGroupMaster-> " + ex.Message + " " + ex.StackTrace);
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

    #region STORE_ITEM_MASTER

    private void BindListViewItemMaster(int Deptno) // ,string StoreUser)
    {
        try
        {
            DataSet ds = objStrMaster.GetAllItemMaster(Deptno); //, StoreUser);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvItemMaster.DataSource = ds;
                lvItemMaster.DataBind();
                pnlItemMaster.Visible = true;
            }
            else
            {
                lvItemMaster.DataSource = null;
                lvItemMaster.DataBind();
                pnlItemMaster.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewItemMaster()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllItemMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvItemMaster.DataSource = ds;
                lvItemMaster.DataBind();
                pnlItemMaster.Visible = true;
            }
            else
            {
                lvItemMaster.DataSource = null;
                lvItemMaster.DataBind();
                pnlItemMaster.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butItemMasterSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            StoreMaster objStrMst = new StoreMaster();
            int duplicateCkeck;

            if (ViewState["Dept_Wise_Item"].ToString() == "1")
            {
                if (ddlDepartment.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(updatePanel1, "Please Select Department.", this);
                    return;
                }
                else
                {
                    objStrMst.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
                }
            }
            else
            {
                objStrMst.DEPTNO = 0;
            }

            objStrMst.ITEM_CODE = txtItemCode.Text;
            objStrMst.ITEM_DETAILS = txtCommonDescriptionOfItem.Text;
            objStrMst.ITEM_NAME = txtItemName.Text;
            objStrMst.MISGNO = Convert.ToInt32(ddlItemSubGroup.SelectedValue);
            objStrMst.MIGNO = Convert.ToInt32(objCommon.LookUp("STORE_MAIN_ITEM_SUBGROUP", "migno", "MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue)));
            objStrMst.ITEM_MAX_QTY = Convert.ToInt32(txtMaxQuantity.Text);
            objStrMst.ITEM_MIN_QTY = Convert.ToInt32(txtMinQuantity.Text);
            objStrMst.ITEM_CUR_QTY = 0;
            objStrMst.ITEM_OB_QTY = Convert.ToInt32(txtOpeningBalanceQuantity.Text);
            objStrMst.ITEM_OB_VALUE = Convert.ToInt32(txtOpeningBalanceValue.Text);
            objStrMst.ITEM_BUD_QTY = Convert.ToInt32(txtBudgetQuantity.Text);
            objStrMst.ITEM_REORDER_QTY = Convert.ToInt32(txtReorderLevel.Text);
            objStrMst.COLLEGE_CODE = Session["colcode"].ToString();
            objStrMst.ITEM_UNIT = txtUnit.Text;
            objStrMst.ITEM_APPROVAL = "Y";


            //--------------------------------------------------------
            if (chkTax.Checked == true)
            {
                int count = 0;
                foreach (ListViewItem i in lvTaxFields.Items)
                {

                    CheckBox chkTAXID = (CheckBox)i.FindControl("chkTAXID");
                    if (chkTAXID.Checked)
                    {
                        count++;

                    }


                }
                if (count == 0)
                {
                    objCommon.DisplayMessage(updatePanel1, "Please Select Tax.", this);
                    return;
                }

            }


            DataTable TaxFieldsTbl = new DataTable("TaxFieldsTbl");

            TaxFieldsTbl.Columns.Add("TAX_SRNO", typeof(int));
            TaxFieldsTbl.Columns.Add("TAXID", typeof(int));

            DataRow dr = null;
            foreach (ListViewItem i in lvTaxFields.Items)
            {
                CheckBox chkTAXID = (CheckBox)i.FindControl("chkTAXID");
                HiddenField HDTAXSRNO = (HiddenField)i.FindControl("HDTAXSRNO");
                HiddenField hdTAXID = (HiddenField)i.FindControl("hdTAXID");



                if (chkTAXID.Checked == true)
                {
                    dr = TaxFieldsTbl.NewRow();
                    dr["TAX_SRNO"] = HDTAXSRNO.Value;
                    dr["TAXID"] = hdTAXID.Value;


                    TaxFieldsTbl.Rows.Add(dr);
                }
            }


            objStrMst.TaxFieldsTbl_TRAN = TaxFieldsTbl;
            //-------------------------------------------------------



            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (ViewState["Dept_Wise_Item"].ToString() == "1")
                    {
                        duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " count(*)", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND MDNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " '"));
                    }
                    else
                    {
                        duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " count(*)", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " '"));
                    }


                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddItemMaster(objStrMst, Session["userfullname"].ToString(), Convert.ToInt32(ddlItmType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            // int item_no = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " ITEM_NO", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'and item_no <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                            //  int item_no = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "ITEM_NO", "ITEM_NAME='" +txtItemName.Text+"'"));
                            // SaveTaxItem(item_no);
                            clear();
                            objCommon.DisplayMessage(updatePanel1, "Record Saved Successfully", this);
                            if (ViewState["Dept_Wise_Item"].ToString() != "1")
                            {
                                BindListViewItemMaster();
                            }
                            else
                            {
                                BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue));
                            }

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel1, "Record Already exist", this);
                    }
                }
                else
                {
                    if (ViewState["itemNo"] != null)
                    {
                        if (ViewState["Dept_Wise_Item"].ToString() == "1")
                        {
                            duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " count(*)", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "AND MDNO=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'and item_no <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                        }
                        else
                        {
                            duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", " count(*)", "misgno=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue) + "and ITEM_NAME= '" + Convert.ToString(txtItemName.Text) + " 'and item_no <> " + Convert.ToInt32(ViewState["itemNo"].ToString())));
                        }
                        if (duplicateCkeck == 0)
                        {
                            objStrMst.ITEM_NO = Convert.ToInt32(ViewState["itemNo"].ToString());
                            CustomStatus cs = (CustomStatus)objStrMaster.UpdateItemMaster(objStrMst, Session["userfullname"].ToString(), Convert.ToInt32(ddlItmType.SelectedValue));
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                clear();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(updatePanel1, "Record Updated Successfully", this);
                                if (ViewState["Dept_Wise_Item"].ToString() != "1")
                                {
                                    BindListViewItemMaster();
                                }
                                else
                                {
                                    BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue));
                                }

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel1, "Record Already exist", this);
                        }

                    }
                }
            }
        }




        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                objCommon.DisplayMessage(updatePanel1, "Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.butItemMasterSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
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
            ShowEditDetailsTaxItemMaster(Convert.ToInt32(ViewState["itemNo"].ToString()));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.btnEditItemMaster_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsTaxItemMaster(int itemNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordTaxItemMaster(itemNo);
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvTaxFields.DataSource = ds.Tables[0];
                lvTaxFields.DataBind();
                string TAX_STATUS = ds.Tables[0].Rows[0]["TAX_ITEM_STATUS"].ToString();
                if (TAX_STATUS == "1")
                {
                    lvTaxFields.Visible = true;
                    chkTax.Checked = true;
                }
                else
                {
                    lvTaxFields.Visible = false;
                    chkTax.Checked = false;
                }
                for (int i = 0; i < lvTaxFields.Items.Count; i++)
                {

                    CheckBox lblchkTAXID = lvTaxFields.Items[i].FindControl("chkTAXID") as CheckBox;          //lvTaxFields.Items[i].FindControl("chkTAXID") as CheckBox;
                    // if (ds.Tables[0].Rows[i]["TAXID"].ToString().Equals("True"))

                    //  if (ds.Tables[0].Rows[i]["TAX_ITEM_STATUS"].ToString().Equals("True"))
                    string ITEM_STATUS = ds.Tables[0].Rows[i]["TAX_ITEM_STATUS"].ToString();
                    if (ITEM_STATUS == "1")
                    {
                        lblchkTAXID.Checked = true;

                    }
                    else
                    {
                        lblchkTAXID.Checked = false;

                    }

                }
            }
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    BindListViewTax();
            //    chkTax.Checked = true;
            //    for (int i = 0; i < lvTaxFields.Items.Count; i++)
            //    {
            //        CheckBox lblchkTAXID = lvTaxFields.Items[i].FindControl("chkTAXID") as CheckBox;
            //        if (ds.Tables[0].Rows[i]["TAXID"].ToString().Equals(""))
            //        {
            //            lblchkTAXID.Checked = false;

            //        }
            //        else
            //        {
            //            lblchkTAXID.Checked = true;
            //        }
            //    }
            //}
            //else
            //{

            //    chkTax.Checked =false;

            //    lvTaxFields.Visible = false;
            //    lvTaxFields.DataSource = null;
            //    lvTaxFields.DataBind();

            //}

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowEditDetailsItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowEditDetailsItemMaster(int itemNo)
    {
        DataSet ds = null;

        try
        {
            ds = objStrMaster.GetSingleRecordItemMaster(itemNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlItemSubGroup.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();
                txtBudgetQuantity.Text = ds.Tables[0].Rows[0]["ITEM_BUD_QTY"].ToString();
                txtCommonDescriptionOfItem.Text = ds.Tables[0].Rows[0]["ITEM_DETAILS"].ToString();
                txtItemCode.Text = ds.Tables[0].Rows[0]["ITEM_CODE"].ToString().Trim();
                txtItemName.Text = ds.Tables[0].Rows[0]["ITEM_NAME"].ToString();
                txtMaxQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString();
                txtMinQuantity.Text = ds.Tables[0].Rows[0]["ITEM_MIN_QTY"].ToString();
                txtOpeningBalanceQuantity.Text = ds.Tables[0].Rows[0]["ITEM_OB_QTY"].ToString();
                txtOpeningBalanceValue.Text = ds.Tables[0].Rows[0]["ITEM_OB_VALUE"].ToString();
                txtUnit.Text = ds.Tables[0].Rows[0]["ITEM_UNIT"].ToString();
                txtReorderLevel.Text = ds.Tables[0].Rows[0]["ITEM_REORDER_QTY"].ToString();
                ddlItmType.SelectedValue = ds.Tables[0].Rows[0]["ITPNO"].ToString();
                if (ViewState["Dept_Wise_Item"].ToString() == "1")
                {
                    divDept.Visible = true;
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                }

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
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowEditDetailsItemMaster-> " + ex.Message + " " + ex.StackTrace);
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
        //if (ViewState["Dept_Wise_Item"].ToString() == "1")
        //{
        //    BindListViewItemMaster();
        //}
        //else
        //{
        //    BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue));
        //}
    }

    protected void FillGroupNames()
    {
        try
        {
            objCommon.FillDropDownList(ddlItemGroupName, "STORE_MAIN_ITEM_GROUP", "migno", "migname", "", "migname DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.FillGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillItemTypes()
    {
        try
        {
            objCommon.FillDropDownList(ddlItmType, "STORE_ITEMTYPE", "itpno", "itpname", "", "itpname DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.FillGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillSubGroupNames()
    {
        try
        {
            //  objCommon.FillDropDownList(ddlItemSubGroup, "STORE_MAIN_ITEM_SUBGROUP", "misgno", "MISGNAME+ ' [ '+dbo.FN_DESC('GROUP',MIGNO)+' ]' ", "", "misgname");
            objCommon.FillDropDownList(ddlItemSubGroup, "STORE_MAIN_ITEM_SUBGROUP A Inner join STORE_MAIN_ITEM_GROUP B on a.MIGNO=b.MIGNO", "a.misgno", "a.MISGNAME +' [ '+ b.MIGNAME  + ' ] ' ", "", "a.misgname DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.FillSubGroupNames-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void clear()
    {
        ddlItemSubGroup.SelectedValue = "0";
        // ddlDepartment.SelectedValue = "0";
        txtBudgetQuantity.Text = "0";
        txtCommonDescriptionOfItem.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtMaxQuantity.Text = "0";
        txtMinQuantity.Text = "0";
        txtOpeningBalanceQuantity.Text = "0";
        txtOpeningBalanceValue.Text = "0";
        txtUnit.Text = string.Empty;
        txtReorderLevel.Text = "0";
        ddlItmType.SelectedValue = "0";
        ViewState["action"] = "add";
        ViewState["itemNo"] = null;

        lvTaxFields.DataSource = null;
        lvTaxFields.DataBind();
        lvTaxFields.Visible = false;
        chkTax.Checked = false;

    }


    #endregion

    protected void tab_activetabindexchanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }
    protected void btnshowrptItems_Click(object sender, EventArgs e)
    {
        ShowReport("ITEMS MASTER REPORT", "Items_Master_Report.rpt");
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"];
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowReport("ITEMS MASTER REPORT", "Items_Master_Report.rpt");
    }
    protected void btnshowsubgrprpt_Click(object sender, EventArgs e)
    {
        ShowReport("ITEM SUBGRP REPORT", "Item_Subgrp_Report.rpt");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ShowReport("ITEM SUBGRP REPORT", "Item_Subgrp_Report.rpt");
    }
    protected void btnshorptitemgrp_Click(object sender, EventArgs e)
    {
        ShowReport("ITEM GRP MASTER", "Item_grp_Master.rpt");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ShowReport("ITEM GRP MASTER", "Item_grp_Master.rpt");
    }

    // It is used to check the selected Item Sub Group is fixed asset or not to add. 
    protected void ddlItemSubGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            ds = objCommon.FillDropDown("STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO IN ( SELECT MIGNO FROM STORE_MAIN_ITEM_GROUP WHERE ITEM_TYPE='F') AND MISGNO=" + Convert.ToInt32(ddlItemSubGroup.SelectedValue), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //divOB.Visible = false;
                //divOBValue.Visible = false;
                //txtOpeningBalanceQuantity.Text = "0";
                //txtOpeningBalanceValue.Text = "0";
            }
            else
            {
                divOB.Visible = true;
                divOBValue.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void lvItemMaster_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        // HtmlControl thDepartment = (HtmlControl)e.Item.FindControl("thDept");
        HtmlControl tdDepartment = (HtmlControl)e.Item.FindControl("tdDept");
        if (ViewState["Dept_Wise_Item"].ToString() == "1")
        {
            thDept.Visible = true;
            tdDepartment.Visible = true;
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewItemMaster(Convert.ToInt32(ddlDepartment.SelectedValue));//, ViewState["StoreUser"].ToString());
    }
    protected void btnSubmititemsubgroup_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action3"] != null)
            {
                int DepId = 0;
                int SubGrpId = Convert.ToInt32(ddlItemSubgroupdep.SelectedValue);
                decimal DepPercentage = Convert.ToDecimal(txtDepper.Text);
                DateTime DepDate = Convert.ToDateTime(txtDepdate.Text);


                if (ViewState["action3"].ToString().Equals("add"))
                {

                    //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPRECIATION_MISGNO_MAP", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubgroupdep.SelectedValue) + " and DEPR_FROM_DATE='" + Convert.ToDateTime(txtDepdate.Text).ToString("yyyy-MM-dd") + "'"));
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPRECIATION_MISGNO_MAP", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubgroupdep.SelectedValue)));
                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.AddSubGroupDepriciationEntry(DepId, SubGrpId, DepPercentage, DepDate);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            objCommon.DisplayMessage(updatePanel4, "Record Saved Successfully", this);
                            BindListViewDepriciation();
                            ClearDepreciationEntry();

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel4, "Record Already Exist", this);
                    }
                }
                else
                {

                    if (ViewState["DeprId"] != null)
                    {
                        int DepeId = Convert.ToInt32(ViewState["DeprId"]);
                        //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPRECIATION_MISGNO_MAP", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubgroupdep.SelectedValue) + " and DEPR_FROM_DATE='" + Convert.ToDateTime(txtDepdate.Text).ToString("yyyy-MM-dd") + "'AND DEP_MAP_ID!='" + DepeId + "' "));
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPRECIATION_MISGNO_MAP", " count(*)", "MISGNO=" + Convert.ToInt32(ddlItemSubgroupdep.SelectedValue) + " AND DEP_MAP_ID!=" + DepeId));

                        if (duplicateCkeck == 0)
                        {

                            CustomStatus csupd = (CustomStatus)objStrMaster.AddSubGroupDepriciationEntry(DepeId, SubGrpId, DepPercentage, DepDate);
                            if (csupd.Equals(CustomStatus.RecordSaved))
                            {

                                ViewState["action3"] = "add";
                                objCommon.DisplayMessage(updatePanel4, "Record Updated Successfully", this);
                                BindListViewDepriciation();
                                ClearDepreciationEntry();
                                //ddlItemSubgroupdep.Enabled = true;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel4, "Record Already Exist", this);
                        }

                    }


                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation") || ex.Message.Contains("UniqueKeyViolationException"))
            {
                objCommon.DisplayMessage(updatePanel1, "Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.butSubItemSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnCancelitemsubgroup_Click(object sender, EventArgs e)
    {
        ClearDepreciationEntry();
    }
    public void ClearDepreciationEntry()
    {
        ddlItemSubgroupdep.SelectedValue = "0";
        txtDepper.Text = string.Empty;
        txtDepdate.Text = string.Empty;
        ViewState["action3"] = "add";
        //  ddlItemSubgroupdep.Enabled = true;
    }
    protected void btnEditDepreciarion_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            ViewState["DeprId"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action3"] = "edit";
            int checkDepMapId = Convert.ToInt32(objCommon.LookUp("STORE_DEPRECIATION_CALCULATION", "Count(*)", "DEP_MAP_ID=" + Convert.ToInt32(ViewState["DeprId"])));

            if (checkDepMapId > 0)
            {
                objCommon.DisplayMessage(updatePanel4, "Store Depreciation Calculation Already Done", this);
                return;
            }
            else
            {

                ShowEditDetailsDepreciation(Convert.ToInt32(ViewState["DeprId"].ToString()));

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.btnEditDepreciarion_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsDepreciation(int DepId)
    {
        DataSet ds = null;

        try
        {
            //  ds = objStrMaster.GetSingleRecordMainItemGroup(gprNo);
            ds = objCommon.FillDropDown("STORE_DEPRECIATION_MISGNO_MAP D INNER JOIN STORE_MAIN_ITEM_SUBGROUP S ON D.MISGNO=S.MISGNO", "D.DEP_MAP_ID,S.MISGNAME,D.DEPR_PER,D.MISGNO", "CONVERT(VARCHAR,DEPR_FROM_DATE,103) AS DEPR_FROM_DATE", "DEP_MAP_ID=" + DepId, "");
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtDepdate.Text = ds.Tables[0].Rows[0]["DEPR_FROM_DATE"].ToString();
                txtDepper.Text = ds.Tables[0].Rows[0]["DEPR_PER"].ToString();
                ddlItemSubgroupdep.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.ShowEditDetailsDepreciation-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void BindListViewDepriciation()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("STORE_DEPRECIATION_MISGNO_MAP D INNER JOIN STORE_MAIN_ITEM_SUBGROUP S ON D.MISGNO=S.MISGNO", "D.DEP_MAP_ID,D.MISGNO,S.MISGNAME,D.DEPR_PER", "CONVERT(VARCHAR,DEPR_FROM_DATE,103) AS DEPR_FROM_DATE", "", "DEP_MAP_ID DESC");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDepreciation.DataSource = ds;
                lvDepreciation.DataBind();

            }
            else
            {
                lvDepreciation.DataSource = null;
                lvDepreciation.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Item_Master.BindListViewItemMaster-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void DataPager1_PreRender(object sender, EventArgs e)
    {
        BindListViewDepriciation();

    }
}
