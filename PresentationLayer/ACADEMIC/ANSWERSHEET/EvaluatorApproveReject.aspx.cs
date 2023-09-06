using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ANSWERSHEET_EvaluatorApproveReject : System.Web.UI.Page
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
                    //txtReportDate.Text = DateTime.Today.ToShortDateString();
                    btnReport.Enabled = false;

                }
            }
            divMsg.InnerHtml = string.Empty;
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=AnswersheetRecieve.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AnswersheetRecieve.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COL_SCHEME_NAME ASC");
           // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
           // objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1", "EXAMNO DESC");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetEvaluator.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ViewState["branchno"] + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ViewState["schemeno"], "SE.SEMESTERNO");
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
       PopulateCourses();
        //objCommon.FillDropDownList(ddlExamType, "acd_exam_name", "examno", "examname", "examno>0", "examno");
    }
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue, "SE.SEMESTERNO");
        //if (ddlExamType.SelectedValue == "1")
        //{
        //    PopulateCourses();

        //}
        //else
        //{
        //    RevaluationCourses();
        //}
        //txtStudCount.Text = string.Empty;
        //lvEvaluator.DataSource = null;
        //lvEvaluator.DataBind();

    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
       //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue, "SE.SEMESTERNO");
        if (ddlExamType.SelectedValue == "1")
        {
            PopulateCourses();
            
        }
        else
        {
            RevaluationCourses();
        }
        txtStudCount.Text = string.Empty;
        lvEvaluator.DataSource = null;
        lvEvaluator.DataBind();

    }

    private void PopulateCourses()
    {

        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE+'-'+COURSE_NAME", "SEMESTERNO =" + ddlSem.SelectedValue + " AND SUBID=1 AND SCHEMENO =" + ViewState["schemeno"] + " AND COURSENO  IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMNO=" + ddlExam.SelectedValue + ")", "COURSE_NAME ASC,COURSENO");
        // objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT COURSENO", "CCODE+'-'+COURSENAME", "SEMESTERNO =" + ddlSem.SelectedValue + "AND SCHEMENO =" + ViewState["schemeno"] + " AND COURSENO IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMNO=" + ddlExam.SelectedValue + ")", "COURSENO"); //SUBID IN (1,3)
       // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
    }

    private void RevaluationCourses()
    {

        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "CCODE+'-'+COURSE_NAME", "SEMESTERNO =" + ddlSem.SelectedValue + " AND SUBID IN (1,3) AND SCHEMENO =" + ViewState["schemeno"] + " AND COURSENO  IN (SELECT COURSENO FROM ACD_REVAL_RESULT WHERE SESSIONNO=" + ddlSession.SelectedValue + " AND REV_APPROVE_STAT=1 AND EXAMNO=" + ddlExam.SelectedValue + ")", "COURSENO");
        //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT COURSENO", "CCODE+'-'+COURSENAME", "SEMESTERNO =" + ddlSem.SelectedValue + "AND SCHEMENO =" + ViewState["schemeno"] + " AND COURSENO IN (SELECT COURSENO FROM ACD_EXAM_EVALUATOR WHERE STATUS=0 AND SESSIONNO=" + ddlSession.SelectedValue, "COURSENO");
    }

  


    private void BindListView()
    {
        try
        {

            int studCount;

            AnswerSheet objAnsSheet = new AnswerSheet();

            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAnsSheet.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);


            DataSet ds = objAnsSheetController.GetEvaluatorListForApproval(objAnsSheet);
            if (ddlExamType.SelectedValue == "1")
            {
                studCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(DISTINCT IDNO) ", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED=1 AND SCHEMENO =" + ViewState["schemeno"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            }
            else
            {
                studCount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT WITH (NOLOCK)", "COUNT(DISTINCT IDNO) ", "SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0  AND SCHEMENO =" + ViewState["schemeno"] + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            }
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvEvaluator.DataSource = ds;
                lvEvaluator.DataBind();
                lvEvaluator.Visible = true;
                ddlSession.Enabled = true;
                ddlDegree.Enabled = true;
                ddlBranch.Enabled = true;
                ddlScheme.Enabled = true;
                ddlExam.Enabled = true;
                ddlSem.Enabled = true;
                btnReport.Enabled = true;
                txtStudCount.Text = studCount.ToString();
                btnSubmit.Enabled = true;
            }
            else
            {
                lvEvaluator.DataSource = null;
                lvEvaluator.DataBind();
                lvEvaluator.Visible = false;
                objCommon.DisplayMessage(this.updSession, "Records not found.", this.Page);
                btnSubmit.Enabled = false;
            }

            //foreach (ListViewItem dataitem in lvEvaluator.Items)
            //{
            //    //(dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
            //    //CheckBox chkBox = dataitem.FindControl("chkIDNo") as CheckBox;
            //    //(dataitem.FindControl("ddlApproval") as DropDownList).SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString(); 
            //    DropDownList chkBox = dataitem.FindControl("ddlApproval") as DropDownList;
            //    int rt=Convert.ToInt32(chkBox.ToolTip);
            //    //Label lblstatus1 = (dataitem.FindControl("lblstatus") as Label);

            //    //int ok1 = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_EVALUATOR","APPROVE_STATUS" ,"EXAM_STAFF_NO="+StaffNo);
                
            //    for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
            //    {
            //        int r = rt;
            //        int lblStatus = Convert.ToInt32(chkBox.ToolTip);
            //        if (rt == 0 || rt == null)
            //        {
            //            chkBox.Enabled = true;
            //            chkBox.SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            //            //lblStatus.Text = "Please Select";
            //            //lblstatus1.ForeColor = System.Drawing.Color.Green;
            //            //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

            //        }

            //        if (rt == 1)
            //        {
            //            chkBox.Enabled = false;
            //            chkBox.SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            //            //lblStatus.ForeColor = System.Drawing.Color.Green;
            //            //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

            //        }
            //        else if (rt == 2)
            //        {
            //            chkBox.Enabled = false;
            //            chkBox.SelectedValue = ds.Tables[0].Rows[1]["APPROVE_STATUS"].ToString();
            //            //lblStatus.Text = "Reject";
            //            //lblStatus.ForeColor = System.Drawing.Color.Green;
            //            //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

            //        }
            //        else
            //        {
            //            chkBox.Enabled = true;
            //            chkBox.SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            //            //lblStatus.Text = "Please Select";
            //            //lblstatus1.ForeColor = System.Drawing.Color.Green;
            //            //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetEvaluator.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void lvEvaluator_ItemDataBound(object sender,RepeaterCommandEventArgs e)
    //{
        
    //    RepeaterItem dataitem =(RepeaterItem) e.Item;

    //    //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
    //    Label lblStat = dataitem.FindControl("lblstatus") as Label;
    //    //CheckBox ChkOffer = dataitem.FindControl("chkIDNo") as CheckBox;
    //    //ChkOffer.Checked = lblStat.ToolTip.Equals("1") ? true : false;
    //    DropDownList ChkOffer = dataitem.FindControl("ddlApproval") as DropDownList;
    //    ChkOffer.SelectedValue = lblStat.ToolTip;
    //}
    protected void btnEvaluatorReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updSession, "Please Select Session ", this.Page);
        }
        else
        {
            ShowReport("EvaaluatorReport", "rptInternalExternalEvaluatorApprove.rpt");
        }
    }
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETEVALUATOR", "rptanswersheetEvaluator.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_STATUS=" + 0 + ",@UserName=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            //",@P_ExamNo=" + ddlExam.SelectedValue + 
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    //private void ShowReport(string reportTitle, string rptFileName)
    //{
      
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_ExamNo=" + ddlExam.SelectedValue + ",@P_EXAM_STAFF_NO=" + 1 + ",@P_EXAM_STAFF_TYPE=" + 1;
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        //To open new window from Updatepanel
    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        //sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        //ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string status = "";
            int count = 0;
            int selectedEvaluator = 0;
           
            AnswerSheet objAnsSheet = new AnswerSheet();

            if (selectedEvaluator >= 0)
            {
                int ret = 0;
                string StaffNo = string.Empty;
                 
                foreach (ListViewItem dataitem in lvEvaluator.Items)
                {

                    DropDownList Result = dataitem.FindControl("ddlApproval") as DropDownList;
                    int ApproveStatus = Convert.ToInt32(Result.SelectedValue);
                    ViewState["ApproveStatus"] = ApproveStatus;
                    int EVALAPPID = Convert.ToInt32(Result.ToolTip);
                    


                    //status = status + (dataitem.FindControl("ddlApproval") as DropDownList).SelectedValue + "$";

                    //Label lblEvalName = dataitem.FindControl("lblEvalName") as Label;
                    //StaffNo = lblEvalName.ToolTip;




                    //int ret = 0;
                    //string StaffNo = string.Empty;


                    objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
                    objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
                    objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
                    objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
                    objAnsSheet.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                    objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);

                    {
                        ret = ret + objAnsSheetController.UpdateEvaluatorApproveStatus(objAnsSheet, ApproveStatus, EVALAPPID);
                    }


                    if (ret > 0)
                    {
                        objCommon.DisplayMessage(this.updSession, "Evaluator Status Updated Successfully.", this.Page);
                        //  ClearSelection();

                    }
                    else
                    {
                        objCommon.DisplayMessage("Error..!!", this.Page);
                    }
                   
                    //foreach (RepeaterItem dataitem in lvEvaluator.Items)
                    //{
                    //    //(dataitem.FindControl("chkIDNo") as CheckBox).Checked = false;
                    //    //CheckBox chkBox = dataitem.FindControl("chkIDNo") as CheckBox;
                    //    (dataitem.FindControl("ddlApproval") as DropDownList).SelectedValue = "0";
                    //    DropDownList chkBox = dataitem.FindControl("ddlApproval") as DropDownList;
                    //    string lblStatus = (dataitem.FindControl("lblstatus") as Label).ToolTip;
                    //    if (lblStatus == "1")
                    //    {
                    //        chkBox.Enabled = false;
                    //    }
                    //}
                }
                BindListView();
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Please select atleast one evaluator.", this.Page);
            }
            
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_EvaluatorApproveReject.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        } objUaimsCommon.ShowError(Page, "Server Unavailable.");
    }

    private void ClearFields()
    {
        ddlClgname.SelectedIndex = -1;
        ddlSession.SelectedIndex = -1;
        ddlBranch.SelectedIndex = -1;
        ddlDegree.SelectedIndex = -1;
        ddlExam.SelectedIndex = -1;
        ddlExamType.SelectedIndex = -1;
        ddlScheme.SelectedIndex = -1;
        ddlSem.SelectedIndex = -1;
        ddlCourse.SelectedIndex = -1;
        txtStudCount.Text = string.Empty;
        
    }



    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);

        Page.Response.Redirect(Page.Request.Url.ToString(), true);
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
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.Items.Clear();
            ddlExam.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            // ddlsemester.Items.Clear();
            // ddlsemester.Items.Add(new ListItem("Please Select", "0"));
            // ddlSubjectType.Items.Clear();
            // ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            // divstatus.Visible = false;
        }

        //Clear();




    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

        //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1  and examname <>''", "EXAMNO DESC");
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue, "SE.SEMESTERNO");
    
    
    }
    // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ddlScheme.SelectedValue, "SE.SEMESTERNO");



    //protected DataTable GetData()
    //{
    //    DataTable dt = new DataTable();

    //    DataColumn dc = new DataColumn("EVALUATOR_NAME", typeof(String));     
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("APPROVE_STATUS", typeof(String));
    //    dt.Columns.Add(dc);

        
    //    DataRow dr = dt.NewRow();
    //    dr[0] = "0";
    //    dr[1] = "1";
    //    //dr[2] = "2";


    //    dt.Rows.Add(dr);
    //    return dt;
    //}


    //protected void lvEvaluator_ItemDataBound1(object sender, RepeaterItemEventArgs e)
    //{
    //    #region commented

    //    //DropDownList ddlstatus = e.Item.FindControl("ddlApproval") as DropDownList;

    //     //Find the DropDownList in the Repeater Item.
    //   // DropDownList ddlCountries = (e.Item.FindControl("ddlApproval") as DropDownList);
    //   // ddlCountries.DataSource = this.GetData();
    //   // ddlCountries.DataTextField = "APPROVE_STATUS";
    //   // ddlCountries.DataValueField = "APPROVE_STATUS";
    //   // ddlCountries.DataBind();
 
    //   // //Add Default Item in the DropDownList.
    //   //ddlCountries.Items.Insert(0, new ListItem("Please select"));
 
    //   // //Select the Country of Customer in DropDownList.
    //   //string country = (e.Item.DataItem as DataRowView)["APPROVE_STATUS"].ToString();
    //   // ddlCountries.Items.FindByValue(country).Selected = true;



    //    //DropDownList ddlstatus = e.Item.FindControl("ddlstatus") as DropDownList;
    //    //DropDownList ddldrop = (DropDownList)e.Item.FindControl("ddlApproval");
    //    //int ddlApp = Convert.ToInt32(ddldrop.ToolTip);

    //    //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)                  //|| e.Item.ItemType != ListItemType.AlternatingItem
    //    //{
    //    //    //var ddlApp = (DropDownList)e.Item.FindControl("ddlApproval");

    //    //    if (ddlApp == 0)
    //    //    {
    //    //        //ddlApp = 0;
    //    //        ddldrop.SelectedIndex = 0;
    //    //    }
    //    //    else if (ddlApp == 1)
    //    //    {
    //    //        //ddlApp = 1;
    //    //        ddldrop.SelectedIndex = 1;
    //    //    }
    //    //    else if (ddlApp == 2)
    //    //    {
    //    //        //ddlApp = 2;
    //    //        ddldrop.SelectedIndex = 2;
    //    //    }
    //    //}
    //    #endregion


    //    int temp=0;
    //    DropDownList ddldroptemp = (DropDownList)e.Item.FindControl("ddlApproval");
    //    temp=Convert.ToInt32(ddldroptemp.SelectedValue);
    //    //if (ddldrop==null)
    //    //{
    //    //    ddldrop.SelectedValue = "0";
    //    //}

    //    AnswerSheet objAnsSheet = new AnswerSheet();

    //    objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
    //    objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
    //    objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
    //    objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
    //    objAnsSheet.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
    //    objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);


    //    DataSet ds = objAnsSheetController.GetEvaluatorListForApproval(objAnsSheet);

    //    if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //    {
    //        //DropDownList ddlstatus = e.Item.FindControl("ddlApproval") as DropDownList;

    //        //DropDownList ddldrop = (DropDownList)e.Item.FindControl("ddlApproval");
    //        //int ddlApp = Convert.ToInt32(ddldrop.ToolTip);


    //        //ddldrop.SelectedValue = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
    //        //if (ddldrop.SelectedValue == null)
    //        //{
    //        //    ddldrop.SelectedValue = "0";
    //        //}
    //        //if (ddldrop.SelectedValue == "0")
    //        //{
    //        //    ddlApp = 0;
    //        //    ddldrop.SelectedIndex = 0;
    //        //}
    //        //else if (ddldrop.SelectedValue == "1")
    //        //{
    //        //    ddlApp = 1;
    //        //    ddldrop.SelectedIndex = 1;
    //        //}
    //        //else if (ddldrop.SelectedValue == "2")
    //        //{
    //        //    ddlApp = 2;
    //        //    ddldrop.SelectedIndex = 2;
    //        //}

    //    }
    //    //DropDownList ddlstatus = e.Item.FindControl("ddlApproval") as DropDownList;

    //}
    protected void lvEvaluator_ItemDataBound(object sender, ListViewItemEventArgs e) 
    {
        //DropDownList ddlstatus = e.Item.FindControl("ddlstatus") as DropDownList;
        DropDownList ddlApproval = e.Item.FindControl("ddlApproval") as DropDownList;
        int rt = Convert.ToInt32(ddlApproval.ToolTip);
        int evalidd = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_EVALUATOR", "APPROVE_STATUS", "EVAL_APPID=" + Convert.ToInt32(ddlApproval.ToolTip)));

        //for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
                {
                    //int r = rt;
                    //int lblStatus = Convert.ToInt32(chkBox.ToolTip);
                    if (evalidd == 0)
                    {
                        ddlApproval.Enabled = true;
                        //chkBox.SelectedValue ="0";                //ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                        //lblStatus.Text = "Please Select";
                        //lblstatus1.ForeColor = System.Drawing.Color.Green;
                        //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

                    }

                    else if (evalidd == 1)
                    {
                        ddlApproval.Enabled = true;
                        ddlApproval.SelectedValue = "1";                     // ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                        ddlApproval.ForeColor = System.Drawing.Color.Green;
                        //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

                    }
                    else if (evalidd == 2)
                    {
                        ddlApproval.Enabled = true;
                        ddlApproval.SelectedValue = "2";                                //ds.Tables[0].Rows[1]["APPROVE_STATUS"].ToString();
                        ddlApproval.ForeColor = System.Drawing.Color.Red;
                        //lblStatus.Text = "Reject";
                        //lblStatus.ForeColor = System.Drawing.Color.Green;
                        //lblstatus1.Font = System.Drawing.FontStyle.Bold;   

                    }
                    else
                    {
                        //ddlApproval.Enabled = true;
                        //ddlApproval.SelectedValue = "0";           // ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                       

                   }
           }



        
    }
}