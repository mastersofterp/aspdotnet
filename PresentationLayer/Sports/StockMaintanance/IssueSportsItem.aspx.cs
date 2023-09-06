//====================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : SPORTS
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 20-MAY-2017
// DESCRIPTION   : THIS FORM IS USED TO ISSUE ITEMS TO TEAMS OR CLUBS
//====================================================================
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
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;



public partial class Sports_StockMaintanance_IssueSportsItem : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EventKitEnt objEK = new EventKitEnt();
    KitController objKCon = new KitController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    lvIssueList.DataSource = null;
                    lvIssueList.DataBind();
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;

                    objCommon.FillDropDownList(ddlTeams, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "TM.TEAM_TYPE='U'", "TM.TEAMNAME");                   
                    objCommon.FillDropDownList(ddlClubs, "SPRT_CLUB_MASTER", "CLUBID", "CLUBNAME", "", "CLUBID");
                    objCommon.FillDropDownList(ddlItem, "SPRT_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
                    BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // This Event is used to display data page wise.
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetItemName(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {

            conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {              

                cmd.CommandText = "select ITEM_NO, ITEM_NAME AS ITEM_NAME from SPRT_ITEM where ITEM_NAME like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();               

                List<string> ItemsName = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ItemsName.Add(sdr["ITEM_NO"].ToString() + "---------*" + sdr["ITEM_NAME"].ToString());
                    }
                }                
                conn.Close();
                return ItemsName;                
            }
        }        
    }
    

    // This method is used to display the list of items issued.
    private void BindList(char cat)
    {
        try
        {
            DataSet ds = objKCon.GetIssueDetails(cat);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvIssueEntryList.DataSource = ds;
                lvIssueEntryList.DataBind();
            }
            else
            {
                lvIssueEntryList.DataSource = null;
                lvIssueEntryList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.BindList -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
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
    // This method is used to fill the drop down list.
  
    //This event is used to cancel the last selection.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    
   
    //This method is used to clear the controls.
    private void clear()
    {
        ddlTeams.SelectedIndex = 0;
        ddlClubs.SelectedIndex = 0;
        txtIssueDate.Text = string.Empty;      
        txtRemark.Text = string.Empty;
        txtQty.Text = string.Empty;      
        ViewState["action"] = "add";
        lblUnit.Text = string.Empty;      
        lvIssueList.DataSource = null;
        lvIssueList.DataBind();
        Session["RecTbl"] = null;
        txtAvailableQty.Text = string.Empty;
        ddlItem.SelectedIndex = 0;

    }
    // This event is used to modify the issue entry. 
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        int IssueId = int.Parse(imgBtn.CommandArgument);
        ViewState["ISSUE_ID"] = int.Parse(imgBtn.CommandArgument);
        ViewState["action"] = "edit";
        ShowDetails(IssueId);
    }
    // This method is used to show the details of the selected record.
    private void ShowDetails(int IssueId)
    {
        try
        {           
            DataSet ds = objKCon.GetIssueItemDetailsById(IssueId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    rdbIssueTo.SelectedValue = ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString();

                    if (ds.Tables[0].Rows[0]["ISSUE_TYPE"].ToString() == "T")
                    {
                        trTeams.Visible = true;
                        trClubs.Visible = false;
                        ddlTeams.SelectedValue = ds.Tables[0].Rows[0]["TEAMID"].ToString();
                    }
                    else
                    {
                        trTeams.Visible = false;
                        trClubs.Visible = true;
                        ddlClubs.SelectedValue = ds.Tables[0].Rows[0]["CLUBID"].ToString();
                    }

                    txtIssueDate.Text = Convert.ToDateTime(dr["ISSUE_DATE"]).ToString("dd/MM/yyyy");                  
                    txtRemark.Text = dr["REMARK"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lvIssueList.DataSource = ds.Tables[1];
                        lvIssueList.DataBind();
                        lvIssueList.Visible = true;
                        Session["RecTbl"] = ds.Tables[1];
                    }
                    else
                    {
                        lvIssueList.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.ShowDetails() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    // This event is used to save the issue entry.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {  
           

            if (lvIssueList.Items.Count == 0)
            {
                Showmessage("Please Add Item To Issue List");
                return;
            }
            objEK.TEAMID = Convert.ToInt32(ddlTeams.SelectedValue);
            objEK.CLUBID = Convert.ToInt32(ddlClubs.SelectedValue);
            objEK.ISSUE_TYPE = Convert.ToChar(rdbIssueTo.SelectedValue);
            objEK.ISSUE_DATE = Convert.ToDateTime(txtIssueDate.Text);
            objEK.REMARK = txtRemark.Text.Trim() == string.Empty ? string.Empty : txtRemark.Text.Trim();
            objEK.USERID = Convert.ToInt32(Session["userno"]);
            DataTable dt;
            dt = (DataTable)Session["RecTbl"];
            objEK.ISSUE_ITEM_LIST = dt;
            
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objEK.ISSUE_ID = 0;
                    CustomStatus cs = (CustomStatus)objKCon.ItemIssueInsertUpdate(objEK);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Showmessage("Record Save Successfully.");
                        clear();
                        ViewState["action"] = "add";
                        BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
                    }
                }
                else
                {
                    if (ViewState["ISSUE_ID"] != null)
                    {
                        objEK.ISSUE_ID = Convert.ToInt32(ViewState["ISSUE_ID"].ToString());
                        CustomStatus cs = (CustomStatus)objKCon.ItemIssueInsertUpdate(objEK);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            Showmessage("Record Updated Successfully.");
                            ViewState["action"] = "add";
                            BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  
    // this event is used to fill the list of items.
    protected void rdbIssueTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbIssueTo.SelectedValue == "T")
            {
                trTeams.Visible = true;
                trClubs.Visible = false;
                //BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
            }
            else
            {
                trTeams.Visible = false;
                trClubs.Visible = true;
                //BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }        
    }  
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowItemIssueDetails("ItemIssueDetails", "ItemIssueDetailsReport.rpt");
    }

    private void ShowItemIssueDetails(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            url += "&filename=ItemIssueDetailsReport.pdf";
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=@P_ITEMNO=0,@P_FDATE=null";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.ShowItemIssueDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnBalReport_Click(object sender, EventArgs e)
    {
        ShowItemStockBalance("Item Stock Balance", "rptItemBalanceStock.rpt");
    }

    private void ShowItemStockBalance(string exporttype, string rptFileName)
    {
        try
        {
            //string Script = string.Empty;

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
            //url += "Reports/CommonReport.aspx?";
            //url += "exporttype=pdf";
            //url += "&filename=ItemBalanceStockReport.pdf";
            //url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
            //url += "&param=@P_ITEM_TYPE=" + ddlItem.SelectedValue;

            //// To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.ShowItemStockBalance() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }   

    // This method is used to create table.
    private DataTable CreateTabel()
    {
        DataTable dtItem = new DataTable();
        dtItem.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtItem.Columns.Add(new DataColumn("ITEM_NAME", typeof(string)));
        dtItem.Columns.Add(new DataColumn("ITEM_NO", typeof(int)));
        dtItem.Columns.Add(new DataColumn("QUANTITY", typeof(double)));
        dtItem.Columns.Add(new DataColumn("BALANCE_QTY", typeof(double)));
        return dtItem;
    }

    private bool CheckDuplicateItemName(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_NAME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.CheckDuplicateItemName -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {               

                int maxVal = 0;
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();

                if (CheckDuplicateItemName(dt, ddlItem.SelectedItem.Text))
                {
                    ClearRec();                
                    Showmessage("This Item Name Already Exist");
                    return;
                }

                if (Convert.ToDouble(txtQty.Text) <= 0)
                {
                    Showmessage("Issue Quantity Should Not Be Zero.");
                    return;
                }

              
                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                }
                dr["SRNO"] = maxVal + 1;
                dr["ITEM_NAME"] = ddlItem.SelectedItem.Text; //txtItemName.Text;
                dr["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);   //Convert.ToString(hfItemName.Value);
                dr["QUANTITY"] = Convert.ToString(txtQty.Text);

                if (Convert.ToDouble(txtAvailableQty.Text) < Convert.ToDouble(txtQty.Text))
                {
                    Showmessage("Issue Qty. Should Not Be greater than Available Qty.");
                    return;
                }
                else
                {
                    dr["BALANCE_QTY"] = Convert.ToString(Convert.ToDouble(txtAvailableQty.Text) - Convert.ToDouble(txtQty.Text));
                }

                


                dr["BALANCE_QTY"] = Convert.ToString(Convert.ToDouble(txtAvailableQty.Text) - Convert.ToDouble(txtQty.Text));
                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvIssueList.DataSource = dt;
                lvIssueList.DataBind();
                lvIssueList.Visible = true;
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                if (Convert.ToDouble(txtQty.Text) <= 0)
                {
                    Showmessage("Issue Quantity Should Not Be Zero.");
                    return;
                }

                DataTable dt = this.CreateTabel();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["ITEM_NAME"] = ddlItem.SelectedItem.Text; //txtItemName.Text;
                dr["ITEM_NO"] = Convert.ToInt32(ddlItem.SelectedValue);   //Convert.ToString(hfItemName.Value);
                dr["QUANTITY"] = Convert.ToString(txtQty.Text);

                if (Convert.ToDouble(txtAvailableQty.Text) < Convert.ToDouble(txtQty.Text))
                {
                    Showmessage("Issue Qty. Should Not Be greater than Available Qty.");
                    txtAvailableQty.Focus();
                    return;
                }
                else
                {
                    dr["BALANCE_QTY"] = Convert.ToString(Convert.ToDouble(txtAvailableQty.Text) - Convert.ToDouble(txtQty.Text));
                }

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvIssueList.DataSource = dt;
                lvIssueList.DataBind();
                lvIssueList.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void ClearRec()
    {
        //txtItemName.Text = string.Empty;
        //hfItemName.Value = "";
        ddlItem.SelectedIndex = 0;
        txtQty.Text = string.Empty;
        txtAvailableQty.Text = string.Empty;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvIssueList.DataSource = dt;
                lvIssueList.DataBind();
                Showmessage("Record Deleted Successfully.");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // This method is used fetch data of the individual Query. 
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
                objCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.GetEditableDatarow -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }    


    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objKCon.GetItemsAvailableQuantity(Convert.ToInt32(ddlItem.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAvailableQty.Text = ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString();
                if (lvIssueList.Items.Count == 0)
                {
                    lvIssueList.Visible = false;
                }
                else
                {
                    lvIssueList.Visible = true;
                }

                if (ds.Tables[0].Rows[0]["ITEM_MAX_QTY"].ToString() == "0.00")
                {
                    Showmessage("No Sufficient Stock");
                    return;
                }
            }
            if (ddlItem.SelectedIndex == 0)   // 18/04/2022
            {
                txtAvailableQty.Text = string.Empty;      // 18/04/2022
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_IssueSportsItem.ddlItem_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}