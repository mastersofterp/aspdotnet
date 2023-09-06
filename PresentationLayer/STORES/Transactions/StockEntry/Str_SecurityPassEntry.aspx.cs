
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                       
// CREATION DATE : 19-June-2021                                                 
// CREATED BY    : GOPAL ANTHATI                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class STORES_Transactions_StockEntry_Str_SecurityPassEntry : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StrSecurityPassEnt objSecEnt = new StrSecurityPassEnt();
    StrSecurityPassCon objSecCon = new StrSecurityPassCon();

    DataTable dtItemTable = null;
    DataRow datarow = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
        //   ViewState["action"] = "add";


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
                ViewState["Action"] = "Add";
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {

                }

            }
            ViewState["Items"] = null;
            ViewState["dtItem"] = null; ViewState["ClickCount"] = 0;
            FillDropDownList();

            //GenerateSPNumber();
            BindListView();
            txtSpDate.Text = DateTime.Now.ToString();
        }
        //
        divMsg.InnerText = string.Empty;
    }

    private void BindListView()
    {
        DataSet ds = objCommon.FillDropDown("STORE_SEC_PASS_MAIN A INNER JOIN STORE_PARTY B ON (A.PNO=B.PNO)", "SPID,SPDATE,SP_NUMBER", "VEHICLE_NO,DMDATE,A.REMARK,B.PNAME", "", "SPID DESC");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvSecPass.DataSource = ds.Tables[0];
            lvSecPass.DataBind();
            lvSecPass.Visible = true;
        }
        else
        {
            lvSecPass.DataSource = null;
            lvSecPass.DataBind();
            lvSecPass.Visible = false;
        }
    }

    private void GenerateSPNumber()
    {
        DataSet ds = objSecCon.GetSecPassNumber();
        txtSecNumber.Text = ds.Tables[0].Rows[0]["SP_NUMBER"].ToString();
    }

    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlVendor, "STORE_PARTY", "PNO", "PNAME", "", "PNAME");
        ddlPO.Items.Clear();


        //DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_GRN_ITEM) AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE_ITEM)", "PORDNO");
        //DataSet ds = objCommon.FillDropDown("STORE_PORDER", "PORDNO", "REFNO", "STAPPROVAL='A' AND PORDNO NOT IN (SELECT PORDNO FROM STORE_INVOICE)", "PORDNO DESC");


        DataSet ds = objSecCon.GetPODropdown();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
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
    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAddItem.Visible = false;
        divSPNumber.Visible = false;
        string PoNumbers = GetPONumbers();
        DataSet ds1 = objSecCon.GetItemsByPO(PoNumbers);
        lvItem.DataSource = ds1.Tables[0];
        lvItem.DataBind();

        dtItemTable = ds1.Tables[0];
        divlvItem.Visible = true;
        ViewState["dtItem"] = dtItemTable;
        if (ViewState["dtItem"] != null || (DataTable)ViewState["dtItem"] != null)
        {
            AddItemTable();
            for (int i = 0; i < dtItemTable.Rows.Count; i++)
            {
                dtItemTable.Rows[i]["SRNO"] = i + 1;
            }
            ViewState["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
        }
        //dtItemTable.Columns.Remove("REFNO");
        CalItemCount();
        AddPONumber();
        thPOQty.Visible = true;
        if (ddlPO.SelectedValue == "")
            divAddItem.Visible = true;
        else
            divAddItem.Visible = false;
    }
    private string GetPONumbers()
    {
        string PONumber = "";
        string PoValues = string.Empty;
        int count = 0;
        //  degreeNo = hdndegreeno.Value;
        // pnlFeeTable.Update();   9/11/2021
        foreach (ListItem item in ddlPO.Items)
        {
            if (item.Selected == true)
            {
                PONumber += item.Value + '$';
                count = 1;
            }
        }
        if (count > 0)
        {
            PoValues = PONumber.Substring(0, PONumber.Length - 1);
            if (PoValues != "")
            {
                string[] degValue = PoValues.Split('$');

            }
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return PoValues;

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtItemTable = this.CreateIteamTable();

        datarow = null;
        foreach (ListViewItem i in lvItem.Items)
        {
            HiddenField hdnSrNo = i.FindControl("hdnSrNo") as HiddenField;
            Label lblItemCode = i.FindControl("lblItemCode") as Label;
            Label lblItemName = i.FindControl("lblItemName") as Label;
            TextBox lblItemQty = i.FindControl("lblItemQty") as TextBox;
            TextBox lblItemRemark = i.FindControl("lblItemRemark") as TextBox;
            HiddenField hdnItemno = i.FindControl("hdnItemno") as HiddenField;
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;

            datarow = dtItemTable.NewRow();
            datarow["SRNO"] = hdnSrNo.Value;
            datarow["ITEM_NO"] = hdnItemno.Value;
            datarow["ITEM_CODE"] = lblItemCode.Text;
            datarow["ITEM_NAME"] = lblItemName.Text;
            if (lblItemQty.Text == "")
            {
                MessageBox("Please Enter Received Qty For Item Name : " + lblItemName.Text);
                return;
            }
            else if (Convert.ToDecimal(lblItemQty.Text) < 1)
            {
                MessageBox("Received Qty Should Be Greater Than Zero.");
                return;
            }
            datarow["ITEM_QTY"] = lblItemQty.Text == "" ? "0" : lblItemQty.Text;
            datarow["ITEM_REMARK"] = lblItemRemark.Text;
            datarow["PORDNO"] = hdnPordno.Value;

            dtItemTable.Rows.Add(datarow);
        }
        objSecEnt.SEC_PASS_ITEM_TBL = dtItemTable;
        string PoNumber = GetPONumbers();

        if (dtItemTable.Rows.Count == 0)
        {
            MessageBox("Please Add Item Details");
            return;
        }
        //objSecEnt.SEC_PASS_ITEM_TBL = (DataTable)ViewState["dtItem"];
        GenerateSPNumber();
        objSecEnt.SP_NUMBER = txtSecNumber.Text;
        objSecEnt.SPDATE = Convert.ToDateTime(txtSpDate.Text);
        objSecEnt.TIME = Convert.ToDateTime(txtTime.Text).ToString("hh:mm tt");
        objSecEnt.DMDATE = Convert.ToDateTime(txtDMDate.Text);
        objSecEnt.DMNO = txtDMNo.Text;
        objSecEnt.PNO = Convert.ToInt32(ddlVendor.SelectedValue);
        objSecEnt.GATE_KEEPER = txtGateKeeper.Text;
        objSecEnt.INCHARGE = txtInCharge.Text;
        objSecEnt.REMARK = txtRemark.Text;
        objSecEnt.VEHICLE_NO = txtVehicleno.Text;
        objSecEnt.PORDNO = PoNumber;
        objSecEnt.MDNO = Convert.ToInt32(Session["strdeptcode"]);
        objSecEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
        objSecEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);

        if (ViewState["Action"].ToString().Equals("Add"))
        {
            objSecEnt.SPID = 0;

            // int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SEC_PASS_MAIN", "count(*)", "TIME='" + txtTime.Text + "' AND VEHICLE_NO='" + txtVehicleno.Text + "' AND SPDATE='" + Convert.ToDateTime(txtSpDate.Text).ToString("yyyy-MM-dd") + "' AND DMDATE='" + Convert.ToDateTime(txtDMDate.Text).ToString("yyyy-MM-dd") + "' AND PNO='" + ddlVendor.SelectedValue + "' AND DMNO='" + txtDMNo.Text + "' AND GATE_KEEPER='" + txtGateKeeper.Text + "' AND INCHARGE='" + txtInCharge.Text + "' AND REMARK='" + txtRemark.Text + "'"));

            //if (duplicateCkeck == 0)
            //{
            CustomStatus cs = (CustomStatus)objSecCon.InsUpdateSecurityPass(objSecEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved & SP Number Generated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
            // }
            // else
            // {

            //MessageBox("Record Already Exist.");
            // }
        }
        else
        {
            objSecEnt.SPID = Convert.ToInt32(ViewState["SPID"]);

            //int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SEC_PASS_MAIN", "count(*)", "TIME='" + txtTime.Text + "' AND VEHICLE_NO='" + txtVehicleno.Text + "' AND SPDATE='" + Convert.ToDateTime(txtSpDate.Text).ToString("yyyy-MM-dd") + "' AND DMDATE='" + Convert.ToDateTime(txtDMDate.Text).ToString("yyyy-MM-dd") + "' AND PNO='" + ddlVendor.SelectedValue + "' AND DMNO='" + txtDMNo.Text + "' AND GATE_KEEPER='" + txtGateKeeper.Text + "' AND INCHARGE='" + txtInCharge.Text + "' AND REMARK='" + txtRemark.Text + "' AND  SPID!='" + Convert.ToInt32(ViewState["SPID"]) + "'"));

            //if (duplicateCkeck == 0)
            //{
            CustomStatus cs = (CustomStatus)objSecCon.InsUpdateSecurityPass(objSecEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Updated Successfully.");
            }
            else
            {
                MessageBox("Transaction Failed.");
            }
            //}
            //else
            //{

            //    MessageBox("Record Already Exsist");
            //}

        }
        //ClearAll();
        divSPNumber.Visible = true;
        btnSubmit.Enabled = false;
        btnAddNew2.Visible = true;
        btnCancel.Visible = false;

    }
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        divPONum.Visible = false;
        divSPNumber.Visible = false;
        PnlItem.Visible = true;
        divAddItem.Visible = false;
        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
        thPOQty.Visible = false;
    }
    #region AddItem
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        try
        {
            divlvItem.Visible = true;
            if (Convert.ToInt32(txtItemQty.Text) < 1)
            {
                MessageBox("Received Qty Should Be Greater Than Zero.");
                return;
            }
            if (ViewState["dtItem"] != null)
            {
                DataTable dtitemdup = ViewState["dtItem"] as DataTable;

                if (CheckDuplicateItemRow(dtitemdup, ddlItem.SelectedItem.Text.Trim()))
                {
                    lvItem.DataSource = dtitemdup;
                    lvItem.DataBind();
                    ddlItem.SelectedIndex = 0;
                    MessageBox("This Item Is Already Exist.");
                    return;
                }
            }

            int maxVal = 0;
            AddItemTable();
            ViewState["dtItem"] = dtItemTable;
            datarow = dtItemTable.NewRow();

            if (datarow != null)
            {
                maxVal = Convert.ToInt32(dtItemTable.AsEnumerable().Max(row => row["SRNO"]));
            }
            datarow["SRNO"] = maxVal + 1;
            datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
            datarow["ITEM_CODE"] = objCommon.LookUp("STORE_ITEM", "ITEM_CODE", "ITEM_NO=" + ddlItem.SelectedValue);
            datarow["ITEM_NAME"] = ddlItem.SelectedItem.Text;
            datarow["ITEM_QTY"] = Convert.ToDecimal(txtItemQty.Text);
            datarow["ITEM_REMARK"] = txtItemRemark.Text;
            datarow["PORDNO"] = 0;

            dtItemTable.Rows.Add(datarow);

            ViewState["dtItem"] = dtItemTable;
            lvItem.DataSource = dtItemTable;
            lvItem.DataBind();
            CalItemCount();
            ClearItem();
            AddPONumber();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckDuplicateItemRow(DataTable dtitemdup, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtitemdup.Rows)
            {
                if (dr["ITEM_NAME"].ToString().ToLower() == value.ToLower())
                {
                    retVal = true;
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.CheckDuplicateItemRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    private void AddPONumber()
    {
        foreach (ListViewItem lv in lvItem.Items)
        {
            HiddenField hdnPordno = lv.FindControl("hdnPordno") as HiddenField;
            Label lblRefno = lv.FindControl("lblRefno") as Label;
            lblRefno.Text = hdnPordno.Value == "0" ? "" : objCommon.LookUp("STORE_PORDER", "REFNO", "PORDNO=" + hdnPordno.Value);

        }
    }

    private void CalItemCount()
    {
        double ItemQtyCount = 0.0;
        for (int i = 0; i < dtItemTable.Rows.Count; i++)
        {
            ItemQtyCount += Convert.ToDouble(dtItemTable.Rows[i]["ITEM_QTY"].ToString());
        }
        //divItemCount.Visible = true;
        lblItemCount.Text = dtItemTable.Rows.Count.ToString();
        lblItemQtyCount.Text = ItemQtyCount.ToString();
        hdnrowcount.Value = dtItemTable.Rows.Count.ToString();
    }



    private DataTable CreateIteamTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_CODE", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEM_QTY", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ITEM_REMARK", typeof(string)));
        dt.Columns.Add(new DataColumn("PORDNO", typeof(int)));
        return dt;
    }
    private void ClearItem()
    {
        ddlItem.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.GetEditableDatarowFromTOG -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    private void AddItemTable()
    {
        dtItemTable = this.CreateIteamTable();

        datarow = null;
        foreach (ListViewItem i in lvItem.Items)
        {
            HiddenField hdnSrNo = i.FindControl("hdnSrNo") as HiddenField;
            Label lblItemCode = i.FindControl("lblItemCode") as Label;
            Label lblItemName = i.FindControl("lblItemName") as Label;
            TextBox lblItemQty = i.FindControl("lblItemQty") as TextBox;
            TextBox lblItemRemark = i.FindControl("lblItemRemark") as TextBox;
            HiddenField hdnItemno = i.FindControl("hdnItemno") as HiddenField;
            HiddenField hdnPordno = i.FindControl("hdnPordno") as HiddenField;

            datarow = dtItemTable.NewRow();
            datarow["SRNO"] = hdnSrNo.Value;
            datarow["ITEM_NO"] = hdnItemno.Value;
            datarow["ITEM_CODE"] = lblItemCode.Text;
            datarow["ITEM_NAME"] = lblItemName.Text;
            datarow["ITEM_QTY"] = lblItemQty.Text == "" ? "0" : lblItemQty.Text;
            datarow["ITEM_REMARK"] = lblItemRemark.Text;
            datarow["PORDNO"] = hdnPordno.Value;

            dtItemTable.Rows.Add(datarow);

        }
    }

    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["dtItem"] != null && ((DataTable)ViewState["dtItem"]) != null)
            {
                //AddItemTable();
                dtItemTable = (DataTable)ViewState["dtItem"];
                dtItemTable.Rows.Remove(this.GetEditableDatarow(dtItemTable, btnDelete.CommandArgument));
                for (int i = 0; i < dtItemTable.Rows.Count; i++)
                {
                    dtItemTable.Rows[i]["SRNO"] = i + 1;
                }
                ViewState["dtItem"] = dtItemTable;
                lvItem.DataSource = dtItemTable;
                lvItem.DataBind();
                if (dtItemTable.Rows.Count > 0)
                {
                    divlvItem.Visible = true;
                    CalItemCount();
                }
                else
                {
                    divlvItem.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnDeleteRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion
    private void ClearAll()
    {
        txtSpDate.Text = DateTime.Now.ToString();
        // GenerateSPNumber();
        txtTime.Text = string.Empty;
        txtVehicleno.Text = string.Empty;
        txtDMDate.Text = string.Empty;
        txtDMNo.Text = string.Empty;
        ddlVendor.SelectedIndex = 0;
        txtGateKeeper.Text = string.Empty;
        txtInCharge.Text = string.Empty;
        txtRemark.Text = string.Empty;
        FillDropDownList();
        lvItem.DataSource = null;
        lvItem.DataBind();
        divlvItem.Visible = false;
        ViewState["dtItem"] = null;
        ViewState["Items"] = null;
        ViewState["Action"] = "Add";
        ddlSecNumber.Visible = false;
        txtSecNumber.Visible = true;
        PnlItem.Visible = false;
        divAddItem.Visible = true;
        ViewState["SPID"] = null;
        lblItemQtyCount.Text = string.Empty;
        lblItemCount.Text = string.Empty;
        // divItemCount.Visible = false;
        btnAdNew.Visible = false;
        txtSecNumber.Text = string.Empty;
        divSPNumber.Visible = false;
        divPONum.Visible = true;

    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ddlSecNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        divAddItem.Visible = false;
        divPONum.Visible = false;
        ShowDetails(Convert.ToInt32(ddlSecNumber.SelectedValue));
    }

    private void ShowDetails(int Spid)
    {
        DataSet ds = objSecCon.GetSecPassDetailsForEdit(Spid);
        txtSecNumber.Text = ds.Tables[0].Rows[0]["SP_NUMBER"].ToString();
        txtSpDate.Text = ds.Tables[0].Rows[0]["SPDATE"].ToString();
        txtTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["TIME"]).ToString("hh:mm tt");
        txtVehicleno.Text = ds.Tables[0].Rows[0]["VEHICLE_NO"].ToString();
        txtDMDate.Text = ds.Tables[0].Rows[0]["DMDATE"].ToString();
        txtDMNo.Text = ds.Tables[0].Rows[0]["DMNO"].ToString();
        ddlVendor.SelectedValue = ds.Tables[0].Rows[0]["PNO"].ToString();
        txtGateKeeper.Text = ds.Tables[0].Rows[0]["GATE_KEEPER"].ToString();
        txtInCharge.Text = ds.Tables[0].Rows[0]["INCHARGE"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        if (ds.Tables[0].Rows[0]["PORDNO"].ToString() == "" || ds.Tables[0].Rows[0]["PORDNO"].ToString() == "0")
        {
            thPOQty.Visible = false;
            divAddItem.Visible = true;
            divPONum.Visible = false;
        }
        else
        {
            string[] Ponumber = ds.Tables[0].Rows[0]["PORDNO"].ToString().Split('$');
            foreach (string lst in Ponumber)
            {
                if (ddlPO.Items.FindByValue(lst) != null)
                {
                    ddlPO.Items.FindByValue(lst).Selected = true;
                }
            }
            thPOQty.Visible = true;
            divAddItem.Visible = false;
            divAddItem.Visible = true;
        }


        lvItem.DataSource = ds.Tables[1];
        lvItem.DataBind();
        divlvItem.Visible = true;
        ViewState["dtItem"] = ds.Tables[1];
        dtItemTable = ds.Tables[1];
        CalItemCount();
        AddPONumber();
    }

    protected void btnCancelItem_Click(object sender, EventArgs e)
    {
        ClearItem();
    }
    protected void lblItemQty_TextChanged(object sender, EventArgs e)
    {
        double ItemQtyCount = 0.0;
        foreach (ListViewItem i in lvItem.Items)
        {
            TextBox lblItemQty = i.FindControl("lblItemQty") as TextBox;
            ItemQtyCount += Convert.ToDouble(lblItemQty.Text);
        }
        lblItemQtyCount.Text = ItemQtyCount.ToString();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int Spid = Convert.ToInt32(btnEdit.CommandArgument);
        if (Convert.ToInt32(objCommon.LookUp("STORE_GRN_MAIN", "COUNT(*)", "SPID=" + Spid)) > 0)
        {
            MessageBox("GRN Entry Already Submitted For This SP Number.So,You Can Not Modify.");
            return;
        }
        // ClearAll();

        DataSet ds = objSecCon.GetPODropdownForEdit(Spid);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlPO.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }


        ViewState["SPID"] = Spid;
        ViewState["Action"] = "edit"; /// added by nikhil m
        ShowDetails(Spid);
        divSPEntry.Visible = true;
        lvSecPass.Visible = false;
        //btnAdNew.Visible = false;
        divSPNumber.Visible = false;
        btnSubmit.Enabled = true;
        btnCancel.Visible = false;
        btnAddNew2.Visible = true;
        btnAdNew.Visible = false;
        //divAddItem.Visible = false;
    }
    protected void btnAdNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        divSPEntry.Visible = true;
        lvSecPass.Visible = false;
        divSPNumber.Visible = false;
        btnSubmit.Enabled = true;
        btnAddNew2.Visible = false;
        btnCancel.Visible = true;
        divPONum.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClearAll();
        divSPEntry.Visible = false;
        lvSecPass.Visible = true;
        btnAdNew.Visible = true;
        BindListView();
    }

}