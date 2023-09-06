//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_DSR_Entry.aspx.cs                                                 
// CREATION DATE : 25-Nov-2010                                                        
// CREATED BY    : Kumar Premankit                                                         
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Collections.Generic;

public partial class Stores_Reports_DSR_Report : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Str_DSR_Entry_Controller objDSR = new Str_DSR_Entry_Controller();
    StoreMasterController objStrMaster = new StoreMasterController();
    Str_Invoice_Entry_Controller objInvoice = new Str_Invoice_Entry_Controller();

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
                ////Page Authorization
                CheckPageAuthorization();
                CheckMainStoreUser();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.BindDepartment();
                if (ViewState["StoreUser"] != "MainStoreUser")
                {
                    Session["MDNO"] = objCommon.LookUp("STORE_DEPARTMENTUSER", "MDNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));//Added by Vijay 03-06-2020
                    ddlDept.SelectedValue = Session["MDNO"].ToString();//Added by Vijay 03-06-2020
                    ddlDept.Enabled = false;
                }
                this.BindDSR();//Added by Vijay 03-06-2020

                //lbluser.Text = Session["userfullname"].ToString();
                //lblDept.Text = Session["strdeptname"].ToString();
            }

        }
        divMsg.InnerHtml = string.Empty;
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


    void BindDepartment()
    {
        ddlDept.Items.Clear();
        ddlDept.Items.Add("Please Select");
        ddlDept.SelectedItem.Value = "0";
        DataSet ds = objDSR.GetAllDept();
        ddlDept.DataSource = ds;
        ddlDept.DataTextField = "MDNAME";
        ddlDept.DataValueField = "MDNO";
        ddlDept.DataBind();
    }
    void BindDSR()
    {
        ddlDSR.Items.Clear();
        ddlDSR.Items.Add("Please Select");
        ddlDSR.SelectedItem.Value = "0";
        DataSet ds = objDSR.GetSingleRecordDSRBYMDNO(Convert.ToInt32(ddlDept.SelectedValue));//Previous Code
        //DataSet ds = objDSR.GetSingleRecordDSRBYMDNO(Convert.ToInt32(Session["MDNO"].ToString()));//Added by Vijay 03-06-2020

        ddlDSR.DataSource = ds;
        ddlDSR.DataTextField = "DSTK_NAME";
        ddlDSR.DataValueField = "DSTKNO";
        ddlDSR.DataBind();
    }
    void BindSession()
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add("Please Select");
        ddlSession.SelectedItem.Value = "0";
        DataSet ds = objDSR.GetAllFinancialYear();
        ddlSession.DataSource = ds;
        ddlSession.DataTextField = "FNAME";
        ddlSession.DataValueField = "FNO";
        ddlSession.DataBind();
    }
    Boolean validate_DSR()
    {
        DataSet ds = new DataSet();
        ds = objDSR.GetAllDSR();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            int dsrno = Convert.ToInt32(dr["DSTKNO"]);
            int mdno = Convert.ToInt32(dr["MDNO"]);
            string year = Convert.ToString(dr["DSTK_YEAR"]);
            string Dropyear = Convert.ToString(ddlSession.SelectedItem);
            if (Convert.ToInt32(ddlDSR.SelectedValue) == dsrno && Convert.ToInt32(ddlDept.SelectedValue) == mdno
                && Dropyear == year)
            {
                ViewState["DSR"] = dr["DSTKNO"].ToString();
                ViewState["mdno"] = dr["MDNO"].ToString();
                ViewState["year"] = dr["DSTK_YEAR"].ToString();
                return true;

            }
            else
                continue;
        }
        return false;

    }
    void DisplayMessage(string Message)
    {

        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (txtFrom.Text != string.Empty && txtTo.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
            {
                MessageBox("To Date Should Be Greater Than Or Equal To From Date ");
                return;
            }
        }
        ShowReport("Bulk_DSR", "store_deadstock.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.BindDepartment();
        this.BindDSR();
        this.BindSession();
        txtFrom.Text = string.Empty;
        txtTo.Text = string.Empty;
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["MDNO"] = ddlDept.SelectedValue;
        this.BindDSR();
    }
    protected void ddlDSR_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindSession();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (validate_DSR() == true)
        {
            txtFrom.Focus();
        }
        else
        {
            this.DisplayMessage("Cant Find DSR ! Make sure u supplied correct information");
            ddlSession.Focus();
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Stores," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + GetStudentIDs() + ",UserName=" + Session["username"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]);@P_IDNO
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_PREVSTATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@END=" + Convert.ToDateTime(txtTo.Text) + "," + "@START=" + Convert.ToDateTime(txtFrom.Text) + "," + "@DSTKNO=" + Convert.ToInt32(ViewState["DSR"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@MDNO=" + Session["MDNO"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
