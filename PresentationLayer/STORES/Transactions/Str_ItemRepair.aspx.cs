//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_JvStockEntry.aspx                                             
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
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Caching;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class STORES_Transactions_Str_ItemRepair : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_JvStockCon objJVCon = new Str_JvStockCon();
    Str_JvStockEnt objJVEnt = new Str_JvStockEnt();

    DataTable ItemRTbl = null;
    DataRow dtRow = null;
    int OWReturnCount = 0;
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
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                ViewState["Action"] = "Add";
                ViewState["dtItem"] = null;
                BindListView();
                ViewState["IsSecGPEntry"] = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "ISNULL(IS_SEC_GP_ENTRY,0)", ""));

            }
            FillDropDownList();

        }
    }

    private void BindListView()
    {
        //DataSet ds = objCommon.FillDropDown("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM B ON (A.ITEM_NO = B.ITEM_NO)", "IR_ID,ITEM_IN,B.ITEM_NAME,GP_NUMBER", "(CASE WHEN MODIFIED_DATE IS NULL THEN CREATED_DATE ELSE MODIFIED_DATE END) AS TRAN_DATE", "", "TRAN_DATE desc");

        //if (rdlList.SelectedValue == "1")
        //{
        //     ds = objCommon.FillDropDown("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM B ON (A.ITEM_NO = B.ITEM_NO) LEFT JOIN STORE_SEC_PASS_OUTWARD O ON (A.IR_ID = O.IR_ID)", "A.IR_ID, A.ITEM_IN, B.ITEM_NAME, A.GP_NUMBER", "(CASE WHEN A.MODIFIED_DATE IS NULL THEN A.CREATED_DATE ELSE A.MODIFIED_DATE END) AS TRAN_DATE, ISNULL(O.IR_ID,0) AS SEC_OUT_DONE_ID", "", "TRAN_DATE DESC");
        //}
        //else
        //{

        //    //ds = objCommon.FillDropDown("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM B ON (A.ITEM_NO = B.ITEM_NO) INNER JOIN STORE_SEC_PASS_OUTWARD O ON (A.IR_ID = O.IR_ID) inner join STORE_ITEM_REPAIR_TRAN IT on (A.IR_ID = O.IR_ID)", "A.IR_ID, A.ITEM_IN, B.ITEM_NAME, A.GP_NUMBER, 0 as FLAG", "(CASE WHEN A.MODIFIED_DATE IS NULL THEN A.CREATED_DATE ELSE A.MODIFIED_DATE END) AS TRAN_DATE, ISNULL(O.IR_ID,0) AS SEC_OUT_DONE_ID", " IT.IS_RETURN IS NULL", "TRAN_DATE DESC");
        //    //ds = objCommon.FillDropDown("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM B ON (A.ITEM_NO = B.ITEM_NO) INNER JOIN STORE_SEC_PASS_OUTWARD O ON (A.IR_ID = O.IR_ID)", "A.IR_ID, A.ITEM_IN, B.ITEM_NAME, A.GP_NUMBER", "(CASE WHEN A.MODIFIED_DATE IS NULL THEN A.CREATED_DATE ELSE A.MODIFIED_DATE END) AS TRAN_DATE, ISNULL(O.IR_ID,0) AS SEC_OUT_DONE_ID", "", "TRAN_DATE DESC");

        //}
        //Modified By Shabina 29-09-2021


        DataSet ds = null;      //Modified By Shabina 29-09-2021
        int RDL = Convert.ToInt32(rdlList.SelectedValue);
        ds = objJVCon.GetReturnableItemsList(RDL, Convert.ToInt32(ViewState["IsSecGPEntry"]));


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvItemRepairEntry.DataSource = ds.Tables[0];
            lvItemRepairEntry.DataBind();
            divListItemReapir.Visible = true;
            hdnRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            divEmptyLabel.Visible = false;

        }
        else
        {
            lvItemRepairEntry.DataSource = null;
            lvItemRepairEntry.DataBind();
            divListItemReapir.Visible = false;
            divEmptyLabel.Visible = true;
        }
        //if (rdlList.SelectedValue == "1")
        //{
        //    thEditAction.Visible = true;            
        //    thSelAction.Visible = false;           
        //    divAddNew.Visible = true;
        //}
        //else
        //{            
        //    thEditAction.Visible = false;
        //    thSelAction.Visible = true;
        //    divAddNew.Visible = false;
        //}
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
    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1;
        if (Session["usertype"].ToString() != "1")
        {
            test1 = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND APLNO=1");
        }
        else
        {
            test1 = Session["strdeptcode"].ToString();
        }


        // if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        if (test1 == Application["strrefmaindept"].ToString())
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
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

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
    protected void rdlItemIn_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearAll();
        if (rdlItemIn.SelectedItem.Text == "MainStore")
        {
            divCollege.Visible = false;
            divDept.Visible = false;
        }
        else
        {
            divCollege.Visible = true;
            divDept.Visible = true;
        }
        lvDsrItem.DataSource = null;
        lvDsrItem.DataBind();
        divRepairItemDet.Visible = false;
    }

    private void FillDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "COLLEGE_NAME");
            objCommon.FillDropDownList(ddlDept, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");
            //objCommon.FillDropDownList(ddlToDept, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");
            objCommon.FillDropDownList(ddlToDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
            objCommon.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "STORES_Transactions_Str_ItemRepair.FillItems() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        //lvDsrItem.DataSource = null;
        //lvDsrItem.DataBind();
        //divRepairItemDet.Visible = false;
        //divButtons.Visible = false;
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (rdlItemIn.SelectedItem.Text == "College")
        {
            if (ddlCollege.SelectedIndex == 0)
            {
                MessageBox("Please Select College");
                return;
            }
            if (ddlDept.SelectedIndex == 0)
            {
                MessageBox("Please Select Department");
                return;
            }

        }
        if (Convert.ToInt32(ViewState["IsSecGPEntry"]) == 1)
        {
            divOutFieldsConfig.Visible = true;
            divToWhomSent.Visible = true;
            divVehicleNum.Visible = false;
            divOutDate.Visible = false;
            divOutTime.Visible = false;
        }
        else
        {

            divOutFieldsConfig.Visible = true;
            divToWhomSent.Visible = true;
            divVehicleNum.Visible = true;
            divOutDate.Visible = true;
            divOutTime.Visible = true;
        }
        int Misgno = Convert.ToInt32(ddlSubCategory.SelectedValue);
        int ItemNo = Convert.ToInt32(ddlItem.SelectedValue);
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        int DeptNo = Convert.ToInt32(ddlDept.SelectedValue);


        DataSet ds = objJVCon.GetItemsForRepair(Misgno, ItemNo, rdlItemIn.SelectedItem.Text, CollegeNo, DeptNo, ViewState["Action"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDsrItem.DataSource = ds.Tables[0];
            lvDsrItem.DataBind();
            hdnDsrRowCount.Value = ds.Tables[0].Rows.Count.ToString();
            divRepairItemDet.Visible = true;
            divCarryEmpDetails.Visible = true;
            divOWEntryList.Visible = true;
            divDsr.Visible = true;
            divAddItem.Visible = true;
            divButtons.Visible = true;
            btnBackShow.Visible = false;
            divOWReturnbleList.Visible = false;
        }
        else
        {
            MessageBox("No Records Found.");
            return;
            //lvDsrItem.DataSource = null;
            //lvDsrItem.DataBind();
            //divRepairItemDet.Visible = false;
            //divButtons.Visible = false;
            //btnBackShow.Visible = true;
        }
        btnAddNew2.Visible = false;
        if (rdlCarriedEmp.SelectedValue == "1")
        {
            divToDept.Visible = true;
            divToEmp.Visible = true;
            divCarriedEmpName.Visible = false;
            divMobileNo.Visible = false;
        }
        else
        {
            divToDept.Visible = false;
            divToEmp.Visible = false;
            divCarriedEmpName.Visible = true;
            divMobileNo.Visible = true;
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDsrItem.DataSource = null;
        lvDsrItem.DataBind();
        divRepairItemDet.Visible = false;
        divButtons.Visible = false;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDsrItem.DataSource = null;
        lvDsrItem.DataBind();
        divRepairItemDet.Visible = false;
        divButtons.Visible = false;
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvDsrItem.DataSource = null;
        lvDsrItem.DataBind();
        divRepairItemDet.Visible = false;
        divButtons.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdlList.SelectedValue == "1")
            {
                SaveOWItemRepair();
            }
            else
            {
                //Save OW-Returnable Entry
                dtRow = null;
                ItemRTbl = this.CreateItemRTable();

                foreach (ListViewItem lv in lvOWReturnble.Items)
                {
                    CheckBox chkOWRDsrselect = lv.FindControl("chkOWRDsrselect") as CheckBox;
                    Label lblOWRDsrNumber = lv.FindControl("lblOWRDsrNumber") as Label;
                    Label txtOWRNatureOfComplaint = lv.FindControl("txtOWRNatureOfComplaint") as Label;
                    TextBox txtRepairCost = lv.FindControl("txtRepairCost") as TextBox;
                    HiddenField hdnOWDSRId = lv.FindControl("hdnOWDSRId") as HiddenField;

                    if (chkOWRDsrselect.Checked)
                    {
                        OWReturnCount++;
                        dtRow = ItemRTbl.NewRow();
                        dtRow["DSR_NUMBER"] = lblOWRDsrNumber.Text;
                        dtRow["COMPLAINT_NATURE"] = txtOWRNatureOfComplaint.Text;
                        //   dtRow["REPAIR_COST"] = txtRepairCost.Text == "" ? 0 : Convert.ToDouble(txtRepairCost.Text);
                        if (txtRepairCost.Text == string.Empty || txtRepairCost.Text == "0")
                        {
                            MessageBox("Please Enter Repair Cost For Item : " + lblOWRDsrNumber.Text.Trim());
                            return;
                        }

                        else
                        {
                            dtRow["REPAIR_COST"] = Convert.ToDouble(txtRepairCost.Text);
                        }

                        dtRow["INVDINO"] = Convert.ToInt32(hdnOWDSRId.Value);

                        ItemRTbl.Rows.Add(dtRow);
                    }
                }
                if (OWReturnCount == 0)
                {
                    MessageBox("Please Add At Least One Item.");
                    return;
                }
            }
            if (ViewState["Action"].ToString() == "Add")
            {

                if (ItemRTbl.Rows.Count == 0)
                {
                    MessageBox("Please Add At Least One Item.");
                    return;
                }
            }
            objJVEnt.TRAN_TYPE = Convert.ToInt16(rdlList.SelectedValue);
            objJVEnt.ITEM_IN = rdlItemIn.SelectedItem.Text;
            objJVEnt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objJVEnt.DEPT_NO = Convert.ToInt32(ddlDept.SelectedValue);
            objJVEnt.ITEM_NO = Convert.ToInt32(ddlItem.SelectedValue);
            objJVEnt.TO_DEPT = Convert.ToInt32(ddlToDept.SelectedValue);
            objJVEnt.TO_EMPLOYEE = Convert.ToInt32(ddlToEmployee.SelectedValue);
            if (txtReturnDate.Text != "")
                objJVEnt.RETURN_DATE = Convert.ToDateTime(txtReturnDate.Text);

            objJVEnt.EMP_FROM = Convert.ToInt32(rdlCarriedEmp.SelectedValue);
            objJVEnt.CARRY_EMP_NAME = txtCarriedEmpName.Text;
            objJVEnt.SENT_TO = txtSentTo.Text;
            objJVEnt.CARRY_EMP_MBL_NO = txtMobileNo.Text;

            objJVEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
            objJVEnt.MODIFIED_BY = Convert.ToInt32(Session["userno"]);
            objJVEnt.ITEM_REPAIR_TBL = ItemRTbl;

            objJVEnt.VEHICLE_NO = txtVehicleNo.Text;
            if (txtOutDate.Text == string.Empty)
                objJVEnt.OUT_DATE = DateTime.MinValue;
            else
                objJVEnt.OUT_DATE = Convert.ToDateTime(txtOutDate.Text);

            if (txtOutTime.Text == string.Empty)
                objJVEnt.OUT_TIME = "";
            else
                objJVEnt.OUT_TIME = Convert.ToDateTime(txtOutTime.Text).ToString("hh:mm tt");

            if (txtReceiveDate.Text == string.Empty)
                objJVEnt.RECEIVED_DATE = DateTime.MinValue;
            else
                objJVEnt.RECEIVED_DATE = Convert.ToDateTime(txtReceiveDate.Text);

            if (txtReceiveTime.Text == string.Empty)
                objJVEnt.RECEIVED_TIME = string.Empty;
            else
                objJVEnt.RECEIVED_TIME = Convert.ToDateTime(txtReceiveTime.Text).ToString("hh:mm tt");


            if (ViewState["Action"].ToString() == "Add")
            {
                objJVEnt.IR_ID = 0;
                GetGatePassNo();
                CustomStatus cs = (CustomStatus)objJVCon.InsUpdateItemRepair(objJVEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    MessageBox("Record Saved & Gate Pass Number Generated Successfully.");
                }
                else
                {
                    MessageBox("Transaction Failed.");
                }
            }
            else
            {
                objJVEnt.IR_ID = Convert.ToInt32(ViewState["IR_ID"]);
                objJVEnt.GATEPASS_NO = txtGatePassNum.Text;
                CustomStatus cs = (CustomStatus)objJVCon.InsUpdateItemRepair(objJVEnt);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    MessageBox("Record Updated Successfully.");
                    //lvDsrItem.Visible = false;  //(28-04-2022)
                }
                else
                {
                    MessageBox("Transaction Failed.");
                }
            }
            divGatePassNum.Visible = true;
            btnSubmit.Enabled = false;
            btnAddNew2.Visible = true;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Str_ItemRepair.butSubmit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }



    private void SaveOWItemRepair()
    {
        dtRow = null;
        ItemRTbl = this.CreateItemRTable();

        foreach (ListViewItem lv in lvAddDsr.Items)
        {
            Label lblDsrNumber = lv.FindControl("lblAddDsrNumber") as Label;
            Label txtNatureOfComplaint = lv.FindControl("txtAddNatureOfComplaint") as Label;
            HiddenField hdnDSRIdNo = lv.FindControl("hdnDSRId") as HiddenField;

            dtRow = ItemRTbl.NewRow();
            dtRow["DSR_NUMBER"] = lblDsrNumber.Text;
            dtRow["COMPLAINT_NATURE"] = txtNatureOfComplaint.Text;
            dtRow["REPAIR_COST"] = 0;
            dtRow["INVDINO"] = Convert.ToInt32(hdnDSRIdNo.Value);

            ItemRTbl.Rows.Add(dtRow);
        }
    }

    private void GetGatePassNo()
    {
        DataSet ds = objJVCon.GenrateGatePassNo();
        txtGatePassNum.Text = ds.Tables[0].Rows[0]["GATEPASS_NUMBER"].ToString();
        objJVEnt.GATEPASS_NO = txtGatePassNum.Text;
    }

    private void ClearAll()
    {
        //rdlItemIn.SelectedValue = "1";
        ddlSubCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        lvDsrItem.DataSource = null;
        lvDsrItem.DataBind();
        lvAddDsr.DataSource = null;
        lvAddDsr.DataBind();
        divRepairItemDet.Visible = false;
        divAddDsrList.Visible = false;
        ddlToDept.SelectedIndex = 0;
        ddlToEmployee.SelectedIndex = 0;
        txtReturnDate.Text = string.Empty;
        ViewState["Action"] = "Add";
        divGatePassNum.Visible = false;
        divButtons.Visible = false;
        btnBackShow.Visible = true;
        txtCarriedEmpName.Text = string.Empty;
        txtMobileNo.Text = string.Empty;
        txtSentTo.Text = string.Empty;
        txtVehicleNo.Text = string.Empty;
        txtOutTime.Text = string.Empty;
        txtOutDate.Text = string.Empty;
        divOutFieldsConfig.Visible = false;

        rdlCarriedEmp.SelectedValue = "1";   //Shaikh Juned(27-04-2022)
        divToDept.Visible = false;
        divToEmp.Visible = false;
        divCarriedEmpName.Visible = true;
        divMobileNo.Visible = true;
        ddlToDept.SelectedIndex = 0;
        ddlToEmployee.SelectedIndex = 0;

        rdlItemIn.Enabled = true;
        ddlCollege.Enabled = true;
        ddlDept.Enabled = true;
        ddlSubCategory.Enabled = true;
        ddlItem.Enabled = true;
        txtReceiveDate.Text = string.Empty;
        txtReceiveTime.Text = string.Empty;
        rdlItemIn.Enabled = true;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    private void AddItemRepair()
    {
        dtRow = null;
        ItemRTbl = this.CreateItemRTable();

        foreach (ListViewItem lv in lvAddDsr.Items)
        {
            Label lblDsrNumber = lv.FindControl("lblAddDsrNumber") as Label;
            Label txtNatureOfComplaint = lv.FindControl("txtAddNatureOfComplaint") as Label;
            HiddenField hdnDSRIdNo = lv.FindControl("hdnDSRId") as HiddenField;
            if (ItemRTbl.Rows.Count > 0)
            {


                //if (ItemRTbl.Rows[]["INVDINO"] == hdnDSRIdNo)
                //{
                //    MessageBox("Item Already Axist in the List.");
                //    return;

                //}


            }
            dtRow = ItemRTbl.NewRow();
            dtRow["DSR_NUMBER"] = lblDsrNumber.Text;
            dtRow["COMPLAINT_NATURE"] = txtNatureOfComplaint.Text;
            dtRow["REPAIR_COST"] = 0;
            dtRow["INVDINO"] = Convert.ToInt32(hdnDSRIdNo.Value);
            ItemRTbl.Rows.Add(dtRow);
        }
        //int Count = 0;
        foreach (ListViewItem lv in lvDsrItem.Items)
        {
            CheckBox chkDsrselect = lv.FindControl("chkDsrselect") as CheckBox;
            Label lblDsrNumber = lv.FindControl("lblDsrNumber") as Label;
            TextBox txtNatureOfComplaint = lv.FindControl("txtNatureOfComplaint") as TextBox;
            HiddenField hdnDSRListId = lv.FindControl("hdnDSRListId") as HiddenField;
            if (chkDsrselect.Checked)
            {
                if (ItemRTbl.Rows.Count > 0)
                {
                    for (int i = 0; i < ItemRTbl.Rows.Count; i++)
                    {
                        string dsrnum = ItemRTbl.Rows[i]["DSR_NUMBER"].ToString();
                        if (dsrnum == lblDsrNumber.Text)
                        {
                            MessageBox("Item Already Exist in the List.");
                            return;

                        }
                        if (chkDsrselect.Checked == false)
                        {
                            MessageBox("Please Select Atlest One Item");
                            return;
                        }
                    }
                }

                dtRow = ItemRTbl.NewRow();
                dtRow["DSR_NUMBER"] = lblDsrNumber.Text;
                dtRow["COMPLAINT_NATURE"] = txtNatureOfComplaint.Text;
                dtRow["REPAIR_COST"] = 0;
                dtRow["INVDINO"] = Convert.ToInt32(hdnDSRListId.Value);
                ItemRTbl.Rows.Add(dtRow);
                //Count++;
            }
        }
        //if (Count == 0)
        //{
        //    MessageBox("Please Select Atleast One Item.");
        //    return;
        //}
        lvAddDsr.DataSource = ItemRTbl;
        lvAddDsr.DataBind();
        hdnAddedDsrRowCount.Value = ItemRTbl.Rows.Count.ToString();
        divAddDsrList.Visible = true;
        ViewState["dtItem"] = ItemRTbl;

    }

    private DataTable CreateItemRTable()
    {
        ItemRTbl = new DataTable();
        ItemRTbl.Columns.Add("DSR_NUMBER", typeof(string));
        ItemRTbl.Columns.Add("COMPLAINT_NATURE", typeof(string));
        ItemRTbl.Columns.Add("REPAIR_COST", typeof(double));
        ItemRTbl.Columns.Add("INVDINO", typeof(int));
        return ItemRTbl;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BindListView();
        divItemRepair.Visible = false;
        pnlAddNew.Visible = true;
        divRadioList.Visible = true;
        btnAddNew.Visible = true;
        divButtons.Visible = false;
        rdlCarriedEmp.SelectedValue = "1"; //Shaikh Juned(27-04-2022)
        divToDept.Visible = false;
        divToEmp.Visible = false;
        divCarriedEmpName.Visible = true;
        divMobileNo.Visible = true;
        ddlToDept.SelectedIndex = 0;
        ddlToEmployee.SelectedIndex = 0;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton img = sender as ImageButton;
        int IrId = Convert.ToInt32(img.CommandArgument);

        //int Count = Convert.ToInt32(objCommon.LookUp("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM_REPAIR_TRAN B ON (A.IR_ID=B.IR_ID)", "COUNT(*)", "IS_RETURN = 'Y' AND A.IR_ID=" + IrId));
        if (Convert.ToInt32(img.ToolTip) > 0)
        {
            MessageBox("Some Items Received Against This Gate Pass Number,So You Can Not Modify");
            return;
        }

        ViewState["IR_ID"] = IrId;
        DataSet ds = objJVCon.GetItemRepairDetailsForEdit(IrId);
        ClearAll();

        FillDropDownList();
        objCommon.FillDropDownList(ddlToEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "", "FNAME");//SUBDEPTNO=" + ddlToDept.SelectedValue
        ViewState["Action"] = "Edit";
        pnlAddNew.Visible = false;
        btnSubmit.Enabled = true;

        txtGatePassNum.Text = ds.Tables[0].Rows[0]["GP_NUMBER"].ToString();
        ddlSubCategory.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();

        txtVehicleNo.Text = ds.Tables[0].Rows[0]["IR_VEHICLE_NO"].ToString();
        txtOutDate.Text = ds.Tables[0].Rows[0]["IR_OUT_DATE"].ToString();
        if (ds.Tables[0].Rows[0]["IR_OUT_TIME"].ToString() != "")
            txtOutTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["IR_OUT_TIME"]).ToString("hh:mm tt");

        if (Convert.ToInt32(ViewState["IsSecGPEntry"]) == 1)
        {
            divOutFieldsConfig.Visible = true;
            divToWhomSent.Visible = true;
            divVehicleNum.Visible = false;
            divOutDate.Visible = false;
            divOutTime.Visible = false;
        }
        else
        {
            divOutFieldsConfig.Visible = true;
            divToWhomSent.Visible = true;
            divVehicleNum.Visible = true;
            divOutDate.Visible = true;
            divOutTime.Visible = true;
        }

        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        ddlItem.SelectedValue = ds.Tables[0].Rows[0]["ITEM_NO"].ToString();

        divGatePassNum.Visible = true;
        rdlItemIn.SelectedValue = ds.Tables[0].Rows[0]["ITEM_IN"].ToString() == "MainStore" ? "1" : "2";
        if (ds.Tables[0].Rows[0]["ITEM_IN"].ToString() == "MainStore")
        {
            divCollege.Visible = false;
            divDept.Visible = false;
        }
        else
        {
            divCollege.Visible = true;
            divDept.Visible = true;
        }

        ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
        ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPT_NO"].ToString();

        rdlCarriedEmp.SelectedValue = ds.Tables[0].Rows[0]["EMP_FROM"].ToString();
        if (ds.Tables[0].Rows[0]["EMP_FROM"].ToString() == "1")
        {
            ddlToDept.SelectedValue = ds.Tables[0].Rows[0]["CARRY_EMP_DEPTNO"].ToString();
            ddlToEmployee.SelectedValue = ds.Tables[0].Rows[0]["CARRY_EMP_IDNO"].ToString();
            divToDept.Visible = true;
            divToEmp.Visible = true;
            divCarriedEmpName.Visible = false;
            divMobileNo.Visible = false;
        }
        else
        {
            txtCarriedEmpName.Text = ds.Tables[0].Rows[0]["CARRY_EMP_NAME"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["CARRY_EMP_MBL_NO"].ToString();
            divCarriedEmpName.Visible = true;
            divMobileNo.Visible = true;
            divToDept.Visible = false;
            divToEmp.Visible = false;
        }


        txtSentTo.Text = ds.Tables[0].Rows[0]["SENT_TO"].ToString();

        txtReturnDate.Text = ds.Tables[0].Rows[0]["RETURN_DATE"].ToString();
        //btnShow_Click(sender, e);
        divListItemReapir.Visible = false;
        btnAddNew.Visible = false;
        divItemRepair.Visible = true;

        btnShow_Click(sender, e);

        if (ds.Tables[1].Rows.Count > 0)
        {
            lvAddDsr.DataSource = ds.Tables[1];
            lvAddDsr.DataBind();
            hdnAddedDsrRowCount.Value = ds.Tables[1].Rows.Count.ToString();
            ViewState["dtItem"] = ds.Tables[1];

            divOWEntryList.Visible = true;
            divAddDsrList.Visible = true;
            divRepairItemDet.Visible = true;
            //divAddItem.Visible = false;
            divButtons.Visible = true;
            btnBackShow.Visible = false;
        }
        else
        {
            lvAddDsr.DataSource = null;
            lvAddDsr.DataBind();

            divOWEntryList.Visible = false;
            divAddDsrList.Visible = false;
            divRepairItemDet.Visible = false;
            //divAddItem.Visible = true;
            divButtons.Visible = false;
            btnBackShow.Visible = true;

        }

        btnCancel.Visible = false;
        btnAddNew2.Visible = false;
        rdlItemIn.Enabled = false;
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        //ClearAll();
        GetGatePassNo();
        btnAddNew.Visible = false;
        divListItemReapir.Visible = false;
        divItemRepair.Visible = true;
        divButtons.Visible = false;
        pnlAddNew.Visible = false;
        divShowBack.Visible = true;
        btnSubmit.Enabled = true;
        rdlItemIn.SelectedValue = "1";
        rdlItemIn_SelectedIndexChanged(sender, e);
        divReceiveFieldsConfig.Visible = false;     //Shaikh Juned(28-04-2022)
        btnCancel.Visible = true;
        rdlItemIn.Enabled = true;
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (rdlList.SelectedValue == "2")
        {
            foreach (ListViewDataItem lv in lvOWReturnble.Items)
            {
                CheckBox chkOWRDsrselect = (CheckBox)lv.FindControl("chkOWRDsrselect");
                TextBox txtRepairCost = (TextBox)lv.FindControl("txtRepairCost");
                chkOWRDsrselect.Checked = false;
                txtRepairCost.Text = string.Empty;
            }
        }
        else
        {
            ClearAll();
            divShowBack.Visible = true;
        }
    }
    protected void rdlList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnSelectList.Value = rdlList.SelectedValue;
        if (rdlList.SelectedValue == "2")
        {
            divAddNew.Visible = false;
            thEditAction.Visible = false;
            thSelAction.Visible = true;
            if (Convert.ToInt32(ViewState["IsSecGPEntry"]) == 1)
                lblhead.Text = "Security Outward-Returnable Entry List";
            else
                lblhead.Text = "Outward Item Entry List";
            // btnSelect.enabled = false;
        }
        else
        {
            divAddNew.Visible = true;
            thEditAction.Visible = true;
            thSelAction.Visible = false;
            lblhead.Text = "Item Repair Entry List";
        }
        BindListView();

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int IrId = Convert.ToInt32(btn.CommandArgument);

        //DataSet ds = objJVCon.GetItemRepairDetails(IrId);


        //   int Count = Convert.ToInt32(objCommon.LookUp("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM_REPAIR_TRAN B ON (A.IR_ID=B.IR_ID)", "COUNT(*)", "IS_RETURN = 'Y' AND A.IR_ID=" + IrId));
        //int Count = Convert.ToInt32(objCommon.LookUp("STORE_ITEM_REPAIR_MAIN A INNER JOIN STORE_ITEM_REPAIR_TRAN B ON (A.IR_ID=B.IR_ID)", "COUNT(*)", "IS_RETURN IS NULL AND A.IR_ID=" + IrId));
        //if (Count == 0)
        //{
        //    MessageBox("Some Items Returned Against This Gate Pass Number,So You Can Not Modify");
        //    return;
        //}



        ViewState["IR_ID"] = IrId;
        DataSet ds = objJVCon.GetItemRepairDetailsOw_Ret(IrId);
        //if (ds.Tables[1].Rows.Count == 0)
        //{
        //    foreach (ListViewItem lv in lvItemRepairEntry.Items)
        //    {
        //        Button btnSelect = lv.FindControl("btnSelect") as Button;
        //        btnSelect.Enabled = false;
        //    }

        //    MessageBox("Already Saved Item Can not be Edit");
        //    return;

        //}

        ClearAll();
        btnSubmit.Enabled = true;
        FillDropDownList();
        objCommon.FillDropDownList(ddlToEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "", "FNAME");//SUBDEPTNO=" + ddlToDept.SelectedValue
        ViewState["Action"] = "Edit";

        txtGatePassNum.Text = ds.Tables[0].Rows[0]["GP_NUMBER"].ToString();
        divGatePassNum.Visible = true;
        rdlItemIn.SelectedValue = ds.Tables[0].Rows[0]["ITEM_IN"].ToString() == "MainStore" ? "1" : "2";
        ddlSubCategory.SelectedValue = ds.Tables[0].Rows[0]["MISGNO"].ToString();
        objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");
        ddlItem.SelectedValue = ds.Tables[0].Rows[0]["ITEM_NO"].ToString();
        ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
        ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPT_NO"].ToString();
        rdlCarriedEmp.SelectedValue = ds.Tables[0].Rows[0]["EMP_FROM"].ToString();
        if (ds.Tables[0].Rows[0]["EMP_FROM"].ToString() == "1")
        {
            ddlToDept.SelectedValue = ds.Tables[0].Rows[0]["CARRY_EMP_DEPTNO"].ToString();
            ddlToEmployee.SelectedValue = ds.Tables[0].Rows[0]["CARRY_EMP_IDNO"].ToString();
            //divToDept.Visible = true;
            //divToEmp.Visible = true;
            //divCarriedEmpName.Visible = false;
            //divMobileNo.Visible = false;
        }
        else
        {
            txtCarriedEmpName.Text = ds.Tables[0].Rows[0]["CARRY_EMP_NAME"].ToString();
            txtMobileNo.Text = ds.Tables[0].Rows[0]["CARRY_EMP_MBL_NO"].ToString();
            //divCarriedEmpName.Visible = true;
            //divMobileNo.Visible = true;
            //divToDept.Visible = false;
            //divToEmp.Visible = false;
        }
        txtVehicleNo.Text = ds.Tables[0].Rows[0]["IR_VEHICLE_NO"].ToString();
        txtOutDate.Text = ds.Tables[0].Rows[0]["IR_OUT_DATE"].ToString();
        if (ds.Tables[0].Rows[0]["IR_OUT_TIME"].ToString() != "")
            txtOutTime.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["IR_OUT_TIME"]).ToString("hh:mm tt");

        //if (Convert.ToInt32(ViewState["IsSecGPEntry"]) == 1)
        //{
        //    divOutFieldsConfig.Visible = false;
        //}
        //else
        //{
        //    divOutFieldsConfig.Visible = true;
        //}
        if (Convert.ToInt32(ViewState["IsSecGPEntry"]) == 1)
        {
            divReceiveFieldsConfig.Visible = false;
        }
        else
        {
            divReceiveFieldsConfig.Visible = true;
        }
        txtSentTo.Text = ds.Tables[0].Rows[0]["SENT_TO"].ToString();
        txtReturnDate.Text = ds.Tables[0].Rows[0]["RETURN_DATE"].ToString();
        //btnShow_Click(sender, e);
        divListItemReapir.Visible = false;
        btnAddNew.Visible = false;
        divItemRepair.Visible = true;
        divCarryEmpDetails.Visible = false;
        divAddItem.Visible = false;
        divOWEntryList.Visible = false;

        rdlItemIn.Enabled = false;
        ddlCollege.Enabled = false;
        ddlDept.Enabled = false;
        ddlSubCategory.Enabled = false;
        ddlItem.Enabled = false;


        if (ds.Tables[1].Rows.Count > 0)
        {
            lvOWReturnble.DataSource = ds.Tables[1];
            lvOWReturnble.DataBind();


            divRepairItemDet.Visible = true;
            divOWEntryList.Visible = true;
            divOWReturnbleList.Visible = true;
            divShowBack.Visible = false;
            divButtons.Visible = true;
            pnlAddNew.Visible = false;

        }
        else
        {
            lvDsrItem.DataSource = null;
            lvDsrItem.DataBind();
            divOWReturnbleList.Visible = false;
        }

        btnAddNew2.Visible = false;
        btnCancel.Visible = true;
    }
    protected void lvItemRepairEntry_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //foreach (ListViewItem lv in lvItemRepairEntry.Items)
        //{

        //    HtmlControl tdEditAction = (HtmlControl)e.Item.FindControl("tdEditAction");
        //    HtmlControl tdSelAction = (HtmlControl)e.Item.FindControl("tdSelAction");
        //    if (rdlList.SelectedValue == "1")
        //    {
        //        thEditAction.Visible = true;
        //        //tdEditAction.Visible = true;
        //        thSelAction.Visible = false;
        //        //tdSelAction.Visible = false;
        //        divAddNew.Visible = true;
        //    }
        //    else
        //    {
        //        //tdEditAction.Visible = false;
        //       // tdSelAction.Visible = true;
        //        thEditAction.Visible = false;
        //        thSelAction.Visible = true;
        //        divAddNew.Visible = false;
        //    }

        //}
    }
    protected void rdlCarriedEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlCarriedEmp.SelectedValue == "1")
        {
            divToDept.Visible = true;
            divToEmp.Visible = true;
            divCarriedEmpName.Visible = false;
            divMobileNo.Visible = false;
            txtCarriedEmpName.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
        }
        else
        {
            divToDept.Visible = false;
            divToEmp.Visible = false;
            divCarriedEmpName.Visible = true;
            divMobileNo.Visible = true;
            ddlToDept.SelectedIndex = 0;
            ddlToEmployee.SelectedIndex = 0;

        }



    }

    protected void btnAdditem_Click(object sender, EventArgs e)
    {
        AddItemRepair();
    }
    protected void btnDeleteItem_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["dtItem"] != null && ((DataTable)ViewState["dtItem"]) != null)
            {
                //AddItemRepair();
                ItemRTbl = ViewState["dtItem"] as DataTable;
                ItemRTbl.Rows.Remove(this.GetEditableDatarow(ItemRTbl, btnDelete.CommandArgument));
                //for (int i = 0; i < ItemRTbl.Rows.Count; i++)
                //{
                //    ItemRTbl.Rows[i]["ITEM_SRNO"] = i + 1;
                //}
                //Session["dtItem"] = ItemRTbl;
                lvAddDsr.DataSource = ItemRTbl;
                lvAddDsr.DataBind();
                hdnAddedDsrRowCount.Value = ItemRTbl.Rows.Count.ToString();
                divAddDsrList.Visible = true;
                //CalItemCount();
                MessageBox("Item Deleted Successfully");  //Shaikh Juned (27-04-2022) add confirm altert in btnDeleteItem
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

    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DSR_NUMBER"].ToString() == value)
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
    protected void ddlToDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlToEmployee, "PAYROLL_EMPMAS", "IDNO", "ISNULL(TITLE,'')+' '+ISNULL(FNAME,'')+' '+ISNULL(MNAME,'')+' '+ISNULL(LNAME,'') AS NAME", "SUBDEPTNO=" + ddlToDept.SelectedValue, "FNAME");
    }
}