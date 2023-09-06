using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;

public partial class ACADEMIC_MASTERS_ExamBarCode : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    SeatingController objSC = new SeatingController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            PopulateDropDownList();
        }
    }
    private void PopulateDropDownList()
    {

        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME AS COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }



    protected void btnExamTimeTable_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReport("ExamTimeTable", "rptExamTimeTable_BarCode.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + 
                ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + 
                ",@P_SLOTNO=" + ddlslot.SelectedValue + 
                ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;
            if (reportTitle=="QuestionPaperReportII")
            {
                url += ",@P_IS_MID_END=" + ddlslot.SelectedValue + ",@P_ROOMNO=" + ddlslot.SelectedValue;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnQPReportView_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtExamDate.Text != string.Empty)
            {
                ShowReport("QuestionPaperReport", "rptQPPrintingReportView.rpt");
            }
            else
            {
                objCommon.DisplayMessage("Please Select date!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnQPReportView_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReportForExam(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_MID_END=" +ddlExamName.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtblockarrrangement_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptseatingplanroomwise_forexamdate.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }



    }
    protected void txtstudeattendence_Click(object sender, EventArgs e)
    {


        ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetAccordRoom_forexam1.rpt");

    }

    private void ShowReportAttedence(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtmasterseatplan_Click(object sender, EventArgs e)
    {
        //ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam.rpt");
        DataSet dsData = objSC.GetDataForSeatingPlan((Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlRoom.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
        if (dsData != null && dsData.Tables[0].Rows.Count > 0)
        {
                ShowReportMaster("Master_Seating_Plan", "rptNewDoorSeatPlan.rpt");
        }
        else
        {
            objCommon.DisplayUserMessage(updBarcode, "No data found for given selection!", this.Page);
            return;
        }
    }

    private void ShowReportMaster(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd hh:mm:ss");
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
                ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) +
                ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) +
                ",@P_ROOMNO=" + Convert.ToInt32(ddlRoom.SelectedValue)+
                ",@P_IS_MID_END=" +Convert.ToInt32(ddlExamName.SelectedValue);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnenvelop_Click(object sender, EventArgs e)
    {
        ShowReportEnvelop("ShowReportEnvelop", "rptshowreportenvelop.rpt");
    }
    private void ShowReportEnvelop(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSeatPlan_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamAttendanceSheetDualPlan.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnDualSeatingPlan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updBarcode, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";

        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }
    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        objCommon.FillDropDownList(ddlRoom, "ACD_SEATING_PLAN SP INNER JOIN ACD_SEATING_ARRANGEMENT A  ON(SP.ROOMNO=A.ROOMNO AND SP.COLLEGE_ID=A.COLLEGE_ID)INNER JOIN ACD_ROOM R ON(SP.ROOMNO=R.ROOMNO AND SP.COLLEGE_ID=R.COLLEGE_ID)", "DISTINCT SP.ROOMNO", "R.ROOMNAME", "SP.COLLEGE_ID='" + ddlCollege.SelectedValue + "' AND A.EXAMDATE='" + EXDATE + "' AND A.SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue), "SP.ROOMNO");
    }
    protected void btnQPF_two_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("QuestionPaperReportII", "rptQPFormat2_Roomwise.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters.btnQPF2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnDisplaySitting_Click(object sender, EventArgs e)
    {
        ShowReportDisplaySitting("DisplaySittingReport", "rptDisplaySitting.rpt");
    }

    public void ShowReportDisplaySitting(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                ",@P_SLOTNO=" + ddlslot.SelectedValue +
                ",@P_IS_MID_END=" + ddlExamName.SelectedValue;
               
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnDualSeatingPlan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}