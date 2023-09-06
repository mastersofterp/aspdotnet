//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : STORE
// PAGE NAME     : Str_approved_requisition_list.aspx
// CREATION DATE : 12-12-2014
// CREATED BY    : Nitin Meshram
// MODIFIED DESC : THIS PAGE IS USED FOR DISPLAY APPROVED REQUISITION LIST
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
using IITMS.NITPRM;

public partial class STORES_Reports_Str_approved_requisition_list : System.Web.UI.Page
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

                ViewState["butAction"] = "add";
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                this.FillDept();
               // ObjComman.FillDropDownList(ddlDepartment, "store_department", "MDNO", "MDNAME", "", "");

            }

            ViewState["action"] = "add";
           // txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
           // txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
        string test = ObjComman.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
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
    }


    protected void FillDept()
    {
        try
        {
            //main store user
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                ObjComman.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
                
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                ObjComman.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "mdname");
                ddlDepartment.SelectedValue = Session["strdeptcode"].ToString();
                ddlDepartment.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Stores_Masters_Dept_User.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnRpt_Click(object sender, EventArgs e)
    {
        string FromDate = string.Empty;
        string ToDate = string.Empty;
        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                ObjComman.DisplayMessage(this.Page, "To Date Should Be Greater Than Or Equal To From Date.", this.Page);
                return;
            }
            FromDate = Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy");
            ToDate = Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
        }
        else
        {
            FromDate = null;
            ToDate = null;
        }
        ShowReport("Requistion List", "Str_approved_requisition_list.rpt",FromDate,ToDate);
    }
    private void ShowReport(string reportTitle, string rptFileName,string FromDate,string ToDate)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            //url += "&param=@P_DEPTNO=" + ddlDepartment.SelectedValue.ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + ",@P_TO_DATE="+Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy");
            url += "&param=@P_DEPTNO=" + ddlDepartment.SelectedValue.ToString() + "," + "@P_FROM_DATE=" + FromDate + ",@P_TO_DATE=" + ToDate;
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updMain, updMain.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlDepartment.SelectedValue = "0";
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        //txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
        //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        
    }
}
