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
using System.Data;
using System.IO;

public partial class ACADEMIC_MASTERS_SeatingArrangementReportFixed : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();
    Seating objSeating = new Seating();
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
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME+'('+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED WITH (NOLOCK) INNER JOIN ACD_EXAM_TT_SLOT AEIS WITH (NOLOCK) ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "", "SLOTNO");
    }


    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updBarcode, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";
            ddlExamName.SelectedValue = "0";    //added on 23-03-2020 by Vaishali

        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED WITH (NOLOCK) INNER JOIN ACD_EXAM_TT_SLOT AEIS WITH (NOLOCK) ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    protected void btnExamTimeTable_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReport("ExamTimeTable", "rptExamTimeTable_BarCode_Fixed.rpt");
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
            // added on 11-09-2019 by Vaishali to check for record for MID SEM & END SEM on existing date
            //string examname = objCommon.LookUp(" ACD_EXAM_DATE A INNER JOIN ACD_EXAM_NAME B ON A.EXAM_TT_TYPE = B.EXAMNO INNER JOIN ACD_SEATING_ARRANGEMENT C ON A.SESSIONNO = C.SESSIONNO", " DISTINCT FLDNAME", "A.EXAMDATE= '" + Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") + "' AND C.SLOTNO = " + Convert.ToInt32(ddlslot.SelectedValue) + " AND C.SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) +" AND IS_MIDS_ENDS = "+ddlExamName.SelectedValue);
            //if (!string.IsNullOrEmpty(examname))
            //{

             //added on 13-03-2020 by Vaishali
            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) INNER JOIN ACD_EXAM_DATE ED WITH (NOLOCK) ON  A.SESSIONNO=ED.SESSIONNO AND A.SCHEMENO =ED.SCHEMENO AND A.SLOTNO =ED.SLOTNO INNER JOIN ACD_ROOM RM WITH (NOLOCK) ON RM.ROOMNO = A.ROOMNO INNER JOIN ACD_EXAM_TT_SLOT T WITH (NOLOCK) ON T.SLOTNO=A.SLOTNO", "COUNT(1)", "(ED.EXAMDATE = " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " OR " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " = '') AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue));
            if (count != 0)
            {
                string dt = txtExamDate.Text != string.Empty ? Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") : string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END="+ddlExamName.SelectedValue;// COMMENTED ON 13-03-2020 BY VAISHALI
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (txtExamDate.Text != string.Empty ? dt : string.Empty) + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue;
                
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updBarcode, "No Record Found !!!!", this.Page);
                return;
            }
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
            //if (txtExamDate.Text != string.Empty)
            //{
            //rfvRoom.Enabled = false;
            ShowReport("QuestionPaperReport", "rptQPPrintingReportView_Fixed.rpt");
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Please Select date!!", this.Page);
            //}
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
            //added on 13-03-2020 by Vaishali
            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) INNER JOIN ACD_EXAM_DATE ED WITH (NOLOCK) ON  A.SESSIONNO=ED.SESSIONNO AND A.SCHEMENO =ED.SCHEMENO AND A.SLOTNO =ED.SLOTNO INNER JOIN ACD_ROOM RM WITH (NOLOCK) ON RM.ROOMNO = A.ROOMNO INNER JOIN ACD_EXAM_TT_SLOT T ON T.SLOTNO=A.SLOTNO", "COUNT(1)", "(ED.EXAMDATE = " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " OR " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " = '') AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue));
            if (count != 0)
            {
                string dt = txtExamDate.Text != string.Empty ? Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") : string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END="+ddlExamName.SelectedValue; //modified on 12-03-2020 by Vaishali
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (txtExamDate.Text != string.Empty ? dt : string.Empty) + ",@SLOTNO=" + ddlslot.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue; //modified on 12-03-2020 by Vaishali

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updBarcode,"Record Not Found !!!!", this.Page);
                return;
            }

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
            //if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            //{

            //    objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            //}

            //else if (txtExamDate.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            //}
            //else
            //{
            //rfvRoom.Enabled = false;
            ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptseatingplanroomwise_forexamdate_Fixed.rpt");
            //}
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
        //rfvRoom.Enabled = false;
        ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetAccordRoom_forexam1_Fixed.rpt");
    }

    private void ShowReportAttedence(string reportTitle, string rptFileName)
    {
        try
        {
            //added on 13-03-2020 by Vaishali
            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) INNER JOIN ACD_EXAM_DATE ED WITH (NOLOCK) ON  A.SESSIONNO=ED.SESSIONNO AND A.SCHEMENO =ED.SCHEMENO AND A.SLOTNO =ED.SLOTNO INNER JOIN ACD_ROOM RM WITH (NOLOCK) ON RM.ROOMNO = A.ROOMNO INNER JOIN ACD_EXAM_TT_SLOT T WITH (NOLOCK) ON T.SLOTNO=A.SLOTNO", "COUNT(1)", "(ED.EXAMDATE = " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " OR " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " = '') AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue));
            if (count != 0)
            {
                string dt = txtExamDate.Text != string.Empty ? Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") : string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue; // modified on 12-03-2020 by Vaishali
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (txtExamDate.Text != string.Empty ? dt : string.Empty) + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue; // modified on 12-03-2020 by Vaishali

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updBarcode, "Record Not Found !!!!", this.Page);
                return;
            }
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
        //ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam_Fixed.rpt");
        //rfvRoom.Enabled = true;
        //rfvRoom.Visible = true;
        if (ddlRoom.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updBarcode,"Please Select Room !!!!", this.Page);
            return;
        }
        else
            ShowReportMaster("Master_Seating_Plan", "rptNewFixedDoorSeatPlan.rpt");
    }

    private void ShowReportMaster(string reportTitle, string rptFileName)
    {
        try
        {
            //added on 13-03-2020 by Vaishali
            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) INNER JOIN ACD_EXAM_DATE ED WITH (NOLOCK) ON  A.SESSIONNO=ED.SESSIONNO AND A.SCHEMENO =ED.SCHEMENO AND A.SLOTNO =ED.SLOTNO INNER JOIN ACD_ROOM RM WITH (NOLOCK) ON RM.ROOMNO = A.ROOMNO INNER JOIN ACD_EXAM_TT_SLOT T WITH (NOLOCK) ON T.SLOTNO=A.SLOTNO", "COUNT(1)", "(ED.EXAMDATE = " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " OR " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " = '') AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue));
            if (count != 0)
            {
                string dt = txtExamDate.Text != string.Empty ? Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") : string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue + ",@ExamName=" + ddlExamName.SelectedItem.Text; //COMMENTED ON 13-03-2020 BY VAISHALI
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (txtExamDate.Text != string.Empty ? dt : string.Empty) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue +",@P_ROOMNO="+ddlRoom.SelectedValue;
               
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updBarcode, "Record Not Found !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnQPReportExcel_Click(object sender, EventArgs e)
    {
        if (txtExamDate.Text != string.Empty)
        {
            ShowReportExcel("QuestionPaperReport", "rptQPPintingReportView.rpt");
        }
        else
        {
            objCommon.DisplayMessage("Please Select date!!", this.Page);
        }
    }
    private void ShowReportExcel(string reportTitle, string rptFileName)
    {
        ExportinExcelQuestionPaperReport();

    }
    private void ExportinExcelQuestionPaperReport()
    {
        //string attachment = "attachment; filename=" + "QuestionPaperRepot.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/" + "ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);

        //string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        //int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        //DataSet dsfee;
        //dsfee = objSC.Get_QUESTION_PAPER_REPORT(EXAMDATE, slotno);

        //DataGrid dg = new DataGrid();
        ////DataTable dt = null;
        ////dt = ds.

        //if (dsfee.Tables.Count > 0)
        //{
        //    //dsfee.Tables[0].Columns.Remove("tot_students");
        //    dsfee.Tables[0].Columns.Remove("COURSENO");
        //    dsfee.Tables[0].Columns.Remove("DEGREENO");
        //    dsfee.Tables[0].Columns.Remove("BRANCHNO");
        //    dsfee.Tables[0].Columns.Remove("TIMEFROM");
        //    dsfee.Tables[0].Columns.Remove("TIMETO");
        //    dsfee.Tables[0].Columns.Remove("SESSIONNO");
        //    dsfee.Tables[0].Columns.Remove("SESSION_NAME");
        //    dsfee.Tables[0].Columns.Remove("SLOTNAME");
        //    dsfee.Tables[0].Columns.Remove("SHORTNAME");




        //    dg.DataSource = dsfee.Tables[0];
        //    dg.DataBind();
        //}
        //dg.HeaderStyle.Font.Bold = true;
        //dg.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();


    }
    protected void btnenvelop_Click(object sender, EventArgs e)
    {
        ShowReportEnvelop("ShowReportEnvelop", "rptshowreportenvelop_Fixed.rpt");
    }
    private void ShowReportEnvelop(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_COLLEGE_ID=" + ddlCollege.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue;

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
    protected void btnprctlreprt_Click(object sender, EventArgs e)
    {
        ShowReportPractical("Practical_Attendance_Report", "rptpracticalattreport.rpt");
    }
    private void ShowReportPractical(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue;

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

    protected void btnblankmarksheet_Click(object sender, EventArgs e)
    {
        ShowReportPractical("Practical_Attendance_Report", "rptpracticalBlankMarksheet.rpt");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReportForExam("Seating_Arrangement_SuperVisor_Desc", "rptSeatingPlanSupervisorDescription_Fixed.rpt");
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

    protected void btnConsolidatedAbsStud_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReportForExam("Absent_Student_Consolidated_Report", "rpConsolidate_Report_For_Absent_Stud_Fixed.rpt");
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
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (txtExamDate.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                ShowReportForExam("Absent_Student_Consolidated_Report", "rpConsolidate_Report_For_Absent_Stud_With_UFM_Fixed.rpt");
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
    protected void btnblockarrangementExcel_Click(object sender, EventArgs e)
    {

        if (txtExamDate.Text != string.Empty)
        {
            ShowBlockArrangementReportExcel("Seating_Arrangement_Roomwise_Exam", "rptseatingplanroomwise_forexamdate_Fixed.rpt");
        }
        else
        {
            objCommon.DisplayMessage("Please Select date!!", this.Page);
        }

    }

    private void ShowBlockArrangementReportExcel(string reportTitle, string rptFileName)
    {
        ExportinExcelBlockArrangementReport();

    }
    private void ExportinExcelBlockArrangementReport()
    {
        //string attachment = "attachment; filename=" + "BlockArrangementRepot.xls";
        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/" + "ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);

        //string EXAM_DATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        //int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        //DataSet dsfee;
        //dsfee = objSC.Get_BLOCK_ARRANGEMENT_REPORT(EXAM_DATE, slotno);

        //DataGrid dg = new DataGrid();
        ////DataTable dt = null;
        ////dt = ds.

        //if (dsfee.Tables.Count > 0)
        //{


        //    dsfee.Tables[0].Columns.Remove("TIMEFROM");
        //    dsfee.Tables[0].Columns.Remove("TIMETO");
        //    dsfee.Tables[0].Columns.Remove("COLLEGE_ID");
        //    dsfee.Tables[0].Columns.Remove("ROOMNO");
        //    dsfee.Tables[0].Columns.Remove("SLOTNAME");
        //    dsfee.Tables[0].Columns.Remove("EXAMDATE");
        //    dsfee.Tables[0].Columns.Remove("SESSIONNAME");


        //    dg.DataSource = dsfee.Tables[0];
        //    dg.DataBind();
        //}
        //dg.HeaderStyle.Font.Bold = true;
        //dg.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();


    }
    protected void btnDualSeatingPlan_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamAttendanceSheetDualPlan_Fixed.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnDualSeatingPlan_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnStudAttFormat2_Click(object sender, EventArgs e)
    {
        ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetFormat2_Fixed.rpt");
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlRoom, "ACD_SEATING_PLAN SP WITH (NOLOCK) INNER JOIN ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) ON(SP.ROOMNO=A.ROOMNO ) INNER JOIN ACD_ROOM R WITH (NOLOCK) ON(SP.ROOMNO=R.ROOMNO)", "DISTINCT SP.ROOMNO", "R.ROOMNAME", "A.SESSIONNO = " + ddlSession.SelectedValue + " AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue, "SP.ROOMNO");
    }

    protected void btnQPF2_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("QuestionPaperReport", "rptQPCountFormat2.rpt"); 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters.btnQPF2_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added on 04-04-2020 by Vaishali
    private void ShowReportDisplaySitting(string reportTitle, string rptFileName)
    {
        try
        {

            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT_FIXED A WITH (NOLOCK) INNER JOIN ACD_EXAM_DATE ED WITH (NOLOCK) ON  A.SESSIONNO=ED.SESSIONNO AND A.SCHEMENO =ED.SCHEMENO AND A.SLOTNO =ED.SLOTNO INNER JOIN ACD_ROOM RM WITH (NOLOCK) ON RM.ROOMNO = A.ROOMNO INNER JOIN ACD_EXAM_TT_SLOT T ON T.SLOTNO=A.SLOTNO", "COUNT(1)", "(ED.EXAMDATE = " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " OR " + (txtExamDate.Text != string.Empty ? txtExamDate.Text : "''") + " = '') AND A.SLOTNO = " + ddlslot.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue + " AND A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue));
            if (count != 0)
            {
                string dt = txtExamDate.Text != string.Empty ? Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") : string.Empty;

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (txtExamDate.Text != string.Empty ? dt : string.Empty) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_IS_MID_END=" + ddlExamName.SelectedValue;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(this.updBarcode, "Record Not Found !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added on 04-04-2020 by Vaishali
    protected void btnDisplaySitting_Click(object sender, EventArgs e)
    {
        ShowReportDisplaySitting("DisplaySittingReport", "rptDisplaySittingFixed.rpt");
    }
}