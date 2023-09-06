using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.SQLServer.SQLDAL;

public partial class STORES_Reports_Str_Search_Requisition : System.Web.UI.Page
{
    Common objCommon = new Common();
    Masters objMasters = new Masters();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();

    string UsrStatus = string.Empty;

    //Check Logon Status and Redirect To Login Page(Default.aspx) if not logged in
    protected void Page_Load(object sender, EventArgs e)
    {
        //For displaying user friendly messages
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\js\jquery-1.4.2.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\js\jquery.ui.widget.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\js\jquery.ui.button.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective3", ResolveUrl(@"~\impromptu\jquery-impromptu.2.6.min.js"));

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

 
   

    //fill dropdownlist on the basis of selected group of item
    protected void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "SDNO>0", "SDNAME");
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }
    }

    protected void btnRpt_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
        {
            objCommon.DisplayMessage(this.Page, "To Date Should Be Greater Than Or Equal To From Date", this.Page);
            return;
        }
        //ShowReport("GoodsIssueReport", "Str_DeptIssueReport.rpt");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlDepartment.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        lvDepartment.DataSource = null;
        lvDepartment.DataBind();

        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtFromDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
            {
                objCommon.DisplayMessage(this.Page, "To Date Should Be Greater Than Or Equal To From Date", this.Page);
                return;
            }
        }
       
        int dept = 0;
        if (Convert.ToInt32(ddlDepartment.SelectedValue) > 0)
        {
            dept = Convert.ToInt32(ddlDepartment.SelectedValue);
        }
        else
        {
            dept = 0;
        }
        DateTime DiF = DateTime.MinValue;
        DateTime DiT = DateTime.MinValue;

        if (!txtFromDate.Text.Equals(string.Empty)) DiF = Convert.ToDateTime(txtFromDate.Text);
        if (!txtToDate.Text.Equals(string.Empty)) DiT = Convert.ToDateTime(txtToDate.Text);

        DataSet ds = objDeptReqController.DisplaySearchReq(DiF, DiT, dept);

        if (ds.Tables[0].Rows.Count > 0 && ds != null)
        {
            lvDepartment.DataSource = ds;
            lvDepartment.DataBind();
        }
        else
        {
            lvDepartment.DataSource = null;
            lvDepartment.DataBind();
            objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
            return;
        }


    }
    protected void btnEditPartyCategory_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEditPartyCategory = sender as ImageButton;
        int reqtrno = int.Parse(btnEditPartyCategory.CommandArgument);
       
        ShowReport3("Quotation_Report", "StrDepartmentPraposal.rpt", reqtrno);

    }
    private void ShowReport3(string reportTitle, string rptFileName, int reqtrno)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;

            //url += "&param=@P_SDNO=" + ddlDepartment.SelectedValue + ",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + ",@P_FLAG=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_REQTRNO=" + reqtrno;  // +",@P_FROM_DATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtToDate.Text).ToString("yyyy-MM-dd") + ",@P_FLAG=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, ex.Message.ToString(), this);
        }

    }
}