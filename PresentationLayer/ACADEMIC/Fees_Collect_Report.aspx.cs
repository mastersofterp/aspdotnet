﻿//======================================================================================
// PROJECT NAME  : RF-Common Code                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : STUDENT FEES RELATED REPORT                                 
// CREATION DATE : 02-JULY-2013                                                       
// CREATED BY    : ASHISH DHAKATE                                                           
// MODIFIED DATE : 24-SEPT-2019
// MODIFIED BY   : Rita Munde                                                  
// MODIFIED DESC : Add Radio button for Excess Amount.....  
// MODIFIED DATE : 28-02-2024
// MODIFIED BY   : Vipul Tichkule   
// MODIFIED DESC : Added Fliter Reciept Type in Tally Integration Report (TkNo.52451)                                                                 
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class CourseWise_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeCntrl = new FeeCollectionController();
    //ConnectionString
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Config objConfig = new Config();
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
                PopulateDropDown();
                btnExcel.Visible = false;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                



                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER
                {


                    objCommon.FillDropDownList(ddlReport, "ACD_FEESREPORTS_MASTER", "RID", "REPORT_NAME", "Org_ID=1", "RID");
                   
                    //bntStudentArrears.Visible = true;
                    //btnStudentArrearPdf.Visible = true;
                    //btnStudentArrearsHeadwise.Visible = true;
                    //btnBalanceReport.Visible = true;
                    //btnstudLedgerReport.Visible = true;
                    //btnStudentledgerExl.Visible = true;
                    //btnledgerExcelFormatII.Visible = true;
                    //btnSummaryReport.Visible = true;
                    //divPaymentMode.Visible = false;
                    //btnDcrExcelFormatII.Visible = true;
                    //reportSelection.Visible = true;
                    ////btnOnlineDcrReport.Visible = true;
                    //btnTallyIntegration.Visible = true;
                    //btnCancelrecieptsummary.Visible = true; // added by Nehal on 27062023
                }

                 if (Session["OrgId"].ToString() == "5" )// For Jecrc
                 {
                     objCommon.FillDropDownList(ddlReport, "ACD_FEESREPORTS_MASTER", "RID", "REPORT_NAME", " Org_ID=5 OR COMMAN_FLAG=1", "RID");
                    //btnExcelConsolidated.Visible = true;
                 }

                 if (Session["OrgId"].ToString() != "1" && Session["OrgId"].ToString() != "6" && Session["OrgId"].ToString() != "5")// For ALL common code
                 {

                     objCommon.FillDropDownList(ddlReport, "ACD_FEESREPORTS_MASTER", "RID", "REPORT_NAME", "COMMAN_FLAG=1 ", "RID");
                     //btnOverallOutstandingReport.Visible = true;
                     //btnSummaryReport.Visible = true;
                 }

            }

        }
        //Blank Div
        divMsg.InnerHtml = string.Empty;
        trSemester.Visible = true;
        PnlSemesterwiseOS.Visible = false;
        //btnReport.Visible = false;
        //btnExcel.Visible = true;
        btnOSUptoSemReport.Visible = false;
        btnFutureOSReport.Visible = false;
        pnlDemand.Visible = true;
        pnlSem.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseWise_Registration.aspx");
        }
    }
    //private string GetDegreeNew()
    //{
    //    string DegreeNos = "";


    //    foreach (ListItem item in ddlRecMultiCheck.Items)
    //    {
    //        if (item.Selected == true)
    //        {
    //            DegreeNos += item.Value + ',';
    //        }

    //    }
    //    if (!string.IsNullOrEmpty(DegreeNos))
    //    {
    //        objConfig.DegreeNoS = DegreeNos.Substring(0, DegreeNos.Length - 1);
    //    }
    //    return DegreeNos;
    //}
    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "SEMESTERNO");
            // Fill Reciept Type
            objCommon.FillDropDownList(ddlAdmStatus, "ACD_STUDENT_ADMISSION_STATUS", "STUDENT_ADMISSION_STATUS_ID", "STUDENT_ADMISSION_STATUS_DESCRIPTION", "ISNULL(ACTIVE_STATUS,0) = 1", "STUDENT_ADMISSION_STATUS_ID");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "DEGREENO");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0 AND ISNULL(ACTIVESTATUS,0) = 1", "YEAR");
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlPaymentMode, "ACD_PAYTYPE", "PTYPE_CODE", "PTYPENAME", "PAYNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "PAYNO");
            //DataSet dsChekList = objCommon.FillDropDown("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "RECIEPT_TITLE");

            DataSet dsChekList = feeCntrl.GetReceiptTypeforFeeReport();

            if (dsChekList.Tables[0].Rows.Count > 0)
            {
                lvAdTeacher.DataSource = dsChekList;
                lvAdTeacher.DataBind();
                lvAdTeacher.Visible = true;
            }
            else
            {
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string rectype = this.GetRecType();
            if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            rectype = rectype.Substring(0, rectype.Length - 1);
            ViewState["rectype"] = rectype;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void Show_Summary_Report_Balance_fees(string reportTitle, string rptFileName)
    {
        string rectype = this.GetRecTypeReport();

        //if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        //{
        //    objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
        //    return;
        //}

        //DateTime fromDate = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd"); 
        //DateTime toDate = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd"); 
       // string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
        //int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        //int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        //int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE= 0,@P_FROM_DATE=" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + ",@P_TO_DATE=" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + ",@P_RECIEPT_CODE=" + rectype;
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";

        //To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeeTable, this.GetType(), "controlJSScript", sb.ToString(), true);
    }

    //private void ShowReportFees(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string rectype = this.GetRecType();
    //        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
    //        {
    //            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
    //            return;
    //        }
    //        rectype = rectype.Substring(0, rectype.Length - 1);
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " window.close();";
    //        //divMsg.InnerHtml += " </script>";

    //        //To open new window from Updatepanel
    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        //sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        //ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}


    private void BindListView(string rectype, string CName)
    {
        try
        {
            if (CName == "btnShow")
            {
                string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
                DataSet dssem = feeCntrl.GetFeeDetails_Fees_Report(Convert.ToInt32(ddlSemester.SelectedValue), rectype, paymode,Convert.ToInt32(ddlAcdYear.SelectedValue),Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue),Convert.ToInt32(ddlYear.SelectedValue));
                if (dssem.Tables.Count > 0)
                {
                    if (dssem.Tables[0].Rows.Count > 0)
                    {
                        lvSemesterFee.DataSource = dssem;
                        lvSemesterFee.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSemesterFee);//Set label 
                        divlvSemester.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updFeeTable, "No Data Found!", this.Page);
                    }
                }
            }
            else
            {
                DateTime FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
                DateTime ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;
                string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
                DataSet dssem = feeCntrl.GetFeeDetails_Fees_Report_UpTpSem(FromDate, ToDate, rectype, paymode);
                if (dssem.Tables.Count > 0)
                {
                    if (dssem.Tables[0].Rows.Count > 0)
                    {
                        lvSemesterFee.DataSource = dssem;
                        lvSemesterFee.DataBind();
                        divlvSemester.Visible = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string rectype = this.GetRecType();
        string cmdName = btn.CommandName.ToString();
        if (cmdName == "btnShow")
        {
            PnlSemesterwiseOS.Visible = false;
           // btnExcel.Visible = true;
            pnlSem.Visible = false;
            pnlDemand.Visible = true;
            btnOSUptoSemReport.Visible = false;
            btnFutureOSReport.Visible = false;

        }
        else
        {
            PnlSemesterwiseOS.Visible = true;
            btnExcel.Visible = false;
            pnlSem.Visible = true;
            pnlDemand.Visible = false;
            btnOSUptoSemReport.Visible = true;
            btnFutureOSReport.Visible = true;
        }


        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        if (TextBox1.Text == String.Empty)
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
            return;
        }
        if (TextBox2.Text == String.Empty)
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);


        BindListView(rectype, cmdName);
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlvSemester.Visible = false;
    }

    private void ShowSBICollectStudentReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName; ;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowCashCollect(string reportTitle, string rptFileName)
    {
        try
        {
            string rectype = this.GetRecType();
            if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            rectype = rectype.Substring(0, rectype.Length - 1);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //if (rblSelection.SelectedValue == "1")
        //{
        //    ExportinExcelforFeesDegreeBranchWise();
        //    //ShowReportinFormateBranch("xls", "rptFeesForBranchWise1.rpt");
        //}
        //else if (rblSelection.SelectedValue == "2")
        //{


        //}
        //else if (rblSelection.SelectedValue == "3")
        //{
        //    //this.ExportinExcelforFees();
        //    this.ExportinExcelforFeesWithHeads();
        //}
        //else if (rblSelection.SelectedValue == "5")
        //{
        //    this.ExportinExcelforFeesWithExcessAmount();
        //}
        //else
        //{
        this.ExportinExcelforDemandFeesWithHeads(1);

        //this.ExportinExcelforFee_Leger();
        //}

    }

    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string rectype = this.GetRecType();
            if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            rectype = rectype.Substring(0, rectype.Length - 1);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlSemester.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            //if (rdbReport.SelectedValue == "1")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype;
            ////else if (rdbReport.SelectedValue == "2")
            ////    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);
            //else if (rdbReport.SelectedValue == "2")
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportinFormateBranch(string exporttype, string rptFileName)
    {
        try
        {
            string rectype = this.GetRecType();
            if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            rectype = rectype.Substring(0, rectype.Length - 1);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlSemester.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_RECIEPT_TYPE=" + rectype;


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSemesterFee.DataSource = null;
        lvSemesterFee.DataBind();
        divlvSemester.Visible = false;
        ClearCheckbox();
        ddlSemester.Items.Clear();
       // ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        // Fill Reciept Type
        //objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE", "RCPTTYPENO", "RECIEPT_TITLE", "RCPTTYPENO>0", "RCPTTYPENO");

        if (rblSelection.SelectedValue == "1")
        {
            PnlSemesterwiseOS.Visible = true;
            PnlFeesCollection.Visible = false;
            divtodate.Visible = true;
            divfromdate.Visible = true;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            trSemester.Visible = false;

            //btnReport.Visible = false;
            btnExcel.Visible = false;
            btnOSUptoSemReport.Visible = true;
            btnFutureOSReport.Visible = true;
            pnlDemand.Visible = false;
            pnlSem.Visible = true;
        }
        else if (rblSelection.SelectedValue == "2")
        {

            trSemester.Visible = false;

            //btnReport.Visible = true;
            btnOSUptoSemReport.Visible = false;
            btnFutureOSReport.Visible = false;
        }
        else if (rblSelection.SelectedValue == "4")
        {
            //trAdmbatch.Visible = true;// Session
            divtodate.Visible = false;
            divfromdate.Visible = false;
            btnExcel.Visible = true;


            trSemester.Visible = true;
            pnlDemand.Visible = true;
            pnlSem.Visible = false;
            //btnReport.Visible = false;
            btnOSUptoSemReport.Visible = false;
            btnFutureOSReport.Visible = false;
            Response.Redirect(Request.Url.ToString());
        }
        else if (rblSelection.SelectedValue == "5")
        {

            divtodate.Visible = true;
            divfromdate.Visible = true;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            trSemester.Visible = false;

            //btnReport.Visible = false;
            btnExcel.Visible = true;
            btnOSUptoSemReport.Visible = false;
            btnFutureOSReport.Visible = false;
        }
        else
        {
            trSemester.Visible = false;

            //btnReport.Visible = false;
            btnOSUptoSemReport.Visible = false;
            btnFutureOSReport.Visible = false;
        }

    }


    private void ShowMessage(string msg)
    {
        this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert('" + msg + "'); </script>";
    }

    private void ExportinExcelforFeesCollegeWiseDDList()
    {
        string attachment = "attachment; filename=" + "forFeesCollegeWiseDDList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        //DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_COLLECTION_DD_WISE(sessionNo, rectype, semesterno);
        DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_COLLECTION_DD_WISE(0, rectype, semesterno);
        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }


    private void ExportinExcelforFeesCollegeWiseCashList()
    {
        string attachment = "attachment; filename=" + "forFeesCollegeWiseCashList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_COLLECTION_CASH_WISE(0, rectype, semesterno);

        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }
    private void ExportinExcelforFeesDegreeBranchWise()
    {
        string attachment = "attachment; filename=" + "DegreeBranchWiseExcel_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

        //int collegeid = Convert.ToInt32(ddlSchClg.SelectedValue);
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_BRANCH_DEGREE_WISE(0, rectype, 0, 0, semesterno, 0);

        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {

            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }

    private void ExportinExcelforFeesWithHeads()
    {
        string attachment = "attachment; filename=" + "FeesPaidStudentsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS(0, rectype);
        DataGrid dg = new DataGrid();


        if (dsfee.Tables.Count > 0)
        {
            dsfee.Tables[0].Columns.Remove("IDNO");
            dsfee.Tables[0].Columns.Remove("COLLEGE_ID");
            dsfee.Tables[0].Columns.Remove("DEGREENO");
            dsfee.Tables[0].Columns.Remove("BRANCHNO");
            dsfee.Tables[0].Columns.Remove("SEMESTERNAME");
            dsfee.Tables[0].Columns.Remove("SESSIONNO");
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    private void ClearCheckbox()
    {

        foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
        {
            CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
            if (chkIDNo.Checked == true)
            {
                chkIDNo.Checked = false;
            }
        }

    }

    private string GetRecTypeReport()
    {
        string RecType = string.Empty;
        //RecType = hdnRecno.Value;
        foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
        {
            CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
            if (chkIDNo.Checked == true)
            {
                RecType += chkIDNo.ToolTip + "$";
            }
            //else   // *** commented on 20/08/2019
            //    count++;

            //if (lvAdTeacher.Items.Count == count)
            //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
            //****  end  ****************
        }
        return RecType;
    }
    private string GetRecType()
    {
        string RecType = string.Empty;
        //RecType = hdnRecno.Value;
        foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
        {
            CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
            if (chkIDNo.Checked == true)
            {
                RecType += chkIDNo.ToolTip + ",";
            }
            //else   // *** commented on 20/08/2019
            //    count++;

            //if (lvAdTeacher.Items.Count == count)
            //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
            //****  end  ****************
        }
        return RecType;
    }
    private void ExportinExcelforDemandFeesWithHeads(int flag)
    {
        if (TextBox1.Text == String.Empty)
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
            return;
        }
        if (TextBox2.Text == String.Empty)
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
            return;
        }
        string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);

        string attachment = "attachment; filename=" + "DemandWiseFeesPaidStudentsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        ////int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        DataSet dsfee;
        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        if (flag == 1)
        {
            dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT_WITH_HEADS_DEMANDWISE(rectype, semesterNo, FromDate, ToDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue),paymode);
        }
        else
        {
            dsfee = feeCntrl.Get_FEE_PAYMENT_WITH_STUDENT_WISE(rectype, semesterNo, FromDate, ToDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
        }
        DataGrid dg = new DataGrid();


        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        //Response.Flush();

    }

    private void ExportinExcelforFee_Leger()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        //string[] degValue = degreeno.Split(',');
        //foreach (string s in degValue)
        //{
        //    _degreeNo = Convert.ToInt32(s);
        //    ck = objmp.AddDegree(Convert.ToInt32(_degreeNo), Convert.ToInt32(ddlColg.SelectedValue));
        //}

        string attachment = "attachment; filename=" + "StudentWiseFeesPaidist_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        ////int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);
        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        DataSet dsfeestud = feeCntrl.Get_FEE_PAYMENT_WITH_STUDENT_WISE(rectype, semesterNo, FromDate, ToDate, degreeno, branchno, year, admstatus, Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();

        if (dsfeestud.Tables.Count > 0)
        {
            dg.DataSource = dsfeestud.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        //Response.Flush();

    }



    private void ExportinExcelforCurrentStudentDetailsFeeLeger()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);    
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);     
     
        DataSet dsfeestud = feeCntrl.Get_FEE_PAYMENT_WITH_STUDENT_WISE_AND_FACULTY_DESCRIPTION(rectype, semesterNo, FromDate, ToDate, degreeno, branchno, year, admstatus, Convert.ToInt32(ddlAcdYear.SelectedValue));

        if (dsfeestud!= null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "StudentWiseFeesPaidist";
            dsfeestud.Tables[1].TableName = "FacultyWise Summary";
            dsfeestud.Tables[2].TableName = "Balance Report";

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)                                          
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= StudentWiseFeesPaidist_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }    
        }              
        else
        {
            objCommon.DisplayUserMessage(updFeeTable, "No Record Found", this.Page);
            return;
        }               

    }



    private void ExportinExcelforFee()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        //string[] degValue = degreeno.Split(',');
        //foreach (string s in degValue)
        //{
        //    _degreeNo = Convert.ToInt32(s);
        //    ck = objmp.AddDegree(Convert.ToInt32(_degreeNo), Convert.ToInt32(ddlColg.SelectedValue));
        //}

        string attachment = "attachment; filename=" + "StudentFeesDetails_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        ////int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        int yearid = Convert.ToInt32(ddlYear.SelectedValue);
        int AcdYearId = Convert.ToInt32(ddlAcdYear.SelectedValue);

        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;

        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        DataSet dsfeestud = feeCntrl.Get_FEE_PAYMENT_WITH_DCR(rectype, semesterNo, FromDate, ToDate, degreeno, branchno, yearid, AcdYearId);
        DataGrid dg = new DataGrid();


        if (dsfeestud.Tables.Count > 0)
        {
            dg.DataSource = dsfeestud.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        //Response.Flush();

    }

    private void ExportOSExcelUptoSem_FutureSem()
    {

        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }

        rectype = rectype.Substring(0, rectype.Length - 1);

        //string attachment = "";
        //if (flag == 1)
        //{
        //    attachment = "attachment; filename=" + "OS_UpTo_Sem_Excel.xls";
        //}
        //if (flag == 2)
        //{
        //    attachment = "attachment; filename=" + "OS_Future_Sem_Excel.xls";
        //}

        string attachment = "attachment; filename=" + "StudentWiseFees_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        //DataSet dsfee = feeCntrl.GetOSDataUpToSem_FutureSem(rectype, FromDate, ToDate, flag);
        DataSet dsfeestud = feeCntrl.Get_FEE_PAYMENT_WITH_STUDENT_WISE(rectype, semesterNo, FromDate, ToDate, degreeno, branchno, year, admstatus, Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();
        dg.DataSource = dsfeestud.Tables[0];
        dg.DataBind();
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.Charset = "";
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }

    private void ExportinExcelforFees()
    {
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        string attachment = "attachment; filename=" + "FeesPaidStudentsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        DataSet dsfee = feeCntrl.Get_STUDENT_FOR_FEE_PAYMENT(0, rectype);
        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {
            dsfee.Tables[0].Columns.Remove("COLLEGE_ID");
            dsfee.Tables[0].Columns.Remove("DEGREENO");
            dsfee.Tables[0].Columns.Remove("BRANCHNO");
            dsfee.Tables[0].Columns.Remove("SEMESTERNO");
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }

    private void ExportinExcelforFeesWithExcessAmount()
    {
        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        string attachment = "attachment; filename=" + "ExcessPaymentPaidStudentsList_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        // int sessionNo = sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        DateTime FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
        DateTime ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;

        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");
        DataSet dsfee = feeCntrl.GET_STUDENT_FOR_EXCESS_AMOUNT_WITH_HEADS_DEMANDWISE(rectype, FromDate, ToDate);
        DataGrid dg = new DataGrid();

        if (dsfee.Tables.Count > 0)
        {
            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }
    private void ExportOSExcelUptoSem_FutureSem(int flag)
    {
        trSemester.Visible = false;
        PnlSemesterwiseOS.Visible = true;
        btnExcel.Visible = false;
        pnlSem.Visible = true;
        pnlDemand.Visible = false;
        btnOSUptoSemReport.Visible = true;
        btnFutureOSReport.Visible = true;


        string rectype = this.GetRecType();
        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }

        rectype = rectype.Substring(0, rectype.Length - 1);

        string attachment = "";
        if (flag == 1)
        {
            attachment = "attachment; filename=" + "OS_UpTo_Sem_Excel.xls";
        }
        if (flag == 2)
        {
            attachment = "attachment; filename=" + "OS_Future_Sem_Excel_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
        }

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //string rectype = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RCPTTYPENO =" + Convert.ToInt32(ddlRecType.SelectedValue) + "");

        DateTime FromDate = (txtFromDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtFromDate.Text) : DateTime.MinValue;
        DateTime ToDate = (txtToDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtToDate.Text) : DateTime.MinValue;

        DataSet dsfee = feeCntrl.GetOSDataUpToSem_FutureSem(rectype, FromDate, ToDate, flag);
        DataGrid dg = new DataGrid();
        dg.DataSource = dsfee.Tables[0];
        dg.DataBind();
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();

    }

    protected void btnOSUptoSemReport_Click(object sender, EventArgs e)
    {
        ExportOSExcelUptoSem_FutureSem(1);
    }

    protected void btnFutureOSReport_Click(object sender, EventArgs e)
    {
        ExportOSExcelUptoSem_FutureSem(2);
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSemesterFee.DataSource = null;
        lvSemesterFee.DataBind();
        divlvSemester.Visible = false;
    }


    protected void btnstudexcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER
            {
                this.ExportinExcelforCurrentStudentDetailsFeeLeger();
            }
            else
            {
                this.ExportinExcelforFee_Leger();
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnstud_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExportinExcelforFee();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSemesterFee.DataSource = null;
        lvSemesterFee.DataBind();
        divlvSemester.Visible = false;
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", "ISNULL(B.ACTIVESTATUS,0) = 1 AND  CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlDegree.Focus();
        }
    }
    protected void btnstudLedgerReport_Click(object sender, EventArgs e)
    {
        if (Session["OrgId"].ToString() == "6")
        {
            ShowReportLedger("Student_Ledger_Report", "NewDailyFeecollectionReportRCPIPER.rpt");
        }
        else
        {
            ShowReportLedger("Student_Ledger_Report", "NewDailyFeecollectionReport.rpt");
        }
        
    }

    private void ShowReportLedger(string reportTitle, string rptFileName)
    {
        string recTyp = "";
        int FromYear = 0;
        try
        {

            DateTime fromDate = Convert.ToDateTime(TextBox1.Text);

            FromYear = Convert.ToDateTime(TextBox1.Text).Year;

            int Frommonth = Convert.ToDateTime(TextBox1.Text).Month;
            int Fromday = Convert.ToDateTime(TextBox1.Text).Day;


            if (Frommonth > 3 && Fromday <= 31)
            {
                FromYear++;
            }

            string financialDate = "31/03/" + FromYear;

            DateTime toDate = Convert.ToDateTime(TextBox2.Text);

            if (toDate > Convert.ToDateTime(financialDate))
            {
                objCommon.DisplayMessage(this.Page, "To date should be less than or equal to financial year :" + financialDate, this.Page);
                return;
            }

            foreach (ListViewDataItem items in lvAdTeacher.Items)
            {
                CheckBox chkBox = (items.FindControl("chkIDNo")) as CheckBox;
                if (chkBox.Checked)
                {
                    if (recTyp == "")
                    {
                        recTyp = chkBox.ToolTip;
                    }
                    else
                    {
                        recTyp = recTyp + "$" + chkBox.ToolTip;
                    }
                }
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROMDATE=" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd") + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_YEAR=" + Convert.ToInt32(ddlYear.SelectedValue) + ",@P_RECIEPT_TYPE=" + recTyp + ",@P_ADM_STATUS=" + Convert.ToInt32(ddlAdmStatus.SelectedValue) + ",@P_ACADEMIC_YEAR_ID=" + Convert.ToInt32(ddlAcdYear.SelectedValue);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFeeTable, this.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //protected void bntStudentArrears_Click(object sender, EventArgs e)
    //{
    //    string rectype = this.GetRecType();

    //    if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
    //    {
    //        objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
    //        return;
    //    }
    //    rectype = rectype.Substring(0, rectype.Length - 1);
    //    DateTime FromDate = DateTime.Now;
    //    FromDate = Convert.ToDateTime(TextBox1.Text);
    //    DateTime ToDate = DateTime.Now;
    //    ToDate = Convert.ToDateTime(TextBox1.Text);
    //    int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
    //    int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
    //    int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
    //    DataSet dsArrears = feeCntrl.GetStudentArrears_Excel_Report(FromDate, ToDate, Semesterno, Degreeno, Branchno, rectype, Convert.ToInt32(ddlAcdYear.SelectedValue));
    //    DataGrid dg = new DataGrid();


    //    if (dsArrears.Tables.Count > 0)
    //    {
    //        string attachment = "attachment; filename=" + "StudentArrearsReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/" + "ms-excel";
    //        StringWriter sw = new StringWriter();
    //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        dg.DataSource = dsArrears.Tables[0];
    //        dg.DataBind();

    //        dg.HeaderStyle.Font.Bold = true;
    //        dg.RenderControl(htw);
    //        Response.Write(sw.ToString());
    //        Response.End();
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
    //    }
    //}

    protected void btnStudentledgerExl_Click(object sender, EventArgs e)
    {
        ShowReport();
    }

    private void ShowReport()
    {
        try
        {
            string recTyp = this.GetRecType();
            int FromYear = 0;
            if (string.IsNullOrEmpty(recTyp))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            recTyp = recTyp.Substring(0, recTyp.Length - 1);

            //DateTime fromDate = Convert.ToDateTime(TextBox1.Text);
            //DateTime toDate = Convert.ToDateTime(TextBox2.Text);


            DateTime fromDate = Convert.ToDateTime(TextBox1.Text);

            FromYear = Convert.ToDateTime(TextBox1.Text).Year;

            int Frommonth = Convert.ToDateTime(TextBox1.Text).Month;
            int Fromday = Convert.ToDateTime(TextBox1.Text).Day;


            if (Frommonth > 3 && Fromday <= 31)
            {
                FromYear++;
            }

            string financialDate = "31/03/" + FromYear;

            DateTime toDate = Convert.ToDateTime(TextBox2.Text);

            if (toDate > Convert.ToDateTime(financialDate))
            {
                objCommon.DisplayMessage(this.Page, "To date should be less than or equal to financial year :" + financialDate, this.Page);
                return;
            }

            //foreach (ListViewDataItem items in lvAdTeacher.Items)
            //{
            //    CheckBox chkBox = (items.FindControl("chkIDNo")) as CheckBox;
            //    if (chkBox.Checked)
            //    {
            //        if (recTyp == "")
            //        {
            //            recTyp = chkBox.ToolTip;
            //        }
            //        else
            //        {
            //            recTyp = recTyp + "$" + chkBox.ToolTip;
            //        }
            //    }
            //}

            DataSet ds = feeCntrl.GetStudentLedgerReportData(fromDate, toDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), recTyp, Convert.ToInt32(ddlAdmStatus.SelectedValue), Convert.ToInt32(ddlAcdYear.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Student Ledger Report";
            }

            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Tables[0].Rows.Add("No Record Found");
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StudentLedger_Excel_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void ShowReportFormatII()
    {
        try
        {
            string recTyp = this.GetRecType();
            int FromYear=0;
            if (string.IsNullOrEmpty(recTyp))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            recTyp = recTyp.Substring(0, recTyp.Length - 1);

            DateTime fromDate = Convert.ToDateTime(TextBox1.Text);

             FromYear = Convert.ToDateTime(TextBox1.Text).Year;

            int Frommonth = Convert.ToDateTime(TextBox1.Text).Month;
            int Fromday = Convert.ToDateTime(TextBox1.Text).Day;


            if( Frommonth > 3 && Fromday <= 31)
            {
                FromYear++;
            }

            string financialDate = "31/03/" + FromYear;

            DateTime toDate = Convert.ToDateTime(TextBox2.Text);

            if (toDate > Convert.ToDateTime(financialDate))
            {
                objCommon.DisplayMessage(this.Page, "To date should be less than or equal to financial year :" + financialDate, this.Page);
                return;
            }
            //foreach (ListViewDataItem items in lvAdTeacher.Items)
            //{
            //    CheckBox chkBox = (items.FindControl("chkIDNo")) as CheckBox;
            //    if (chkBox.Checked)
            //    {
            //        if (recTyp == "")
            //        {
            //            recTyp = chkBox.ToolTip;
            //        }
            //        else
            //        {
            //            recTyp = recTyp + "$" + chkBox.ToolTip;
            //        }
            //    }
            //}

            DataSet ds = feeCntrl.GetStudentLedgerReportDataFormatII(fromDate, toDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), recTyp, Convert.ToInt32(ddlAdmStatus.SelectedValue), Convert.ToInt32(ddlAcdYear.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].TableName = "Student Ledger Report";
            }

            if (ds.Tables[0].Rows.Count < 1)
            {
                ds.Tables[0].Rows.Add("No Record Found");
            }


            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StudentLedger_Excel_FormatII_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSummaryReport_Click(object sender, EventArgs e)
    {
        Show_Summary_Report("Fee_Collection_Summary_Report", "FeeCollectionSummeryReport.rpt");
    }

    private void Show_Summary_Report(string reportTitle, string rptFileName)
    {
        string rectype = this.GetRecTypeReport();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }

        //DateTime fromDate = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd"); 
        //DateTime toDate = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd"); 
        string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
        int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd") + ",@P_DEGREENO=" + Degreeno + ",@P_SEMESTERNO=" + Semesterno + ",@P_BRANCHNO=" + Branchno + ",@P_RECIEPT_TYPE=" + rectype + ",@P_PAY_MODE=" + paymode; 
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";

        //To open new window from Updatepanel
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updFeeTable, this.GetType(), "controlJSScript", sb.ToString(), true);
    }

    private void ShowArrearReport(string reportTitle, string rptFileName)
    {
        try
        {

            string rectype = this.GetRecType();
            //DateTime fromDate = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd"); 
            //DateTime toDate = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd"); 
            int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
            int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
            int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            int AcdamicYear = ddlAcdYear.SelectedIndex > 0 ? Convert.ToInt32(ddlAcdYear.SelectedValue) : 0; 
            if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
                return;
            }
            rectype = rectype.Substring(0, rectype.Length - 1);
            ViewState["rectype"] = rectype;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_FROM_DATE=" + Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd") + ",@P_DEGREENO=" + Degreeno + ",@P_SEMESTERNO=" + Semesterno + ",@P_BRANCHNO=" + Branchno + ",@P_RECIEPT_TYPE=" + rectype + ",@P_ACADEMIC_YEAR_ID=" + AcdamicYear;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            ////To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updFeeTable, this.updFeeTable.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnStudentArrearPdf_Click(object sender, EventArgs e)
    {
        ShowArrearReport("Arrears Report PDF", "Student_Arrear_Report_PDF.rpt");
    }


    protected void btnStudentArrearsHeadwise_Click(object sender, EventArgs e) 
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DateTime FromDate = DateTime.Now;
        FromDate = Convert.ToDateTime(TextBox1.Text);
        DateTime ToDate = DateTime.Now;
        ToDate = Convert.ToDateTime(TextBox1.Text);
        int year = ddlYear.SelectedIndex > 0 ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
        int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int AdmStatus = ddlAdmStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmStatus.SelectedValue) : 0;
        DataSet dsArrears = feeCntrl.GetStudentArrears_Headwise_Report(FromDate, ToDate, Degreeno, Branchno, year, rectype, AdmStatus, Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();


        if (dsArrears.Tables.Count > 0)
        {
            string attachment = "attachment; filename=" + "StudentArrearsHeadwiseReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = dsArrears.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }
    }

    protected void btnBalanceReport_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforBalanceReport();
    }

    private void ExportinExcelforBalanceReport()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        DataSet dsfeestud = feeCntrl.Get_CURRENT_STUDENT_DETAILS_AND_BALNCE_REPORT(rectype, semesterNo, FromDate, ToDate, degreeno, branchno, year, admstatus, Convert.ToInt32(ddlAcdYear.SelectedValue));

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "Current StudentWise Fees Report";            
            dsfeestud.Tables[1].TableName = "Balance Report";

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= CurrentStudent & SchoolDetailsFeeReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updFeeTable, "No Record Found", this.Page);
            return;
        }

    }

    protected void btnDcrExcelFormatII_Click(object sender, EventArgs e)
    {
        this.ExportinExcelforDCRExcelReportFormatII();
    }

    private void ExportinExcelforDCRExcelReportFormatII()
    {

        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);

        int year = Convert.ToInt32(ddlYear.SelectedValue);

        int AcademicYear = Convert.ToInt32(ddlAcdYear.SelectedValue);

        DataSet dsfeestud = feeCntrl.Get_Fee_Details_DCR_Excel_Report_FormatII(degreeno, branchno, year, AcademicYear);

        DataGrid dg = new DataGrid();

        if (dsfeestud.Tables.Count > 0)
        {
            string attachment = "attachment; filename=" + "StudentWiseFeesPaidistFormatII_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = dsfeestud.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }       

    }

    private void ExportinExcelforBalanceReportFormatNew()
    {
       string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        int AcademicYear = Convert.ToInt32(ddlAcdYear.SelectedValue);
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        DataSet dsfeestud = feeCntrl.Get_Fee_Details_BalanceReportFormatNew(degreeno, branchno, year, AcademicYear, semesterNo, rectype, admstatus);

        DataGrid dg = new DataGrid();

        if (dsfeestud.Tables.Count > 0)
        {
            string attachment = "attachment; filename=" + "StudentWiseBalanceReportFormatNew_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = dsfeestud.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }

    }

    protected void btnOnlineDcrReport_Click(object sender, EventArgs e)
    {
        string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        DataSet ds;
        ds = feeCntrl.Get_STUDENT_FOR_ONLINE_DCR_REPORT(rectype, semesterNo, FromDate, ToDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds != null)
        {
            string attachment = "attachment; filename=" + "ONLINE_PAYMODE_DCR_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = ds.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }

    private void OnlineDRCReports()
     {
         string paymode = ddlPaymentMode.SelectedValue == "0" ? "" : ddlPaymentMode.SelectedValue;
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        DataSet ds;
        ds = feeCntrl.Get_STUDENT_FOR_ONLINE_DCR_REPORT(rectype, semesterNo, FromDate, ToDate, Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
         DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0 && ds != null )
        {
            string attachment = "attachment; filename=" + "ONLINE_PAYMODE_DCR_REPORT_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = ds.Tables[0];
            dg.DataBind();
            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
     }


    protected void btnledgerExcelFormatII_Click(object sender, EventArgs e)
    {
        this.ShowReportFormatII();
    }

    protected void btnExcelConsolidated_Click(object sender, EventArgs e)
    {
        this.ExportinCosolidatedReport();
    }

    private void ExportinCosolidatedReport()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        //int year = Convert.ToInt32(ddlYear.SelectedValue);
       // int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        DataSet dsfeestud = feeCntrl.GetdataForConsolidatedReport(rectype, semesterNo, FromDate, ToDate, degreeno, branchno);

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "Consolidated Fees Report";
           
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= CurrentStudent & SchoolDetailsFeeReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updFeeTable, "No Record Found", this.Page);
            return;
        }

    }

    private void ExportinCosolidatedReportCPU()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);

        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        //int year = Convert.ToInt32(ddlYear.SelectedValue);
        // int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        DataSet dsfeestud = feeCntrl.GetdataForConsolidatedOutstandingReport(semesterNo, FromDate, ToDate, degreeno, branchno, rectype);

        if (dsfeestud != null && dsfeestud.Tables.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "Outstanding Report RecieptWise";
            dsfeestud.Tables[1].TableName = "Consolidated Fees Report";
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Consolidated Outstanding Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updFeeTable, "No Record Found", this.Page);
            return;
        }

    }

    protected void btnOverallOutstandingReport_Click(object sender, EventArgs e)
    {
        this.ExportinCosolidatedReportCPU();
    }

    //added by nehal on 04/04/23
    protected void btnexcelPaymentModification_Click(object sender, EventArgs e)
    {
        if (ddlAcdYear.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select Academic Year.", this.Page);
            ddlAcdYear.Focus();
            return;
        }
        else
        {
            int AcdYear = Convert.ToInt32(ddlAcdYear.SelectedValue);

            DataSet ds = feeCntrl.GetPaymentModificationReportExcel(AcdYear);
            
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = "Outstanding Report RecieptWise";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= Payment Modification Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updFeeTable, "No Record Found", this.Page);
                return;

            }
        }
    }

    protected void btnTallyIntegration_Click(object sender, EventArgs e)
    {
        this.EXCEL_REPORT_TALLY_INTEGRATION();
    }

    private void EXCEL_REPORT_TALLY_INTEGRATION()
    {
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);

        int semesterNo = semesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
        int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
        int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
        DateTime FromDate = (TextBox1.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox1.Text) : DateTime.MinValue;
        DateTime ToDate = (TextBox2.Text.Trim() != string.Empty) ? Convert.ToDateTime(TextBox2.Text) : DateTime.MinValue;
        int year = Convert.ToInt32(ddlYear.SelectedValue);
        //int admstatus = Convert.ToInt32(ddlAdmStatus.SelectedValue);

        // ( rectype ) added by vipul t on date 29-02-2024 as per TNo:-52451
        DataSet dsfeestud = feeCntrl.Get_Tally_Integration_Reports_Excel(FromDate, ToDate, degreeno, branchno, Convert.ToInt32(ddlAcdYear.SelectedValue), semesterNo, year, rectype);

        if (dsfeestud != null && dsfeestud.Tables[0].Rows.Count > 0)
        {
            dsfeestud.Tables[0].TableName = "TallyIntegrationReportDetails";
            
            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in dsfeestud.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    if (dt != null && dt.Rows.Count > 0)
                        wb.Worksheets.Add(dt);
                }
                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= TallyIntegrationReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updFeeTable, "No Record Found", this.Page);
            return;
        }             
    }

    // added by Nehal on 27062023
    protected void btnCancelrecieptsummary_Click(object sender, EventArgs e)
    {
        Show_Summary_Report("Cancelled_Receipt_Summary_Report", "Canceled_receipt_summary_report.rpt");
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSemesterFee.DataSource = null;
        lvSemesterFee.DataBind();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvSemesterFee.DataSource = null;
        lvSemesterFee.DataBind();
    }

    private void Payment_Modification_Excel()
    {
        if (ddlAcdYear.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select Academic Year.", this.Page);
            ddlAcdYear.Focus();
            return;
        }
        else
        {
            int AcdYear = Convert.ToInt32(ddlAcdYear.SelectedValue);
            DataSet ds = feeCntrl.GetPaymentModificationReportExcel(AcdYear);
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = "Outstanding Report RecieptWise";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename= Payment Modification Report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updFeeTable, "No Record Found", this.Page);
                return;
            }
        }
    }


    private void Student_Arrears_Report()
    {
        if (ddlAcdYear.SelectedValue == "0")
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select Academic Year.", this.Page);
            ddlAcdYear.Focus();
            return;
        }
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DateTime FromDate = DateTime.Now;
        FromDate = Convert.ToDateTime(TextBox1.Text);
        DateTime ToDate = DateTime.Now;
        ToDate = Convert.ToDateTime(TextBox1.Text);
        int Semesterno = ddlSemester.SelectedIndex > 0 ? Convert.ToInt32(ddlSemester.SelectedValue) : 0;
        int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        DataSet dsArrears = feeCntrl.GetStudentArrears_Excel_Report(FromDate, ToDate, Semesterno, Degreeno, Branchno, rectype, Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();


        if (dsArrears.Tables.Count > 0)
        {
            string attachment = "attachment; filename=" + "StudentArrearsReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = dsArrears.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }
    }

    private void Student_Arrears_Headwise()
    {
     
        string rectype = this.GetRecType();

        if (string.IsNullOrEmpty(rectype))//GetDegreeNew()
        {
            objCommon.DisplayUserMessage(updFeeTable, "Please Select At least One Receipt Type !", this.Page);
            return;
        }
        rectype = rectype.Substring(0, rectype.Length - 1);
        DateTime FromDate = DateTime.Now;
        FromDate = Convert.ToDateTime(TextBox1.Text);
        DateTime ToDate = DateTime.Now;
        ToDate = Convert.ToDateTime(TextBox1.Text);
        int year = ddlYear.SelectedIndex > 0 ? Convert.ToInt32(ddlYear.SelectedValue) : 0;
        int Branchno = ddlBranch.SelectedIndex > 0 ? Convert.ToInt32(ddlBranch.SelectedValue) : 0;
        int Degreeno = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
        int AdmStatus = ddlAdmStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmStatus.SelectedValue) : 0;
        DataSet dsArrears = feeCntrl.GetStudentArrears_Headwise_Report(FromDate, ToDate, Degreeno, Branchno, year, rectype, AdmStatus, Convert.ToInt32(ddlAcdYear.SelectedValue));
        DataGrid dg = new DataGrid();

        if (dsArrears.Tables.Count > 0)
        {
            string attachment = "attachment; filename=" + "StudentArrearsHeadwiseReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.DataSource = dsArrears.Tables[0];
            dg.DataBind();

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No data found.", this.Page);
        }
    }

    //Added by Sakshi M. 29112023
    protected void btnReport_Click1(object sender, EventArgs e)
    {
        if (ddlReport.SelectedValue == "1")
        {
            this.ExportinExcelforDemandFeesWithHeads(1);
        }
        else if (ddlReport.SelectedValue == "2")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER
            {
                this.ExportinExcelforCurrentStudentDetailsFeeLeger();
            }
            else
            {
                this.ExportinExcelforFee_Leger();
            }
        }
        else if (ddlReport.SelectedValue == "3")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
           this.OnlineDRCReports();
        }
        else if (ddlReport.SelectedValue == "4")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.ExportinExcelforFee();
        }
        else if (ddlReport.SelectedValue == "5")
        {
            this.Payment_Modification_Excel();
        }
        else if (ddlReport.SelectedValue == "6")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            if (Session["OrgId"].ToString() == "6")
            {
                ShowReportLedger("Student_Ledger_Report", "NewDailyFeecollectionReportRCPIPER.rpt");
            }
            else
            {
                ShowReportLedger("Student_Ledger_Report", "NewDailyFeecollectionReport.rpt");
            }
        }
        else if (ddlReport.SelectedValue == "7")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            ShowReport();
        }
        else if (ddlReport.SelectedValue == "8")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.ShowReportFormatII();
        }
        else if (ddlReport.SelectedValue == "9")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.Student_Arrears_Report();
        }
        else if (ddlReport.SelectedValue == "10")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            ShowArrearReport("Arrears Report PDF", "Student_Arrear_Report_PDF.rpt");
        }
        else if (ddlReport.SelectedValue == "11")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.Student_Arrears_Headwise();
        }
        else if (ddlReport.SelectedValue == "12")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.ExportinExcelforBalanceReport();
        }
        else if (ddlReport.SelectedValue == "13")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            Show_Summary_Report("Fee_Collection_Summary_Report", "FeeCollectionSummeryReport.rpt");
        }
        else if (ddlReport.SelectedValue == "14")
        {
           
            this.ExportinExcelforDCRExcelReportFormatII();
        }
        else if (ddlReport.SelectedValue == "15")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.ExportinCosolidatedReport();
        }
        else if (ddlReport.SelectedValue == "16")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.ExportinCosolidatedReportCPU();
        }
        else if (ddlReport.SelectedValue == "17")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            this.EXCEL_REPORT_TALLY_INTEGRATION();
        }
        else if (ddlReport.SelectedValue == "18")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            Show_Summary_Report("Cancelled_Receipt_Summary_Report", "Canceled_receipt_summary_report.rpt");
        }
        else if (ddlReport.SelectedValue == "20")
        {
            if (TextBox1.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select From Date !", this.Page);
                return;
            }
            if (TextBox2.Text == String.Empty)
            {
                objCommon.DisplayUserMessage(updFeeTable, "Please Select To Date !", this.Page);
                return;
            }
            Show_Summary_Report_Balance_fees("Fees_Payment_Summery_Report", "rptFeespaymentsummeryreport.rpt");
        }
        else if (ddlReport.SelectedValue == "21")
        {
            this.ExportinExcelforDCRExcelReportFormatII();
        }
    }


    //Added by Sakshi M. 29112023
    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        string IsPdf = objCommon.LookUp("ACD_FEESREPORTS_MASTER", "IS_PDF", "RID =" + Convert.ToInt32(ddlReport.SelectedValue) + "");

        if (IsPdf == "1")
        {
            btnReport.Text = "Report (PDF)";
        }
        else
        {
            btnReport.Text = "Report (Excel)";
        }

        ddlAcdYear.SelectedValue = "0";
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlBranch.SelectedValue = "0";
        ddlDegree.SelectedValue = "0";
        ddlPaymentMode.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ddlYear.SelectedValue = "0";
        ddlAdmStatus.SelectedValue = "0";
        divlvSemester.Visible = false;
        acdyear.Visible = false;
        fromDSpan.Visible = false;
        toDSpan.Visible = false;
        degreed.Visible = false;
        branchd.Visible = false;
        reciptd.Visible = false;
        divPaymentMode.Visible = false;
        yeard.Visible = false;
        Statusd.Visible = false;
        semesterd.Visible = false;
        TextBox1.Text = string.Empty;
        TextBox2.Text = string.Empty;
        btnCancel.Visible = true;
        btnReport.Visible = true;
        btnShow.Visible = true;

        if (ddlReport.SelectedValue == "1")
        {
            //acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            divPaymentMode.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "2")
        {
            acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;

        }
        else if (ddlReport.SelectedValue == "3")
        {
            //acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
           // divPaymentMode.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "4")
        {
            acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            semesterd.Visible = true;
            yeard.Visible = true;
            btnShow.Visible = false;

        }
        else if (ddlReport.SelectedValue == "5")
        {
            acdyear.Visible = true;
            btnShow.Visible = false;
          
        }
        else if (ddlReport.SelectedValue == "6")
        {
            acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "7")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            acdyear.Visible = true;
            btnShow.Visible = false;

        }
        else if (ddlReport.SelectedValue == "8")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            acdyear.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "9")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            acdyear.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "10")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            acdyear.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "11")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            acdyear.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "12")
        {
            acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            yeard.Visible = true;
            Statusd.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "13")
        {
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            semesterd.Visible = true;
            reciptd.Visible = true;
            divPaymentMode.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "14")
        {
            degreed.Visible = true;
            branchd.Visible = true;
            yeard.Visible = true;
            acdyear.Visible = true;
            btnShow.Visible = false;

        }
        else if (ddlReport.SelectedValue == "15")
        {
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            semesterd.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "16")
        {

            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            semesterd.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "17")
        {
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            semesterd.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            yeard.Visible = true;
            acdyear.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "18")
        {
        
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            divPaymentMode.Visible = true;
            semesterd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "19")
        {
            acdyear.Visible = true;
            fromDSpan.Visible = true;
            toDSpan.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            divPaymentMode.Visible = true;
            yeard.Visible = true;
            semesterd.Visible = true;
            btnReport.Visible = false;
            btnShow.Visible = true;
           
        }
        else if (ddlReport.SelectedValue == "20")
        {
            
            fromDSpan.Visible = true;
            toDSpan.Visible = true;     
            reciptd.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "21")
        {
            acdyear.Visible = true;
            degreed.Visible = true;
            branchd.Visible = true;
            reciptd.Visible = true;
            semesterd.Visible = true;
            yeard.Visible = true;
            btnShow.Visible = false;
        }
        else if (ddlReport.SelectedValue == "0")
        {
            btnReport.Visible = false;
            btnShow.Visible = false;
            btnCancel.Visible = false;
        }
        ClearCheckbox();
    }
}


