//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_ISSUE_ITEM.aspx.cs                                                 
// CREATION DATE : 27-May-2014                                                        
// CREATED BY    : VINOD ANDHALE                                                        
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

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

public partial class STORES_Reports_Str_IssueItemReport : System.Web.UI.Page
{

    Common objCommon = new Common();
    Masters objMasters = new Masters();


    string UsrStatus = string.Empty;

    //Check Logon Status and Redirect To Login Page(Default.aspx) if not logged in
    protected void Page_Load(object sender, EventArgs e)
    {
        //For displaying user friendly messages
        //Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        //Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

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
            DataSet ds = new DataSet();
            ViewState["action"] = "add";
            Session["butAction"] = "add";

            if (Session["userno"] != null && Session["usertype"].ToString() != "1")
            {
                Session["strdeptcode"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
            }
            else if (Session["userno"] != null && Session["usertype"].ToString() == "1")
            {
                //int mdno = 0;
                //Session["strdeptcode"] = mdno.ToString();
            }
            else
            {
                objCommon = new Common();
                objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured.ToString(), this);
            }

            this.FillDepartment();
            // txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
            // txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CheckMainStoreUser();
            FillRequisition();
            FillIssueNo();
            //if (ViewState["StoreUser"] != "MainStoreUser")
            //{
            //    Session["MDNO"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));//Added by Vijay 03-06-2020
            //   // ddlDepartment.SelectedValue = Session["SubDepID"].ToString();//Added by Vijay 03-06-2020
            //    //ddlDepartment.Enabled = false;
            //}
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

    //Generate the report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {


            String IssueType = string.Empty;
            if (rdbRequisition.Checked)
            {
                IssueType = "R";
            }
            else
            {
                IssueType = "I";
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;

            // string type = "R";
            url += "&param=@UserName=" + Convert.ToString(Session["UserName"]) + ",@P_ISSUENO=" + ddlIssueNo.SelectedValue + ",@P_REQTRNO=" + ddlRequisitionNo.SelectedValue + ",@P_TYPE=" + IssueType + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();


            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);

















            //string Script = string.Empty;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            //url += "Reports/commonreport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,STORES," + rptFileName;

            //url += "&param=@P_SDNO=" + ddlDepartment.SelectedValue + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + ",@P_FLAG=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MDNO=" + Session["MDNO"].ToString();
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }

    }

    private void ShowIndentSlipReport(string reportTitle, string rptFileName)
    {
        try
        {
           
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            // string type = "R";
            url += "&param=";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }

    }


    //fill dropdownlist on the basis of selected group of item
    protected void FillDepartment()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "SDNO>0", "SDNAME");
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }
    }
    protected void btnRpt_Click(object sender, EventArgs e)
    {
        // ShowReport("GoodsIssueReport", "Str_GoodsIssueReport.rpt");

        if (rdbRequisition.Checked && ddlRequisitionNo.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updpanel, "Please Select Requisition No.", this);
            return;
        }
        if (rdbDirectIssue.Checked && ddlIssueNo.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updpanel, "Please Select Issue Slip No.", this);
            return;
        }
        if (rdbIndentSlip.Checked)
            ShowIndentSlipReport("Indent Slip", "IndentSlip_Report.rpt");
        else
            ShowReport("IssueItemReport", "Str_IssueItem_Report.rpt");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlDepartment.SelectedValue = "0";

        //rdbRequisition_CheckedChanged(sender, e);
        divReq.Visible = true;
        divIssue.Visible = false;
        rdbRequisition.Checked = true;
        rdbDirectIssue.Checked = false;
        rdbIndentSlip.Checked = false;
        ddlIssueNo.SelectedIndex = 0;
        ddlRequisitionNo.SelectedIndex = 0;

    }
    protected void rdbRequisition_CheckedChanged(object sender, EventArgs e)
    {
        divReq.Visible = true;
        divIssue.Visible = false;
        //FillRequisition();
        ddlIssueNo.SelectedIndex = 0;
        ddlRequisitionNo.SelectedIndex = 0;
    }
    protected void rdbDirectIssue_CheckedChanged(object sender, EventArgs e)
    {
        divIssue.Visible = true;
        divReq.Visible = false;
        //FillIssueNo();
        ddlIssueNo.SelectedIndex = 0;
        ddlRequisitionNo.SelectedIndex = 0;
    }
    protected void FillIssueNo()
    {
        try
        {
            objCommon.FillDropDownList(ddlIssueNo, "STORE_JVSTOCK_MAIN", "JVTRAN_ID", "JVTRAN_SLIP_NO", "", "");
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }
    }
    //fill dropdownlist on the basis of selected group of item
    protected void FillRequisition()
    {
        try
        {
            objCommon.FillDropDownList(ddlRequisitionNo, "STORE_REQ_MAIN", "REQTRNO", "REQ_NO", "REQTRNO IN (SELECT DISTINCT  REQTRNO FROM STORE_JVSTOCK_MAIN )", "");
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }
    }
    protected void rdbIndentSlip_CheckedChanged(object sender, EventArgs e)
    {
        divIssue.Visible = false;
        divReq.Visible = false;
    }
}
