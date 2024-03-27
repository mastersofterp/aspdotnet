//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_InvoiceReport.aspx                                                
// CREATION DATE : 23-DEC-2020                                                        
// CREATED BY    : GOPAL ANTHATI                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.Linq;

public partial class STORES_Reports_Str_InvoiceReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Str_AMC_ENT ObjEnt = new Str_AMC_ENT();
    Str_AMC_Con ObjCon = new Str_AMC_Con();

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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }
                    CheckMainStoreUser();
                    // CheckDeptStoreUser();
                    BindData();
                    Session["RecTblBillPayment"] = null;
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_ApprovalAnnualMaintenance.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        try
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_ApprovalAnnualMaintenance.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

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

    public void BindData()
    {
        try
        {
            //if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            string StoreUser = ViewState["StoreUser"].ToString();
            DataSet ds = objCommon.FillDropDown("STORE_INVOICE A LEFT JOIN STORE_PORDER B ON (B.PORDNO=A.PORDNO)", "INVTRNO,INVNO,INVDATE", "(CASE A.PORDNO WHEN 0 THEN '-' ELSE B.REFNO END) AS POREFNO", "A.MDNO = " + Convert.ToInt32(Session["strdeptcode"]), "");
            lvInvdetails.DataSource = ds;
            lvInvdetails.DataBind();
            ViewState["Action"] = "add";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_ApprovalAnnualMaintenance.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int Invtrno = int.Parse(btn.CommandArgument);
        ShowReport("INVOICE_REPORT", "Str_Invoice.rpt", Invtrno);
    }
    private void ShowReport(string reportTitle, string rptFileName, int Invtrno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_INVTRNO=" + Invtrno + "," + "@username=" + Session["userfullname"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
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