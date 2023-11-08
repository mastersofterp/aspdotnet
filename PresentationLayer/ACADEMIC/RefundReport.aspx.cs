//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : RefundReport.aspx                                               
// CREATION DATE : 22-12-2019                                                   
// CREATED BY    : Rita Munde                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using System.Text;
using System.Data;


public partial class ACADEMIC_RefundReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    //StudentFeedBack objSEB = new StudentFeedBack();
    //StudentFeedBackController objSFBC = new StudentFeedBackController();
    DailyFeeCollectionController objdfc = new DailyFeeCollectionController();

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

                    string College_code = objCommon.LookUp("REFF", "College_code", "OrganizationId = '" + Session["OrgId"].ToString() + "'");
                    ViewState["college_id"] = College_code;

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    public void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME DESC");
        this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO NOT IN (9,10)", "RCPTTYPENO");
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");                    
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
         //   DailyFeeCollectionRpt dcrReport = GetReportCriteria();
            DataSet ds = objdfc.GetRefundReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), (ddlReceiptType.SelectedValue),txtFromDate.Text,txtToDate.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment;filename=RefundReports_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updtime, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //private DailyFeeCollectionRpt GetReportCriteria()
    //{
    //    DailyFeeCollectionRpt dcrReport = new DailyFeeCollectionRpt();
    //    try
    //    {
    //        //dcrReport.SessionNo = (ddlSession.SelectedIndex > 0) ? Convert.ToInt32(ddlSession.SelectedValue) : 0;
    //        dcrReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
    //        dcrReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
    //        dcrReport.ReceiptTypes = (ddlReceiptType.SelectedIndex > 0) ? Convert.ToInt32(ddlReceiptType.SelectedValue) : 0;
    //        //dcrReport.BranchNo = (ddlReceiptType.SelectedIndex > 0) ? Convert.ToInt32(ddlReceiptType.SelectedValue) : 0;  
    //        // dcrReport.UaFNo = 2253;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_DCR_ReportUI.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return dcrReport;
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = -1;
        ddlReceiptType.SelectedIndex = -1;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("RefundReceipt", "rptRefundReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue 
                + ",@P_DEGREENO="+ 0 
                + ",@P_BRANCHNO=" + 0 
                + ",@P_RECIEPT_CODE=" + ddlReceiptType.SelectedValue
                + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_FROMDATE=" + txtFromDate.Text 
                + ",@P_TODATE="+txtToDate.Text;
            
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlReceiptType.SelectedIndex = -1;
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
    }
    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtToDate.Text = string.Empty;
        txtFromDate.Text = string.Empty;
    }
}