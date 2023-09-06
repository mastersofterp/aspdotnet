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

public partial class ACADEMIC_ANSWERSHEET_AnswersheetRecieve : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AnswerSheetController objAnsSheetController = new AnswerSheetController();
    #region Page Load
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
                   // btnReport.Enabled = false;  
                    // studPnl.Enabled = true;
                    //Calendar1.EndDate = DateTime.Now;  

                }
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            //this.BindListView();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve_Load() --> " + ex.Message + " " + ex.StackTrace);
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
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
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
    
        //try
        //{
        //    //objCommon.FillDropDownList(ddlSessionDate, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        //    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        //    objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1", "EXAMNO DESC");
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_RECIEVE_AnswersheetRecieve.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUaimsCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    #endregion


    # region Other Events
    private void BindListView()
    {
        try
        {
           // ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
           // ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
           // ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
           // ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            if (txtExamDate.Text != string.Empty)
            {
                objAnsSheet.Receiver_Date = Convert.ToDateTime(txtExamDate.Text);
            }

            DataSet ds = objAnsSheetController.GetAnswerSheetCourses(objAnsSheet);
            if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();

                foreach (RepeaterItem items in lvStudents.Items)
                {
                    RangeValidator rvCount = items.FindControl("rvansered") as RangeValidator;
                    TextBox txtAnsRced = items.FindControl("txtAnsRced") as TextBox;

                    if (txtAnsRced.ToolTip.ToString().Trim() != string.Empty || txtAnsRced.ToolTip.ToString().Trim() != "")
                    {
                        rvCount.MaximumValue = txtAnsRced.ToolTip.ToString().Trim();
                    }
                    else
                    {
                        rvCount.MaximumValue = "0";
                    }
                }
                ddlSession.Enabled = false;
                ddlDegree.Enabled = false;
                ddlBranch.Enabled = false;
                ddlScheme.Enabled = false;
                ddlExamType.Enabled = false;
                ddlExamType.Enabled = false;
            }
            else
            {
                lvClear();
                objCommon.DisplayMessage(this.updSession, "Records not found.", this.Page);
                //  btnSubmit.Enabled = false;lvStudents
                btnReport.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        pnlStudent.Visible = true;
        this.BindListView();
        btnReport.Enabled = true;
       

    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB WITH (NOLOCK) ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "LONGNAME");
            ddlBranch.Focus();
        }

        lvClear();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDegree.SelectedValue) > 0 && ddlBranch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ddlBranch.SelectedValue + "AND DEGREENO =" + ddlDegree.SelectedValue, "SCHEMENO");
            //pnlStudent.Visible = false;
        }
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
       // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ddlScheme.SelectedValue, "SE.SEMESTERNO");
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER SE WITH (NOLOCK) ON SR.SEMESTERNO=SE.SEMESTERNO", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + "AND SR.SCHEMENO =" + ViewState["schemeno"], "SE.SEMESTERNO");
        ddlSem.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        lvClear();
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME WITH (NOLOCK)", "EXAMNO", "EXAMNAME", "EXAMTYPE=2 and PATTERNNO=1 and EXAMNAME<>''", "EXAMNO DESC");
        ddlExam.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        lvClear();
    }


    # endregion

    #region Click Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string coursename = string.Empty;
            string uanoRecd = string.Empty;
            string uanoSub = string.Empty;
            string AnsRecd = string.Empty;
            string Date = string.Empty;
            string Remark = string.Empty;
            string recstaffuano = Session["userno"].ToString();
            string totalstudent = string.Empty;
            int countduties = 0;


            foreach (RepeaterItem items in lvStudents.Items)
            {
                //if (Convert.ToInt32((items.FindControl("ddlRcedStaff") as DropDownList).SelectedValue) > 0 && Convert.ToInt32((items.FindControl("txtAnsSub") as DropDownList).SelectedValue) > 0)
                if ((items.FindControl("txtAnsSub") as TextBox).Text != string.Empty && recstaffuano.ToString() != string.Empty)
                {
                    coursename += (items.FindControl("hdfcoursename") as HiddenField).Value + ",";
                    AnsRecd += (items.FindControl("txtAnsRced") as TextBox).Text + ",";
                    uanoRecd += recstaffuano.ToString() + ",";
                    uanoSub += (items.FindControl("txtAnsSub") as TextBox).Text + ",";
                    totalstudent = (items.FindControl("txtRegStud") as TextBox).Text;
                    Date += (items.FindControl("txtExamDate") as TextBox).Text + ",";
                    Remark += (items.FindControl("txtRemark") as TextBox).Text + ",";
                    countduties++;
                }

            }
            if (uanoSub.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSession, "Please Insert Total Answersheet and Its Submission Date.", this.Page);

                return;
            }

            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objAnsSheet.DegreeNo = Convert.ToInt32(ViewState["degreeno"]);
            objAnsSheet.BranchNo = Convert.ToInt32(ViewState["branchno"]);
            objAnsSheet.SchemeNo = Convert.ToInt32(ViewState["schemeno"]);
            objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
            objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.CourseName = coursename.TrimEnd(',');
            objAnsSheet.SplitAnsRecd = AnsRecd.TrimEnd(',');
            objAnsSheet.SplitUanoRecd = uanoRecd.TrimEnd(',');
            objAnsSheet.SplitUanoSub = uanoSub.TrimEnd(',');
            objAnsSheet.SplitReportTime = Date.TrimEnd(',');
            //objAnsSheet.SplitRemark = Remark(',');
            objAnsSheet.SplitRemark = Remark.ToString();

            CustomStatus cs = (CustomStatus)objAnsSheetController.InsertAnswerSheetMark(objAnsSheet, totalstudent);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updSession, "Answersheet Save Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Server Error...", this.Page);
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lvStudents_ItemDataBound(Object sender, RepeaterCommandEventArgs e)
    {
        try
        {
            DropDownList ddlRcedStaff = e.Item.FindControl("ddlRcedStaff") as DropDownList;


            DataSet ds = objCommon.FillDropDown("ACD_EXAM_STAFF WITH (NOLOCK)", "DISTINCT(EXAM_STAFF_NO)", "STAFF_NAME", "(STAFF_TYPE='S' OR STAFF_TYPE='B') AND ACTIVE=1 AND SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "EXAM_STAFF_NO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlRcedStaff.Items.Add(new ListItem(dtr["STAFF_NAME"].ToString(), dtr["EXAM_STAFF_NO"].ToString()));

                }
            }
            ddlRcedStaff.SelectedValue = ddlRcedStaff.ToolTip;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (txtExamDate.Text == "")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"].ToString() + ",@P_BRANCHNO=" + ViewState["branchno"].ToString() + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@username=" + Session["username"].ToString() + ",@P_EXAMTYPE=" + ddlExamType.SelectedValue + ",@P_EXAMDATE=null";
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ViewState["degreeno"].ToString() + ",@P_BRANCHNO=" + ViewState["branchno"].ToString() + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@username=" + Session["username"].ToString() + ",@P_EXAMTYPE=" + ddlExamType.SelectedValue + ",@P_EXAMDATE=" + Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd");

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession .GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lvClear();
        Clear();

    }

    protected void btnCancelDate_Click(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        Clear();

    }
    private void Clear()
    {
        ddlClgname.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        // ddlSessionDate.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        txtExamDate.Text = string.Empty;
        ddlBranch.Enabled = true;
        ddlSession.Enabled = true;
        ddlDegree.Enabled = true;
        ddlExam.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSem.Enabled = true;
        ddlExamType.Enabled = true;
        btnReport.Enabled = false;
    }

    # endregion
    private void lvClear()
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        pnlStudent.Visible = false;
    }

    protected void rblDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblDetails.SelectedValue == "1")
        {
            Clear();
            //answerPnl1.Visible = false;          
            //pnlStudent.Visible = false;
            ExamDate.Visible = true;
            DateButton.Visible = true;
            answerPnl2.Visible = false;
            answerpnl1.Visible = true;
            pnlStudent.Visible = false;
            AllSelectionButton.Visible = false;

        }
        if (rblDetails.SelectedValue == "0")
        {
            Clear();
            ExamDate.Visible = false;
            answerpnl1.Visible = true;
            pnlStudent.Visible = false;
            answerPnl2.Visible = true;
            DateButton.Visible = false;
            AllSelectionButton.Visible = true;
        }

    }
    protected void btnShow1_Click(object sender, EventArgs e)
    {
        pnlStudent.Visible = true;
        this.BindListView();
        btnReport.Enabled = true;
       
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
           // int examno = 0;
            string coursename = string.Empty;
            string uanoRecd = string.Empty;
            string uanoSub = string.Empty;
            string AnsRecd = string.Empty;
            string Date = string.Empty;
            string Remark = string.Empty;
            string ExamType = string.Empty;
            string recstaffuano = Session["userno"].ToString();

            int countduties = 0;


            foreach (RepeaterItem items in lvStudents.Items)
            {
                //if (Convert.ToInt32((items.FindControl("ddlRcedStaff") as DropDownList).SelectedValue) > 0 && Convert.ToInt32((items.FindControl("txtAnsSub") as DropDownList).SelectedValue) > 0)
                if ((items.FindControl("txtAnsSub") as TextBox).Text != string.Empty && recstaffuano.ToString() != string.Empty)
                {
                    AnsRecd += (items.FindControl("txtAnsRced") as TextBox).Text + ",";
                    coursename += (items.FindControl("hdfcoursename") as HiddenField).Value + ",";
                    AnsRecd += (items.FindControl("txtAnsRced") as TextBox).Text + ",";
                    uanoRecd += recstaffuano.ToString() + ",";
                    uanoSub += (items.FindControl("txtAnsSub") as TextBox).Text + ",";
                   // examno +=Convert.ToInt32( (items.FindControl("ddlexam") as DropDownList).SelectedValue + ",");
                    Date += (items.FindControl("txtExamDate") as TextBox).Text + ",";
                    Remark += (items.FindControl("txtRemark") as TextBox).Text + ",";
                    ExamType += (items.FindControl("hdfExamType") as HiddenField).Value + ",";


                    countduties++;
                }

            }
            if (uanoSub.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSession, "Please Insert Total Answersheet and Its Submission Date.", this.Page);

                return;
            }


            AnswerSheet objAnsSheet = new AnswerSheet();
            objAnsSheet.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            //objAnsSheet.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            //objAnsSheet.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            //objAnsSheet.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
           // objAnsSheet.ExamNo = Convert.ToInt32(ddlExam.SelectedValue);
            objAnsSheet.ExamNo = 6;
            //objAnsSheet.Examtype = Convert.ToInt32(ddlExamType.SelectedValue);
            //objAnsSheet.SemesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            objAnsSheet.CourseName = coursename.TrimEnd(',');
            objAnsSheet.SplitAnsRecd = AnsRecd.TrimEnd(',');
            objAnsSheet.SplitUanoRecd = uanoRecd.TrimEnd(',');
            objAnsSheet.SplitUanoSub = uanoSub.TrimEnd(',');
            objAnsSheet.SplitReportTime = Date.TrimEnd(',');
            objAnsSheet.SplitExamtype = ExamType.TrimEnd(',');
            //objAnsSheet.SplitRemark = Remark(',');
            objAnsSheet.SplitRemark = Remark.ToString();

            CustomStatus cs = (CustomStatus)objAnsSheetController.updateAnswerSheetMark(objAnsSheet);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updSession, "Answersheet Save Successfully.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updSession, "Server Error...", this.Page);
            }
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnReport_Click1(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETRECD", "rptanswersheetRecieve.rpt");
    }
    protected void btnReport1_Click(object sender, EventArgs e)
    {
        ShowReport("ANSWERSHEETRECD", "rptanswersheetRecieve.rpt");
    }
 
    //protected void btnEvaluationReport1_Click(object sender, EventArgs e)     //Injamam Date:23-2-23 changes as per Sabhaz Sir
    //{
    //    ShowReportEvaluation("ANSWER_SHEE_DETAILS", "rptCASRegister.rpt");
    //}

    //private void ShowReportEvaluation(string reportTitle, string rptFileName)              //Injamam Date:23-2-23 changes as per Sabhaz Sir
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        if (txtExamDate.Text == "")
    //            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREE=" + ViewState["degreeno"].ToString()+ ",@P_BRANCH=" + ViewState["branchno"].ToString() + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SUBID=" + ddlExamType.SelectedValue;
    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        //To open new window from Updatepanel
    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        //sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        //ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession .GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_ANSWERSHEET_AnswersheetRecieve.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}


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
                ddlSession.SelectedIndex = 0;
                ddlSem.SelectedIndex = 0;
                ddlExam.SelectedIndex = 0;
                ddlExamType.SelectedIndex = 0;
            }
        }

        else
        {
           //ddlCourse.Items.Clear();
           //ddlCourse.Items.Add(new ListItem("Please Select", "0"));
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
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlExamType.SelectedIndex = 0;
        lvClear();
    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvClear();
    }
}