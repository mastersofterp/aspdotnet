using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DynamicAL_v2;

public partial class ACADEMIC_EXAMINATION_CreateBundle : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    DynamicControllerAL AL = new DynamicControllerAL();
    string que_out;
    string a;

    #region "Page Event"
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
            //if (Page.IsPostBack)
            //{
            //    if (rbExStudent.Checked)
            //    {
            //        txtSeatFrom.Text = "0";
            //        txtSeatTo.Text = "0";
            //        txtNoOfAnswersheet.Text = "0";
            //        txtSeatFrom.Enabled = false;
            //        txtSeatTo.Enabled = false;
            //    }
            //}
            if (!this.Page.IsPostBack)
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
                    //this.BindListView();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                        //ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));                  
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //this.BindListView();
                    ViewState["bundleno"] = null;
                }
                PopulateDropDown();
                FillDropdown();

            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AutoAssignValuer.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateBundle.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateBundle.aspx");
        }
    }
    #endregion "Page Event"

    #region "General"
    private void FillDropdown()
    {
        try
        {
            // To Fill Dropdown of Session

            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "FLOCK = 1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID"); 
            int userno = Convert.ToInt32(Session["userno"]);//int onlyrtm = Convert.ToInt32(objCommon.LookUp("USER_ACC", "ISNULL(ONLY_RTM,0) ONLY_RTM", "UA_NO=" + userno));
            int usertype = Convert.ToInt32(Session["usertype"]);
            //String deptno = Session["userdeptno"].ToString();
            string usename = Session["username"].ToString();
            //objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT", "SLOTNO AS SLOT", "SLOTNAME", "SLOTNO>0", "SLOTNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_CreateBundle.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion "General"

    #region "SelectedIndexChanged"


    // Slot Selection Change
    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSlot.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_EXAM_DATE A INNER JOIN ACD_COURSE C ON (A.CCODE = C.CCODE AND A.SCHEMENO = C.SCHEMENO AND A.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = A.BRANCHNO)", "A.CCODE", "(C.CCODE + ' - ' + C.COURSE_NAME + ' - ' + B.SHORTNAME)COURSE_NAME", "SLOTNO= " + Convert.ToInt32(ddlSlot.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND EXAMDATE = CONVERT(DATETIME,'" + txtDtOfPaper.Text + "',103)", "COURSE_NAME");
            //objCommon.FillDropDownList(ddlCourse, "ACD_EXAM_DATE A INNER JOIN ACD_COURSE C ON (A.COURSENO = C.COURSENO AND A.SCHEMENO = C.SCHEMENO AND A.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = A.BRANCHNO) INNER JOIN ACD_SESSION_MASTER SM ON (A.SESSIONNO = SM.SESSIONNO) INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO = S.SEMESTERNO)", " DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME + ' - ' + B.SHORTNAME + ' - ' + S.SEMESTERNAME)COURSE_NAME", "C.SUBID = 1 AND SLOTNO= " + Convert.ToInt32(ddlSlot.SelectedValue) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "COURSE_NAME");
            objCommon.FillDropDownList(ddlCourse, "ACD_EXAM_DATE A INNER JOIN ACD_COURSE C ON (A.COURSENO = C.COURSENO AND A.SCHEMENO = C.SCHEMENO AND A.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = A.BRANCHNO) INNER JOIN ACD_SESSION_MASTER SM ON (A.SESSIONNO = SM.SESSIONNO) INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO = S.SEMESTERNO) INNER JOIN ACD_SUBJECTTYPE ST ON (C.SUBID = ST.SUBID)", " DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME + ' - ' + B.SHORTNAME + ' - ' + S.SEMESTERNAME)COURSE_NAME", "ST.TH_PR =1 AND SLOTNO= " + Convert.ToInt32(ddlSlot.SelectedValue) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + " AND A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + " AND EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "COURSE_NAME");
            lvBundleList.DataSource = null;
            lvBundleList.DataBind();
            lvBundleList.Visible = false;
        }
        else
        {
            ddlCourse.SelectedIndex = 0;
            lvBundleList.DataSource = null;
            lvBundleList.DataBind();
            lvBundleList.Visible = false;
        }
    }
    // Course Code Selection Change
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(txtStudPerBundle.Text).Equals(string.Empty))
        {
            BindBundleList();
        }
        else
        {
            objCommon.DisplayMessage(updExam, "Please enter No. of Students Per Bundle.", this.Page);
            //ddlSlot.SelectedIndex = -1;
            //ddlCourse.SelectedIndex = -1;
        }
    }

    private void BindBundleList()
    {
        DataSet ds;
        string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        //DataSet ds = objExamController.GetBundleNo_SeatNoDetails(Convert.ToInt32(ddlSession.SelectedValue), ddlCourse.SelectedValue, Convert.ToDateTime(txtDtOfPaper.Text), Convert.ToInt32(ddlBranch.SelectedValue));
        //DataSet ds = objExamController.GetBundleNo_SeatNoDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToDateTime(ddlDate.SelectedValue.ToString()), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(txtStudPerBundle.Text.Trim()));
        //if (ddlBranch.SelectedIndex > 0)
        //{
        //    //string sp_procedure = "PKG_SPECIALISATION_COURSE_STUDENT_LIST";
        //    //string sp_parameters = "@P_SCHEMENO,@P_BRANCHNO,@P_DEGREENO";
        //    //string sp_callValues = "" + Convert.ToInt32(ddlScheme.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlDegree.SelectedValue) + "";

        //    //ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
        //    ds = objExamController.GetBundleNo_SeatNoDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToDateTime(ddlDate.SelectedValue.ToString()), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(txtStudPerBundle.Text.Trim()));
        //}
        //else
        //{

        // added by shubham on Excel as per PCEN Requirement
        string SchBranchnos = "";
        DataSet dsBranchnos = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO = SR.IDNO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO = SR.SCHEMENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO = S.BRANCHNO)", "DISTINCT BR.BRANCHNO", "BR.LONGNAME", "SR.SESSIONNO = " + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND  SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0", "BR.BRANCHNO");

        if (dsBranchnos.Tables.Count > 0)
        {
            if (dsBranchnos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsBranchnos.Tables[0].Rows)
                {
                    SchBranchnos += row["BRANCHNO"].ToString() + "$";
                }
                SchBranchnos = SchBranchnos.TrimEnd('$');
            }
        }
        DateTime selectedDate = Convert.ToDateTime(ddlDate.SelectedValue);
        string formattedDate = selectedDate.ToString("dd/MMM/yyyy").ToUpper();
        string sp_procedure = "PKG_ACAD_GET_BUNDLENO_SEATNO_NEW";
        string sp_parameters = "@P_SESSIONID,@P_COURSENO,@P_EXAMDATE,@P_BRANCHNO,@P_BRANCHNO_S,@P_BUNDLENO";
        string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + formattedDate + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + SchBranchnos.ToString() + "," + Convert.ToInt32(txtStudPerBundle.Text.Trim()) + "";
        ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
        //int brchno = Convert.ToInt32(ddlBranch.SelectedValue);
        //    ds = objExamController.GetBundleNo_SeatNoDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToDateTime(ddlDate.SelectedValue.ToString()), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(txtStudPerBundle.Text.Trim()));
        //}

        // end here by Shubham as per PCEN Requirement
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvBundleList.DataSource = ds.Tables[0];
                lvBundleList.DataBind();
                lvBundleList.Visible = true;
                ColumnHide(ds);
            }
            else
            {
                lvBundleList.DataSource = null;
                lvBundleList.DataBind();
                lvBundleList.Visible = false;
            }

        }
        else
        {
            lvBundleList.DataSource = null;
            lvBundleList.DataBind();
            lvBundleList.Visible = false;
        }
    }

    // added by shubham on 11-01-24
    protected void ColumnHide(DataSet ds)
    {
        int count = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["SEATNOFROM"].ToString()) || ds.Tables[0].Rows[i]["SEATNOFROM"].ToString() == "0")
            {
                count++;
            }
        }
        if (count == ds.Tables[0].Rows.Count)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#hdSeatfrom').hide();$('td:nth-child(7)').hide();$('#hdSeatto').hide();$('td:nth-child(8)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#hdSeatfrom').hide();$('td:nth-child(7)').hide();$('#hdSeatto').hide();$('td:nth-child(8)').hide();});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#hdSeatfrom').show();$('td:nth-child(7)').show();$('#hdSeatto').show();$('td:nth-child(8)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#hdSeatfrom').show();$('td:nth-child(7)').show();$('#hdSeatto').show();$('td:nth-child(8)').show();});", true);
        }
    }

    #endregion "SelectedIndexChanged"

    #region "Button Event"
    // Submit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlDate.SelectedIndex > 0)
            {
                CustomStatus cs = CustomStatus.Error;


                foreach (ListViewDataItem item in lvBundleList.Items)
                {
                    Label lblBundle = item.FindControl("lblBundleNo") as Label;
                    Label lblCourseNo = item.FindControl("lblCoursrNo") as Label;
                    Label lblRegFrom = item.FindControl("lblRegNoFrom") as Label;
                    Label lblRegTo = item.FindControl("lblRegNoTo") as Label;
                    Label lblSeatFrom = item.FindControl("lblSeatNoFrom") as Label;
                    Label lblSeatto = item.FindControl("lblSeatNoTo") as Label;
                    Label lblBranchno = item.FindControl("lblBranch") as Label;
                    Label lblBundleCount = item.FindControl("lblBundleCount") as Label;

                    cs = (CustomStatus)objExamController.CreateBundle(Convert.ToInt32(lblBundle.ToolTip), Convert.ToInt32(lblCourseNo.ToolTip), (lblRegFrom.ToolTip).ToString(), (lblRegTo.ToolTip).ToString(), (lblSeatFrom.ToolTip).ToString(), (lblSeatto.ToolTip).ToString(), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranchno.ToolTip), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(lblBundleCount.ToolTip));
                    // cs = (CustomStatus)objExamController.CreateBundle(Convert.ToInt32(lblBundle.ToolTip), Convert.ToInt32(lblCourseNo.ToolTip), (lblRegFrom.ToolTip).ToString(), (lblRegTo.ToolTip).ToString(), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblBranchno.ToolTip), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(lblBundleCount.ToolTip));
                    // cs = (CustomStatus)objExamController.CreateBundle(Convert.ToInt32(lblBundle.ToolTip), ddlCourse.SelectedValue, Convert.ToInt32(lblSeatFrom.ToolTip), Convert.ToInt32(lblSeatTo.ToolTip), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                    //string SP_Name = "PKG_ACAD_INS_BUNDLE";
                    //string SP_Parameters = "@P_BUNDLENO,@P_COURSENO,@P_REGNOFROM,@P_REGNOTO,@P_SEATNOFROM,@P_SEATNOTO,@P_SESSIONID,@P_BRANCHNO,@P_COLLEGEID,@P_COUNT,@P_OUT";
                    //string Call_Values = "" + Convert.ToInt32(lblBundle.ToolTip) + "," + Convert.ToInt32(lblCourseNo.ToolTip) + "," + (lblRegFrom.ToolTip).ToString() + "," + (lblRegTo.ToolTip).ToString() + "," + (lblSeatFrom.ToolTip).ToString() + "," + (lblSeatto.ToolTip).ToString() + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(lblBranchno.ToolTip) + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32(lblBundleCount.ToolTip) + "," + ",1";


                    //que_out = AL.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true, 1);
                    //a = que_out;
                }

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updExam, "Bundle Creation Done Successfuly..!!", this.Page);

                }
                else if (cs.Equals(CustomStatus.RecordNotFound))
                {
                    objCommon.DisplayMessage(updExam, "Bundle Already Created..!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updExam, "Error in Bundle Creation ..", this.Page);
                }
                BindBundleList();

            }
            else
            {
                objCommon.DisplayMessage(updExam, "Please select Date of Paper", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_CreateBundle.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    // Cancel Click Event
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    // Report Click Event
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Plesse Select Date of Exam..!!", this.Page);
        }
        else
        {

            string SP_Name = "PKG_ROUGH_BUNDLE_REPORT";
            string SP_Parameters = "@P_SESSIONID,@P_EXAM_DATE,@P_SLOTNO,@P_COURSENO";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlDate.SelectedValue.ToString() + "," + Convert.ToInt32(ddlSlot.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ShowReport("Assigning rough Bundle Sheet", "rptRoughBundleReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
            }
        }
    }
    protected void btnStickerReport_Click(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Plesse Select Date of Exam..!!", this.Page);
        }
        else
        {
            string SP_Name = "PKG_BUNDLE_REPORT_COURSEWISE";
            string SP_Parameters = "@P_SESSIONID,@P_EXAM_DATE,@P_SLOTNO,@P_COURSENO";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlDate.SelectedValue.ToString() + "," + Convert.ToInt32(ddlSlot.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ShowReport1("Assigning rough Bundle Sheet", "BundleReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
            }
        }

    }
    protected void btnDateWiseReport_Click(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Plesse Select Date of Exam..!!", this.Page);
        }
        else
        {
            string SP_Name = "PKG_BUNDLE_REPORT_DATEWISE";
            string SP_Parameters = "@P_SESSIONID,@P_EXAM_DATE,@P_SLOTNO";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlDate.SelectedValue.ToString() + "," + Convert.ToInt32(ddlSlot.SelectedValue) + "";
            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ShowReport2("Date_Wise Bundle List", "rptDateWiseBundleReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Data Found for current selection.", this.Page);
            }
        }

    }
    #endregion "Button Event"

    #region "Show Report"
    // Show Report Method
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "exporttype=" + exporttype;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by Shubham on 01022023
            {

                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER SM INNER JOIN ACD_EXAM_DATE A ON (A.SESSIONNO = SM.SESSIONNO)", "SM.COLLEGE_ID", "SM.SESSIONID>0 AND SM.IS_ACTIVE = 1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)" + " AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue)));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue;
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.updExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport1(string reportTitle, string rptFileName)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "exporttype=" + exporttype;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by Shubham on 07/02/2023
            {
                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER SM INNER JOIN ACD_EXAM_DATE A ON (A.SESSIONNO = SM.SESSIONNO)", "SM.COLLEGE_ID", "SM.SESSIONID>0 AND SM.IS_ACTIVE = 1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)" + " AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue)));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue;
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue) + ",@P_COURSENO=" + ddlCourse.SelectedValue;
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.updExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ShowReport2(string reportTitle, string rptFileName)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "exporttype=" + exporttype;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by Shubham On 01022023
            {

                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER SM INNER JOIN ACD_EXAM_DATE A ON (A.SESSIONNO = SM.SESSIONNO)", "SM.COLLEGE_ID", "SM.SESSIONID>0 AND SM.IS_ACTIVE = 1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)" + " AND A.COURSENO =" + Convert.ToInt32(ddlCourse.SelectedValue)));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue);

            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue);
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.updExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        try
        {
            Online_Evaluation_Report();

            //Response.Clear();
            //string fileName = "Online_Evaluation";
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");

            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.xls";

            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //DataSet ds = null;
            //ds = objExamController.GetOnlineEvalutionReport(Convert.ToInt32(ddlSession.SelectedValue));
            ////lvBundleList.Visible = true;
            //lvOnlineEvaluation.DataSource = ds;
            //lvOnlineEvaluation.DataBind();

            //lvOnlineEvaluation.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());

            //Response.End();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_ShowReport.btnExcelReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void Online_Evaluation_Report()
    {

        string attachment = "attachment; filename=" + "Online_Evaluation.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/" + "ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        // int sessionNo = 0;
        // sessionNo = Convert.ToInt32(ddlAdmbatch.SelectedValue);
        DataSet ds = null;
        ds = objExamController.GetOnlineEvalutionReport(Convert.ToInt32(ddlSession.SelectedValue));
        DataGrid dg = new DataGrid();
        if (ds.Tables.Count > 0)
        {
            dg.DataSource = ds.Tables[0];
            dg.DataBind();
        }
        dg.HeaderStyle.Font.Bold = true;
        dg.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void btnStickerOnScreenReport_Click(object sender, EventArgs e)
    {
        //if (txtDtOfPaper.Text == string.Empty)
        //{
        //    objCommon.DisplayMessage("Plesse Select Date of Exam..!!", this.Page);
        //}
        //else
        //{
        //    ShowReport1("Assigning_rough_Bundle_Sheet_On_Screen", "BundleOnScreenReport.rpt");
        //}
    }
    #endregion "Show Report"



    protected void txtStudPerBundle_TextChanged(object sender, EventArgs e)
    {
        if (!(txtStudPerBundle.Text).Equals(string.Empty))
        {
            BindBundleList();
        }
        else
        {
            objCommon.DisplayMessage(updExam, "Please enter No. of Students Per Bundle.", this.Page);
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {

            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S WITH (NOLOCK) INNER JOIN RESULT_PUBLISH_DATA TR WITH (NOLOCK) ON (S.SESSIONNO=TR.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "TR.SESSIONNO > 0 and isnull(is_active,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_NAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONID DESC");
            }

            ddlSession.SelectedIndex = 0;
            //ddlSem.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO desc");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID) ", "S.SESSIONID", "S.SESSION_NAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + ViewState["college_id"].ToString(), "S.SESSIONID DESC");
        }
        else
        {
            ddlCollege.SelectedIndex = 0;
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            Common objCommon = new Common();
            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 ", "S.SEMESTERNO");
            ddlSession.Focus();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "CourseWise_Registration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDate, "ACD_EXAM_DATE E INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)", "DISTINCT CONVERT(NVARCHAR,E.EXAMDATE,103)DATE", "CONVERT(NVARCHAR,E.EXAMDATE,103)EXAMDATE", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND E.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "DATE");
            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND IS_ACTIVE = 1"));
            ViewState["SESSIONNO"] = SessionNo;
            objCommon.FillDropDownList(ddlBranch, "ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON (S.IDNO = SR.IDNO) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO = SR.SCHEMENO) INNER JOIN ACD_BRANCH BR ON (BR.BRANCHNO = S.BRANCHNO)", "DISTINCT BR.BRANCHNO", "BR.LONGNAME", "SR.SESSIONNO = " + Convert.ToInt32(ViewState["SESSIONNO"]) + " AND  SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(SR.CANCEL,0)=0 AND ISNULL(S.CAN,0)=0 AND ISNULL(S.ADMCAN,0)=0", "BR.BRANCHNO");

        }
        else
        {
            ddlSession.SelectedIndex = 0;
        }
    }
    protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_TT_SLOT ES INNER JOIN ACD_EXAM_DATE E ON (E.SLOTNO = ES.SLOTNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)", "DISTINCT ES.SLOTNO", "ES.SLOTNAME", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SM.COLLEGE_ID =" + Convert.ToInt32(ViewState["college_id"]) + " AND E.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND E.EXAMDATE =CONVERT (DATETIME,'" + ddlDate.SelectedValue.ToString() + "',103)", "ES.SLOTNO");
        }
        else
        {
            ddlDate.SelectedIndex = 0;
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string proc_name = "PKG_ACD_ANSWERSHEET_BUNDLE_STUD_GETLIST_PCEN";
            string param = "@P_SESSIONNO";
            string call_values = "" + Convert.ToInt32(ViewState["SESSIONNO"]) + "";

            DataSet ds = objCommon.DynamicSPCall_Select(proc_name, param, call_values);
            GridView dg = new GridView();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                    //AddReportHeader(dg);
                    string attachment = "attachment; filename=" + "Bundle_Details " + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
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
                    objCommon.DisplayMessage(this, "No Data Found for this selection.", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found for this selection.", this.Page);
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                BindBundleList();
            }
        }
        catch (Exception ex)
        {
        }
    }
}