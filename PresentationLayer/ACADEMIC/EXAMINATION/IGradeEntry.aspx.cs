using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using mastersofterp_MAKAUAT;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.IO;
public partial class ACADEMIC_EXAMINATION_IGradeEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GradeController objG = new GradeController();
    FeeCollectionController feeController = new FeeCollectionController();
    //ConnectionStrings
    string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                CheckPageAuthorization();
                //if (Session["usertype"].ToString() == "1")
                //{
                //    //objCommon.FillDropDownList(ddlAcdYear, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
                //}
                //else
                //{
                //    Response.Redirect("~/notauthorized.aspx?page=AffiliatedFeeDetails.aspx");
                //}
            }
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO >0 AND IS_ACTIVE=1", "SESSIONNO DESC");
            //**objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO)", "DISTINCT R.SESSIONNO", "SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");

            // WORKING CODE FOR USERDEPTNO= 0

            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"].ToString()) + " AND (DB.DEPTNO in  (" + (Session["userdeptno"].ToString()) + ") OR ISNULL(" + (Session["userdeptno"].ToString()) + ",0)=0)", "");
            // 

            // ADDED BY NARESH BEERLA ON DT 12032022
            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();

            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");


            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + (deptno) + ") OR ISNULL(" + (deptno) + ",0)=0)", "");

            // ENDS HERE BY NARESH BEERLA ON DT 12032022

            //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO in  (" + Convert.ToInt32(Session["userdeptno"]) + ") OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");


        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=IGradeEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=IGradeEntry.aspx");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {



        if (ddlSession.SelectedIndex > 0)
        {
            ddlSemester.Items.Clear();
            //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT WITH (NOLOCK)", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO) AS SEMESTER", "SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "SEMESTERNO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            ddlSemester.Focus();
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            //ddlStudent.Items.Clear();
            //ddlStudent.Items.Add(new ListItem("Please Select", "0"));

        }
        ddlSemester.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;



        //if (ddlSession.SelectedIndex > 0)
        //{
        //    //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            
        //    ddlCollege.Focus();
        //    ddlCollege.Items.Clear();
        //    ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        //    ddlDegree.Items.Clear();
        //    ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        //    ddlBranch.Items.Clear();
        //    ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        //    ddlScheme.Items.Clear();
        //    ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        //    ddlSubject.Items.Clear();
        //    ddlSubject.Items.Add(new ListItem("Please Select", "0"));
        //    objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        //}
        //else
        //{
        //    ddlCollege.SelectedIndex = 0;
        //    ddlDegree.SelectedIndex = 0;
        //    ddlBranch.SelectedIndex = 0;
        //    ddlScheme.SelectedIndex = 0;
        //    ddlSemester.SelectedIndex = 0;
        //    ddlSubject.SelectedIndex = 0;
        //    ddlSession.SelectedIndex = 0;
        //}
        ClearListView();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex > 0)
        {
            ddlDegree.Focus();
            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK), ACD_COLLEGE_DEGREE C WITH (NOLOCK), ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK)", "DISTINCT (D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND (C.COLLEGE_ID=" + ddlCollege.SelectedValue + " OR " + ddlCollege.SelectedValue + "= 0)", "DEGREENO");

        }
        else
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlCollege.SelectedIndex = 0;
        }
        ClearListView();
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            ddlBranch.Focus();
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("Please Select", "0"));
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

        }
        else
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
        }
        ClearListView();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");

        }
        else
        {
            ddlScheme.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
        }
        ClearListView();
    }
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSemester.Focus();
            ddlSemester.Items.Clear();
            ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");

        }
        else
        {
            ddlSemester.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
        }
        ClearListView();
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            ddlSubject.Focus();
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "Case When ISNULL(GLOBALELE,0)=1 then (SR.CCODE + ' - ' + SR.COURSENAME +' [Global]') Else (SR.CCODE + ' - ' + SR.COURSENAME) End as COURSENAME", "SR.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "");

        }
        else
        {
            ddlSubject.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
        }
        ClearListView();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearListView();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objG.GetStudentsList_For_I_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlStudents.Visible = true;
                lvStudents.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                foreach (ListViewDataItem item in lvStudents.Items)
                {
                    CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                    TextBox Grade = item.FindControl("txtGrade") as TextBox;
                    Label lbl = item.FindControl("lblStatus") as Label;
                    if (!Grade.Text.Equals(string.Empty) && Grade.Text.Equals("I")) // ADDED BY NARESH BEERLA ON DT 04042022
                    {
                        chek.Checked = true;
                        Grade.Enabled = true;
                        lbl.Text = "Done".ToString();
                        lbl.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl.Text = "Not Done".ToString();
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }
                btnSubmit.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage(updGrade, "Data Not Found", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ClearListView()
    {
        pnlStudents.Visible = false;
        lvStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int checkCount = 0;
            int updCount = 0;
            int Idno = 0;
            foreach (ListViewDataItem lv in lvStudents.Items)
            {
                CheckBox chek = lv.FindControl("chkSelect") as CheckBox;
                TextBox txt = lv.FindControl("txtGrade") as TextBox;
                if (chek.Checked)
                {
                    if (txt.Text.Equals(string.Empty))
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Grade For Selected Students", this.Page);
                        return;
                    }
                    Idno = Convert.ToInt32(chek.ToolTip);
                    checkCount++;
                    CustomStatus cs = (CustomStatus)objG.UpdateStudents_For_I_Grade(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue), Idno);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;
                    }
                }
            }
            if (checkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List", this.Page);
                return;
            }
            else if (checkCount == updCount)
            {
                objCommon.DisplayMessage(this.Page, "Students Grade Are Updated Successfully", this.Page);
                ClearListView();
                return;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ClearField()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        pnlStudents.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox check = item.FindControl("chkSelect") as CheckBox;
                TextBox TextGrade = item.FindControl("txtGrade") as TextBox;
                if (check.Checked)
                {
                    TextGrade.Enabled = true;
                    TextGrade.Text = "I";
                }
                else
                {
                    TextGrade.Enabled = false;
                    TextGrade.Text = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.chkSelect_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void rdoMultiple_CheckedChanged(object sender, EventArgs e)
    {
        divMultiple.Visible = true;
        divSingle.Visible = false;
        divstudDetails.Visible = false;
        ClearListView();
        divSubjectsList.Visible = false;
        ddlClgname.Focus();

    }
    protected void ddlSessionSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSessionSingle.SelectedIndex > 0)
        {
            ddlSemesterSingle.Focus();
            ddlSemesterSingle.Items.Clear();
            ddlSemesterSingle.Items.Add(new ListItem("Please Select", "0"));
            txtRegNo.Text = string.Empty;
            objCommon.FillDropDownList(ddlSemesterSingle, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSessionSingle.SelectedValue + " ", "SR.SEMESTERNO");
        }
        divSubjectsList.Visible = false;
    }
    protected void ddlSemesterSingle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemesterSingle.SelectedIndex > 0)
        {
            txtRegNo.Text = string.Empty;
            txtRegNo.Focus();
        }
        divSubjectsList.Visible = false;
    }
    protected void btnShowSingle_Click(object sender, EventArgs e)
    {
        try
        {
            ClearListView();
            string Idno ;
            //int idnos;
            string Reg = "'"+txtRegNo.Text.Trim()+"'";
            Idno =(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO= " + Reg));

            if (Idno == "" || Idno == string.Empty)
            {
                txtRegNo.Text = string.Empty;
                divstudDetails.Visible = false;
                divSubjectsList.Visible = false;
                objCommon.DisplayMessage(this.Page,"No Data Found...", this.Page);
             
                return;
            }

            DataSet dsSubject = objG.GetSubjectsList_For_I_Grade_ByIdno(Convert.ToInt32(ddlSessionSingle.SelectedValue), Convert.ToInt32(ddlSemesterSingle.SelectedValue), Convert.ToInt32(Idno));
            if (dsSubject.Tables[0].Rows.Count > 0)
            {
                ShowDetails();
                divstudDetails.Visible = true;
                divSubjectsList.Visible = true;
                pnlSubjects.Visible = true;                
                lvSubjects.DataSource = dsSubject;
                lvSubjects.DataBind();
                foreach (ListViewDataItem item in lvSubjects.Items)
                {
                    CheckBox chekSub = item.FindControl("chkSubject") as CheckBox;
                    TextBox Grade = item.FindControl("txtGrade_Sub") as TextBox;
                    Label lblStatus = item.FindControl("lblStatusSub") as Label;
                    if (!Grade.Text.Equals(string.Empty) && Grade.Text.Equals("I")) // ADDED BY NARESH BEERLA ON DT 04042022
                    {
                        chekSub.Checked = true;
                        Grade.Enabled = true;
                        lblStatus.Text = "Done".ToString();
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblStatus.Text = "Not Done".ToString();
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                btnSubmitSingle.Enabled = true;
            }
            else
            {
                txtRegNo.Text = string.Empty;
                divstudDetails.Visible = false;
                divSubjectsList.Visible = false;
                objCommon.DisplayMessage(this.Page,"Data Not Found",this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.btnShowSingle_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnCancelSingle_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void rdoSingle_CheckedChanged1(object sender, EventArgs e)
    {
        divMultiple.Visible = false;
        divSingle.Visible = true;
        ClearListView();
        //objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO >0 AND IS_ACTIVE=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSessionSingle, "ACD_SESSION_MASTER M INNER JOIN ACD_STUDENT_RESULT R ON(M.SESSIONNO=R.SESSIONNO) INNER JOIN ACD_COLLEGE_MASTER C ON (ISNULL(M.COLLEGE_ID,0)=ISNULL(C.COLLEGE_ID,0))", "DISTINCT R.SESSIONNO", "SESSION_NAME +' - '+ C.COLLEGE_NAME SESSION_NAME", "M.SESSIONNO >0 AND IS_ACTIVE=1", "SESSION_NAME DESC");
        ddlSessionSingle.Focus();
        ddlSemesterSingle.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;

    }
    protected void chkSubject_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem item in lvSubjects.Items)
            {
                CheckBox check = item.FindControl("chkSubject") as CheckBox;
                TextBox TextGrade = item.FindControl("txtGrade_Sub") as TextBox;
                if (check.Checked)
                {
                    TextGrade.Enabled = true;
                    TextGrade.Text = "I";
                }
                else
                {
                    TextGrade.Enabled = false;
                    TextGrade.Text = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.chkSubject_CheckedChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void btnSubmitSingle_Click(object sender, EventArgs e)
    {
        try
        {
            int checkCount = 0;
            int updCount = 0;
            int Sub = 0;
            string RegNo = "'"+txtRegNo.Text.Trim()+"'";
            int Id =Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO= "+ RegNo));
            foreach (ListViewDataItem lv in lvSubjects.Items)
            {
                CheckBox checkSub = lv.FindControl("chkSubject") as CheckBox;
                TextBox GradeText = lv.FindControl("txtGrade_Sub") as TextBox;
                if (checkSub.Checked)
                {
                    if (GradeText.Text.Equals(string.Empty))
                    {
                        objCommon.DisplayMessage(this.Page, "Please Enter Grade For Selected Subjects", this.Page);
                        return;
                    }
                    Sub =Convert.ToInt32(checkSub.ToolTip);
                    checkCount++;
                    CustomStatus cs = (CustomStatus)objG.UpdateGradeBy_Id(Convert.ToInt32(ddlSessionSingle.SelectedValue), Convert.ToInt32(ddlSemesterSingle.SelectedValue), Sub, Id);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        updCount++;
                    }
                }
            }
            if (checkCount == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student From List", this.Page);
                return;
            }
            else if (checkCount == updCount)
            {
                objCommon.DisplayMessage(this.Page, "Students Grade Are Updated Successfully", this.Page);                              
                divSubjectsList.Visible = false;
                txtRegNo.Text = string.Empty;
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.btnSubmitSingle_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ClearSingleField()
    {
        ddlSessionSingle.SelectedIndex = 0;
        ddlSemesterSingle.SelectedIndex = 0;
        txtRegNo.Text = string.Empty;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {

            //ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            GridView GvStudent = new GridView();
            DataSet ds = objG.Get_Students_List_I_Grade_Excel_Rpt(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlSubject.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GvStudent.DataSource = ds;
                GvStudent.DataBind();
                string attachment = "attachment; filename=IGradeStudentsList.xlsx";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/xlsx";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GvStudent.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
                
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Data Not Found", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "IGradeEntry.btnExcel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        }
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0; 
        ddlSubject.SelectedIndex = 0;


    }

    // added by prafull on dt 09092022 for student details

    private void ShowDetails()
    {
        //lvFailCourse.DataSource = null;
        //lvFailCourse.DataBind();
        //lvFailInaggre.DataSource = null;
        //lvFailInaggre.DataBind();
        //btnSubmit.Visible = false;
        //btnPrintRegSlip.Visible = false;
        //btnReport.Visible = false;
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        StudentController objSC = new StudentController();
        //if (ViewState["usertype"].ToString() == "2")
        //{
        //    idno = Convert.ToInt32(Session["idno"]);
        //}
        //if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "7")
        //{
            idno = feeController.GetStudentIdByEnrollmentNo(txtRegNo.Text);


        //}

        try
        {
            if (idno > 0)
            {
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //if (ViewState["semesternos"].ToString().Contains(dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString()))
                        //{

                            lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                            lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                           // lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                            //lblMotherName.Text = " (<b>Mothers Name : </b>" + dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString() + ")";

                            lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                            lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                            lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                            lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                            lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                            lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                            lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                            lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                            lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                      
                        
                    }
                    else
                    {
                        objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                        divstudDetails.Visible = false;
                        //flag = false;
                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                    divstudDetails.Visible = false;
                  //  flag = false;
                }
            }
            else
            {
                objCommon.DisplayMessage("No Records Found for this Student.!!", this.Page);
                divstudDetails.Visible = false;
               // flag = false;
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


}