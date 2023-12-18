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
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.IO;

public partial class examseatingreports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingArrangementController objSC = new SeatingArrangementController();
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

    #region Page load
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
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            PopulateDropDownList();
            //  getcollegeid();
           

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }
    }

    private void PopulateDropDownList()
    {
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
            ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }//Added by lalit 16-01-2023
        //   objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

    }

    #endregion
    public void getcollegeid()
    {

        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ViewState["college_id"]));

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0  AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

        }

    }

    #region Examdate
    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        //   string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");

        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
        string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");

        //  string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");

        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'");



        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updBarcode, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";

        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO ASC");
        }
    }

    #endregion

    #region commented
    //protected void btnExamTimeTable_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
    //        {

    //            objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

    //        }

    //        else if (txtExamDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

    //        }
    //        else
    //        {
    //            ShowReport("ExamTimeTable", "rptExamTimeTable_BarCode.rpt");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    #endregion

    #region Reset

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue+",@P_PREV_STATUS="+ddlExamType.SelectedValue;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",CONVERT(VARCHAR(50),@P_DATE,103)=" + ddlExamdate.SelectedItem.Text.Trim() + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;


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
    #endregion

    #region commented
    //protected void btnQPReportView_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtExamDate.Text != string.Empty)
    //        {
    //            ShowReport("QuestionPaperReport", "rptQPPrintingReportView.rpt");
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Please Select date!!", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnQPReportView_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    #endregion

    #region
    private void ShowReportForExam(string reportTitle, string rptFileName)
    {
        try
        {
            getcollegeid();
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {
                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd")
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + prevstatus;                            //ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("dd/MM/yyyy") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 7)
            {
                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd")
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + prevstatus;                            //ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("dd/MM/yyyy") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20)
            {

                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd")
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + prevstatus;                            //ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("dd/MM/yyyy") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            {
                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd")
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + prevstatus;                            //ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("dd/MM/yyyy") + ",@SLOTNO=" + ddlslot.SelectedValue + ",@PREV_STATUS=" + ddlExamType.SelectedValue;     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@EXAM_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);     //",@PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


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
    #endregion

    #region
    protected void txtblockarrrangement_Click(object sender, EventArgs e)  //rptSeatingPlanRoomwise_ForExamDtxtmasterseatplan_Clickate
    {
        try
        {
            //if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "CONVERT(NVARCHAR(50),EXAMDATE,103)='" + (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd") + "'") == "0")  //txtExamDate

            if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "CONVERT(NVARCHAR(50),EXAMDATE,103)='" + ddlExamdate.SelectedItem.Text.Trim() + "'") == "0")
            {

                objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

            }

            else if (ddlExamdate.Text == string.Empty)   //txtExamDate
            {
                objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

            }
            else
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptSeatingPlanRoomwise_ForExamDate_RCPIT.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptSeatingPlanRoomwise_ForExamDate_RCPIPER.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 20)
                {
                    ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptSeatingPlanRoomwise_ForExamDate_PJLCE.rpt");
                }
                else
                {
                    ShowReportForExam("Seating_Arrangement_Roomwise_Exam", "rptseatingplanroomwise_forexamdate.rpt");
                }
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
        if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamStudAttdanceSheetforexam_RCPIPER.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamStudAttdanceSheetforexam_RCPIT_New.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 20)
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetAccordRoom_forexam1_PJLCE.rpt");
        }
        else
        {
            ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetAccordRoom_forexam1.rpt");
        }
       
    }
    #endregion

    #region
    private void ShowReportAttedence(string reportTitle, string rptFileName)
    {
        try
        {
            getcollegeid();
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;                           //",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbtxtblockarrrangement_Clickars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 7)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;                           //",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbtxtblockarrrangement_Clickars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20)
            {
                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;                           //",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"] + ",@P_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbtxtblockarrrangement_Clickars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            {

                // int prevstatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;                           //",@EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") +
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbtxtblockarrrangement_Clickars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

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
        if (rdbSingle.Checked)
        {
            if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam_RCPIPER.rpt");
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam_RCPIT_new.rpt");
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20)
            {

                ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam_PJLCE.rpt");
            }
            else
            {
                ShowReportMaster("Master_Seating_Plan", "rptMasterSeatingPlan_ForExam.rpt");
            }
        }
        else
            if (rdbDouble.Checked)
            {
                ViewState["DoubleSeating"] = "2";
                if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ShowReportMasterDouble("Master_Seating_Plan", "rptMasterSeatingPlan_ForExamDouble_RCPIPER.rpt");

                }
                else if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    ShowReportMasterDouble("Master_Seating_Plan", "rptMasterSeatingPlan_ForExamDouble_RCPIT_New.rpt");
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 20)
                {

                    ShowReportMasterDouble("Master_Seating_Plan", "rptMasterSeatingPlan_ForExamDouble_PJLCE.rpt");
                }
                else
                {
                    ShowReportMasterDouble("Master_Seating_Plan", "rptMasterSeatingPlan_ForExamDouble.rpt");
                }
            }
            else 
            {
                ViewState["TripleSeating"] = '3';
              
            }


    }

    private void ShowReportMaster(string reportTitle, string rptFileName)
    {
        try
        {
            //getcollegeid();
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                //int prevStatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 7)
            {

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

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

   


    private void ShowReportExcel(string reportTitle, string rptFileName)
    {
        ExportinExcelQuestionPaperReport();

    }
    #endregion

    #region building chart
    private void ExportinExcelQuestionPaperReport()
    {
        string attachment = "attachment; filename=" + "QuestionPaperRepot.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");

        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
        int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        DataSet dsfee;
        dsfee = objSC.Get_QUESTION_PAPER_REPORT(EXAMDATE, slotno, Convert.ToInt32(ddlExamType.SelectedValue));

        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {
            //dsfee.Tables[0].Columns.Remove("tot_students");
            dsfee.Tables[0].Columns.Remove("COURSENO");
            dsfee.Tables[0].Columns.Remove("DEGREENO");
            dsfee.Tables[0].Columns.Remove("BRANCHNO");
            dsfee.Tables[0].Columns.Remove("TIMEFROM");
            dsfee.Tables[0].Columns.Remove("TIMETO");
            dsfee.Tables[0].Columns.Remove("SESSIONNO");
            dsfee.Tables[0].Columns.Remove("SESSION_NAME");
            dsfee.Tables[0].Columns.Remove("SLOTNAME");
            dsfee.Tables[0].Columns.Remove("SHORTNAME");




            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }

 

    #endregion

    #region Showreport

    private void ShowReportEnvelop(string reportTitle, string rptFileName)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

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

    //protected void btnprctlreprt_Click(object sender, EventArgs e)
    //{
    //    ShowReportPractical("Practical_Attendance_Report", "rptpracticalattreport.rpt"); //Practical report for Regular
    //}

    private void ShowReportPractical(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAM_DATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAM_DATE=" + (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue);


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

    #endregion

    #region not used
    //protected void btnblankmarksheet_Click(object sender, EventArgs e)
    //{
    //    ShowReportPractical("Practical_Attendance_Report", "rptpracticalBlankMarksheet.rpt");   // Blank MarksSheet for regular
    //}

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
    //        {

    //            objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

    //        }

    //        else if (txtExamDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

    //        }
    //        else
    //        {
    //            ShowReportForExam("Seating_Arrangement_SuperVisor_Desc", "rptSeatingPlanSupervisorDescription.rpt");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }


    //}

    //protected void btnConsolidatedAbsStud_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
    //        {

    //            objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

    //        }

    //        else if (txtExamDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

    //        }
    //        else
    //        {
    //            ShowReportForExam("Absent_Student_Consolidated_Report", "rpConsolidate_Report_For_Absent_Stud.rpt");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }


    //}


    //protected void btnreport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (objCommon.LookUp("ACD_EXAM_DATE", "count(1)", "EXAMDATE='" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + "'") == "0")
    //        {

    //            objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);

    //        }

    //        else if (txtExamDate.Text == string.Empty)
    //        {
    //            objCommon.DisplayMessage(this.updBarcode, "Please Select date!!", this.Page);

    //        }
    //        else
    //        {
    //            ShowReportForExam("Absent_Student_Consolidated_Report", "rpConsolidate_Report_For_Absent_Stud_With_UFM.rpt");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "Academic_Masters_ExamBarCode.btnExamTimeTable_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }


    //}

    #endregion

    #region Block Arrangment
    protected void btnblockarrangementExcel_Click(object sender, EventArgs e)
    {

        if (ddlExamdate.Text != string.Empty)   //txtExamDate
        {
            ShowBlockArrangementReportExcel("Seating_Arrangement_Roomwise_Exam", "rptseatingplanroomwise_forexamdate.rpt");
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
    #endregion

    #region blockarrangement
    private void ExportinExcelBlockArrangementReport()
    {
        string attachment = "attachment; filename=" + "BlockArrangementRepot.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        // string EXAM_DATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        // string EXAM_DATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");

        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
        //string EXAM_DATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");

        string EXAM_DATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
        // string EXAM_DATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");


        int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        DataSet dsfee;
        dsfee = objSC.Get_BLOCK_ARRANGEMENT_REPORT(EXAM_DATE, slotno, Convert.ToInt32(ddlExamType.SelectedValue));

        DataGrid dg = new DataGrid();
        //DataTable dt = null;
        //dt = ds.

        if (dsfee.Tables.Count > 0)
        {


            dsfee.Tables[0].Columns.Remove("TIMEFROM");
            dsfee.Tables[0].Columns.Remove("TIMETO");
            dsfee.Tables[0].Columns.Remove("COLLEGE_ID");
            dsfee.Tables[0].Columns.Remove("ROOMNO");
            dsfee.Tables[0].Columns.Remove("SLOTNAME");
            dsfee.Tables[0].Columns.Remove("EXAMDATE");
            dsfee.Tables[0].Columns.Remove("SESSIONNAME");


            dg.DataSource = dsfee.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();


    }

    #endregion

    #region BlankMarksheet
    protected void btnblankmarksheetrepeater_Click(object sender, EventArgs e)
    {
        ShowReportPractical("Practical_Blank_Attendance_Report_Repeater", "rptpracticalBlankMarksheetRepeater.rpt");
    }
    protected void btnprctlreprtrepeater_Click(object sender, EventArgs e)
    {
        ShowReportPractical("Practical_Blank_Attendance_Report_Repeater", "rptpracticalattreportRepeater.rpt");
    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region
    protected void btnbuildingchart_Click(object sender, EventArgs e)
    {
        //    string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        //string EXAMDATE = ddlExamdate.SelectedItem.ToString();
        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
        // string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
        string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
        //string attachment = "attachment; filename=" + "BulidingChart_" + EXAMDATE + ".xls";


        //Response.ClearContent();
        //Response.AddHeader("content-disposition", attachment);
        //Response.ContentType = "application/" + "ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        DataSet dsfee, dssumm;
        DataTable dt = new DataTable();
        DataTable dtsum = new DataTable();
        dsfee = objSC.Get_BuildingChartDateWise(0, EXAMDATE, slotno, Convert.ToInt32(ddlExamType.SelectedValue));


        dssumm = objSC.Get_BuildingChartDateWiseSummary(0, EXAMDATE, slotno, Convert.ToInt32(ddlExamType.SelectedValue));
        dtsum = dssumm.Tables[0];



        //  this.ExporttoExcel(dt);
        DataGrid dg = new DataGrid();
        dt = dsfee.Tables[0];
        dt.Columns.Add("SUMMARY", typeof(System.String));
        if (dssumm.Tables[0].Rows.Count > 0 && dsfee != null)
        {
            //for (int i = 0; i <= 7; i++)
            //{

            //Added on 19052022 for to generate excel 
            string attachment = "attachment; filename=" + "BulidingChart_" + EXAMDATE + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);


            if (dtsum.Rows.Count > 0)
            {
                dt.Rows[0]["SUMMARY"] = "Exam Date";

                dt.Rows[1]["SUMMARY"] = dtsum.Rows[0]["DATE"].ToString();

                dt.Rows[2]["SUMMARY"] = dtsum.Rows[0]["SLOTNAME"].ToString();

                dt.Rows[3]["SUMMARY"] = dtsum.Rows[0]["STUDCOUNT"].ToString();
            }



            if (dtsum.Rows.Count > 1)
            {
                dt.Rows[4]["SUMMARY"] = dtsum.Rows[1]["SLOTNAME"].ToString();

                dt.Rows[5]["SUMMARY"] = dtsum.Rows[1]["STUDCOUNT"].ToString();

                dt.Rows[6]["SUMMARY"] = "Total Student Count";

                dt.Rows[7]["SUMMARY"] = dtsum.Rows[0]["TOTALCOUNT"].ToString();
            }
            else
            {
                dt.Rows[4]["SUMMARY"] = "Total Student Count";

                dt.Rows[5]["SUMMARY"] = dtsum.Rows[0]["TOTALCOUNT"].ToString();
            }
            //}

            if (dsfee.Tables.Count > 0)
            {
                dsfee.Tables[0].Columns.Remove("SRNO");
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();
            }

            dg.HeaderStyle.Font.Bold = true;
            dg.RenderControl(htw);

            Response.Write("<style> TABLE { border:solid 0.7px black; } " +
                      "TD { border:solid 0.7px black; text-align:center } </style>");
            // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
            //Response.Write("<table><tr><td><b>Total: </b></td><td></td><td><b>" +
            //    dTotalPrice.ToString("N2") + "</b></td></tr></table>");

            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(this.updBarcode, "Record not found for selectd date", this.Page);
        }
    }
    #endregion

    #region
    private void ExporttoExcel(DataTable table)
    {

        GridView GridView_Result = new GridView();
        GridView_Result.DataSource = table;
        GridView_Result.DataBind();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = GridView_Result.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(GridView_Result.Columns[j].HeaderText.ToString());
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }
    #endregion

    #region Dropdownlist
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            //DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", "@P_COLSCHEMENO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "");
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                if (ddlCollege.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    ddlSession.Focus();
                }
                else
                {
                    objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                }
            }
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        string examno = string.Empty;
        int pt = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(PATTERNNO,0) as PATTERNNO", "SCHEMENO='" + ViewState["schemeno"] + "'")));
        DataSet ds = objCommon.FillDropDown("ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + pt + " and ACTIVESTATUS=1 and fldname like'%EXTERMARK%'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                examno += ds.Tables[0].Rows[i]["EXAMNO"].ToString() + "$";

            }
            examno = examno.TrimEnd('$');
        }
        string dates = DateTime.Now.ToString("yyyy-MM-dd");
        ////objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL" + " AND EXAM_TT_TYPE = 11", "SLOTNO");
        string proc_name = "PKG_GET_EXAM_DATE_FOR_DATE";
        string parameter = "@P_EXAMDATE,@P_EXAM_TT_TYPE,@P_SESSIONNO";
        string Call_values = "" + dates + "," + examno + "," + ddlSession.SelectedValue + "";
        DataSet ds1 = objCommon.DynamicSPCall_Select(proc_name, parameter, Call_values);
        // DataSet ds = objsc.GetStudentsExamDateNEW(dates, examno);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlExamdate.DataSource = ds1;
            ddlExamdate.DataTextField = "Dates";
            ddlExamdate.DataValueField = "ID";
            ddlExamdate.DataBind();
        }
        ddlslot.Focus();
        
        //objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL" + " AND EXAMDATE >='" + dates + "' AND EXAM_TT_TYPE = " + examno + "", "SLOTNO");  // AND EXAM_TT_TYPE = 11" 

    }
    protected void ddlExamdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //   string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");

        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
        string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");

        //  string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");

        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'");

        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updBarcode, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";

        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO ASC");
            //ddlslot.SelectedIndex = 0;
        }
    }
    #endregion

    // ADDED BY SHUBHAM ON 14/03/2023
    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDouble.Checked)
        {
            rdbSingle.Checked = false;
            rdbTriple.Checked = false;
        }
    }

    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbTriple.Checked)
        {
            rdbDouble.Checked = false;
            rdbSingle.Checked = false;
        }

    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbSingle.Checked)
        {
            rdbDouble.Checked = false;
            rdbTriple.Checked = false;
        }
    }

 // ADDED BY SHUBHAM ON 14/03/2023
    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {

        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
        string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            btnexcelbutton.Visible = true;
            //ddlBlock.Visible = true;
            divBlockSet.Visible = true;
            objCommon.FillDropDownList(ddlBlock, "acd_seating_arrangement SE inner join ACD_ROOM R on R.ROOMNO=SE.ROOMNO inner join ACD_BLOCK B on r.BLOCKNO=B.BLOCKNO ", "distinct B.BLOCKNO", "B.BLOCKNAME", "CONVERT(VARCHAR(50),SE.EXAMDATE,103)='" + EXAMDATE + "' and ISNULL(B.ACTIVESTATUS,0)=1", "B.BLOCKNO ASC");
        }
 
        int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "Count(*)", "SLOTNO='" + Convert.ToInt32(ddlslot.SelectedValue) + "' AND CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'"));
        if (count > 0)
        {
            int pt = Convert.ToInt32((objCommon.LookUp("ACD_SEATING_ARRANGEMENT S INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=S.SLOTNO", " DISTINCT SEAT_STATUS", "S.SLOTNO='" + Convert.ToInt32(ddlslot.SelectedValue) + "' AND CONVERT(VARCHAR(50),S.EXAMDATE,103)='" + EXAMDATE + "'")));
            if (pt == 1)
            {
                rdbSingle.Checked = true;
                rdbDouble.Checked = false;
                rdbTriple.Checked = false;
            }
            else if (pt == 2)
            {
                rdbSingle.Checked = false;
                rdbDouble.Checked = true;
                rdbTriple.Checked = false;
            }
            else
            {
                rdbSingle.Checked = false;
                rdbDouble.Checked = false;
                rdbTriple.Checked = true;
            }
        }
        else
        {
           // objCommon.DisplayMessage("Please Configuration the Seating Plan", this.Page);

            objCommon.DisplayUserMessage(updBarcode, "Please configuration the Seating Arrangment ", this.Page);
        }
       

    }

    // ADDED BY SHUBHAM ON 14/03/2023
    private void ShowReportMasterDouble(string reportTitle, string rptFileName)
    {
        try
        {
            //getcollegeid();
            if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                //int prevStatus = 0;
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue) + ",@P_SEAT_STATUS=" + Convert.ToInt32(ViewState["DoubleSeating"]);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 7)
            {

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue) + ",@P_SEAT_STATUS=" + Convert.ToInt32(ViewState["DoubleSeating"]);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);


            }
            else if (Convert.ToInt32(Session["OrgId"]) == 20)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue) + ",@P_SEAT_STATUS=" + Convert.ToInt32(ViewState["DoubleSeating"]);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + ddlslot.SelectedValue + ",@P_PREV_STATUS=" + ddlExamType.SelectedValue;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + ",@P_SLOT_NO=" + Convert.ToInt32(ddlslot.SelectedValue) + ",@P_PREV_STATUS=" + Convert.ToInt32(ddlExamType.SelectedValue) + ",@P_SEAT_STATUS=" + Convert.ToInt32(ViewState["DoubleSeating"]);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updBarcode, this.updBarcode.GetType(), "controlJSScript", sb.ToString(), true);

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

    private void ShowReport_Excel(string exporttype, string rptFileName)
    {
        try
        {

            string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);

            //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd-MM-yyyy");  //ToString("yyyy-MMyyyy");

             string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
             //string EXAMDATE = Convert.ToString(EXAMDATE.ToString("dd-MMM-yyyy"));   


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlslot.SelectedItem.Text + "_MarkEntryReport" + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();

            url += "&param=@P_EXAMDATE=" + EXAMDATE + ",@P_SLOTNO="+ddlslot.SelectedValue+",@P_BLOCKNO=" +ddlBlock.SelectedValue+ "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void AddReportHeader(GridView gv)
    {
        try
        {
            string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);

            //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd-MM-yyyy");  //ToString("yyyy-MMyyyy");

            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
            DataSet dsfee;
            dsfee = objSC.Get_BLOCK_ARRANGEMENT_REPORT_BLOCKWISE(EXAMDATE, Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue)); 

            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header1Cell = new TableCell();

            Header1Cell.Text = "R. C. Patel Institute of Technology";
            Header1Cell.ColumnSpan = 6;
            Header1Cell.Font.Size = 14;
            Header1Cell.Font.Bold = true;
            Header1Cell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow1.Cells.Add(Header1Cell);
            gv.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            GridViewRow HeaderGridRow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header2Cell = new TableCell();

            Header2Cell.Text = "SUBJECT NAME:-" + dsfee.Tables[0].Rows[0]["COURSENAME"].ToString()  + "      " +  "CCODE:-"+ dsfee.Tables[0].Rows[0]["CCODE"].ToString();
            Header2Cell.ColumnSpan =6;
            Header2Cell.Font.Size = 12;
            Header2Cell.Font.Bold = true;
            Header2Cell.HorizontalAlign = HorizontalAlign.NotSet;
            HeaderGridRow2.Cells.Add(Header2Cell);
            gv.Controls[0].Controls.AddAt(1, HeaderGridRow2);


            GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header3Cell = new TableCell();

            Header3Cell.Text = "Block Name:-" + dsfee.Tables[0].Rows[0]["BLOCKNAME"].ToString() + "                                " + "Date:-" + dsfee.Tables[0].Rows[0]["EXAMDATE"].ToString() +"                                 "+ "Bundle Name :-                 " ;
            Header3Cell.ColumnSpan = 6;
            Header3Cell.Font.Size = 12;
            Header3Cell.Font.Bold = true;
            Header3Cell.HorizontalAlign = HorizontalAlign.NotSet;
            HeaderGridRow3.Cells.Add(Header3Cell);
            gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

           

            GridViewRow HeaderGridRow4 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Header4Cell = new TableCell();

            //Header3Cell.Text = "Report : Students Strength " + ddlHostelNo.SelectedItem.Text;
            //Header3Cell.ColumnSpan = 10;
            //Header3Cell.Font.Size = 12;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

             gv.HeaderRow.Visible = true;

            //GridViewRow HeaderGridRow3 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell Header3Cell = new TableCell();


            //Header3Cell.Text = "BLOCK";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "I YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "II (LE)";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "III YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "IV YEAR";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

            //Header3Cell = new TableCell();
            //Header3Cell.Text = "TOTAL STRENGTH";
            //Header3Cell.ColumnSpan = 1;
            //Header3Cell.RowSpan = 1;
            //Header3Cell.Font.Size = 10;
            //Header3Cell.Font.Bold = true;
            //Header3Cell.HorizontalAlign = HorizontalAlign.Center;
            //HeaderGridRow3.Cells.Add(Header3Cell);
            //gv.Controls[0].Controls.AddAt(2, HeaderGridRow3);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnexcelbutton_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlBlock.SelectedIndex > 0)
            //{
            //    if (Convert.ToInt32(Session["OrgId"]) == 1)
            //    {
            //        this.ShowReport_Excel("xls", "rptExcelBlockwiseReport_RCPIT.rpt");
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updBarcode, "Please Select Block!!", this.Page);
            //}
            string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);

            //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd-MM-yyyy");  //ToString("yyyy-MMyyyy");

            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
            DataSet dsfee;
            dsfee = objSC.Get_BLOCK_ARRANGEMENT_REPORT_BLOCKWISE(EXAMDATE, Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlBlock.SelectedValue));            //DataGrid dg = new DataGrid();
            GridView dg = new GridView();
            if (dsfee.Tables.Count > 0)
            {
                dg.DataSource = dsfee.Tables[0];
                dg.DataBind();
                AddReportHeader(dg);
                string attachment = "attachment; filename=" + "BLOCKLIST.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.updBarcode, "No Data Found for this selection.", this.Page);
            }
        }
        catch (Exception ex)
        {

        }
    }
}