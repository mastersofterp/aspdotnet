using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_EXAMINATION_AssignBundleToFaculty : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();

    #region "Page Event"
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

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                                       
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                   
                    ViewState["bundleno"] = null;
                }
                FillDropdown();
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_AssignBundleToVauer.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateBundle.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateBundle.aspx");
        }
    }
    #endregion "Page Event"

    #region "General"
    private void FillDropdown()
    {
        try
        {
            // To Fill Dropdown of Session
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "FLOCK = 1", "SESSIONNO DESC");
           // objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_PNAME", "FLOCK = 1 AND IS_ACTIVE = 1  ", "SESSIONID DESC");
            objCommon.FillDropDownList(ddlSession, "acd_exam_bundlelist b inner join acd_session_master sm on (sm.sessionno = b.SESSIONNO) inner join ACD_SESSION S ON (S.SESSIONID = SM.SESSIONID)", "DISTINCT S.SESSIONID","S.SESSION_PNAME", "S.SESSIONID > 0", "S.SESSIONID DESC");
            int userno = Convert.ToInt32(Session["userno"]);
            int usertype = Convert.ToInt32(Session["usertype"]);
            //int deptno = Convert.ToInt32(Session["userdeptno"]);

            string usename = Session["username"].ToString();
            txtDtOfIssue.Text = System.DateTime.Now.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_AssignBundleToVauer.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion "General"

    #region "SelectedIndexChanged"
    // On Course Code Change
    //protected void txtCcode_TextChanged(object sender, EventArgs e)
    //{
    //    DataSet ds;
    //    int courseno = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "COURSENO", "CCODE='" + txtCcode.Text.Trim().ToString()+"'"));
    //    objCommon.FillDropDownList(ddlBundle, "ACD_EXAM_BUNDLELIST", "BUNDLEID", "BUNDLE", "COURSENO=" + courseno + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "BUNDLEID");
    //    //ds = objCommon.FillDropDown("ACD_RECOMMENDED_PANEL", "UA_NO", "(CASE INTERNAL_EXTERNAL WHEN 1 THEN DBO.FN_DESC('UA',UA_NO) WHEN 2 THEN DBO.FN_DESC('VALUER',UA_NO) END)RPNAME", "CCODE='" + txtCcode.Text.Trim().ToString() + "' AND ADMBATCH = (SELECT YEAR FROM ACD_SESSION_MASTER WHERE SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ")", "UA_NO");
    //    objCommon.FillDropDownList(ddlValuer, "ACD_COURSE_TEACHER", "UA_NO", "DBO.FN_DESC('UA',UA_NO)UANAME", "CCODE='" + txtCcode.Text.Trim().ToString() + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCEL,0)=0", "UA_NO");
        
    //    BindBundleList();
    //}

    // ADDED BY SHUBHAM ON 11-01-2023
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCourse, "ACD_EXAM_BUNDLELIST B INNER JOIN ACD_COURSE C ON (C.COURSENO = B.COURSENO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = B.SESSIONNO)", "DISTINCT C.COURSENO", "C.COURSE_NAME", "SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "C.COURSENO");
    }

    // ADDED BY SHUBHAM ON 11-01-2023
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlBundle, "ACD_EXAM_BUNDLELIST", "BUNDLEID", "BUNDLE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "BUNDLEID");
        objCommon.FillDropDownList(ddlBundle, "ACD_EXAM_BUNDLELIST B INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = B.SESSIONNO)", "BUNDLEID", "B.BUNDLE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "BUNDLE");
        //objCommon.FillDropDownList(ddlValuer, "ACD_COURSE_TEACHER", "UA_NO", "DBO.FN_DESC('UA',UA_NO)UANAME", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCEL,0)=0", "UA_NO");
        objCommon.FillDropDownList(ddlValuer, "ACD_COURSE_TEACHER T INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = T.SESSIONNO)", "DISTINCT UA_NO", "DBO.FN_DESC('UA',UA_NO)UANAME", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCEL,0)=0", "UA_NO");
        //objCommon.FillDropDownList(ddlValuer, "ACD_COURSE_TEACHER T INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = T.SESSIONNO) INNER JOIN USER_ACC U ON (T.UA_NO = U.UA_NO) INNER JOIN ACD_DEPARTMENT D ON (D.DEPTNO = T.DEPTNO)", "DISTINCT T.UA_NO", "(UA_FULLNAME + ' - ' + D.DEPTCODE)Name", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCEL,0)=0", "UA_NO");
        BindBundleList();

    }


    #endregion "SelectedIndexChanged"

    #region "Button Event"
    // Submit Click Event
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDtOfIssue.Text.Trim() != "")
            {
                int uano = 0;
                CustomStatus cs = CustomStatus.Error;
                if (ddlValuer.SelectedValue != "0")
                {
                    uano = Convert.ToInt32(ddlValuer.SelectedValue);
                }
               
                //cs = (CustomStatus)objExamController.UpdateValuerWithBundle(Convert.ToInt32(ddlSession.SelectedValue), txtCcode.Text.Trim(), Convert.ToInt32(ddlBundle.SelectedValue), Convert.ToInt32(ddlValuer.SelectedValue), Convert.ToDateTime(txtDtOfIssue.Text));
                cs = (CustomStatus)objExamController.UpdateValuerWithBundle(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlBundle.SelectedValue), uano, Convert.ToDateTime(txtDtOfIssue.Text));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(updExam, "Bundle Assigned to Valuer Successfuly..!!", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordFound))
                {
                    objCommon.DisplayMessage(updExam, "Bundle is alredy Assigned to Valuer..!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updExam, "Error in Bundle Assignment ..", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updExam, "Please select Date of Issue", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_AssignBundleToVauer.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        ClearControls();
        BindBundleList();

    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    // Report Click Event
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlDate.SelectedIndex > 0)
        {
            ShowReport("Assign Bundle Report", "rptAssignBundlereport.rpt");
        }
        else
        {
            objCommon.DisplayMessage(updExam, "Please select Date of Paper", this.Page);
        }
    }
    #endregion "Button Event"

    #region "Private Methods"
    // To Bind The List View
    private void BindBundleList()
    {
        DataSet ds = objExamController.GetValuerInfoWithBundleNo(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
        //Convert.ToInt32(ddlCourse.SelectedValue)
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvAssignBundleTOFaculty.DataSource = ds.Tables[0];
                lvAssignBundleTOFaculty.DataBind();
            }
            else
            {
                lvAssignBundleTOFaculty.DataSource = null;
                lvAssignBundleTOFaculty.DataBind();
            }
            lvAssignBundleTOFaculty.Visible = true;
            //pnlValuer.Visible = true;
        }
        else
        {
            lvAssignBundleTOFaculty.DataSource = null;
            lvAssignBundleTOFaculty.DataBind();
        }
    }
    // Show Report Method
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
           // url += "exporttype=" + exporttype;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (Convert.ToInt32(Session["OrgId"]) == 9)// Added by Shubham on 01022023
            {

                int clg_id = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER SM INNER JOIN ACD_EXAM_DATE A ON (A.SESSIONNO = SM.SESSIONNO) inner join ACD_EXAM_BUNDLELIST B ON (B.COURSENO = A.COURSENO)", "SM.COLLEGE_ID", " EXAMDATE = CONVERT(DATETIME,'" + ddlDate.SelectedValue + "',103)"));
                url += "&param=@P_COLLEGE_CODE=" + clg_id + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + " ";
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAM_DATE=" + ddlDate.SelectedValue.ToString() + " ";
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.updExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PREEXAMINATION_AssignBundleToVauer.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    // To Clear Controls 
    private void ClearControls()
    {
        ddlValuer.SelectedIndex = 0;
        //txtDtOfIssue.Text = string.Empty;
    }
    #endregion "Private Methods"


    protected void ddlBundle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBundle.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlDate, "ACD_EXAM_DATE E INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = E.SESSIONNO)INNER JOIN ACD_EXAM_BUNDLELIST B ON (B.SESSIONNO = E.SESSIONNO AND B.COURSENO = E.COURSENO)", "DISTINCT CONVERT(NVARCHAR,E.EXAMDATE,103)EXAMDATE ", "CONVERT(NVARCHAR,E.EXAMDATE,103)DATE", "SM.SESSIONID =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND E.COURSENO  =" + Convert.ToInt32(ddlCourse.SelectedValue), "EXAMDATE");
        }
        else
        {
            ddlBundle.SelectedIndex = 0;
        }
    }
}