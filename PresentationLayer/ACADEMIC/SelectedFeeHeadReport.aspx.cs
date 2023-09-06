//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : SELECTED FEE HEAD                                                    
// CREATION DATE : 07-Aug-2009                                                          
// CREATED BY    : MANGESH BARMATE AND SANJAY RATNAPARKHI                               
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_SelectedFeeHeadReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    #region Page Events

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    //this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Load drop down lists
                    //this.objCommon.FillDropDownList(ddlCounterNo, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "", "COUNTERNO");
                    //this.objCommon.FillDropDownList(ddlPaymentMode, "ACD_PAYMENT_MODE_MAS", "PAY_MODE_CODE", "LINK_CAPTION", "LINK_CAPTION IS NOT NULL AND LINK_CAPTION <> ''", "PAY_MODE_NO");
                    this.objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "CODE", "CODE <> ''", "CODE");
                    this.objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
                    this.objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "", "");
                    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    this.objCommon.FillDropDownList(ddlReceiptType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "", "RECIEPT_TITLE");
                }
            }
            txtFromDate.Focus();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeeCollection.aspx");
        }
    }

    #endregion

    #region Form Events
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex == 0)
            {
                ShowMessage("Receipt type is required for this report.");
                ddlReceiptType.Focus();
                return;
            }
            int sequence = 0;
            string[] feeHeads = new string[6] { "0", "0", "0", "0", "0" , "0" };
            string[] feeShortName = new string[6] { "0", "0", "0", "0", "0" , "0"};

            foreach (ListViewDataItem lvitem in lvSelectedFeeItems.Items)
            {
                if ((lvitem.FindControl("chkReport") as CheckBox).Checked)
                {
                    if (sequence < 6)
                    {
                        string feeHead = (lvitem.FindControl("hidFeeHead") as HiddenField).Value;
                        feeHeads[sequence] = feeHead;

                        string feeSName = (lvitem.FindControl("lblFeeShortName") as Label).Text;
                        feeShortName[sequence] = feeSName;
                        sequence++;
                    }
                }
            }
            DailyFeeCollectionRpt dcrReport = this.GetReportCriteria();
            DailyFeeCollectionController dfcController = new DailyFeeCollectionController();
            DataSet ds = dfcController.GetSelectedFeeItem(dcrReport, feeHeads);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                this.ShowReportForSelectedFeeItem(dcrReport,feeHeads,feeShortName, "Selected_Fee_Item", "SelectedFeeHeadReport.rpt");
            else
                this.ShowMessage("No information found based on given criteria.");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Reload/refresh complete page. 
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlReceiptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptType.SelectedIndex > 0)
            {
                DailyFeeCollectionController dfcController = new DailyFeeCollectionController();
                DataSet ds = dfcController.GetRecieptFeeItems(ddlReceiptType.SelectedValue);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lvSelectedFeeItems.DataSource = ds.Tables[0];
                    lvSelectedFeeItems.DataBind();
                    lvSelectedFeeItems.Style["display"] = "block";
                }
            }
            else
                lvSelectedFeeItems.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport .ddlReceiptType_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCaution_Click(object sender, EventArgs e)
    {
        try
        {
            int selectionCount = 0;
            string selectedFeeHead = string.Empty;

            foreach (ListViewDataItem item in lvSelectedFeeItems.Items)
            {
                if ((item.FindControl("chkReport") as CheckBox).Checked)
                {
                    selectionCount++;
                    selectedFeeHead = (item.FindControl("hidFeeHead") as HiddenField).Value;
                }
            }
            if (selectionCount > 1)
            {
                ShowMessage("Selection of more than one fee head is not allowed for caution money report.");
            }
            else if ((selectionCount == 1))// && (selectedFeeHead == ConfigurationManager.AppSettings["CautionMoneyFeeHead"]))
            {
                ShowReportForCautionMoneyFeeItem();
            }
            else if (selectionCount != 1)
            {
                ShowMessage("Selected fee head is not caution money. \\nPlease select caution money fee head.");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }    
    #endregion
    
    #region Private Methods
    
    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private DailyFeeCollectionRpt GetReportCriteria()
    {
        DailyFeeCollectionRpt dcrReport = new DailyFeeCollectionRpt();
        try
        {
            dcrReport.FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
            dcrReport.ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;
            dcrReport.DegreeNo = (ddlDegree.SelectedIndex > 0) ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            dcrReport.BranchNo = (ddlBranch.SelectedIndex > 0) ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            dcrReport.YearNo = (ddlYear.SelectedIndex > 0) ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
            dcrReport.SemesterNo = (ddlSemester.SelectedIndex > 0) ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
            dcrReport.ReceiptTypes = (ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedValue : string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.GetReportCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return dcrReport;
    }

    private void ShowReportForSelectedFeeItem(DailyFeeCollectionRpt rptParams, string[] feeHeads, string[] feeShortName, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitleForSelectedFeeItem=" + reportTitle;
            url += "&pathForSelectedFeeItems=~,Reports,Academic," + rptFileName;
            url += "&FeeHead1=" + feeHeads[0] + "&FeeHead2=" + feeHeads[1] + "&FeeHead3=" + feeHeads[2] + "&FeeHead4=" + feeHeads[3] + "&FeeHead5=" + feeHeads[4] + "&FeeHead6=" + feeHeads[5];
            url += "&FeeShortName1=" + feeShortName[0] + "&FeeShortName2=" + feeShortName[1] + "&FeeShortName3=" + feeShortName[2] + "&FeeShortName4=" + feeShortName[3] + "&FeeShortName5=" + feeShortName[4] + "&FeeShortName6=" + feeShortName[5];
            url += "&ReceiptCode=" + rptParams.ReceiptTypes + "&FromDate=" + txtFromDate.Text.Trim() + "&@P_RECIEPT_CODE=" + ddlReceiptType.SelectedItem.Text;
            url += "&ToDate=" + txtToDate.Text + "&SemesterNo=" + Convert.ToInt32(ddlSemester.SelectedValue);
            url += "&BranchNo=" + Convert.ToInt32(ddlBranch.SelectedValue) + "&DegreeNo=" + Convert.ToInt32(ddlDegree.SelectedValue);
            url += "&YearNo=" + Convert.ToInt32(ddlYear.SelectedValue);
            url += "&paramForSelectedFeeItemsRpt=" + this.GetReportParameters(rptParams, feeHeads, feeShortName);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    // method for caution money

    private void ShowReportForCautionMoneyFeeItem()
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "PageTitleForSelectedCautionMoneyFeeItem=Caution_Money_Report";
            url += "&Path=~,Reports,Academic,FeeHeadCautionMoneyReport.rpt";
            url += "&Param="+ Session["userfullname"].ToString() + this.GetParametersForCautionMoneyData();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'> try{";
            divMsg.InnerHtml += " window.open('" + url + "','Caution_Money_Report','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description); } </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(DailyFeeCollectionRpt dcrRpt, string[] feeHeads, string[] feeShortName)
    {
        StringBuilder param = new StringBuilder();
        try
        {
            param.Append("CollegeName=" + Session["coll_name"].ToString());
            param.Append(",UserName=" + Session["userfullname"].ToString());
            param.Append(",Period_From=" + dcrRpt.FromDate.ToShortDateString());
            param.Append(",Period_To=" + dcrRpt.ToDate.ToShortDateString());
            //param.Append(",@P_RECIEPT_CODE=" + dcrRpt.ReceiptTypes);
            param.Append(",Degree=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedItem.Text : "0"));
            param.Append(",Branch=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedItem.Text : "0"));
            param.Append(",Year=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedItem.Text : "0"));
            param.Append(",Semester=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedItem.Text : "0"));
            param.Append(",Head1=" + feeHeads[0]);
            param.Append(",Head2=" + feeHeads[1]);
            param.Append(",Head3=" + feeHeads[2]);
            param.Append(",Head4=" + feeHeads[3]);
            param.Append(",Head5=" + feeHeads[4]);
            param.Append(",Head6=" + feeHeads[5]);
            param.Append(",FeeShortname1=" + feeShortName[0]);
            param.Append(",FeeShortname2=" + feeShortName[1]);
            param.Append(",FeeShortname3=" + feeShortName[2]);
            param.Append(",FeeShortname4=" + feeShortName[3]);
            param.Append(",FeeShortname5=" + feeShortName[4]);
            param.Append(",FeeShortname6=" + feeShortName[5]);
            //param.Append(",@P_COLLEGE_CODE=" + Session["colcode"].ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.GetReportParameters() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }

    private string GetParametersForCautionMoneyData()
    {
        StringBuilder param = new StringBuilder();
        try
        {
            param.Append("&@P_RECIEPT_CODE=" + ((ddlReceiptType.SelectedIndex > 0) ? ddlReceiptType.SelectedValue : "0"));
            param.Append("&@P_FROM_DATE=" + txtFromDate.Text.Trim());
            param.Append("&@P_TO_DATE=" + txtToDate.Text.Trim());
            param.Append("&@P_SEMESTERNO=" + ((ddlSemester.SelectedIndex > 0) ? ddlSemester.SelectedValue : "0"));
            param.Append("&@P_BRANCHNO=" + ((ddlBranch.SelectedIndex > 0) ? ddlBranch.SelectedValue : "0"));
            param.Append("&@P_DEGREENO=" + ((ddlDegree.SelectedIndex > 0) ? ddlDegree.SelectedValue : "0"));
            param.Append("&@P_YEAR=" + ((ddlYear.SelectedIndex > 0) ? ddlYear.SelectedValue : "0"));
            param.Append("&@P_FEEHEAD=" + ConfigurationManager.AppSettings["CautionMoneyFeeHead"]);
            param.Append("&@P_PAIDAMOUNT=" +(chkWithoutZero.Checked ? chkWithoutZero.Checked: false));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_SelectedFeeHeadReport.GetReportParametersForCautionMoney --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
        return param.ToString();
    }
    
    #endregion   
}