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
using IITMS.SQLServer.SQLDAL;

public partial class HOSTEL_CutOffStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRM = new RoomAllotmentController();
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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                //CheckPageAuthorization();
                objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                ddlSession.SelectedIndex = 1;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
            }
            
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineApply.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CutOffStudent.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CutOffStudent.aspx");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue != "0")
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0 AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.SelectedIndex = 0;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        FillListView();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string StudentIds = string.Empty;
        foreach (ListViewDataItem items in lvDetails.Items)
        {
            CheckBox chkIdno = items.FindControl("chkIdno") as CheckBox;
            if (chkIdno.Checked)
                StudentIds += chkIdno.ToolTip + ",";
        }
        if (StudentIds == "")
        {
            objCommon.DisplayMessage("Please Select Atleast one Student!", this.Page);
            return;
        }
        int output = objRM.CutOffStudent(StudentIds, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));
        if (output != -99)
        {
            objCommon.DisplayMessage("Record Saved Successfully", this.Page);
            FillListView();
        }
        else
            objCommon.DisplayMessage("Transaction Failed!!", this.Page);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void FillListView()
    {
        DataSet ds = null;
        if (rdoSelect.SelectedValue == "1")
            ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_HOSTEL_APPLY_STUDENT H ON (S.IDNO=H.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME,ENROLLNO,S.ROLLNO", "S.CAN=0 AND S.ADMCAN=0 AND H.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND H.STATUS=1 AND (S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) ", "S.STUDNAME");
        else
            ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_HOSTEL_APPLY_STUDENT H ON (S.IDNO=H.IDNO)INNER JOIN ACD_DEMAND D ON (H.IDNO=D.IDNO AND H.SESSIONNO=D.SESSIONNO)INNER JOIN ACD_DCR DR ON (H.IDNO=DR.IDNO AND H.SESSIONNO=DR.SESSIONNO)", "DISTINCT S.IDNO", "S.STUDNAME,ENROLLNO,S.ROLLNO", "S.CAN=0 AND S.ADMCAN=0 AND DR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND H.STATUS=1 AND (S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) ", "S.STUDNAME");
            
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvDetails.Visible = true;
            lvDetails.DataSource = ds;
            lvDetails.DataBind();
        }
        else
        {
            lvDetails.Visible = false;
            lvDetails.DataSource = null;
            lvDetails.DataBind();
            objCommon.DisplayMessage("Record Not Found!!", this.Page);
        }
    }
    protected void rdoSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoSelect.SelectedValue == "1")
        {
            btnSubmit.Visible = true;
            btnReceipt.Visible = false;
            btnMess.Visible = false;
            trnote.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
            btnReceipt.Visible = true;
            btnMess.Visible = true;
            trnote.Visible = false;
        }
    }
    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        string StudentIds = "0";
        foreach (ListViewDataItem items in lvDetails.Items)
        {
            CheckBox chkIdno = items.FindControl("chkIdno") as CheckBox;
            if (chkIdno.Checked)
                StudentIds += chkIdno.ToolTip + ".";
        }
        ShowReport(StudentIds,"Fee_Receipt_Hostel", "FeeCollectionReceipt_Hostel.rpt","HF");
    }

    private void ShowReport(string param, string reportTitle, string rptFileName,string Receipt_code)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + param.ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_RECEIPT_CODE=" + Receipt_code.ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_HostelStudentBonafideCertificate.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnMess_Click(object sender, EventArgs e)
    {
        string StudentIds = "0";
        foreach (ListViewDataItem items in lvDetails.Items)
        {
            CheckBox chkIdno = items.FindControl("chkIdno") as CheckBox;
            if (chkIdno.Checked)
                StudentIds += chkIdno.ToolTip + ".";
        }
        ShowReport(StudentIds, "Fee_Receipt_Hostel", "FeeCollectionReceipt_Mess.rpt", "MF");
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=StudentCutOffList";
            url += "&path=~,Reports,Hostel,rptCutOffStudent.rpt";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_STATUS=" + Convert.ToInt32(rdlEligible.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','StudentCutOffList','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "HOSTEL_CutOffStudent.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
