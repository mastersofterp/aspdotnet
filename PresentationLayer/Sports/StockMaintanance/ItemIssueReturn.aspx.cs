//=======================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : SPORTS
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DATE : 25-MAY-2017
// DESCRIPTION   : THIS FORM IS USED TO RETURN ISSUE ITEMS TO SPORTS DEPT
//=======================================================================
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

public partial class Sports_StockMaintanance_ItemIssueReturn : System.Web.UI.Page
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

                    ViewState["SRNO"] = 0;

                    objCommon.FillDropDownList(ddlTeams, "SPRT_TEAM_MASTER TM LEFT JOIN ACD_COLLEGE_MASTER CM ON (TM.COLLEGE_NO = CM.COLLEGE_ID)", "TEAMID", "TEAMNAME+ ' - ' +(CASE TM.COLLEGE_NO WHEN 0 THEN TM.COLLEGE_NAME ELSE CM.COLLEGE_NAME END) AS TEAMNAME", "TM.TEAM_TYPE='U'", "TM.TEAMNAME");
                    objCommon.FillDropDownList(ddlClubs, "SPRT_CLUB_MASTER", "CLUBID", "CLUBNAME", "", "CLUBID");
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlTeams_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlIssueDate, "SPRT_ISSUE_ITEM", "ISSUE_ID", "CONVERT(VARCHAR(30),ISSUE_DATE, 113) AS ISSUE_DATE", "TEAMID= " + Convert.ToInt32(ddlTeams.SelectedValue), "ISSUE_ID DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ddlTeams_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlIssueDate, "SPRT_ISSUE_ITEM", "ISSUE_ID", "CONVERT(VARCHAR(30),ISSUE_DATE, 113) AS ISSUE_DATE", "CLUBID= " + Convert.ToInt32(ddlClubs.SelectedValue), "ISSUE_ID DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ddlTeams_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlIssueDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objKCon.GetItemIssueDetailsOnIssueDate(Convert.ToInt32(ddlIssueDate.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvIssueList.DataSource = ds.Tables[0];
                lvIssueList.DataBind();
                lvIssueList.Visible = true;
                Session["RecTbl"] = ds.Tables[0];
                txtFinalRemark.Text = ds.Tables[0].Rows[0]["FINAL_REMARK"].ToString();
            }
            else
            {
                lvIssueList.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ddlIssueDate_SelectedIndexChanged -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
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
                lvIssueList.DataSource = ds;
                lvIssueList.DataBind();
            }
            else
            {
                lvIssueList.DataSource = null;
                lvIssueList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.BindList -> " + ex.Message + " " + ex.StackTrace);
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
        ViewState["action"] = "add";
        lvIssueList.DataSource = null;
        lvIssueList.DataBind();
        lvIssueList.Visible = false;
        if (ddlIssueDate.SelectedIndex > 0)   // 02/03/2022
        {
            ddlIssueDate.SelectedIndex = 0;
        }
        txtFinalRemark.Text = string.Empty;
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



                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ShowDetails() -> " + ex.Message + " " + ex.StackTrace);
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
            objEK.ISSUE_ID = Convert.ToInt32(ddlIssueDate.SelectedValue);
            objEK.USERID = Convert.ToInt32(Session["userno"]);
            objEK.REMARK = txtFinalRemark.Text == string.Empty ? string.Empty : txtFinalRemark.Text;

            DataTable ReturnTbl = new DataTable("RetTbl");
            ReturnTbl.Columns.Add("ISSUE_ITEM_ID", typeof(int));
            ReturnTbl.Columns.Add("ISSUE_ID", typeof(int));
            ReturnTbl.Columns.Add("ITEM_NO", typeof(int));
            ReturnTbl.Columns.Add("SRNO", typeof(int));
            ReturnTbl.Columns.Add("RETURN_STATUS", typeof(string));
            ReturnTbl.Columns.Add("REMARK", typeof(string));


            DataRow dr = null;
            foreach (ListViewItem i in lvIssueList.Items)
            {
                HiddenField HDNIssueDetailsId = (HiddenField)i.FindControl("hdnIssueDetailsId");
                HiddenField HDNIssueId = (HiddenField)i.FindControl("hdnIssueId");
                HiddenField HDNItemNo = (HiddenField)i.FindControl("hdnItemNo");
                Label LblSRNO = (Label)i.FindControl("lblSRNO");
                CheckBox chkReturnStatus = (CheckBox)i.FindControl("chkReturn");
                TextBox TXTRemark = (TextBox)i.FindControl("txtRemark");

                if (chkReturnStatus.Checked == true)
                {
                    dr = ReturnTbl.NewRow();
                    dr["ISSUE_ITEM_ID"] = HDNIssueDetailsId.Value;
                    dr["ISSUE_ID"] = HDNIssueId.Value;
                    dr["ITEM_NO"] = HDNItemNo.Value;
                    dr["SRNO"] = LblSRNO.Text;
                    dr["RETURN_STATUS"] = "1";
                    dr["REMARK"] = TXTRemark.Text;

                    ReturnTbl.Rows.Add(dr);
                }
                else
                {
                    Showmessage("Please Select Return Status.");
                }
            }

            objEK.ISSUE_RETURN_TBL = ReturnTbl;


            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {                   
                    CustomStatus cs = (CustomStatus)objKCon.IssueReturnInsertUpdate(objEK);
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
                        CustomStatus cs = (CustomStatus)objKCon.IssueReturnInsertUpdate(objEK);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            clear();
                            Showmessage("Record Updated Successfully.");
                            ViewState["action"] = "add";
                           // BindList(Convert.ToChar(rdbIssueTo.SelectedValue));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
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
            }
            else
            {
                trTeams.Visible = false;
                trClubs.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }       
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowIssueReturnDetails("IssueReturnDetails", "IssueReturnDetailsReport.rpt");
    }

    private void ShowIssueReturnDetails(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("sports")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=pdf";
            //url += "&filename=IssueReturnDetails";  18/04/2022
            url += "&filename=IssueReturnDetailsReport.pdf";    //18/04/2022
            url += "&path=~,Reports,Sports," + rptFileName;
            url += "&param=";

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ShowIssueReturnDetails() --> " + ex.Message + " " + ex.StackTrace);
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
                objUCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.ShowItemStockBalance() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
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
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
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
                objCommon.ShowError(Page, "Sports_StockMaintanance_ItemIssueReturn.GetEditableDatarow -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }






}