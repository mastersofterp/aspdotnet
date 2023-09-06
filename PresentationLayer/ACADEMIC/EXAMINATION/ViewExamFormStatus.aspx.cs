//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     :ACADEMIC_ModificationExamRegStatus                                  
// CREATION DATE : 15-OCT-2011
// CREATED BY    : renuka a                                                
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================


using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ViewExamFormStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objmec = new MarksEntryController(); string idnos = string.Empty;
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
                //Check for Activity On/Off for course registration.
                if (CheckActivity())
                {
                    btnSubmit.Enabled = true;
                    divInfo.Visible = true;
                    divFooter.Visible = true;
                    if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8") //Added By Nikhil V.Lambe on 16/03/2021 for page should be access by Admin and HOD.
                        this.PopulateDropDownList();
                    else
                        Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
                }
                else
                {
                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage("Activity has been stopped. You can only view exam form status.", this.Page);
                    if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "8") //Added By Nikhil V.Lambe on 16/03/2021 for page should be access by Admin and HOD.
                        this.PopulateDropDownList();
                    else
                        Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
                    //activitystatus = 1;
                    divInfo.Visible = true;
                    divFooter.Visible = true;
                }
               
            }
        }
    }

    private bool CheckActivity()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;

        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
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
            //objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                divDept.Visible = true;
                objCommon.FillDropDownList(ddlDepartment, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON(U.UA_DEPTNO=D.DEPTNO)", "DISTINCT D.DEPTNO", "D.DEPTNAME", "UA_TYPE=8 AND D.DEPTNO =" + Convert.ToInt32(Session["userdeptno"]), "DEPTNO");
            }
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");
            ddlSession.SelectedIndex = 0;
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

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlDegree.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
            if (ddlDegree.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlColg.SelectedValue + "AND B.DEPTNO=" + Convert.ToInt32(Session["userdeptno"]), "A.LONGNAME");
 
                }
                else
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "A.LONGNAME");

                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
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

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
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

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSemester.SelectedIndex = -1;
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0

            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = -1;
            ddlBranch.SelectedIndex = -1;
            ddlScheme.SelectedIndex = -1;
            ddlSemester.SelectedIndex = -1;
            if (ddlColg.SelectedIndex > 0)
            {
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlDepartment, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON(U.UA_DEPTNO=D.DEPTNO)", "DISTINCT D.DEPTNO", "D.DEPTNAME", "UA_TYPE=8 AND D.DEPTNO =" + Convert.ToInt32(Session["userdeptno"]), "DEPTNO");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue + "AND B.DEPTNO=" + Convert.ToInt32(Session["userdeptno"]), "D.DEGREENO");
                }
                else
                {

                    objCommon.FillDropDownList(ddlDepartment, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON(U.UA_DEPTNO=D.DEPTNO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEPTNO=B.DEPTNO)", "DISTINCT D.DEPTNO", "D.DEPTNAME", " B.COLLEGE_ID =" + Convert.ToInt32(ddlColg.SelectedValue), "DEPTNO");
                    //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE CD WITH (NOLOCK) ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlColg.SelectedValue + "", "D.DEGREENO");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");
                }

               
                ddlDegree.Focus();
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_STUDENT B WITH (NOLOCK) ON (A.IDNO=B.IDNO) LEFT OUTER JOIN ACD_ADMITCARD_LOG ADL ON (A.IDNO=ADL.IDNO AND A.SEMESTERNO=ADL.SEMESTERNO AND GENERATED_BY='S')", "DISTINCT B.IDNO,ISNULL(A.EXAM_REGISTERED,0) AS EXAM_REGISTERED,ISNULL(STUD_EXAM_REGISTERED,0)STUD_EXAM_REGISTERED,GENERATED_BY", "B.REGNO,B.STUDNAME", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND B.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND B.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND b.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " AND B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0", "B.IDNO");
        DataSet ds =objmec.GetViewExamFormStatus(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlStudType.SelectedValue));
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudents.DataSource = ds;
            lvStudents.DataBind();
            //btnSubmit.Enabled = true;
            //btnSlip.Enabled=true;
          //  btnSlip.Enabled = true;
            //btnSubmit.Enabled = true;
            btnExcel.Enabled = true;
        }
        else
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            objCommon.DisplayMessage(this.updExam, "No Student Found.", this.Page);
             //btnSubmit.Enabled = false;
             //btnSlip.Enabled=false;
           // btnExcel.Enabled = false;
            btnSubmit.Enabled = false;
            return;
        }
        
     }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string idnos = string.Empty;
        int ua_no = Convert.ToInt32(Session["userno"].ToString());
        ViewState["ip_Address"] = Request.ServerVariables["REMOTE_ADDR"].ToString();
        foreach (ListViewDataItem itm in lvStudents.Items)
        {
            HiddenField hdn = itm.FindControl("hidStudentId") as HiddenField;
            // CheckBox chk = itm.FindControl("cbHead") as CheckBox;
            if ((itm.FindControl("chkRow") as CheckBox).Checked == true && (itm.FindControl("chkRow") as CheckBox).Enabled == true)
            {

                idnos += hdn.Value + ',';
            }
        }
        if (idnos == string.Empty)
        {
            objCommon.DisplayMessage(this.updExam, "Please Select At Least One Students!", this.Page);

        }
        int cs = objmec.UpdateExamInchargeStatus(idnos, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), ua_no, ViewState["ip_Address"].ToString(), Convert.ToInt32(ddlStudType.SelectedValue));
        if (cs == 2)
        {
            objCommon.DisplayMessage(this.updExam, "Student Exam Form Approved Succesfullly!", this.Page);
            btnShow_Click(sender, e);
            return;

        }
    }

    private void ExcelReport(DataSet ds, string Title)
    {
        GridView GV = new GridView();
        string ContentType = string.Empty;

        GV.DataSource = ds;
        GV.DataBind();
        string attachment = "attachment; filename=" + Title + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.MS-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GV.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        // int sessionno = Convert.ToInt32(Session["currentsession"].ToString());
        // int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        // int idno = Convert.ToInt32(hidStudentId.);

        // HiddenField hdf = new HiddenField();

        //  hdf = Convert.ToString( hidStudentId); rptBulkExamRegistrationSlip
        // ShowReport("BulkExamRegistrationSlip", "rptBulkExamRegistrationSlip.rpt");

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue);

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updExam, this.updExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }








    protected void btnSlip_Click(object sender, EventArgs e)
    {

        ShowReport("BulkExamRegistrationSlip", "rptBulkExamRegistrationSlip.rpt");

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStudType.SelectedIndex = -1;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue + "AND B.DEPTNO=" + Convert.ToInt32(Session["userdeptno"]), "D.DEGREENO");
        }
        else
        {
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE CD WITH (NOLOCK) ON (D.DEGREENO = CD.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlColg.SelectedValue + "", "D.DEGREENO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue + "AND B.DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "D.DEGREENO");
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = objmec.GetViewExamFormStatusForExcel(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlStudType.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ExcelReport(ds, "View_Exam_Form_Status");

        }
    }
}
