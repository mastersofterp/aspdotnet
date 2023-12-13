using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using IITMS;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Data;
using ClosedXML.Excel;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using IITMS.UAIMS;

public partial class OBE_QuestionWiseMarksEntry : System.Web.UI.Page
{
    CommonModel ObjComModel = new CommonModel();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    ExamTypeModel objExamType = new ExamTypeModel();
    OBEMarkEntryController obeMarkEnrty = new OBEMarkEntryController();
    ExamQuestionPaperController objStatC = new ExamQuestionPaperController();

    static int cnt = 0;
    int userno = 0;
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


                DataSet ds_CheckActivity = obeMarkEnrty.CheckSessionActivity(Session["usertype"].ToString(), Request.QueryString["pageno"].ToString(), Convert.ToInt32(Session["userno"]));


                int USER_TYPE = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NO=" + Session["userno"]));
                if (USER_TYPE == 3)
                {

                    if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    {
                        objCommon.DisplayMessage(this.Page, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                        return;
                    }
                    else
                    {
                        ddlSession.DataSource = ds_CheckActivity;
                        ddlSession.DataTextField = "SESSION_NAME";
                        ddlSession.DataValueField = "SESSIONNO";
                        ddlSession.DataBind();
                    }

                }
                else
                {

                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER SM ON(S.COLLEGE_ID=SM.COLLEGE_ID)", "S.sessionno", "Concat(s.SESSION_NAME , '-',SM.SHORT_NAME)SESSION_NAME", "FLOCK=1", "S.sessionno");

                }

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO = 237 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");

                Page.Title = Session["coll_name"].ToString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }

        }
        else
        {

            if (Request.Params["__EVENTTARGET"] != null &&
                Request.Params["__EVENTTARGET"].ToString() != string.Empty)
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "ddlExamName")
                    this.ddlExamName_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        userno = Convert.ToInt32(Session["userno"]);
        hdnUserno.Value = Convert.ToString(Session["userno"]);
        hdnIpAddress.Value = ViewState["ipAddress"].ToString();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=QuestionWiseMarksEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QuestionWiseMarksEntry.aspx");
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        pnlSubjectDetail.Visible = false;
        divSession.Visible = false;
        pnlStudentMarksEntry.Visible = true;
        LinkButton LNK = sender as LinkButton;
        string SchemeSubjectId = LNK.ToolTip;
        ViewState["SchemeSubjectId"] = LNK.ToolTip;
        hdfSubjectId.Value = LNK.ToolTip;
        string SectionId = LNK.CommandArgument;
        hdfSectionId.Value = LNK.CommandArgument;
        ViewState["SectionId"] = LNK.CommandArgument;
        hdnUserSession.Value = ddlSession.SelectedValue;
        //objCommon.FillDropDownList(ddlExamName, "tblExamPatternMapping M LEFT JOIN tblExamQuestionPaper Q on(Q.ExamPatternMappingId=M.ExamPatternMappingId) INNER JOIN tblExamNameMaster E ON(M.ExamNameId=E.ExamNameId AND E.PATTERNNO=M.ExamPatternMappingId)", "Q.QuestionPaperId", "E.ExamName", "Q.SchemeSubjectId=" + SchemeSubjectId + " AND ((Q.SectionId=" + SectionId + " OR " + SectionId + "= 0) OR SectionId=0) AND E.ExamNameId<>8", "E.ExamName");
        //added 260822
        //objCommon.FillDropDownList(ddlExamName, "tblExamPatternMapping M LEFT JOIN tblExamQuestionPaper Q on(Q.ExamPatternMappingId=M.ExamPatternMappingId) INNER JOIN tblExamNameMaster E ON(M.ExamNameId=E.ExamNameId)", "Q.QuestionPaperId", "E.ExamName", "Q.SchemeSubjectId=" + SchemeSubjectId + " AND ((Q.SectionId=" + SectionId + " OR " + SectionId + "= 0) OR SectionId=0) AND E.ExamNameId<>8 and Q.IsLock=1", "E.ExamName");
        //added on 31012023
        //objCommon.FillDropDownList(ddlExamName, "tblExamPatternMapping M LEFT JOIN tblExamQuestionPaper Q on(Q.ExamPatternMappingId=M.ExamPatternMappingId) INNER JOIN tblExamNameMaster E ON(M.ExamNameId=E.ExamNameId)", "Q.QuestionPaperId", "E.ExamName", "Q.SchemeSubjectId=" + SchemeSubjectId + " AND ((Q.SectionId=" + SectionId + " OR " + SectionId + "= 0) OR SectionId=0) AND E.ExamNameId<>8 and Q.IsLock=1 and sessionid=" + hdnUserSession.Value + "", "E.ExamName");
        //lblSubjectName.Text = ("Subject Name : " + LNK.Text + " (Sec ~ " + LNK.CommandName + ")");
        //lblQuestionDetails.Visible = false;


        //*********************Added On 06022023(For ONE SUBJECT , one Session and Multiple Schemes)********************



        int ret = Convert.ToInt32(objStatC.GetExamDetails(Convert.ToInt32(ViewState["SchemeSubjectId"])));

        if ((int)ret == 2 || (int)ret == 3)
        {

            objCommon.DisplayMessage(this.Page, "CO Is Not Created Or COPO Mapping is not Done..', '", this.Page);
            pnlStudentMarksEntry.Visible = false;
            divSession.Visible = true;
            ddlSession_SelectedIndexChanged(sender, e);
            return;
        }


        string Coursecode = objCommon.LookUp("tblacdschemesubjectmapping tsm inner join ACD_COURSE AC on (tsm.subjectid=AC.courseno and tsm.SubjectTypeId=AC.subid)", "CCODE", "SchemeSubjectId=" + ViewState["SchemeSubjectId"]);

        //objCommon.FillDropDownList(ddlExamName, "tblExamPatternMapping M LEFT JOIN tblExamQuestionPaper Q on(Q.ExamPatternMappingId=M.ExamPatternMappingId) INNER JOIN tblExamNameMaster E ON(M.ExamNameId=E.ExamNameId)", "Q.QuestionPaperId", "E.ExamName", "Q.CCODE=" + "'" + Coursecode + "'" + " AND Q.IsLock=1 and sessionid=" + hdnUserSession.Value + "", "E.ExamName");

        DataSet ds = objStatC.BindExamName(Coursecode, Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ViewState["SectionId"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(hdnUserSession.Value));//when  question paper lock then display Exam Names 
        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlExamName.Items.Clear();
            ddlExamName.DataSource = ds;
            ddlExamName.DataTextField = "ExamName";
            ddlExamName.DataValueField = "QuestionPaperId";
            ddlExamName.DataBind();
            ddlExamName.Items.Insert(0, new ListItem("Please select Exam Name", "0"));
            ddlExamName.SelectedIndex = 0;




        }
        else
        {
            ddlExamName.DataSource = null;
            ddlExamName.DataBind();
            objCommon.DisplayMessage(this.Page, "Please Create Question Paper First..!!!", this.Page);
            pnlStudentMarksEntry.Visible = false;
            divSession.Visible = true;
            ddlSession_SelectedIndexChanged(sender, e);

        }


        lblSubjectName.Text = ("Subject Name : " + LNK.Text + " (Sec ~ " + LNK.CommandName + ")");
        lblQuestionDetails.Visible = false;


        //**********************END**********************************

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet ds = obeMarkEnrty.GetSubjectData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]));
            rptCourse.DataSource = ds;
            rptCourse.DataBind();
            pnlSubjectDetail.Visible = true;
        }
        else
        {
            rptCourse.DataSource = null;
            rptCourse.DataBind();
            pnlSubjectDetail.Visible = false;
            objCommon.DisplayMessage(this, "No Record Found !!!", this.Page);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlSubjectDetail.Visible = true;
        divSession.Visible = true;
        pnlStudentMarksEntry.Visible = false;
        divsubmarks.Visible = false;
        btnOperation.Visible = false;
        ltTable.Visible = false;
        ltrStatusCode.Visible = false;
        divStatusCode.Visible = false;
        divExcelExport.Visible = false;
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //******************added on 21112022****************************************
            int SCHEMENO = Convert.ToInt32(objCommon.LookUp("tblacdschemesubjectmapping", "SCHEMEID", "SchemeSubjectId=" + ViewState["SchemeSubjectId"]));
            int COURSENO = Convert.ToInt32(objCommon.LookUp("tblacdschemesubjectmapping", "SUBJECTID", "SchemeSubjectId=" + ViewState["SchemeSubjectId"]));
            hdfSchemeTest.Value = SCHEMENO.ToString();
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);

            //*******************RULE validation added DT 04072023***********************

            int Org = Convert.ToInt32(objCommon.LookUp("reff", "OrganizationId", ""));
            int Global = Convert.ToInt32(objCommon.LookUp("ACD_course", "CAST (ISNULL(GLOBALELE,0) AS INT) AS GLOBALELE", "Courseno=" + COURSENO));
            int SUBEXAMNO = Convert.ToInt32(objCommon.LookUp("tblExamQuestionPaper  QP INNER JOIN TBLEXAMPATTERNMAPPING EP ON QP.ExamPatternMappingId=EP.ExamPatternMappingId INNER JOIN TBLEXAMNAMEMASTER EMN ON EP.EXAMNAMEID=EMN.EXAMNAMEID INNER JOIN ACD_SUBEXAM_NAME SN ON EMN.FLDNAME=SN.FLDNAME and EMN.Patternno=SN.PAtternno and EMN.SUBID=SN.SUBEXAM_SUBID", "DISTINCT isnull(SUBEXAMNO,0)SUBEXAMNO", " ISNULL(SN.ACTIVESTATUS,0)=1 AND QuestionPaperId=" + ddlExamName.SelectedValue));

            int SUBJECTTYPE = Convert.ToInt32(objCommon.LookUp("tblacdschemesubjectmapping", "SubjectTypeId", "SchemeSubjectId=" + ViewState["SchemeSubjectId"]));

            int USER_TYPE = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_TYPE", "UA_NO=" + Session["userno"]));
            string CCODE = Convert.ToString(objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + COURSENO));

            if ((Global != 1))
            {
                if (SUBJECTTYPE != 2)//Practical -Subject Type
                {
                    if (SUBJECTTYPE != 6)//Project -Subject Type
                    {

                        DataSet ds4 = objCommon.FillDropDown("ACD_STUDENT_RESULT", "distinct SEMESTERNO", "Sessionno", "COURSENO=" + Convert.ToInt32(COURSENO) + "and Schemeno=" + Convert.ToInt32(SCHEMENO) + "and sessionno =" + Convert.ToInt32(ddlSession.SelectedValue) + "and sectionno =" + Convert.ToInt32(ViewState["SectionId"]) + " and isnull(cancel,0)=0 and isnull(exam_registered,0)=1", "semesterno");

                        if ((objCommon.LookUp("ACD_COURSE", "ISNULL(CAST(GLOBALELE AS INT),0) AS GLOBALELE", "COURSENO=" + Convert.ToInt32(ViewState["COURSENO"])) == string.Empty ? 0 : Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(CAST(GLOBALELE AS INT),0) AS GLOBALELE", "COURSENO=" + Convert.ToInt32(ViewState["COURSENO"])))) != 1)
                        {
                            #region For Crescent allowed only CCODE wise Mark entry rule.
                            if (Org == 2)
                            {
                                DataSet ds7 = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + SUBEXAMNO + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND CCODE='" + CCODE + "' AND SEMESTERNO in (" + Convert.ToString(ds4.Tables[0].Rows[0]["SEMESTERNO"]) + ")", "");

                                if (ds7 != null && ds7.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(ds7.Tables[0].Rows[0][0]) < 0)
                                    {
                                        objCommon.DisplayMessage(this.Page, "STOP !!! Rule 1 for " + Convert.ToString(ddlExamName.SelectedItem.Text) + " is not Defined", this.Page);
                                        return;
                                    }
                                    else if (Convert.ToInt32(ds7.Tables[0].Rows[0][1]) < 0)
                                    {
                                        objCommon.DisplayMessage(this.Page, "STOP !!! Rule 2 for " + Convert.ToString(ddlExamName.SelectedItem.Text) + " is not Defined", this.Page);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "STOP !!! Exam Rule is not Defined", this.Page);
                                    return;
                                }

                            }
                            #endregion
                            else
                            {

                                DataSet ds7 = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + SUBEXAMNO + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + COURSENO + ") AND COURSENO=" + COURSENO + " AND SEMESTERNO in (" + Convert.ToString(ds4.Tables[0].Rows[0]["SEMESTERNO"]) + ")", "");


                                if (ds7 != null && ds7.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(ds7.Tables[0].Rows[0][0]) < 0)
                                    {
                                        objCommon.DisplayMessage(this.Page, "STOP !!! Rule 1 for " + Convert.ToString(ddlExamName.SelectedItem.Text) + " is not Defined", this.Page);
                                        return;
                                    }
                                    else if (Convert.ToInt32(ds7.Tables[0].Rows[0][1]) < 0)
                                    {
                                        objCommon.DisplayMessage(this.Page, "STOP !!! Rule 2 for " + Convert.ToString(ddlExamName.SelectedItem.Text) + " is not Defined", this.Page);
                                        return;
                                    }
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.Page, "STOP !!! Exam Rule is not Defined", this.Page);
                                    return;
                                }

                            }
                        }
                    }
                }

            }
            //***********************END***************************

            // DataSet checklock = obeMarkEnrty.GETLOCKCOUNT(SCHEMENO, COURSENO, userno);
            //  DataSet checklock = obeMarkEnrty.GETLOCKCOUNT(SCHEMENO, COURSENO, userno, Convert.ToInt32(ViewState["SectionId"]));//added on 30032023
            //DataSet checklock = obeMarkEnrty.GETLOCKCOUNT(SCHEMENO, COURSENO, userno, Convert.ToInt32(ViewState["SectionId"]),Sessionno);//added on 26062023
            string Examname = objCommon.LookUp("TBLEXAMPATTERNMAPPING M LEFT JOIN TBLEXAMQUESTIONPAPER Q ON(Q.EXAMPATTERNMAPPINGID=M.EXAMPATTERNMAPPINGID) INNER JOIN TBLEXAMNAMEMASTER E ON(M.EXAMNAMEID=E.EXAMNAMEID) INNER JOIN ACD_SUBEXAM_NAME SN ON E.EXAMNO = SN.EXAMNO AND SN.SUBEXAMNAME = EXAMNAME ", "distinct substring(SN.FLDNAME,1,1)FLDNAME", "Q.QUESTIONPAPERID=" + ddlExamName.SelectedValue);

            if (Examname == "E" && USER_TYPE == 3)
            {
                DataSet checklock = obeMarkEnrty.GETLOCKCOUNT(SCHEMENO, COURSENO, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ViewState["SectionId"]), Sessionno);//added on 26062023
                //if (checklock.Tables[0].Rows.Count > 0)
                if (Convert.ToInt32(checklock.Tables[0].Rows[0]["LOCK"]) == 0)
                {

                    objCommon.DisplayMessage(this.Page, "FIRST COMPLETE YOUE INTERNAL COMPONENT MARKS ENTRY..!!!", this.Page);
                    pnlSubjectDetail.Visible = true;
                    divSession.Visible = true;
                    pnlStudentMarksEntry.Visible = false;
                    divsubmarks.Visible = false;
                    btnOperation.Visible = false;
                    ltTable.Visible = false;
                    ltrStatusCode.Visible = false;
                    divStatusCode.Visible = false;
                    divExcelExport.Visible = false;
                    Label1.Visible = false;
                    lblTotalCount.Visible = false;
                    return;
                }
            }
            //*******************END****************************
            // checkMarkEntry();
            int status = (checkMarkEntry());
            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "return", this.Page);
                return;
            }
            else
            {
                lblQuestionDetails.Text = null;
                if (ddlExamName.SelectedIndex > 0)
                {
                    DataSet ds1 = obeMarkEnrty.GetSubjectDetails(Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SectionId"]));
                    string scheme = objCommon.LookUp("acd_course", "schemeno", "courseno=" + Convert.ToInt32(ds1.Tables[0].Rows[0]["COURSENO"]));
                    string ccode = Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]);
                    string elect_type = string.Empty;
                    if (scheme == string.Empty)
                    {
                        elect_type = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "CCODE = '" + Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]) + "'"));

                    }
                    else
                    {
                        elect_type = Convert.ToString(objCommon.LookUp("ACD_COURSE", "ELECT", "CCODE = '" + Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]) + "' and schemeno=" + Convert.ToInt32(scheme) + ""));

                    }

                    hdnElectType.Value = Convert.ToString(elect_type);
                    hdnCcode.Value = ccode;

                    divExcelExport.Visible = true;
                    DataSet LockStatus = obeMarkEnrty.GetLockStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfSubjectId.Value), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(hdfSectionId.Value));
                    if (LockStatus.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(LockStatus.Tables[0].Rows[0]["ISLOCK"]) == true)
                        {
                            //*************************************
                            //btnLock.Disabled = true;
                            // btnSubmit.Disabled = true;
                            //**************************************

                            divsubmarks.Visible = true;
                            chkIsLock.Checked = true;
                            if (Convert.ToBoolean(LockStatus.Tables[0].Rows[0]["ACTIVESTATUS"]) == true)
                            {
                                chkIsActive.Checked = true;

                            }
                            else
                            {
                                chkIsActive.Checked = false;
                            }
                        }
                        else
                        {
                            divsubmarks.Visible = false;
                            chkIsLock.Checked = false;
                            chkIsActive.Checked = false;
                            //btnLock.Disabled = false;
                            // btnSubmit.Disabled = false;
                        }
                    }
                    //DataSet ds = obeMarkEnrty.GetMarksEntryStudentData(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SectionId"]));

                    DataSet ds = obeMarkEnrty.GetMarksEntryStudentData(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SectionId"]), Convert.ToInt32(Session["userno"]));//added on 18032023
                    string tblHeading = string.Empty;
                    string tblMapping = string.Empty;
                    string tblCORBT = string.Empty;
                    string tblBody = string.Empty;
                    int quecount = 0;
                    int corbtcount = 0;
                    int countr = 0;
                    string quemarks = "0";
                    int count = 0;
                    string StatusBody = string.Empty;
                    string StatusCode = string.Empty;
                    int StatusCount = 0;
                    int RowDisableCount = 0;
                    //***********************************NEW PATCH*******************
                    //int Temp_Type = Convert.ToInt32(objCommon.LookUp("TBLQUESTIONSOBTAINEDMARKS", "count(*)", "courseregistrationdetailsid = '" + Convert.ToString(ds.Tables[1].Rows[0]["COURSEREGISTRATIONDETAILSID"]) + "' and IsLock=" + 1 + ""));

                    //if (Temp_Type > 0)
                    //{
                    //    //btnSubmit.Visible = false;

                    //    btnSubmit.Disabled = true;
                    //    btnLock.Disabled = true;
                    //}
                    //else
                    //{
                    //    btnSubmit.Disabled = false;
                    //    btnLock.Disabled = false;
                    //}
                    //********************************************END PATCH*****************
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            Label1.Visible = true;
                            lblTotalCount.Visible = true;
                            Label2.Visible = true;

                            if (ds.Tables[4].Rows.Count > 0)
                            {
                                if (ds.Tables[3].Rows.Count > 0)
                                {
                                    txtsubmarks.Text = ds.Tables[3].Rows[0]["MAXMARKS"].ToString();
                                }
                                for (int j = 0; j < ds.Tables[2].Rows.Count; j++)
                                {
                                    if (ds.Tables[2].Rows.Count > 0)
                                    {
                                        countr++;
                                    }
                                }
                                if (ds.Tables[4].Rows.Count > 0)
                                {
                                    StatusBody = "<table class='table table-bordered table-striped' id='tblMarkEntryStatusCode' border='0' height='100px'>";
                                    for (int d = 0; d < ds.Tables[4].Rows.Count; d++)
                                    {
                                        StatusBody += "<tr><td>" + Convert.ToString(ds.Tables[4].Rows[d]["CODE_VALUE"]) + " : " + Convert.ToString(ds.Tables[4].Rows[d]["CODE_DESC"]) + " ( Please check Status Code checkbox & enter status code in Total Marks) <input type='hidden' id='hdfStatus_" + d + "' value='" + Convert.ToString(ds.Tables[4].Rows[d]["ID"]) + "'/></td></tr>";
                                        StatusCode += Convert.ToString(ds.Tables[4].Rows[d]["CODE_VALUE"]) + ',';
                                    }
                                    StatusBody += "</table>";
                                    ltrStatusCode.Text = StatusBody;
                                    StatusCode = StatusCode.TrimEnd(',');
                                    StatusCode = '"' + StatusCode + '"';
                                }
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    lblQuestionDetails.Text += Convert.ToString(ds.Tables[0].Rows[i]["QUESTIONWITHMAXMARKS"]) + " | ";
                                    lblQuestionDetails.Visible = true;
                                    if (lblQuestionDetails.Text.Contains("OR"))
                                    {
                                    }
                                }
                                lblQuestionDetails.Text = lblQuestionDetails.Text.Substring(0, lblQuestionDetails.Text.Length - 2);
                                lblQuestionDetails.Text = "Question Wise Mark " + ": " + lblQuestionDetails.Text;

                                if (countr > 0)
                                {
                                    btnLock.Disabled = false;
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        tblHeading = "<div style='overflow-y: auto;height: 400px;'><table id='tblStudentMarksEntry' class='table table-striped table-bordered' border='1' style='text-align: center;  border-collapse: collapse; width: 100%;' width='$( window ).width()'><thead><tr><th style='position: sticky;top: 0;background-color:#3c8dbc; color: White;'>Registration No.</th>";
                                        tblHeading += "<th style='position: sticky;top: 0;background-color:#3c8dbc; color: White; width=10px;'>Status Code</th>";
                                        //tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White;Z-Index:1080; '>";
                                        //tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White;Z-Index:1080; '>";
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                            {
                                                //tblHeading += "<th style='width:10%'>" + ds.Tables[0].Rows[i]["QUESTIONNO"].ToString() + "<input type='hidden' id='hdf_" + i + "' value='" + ds.Tables[0].Rows[i]["MAXMARKS"].ToString() + "'/><input type='hidden' id='hdfGroup_" + i + "' value='" + ds.Tables[0].Rows[i]["GROUPID"].ToString() +
                                                //    "'/><input type='hidden' id='hdfSubQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SUB_QUESTION"].ToString() + "'/><input type='hidden' id='hdfTotalQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["TotalQue"].ToString() +
                                                //    "'/><input type='hidden' id='hdfSolveQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SolveNoQue"].ToString() + "'/><input type='hidden' id='hdfMaxLevel_" + i + "' value='" + ds.Tables[0].Rows[i]["MAX_LEVEL"].ToString() + "'/><input type='hidden' id='hdfPgId_" + i + "' value='" + ds.Tables[0].Rows[i]["PG_ID"].ToString() + "'/><input type='hidden' id='hdfOQ_" + i + "' value='" + ds.Tables[0].Rows[i]["OQ"].ToString() + "'/></th>";
                                                //quecount++;

                                                tblHeading += "<th style='position: sticky;top: 0;background-color:#3c8dbc; color: White;'>" + ds.Tables[0].Rows[i]["QUESTIONNO"].ToString() + " " + "(" + ds.Tables[0].Rows[i]["MAXMARKS"].ToString() + ")" + "<input type='hidden' id='hdf_" + i + "' value='" + ds.Tables[0].Rows[i]["MAXMARKS"].ToString() + "'/><input type='hidden' id='hdfGroup_" + i + "' value='" + ds.Tables[0].Rows[i]["GROUPID"].ToString() +
                                                    "'/><input type='hidden' id='hdfSubQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SUB_QUESTION"].ToString() + "'/><input type='hidden' id='hdfTotalQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["TotalQue"].ToString() +
                                                    "'/><input type='hidden' id='hdfSolveQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SolveNoQue"].ToString() + "'/><input type='hidden' id='hdfMaxLevel_" + i + "' value='" + ds.Tables[0].Rows[i]["MAX_LEVEL"].ToString() + "'/><input type='hidden' id='hdfPgId_" + i + "' value='" + ds.Tables[0].Rows[i]["PG_ID"].ToString() + "'/><input type='hidden' id='hdfOQ_" + i + "' value='" + ds.Tables[0].Rows[i]["OQ"].ToString() + "'/></th>";
                                                //<input type='button' id='hdfOQ_" + i + "'/>
                                                // tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; font-size:12px; text-align:left; color: White; width: 190px; Z-Index:1080;'>" + "CO" + ds.Tables[5].Rows[i]["CO"].ToString() + "<br>" + "RBT" + ds.Tables[5].Rows[i]["Bloom"].ToString() + "</td>";
                                                quecount++;
                                            }
                                        }
                                        // tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White; text-align:left; Z-Index:1080;'>";
                                        tblHeading += "<th style='position: sticky;top: 0;background-color:#3c8dbc; color: White;'>Total Marks</th>";
                                        tblHeading += "</tr></thead>";
                                        tblMapping += tblHeading;


                                        //tblCORBT += "<thead><tr>";
                                        // if (ds.Tables[0].Rows.Count > 0)
                                        //{
                                        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        //    {

                                        //        tblCORBT += "<td style='position: sticky;top: 0;background-color:#FF0000; color: White;'>" + "(" + ds.Tables[5].Rows[i]["CO"].ToString() + ")" + " " + "(" + ds.Tables[5].Rows[i]["Bloom"].ToString() + ")'/></td>";

                                        //    }
                                        //}
                                        // tblCORBT += "</tr></thead>";


                                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                        {
                                            tblBody += "<tr><td style='width:10%;'>" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "<input type='hidden' id='hdfCourseRegistrationDetailsId' value='" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' /><input type='hidden' id='hdfExamPatternMappingId' value='" + ds.Tables[1].Rows[i]["EXAMPATTERNMAPPINGID"].ToString() + "' /></td>";
                                            tblBody += "<td style='width:10%;'><div class='checkbox checkbox-primary'>";
                                            for (int z = 0; z < ds.Tables[4].Rows.Count; z++)
                                            {
                                                if (Convert.ToString(ds.Tables[1].Rows[i]["TOTMARKS"]) != string.Empty)
                                                {
                                                    if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) == true && Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMARKS"]) == Convert.ToDecimal(ds.Tables[4].Rows[z]["CODE_VALUE"]))
                                                    {
                                                        tblBody += "<input type='checkbox' class='chkAbsentCls' checked='checked' id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px' disabled='disabled' onclick='disableRows(this);'/><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";
                                                        StatusCount = 1;
                                                        break;
                                                    }
                                                    else if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) != true && Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMARKS"]) == Convert.ToDecimal(ds.Tables[4].Rows[z]["CODE_VALUE"]))
                                                    {
                                                        tblBody += "<input type='checkbox' class='chkAbsentCls'  id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px' checked='checked'  onclick='disableRows(this);'  /><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";
                                                        StatusCount = 1;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    //tblBody += "<input type='checkbox' class='chkAbsentCls' onclick='disableRows(this);'  id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px' /><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";   

                                                }
                                            }
                                            if (StatusCount == 0 && Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) != true)
                                            {
                                                tblBody += "<input type='checkbox' class='chkAbsentCls' onclick='disableRows(this);'  id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px' /><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";
                                            }
                                            else if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"].ToString()) == true && StatusCount == 0)
                                            {
                                                tblBody += "<input type='checkbox' class='chkAbsentCls'  id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px' disabled='disabled' onclick='disableRows(this);'  /><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";

                                            }
                                            else
                                            {
                                                StatusCount = 0;
                                            }
                                            tblBody += "</div></td>";


                                            for (int S = 0; S < ds.Tables[0].Rows.Count; S++)
                                            {
                                                int DisableCount = 0;
                                                if (ds.Tables[0].Rows.Count > 0)
                                                {
                                                    DataTable dt = ds.Tables[2];

                                                    quemarks = Convert.ToString(checkMark(Convert.ToInt32(ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString()), Convert.ToInt32(ds.Tables[1].Rows[i]["IDNO"].ToString()), Convert.ToInt32(ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString()), dt));

                                                }
                                                for (int z = 0; z < ds.Tables[4].Rows.Count; z++)
                                                {
                                                    if (Convert.ToString(ds.Tables[1].Rows[i]["TOTMARKS"]) != string.Empty)
                                                    {
                                                        if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) != true && Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMARKS"]) == Convert.ToDecimal(ds.Tables[4].Rows[z]["CODE_VALUE"]))
                                                        {
                                                            tblBody += "<td><input type='text' disabled = 'disabled'  class='form-control quetext' maxlength='4'  id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                            DisableCount = 1;
                                                        }
                                                    }
                                                }


                                                if (DisableCount == 0 && Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"].ToString()) != true)
                                                {
                                                    string ckd = Convert.ToString(ds.Tables[0].Rows[S]["QUESTIONWITHMAXMARKS"]);
                                                    string ckd2 = null;
                                                    {
                                                        int b = (ds.Tables[0].Rows.Count - 1);

                                                        if (cnt < b)
                                                        {
                                                            ckd2 = Convert.ToString(ds.Tables[0].Rows[S + 1]["QUESTIONWITHMAXMARKS"]);
                                                        }
                                                        else
                                                        {
                                                            ckd2 = string.Empty;
                                                        }

                                                    }

                                                    cnt++;
                                                    tblBody += "<td><input type='text'  class='form-control quetext' maxlength='4' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /><input type='hidden' id='hdMaxMarks' value=" + ds.Tables[0].Rows[S]["MAXMARKS"].ToString().Replace(" M", "") + "/></td>";

                                                    //if (ckd2.Contains("OR"))
                                                    //{
                                                    //    tblBody += "<td><input type='text'  class='form-control quetext' maxlength='2' style='background-color:Yellow;' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //}
                                                    //else if (ckd.Contains("OR"))
                                                    //    {
                                                    //        tblBody += "<td><input type='text'  class='form-control quetext' maxlength='2' style='background-color:Yellow;' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    }
                                                    //else
                                                    //    {
                                                    //        tblBody += "<td><input type='text'  class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    }

                                                    //if (ckd.Contains("OR"))
                                                    //{
                                                    //    tblBody += "<td><input type='text'  class='form-control quetext' maxlength='2' style='background-color:Yellow;' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //}
                                                    //else
                                                    //{
                                                    //    tblBody += "<td><input type='text'  class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value=" + quemarks + " ><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //}
                                                }
                                                else if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"].ToString()) == true)
                                                {
                                                    string ckd = Convert.ToString(ds.Tables[0].Rows[S]["QUESTIONWITHMAXMARKS"]);
                                                    //string ckd2 = Convert.ToString(ds.Tables[0].Rows[S + 1]["QUESTIONWITHMAXMARKS"]);

                                                    //if (ckd2.Contains("OR"))
                                                    //{
                                                    //    tblBody += "<td><input type='text' disabled = 'disabled' style='background-color:powderblue;'  class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    count++;
                                                    //}
                                                    //else if (ckd.Contains("OR"))
                                                    //{
                                                    //    tblBody += "<td><input type='text' disabled = 'disabled' style='background-color:powderblue;'  class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    count++;
                                                    //}
                                                    //else
                                                    //{
                                                    //    tblBody += "<td><input type='text' disabled = 'disabled' class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    count++;
                                                    //}


                                                    tblBody += "<td><input type='text' disabled = 'disabled' class='form-control quetext' maxlength='4' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /><input type='hidden' id='hdMaxMarks' value=" + ds.Tables[0].Rows[S]["MAXMARKS"].ToString().Replace(" M", "") + "/></td>";
                                                    count++;


                                                    //if (ckd.Contains("OR"))
                                                    //{

                                                    //    tblBody += "<td><input type='text' disabled = 'disabled' style='background-color:powderblue;'  class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    count++;
                                                    //}
                                                    //else
                                                    //{
                                                    //    tblBody += "<td><input type='text' disabled = 'disabled' class='form-control quetext' maxlength='2' id='txtObtainedMarks_" + S + "_" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "' onkeypress='return isNumberKey(event)' value= " + quemarks + "><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[S]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                    //    count++;
                                                    //}
                                                }
                                                else
                                                {
                                                    DisableCount = 0;
                                                }
                                            }


                                            for (int z = 0; z < ds.Tables[4].Rows.Count; z++)
                                            {
                                                if (Convert.ToString(ds.Tables[1].Rows[i]["TOTMARKS"]) != string.Empty)
                                                {
                                                    if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) != true && Convert.ToDecimal(ds.Tables[1].Rows[i]["TOTMARKS"]) == Convert.ToDecimal(ds.Tables[4].Rows[z]["CODE_VALUE"]))
                                                    {
                                                        tblBody += "<td><input type='text' class='form-control MarksTotCls' maxlength='3' id='txtToT_" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' value='" + ds.Tables[1].Rows[i]["TOTMARKS"].ToString() + "' onkeypress='return isNumberKey(event)' onblur=' return QueStatusCheck(" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "," + StatusCode + ")'/></td>";
                                                        RowDisableCount = 1;
                                                    }
                                                }
                                            }
                                            if (RowDisableCount == 0 && Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"].ToString()) != true)
                                            {
                                                tblBody += "<td><input type='text' class='form-control MarksTotCls' maxlength='4' id='txtToT_" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' value='" + ds.Tables[1].Rows[i]["TOTMARKS"].ToString() + "' onkeypress='return isNumberKey(event)' disabled='disabled' onblur=' return QueStatusCheck(" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "," + StatusCode + ")'/></td>"; // disabled='disabled'

                                            }
                                            else if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"].ToString()) == true)
                                            {
                                                //string lblTest= Convert.ToString(ds.Tables[0].Rows[i]["QUESTIONWITHMAXMARKS"]);
                                                //if (lblTest.Contains("OR"))
                                                //{
                                                //    tblBody += "<td><input type='text' class='form-control MarksTotCls' maxlength='3' id='txtToT_" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' value='" + ds.Tables[1].Rows[i]["TOTMARKS"].ToString() + "' onkeypress='return isNumberKey(event)' disabled='disabled' onblur=' return QueStatusCheck(" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "," + StatusCode + ")'/></td>"; //
                                                //}
                                                //else {
                                                tblBody += "<td><input type='text' class='form-control MarksTotCls' maxlength='3' id='txtToT_" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' value='" + ds.Tables[1].Rows[i]["TOTMARKS"].ToString() + "' onkeypress='return isNumberKey(event)' disabled='disabled' onblur=' return QueStatusCheck(" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "," + StatusCode + ")'/></td>"; //
                                                //}

                                            }
                                            else
                                            {
                                                RowDisableCount = 0;
                                            }
                                            tblBody += "</tr>";
                                        }
                                    }
                                }
                                else
                                {
                                    btnLock.Disabled = true;
                                    tblHeading = "<div style='overflow-x: auto;white-space: nowrap; overflow-y: auto;height: 400px;'><table id='tblStudentMarksEntry' class='table table-striped table-bordered' border='1' style='text-align: center; border-collapse: collapse; width: 100%;'><thead><tr><th style='position: sticky;top: 0;background-color:#3c8dbc; color: White; width:18%'>Registration No.</th>";
                                    tblHeading += "<th style='width:10%; position: sticky;top: 0;background-color:#3c8dbc; color: White; width=10px;' >Status <br /> Code</th>";
                                    //tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White;Z-Index:1080; '>";
                                    //tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White;Z-Index:1080; '>";
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                        {
                                            tblHeading += "<th style='width:10%; position: sticky;top: 0;background-color:#3c8dbc; color: White;'>" + ds.Tables[0].Rows[i]["QUESTIONNO"].ToString() + " " + "(" + ds.Tables[0].Rows[i]["MAXMARKS"].ToString() + ")" + "<input type='hidden' id='hdf_" + i + "'value='" + ds.Tables[0].Rows[i]["MAXMARKS"].ToString() + "'/><input type='hidden' id='hdfGroup_" + i + "' value='" + ds.Tables[0].Rows[i]["GROUPID"].ToString() +
                                                "'/><input type='hidden' id='hdfSubQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SUB_QUESTION"].ToString() + "'/><input type='hidden' id='hdfTotalQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["TotalQue"].ToString() +
                                                "'/><input type='hidden' id='hdfSolveQuestion_" + i + "' value='" + ds.Tables[0].Rows[i]["SolveNoQue"].ToString() + "'/><input type='hidden' id='hdfMaxLevel_" + i + "' value='" + ds.Tables[0].Rows[i]["MAX_LEVEL"].ToString() + "'/><input type='hidden' id='hdfPgId_" + i + "' value='" + ds.Tables[0].Rows[i]["PG_ID"].ToString() + "'/><input type='hidden' id='hdfOQ_" + i + "' value='" + ds.Tables[0].Rows[i]["OQ"].ToString() + "'/></th>";
                                            //  tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; font-size:12px; text-align:left; color: White; width: 190px; Z-Index:1080;'>" + "CO" + ds.Tables[5].Rows[i]["CO"].ToString() + "<br>" + "RBT" + ds.Tables[5].Rows[i]["Bloom"].ToString() + "</td>";
                                            quecount++;
                                        }
                                    }
                                    //tblBody += "<td class='' style='position: sticky;top: 58px;background-color:#3c8dbc; color: White; text-align:left; Z-Index:1080;'>";
                                    tblHeading += "<th style='width:10%; position: sticky;top: 0;background-color:#3c8dbc; color: White;'>Total Marks</th>";
                                    tblHeading += "</tr></thead>";
                                    tblMapping += tblHeading;
                                    // tblBody = "<tbody>";
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        tblBody += "<tr><td style='width:10%;'>" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "<input type='hidden' id='hdfCourseRegistrationDetailsId' value='" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' /><input type='hidden' id='hdfExamPatternMappingId' value=" + ds.Tables[1].Rows[i]["EXAMPATTERNMAPPINGID"].ToString() + " /></td>";
                                        tblBody += "<td style='width:3%;'><div class='checkbox checkbox-primary'>";
                                        tblBody += "<input type='checkbox' class='chkAbsentCls' onclick='disableRows(this);'  id='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "' style='margin-left:5px'/><label for='chkAbsent" + ds.Tables[1].Rows[i]["ENROLLMENTNO"].ToString() + "'>&nbsp;</label>";
                                        tblBody += "</div></td>";
                                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                                        {
                                            if (Convert.ToBoolean(ds.Tables[1].Rows[i]["ISLOCK"]) == true)
                                            {
                                                tblBody += "<td> <input type='text' disabled = 'disabled'  class='form-control quetext' maxlength='4' id='txtObtainedMarks_" + j + "_" + ds.Tables[2].Rows[j]["COURSEREGISTRATIONDETAILSID"].ToString() + "' onkeypress='return isNumberKey(event)' maxLength='4' /><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[j]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                            }
                                            else
                                            {
                                                tblBody += "<td style='width:20px;'> <input type='text'  class='form-control quetext' maxlength='4' id='txtObtainedMarks_" + j + "_" + ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"].ToString() + "' onkeypress='return isNumberKey(event)' maxLength='4' /><input type='hidden' id='hdfPaperQuestionsId' value=" + ds.Tables[0].Rows[j]["PAPERQUESTIONSID"].ToString() + " /></td>";
                                                count = 0;
                                            }
                                        }

                                        tblBody += "<td><input type='text' class='form-control MarksTotCls' maxlength='3' id='txtToT_" + Convert.ToString(ds.Tables[1].Rows[i]["COURSEREGISTRATIONDETAILSID"]) + "' onkeypress='return isNumberKey(event)' disabled='disabled' onblur=' return QueStatusCheck(" + ds.Tables[1].Rows[i]["CourseRegistrationDetailsId"].ToString() + "," + StatusCode + ")' maxLength='5'/></td>"; //disabled='disabled'
                                        tblBody += "</tr>";
                                    }
                                }

                                tblBody += "</tbody>";
                                tblMapping += tblBody;
                                tblMapping += "</table></div>";
                                ltTable.Text = tblMapping;
                                btnOperation.Visible = true;
                                ltTable.Visible = true;
                                btnOperation.Visible = true;
                                ltrStatusCode.Visible = true;
                                divStatusCode.Visible = true;
                                if (ds.Tables[2].Rows.Count > 0)
                                {
                                    if (ds.Tables[2].Rows.Count == count)
                                    {
                                        btnSubmit.Visible = false;
                                        btnLock.Visible = false;
                                        divExcelExport.Visible = false;
                                    }
                                    else
                                    {
                                        btnSubmit.Visible = true;
                                        btnLock.Visible = true;
                                        divExcelExport.Visible = true;
                                    }
                                }
                                else
                                {
                                    btnSubmit.Visible = true;
                                    btnLock.Visible = true;
                                    divExcelExport.Visible = true;
                                }

                                //*******************NEW Added on 010922************
                                //int Temp_Type = Convert.ToInt32(objCommon.LookUp("TBLQUESTIONSOBTAINEDMARKS", "count(*)", "courseregistrationdetailsid = '" + Convert.ToString(ds.Tables[1].Rows[0]["COURSEREGISTRATIONDETAILSID"]) + "' and IsLock=" + 1 + ""));
                                //int Temp_Type = Convert.ToInt32(objCommon.LookUp("TBLQUESTIONSOBTAINEDMARKS T inner join TBLACDCOURSEREGISTRATIONMARKS TB on (T.CourseregistrationDetailsId=TB.CourseregistrationDetailsId)", "count(*)", "T.courseregistrationdetailsid = '" + Convert.ToString(ds.Tables[1].Rows[0]["COURSEREGISTRATIONDETAILSID"]) + "' and IsLock=" + 1 + "and ExamPatternMappingId=" + ds.Tables[1].Rows[0]["EXAMPATTERNMAPPINGID"].ToString() + ""));

                                int Temp_Type = Convert.ToInt32(objCommon.LookUp("TBLQUESTIONSOBTAINEDMARKS T inner join TBLACDCOURSEREGISTRATIONMARKS TB on (T.CourseregistrationDetailsId=TB.CourseregistrationDetailsId)", "count(*)", "T.courseregistrationdetailsid = '" + Convert.ToString(ds.Tables[1].Rows[0]["COURSEREGISTRATIONDETAILSID"]) + "' and IsLock=" + 1 + "and ExamPatternMappingId=" + ds.Tables[1].Rows[0]["EXAMPATTERNMAPPINGID"].ToString() + "and paperquestionsid=" + ds.Tables[0].Rows[0]["paperquestionsid"].ToString() + ""));

                                if (Temp_Type > 0)
                                {
                                    btnSubmit.Visible = false;
                                    btnLock.Visible = false;
                                    FileUpload1.Visible = false;
                                    btnImportExcel.Visible = false;
                                    btnDownloadExcelFormat.Visible = false;
                                    xls.Visible = false;
                                    btnUnLock.Visible = false;// change 
                                    if (Convert.ToInt32(Session["usertype"]) == 1)
                                    {
                                        btnUnLock.Visible = true;
                                        btnSubmit.Visible = false;
                                        btnLock.Visible = false;
                                        FileUpload1.Visible = false;
                                        btnImportExcel.Visible = false;
                                        btnDownloadExcelFormat.Visible = false;
                                        //btnDownloadExcel.Visible = false;
                                        xls.Visible = false;
                                    }
                                    else
                                    {
                                        btnSubmit.Visible = false;
                                        btnLock.Visible = false;
                                        FileUpload1.Visible = false;
                                        btnImportExcel.Visible = false;
                                        btnDownloadExcelFormat.Visible = false;
                                        xls.Visible = false;


                                    }


                                }
                                else
                                {

                                    btnSubmit.Visible = true;
                                    btnLock.Visible = true;
                                    FileUpload1.Visible = true;
                                    btnImportExcel.Visible = true;
                                    btnDownloadExcelFormat.Visible = true;
                                    xls.Visible = true;
                                    btnUnLock.Visible = false;

                                    if (Convert.ToInt32(Session["usertype"]) == 1)
                                    {
                                        btnUnLock.Visible = false;
                                        btnSubmit.Visible = true;//c
                                        btnLock.Visible = true;//c
                                        FileUpload1.Visible = true;//c
                                        btnImportExcel.Visible = true;//c
                                        btnDownloadExcelFormat.Visible = true;//c
                                        btnDownloadExcelFormat.Visible = true;//c
                                        xls.Visible = true;//c
                                    }
                                    else
                                    {
                                        btnSubmit.Visible = true;
                                        btnLock.Visible = true;
                                        FileUpload1.Visible = true;
                                        btnImportExcel.Visible = true;
                                        btnDownloadExcelFormat.Visible = true;
                                        xls.Visible = true;
                                        btnUnLock.Visible = false;
                                    }

                                }


                                //*************END**********


                            }
                            else
                            {
                                objCommon.DisplayMessage(this.Page, "Status Code Not Define !!!", this.Page);
                            }

                            lblTotalCount.Text = Convert.ToString(ds.Tables[1].Rows.Count);
                        }
                        else
                        {
                            //**********Added on 20072023**************
                            if (Convert.ToInt32(Session["usertype"]) == 1)
                            {
                                btnUnLock.Visible = false;
                                btnSubmit.Visible = false;
                                btnLock.Visible = false;
                                FileUpload1.Visible = false;
                                btnImportExcel.Visible = false;
                                btnDownloadExcelFormat.Visible = false;
                                btnDownloadExcel.Visible = false;
                                xls.Visible = false;
                            }
                            //******************************************
                            objCommon.DisplayMessage(this.Page, "NO Student Found For Mark Entry !!!", this.Page);

                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Question Pattern Not Found !!!", this.Page);
                    }
                }
                else
                {
                    divsubmarks.Visible = false;
                    btnOperation.Visible = false;
                    divExcelExport.Visible = false;
                    ltTable.Visible = false;
                    ltrStatusCode.Visible = false;
                    divStatusCode.Visible = false;

                    Label1.Visible = false;
                    lblTotalCount.Visible = false;
                    Label2.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private string checkMark(int QUESTIONID, int IDNO, int COURSEID, DataTable STUDENTMARK)
    {
        string value = "0";
        for (int i = 0; i < STUDENTMARK.Rows.Count; i++)
        {
            if (Convert.ToInt32(STUDENTMARK.Rows[i]["PAPERQUESTIONSID"]) == QUESTIONID && Convert.ToInt32(STUDENTMARK.Rows[i]["IDNO"]) == IDNO && Convert.ToInt32(STUDENTMARK.Rows[i]["COURSEREGISTRATIONDETAILSID"]) == COURSEID)
            {
                value = Convert.ToString(STUDENTMARK.Rows[i]["Que_marks"]);
                //return true;
                btnLock.Visible = true;
            }
        }
        return value;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SaveMarkEntry(int userno, string MarkEntryList, string TotMarksList, int LockStatus, int ExamNameId, string IpAddress, int Flag, string ElectType, string Ccode, int schemeno)
    {

        Object outval = 0;
        OBEMarkEntryController obeMark = new OBEMarkEntryController();
        var unQuotedString = MarkEntryList.TrimStart('"').TrimEnd('"');
        List<MarkEntryListDataModel> data = JsonConvert.DeserializeObject<List<MarkEntryListDataModel>>(unQuotedString);
        string json = JsonConvert.SerializeObject(data);
        DataTable dtMarkentryData = JsonConvert.DeserializeObject<DataTable>(json);

        var unQuotedString1 = TotMarksList.TrimStart('"').TrimEnd('"');
        List<TotMarksListDataModel> data1 = JsonConvert.DeserializeObject<List<TotMarksListDataModel>>(unQuotedString1);
        string json1 = JsonConvert.SerializeObject(data1);
        DataTable dtTotMarkData = JsonConvert.DeserializeObject<DataTable>(json1);

        if (ElectType != "2")
        {


            //outval = obeMark.SaveMarkEntry(userno, dtMarkentryData, dtTotMarkData, LockStatus, ExamNameId, IpAddress, Flag);
            outval = obeMark.SaveMarkEntry_Advance(userno, dtMarkentryData, dtTotMarkData, LockStatus, ExamNameId, IpAddress, Flag, schemeno);

        }
        else
        {

            outval = obeMark.SaveFreeElectMarkEntry(userno, dtMarkentryData, dtTotMarkData, LockStatus, ExamNameId, IpAddress, Flag, Ccode);
        }
        return JsonConvert.SerializeObject(outval);

    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = obeMarkEnrty.DownloadMarkEntryExcel(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(hdfSubjectId.Value), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfSectionId.Value), Convert.ToInt32(Session["userno"]));
        string SubjectName = string.Empty;
        string SessionName = string.Empty;
        string handle = Guid.NewGuid().ToString();
        int count = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = null;
            DataTable dtstudent = new DataTable("temptblStudentMarkEntryData");
            dtstudent.Columns.Add("Registration_Id", typeof(String));
            dtstudent.Columns.Add("Session", typeof(String));
            dtstudent.Columns.Add("Degree", typeof(String));
            dtstudent.Columns.Add("Branch", typeof(String));
            dtstudent.Columns.Add("Subject", typeof(String));
            dtstudent.Columns.Add("ExamName", typeof(String));
            dtstudent.Columns.Add("Semester", typeof(String));
            dtstudent.Columns.Add("Section", typeof(String));
            dtstudent.Columns.Add("RollNo", typeof(String));
            dtstudent.Columns.Add("Faculty", typeof(String));
            dtstudent.Columns.Add("[Student OBE Registration No]", typeof(String));
            dtstudent.Columns.Add("[Registration Number]", typeof(String));
            dtstudent.Columns.Add("StudentName", typeof(String));
            foreach (DataRow dr1 in ds.Tables[0].Rows)
            {
                dtstudent.Columns.Add("[" + dr1["QUESTIONWITHMAXMARKS"] + "]", typeof(String));
                count++;
            }
            dtstudent.Columns.Add("TOTAL", typeof(String));


            int i = 0;

            foreach (DataRow dr2 in ds.Tables[1].Rows)
            {
                SubjectName = dr2["SubjectName"].ToString();
                SessionName = dr2["SESSION_NAME"].ToString();
                dr = dtstudent.NewRow();
                DataView dvStudMarks = null;
                i += 1;
                dr["Registration_Id"] = dr2["CourseRegistrationId"];
                dr["Session"] = dr2["SESSION_NAME"];
                dr["Degree"] = dr2["DegreeName"];
                dr["Branch"] = dr2["LONGNAME"];
                dr["Subject"] = dr2["SubjectName"];
                dr["ExamName"] = ddlExamName.SelectedItem.Text; //SchemeMappingName exam name
                dr["Semester"] = dr2["SemesterName"];
                dr["Section"] = dr2["SectionName"];
                dr["RollNo"] = dr2["REGNO"];
                dr["Faculty"] = dr2["UA_FULLNAME"];
                dr["[Student OBE Registration No]"] = dr2["ENROLLMENTNO"];
                dr["[Registration Number]"] = dr2["REGNO"];
                dr["StudentName"] = dr2["STUDNAME"];
                dvStudMarks = new DataView(ds.Tables[2], "IDNO = " + dr2["IDNO"] + "", "PaperQuestionsId", DataViewRowState.CurrentRows);
                if (dvStudMarks.Count > 0)
                {
                    int j = 0;
                    foreach (DataRowView rowView in dvStudMarks)
                    {
                        dr[j + 13] = rowView["Que_marks"];
                        j += 1;
                    }
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        dr[j + 13] = "";
                    }
                }
                dr["TOTAL"] = dr2["TOTMARKS"];

                dtstudent.Rows.Add(dr);

            }



            var path = Server.MapPath("~/temp");
            #region Added condition as per Ticket Number 51203
            int Org = Convert.ToInt32(objCommon.LookUp("reff", "OrganizationId", ""));
            var fileName = string.Empty;
            if (Org == 5)
            {
                fileName = SessionName.Replace(' ', '_') + "_" + SubjectName.Replace(' ', '_').Replace(':', '-') + "_" + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xls";//"Spreadsheet.xlsx";
            }
            else
            {
                fileName = SessionName.Replace(' ', '_') + "_" + SubjectName.Replace(' ', '_').Replace(':', '-') + "_" + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xlsx";//"Spreadsheet.xlsx";
            }
            #endregion
            //var fileName = SessionName.Replace(' ', '_') + "_" + SubjectName.Replace(' ', '_').Replace(':', '-') + "_" + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xlsx";//"Spreadsheet.xlsx";

            // Create temp path if not exits
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            DataView dv = dtstudent.DefaultView;
            string SortColumn = "[[Registration Number]]";
            dv.Sort = SortColumn + " ASC";
            DataTable sortedDT = dv.ToTable();
            string fullPath = Path.Combine(path, fileName);

            GridView gvStudData = new GridView();
            gvStudData.DataSource = sortedDT;
            gvStudData.DataBind();
            string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            string attachment = fileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Response.Write(FinalHead);
            gvStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (ddlExamName.SelectedIndex > 0)
        {
            DataSet ds1 = obeMarkEnrty.GetSubjectDetails(Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SectionId"]));
            int CourseNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["COURSENO"]);

            DataSet ds = obeMarkEnrty.GetExamDetailsByPaperId(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(CourseNo), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()));

            string degree = Convert.ToString(ds.Tables[1].Rows[0]["DEGREENAME"]);
            string branch = Convert.ToString(ds.Tables[1].Rows[0]["LONGNAME"]);
            string semname = Convert.ToString(ds.Tables[2].Rows[0]["SEMFULLNAME"]);
            string scheme = Convert.ToString(ds.Tables[2].Rows[0]["SCHEMENO"]);
            string fieldname = Convert.ToString(ds.Tables[0].Rows[0]["FLDNAME"]);
            string examname = Convert.ToString(ds.Tables[0].Rows[0]["EXAMNAME"]);
            string ccode = Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]);
            int subid = Convert.ToInt32(ds1.Tables[0].Rows[0]["SUBID"]);
            string degree1 = Convert.ToString(ds1.Tables[0].Rows[0]["DEGREENAME"]);

            int elect_type = 0;
            if (scheme == string.Empty)
            {
                elect_type = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ELECT_TYPE", "CCODE = '" + Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]) + "'"));
            }
            else
            {
                elect_type = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ELECT_TYPE", "CCODE = '" + Convert.ToString(ds1.Tables[0].Rows[0]["CCODE"]) + "'and schemeno=" + Convert.ToInt32(scheme)));

            }
            if (elect_type != 2)
            {
                int status = 0;
                int schemeno = Convert.ToInt32(scheme);
                int courseno = Convert.ToInt32(Convert.ToInt32(ds1.Tables[0].Rows[0]["COURSENO"]));

                status = Convert.ToInt32(objCommon.LookUp("ACD_MSE_III_MARK_COMPARISON", "COUNT(*)", "SESSIONNO =" + ddlSession.SelectedValue + "AND SCHEMENO=" + schemeno + "AND COURSENO =" + courseno + "AND UA_NO = " + Convert.ToInt32(Session["userno"].ToString()) + "AND SUBID = " + Convert.ToInt32(ds1.Tables[0].Rows[0]["SUBID"]) + "AND SECTIONNO=" + Convert.ToString(ViewState["SectionId"])));
                if (status == 0)
                {

                    this.ShowMarkListReport("MarkList", "Marksheet_Report.rpt", degree, branch, courseno, fieldname, examname, semname, ccode, subid);   //before mark comparison    
                }
                else
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["FLDNAME"]) == "S1")
                    {
                        this.ShowMarkListReport("MarkList", "Marksheet_Report.rpt", degree, branch, courseno, fieldname, examname, semname, ccode, subid);   //before mark comparison   
                    }
                    else
                    {
                        this.ShowMarkListReport("MarkListAfterComparison", "Marksheet_After_Comp_Report.rpt", degree, branch, courseno, fieldname, examname, semname, ccode, subid);  // after mark comparison
                    }
                }
            }
            else
            {
                this.ShowFreeElectMarkListReport("MarkList", "FreeElect_Marksheet_Report.rpt", degree1, ccode, fieldname, examname, subid, semname);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Please Select Exam Name !!!", this.Page);
        }
    }

    private void ShowMarkListReport(string reportTitle, string rptFileName, string degree, string branch, int courseno, string fieldname, string examname, string semname, string ccode, int subid)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("obe")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_TERM=" + ddlSession.SelectedItem.Text.ToString() +
                ",@P_SECTIONNO=" + Convert.ToString(ViewState["SectionId"]) +
                ",username=" + Session["username"].ToString() +
                ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) +
                ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                ",@P_BRANCHNAME=" + branch.Replace("&", "and") +
                ",@P_DEGREENAME=" + degree.ToString() +
                ",@P_CCODE=" + ccode + ",@P_SUBID=" + Convert.ToInt32(subid) +
                ",@P_COURSENO=" + courseno +
                ",@P_SEMESTERNAME=" + semname + ",@P_EXAM=" + fieldname +
                ",@P_EXAMNAME=" + examname +
                ",IPADDRESS=" + ViewState["ipAddress"].ToString() +
                ",@P_TEACHERNAME=" + Session["userfullname"].ToString() +
                ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_hallticket.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowFreeElectMarkListReport(string reportTitle, string rptFileName, string degree, string ccode, string fieldname, string examname, int subid, string semname)
    {
        try
        {
            string CCODE = ccode;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_TERM=" + ddlSession.SelectedItem.Text.ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["SectionId"]) + ",username=" + Session["username"].ToString() + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENAME=" + degree.ToString() + ",@P_CCODE=" + CCODE + ",@P_SUBID=" + Convert.ToInt32(subid) + ",@P_SEMESTERNAME=" + semname + ",@P_EXAM=" + fieldname + ",@P_EXAMNAME=" + examname + ",IPADDRESS=" + ViewState["ipAddress"].ToString() + ",@P_TEACHERNAME=" + Session["userfullname"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //url += "&param=
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            // To open new window from Updatepanel
            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_hallticket.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDownloadExcelFormat_Click(object sender, EventArgs e)
    {
        DataSet ds = obeMarkEnrty.DownloadMarkEntryExcel(Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(hdfSubjectId.Value), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfSectionId.Value), Convert.ToInt32(Session["userno"]));
        string SubjectName = string.Empty;
        string SessionName = string.Empty;
        string handle = Guid.NewGuid().ToString();
        int count = 0;
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = null;
            DataTable dtstudent = new DataTable("temptblStudentMarkEntryData");
            dtstudent.Columns.Add("Registration_Id", typeof(String));
            dtstudent.Columns.Add("Session", typeof(String));
            dtstudent.Columns.Add("Degree", typeof(String));
            dtstudent.Columns.Add("Branch", typeof(String));
            dtstudent.Columns.Add("Subject", typeof(String));
            dtstudent.Columns.Add("ExamName", typeof(String));
            dtstudent.Columns.Add("Semeter", typeof(String));
            dtstudent.Columns.Add("Section", typeof(String));
            dtstudent.Columns.Add("[Roll No.]", typeof(String));
            dtstudent.Columns.Add("Faculty", typeof(String));
            dtstudent.Columns.Add("[Student OBE Registration No.]", typeof(String));
            dtstudent.Columns.Add("[Registration Number]", typeof(String));
            dtstudent.Columns.Add("StudentName", typeof(String));
            foreach (DataRow dr1 in ds.Tables[0].Rows)
            {
                dtstudent.Columns.Add("[" + dr1["QUESTIONWITHMAXMARKS"] + "]", typeof(String));
                count++;
            }
            dtstudent.Columns.Add("TOTAL", typeof(String));


            int i = 0;

            foreach (DataRow dr2 in ds.Tables[1].Rows)
            {
                SubjectName = dr2["SubjectName"].ToString();
                SessionName = dr2["SESSION_NAME"].ToString();
                dr = dtstudent.NewRow();
                DataView dvStudMarks = null;
                i += 1;
                dr["Registration_Id"] = dr2["CourseRegistrationId"];
                dr["Session"] = dr2["SESSION_NAME"];
                dr["Degree"] = dr2["DegreeName"];
                dr["Branch"] = dr2["LONGNAME"];
                dr["Subject"] = dr2["SubjectName"];
                dr["ExamName"] = ddlExamName.SelectedItem.Text; //SchemeMappingName exam name
                dr["Semeter"] = dr2["SemesterName"];
                dr["Section"] = dr2["SectionName"];
                dr["[Roll No.]"] = dr2["REGNO"];
                dr["Faculty"] = dr2["UA_FULLNAME"];
                dr["[Student OBE Registration No.]"] = dr2["ENROLLMENTNO"];
                dr["[Registration Number]"] = dr2["REGNO"];
                dr["StudentName"] = dr2["STUDNAME"];
                dvStudMarks = new DataView(ds.Tables[2], "IDNO = " + dr2["IDNO"] + "", "PaperQuestionsId", DataViewRowState.CurrentRows);
                if (dvStudMarks.Count > 0)
                {
                    int j = 0;
                    foreach (DataRowView rowView in dvStudMarks)
                    {
                        dr[j + 13] = rowView["Que_marks"];
                        j += 1;
                    }
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        dr[j + 13] = "";
                    }
                }
                dr["TOTAL"] = dr2["TOTMARKS"];

                dtstudent.Rows.Add(dr);

            }



            var path = Server.MapPath("~/temp");
            #region Added condition as per Ticket Number 51203
            int Org = Convert.ToInt32(objCommon.LookUp("reff", "OrganizationId", ""));
            var fileName = string.Empty;
            if (Org == 5)
            {
                fileName =// SessionName.Replace(' ', '_') + "_" + SubjectName.Replace(' ', '_').Replace(':', '-') + "_" + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xlsx";

               SessionName.Replace(' ', '_') + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xls";
            }
            else
            {
                fileName =// SessionName.Replace(' ', '_') + "_" + SubjectName.Replace(' ', '_').Replace(':', '-') + "_" + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xlsx";

               SessionName.Replace(' ', '_') + ddlExamName.SelectedItem.Text.Replace(' ', '_').Replace(':', '-') + ".xlsx";
            }
            #endregion

            // Create temp path if not exits
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            DataView dv = dtstudent.DefaultView;
            string SortColumn = "[[Registration Number]]";
            dv.Sort = SortColumn + " ASC";
            DataTable sortedDT = dv.ToTable();
            string fullPath = Path.Combine(path, fileName);
            //******************************************Added on 23022023**********************************
            // string fullPath = path+fileName;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(sortedDT);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    //Response.Flush();
                    Response.End();

                }
            }



            //*************************************************
            //GridView gvStudData = new GridView();
            //gvStudData.DataSource = sortedDT;
            //gvStudData.DataBind();
            //string FinalHead = @"<style>.FinalHead { font-weight:bold; }</style>";
            //string attachment = fileName;
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            //Response.ContentType = "application/vnd.MS-excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //Response.Write(FinalHead);
            //gvStudData.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();
        }
    }
    protected void btnImportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile.FileName.ToString() == string.Empty)
            {
                objCommon.DisplayMessage(this.Page, "Please Attach File. !!!", this.Page);
            }
            else
            {
                string fileName = FileUpload1.PostedFile.FileName.Trim();

                if (!System.IO.Directory.Exists(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"])))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"]));
                }

               string path = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["XlsFilePath"] + fileName); 

                DataTable dt = new DataTable();
                FileUpload1.SaveAs(path);
                string conString = string.Empty;
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName.Trim());
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        //conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                if (extension == ".xls")
                {
                    
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
                }
                else
                {
                    conString = String.Format(conString, path);
                }
          
                //  conString = String.Format(conString, path);
                //conString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0 Xml;HDR=YES;IMEX=1;", path);



                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    DataTable dtExcelSchema = new DataTable();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

                    dtExcelSchema = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * From [" + SheetName + "]", excel_con))
                    {
                        oda.Fill(dt);

                        DataSet ds = obeMarkEnrty.GetPaperQuestionsId(Convert.ToInt32(ddlExamName.SelectedValue));
                        string QuestionPaperId = ds.Tables[0].Rows[0]["EXAMPATTERNMAPPINGID"].ToString();

                        DataRow drMarkentryData = null;
                        DataRow drTotMarkData = null;
                        DataTable dtMarkentryData = new DataTable("dtMarkentryData");
                        dtMarkentryData.Columns.Add("QuestionPaperId", typeof(int));
                        dtMarkentryData.Columns.Add("SchemeSubjectId", typeof(int));
                        dtMarkentryData.Columns.Add("SessionId", typeof(int));
                        dtMarkentryData.Columns.Add("OrganizationId", typeof(int));
                        dtMarkentryData.Columns.Add("ExamPatternMappingId", typeof(int));
                        dtMarkentryData.Columns.Add("StudentId", typeof(String));
                        dtMarkentryData.Columns.Add("CourseRegistrationDetailsId", typeof(int));
                        dtMarkentryData.Columns.Add("Que_marks", typeof(String));
                        dtMarkentryData.Columns.Add("SequeceId", typeof(int));



                        DataTable dtTotMarkData = new DataTable("dtTotMarkData");
                        dtTotMarkData.Columns.Add("QuestionPaperId", typeof(int));
                        dtTotMarkData.Columns.Add("SchemeSubjectId", typeof(int));
                        dtTotMarkData.Columns.Add("SessionId", typeof(int));
                        dtTotMarkData.Columns.Add("OrganizationId", typeof(int));
                        dtTotMarkData.Columns.Add("ExamPatternMappingId", typeof(int));
                        dtTotMarkData.Columns.Add("StudentId", typeof(String));
                        dtTotMarkData.Columns.Add("CourseRegistrationDetailsId", typeof(int));
                        dtTotMarkData.Columns.Add("TotalMark", typeof(String));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int TotCount = dt.Columns.Count - 1;
                            StudentsMult stud = new StudentsMult();
                            stud.StudentId = Convert.ToString(dt.Rows[i][10]);

                            int temp = 0;
                            drTotMarkData = dtTotMarkData.NewRow();

                            drTotMarkData["QuestionPaperId"] = ddlExamName.SelectedValue;
                            drTotMarkData["SchemeSubjectId"] = hdfSubjectId.Value;
                            drTotMarkData["SessionId"] = ddlSession.SelectedValue;
                            drTotMarkData["OrganizationId"] = 0;
                            drTotMarkData["ExamPatternMappingId"] = QuestionPaperId;
                            drTotMarkData["StudentId"] = dt.Rows[i][10];
                            drTotMarkData["CourseRegistrationDetailsId"] = dt.Rows[i][0];
                            drTotMarkData["TotalMark"] = (dt.Rows[i][TotCount] == null || dt.Rows[i][TotCount] == "") ? "0" : dt.Rows[i][TotCount];

                            dtTotMarkData.Rows.Add(drTotMarkData);

                            for (int j = 13; j < dt.Columns.Count - 1; j++)
                            {
                                drMarkentryData = dtMarkentryData.NewRow();
                                drMarkentryData["QuestionPaperId"] = ddlExamName.SelectedValue;
                                drMarkentryData["SchemeSubjectId"] = hdfSubjectId.Value;
                                drMarkentryData["SessionId"] = ddlSession.SelectedValue;
                                drMarkentryData["OrganizationId"] = 0;
                                drMarkentryData["ExamPatternMappingId"] = QuestionPaperId;
                                drMarkentryData["StudentId"] = dt.Rows[i][10];
                                drMarkentryData["CourseRegistrationDetailsId"] = dt.Rows[i][0];
                                drMarkentryData["Que_marks"] = (dt.Rows[i][j] == null || dt.Rows[i][j] == "") ? "0" : dt.Rows[i][j]; //exam name
                                drMarkentryData["SequeceId"] = ds.Tables[0].Rows[temp]["PAPERQUESTIONSID"].ToString();  //dsExamQue.Tables[0].Rows[temp]["PAPERQUESTIONSID"];
                                dtMarkentryData.Rows.Add(drMarkentryData);
                                temp += 1;
                            }
                        }


                        CustomStatus cs;
                        OBEMarkEntryController obeMark = new OBEMarkEntryController();

                        cs = (CustomStatus)obeMark.ImportExcelData(Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["userno"]), Convert.ToString(Session["ipAddress"]), Convert.ToString(Session["macAddress"]), 0, dtMarkentryData, dtTotMarkData, Convert.ToInt32(ddlExamName.SelectedValue));

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this.Page, "Excel file Import Successfully. !!!", this.Page);
                            this.ddlExamName_SelectedIndexChanged(new object(), new EventArgs());
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Error in Import Excel file Please download proper Excel format. !!!", this.Page);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }


    private int checkMarkEntry()
    {
        int status1 = 0;
        bool S2Lock = false;
        bool S3Lock = false;
        bool T1Lock = false;
        bool T2Lock = false;
        bool T3Lock = false;
        bool T4Lock = false;
        try
        {
            DataSet ds1 = obeMarkEnrty.GetMarkEntryStatus(Convert.ToInt32(ViewState["SchemeSubjectId"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SectionId"]));

            if (ds1.Tables[0].Rows.Count > 0)
            {
                int CourseNo = Convert.ToInt32(ds1.Tables[0].Rows[0]["COURSENO"]);
                int SubId = Convert.ToInt32(ds1.Tables[0].Rows[0]["SUBID"]);

                if (SubId == 1)
                {
                    if (ds1.Tables[0].Rows[0]["LOCKS2"].ToString() == "")
                    {
                        S2Lock = false;
                    }
                    else
                    {
                        S2Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKS2"]);
                    }
                    if (ds1.Tables[0].Rows[0]["LOCKS3"].ToString() == "")
                    {
                        S3Lock = false;
                    }
                    else
                    {
                        S3Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKS3"]);
                    }
                    if (ddlExamName.SelectedItem.Text == "MSE-II")
                    {

                        if (S2Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSE-I.", this.Page);
                            status1 = 1;
                        }

                    }

                    if (ddlExamName.SelectedItem.Text == "MSE-III")
                    {
                        if (S2Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSE-I.", this.Page);
                            status1 = 1;
                        }
                        if (S3Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSE-II.", this.Page);
                            status1 = 1;
                        }
                    }
                }
                else if (SubId == 2)
                {
                    DataSet ds = obeMarkEnrty.GetPracticalMarkEntryStatus(Convert.ToInt32(ddlSession.SelectedValue), CourseNo, Convert.ToInt32(ViewState["SectionId"]));

                    if (ds1.Tables[0].Rows[0]["LOCKT1"].ToString() == "")
                    {
                        T1Lock = false;
                    }
                    else
                    {
                        T1Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKT1"]);
                    }
                    if (ds1.Tables[0].Rows[0]["LOCKT2"].ToString() == "")
                    {
                        T2Lock = false;
                    }
                    else
                    {
                        T2Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKT2"]);
                    }
                    if (ds1.Tables[0].Rows[0]["LOCKT3"].ToString() == "")
                    {
                        T3Lock = false;
                    }
                    else
                    {
                        T3Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKT3"]);
                    }
                    if (ds1.Tables[0].Rows[0]["LOCKT4"].ToString() == "")
                    {
                        T4Lock = false;
                    }
                    else
                    {
                        T4Lock = Convert.ToBoolean(ds1.Tables[0].Rows[0]["LOCKT4"]);
                    }
                    if (ddlExamName.SelectedItem.Text == "MSPA-2")
                    {
                        if (T1Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA1.", this.Page);
                            status1 = 1;
                        }
                    }
                    if (ddlExamName.SelectedItem.Text == "MSPA-3")
                    {
                        if (T1Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA1.", this.Page);
                            status1 = 1;
                        }
                        if (T2Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA2.", this.Page);
                            status1 = 1;
                        }
                    }
                    if (ddlExamName.SelectedItem.Text == "MSPA-4")
                    {
                        if (T1Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA1.", this.Page);
                            status1 = 1;
                        }
                        if (T2Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA2.", this.Page);
                            status1 = 1;
                        }
                        if (T3Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA3.", this.Page);
                            status1 = 1;
                        }
                    }
                    if (ddlExamName.SelectedItem.Text == "ESE-PR")
                    {
                        if (T1Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA1.", this.Page);
                            status1 = 1;
                        }
                        if (T2Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA2.", this.Page);
                            status1 = 1;
                        }
                        if (T3Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA3.", this.Page);
                            status1 = 1;
                        }
                        if (T4Lock == false)
                        {
                            objCommon.DisplayMessage(this.Page, "Mark entry not Locked for MSPA4.", this.Page);
                            status1 = 1;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            objCommon.ShowError(Page, "Academic_MarkEntry.checkMSPAMarkEntry --> " + e.Message + " " + e.StackTrace);
        }
        return status1;
    }
    protected void btnB_Click(object sender, EventArgs e)
    {

        pnlSubjectDetail.Visible = true;
        divSession.Visible = true;
        pnlStudentMarksEntry.Visible = false;
        divsubmarks.Visible = false;
        btnOperation.Visible = false;
        ltTable.Visible = false;
        ltrStatusCode.Visible = false;
        divStatusCode.Visible = false;
        divExcelExport.Visible = false;

    }
}