using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.Net;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.NetworkInformation;

using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class Academic_REPORTS_MarksEntryNotDone : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    CourseController objCC = new CourseController();

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

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S inner join ACD_STUDENT_RESULT ST on S.SESSIONNO=ST.SESSIONNO", "Distinct(ST.SESSIONNO)", "S.SESSION_PNAME", "S.SESSIONNO ", "SESSIONNO DESC");
                //--if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8") //Added By Nikhil V.Lambe on 24/02/2021 for page should be access by Admin and HOD.
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S inner join ACD_STUDENT_RESULT ST on S.SESSIONNO=ST.SESSIONNO", "Distinct(ST.SESSIONNO)", "S.SESSION_PNAME", "S.SESSIONNO > 0", "SESSIONNO DESC");



                //else
                // --   Response.Redirect("~/notauthorized.aspx?page=LockMarksByScheme.aspx");
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 ", "DEGREENO");
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  


                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "SESSIONID > 0 AND ISNULL(IS_ACTIVE,0) = 1", "SESSION_NAME DESC");

                //objCommon.FillListBox(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "SESSIONID > 0 AND ISNULL(IS_ACTIVE,0) = 1", "SESSION_NAME DESC");

                PopulateSessionDropDown();

                if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    tbnStatus.Visible = true;
                    Note.Visible = true;
                    btnMarkStatus.Visible = false;
                    Note2.Visible = false;
                    btnAbsententryreport.Visible = false;
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    btnAbsententryreport.Visible = true;
                    btnMarkStatus.Visible = true;
                }
                else
                {
                    tbnStatus.Visible = false;
                    Note.Visible = false;
                    btnMarkStatus.Visible = true;
                    Note2.Visible = false;
                    btnAbsententryreport.Visible = false;
                }

            }

        }
        string strHostName = System.Net.Dns.GetHostName();
        string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        // Label1.Text = GetIP4Address();
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateSessionDropDown()   //Added by Sachin A dt on 27022023 as per requirement
    {
        try
        {



            ////Fill Dropdown Session 
            //string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            //DataSet dsCollegeSession = objCC.GetCollegeSession(0, college_IDs);

            //ddlSession.Items.Clear();
            ////ddlCollege.Items.Add("Please Select");
            //ddlSession.DataSource = dsCollegeSession;
            //ddlSession.DataValueField = "SESSIONNO";
            //ddlSession.DataTextField = "COLLEGE_SESSION";
            //ddlSession.DataBind();
            ////  rdbReport.SelectedIndex = -1;

            objCommon.FillDropDownList(ddlSessionID, "ACD_SESSION_MASTER S INNER JOIN ACD_STUDENT_RESULT ST on S.SESSIONNO=ST.SESSIONNO INNER JOIN ACD_SESSION SI ON (S.SESSIONID = SI.SESSIONID)", "Distinct SI.SESSIONID", "SI.SESSION_NAME", "SI.SESSIONID > 0 AND ISNULL(S.IS_ACTIVE,0) = 1", "SI.SESSIONID DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    string GetIP4Address()
    {
        string IP4Address = String.Empty;


        foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (IPA.AddressFamily.ToString() == "InterNetwork")
            {
                IP4Address = IPA.ToString();
                break;
            }
        }
        return IP4Address;
    }

    //protected void btnReport1_Click(object sender, EventArgs e)
    //{
    //    ShowReport("REPORT", "rptTeacherNotAlloted.rpt"); 
    //}

    //protected void btnReport2_Click(object sender, EventArgs e)
    //{
    //    if (ddlTest.SelectedValue == "0" || ddlTest.SelectedValue == "1" || ddlTest.SelectedValue == "2" || ddlTest.SelectedValue == "3" || ddlTest.SelectedValue == "4")
    //    {
    //        try
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=REPORT";
    //            url += "&path=~,Reports,Academic," + "rptTestMarksEntryNotDone.rpt";
    //            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
    //            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue +",@P_SUBEXAMTYPE="+ (divSubExam.Visible == true ? ddlSubExam.SelectedValue : string.Empty); // modified on 15-02-2020 by Vaishali

    //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //            divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //            divMsg.InnerHtml += " </script>";

    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");

    //            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }
    //    else
    //    {

    //        if (rblStud.SelectedValue == "0")
    //        {
    //            try
    //            {
    //                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //                url += "Reports/CommonReport.aspx?";
    //                url += "pagetitle=REPORT";
    //                url += "&path=~,Reports,Academic," + "rptTestMarksEntryNotDone.rpt";
    //                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

    //                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //                divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //                divMsg.InnerHtml += " </script>";

    //                System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //                sb.Append(@"window.open('" + url + "','','" + features + "');");

    //                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    //            }
    //            catch (Exception ex)
    //            {
    //                if (Convert.ToBoolean(Session["error"]) == true)
    //                    objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //                else
    //                    objUCommon.ShowError(Page, "Server Unavailable.");
    //            }
    //        }
    //        else
    //        {
    //            try
    //            {
    //                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //                url += "Reports/CommonReport.aspx?";
    //                url += "pagetitle=REPORT";
    //                url += "&path=~,Reports,Academic," + "rptTestMarksEntryNotDoneForBacklog.rpt";
    //                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

    //                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //                divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //                divMsg.InnerHtml += " </script>";
    //                System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //                sb.Append(@"window.open('" + url + "','','" + features + "');");

    //                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);


    //            }
    //            catch (Exception ex)
    //            {
    //                if (Convert.ToBoolean(Session["error"]) == true)
    //                    objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //                else
    //                    objUCommon.ShowError(Page, "Server Unavailable.");
    //            }
    //        }
    //    }
    //}


    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    private void PopulateDropDown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            else
                objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH WITH (NOLOCK)", "BRANCHNO", "LONGNAME", "DEGREENO >0", "BRANCHNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_BulkRegistration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private string GetSessionNew()
    {
        string SessionNos = "";


        foreach (ListItem item in ddlSession.Items)
        {
            if (item.Selected == true)
            {
                SessionNos += item.Value + '$';
            }

        }
        //if (!string.IsNullOrEmpty(DegreeNos))
        //{
        //    objConfig.DegreeNoS = DegreeNos.Substring(0, DegreeNos.Length - 1);
        //}
        return SessionNos;
    }


    protected void btnReport2_Click(object sender, EventArgs e)
    {
        //modified on 15-02-2020 by Vaishali
        //if (divSubExam.Visible == true)
        //    rfvSubExam.Enabled = true;
        //else
        //    rfvSubExam.Enabled = false;
        try
        {
            DataSet ds = null;
            // ADDED BY SHUBHAM
            string CollageID = GetCollageID();
            string CollageIDs = CollageID.Remove(CollageID.Length - 1);
            //            ViewState["SessionNo"] = SessionNo;
            ViewState["SessionNo"] = objCommon.LookUp("ACD_SESSION_MASTER", "STRING_AGG(SESSIONNO,'$')", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + "AND COLLEGE_ID IN (" + CollageIDs + ")");



            //DataSet ds = objMarksEntry.GetMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSchool.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue, divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);
            //DataSet ds = objMarksEntry.GetMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue.Split('-')[1].ToString(), divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);

            //DataSet ds = objMarksEntry.GetMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue.Split('-')[1].ToString(), divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);
            //DataSet ds = objMarksEntry.GetMarkEntryNotDoneRecordReport(Convert.ToInt32(ViewState["SessionNo"]), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue.Split('-')[1].ToString(), divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);


            string sp_procedure = "PKG_ACAD_TEST_MARKS_ENTRY_NOTDONE";
            string sp_parameters = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_SEMESTERNO,@P_EXAMNO,@P_SUBEXAMTYPE,@P_SUBJECTTYPE";
            // string sp_callValues = "'" + ViewState["SessionNo"].ToString() + "'," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + ",'" + ddlSubExam.SelectedValue + "'";
            string sp_callValues = "" + ViewState["SessionNo"] + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + "," + ddlSubExam.SelectedValue + "," + ddlSubType.SelectedValue + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(updpnlExam, "Record Not Found !!!!", this.Page);
                return;
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=REPORT";
                url += "&path=~,Reports,Academic," + "rptTestMarksEntryNotDone.rpt";
                //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
                //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SUBEXAMTYPE=" + (divSubExam.Visible == true ? ddlSubExam.SelectedValue : string.Empty); // modified on 15-02-2020 by Vaishali
                //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SUBEXAMTYPE=" + (divSubExam.Visible == true ? ddlSubExam.SelectedValue : string.Empty); // modified on 15-02-2020 by Vaishali
                //url += "&param=@P_SESSIONNO=" + ViewState["SessionNo"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SUBEXAMTYPE=" + ddlSubExam.SelectedValue; // modified on 15-02-2020 by Vaishali

                if (!CollageIDs.Contains(","))
                {
                    url += "&param=@P_SESSIONNO=" + ViewState["SessionNo"] + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(CollageIDs) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SUBEXAMTYPE=" + ddlSubExam.SelectedValue + ",@P_SUBJECTTYPE=" + ddlSubType.SelectedValue; // modified on 15-02-2020 by Vaishali
                }else{
                url += "&param=@P_SESSIONNO=" + ViewState["SessionNo"] + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_SUBEXAMTYPE=" + ddlSubExam.SelectedValue + ",@P_SUBJECTTYPE=" + ddlSubType.SelectedValue; // modified on 15-02-2020 by Vaishali
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO>0", "BRANCHNO");
            //ddlBranch.Focus();
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
        //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "LONGNAME");

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        //  objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + "AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENAME");
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        if (ddlBranch.SelectedIndex > 0)
        {
            //if (ddlBranch.SelectedValue == "99")
            //    objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME B ON (A.SCHEMENO=B.SCHEMENO)", "DISTINCT B.SCHEMENO", "B.SCHEMENAME", " SCHEMETYPE = 1 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND A.SESSIONNO=" + ddlSession.SelectedValue, "schemename");
            //else
            //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "DISTINCT SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
            //ddlScheme.Focus();

            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "branchno = " + ddlBranch.SelectedValue, "SCHEMENO");
        }

    }

    //static String SessionNo_New;

    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlSession.SelectedIndex > 0)
    //    if (ddlDegree.SelectedIndex == -1)
    //    {
    //        String SessionNo = String.Empty;
    //        String SessionNo_New = String.Empty;
    //        ViewState["SessionNo"] = null;

    //       // SessionNo = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue)  + "AND COLLEGE_ID = " + Convert.ToInt32(ddlSession.SelectedValue));

    //        foreach (ListItem items in ddlSession.Items)
    //        {
    //            if (items.Selected == true)
    //            {
    //                //SessionNo += items.Value + '$';
    //                SessionNo += items.Value + ',';
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(SessionNo))
    //        {
    //            SessionNo = SessionNo.Remove(SessionNo.Length - 1);
    //            ViewState["SessionNo"] = SessionNo;
    //        }

    //        //SessionNo = SessionNo.Remove(SessionNo.Length - 1);
    //        //ViewState["SessionNo"] = SessionNo;

    //        foreach (ListItem items in ddlSession.Items)
    //        {
    //            if (items.Selected == true)
    //            {
    //                SessionNo_New += items.Value + ',';
    //            }
    //        }

    //        if (!string.IsNullOrEmpty(SessionNo_New))
    //        {
    //            SessionNo_New = SessionNo_New.Remove(SessionNo_New.Length - 1);
    //            ViewState["SessionNo"] = SessionNo_New;
    //            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
    //            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO IN (SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID = " + ddlSession.SelectedValue + ")", "S.SEMESTERNO");
    //            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO IN (" + SessionNo_New + ")", "S.SEMESTERNO");
    //            ddlSemester.Focus();
    //        }
    //    }
    //    else
    //    {
    //        ddlSemester.Items.Clear();
    //        ddlSemester.Items.Add("Please Select");
    //        ddlSemester.SelectedItem.Value = "0";
    //    }

    //    //ddlSchool.Items.Clear();
    //    //ddlSchool.Items.Add(new ListItem("Please Select", "0"));

    //    //ddlScheme.Items.Clear();
    //    //ddlScheme.Items.Add(new ListItem("Please Select", "0"));

    //    //   ddlSemester.Items.Clear();
    //    // ddlSemester.Items.Add(new ListItem("Please Select", "0"));

    //    //ddlTest.Items.Clear();
    //    //ddlTest.Items.Add(new ListItem("Please Select", "0"));

    //    //ddlSubExam.Items.Clear();
    //    //ddlSubExam.Items.Add(new ListItem("Please Select", "0"));


    //    //ddlSchool.SelectedIndex = 0;
    //    //ddlScheme.SelectedIndex = 0;
    //    //// ddlSemester.SelectedIndex = 0;
    //    //ddlTest.SelectedIndex = 0;
    //    ////  divSubExam.Visible = false;

    //    //ddlSemester.Focus();

    //    //ddlSemester.Items.Clear();
    //    //ddlSemester.Items.Add("Please Select");
    //    //ddlSemester.SelectedItem.Value = "0";

    //    ddlSubType.Items.Clear();
    //    ddlSubType.Items.Add("Please Select");
    //    ddlSubType.SelectedItem.Value = "0";

    //    ddlPattern.Items.Clear();
    //    ddlPattern.Items.Add("Please Select");
    //    ddlPattern.SelectedItem.Value = "0";

    //    ddlTest.Items.Clear();
    //    ddlTest.Items.Add("Please Select");
    //    ddlTest.SelectedItem.Value = "0";

    //    ddlSubExam.Items.Clear();
    //    ddlSubExam.Items.Add("Please Select");
    //    ddlSubExam.SelectedItem.Value = "0";

    //    //}

    //    //*if (ddlSession.SelectedIndex > 0)
    //    //{

    //    //    this.PopulateDropDown();
    //    //    ddlSession.Focus();

    //    //    //if (Session["usertype"].ToString() == "8")
    //    //    //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC INNER JOIN USER_ACC AC ON(SC.DEPTNO=AC.UA_DEPTNO)", "SC.SCHEMENO", "SC.SCHEMENAME", "AC.UA_TYPE=8 AND SC.SCHEMENO > 0 AND UA_NAME= " +"'" +Session["username"].ToString()+"'", "SC.SCHEMENAME ASC");
    //    //    //else
    //    //    //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0", "SCHEMENAME ASC"); 
    //    //*}


    //}


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSession.SelectedIndex > 0)
        if (ddlDegree.SelectedIndex == -1)
        {
            String SessionNo = String.Empty;
            String CollageIDs = String.Empty;
            String SessionNo_New = String.Empty;
            ViewState["SessionNo"] = null;

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO = SR.SESSIONNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SM.SESSIONID IN (" + ddlSessionID.SelectedValue + ")", "S.SEMESTERNO");
            ddlSemester.Focus();

            //if (!string.IsNullOrEmpty(SessionNo_New))
            //{
            //    SessionNo_New = SessionNo_New.Remove(SessionNo_New.Length - 1);
            //    ViewState["SessionNo"] = SessionNo_New;
            //    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO IN (" + SessionNo_New + ")", "S.SEMESTERNO");
            //    ddlSemester.Focus();
            //}
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add("Please Select");
            ddlSemester.SelectedItem.Value = "0";
        }



        ddlSubType.Items.Clear();
        ddlSubType.Items.Add("Please Select");
        ddlSubType.SelectedItem.Value = "0";

        ddlPattern.Items.Clear();
        ddlPattern.Items.Add("Please Select");
        ddlPattern.SelectedItem.Value = "0";

        ddlTest.Items.Clear();
        ddlTest.Items.Add("Please Select");
        ddlTest.SelectedItem.Value = "0";

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add("Please Select");
        ddlSubExam.SelectedItem.Value = "0";



    }


    //protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlTest.SelectedValue == "5" || ddlTest.SelectedValue == "6")
    //    //{
    //    //    trStud.Visible = true;
    //    //}
    //    //else
    //    //{
    //    //    trStud.Visible = false;
    //    //}

    //}
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {


        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        ddlTest.Items.Clear();
        ddlTest.Items.Add(new ListItem("Please Select", "0"));

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add(new ListItem("Please Select", "0"));

        divSubExam.Visible = false;
        ddlSemester.SelectedIndex = 0;
        ddlTest.SelectedIndex = 0;


        if (ddlScheme.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON SR.SEMESTERNO = S.SEMESTERNO", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SEMESTERNO > 0" + "AND SESSIONNO=" + ddlSession.SelectedValue, "SR.SEMESTERNO");
            ddlScheme.Focus();

        }

        // ddlSemester.SelectedIndex = 0;
        // ddlTest.SelectedIndex = 0;    
        // divSubExam.Visible = false;
    }

    //protected void btnReportlock_Click(object sender, EventArgs e)
    //{

    //    if (ddlTest.SelectedValue == "0" || ddlTest.SelectedValue == "1" || ddlTest.SelectedValue == "2" || ddlTest.SelectedValue == "3" || ddlTest.SelectedValue == "4")
    //    {
    //        try
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=REPORT";
    //            url += "&path=~,Reports,Academic," + "rptMarkEntryLockNotDone.rpt";
    //            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

    //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //            divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //            divMsg.InnerHtml += " </script>";

    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");

    //            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "LockEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }
    //    else
    //    {

    //        if (rblStud.SelectedValue == "0")
    //        {
    //            try
    //            {
    //                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //                url += "Reports/CommonReport.aspx?";
    //                url += "pagetitle=REPORT";
    //                url += "&path=~,Reports,Academic," + "rptMarkEntryLockNotDone.rpt";
    //                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

    //                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //                divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //                divMsg.InnerHtml += " </script>";

    //                System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //                sb.Append(@"window.open('" + url + "','','" + features + "');");

    //                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);

    //            }
    //            catch (Exception ex)
    //            {
    //                if (Convert.ToBoolean(Session["error"]) == true)
    //                    objUCommon.ShowError(Page, "LockEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //                else
    //                    objUCommon.ShowError(Page, "Server Unavailable.");
    //            }
    //        }
    //        else
    //        {
    //            try
    //            {
    //                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //                url += "Reports/CommonReport.aspx?";
    //                url += "pagetitle=REPORT";
    //                url += "&path=~,Reports,Academic," + "rptMarkEntryLockNotDoneForBacklog.rpt";
    //                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;

    //                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //                divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //                divMsg.InnerHtml += " </script>";
    //            }
    //            catch (Exception ex)
    //            {
    //                if (Convert.ToBoolean(Session["error"]) == true)
    //                    objUCommon.ShowError(Page, "LockEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //                else
    //                    objUCommon.ShowError(Page, "Server Unavailable.");
    //            }
    //        }
    //    }
    //}

    protected void btnReportlock_Click(object sender, EventArgs e)
    {
        //modified on 15-02-2020 by Vaishali
        if (divSubExam.Visible == true)
            rfvSubExam.Enabled = true;
        else
            rfvSubExam.Enabled = false;
        try
        {
            DataSet ds = null;
            // ADDED BY SHUBHAM
            string CollageID = GetCollageID();
            string CollageIDs = CollageID.Remove(CollageID.Length - 1);
            //            ViewState["SessionNo"] = SessionNo;
            ViewState["SessionNo"] = objCommon.LookUp("ACD_SESSION_MASTER", "STRING_AGG(SESSIONNO,'$')", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + "AND COLLEGE_ID IN (" + CollageIDs + ")");

            // DataSet ds = objMarksEntry.GetLockMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue, divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);
            //DataSet ds = objMarksEntry.GetLockMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue.Split('-')[1].ToString(), divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);

            //DataSet ds = objMarksEntry.GetLockMarkEntryNotDoneRecordReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(ddlSemester.SelectedValue), ddlTest.SelectedValue.Split('-')[1].ToString(), divSubExam.Visible == true ? ddlSubExam.SelectedValue.Trim() : string.Empty);

            string sp_procedure = "PKG_ACAD_TEST_LOCK_ENTRY_NOTDONE";
            string sp_parameters = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_SEMESTERNO,@P_EXAMNO,@P_SUBEXAMTYPE,@P_SUBJECTTYPE";
            string sp_callValues = "" + ViewState["SessionNo"] + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + "," + ddlSubExam.SelectedValue + "," + ddlSubType.SelectedValue + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(updpnlExam, "Record Not Found !!!!", this.Page);
                return;
            }
            else
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=REPORT";
                url += "&path=~,Reports,Academic," + "rptMarkEntryLockNotDone.rpt";

                if (!CollageIDs.Contains(","))
                {
                    url += "&param=@P_SESSIONNO=" + ViewState["SessionNo"] + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(CollageIDs) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SUBEXAMTYPE=" + (divSubExam.Visible == true ? ddlSubExam.SelectedValue : string.Empty) + ",@P_SUBJECTTYPE=" + ddlSubType.SelectedValue;  // modified on 15-02-2020 by Vaishali
                }
                else 
                {
                    url += "&param=@P_SESSIONNO=" + ViewState["SessionNo"] + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_EXAMNO=" + ddlTest.SelectedValue.Split('-')[1].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SUBEXAMTYPE=" + (divSubExam.Visible == true ? ddlSubExam.SelectedValue : string.Empty) + ",@P_SUBJECTTYPE=" + ddlSubType.SelectedValue;  // modified on 15-02-2020 by Vaishali
                }


                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                //sb.Append(@"window.open('" + url + "','','" + features + "');");

                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "controlJSScript", sb.ToString(), true);

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnMarkStatus_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            // ADDED BY SHUBHAM
            string CollageID = GetCollageID();
            string CollageIDs = CollageID.Remove(CollageID.Length - 1);
            //            ViewState["SessionNo"] = SessionNo;
            ViewState["SessionNo"] = objCommon.LookUp("ACD_SESSION_MASTER", "STRING_AGG(SESSIONNO,'$')", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + "AND COLLEGE_ID IN (" + CollageIDs + ")");

            //int Sessionno = (ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0);
            //string Examno = ddlExam.SelectedValue;
            GridView GV = new GridView();
            //DataSet ds = objMarksEntry.GetMarkEntryStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));
            //DataSet ds = objMarksEntry.GetMarkEntryStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(0), Convert.ToInt32(ddlSemester.SelectedValue));
            //DataSet ds = objMarksEntry.GetMarkEntryStatus(ViewState["SessionNo"].ToString(), Convert.ToInt32(0), Convert.ToInt32(ddlSemester.SelectedValue));

            string sp_procedure = "PKG_ACD_TESTMARK_STATUS";
            string sp_parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO";
            string sp_callValues = "" + ViewState["SessionNo"] + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds.Tables.Count > 0 && ds.Tables != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string Attachment = "Attachment ; filename=AllExamMarkEntryStatus" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", Attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.HeaderStyle.Font.Bold = true;
                    GV.HeaderStyle.Font.Name = "Times New Roman";
                    GV.RowStyle.Font.Name = "Times New Roman";
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("No information found based on given criteria.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage("No information found based on given criteria.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTest.SelectedIndex > 0)
        {
            divSubExam.Visible = true;
            //objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "SE.SUBEXAMNAME<>' ' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND ED.FLDNAME = '" + ddlTest.SelectedValue + "'", "SE.FLDNAME");

            //objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ")", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND ED.FLDNAME = '" + ddlTest.SelectedValue.Split('-')[0].ToString() + "'", "SE.FLDNAME");

            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) // Added By Sagar Mankar On Date 05072023 For CPUKOTA/CPUHamirpur
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ")", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = '" + ddlTest.SelectedValue.Split('-')[1].ToString() + "'", "SE.SUBEXAMNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ")", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = '" + ddlTest.SelectedValue.Split('-')[1].ToString() + "'", "SE.FLDNAME");
            }

            //objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ")", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND SE.EXAMNO = " + ddlTest.SelectedValue + "", "SE.FLDNAME");


            ddlSubExam.Focus();
        }
        else
        {
            ddlTest.Focus();
            ddlSubExam.SelectedIndex = 0;

            ddlSubExam.Items.Clear();
            ddlSubExam.Items.Add("Please Select");
            ddlSubExam.SelectedItem.Value = "0";
        }
        //*ddlSubExam.Items.Clear();
        //ddlSubExam.Items.Add(new ListItem("Please Select", "0"));

        //divSubExam.Visible = false;
        //ddlSubExam.SelectedIndex = 0;

        //if (ddlTest.SelectedValue == "S1")
        ////|| ddlTest.SelectedValue == "S7")
        //{
        //    divSubExam.Visible = true;
        //    objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "SE.SUBEXAMNAME<>' ' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND ED.FLDNAME = '" + ddlTest.SelectedValue + "' AND SE.SUBEXAMNO NOT IN(5)", "SE.FLDNAME");
        //    ddlTest.Focus();
        //}
        //else if (ddlTest.SelectedValue == "S3")
        //{
        //    divSubExam.Visible = true;
        //    objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "SE.SUBEXAMNAME<>' ' AND S.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + "  AND ED.FLDNAME = '" + ddlTest.SelectedValue + "' AND SE.SUBEXAMNO NOT IN(5)", "SE.FLDNAME");
        //    ddlTest.Focus();

        //}

        //else
        //  *  divSubExam.Visible = false;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT SR ON SR.SUBID=S.SUBID ", "DISTINCT S.SUBID", "S.SUBNAME", "S.SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SESSIONNO IN (SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND SEMESTERNO=" + ddlSemester.SelectedValue + " ", "SUBID ");
            //objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT SR ON SR.SUBID=S.SUBID ", "DISTINCT S.SUBID", "S.SUBNAME", "S.SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SESSIONNO IN (" + ddlSession.SelectedValue + ") AND SEMESTERNO=" + ddlSemester.SelectedValue + " ", "SUBID ");
            // objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT SR ON SR.SUBID=S.SUBID ", "DISTINCT S.SUBID", "S.SUBNAME", "S.SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SESSIONNO IN (" + ViewState["SessionNo"] + ") AND SEMESTERNO=" + ddlSemester.SelectedValue + " ", "SUBID ");
            objCommon.FillDropDownList(ddlSubType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT SR ON SR.SUBID=S.SUBID INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONNO = SR.SESSIONNO)", "DISTINCT S.SUBID", "S.SUBNAME", "S.SUBID > 0 AND ISNULL(ACTIVESTATUS,0)=1 AND SM.SESSIONID IN (" + ddlSessionID.SelectedValue + ") AND SEMESTERNO=" + ddlSemester.SelectedValue + " ", "SUBID ");
            ddlSubType.Focus();
        }
        else
        {
            ddlSubType.Items.Clear();
            ddlSubType.Items.Add("Please Select");
            ddlSubType.SelectedItem.Value = "0";
        }

        ddlPattern.Items.Clear();
        ddlPattern.Items.Add("Please Select");
        ddlPattern.SelectedItem.Value = "0";

        ddlTest.Items.Clear();
        ddlTest.Items.Add("Please Select");
        ddlTest.SelectedItem.Value = "0";

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add("Please Select");
        ddlSubExam.SelectedItem.Value = "0";
        //}

        // divSubExam.Visible = false;

        //*if (ddlSemester.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlTest, " ACD_EXAM_NAME EX INNER JOIN  ACD_SCHEME SC ON(SC.PATTERNNO=EX.PATTERNNO) ", " FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !='' AND  SC.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "EXAMNO");
        //    ddlSemester.Focus();
        //}
        //*ddlTest.SelectedIndex = 0;
        // divSubExam.Visible = false;

        ddlSubType.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        ddlTest.Items.Clear();
        ddlTest.Items.Add(new ListItem("Please Select", "0"));

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add(new ListItem("Please Select", "0"));

        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlTest.SelectedIndex = 0;
        // divSubExam.Visible = false;
        ddlSession.SelectedIndex = 0;
        ddlSubExam.SelectedIndex = 0;





        if (ddlSchool.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddlScheme, "ACD_STUDENT  M INNER JOIN ACD_SCHEME SC ON SC.DEGREENO = M.DEGREENO AND SC.BRANCHNO = M.BRANCHNO AND M.ADMBATCH = SC.ADMBATCH INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID = M.COLLEGE_ID", "DISTINCT M.SCHEMENO", "SCHEMENAME", "M.SCHEMENO <> 0 AND M.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "M.SCHEMENO");
            ddlSchool.Focus();
        }




    }
    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ddlSubExam.Focus();
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common objCommon = new Common();

        //if (ddlClgname.SelectedIndex > 0)
        //{
        //    //Common objCommon = new Common();
        //    DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
        //    //ViewState["degreeno"]

        //    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        //    {
        //        ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
        //        ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
        //        ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
        //        ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

        //        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

        //        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

        //    }
        //}

        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSubExam.SelectedIndex = 0;
        ddlTest.SelectedIndex = 0;


        //if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8") //Added By Nikhil V.Lambe on 24/02/2021 for page should be access by Admin and HOD.
        // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");



        // else
        //  Response.Redirect("~/notauthorized.aspx?page=MarksEntryNotDone.aspx");
        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 ", "DEGREENO");
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubType.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlTest, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ") ", " ED.FLDNAME", "ED.EXAMNAME", " ED.EXAMNAME<>'' AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "ED.EXAMNO");

            //objCommon.FillDropDownList(ddlTest, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ") ", " (ED.FLDNAME+'-'+ CAST(ED.EXAMNO AS VARCHAR(20)))FLDNAME ", "ED.EXAMNAME", " ED.EXAMNAME<>'' AND  S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "  group by ED.FLDNAME,ED.EXAMNO,ED.EXAMNAME", "ED.EXAMNO");
            //objCommon.FillDropDownList(ddlTest, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ") ", " (ED.FLDNAME+'-'+ CAST(ED.EXAMNO AS VARCHAR(20)))FLDNAME ", "ED.EXAMNAME", " ED.EXAMNAME<>'' group by ED.FLDNAME,ED.EXAMNO,ED.EXAMNAME", "ED.EXAMNO");
            objCommon.FillDropDownList(ddlPattern, "ACD_EXAM_PATTERN", "DISTINCT PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1", "PATTERN_NAME ASC ");
            ddlPattern.Focus();
        }
        else
        {
            ddlPattern.Items.Clear();
            ddlPattern.Items.Add("Please Select");
            ddlPattern.SelectedItem.Value = "0";
        }

        ddlSubType.Focus();

        ddlTest.Items.Clear();
        ddlTest.Items.Add("Please Select");
        ddlTest.SelectedItem.Value = "0";

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add("Please Select");
        ddlSubExam.SelectedItem.Value = "0";
        //}
    }
    protected void tbnStatus_Click(object sender, EventArgs e)
    {
        {
            DataSet ds = new DataSet();
            // ADDED BY SHUBHAM
            string CollageID = GetCollageID();
            string CollageIDs = CollageID.Remove(CollageID.Length - 1);
            //            ViewState["SessionNo"] = SessionNo;
            ViewState["SessionNo"] = objCommon.LookUp("ACD_SESSION_MASTER", "STRING_AGG(SESSIONNO,'$')", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + "AND COLLEGE_ID IN (" + CollageIDs + ")");

            GridView MArkEntrystatus = new GridView();
            // ResultProcessing objRes = new ResultProcessing();
            //objMarksEntry.GetLockMarkEntryNotDoneRecordReport

            //ds = objMarksEntry.MarkEntryStatus(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSession.SelectedValue));
            //ds = objMarksEntry.MarkEntryStatus(Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue));
            //string Sessionnos = this.GetSessionNew(); //Seprated by '$' 
            string sp_procedure = "PKG_ACAD_MARK_ENTRY_STATUS";
            string sp_parameters = "@P_SESSIONNO,@P_SCHEMENO";
            string sp_callValues = "" + ViewState["SessionNo"] + "," + Convert.ToInt32(0) + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MArkEntrystatus.DataSource = ds;
                MArkEntrystatus.DataBind();

                string attachment = "attachment; filename=MarkEntryDetailStatus.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                MArkEntrystatus.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found.", this.Page);
            }
        }
    }

    protected void ddlPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPattern.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlTest, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO AND SE.SUBEXAM_SUBID=" + ddlSubType.SelectedValue + ") INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", " (ED.FLDNAME+'-'+ CAST(ED.EXAMNO AS VARCHAR(20)))FLDNAME ", "ED.EXAMNAME", " ED.EXAMNAME<>'' AND EP.PATTERNNO='" + ddlPattern.SelectedValue + "' group by ED.FLDNAME,ED.EXAMNO,ED.EXAMNAME,EP.PATTERNNO", "ED.EXAMNO");
            ddlTest.Focus();
        }
        else
        {
            ddlTest.Items.Clear();
            ddlTest.Items.Add("Please Select");
            ddlTest.SelectedItem.Value = "0";
        }

        ddlSubExam.Items.Clear();
        ddlSubExam.Items.Add("Please Select");
        ddlSubExam.SelectedItem.Value = "0";
        //}
    }

    protected void btnAbsententryreport_Click(object sender, EventArgs e)
    {
        {
            DataSet ds = new DataSet();
            GridView MArkEntrystatus = new GridView();
            // ResultProcessing objRes = new ResultProcessing();
            //objMarksEntry.GetLockMarkEntryNotDoneRecordReport

            //ds = objMarksEntry.MarkEntryStatus(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSession.SelectedValue));
            //ds = objMarksEntry.MarkEntryStatus(Convert.ToInt32(0), Convert.ToInt32(ddlSession.SelectedValue));
            // ADDED BY SHUBHAM
            string CollageID = GetCollageID();
            string CollageIDs = CollageID.Remove(CollageID.Length - 1);
            //            ViewState["SessionNo"] = SessionNo;
            ViewState["SessionNo"] = objCommon.LookUp("ACD_SESSION_MASTER", "STRING_AGG(SESSIONNO,'$')", "SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + "AND COLLEGE_ID IN (" + CollageIDs + ")");

            //string sp_procedure = "PKG_ACAD_ABSENT_ENTRY_REPORT";
            //string sp_parameters = "@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_SEMESTERNO,@P_EXAMNO,@P_SUBEXAMTYPE,@SUBTYPE,@PATTERN";
            //// string sp_callValues = "'" + ViewState["SessionNo"].ToString() + "'," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + ",'" + ddlSubExam.SelectedValue + "'";
            //string sp_callValues = "" + Sessionnos + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + "," + ddlSubExam.SelectedValue + "," + ddlSubType.SelectedValue + "," + ddlPattern.SelectedValue + "";

            string sp_procedure = "PKG_EXM_ABSENT_ENTRY_REPORT_CRESCENT";
            string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO,@P_EXAMNO,@P_SUBEXAMTYPE,@P_SUBTYPE,@P_PATTERN";
            // string sp_callValues = "'" + ViewState["SessionNo"].ToString() + "'," + Convert.ToInt32(0) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + ",'" + ddlSubExam.SelectedValue + "'";
            //string sp_callValues = "" + Sessionnos + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + ddlTest.SelectedValue.Split('-')[1].ToString() + "," + ddlSubExam.SelectedValue + "," + ddlSubType.SelectedValue + "," + ddlPattern.SelectedValue + "";
            string sp_callValues = "" + ViewState["SessionNo"] + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 + "," + 0 + "," + ddlSubType.SelectedValue + "," + ddlPattern.SelectedValue + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            //if (ds.Tables[0].Rows.Count > 0)
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MArkEntrystatus.DataSource = ds;
                    MArkEntrystatus.DataBind();

                    string attachment = "attachment; filename=MarkEntryDetailStatus.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    MArkEntrystatus.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Record Not Found.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Record Not Found.", this.Page);
            }

        }
    }

    // ADDED BY SHUBHAM
    protected void ddlSessionID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSessionID.SelectedIndex > 0)
            {

                string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());

                //DataSet dsCollegeSession = objCommon.FillDropDown("ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SESSIONNO = SM.SESSIONNO)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue), "CM.COLLEGE_ID");
                DataSet dsCollegeSession = objCommon.FillDropDown("ACD_SESSION_MASTER SM INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID) INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SESSIONNO = SM.SESSIONNO)", "DISTINCT CM.COLLEGE_ID", "COLLEGE_NAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSessionID.SelectedValue) + " AND (CM.COLLEGE_ID IN (SELECT VALUE FROM DBO.SPLIT('" + college_IDs + "',',')) OR '" + college_IDs + "'='')", "CM.COLLEGE_ID");


                //DataSet dsCollegeSession = objCC.GetCollegeSession(0, college_IDs);

                ddlSession.Items.Clear();
                //ddlCollege.Items.Add("Please Select");
                ddlSession.DataSource = dsCollegeSession;
                ddlSession.DataValueField = "COLLEGE_ID";
                ddlSession.DataTextField = "COLLEGE_NAME";
                ddlSession.DataBind();
                // rdbReport.SelectedIndex = -1 ;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
        }
    }

    // ADDED BY SHUBHAM
    private string GetCollageID()
    {
        string CollageIDs = "";


        foreach (ListItem item in ddlSession.Items)
        {
            if (item.Selected == true)
            {
                CollageIDs += item.Value + ',';
            }

        }
        //if (!string.IsNullOrEmpty(DegreeNos))
        //{
        //    objConfig.DegreeNoS = DegreeNos.Substring(0, DegreeNos.Length - 1);
        //}
        return CollageIDs;
    }

}
