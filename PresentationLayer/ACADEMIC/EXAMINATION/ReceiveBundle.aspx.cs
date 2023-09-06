//======================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Receive Bundle                             
// CREATION DATE : 06 DEC 2012                                                          
// CREATED BY    :                                          
// MODIFIED DATE :                                                                      
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
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_EXAMINATION_ReceiveBundle : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    Exam objExam = new Exam();

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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                    this.FillDropdown();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }

    private void FillDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        Clear();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //check if previous entry is done
            string str = "";// objCommon.LookUp("ACD_EXAM_BUNDLELIST", "BUNDLEID", "SESSIONNO=" + ddlSession.SelectedValue + " AND COURSENO=" + ddlCourse.SelectedValue);
            if (str == "")
            {
                string ccode = lblCourse.Text;
                string seatfrom = string.Empty;
                string seatto = string.Empty; 
                if (ccode != "")
                {
                    CustomStatus ret = (CustomStatus)objExamController.ReceiveBundle(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(lblCourse.ToolTip), lblccode.Text, Convert.ToInt16(rdbSet.SelectedValue), 0, Convert.ToInt16(lblFaculty.ToolTip), Convert.ToInt16(txtBundle.ToolTip.ToString()));
                    if (ret != CustomStatus.TransactionFailed || ret != CustomStatus.Error)
                    {
                        objCommon.DisplayMessage(this.updBundle,"Bundle alloted to faculty Successfully", this.Page);
                    }
                    else
                        objCommon.DisplayMessage(this.updBundle, "Error in Saving Record...", this.Page);
                }
            }
            else
                objCommon.DisplayMessage(this.updBundle, "Bundle already alloted", this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AbsentStudentEntry.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    //private void FillDepartment()
    //{
    //    //fill department
    //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "DEPTNO", "DEPTNAME", "DEPTNO>0", "DEPTNAME");
    //}


    protected void txtBundle_TextChanged(object sender, EventArgs e)
    {
        Clear();
        string str = objCommon.LookUp("ACD_EXAM_BUNDLELIST", "BUNDLEID", "BUNDLE = '"+ txtBundle.Text +"'");
        if (str != "")
        {
            objCommon.DisplayMessage(this.updBundle,"Bundle already exists!", this.Page);
            btnSubmit.Enabled = false;
        }
        else
            btnSubmit.Enabled = true;
    }
    
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ExamController objSC = new ExamController();
        //DataTableReader dr = null;
        //Clear();
        //dr = objCommon.FillDropDown("ACD_EXAM_BUNDLELIST", "BUNDLEID,SESSIONNO,VALUER_UA_NO,COURSENO,CCODE", "SEATNOFROM,SEATNOTO,PREV_STATUS,ISSUEDATE,STATUS,LOCK,SECTIONNO,BUNDLE,BUNDLECREATED_DATE", "SESSIONNO = " + ddlSession.SelectedValue + " AND BUNDLE='" + txtBundle.Text+"'", "BUNDLEID").CreateDataReader();

        DataTableReader dtr = objSC.GetIssueBundle(Convert.ToInt32(ddlSession.SelectedValue),txtBundle.Text.Trim());

        if (dtr != null)

            if (dtr.Read())
           {
            lblScheme.Text = objCommon.LookUp("ACD_SCHEME", "SCHEMENAME", "SCHEMENO IN (SELECT SCHEMENO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
            lblScheme.ToolTip = objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "SCHEMENO IN (SELECT SCHEMENO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
            lblSemester.Text = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO IN (SELECT SEMESTERNO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
            lblSemester.ToolTip = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNO", "SEMESTERNO IN (SELECT SEMESTERNO FROM ACD_COURSE WHERE COURSENO =" + dtr["COURSENO"].ToString() + ")");
            lblCourse.Text = objCommon.LookUp("ACD_COURSE", "COURSE_NAME", " COURSENO =" + dtr["COURSENO"].ToString());
            lblCourse.ToolTip = dtr["COURSENO"].ToString();
            lblccode.Text = objCommon.LookUp("ACD_COURSE", "CCODE", " COURSENO =" + dtr["COURSENO"].ToString());
            lblFaculty.Text = objCommon.LookUp("USER_ACC", "UA_FULLNAME", " UA_NO =" + dtr["VALUER_UA_NO"].ToString());
            lblFaculty.ToolTip = dtr["VALUER_UA_NO"].ToString();
            txtBundle.ToolTip = dtr["BUNDLEID"].ToString();
            trbtnSubmit.Visible = true;
            trFaculty.Visible = true;
            trScheme.Visible = true;
            trSemester.Visible = true;
            trCourse.Visible = true;
           }
           else
           {
             objCommon.DisplayMessage(this.updBundle, "Bundle does not exists!", this.Page);
           }
          dtr.Close();
    }
    
    private void Clear()
    {
        lblccode.Text = string.Empty;
        lblCourse.Text = string.Empty;
        lblFaculty.Text = string.Empty;
        lblScheme.Text = string.Empty;
        lblSemester.Text = string.Empty;
    }
}

