//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : bulk semester promotion                                    
// CREATION DATE : 15-OCT-2016
// CREATED BY    :                                                 
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;

public partial class ACADEMIC_BulkSemesterPromotionWithoutCriteria : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                this.PopulateDropDownList();
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  - 

            btnSave.Visible = false;
            btnCancelSemPromotion.Visible = false;
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.ORGANIZATION_ID = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "(COSCHNO,COL_SCHEME_NAME)", "", "SM.COLLEGE_ID =" + (Convert.ToInt32(Session["college_nos"])) AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND (DB.DEPTNO =ISNULL  + (Convert.ToInt32(Session["userdeptno"]), 0)", "");
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else

                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0  AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");
            //ddlSession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=teacherallotment.aspx");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student objS = new Student();
            bool flag = false;
            foreach (ListViewDataItem item in lvStudent.Items)
            {

                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                ////***Label lblstatus = item.FindControl("lblstatus") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                if (chksub.Checked == true && chksub.Enabled == true)
                {
                    flag = true;//************
                    ////****if (lblstatus.Text == "YES")
                    ////****{

                    //Added by nehal n on 06072023 to handle section null value
                    string sectionno = "0";
                    if (lblstudname.ToolTip == string.Empty)
                    {
                        sectionno = "0";
                    }
                    else
                    {
                        sectionno = lblstudname.ToolTip;
                    }

                    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objS.IdNo = Convert.ToInt32(lblregno.ToolTip);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.SectionNo = Convert.ToInt32(sectionno);
                    objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    objS.CollegeCode = Session["colcode"].ToString();
                    objS.Dob = DateTime.Now;
                    int yearID = Convert.ToInt32(ddlAcdYear.SelectedValue);
                    if (!lblregno.Text.Trim().Equals(string.Empty)) objS.RollNo = lblregno.Text.Trim();
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];

                    //  int promotion = (GetViewStateItem("PROMOTIONNO") != string.Empty ? int.Parse(GetViewStateItem("PROMOTIONNO")) : 0);

                    CustomStatus cs = (CustomStatus)objSC.bulkStudentSemPromoWithoutCriteria(objS, sessionno, ipAddress, yearID);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.upddetails1, "Semester Promoted Successfully", this.Page);
                        btnShow_Click(sender, e);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.upddetails1, "Error", this.Page);
                    }

                    ////}
                    ////else
                    ////{
                    ////    objCommon.DisplayMessage(lblstudname.Text + "  is Not Eligible for Semester promotion", this.Page);
                    ////    return;
                    ////}

                }
            }

            if (flag == false)
            {
                objCommon.DisplayMessage(this.upddetails1, "Please select atleast single student", this.Page);
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            btnCancelSemPromotion.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");

            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            btnCancelSemPromotion.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            //if (ddlBranch.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            //    ddlScheme.Focus();
            //}
            //else
            //{
            //    ddlScheme.Items.Clear();
            //    ddlBranch.SelectedIndex = 0;
            //}

            //Added Mahesh on Dated 04/02/2020
            if (ddlBranch.SelectedIndex > 0 && ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus();
            }
            else if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnSave.Visible = false;
            btnCancelSemPromotion.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            ddlBranch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK), ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue, "DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int semester = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH WITH (NOLOCK)", "DURATION", "DEGREENO='" + Convert.ToInt32(ViewState["degreeno"]) + "' AND BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ""));//AAYUSHI  ADDED >= '2018-08-02 00:00:00.000' 
            int semcheck = semester * 2;
            if (rblSemPromotion.SelectedValue != "1")
            {
                if (Convert.ToInt32(ddlSemester.SelectedValue) >= semcheck)
                {
                    //objCommon.DisplayMessage(this.upddetails, "You Can't select semester greater than or equal to student's final semester!!!", this.Page);
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:functionConfirm(); ", true);
                    ddlSemester.SelectedIndex = 0;
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                }
                else
                {
                    StudentController objSC = new StudentController();
                    //ADDED ON 30-01-2020 BY VAISHALI TO GET THE RECORDS FOR SEM PROMOTION & CANCEL SEM PROMOTION
                    DataSet dsshow = null;
                    //if (rblSemPromotion.SelectedValue == "0")
                    //    dsshow = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON(A.IDNO=B.IDNO) LEFT OUTER JOIN ACD_SEM_PROMOTION C WITH (NOLOCK) ON(A.IDNO = C.IDNO AND B.SEMESTERNO=C.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "and A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "and A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "and A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "and A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ADMCAN=0 AND CAN=0", "a.idno");//AND A.IDNO NOT IN (SELECT IDNO FROM ACD_SEM_PROMOTION D WHERE D.IDNO = A.IDNO AND D.SEMESTERNO <> D.PROMOTED_SEM)"
                    //else if (rblSemPromotion.SelectedValue == "1")
                    //    dsshow = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON(A.IDNO=B.IDNO) INNER JOIN ACD_SEM_PROMOTION C WITH (NOLOCK) ON(A.IDNO = C.IDNO AND B.SEMESTERNO=C.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "and A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "and A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "and A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "and b.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ADMCAN=0 AND CAN=0 AND C.SEMESTERNO <> C.PROMOTED_SEM", "a.idno");
                    dsshow = objSC.GetBulkSemesterPromotionDataWithoutCriteria(Convert.ToInt32(rblSemPromotion.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["college_id"]));
                    if (dsshow != null)
                    {

                        if (dsshow.Tables[0].Rows.Count > 0)
                        {
                            lvStudent.Visible = true;
                            lvStudent.DataSource = dsshow;
                            lvStudent.DataBind();
                            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
                            hftot.Value = lvStudent.Items.Count.ToString();

                            //ADDED ON 30-01-2020 BY VAISHALI TO GET THE RECORDS FOR SEM PROMOTION & CANCEL SEM PROMOTION
                            if (rblSemPromotion.SelectedValue == "0")
                            {
                                btnSave.Visible = true;
                                btnCancelSemPromotion.Visible = false;
                                btnReportSemPromotion.Visible = true;
                                btnCancelSemPromoReport.Visible = false;
                                // btnReportsemnotpromotion.Visible = true;
                            }
                            else if (rblSemPromotion.SelectedValue == "1")
                            {
                                btnCancelSemPromotion.Visible = true;
                                btnSave.Visible = false;
                                btnReportSemPromotion.Visible = false;
                                btnCancelSemPromoReport.Visible = true;
                                // btnReportsemnotpromotion.Visible = false;
                            }
                        }
                        else
                        {
                            lvStudent.DataSource = null;
                            lvStudent.DataBind();
                            objCommon.DisplayMessage(this.upddetails1, "No Record Found for this selection!!!", this.Page);
                            //Clear();
                            //Response.Redirect(Request.Url.ToString());
                        }
                    }
                }
            }
            else
            {
                DataSet dsshow = null;
                if (rblSemPromotion.SelectedValue == "0")
                    dsshow = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON(A.IDNO=B.IDNO) LEFT OUTER JOIN ACD_SEM_PROMOTION C WITH (NOLOCK) ON(A.IDNO = C.IDNO AND B.SEMESTERNO=C.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "and A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "and A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "and A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "and A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ADMCAN=0 AND CAN=0", "a.idno");//AND A.IDNO NOT IN (SELECT IDNO FROM ACD_SEM_PROMOTION D WHERE D.IDNO = A.IDNO AND D.SEMESTERNO <> D.PROMOTED_SEM)"
                else if (rblSemPromotion.SelectedValue == "1")
                    dsshow = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_SEM_PROMOTION C WITH (NOLOCK) ON(A.IDNO = C.IDNO and A.SEMESTERNO=C.PROMOTED_SEM)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,c.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "c.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "and A.DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + "and A.BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + "and A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "and A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0 AND C.SEMESTERNO <> C.PROMOTED_SEM", "a.idno");
                //dsshow = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON(A.IDNO=B.IDNO) INNER JOIN ACD_SEM_PROMOTION C WITH (NOLOCK) ON(A.IDNO = C.IDNO AND B.SEMESTERNO=C.SEMESTERNO)", "DISTINCT A.IDNO,A.REGNO,A.STUDNAME,a.sectionno,b.SEMESTERNO , isnull(C.PROMOTED_SEM,0) as PROMOTED_SEM", "", "b.sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and  A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "and A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "and A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "and A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "and b.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ADMCAN=0 AND CAN=0 AND C.SEMESTERNO <> C.PROMOTED_SEM", "a.idno"); //Comment by Mahesh on dated 19-04-2021 due to course not offered then not showing student record due select semester on result table.

                if (dsshow.Tables[0].Rows.Count > 0)
                {
                    lvStudent.Visible = true;
                    lvStudent.DataSource = dsshow;
                    lvStudent.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
                    hftot.Value = lvStudent.Items.Count.ToString();

                    //ADDED ON 30-01-2020 BY VAISHALI TO GET THE RECORDS FOR SEM PROMOTION & CANCEL SEM PROMOTION
                    if (rblSemPromotion.SelectedValue == "0")
                    {
                        btnSave.Visible = true;
                        btnCancelSemPromotion.Visible = false;
                        btnReportSemPromotion.Visible = true;
                        btnCancelSemPromoReport.Visible = false;
                    }
                    else if (rblSemPromotion.SelectedValue == "1")
                    {
                        btnCancelSemPromotion.Visible = true;
                        btnSave.Visible = false;
                        btnReportSemPromotion.Visible = false;
                        btnCancelSemPromoReport.Visible = true;
                    }
                }
                else
                {
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    objCommon.DisplayMessage(this.upddetails1, "No Record Found for this selection!!!", this.Page);
                    //Clear();
                    //Response.Redirect(Request.Url.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnCancelSemPromotion.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT (SESSIONNO)", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
    }

    public void Clear()
    {
        ddlSession.SelectedIndex = 0;
        ddlColg.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
    }
    //ADDED ON 30-01-2020 BY VAISHALI TO GET THE RECORDS FOR SEM PROMOTION & CANCEL SEM PROMOTION
    protected void btnCancelSemPromotion_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student objS = new Student();
            bool flag = false;
            int count = 0;
            foreach (ListViewDataItem item in lvStudent.Items)
            {

                Label lblregno = item.FindControl("lblregno") as Label;
                Label lblstudname = item.FindControl("lblstudname") as Label;
                ////***Label lblstatus = item.FindControl("lblstatus") as Label;
                CheckBox chksub = item.FindControl("chkRegister") as CheckBox;
                if (chksub.Checked == true && chksub.Enabled == true)
                {
                    flag = true;//************
                    ////****if (lblstatus.Text == "YES")
                    ////****{
                    int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objS.IdNo = Convert.ToInt32(lblregno.ToolTip);
                    objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objS.SectionNo = Convert.ToInt32(lblstudname.ToolTip);
                    objS.SemesterNo = Convert.ToInt32(ddlSemester.SelectedValue);
                    objS.Uano = Convert.ToInt32(Session["userno"]);
                    objS.CollegeCode = Session["colcode"].ToString();
                    objS.Dob = DateTime.Now;
                    if (!lblregno.Text.Trim().Equals(string.Empty)) objS.RollNo = lblregno.Text.Trim();
                    string ipAddress = Request.ServerVariables["REMOTE_HOST"];

                    CustomStatus cs = (CustomStatus)objSC.bulkStudentCancelSemPromoWithoutCriteria(objS, sessionno, ipAddress);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.upddetails1, "Semester Promotion Cancelled Successfully", this.Page);
                        btnShow_Click(sender, e);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.upddetails1, "Error", this.Page);
                    }
                }
            }
            if (flag == false)
            {
                objCommon.DisplayMessage(this.upddetails1, "Please select atleast single student", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    //ADDED ON 30-01-2020 BY VAISHALI TO GET THE RECORDS FOR SEM PROMOTION & CANCEL SEM PROMOTION
    protected void rblSemPromotion_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSave.Visible = false;
        btnCancelSemPromotion.Visible = false;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        btnReportSemPromotion.Visible = false;
        btnCancelSemPromoReport.Visible = false;
        if (rblSemPromotion.SelectedValue == "0")
        {
            btnReportsemnotpromotion.Visible = true;
        }
        else if (rblSemPromotion.SelectedValue == "1")
        {
            btnReportsemnotpromotion.Visible = false;
        }
    }

    private void ShowReportSemPromotion(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.upddetails1, this.upddetails1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowReportCancelSemPromotion(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ViewState["college_id"] + ",@P_DEGREENO=" + ViewState["degreeno"] + ",@P_BRANCHNO=" + ViewState["branchno"] + ",@P_SCHEMENO=" + ViewState["schemeno"] + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.upddetails1, this.upddetails1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnReportSemPromotion_Click(object sender, EventArgs e)
    {
        ShowReportSemPromotion("SemPromotedList", "rptSemPromotedWithoutCriteria.rpt");
    }

    protected void btnCancelSemPromoReport_Click(object sender, EventArgs e)
    {
        ShowReportSemPromotion("CancelledSemPromotedList", "rptCancelledSemPromotedWithoutCriteria.rpt");
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
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
                ddlSemester.Focus();
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT (SESSIONNO)", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER SM INNER JOIN  ACD_STUDENT_RESULT SR ON SR.SESSIONNO= SM.SESSIONNO ", "DISTINCT (SR.SESSIONNO)", "SM.SESSION_NAME", "SR.SESSIONNO > 0 AND SM.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + " AND SM.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SR.SESSIONNO");
                ddlSession.Focus();
            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void btnReportsemnotpromotion_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            //ADDED ON 03-10-2022 BY jay t. TO GET THE RECORDS FOR SEM PROMOTION not completed
            DataSet dsshow = null;
            string reportTitle = string.Empty;
            string rptFileName = string.Empty;
            GridView GVDayWiseAtt = new GridView();
            dsshow = objSC.GetBulkSemesterPromotionDataWithoutCriteriaExcel(Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["college_id"]));
            if (dsshow != null)
            {
                if (dsshow != null && dsshow.Tables.Count > 0 && dsshow.Tables[0].Rows.Count > 0)
                {
                    GVDayWiseAtt.DataSource = dsshow;
                    GVDayWiseAtt.DataBind();

                    string attachment = "attachment; filename=" + "Semester_Promotion_Not_Completed_Excel.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    //Response.ContentType = "application/pdf";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVDayWiseAtt.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("No information found based on given criteria.", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_BulkSemesterPromotionWithoutCriteria.btnReportsemnotpromotion_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }
}

