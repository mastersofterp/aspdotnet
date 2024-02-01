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
using System.Net;
using System.Linq;
using ClosedXML.Excel;
using System.IO;

public partial class ACADEMIC_AuditCourseType : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    int retCnt;
    int oldsem;
    int oldyear;


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
                if (Session["usertype"].ToString().Equals("2"))
                {
                    if (CheckActivity())
                    // if (1==1)
                    {
                        ////Page Authorization
                        this.CheckPageAuthorization();
                        // this.CheckSchemeAllotment();
                        //Set the Page Title
                        Page.Title = Session["coll_name"].ToString();

                        string host = Dns.GetHostName();
                        IPHostEntry ip = Dns.GetHostEntry(host);
                        string IPADDRESS = string.Empty;
                        btnPrintChallan.Visible = false;
                        btnPrePrintClallan.Visible = false;
                        IPADDRESS = ip.AddressList[0].ToString();
                        //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                        ViewState["ipAddress"] = IPADDRESS;
                        //check activity for course registration.
                        //Check for Activity On/Off
                        this.binddetails();
                    }
                }
                else
                {

                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO    ");
                    //this.objCommon.FillDropDownList(ddlofSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "", "SEMESTERNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);

                    //divsteps.Visible = false;
                    updEdit.Visible = true;
                }
            }
        }

        divMsg.InnerHtml = string.Empty;
        objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  


        //if (Convert.ToInt32(Session["usertype"]) == 8 || Convert.ToInt32(Session["usertype"]) == 1)
        //    {
        //    btnExcelReport.Visible = true;
        //    }
        //else
        //    {
        //    btnExcelReport.Visible = false;

        //    }


    }

    private void CheckSchemeAllotment()
    {
        try
        {
            int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(SCHEMENO,0) AS SCHEMENO", "IDNO = " + Convert.ToInt32(Session["idno"]) + ""));
            int sectionno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(SECTIONNO,0) AS SECTIONNO", "IDNO = " + Convert.ToInt32(Session["idno"]) + ""));
            if (Schemeno == 0 || sectionno == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Scheme/Section Allotment not Done. Please contact to Administrator.'); window.location='" + Page.ResolveUrl("~/notauthorized.aspx?page=CourseRegistration.aspx") + "'", true);
            }
        }
        catch
        {
            throw;
        }
    }

    private void PopulateDropDownList()
    {


        int collegeId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + ""));
        ViewState["collegeId"] = collegeId;
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM ACD_COURSE_REG_CONFIG_ACTIVITY WHERE STARTED = 1 AND SHOW_STATUS =1) AND COLLEGE_ID = " + collegeId + "", "SESSIONNO DESC");
        if(ddlSession.Items.Count > 0)
        ddlSession.SelectedIndex = 1;
        // mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
    }

    private void binddetails()
    {
        if (Convert.ToInt32(Session["usertype"]) == 2 || Convert.ToInt32(Session["usertype"]) == 1)
        {
            this.PopulateDropDownList();
            DataSet ds = objSReg.Get_Student_Details_for_Course_Registration(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToInt32(Session["usertype"]),5);
            ViewState["DataSet_Student_Details"] = ds;
            if (CheckActivity(ds))
                divNote.Visible = true;
            else
            {
                divNote.Visible = false;
                divCourses.Visible = false;
            }
        }
        else
        {

            this.PopulateDropDownList();
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO = R.SESSIONNO)", "DISTINCT M.SESSIONNO", "M.SESSION_PNAME", "M.SESSIONNO > 0", "M.SESSIONNO DESC");
            divNote.Visible = false;
            divCourses.Visible = true;
            // return;
        }
    }

    private bool CheckActivity(DataSet ds)
    {
        bool ret = true;
        ActivityController objActController = new ActivityController();
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //{
        //    if (ds.Tables[0].Rows[0]["STARTED"].ToString().ToLower().Equals("false"))
        //    {
        //        objCommon.DisplayMessage(UpdatePanel1, "This Activity has been Stopped. Contact Admin.!!", this.Page);
        //        ret = false;
        //    }
        //    if (ds.Tables[0].Rows[0]["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
        //    {
        //        objCommon.DisplayMessage(UpdatePanel1, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
        //        ret = false;
        //    }
        //}
        //else
        //{
        //    objCommon.DisplayMessage(UpdatePanel1, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
        //    ret = false;
        //}
        return ret;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (txtRollNo.Text == string.Empty)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Please Enter Registration No...!!", this.Page);
            return;
        }
        string idno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
        if (idno == "")
        {
            //objCommon.DisplayMessage("Student Not Found having Roll. No.'" + txtRollNo.Text.Trim() + "'", this.Page);
            objCommon.DisplayMessage(UpdatePanel1, "Student Not Found for This Registration No...!!", this.Page);
            return;
        }
        string count = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0");
        if (count != "0")
        {
            objCommon.DisplayMessage(UpdatePanel1, "Student Course Registration already done..!!", this.Page);
            this.ShowDetails();
            lvCurrentSubjects.Enabled = false;
            lvUniCoreSub.Enabled = false;
            lvGlobalSubjects.Enabled = false;
            btnSubmit.Enabled = false;
            btnPrintRegSlip.Enabled = true;
        }
        else
        {
            this.ShowDetails();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //DataTable dt = null;
        // btnExcelReport.Visible = false;
        double TotRegCreditsCount = 0;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        int login_Attempt = 0;
        int maxLoginAttempt = 0;
        int electiveregcount = 0;
        int globalregcount = 0;
        int valueregcount = 0;
        int idno = Convert.ToInt32(lblName.ToolTip);

        int semesterconfig = Convert.ToInt32(objCommon.LookUp("ACD_SEMESTER_CONFIG_AUDIT_MODE", "SEMESTERNO", "SCHEMENO =" + lblScheme.ToolTip + ""));
        int semesterno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno + ""));
        if (semesterno < semesterconfig)
        {
            objCommon.DisplayMessage(UpdatePanel1, "Only " + semesterconfig + "th Sem on-wards student can apply for Audit Course Type registration..!!", this.Page);
            return;
        }
        if (Session["usertype"].ToString() != "1")
        {
            int semesterpervalid = Convert.ToInt32(objCommon.LookUp("ACD_REGIST_SUBJECTS_AUDIT_FLAG", "COUNT(IDNO)", "IDNO=" + idno + " AND SEMESTERNO =" + lblSemester.ToolTip + ""));
            if (semesterpervalid >= 1)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Only one Courses can take under Audit during one Semester..!!", this.Page);
                return;
            }

            int semestervalid = Convert.ToInt32(objCommon.LookUp("ACD_REGIST_SUBJECTS_AUDIT_FLAG", "COUNT(IDNO)", "IDNO=" + idno + ""));
            if (semestervalid >= 2)
            {
                objCommon.DisplayMessage(UpdatePanel1, "Only two Courses can take under Audit during entire program..!!", this.Page);
                return;
            }
        }
        else
        {
            foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;
                    //electiveregcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                    //                                                       "R.COURSENO = " + Convert.ToInt32(Code) +
                    //                                                       "AND ISNULL(R.CANCEL,0)= 0 AND R.IDNO = " + Convert.ToInt32(idno)));
                    electiveregcount = 1;
                }
            }
            foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;
                    //globalregcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                    //                                                       "R.COURSENO = " + Convert.ToInt32(Code) +
                    //                                                       "AND ISNULL(R.CANCEL,0)= 0 AND R.IDNO = " + Convert.ToInt32(idno) + " AND R.SESSIONNO IN(SELECT SESSIONNO FROM ACD_GLOBAL_OFFERED_COURSE WHERE R.COURSENO = COURSENO AND ISNULL(GLOBAL_OFFERED,0)=1)"));
                    globalregcount = 1;
                }
            }
            foreach (ListViewDataItem dataitem in lvValueAdded.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;

                    valueregcount = 1;
                }
            }
            if (ds != null && ds.Tables[7].Rows.Count > 0)
            {
                DataRow[] dr = null;
                string coursePattern = ds.Tables[0].Rows[0]["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"].ToString();
                if (!string.IsNullOrEmpty(coursePattern) && (coursePattern.Contains("2") || coursePattern.Contains("3")))
                {
                    if (lvUniCoreSub.Enabled == true)
                    {
                        maxLoginAttempt = Convert.ToInt32(ds.Tables[0].Rows[0]["MAX_LOGIN_ATTEMPT"]);
                    }
                    dr = ds.Tables[7].Select("COURSE_PATTERN =2");
                    if (dr != null && dr.Count() > 0)
                        login_Attempt = Convert.ToInt32(dr[0]["LOGIN_ATTEMPT"]);
                    if (electiveregcount > 0)
                    {
                    }
                    else
                    {

                    }

                    dr = ds.Tables[7].Select("COURSE_PATTERN =3");
                    if (dr != null && dr.Count() > 0)
                        login_Attempt = Convert.ToInt32(dr[0]["LOGIN_ATTEMPT"]);
                    if (globalregcount > 0)
                    {
                    }
                    else
                    {
                        if (maxLoginAttempt != 0)
                        {

                        }
                    }
                }
            }
            int status = 0;
            foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)

                    status++;
            }
            foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)

                    status++;
            }
            foreach (ListViewDataItem dataitem in lvValueAdded.Items)
            {
                CheckBox chk = dataitem.FindControl("chkAccept") as CheckBox;
                if (chk.Checked == true)

                    status++;
            }

            if (status == 1)
            {
                string studentIDs = lblName.ToolTip;
                //Add registered 
                StudentRegist objSR = new StudentRegist();
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = lblEnrollNo.Text.Trim();
                objSR.ROLLNO = txtRollNo.Text.Trim();
                objSR.Audit_course = "0";
                objSR.EXAM_REGISTERED = 1;  ////// Update as REGISTERED =1;
                int coreSubj = 0;
                int UniCoreSub = 0;
                int GlobalSubjects = 0;
                int ValueAddedSubj = 0;
                int coreSubjcount = 0;
                int UniCoreSubcount = 0;
                int GlobalSubjectscount = 0;
                int ValueAddedSubjcount = 0;
                //foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
                //{
                //    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                //    {
                //        string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                //        TotRegCreditsCount += Convert.ToDouble(Credits);
                //        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                //        objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                //        coreSubj = 1;
                //        coreSubjcount = coreSubjcount+1;
                //        if (coreSubjcount >1)
                //        {
                //            objCommon.DisplayMessage(UpdatePanel1, "Please Select only one audit course for particular semester..!!", this.Page);
                //            return;
                //        }
                //        if (ValueAddedSubj != 0 && UniCoreSub != 0 && GlobalSubjects != 0)
                //        {
                //            objCommon.DisplayMessage(UpdatePanel1, "Please Select only One Course from different course list for Course Registration..!!", this.Page);
                //            return;
                //        }
                //    }
                //}

                foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                        TotRegCreditsCount += Convert.ToDouble(Credits);
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                        UniCoreSub = 2;
                        UniCoreSubcount = UniCoreSubcount + 1;
                        if (UniCoreSubcount > 1)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only one audit course for particular semester..!!", this.Page);
                            return;
                        }
                        if (ValueAddedSubj != 0 && coreSubj != 0 && GlobalSubjects != 0)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only One Course from different course list for Course Registration..!!", this.Page);
                            return;
                        }
                    }
                }

                foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        //string Code = (dataitem.FindControl("lblCCode") as Label).ToolTip;
                        //string maxSeatsForGLobalSubj = objCommon.LookUp("ACD_GLOBAL_OFFERED_COURSE", "ISNULL(CAPACITY,0)",
                        //                                                " ISNULL(GLOBAL_OFFERED,0)=1 AND COURSENO = " + Convert.ToInt32(Code) +
                        //                                                "AND " + Convert.ToInt32(lblSemester.ToolTip) + "  IN (SELECT VALUE FROM DBO.SPLIT(TO_SEMESTERNO,','))");


                        //int studcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                        //                                                   "R.COURSENO = " + Convert.ToInt32(Code) +
                        //                                                   "AND ISNULL(R.CANCEL,0)= 0 AND R.IDNO = " + Convert.ToInt32(objSR.IDNO) + " AND R.SESSIONNO IN(SELECT SESSIONNO FROM ACD_GLOBAL_OFFERED_COURSE WHERE R.COURSENO = COURSENO AND ISNULL(GLOBAL_OFFERED,0)=1)"));

                        //if (studcount > 0)
                        //{

                        //}
                        //else
                        //{

                        //    if (Convert.ToInt32(maxSeatsForGLobalSubj) > 0)
                        //    {
                        //        int TotalRegisteredcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT R", "ISNULL(COUNT(R.COURSENO),0)",
                        //                                                      "R.COURSENO = " + Convert.ToInt32(Code) +
                        //                                                      "AND ISNULL(R.CANCEL,0)= 0 AND R.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID =(SELECT SESSIONID FROM ACD_SESSION_MASTER WHERE SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ")" + ")"));

                        //        int intakeAvailable = Convert.ToInt32(maxSeatsForGLobalSubj) - TotalRegisteredcount;

                        //        if (intakeAvailable <= 0)
                        //        {
                        //            string lblCCode = (dataitem.FindControl("lblCCode") as Label).Text;
                        //            string lblCourseName = (dataitem.FindControl("lblCourseName") as Label).Text;
                        //            objCommon.DisplayMessage(UpdatePanel1, "This Global Courses " + lblCCode + " - " + lblCourseName + " seats are Full.. ", this.Page);
                        //            return;
                        //        }

                        //    }
                        //}

                        string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                        TotRegCreditsCount += Convert.ToDouble(Credits);
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                        GlobalSubjects = 3;
                        GlobalSubjectscount = GlobalSubjectscount + 1;
                        if (GlobalSubjectscount > 1)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only one audit course for particular semester..!!", this.Page);
                            return;
                        }
                        if (ValueAddedSubj != 0 && coreSubj != 0 && UniCoreSub != 0)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only One Course from different course list for Course Registration..!!", this.Page);
                            return;
                        }
                    }
                }

                foreach (ListViewDataItem dataitem in lvValueAdded.Items)
                {
                    if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                    {
                        string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                        TotRegCreditsCount += Convert.ToDouble(Credits);
                        objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                        objSR.SECTIONNOS = objSR.SECTIONNOS + (dataitem.FindControl("lblSection") as Label).ToolTip + "$";
                        ValueAddedSubj = 4;
                        ValueAddedSubjcount = ValueAddedSubjcount + 1;
                        if (ValueAddedSubjcount > 1)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only one audit course for particular semester..!!", this.Page);
                            return;
                        }
                        if (GlobalSubjects != 0 && coreSubj != 0 && UniCoreSub != 0)
                        {
                            objCommon.DisplayMessage(UpdatePanel1, "Please Select only One Course from different course list for Course Registration..!!", this.Page);
                            return;
                        }
                    }
                }
                int ret = 0;
                if (Session["usertype"].ToString() == "1")
                {
                    ret = objSReg.AddAddlRegisteredSubjectsAuditTypeCourseAdmin(objSR);
                }
                else
                {
                    ret = objSReg.AddAddlRegisteredSubjectsAuditTypeCourse(objSR);
                }
                if (ret > 0)
                {
                    int ret1 = 0;
                    if (UniCoreSub > 0)
                    {
                        if (electiveregcount > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                    if (GlobalSubjects > 0)
                    {
                        if (globalregcount > 0)
                        {
                        }
                        else
                        {
                        }
                    }
                    btnPrintChallan.Enabled = true;

                    string facAdvApprovalFlag = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(FACULTY_ADVISOR_APPROVAL,0)", "");
                    if (facAdvApprovalFlag != "1")
                        objCommon.DisplayMessage(this.Page, "Course Registration Done Successfully.You can print the registration slip.!!", this.Page);

                    else
                       // objCommon.DisplayMessage(this.Page, "Course Registration Done Successfully.You can print the registration slip after Faculty Advisor/Admin Approve.!!", this.Page);
                        objCommon.DisplayMessage(this.Page, "Audit Course Registration Done Successfully.You can print the Registration Slip", this.Page);
                    string IsCourseRegistered = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT 1", "ISNULL(REGISTERED,0)=1  AND ISNULL(ACCEPTED,0)=1 AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
                    if (string.IsNullOrEmpty(IsCourseRegistered))
                    {
                        btnSubmit.Enabled = true;
                        //btnPrintRegSlip.Enabled = false;
                    }
                    else
                    {
                        if (Session["usertype"].ToString() == "1")
                        {
                            btnSubmit.Enabled = true;
                            //btnPrintRegSlip.Enabled = true;
                        }
                        else
                        {
                            btnSubmit.Enabled = false;
                        }
                    }
                }
                #region commented code
                ///***********************************************Commented on 21.10.2021 by Dileep Kare Becoz no need to bind different listview for elective courses/global elective courses******************************************/
                ////Elective Subjects
                ////DataSet dsUniCodeSub = objCommon.FillDropDown("ACD_COURSE C LEFT JOIN acd_course_teacher CT ON(C.SCHEMENO=CT.SCHEMENO AND CT.SEMESTERNO=CT.SEMESTERNO AND C.COURSENO=CT.COURSENO) LEFT JOIN USER_ACC U ON (U.UA_NO=CT.UA_NO) LEFT JOIN ACD_SECTION SE ON(SE.SECTIONNO=CT.SECTIONNO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_ELECTGROUP P ON (C.GROUPNO=P.GROUPNO)", "DISTINCT C.COURSENO", "C.CCODE,c.GROUPNO,P.GROUPNAME,ISNULL(P.CHOICEFOR,0) AS CHOICEFOR,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS as CREDITS,S.SUBNAME,(CASE WHEN (SELECT EXAM_REGISTERED FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND COURSENO=C.COURSENO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=0)=1 THEN 1 ELSE 0 END)EXAM_REGISTERED,ISNULL(CT.INTAKE,0)-(select COUNT(ISNULL(COURSENO,0)) from ACD_STUDENT_RESULT where SCHEMENO=CT.SCHEMENO AND SEMESTERNO=CT.SEMESTERNO AND COURSENO=CT.COURSENO AND SESSIONNO=CT.SESSIONNO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AS INTAKE,U.UA_FULLNAME,SE.SECTIONNAME,SE.SectionNO,U.UA_NO", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1 AND  C.ELECT=1", "C.ELECT,C.GROUPNO");
                //DataSet dsUniCodeSub = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 2);
                //lvUniCoreSub.DataSource = dsUniCodeSub.Tables[0];
                //lvUniCoreSub.DataBind();
                //foreach (ListViewDataItem item in lvUniCoreSub.Items)
                //{
                //    Label lblgroupname = item.FindControl("lblgroupname") as Label;
                //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                //    Label lblIntake = item.FindControl("lblIntake") as Label;
                //    if (chkAccept.Checked == true)
                //    {
                //        lblIntake.Text = "Registered";
                //    }
                //    if (lblgroupname.ToolTip == "1" || lblgroupname.ToolTip == "5" || lblgroupname.ToolTip == "9" || lblgroupname.ToolTip == "13" || lblgroupname.ToolTip == "17" || lblgroupname.ToolTip == "21" || lblgroupname.ToolTip == "25" || lblgroupname.ToolTip == "29" || lblgroupname.ToolTip == "33" || lblgroupname.ToolTip == "37" || lblgroupname.ToolTip == "41" || lblgroupname.ToolTip == "45" || lblgroupname.ToolTip == "49" || lblgroupname.ToolTip == "53")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Blue;
                //    }
                //    if (lblgroupname.ToolTip == "2" || lblgroupname.ToolTip == "6" || lblgroupname.ToolTip == "10" || lblgroupname.ToolTip == "14" || lblgroupname.ToolTip == "18" || lblgroupname.ToolTip == "22" || lblgroupname.ToolTip == "26" || lblgroupname.ToolTip == "30" || lblgroupname.ToolTip == "34" || lblgroupname.ToolTip == "38" || lblgroupname.ToolTip == "42" || lblgroupname.ToolTip == "46" || lblgroupname.ToolTip == "50" || lblgroupname.ToolTip == "54")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Green;
                //    }
                //    if (lblgroupname.ToolTip == "3" || lblgroupname.ToolTip == "7" || lblgroupname.ToolTip == "11" || lblgroupname.ToolTip == "15" || lblgroupname.ToolTip == "19" || lblgroupname.ToolTip == "23" || lblgroupname.ToolTip == "27" || lblgroupname.ToolTip == "31" || lblgroupname.ToolTip == "35" || lblgroupname.ToolTip == "39" || lblgroupname.ToolTip == "43" || lblgroupname.ToolTip == "47" || lblgroupname.ToolTip == "51" || lblgroupname.ToolTip == "55")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.DarkGreen;
                //    }
                //    if (lblgroupname.ToolTip == "4" || lblgroupname.ToolTip == "8" || lblgroupname.ToolTip == "12" || lblgroupname.ToolTip == "16" || lblgroupname.ToolTip == "20" || lblgroupname.ToolTip == "24" || lblgroupname.ToolTip == "28" || lblgroupname.ToolTip == "32" || lblgroupname.ToolTip == "36" || lblgroupname.ToolTip == "40" || lblgroupname.ToolTip == "44" || lblgroupname.ToolTip == "48" || lblgroupname.ToolTip == "52" || lblgroupname.ToolTip == "56")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Brown;
                //    }

                //}

                /////Global Subjects
                ////DataSet dsGlobalCodeSub = objCommon.FillDropDown("ACD_COURSE C LEFT JOIN acd_course_teacher CT ON(C.SCHEMENO=CT.SCHEMENO AND CT.SEMESTERNO=CT.SEMESTERNO AND C.COURSENO=CT.COURSENO) LEFT JOIN USER_ACC U ON (U.UA_NO=CT.UA_NO) LEFT JOIN ACD_SECTION SE ON(SE.SECTIONNO=CT.SECTIONNO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID) INNER JOIN ACD_ELECTGROUP P ON (C.GROUPNO=P.GROUPNO)", "DISTINCT C.COURSENO", "C.CCODE,c.GROUPNO,P.GROUPNAME,ISNULL(P.CHOICEFOR,0) AS CHOICEFOR,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS as CREDITS,S.SUBNAME,(CASE WHEN (SELECT EXAM_REGISTERED FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND COURSENO=C.COURSENO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=0)=1 THEN 1 ELSE 0 END)EXAM_REGISTERED,ISNULL(CT.INTAKE,0)-(select COUNT(ISNULL(COURSENO,0)) from ACD_STUDENT_RESULT where  SEMESTERNO=CT.SEMESTERNO AND CCODE=CT.CCODE AND SESSIONNO=CT.SESSIONNO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") AS INTAKE,U.UA_FULLNAME,SE.SECTIONNAME,SE.SectionNO,U.UA_NO", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1 AND  C.ELECT=1 AND ISNULL(C.GLOBALELE,0)=0", "C.ELECT,C.GROUPNO");

                //DataSet dsGlobalCodeSub = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 3);
                //lvGlobalSubjects.DataSource = dsGlobalCodeSub.Tables[0];
                //lvGlobalSubjects.DataBind();
                //foreach (ListViewDataItem item in lvGlobalSubjects.Items)
                //{
                //    Label lblgroupname = item.FindControl("lblgroupname") as Label;
                //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
                //    Label lblIntake = item.FindControl("lblIntake") as Label;
                //    if (chkAccept.Checked == true)
                //    {
                //        lblIntake.Text = "Registered";
                //    }
                //    if (lblgroupname.ToolTip == "1" || lblgroupname.ToolTip == "5" || lblgroupname.ToolTip == "9" || lblgroupname.ToolTip == "13" || lblgroupname.ToolTip == "17" || lblgroupname.ToolTip == "21" || lblgroupname.ToolTip == "25" || lblgroupname.ToolTip == "29" || lblgroupname.ToolTip == "33" || lblgroupname.ToolTip == "37" || lblgroupname.ToolTip == "41" || lblgroupname.ToolTip == "45" || lblgroupname.ToolTip == "49" || lblgroupname.ToolTip == "53")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Blue;
                //    }
                //    if (lblgroupname.ToolTip == "2" || lblgroupname.ToolTip == "6" || lblgroupname.ToolTip == "10" || lblgroupname.ToolTip == "14" || lblgroupname.ToolTip == "18" || lblgroupname.ToolTip == "22" || lblgroupname.ToolTip == "26" || lblgroupname.ToolTip == "30" || lblgroupname.ToolTip == "34" || lblgroupname.ToolTip == "38" || lblgroupname.ToolTip == "42" || lblgroupname.ToolTip == "46" || lblgroupname.ToolTip == "50" || lblgroupname.ToolTip == "54")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Green;
                //    }
                //    if (lblgroupname.ToolTip == "3" || lblgroupname.ToolTip == "7" || lblgroupname.ToolTip == "11" || lblgroupname.ToolTip == "15" || lblgroupname.ToolTip == "19" || lblgroupname.ToolTip == "23" || lblgroupname.ToolTip == "27" || lblgroupname.ToolTip == "31" || lblgroupname.ToolTip == "35" || lblgroupname.ToolTip == "39" || lblgroupname.ToolTip == "43" || lblgroupname.ToolTip == "47" || lblgroupname.ToolTip == "51" || lblgroupname.ToolTip == "55")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.DarkGreen;
                //    }
                //    if (lblgroupname.ToolTip == "4" || lblgroupname.ToolTip == "8" || lblgroupname.ToolTip == "12" || lblgroupname.ToolTip == "16" || lblgroupname.ToolTip == "20" || lblgroupname.ToolTip == "24" || lblgroupname.ToolTip == "28" || lblgroupname.ToolTip == "32" || lblgroupname.ToolTip == "36" || lblgroupname.ToolTip == "40" || lblgroupname.ToolTip == "44" || lblgroupname.ToolTip == "48" || lblgroupname.ToolTip == "52" || lblgroupname.ToolTip == "56")
                //    {
                //        lblgroupname.ForeColor = System.Drawing.Color.Brown;
                //    }
                //}
                #endregion

                BindAllCourses();
                TotalRegisterCreditsCount();
                CheckActivity();
                
                lblTotalRegCredits.Text = objCommon.LookUp("ACD_STUDENT_RESULT", "SUM(CREDITS) AS CREDITS", "IDNO=" + idno + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND IS_AUDIT=1");

            }
            else if (status == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select atleast One Course in course list for Course Registration..!!", this.Page);
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Select only One Course in course list for Course Registration..!!", this.Page);
                return;
            }
        }
    }

    #region
    //private void PopulateDropDownList()
    //{
    //    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "TOP 1 SESSIONNO", "SESSION_NAME", "SESSIONNO>0 and FLOCK = 1", "SESSIONNO DESC");
    //    ddlSession.SelectedIndex = 1;
    //    // objCommon.FillDropDownList(ddlPayType, "ACD_PAYMENTTYPE", "PAYTYPENO", "PAYTYPENAME", "PAYTYPENO > 0", "PAYTYPENO");
    //    // objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
    //    // this.objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "", "BANKNAME");
    //    ddlSession.Focus();
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CourseEnrollment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CourseEnrollment.aspx");
        }
    }

    private void Redirect()
    {
        Response.Redirect("~/notauthorized.aspx?page=CourseRegistration.aspx");
    }

    private void ShowDetails()
    {
        try
        {
            StudentFeedBackController SFB = new StudentFeedBackController();
            string idno;
            //if (!Session["usertype"].ToString().Equals("2") && txtRollNo.Text.Trim() == string.Empty)
            //{
            //    objCommon.DisplayMessage("Please Enter Student Roll No.", this.Page);
            //    return;
            //}
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                idno = Session["idno"].ToString();
            }
            else
            {
                idno = Session["stuinfoidno"].ToString();//= objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'" + "AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0");
            }
            // string feedback_status;
            int sessionno = 0;
            int sessionmax = 0;
            sessionno = Convert.ToInt16(ddlSession.SelectedValue);
            int semester = Convert.ToInt16(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "SEMESTERNO", "IDNO=" + idno + ""));

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(UpdatePanel1, "Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                divCourses.Visible = false;
                divNote.Visible = true;
                return;
            }

            //check the deparment of HOD 


            DataSet dsStudent = (DataSet)ViewState["DataSet_Student_Details"];//objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM WITH (NOLOCK) ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG WITH (NOLOCK) ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.FATHERFIRSTNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH", "S.IDNO = " + idno, string.Empty);
            if (dsStudent != null && dsStudent.Tables[1].Rows.Count > 0)
            {
                if (dsStudent.Tables[1].Rows.Count > 0)
                {
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[1].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[1].Rows[0]["IDNO"].ToString();

                    if (dsStudent.Tables[1].Rows[0]["FATHERNAME"].ToString() != null)
                        lblFatherName.Text = dsStudent.Tables[1].Rows[0]["FATHERNAME"].ToString();
                    else
                        lblFatherName.Text = dsStudent.Tables[1].Rows[0]["FATHERFIRSTNAME"].ToString();

                    lblMotherName.Text = dsStudent.Tables[1].Rows[0]["MOTHERNAME"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[1].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[1].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[1].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[1].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[1].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[1].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[1].Rows[0]["SEMESTERNAME"].ToString();
                    lblSess.Text = dsStudent.Tables[1].Rows[0]["SESSION_NAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[1].Rows[0]["SEMESTERNO"].ToString();
                    //ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    lblAdmBatch.Text = dsStudent.Tables[1].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[1].Rows[0]["ADMBATCH"].ToString();

                    hdfDegreeno.Value = dsStudent.Tables[1].Rows[0]["DEGREENO"].ToString();
                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[1].Rows[0]["PH"].ToString();

                    tblInfo.Visible = true;
                    DataSet dsTotRegCredits = (DataSet)ViewState["DataSet_Student_Details"]; //objSReg.GetTotalCreditsCount(Convert.ToInt32(hdfDegreeno.Value), Convert.ToInt32(lblBranch.ToolTip), Convert.ToInt32(lblScheme.ToolTip), Convert.ToInt32(lblSemester.ToolTip));
                    //lblTotalRegCredits.Text = (dsTotRegCredits != null && dsTotRegCredits.Tables[2].Rows.Count > 0) ? Convert.ToString(dsTotRegCredits.Tables[2].Rows[0]["TOT_CREDIT_GROUP"]) : "0";
                    //lblOfferedRegCreditsFrom.Text = (dsTotRegCredits != null && dsTotRegCredits.Tables[2].Rows.Count > 0) ? Convert.ToString(dsTotRegCredits.Tables[2].Rows[0]["TOT_CREDIT_GROUP_FROM"]) : "0";

                    //Show Current Semester Courses ..
                    //DataSet dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS AS CREDITS,S.SUBNAME,(CASE WHEN (SELECT EXAM_REGISTERED FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND COURSENO=C.COURSENO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=0)=1 THEN 1 ELSE 0 END)EXAM_REGISTERED", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1 AND C.ELECT=0", "S.SUBNAME");
                    //DataSet dsCurrCourses = objCommon.FillDropDown("ACD_COURSE C LEFT JOIN acd_course_teacher CT ON(C.SCHEMENO=CT.SCHEMENO AND CT.SEMESTERNO=CT.SEMESTERNO AND C.COURSENO=CT.COURSENO) LEFT JOIN USER_ACC U ON (U.UA_NO=CT.UA_NO) LEFT JOIN ACD_SECTION SE ON(SE.SECTIONNO=CT.SECTIONNO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSE_NAME,C.SUBID,C.ELECT,C.CREDITS AS CREDITS,S.SUBNAME,(CASE WHEN (SELECT EXAM_REGISTERED FROM ACD_STUDENT_RESULT WHERE IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND COURSENO=C.COURSENO and sectionno=ct.sectionno AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ISNULL(CANCEL,0)=0 AND PREV_STATUS=0)=1 THEN 1 ELSE 0 END)EXAM_REGISTERED,case when CT.INTAKE=isnull then '-' else ISNULL(CT.INTAKE,0)-(select COUNT(ISNULL(COURSENO,0)) from ACD_STUDENT_RESULT where SCHEMENO=CT.SCHEMENO AND SEMESTERNO=CT.SEMESTERNO AND COURSENO=CT.COURSENO AND SESSIONNO=CT.SESSIONNO AND SECTIONNO=CT.SECTIONNO AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ") end AS INTAKE,U.UA_FULLNAME,SE.SECTIONNAME,isnull(SE.SectionNO,0) as SectionNO,U.UA_NO", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + " AND C.OFFERED = 1 AND C.ELECT=0", "S.SUBNAME");
                    string sessionos = dsStudent.Tables[1].Rows[0]["SESSIONNO"].ToString();
                    if (sessionos == string.Empty)
                        sessionos = "0";

                    int countregcourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + idno + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SESSIONNO =" + Convert.ToInt32(sessionos) + " AND REGISTERED = 1 AND (CANCEL <> 1 OR CANCEL IS NULL)"));

                    //int countregcourse = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(IDNO)", "IDNO=" + idno + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND REGISTERED = 1 AND IS_AUDIT IS NULL AND ISNULL(CANCEL,0)=0"));

                     if (countregcourse == 0)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Please do normal course registration first.!!!", this.Page);
                        return;
                    }

                     lblOfferedRegCredits.Text = objCommon.LookUp("ACD_STUDENT_RESULT", "SUM(CREDITS) AS CREDITS", "IDNO=" + idno + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND SESSIONNO =" + Convert.ToInt32(sessionos) + " AND ISNULL(CANCEL,0)=0");

                    lblTotalRegCredits.Text = objCommon.LookUp("ACD_STUDENT_RESULT", "SUM(CREDITS) AS CREDITS", "IDNO=" + idno + "AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "AND IS_AUDIT=1");

                    BindAllCourses();
                    TotalRegisterCreditsCount();
                    divCourses.Visible = true;

                    if (dsStudent != null && dsStudent.Tables[0].Rows.Count > 0)
                    {
                        string coursePattern = dsStudent.Tables[0].Rows[0]["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"].ToString();
                        if (!string.IsNullOrEmpty(coursePattern))
                        {
                            //lvUniCoreSub.Enabled = (coursePattern.Contains("2")) ? true : false;
                            //lvGlobalSubjects.Enabled = (coursePattern.Contains("3")) ? true : false;
                        }
                    }
                    else
                    {
                        //objCommon.DisplayMessage(UpdatePanel1, "Activity has been expired,Please Contact your department.!!!", this.Page);
                        divCourses.Visible = false;
                        divNote.Visible = true;
                        return;
                    }
                }
                else
                {
                    // objCommon.DisplayMessage(UpdatePanel1,"Student with Roll No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    objCommon.DisplayMessage(UpdatePanel1, "Scheme not found, Please contact your department.!!!", this.Page);
                    divCourses.Visible = false;
                    divNote.Visible = true;
                    return;
                }
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }


    private void TotalRegisterCreditsCount()
    {
        double Count = 0;
        foreach (ListViewDataItem item in lvCurrentSubjects.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
            {
                Credits = "0";
            }
            if (chkAccept.Checked == true)
            {
                Count += Convert.ToDouble(Credits);
            }
        }

        foreach (ListViewDataItem item in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
            {
                Credits = "0";
            }
            if (chkAccept.Checked == true)
            {
                Count += Convert.ToDouble(Credits);
            }
        }

        foreach (ListViewDataItem item in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
            {
                Credits = "0";
            }
            if (chkAccept.Checked == true)
            {
                Count += Convert.ToDouble(Credits);
            }
        }

        foreach (ListViewDataItem item in lvValueAdded.Items)
        {
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            string Credits = (item.FindControl("lblCredits") as Label).Text;
            if (Credits == string.Empty)
                Credits = "0";

            if (chkAccept.Checked == true)
                Count += Convert.ToDouble(Credits);
        }

        string totalcount = Convert.ToString(Count);
        if (totalcount != "")
        {
           // lblTotalRegCredits.Text = String.Format("{0:F2}", Count);
        }
        else
        {
           // lblTotalRegCredits.Text = totalcount;
        }

    }

    /// <summary>
    /// Current Course Process
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>
    protected void chkCurrentSubjects_OnCheckedChanged(object Sender, EventArgs e)
    {
        DataTable dt = null;
        DataTable dtCT = null;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        dt = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
        dtCT = (ds != null && ds.Tables[5].Rows.Count > 0) ? ds.Tables[5] : null;
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblCourseTeacher = dataitem.FindControl("lblCourseTeacher") as Label;
            Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;

            if (chkAccept.Checked == true && lblIntake.Text != "NA")
            {
                int RegCoureCount = 0;
                int IntakeCoureCount = 0;
                int courseno = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
                int sectionno = Convert.ToInt32(lblSection.ToolTip);
                CourseDuplicateSelectionCheck(Convert.ToInt32(lblCCode.ToolTip));

                if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {

                    RegCoureCount = (dt != null && dt.Rows.Count > 0) ? dt.AsEnumerable().Count(row => row.Field<int>("COURSENO") == courseno) : 0;
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0)= 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    RegCoureCount = dt.AsEnumerable().Count(row => row.Field<int>("COURSENO") == courseno && row.Field<int>("SECTIONNO") == sectionno);
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0)= 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + "  AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip != string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(sectionno)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip != string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                        intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    IntakeCoureCount = (from intake in dtCT.AsEnumerable()
                                        where intake.Field<int>("COURSENO").Equals(courseno) &&
                                                                          intake.Field<int>("SECTIONNO").Equals(sectionno) &&
                                                                          intake.Field<int>("UA_NO").Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                        select intake.Field<int>("INTAKE")).FirstOrDefault<int>();

                }
                int BalCourseCount = IntakeCoureCount - RegCoureCount;
                lblIntake.Text = Convert.ToString(BalCourseCount);

            }

            //if (chkAccept.Checked == true)
            //    TotalRegisterCreditsCount();
            //else
            TotalRegisterCreditsCount();
        }

    }

    private void CourseDuplicateSelectionCheck(int Courseno)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            if (chkAccept.Checked == true)
            {
                if (Convert.ToInt32(lblCCode.ToolTip) == Courseno)
                    count++;
            }

            if (count > 1)
            {
                objCommon.DisplayMessage(UpdatePanel1, "You Can not select same course ", this.Page);
                chkAccept.Checked = false;
                TotalRegisterCreditsCount();
                return;
            }
        }

    }

    private void ElectiveCourseDuplicateSelectionCheck(int Courseno)
    {
        // int count = 0;
        int sectioncount0 = 0, sectioncount1 = 0, sectioncount2 = 0, sectioncount3 = 0, sectioncount4 = 0;
        int sectioncount5 = 0, sectioncount6 = 0, sectioncount7 = 0, sectioncount8 = 0, sectioncount9 = 0;
        int sectioncount10 = 0, sectioncount11 = 0, sectioncount12 = 0, sectioncount13 = 0, sectioncount14 = 0;
        int sectioncount15 = 0, sectioncount16 = 0, sectioncount17 = 0, sectioncount18 = 0, sectioncount19 = 0;

        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            if (chkAccept.Checked == true)
            {
                if (Convert.ToInt32(lblCCode.ToolTip) == Courseno)
                {
                    //count++;
                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 0)
                    {
                        sectioncount0++;
                    }
                    if (sectioncount0 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 1)
                    {
                        sectioncount1++;
                    }
                    if (sectioncount1 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 2)
                    {
                        sectioncount2++;
                    }
                    if (sectioncount2 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 3)
                    {
                        sectioncount3++;
                    }
                    if (sectioncount3 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 4)
                    {
                        sectioncount4++;
                    }
                    if (sectioncount4 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 5)
                    {
                        sectioncount5++;
                    }
                    if (sectioncount5 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 6)
                    {
                        sectioncount6++;
                    }
                    if (sectioncount6 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 7)
                    {
                        sectioncount7++;
                    }
                    if (sectioncount7 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 8)
                    {
                        sectioncount8++;
                    }
                    if (sectioncount8 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 9)
                    {
                        sectioncount9++;
                    }
                    if (sectioncount9 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 10)
                    {
                        sectioncount10++;
                    }
                    if (sectioncount10 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 11)
                    {
                        sectioncount11++;
                    }
                    if (sectioncount11 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 12)
                    {
                        sectioncount12++;
                    }
                    if (sectioncount12 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 13)
                    {
                        sectioncount13++;
                    }
                    if (sectioncount13 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 14)
                    {
                        sectioncount14++;
                    }
                    if (sectioncount14 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 15)
                    {
                        sectioncount15++;
                    }
                    if (sectioncount15 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 16)
                    {
                        sectioncount16++;
                    }
                    if (sectioncount16 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 17)
                    {
                        sectioncount17++;
                    }
                    if (sectioncount17 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 18)
                    {
                        sectioncount18++;
                    }
                    if (sectioncount18 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 19)
                    {
                        sectioncount19++;
                    }
                    if (sectioncount19 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                }
            }

            //if (count > 1)
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "You Can not select the same course", this.Page);
            //    chkAccept.Checked = false;
            //    return;
            //}



        }

    }


    private void GlobalElectiveCourseDuplicateSelectionCheck(string CCode)
    {
        int count = 0;
        int sectioncount0 = 0, sectioncount1 = 0, sectioncount2 = 0, sectioncount3 = 0, sectioncount4 = 0;
        int sectioncount5 = 0, sectioncount6 = 0, sectioncount7 = 0, sectioncount8 = 0, sectioncount9 = 0;
        int sectioncount10 = 0, sectioncount11 = 0, sectioncount12 = 0, sectioncount13 = 0, sectioncount14 = 0;
        int sectioncount15 = 0, sectioncount16 = 0, sectioncount17 = 0, sectioncount18 = 0, sectioncount19 = 0;
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;


            if (chkAccept.Checked == true)
            {
                if (lblCCode.Text == CCode)
                {
                    //count++;

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 0)
                    {
                        sectioncount0++;
                    }
                    if (sectioncount0 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 1)
                    {
                        sectioncount1++;
                    }
                    if (sectioncount1 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 2)
                    {
                        sectioncount2++;
                    }
                    if (sectioncount2 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 3)
                    {
                        sectioncount3++;
                    }
                    if (sectioncount3 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 4)
                    {
                        sectioncount4++;
                    }
                    if (sectioncount4 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 5)
                    {
                        sectioncount5++;
                    }
                    if (sectioncount5 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 6)
                    {
                        sectioncount6++;
                    }
                    if (sectioncount6 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 7)
                    {
                        sectioncount7++;
                    }
                    if (sectioncount7 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 8)
                    {
                        sectioncount8++;
                    }
                    if (sectioncount8 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 9)
                    {
                        sectioncount9++;
                    }
                    if (sectioncount9 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 10)
                    {
                        sectioncount10++;
                    }
                    if (sectioncount10 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 11)
                    {
                        sectioncount11++;
                    }
                    if (sectioncount11 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 12)
                    {
                        sectioncount12++;
                    }
                    if (sectioncount12 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 13)
                    {
                        sectioncount13++;
                    }
                    if (sectioncount13 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 14)
                    {
                        sectioncount14++;
                    }
                    if (sectioncount14 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 15)
                    {
                        sectioncount15++;
                    }
                    if (sectioncount15 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }


                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 16)
                    {
                        sectioncount16++;
                    }
                    if (sectioncount16 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 17)
                    {
                        sectioncount17++;
                    }
                    if (sectioncount17 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 18)
                    {
                        sectioncount18++;
                    }
                    if (sectioncount18 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }

                    if (chkAccept.Checked == true && Convert.ToInt32(lblSection.ToolTip) == 19)
                    {
                        sectioncount19++;
                    }
                    if (sectioncount19 > 1)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "You can not select same course & same section more than one time.!", this.Page);
                        chkAccept.Checked = false;
                        TotalRegisterCreditsCount();
                        return;
                    }
                }
            }

            //if (count > 1)
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "You Can not select the same course", this.Page);
            //    chkAccept.Checked = false;
            //    return;
            //}
        }

    }


    /// <summary>
    /// Elective Course Process
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chklvUniCoreSub_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dtCourse = null;
        DataTable dtCT = null;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        dtCourse = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
        dtCT = (ds != null && ds.Tables[4].Rows.Count > 0) ? ds.Tables[4] : null;
        foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblCourseTeacher = dataitem.FindControl("lblCourseTeacher") as Label;
            Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;

            if (chkAccept.Checked == true)
            {
                TotalRegisterCreditsCount();
            }
            else
            {
                TotalRegisterCreditsCount();
            }


            if (chkAccept.Checked == true)
            {
                CourseDuplicateGroupCheck(Convert.ToInt32(chkAccept.ToolTip));
                ElectiveCourseDuplicateSelectionCheck(Convert.ToInt32(lblCCode.ToolTip));
            }

            if (chkAccept.Checked == true && lblIntake.Text != "NA")
            {

                int RegCoureCount = 0;
                int IntakeCoureCount = 0;
                int courseno = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
                int Sectionno = Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip);
                if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip)) : 0;
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) = 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip) && row.Field<int>("SECTIONNO") == Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip)) : 0;
                    //Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) = 0 AND COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });

                    //Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip != string.Empty)
                {
                    try
                    {
                        IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                           where intake["COURSENO"].Equals(courseno) && intake["SECTIONNO"].Equals(Sectionno)
                                                           select new
                                                           {
                                                               intake1 = intake["INTAKE"]
                                                           });
                    }
                    catch
                    {
                        IntakeCoureCount = 0;
                    }
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip != string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                        intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                //else if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                //{
                //    IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).Text + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                //}
                else
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " COURSENO=" + (dataitem.FindControl("lblCCode") as Label).ToolTip + "  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                //int RegCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", " COURSENO=" + lblCCode.ToolTip + " AND SECTIONNO=" + lblSection.ToolTip + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));

                //int IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "ISNULL(INTAKE,0)", " COURSENO=" + lblCCode.ToolTip + " AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + lblSection.ToolTip + " AND UA_NO=" + lblCourseTeacher.ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));

                int BalCourseCount = IntakeCoureCount - RegCoureCount;
                lblIntake.Text = Convert.ToString(BalCourseCount);
                //if (BalCourseCount <= 0)
                //{
                //    lblIntake.Text = "0";
                //    objCommon.DisplayMessage(UpdatePanel1, "No more intake is available for selected course " + lblCCode.Text + " - " + lblCourseName.Text, this.Page);
                //    chkAccept.Checked = false;
                //    return;
                //}
            }


        }
    }

    /// <summary>
    /// Check Elective Duplicate Group
    /// </summary>
    /// <param name="GroupNO"></param>
    private void CourseDuplicateGroupCheck(int GroupNO)
    {
        //int count = 0;
        //foreach (ListViewDataItem dataitem in lvUniCoreSub.Items)
        //{
        //    CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
        //    Label lblCCode = dataitem.FindControl("lblCCode") as Label;
        //    Label lblSection = dataitem.FindControl("lblSection") as Label;
        //    Label lblIntake = dataitem.FindControl("lblIntake") as Label;
        //    if (chkAccept.Checked == true)
        //    {
        //        if (Convert.ToInt32(chkAccept.ToolTip) == GroupNO)
        //        {
        //            count++;
        //        }
        //    }

        //    if (count > Convert.ToInt32(lblIntake.ToolTip))
        //    {
        //        objCommon.DisplayMessage(UpdatePanel1, "You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
        //        chkAccept.Checked = false;
        //        TotalRegisterCreditsCount();
        //        return;
        //    }
        //}

    }


    /// <summary>
    /// Global Subject Check Validation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkGlobalSubjects_OnCheckedChanged(object sender, EventArgs e)
    {
        DataTable dtCourse = null;
        DataTable dtCT = null;
        DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
        dtCourse = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
        dtCT = (ds != null && ds.Tables[4].Rows.Count > 0) ? ds.Tables[4] : null;
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblCourseTeacher = dataitem.FindControl("lblCourseTeacher") as Label;
            Label lblCourseName = dataitem.FindControl("lblCourseName") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;

            if (chkAccept.Checked == true)
                TotalRegisterCreditsCount();
            else
                TotalRegisterCreditsCount();

            if (chkAccept.Checked == true)
            {
                CourseDuplicateGloblaGroupCheck(Convert.ToInt32(chkAccept.ToolTip));
                GlobalElectiveCourseDuplicateSelectionCheck(lblCCode.Text);
            }

            if (chkAccept.Checked == true && lblIntake.Text != "NA")
            {


                int RegCoureCount = 0;
                int IntakeCoureCount = 0;
                int courseno = Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip);
                int Sectionno = Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip);
                if ((dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip)) : 0;
                    // RegCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) <> 1 AND CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else
                {
                    RegCoureCount = (dtCourse != null && dtCourse.Rows.Count > 0) ? dtCourse.AsEnumerable().Count(row => row.Field<int>("COURSENO") == Convert.ToInt32((dataitem.FindControl("lblCCode") as Label).ToolTip) && row.Field<int>("SECTIONNO") == Convert.ToInt32((dataitem.FindControl("lblSection") as Label).ToolTip)) : 0;
                    //RegCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", " ISNULL(CANCEL,0) <> 1 AND CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "   AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip == string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip != string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "'  AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }
                else if ((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip != string.Empty && (dataitem.FindControl("lblSection") as Label).ToolTip == string.Empty)
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    // IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "' AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + "  AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                else
                {
                    IntakeCoureCount = Convert.ToInt32(from intake in dtCT.AsEnumerable()
                                                       where intake["COURSENO"].Equals(courseno) &&
                                                       intake["SECTIONNO"].Equals(Sectionno) &&
                                                       intake["UA_NO"].Equals((dataitem.FindControl("lblCourseTeacher") as Label).ToolTip)
                                                       select new
                                                       {
                                                           intake1 = intake["INTAKE"]
                                                       });
                    //IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER WITH (NOLOCK)", "ISNULL(INTAKE,0)", " CCode='" + (dataitem.FindControl("lblCCode") as Label).Text + "' AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + (dataitem.FindControl("lblSection") as Label).ToolTip + " AND UA_NO=" + (dataitem.FindControl("lblCourseTeacher") as Label).ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));
                }

                //int RegCoureCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", " CCode='" + lblCCode.Text + "'  AND SECTIONNO=" + lblSection.ToolTip + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));

                //int IntakeCoureCount = Convert.ToInt32(objCommon.LookUp("acd_course_teacher", "ISNULL(INTAKE,0)", " CCode='" + lblCCode.Text + "' AND SCHEMENO =" + Convert.ToInt32(lblScheme.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + " AND SECTIONNO=" + lblSection.ToolTip + " AND UA_NO=" + lblCourseTeacher.ToolTip + " AND SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ""));

                int BalCourseCount = IntakeCoureCount - RegCoureCount;
                lblIntake.Text = Convert.ToString(BalCourseCount);
            }
        }
    }


    /// <summary>
    /// Check Global Elective Duplicate Group
    /// </summary>
    /// <param name="GroupNO"></param>
    private void CourseDuplicateGloblaGroupCheck(int GroupNO)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvGlobalSubjects.Items)
        {
            CheckBox chkAccept = dataitem.FindControl("chkAccept") as CheckBox;
            Label lblCCode = dataitem.FindControl("lblCCode") as Label;
            Label lblSection = dataitem.FindControl("lblSection") as Label;
            Label lblIntake = dataitem.FindControl("lblIntake") as Label;
            if (chkAccept.Checked == true)
            {
                if (Convert.ToInt32(chkAccept.ToolTip) == GroupNO)
                {
                    count++;
                }
            }

            //if (count > Convert.ToInt32(lblIntake.ToolTip))
            //{
            //    objCommon.DisplayMessage(UpdatePanel1, "You can select only " + lblIntake.ToolTip + " course for same group.!", this.Page);
            //    chkAccept.Checked = false;
            //    TotalRegisterCreditsCount();
            //    return;
            //}
        }

    }


    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + ViewState["collegeId"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    protected void btnProceed_Click(object sender, EventArgs e)
    {

       


            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {

                Divsearch.Visible = false;
                divsteps.Visible = false;
                divNote.Visible = false;
                divCourses.Visible = true;
                divsession.Visible = true;
                divmaincoursereg.Visible = true;

                //updEdit.Visible = true;
                trSession_name.Visible = false;
                //divsession.Visible = false;
                trRollNo.Visible = false;
                divNote.Visible = false;
                btnShow.Visible = false;
                btnCancel.Visible = false;
                tblInfo.Visible = true;
                Divsearch.Visible = false;
                //string count = objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(1)", "IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND REGISTERED=1 AND ISNULL(CANCEL,0)=0");
                DataTable dt;
                DataSet ds = (DataSet)ViewState["DataSet_Student_Details"];
                dt = (ds != null && ds.Tables[1].Rows.Count > 0) ? ds.Tables[1] : null;
                //int count = (dt != null && dt.Rows.Count > 0) ? dt.AsEnumerable().Count(row => row.Field<int>("REGISTERED") == 1 && row.Field<int>("ACCEPTED") == 1 && row.Field<int>("CANCEL") == 0) : 0;
                int count = (dt != null && dt.Rows.Count > 0) ? dt.AsEnumerable().Count(row => row.Field<int>("STUD_COURSE_REG") == 1 && row.Field<int>("CANCEL") == 0) : 0;
                //string count =objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(1)", "IDNO=" + Session["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCEL,0)=0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(ACCEPTED,0)=1");
                DataTable dt1 = (ds != null && ds.Tables[3].Rows.Count > 0) ? ds.Tables[3] : null;
                int IsCrsRegistred = (dt1 != null && dt.Rows.Count > 0) ? dt1.AsEnumerable().Count(row => row.Field<int>("REGISTERED") == 1) : 0;

                ShowDetails();
                //DataSet dsCourseRegistred = objCommon.FillDropDown("ACD_STUDENT_RESULT WITH (NOLOCK)",
                //    "COUNT(ISNULL(REGISTERED,0))TOTAL_REGISTERED_COURSE", " COUNT(DISTINCT COURSENO) TOTAL_COURSES",
                //    "ISNULL(CANCEL,0)= 0  AND ISNULL(PREV_STATUS,0)=0  AND IDNO=" + Convert.ToInt32(lblName.ToolTip) 
                //    + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip)
                //    + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
                //    + " AND SEMESTERNO="+Convert.ToInt32(lblSemester.ToolTip), "");

                DataSet dsCourseRegistred = objSReg.Get_Student_Registered_Course(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip));

                int COREREGISTERED = 0;
                int ELECTREGISTERED = 0;
                int GLOBALREGISTERED = 0;
                int StudCOREREGISTERED = 0;
                int StudELECTREGISTERED = 0;
                int StudGLOBALREGISTERED = 0;

                if (dsCourseRegistred != null && dsCourseRegistred.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[0].Rows[0]["COREREGISTERED"].ToString()))
                        COREREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[0].Rows[0]["COREREGISTERED"].ToString());
                    else
                        COREREGISTERED = 0;

                    if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[0].Rows[0]["ELECTREGISTERED"].ToString()))
                        ELECTREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[0].Rows[0]["ELECTREGISTERED"].ToString());
                    else
                        ELECTREGISTERED = 0;

                    if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[0].Rows[0]["GLOBALREGISTERED"].ToString()))
                        GLOBALREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[0].Rows[0]["GLOBALREGISTERED"].ToString());
                    else
                        GLOBALREGISTERED = 0;

                    //ELECTREGISTERED=(!string.IsNullOrEmpty(dsCourseRegistred.Tables[0].Rows[0]["ELECTREGISTERED"].ToString())?  Convert.ToInt32(dsCourseRegistred.Tables[0].Rows[0]["ELECTREGISTERED"].ToString()):0;
                    //GLOBALREGISTERED=(!string.IsNullOrEmpty(dsCourseRegistred.Tables[0].Rows[0]["GLOBALREGISTERED"].ToString())?Convert.ToInt32(dsCourseRegistred.Tables[0].Rows[0]["GLOBALREGISTERED"].ToString()):0;

                    IsCrsRegistred = (COREREGISTERED == 1 && ELECTREGISTERED == 1 && GLOBALREGISTERED == 1) ? 1 : 0;
                }

                else
                {
                    IsCrsRegistred = 0;

                    if (dsCourseRegistred != null && dsCourseRegistred.Tables[1].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[1].Rows[0]["COREREGISTERED"].ToString()))
                            StudCOREREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[1].Rows[0]["COREREGISTERED"].ToString());
                        else
                            StudCOREREGISTERED = 0;

                        if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[1].Rows[0]["ELECTREGISTERED"].ToString()))
                            StudELECTREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[1].Rows[0]["ELECTREGISTERED"].ToString());
                        else
                            StudELECTREGISTERED = 0;

                        if (!string.IsNullOrEmpty(dsCourseRegistred.Tables[1].Rows[0]["GLOBALREGISTERED"].ToString()))
                            StudGLOBALREGISTERED = Convert.ToInt32(dsCourseRegistred.Tables[1].Rows[0]["GLOBALREGISTERED"].ToString());
                        else
                            StudGLOBALREGISTERED = 0;
                    }
                }


                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (IsCrsRegistred == 0)
                    {

                        btnSubmit.Enabled = true;
                        //btnPrintRegSlip.Enabled = false;
                        lvCurrentSubjects.Enabled = false;
                        string coursePattern = ds.Tables[0].Rows[0]["CORE_ELECT_GLOBAL_COURSE_TYPE_NO"].ToString();
                        if (!string.IsNullOrEmpty(coursePattern))
                        {
                            if (coursePattern.Contains("2"))
                            {
                                if (ELECTREGISTERED == 1)
                                {
                                    //lvUniCoreSub.Enabled = false;
                                    lvUniCoreSub.Visible = coursePattern.Contains("2") ? true : false;
                                }
                                else
                                {
                                    //lvUniCoreSub.Enabled = coursePattern.Contains("2") ? true : false;
                                    lvUniCoreSub.Visible = coursePattern.Contains("2") ? true : false;
                                }
                            }
                            else
                            {
                                //lvUniCoreSub.Enabled = false;
                                lvUniCoreSub.Visible = StudELECTREGISTERED == 1 ? true : false;
                            }
                            if (coursePattern.Contains("3"))
                            {
                                if (GLOBALREGISTERED == 1)
                                {
                                    // lvGlobalSubjects.Enabled = false;
                                    lvGlobalSubjects.Visible = coursePattern.Contains("3") ? true : false;
                                }
                                else
                                {
                                    //lvGlobalSubjects.Enabled = coursePattern.Contains("3") ? true : false;
                                    lvGlobalSubjects.Visible = coursePattern.Contains("3") ? true : false;
                                }
                            }
                            else
                            {
                                //lvGlobalSubjects.Enabled = false;
                                lvGlobalSubjects.Visible = StudGLOBALREGISTERED == 1 ? true : false;
                            }
                        }
                        else
                        {
                            lvUniCoreSub.Visible = false;
                            lvGlobalSubjects.Visible = false;
                        }
                        //lvUniCoreSub.Enabled = () ? false : true;
                        //lvGlobalSubjects.Enabled = (GLOBALREGISTERED == 1) ? false : true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Audit Course registration Approved successfully..Now you can Generate Registration Slip..!!", this.Page);
                        btnSubmit.Enabled = false;
                        btnPrintRegSlip.Enabled = true;
                        lvCurrentSubjects.Enabled = false;
                        lvUniCoreSub.Enabled = false;
                        lvGlobalSubjects.Enabled = false;
                    }
                }
                else
                {
                    if (count > 0)
                    {
                        objCommon.DisplayMessage(UpdatePanel1, "Audit Course registration already done..Now you can Generate Registration Slip..!!", this.Page);
                        lvCurrentSubjects.Enabled = false;
                        lvUniCoreSub.Enabled = false;
                        lvGlobalSubjects.Enabled = false;
                        btnSubmit.Enabled = false;
                        btnPrintRegSlip.Enabled = true;
                    }
                }
            }
            else if (Session["usertype"].ToString().Equals("1"))
            {
                updEdit.Visible = true;
                Divsearch.Visible = true;
                divsteps.Visible = false;
                divNote.Visible = false;
                divCourses.Visible = true;
            }
            else
            {
                divNote.Visible = false;
                divCourses.Visible = true;
                ddlSession.SelectedIndex = 0;
                txtRollNo.Text = string.Empty;
                PopulateDropDownList();
            }

            //if (!CheckActivity())
            //    return;

        
    }

    protected void btnPrintChallan_Click(object sender, EventArgs e)
    {
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        ////Show Tabulation Sheet
        //LinkButton btn = sender as LinkButton;
        //string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        //string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        //string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        //string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;
        //this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt",sessionNo,schemeNo,semesterNo,IdNo);
    }

    private void ShowReport(string rptName, int dcrNo, int studentNo, string copyNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=Fee_Collection_Receipt";
            url += "&path=~,Reports,Academic," + rptName;
            url += "&param=" + this.GetReportParameters(studentNo, dcrNo, copyNo);
            //divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
            //divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private string GetReportParameters(int studentNo, int dcrNo, string copyNo)
    {
        string param = "@P_IDNO=" + studentNo.ToString() + ",@P_DCRNO=" + dcrNo + ",CopyNo=" + copyNo + ",@P_COLLEGE_CODE=" + ViewState["collegeId"].ToString() + "";
        return param;
    }



    protected void rbPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        // btnExcelReport.Visible = false;

        int count = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(Session["idno"]);
        count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO =" + idno + "AND SESSIONNO =" + sessionno + "AND ISNULL(CANCEL,0)=0"));
        if (count > 0)
        {
            // ShowReport("RegistrationSlip", "rptCourseRegSlip.rpt");
            //if (Convert.ToInt32(Session["OrgId"]) == 2)
            //    ShowReportStudentWise(idno, "RegistrationSlip", "rptBulkCourseRegslipForCrescentStudentWise.rpt");
            //else
            //    ShowReportStudentWise(idno, "RegistrationSlip", "rptBulkCourseRegslipStudentwise.rpt");
            ShowReport("RegistrationSlip", "rptCourseRegSlip.rpt");
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Course Registration Not Found.", this.Page);
        }

    }

    private void ShowReportStudentWise(int idno, string reportTitle, string rptFileName)
    {
        try
        {

            int collegeId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO = " + idno + ""));
            int session = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM ACD_COURSE_REG_CONFIG_ACTIVITY WHERE STARTED = 1 AND SHOW_STATUS =1) AND COLLEGE_ID= " + collegeId + ""));
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SCHEMENO", "IDNO = " + idno + ""));
            int SEMESTERNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO = " + idno + ""));
            int ADMBATCH = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO = " + idno + ""));
            int degree = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "degreeno", "IDNO = " + idno + ""));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9 || Convert.ToInt32(Session["OrgId"]) == 7)
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) +
                    ",@P_SESSIONNO=" + session +
                    ",@P_SCHEMENO=" + schemeno +
                    ",@P_SEMESTERNO=" + SEMESTERNO +
                    ",@P_DEGREENO=" + degree +
                    ",@P_ADMBATCH=" + ADMBATCH +
                    ",@UserName=" + Session["username"] +
                    ",@P_COLLEGE_ID=" + collegeId +
                    ",@P_IDNO=" + idno;
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
                    ",@P_SESSIONNO=" + session +
                    ",@P_SCHEMENO=" + schemeno +
                    ",@P_SEMESTERNO=" + SEMESTERNO +
                    ",@P_DEGREENO=" + degree +
                    ",@P_ADMBATCH=" + ADMBATCH +
                    ",@UserName=" + Session["username"] +
                    ",@P_COLLEGE_ID=" + collegeId +
                    ",@P_IDNO=" + idno;
            }
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        // int sessionno = Convert.ToInt32(Session["currentsession"].ToString());
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(Session["idno"]);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + ViewState["collegeId"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();
            //url += "&param=@P_COLLEGE_CODE=1,@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Somethingwent Wrong.!!", this.Page);
        }
    }

    protected void btnPrePrintClallan_Click(object sender, EventArgs e)
    {
        string studentIDs = lblName.ToolTip;
        int selectSemesterNo = Int32.Parse(lblSemester.ToolTip);
        string dcrNo = objCommon.LookUp("ACD_DCR WITH (NOLOCK)", "DCR_NO", "IDNO=" + Convert.ToInt32(studentIDs) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SEMESTERNO=" + selectSemesterNo);
        if (dcrNo != string.Empty && studentIDs != string.Empty)
        {
            this.ShowReport("FeeCollectionReceiptForSemCourseRegister.rpt", Convert.ToInt32(dcrNo), Convert.ToInt32(studentIDs), "1");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnExcelReport_Click1(object sender, EventArgs e)
    {
        ExcelReport();
    }

    private void ExcelReport()
    {
        try
        {

            StudentRegist objSR = new StudentRegist();
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            //  objSR.ROLLNO = txtRollNo.Text.Trim();
            CourseController objCC = new CourseController();
            DataSet ds = objSReg.AllcourseRegistrationDetailsforExcel(objSR);

            ds.Tables[0].TableName = "AllCourseDetails";

            using (XLWorkbook wb = new XLWorkbook())
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    //Add System.Data.DataTable as Worksheet.
                    wb.Worksheets.Add(dt);
                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=All_Couse_Details.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch
        {

        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        string sessionno = string.Empty;
        string degreeno = string.Empty;
        string branchno = string.Empty;
        string semesterno = string.Empty;
        ActivityController objActController = new ActivityController();
        int idno = Convert.ToInt32(Session["idno"]);
        int page = Convert.ToInt32(Request.QueryString["pageno"].ToString());
        int user = Convert.ToInt32(Session["usertype"]);
        DataSet ds = objSReg.Get_Student_Details_for_Course_Registration(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToInt32(Session["usertype"]), 5);
        ViewState["DataSet_Student_Details"] = ds;
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                pnlMain.Visible = false;
                ret = false;
            }

            int IsFeeSubmitted = 0;
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PAYMENT_APPLICABLE_FOR_SEM_WISE"].ToString()) && ds.Tables[0].Rows[0]["PAYMENT_APPLICABLE_FOR_SEM_WISE"].ToString().ToLower().Equals("true"))
            {
                if (ds.Tables[6].Rows.Count > 0)
                    IsFeeSubmitted = 1;
            }
            else
                IsFeeSubmitted = 1;


            if (IsFeeSubmitted > 0)
                ret = true;
            else
            {
                objCommon.DisplayMessage("You have Not paid Admission Fee yet. Please pay the Admission Fee.", this.Page);
                pnlMain.Visible = false;
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            pnlMain.Visible = false;
            ret = false;
        }

        return ret;
    }

    private void BindAllCourses()
    {
        //#region Core Courses
        ////ddlSession.SelectedValue = "105";
        //DataSet dsCurrCourses = objSReg.GetStudentCourseRegistrationSubjectAudit_course_type(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 1);

        //lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
        //lvCurrentSubjects.DataBind();
        //hdnCount.Value = dsCurrCourses.Tables[0].Rows.Count.ToString();
        //foreach (ListViewDataItem item in lvCurrentSubjects.Items)
        //{
        //    CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
        //    Label lblIntake = item.FindControl("lblIntake") as Label;
        //    if (lblIntake.Text != string.Empty && lblIntake.Text != "NA")
        //    {
        //        if (Convert.ToInt32(lblIntake.Text) <= 0)
        //            chkAccept.Enabled = false;
        //    }
        //    if (chkAccept.Checked == true)
        //        lblIntake.Text = "Registered";
        //}
        //#endregion

        #region ELECTIVE COURSES
        /************************************************Commented on 21-10-2021 by Dileep Kare Becoz no need get separate course list**************************************************/
        DataSet dsUniCodeSub = objSReg.GetStudentCourseRegistrationSubjectAudit_course_type(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 2);

        lvUniCoreSub.DataSource = dsUniCodeSub.Tables[0];
        lvUniCoreSub.DataBind();

        int countE = 0;
        foreach (ListViewDataItem item in lvUniCoreSub.Items)
        {
            Label lblgroupname = item.FindControl("lblgroupname") as Label;
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            Label lblIntake = item.FindControl("lblIntake") as Label;
            Label lblAudit = item.FindControl("lblAudit") as Label;
            if (Session["usertype"].ToString() == "1")
            {
                if (lblAudit.Text == "1")
                {
                    chkAccept.Checked = true;
                    chkAccept.BackColor = System.Drawing.Color.Blue;
                }
            }
            else
            {
                if (lblAudit.Text == "1")
                {
                    chkAccept.Enabled = false;
                }
            }
        }
        #endregion

        #region Global Elective Course
        DataSet dsGlobalCodeSub = objSReg.GetStudentCourseRegistrationSubjectAudit_course_type(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 3);

        lvGlobalSubjects.DataSource = dsGlobalCodeSub.Tables[0];
        lvGlobalSubjects.DataBind();
        int countG = 0;
        foreach (ListViewDataItem item in lvGlobalSubjects.Items)
        {
            Label lblgroupname = item.FindControl("lblgroupname") as Label;
            CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;
            Label lblIntake = item.FindControl("lblIntake") as Label;
            Label lblAudit = item.FindControl("lblAudit") as Label;
            
            if (lblAudit.Text == "1")
            {
                chkAccept.Enabled = false;
            }
        #endregion

            #region value Added / Specialization Course
            //lvValueAdded
            DataSet dsValueAddedSub = objSReg.GetStudentCourseRegistrationSubjectAudit_course_type(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 4);
            lvValueAdded.DataSource = dsValueAddedSub.Tables[0];
            lvValueAdded.DataBind();
            #endregion

        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        // divSingleStudDetail.Visible = false;
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
            ViewState["value"] = value;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        //ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        //  divDemandCreation.Visible = false;
        // btnCreateDemand.Visible = false;

    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNew(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {

            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            pnlLV.Visible = true;
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudent.Visible = false;

        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        try
        {
            //Added By Vipul Tichakule on dated 18-01-2024 as per Tno :
            LinkButton btnlink = sender as LinkButton;
            int idno = int.Parse(btnlink.CommandArgument);
            Session["idno"] = idno;
            Session["stuinfoidno"] = idno;
            string url = string.Empty;
            if (Request.Url.ToString().IndexOf("&id=") > 0)
                url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
            else
                url = Request.Url.ToString();

            string value = string.Empty;

            foreach (ListViewDataItem lnk in lvStudent.Items)
            {
                Label lblenrollno = lnk.FindControl("lblstuenrollno") as Label;
                Session["stuinfoenrollno"] = lblenrollno.Text.Trim();          
                break;                             
            }//end
                FeeCollectionController feeController = new FeeCollectionController();
                // int studentId = feeController.GetStudentIdByEnrollmentNo(txtEnrollNo.Text.Trim());
                if (idno > 0)
                {
                    //divSingleStudDetail.Visible = true;

                    if (CheckActivity())
                    // if (1==1)
                    {
                        divmaincoursereg.Visible = true;

                        binddetails();

                        divsession.Visible = true;
                        pnlLV.Visible = false;
                        // pnlMain.Visible = true;
                        divCourses.Visible = true;

                        // btnProceed_Click(sender, e);
                        ShowDetails();
                        lvValueAdded.Visible = true;
                        lvGlobalSubjects.Visible = true;
                        lvUniCoreSub.Visible = true;
                        lvCurrentSubjects.Visible = true;
                        lvHistory.Visible = true;
                        divCourses.Visible = true;
                        divsession.Visible = true;
                        divmaincoursereg.Visible = true;
                        pnlMain.Visible = true;
                        Divsearch.Visible = false;
                        tblInfo.Visible = true;
                        divmainsearch.Visible = false;
                        trSession_name.Visible = false;
                        btnShow.Visible = false;
                        //btnExcelReport.Visible = false;
                        btnCancel.Visible = false;

                    }

                    //lvStudent.Visible = false;
                    // divDemandCreation.Visible = true;
                    // StudentDetails();
                }
                else
                {
                    //ShowMessage("No student found with given enrollment number.");
                    objCommon.DisplayMessage(this.UpdatePanel1, "No student found with given enrollment number.", this.Page);
                    //  divSingleStudDetail.Visible = false;
                
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }



    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        //divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }


  

}