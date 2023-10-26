
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using System.Web;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_BulkRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //DataSet DetainedSubjects, CurrentSubjects, PreviousData;

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

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ////ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));
                // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "FLOCK=1 AND SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");

                //ddlSession.SelectedIndex = 1;
                //Fill DropDownLists
                //this.PopulateDropDown();

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

            }

            if (Session["usertype"].ToString() != "1")
                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE WHEN '" + Session["userdeptno"] + "' ='0'  THEN '0' ELSE DB.DEPTNO END) IN (" + Session["userdeptno"] + ")", "");
            else
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");

            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    } 

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string roll = string.Empty;
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();
        //CheckBox cbRow = new CheckBox();

        try
        {
            double TotalCredit = 0;
            double credits = 0;
            TotalCredit = Convert.ToDouble(lblTotalCredit.Text);
            foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
            {
                CheckBox chkAccept = dataitemCourse.FindControl("cbRow") as CheckBox;
                if (chkAccept.Checked == true)
                {
                    credits = credits + double.Parse(chkAccept.ToolTip);
                }
            }
            if (TotalCredit < credits)
            {
                //objCommon.DisplayMessage(updBulkReg, "Total Credit " + TotalCredit, this.Page);
                objCommon.DisplayMessage(updBulkReg, "The Total Core Credits limits is " + TotalCredit + ", Selected credit is " + credits + ", Kindly check the course credits", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvStudent.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                {
                    objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                    objSR.IDNO = Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).ToolTip);
                    objSR.REGNO = ((dataitem.FindControl("lblIDNo")) as Label).Text;
                    objSR.SEMESTERNO = Convert.ToInt32(ddlSemester.SelectedValue);
                    objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"]);
                    //objSR.SECTIONNO = Convert.ToInt32(ddlSection.SelectedValue);
                    objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                    objSR.COLLEGE_CODE = Session["colcode"].ToString();
                    //objSR.ABSORPTION_STATUS = Convert.ToInt32(ddlStatus.SelectedValue);
                    //objSR.GDPOINT = 0;
                    objSR.UA_NO = Convert.ToInt32(Session["userno"]);

                    //Get Course Details
                    foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
                    {
                        if (((dataitemCourse.FindControl("cbRow")) as CheckBox).Checked == true)
                        {
                            objSR.COURSENOS += ((dataitemCourse.FindControl("lblCCode")) as Label).ToolTip + "$";
                            Label elective = (dataitemCourse.FindControl("lblCourseName")) as Label;
                            if (elective.ToolTip == "False")
                            {
                                objSR.ELECTIVE += "0" + "$";
                            }
                            else
                            {
                                objSR.ELECTIVE += "1" + "$";
                            }
                        }
                    }

                    objSR.ACEEPTSUB = "1";

                    //Register Single Student
                    int prev_status = 0;    //Regular Courses
                    //int seatno = 0;
                    CustomStatus cs = (CustomStatus)objSRegist.AddRegisteredSubjectsBulk(objSR, prev_status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objSR.COURSENOS = string.Empty;
                        objSR.ACEEPTSUB = string.Empty;
                    }
                }
            }
            objCommon.DisplayMessage(updBulkReg, "Student(s) Registered Successfully!!", this.Page);
            clear();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSchemeType.SelectedIndex = 0;
        txtTotStud.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlStudentsReamin.Visible = false;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Response.Redirect(Request.Url.ToString());
        ClearAllFields();
    }

    protected void ClearAllFields()
    {
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlAdmBatch.Items.Clear();
        ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        ddlClgname.SelectedIndex = 0;
        btnSubmit.Enabled = false;
        btnReport.Visible = false;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uano = Session["userno"].ToString();
        string uatype = Session["usertype"].ToString();
        string dept = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(uano));
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Items.Clear();
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + "AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "A.LONGNAME");
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO IN (" + Session["userdeptno"].ToString() + ")", "A.LONGNAME");
            else
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
            ddlScheme.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlStatus.SelectedIndex = 0;
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0)
        {

            //if (ddlSemester.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
            //{
            //    objCommon.DisplayMessage("Please Select Semester/Scheme", this.Page);
            //    return;
            //}
            //else
            //{
            this.BindListView();
            //}
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnSubmit.Enabled = false;
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            //if (ddlBranch.SelectedIndex <= 0 || ddlScheme.SelectedIndex <= 0)
            //{
            //    objCommon.DisplayMessage("Please Select Programme/Branch/Scheme", this.Page);
            //    return;
            //}
            //else
            //{
            int countsection = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT ST WITH (NOLOCK) INNER JOIN  ACD_SECTION  S WITH (NOLOCK) ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT COUNT(ISNULL(S.SECTIONNO,0))", "ST.Schemeno = " + ViewState["schemeno"] + " AND ST.Semesterno= " + ddlSemester.SelectedValue));

            if (countsection > 0)
            {
                ddlSection.Focus();
                objCommon.FillDropDownList(ddlSection, "ACD_STUDENT ST WITH (NOLOCK) INNER JOIN  ACD_SECTION  S WITH (NOLOCK)ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "ST.Schemeno = " + ViewState["schemeno"] + " AND ST.Semesterno= " + ddlSemester.SelectedValue, "S.SECTIONNO"); //added by reena on  4_10_16
            }
            else
            {
                objCommon.DisplayMessage(updBulkReg, "Please Allot Section for Selection Criteria...", this.Page);
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
                return;
            }

            //objCommon.FillDropDownList(ddlSection, "ACD_SECTION", "SECTIONNO", "SECTIONNAME", "SECTIONNO > 0", "SECTIONNO");
            //this.BindListView();
            //}
        }
        else
        {
            ddlSection.SelectedIndex = 0;
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnSubmit.Enabled = false;
        }
    }

    #region User Defined Methods

    public int cInt(string strInteger)
    {
        int i = 0; int.TryParse(strInteger, out i); return i;
    }

    private void BindListView()
    {
        //Get student list as per scheme & semester & secion(AND SECTIONNO = " + ddlSection.SelectedValue)
        StudentController objSC = new StudentController();
        DataSet ds = null;
        lblTotalCredit.Text = Convert.ToString(objCommon.LookUp("ACD_DEFINE_TOTAL_CREDIT WITH (NOLOCK)", "DISTINCT ISNULL(CORE_CREDIT,0) TOTAL_CREDIT", "SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND TERM = " + Convert.ToInt16(ddlSemester.SelectedValue)));
        string totalcredits = lblTotalCredit.Text;
        if (totalcredits.ToString() == "" || totalcredits.ToString() == "0" || totalcredits.ToString() == " ")
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "Please define Core Credit limit", this.Page);
            return;
        }
        //ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
        ds = objSC.GetStudentsByScheme(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ddlSchemeType.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudent.DataSource = ds.Tables[0];
                lvStudent.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label 
                pnlStudents.Visible = true;
                btnSubmit.Enabled = true;
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    btnReport.Visible = true;
                }
                hftot.Value = ds.Tables[0].Rows.Count.ToString();

                //for getting student list semester wise
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chkBox = item.FindControl("cbRow") as CheckBox;
                    String lblReg = (item.FindControl("lblStudName") as Label).ToolTip;
                    Button lnkPrintRegReport = item.FindControl("lnkPrintRegReport") as Button;

                    if (lblReg == "1")  //IF STUDENT IS ALREADY REGISTERED THEN CHECKBOX WILL BE DISABLED
                    {
                        chkBox.Enabled = false;
                        //chkBox.Checked = true;
                        chkBox.BackColor = System.Drawing.Color.Green;
                        lnkPrintRegReport.Text = "Available";
                        lnkPrintRegReport.ForeColor = System.Drawing.Color.Green;
                        lnkPrintRegReport.Enabled = true;
                    }
                    else
                    {
                        lnkPrintRegReport.Text = "Not Available";
                        lnkPrintRegReport.ForeColor = System.Drawing.Color.Red;
                        lnkPrintRegReport.Enabled = false;

                    }
                    lblReg = string.Empty;
                }

                //for getting courses semester wise
                //DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_OFFERED_COURSE O ON (O.COURSENO=C.COURSENO)", "C.COURSENO", "C.CCODE,C.COURSE_NAME,ISNULL(C.ELECT,0) ELECT", "C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND C.SEMESTERNO = " + ddlSemester.SelectedValue + " AND O.SEMESTERNO = " + ddlSemester.SelectedValue + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND OFFERED = 1 AND ISNULL(C.ELECT,0)=0", "C.COURSENO");
                CourseController objCourse = new CourseController();
                DataSet dsCourse = objCourse.GetOfferedCourseListForBulkCourseRegistration(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));

                hfdCourseCount.Value = dsCourse.Tables[0].Rows.Count.ToString();
                if (dsCourse != null && dsCourse.Tables.Count > 0)
                {
                    if (dsCourse.Tables[0].Rows.Count > 0)
                    {
                        lvCourse.DataSource = dsCourse;
                        lvCourse.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvCourse);//Set label 
                        pnlCourses.Visible = true;
                    }
                    else
                    {
                        lvCourse.DataSource = null;
                        lvCourse.DataBind();
                        pnlCourses.Visible = false;
                    }
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    pnlCourses.Visible = false;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnSubmit.Enabled = false;
                pnlStudents.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updBulkReg, "No Record Found!", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            pnlStudents.Visible = false;
            btnSubmit.Enabled = false;
        }
    }

    //private void PopulateDropDown()
    //{
    //    try
    //    {
    //        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
    //        if (Session["usertype"].ToString() != "1")
    //            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
    //        else
    //            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH WITH (NOLOCK)", "BRANCHNO", "LONGNAME", "DEGREENO >0", "BRANCHNO");
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_BulkRegistration.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkRegistration.aspx");
        }
    }

    #endregion

    protected void lvCourse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label elective = (e.Item.FindControl("lblCourseName")) as Label;
        if (elective.ToolTip == "False")
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = true;
        }
        else
        {
            ((e.Item.FindControl("cbRow")) as CheckBox).Checked = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        LinkButton printButton = sender as LinkButton;
        int idno = Int32.Parse(printButton.CommandArgument);
        ViewState["IDNO"] = idno;
        int count = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT WITH (NOLOCK)", "COUNT(*)", "IDNO=" + idno + " AND SESSIONNO = " + ddlSession.SelectedValue));
        if (count > 0)
        {
            objCommon.DisplayMessage("This Student is already registered for session " + ddlSession.SelectedItem.Text, this.Page);
            ShowReport("Registered_Courses", "rptPreRegSlip.rpt");
        }
        else
        {
            objCommon.DisplayMessage("This Student is not registered for session " + ddlSession.SelectedItem.Text, this.Page);
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
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_SESSIONNO=" + ddlSession.SelectedValue 
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
                + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue 
                + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) 
                + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue 
                + ",@UserName=" + Session["username"] 
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);
            
            
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBulkReg, this.updBulkReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SES ON(SES.ODD_EVEN = SM.ODD_EVEN)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO=" + ddlDegree.SelectedValue + " AND S.SCHEMENO=" + ddlScheme.SelectedValue + " AND SES.SESSIONNO = " + ddlSession.SelectedValue, "SM.SEMESTERNO");
                ddlSemester.Focus();
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            else
            {
                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
                ddlStatus.SelectedIndex = 0;
                ddlSection.Items.Clear();
                ddlSection.Items.Add(new ListItem("Please Select", "0"));
            }
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            pnlStudentsReamin.Visible = false;
            btnSubmit.Enabled = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            ddlAdmBatch.Focus();

            //Commented by As per Requirement Of Romal Saluja Sir 

            // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
            if (ds != null && ds.Tables.Count > 0)
            {
                ddlSemester.DataSource = ds;
                ddlSemester.DataTextField = "SEMESTERNAME";
                ddlSemester.DataValueField = "SEMESTERNO";
                ddlSemester.DataBind();
            }

            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SES ON(SES.ODD_EVEN = SM.ODD_EVEN)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SES.SESSIONNO =" + ddlSession.SelectedValue, "SM.SEMESTERNO");

            ddlSemester.Focus();
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            lvStudentsRemain.DataSource = null;
            lvStudentsRemain.DataBind();
            pnlCourses.Visible = false;
            pnlStudents.Visible = false;
            btnSubmit.Enabled = false;
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            btnSubmit.Enabled = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedValue != "0")
        {
            ViewState["college_id"] = Convert.ToInt32(ddlCollege.SelectedValue).ToString();
        }
        if (Convert.ToInt32(Session["OrgId"]) == 2)
            ShowReport("RegistrationSlip", "rptBulkCourseRegslipForCrescent.rpt");
        else
            ShowReport("RegistrationSlip", "rptBulkCourseRegSlip_01.rpt");
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND B.DEPTNO IN ( " + Session["userdeptno"].ToString() + " )", "D.DEGREENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlCollege.SelectedValue, "D.DEGREENO");
        }

        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;

        //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO = D.DEGREENO)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", ", "D.DEGREENO");
        ddlDegree.Focus();
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlStatus.SelectedIndex = 0;
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudentsRemain.DataSource = null;
        lvStudentsRemain.DataBind();
        pnlCourses.Visible = false;
        pnlStudents.Visible = false;
        pnlStudentsReamin.Visible = false;
        btnSubmit.Enabled = false;

        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));

        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM WITH (NOLOCK) INNER JOIN ACD_COLLEGE_MASTER CM ON (SM.COLLEGE_ID = CM.COLLEGE_ID)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND SM.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                ddlSession.Focus();
            }
        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Add(new ListItem("Please Select", "0"));
            //objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
    if (ddlAdmBatch.SelectedIndex > 0)
        {
        //Commented by As per Requirement Of Romal Saluja Sir 

        // objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        DataSet ds = objCommon.GetSemesterSessionWise(Schemeno, SessionNo, 1);
        if (ds != null && ds.Tables.Count > 0)
        {
            ddlSemester.DataSource = ds;
            ddlSemester.DataTextField = "SEMESTERNAME";
            ddlSemester.DataValueField = "SEMESTERNO";
            ddlSemester.DataBind();
        }
        //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SES ON(SES.ODD_EVEN = SM.ODD_EVEN)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND SES.SESSIONNO =" + ddlSession.SelectedValue, "SM.SEMESTERNO");
        ddlSemester.Focus();
        }
    else
        {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        }

    }
    protected void lnkPrintRegReport_Click(object sender, EventArgs e)
    {
        int idno = Convert.ToInt32((sender as Button).ToolTip);

        if (Convert.ToInt32(Session["OrgId"]) == 2)
            ShowReportStudentWise(idno, "RegistrationSlip", "rptBulkCourseRegslipForCrescentStudentWise.rpt");
        else
        {
            //  ShowReportStudentWise(idno, "RegistrationSlip", "rptBulkCourseRegslipStudentwise.rpt"); // commented by vipul T. As per T-49243 on dated 26.10.2023
            ShowReportStudentWise(idno, "RegistrationSlip", "rptCourseRegSlip.rpt"); // added by vipul T. As per T-49243 on dated 26.10.2023
        }
    }

    private void ShowReportStudentWise(int idno, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9 || Convert.ToInt32(Session["OrgId"]) == 7)
            {
                url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
                    + ",@P_SESSIONNO=" + ddlSession.SelectedValue
                    + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                    + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue
                    + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"])
                    + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue
                    + ",@UserName=" + Session["username"]
                    + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                    + ",@P_IDNO=" + idno;            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            }
            else
            {
                if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()
                    + ",@P_SESSIONNO=" + ddlSession.SelectedValue
                    + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
                    + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue
                    + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"])
                    + ",@P_ADMBATCH=" + ddlAdmBatch.SelectedValue
                    + ",@UserName=" + Session["username"]
                    + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                    + ",@P_IDNO=" + idno;            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                }
                else
                {
                    url += "&param=@P_COLLEGE_CODE=" + ViewState["college_id"].ToString()
                       + ",@P_IDNO=" + idno
                       + ",@P_SESSIONNO=" + ddlSession.SelectedValue
                       + ",@UserName=" + Session["username"].ToString();
                }
            }
            
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updBulkReg, this.updBulkReg.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void cbRow_CheckedChanged(object sender, EventArgs e)
    {
        double TotalCredit = 0;
        double credits = 0;
        TotalCredit = Convert.ToDouble(lblTotalCredit.Text);
        foreach (ListViewDataItem dataitemCourse in lvCourse.Items)
        {
            CheckBox chkAccept = dataitemCourse.FindControl("cbRow") as CheckBox;
            if (chkAccept.Checked == true)
            {
                credits = credits + double.Parse(chkAccept.ToolTip);
            }
        }
        if (TotalCredit < credits)
        {
            //objCommon.DisplayMessage(updBulkReg, "Total Credit " + TotalCredit, this.Page);
            objCommon.DisplayMessage(updBulkReg, "The Total Credits limits is " + TotalCredit + ", Selected credit is " + credits + ", Kindly check the course credits", this.Page);
            return;
        }
    }
}
