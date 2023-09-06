using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STORES_Reports_No_Due_Report : System.Web.UI.Page
{
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
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
                //BindEmployee();
                BindUserType();

            }
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

    private void BindUserType()
    {
        try
        {
            objCommon.FillDropDownList(ddlDept, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindEmployee()
    {
        try
        {
            //int type = Convert.ToInt32(ddlUserType.SelectedValue);
            // objCommon.FillDropDownList(ddlEmployees, "User_ACC", "UA_NO", "UA_FULLNAME", "UA_Type=" + type, "UA_FULLNAME");
            int payrolldno = Convert.ToInt32(objCommon.FillDropDown("STORE_SUBDEPARTMENT", "PAYROLL_SUBDEPTNO", "PAYROLL_SUBDEPTNO", "SDNO=" + ddlDept.SelectedValue, "").Tables[0].Rows[0][0].ToString());
            objCommon.FillDropDownList(ddlEmployees, "PAYROLL_EMPMAS a inner join User_Acc b on (a.IDNO=b.UA_IDNO)", "distinct b.ua_no", "ISNULL(b.UA_FULLNAME,'')+'' UA_FULLNAME", "(b.ua_type=5 OR b.ua_type=4 or b.UA_TYPE=1 or b.ua_type=3) and a.SUBDEPTNO=" + payrolldno, "ua_no,UA_FULLNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_vendor_Master.BindListViewPartyCategory-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lvEmployee.Visible == true)
        {
            btnButtons.Visible = true;
        }
        else
        {
            btnButtons.Visible = false;
        }
        DataSet ds = null;
        int id = Convert.ToInt32(ddlEmployees.SelectedValue);
        ds = objStrMaster.GetEmployeeItemDetails(id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvEmployee.DataSource = ds;
            lvEmployee.DataBind();
            lvEmployee.Visible = true;
            btnButtons.Visible = true;
        }
        else
        {
            lvEmployee.DataSource = null;
            lvEmployee.DataBind();
            lvEmployee.Visible = false;
            btnButtons.Visible = false;
        }



    }



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("No_DUE_Report", "No_Due_Report.rpt", "pdf");
    }

    private void ShowReport(string reportTitle, string rptFileName, string format)
    {
        try
        {
            //GetStudentIDs();
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Stores," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UA_No=" + Convert.ToInt32(ddlEmployees.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEmployees.SelectedValue = "0";
        lvEmployee.Visible = false;
        btnButtons.Visible = false;
        ddlDept.SelectedValue = "0";
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }
}