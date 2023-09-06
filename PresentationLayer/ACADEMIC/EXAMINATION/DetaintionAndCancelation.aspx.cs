//======================================================================================
// PROJECT NAME  : UAIMS [RAIPUR]                                                          
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : DETENTION AND CANCELATION
// CREATION DATE : 30 OCT 2012                                                          
// CREATED BY    :                                                 
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
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;


public partial class ACADEMIC_EXAMINATION_DetaintionAndCancelation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //string undo_DetainRemark = string.Empty;
    //string undoDetainDate = string.Empty;

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
                if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "4") || (Session["usertype"].ToString() == "1"))
                {
                    //check availibility
                    CheckActivity();//[23-09-2016]
                    this.FillDropdown();
                }
                else
                {
                    objCommon.DisplayMessage(this.updDetained, "You are not authorized to view this page!!", this.Page);
                }
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            ddlDegree.Focus();
        }
    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExamRegistration.aspx");
        }
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        //sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'EXAMREG' AND SA.STARTED = 1");
        sessionno = Session["currentsession"].ToString();
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                updDetained.Visible = false;

            }
            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                updDetained.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            updDetained.Visible = false;
        }
        dtr.Close();
    }
    #endregion

    #region Cancelation
    protected void Cancelation_Show_Students_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region Common Methods
    private void FillDropdown()
    {
        try
        {
            //Term.
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSessionCancel, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");

            //Degree Name
            if (Session["usertype"].ToString() == "4" || Session["usertype"].ToString() == "1")
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            else
                objCommon.FillDropDownList(ddlDegree, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO > 0 AND DEPTNO =  " + Session["userdeptno"].ToString(), "D.DEGREENO");

            if (Session["usertype"].ToString() == "3" && (Session["username"].ToString().ToUpper() == "HODCHEMISTRY" || Convert.ToString(Session["username"]) == "HOD_PHY"))
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO = 1", "DEGREENO");
            }
            objCommon.FillDropDownList(ddlDegreeCancel, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            ddlSession.SelectedIndex = -1;
            ddlSession.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Detaintion
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH BR ON (CD.BRANCHNO=BR.BRANCHNO)", "CD.BRANCHNO", "LONGNAME", "DEGREENO =" + Convert.ToInt32(ddlDegree.SelectedValue), "BRANCHNO");
                ddlBranch.Focus();
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                }
                else
                {
                    objCommon.DisplayMessage("Please Select Branch!", this.Page);
                    ddlBranch.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Degree!", this.Page);
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
            }
            else
            {
                objCommon.DisplayMessage("Please Select Scheme!", this.Page);
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //GET STUDENT DETAILS
    protected void btnShowStudentDetaintion_Click(object sender, EventArgs e)
    {
        this.show_StudentsForDetaintion();
        this.EnableRadioButton();
    }

    private void EnableRadioButton()
    {
        if (ddlSession.SelectedIndex != -1 && ddlDegree.SelectedIndex != -1 && ddlBranch.SelectedIndex != -1 && ddlScheme.SelectedIndex != -1 && ddlSem.SelectedIndex != -1)
        {
            rbtlDetaind.Visible = true;
        }
        else
        {
            rbtlDetaind.Visible = false;
        }
        rbtlDetaind.Visible = false;
    }

    private void show_StudentsForDetaintion()
    {
        try
        {
            StudentController detCan = new StudentController();

            // if (rdbOptions.SelectedValue != "")
            // {

            DataSet ds = null;
            ds = detCan.GetStudentListForDetention(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), 2);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDetained.DataSource = ds;
                lvDetained.DataBind();
                this.tblBackLog.Visible = true;
                btnSubmit.Enabled = true;

                //DataTable dt = ds.Tables[0];
                //DataRow dr = dt.Rows[0];

                foreach (RepeaterItem LvItem in lvDetained.Items)
                {
                    DropDownList ddlstatus = LvItem.FindControl("ddlstatus") as DropDownList;
                    Label lblStatus = LvItem.FindControl("lblStatus") as Label;

                    if (lblStatus.Text != "Please Select" && lblStatus.Text != "")
                    {
                        ddlstatus.SelectedItem.Text = lblStatus.Text;
                        ddlstatus.Enabled = false;
                    }
                    else
                    {
                        //ddlstatus.Items.Insert(0, "Please Select");

                        //ddlstatus.Items.Insert(0, "ELIGIBLE");
                        //ddlstatus.Items.Insert(1, "DETAINED");
                        //ddlstatus.Items.Insert(2, "PENDING");
                    }
                }
            }
            else
            {
                lvDetained.DataSource = null;
                lvDetained.DataBind();
                objCommon.DisplayMessage(updDetained, "NO STUDENT FOUND", this.Page);
            }

            // }
            // else
            // {
            //     objCommon.DisplayMessage(updDetained, "Please Select Detained By Option!", this.Page);
            //   rdbOptions.Focus();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.show_StudentsForDetaintion --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //SUBMIT DATAA
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController detCan = new StudentController();
        CustomStatus cs = CustomStatus.Others;

        string idNos = string.Empty;
        string provDetentions = string.Empty; string finalDetentions = string.Empty;
        string detentionRemarks = string.Empty; string finalDetentionRemarks = string.Empty;
        string undo_DetainRemark = string.Empty; string Remarks = string.Empty;
        int count = 0; int countRem = 0; int countddl = 0;
        string Status = string.Empty;
        try
        {
            //if (rdbOptions.SelectedValue != "")
            //{
            foreach (RepeaterItem lvItem in lvDetained.Items)
            {
                CheckBox chkFinalDetained = lvItem.FindControl("chkFinalDetain") as CheckBox;
                TextBox txtremarks = lvItem.FindControl("txtremarks") as TextBox;
                DropDownList ddlStatus = lvItem.FindControl("ddlStatus") as DropDownList;

                if (chkFinalDetained.Checked && chkFinalDetained.Enabled == true)
                {
                    provDetentions += "Y" + "$";
                    finalDetentions += "Y" + "$";
                    Label lblIdNo = lvItem.FindControl("idNo") as Label;
                    if (!idNos.Contains(lblIdNo.ToolTip.ToString()))
                    {
                        idNos += lblIdNo.ToolTip + "$";
                        count++;
                    }
                    if (txtremarks.Text != string.Empty)
                    {
                        Remarks += txtremarks.Text.Trim() + "$";
                    }
                    else
                    {
                        countRem++;
                    }
                    if (ddlStatus.SelectedIndex > 0)
                    {
                        Status += Convert.ToInt32(ddlStatus.SelectedValue) + "$";
                    }
                    else
                    {
                        countddl++;
                    }
                }
            }
            int semesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            int sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int sectionNo = Convert.ToInt32(ddlSectionDetaintion.SelectedValue);
            string collageCode = Session["colcode"].ToString();
            int schemono = Convert.ToInt32(ddlScheme.SelectedValue);
            if (countddl > 0)
            {
                objCommon.DisplayMessage(updDetained, "Please Select Status!", this.Page);
                return;
            }
            if (countRem > 0)
            {
                objCommon.DisplayMessage(updDetained, "Remarks Cannot Be Blank!", this.Page);
                return;
            }
            if (count == 0)
                objCommon.DisplayMessage(updDetained, "Please select atleast one student!", this.Page);
            else
            {
                cs = (CustomStatus)detCan.UpdateDetention(sessionNo, idNos, schemono, semesterNo, provDetentions, finalDetentions, collageCode, 2, Remarks, Convert.ToInt32(Session["userno"]), Status);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    this.show_StudentsForDetaintion();
                    objCommon.DisplayMessage(updDetained, "Record Save successfully!", this.Page);
                }
            }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updDetained, "Please Select Detained By Option!", this.Page);
            //    rdbOptions.Focus();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetentionCancelation.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //GET REPORT
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                if (ddlScheme.SelectedIndex > 0)
                {
                    if (ddlSem.SelectedIndex > 0)
                    {
                        ShowReport("Detained Student Report", "rpt_Detaind_student.rpt");
                    }
                    else
                    {
                        objCommon.DisplayMessage(updDetained, "Please Select Semester!", this.Page); ddlSem.Focus();
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updDetained, "Please Select Scheme!", this.Page); ddlScheme.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(updDetained, "Please Select Branch!", this.Page); ddlBranch.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage(updDetained, "Please Select Degree!", this.Page); ddlDegree.Focus();
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_DETANIED_BY=2,@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USER_NAME=" + Session["username"].ToString();

            // code for showing reports
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DetainedAndCancel.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Not In Use Code
    protected void ddlBranchCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegreeCancel.SelectedValue == "1" && ddlBranchCancel.SelectedValue == "99")
            {
                ddlSchemeCancel.Items.Clear();
                ddlSchemeCancel.Items.Add(new ListItem("Please Select", "0"));
                ddlSchemeCancel.Items.Add(new ListItem("FIRST YEAR [RTM]", "24"));
                ddlSchemeCancel.Items.Add(new ListItem("FIRST YEAR[AUTONOMOUS]", "1"));
                ddlSchemeCancel.Focus();
            }
            else
            {
                // Scheme Name
                objCommon.FillDropDownList(ddlSchemeCancel, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranchCancel.SelectedValue), "B.BRANCHNO");
                ddlSchemeCancel.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlDegreeCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // Branch Name
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            objCommon.FillDropDownList(ddlBranchCancel, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegreeCancel.SelectedValue), "B.BRANCHNO");
            ddlBranchCancel.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSchemeCancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchemeCancel.SelectedItem.Text == "FIRST YEAR [R.T.M]")
            {
                ddlSemesterCancel.Items.Clear();
                ddlSemesterCancel.Items.Add(new ListItem("Please Select", "0"));
                ddlSemesterCancel.Items.Add(new ListItem("I", "1"));
            }
            else
            {
                //SemesterNo
                objCommon.FillDropDownList(ddlSemesterCancel, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            }
            if (ddlSchemeCancel.SelectedItem.Text == "FIRST YEAR[AUTONOMOUS]")
            {
                SectionDetained.Visible = true;
            }
            else
            {
                SectionDetained.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void getStudentList(int sesionno, int degreeno, int branchno, int semno, string DetainStatus)
    {
        StudentController scontroller = new StudentController();
        DataSet ds = null;
        try
        {
            ds = scontroller.GetDetainedList(sesionno, degreeno, branchno, semno, DetainStatus);
            if (ds != null)
            {
                lvDetainedStudent.DataSource = ds;
                lvDetainedStudent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetentionCancelation.getStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rbtlDetaind_SelectedIndexChanged(object sender, EventArgs e)
    {
        int sesionno;
        int degreeno;
        int branchno;
        int semno;
        string DetainStatus = string.Empty;
        if (ddlSession.SelectedIndex != -1 && ddlDegree.SelectedIndex != -1 && ddlBranch.SelectedIndex != -1 && ddlSem.SelectedIndex != -1)
        {
            sesionno = Convert.ToInt32(ddlSession.SelectedValue);
            degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            semno = Convert.ToInt32(ddlSem.SelectedValue);
            if (rbtlDetaind.SelectedValue == "Prov Detain")
            {
                DetainStatus = "N";
                pnlDetained.Visible = true;
                // ''lvDetainedStudent.Visible = true;
                pnlDetained.Visible = true;
                getStudentList(sesionno, degreeno, branchno, semno, DetainStatus);
            }
            else if (rbtlDetaind.SelectedValue == "Final Detain")
            {
                DetainStatus = "Y";
                pnlDetained.Visible = true;
                // ''lvDetainedStudent.Visible = true;
                pnlDetained.Visible = true;
                getStudentList(sesionno, degreeno, branchno, semno, DetainStatus);
            }
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            btnReport.Visible = true;
            //' 'btnCancelStudReport.Visible = true;
        }
        else
        {
            btnReport.Visible = false;
            // ''btnCancelStudReport.Visible = false;
        }
    }

    protected void btnCancelStudReport_Click(object sender, EventArgs e)
    {
        //if(Session["usertype"] != "4")
        //if(ddlDegree.SelectedIndex <=0  || ddlBranch.SelectedIndex <= 0)
        //objCommon.FillDropDown
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + "Cancel Student Report";
        url += "&path=~,Reports,Academic," + "rptCancelStudentReport.rpt";
        //  if(Session["usertype"] == "4")
        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_SEMESTER=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

        // code for showing reports
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updDetained, this.updDetained.GetType(), "controlJSScript", sb.ToString(), true);
    }
    #endregion

}
