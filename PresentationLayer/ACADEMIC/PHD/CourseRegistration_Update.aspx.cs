using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_PHD_CourseRegistration_Update : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new IITMS.UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    PhdController objPhdC = new PhdController();
    bool flag = false;

    string ua_dept = string.Empty;

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

                    if (ViewState["action"] == null)
                        ViewState["action"] = "add";
                    string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    string ua_dec = objCommon.LookUp("User_Acc", "UA_DEC", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ua_dept = objCommon.LookUp("User_Acc", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                    ViewState["usertype"] = ua_type;
                    ViewState["dec"] = ua_dec;
                    FillDropDown();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        updEdit.Visible = false;
                        divmain.Visible = true;                   
                        //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_PHD_COURSE_STATUS B ON A.SEMESTERNO=B.SEMESTERNO", "DISTINCT B.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND B.IDNO = " + Convert.ToInt32(Session["idno"].ToString()), "B.SEMESTERNO");
                        trremark.Visible = false;
                        ShowStudentDetails();
                        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER AC INNER JOIN ACD_STUDENT S ON(S.COLLEGE_ID=AC.COLLEGE_ID)", "AC.COLLEGE_ID", "ISNULL(AC.COLLEGE_NAME,'')+(CASE WHEN AC.LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(AC.LOCATION,'') COLLEGE_NAME", "AC.COLLEGE_ID > 0 and AC.OrganizationId=" + Convert.ToInt32(Session["OrgId"])+"AND S.IDNO="+Convert.ToInt32(Session["idno"].ToString()), "AC.COLLEGE_ID");

                    }
                    else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
                    {
                        trremark.Visible = true;
                    }
                    else if (ViewState["usertype"].ToString() == "4")
                    {
                        ControlActivityOFF();
                    }

                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["action"] = "edit";
                        ShowStudentDetails();
                    }

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //   lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void ControlActivityOFF()
    {
        //  txtResearch.Enabled = ddlSupervisor.Enabled = ddlSupervisorrole.Enabled = btnSubmit.Enabled = CheckBox1.Enabled = false;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhD_Registration_Approval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhD_Registration_Approval.aspx");
        }
    }
    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 and OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA_PHD", "ID", "CRITERIANAME", "ID > 0 AND IS_FEE_RELATED = 0", "SRNO");         
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            ddlSem.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region DYNAMIC SEARCH
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Panellistview.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Panellistview.Visible = true;

        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }
        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        Session["idno"] = Session["stuinfoidno"].ToString();
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        divmain.Visible = true;      
        divmain.Visible = true;
        DivSutLog.Visible = true;
        updEdit.Visible = false;
        Panellistview.Visible = false;
        ShowStudentDetails();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Panel3.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails_Phd(ddlSearch.SelectedItem.Text);
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


                        objCommon.FillDropDownList(ddlDropdown, tablename, "DISTINCT " + column1, column2, "UGPGOT=3", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
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
        txtSearch.Text = string.Empty;
    }

    private void ShowStudentDetails()
    {
        StudentController objSC = new StudentController();
        DataTableReader dtr = null;
        if (ViewState["usertype"].ToString() == "2")
        {
            dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
        }
        else
        {
            if (Request.QueryString["id"] != null)
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
            else
            {
                dtr = objSC.GetStudentPHDDetails(Convert.ToInt32(Session["idno"]));
            }
        }
        if (dtr != null)
        {
            if (dtr.Read())
            {
                string AllotSup = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "count(IDNO)", "IDNO=" + Convert.ToInt32(Session["idno"]));
                string scheme = objCommon.LookUp("acd_student", "isnull(schemeno,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
                string deanStatus = objCommon.LookUp("ACD_PHD_ALLOTED_SUPERVISOR", "DEAN_APPROVE", "IDNO=" + Convert.ToInt32(Session["idno"]));
                string SEMESTERNO = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
              //  lblTotalCredit.Text = Convert.ToString(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT WITH (NOLOCK)", "DISTINCT ISNULL(TO_CREDIT,0) TOTAL_CREDIT", "SCHEMENO =" + Convert.ToInt32(scheme) + " AND TERM = " + Convert.ToInt16(SEMESTERNO)));
                lblTotalCredit.Text = Convert.ToString(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT WITH (NOLOCK)", "DISTINCT ISNULL(CORE_CREDIT,0) TOTAL_CREDIT", "SCHEMENO =" + Convert.ToInt32(scheme) + " AND TERM = " + Convert.ToInt16(SEMESTERNO)));
                string totalcredits = lblTotalCredit.Text;

                if (AllotSup == "0")
                {
                    objCommon.DisplayMessage(updEdit, "Supervisor Allotment Not Done", this.Page);
                    updEdit.Visible = true;
                    divmain.Visible = false;
                    ddlSearch.SelectedIndex = 0;
                   // btnCancel.Visible = false;
                    return;
                }
                else
                {
                    if (deanStatus == "0")
                    {
                        objCommon.DisplayMessage(updEdit, "Dean Approval Not Done", this.Page);
                        updEdit.Visible = true;
                        divmain.Visible = false;
                        ddlSearch.SelectedIndex = 0;
                        return;
                    }
                    else
                    {
                        if (scheme == "0")
                        {
                            objCommon.DisplayMessage(updEdit, "Scheme Allotment Not Done", this.Page);
                            updEdit.Visible = true;
                            divmain.Visible = false;
                            ddlSearch.SelectedIndex = 0;
                            return;
                        }
                        else
                        if (totalcredits.ToString() == "" || totalcredits.ToString() == "0" || totalcredits.ToString() == " ")
                        {
                            objCommon.DisplayUserMessage(this.updEdit, "Please define Credit limit", this.Page);
                            updEdit.Visible = true;
                            divmain.Visible = false;
                            ddlSearch.SelectedIndex = 0;
                            return;
                        }
                        else
                        {
                            lblidno.Text = dtr["IDNO"].ToString();
                            lblenrollmentnos.Text = dtr["ENROLLNO"].ToString();
                            lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                            lblnames.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                            lblfathername.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                            lbljoiningdate.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                            lblDepartment.Text = dtr["BRANCHNAME"].ToString();
                            ViewState["SCHEMENO"] = dtr["SCHEMENO"].ToString();
                            ViewState["DEGREENO"] = dtr["DEGREENO"].ToString();
                            lblScheme.Text = dtr["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dtr["SCHEMENO"].ToString();
                            lblDepartment.ToolTip = dtr["BRANCHNO"].ToString();
                            lbladmbatch.Text = dtr["ADMBATCHNAME"].ToString();
                            lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();

                            if (dtr["PHDSTATUS"] == null)
                            {
                                lblstatussup.Text = "";

                            }
                            if (dtr["PHDSTATUS"].ToString() == "1")
                            {
                                lblstatussup.Text = "Full Time";

                            }
                            if (dtr["PHDSTATUS"].ToString() == "2")
                            {
                                lblstatussup.Text = "Part Time";

                            }
                            if (dtr["SUPERROLE"].ToString() == "S")
                            {
                                lblSuperRole.Text = "Singly";
                            }
                            else
                                if (dtr["SUPERROLE"].ToString() == "J")
                                {
                                    lblSuperRole.Text = "Jointly";
                                }
                                else if (dtr["SUPERROLE"].ToString() == "T")
                                {
                                    lblSuperRole.Text = "Multiple";
                                }
                            lblNDM.Text = dtr["NOOFDGC"].ToString();
                            lblAoR.Text = dtr["RESEARCH"].ToString();
                            btnSubmit.Enabled = false;
                        }
                    }
                }

            }
        }
    }
    private void BindAvailableCourseList()
    {
        #region Core Courses
        DataSet dsCurrCourses = objSReg.GetStudentCourseRegistrationSubject(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblidno.Text), Convert.ToInt32(lblSemester.ToolTip), Convert.ToInt32(lblScheme.ToolTip), 1);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            btnSubmit.Enabled = true;
            foreach (ListViewDataItem item in lvCurrentSubjects.Items)
            {
                CheckBox chkAccept = item.FindControl("chkAccept") as CheckBox;

            }
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCurrentSubjects);//Set label 
               lvCurrentSubjects.Visible = true;
        }
        else
        {
            btnSubmit.Enabled = false;
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            objCommon.DisplayMessage(updEdit, "No Subject found in Allotted Scheme and Semester.", this.Page);
        }
        #endregion
    }

    private void BindStudentFailedCourseList()
    {
        DataSet dsCurrCourses = null;
        //Show Backlog Semester Courses ..
        dsCurrCourses = objSReg.GetStudentCoursesForBacklogRegistration(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), 0);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvBacklogSubjects.DataSource = dsCurrCourses.Tables[0];
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = true;
        }
        else
        {
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
        }
    }

    #endregion


    #region Course Registration
 
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOfferedCourse, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE AC ON(OC.COURSENO=AC.COURSENO) ", "OC.COURSENO", "AC.COURSE_NAME", "sessionno=" + ddlSession.SelectedValue + " AND OC.SEMESTERNO=" + ddlSem.SelectedValue + "AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"].ToString()) + "AND OC.SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"].ToString()), "OC.COURSENO DESC");
        ddlOfferedCourse.Focus();
    }
    protected void ddlOfferedCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            BindAvailableCourseList();
            BindStudentFailedCourseList();
        }
        else
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
        }

    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]) + " AND COLLEGE_ID=" + ddlSchool.SelectedValue, "SESSIONNO DESC");
            ddlSession.Focus();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int flag = 0;
            double CREDIT = 0;
            int count = 0;
            double TotalCredit = 0;
            //double credits = 0;
            TotalCredit = Convert.ToDouble(lblTotalCredit.Text);
            StudentRegist objSR = new StudentRegist();

            objSR.EXAM_REGISTERED = 0;

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
              //  Label credit = dataitem.FindControl("lblCredits") as Label;
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true && (dataitem.FindControl("chkRegister") as CheckBox).Enabled==true)
                {
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    string Credits = (dataitem.FindControl("lblCredits") as Label).Text;
                    CREDIT += Convert.ToDouble(Credits);
                    count++;
                }
                
            }
            foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            {
                if ((dataitem.FindControl("chkRegister") as CheckBox).Checked == true && (dataitem.FindControl("chkRegister") as CheckBox).Enabled == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    string Credits1 = (dataitem.FindControl("lblCredits") as Label).Text;
                    CREDIT += Convert.ToDouble(Credits1);
                    count++;
                }

            }
            if (objSR.COURSENOS == null)
            {
                objSR.COURSENOS = "0";
                flag = 1;
            }
            else
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
            if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0)
            {
                if (count > 0)
                {

                    //Add registered 
                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(lblidno.Text);
                    objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                    objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                    objSR.IPADDRESS = Session["ipAddress"].ToString();
                    objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    objSR.REGNO = lblenrollmentnos.Text;
                    DataSet dsCurrCourses = null;
                    dsCurrCourses = objCommon.FillDropDown("acd_student", "DISTINCT idno", "ROLLNO", " IDNO=" + lblidno.Text, "");
                    string  ROLLNO = dsCurrCourses.Tables[0].Rows[0]["ROLLNO"].ToString();
                    objSR.ROLLNO = ROLLNO;

                    if (CREDIT == TotalCredit)
                    {
                        int ret = objPhdC.InsertStudentRegistration(objSR);

                        if (ret == 1)
                        {
                            if (flag == 1)
                            {
                                objCommon.DisplayMessage(updEdit, "Course Removal Done Successfully!!", this.Page);
                            }
                            else
                            {
                                objCommon.DisplayMessage(updEdit, "Course Registration Successfully.", this.Page);
                                btnPrintRegSlip.Visible = true;
                                BindAvailableCourseList();
                                BindStudentFailedCourseList();
                            }
                        }
                        else
                            objCommon.DisplayMessage(updEdit, "Registration Failed! Error in saving record.", this.Page);
                       //   checkbox();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updEdit, "The Total Credits limits is " + TotalCredit + ", Selected credit is " + CREDIT + ", Kindly check the course credits", this.Page);
                        //return;
                        //objCommon.DisplayMessage(updEdit, "Course Registration credit Should be less than 16 ", this.Page);
                        //checkbox();
                        //btnPrintRegSlip.Visible = true;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updEdit, "Please select at least one Course for Registration ", this.Page);
                   // checkbox();
                }

            }
            
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void checkbox()
    {
        foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
        {
            CheckBox chk= dataitem.FindControl("chkRegister") as CheckBox;
            chk.Checked=false;          
        }
        foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
        {
           CheckBox chk= dataitem.FindControl("chkRegister") as CheckBox;
            chk.Checked=false;       
        }
            
    }
    #endregion

    #region print 
    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        int count = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblidno.Text);
        count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(1)", "IDNO =" + idno + "AND SESSIONNO =" + sessionno + "AND ISNULL(CANCEL,0)=0"));
        if (count > 0)
        {
            ShowReport("RegistrationSlip", "rptCourseRegSlip_phd.rpt");
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Course Registration Not Found.", this.Page);
        }
    }
  
    private void ShowReport(string reportTitle, string rptFileName)
    { // int sessionno = Convert.ToInt32(Session["currentsession"].ToString());
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblidno.Text);
        int collegeId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO = " + lblidno.Text + ""));
        ViewState["collegeId"] = collegeId;
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE="  + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@UserName=" + Session["username"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
#endregion

}