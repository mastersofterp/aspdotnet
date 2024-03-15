//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : INVIGILATOR DUTY ENTRY 
// CREATION DATE : 21-MAR-2012
// CREATED BY    : PRIYANKA KABADE
// MODIFIED DATE :
// MODIFIED DESC :
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_InvigilationDutyEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();
    SeatingController objSc = new SeatingController();
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            PopulateDropDownList();
            //FillText();
            //btnReport.Visible = false;
        }
        objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    #endregion

    #region Other Events

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExTTType.Items.Clear();
        ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
        ddlDate.Items.Clear();
        ddlDate.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        lvRoomDetails.DataSource = null;
        lvRoomDetails.DataBind();
        lvRoomDetails.Visible = false;
        txtExtraInv.Text = "0";
        int patternno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "PATTERNNO", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ""));
        if (patternno > 0)
        {
            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", " DISTINCT EXAMNO", "EXAMNAME", "EXAMNO>0 AND PATTERNNO=" + patternno + " AND FLDNAME='EXTERMARK'", "EXAMNO ASC");
            //Added BY SHUBHAM ON 17022023
            //string SUBID = objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + ddlcourseforabset.SelectedValue.Split('-')[0]);
            //int course = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COURSENO", "COURSENO=" + Convert.ToInt32(ddlcourseforabset.SelectedValue.Split('-')[0]) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ""));
            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME", " DISTINCT EXAMNO", "EXAMNAME", "EXAMNO>0 AND PATTERNNO=" + patternno + " AND ACTIVESTATUS = 1", "EXAMNO ASC");
            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E INNER JOIN ACD_EXAM_DATE D ON (D.EXAM_TT_TYPE = E.EXAMNO)", " DISTINCT E.EXAMNO", "E.EXAMNAME", "D.COLLEGE_ID=" + ddlcollege.SelectedValue + "AND D.SCHEMENO=" + schemeno + "AND D.COURSENO=" + course + "AND D.SUBID=" + Convert.ToInt32(SUBID.ToString()) + "AND D.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "", "EXAMNO");
            //objCommon.FillDropDownList(ddlexamnameabsentstudent, "ACD_EXAM_NAME E INNER JOIN ACD_EXAM_DATE D ON (D.EXAM_TT_TYPE = E.EXAMNO)", " DISTINCT E.EXAMNO", "E.EXAMNAME", "D.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND D.COURSENO=" + course + "AND D.SESSIONNO=" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "", "EXAMNO");
            objCommon.FillDropDownList(ddlExTTType, "ACD_EXAM_NAME E INNER JOIN ACD_EXAM_DATE D ON (D.EXAM_TT_TYPE = E.EXAMNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = D.SESSIONNO)", " DISTINCT E.EXAMNO", "E.EXAMNAME", "D.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND D.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND E.PATTERNNO=" + Convert.ToInt32(patternno) + "", "EXAMNO");
            ddlExTTType.Focus();
        }
        //objCommon.FillDropDownList(ddlExTTType, "ACD_EXAM_NAME ED WITH (NOLOCK)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('S3','EXTERMARK') AND EXAMNAME<>''", "EXAMNAME");

    }
    //protected void ddlSession2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlSession2.SelectedValue != "0")
    //        objCommon.FillDropDownList(ddlInvigilator, "ACD_INVIGilATION_DUTY D INNER JOIN ACD_EXAM_INVIGILATOR EX ON (D.UA_NO = EX.UA_NO AND EX.STATUS = 1) INNER JOIN USER_ACC U ON (D.UA_NO = U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME ", "D.SESSIONNO=" + ddlSession2.SelectedValue, "U.UA_FULLNAME");
    //    else
    //    {
    //        lblExamDate2.Text = null;
    //        ddlSlot2.Items.Clear();
    //        ddlSlot2.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSlot2.Focus();
    //    }
    //}
    //protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDay.SelectedValue != "0")
    //    {
    //        string sessionno = ddlSession.SelectedValue;
    //        string day_no = ddlDay.SelectedValue;
    //        string slot = ddlSlot.SelectedValue;
    //        lblExamDate.Text = objCommon.LookUp("ACD_EXAM_DATE WITH (NOLOCK)", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103)", "DAYNO = " + ddlDay.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);
    //        objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE EX WITH (NOLOCK) INNER JOIN ACD_EXAM_TT_SLOT S WITH (NOLOCK) ON (S.SLOTNO = EX.SLOTNO)", "DISTINCT EX.SLOTNO", "S.SLOTNAME", " EX.SESSIONNO = " + ddlSession.SelectedValue + " AND EX.DAYNO = " + ddlDay.SelectedValue, "EX.SLOTNO");
    //        //txtStudent.Text = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO)", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + "AND SR.EXAM_REGISTERED=1 AND (DETAIND = 0 OR DETAIND IS NULL )AND SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " ) AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " )");
    //    }
    //    else
    //    {
    //        lblExamDate.Text = null;
    //        ddlSlot.Items.Clear();
    //        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSlot.Focus();
    //    }
    //}
    //protected void ddlDay2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDay2.SelectedValue != "0")
    //    {
    //        lblExamDate2.Text = objCommon.LookUp("ACD_EXAM_DATE", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103) + ' - ' +DATENAME(DW,EXAMDATE)", "DAYNO = " + ddlDay2.SelectedValue + " AND SESSIONNO = " + ddlSession2.SelectedValue);
    //        objCommon.FillDropDownList(ddlSlot2, "ACD_INVIGILATION_DUTY I INNER JOIN ACD_EXAM_TT_SLOT S ON (S.SLOTNO = I.SLOTNO)", "DISTINCT I.SLOTNO", "S.SLOTNAME", " I.SESSIONNO = " + ddlSession2.SelectedValue + " AND I.DAYNO = " + ddlDay2.SelectedValue + " AND I.UA_NO = " + ddlInvigilator.SelectedValue, "I.SLOTNO");
    //    }
    //    else
    //    {
    //        lblExamDate2.Text = null;
    //        ddlSlot2.Items.Clear();
    //        ddlSlot2.Items.Add(new ListItem ("Please Select","0"));
    //        ddlSlot2.Focus();

    //    }
    //}
    //protected void ddlInvigilator_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlInvigilator.SelectedValue != "0")
    //        objCommon.FillDropDownList(ddlDay2, "ACD_INVIGILATION_DUTY", "DISTINCT DAYNO AS DAY", "DAYNO", "SESSIONNO =" + ddlSession2.SelectedValue + " AND UA_NO=" + ddlInvigilator.SelectedValue, "DAYNO");
    //    else
    //    {
    //        ddlDay2.Items.Clear();
    //        ddlDay2.Items.Add(new ListItem ("Please Select","0"));
    //        ddlInvigilator.Focus();
    //        lblExamDate2.Text = null;
    //        ddlSlot2.Items.Clear();
    //        ddlSlot2.Items.Add(new ListItem ("Please Select","0"));
    //        ddlSlot2.Focus();
    //    }
    //}
    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtExtraInv.Text = "0";
    }
    #endregion

    #region Click Events
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {

            //var examdate = DateTime.ParseExact(txtExamDate.Text, "YYYY-MM-DD", null);
            DateTime _date;
            string examdate = "";
            string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
            DateTime toknow = Convert.ToDateTime(EXAMDAT);
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
            //_date = DateTime.Parse(txtExamDate.Text);
            //examdate = _date.ToString("dd-MMM-yyyy");

            // Convert.ToDateTime(examdate);

            //return;
            //DateTime examDate = txtExamDate.Text.ToString("YYYY-MM-DD HH:MM:DD:MMM");
            //CustomStatus cs = (CustomStatus)objSc.InvigilatorDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToDateTime(lblExamDate.Text), Convert.ToInt32(ddlSlot.SelectedValue),Convert.ToInt32(txtExtraInv.Text),Session["colcode"].ToString());
            CustomStatus cs = (CustomStatus)objSc.InvigilatorDuty(SessionNo, Convert.ToInt32(ddlExTTType.SelectedValue), Convert.ToDateTime(toknow), Convert.ToInt32(ddlSlot.SelectedValue), Convert.ToInt32(txtExtraInv.Text), Session["colcode"].ToString());
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updInvigDuty, "Invigilation Duty Done ...!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updInvigDuty, "Error while Invigilation Duty..", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_InvigilationDutyEntry.btnGenerate_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0" || ddlExTTType.SelectedValue != "0")
        {
            //this.ShowReportDayWise("Invigilation_Duty", "rptInvigilation.rpt");
            // ShowReportinFormate(rdoReportType.SelectedValue, "rptInvigilation.rpt");
            if (rdoReportType.SelectedValue == "pdf")
            {
                ShowReportDayWise("Invigilation_Duty", "rptInvigilation.rpt");
            }

            else if (rdoReportType.SelectedValue == "xls")
            {
                ShowReportExcel(rdoReportType.SelectedValue, "rptInvigilation.rpt");
            }
            else if (rdoReportType.SelectedValue == "doc")
            {
                ShowReportWord(rdoReportType.SelectedValue, "rptInvigilation.rpt");

            }
        }
        else
        {
            objCommon.DisplayMessage(this.updInvigDuty, "Please select Session and Exam Name..!!", this.Page);
        }

    }

    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlExTTType.SelectedItem.Text + "_" + ddlSession.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + SessionNo + ",@P_EXAM_NO=" + ddlExTTType.SelectedValue + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            // ScriptManager.RegisterClientScriptBlock(this.updInvigDuty, this.updInvigDuty.GetType(), "controlJSScript", sb.ToString(), true);
            ScriptManager.RegisterClientScriptBlock(this.updInvigDuty, this.updInvigDuty.GetType(), "controlJSScript", sb.ToString(), true);
            //string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {
        //this.ShowReportInvigilatorWise("Invigilation_Duty_Invigilator_Wise", "rptInvigilationWise.rpt");
    }
    #endregion

    #region User Methods
    private void PopulateDropDownList()
    {
        string deptno = string.Empty;
        if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
            deptno = "0";
        else
            deptno = Session["userdeptno"].ToString();
        if (Session["usertype"].ToString() != "1")
            objCommon.FillDropDownList(ddlclgScheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
        //AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
        else

            objCommon.FillDropDownList(ddlclgScheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

        //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND OrganizationId=" + Convert.ToInt32(Session["OrgId"].ToString()) + "", "COLLEGE_ID");
        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession2, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 ", "SESSIONNO DESC");
        //Exam Time Table Types
        //ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
        //ddlExTTType.Items.Add(new ListItem("Mid Exam Time Table", "1"));
        //ddlExTTType.Items.Add(new ListItem("End Exam Time Table", "2"));
        //
        //objCommon.FillDropDownList(ddlExTTType, "ACD_EXAM_NAME ED WITH (NOLOCK)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('S3','EXTERMARK') AND EXAMNAME<>'' AND ED.PATTERNNO=5 ", "EXAMNAME");
        //objCommon.FillDropDownList(ddlExTTType, "ACD_EXAM_NAME ED WITH (NOLOCK)", " DISTINCT EXAMNO", "EXAMNAME", " ED.FLDNAME IN('S3','EXTERMARK') AND EXAMNAME<>''", "EXAMNAME");

    }
    //private void FillText()
    //{
    //    txtSeat.Text = objCommon.LookUp("ACD_ROOM", "AVG(ACTUALCAPACITY)", "ROOMNO != 0");

    //    }

    private void ShowReportDayWise(string reportTitle, string rptFileName)
    {
        try
        {
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
            string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
            string SP_Name = "PKG_ACAD_REPORT_INVIGILATION_DUTY";
            string SP_Parameters = "@P_SESSIONNO,@P_EXAM_NO,@P_EXAM_DATE";
            string Call_Values = "" + SessionNo + ", " + ddlExTTType.SelectedValue + ", " + EXAMDATE + "";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + SessionNo + ",@P_EXAM_NO=" + ddlExTTType.SelectedValue + ",@P_EXAM_DATE=" + EXAMDATE + ",@P_COLLEGE_CODE=" + ViewState["college_id"].ToString();

                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updInvigDuty, this.updInvigDuty.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updInvigDuty, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //private void ShowReportInvigilatorWise(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_DAYNO=" +ddlDay2.SelectedValue + ",@P_SLOTNO=" + ddlSlot2.SelectedValue + ",@P_UA_NO=" + (string.IsNullOrEmpty(ddlInvigilator.SelectedValue) == true ? "0" : ddlInvigilator.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OrderNo=" + txtOrderNo.Text + ",@P_Date=" + txtDate.Text;

    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updInvigAuto, this.updInvigAuto.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlExTTType.SelectedIndex = 0;
        // ddlDay.SelectedIndex = 0;
        ddlSlot.SelectedIndex = 0;
        // lblExamDate.Text = null;
        // txtExamDate.Text = null;
        txtExtraInv.Text = string.Empty;
    }
    #endregion

    //protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID="+ddlcollege.SelectedValue, "SESSIONNO DESC");
    //}



    protected void ddlExTTType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDate.Items.Clear();
        ddlDate.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        txtExtraInv.Text = "0";
        lvRoomDetails.DataSource = null;
        lvRoomDetails.DataBind();
        lvRoomDetails.Visible = false;
        if (ddlExTTType.SelectedValue != "0")
        {
            int count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT SA INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = SA.SESSIONNO)", "COUNT(1)", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND SA.EXAMNO =" + Convert.ToInt32(ddlExTTType.SelectedValue) + ""));
           // int count = 1;
            if (count > 0)
            {
                objCommon.FillDropDownList(ddlDate, "ACD_EXAM_DATE E INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)", "DISTINCT CONVERT(NVARCHAR,E.EXAMDATE,103)DATE", "CONVERT(NVARCHAR,E.EXAMDATE,103)EXAMDATE", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND E.STATUS=1 AND  E.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "DATE");
            }
            else
            {
                objCommon.DisplayMessage(updInvigDuty, "Seating Arrangement Not Define Please define Seating Arrangement First", this.Page);
            }
        }
        else ;
        //ClearControls();
    }

    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        string sessionno = ddlSession.SelectedValue;
        string slot = ddlSlot.SelectedValue;
        //lblExamDate.Text = objCommon.LookUp("ACD_EXAM_DATE", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103)", "DAYNO = " + ddlDay.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);
        objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE EX WITH (NOLOCK) INNER JOIN ACD_EXAM_TT_SLOT S WITH (NOLOCK) ON (S.SLOTNO = EX.SLOTNO)", "DISTINCT EX.SLOTNO", "S.SLOTNAME", " EX.SESSIONNO = " + ddlSession.SelectedValue, "EX.SLOTNO");
    }

    //changes done by shubham on 07/04/2023
    protected void ddlclgScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDate.Items.Clear();
        ddlDate.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot.Items.Clear();
        ddlSlot.Items.Add(new ListItem("Please Select", "0"));
        ddlExTTType.Items.Clear();
        ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
        txtExtraInv.Text = "0";
        try
        {
            if (ddlclgScheme.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlclgScheme.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and ISNULL(IS_ACTIVE,0)=1  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0  AND ISNULL(IS_ACTIVE,0)=1 AND FLOCK = 1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");//--AND FLOCK = 1
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_NAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONID DESC");
                    ddlSession.Focus();
                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    //added by shubham On 17-04-23
    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex > 0)
        {
            string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            txtExtraInv.Text = "0";

            //  string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");

            string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'");

            if (a.ToString() == "0")
            {
                objCommon.DisplayUserMessage(updInvigDuty, "No Exams Are Conducted on Selected Date", this.Page);
                ddlSlot.SelectedValue = "0";

            }
            else
            {
                // added by Shubham on 22022024
                //string RoomNos = objCommon.LookUp("", "DBO.SPLIT(,',')", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND ET.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SA.EXAMNO =" + Convert.ToInt32(ddlExTTType.SelectedValue) + " AND SA.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)");
                DataSet ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT SA INNER JOIN ACD_EXAM_DATE ET ON (SA.SESSIONNO = ET.SESSIONNO AND ET.EXAM_TT_TYPE = SA.EXAMNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = SA.SESSIONNO)", " DISTINCT SA.ROOMNO", "SA.ROOMNO AS RNO", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND ET.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SA.EXAMNO =" + Convert.ToInt32(ddlExTTType.SelectedValue) + " AND SA.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "SA.ROOMNO");
                if (ds.Tables.Count > 0)
                {
                    int RoomCount = Convert.ToInt32(ds.Tables[0].Rows.Count);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int InvRCount = Convert.ToInt32(objCommon.LookUp("ACD_ROOM", "Count(*)", "ISNULL(REQD_INVIGILATORS,0) > 0 and ROOMNO IN (SELECT  DISTINCT SA.ROOMNO FROM ACD_SEATING_ARRANGEMENT SA INNER JOIN ACD_EXAM_DATE ET ON (SA.SESSIONNO = ET.SESSIONNO AND ET.EXAM_TT_TYPE = SA.EXAMNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = SA.SESSIONNO) WHERE SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND ET.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SA.EXAMNO =" + Convert.ToInt32(ddlExTTType.SelectedValue) + " AND SA.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103))"));
                        if (RoomCount == InvRCount)
                        {
                            //objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO ASC");
                            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT ES INNER JOIN ACD_EXAM_DATE E ON (E.SLOTNO = ES.SLOTNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)", "DISTINCT ES.SLOTNO", "ES.SLOTNAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND E.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND E.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "ES.SLOTNO");
                            ddlSlot.Focus();
                            btnGenerate.Enabled = true;
                        }
                        else
                        {
                            DataSet dsRInvg = objCommon.FillDropDown("ACD_ROOM", " DISTINCT ROOMNO", "ROOMNAME", "ISNULL(REQD_INVIGILATORS,0) = 0 and ROOMNO IN (SELECT  DISTINCT SA.ROOMNO FROM ACD_SEATING_ARRANGEMENT SA INNER JOIN ACD_EXAM_DATE ET ON (SA.SESSIONNO = ET.SESSIONNO AND ET.EXAM_TT_TYPE = SA.EXAMNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = SA.SESSIONNO) WHERE SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND ET.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SA.EXAMNO =" + Convert.ToInt32(ddlExTTType.SelectedValue) + " AND SA.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103))", "ROOMNO");
                            lvRoomDetails.DataSource = dsRInvg;
                            lvRoomDetails.DataBind();
                            lvRoomDetails.Visible = true;
                            btnGenerate.Enabled = false;

                            objCommon.DisplayMessage(updInvigDuty, "Required Invigilator Entry Not Done for Rooms.", this.Page);
                            //Response.Redirect("http://localhost:61091/PresentationLayer/ACADEMIC/SEATINGARRANGEMENT/RoomInvigilator.aspx?pageno=3011");
                           
                        }
                    }
                    else
                    {
                        lvRoomDetails.DataSource = null;
                        lvRoomDetails.DataBind();
                        lvRoomDetails.Visible = false;
                        btnGenerate.Enabled = false;
                        objCommon.DisplayMessage(updInvigDuty, "Seating Arrangement Not Done for " + EXAMDATE + "!.", this.Page);
                    }
                }
                else
                {
                    lvRoomDetails.DataSource = null;
                    lvRoomDetails.DataBind();
                    lvRoomDetails.Visible = false;
                    btnGenerate.Enabled = false;
                    objCommon.DisplayMessage(updInvigDuty, "Seating Arrangement Not Done for " + EXAMDATE + "!.", this.Page);
                }
                //objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR(50),EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO ASC");
                //objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT ES INNER JOIN ACD_EXAM_DATE E ON (E.SLOTNO = ES.SLOTNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)", "DISTINCT ES.SLOTNO", "ES.SLOTNAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND E.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND E.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "ES.SLOTNO");
            }
        }
        else 
        {
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            //ddlSlot.Items.Clear();
            //ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            //ddlExTTType.Items.Clear();
            //ddlExTTType.Items.Add(new ListItem("Please Select", "0"));
            //txtExtraInv.Text = "0";
            lvRoomDetails.DataSource = null;
            lvRoomDetails.DataBind();
            lvRoomDetails.Visible = false;
            
        }
    }

    //added by shubham On 17-04-23
    private void ShowReportExcel(string exporttype, string rptFileName)
    {
        try
        {
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
            string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
            string attachment = "attachment; filename=" + "InvigilationDutyEntry.xls";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSIONNO", Convert.ToInt32(SessionNo)),
               new SqlParameter("@P_EXAM_NO", Convert.ToInt32(ddlExTTType.SelectedValue)),
               new SqlParameter("@P_EXAM_DATE", EXAMDATE),
              
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_INVIGILATION_DUTY", objParams);
            if (dsfee.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsfee.Tables[0];
                foreach (DataColumn dc in dt.Columns)
                {

                }
                DataGrid dg = new DataGrid();

                if (dsfee.Tables.Count > 0)
                {
                    dg.DataSource = dsfee.Tables[0];
                    dg.DataBind();

                }
                dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updInvigDuty, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //added by shubham On 17-04-23
    private void ShowReportWord(string exporttype, string rptFileName)
    {
        try
        {
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "SM.SESSIONNO", "isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString() + "AND S.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue)));
            string EXAMDAT = Convert.ToString(ddlDate.SelectedItem);
            string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("yyyy-MM-dd");
            string attachment = "attachment; filename=" + "InvigilationDutyEntry.doc";

            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/" + "ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = new SqlParameter[] 
           { 
               new SqlParameter("@P_SESSIONNO", Convert.ToInt32(SessionNo)),
               new SqlParameter("@P_EXAM_NO", Convert.ToInt32(ddlExTTType.SelectedValue)),
               new SqlParameter("@P_EXAM_DATE",EXAMDATE) ,
              
           };
            DataSet dsfee = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_INVIGILATION_DUTY", objParams);
            if (dsfee.Tables[0].Rows.Count > 0)
            {
                DataTable dt = dsfee.Tables[0];
                foreach (DataColumn dc in dt.Columns)
                {

                }
                DataGrid dg = new DataGrid();

                if (dsfee.Tables.Count > 0)
                {
                    dg.DataSource = dsfee.Tables[0];
                    dg.DataBind();

                }
                dg.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                dg.HeaderStyle.BackColor = System.Drawing.Color.DeepSkyBlue;
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updInvigDuty, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_REPORTS_CoursewiseStudentRollList.ShowReportRegForm() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}

