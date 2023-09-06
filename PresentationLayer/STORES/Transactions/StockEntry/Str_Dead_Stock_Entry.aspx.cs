
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                                       
// CREATION DATE : 21-Oct-2022                                                
// CREATED BY    : Shaikh Juned                                                    
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
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;

public partial class STORES_Transactions_StockEntry_Str_Dead_Stock_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_SerialNumberGenController strSerial = new Str_SerialNumberGenController();
    Str_SerialNumber objLM = new Str_SerialNumber();
    DataTable dtItemTable = null;
    DataRow datarow = null;
    int dupItemFlag = 0;
    int SaveItemFlag = 0;
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

                }

            }
            ViewState["Items"] = null;
            Session["dtItem"] = null;
            ViewState["Action"] = "Add";
           // divauto.Visible = false;
            objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
            BindListView();
            PnlitemDetail.Visible = true;
            divauto.Visible = false;
        }
        //
        divMsg.InnerText = string.Empty;
        BindListView();
        //PnlitemDetail.Visible = true;
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
  
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        //PnlItem.Visible = true;
        //divAddItem.Visible = false;
        //objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");

    }
    #region AddItem

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        try
        {
            ViewState["Edit_item"] = null;
            pnlitems.Visible = true;
            PnlItem.Visible = true;
            lvitems.Visible = true;

            //-----------16-11-2022------
            int result = 0;
            bool isValidInt = int.TryParse(txtItemQty.Text, out result);
            if (isValidInt==false)
            {
                objCommon.DisplayMessage("Please Enter Qty In Number Format", this);
                return;
            }

            bool isValidInt1 = int.TryParse(txtRate.Text, out result);
            if (isValidInt1 == false)
            {
                objCommon.DisplayMessage("Please Rate Enter In Number Format", this);
                return;
            }

        
            //----------------
            SaveItemFlag = 1;
            DataTable dtItemDup = (DataTable)Session["dtItem"];
            int maxVal = 0;
            AddItemTable();
            if (dtItemTable != null)
            {

                    int Qty = Convert.ToInt32(txtItemQty.Text);
                    for (int i = 0; i < Qty; i++)
                    {
                        if (dupItemFlag == 1)
                        {
                            return;
                        }

                        Session["dtItem"] = dtItemTable;
                        datarow = dtItemTable.NewRow();

                        if (datarow != null)
                        {
                            maxVal = Convert.ToInt32(dtItemTable.AsEnumerable().Max(row => row["ITEM_SRNO"]));
                        }
                        datarow["ITEM_SRNO"] = maxVal + 1;
                        datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
                        datarow["ITEM_NAME"] = ddlItem.SelectedItem.Text;
                        datarow["ITEM_RATE"] = txtRate.Text;
                        datarow["DSR_NUMBER"] = "";
                        datarow["TECH_SPEC"] = "";
                        datarow["QUALITY_QTY_SPEC"] = "";
                        datarow["DEPR_CAL_START_DATE"] = DBNull.Value;

                        datarow["DSTK_ENTRY_ID"] = DBNull.Value;


                        dtItemTable.Rows.Add(datarow);
                        Session["dtItem"] = dtItemTable;
                        lvitems.DataSource = dtItemTable;
                        lvitems.DataBind();
                    }
                    foreach (ListViewItem i in lvitems.Items)
                    {
                        DropDownList ddlDept = i.FindControl("ddldept") as DropDownList;
                        DropDownList ddllocation = i.FindControl("ddllocation") as DropDownList;
                        objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                        objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
                    }
            }
             lvitems.Visible = true;
             divauto.Visible = false;
             PnlitemDetail.Visible = false;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable CreateItemTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPR_CAL_START_DATE", typeof(DateTime)));


        dt.Columns.Add(new DataColumn("DSTK_ENTRY_ID", typeof(DateTime)));
        return dt;
    }
    private void ClearItem()
    {
       ddlItem.SelectedIndex = 0;
        txtItemQty.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtIssueDate.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_SRNO"].ToString() == value)
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
        dtItemTable = this.CreateItemTable();

        datarow = null;
    }

   

    #endregion

  

    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_QUENTITY", typeof(int)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPARTMENT", typeof(int)));
        dt.Columns.Add(new DataColumn("LOCATION", typeof(int)));
        return dt;
    }


    public void DataTable()
    {
        //----------------Data Table-----------------//

        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(int)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPARTMENT", typeof(int)));
        dt.Columns.Add(new DataColumn("LOCATION", typeof(int)));

        DataRow dr1 = null;
        foreach (ListViewItem ii in lvitems.Items)
        {

            System.Web.UI.WebControls.Label lblitemSrNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblitemSrNo");
            System.Web.UI.WebControls.Label lblitemNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblitemNo");
            System.Web.UI.WebControls.Label lblItemName = (System.Web.UI.WebControls.Label)ii.FindControl("lblItemName");
            System.Web.UI.WebControls.Label lblQty = (System.Web.UI.WebControls.Label)ii.FindControl("lblQty");
            System.Web.UI.WebControls.Label lblItemRate = (System.Web.UI.WebControls.Label)ii.FindControl("lblItemRate");
            System.Web.UI.WebControls.TextBox txtSerialNo = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtSerialNo");
            System.Web.UI.WebControls.TextBox txtSpecification = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtSpecification");
            System.Web.UI.WebControls.TextBox txtQtySpec = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtQtySpec");
            System.Web.UI.WebControls.DropDownList ddldept = (System.Web.UI.WebControls.DropDownList)ii.FindControl("ddldept");
            System.Web.UI.WebControls.DropDownList ddllocation = (System.Web.UI.WebControls.DropDownList)ii.FindControl("ddllocation");

            dr1 = dt.NewRow();
            dr1["ITEM_SRNO"] = lblitemSrNo.Text;
            dr1["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
            dr1["ITEM_NAME"] = lblItemName.Text;
            dr1["QTY"] = 1;
            dr1["ITEM_RATE"] = lblItemRate.Text;
            dr1["DSR_NUMBER"] = txtSerialNo.Text;
            if (txtSpecification.Text == "")
            {
                dr1["TECH_SPEC"] = "";
            }
            else
            {
                dr1["TECH_SPEC"] = txtSpecification.Text;
            }
            if (txtQtySpec.Text == "")
            {
                dr1["QUALITY_QTY_SPEC"] = "";
            }
            else
            {
                dr1["QUALITY_QTY_SPEC"] = txtQtySpec.Text;
            }
            if (ddldept.SelectedValue == "")
            {
                dr1["DEPARTMENT"] = "";
            }
            else
            {
                dr1["DEPARTMENT"] = ddldept.SelectedValue;
            }
            if (ddllocation.SelectedValue == "")
            {
                dr1["LOCATION"] = "";
            }
            else
            {
                dr1["LOCATION"] = ddllocation.SelectedValue;
            }
        
            dt.Rows.Add(dr1);

        }
        objLM.DEAD_STOCK_ITEM_TBL = dt;


        //-------------------------------------------//
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        { 
            foreach (ListViewItem lv in lvitems.Items)
            {
                Label lblSrNo = lv.FindControl("lblSrNo") as Label;
                Label lblItemNo = lv.FindControl("lblItemNo") as Label;
                Label lblItemName = lv.FindControl("lblItemName") as Label;
                Label lblItemRate = lv.FindControl("lblItemRate") as Label;
                TextBox txtSerialNo = lv.FindControl("txtSerialNo") as TextBox;
                TextBox txtSpecification = lv.FindControl("txtSpecification") as TextBox;
                TextBox txtQtySpec = lv.FindControl("txtQtySpec") as TextBox;
                TextBox QUALITY_QTY_SPEC = lv.FindControl("QUALITY_QTY_SPEC") as TextBox;
                DropDownList ddldept = lv.FindControl("ddldept") as DropDownList;
                DropDownList ddllocation = lv.FindControl("ddllocation") as DropDownList;

                
                if (txtSerialNo.Text == "")
                {
                    objCommon.DisplayMessage("Please Fill Serial Number Field", this);
                    return;
                }

                if (ddldept.SelectedValue!="0")
                {
                    if (txtIssueDate.Text == string.Empty)
                    {
                        objCommon.DisplayMessage("Please Enter Issue Date", this);
                        return; 
                    }
                }
           

            }
            if (lvitems.Items.Count == 0)
            {
                objCommon.DisplayMessage("Please Add Item Details", this);
                return;
            }
            if (txtIssueDate.Text != string.Empty)
            {
            if (Convert.ToDateTime(txtIssueDate.Text) > DateTime.Today)
            {
                objCommon.DisplayMessage("Issue Date Should Not Be Greater Than Current Date", this);
                return;
            }
            }
        
            
            CustomStatus cs = new CustomStatus();
            if (ViewState["Edit_item"]==null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataTable();
                    int orgId =Convert.ToInt32( Session["OrgId"]);
                    objLM.REMARK = txtRemark.Text;

                    if (txtIssueDate.Text != string.Empty)
                    {
                        objLM.ISSUE_DATE = Convert.ToDateTime(txtIssueDate.Text);
                    }
                    else
                    {
                        objLM.ISSUE_DATE = DateTime.MinValue;
                    }
                    int createdBy = Convert.ToInt32(Session["userno"]);
                    DateTime TranDate = DateTime.Today;
                    DateTime CreatedDate = DateTime.Today;
                    int DSTKID = 0;
                    int modifyby = 0;
                    cs = (CustomStatus)strSerial.AddDeadStockEntry(objLM, orgId, createdBy, CreatedDate, TranDate, DSTKID, modifyby); //Shaikh Juned (29/03/2022)
                    objCommon.DisplayMessage("Records Saved Successfully", this);
                    ClearItem();
                    pnlitems.Visible = false;
                    PnlItem.Visible = false;
                    divAddItem.Visible = false;
                    PnlItem.Visible = false;
                    btnSaveItem.Visible = false;
                    PnlitemDetail.Visible = false;
                    btnAddNew.Visible = true;
                    PnlitemDetail.Visible = true;
                    divGRNEtry.Visible = false;
                    divauto.Visible = false;
                    divbtn.Visible = false;
                    BindListView();
                }
            }
            else
            {
            //else if (ViewState["action"].ToString().Equals("edit"))
            //{
                //TRANNO = Convert.ToInt32(radlTransfer.SelectedValue);
                ////cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, dtAppRecord);
                //cs = (CustomStatus)strSerial.Add_Update_SerialNumber(objLM, TRANNO);
                DataTable();
                objLM.REMARK = txtRemark.Text;

                if (txtIssueDate.Text != string.Empty)
                {
                    objLM.ISSUE_DATE = Convert.ToDateTime(txtIssueDate.Text);
                }
                else
                {
                    objLM.ISSUE_DATE = DateTime.MinValue;
                }
                int orgId = Convert.ToInt32(Session["OrgId"]);
                int ModifyBy = Convert.ToInt32(Session["userno"]);
                int DSTKID = Convert.ToInt32(ViewState["Edit_item"]);
                DateTime TranDate = DateTime.Today;
                DateTime CreatedDate = DateTime.MinValue;
                int createdBy = 0;
                cs = (CustomStatus)strSerial.UpdDeadStockEntry(objLM,orgId,createdBy,CreatedDate, ModifyBy, TranDate, DSTKID); //Shaikh Juned (29/03/2022)
                objCommon.DisplayMessage("Records Updated Successfully", this);
                ClearItem();
                pnlitems.Visible = false;
                PnlItem.Visible = false;
                divAddItem.Visible = true;
                PnlItem.Visible = true;
                btnSaveItem.Visible = true;
                PnlitemDetail.Visible = false;
                btnSaveItem.Visible = false;
                PnlitemDetail.Visible = false;
                btnAddNew.Visible = true;
                PnlitemDetail.Visible = true;
                divGRNEtry.Visible = false;
                divauto.Visible = false;
                divbtn.Visible = false;
                BindListView();
           // }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnAdNew_Click(object sender, EventArgs e)
    {
        ClearAll();
        divbtn.Visible = true;
        divGRNEtry.Visible = true;
        btnSubmit.Enabled = true;
        btnCancel.Visible = true;
        btnAddNew2.Visible = false;
        PnlItem.Visible = true;
        PnlitemDetail.Visible = false;
        ViewState["Edit_item"] = null;
    }

    private void ClearAll()
    {
        
        btnAddNew.Visible = false;
        PnlItem.Visible = true;
        btnSaveItem.Visible = true;
        pnlitems.Visible = false;
        divAddItem.Visible = true;
        txtItemQty.Text = "";   
        ViewState["Items"] = null;
        Session["dtItem"] = null;
        ViewState["Action"] = "Add";
        txtIssueDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRate.Text = string.Empty;
        divauto.Visible = false;
        ViewState["Edit_item"] = null;
        ddlItem.SelectedValue = "0";

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
       // ClearAll();
        btnAddNew.Visible = true;
        divGRNEtry.Visible = false;
        PnlItem.Visible = false;
        divbtn.Visible = false;
        PnlitemDetail.Visible = true;
        ddlItem.SelectedValue = "0";
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int GRNID = Convert.ToInt32(btnEdit.CommandArgument);
        if (Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_ITEM", "COUNT(*)", "GRNID=" + GRNID)) > 0)
        {
            MessageBox("Invoice Entry Already Submitted For This GRN Number.So,You Can Modify.");
            return;
        }
        ClearAll();
        ViewState["Action"] = "edit";
        divGRNEtry.Visible = true;
        btnSubmit.Enabled = true;
        btnCancel.Visible = true;
        btnAddNew2.Visible = true;
    }

    protected void chkAutoSerial_CheckedChanged(object sender, EventArgs e)
    {
        int Item_no = 0;
       // int Type = 0;

        Boolean Autoincrement = false;
        try
        {


            if (ddlItem.SelectedIndex > 0)
            {
                Item_no = Convert.ToInt32(ddlItem.SelectedValue);
            }
            else
            {
                MessageBox("Please Select Item");
                return;
            }

          //  Type = Convert.ToInt32(radlTransfer.SelectedValue);

            if (chkAutoSerial.Checked == true)
            {
                Autoincrement = true;
            }
            else
            {
                Autoincrement = false;
            }
            //DataSet ds = strSerial.GetDeadStockAllItemserial(Item_no, Autoincrement);
            //if (ds != null && ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        lvitems.DataSource = ds;
            //        lvitems.DataBind();
            //        lvitems.Visible = true;
            //        pnlitems.Visible = true;
            //    }
            //    else
            //    {
            //        lvitems.DataSource = null;
            //        lvitems.DataBind();
            //        lvitems.Visible = false;
            //        pnlitems.Visible = false;
            //        MessageBox("No Record Found !.");
            //    }
            //}
            //else
            //{

            //    lvitems.DataSource = null;
            //    lvitems.DataBind();
            //    lvitems.Visible = false;
            //    pnlitems.Visible = false;
            //    MessageBox("No Record Found !.");
            //}
        }
        catch (Exception ex)
        {

        }
    }


    public void BindListView()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO)", "SUM( A.QTY) AS 'ITEM_QUENTITY' ", "B.ISSUE_DATE,B.ITEM_NAME,B.REMARK,A.DSTK_ENTRY_ID", "DS_STATUS='S' group by B.ISSUE_DATE,B.ITEM_NAME,B.REMARK,A.DSTK_ENTRY_ID", "B.ITEM_NAME");

        listitemDetail.DataSource = ds;
        listitemDetail.DataBind();

    }

    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        ViewState["Edit_item"]=null;
        ImageButton btnEdit = sender as ImageButton;
        int DSTK_ENTRY_ID = Convert.ToInt32(btnEdit.CommandArgument);
        ViewState["Edit_item"] = DSTK_ENTRY_ID;
        ShowDatail(DSTK_ENTRY_ID);
        //ViewState["Edit_item"] = DSTK_ENTRY_ID;
        ViewState["Action"] = "edit";
        divbtn.Visible = true;
    }

    public void ShowDatail(int DSTK_ENTRY_ID)
    {
        lvitems.DataSource = null;
        lvitems.DataBind();

        DataSet ds = null;
        DataSet ds1 = null;
        ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO)", "B.SRNO as 'ITEM_SRNO',B.ITEM_NO as 'ITEM_NO',B.ITEM_NAME as 'ITEM_NAME',B.RATE as 'ITEM_RATE',B.DSR_NUMBER as 'DSR_NUMBER' ,B.DSTK_ENTRY_ID ", "B.TECH_SPEC as 'TECH_SPEC',B.QUALITY_QTY_SPEC as 'QUALITY_QTY_SPEC',B.MDNO as 'DEPARTMENT',B.LOCATION as 'LOCATION',B.ISSUE_DATE,B.REMARK", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "' ", "");
        txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUE_DATE"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        ds1 = objCommon.FillDropDown("[dbo].[STORE_DEAD_STOCK_DATA] ", "SUM(QTY) AS 'Total QTY'  ,ITEM_NO", "RATE", "DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "' group by ITEM_NO,RATE ", "");
        ddlItem.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["ITEM_NO"]).ToString();
        txtItemQty.Text = ds1.Tables[0].Rows[0]["Total QTY"].ToString();
        txtRate.Text = ds1.Tables[0].Rows[0]["RATE"].ToString();

        lvitems.DataSource = ds;
        lvitems.DataBind();


        //foreach (ListViewItem i in lvitems.Items)
        //{
        //    DropDownList ddlDept = i.FindControl("ddldept") as DropDownList;
        //    DropDownList ddllocation = i.FindControl("ddllocation") as DropDownList;

        //    objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
        //    objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");

        //}

        pnlitems.Visible = true;
        divGRNEtry.Visible = true;
        PnlItem.Visible = true;
       // btnSaveItem.Visible = true;
        btnAddNew.Visible = false;
        PnlitemDetail.Visible = false;
    }

    protected void lvitems_DataBound(object sender, ListViewItemEventArgs e)
    {
        if (ViewState["Edit_item"] != null)
        {
            DropDownList ddlDept = e.Item.FindControl("ddlDept") as DropDownList;
            DropDownList ddllocation = e.Item.FindControl("ddllocation") as DropDownList;
            HiddenField hdnDSTK_ENTRY_ID = e.Item.FindControl("hdnDSTK_ENTRY_ID") as HiddenField;
            Label lblitemSrNo = e.Item.FindControl("lblitemSrNo") as Label;

            objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
            objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
            DataSet ds = null;
            ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO)", "B.SRNO as 'ITEM_SRNO',B.ITEM_NO as 'ITEM_NO',B.ITEM_NAME as 'ITEM_NAME',B.RATE as 'ITEM_RATE',B.DSR_NUMBER as 'DSR_NUMBER' ,B.DSTK_ENTRY_ID ", "B.TECH_SPEC as 'TECH_SPEC',B.QUALITY_QTY_SPEC as 'QUALITY_QTY_SPEC',B.MDNO as 'DEPARTMENT',B.LOCATION as 'LOCATION',B.ISSUE_DATE,B.REMARK", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + (hdnDSTK_ENTRY_ID.Value) + "' and B.SRNO=" + Convert.ToInt32(lblitemSrNo.Text), "");

            ddlDept.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT"]).ToString();
            ddllocation.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]).ToString();
        }
        //PnlitemDetail.Visible = false;
    }
}