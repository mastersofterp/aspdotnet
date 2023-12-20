using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ANSWERSHEET_AnswerSheetIssue : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AnswerSheetController objAnsSheetController = new AnswerSheetController();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                        this.FillDropdown();
                    btnReport.Enabled = false;
                    btnShow.Enabled = false;
                    //add for edit validation by Shubham B
                    ViewState["action"] = "add";
                    //AjaxControlToolkit.CalendarExtender CalID = new AjaxControlToolkit.CalendarExtender();
                    //CalID1.StartDate = DateTime.Now;


                }
                divMsg.InnerHtml = string.Empty;
            }

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

            //this.BindListView();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve_Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AnswersheetIssuer.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AnswersheetIssuer.aspx");
        }
    }
    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1 ", "EXAMNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_AnswersheetIssuer.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListView()
    {
        try
        {
            AnswerSheet objAnsSheet = new AnswerSheet();
            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.CourseNo = Convert.ToInt32(ddlCourses.SelectedValue);
            objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);

            DataSet ds = objAnsSheetController.GetAnswerSheetCoursesIssuer(objAnsSheet);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblTotRecv.Text = ds.Tables[0].Rows[0]["TOT_ANS_RECD"].ToString();
                    lblTotBal.Text = ds.Tables[0].Rows[0]["REMAINING"].ToString();
                    lblRecdId.Text = ds.Tables[0].Rows[0]["RECDID"].ToString();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        //pnlDays.Visible = true;
                        lvStudentsIssuer.DataSource = ds.Tables[1];
                        lvStudentsIssuer.DataBind();
                        lvStudentsIssuer.Visible = true;
                        ddlSession.Enabled = false;
                        ddlDegree.Enabled = false;
                        ddlBranch.Enabled = false;
                        ddlScheme.Enabled = false;
                        // ddlSem.Enabled = false;
                        // ddlExam.Enabled = false;

                    }
                    else
                    {
                        lvStudentsIssuer.DataSource = null;
                        lvStudentsIssuer.DataBind();
                        lvStudentsIssuer.Visible = false;
                        btnReport.Enabled = false;

                    }

                }
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //pnlDays.Visible = true;
                lvStudentsIssuer.DataSource = ds.Tables[1];
                lvStudentsIssuer.DataBind();
                ddlSession.Enabled = false;
                ddlDegree.Enabled = false;
                ddlBranch.Enabled = false;
                ddlScheme.Enabled = false;
                // ddlSem.Enabled = false;
                btnReport.Enabled = true;
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AnswersheetIssuer.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        //ClearData();
        this.BindListView();
        tblFacdetai.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        try
        {
            int IssuerId;
            string recdname = string.Empty; ;
            string recd_date = string.Empty; ;
            AnswerSheet objAnsSheet = new AnswerSheet();
            if (ViewState["action"].ToString().Equals("edit"))
            {
                int Duplicate = Convert.ToInt32(objCommon.LookUp("ACD_ANS_ISSUE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND EXAM_STAFF_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "AND  COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + " AND RECEIVER_DATE != '' AND RECEIVER_NAME IS NOT NULL"));
                if (Duplicate > 0)
                {
                    objCommon.DisplayMessage(this.updSession, "Record Already Exists.", this.Page);
                    return;
                }
            }
            else 
            {
                int Duplicate = Convert.ToInt32(objCommon.LookUp("ACD_ANS_ISSUE WITH (NOLOCK)", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND EXAM_STAFF_NO=" + Convert.ToInt32(ddlFaculty.SelectedValue) + "AND  COURSENO=" + Convert.ToInt32(ddlCourses.SelectedValue) + ""));
                if (Duplicate > 0)
                {
                    objCommon.DisplayMessage(this.updSession, "Record Already Exists.", this.Page);
                    return;
                }
            }
           
            if (txtReceiveName.Text != null && (hdissuerid.Value) != string.Empty)
            {
                IssuerId = Convert.ToInt32(hdissuerid.Value);
                objAnsSheet.RecdName = txtReceiveName.Text;
                //objAnsSheet.Receiver_Date = Convert.ToDateTime(txtRecdDate.Text);
                objAnsSheet.Receiver_Date = (txtRecdDate.Text.Trim() != string.Empty) ? Convert.ToDateTime(txtRecdDate.Text) : DateTime.MinValue;
            }
            else
            {
                IssuerId = 0;
                objAnsSheet.RecdName = recdname;
                objAnsSheet.Receiver_Date = Convert.ToDateTime(null);
                // objAnsSheet.Receiver_Date = DBNull.Value;
            }
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.CourseNo = Convert.ToInt32(ddlCourses.SelectedValue);
            objAnsSheet.IssuerId = IssuerId;
            objAnsSheet.IssuerName = txtIssuerName.Text;
            objAnsSheet.RecdId = Convert.ToInt32(lblRecdId.Text);
            objAnsSheet.FacultyNo = Convert.ToInt32(ddlFaculty.SelectedValue);
            objAnsSheet.FacultyName = ddlFaculty.SelectedItem.Text;
            objAnsSheet.AnsSheetIssue = Convert.ToInt32(txtAnsIssue.Text);
            objAnsSheet.Balance = Convert.ToInt32(txtBalance.Text);
            objAnsSheet.Bundle = txtBundle.Text;
            objAnsSheet.Issuer_Date = Convert.ToDateTime(txtIssueDate.Text);
            //objAnsSheet.Receiver_Date = Convert.ToDateTime(txtRecdDate.Text);
            objAnsSheet.Remark = txtRemark.Text;
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);


            CustomStatus cs = (CustomStatus)objAnsSheetController.InsertAnswerSheetMarkIssuer(objAnsSheet);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updSession, "Record Saved Successfully.", this.Page);
                //add for edit validation by Shubham B
                RfvReceiveName.Enabled = false;
                rfvRecdDate.Enabled = false;
                ViewState["action"] = "add";
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Server Error...", this.Page);
            }
           // ClearData();
            BindListView();
            // txtRecdDate.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_AnswersheetIssuer.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int issuerid = int.Parse(btnEdit.CommandArgument);
            //btnCheckListReport.Visible = true;
            btnShow.Enabled = true;
            ReceiverName.Visible = true;
            ReceiveDT.Visible = true;
            lblReceiveDT.Visible = true;
            //ReceiveName.Visible = true;
            txtRecdDate.Visible = true;
            lblReceiveName.Visible = true;
            ViewState["action"] = "edit";
            try
            {
                DataSet ds = objAnsSheetController.ShowIssuerDetails(issuerid);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    hdissuerid.Value = ds.Tables[0].Rows[0]["ISSUERID"].ToString();
                    ddlFaculty.SelectedItem.Text = ds.Tables[0].Rows[0]["EVALUATOR"].ToString();
                    ddlFaculty.SelectedValue = ds.Tables[0].Rows[0]["EXAM_STAFF_NO"].ToString();
                    lblTotRecv.Text = ds.Tables[0].Rows[0]["TOT_ANS_RECD"].ToString();
                    txtAnsIssue.Text = ds.Tables[0].Rows[0]["QUANTITY"].ToString();
                    lblTotBal.Text = ds.Tables[0].Rows[0]["REMAINING"].ToString();
                    txtBundle.Text = ds.Tables[0].Rows[0]["BUNDLE_NO"].ToString();
                    txtIssuerName.Text = ds.Tables[0].Rows[0]["ISSUER_NAME"].ToString();
                    txtIssueDate.Text = ds.Tables[0].Rows[0]["ISSUER_DATE"].ToString();
                    txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                    txtRecdDate.Text = ds.Tables[0].Rows[0]["RECEIVER_DATE"].ToString();
                    txtReceiveName.Text = ds.Tables[0].Rows[0]["RECEIVER_NAME"].ToString();
                    txtBalance.Text = ds.Tables[0].Rows[0]["QUTBALANCE"].ToString();

                    //add for edit validation by Shubham B
                    RfvReceiveName.Enabled = true;
                    rfvRecdDate.Enabled = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updSession, "No Data Found...", this.Page);
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetIssuer.btnEdit-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ANSWERSHEET_AnswersheetIssuer.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETISSUER", "rptanswersheetIssuer.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_ExamNo=" + ddlExam.SelectedValue + ",@P_COURSENO=" + ddlCourses.SelectedValue + ",@P_EXAMYPE=" + ddlExamType.SelectedValue;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_ANSWERSHEET_AnswersheetIssuer.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearSelection()
    {
        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
        ddlBranch.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourses.SelectedIndex = 0;
        lblTotRecv.Text = string.Empty;
        lblTotRecv.Text = string.Empty;
        lblTotBal.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlSession.Enabled = true;
        ddlDegree.Enabled = true;
        ddlExam.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSem.Enabled = true;
        ddlCourses.Enabled = true;
        ddlFaculty.SelectedItem.Text = string.Empty;
        ddlClgname.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;

    }
    private void ClearData()
    {
        ddlFaculty.SelectedIndex = 0;
        txtAnsIssue.Text = string.Empty;
        txtBalance.Text = string.Empty;
        txtBundle.Text = string.Empty;
        txtIssueDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtRecdDate.Text = string.Empty;
        txtIssuerName.Text = string.Empty;
        txtReceiveName.Text = string.Empty;
        ddlFaculty.SelectedItem.Text = "";

    }
    protected void txtRecdDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dt1 = Convert.ToDateTime(txtIssueDate.Text);
        DateTime dt2 = Convert.ToDateTime(txtRecdDate.Text);
        if (dt2 < dt1)
        {
            //Response.Write("You cannot select a day earlier than textbox1!");
            objCommon.DisplayMessage(this.updSession, "You cannot select a day earlier than issue Date.", this.Page);
            txtRecdDate.Text = string.Empty;
        }


    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON S.SEMESTERNO=SR.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO=" + ViewState["schemeno"], "S.SEMESTERNO");
        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "DISTINCT EXAMNO", "EXAMNAME", "EXAMTYPE= 2 and PATTERNNO=1 and EXAMNAME<>''", "EXAMNO");
        //objCommon.FillDropDownList(ddlCourses, "ACD_ANS_RECD AR WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON AR.COURSENO=C.COURSENO", "DISTINCT C.COURSENO", "(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME", " AR.SESSIONNO = " + ddlSession.SelectedValue + " AND AR.SCHEMENO = " + ddlScheme.SelectedValue + "", "");

        // objCommon.FillDropDownList(ddlCourses, "ACD_ANS_RECD AR INNER JOIN ACD_COURSE C ON AR.COURSENO=C.COURSENO LEFT JOIN ACD_ANS_ISSUE AI ON AR.RECDID=AI.RECDID", "DISTINCT C.COURSENO", "(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME", " AR.SESSIONNO = " + ddlSession.SelectedValue + " AND AR.SCHEMENO = " + ddlScheme.SelectedValue + " AND AR.SEMESTERNO = " + ddlSem.SelectedValue + "AND AR.EXAMTYPE = " + ddlExamType.SelectedValue, "C.COURSENO");
        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
        ClearData();
        lblTotRecv.Text = string.Empty;
        lblTotBal.Text = string.Empty;
    }
    protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlCourses.SelectedValue) > 0)
        {
            // this.BindListView();
            //objCommon.FillDropDownList(ddlFaculty, "ACD_EXAM_STAFF A WITH (NOLOCK) INNER JOIN ACD_EXAM_EVALUATOR B WITH (NOLOCK) ON A.EXAM_STAFF_NO =B.EXAM_STAFF_NO", "distinct A.EXAM_STAFF_NO", "A.STAFF_NAME", "A.ACTIVE=1 and B.STATUS=0 AND APPROVE_STATUS=1 AND B.COURSENO=" + ddlCourses.SelectedValue + " AND B.SESSIONNO=" + ddlSession.SelectedValue + "AND B.EXAMTYPE=" + ddlExamType.SelectedValue, "A.EXAM_STAFF_NO");
            objCommon.FillDropDownList(ddlFaculty, "ACD_EXAM_STAFF A WITH (NOLOCK) INNER JOIN ACD_EXAM_EVALUATOR B WITH (NOLOCK) ON A.EXAM_STAFF_NO =B.EXAM_STAFF_NO", "distinct A.EXAM_STAFF_NO", "A.STAFF_NAME", "A.ACTIVE=1 and B.STATUS=0 AND APPROVE_STATUS=1 AND B.COURSENO=" + ddlCourses.SelectedValue + " AND B.SESSIONNO=" + ddlSession.SelectedValue, "A.EXAM_STAFF_NO");
            btnShow.Enabled = true;
            //tblFacdetai.Visible = true;
            //ClearData();
        }
        else
        {
            lblTotRecv.Text = string.Empty;
            lblTotBal.Text = string.Empty;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlDegree.SelectedIndex) > 0 && ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ViewState["branchno"] + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
            lvStudentsIssuer.DataSource = null;
            lvStudentsIssuer.DataBind();
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }

        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearSelection();
        ClearData();
        tblFacdetai.Visible = false;
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlClgname.SelectedIndex > 0)
        {

            Common objCommon = new Common();

            if (ddlClgname.SelectedIndex > 0)
            {
                //Common objCommon = new Common();
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                }
            }
        }

        else
        {
            ddlExam.Items.Clear();
            ddlExam.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));

        }

    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindListView();
        //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ViewState["schemeno"], "SE.SEMESTERNO");
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH(NOLOCK) INNER JOIN ACD_SEMESTER SE WITH(NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO=" + ddlSession.SelectedValue + "AND SR.SCHEMENO=" + ViewState["schemeno"], "SE.SEMESTERNO");
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlCourses, "ACD_ANS_RECD AR WITH (NOLOCK) INNER JOIN ACD_COURSE C WITH (NOLOCK) ON AR.COURSENO=C.COURSENO", "DISTINCT C.COURSENO", "(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME", " AR.SESSIONNO = " + ddlSession.SelectedValue + " AND AR.SCHEMENO = " + ddlScheme.SelectedValue + "", "");

        objCommon.FillDropDownList(ddlCourses, "ACD_ANS_RECD AR INNER JOIN ACD_COURSE C ON AR.COURSENO=C.COURSENO LEFT JOIN ACD_ANS_ISSUE AI ON AR.RECDID=AI.RECDID", "DISTINCT C.COURSENO", "(C.CCODE+'-'+ C.COURSE_NAME) AS COURSENAME", " AR.SESSIONNO = " + ddlSession.SelectedValue + " AND AR.SCHEMENO = " + ViewState["schemeno"] + " AND AR.SEMESTERNO = " + ddlSem.SelectedValue + "AND AR.EXAMTYPE = " + ddlExamType.SelectedValue, "C.COURSENO");
        lvStudentsIssuer.DataSource = null;
        lvStudentsIssuer.DataBind();
        ClearData();
        lblTotRecv.Text = string.Empty;
        lblTotBal.Text = string.Empty;
    }
}