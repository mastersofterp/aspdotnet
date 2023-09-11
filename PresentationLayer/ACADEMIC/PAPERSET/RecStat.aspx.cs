using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS;
using IITMS.UAIMS;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class ACADEMIC_PAPERSET_RecStat : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    // Set form action as add
                    ViewState["action"] = "add";

                    // Fill Dropdown lists
                    FillDropdown();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CreateOperator.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillDropdown() // ADDED BY SHUBHAM ON 20022023
    {
        try
        {
            string deptno = string.Empty;
            if (Session["userdeptno"].ToString() == null || Session["userdeptno"].ToString() == string.Empty)
                deptno = "0";
            else
                deptno = Session["userdeptno"].ToString();
            if (Session["usertype"].ToString() != "1")
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (CASE '" + deptno + "' WHEN '0' THEN 0 ELSE CAST(DB.DEPTNO AS VARCHAR) END) IN (" + deptno + ")", "");
            //AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
            else
            {
                objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
            }

            lvCourse4.DataSource = null;
            lvCourse4.DataBind();
            pnlList4.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region TAB4 RECIEVING STATUS
    #region "General"
    private void BindListView4()
    {
        //string dept = ddlDepartment.SelectedValue;
        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        string sem = ddlSemester.SelectedValue;
        string ccode = ddlCourse.SelectedValue == "0" ? "" : ddlCourse.SelectedValue;

        //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )A ON (A.STAFFNO = P.FACULTY1 AND APPROVED1 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )B ON (B.STAFFNO = P.FACULTY2 AND APPROVED2 = 1)LEFT OUTER JOIN (SELECT STAFFNO,UA_NO,STAFF_NAME,STAFF_ADDRESS,CONTACTNO,EMAIL_ID,DEPTNO,INTERNAL_EXTERNAL FROM ACD_STAFF  S )D ON (D.STAFFNO = P.FACULTY3 AND APPROVED3 = 1)", "DISTINCT ISNULL(A.STAFF_NAME,'-') AS NAME1", "(CASE WHEN A.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN A.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC1,ISNULL(B.STAFF_NAME,'-') AS NAME2,(CASE WHEN B.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN B.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC2,ISNULL(D.STAFF_NAME,'-') AS NAME3,(CASE WHEN D.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN D.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC3", "SESSIONNO = " + ddlSession3.SelectedValue + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED1 IS NOT NULL AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", string.Empty);
        //DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)INNER  JOIN ACD_STAFF  S ON (S.STAFFNO = P.APPROVED  AND( S.CANCEL=0 OR S.CANCEL IS NULL )) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", " DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,APPROVED,ISNULL(RECEIVED,0)RECEIVED,ISNULL(P.CANCEL,0)CANCEL,(CASE WHEN FACULTY1 = APPROVED THEN QT1 WHEN FACULTY2 = APPROVED THEN QT2 WHEN FACULTY3 = APPROVED THEN QT3 END)QTY,(CASE WHEN FACULTY1 = APPROVED THEN MOI1 WHEN FACULTY2 = APPROVED THEN MOI2 WHEN FACULTY3 = APPROVED THEN MOI3 END)MOI,QT_RCVD,MOI_RCVD,ISNULL(S.STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND (P.BOS_DEPTNO = " + dept + " OR " + dept + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND APPROVED > 0 AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
        DataSet ds = objCommon.FillDropDown("ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO)INNER  JOIN ACD_STAFF  S ON (S.STAFFNO = P.APPROVED  AND( S.CANCEL=0 OR S.CANCEL IS NULL )) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", " DISTINCT  P.CCODE,COURSE_NAME,P.SEMESTERNO,DBO.FN_DESC('SEMESTER',P.SEMESTERNO)SEMESTER,APPROVED,ISNULL(RECEIVED,0)RECEIVED,ISNULL(P.CANCEL,0)CANCEL,(CASE WHEN FACULTY1 = APPROVED THEN QT1 WHEN FACULTY2 = APPROVED THEN QT2 WHEN FACULTY3 = APPROVED THEN QT3 END)QTY,(CASE WHEN FACULTY1 = APPROVED THEN MOI1 WHEN FACULTY2 = APPROVED THEN MOI2 WHEN FACULTY3 = APPROVED THEN MOI3 END)MOI,QT_RCVD,MOI_RCVD,ISNULL(S.STAFF_NAME,'-') AS NAME", "(CASE WHEN S.INTERNAL_EXTERNAL = 'I' THEN 'INTERNAL' WHEN S.INTERNAL_EXTERNAL = 'E'  THEN 'EXTERNAL' END) FAC", "SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND (C.SCHEMENO = " + schemeno + " OR " + schemeno + " = 0 ) AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND APPROVED > 0 AND DEAN_LOCK = 1 AND (P.CCODE = '" + ccode + "' OR '" + ccode + "' ='') AND (P.SEMESTERNO =" + sem + " or " + sem + "= 0 )", " P.SEMESTERNO,P.CCODE");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lvCourse4.DataSource = ds;
            lvCourse4.DataBind();
            pnlList4.Visible = true;
            btnSubmit5.Visible = true;

        }
        else
        {
            lvCourse4.DataSource = null;
            lvCourse4.DataBind();
            pnlList4.Visible = false;
            btnSubmit5.Enabled = false;
            objCommon.DisplayMessage(this.updRcvSatus, "Data not found please Complete the privous process!", this.Page);
        }

    }
    #endregion "General"

    #region "SelectedIndexChanged"
    //protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlScheme.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE AND C.BOS_DEPTNO = P.BOS_DEPTNO)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "(P.CANCEL = 0 OR P.CANCEL IS NULL) AND S.SEMESTERNO >0  AND C.SCHEMENO =" + ddlScheme.SelectedValue + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
    //        }
    //        else
    //            ddlSemester.SelectedIndex = 0;

    //        lvCourse4.DataSource = null;
    //        lvCourse4.DataBind();
    //        pnlList4.Visible = false;
    //        // btnSubmit5.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ddlScheme_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    // BindListView4();
    //}

    protected void ddlSemester4_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_STAFF  S ON (S.STAFFNO =  APPROVED) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT C.CCODE", "C.CCODE +  ' - '+ COURSE_NAME", "SM.SESSIONID = " + ddlSession.SelectedValue + " AND P.BOS_DEPTNO = " + ddlDepartment.SelectedValue + " AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1  AND P.SEMESTERNO = " + ddlSemester.SelectedValue, "CCODE");
                objCommon.FillDropDownList(ddlCourse, "ACD_PAPERSET_DETAILS P INNER JOIN ACD_COURSE C ON (P.CCODE = C.CCODE AND P.SEMESTERNO = C.SEMESTERNO) INNER JOIN ACD_STAFF  S ON (S.STAFFNO =  APPROVED) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT C.CCODE", "C.CCODE +  ' - '+ COURSE_NAME", "SM.SESSIONID = " + ddlSession.SelectedValue + " AND C.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND P.BOS_LOCK = 1 AND APPROVED IS NOT NULL AND DEAN_LOCK = 1  AND P.SEMESTERNO = " + ddlSemester.SelectedValue, "CCODE");
                BindListView4();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                lvCourse4.DataSource = null;
                lvCourse4.DataBind();
                pnlList4.Visible = false;
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ddlCourse4_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView4();
    }

    #endregion "SelectedIndexChanged"

    #region "Submit"

    protected void btnRcvdAll_Click(object sender, EventArgs e)
    {
        try
        {
            CourseController objCC = new CourseController();
            string ccode = ddlCourse.SelectedValue == "0" ? "" : ddlCourse.SelectedValue;
            int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
            int ret = 0;
            foreach (ListViewItem item in lvCourse4.Items)
            {
                TextBox txtRcvdQTY = item.FindControl("txtRcvdQTY") as TextBox;
                TextBox txtRcvdMOI = item.FindControl("txtRcvdMOI") as TextBox;
                Label lblCcode = item.FindControl("lblCcode") as Label;
                Label lblQTY = item.FindControl("lblQTY") as Label;
                Label lblMOI = item.FindControl("lblMOI") as Label;
                Label lblFaculty1 = item.FindControl("lblFaculty1") as Label;

                int rcvdQTY = txtRcvdQTY.Text == "" ? 0 : Convert.ToInt16(txtRcvdQTY.Text);
                int rcvdMoi = txtRcvdMOI.Text == "" ? 0 : Convert.ToInt16(txtRcvdMOI.Text);
                int rcvd = Convert.ToInt16(lblQTY.Text) <= Convert.ToInt16(txtRcvdQTY.Text) ? 1 : 0;
                int CollegeId = Convert.ToInt32(ViewState["college_id"]);
                //ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(lblFaculty1.ToolTip), CollegeId);
                ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession.SelectedValue), Deptno, Convert.ToInt16(ddlSemester.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(lblFaculty1.ToolTip), CollegeId);
                if (ret != Convert.ToInt32(CustomStatus.Error))
                    objCommon.DisplayMessage(this.updRcvSatus, "Status received for paper setter!", this.Page);
            }
            BindListView4();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRCVd_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        try
        {
            CourseController objCC = new CourseController();
            string ccode = ddlCourse.SelectedValue == "0" ? "" : ddlCourse.SelectedValue;
            int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
            foreach (ListViewItem item in lvCourse4.Items)
            {
                Button btnRCVd1 = item.FindControl("btnRCVd1") as Button;
                if (btnRCVd1.GetHashCode() == btn.GetHashCode())
                {
                    TextBox txtRcvdQTY = item.FindControl("txtRcvdQTY") as TextBox;
                    TextBox txtRcvdMOI = item.FindControl("txtRcvdMOI") as TextBox;
                    Label lblCcode = item.FindControl("lblCcode") as Label;
                    Label lblQTY = item.FindControl("lblQTY") as Label;
                    Label lblMOI = item.FindControl("lblMOI") as Label;
                    int rcvdQTY = txtRcvdQTY.Text == "" ? 0 : Convert.ToInt16(txtRcvdQTY.Text);
                    int rcvdMoi = txtRcvdMOI.Text == "" ? 0 : Convert.ToInt16(txtRcvdMOI.Text);
                    int rcvd = Convert.ToInt16(lblQTY.Text) <= Convert.ToInt16(txtRcvdQTY.Text) ? 1 : 0;
                    int CollegeId = Convert.ToInt32(ViewState["college_id"]);

                    //int ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(btn.CommandArgument), CollegeId);
                    int ret = objCC.AddPaperSetReceivedStatus(Convert.ToInt16(ddlSession.SelectedValue), Deptno, Convert.ToInt16(ddlSemester.SelectedValue), rcvdQTY, rcvdMoi, rcvd, lblCcode.Text, Convert.ToInt16(btn.CommandArgument), CollegeId);
                    if (ret != Convert.ToInt32(CustomStatus.Error))
                        objCommon.DisplayMessage(this.updRcvSatus, "Satus received for paper setter!", this.Page);
                    BindListView4();
                    return;
                }
            }
            BindListView4();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnCancelStatus_Click(object sender, EventArgs e)
    {
        int Deptno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT DEPTNO", "SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"])));
        Button btn = sender as Button;
        try
        {
            CourseController objCC = new CourseController();
            // string ccode = ddlCourse4.SelectedValue == "0" ? "" : ddlCourse4.SelectedValue;
            int collegeId = Convert.ToInt32(ViewState["college_id"]);
            //int ret = objCC.CancelPaperSetEntry(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlDepartment.SelectedValue), Convert.ToInt16(ddlSemester.SelectedValue), btn.CommandName, Convert.ToInt16(btn.CommandArgument), collegeId);
            int ret = objCC.CancelPaperSetEntry(Convert.ToInt16(ddlSession.SelectedValue), Deptno, Convert.ToInt16(ddlSemester.SelectedValue), btn.CommandName, Convert.ToInt16(btn.CommandArgument), collegeId);
            if (ret != Convert.ToInt32(CustomStatus.Error))
                objCommon.DisplayMessage(this.updRcvSatus, "Cancelled this entry!", this.Page);
            BindListView4();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_PaperSetterIssueLetter.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnClear5_Click(object sender, EventArgs e)
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        // ddlSemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        lvCourse4.DataSource = null;
        lvCourse4.DataBind();
        pnlList4.Visible = false;
    }

    #endregion "Submit"


    #endregion

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 and ISNULL(IS_ACTIVE,0)=1  and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM WITH (NOLOCK) ON (SM.SESSIONID = S.SESSIONID)", "S.SESSIONID", "S.SESSION_PNAME", "S.SESSIONID > 0 and isnull(SM.IS_ACTIVE,0)=1 and COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "S.SESSIONID DESC"); //--AND FLOCK = 1
            }

            ddlSession.Focus();
        }
        else
        {

            ddlSession.SelectedIndex = 0;

            ddlSemester.SelectedIndex = 0;
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE AND C.BOS_DEPTNO = P.BOS_DEPTNO)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
            //ddlSemester.Focus();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_PAPERSET_DETAILS P ON  (S.SEMESTERNO = P.SEMESTERNO AND (CANCEL IS NULL OR CANCEL = 0)) INNER JOIN ACD_COURSE C ON (C.CCODE = P.CCODE)INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = P.SESSIONNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >0  AND C.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SM.SESSIONID = " + Convert.ToInt32(ddlSession.SelectedValue), " S.SEMESTERNO");
            ddlSemester.Focus();
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
        lvCourse4.DataSource = null;
        lvCourse4.DataBind();
        pnlList4.Visible = false;
    }

}