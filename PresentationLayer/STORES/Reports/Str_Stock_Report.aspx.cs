
//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : STR_STOCK_REPORT
// CREATION DATE : 23.04.2014
// CREATED BY    : VINOD ANDHALE
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY STOCK SUMMARY REPORT BY DATE-WISE
// MODIFIED BY   : 
// MODIFIED DATE : 
// MODIFIED DESC : 
//======================================================================================


using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.UAIMS;
using IITMS;



public partial class STORES_Reports_Stock_Report : System.Web.UI.Page
{
    Common ObjComman = new Common();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
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
                    // lblHelp.Text = ObjComman.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //lbluser.Text = Session["userfullname"].ToString();
                //lblDept.Text = Session["strdeptname"].ToString();
                ViewState["butAction"] = "add";
                //Session["dtitems"] = null;
                //InitializeItemTable();
                //drpoDowntr.Visible = false;
                FillItemsGroup();
                CheckMainStoreUser();
                ObjComman.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT","MDNO","MDNAME","","");
                ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
               // Session["strdeptcode"] = "0";
                //if (ViewState["StoreUser"] != "MainStoreUser")
                //{
                    //Session["strdeptcode"] = ObjComman.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                   
                //}
            }

            ViewState["action"] = "add";
            txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
    private bool CheckMainStoreUser()
    {
        try
        {
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "DSR_Report.aspx.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
            return false;
        }
    } //added by vijay 03-06-2020
    private bool CheckDeptStoreUser()
    {
        string test = ObjComman.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        string deptStoreUser = ObjComman.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

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
    } //added by vijay 03-06-2020
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

    protected void btnRpt_Click(object sender, EventArgs e)
    {
        ShowReport("Stock Register", "Str_Stock_Report.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        string fromDate = (String.Format("{0:u}", Convert.ToDateTime(txtFromDate.Text)));
        fromDate = fromDate.Substring(0, 10);
        string toDate = (String.Format("{0:u}", Convert.ToDateTime(txtToDate.Text)));
        toDate = toDate.Substring(0, 10);

        int itemno = 0;
        if (rblGroup.SelectedValue == "1")
            itemno = Convert.ToInt32(ddlItem.SelectedValue);//Main Item Group
        else if (rblGroup.SelectedValue == "2")
            itemno = Convert.ToInt32(ddlItem.SelectedValue);//Sub Item Group
        else if (rblGroup.SelectedValue == "3")
            itemno = Convert.ToInt32(ddlItem.SelectedValue);//Item

        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_FROMDATE=" + fromDate + "," + "@P_ENDDATE=" + toDate + "," + "@username=" + Session["userfullname"].ToString() + ",@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMNO=" + itemno + ",@P_MDNO=" + Session["strdeptcode"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }






















        //try
        //{

        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "pagetitle=" + reportTitle;
        //    url += "&path=~,Reports,STORES," + rptFileName;
        //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_FROMDATE=" + fromDate + "," + "@P_ENDDATE=" + toDate + "," + "@username=" + Session["userfullname"].ToString();

        //    //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //    //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //    //divMsg.InnerHtml += " </script>";


        //    //To open new window from Updatepanel
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //    sb.Append(@"window.open('" + url + "','','" + features + "');");
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        ObjComman.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        ObjComman.ShowError(Page, "Server Unavailable.");
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        rblGroup.SelectedValue = "1";
        ddlItem.SelectedIndex = 0;
    }

    protected void FillItemsGroup()
    {
        try
        {
            if (rblGroup.SelectedValue == "1")
                ObjComman.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
            else if (rblGroup.SelectedValue == "2")
                ObjComman.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "MISGNAME");
            else if (rblGroup.SelectedValue == "3")
                ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");
        }
        catch (Exception ex)
        {
            ObjComman = new Common();
            ObjComman.DisplayMessage(updpnlMain, ex.Message.ToString(), this);
        }
    }

    protected void rblGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.FillItemsGroup();
    }

    protected void rblSelectAllItem_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}
