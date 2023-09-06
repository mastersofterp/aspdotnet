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

public partial class STORES_Transactions_Quotation_STR_WriteOff : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    STR_DEPT_REQ_CONTROLLER objDeptReqController = new STR_DEPT_REQ_CONTROLLER();
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //dropdown();
                BindWriteoffData();
            }

        }
        //dropdown();
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


    private void BindWriteoffData()
    {

        DataSet ds = new DataSet();
        ds = objDeptReqController.GetItemWriteOff();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvitemInvoice.DataSource = ds;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = true;
                Panel1.Visible = true;
            }
            else
            {
                lvitemInvoice.DataSource = null;
                lvitemInvoice.DataBind();
                lvitemInvoice.Visible = false;
                Panel1.Visible = false;
                objCommon.DisplayUserMessage(updpnlMain, "No Record Found", Page);
            }
        }
        else
        {
            lvitemInvoice.DataSource = null;
            lvitemInvoice.DataBind();
            lvitemInvoice.Visible = false;
            Panel1.Visible = false;
            objCommon.DisplayUserMessage(updpnlMain, "No Record Found", Page);
        }
    }
    //private void dropdown()
    //{
    //    objCommon.FillDropDownList(ddlDepartment, "STORE_SUBDEPARTMENT", "SDNO", "SDNAME", "", "SDNAME");
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            DataSet ds = new DataSet();
            string chkItemNo = string.Empty;
            string chkWorking = string.Empty;
            foreach (ListViewItem i in lvitemInvoice.Items)
            {
                CheckBox chk = i.FindControl("chkSelect") as CheckBox;
                DropDownList ddl = i.FindControl("ddlCondition") as DropDownList;

                if (chk.Checked == true)
                {
                    if (ddl.SelectedValue == "1")
                    {
                        chkItemNo += chk.ToolTip.ToString() + ",";
                        count++;
                        ds = objDeptReqController.ItemWriteOff(chkItemNo);
                    }
                    if (ddl.SelectedValue == "0")
                    {
                        chkWorking += chk.ToolTip.ToString() + ",";
                        count++;
                        ds = objDeptReqController.SetWorkingStatus(chkWorking);
                    }
                }


            }


            if (count == 0)
            {
                objCommon.DisplayUserMessage(updpnlMain, "Please Select Atleast One Item", Page);
            }
            else
            {

                objCommon.DisplayUserMessage(updpnlMain, "Item Write off Successfully", Page);
                BindWriteoffData();
                return;

            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnWriteOff_Click(object sender, EventArgs e)
    {
        ShowReport("Item Write Off Report", "Item_Write_off_REPORT.rpt");
    }
    protected void btnDamaged_Click(object sender, EventArgs e)
    {
        ShowReport1("Damaged ITem Report", "Damaged_Item_report.rpt");
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
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


    private void ShowReport1(string reportTitle, string rptFileName)
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();           


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