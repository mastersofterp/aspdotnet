
//======================================================================================
// PROJECT NAME  : COMMON CODE                                                          
// MODULE NAME   : EXAMINATION
// PAGE NAME     : REMOVE ABSENT ENTRY [EXAMINATION]
// CREATION DATE :                                         
// CREATED BY    : PRAFULL MUKE          
                                                 
//=======================================================================================


using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;

public partial class ACADEMIC_EXAMINATION_AbscentIgradeRemove : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamC = new ExamController();
    Exam ObjE = new Exam();
    int UA_NO = 0;

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
                Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
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

                }

            }

            //BindListView();
            //this.PopulatePattern();
            ViewState["action"] = "add";
            btnSave.Visible = false;
            // GenerateDyanamicJavaScript();
            ViewState["userno"] = Session["userno"].ToString();
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbscentIgradeRemove.aspx");
            }
        }
        else
        {
                Response.Redirect("~/notauthorized.aspx?page=AbscentIgradeRemove.aspx");
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
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
            }
            lvAbIGRemove.Visible = false;
            lvAbIGRemove.DataSource = null;
            lvAbIGRemove.DataBind();
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT AR on (S.SEMESTERNO=AR.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SEMESTERNAME", "SESSIONNO=" + ddlSession.SelectedValue + "", "SEMESTERNO ASC");
        lvAbIGRemove.DataSource = null;
        lvAbIGRemove.DataBind();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCourse, "ACD_COURSE AC INNER JOIN ACD_STUDENT_RESULT AR ON (AC.SCHEMENO=AR.SCHEMENO)", "DISTINCT AC.COURSENO", "AC.CCODE +' - '+ AC.COURSE_NAME AS COURSENAME", " AR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND AR.SCHEMENO="+Convert.ToInt32(Convert.ToInt32(ViewState["schemeno"]))+" AND  AR.SESSIONNO=" + ddlSession.SelectedValue + "", "COURSENO DESC");  //AND  AR.SESSIONNO='" + ddlSession.SelectedValue + "'
        lvAbIGRemove.DataSource = null;
        lvAbIGRemove.DataBind();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvAbIGRemove.DataSource = null;
        lvAbIGRemove.DataBind();
        ddlAbIgEntry.SelectedIndex = 0;
    }

    protected void ddlAbIgEntry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAbIgEntry.SelectedValue == "1")
        {
            objCommon.FillDropDownList(ddlExamName, "ACD_EXAM_NAME A INNER JOIN ACD_STUDENT_RESULT R on(A.COLLEGE_CODE=R.COLLEGE_CODE)", "DISTINCT A.EXAMNO", " ISNULL(A.EXAMNAME,'') as EXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND A.EXAMNO>0", "");
        }
        else if (ddlAbIgEntry.SelectedValue == "2")
        {
            objCommon.FillDropDownList(ddlExamName, "ACD_EXAM_NAME A INNER JOIN ACD_STUDENT_RESULT R on(A.COLLEGE_CODE=R.COLLEGE_CODE)", "DISTINCT A.EXAMNO", " ISNULL(A.EXAMNAME,'') as EXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND A.EXAMNO>0", "");
        }

        //lvAbIGRemove.Visible = false;
        lvAbIGRemove.DataSource = null;
        lvAbIGRemove.DataBind();
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSubExamName, "ACD_EXAM_NAME AN INNER JOIN ACD_SUBEXAM_NAME AC ON (AN.EXAMNO=AC.EXAMNO)", " DISTINCT AC.SUBEXAMNO", "AC.SUBEXAMNAME", "AC.EXAMNO=" + ddlExamName.SelectedValue, "");
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }


    private void BindListView()
    {
        lvAbIGRemove.DataSource = null;
        lvAbIGRemove.DataBind();
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int Session = Convert.ToInt32(ddlSession.SelectedValue);
        int Semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int Courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        string AbIGEntry = string.Empty;
        //int Examno = Convert.ToInt32(ddlExamName.SelectedValue);
        //int SubExamno = Convert.ToInt32(ddlSubExamName.SelectedValue);

        if (ddlAbIgEntry.SelectedValue == "1")
        {
            AbIGEntry = "AB";

        }
        else if (ddlAbIgEntry.SelectedValue == "2")
        {
            AbIGEntry = "I";
        }
        else
        {
            AbIGEntry = "UFM";
        }


        DataSet ds = objExamC.GetStudentByExam(Schemeno, Session, Semester, Courseno, AbIGEntry);

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            lvAbIGRemove.DataSource = ds;
            lvAbIGRemove.DataBind();
            lvAbIGRemove.Visible = true;
            ViewState["ccode"] = ds.Tables[0].Rows[0]["CCODE"].ToString();
            ViewState["grade"] = ds.Tables[0].Rows[0]["GRADE"].ToString();
            ViewState["SEMESTERNO"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();

        }
        else
        {
            objCommon.DisplayMessage(this.Page, "No Data Found For Current Selection", this.Page);
            btnSave.Visible = false;
            lvAbIGRemove.DataSource = null;
            lvAbIGRemove.DataBind();
           // ClearControls();
            return;

        }

        foreach (ListViewItem item in lvAbIGRemove.Items)
        {
            CheckBox chk_AbGrade = (CheckBox)item.FindControl("chk_AbGrade");
            CheckBox chk_Igrade = (CheckBox)item.FindControl("IGrade");
            CheckBox chk_Ufmgrade = (CheckBox)item.FindControl("UfmGrade");

            if (ddlAbIgEntry.SelectedValue == "1")
            {
                chk_AbGrade.Visible = true;
                chk_Igrade.Visible = false;
                chk_Ufmgrade.Visible = false;
                //lblGrade.Text = "AbGrade";

            }
            else if (ddlAbIgEntry.SelectedValue == "2")
            {
                chk_Igrade.Visible = true;
                chk_AbGrade.Visible = false;
                chk_Ufmgrade.Visible = false;
                //lblGrade.Text = "IGrade";
            }
            else if (ddlAbIgEntry.SelectedValue == "3")
            {
                chk_Igrade.Visible = false;
                chk_AbGrade.Visible = false;
                chk_Ufmgrade.Visible = true;
                //lblGrade.Text = "IGrade";
            }


        }
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            Label lblGrade = this.lvAbIGRemove.Controls[0].FindControl("AbIgade") as Label;
            if (ddlAbIgEntry.SelectedValue == "1")
            {
                lblGrade.Text = "Absent Grade";
            }
            else if (ddlAbIgEntry.SelectedValue == "2")
            {
                lblGrade.Text = "I Grade";
            }
        }
        btnSave.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int Schemeno = Convert.ToInt32(ViewState["schemeno"]);
        int Session = Convert.ToInt32(ddlSession.SelectedValue);
        int Semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int Courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        string Ccode = Convert.ToString(ViewState["ccode"]);
        string idno = "";
        string hfd_IDNO = "";
        string ABgrade = "";
        string IGrdae = "";
        string UFMGrdae = "";
        string NewGrade = string.Empty;
        string OldGrade = string.Empty;
        int Semsterno = Convert.ToInt32(ViewState["SEMESTERNO"]);


        foreach (ListViewItem item in lvAbIGRemove.Items)
        {
            CheckBox chk_AbGrade = (CheckBox)item.FindControl("chk_AbGrade");
            CheckBox chk_Igrade = (CheckBox)item.FindControl("IGrade");
            CheckBox UfmGrade = (CheckBox)item.FindControl("UfmGrade");
            hfd_IDNO = ((HiddenField)item.FindControl("hdf_IDNO")).Value;
            if (ddlAbIgEntry.SelectedValue == "1")
            {
                if (chk_AbGrade.Checked == false)
                {

                    if (chk_AbGrade.Checked == false && chk_AbGrade.Enabled == true)
                    {
                        ABgrade += "1" + ",";
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        ABgrade += "0" + ',';
                    }

                    if (chk_Igrade.Checked == false && chk_Igrade.Enabled == true)
                    {
                        IGrdae += "1" + ',';
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        IGrdae += "0" + ',';
                    }
                    OldGrade += ViewState["grade"] + ",";
                    idno += hfd_IDNO + ",";
                }
            }
            else if (ddlAbIgEntry.SelectedValue == "2")
            {
                if (chk_Igrade.Checked == false )
                {

                    if (chk_AbGrade.Checked == false && chk_AbGrade.Enabled == true)
                    {
                        ABgrade += "1" + ",";
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        ABgrade += "0" + ',';
                    }

                    if (chk_Igrade.Checked == false && chk_Igrade.Enabled == true)
                    {
                        IGrdae += "1" + ',';
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        IGrdae += "0" + ',';
                    }
                    OldGrade += ViewState["grade"] + ",";
                    idno += hfd_IDNO + ",";
                }
                
            }
            else if (ddlAbIgEntry.SelectedValue == "3")
            {
                if (UfmGrade.Checked == false)
                {

                    if (UfmGrade.Checked == false && UfmGrade.Enabled == true)
                    {
                        ABgrade += "1" + ",";
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        ABgrade += "0" + ',';
                    }

                    if (chk_Igrade.Checked == false && chk_Igrade.Enabled == true)
                    {
                        IGrdae += "1" + ',';
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        IGrdae += "0" + ',';
                    }


                    if (UfmGrade.Checked == false && UfmGrade.Enabled == true)
                    {
                        UFMGrdae += "1" + ",";
                        //OldGrade += ViewState["grade"] + ",";
                        //idno += hfd_IDNO + ",";
                    }
                    else
                    {
                        UFMGrdae += "0" + ',';
                    }

                    OldGrade += ViewState["grade"] + ",";
                    idno += hfd_IDNO + ",";
                }
            }

        }
       // return;

        CustomStatus cs = (CustomStatus)objExamC.UpdateStudentByGrade(Schemeno, Session, Semester, Courseno, Ccode, idno, ABgrade, IGrdae, UFMGrdae, OldGrade, Convert.ToInt32(ViewState["userno"]));

        if (cs.Equals(CustomStatus.RecordUpdated))
        //if (1==1)
        {
            objCommon.DisplayMessage(updGradeRemove, "Grade Removed Successfully!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(updGradeRemove, "Failed to Remove Grade!", this.Page);
        }
       



        BindListView();
        //ClearControls();


    }
    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    private void ClearControls()
    {
        ddlCollege.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlAbIgEntry.SelectedIndex = 0;
        ddlExamName.SelectedIndex = 0;
        ddlSubExamName.SelectedIndex = 0;
        btnSave.Visible = false;
        lvAbIGRemove.Visible = false;
    }

}

