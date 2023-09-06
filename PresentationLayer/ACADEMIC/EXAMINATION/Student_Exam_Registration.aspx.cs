
//======================================================================================
// PROJECT NAME  : UAIMS [ARKA JAIN UNIVERSITY]                                                          
// MODULE NAME   : ACADEMIC/EXAMINATION                                                             
// PAGE NAME     : STUD EXAM REGISTRATION                                    
// CREATION DATE : 22-APRIL-2019
// CREATED BY    : MD. REHBAR SHEIKH                                     
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
using System.Xml.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class ACADEMIC_EXAMINATION_Student_Exam_Registration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentRegistration objSReg = new StudentRegistration();
    int cnt_registered = 0;
    int cnt_pending = 0;
    int cnt_total = 0;
    bool IsNotActivitySem = false;
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
        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;

                //Check for Activity On/Off for course registration.
                if (CheckActivity())
                {
                    this.PopulateDropDownList();
                    ViewState["action"] = "add";
                    ViewState["idno"] = "0";
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        divOptions.Visible = false;
                        ViewState["idno"] = Session["idno"].ToString();

                        this.ShowDetails();
                        BindStudentDetails();
                    }
                    else if (Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("7"))     //Admin OR Operator 
                    {
                        divOptions.Visible = true;
                        LoadAdminPanel();
                    }
                    else
                    {
                        divOptions.Visible = true;
                        LoadFacultyPanel();
                    }
                }
                else
                {
                    divCourses.Visible = false;

                    divOptions.Visible = false;
                }
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_ExamRegistration.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND AM.ACTIVITY_NO=" + Convert.ToInt32(ViewState["ACTIVITY_NO"]), "");
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            //ViewState["degreenos"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            //ViewState["branchnos"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["semesternos"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        //mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
    }

    private bool CheckActivity()
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();

        string sessionno = string.Empty;

        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA WITH (NOLOCK) INNER JOIN ACTIVITY_MASTER AM WITH (NOLOCK) ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.PAGE_LINK like '%' +  CAST('" + Convert.ToInt32(Request.QueryString["pageno"].ToString()) + "' AS NVARCHAR(5))  +'%'   AND SA.STARTED = 1 UNION ALL SELECT 0 AS SESSION_NO");
        //sessionno = Session["currentsession"].ToString();
        ViewState["Session"] = sessionno;
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            ViewState["ACTIVITY_NO"] = Convert.ToInt32(dtr["ACTIVITY_NO"]);
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void ShowDetails()
    {
        try
        {
            int sessionno = 0;
            if (Session["usertype"].ToString() == "2")
            {
                //sessionno = Convert.ToInt32(Session["currentsession"]);
               sessionno= Convert.ToInt32(ViewState["Session"]);
                //sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            }
            else
            {
                sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            }

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (S.SCHEMENO = SC.SCHEMENO) INNER JOIN ACD_ADMBATCH AM WITH (NOLOCK) ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG WITH (NOLOCK) ON (S.DEGREENO = DG.DEGREENO) LEFT JOIN ACD_PAYMENTTYPE P WITH (NOLOCK) ON(S.PTYPE=P.PAYTYPENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,P.PAYTYPENAME,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE ISNULL(S.PHYSICALLY_HANDICAPPED,0) WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            
            string examStandardFees = "0";
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                        lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                        lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                        lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                        lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();
                        lblPtype.Text = dsStudent.Tables[0].Rows[0]["PAYTYPENAME"].ToString();
                        lblSelectedCourseFee.Text = examStandardFees;
                        hdnSelectedCourseFee.Value = examStandardFees;
                        lblLateFine.Text = "0";
                        hdnLateFine.Value = "0";

                        lblTotalFee.Text = examStandardFees;
                        hdnTotalFee.Value = examStandardFees;

                        lblBacklogFine.Text = "0";
                        hdnBacklogFine.Value = "0";

                        tblInfo.Visible = true;
                        divCourses.Visible = true;
                        txtnonCBCSSem.Text = lblSemester.ToolTip;

                    //    int standardFeesCount = Convert.ToInt32(objCommon.LookUp("ACD_STANDARD_FEES", "COUNT(*) AS CNT",
                    //"DEGREENO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"]) +
                    //" AND BRANCHNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"]) +
                    //" AND BATCHNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"]) +
                    // " AND PAYTYPENO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"]) +
                    //" AND RECIEPT_CODE='EF'"));
                    //    ViewState["Count_IS_StandardFees"] = standardFeesCount;
                    //    if (standardFeesCount > 0)
                    //    {
                    //        examStandardFees = objCommon.LookUp("ACD_STANDARD_FEES", "SEMESTER" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]),
                    //            "DEGREENO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["DEGREENO"]) +
                    //            " AND BRANCHNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["BRANCHNO"]) +
                    //            " AND BATCHNO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["ADMBATCH"]) +
                    //            " AND PAYTYPENO=" + Convert.ToInt32(dsStudent.Tables[0].Rows[0]["PTYPE"]) +
                    //            " AND RECIEPT_CODE='EF'");
                    //        if (examStandardFees == "" || examStandardFees == null)
                    //        {
                    //            examStandardFees = "0";
                    //        }
                    //        lblSelectedCourseFee.Text = examStandardFees;
                    //        hdnSelectedCourseFee.Value = examStandardFees;
                    //        lblTotalFee.Text = examStandardFees;
                    //        hdnTotalFee.Value = examStandardFees;
                    //    }
                    //    else
                    //    {
                    //        examStandardFees = "0";
                    //        objCommon.DisplayMessage("Standard Fees Defination not found to get Exam Fees amount.Please Define Standard Fees", this.Page);
                    //        return;
                    //    }
                    }
                    else
                    {
                        IsNotActivitySem = true;
                    }
                  
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindStudentDetails()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
           // sessionno = Convert.ToInt32(Session["currentsession"]);
           // sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        btnSubmit.Visible = true;
        btnPrintRegSlip.Visible = true;

        BindAvailableCourseList();
        //BindStudentFailedCourseList();
        //BindReAppearedCourseList();
        //BindAuditCourseList();
        // Rohit Tiwari Discuss with umesh sir for Late Fee.(Need to Show "0.00")
        //lblLateFine.Text = "0.00";
        //hdnLateFine.Value = "0.00";
        string date = objCommon.LookUp("REFF WITH (NOLOCK)", "CONVERT(VARCHAR(12), UPTODATE, 103)UPTODATE", "");
        //DateTime PenaltyDate = string.IsNullOrEmpty(objCommon.LookUp("REFF", "CAST(UPTODATE AS DATE)UPTODATE", "")) ? (DateTime?)null : Convert.ToDateTime(txtDateAction.Text);

        DateTime PenaltyDate = Convert.ToDateTime(date); ;

        // objRef.PenaltyDate = Convert.ToDateTime(txtpenaltyDate.Text.Trim()); 
        //DataSet ds_LateFine = objSReg.Get_Courses_LateFine(PenaltyDate);

        //lblLateFine.Text = ds_LateFine.Tables[0].Rows[0]["LATE_FINE"].ToString();
        //hdnLateFine.Value = ds_LateFine.Tables[0].Rows[0]["LATE_FINE"].ToString();
        if (lvCurrentSubjects.Visible == true || lvBacklogSubjects.Visible == true || lvAuditSubjects.Visible == true)
        {
            btnSubmit.Visible = true;
        }
        else
        { btnSubmit.Visible = false; }

        //if (Session["usertype"].ToString().Equals("2"))     //Student 
        //{
        //    lblTotalFee.Text = Convert.ToString(lblTotalFee.Text);

           
        //}
       

        //Check current semester registered or not  //PREV_STATUS = 0 and 
        string count = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND IDNO=" + ViewState["idno"] + " AND (INCH_EXAM_REG = 1 OR STUD_EXAM_REGISTERED=1) AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip));
        if (count != "0")
        {
            objCommon.DisplayMessage(" Exam Registration Already Done.You Can Print Examination Form.", this.Page);
            if (Session["usertype"].ToString().Equals("2"))
            {
                btnSubmit.Enabled = false;
            }
            else
                btnSubmit.Enabled = true;
            btnSubmit.Visible = true;
            btnPrintRegSlip.Visible = true;
            btnPrintRegSlip.Enabled = true;
        }
        else
        {
            btnPrintRegSlip.Enabled = false;
        }
    }

    private void BindAvailableCourseList()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
           // sessionno = Convert.ToInt32(Session["currentsession"]);
            //sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        DataSet dsCurrCourses = null;
        //Show Current Semester Courses ..
        //dsCurrCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSENAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, ISNULL(REGISTERED,0)REGISTERED, ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + "AND ACCEPTED = 1 AND ISNULL(AUDIT_COURSE,0)=0 AND SESSIONNO = " + ddlSession.SelectedValue + "AND IDNO="+lblName.ToolTip, "C.CCODE"); /// + " AND C.OFFERED = 1"
        int schemeno = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SCHEMENO", "IDNO=" + Convert.ToInt32(ViewState["idno"])));
        int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", " ADMCAN=0 AND idno=" + Convert.ToInt32(ViewState["idno"])));
        dsCurrCourses = objSReg.GetStudentCoursesForRegularRegistration1(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(sessionno), schemeno, semester, 2);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                Label lblCourseno = item.FindControl("lblCCode") as Label;
                if (Session["usertype"].ToString() == "2")
                {
                    string count = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SEMESTERNO=" + semester + "  AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 ");
                    if (count == "0")
                    {
                        chk.Enabled = true;
                        btnPrintRegSlip.Enabled = false;
                    }
                    else
                    {
                        chk.Enabled = false;
                        chk.Checked=true;
                        
                    }
                }
                else
                {
                    btnPrintRegSlip.Enabled = true;
                    btnSubmit.Enabled = true;
                }
                if (Session["usertype"].ToString() == "1")
                {
                    string count1 = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND CCODE='" + lblCourseno.Text + "' AND  COURSENO=" + Convert.ToInt32(lblCourseno.ToolTip) + " AND SEMESTERNO=" + semester + "  AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1)");
                    if (count1 == "0")
                    {
                        chk.Checked = false;
                        btnPrintRegSlip.Enabled = false;
                    }
                    else
                    {
                        chk.Checked = true;
                        btnPrintRegSlip.Enabled = true;
                    }
                }
            }
              
            ////if (Session["usertype"].ToString().Equals("2"))     //Student 
            //if (lblTotalFee.Text.Equals("0.00") || lblTotalFee.Text.Equals("0"))
            //{
            //    lblCommanFee.Text = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
            //    hdnCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();

            //    lblLateFine.Text = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();
            //    hdnLateFine.Value = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();

            //}
            //hdnDefaultCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
        }
        else
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage("No Course found in Allotted Scheme and Semester.", this.Page);
        }
    }

    private void BindStudentFailedCourseList()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            sessionno = Convert.ToInt32(Session["currentsession"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        DataSet dsCurrCourses = null;
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", " ADMCAN=0 AND REGNO='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
        //Show Backlog Semester Courses ..
        dsCurrCourses = objSReg.GetStudentCoursesForBacklogRegistration1(sessionno, Convert.ToInt32(ViewState["idno"]), semesterno, 2);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            hdnIsbacklog.Value = "1";
            lvBacklogSubjects.DataSource = dsCurrCourses.Tables[0];
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
            //if (Session["usertype"].ToString().Equals("2"))     //Student 
            if (lblTotalFee.Text.Equals("0.00") || lblTotalFee.Text.Equals("0"))
            {
                //lblCommanFee.Text = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
                //hdnCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();

                //lblLateFine.Text = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();
                //hdnLateFine.Value = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();
                //lblBacklogFine.Text = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
                //hdnBacklogFine.Value = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
                //txtnew.Text = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
            }
            //hdnDefaultCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
            //lblBacklogFine.Text = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
            //txtnew.Text = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
        }
        else
        {
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
        }
    }

    private void BindReAppearedCourseList()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
          //  sessionno = Convert.ToInt32(Session["currentsession"]);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        DataSet dsReappearCourse = null;
        //Show ReAppeared Course List
        dsReappearCourse = objSReg.GetStudentCoursesForReAppearedCourseRegistration1(sessionno, Convert.ToInt32(ViewState["idno"]), 0, 2);
        if (dsReappearCourse != null && dsReappearCourse.Tables.Count > 0 && dsReappearCourse.Tables[0].Rows.Count > 0)
        {
            lvReAppearedCourse.DataSource = dsReappearCourse.Tables[0];
            lvReAppearedCourse.DataBind();
            lvReAppearedCourse.Visible = false;
        }
        else
        {
            lvReAppearedCourse.DataSource = null;
            lvReAppearedCourse.DataBind();
            lvReAppearedCourse.Visible = false;
        }
    }

    private void BindAuditCourseList()
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
           // sessionno = Convert.ToInt32(Session["currentsession"]);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        DataSet dsAuditCourse = null;
        //Show Audit Courses ..
        //dsAuditCourse = objCommon.FillDropDown("ACD_STUDENT_RESULT C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSENAME,C.SUBID,C.ELECT,0 as CREDITS,S.SUBNAME, ISNULL(REGISTERED,0)REGISTERED, ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED,DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER", "IDNO = " + ViewState["idno"] + " AND SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(ACCEPTED,0)=1 AND ISNULL(AUDIT_COURSE,0)=1 AND ISNULL(PREV_STATUS,0)=0", "C.CCODE");
        dsAuditCourse = objSReg.GetStudentCoursesForAuditRegistration1(Convert.ToInt32(ViewState["idno"]), sessionno, 2);
        if (dsAuditCourse != null && dsAuditCourse.Tables.Count > 0 && dsAuditCourse.Tables[0].Rows.Count > 0)
        {
            lvAuditSubjects.DataSource = dsAuditCourse.Tables[0];
            lvAuditSubjects.DataBind();
            lvAuditSubjects.Visible = false;
        }
        else
        {
            lvAuditSubjects.DataSource = null;
            lvAuditSubjects.DataBind();
            lvAuditSubjects.Visible = false;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            //sessionno = Convert.ToInt32(Session["currentsession"]);
            //sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_SEM=" + Convert.ToInt32(lblSemester.ToolTip);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindStudentList()
    {
        try
        {
            if (ddlSessionReg.SelectedValue == "0")
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                return;
            }

            StudentRegistration objSRegist = new StudentRegistration();
            DataSet dsStudent = objSRegist.GetCourseRegStudentList1(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["dec"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSessionReg.SelectedValue), 2);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = dsStudent.Tables[0];
                    lvStudent.DataBind();
                    lblRegistered.Text = cnt_registered.ToString();
                    lblPending.Text = cnt_pending.ToString();
                    lblTotal.Text = cnt_total.ToString();
                }
                else
                {
                    objCommon.DisplayMessage("Students Not Registered for selected Session.", this.Page);
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblRegistered.Text = string.Empty;
                    lblPending.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                }
            }
            else
            {
                objCommon.DisplayMessage("Students Not Registered for selected Session.", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void LoadAdminPanel()
    {
        if (Session["usertype"].ToString().Equals("7") || Session["usertype"].ToString().Equals("1"))
        {
            //objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = " + Request.QueryString["pageno"].ToString() + ")", "SESSIONNO DESC");
            //  objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            if (ddlSessionReg.Items.Count > 1)
            {
                ddlSessionReg.SelectedIndex = 1;
            }
        }
        rblOptions.SelectedValue = "S";
        divOptions.Visible = false;
        txtRollNo.Text = string.Empty;

        divCourses.Visible = true;
        pnlDept.Visible = false;
        btnBackHOD.Visible = false;
        tblInfo.Visible = false;
        tblSession.Visible = true;
    }

    private void LoadFacultyPanel()
    {
        if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("7"))   ///|| Session["usertype"].ToString().Equals("1")
        {
            //objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = " + Request.QueryString["pageno"].ToString() + ")", "SESSIONNO DESC");
            // objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            if (ddlSessionReg.Items.Count > 1)
            {
                ddlSessionReg.SelectedIndex = 1;
            }
            BindStudentList();
        }
        rblOptions.SelectedValue = "M";

        divCourses.Visible = false;
        pnlDept.Visible = true;
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        PopulateDropDownList();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        int sessionno = 0;
        if (Session["usertype"].ToString() == "2")
        {
            //sessionno = Convert.ToInt32(Session["currentsession"]);
           // sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            sessionno = Convert.ToInt32(ViewState["Session"]);
        }
        else
        {
            sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        }

        string idno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'" + "and admcan=0");

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found For Entered Enrollment No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            if ((Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("7")) ? true : ValidateFacultyAdvisor())
            {
                //Check current semester applied or not
                
                ViewState["action"] = "edit";
                this.ShowDetails();
                if (IsNotActivitySem == true)
                {
                    objCommon.DisplayMessage("Activity Is Not Started For This Semester Student.", this.Page);
                    return;
                }
                else
                {
                    string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(REGISTERED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(sessionno) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip));
                    if (applyCount == "0")
                    {
                        //objCommon.DisplayMessage("Student with registration No. [" + txtRollNo.Text.Trim() + "] has not applied for selected session exam. \\nBut you can directly register him.", this.Page);
                        objCommon.DisplayMessage("Course Registration Not Found For Student With Enrollment No. [" + txtRollNo.Text.Trim() + "] For Selected Session,Scheme And Semester.", this.Page);
                        return;
                    }
                    BindStudentDetails();
                    btnBackHOD.Visible = false;
                    txtRollNo.Enabled = false;
                    ddlSession.Enabled = false;
                    rblOptions.Enabled = false;
                }
            }
        }
    }

    private bool ValidateFacultyAdvisor()
    {
        bool ret = true;
        //Validate Faculty Advisor
        int facAdvisor = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "ISNULL(FAC_ADVISOR,0)FAC_ADVISOR", "IDNO=" + ViewState["idno"].ToString()));

        if (facAdvisor != Convert.ToInt32(Session["userno"]))
        {
            objCommon.DisplayMessage("You did not have the permission to change selected student registration.\\nOnly alloted faculty advisor can do this.", this.Page);
            ret = false;
        }
        return ret;
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("ExamRegistrationSlip", "rptExamination_Form.rpt");
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        Student objS = new Student();
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "BRANCHNO", "REGNO='" + lblEnrollNo.Text + "'"));
        int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "DEGREENO", "REGNO='" + lblEnrollNo.Text + "'"));
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", "REGNO='" + lblEnrollNo.Text + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "ADMBATCH", "REGNO='" + lblEnrollNo.Text + "'"));
        int paymenttypeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
        //lblEnrollNo

        try
        {
            demandCriteria.StudentId = Convert.ToInt32(ViewState["idno"]);
            demandCriteria.StudentName = lblName.Text;
           // demandCriteria.SessionNo = Convert.ToInt32(Session["currentsession"]);
            demandCriteria.SessionNo = Convert.ToInt32(ViewState["Session"]);
            demandCriteria.ReceiptTypeCode = "EF";
            demandCriteria.BranchNo = branchno;
            demandCriteria.DegreeNo = degreeno;
            demandCriteria.SemesterNo = semesterno;
            demandCriteria.AdmBatchNo = admbatch;
            demandCriteria.PaymentTypeNo = paymenttypeno;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EaxmRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int count_Backlog_idno = 0;
        int count_Regular_idno = 0;

        try
        {
            int sem = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", "REGNO='" + lblEnrollNo.Text + "'"));
            //string exmcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND INCH_EXAM_REG = 1 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //if (exmcount != "0")
            //{
            //    objCommon.DisplayMessage("Exam Registration Is Already Done!.", this.Page);
            //    btnSubmit.Visible = false;
            //    return;
            //}
            //if (Convert.ToInt32(ViewState["Count_IS_StandardFees"]) == 0)
            //{
            //    objCommon.DisplayMessage("Standard Fees Defination not found to get Exam Fees amount.Please Define Standard Fees", this.Page);
            //    btnSubmit.Visible = false;
            //    return;
            //}
            
            StudentRegist objSR = new StudentRegist();

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    //string amt = (dataitem.FindControl("hdnCourseRegister") as HiddenField).Value.Trim();
                }
            }
            //foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {
            //        objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            //        //string amt = (dataitem.FindControl("hdnBacklogCourse") as HiddenField).Value.Trim();
            //        objSR.Backlogfees = Convert.ToDecimal(hdnBacklogFine.Value);
            //        //lblBacklogFine.Text = txtnew.Text.Trim() + objSR.CourseFee;
            //    }
            //}
            //foreach (ListViewDataItem dataitem in lvReAppearedCourse.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {
            //        objSR.Re_Appeared = objSR.Re_Appeared + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            //        string amt = (dataitem.FindControl("hdnReAppearedCourse") as HiddenField).Value.Trim();
            //        objSR.CourseFee = objSR.CourseFee + (amt != string.Empty ? Convert.ToDecimal(amt) : 0);
            //    }
            //}
            //foreach (ListViewDataItem dataitem in lvAuditSubjects.Items)
            //{
            //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
            //    {
            //        objSR.Audit_course = objSR.Audit_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
            //        string amt = (dataitem.FindControl("hdnAuditCourse") as HiddenField).Value.Trim();
            //        objSR.CourseFee = objSR.CourseFee + (amt != string.Empty ? Convert.ToDecimal(amt) : 0);
            //    }
            //}

            if (objSR.COURSENOS != null)
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            else
            {
                objSR.COURSENOS = "";
            }
            //objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
            //objSR.Re_Appeared = objSR.Re_Appeared.TrimEnd('$');
            //objSR.Audit_course = objSR.Audit_course.TrimEnd('$');

                objSR.EXAM_REGISTERED = 0;

            if (objSR.COURSENOS.Length > 0)
            {
                string studentIDs = lblName.ToolTip;

                //Add registered 
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = lblEnrollNo.Text.Trim();
                objSR.ROLLNO = txtRollNo.Text.Trim();
                //objSR.CommanFee = hdnCommanFee.Value.Trim() != string.Empty ? Convert.ToDecimal(hdnCommanFee.Value) : 0;
                //objSR.LateFine = hdnLateFine.Value.Trim() != string.Empty ? Convert.ToDecimal(hdnLateFine.Value) : 0;
                //objSR.CommanFee = lblCommanFee.Text.Trim() != string.Empty ? Convert.ToDecimal(lblCommanFee.Text) : 0;
                // objSR.LateFine = lblLateFine.Text.Trim() != string.Empty ? Convert.ToDecimal(lblLateFine.Text) : 0;
                objSR.CourseFee = Convert.ToDecimal(lblSelectedCourseFee.Text);
                objSR.TotalFee = objSR.CourseFee;
                objSR.ReceiptFlag = "EXM";
                int paymenttypenoOld = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
              //  FeeDemand demandCriteria = this.GetDemandCriteria();
                int ret = objSReg.AddExamRegiSubjects1(objSR);

                if (ret == 1)
                {
                    int semno = Convert.ToInt32(lblSemester.ToolTip);

                    decimal amount = txtTEAmount.Text.Trim() != string.Empty ? Convert.ToDecimal(txtTEAmount.Text) : 0;
                    int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (S.SCHEMENO=SC.SCHEMENO)", "SCHEMETYPE", "regno='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
                    decimal total = 0;
                    //if ((semno == 1 && schemetype == 1) || (semno == 2 && schemetype == 1) || (semno == 3 && schemetype == 1) || (semno == 4 && schemetype == 1))
                    //{
                    //    total = objSR.CourseFee + objSR.CommanFee + objSR.LateFine + objSR.Backlogfees;
                    //}
                    //else
                    //{
                        //count_Backlog_idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "count(isnull(IDNO,0))IDNO", "IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND PREV_STATUS=1 "));
                    count_Regular_idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(ISNULL(IDNO,0))IDNO", "IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (PREV_STATUS=0 OR  PREV_STATUS is null)"));

                        //// For Regular And Backlog Student  
                        //if (count_Backlog_idno > 0 && count_Regular_idno > 0)
                        //{
                        //    total = amount + objSR.LateFine + objSR.Backlogfees + objSR.CourseFee;
                        //}
                        //// For Backlog Student  
                        //else if (count_Backlog_idno > 0)
                        //{
                        //    total = objSR.LateFine + objSR.Backlogfees + objSR.CourseFee;
                        //}

                        //// For Regular Student
                        //else
                        //{
                            total =objSR.CourseFee;
                        //}
                    //}

                   // feeController.CreateDemandForExamination(demandCriteria, paymenttypenoOld, lblEnrollNo.Text.Trim(), total);
                    if ((Session["usertype"].ToString().Equals("2"))) //student
                    {
                        objCommon.DisplayMessage("Exam Registration Done.You Can Print Examination Form.", this.Page);

                        objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.You Can Print Examination Form.", this.Page);

                        objCommon.DisplayMessage("Provisional Exam Registration Done  .You Can Print Examination Form.", this.Page);

                        foreach (ListViewItem item in lvCurrentSubjects.Items)
                        {
                            CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                            Label lblCourseno = item.FindControl("lblCCode") as Label;


                            string count = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(1)", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND (ISNULL(STUD_EXAM_REGISTERED,0)=1 OR ISNULL(INCH_EXAM_REG,0)=1)");
                            if (count == "0")
                            {
                                chk.Enabled = true;
                                btnPrintRegSlip.Enabled = false;
                            }
                            else
                            {
                                chk.Enabled = false;
                                btnSubmit.Enabled = false;
                                
                            }
                        }
                    }

                    else
                    {
                        //objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.You Can Print Examination Form.", this.Page);
                        objCommon.DisplayMessage("Exam Registration Done .You Can Print Examination Form.", this.Page);
                       
                        objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.You Can Print Examination Form.", this.Page);
                        //objCommon.DisplayMessage("Provisional Exam Registration Done and Exam Fees Demand Created Successfully.You Can Print Examination Form.", this.Page);
                        objCommon.DisplayMessage("Provisional Exam Registration Done .You Can Print Examination Form.", this.Page);
                    }
                    //btnSubmit.Visible = false;
                    btnPrintRegSlip.Enabled = true;
                    txtRollNo.Enabled = true;
                    // ShowReport("ExamRegistrationSlip", "rptExamRegForm.rpt");
                }
                else
                {
                    objCommon.DisplayMessage("Error! in saving record.", this.Page);
                }
                if ((Session["usertype"].ToString().Equals("1")))
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select atleast One Course in course list for Exam Registration.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}