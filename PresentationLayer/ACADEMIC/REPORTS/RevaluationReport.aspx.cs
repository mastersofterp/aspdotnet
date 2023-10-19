//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackReport.aspx                                               
// CREATION DATE : 04-06-2012                                                   
// CREATED BY    : Pawan Mourya                               
// MODIFIED BY   : Sachin A
// MODIFIED DESC : Added New Report and changes in procedure
//=================================================================================
using System;
using System.Web.UI;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.IO;

public partial class ACADEMIC_RevaluationReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //fill dropdown
                FillDropDownList();
                PopulateSessionDropDown();
                this.ReportStatus();

                //to clear all controls
                AllClear();
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

    }

    #region FillDropDownList
    //function to fill all dropdown
    private void FillDropDownList()
    {
        if (Convert.ToInt32(Session["OrgId"]) != 2 || Convert.ToInt32(Session["OrgId"]) != 6)
        {
            if (Session["usertype"].ToString().Equals("1"))
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            }
            else
            {
                objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            }
        }
        else
        {
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1", "SESSIONNO DESC");
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + " AND COLLEGE_IDS LIKE '%" + collegeid + "%'" + " AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() , "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0  AND ISNULL(IS_ACTIVE,0) = 1 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'" + " AND COLLEGE_IDS LIKE '%" + Session["college_nos"] + "%'" + " AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString(), "SESSIONNO DESC");
        }
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO>0", "SECTIONNO");
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

    }
    #endregion

    #region CheckPageAuthorization
    //function to check page is authorized or not
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RevaluationReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RevaluationReport.aspx");
        }
    }
    #endregion

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT_HIST R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "R.SESSIONNO=" + ddlSession.SelectedValue + "AND S.SEMESTERNO > 0", "R.SEMESTERNO");
                ddlDegree.Focus();
                ddlDegree.SelectedValue = "0";
                ddlBranch.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                ddlSection.SelectedValue = "0";
                ddlAdmBatch.SelectedValue = "0";
                ddlRevalType.SelectedValue = "0";
            }
            else
            {
                ddlDegree.SelectedValue = "0";
                ddlBranch.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                ddlSection.SelectedValue = "0";
                ddlAdmBatch.SelectedValue = "0";
                ddlRevalType.SelectedValue = "0";
            }
        }
        catch { }
    }

    //load branches according to selecetd degree 
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() == "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B on (A.BRANCHNO = B.BRANCHNO)", " distinct B.BRANCHNO", "LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
                }
                else
                {
                    //objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B on (A.BRANCHNO = B.BRANCHNO)", " distinct B.BRANCHNO", "LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + Convert.ToInt32(Session["userdeptno"]), "B.BRANCHNO");
                    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B on (A.BRANCHNO = B.BRANCHNO)", " distinct B.BRANCHNO", "LONGNAME", "A.DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
                }
                ddlBranch.Focus();
                ddlBranch.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                ddlSection.SelectedValue = "0";
                ddlAdmBatch.SelectedValue = "0";
                ddlRevalType.SelectedValue = "0";
            }
            else
            {
                ddlBranch.SelectedValue = "0";
                ddlSemester.SelectedValue = "0";
                ddlSection.SelectedValue = "0";
                ddlAdmBatch.SelectedValue = "0";
                ddlRevalType.SelectedValue = "0";

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RevaluationReport.ddlDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string session = GetSessionns();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT_HIST R ON (S.SEMESTERNO=R.SEMESTERNO)", "DISTINCT R.SEMESTERNO", "S.SEMESTERNAME", "R.SESSIONNO=" + session + "AND S.SEMESTERNO > 0", "R.SEMESTERNO");
            ddlSemester.Focus();
            ddlSemester.SelectedValue = "0";
            ddlSection.SelectedValue = "0";
            ddlAdmBatch.SelectedValue = "0";
            ddlRevalType.SelectedValue = "0";
        }
        catch { }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.Focus();
        ddlSection.SelectedValue = "0";
        ddlAdmBatch.SelectedValue = "0";
        ddlRevalType.SelectedValue = "0";
    }

    //load exam names according to scheme
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlAdmBatch.Focus();
            ddlAdmBatch.SelectedValue = "0";
            ddlRevalType.SelectedValue = "0";
        }
        catch { }
    }


    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlRevalType.Focus();
            ddlRevalType.SelectedValue = "0";
        }
        catch { }
    }

    //to clear all controls
    private void AllClear()
    {
        ddlSession.SelectedValue = "0";
        ddlDegree.SelectedValue = "0";
        ddlBranch.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
        ddlSection.SelectedValue = "0";
        ddlRevalType.SelectedValue = "0";
        ddlAdmBatch.SelectedValue = "0";
    }
    //to cancel report
    protected void btnCancelReport_Click(object sender, EventArgs e)
    {
        AllClear();
    }


    protected void btnStudentWisePhotoReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Student_Wise_PhotoCopy_Details", "rptStudentWisePhotoCopyDetailsCrescent.rpt", param);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Student_Wise_PhotoCopy_Details", "rptStudentWisePhotoCopyDetails_Rcpiper.rpt", param);
            }
            else
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Student_Wise_PhotoCopy_Details", "rptStudentWisePhotoCopyDetails.rpt", param);
            }
        }
        catch { }
    }
    protected void btnSubjectWisePhotoReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 2)
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Subject_Wise_PhotoCopy_Details", "rptSubjectWisePhotoCopyDetailsCrescent.rpt", param);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Subject_Wise_PhotoCopy_Details", "rptSubjectWisePhotoCopyDetailsRcpiper.rpt", param);
            }
            else
            {
                string sessionns = GetSessionnsNew();
                string param = "@P_SESSIONNO=" + sessionns + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_REVAL_TYPE=" + Convert.ToInt32(ddlRevalType.SelectedValue) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue);
                ShowReport("Subject_Wise_PhotoCopy_Details", "rptSubjectWisePhotoCopyDetails.rpt", param);
            }
        }
        catch { }
    }


    //function to show report
    private void ShowReport(string reportTitle, string rptFileName, string param)
    {
        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //url += "Reports/CommonReport.aspx?";
        //url += "pagetitle=" + reportTitle;
        //url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','Student_FeedBack','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        //////To open new window from Updatepanel
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //sb.Append(@"window.open('" + url + "','','" + features + "');");

        //ScriptManager.RegisterClientScriptBlock(this.updProg, this.updProg.GetType(), "controlJSScript", sb.ToString(), true);



        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        if (Convert.ToInt32(Session["OrgId"]) == 9)
        {
            url += "&param=" + param + ",@P_COLLEGE_CODE=" + ddlCollege.SelectedValue;               //Session["colcode"].ToString();          
        }
        if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        }
        else
        {
            url += "&param=" + param + ",@P_COLLEGE_CODE=" + ddlCollege.SelectedValue;
        }
        //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(updReval, updReval.GetType(), "controlJSScript", sb.ToString(), true);

    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (ddlCollege.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO DESC");
        //    ddlSession.Focus();
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Please select College/School Name.", this.Page);
        //}
    }

    #region PopulateSessionDropDown
    private void PopulateSessionDropDown()   //Added by Sachin A dt on 27022023 as per requirement
    {
        try
        {
            //Fill Dropdown Session 
            string college_IDs = objCommon.LookUp("User_Acc", "UA_COLLEGE_NOS", "UA_NO=" + Session["userno"].ToString());
            //DataSet dsCollegeSession = objCC.GetCollegeSession(7, college_IDs);
            string collegeids = college_IDs.Replace(",", "$");

            string sp_procedure = "PKG_ACD_GET_COLLEGESESSION";
            string sp_parameters = "@P_MODE,@P_COLLEGE_IDNOS";
            string sp_callValues = "" + 1 + "," + collegeids + "";
            DataSet dsCollegeSession = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            ddlCollegeSession.Items.Clear();
            //ddlCollege.Items.Add("Please Select");
            ddlCollegeSession.DataSource = dsCollegeSession;
            ddlCollegeSession.DataValueField = "SESSIONNO";
            ddlCollegeSession.DataTextField = "COLLEGE_SESSION";
            ddlCollegeSession.DataBind();
            //  rdbReport.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region btnExcelRegStud_Click
    protected void btnExcelRegStud_Click(object sender, EventArgs e)
    {
        string sessionno = GetSessionns();
        string sp_procedure = "PKG_PHOTO_SP_COPYDETAILS_EXCEL";
        string sp_parameters = "@P_SESSIONNO,@P_DEGREENO,@P_BRANCHNO,@P_SEMESTERNO,@P_SECTIONNO,@P_ADMBATCH,@P_REVAL_TYPE";
        string sp_callValues = "" + sessionno + "," + ddlDegree.SelectedValue + "," + ddlBranch.SelectedValue + "," + ddlSemester.SelectedValue + "," + ddlSection.SelectedValue + "," + ddlAdmBatch.SelectedValue + "," + ddlRevalType.SelectedValue + "";

        DataSet dsMarkchk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

        if (dsMarkchk != null && dsMarkchk.Tables[0].Rows.Count > 0)
        {
            string revaltype = string.Empty;
            if (ddlRevalType.SelectedValue == "1") { revaltype = "PhotoCopy"; } else { revaltype = "Revaluation"; }

            GridView GV = new GridView();
            using (XLWorkbook wb = new XLWorkbook())
            {
                GV.DataSource = dsMarkchk;
                GV.DataBind();

                string Attachment = "Attachment ; filename=" + revaltype + "_Report.xls";
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
                Response.Flush();
                Response.End();
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            return;
        }
    }
    #endregion

    #region btnSupplyReport_Click
    protected void btnSupplyReport_Click(object sender, EventArgs e)
    {
        try
        {
            string sessionno = GetSessionns();
            string sp_procedure = "PKG_SUPPLEMENTARY_EXAM_REG_SP_COPYDETAILS_EXCEL";
            string sp_parameters = "@P_SESSIONNO";
            string sp_callValues = "" + sessionno + "";

            DataSet dsMarkchk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (dsMarkchk != null && dsMarkchk.Tables[0].Rows.Count > 0)
            {
                GridView GV = new GridView();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    GV.DataSource = dsMarkchk;
                    GV.DataBind();

                    string Attachment = "Attachment ; filename= SupplementaryExamRegistrationReport.xls";
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
                    Response.Flush();
                    Response.End();
                }
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_RevaluationReport.btnSupplyReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        finally
        {
        }
    }
    #endregion

    #region ReportStatus
    protected void ReportStatus()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 2)
        {
            btnSupplyReport.Visible = true;
            btnReviewReport.Visible = false;
            btnApproved.Visible = false;
            btnRefund.Visible = false;
        }
        else
        {
            btnSupplyReport.Visible = false;
            btnReviewReport.Visible = false;
            btnApproved.Visible = false;
            btnRefund.Visible = false;
        }
    }
    public string GetSessionns()
    {
        try
        {
            string sessionno = string.Empty;
            for (int k = 0; k < ddlCollegeSession.Items.Count; k++)
            {
                if (ddlCollegeSession.Items[k].Selected == true)
                {
                    sessionno += ddlCollegeSession.Items[k].Value + "$";
                }
            }

            string sessionns = string.Empty;
            if (sessionno.Equals(""))
            {
                sessionns = "0";
            }
            else
            {
                sessionns = sessionno.TrimEnd('$');
            }
            return sessionns;
        }
        catch { return null; }
    }
    #endregion

    #region Notinuse
    protected void btnReviewReport_Click(object sender, EventArgs e)
    {
        try
        {
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue;
            ShowReviewReport("Review Report", "rptReviewRegStudeReport.rpt", param);
        }
        catch { }
    }
    //function to show report
    private void ShowReviewReport(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(updReval, updReval.GetType(), "controlJSScript", sb.ToString(), true);


    }
    private void ShowReviewApproved(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(updReval, updReval.GetType(), "controlJSScript", sb.ToString(), true);


    }
    private void ShowReviewRefund(string reportTitle, string rptFileName, string param)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=" + param + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
        //@P_CHALLAN_TYPE = 1 --- for photo copy and  2 --for reval
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(updReval, updReval.GetType(), "controlJSScript", sb.ToString(), true);


    }
    protected void btnApproved_Click(object sender, EventArgs e)
    {
        try
        {
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue;          // +",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            ShowReviewApproved("Review Report", "rptReviewApproval.rpt", param);
        }
        catch { }
    }
    protected void btnRefund_Click(object sender, EventArgs e)
    {
        try
        {
            string param = "@P_SESSIONNO=" + ddlSession.SelectedValue;//+ ",@P_BRANCHNO=" + ddlBranch.SelectedValue;
            ShowReviewRefund("Review Report", "rptReviewRefund.rpt", param);
        }
        catch { }
    }
    public string GetSessionnsNew()
    {
        try
        {
            int count = 0;
            string sessionno = string.Empty;
            for (int k = 0; k < ddlCollegeSession.Items.Count; k++)
            {
                if (ddlCollegeSession.Items[k].Selected == true)
                {
                    sessionno += ddlCollegeSession.Items[k].Value + "$";
                    count++;
                }
            }
            int cnt = ddlCollegeSession.Items.Count == null ? 0 : ddlCollegeSession.Items.Count;

            string sessionns = string.Empty;
            if (sessionno.Equals(""))
            {
                sessionns = "0";
            }
            else if (cnt == count)
            {
                sessionns = "0";
            }
            else
            {
                sessionns = sessionno.TrimEnd('$');
            }
            return sessionns;
        }
        catch { return null; }
    }
    #endregion
}
