//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : COURSE TEACHER ALLOTMMENT                                            
// CREATION DATE : 05-JULY-2011                                                          
// CREATED BY    : S.Patil                                                
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
using System.Linq;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_courseAllot_Auto : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;
    AllotmentMaster objAM = new AllotmentMaster();
    CourseTeacherAllotController objCTA = new CourseTeacherAllotController();
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());

                    //if (dec == "1")
                    //{
                    //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEPTNO=" + deptno,"");
                    //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_BRANCH B ON D.DEGREENO=B.DEGREENO ", "distinct(D.DEGREENO)", "DEGREENAME", "DEPTNO=" + deptno, "DEGREENO");
                    //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                    //}
                    //else
                    //{
                    ViewState["DEPTNO"] = "0";
                    //}
                }
                else
                {
                    ViewState["DEPTNO"] = "0";
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");
                }

                PopulateDropDownList();
                FillTeacher();

                dvAdt.Visible = true;
                FillAdTeacher();
                btnPrint.Enabled = false;
                BindListView();
            }
            Session["reportdata"] = null;
            ViewState["action"] = null;

            //ddlroommon.SelectedValue = "0";
            //ddlroomtue.SelectedValue = "0";
            //ddlroomwed.SelectedValue = "0";
            //ddlroomthur.SelectedValue = "0";
            //ddlroomfri.SelectedValue = "0";
            //ddlroomsat.SelectedValue = "0";
            lbl_mon.Visible = false;
            lbl_tue.Visible = false;
            lbl_wed.Visible = false;
            lbl_thur.Visible = false;
            lbl_fri.Visible = false;
            lbl_sat.Visible = false;

            ddlRoomMon.Visible = false;
            ddlRoomTue.Visible = false;
            ddlRoomWed.Visible = false;
            ddlRoomThur.Visible = false;
            ddlRoomFri.Visible = false;
            ddlRoomSat.Visible = false;
        }
        divMsg.InnerHtml = string.Empty;

        //objCommon.ReportPopUp(btnPrint, "pagetitle=UAIMS(Course Allotment Report)&path=~" + "," + "Reports" + "," + "Academic" + "," + "rptCourse_Allotment.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString() + "," + "@P_SESSIONNO=" + ddlSession.SelectedValue + "," + "@P_SCHEMENO=" + ddlScheme.SelectedValue + "," + "@P_SEMESTERNO=" + ddlSem.SelectedValue, "UAMIS");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                objCommon.FillDropDownList(ddlSem, "ACD_COURSE SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SCHEMENO = " + ddlScheme.SelectedValue + "", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
            }
            else
            {
                ddlSem.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillRooms()
    {
        DataSet ds1 = objCTA.Getroomsdepartment(Convert.ToInt32(ddlDegree.SelectedValue));
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lbl_mon.Visible = true;
            lbl_tue.Visible = true;
            lbl_wed.Visible = true;
            lbl_thur.Visible = true;
            lbl_fri.Visible = true;
            lbl_sat.Visible = true;
            ddlRoomMon.Visible = true;
            ddlRoomTue.Visible = true;
            ddlRoomWed.Visible = true;
            ddlRoomThur.Visible = true;
            ddlRoomFri.Visible = true;
            ddlRoomSat.Visible = true;
            ddlRoomMon.DataSource = ds1;
            ddlRoomMon.DataTextField = "ROOMNAME";
            ddlRoomMon.DataValueField = "Roomno";
            ddlRoomMon.DataBind();

            ddlRoomTue.DataSource = ds1;
            ddlRoomTue.DataTextField = "ROOMNAME";
            ddlRoomTue.DataValueField = "Roomno";
            ddlRoomTue.DataBind();

            ddlRoomWed.DataSource = ds1;
            ddlRoomWed.DataTextField = "ROOMNAME";
            ddlRoomWed.DataValueField = "Roomno";
            ddlRoomWed.DataBind();

            ddlRoomThur.DataSource = ds1;
            ddlRoomThur.DataTextField = "ROOMNAME";
            ddlRoomThur.DataValueField = "Roomno";
            ddlRoomThur.DataBind();

            ddlRoomFri.DataSource = ds1;
            ddlRoomFri.DataTextField = "ROOMNAME";
            ddlRoomFri.DataValueField = "Roomno";
            ddlRoomFri.DataBind();

            ddlRoomSat.DataSource = ds1;
            ddlRoomSat.DataTextField = "ROOMNAME";
            ddlRoomSat.DataValueField = "Roomno";
            ddlRoomSat.DataBind();

        }
        else
        {

            //chksat.SelectedIndex
        }
    }
    private void FillCourse()
    {
        try
        {
            CourseController objCC = new CourseController();
            DataSet dsCourse = objCC.GetCourseForCourseAllotment(Convert.ToInt32(ddlScheme.SelectedValue));

            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            if (dsCourse.Tables.Count > 0)
            {
                ddlCourse.DataValueField = dsCourse.Tables[0].Columns[0].ColumnName;
                ddlCourse.DataTextField = dsCourse.Tables[0].Columns[1].ColumnName;
                ddlCourse.DataSource = dsCourse;
                ddlCourse.DataBind();
            }
            else
            {
                ddlCourse.DataSource = null;
                ddlCourse.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_courseAllot.FillCourse-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void BindSchemeforSelectedCourseListView()
    {
        try
        {
            CourseController objCC = new CourseController();
            if (ddlCourse.SelectedIndex > 0)
            //if (ddlSection.SelectedIndex > 0)
            {
                string[] CCodeInfo = ddlCourse.SelectedItem.Text.Split('-');
                string CCode = CCodeInfo[0];

                DataSet ds = objCC.GetSchemeforAllotmentCCode(CCode, Convert.ToInt32(ddlSem.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objCommon.DisplayUserMessage(updpnl, "Selected Elective Subject Offered in Below Scheme, Section Intake considers for below programmes students", this.Page);
                    lvSchemewithSelectedCourse.DataSource = ds;
                    lvSchemewithSelectedCourse.DataBind();
                    lvSchemewithSelectedCourse.Visible = true;
                    dvSchemewithSelectedCourse.Visible = true;
                }
                else
                {
                    lvSchemewithSelectedCourse.DataSource = null;
                    lvSchemewithSelectedCourse.DataBind();
                    lvSchemewithSelectedCourse.Visible = false;
                    dvSchemewithSelectedCourse.Visible = false;
                }
            }
            else
            {
                lvSchemewithSelectedCourse.DataSource = null;
                lvSchemewithSelectedCourse.DataBind();
                lvSchemewithSelectedCourse.Visible = false;
                dvSchemewithSelectedCourse.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.BindSchemeforSelectedCourseListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListView()
    {
        try
        {
            CourseController objCC = new CourseController();
            if (ddlScheme.SelectedIndex > 0)
            {
                ////DataSet ds = objCC.GetCourseAllotment(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue));
                DataSet ds = objCC.GetCourseAllotmentSectionwiseAuto(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourse.DataSource = ds;
                    lvCourse.DataBind();
                    lvCourse.Visible = true;
                    btnPrint.Enabled = true;
                    dvCourse.Visible = true;
                }
                else
                {
                    lvCourse.DataSource = null;
                    lvCourse.DataBind();
                    lvCourse.Visible = false;
                    btnPrint.Enabled = false;
                    dvCourse.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTeacher.SelectedIndex = 0;
        lbltotcount.Text = "Total student Count  " + objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN  ACD_STUDENT S ON SR.IDNO=S.IDNO INNER JOIN ACD_COURSE C ON C.COURSENO=SR.COURSENO ", "COUNT(SR.IDNO)TOTCNT", " SR.SESSIONNO=" + ddlSession.SelectedValue + " AND C.COURSENO= " + ddlCourse.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND  SR.SEMESTERNO = " + ddlSem.SelectedValue + "  AND S.BRANCHNO=" + ddlBranch.SelectedValue + " AND S.DEGREENO= " + ddlDegree.SelectedValue + " AND S.SECTIONNO= " + ddlSection.SelectedValue + " ");
        BindSchemeforSelectedCourseListView();
        //BindListView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        //Response.Redirect(Request.Url.ToString());
    }

    private void Clear()
    {
        ddlScheme.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        lvSchemewithSelectedCourse.DataSource = null;
        lvSchemewithSelectedCourse.DataBind();
        lvSchemewithSelectedCourse.Visible = false;
        dvSchemewithSelectedCourse.Visible = false;
        ddlTeacher.SelectedIndex = 0;
        txtTot.Text = string.Empty;
        lblStatus.Text = string.Empty;
        btnPrint.Enabled = false;
        dvAdt.Visible = false;
        lvAdTeacher.DataSource = null;
        lvAdTeacher.DataBind();
        ddltheorypractical.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlDeptName.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        lvCourse.DataSource = null;
        lvCourse.DataBind();
        dvCourse.Visible = false;
        txtIntake.Text = string.Empty;
        ddlSession.Enabled = true;
        ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSem.Enabled = true;
        ddlSection.Enabled = true;
        ddlSubjectType.Enabled = true;
        ddlCourse.Enabled = true;
        btnAd.Text = "Submit";
        ViewState["action"] = null;
    }
    private void ClearAfterUpdate()
    {
        ddlSession.Enabled = true;
        ddlDegree.Enabled = true;
        ddlBranch.Enabled = true;
        ddlScheme.Enabled = true;
        ddlSem.Enabled = true;
        ddlSection.Enabled = true;
        ddlSubjectType.Enabled = true;
        ddlCourse.Enabled = true;
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
        ddlCourse.SelectedItem.Value = "0";
        ddlSubjectType.SelectedIndex = 0;
        ddlDeptName.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
        txtIntake.Text = string.Empty;
        lvAdTeacher.DataSource = null;
        lvAdTeacher.DataBind();
        dvAdt.Visible = false;
        btnGenerate.Text = "Submit";
        ClearAfterInsert();
        ViewState["action"] = null;
    }

    private void ClearAfterInsert()
    {
        txtMon.Text = "";
        txtTue.Text = "";
        txtWed.Text = "";
        txtThu.Text = "";
        txtFri.Text = "";
        txtSat.Text = "";
        ddlTeacher.SelectedIndex = 0;
        foreach (ListItem item in ddlRoomMon.Items)
        {
            item.Selected = false;

        }
        foreach (ListItem item in ddlRoomTue.Items)
        {
            item.Selected = false;

        }
        foreach (ListItem item in ddlRoomWed.Items)
        {
            item.Selected = false;

        }
        foreach (ListItem item in ddlRoomThur.Items)
        {
            item.Selected = false;

        }
        foreach (ListItem item in ddlRoomFri.Items)
        {
            item.Selected = false;

        }
        foreach (ListItem item in ddlRoomSat.Items)
        {
            item.Selected = false;

        }
        ViewState["action"] = null;
    }
    protected void btnAd_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int subid = 0;
            int dup = 0;
            // int chkmarkentry = 0;
            CourseController objCC = new CourseController();
            Student_Acd objStudent = new Student_Acd();
            // int allowmarkentry = 0;

            //if (ddltheorypractical.SelectedValue == "1")
            //{
            foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            {
                CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
                if (chkIDNo.Checked == true)
                {
                    objStudent.AdTeacher += chkIDNo.ToolTip + ",";
                }
                //else   // *** commented on 20/08/2019
                //    count++;

                //if (lvAdTeacher.Items.Count == count)
                //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
                //****  end  ****************
            }

            if (objStudent.AdTeacher.Contains(ddlTeacher.SelectedValue))
            {
                String str = objStudent.AdTeacher;
                dup = Convert.ToInt32(ddlTeacher.SelectedValue);
                var uniques = str.Split(',').Reverse().Distinct().Take(dup).Reverse().Take(dup).ToList();
                objStudent.AdTeacher = string.Join(",", uniques.ToArray());
            }
            //else
            //    objStudent.AdTeacher += ddlTeacher.SelectedValue + ","; // *** commented on 20/08/2019


            if (objStudent.AdTeacher.Length > 0)
            {
                if (objStudent.AdTeacher.Substring(objStudent.AdTeacher.Length - 1) == ",")
                    objStudent.AdTeacher = objStudent.AdTeacher.Substring(0, objStudent.AdTeacher.Length - 1);
            }
            //}
            //else
            //{
            //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
            //}
            //objStudent.AdTeacher += chkIDNo.ToolTip + ",";
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            // objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            //objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.Sem = ddlSem.SelectedValue;
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);

            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
            //objStudent.AdTeacher = (ddlTeacher.SelectedValue);

            //subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "subid", "CourseNo=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            objStudent.Pract_Theory = Convert.ToInt32(ddlSubjectType.SelectedValue); //Convert.ToInt32(subid);
            objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
            //objStudent.Intake = Convert.ToInt32(txtIntake.Text.Trim());

            object objret = 0;

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                objret = objCC.AddCourseAllot(objStudent);
            }
            else
            {


                if (lvSchemewithSelectedCourse.Visible == true)
                {
                    foreach (ListViewDataItem dataitem in lvSchemewithSelectedCourse.Items)
                    {
                        Label lblSchemeWithSelectedCourse = dataitem.FindControl("lblSchemeWithSelectedCourse") as Label;
                        HiddenField hdfCourseNo = dataitem.FindControl("hdfCourseNo") as HiddenField;

                        count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER_DEMO", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(UA_NO,0)<>0 AND SCHEMENO=" + Convert.ToInt32(lblSchemeWithSelectedCourse.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(hdfCourseNo.Value) + " And th_pr =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue)));
                        if (count >= 1)
                        {
                            objCommon.DisplayUserMessage(updpnl, "This Course is Already alloted to another faculty !", this.Page);
                        }
                        else
                        {
                            objStudent.CourseNo = Convert.ToInt32(hdfCourseNo.Value);
                            objStudent.SchemeNo = Convert.ToInt32(lblSchemeWithSelectedCourse.ToolTip);
                            objret = objCC.AddCourseAllot(objStudent);
                        }
                    }
                }
                else
                {

                    count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER_DEMO", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(UA_NO,0)<>0  AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " And th_pr =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue)));
                    if (count >= 1)
                    {
                        objCommon.DisplayUserMessage(updpnl, "This Course is Already alloted to another faculty !", this.Page);

                    }
                    else
                    {
                        objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                        objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                        objret = objCC.AddCourseAllot(objStudent);
                    }
                }
            }

            if (Convert.ToInt32(objret) == 1 && ViewState["action"] == null)
            {
                objCommon.DisplayUserMessage(updpnl, "Course Teacher Allotted Successfully.", this.Page);
            }
            else if ((Convert.ToInt32(objret) == 1) && (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit")))
            {
                objCommon.DisplayUserMessage(updpnl, "Course Teacher Updated Successfully.", this.Page);
                this.ClearAfterUpdate();
            }
            else
            {
                objCommon.DisplayUserMessage(updpnl, "Course Allocation Fail Try Again..!", this.Page);
            }
            //}
            BindListView();
            //Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");

            //Fill Department
            //if (Session["usertype"].ToString() != "1")
            //    objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0 AND DEPTNO =" + Session["userdeptno"].ToString(), "DEPTNAME");
            //else
            objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");

            ddlDeptName.SelectedValue = deptno;
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillTeacher()
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));
            DataTable dtTeacher = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            //DropDownList
            ddlTeacher.Items.Clear();
            ddlTeacher.Items.Add(new ListItem("Please Select", "0"));
            if (dtTeacher.Rows.Count > 0)
            {
                ddlTeacher.DataSource = dtTeacher;
                ddlTeacher.DataTextField = dtTeacher.Columns["UA_FULLNAME"].ToString();
                ddlTeacher.DataValueField = dtTeacher.Columns["UA_NO"].ToString();
                ddlTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillAdTeacher()
    {
        try
        {
            //Populating Faculty dropdownlist
            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
            SqlParameter[] objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(ddlDeptName.SelectedValue));

            DataTable dtFaculty = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_ALL_FACULTIES_BY_DEPT", objParams).Tables[0];
            if (dtFaculty.Rows.Count > 0)
            {
                lvAdTeacher.DataSource = dtFaculty;
                lvAdTeacher.DataBind();
                lvAdTeacher.Visible = true;
            }
            else
            {
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillAdTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnDel = sender as ImageButton;
        CourseController objCC = new CourseController();
        Student_Acd objSA = new Student_Acd();
        objSA.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objSA.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
        objSA.UA_No = Convert.ToInt32(btnDel.AlternateText);
        objSA.CourseNo = Convert.ToInt32(btnDel.CommandArgument);

        int UA_No = Convert.ToInt32(btnDel.AlternateText);
        int CourseNo = Convert.ToInt32(btnDel.CommandArgument);

        objSA.sub_id = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);

        int sub_id = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);

        //objSA.Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
        objSA.Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
        //objSA.UANO_PRAC = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfuanoprac") as HiddenField).Value);
        objSA.Sectionno = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfSecNo") as HiddenField).Value);

        int Sectionno = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfSecNo") as HiddenField).Value);

        objSA.Ua_no_delete = Convert.ToInt32(Session["userno"]);
        // objSA.Intake = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdnintake") as HiddenField).Value);

        int MarkCount = 0;
        MarkCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(COUNT(COURSENO),0)CNT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(CourseNo) + "AND SUBID=" + Convert.ToInt32(sub_id) + "AND S1MARK IS NOT NULL AND S2MARK IS NOT NULL AND S3MARK IS NOT NULL AND EXTERMARK IS NOT NULL"));


        int AttendanceCount = 0;
        // CourseRegCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(COUNT(COURSENO),0)CNT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(CourseNo) + "AND SUBID=" + Convert.ToInt32(sub_id) + "AND SECTIONNO=" + Convert.ToInt32(Sectionno)));
        //CourseRegCount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT ISNULL(COUNT(COURSENO),0)CNT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(CourseNo) + "AND SUBID=" + Convert.ToInt32(sub_id)));
        AttendanceCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE", "DISTINCT ISNULL(COUNT(COURSENO),0)CNT", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND COURSENO=" + Convert.ToInt32(CourseNo) + "AND SUBID=" + Convert.ToInt32(sub_id)));
        if (MarkCount > 0 || AttendanceCount > 0)
        {
            objCommon.DisplayUserMessage(updpnl, "You cannot delete this Course Teacher Allotement, Because Attendance or Mark Entry Already Done for Selected Course", this.Page);
        }
        else
        {
            if (Convert.ToInt16(objCC.DeleteCourseAllot_Auto(objSA)) == Convert.ToInt16(CustomStatus.RecordUpdated))
            {
                //lblStatus.Text = "Course Teacher Deleted Successfully.";
                objCommon.DisplayUserMessage(updpnl, "Course Teacher Deleted Successfully.", this.Page);
                ////ddlSubjectType.SelectedIndex = 0;
                ////ddlCourse.SelectedIndex = 0;
                ////ddlSubjectType.Focus();
                BindListView();
                ////Clear();
            }
            else if (Convert.ToInt16(objCC.DeleteCourseAllot_Auto(objSA)) == Convert.ToInt16(CustomStatus.RecordFound))
            {
                objCommon.DisplayUserMessage(updpnl, "Can Not Delete Course because Mark Entry has been done.", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(updpnl, "Can Not Delete Course.", this.Page);
            }
            //lblStatus.Text = "Can Not Delete Course.";
            //lblStatus.Text = "Can Not Delete Course because Mark Entry has been done.";
        }
    }

    public string GetAdTeachers(object obj)
    {
        DataTableReader dtr = objCommon.FillDropDown("USER_ACC", "UA_FULLNAME", "UA_NO", "UA_NO IN (" + obj.ToString() + ")", "UA_FULLNAME").CreateDataReader();
        string teachers = string.Empty;
        while (dtr.Read())
        {
            teachers += dtr["UA_FULLNAME"].ToString() + ",";
        }
        dtr.Close();

        if (teachers.Substring(teachers.Length - 1) == ",")
            teachers = teachers.Substring(0, teachers.Length - 1);

        return teachers;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO = " + Session["userdeptno"].ToString(), "A.LONGNAME");
                else
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
            FillRooms();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C ", "DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "C.SCHEMENO = " + ddlScheme.SelectedValue + " AND C.SUBID = " + ddlSubjectType.SelectedValue + " AND C.SEMESTERNO = " + ddlSem.SelectedValue , "COURSE_NAME");

                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_ELECTGROUP E ON (isnull(C.GROUPNO,0)=isnull(E.GROUPNO,0))", "DISTINCT COURSENO", "CASE WHEN isnull(c.ELECT,0)=0 then CCODE + ' - ' + COURSE_NAME else (CCODE + ' - ' + COURSE_NAME +'['+ E.GROUPNAME +']') END AS COURSE_NAME", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "COURSE_NAME");
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(SR.CANCEL,0) = 0", "COURSE_NAME");
                ddlCourse.Focus();
                dvAdt.Visible = true;
                FillAdTeacher();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlSubjectType.SelectedIndex = 0;

                dvAdt.Visible = false;
                ddltheorypractical.SelectedIndex = 0;
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
                lvAdTeacher.Visible = false;//***********
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
            }
            //dvAdt.Visible = false;
            ddltheorypractical.SelectedIndex = 0;
            //lvAdTeacher.DataSource = null;
            //lvAdTeacher.DataBind();
            //lvAdTeacher.Visible = false;//***********
            //lvCourse.DataSource = null;
            //lvCourse.DataBind();
            //lvCourse.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSubjectType_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSem.SelectedIndex > 0)
            {

                int countsection = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT COUNT(ISNULL(SR.SECTIONNO,0))", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SECTIONNO > 0"));

                if (countsection > 0)
                {
                    objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SECTIONNO > 0", "SC.SECTIONNAME");
                }
                else
                {
                    objCommon.DisplayUserMessage(updpnl, "Please Register Subject for Selected Criteria...", this.Page);
                    // ddlSection.SelectedIndex = 0;
                    ddlSection.Items.Clear();
                    ddlSection.Items.Add("Please Select");
                    return;
                }

                //objCommon.FillDropDownList(ddlSection, "ACD_SECTION SC ", "DISTINCT SC.SECTIONNO", "SC.SECTIONNAME", " SC.SECTIONNO > 0", "SC.SECTIONNAME");

                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue", "C.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSem.SelectedIndex = 0;
            }
            //ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            lvSchemewithSelectedCourse.DataSource = null;
            lvSchemewithSelectedCourse.DataBind();
            lvSchemewithSelectedCourse.Visible = false;
            dvSchemewithSelectedCourse.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //BindSchemeforSelectedCourseListView();
            BindListView();

            ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            lvSchemewithSelectedCourse.DataSource = null;
            lvSchemewithSelectedCourse.DataBind();
            lvSchemewithSelectedCourse.Visible = false;
            dvSchemewithSelectedCourse.Visible = false;
            ddlTeacher.SelectedIndex = 0;
            lbltotcount.Text = string.Empty;
            //lvCourse.DataSource = null;
            //lvCourse.DataBind();
            ddlSubjectType.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSection_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTeacher();
        FillAdTeacher();
        dvAdt.Visible = true;
    }

    protected void ddltheorypractical_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltheorypractical.SelectedIndex > 0 && ddltheorypractical.SelectedValue == "1")
        {
            FillAdTeacher();
            //dvAdt.Visible = true;
        }
        else
        {
            lvAdTeacher.DataSource = null;
            lvAdTeacher.DataBind();
            dvAdt.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport(rdoReportType.SelectedValue, "rptCourse_Allotment1.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.btnPrint_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + ddlBranch.SelectedItem.Text + "_" + ddlSem.SelectedItem.Text + "." + rdoReportType.SelectedValue;  //+ ddlSection.SelectedItem.Text +
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_DEGREENAME=" + ddlDegree.SelectedItem.Text + ",@P_BRANCHNAME=" + ddlBranch.SelectedItem.Text + ",@P_SEMESTER=" + ddlSem.SelectedItem.Text;
            url += "&param=@P_USERNAME=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SECTIONNO=" + ddlSection.SelectedValue;

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " window.close();";
            //divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            CourseController objCC = new CourseController();
            Student_Acd objSA = new Student_Acd();
            int SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            int UA_No = Convert.ToInt32(btnDel.AlternateText);
            int CourseNo = Convert.ToInt32(btnDel.CommandArgument);

            int sub_id = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
            //objSA.Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
            int Th_Pr = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfthpr") as HiddenField).Value);
            //objSA.UANO_PRAC = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfuanoprac") as HiddenField).Value);
            int Sectionno = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdfSecNo") as HiddenField).Value);

            DataSet ds = null;
            //int Intake = Convert.ToInt32((((sender as ImageButton).Parent as ListViewDataItem).FindControl("hdnintake") as HiddenField).Value);

            //ds = objCommon.FillDropDown("ACD_COURSE_TEACHER_DEMO AC INNER JOIN ACD_SCHEME SC ON(AC.SCHEMENO=SC.SCHEMENO) INNER JOIN USER_ACC UA ON(UA.UA_NO=AC.UA_NO)", "AC.SESSIONNO,SC.BRANCHNO,SC.DEGREENO,SC.SCHEMENO,AC.COURSENO,AC.CCODE,AC.SUBID,AC.TH_PR,AC.UA_NO,AC.SECTIONNO,AC.BATCHNO,AC.DEPTNO,AC.SEMESTERNO,AC.INTAKE", "DBO.FN_DESC('SESSION',AC.SESSIONNO)SESSION,DBO.FN_DESC('DEGREENAME',SC.DEGREENO)DEGREE,DBO.FN_DESC('BRANCHLNAME',SC.BRANCHNO)BRANCH,DBO.FN_DESC('SCHEME',SC.SCHEMENO)SCHEME,DBO.FN_DESC('SEMESTER',AC.SEMESTERNO)SEMESTER,DBO.FN_DESC('SECTIONNAME',AC.SECTIONNO)SECTION,DBO.FN_DESC('SUBJECTTYPE',AC.SUBID)SUBNAME,DBO.FN_DESC('COURSENAME',AC.COURSENO)COURSENAME,UA.UA_DEPTNO", " AC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND AC.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND AC.COURSENO=" + Convert.ToInt32(btnDel.CommandArgument) + " AND AC.UA_NO=" + Convert.ToInt32(btnDel.AlternateText) + " AND AC.SUBID=" + Convert.ToInt32(sub_id) + " AND AC.SECTIONNO=" + Convert.ToInt32(Sectionno), "AC.COURSENO");
            ds = objCommon.FillDropDown("ACD_COURSE_TEACHER_DEMO AC INNER JOIN ACD_SCHEME SC ON(AC.SCHEMENO=SC.SCHEMENO) INNER JOIN USER_ACC UA ON(UA.UA_NO=AC.UA_NO) INNER JOIN ACD_COURSE C ON (C.COURSENO = AC.COURSENO)", "AC.SESSIONNO,SC.BRANCHNO,SC.DEGREENO,SC.SCHEMENO,AC.COURSENO,AC.CCODE,AC.SUBID,AC.TH_PR,AC.UA_NO,AC.SECTIONNO,AC.BATCHNO,AC.DEPTNO,AC.SEMESTERNO,AC.INTAKE,STUFF(( SELECT DISTINCT ','+ CAST(B.UA_NO AS NVARCHAR) FROM USER_ACC B INNER JOIN ACD_COURSE_TEACHER_DEMO CT CROSS APPLY DBO.SPLIT(CT.ADTEACHER,',') P ON (P.VALUE = B.UA_NO AND CT.COURSENO = C.COURSENO) FOR XML PATH('')),1,1,'') AS ADTEACHER", "DBO.FN_DESC('SESSION',AC.SESSIONNO)SESSION,DBO.FN_DESC('DEGREENAME',SC.DEGREENO)DEGREE,DBO.FN_DESC('BRANCHLNAME',SC.BRANCHNO)BRANCH,DBO.FN_DESC('SCHEME',SC.SCHEMENO)SCHEME,DBO.FN_DESC('SEMESTER',AC.SEMESTERNO)SEMESTER,DBO.FN_DESC('SECTIONNAME',AC.SECTIONNO)SECTION,DBO.FN_DESC('SUBJECTTYPE',AC.SUBID)SUBNAME,DBO.FN_DESC('COURSENAME',AC.COURSENO)COURSENAME,UA.UA_DEPTNO,ISNULL(LECT_COUNT1,0) LECT_COUNT1,ISNULL(LECT_COUNT2,0) LECT_COUNT2,ISNULL(LECT_COUNT3,0) LECT_COUNT3,ISNULL(LECT_COUNT4,0) LECT_COUNT4,ISNULL(LECT_COUNT5,0) LECT_COUNT5,ISNULL(LECT_COUNT6,0) LECT_COUNT6,ISNULL(ROOM1,0) ROOM1	,ISNULL(ROOM2,0) ROOM2	,ISNULL(ROOM3,0)	ROOM3,ISNULL(ROOM4,0) ROOM4	,ISNULL(ROOM5,0) ROOM5	,ISNULL(ROOM6,0) ROOM6	,ISNULL(ROOM7,0) ROOM7", " AC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND AC.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND AC.COURSENO=" + Convert.ToInt32(btnDel.CommandArgument) + " AND AC.UA_NO=" + Convert.ToInt32(btnDel.AlternateText) + " AND AC.SUBID=" + Convert.ToInt32(sub_id) + " AND AC.SECTIONNO=" + Convert.ToInt32(Sectionno), "AC.COURSENO");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtMon.Text = ds.Tables[0].Rows[0]["LECT_COUNT1"].ToString();
                txtTue.Text = ds.Tables[0].Rows[0]["LECT_COUNT2"].ToString();
                txtWed.Text = ds.Tables[0].Rows[0]["LECT_COUNT3"].ToString();
                txtThu.Text = ds.Tables[0].Rows[0]["LECT_COUNT4"].ToString();
                txtFri.Text = ds.Tables[0].Rows[0]["LECT_COUNT5"].ToString();
                txtSat.Text = ds.Tables[0].Rows[0]["LECT_COUNT6"].ToString();


                string[] strMon = ds.Tables[0].Rows[0]["ROOM1"].ToString().Split(',');
                string[] strTue = ds.Tables[0].Rows[0]["ROOM2"].ToString().Split(',');
                string[] strWed = ds.Tables[0].Rows[0]["ROOM3"].ToString().Split(',');
                string[] strThur = ds.Tables[0].Rows[0]["ROOM4"].ToString().Split(',');
                string[] strFri = ds.Tables[0].Rows[0]["ROOM5"].ToString().Split(',');
                string[] strSat = ds.Tables[0].Rows[0]["ROOM6"].ToString().Split(',');


                for (int i = 0; i < ddlRoomMon.Items.Count; i++)
                {
                    foreach (string value in strMon)
                    {
                        if (ddlRoomMon.Items[i].Value == value.ToString())
                        {
                            ddlRoomMon.Items[i].Selected = true;
                        }
                    }
                }

                for (int i = 0; i < ddlRoomTue.Items.Count; i++)
                {
                    foreach (string value in strTue)
                    {
                        if (ddlRoomTue.Items[i].Value == value.ToString())
                        {
                            ddlRoomTue.Items[i].Selected = true;
                        }
                    }
                }
                for (int i = 0; i < ddlRoomWed.Items.Count; i++)
                {
                    foreach (string value in strWed)
                    {
                        if (ddlRoomWed.Items[i].Value == value.ToString())
                        {
                            ddlRoomWed.Items[i].Selected = true;
                        }
                    }
                }
                for (int i = 0; i < ddlRoomThur.Items.Count; i++)
                {
                    foreach (string value in strThur)
                    {
                        if (ddlRoomThur.Items[i].Value == value.ToString())
                        {
                            ddlRoomThur.Items[i].Selected = true;
                        }
                    }
                }
                for (int i = 0; i < ddlRoomFri.Items.Count; i++)
                {
                    foreach (string value in strFri)
                    {
                        if (ddlRoomFri.Items[i].Value == value.ToString())
                        {
                            ddlRoomFri.Items[i].Selected = true;
                        }
                    }
                }
                for (int i = 0; i < ddlRoomSat.Items.Count; i++)
                {
                    foreach (string value in strSat)
                    {
                        if (ddlRoomSat.Items[i].Value == value.ToString())
                        {
                            ddlRoomSat.Items[i].Selected = true;
                        }
                    }
                }

                ddlSession.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                ddlScheme.SelectedValue = ds.Tables[0].Rows[0]["SCHEMENO"].ToString();
                ddlSem.SelectedValue = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                ddlSection.SelectedValue = ds.Tables[0].Rows[0]["SECTIONNO"].ToString();
                FillSubjectUpdate(); //Added Mahesh on Dated 03/02/2020 due to Subject type and Course bind on Same Method t.e seperated it.
                ddlSubjectType.SelectedValue = ds.Tables[0].Rows[0]["SUBID"].ToString();
                this.FillCourseUpdate();
                ddlCourse.SelectedValue = ds.Tables[0].Rows[0]["COURSENO"].ToString();
                //ddlSession.SelectedItem.Text = ds.Tables[0].Rows[0]["SESSION"].ToString();
                //ddlDegree.SelectedItem.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                //ddlBranch.SelectedItem.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                //ddlScheme.SelectedItem.Text = ds.Tables[0].Rows[0]["SCHEME"].ToString();
                //ddlSem.SelectedItem.Text = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
                //ddlSection.SelectedItem.Text = ds.Tables[0].Rows[0]["SECTION"].ToString();
                //ddlSubjectType.SelectedItem.Text = ds.Tables[0].Rows[0]["SUBNAME"].ToString();
                //ddlCourse.SelectedItem.Text = ds.Tables[0].Rows[0]["COURSENAME"].ToString();
                // objCommon.FillDropDownList(ddlDeptName, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "DEPTNO >0", "DEPTNAME");
                if (ds.Tables[0].Rows[0]["UA_DEPTNO"].ToString().Trim().Equals(""))
                {
                    ddlDeptName.SelectedIndex = 0;
                }
                else
                {
                    ddlDeptName.SelectedValue = ds.Tables[0].Rows[0]["UA_DEPTNO"].ToString().Trim();
                }
                this.FillTeacher();
                ddlTeacher.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                txtIntake.Text = ds.Tables[0].Rows[0]["INTAKE"].ToString();
                string adteacher = ds.Tables[0].Rows[0]["ADTEACHER"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["ADTEACHER"].ToString();

                ddlSession.Enabled = false;
                ddlDegree.Enabled = false;
                ddlBranch.Enabled = false;
                ddlScheme.Enabled = false;
                ddlSem.Enabled = false;
                ddlSection.Enabled = false;
                ddlSubjectType.Enabled = false;
                ddlCourse.Enabled = false;
                if (ds.Tables[0].Rows[0]["ADTEACHER"].ToString() != string.Empty && ds.Tables[0].Rows[0]["ADTEACHER"].ToString() != "")
                {
                    //FillAdTeacher();
                    FillAdTeacherUpdate(Convert.ToInt32(ddlDeptName.SelectedValue), adteacher);
                    dvAdt.Visible = true;

                }
                else
                {
                    dvAdt.Visible = true;
                    FillAdTeacher();
                }
                //btnAd.Text = "Update";
                btnGenerate.Text = "Update";
                ViewState["action"] = "edit";

            }
            else
            {
                ddlSubjectType.Enabled = true;
                ddlCourse.Enabled = true;
                ddlSubjectType.SelectedIndex = 0;
                ddlCourse.SelectedIndex = 0;
                ddlDeptName.SelectedIndex = 0;
                ddlTeacher.SelectedIndex = 0;
                txtIntake.Text = string.Empty;
                dvAdt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CourseAllotment.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void FillAdTeacherUpdate(int department, string AdTeacher)
    {
        try
        {

            //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            DataSet dtFaculty = objCommon.FillDropDown("USER_ACC", "UA_NO", "( UA_FULLNAME+'-'+(UA_NAME COLLATE DATABASE_DEFAULT) ) AS UA_FULLNAME", "UA_TYPE in(3)", "UA_FULLNAME");
            string[] strArr = AdTeacher.Split(',');
            if (dtFaculty.Tables[0].Rows.Count > 0)
            {
                lvAdTeacher.DataSource = dtFaculty;
                lvAdTeacher.DataBind();
                lvAdTeacher.Visible = true;

                foreach (ListViewItem item in lvAdTeacher.Items)
                {
                    CheckBox chkadteacher = item.FindControl("chkIDNo") as CheckBox;

                    foreach (string value in strArr)
                    {
                        if (chkadteacher.ToolTip == value.ToString())
                        {
                            chkadteacher.Checked = true;
                        }
                        else
                        {
                            // chkadteacher.Checked=false;
                        }
                    }
                }
                //FillAdTeacher();
            }
            else
            {
                lvAdTeacher.DataSource = null;
                lvAdTeacher.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.FillAdTeacher-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void FillSubjectUpdate()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO)", "DISTINCT COURSENO", "(CCODE + ' - ' + COURSE_NAME +'['+ E.GROUPNAME +']') AS COURSE_NAME ", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "COURSE_NAME");
            // objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
        }
        catch (Exception ex)
        { }
    }

    public void FillCourseUpdate()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_ELECTGROUP E ON (C.GROUPNO=E.GROUPNO)", "DISTINCT COURSENO", "(CCODE + ' - ' + COURSE_NAME +'['+ E.GROUPNAME +']') AS COURSE_NAME ", "SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSem.SelectedValue, "COURSE_NAME");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
        }
        catch (Exception ex)
        { }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int subid = 0;
            int dup = 0;
            // int chkmarkentry = 0;
            CourseController objCC = new CourseController();
            Student_Acd objStudent = new Student_Acd();
            // int allowmarkentry = 0;

            //if (ddltheorypractical.SelectedValue == "1")
            //{
            foreach (ListViewDataItem dataitem in lvAdTeacher.Items)
            {
                CheckBox chkIDNo = dataitem.FindControl("chkIDNo") as CheckBox;
                if (chkIDNo.Checked == true)
                {
                    objStudent.AdTeacher += chkIDNo.ToolTip + ",";
                }
                //else   // *** commented on 20/08/2019
                //    count++;

                //if (lvAdTeacher.Items.Count == count)
                //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
                //****  end  ****************
            }

            if (objStudent.AdTeacher.Contains(ddlTeacher.SelectedValue))
            {
                String str = objStudent.AdTeacher;
                dup = Convert.ToInt32(ddlTeacher.SelectedValue);
                var uniques = str.Split(',').Reverse().Distinct().Take(dup).Reverse().Take(dup).ToList();
                objStudent.AdTeacher = string.Join(",", uniques.ToArray());
            }
            //else
            //    objStudent.AdTeacher += ddlTeacher.SelectedValue + ","; // *** commented on 20/08/2019


            if (objStudent.AdTeacher.Length > 0)
            {
                if (objStudent.AdTeacher.Substring(objStudent.AdTeacher.Length - 1) == ",")
                    objStudent.AdTeacher = objStudent.AdTeacher.Substring(0, objStudent.AdTeacher.Length - 1);
            }
            //}
            //else
            //{
            //    objStudent.AdTeacher = (ddlTeacher.SelectedValue);
            //}
            //objStudent.AdTeacher += chkIDNo.ToolTip + ",";
            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            // objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
            //objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
            objStudent.Sem = ddlSem.SelectedValue;
            objStudent.Sectionno = Convert.ToInt32(ddlSection.SelectedValue);

            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
            //objStudent.AdTeacher = (ddlTeacher.SelectedValue);

            //subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "subid", "CourseNo=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            objStudent.Pract_Theory = Convert.ToInt32(ddlSubjectType.SelectedValue); //Convert.ToInt32(subid);
            objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
            //objStudent.Intake = Convert.ToInt32(txtIntake.Text.Trim());

            object objret = 0;
            string slotmon = string.Empty.Trim();
            string slottue = string.Empty.Trim();
            string slotwed = string.Empty.Trim();
            string slotthurs = string.Empty.Trim();
            string slotfri = string.Empty.Trim();
            string slotsat = string.Empty.Trim();

            if (txtMon.Text != string.Empty && txtMon.Text != "0")
            {
                slotmon = txtMon.Text.Trim();
                objAM.DAY1 = 1;
            }
            if (txtTue.Text != string.Empty && txtTue.Text != "0")
            {
                slottue = txtTue.Text.Trim();
                objAM.DAY2 = 1;
            }
            if (txtWed.Text != string.Empty && txtWed.Text != "0")
            {
                slotwed = txtWed.Text.Trim();
                objAM.DAY3 = 1;
            }
            if (txtThu.Text != string.Empty && txtThu.Text != "0")
            {
                slotthurs = txtThu.Text.Trim();
                objAM.DAY4 = 1;
            }
            if (txtFri.Text != string.Empty && txtFri.Text != "0")
            {
                slotfri = txtFri.Text.Trim();
                objAM.DAY5 = 1;
            }
            if (txtSat.Text != string.Empty && txtSat.Text != "0")
            {
                slotsat = txtSat.Text.Trim();
                objAM.DAY6 = 1;
            }


            objAM.SLOT1 = slotmon.Trim() == string.Empty ? "0" : slotmon.Trim();
            objAM.SLOT2 = slottue.Trim() == string.Empty ? "0" : slottue.Trim();
            objAM.SLOT3 = slotwed.Trim() == string.Empty ? "0" : slotwed.Trim();
            objAM.SLOT4 = slotthurs.Trim() == string.Empty ? "0" : slotthurs.Trim();
            objAM.SLOT5 = slotfri.Trim() == string.Empty ? "0" : slotfri.Trim();
            objAM.SLOT6 = slotsat.Trim() == string.Empty ? "0" : slotsat.Trim();

            string room1 = "";
            string room2 = "";
            string room3 = "";
            string room4 = "";
            string room5 = "";
            string room6 = "";


            int countRoomMon = 0;
            int countRoomTue = 0;
            int countRoomWed = 0;
            int countRoomThur = 0;
            int countRoomFri = 0;
            int countRoomSat = 0;

            for (int i = 0; i < ddlRoomMon.Items.Count; i++)
            {
                if (ddlRoomMon.Items[i].Selected == true)
                {
                    countRoomMon = countRoomMon + 1;
                    if (room1 != string.Empty)
                    {
                        room1 += "," + ddlRoomMon.Items[i].Value.Trim();
                    }
                    else
                    {
                        room1 = ddlRoomMon.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomMon < Convert.ToInt32(objAM.SLOT1))
            {
                room1 += "," + room1;
            }
            for (int i = 0; i < ddlRoomTue.Items.Count; i++)
            {
                if (ddlRoomTue.Items[i].Selected == true)
                {
                    countRoomTue = countRoomTue + 1;
                    if (room2 != string.Empty)
                    {
                        room2 += "," + ddlRoomTue.Items[i].Value.Trim();
                    }
                    else
                    {
                        room2 = ddlRoomTue.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomTue < Convert.ToInt32(objAM.SLOT2))
            {
                room2 += "," + room2;
            }
            for (int i = 0; i < ddlRoomWed.Items.Count; i++)
            {
                if (ddlRoomWed.Items[i].Selected == true)
                {
                    countRoomWed = countRoomWed + 1;
                    if (room3 != string.Empty)
                    {
                        room3 += "," + ddlRoomWed.Items[i].Value.Trim();
                    }
                    else
                    {
                        room3 = ddlRoomWed.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomWed < Convert.ToInt32(objAM.SLOT3))
            {
                room3 += "," + room3;
            }
            for (int i = 0; i < ddlRoomThur.Items.Count; i++)
            {
                if (ddlRoomThur.Items[i].Selected == true)
                {
                    countRoomThur = countRoomThur + 1;
                    if (room4 != string.Empty)
                    {
                        room4 += "," + ddlRoomThur.Items[i].Value.Trim();
                    }
                    else
                    {
                        room4 = ddlRoomThur.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomThur < Convert.ToInt32(objAM.SLOT4))
            {
                room4 += "," + room4;
            }
            for (int i = 0; i < ddlRoomFri.Items.Count; i++)
            {
                if (ddlRoomFri.Items[i].Selected == true)
                {
                    countRoomFri = countRoomFri + 1;
                    if (room5 != string.Empty)
                    {
                        room5 += "," + ddlRoomFri.Items[i].Value.Trim();
                    }
                    else
                    {
                        room5 = ddlRoomFri.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomFri < Convert.ToInt32(objAM.SLOT5))
            {
                room5 += "," + room5;
            }
            for (int i = 0; i < ddlRoomSat.Items.Count; i++)
            {
                if (ddlRoomSat.Items[i].Selected == true)
                {
                    countRoomSat = countRoomSat + 1;
                    if (room6 != string.Empty)
                    {
                        room6 += "," + ddlRoomSat.Items[i].Value.Trim();
                    }
                    else
                    {
                        room6 = ddlRoomSat.Items[i].Value.Trim();
                    }
                }
            }
            if (countRoomSat < Convert.ToInt32(objAM.SLOT6))
            {
                room6 += "," + room6;
            }
            objAM.ROOM1 = room1.Trim() == string.Empty ? "0" : room1.Trim();
            objAM.ROOM2 = room2.Trim() == string.Empty ? "0" : room2.Trim();
            objAM.ROOM3 = room3.Trim() == string.Empty ? "0" : room3.Trim();
            objAM.ROOM4 = room4.Trim() == string.Empty ? "0" : room4.Trim();
            objAM.ROOM5 = room5.Trim() == string.Empty ? "0" : room5.Trim();
            objAM.ROOM6 = room6.Trim() == string.Empty ? "0" : room6.Trim();


            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {

                objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                objret = objCC.AllotCourseAndTimeTable(objStudent, objAM);

            }
            else
            {
                if (lvSchemewithSelectedCourse.Visible == true)
                {
                    foreach (ListViewDataItem dataitem in lvSchemewithSelectedCourse.Items)
                    {
                        Label lblSchemeWithSelectedCourse = dataitem.FindControl("lblSchemeWithSelectedCourse") as Label;
                        HiddenField hdfCourseNo = dataitem.FindControl("hdfCourseNo") as HiddenField;

                        count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER_DEMO", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(UA_NO,0)<>0 AND SCHEMENO=" + Convert.ToInt32(lblSchemeWithSelectedCourse.ToolTip) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(hdfCourseNo.Value) + " And th_pr =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue)));
                        if (count >= 1)
                        {
                            objCommon.DisplayUserMessage(updpnl, "This Course is Already alloted to another faculty !", this.Page);
                        }
                        else
                        {
                            objStudent.CourseNo = Convert.ToInt32(hdfCourseNo.Value);
                            objStudent.SchemeNo = Convert.ToInt32(lblSchemeWithSelectedCourse.ToolTip);
                            objret = objCC.AllotCourseAndTimeTable(objStudent, objAM);

                        }
                    }
                }
                else
                {

                    count = Convert.ToInt32(objCommon.LookUp("ACD_COURSE_TEACHER_DEMO", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(UA_NO,0)<>0  AND SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " And th_pr =" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SECTIONNO = " + Convert.ToInt32(ddlSection.SelectedValue)));
                    if (count >= 1)
                    {
                        objCommon.DisplayUserMessage(updpnl, "This Course is Already alloted to another faculty !", this.Page);
                    }
                    else
                    {
                        objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                        objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                        objret = objCC.AllotCourseAndTimeTable(objStudent, objAM);

                    }
                }
            }

            if (Convert.ToInt32(objret) == 1 && ViewState["action"] == null)
            {
                objCommon.DisplayUserMessage(updpnl, "Course Teacher Allotted Successfully.", this.Page);
                ClearAfterInsert();
            }
            else if ((Convert.ToInt32(objret) == 1) && (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit")))
            {
                objCommon.DisplayUserMessage(updpnl, "Course Teacher Updated Successfully.", this.Page);
                this.ClearAfterUpdate();
            }
            else
            {
                objCommon.DisplayUserMessage(updpnl, "Course Allocation Fail Try Again..!", this.Page);
            }
            //}
            BindListView();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.btnAd_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}