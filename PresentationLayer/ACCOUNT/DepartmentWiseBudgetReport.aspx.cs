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

public partial class ACCOUNT_DepartmentWiseBudgetReport : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    CostCenterController objCostCenterController = new CostCenterController();
    GridView gvBudgetReport = new GridView();


    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                if (Convert.ToInt32(Session["usertype"].ToString()) == 1)
                {
                    objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
                }
                else
                {
                    objCommon.FillDropDownList(ddldept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPT");
                    ddldept.SelectedValue = Session["UA_EmpDeptNo"].ToString();
                    ddldept.Enabled = false;
                }

                getCollegeName();
            }
            txtFrmDate.Text = "01/04/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year - 1).ToString() : (DateTime.Now.Year).ToString());
            txtUptoDate.Text = "31/03/" + ((DateTime.Now.Month < 4) ? (DateTime.Now.Year).ToString() : (DateTime.Now.Year + 1).ToString());
        }
    }
    protected void getCollegeName()
    {
        DataSet ds = objCommon.FillDropDown("REFF", "CollegeName", "College_address", "College_code=" + Session["colcode"].ToString(), "");
        if (ds != null)
        {
            Session["CollegeName"] = ds.Tables[0].Rows[0]["CollegeName"].ToString();
            string s = txtFrmDate.Text + txtUptoDate.Text;
            Session["clgAddress"] = ds.Tables[0].Rows[0]["College_address"].ToString().Replace(",", "") + s;
        }
    }
    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdbApplied_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnrpt_Click(object sender, EventArgs e)
    {
        Session["Report_type"] = null;
        Session["status"] = null;
        if (rdbApplied.Checked == true)
        {
            Session["Report_type"] = "Applied Budget";
            Session["reportname"] = "DepartmentWiseBudgetReport.rpt";
            Session["status"] = "P";
        }
        else if (rdbApproved.Checked == true)
        {
            Session["Report_type"] = "Approved Budget";
            Session["reportname"] = "DepartmentWiseBudgetReport.rpt";
            Session["status"] = "A";
        }
        string s = txtFrmDate.Text.Substring(6) + "-" + txtUptoDate.Text.Substring(6);
        Session["Report_type"] = Session["Report_type"] + "   " + s;
        ShowReport("Budget_Report", Session["reportName"].ToString());
        Session["Report_type"] = string.Empty;
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("account")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ACCOUNT," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFrmDate.Text.ToString()).ToString("yyyy-MMM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(txtUptoDate.Text.ToString()).ToString("yyyy-MMM-dd") + ",@P_STATUS=" + Session["status"].ToString() + ",@BUDGETTYPE=" + Session["Report_type"].ToString() + ",@P_DEPT_ID=" + ddldept.SelectedValue + ",@Department=Department : "+ddldept.SelectedItem;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.UPBudget, UPBudget.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BUDGETREPORT.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}