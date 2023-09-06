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
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Reports_str_purchase_summary : System.Web.UI.Page
{
    Common objCommon = new Common();
    Masters objMasters;

    //UserAuthorizationController objucc;
    string UsrStatus = string.Empty;

    //Check Logon Status and Redirect To Login Page(Default.aspx) if not logged in
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            // objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!Page.IsPostBack)
        {

            CheckPageAuthorization();
            CheckMainStoreUser();
            // DataSet ds = new DataSet();
            // //if (HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()] == null)
            // //{
            // //    //ds = objCommon.FillDropDown("CCMS_HELP_client", "HELPDESC", "PAGENAME", "PAGENAME='" + objCommon.GetCurrentPageName() + "'", "");
            // //}
            // //else
            // //{
            //     ds = (DataSet)HttpRuntime.Cache["MENUHELPMASTER" + Session["DataBase"].ToString().Trim()];
            //     DataView dv = ds.Tables[0].DefaultView;
            // //    dv.RowFilter = "PAGENAME='" + objCommon.GetCurrentPageName() + "'";
            //     ds.Tables.Remove("Table");
            //     ds.Tables.Add(dv.ToTable());
            //// }
            // if (ds.Tables[0].Rows.Count > 0)
            // {
            //     lblHelp.Text = ds.Tables[0].Rows[0]["HELPDESC"].ToString();
            // }
            // else
            // {
            //     lblHelp.Text = "No Help Present!";
            // }
            ViewState["action"] = "add";
            Session["butAction"] = "add";

            //if (Session["userno"] != null && Session["usertype"].ToString() != "1")
            //{
            //    Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            //}
            //else if (Session["userno"] != null && Session["usertype"].ToString() == "1")
            //{
            //    //int mdno = 0;
            //    //Session["strdeptcode"] = mdno.ToString();
            //}
            Session["strdeptcode"] = "0";
            if (ViewState["StoreUser"] != "MainStoreUser")
            {
                Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            }
          
            else
            {
                objCommon = new Common();
                //  objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
            }

            drpoDowntr.Visible = false;

            this.FillItemsGroup();
            txtFrmDt.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
            txtTodt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
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
                objCommon.ShowError(Page, "DSR_Report.aspx.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
            return false;
        }
    } //added by vijay 03-06-2020
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
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
    } //added by vijay 03-06-2020

    //reload the page.
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    //to show report click on report button.
    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (txtFrmDt.Text != string.Empty && txtTodt.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFrmDt.Text) > Convert.ToDateTime(txtTodt.Text))
            {
                MessageBox("To Date Should Be Greater Than Or Equal To From Date ");
                return;
            }
        }
        //string uRight = GetUserRight();
        //string TwoCharReport = uRight.Substring(4, 2).ToString();
        //if (TwoCharReport == "YR")
        //{
        ShowReport("PurchaseItems", "Str_Purchase_Item.rpt");
        //}
        //else
        //{
        //  //  objCommon.DisplayMessage(updpanel, Common.Message.NoReport, this);
        //    return;
        //}
    }

    //fill dropdownlist on the basis of selected group of item
    protected void FillItemsGroup()
    {
        try
        {
            if (rblGroup.SelectedValue == "1")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "", "MIGNAME");
            else if (rblGroup.SelectedValue == "2")
                objCommon.FillDropDownList(ddlItem, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "", "MISGNAME");
            else if (rblGroup.SelectedValue == "3")
                objCommon.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "", "ITEM_NAME");


        }
        catch (Exception ex)
        {
            objCommon = new Common();
            //objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
        }
    }


    protected void rblGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.FillItemsGroup();
    }

    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
    {

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFrmDt.Text)));
        Fdate = Fdate.Substring(0, 10);
        string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
        Ldate = Ldate.Substring(0, 10);

        int itemno = 0;
        if (rblSelectAllItem.SelectedValue == "0")
        {
            itemno = 0;
        }
        else if (rblSelectAllItem.SelectedValue == "1")
        {
            itemno = Convert.ToInt32(ddlItem.SelectedValue);
        }
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_ReportName=" + reportTitle + ",@P_ITEMGROUP=" + rblGroup.SelectedValue + ",@P_ITEMWISE=" + rblSelectAllItem.SelectedValue + ",@P_ITEMNO=" + itemno + ",@P_FDATE=" + Fdate + ",@P_TDATE=" + Ldate + ",@P_MDNO=" + Session["strdeptcode"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {

            objCommon = new Common();
            // objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured, this);
        }

    }

    //to give rights to the user to generate the report.
    //private string GetUserRight()
    //{
    //   // objucc = new UserAuthorizationController(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());
    //    DataTable dtrights = objucc.GetMenuStatusUserwise(Convert.ToInt32(Session["idno"]), "Stores/Report/" + objCommon.GetCurrentPageName());
    //    if (dtrights != null)
    //    {
    //        if (dtrights.Rows.Count > 0)
    //        {
    //            UsrStatus = dtrights.Rows[0]["STATUS"].ToString().Trim();
    //            return UsrStatus;
    //        }
    //        else
    //        {
    //            objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //            return "INVALID";
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayUserMessage(updpanel, "Invalid Page Request!", this);
    //        return "INVALID";
    //    }
    //}


    //dropdownlist will visible or not 
    protected void rblSelectAllItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelectAllItem.SelectedValue == "0")
            drpoDowntr.Visible = false;
        else if (rblSelectAllItem.SelectedValue == "1")
            drpoDowntr.Visible = true;
    }
    //protected void txtTodt_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime From_date, To_date;
    //    From_date = Convert.ToDateTime(txtFrmDt.Text);
    //    To_date = Convert.ToDateTime(txtTodt.Text);
    //    if (To_date < From_date)
    //    {
    //        Response.Write("<script language='javascript'>alert('To date must be greater than or equal to from date.');</" + "script>");
    //        txtTodt.Text = string.Empty;
    //    }
        
    //}
}
