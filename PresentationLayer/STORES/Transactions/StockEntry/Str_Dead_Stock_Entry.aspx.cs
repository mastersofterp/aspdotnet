
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
using System.Collections.Generic;
using System.Text.RegularExpressions;


public partial class STORES_Transactions_StockEntry_Str_Dead_Stock_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_SerialNumberGenController strSerial = new Str_SerialNumberGenController();
    Str_SerialNumber objLM = new Str_SerialNumber();
    DataTable dtItemTable = null;
    DataRow datarow = null;
    DataTable dtItemTable1 = null;
    DataRow datarow1 = null;
    int dupItemFlag = 0;
    int SaveItemFlag = 0;
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

                }

            }
            ViewState["delete_status"] = null;
            ViewState["DSTK_ENTRY_ID"] = null;
            ViewState["Status"] = null;
            Session["dsItem"] = null;
            Session["dsItem1"] = null;
            ViewState["Items"] = null;
            Session["dtItem"] = null;
            Session["dtItem1"] = null;
            ViewState["action"] = null;
            // divauto.Visible = false;
            objCommon.FillDropDownList(ddlItem, "STORE_ITEM A inner join STORE_MAIN_ITEM_GROUP B ON (A.MIGNO=B.MIGNO)", "ITEM_NO", "A.ITEM_NAME +' ['+B.MIGNAME+']'", "", "ITEM_NAME");
            BindListView();
            PnlitemDetail.Visible = true;
            divauto.Visible = false;
        }
        //
        divMsg.InnerText = string.Empty;
        BindListView();
        //PnlitemDetail.Visible = true;
       // ViewState["Edit_item"] = null;
    
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
           // ViewState["Edit_item"] = null;
            pnlitems.Visible = true;
            PnlItem.Visible = true;
            lvitems.Visible = true;
            ddlItem.Enabled = true;
            lvitem.Visible = true;
          
           
            //-----------16-11-2022------
            int result = 0;
            bool isValidInt = int.TryParse(txtItemQty.Text, out result);
            //if (isValidInt == false)
            //{
            //    objCommon.DisplayMessage("Please Enter Qty In Number Format", this);
            //    return;
            //}

            if (txtRate.Text!=string.Empty)
            {
            bool isValidInt1 = int.TryParse(txtRate.Text, out result);
            //if (isValidInt1 == false)
            //{
            //    objCommon.DisplayMessage("Please Enter Rate In Number Format", this);
            //    return;
            //}
            }

            foreach (ListViewItem i in lvitem.Items)
            {
                Label lblItemName = i.FindControl("lblItemName") as Label;
                HiddenField hdItemNo = i.FindControl("hdItemNo") as HiddenField;

                string itemname1 = ddlItem.SelectedItem.Text;
                string splititem1 = itemname1.Split('[')[0].ToString();
                if (hdItemNo.Value == ddlItem.SelectedValue)
                {
                    objCommon.DisplayMessage("Item Is Already Exists", this);
                        return;
                }
            }

            BindItem();
            //----------------
            SaveItemFlag = 1;
            //DataTable dtItemDup = (DataTable)Session["dtItem"];
            int dsr_id=Convert.ToInt32(ViewState["Edit_item"]);
            if (ViewState["Edit_item"] != null)
            {
                if (Session["dsItem"]!= null)
                {
                    objLM.DEAD_STOCK_ITEM_TBL = (DataTable)Session["dsItem"];
                }
                DataTable();

                dtItemTable = objLM.DEAD_STOCK_ITEM_TBL;
            }
            else
            {
                dtItemTable = (DataTable)Session["dtItem"];
            }
            int maxVal = 0;
            if (dtItemTable == null)
            {
                AddItemTable();
            }


           // var rows = Enumerable.Range(0, 500).Select(i => new { Column1 = i, Column2 = "Row " + i });
           // DataTable table = rows.CopyToDataTable();



            int max = 0;
            int item = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO='" + ddlItem.SelectedValue + "'"));  // Shaikh Juned 10-02-2023
            if (item == 1)   // add
            {                   //add
                if (dtItemTable != null)
                {
                    int item_no =Convert.ToInt32( ddlItem.SelectedValue);
                    //string item_name = ddlItem.SelectedItem.ToString();
                    string itemname = ddlItem.SelectedItem.Text;
                    string splititem = itemname.Split('[')[0].ToString();//itemname.Trim().Replace("[","").ToString();
                    int Qty = Convert.ToInt32(txtItemQty.Text);
                    decimal rate;
                    if (txtRate.Text != "")
                    {
                         rate = Convert.ToDecimal(txtRate.Text);
                    }
                    else
                    {
                         rate = 0;
                    }
                    //for (int i = 0; i < Qty; i++)
                    //{
                    //    if (dupItemFlag == 1)
                    //    {
                    //        return;
                    //    }
                    //    int ItemType = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + Convert.ToInt32(ddlItem.SelectedValue)));  //ViewState["Item_No"]
                    //    Session["dtItem"] = dtItemTable;
                    //    datarow = dtItemTable.NewRow();

                    //    if (datarow != null)
                    //    {

                    //        if (max == 0)
                    //        {
                    //            maxVal = Convert.ToInt32(objCommon.LookUp("STORE_DEAD_STOCK_DATA", "ISNULL(Max(SRNO),0)", "ITEM_NO='" + ddlItem.SelectedValue + "'"));  // Shaikh Juned 15-02-2023
                    //        }
                    //        else
                    //        {
                    //            DataRow lastRow = dtItemTable.AsEnumerable().Last();
                    //            maxVal = lastRow.Field<int>("ITEM_SRNO");
                    //        }
                    //    }
                    //     max = maxVal + 1;
                    //    datarow["ITEM_SRNO"] = maxVal + 1;
                    //    datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
                    //    string itemname = ddlItem.SelectedItem.Text;
                    //    string splititem = itemname.Split('[')[0].ToString();//itemname.Trim().Replace("[","").ToString();
                    //    datarow["ITEM_NAME"] = splititem;
                    //    if (ItemType == 1)
                    //    {
                    //        datarow["QTY"] = 1;
                    //    }
                    //    else
                    //    {
                    //        datarow["QTY"] = txtItemQty.Text;
                    //    }
                    //    if (txtRate.Text == "")
                    //    {
                    //        datarow["ITEM_RATE"] = 0;
                    //    }
                    //    else
                    //    {
                    //        datarow["ITEM_RATE"] = txtRate.Text;
                    //    }
                    //    string YearLabel = string.Empty;
                    //    YearLabel = DateTime.Now.Year.ToString();
                    //   string ItemShortName = Convert.ToString(objCommon.LookUp("STORE_ITEM", "ITEM_CODE", "ITEM_NO='" + ddlItem.SelectedValue + "'"));  // Shaikh Juned 13-03-2023
                    //   datarow["DSR_NUMBER"] = "MAKAUT/" + ItemShortName + "/" + YearLabel + "/" + max;
                    //    datarow["TECH_SPEC"] = "";
                    //    datarow["QUALITY_QTY_SPEC"] = "";
                    //    datarow["DSTK_ENTRY_ID"] = 0;
                    DataSet ds = strSerial.GetDeadStockItems(item_no, splititem, Qty, rate);
                    DataTable dt3 = ds.Tables[0];
                        //dtItemTable.Rows.Add(datarow);

                   // dtItemTable = dt3;

                    dtItemTable.Merge(dt3); //= (DataTable)Session["dtItem"] + (DataTable) dt3;
                    
                   // dtItemTable = Session["dtItem"] as DataTable;
                        Session["dtItem"] = dtItemTable;
                        
                        lvitems.DataSource = Session["dtItem"];
                        lvitems.DataBind();
                        //lvitems.DataSource = ds;
                        //lvitems.DataBind();
                       

                   // }
                    //foreach (ListViewItem i in lvitems.Items)
                    //{
                    //    DropDownList ddlDept = i.FindControl("ddldept") as DropDownList;
                    //    DropDownList ddllocation = i.FindControl("ddllocation") as DropDownList;
                    //    if (ddlDept.SelectedIndex > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                    //    }
                    //    if (ddllocation.SelectedIndex > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
                    //    }
                    //}
                }
                 ddlItem.SelectedValue = "0";
                        txtItemQty.Text = string.Empty;
                        txtRate.Text = string.Empty;
            }  // add
            else
            {
                if (dtItemTable != null)
                {

                    int Qty = Convert.ToInt32(txtItemQty.Text);
                   
                        if (dupItemFlag == 1)
                        {
                            return;
                        }
                        int ItemType = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + Convert.ToInt32(ddlItem.SelectedValue))); //ViewState["Item_No"]
                        Session["dtItem"] = dtItemTable;
                        datarow = dtItemTable.NewRow();

                        if (datarow != null)
                        {
                            maxVal = Convert.ToInt32(dtItemTable.AsEnumerable().Max(row => row["ITEM_SRNO"]));
                        }
                        datarow["ITEM_SRNO"] = maxVal + 1;
                        datarow["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
                        string itemname = ddlItem.SelectedItem.Text;


                        string splititem = itemname.Split('[')[0].ToString();//itemname.Trim().Replace("[","").ToString();

                        datarow["ITEM_NAME"] = splititem;
                        if (ItemType == 1)
                        {
                            datarow["QTY"] = 1;
                        }
                        else
                        {
                            datarow["QTY"] = txtItemQty.Text;
                        }
                        if (txtRate.Text == "")
                        {
                            datarow["ITEM_RATE"] = 0;
                        }
                        else
                        {
                            datarow["ITEM_RATE"] = txtRate.Text;
                        }
                        datarow["DSR_NUMBER"] = "";
                        datarow["TECH_SPEC"] = "";
                        datarow["QUALITY_QTY_SPEC"] = "";
                        //datarow["DEPR_CAL_START_DATE"] = DBNull.Value;

                        datarow["DSTK_ENTRY_ID"] = 0;


                        dtItemTable.Rows.Add(datarow);
                        Session["dtItem"] = dtItemTable;
                        lvitems.DataSource = Session["dtItem"];
                        lvitems.DataBind();
                        ddlItem.SelectedValue = "0";
                        txtItemQty.Text = string.Empty;
                        txtRate.Text = string.Empty;
                    }
                    //foreach (ListViewItem i in lvitems.Items)
                    //{
                    //    DropDownList ddlDept = i.FindControl("ddldept") as DropDownList;
                    //    DropDownList ddllocation = i.FindControl("ddllocation") as DropDownList;
                    //    //ddlDept.SelectedIndex = -1;
                    //    //if (ddlDept.SelectedValue.Length > 0)
                    //    //{
                    //    //    ddlDept.SelectedValue.Remove(0);
                    //    //}
                    //    //objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                    //    if (ddlDept.SelectedIndex > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                    //    }
                    //    if (ddllocation.SelectedIndex > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
                    //    }
                    //}
                
            }
             //lvitems.DataSource = (DataTable)(Session["dtItem"]);
             //lvitems.DataBind();
            foreach (ListViewItem i in lvitems.Items)
            {
                DropDownList ddlDept = i.FindControl("ddldept") as DropDownList;
                DropDownList ddllocation = i.FindControl("ddllocation") as DropDownList;
                //ddlDept.SelectedIndex = -1;
                //if (ddlDept.SelectedValue.Length > 0)
                //{
                //    ddlDept.SelectedValue.Remove(0);
                //}
                //objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                if (ddlDept.SelectedIndex > 0)
                {

                }
                else
                {
                    objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                }
                if (ddllocation.SelectedIndex > 0)
                {

                }
                else
                {
                    objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
                }
            }
            lvitems.Visible = true;
            divauto.Visible = false;
            PnlitemDetail.Visible = false;
            ViewState["Item_No"] = ddlItem.SelectedValue;


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_Quotation_Str_SecurityPassEntry.btnAddVeh_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void BindItem()
    {
        int maxVal = 0;
        AddItemTable1();

        if (Session["dtItem1"] != null)
        {
        dtItemTable1 = (DataTable)Session["dtItem1"];
        }
        if (Session["dsItem1"] != null)
        {
            dtItemTable1 = (DataTable)Session["dsItem1"];
        }
        datarow1 = dtItemTable1.NewRow();
        

        //if (datarow1 != null)
        //{

           
        //        maxVal = Convert.ToInt32(dtItemTable1.AsEnumerable().Max(row => row["ITEM_SRNO"]));
            
        //}
        //datarow1["ITEM_SRNO"] = maxVal + 1;
        datarow1["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);
        // datarow["ITEM_NAME"] = ddlItem.SelectedItem.Text;
        string itemname = ddlItem.SelectedItem.Text;


        string splititem = itemname.Split('[')[0].ToString();//itemname.Trim().Replace("[","").ToString();

        datarow1["ITEM_NAME"] = splititem;
       
            datarow1["QTY"] = txtItemQty.Text;
        
        if (txtRate.Text == "")
        {
            datarow1["ITEM_RATE"] = 0;
        }
        else
        {
            datarow1["ITEM_RATE"] = txtRate.Text;
        }
      


        dtItemTable1.Rows.Add(datarow1);
        Session["dtItem1"] = dtItemTable1;
        lvitem.DataSource = dtItemTable1;
        lvitem.DataBind();
    }

    private DataTable CreateItemTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
       // dt.Columns.Add(new DataColumn("DEPR_CAL_START_DATE", typeof(DateTime)));


        dt.Columns.Add(new DataColumn("DSTK_ENTRY_ID", typeof(int)));
        return dt;
    }
    private void ClearItem()
    {
        ddlItem.SelectedValue = "0";
        txtItemQty.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtIssueDate.Text = string.Empty;
        txtItemRemark.Text = string.Empty;
        ViewState["action"] = null;
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_NO"].ToString() == value)
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
        dt.Columns.Add(new DataColumn("QTY", typeof(int)));
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
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DSR_NUMBER", typeof(string)));
        dt.Columns.Add(new DataColumn("TECH_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("QUALITY_QTY_SPEC", typeof(string)));
        dt.Columns.Add(new DataColumn("DSTK_ENTRY_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("DEPARTMENT", typeof(int)));
        dt.Columns.Add(new DataColumn("LOCATION", typeof(int)));

        DataRow dr1 = null;
        foreach (ListViewItem ii in lvitems.Items)
        {

             System.Web.UI.WebControls.Label lblitemSrNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblitemSrNo");
            System.Web.UI.WebControls.Label lblitemNo = (System.Web.UI.WebControls.Label)ii.FindControl("lblitemNo");
            System.Web.UI.WebControls.TextBox lblItemName = (System.Web.UI.WebControls.TextBox)ii.FindControl("lblItemName");
            System.Web.UI.WebControls.Label lblItemQty = (System.Web.UI.WebControls.Label)ii.FindControl("lblItemQty");
            System.Web.UI.WebControls.Label lblItemRate = (System.Web.UI.WebControls.Label)ii.FindControl("lblItemRate");
            System.Web.UI.WebControls.TextBox txtSerialNo = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtSerialNo");
            System.Web.UI.WebControls.TextBox txtSpecification = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtSpecification");
            System.Web.UI.WebControls.TextBox txtQtySpec = (System.Web.UI.WebControls.TextBox)ii.FindControl("txtQtySpec");
            System.Web.UI.WebControls.HiddenField hdnDSTK_ENTRY_ID = (System.Web.UI.WebControls.HiddenField)ii.FindControl("hdnDSTK_ENTRY_ID");
            System.Web.UI.WebControls.DropDownList ddldept = (System.Web.UI.WebControls.DropDownList)ii.FindControl("ddldept");
            System.Web.UI.WebControls.DropDownList ddllocation = (System.Web.UI.WebControls.DropDownList)ii.FindControl("ddllocation");
            System.Web.UI.WebControls.HiddenField hditemSrNo = (System.Web.UI.WebControls.HiddenField)ii.FindControl("hditemSrNo");
            System.Web.UI.WebControls.HiddenField hdItemNo = (System.Web.UI.WebControls.HiddenField)ii.FindControl("hdItemNo");

            int ItemType = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + Convert.ToInt32(hdItemNo.Value)));
            dr1 = dt.NewRow();
            //--start---juned---15-02-2023
            if (ItemType == 1)
            {
                dr1["ITEM_SRNO"] = hditemSrNo.Value;
            }
            else
            {
                dr1["ITEM_SRNO"] = lblitemSrNo.Text;
            }
            //--end--juned---15-02-2023
            dr1["ITEM_NO"] = Convert.ToInt32(hdItemNo.Value);
            dr1["ITEM_NAME"] = lblItemName.Text;
            //   10-02-2023 Add for insert seprate COMSUMABLE and Non COMSUMABLE Item Qty
            if (ItemType == 1)
            {
                dr1["QTY"] = 1;
            }
            else
            {
               // dr1["QTY"] = txtItemQty.Text;
                dr1["QTY"] = lblItemQty.Text;
            }
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
            if (hdnDSTK_ENTRY_ID.Value == "")
            {
                dr1["DSTK_ENTRY_ID"] = 0;
            }
            else
            {
                dr1["DSTK_ENTRY_ID"] = hdnDSTK_ENTRY_ID.Value;
            }
            ViewState["DSTK_ENTRY_ID"] = hdnDSTK_ENTRY_ID.Value;
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

    //------------27-02-2023---------start single Grid 

    private DataTable CreateItemTable1()
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add(new DataColumn("ITEM_SRNO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(int)));
        dt.Columns.Add(new DataColumn("ITEM_RATE", typeof(decimal)));
        dt.Columns.Add(new DataColumn("DSTK_ENTRY_ID", typeof(int)));
        return dt;
    }
    private void AddItemTable1()
    {
        dtItemTable1 = this.CreateItemTable1();

        datarow = null;
    }
   

    //------------27-02-2023---------end----

    private void GenerateDeadStockNumber()
    {
       // DataSet ds = objGRNCon.GetGRNNumber();
        DataSet ds = strSerial.GetDeadStockNumber();
        txtDStoNumber.Text = ds.Tables[0].Rows[0]["DEAD_STOCK_NUMBER"].ToString();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if()
            //{
            //}
            foreach (ListViewItem lv in lvitems.Items)
            {
                Label lblSrNo = lv.FindControl("lblSrNo") as Label;
                Label lblItemNo = lv.FindControl("lblItemNo") as Label;
                TextBox lblItemName = lv.FindControl("lblItemName") as TextBox;
                Label lblItemQty = lv.FindControl("lblItemQty") as Label;
                Label lblItemRate = lv.FindControl("lblItemRate") as Label;
                TextBox txtSerialNo = lv.FindControl("txtSerialNo") as TextBox;
                TextBox txtSpecification = lv.FindControl("txtSpecification") as TextBox;
                TextBox txtQtySpec = lv.FindControl("txtQtySpec") as TextBox;
                TextBox QUALITY_QTY_SPEC = lv.FindControl("QUALITY_QTY_SPEC") as TextBox;
                DropDownList ddldept = lv.FindControl("ddldept") as DropDownList;
                DropDownList ddllocation = lv.FindControl("ddllocation") as DropDownList;
                HiddenField hditemSrNo = lv.FindControl("hditemSrNo") as HiddenField;
                HiddenField hdItemNo = lv.FindControl("hdItemNo") as HiddenField;
                //ViewState["Item_No"] = ddlItem.SelectedValue;
                ViewState["Item_No"] = hdItemNo.Value;
               
                int itemno = Convert.ToInt32(ViewState["Item_No"]);
                string ItemType = objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO =" + itemno);
                if (ItemType == "1") // Non Consumable Item
                {


                    if (txtSerialNo.Text == "")
                    {
                        objCommon.DisplayMessage("Please Fill Serial Number Feild", this);
                        return;
                    }
                }

                if (ddldept.SelectedValue != "0")
                {
                    if (txtIssueDate.Text == string.Empty)
                    {
                        objCommon.DisplayMessage("Please Enter Stock Entry Date", this);
                        return;
                    }
                }


            }


            if (lvitems.Items.Count == 0)
            {
                lvitem.Visible = false;
                objCommon.DisplayMessage("Please Add At least One Item", this);
                return;
            }


            if (txtIssueDate.Text != string.Empty)
            {
                if (Convert.ToDateTime(txtIssueDate.Text) > DateTime.Today)
                {
                    objCommon.DisplayMessage("Stock Entry Date Is Not Greater Than Current Date", this);
                    return;
                }
            }


            CustomStatus cs = new CustomStatus();
            //if (ViewState["Edit_item"] == null)
            //{
                if (ViewState["action"].ToString().Equals("add"))
                {
                   
                        DataTable();
                        int orgId = Convert.ToInt32(Session["OrgId"]);
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
                         
                   string status= string.Empty;
                        GenerateDeadStockNumber();

                        cs = (CustomStatus)strSerial.AddDeadStockEntry(objLM, orgId, createdBy, CreatedDate, TranDate, DSTKID, modifyby, txtDStoNumber.Text, status); //Shaikh Juned (29/03/2022)
                        objCommon.DisplayMessage(this.Page,"Record Saved & Stock Number Generated Successfully", this);
                        //GenerateDeadStockNumber();
                        divdednumber.Visible = true;
                        btnSubmit.Enabled = false;
                       // ClearItem();
                       // pnlitems.Visible = false;
                       // PnlItem.Visible = false;
                       // divAddItem.Visible = false;
                       // PnlItem.Visible = false;
                       // btnSaveItem.Visible = false;
                       // PnlitemDetail.Visible = false;
                       // btnAddNew.Visible = true;
                       // PnlitemDetail.Visible = true;
                       // divGRNEtry.Visible = false;
                       // divauto.Visible = false;
                       // divbtn.Visible = false;
                       //   BindListView();
                   //     ViewState["action"] = "add";
                    
              //  }
            }
                else if (ViewState["action"].ToString().Equals("edit"))
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
                string deadstockno = string.Empty;
                string status = string.Empty;
                if (ViewState["Status"]!= null)
                    {
                if (ViewState["Status"].ToString() == "deleted")
                {
                    status = ViewState["Status"].ToString();
                }
                else
                {
                    status = "modify";
                }
                    }
                deadstockno = txtDStoNumber.Text;
                    cs = (CustomStatus)strSerial.UpdDeadStockEntry(objLM, orgId, createdBy, CreatedDate, ModifyBy, TranDate, DSTKID, deadstockno, status); //Shaikh Juned (29/03/2022)
                    objCommon.DisplayMessage(this.Page,"Record Updated Successfully", this);
                divdednumber.Visible = true;
                btnSubmit.Enabled = false;
                //ClearItem();
                //pnlitems.Visible = false;
                //PnlItem.Visible = false;
                //divAddItem.Visible = true;
                //PnlItem.Visible = true;
                //btnSaveItem.Visible = true;
                //PnlitemDetail.Visible = false;
                //btnSaveItem.Visible = false;
                //PnlitemDetail.Visible = false;
                //btnAddNew.Visible = true;
                //PnlitemDetail.Visible = true;
                //divGRNEtry.Visible = false;
                //divauto.Visible = false;
                //divbtn.Visible = false;
                //BindListView();
              //  ViewState["action"] = "add";
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
        ViewState["action"] = "add";
    }

    private void ClearAll()
    {
        ddlItem.Enabled = true;
        btnAddNew.Visible = false;
        PnlItem.Visible = true;
        btnSaveItem.Visible = true;
        pnlitems.Visible = false;
        divAddItem.Visible = true;
        txtItemQty.Text = "";
        ViewState["Items"] = null;
        Session["dtItem"] = null;
       ViewState["action"] = "add";
        txtIssueDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRate.Text = string.Empty;
        divauto.Visible = false;
        ViewState["Edit_item"] = null;
        lvitems.DataSource = null;
        lvitems.DataBind();
        lvitem.DataSource = null;
        lvitem.DataBind();
        ddlItem.SelectedValue = "0";
        txtDStoNumber.Text = string.Empty;
        Session["dtItem1"] = null;
        divdednumber.Visible = false;
        btnSubmit.Enabled = true;
        Session["dsItem1"] = null;

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        // ClearAll();
        btnAddNew.Visible = true;
        divGRNEtry.Visible = false;
        PnlItem.Visible = false;
        divbtn.Visible = false;
        PnlitemDetail.Visible = true;
        btnSubmit.Enabled = true;
        ddlItem.Enabled = true;
        ddlItem.SelectedValue = "0";
        txtItemQty.Text = string.Empty;
        txtRate.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton btnEdit = sender as ImageButton;
    //    int GRNID = Convert.ToInt32(btnEdit.CommandArgument);
    //    if (Convert.ToInt32(objCommon.LookUp("STORE_INVOICE_ITEM", "COUNT(*)", "GRNID=" + GRNID)) > 0)
    //    {
    //        MessageBox("Invoice Entry Already Submitted For This GRN Number.So,You Can Modify.");
    //        return;
    //    }
    //    ClearAll();
    //    ViewState["action"] = "edit";
    //    divGRNEtry.Visible = true;
    //    btnSubmit.Enabled = true;
    //    btnCancel.Visible = true;
    //    btnAddNew2.Visible = true;
    //}

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
        ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO)", "SUM( A.QTY) AS 'ITEM_QUENTITY' ", "B.ISSUE_DATE,B.REMARK,A.DSTK_ENTRY_ID,CAST(B.CREATED_DATE AS DATE) CREATED_DATE,DEAD_STOCK_NO", "DS_STATUS='S' and A.IS_DELETED is null group by B.ISSUE_DATE,B.REMARK,A.DSTK_ENTRY_ID,CAST(CREATED_DATE AS DATE),DEAD_STOCK_NO", "DEAD_STOCK_NO desc");
        //ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO)", "SUM( A.QTY) AS 'ITEM_QUENTITY' ", "B.ISSUE_DATE,B.REMARK,A.DSTK_ENTRY_ID,DEAD_STOCK_NO", "DS_STATUS='S' and A.IS_DELETED is null group by B.ISSUE_DATE,B.REMARK,A.DSTK_ENTRY_ID,DEAD_STOCK_NO", "DEAD_STOCK_NO desc");


        listitemDetail.DataSource = ds;
        listitemDetail.DataBind();

    }

    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        ViewState["Edit_item"] = null;
        ImageButton btnEdit = sender as ImageButton;
        int DSTK_ENTRY_ID = Convert.ToInt32(btnEdit.CommandArgument);
        ViewState["Edit_item"] = DSTK_ENTRY_ID;
        ShowDatail(DSTK_ENTRY_ID);
        //ViewState["Edit_item"] = DSTK_ENTRY_ID;
        ViewState["action"] = "edit";
        divbtn.Visible = true;
        divdednumber.Visible = true;
    }

    public void ShowDatail(int DSTK_ENTRY_ID)
    {
        lvitems.DataSource = null;
        lvitems.DataBind();

        DataSet ds = null;
        DataSet ds1 = null;
        ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO) inner join [dbo].[STORE_ITEM] C ON (B.ITEM_NO=C.ITEM_NO)", "B.SRNO as 'ITEM_SRNO',B.ITEM_NO as 'ITEM_NO',C.ITEM_NAME as 'ITEM_NAME',B.QTY,B.RATE as 'ITEM_RATE',B.DSR_NUMBER as 'DSR_NUMBER' ,B.DSTK_ENTRY_ID ", "B.TECH_SPEC as 'TECH_SPEC',B.QUALITY_QTY_SPEC as 'QUALITY_QTY_SPEC',B.MDNO as 'DEPARTMENT',B.LOCATION as 'LOCATION',B.ISSUE_DATE,B.REMARK,B.DEAD_STOCK_NO,B.DSTK_ENTRY_ID", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "' ", "");
        txtDStoNumber.Text = ds.Tables[0].Rows[0]["DEAD_STOCK_NO"].ToString();
        txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUE_DATE"].ToString();
        txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
        //ds1 = objCommon.FillDropDown("[dbo].[STORE_DEAD_STOCK_DATA] ", "SUM(QTY) AS 'Total QTY'  ,ITEM_NO", "RATE", "DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "' group by ITEM_NO,RATE ", "");
        //ddlItem.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["ITEM_NO"]).ToString();
        //txtItemQty.Text = ds1.Tables[0].Rows[0]["Total QTY"].ToString();
        //txtRate.Text = ds1.Tables[0].Rows[0]["RATE"].ToString();

        lvitems.DataSource = ds;
        lvitems.DataBind();
        DataTable dt3 = ds.Tables[0];
        Session["dsItem"] = dt3;

        ds1 = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO) inner join  [dbo].[STORE_ITEM] C on (C.ITEM_NO=B.ITEM_NO)", "sum(A.QTY)QTY,B.ITEM_NO as 'ITEM_NO',C.ITEM_NAME as 'ITEM_NAME',B.RATE as 'ITEM_RATE' ", "A.DSTK_ENTRY_ID", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "' group by B.ITEM_NO,C.ITEM_NAME,B.RATE ,A.DSTK_ENTRY_ID", "");  //,B.SRNO as 'ITEM_SRNO' //,B.SRNO
         
            //  ds1 = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO) inner join  [dbo].[STORE_ITEM] C on (C.ITEM_NO=B.ITEM_NO)", " A.QTY,B.ITEM_NO as 'ITEM_NO',C.ITEM_NAME as 'ITEM_NAME',B.RATE as 'ITEM_RATE' ", "A.DSTK_ENTRY_ID", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + DSTK_ENTRY_ID + "'", "");  //,B.SRNO as 'ITEM_SRNO' //,B.SRNO
          
        lvitem.DataSource = ds1;
        lvitem.DataBind();
        DataTable dt2 = ds1.Tables[0];
        Session["dsItem1"] = dt2;
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

    public void hidtoryModifyDeleteItem(string status, int dstkid, int itemno)
    {

    }

    protected void lvitems_DataBound(object sender, ListViewItemEventArgs e)
    {
        if (ViewState["Edit_item"] != null)
        {
            DropDownList ddlDept = e.Item.FindControl("ddlDept") as DropDownList;
            DropDownList ddllocation = e.Item.FindControl("ddllocation") as DropDownList;
            HiddenField hdnDSTK_ENTRY_ID = e.Item.FindControl("hdnDSTK_ENTRY_ID") as HiddenField;
            Label lblitemSrNo = e.Item.FindControl("lblitemSrNo") as Label;
            HiddenField hditemSrNo = e.Item.FindControl("hditemSrNo") as HiddenField;

            objCommon.FillDropDownList(ddlDept, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
            objCommon.FillDropDownList(ddllocation, "STORE_LOCATION", "LOCATIONNO", "LOCATION", "ACTIVESTATUS=1", "LOCATIONNO");
            DataSet ds = null;
            ds = objCommon.FillDropDown("[dbo].[STORE_INVOICE_DSR_ITEM] A inner join [dbo].[STORE_DEAD_STOCK_DATA] B on (A.DSTK_ENTRY_ID=B.DSTK_ENTRY_ID and A.DSTKNO=B.DSTKNO) INNER JOIN [dbo].[STORE_ITEM] C ON (B.ITEM_NO=C.ITEM_NO)", "B.SRNO as 'ITEM_SRNO',B.ITEM_NO as 'ITEM_NO',C.ITEM_NAME as 'ITEM_NAME',B.QTY,B.RATE as 'ITEM_RATE',B.DSR_NUMBER as 'DSR_NUMBER' ,B.DSTK_ENTRY_ID ", "B.TECH_SPEC as 'TECH_SPEC',B.QUALITY_QTY_SPEC as 'QUALITY_QTY_SPEC',B.MDNO as 'DEPARTMENT',B.LOCATION as 'LOCATION',B.ISSUE_DATE,B.REMARK", "DS_STATUS='S' and A.DSTK_ENTRY_ID= '" + (hdnDSTK_ENTRY_ID.Value) + "'and B.SRNO=" + Convert.ToInt32(hditemSrNo.Value), "");  //and B.SRNO=" + Convert.ToInt32(lblitemSrNo.Text)

            if (ds.Tables[0].Rows.Count > 0)
            {


                if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT"]) == null)
                {
                    ddlDept.SelectedValue = "0";
                }
                else
                {
                    ddlDept.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["DEPARTMENT"]).ToString();
                }
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]) == null)
                {
                    ddllocation.SelectedValue = "0";
                }
                else
                {
                    ddllocation.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["LOCATION"]).ToString();
                }
            }
        }
        //PnlitemDetail.Visible = false;
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["Delete_item"] = null;
        ImageButton btnDelete = sender as ImageButton;
        int DSTK_ENTRY_ID = Convert.ToInt32(btnDelete.CommandArgument);
        ViewState["Delete_item"] = DSTK_ENTRY_ID;
        int user_id =Convert.ToInt32( Session["userno"]);
        int itemNo = Convert.ToInt32(objCommon.LookUp("STORE_DEAD_STOCK_DATA", "ITEM_NO", "DSTK_ENTRY_ID='" + Convert.ToInt32(DSTK_ENTRY_ID) + "'"));  // Shaikh Juned 10-02-2023
        int item = Convert.ToInt32(objCommon.LookUp("STORE_ITEM", "MIGNO", "ITEM_NO='" +Convert.ToInt32( itemNo )+ "'"));  // Shaikh Juned 10-02-2023
        if (item == 2)   // add
        {
           // CustomStatus cs = (CustomStatus)strSerial.DeleteConItem(DSTK_ENTRY_ID, user_id);
            BindListView();
            MessageBox("Item Is Deleted Successfully.");
        }
        else
        {
            MessageBox("Non Comsumable Item Should Not Deleted.");
            return;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
            {
                dt = ((DataTable)Session["dtItem"]);
                int totalRows = dt.Rows.Count;
                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
                ViewState["EDIT_ITEM_NO"] = btnEditRec.CommandArgument;
                ddlItem.SelectedValue = dr["ITEM_NO"].ToString();
               
              //  txtItemQty.Text = dr["QTY"].ToString();
                txtItemQty.Text = totalRows.ToString();
                txtRate.Text = dr["ITEM_RATE"].ToString();
                //txtTax.Text = dr["TAX"].ToString();
                //txtProDesc.Text = dr["PRODUCTION_DESC"].ToString();
                //txtTotalAmount.Text = dr["TOT_AMOUNT"].ToString();
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows.Remove(dr);
                    }
                   
                }
               // dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvitems.DataSource = dt;
                lvitems.DataBind();
                lvitems.Enabled = false;
                ViewState["actionADD"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_StockEntry_DeadStockRegister.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnDelete_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            string Status = "deleted";
            ViewState["Status"] = Status;
            ImageButton btnDelete = sender as ImageButton;
            DataTable dt4 = null;
            if (Session["dsItem1"] != null)
            {

                DataTable dt2 = (DataTable)Session["dsItem1"];
                Session["dtItem1"] = dt2;
            }

            if (Session["dsItem"] != null)
            {

                DataTable dt3 = (DataTable)Session["dsItem"];
                Session["dtItem"] = dt3;
            }
            dt4 = ((DataTable)Session["dtItem1"]);
            DataRow dr = this.GetEditableDatarow(dt4, btnDelete.CommandArgument);
            string item_no1 = dr["ITEM_NO"].ToString();
            //if (dr["DSTK_ENTRY_ID"]!= DBNull.Value)
            //{
            //int stockId = Convert.ToInt32(dr["DSTK_ENTRY_ID"]);
            //cs = (CustomStatus)strSerial.hidtoryModifyDeleteItem(Status, stockId, Convert.ToInt32(item_no1)); // maintain History table and delte data from main table
            //}
            //else
            //{
            //}
            if (Session["dtItem1"] != null && ((DataTable)Session["dtItem1"]) != null)
            {
                DataTable dt = (DataTable)Session["dtItem1"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
              
                Session["dtItem1"] = dt;
                lvitem.DataSource = dt;
                lvitem.DataBind();
            }
            if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
            {
                DataTable dt = (DataTable)Session["dtItem"];
                int item_number = Convert.ToInt32(btnDelete.CommandArgument);
                ViewState["delete_item_no"] = item_number;
                
                List<DataRow> rowsToDelete = dt.AsEnumerable().ToList();
                foreach (DataRow row in rowsToDelete)
                {
                    int item_no =Convert.ToInt32( row["ITEM_NO"]);
                    if (item_no == item_number)
                    {
                        dt.Rows.Remove(this.GetEditableDatarow(dt, item_number.ToString()));
                    }
                }
                Session["dtItem"] = dt;
                lvitems.DataSource = dt;
                lvitems.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_StockEntry_DeadStockRegister.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnEdit_Click2(object sender, ImageClickEventArgs e)
    {
        try
        {
            CustomStatus cs = new CustomStatus();
            string Status = "modify";
            ViewState["Status"] = Status;
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["dsItem1"] != null)
            {
               
            DataTable dt2 = (DataTable)Session["dsItem1"];
            Session["dtItem1"] = dt2;
            }

            if (Session["dsItem"] != null)
            {

                DataTable dt3 = (DataTable)Session["dsItem"];
                Session["dtItem"] = dt3;
            }
            
            if (Session["dtItem1"] != null && ((DataTable)Session["dtItem1"]) != null)
            {
                dt = ((DataTable)Session["dtItem1"]);
                int totalRows = dt.Rows.Count;
                DataRow dr = this.GetEditableDatarow(dt, btnEdit.CommandArgument);
                ViewState["EDIT_ITEM_NO"] = btnEdit.CommandArgument;
                ddlItem.SelectedValue = dr["ITEM_NO"].ToString();
                ddlItem.Enabled = false;
                txtItemQty.Text = dr["QTY"].ToString();
                //txtItemQty.Text = totalRows.ToString();
                txtRate.Text = dr["ITEM_RATE"].ToString();
                string item_no = dr["ITEM_NO"].ToString();
                //if (dr["DSTK_ENTRY_ID"] != DBNull.Value)
                //{
                //    int stockId = Convert.ToInt32(dr["DSTK_ENTRY_ID"]);

                //    cs = (CustomStatus)strSerial.hidtoryModifyDeleteItem(Status, stockId, Convert.ToInt32(item_no)); // maintain History table and delte data from main table
                //}
                //else
                //{
                //}
                if (Session["dtItem1"] != null && ((DataTable)Session["dtItem1"]) != null)
                {
                    DataTable dt1 = (DataTable)Session["dtItem1"];
                    dt.Rows.Remove(this.GetEditableDatarow(dt1, btnEdit.CommandArgument));
                    Session["dtItem1"] = dt;
                    lvitem.DataSource = dt;
                    lvitem.DataBind();
                }
                if (Session["dtItem"] != null && ((DataTable)Session["dtItem"]) != null)
                {
                    DataTable dt1 = (DataTable)Session["dtItem"];
                    int item_number = Convert.ToInt32(btnEdit.CommandArgument);
                    ViewState["edit_item_no"] = item_number;
                    
                   // int item_number = Convert.ToInt32(item_no);
                   // ViewState["delete_item_no"] = item_number;

                    List<DataRow> rowsToDelete = dt1.AsEnumerable().ToList();
                    foreach (DataRow row in rowsToDelete)
                    {
                        int item_no1 = Convert.ToInt32(row["ITEM_NO"]);
                        if (item_no1 == item_number)
                        {
                            dt1.Rows.Remove(this.GetEditableDatarow(dt1, item_number.ToString()));
                        }
                    }
                    Session["dtItem"] = dt1;
                    lvitems.DataSource = dt1;
                    lvitems.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "STORES_Transactions_StockEntry_DeadStockRegister.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btngenerateBarcode_Click(object sender, EventArgs e)
    {
        if (txtDStoNumber.Text==string.Empty)
        {
            lvitem.Visible = false;
            MessageBox("Data Is Not Avelable.");
            return;
        }

        string StockNumber = txtDStoNumber.Text;
        ShowReport("DSR_Number_Barcode_Report", "StrDSR_NumberBarcodeGeneration.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_STOCK_NUMBER=" + txtDStoNumber.Text.ToString(); 
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}