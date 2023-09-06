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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                if ((Session["dec"].ToString() == "1" && Session["usertype"].ToString() == "3") || (Session["usertype"].ToString() == "4"))
                    this.FillDropdown();
                else
                    objCommon.DisplayMessage(this.updDetained,"You are not authorized to view this page!!", this.Page);
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
            if (Session["usertype"].ToString() == "4")
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
            // Branch Name

            if (Session["usertype"].ToString() == "3" && (Session["username"].ToString().ToUpper() == "HODCHEMISTRY"))
            {
                //DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO IN (11,12,13,16,17,18) AND D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");

            }
            else if (Session["usertype"].ToString() == "3" && (Session["username"].ToString().ToUpper() == "HOD_PHY"))
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO IN (14,15,19,20,21) AND D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND DEPTNO = " + Session["userdeptno"].ToString(), "B.BRANCHNO");
            }
            // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND DEPTNO = " + Session["userdeptno"].ToString(), "B.BRANCHNO");
            objCommon.FillDropDownList(ddlBranchCancel, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue), "B.BRANCHNO");
            ddlBranch.Focus();
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
                // Scheme Name
                if ((Session["username"].ToString().ToUpper() == "HODCHEMISTRY") ||(Session["usertype"].ToString() == "4") || (Session["username"].ToString().ToUpper() == "HOD_PHY"))
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND B.DEGREENO = " + ddlDegree.SelectedValue , "B.BRANCHNO");
                else
                    objCommon.FillDropDownList(ddlScheme, "ACD_BRANCH B INNER JOIN ACD_SCHEME S ON S.BRANCHNO = B.BRANCHNO ", "S.SCHEMENO", "S.SCHEMENAME", "B.BRANCHNO= " + Convert.ToInt32(ddlBranch.SelectedValue) + " AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO = " + Session["userdeptno"].ToString(), "B.BRANCHNO");
                ddlScheme.Focus();
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
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
            //DataSet ds = objCommon.FillDropDown("User_Acc", "UA_TYPE", "UA_NAME", "UA_TYPE ='" + Convert.ToString(Session["usertype"]) + "'and (UA_NAME='HOD_CHE'or UA_NAME='HOD_PHY')", "");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            if (Convert.ToString(Session["username"]).ToUpper() == "HODCHEMISTRY" || Convert.ToString(Session["username"]).ToUpper() == "HOD_PHY") 
                    {
                        //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO = 1 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
                        objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO = 1 ", "SEMESTERNO"); 
                    }
                    else
                    {
                        if (ddlDegree.SelectedValue == "1")
                        {
                            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO >1 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
                            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 2", "SEMESTERNO");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN ACD_STUDENT_RESULT SR ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + ddlScheme.SelectedValue, "S.SEMESTERNO");
                        }
                       
                    }
            //    }
            //}
            
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetaintionAndCancelation.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
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
        //try
        //{
        //    //char sp = '-';
        //    //string[] ccode = ddlCourse.SelectedItem.Text.Split(sp);

        //    DataSet ds = null;
        //    //LOOKUP RETURNS 1 WHEN AUTONOMOUS SCHEME IS THERE, SO THAT WE HAVE TO CHECK REGNO AND ROLLNO CONDITION NOT TO BE NULL.
        //    ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT ST ON (S.IDNO = ST.IDNO ) LEFT OUTER JOIN ACD_DETENTION_INFO DET ON (S.IDNO = DET.IDNO AND DET.SESSIONNO = ST.SESSIONNO)", " DISTINCT S.IDNO", " S.IDNO,DBO.FN_DESC('SECTION',ST.SECTIONNO) AS SECTION,ST.SEATNO,STUDNAME,ROLLNO,FINAL_DETAIN,S.SECTIONNO,DET.PROV_DETAIN,DET.IDNO AS DETAIN_IDNO ", "S.ROLLNO IS NOT NULL AND ST.SESSIONNO = " + ddlSession.SelectedValue + " AND PREV_STATUS = 0 AND S.SCHEMENO = " + ddlScheme.SelectedValue + " AND ST.SEMESTERNO = " + ddlSem.SelectedValue, "S.SECTIONNO,DET.PROV_DETAIN DESC");

        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvDetained.DataSource = ds;
        //        lvDetained.DataBind();
        //        this.tblBackLog.Visible = true;
                   
        //        foreach (RepeaterItem item in lvDetained.Items)
        //        {
        //            CheckBox chkProv = item.FindControl("chkAccept") as CheckBox;
        //            CheckBox chkFinal = item.FindControl("chkFinalDetain") as CheckBox;

        //            if (chkProv.Checked == true && chkFinal.Checked == true)
        //            {
        //                chkProv.Checked = true;
        //                chkFinal.Checked = true;
        //                chkProv.Enabled = false;
        //                chkFinal.Enabled = false;
        //            }
        //            //if (chkProv.Checked == true && chkFinal.Checked == false )
        //            //{
        //            //    chkFinal.Checked = false;
        //            //    chkProv.Enabled = false;
        //            //    chkFinal.Enabled = true;
        //            //}
        //        }
        //    }
        //    else
        //    {
        //        lvDetained.DataSource = null;
        //        lvDetained.DataBind();
        //        //pnlStudents.Visible = false;
        //        objCommon.DisplayMessage(updDetained, "NO STUDENT FOUND", this);
        //    }
        //    rbtlDetaind.Items[1].Selected = true;   //final detain
        //    pnlDetained.Visible = true;
        //    lvDetainedStudent.Visible = true;
        //    getStudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), "Y");

        //}
        try
        {
            //char sp = '-';
            //string[] ccode = ddlCourse.SelectedItem.Text.Split(sp);

            DataSet ds = null;
            //LOOKUP RETURNS 1 WHEN AUTONOMOUS SCHEME IS THERE, SO THAT WE HAVE TO CHECK REGNO AND ROLLNO CONDITION NOT TO BE NULL.
            ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT ST ON (S.IDNO = ST.IDNO ) LEFT OUTER JOIN ACD_DETENTION_INFO DET ON (S.IDNO = DET.IDNO AND DET.SESSIONNO = ST.SESSIONNO)", "DISTINCT S.IDNO", "S.IDNO,DBO.FN_DESC('SECTION',ST.SECTIONNO) AS SECTION,ST.SEATNO,STUDNAME,ROLLNO,FINAL_DETAIN,S.SECTIONNO,DET.PROV_DETAIN,DET.IDNO AS DETAIN_IDNO ", "S.ROLLNO IS NOT NULL AND ST.SESSIONNO = " + ddlSession.SelectedValue + " AND PREV_STATUS = 0 AND ST.SCHEMENO = " + ddlScheme.SelectedValue + " AND ST.SEMESTERNO = " + ddlSem.SelectedValue, "S.SECTIONNO,DET.PROV_DETAIN DESC");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvDetained.DataSource = ds;
                lvDetained.DataBind();
                this.tblBackLog.Visible = true;

                foreach (RepeaterItem item in lvDetained.Items)
                {
                    CheckBox chkProv = item.FindControl("chkAccept") as CheckBox;
                    CheckBox chkFinal = item.FindControl("chkFinalDetain") as CheckBox;

                    if (chkProv.Checked == true && chkFinal.Checked == true)
                    {
                        chkProv.Checked = true;
                        chkFinal.Checked = true;
                        chkProv.Enabled = false;
                        chkFinal.Enabled = false;
                    }
                    chkProv.Visible = false;                    
                }                   
            }                
            else
            {
                lvDetained.DataSource = null;
                lvDetained.DataBind();
                //pnlStudents.Visible = false;
                objCommon.DisplayMessage(updDetained, "NO STUDENT FOUND", this);
            }
           Control HeaderTemplate = lvDetained.Controls[0].Controls[0];
            Label lblHeader = HeaderTemplate.FindControl("lblHeader") as Label;
            lblHeader.Visible = false;
            //HeaderTemplate.Visible = false;
            rbtlDetaind.Items[1].Selected = true;   //final detain
            pnlDetained.Visible = true;
            // '' lvDetainedStudent.Visible = true;
            getStudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), "Y");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.FillStudentList --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        StudentController detCan = new StudentController();
        CustomStatus cs = CustomStatus.Others;
        
        string idNos = string.Empty;
        string provDetentions = string.Empty;
        string finalDetentions = string.Empty;
        string detentionRemarks = string.Empty;
        string finalDetentionRemarks = string.Empty;
        string undo_DetainRemark = string.Empty;
        int count = 0;
        try
        {
            foreach (RepeaterItem lvItem in lvDetained.Items)
            {

                //CheckBox chkSelect = lvItem.FindControl("chkAccept") as CheckBox;
                CheckBox chkFinalDetained = lvItem.FindControl("chkFinalDetain") as CheckBox;

                //if (chkSelect.Checked)
                if (chkFinalDetained.Checked)
                {
                    provDetentions += "Y" + "$";
                    //provDetentions = true;
                    Label lblIdNo = lvItem.FindControl("idNo") as Label;
                    idNos += lblIdNo.ToolTip + "$";
                    count++;
                }


                if (chkFinalDetained.Checked)
                {
                    finalDetentions += "Y" + "$";
                    //finalDetentions = true;
                    Label lblIdNo = lvItem.FindControl("idNo") as Label;
                    if (!idNos.Contains(lblIdNo.ToolTip.ToString()))
                    {
                        idNos += lblIdNo.ToolTip + "$";
                        count++;
                    }

                    undo_DetainRemark += " " + "$";
                    //finalDetentionDate += DateTime.Now.ToString() + ",";
                }
                //else if (chkSelect.Checked && !chkFinalDetained.Checked)
                else if (!chkFinalDetained.Checked)
                    finalDetentions += "N" + "$";
            }
            int semesterNo = Convert.ToInt32(ddlSem.SelectedValue);
            int sessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int sectionNo = Convert.ToInt32(ddlSectionDetaintion.SelectedValue);

            //undo_DetainRemark += getUndoRemark();
            string collageCode = Session["colcode"].ToString();
            if (count == 0)
                objCommon.DisplayMessage(updDetained, "Please select atleast 1 student!", this);
            else
            {
                //cs = (CustomStatus)detCan.UpdateDetention(sessionNo, idNos, semesterNo, provDetentions, finalDetentions, collageCode);
                //if (cs.Equals(CustomStatus.RecordUpdated))
                //{
                //    this.show_StudentsForDetaintion();
                //    objCommon.DisplayMessage(updDetained, "Records Save successfully!", this);
                //}
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EXAMINATION_DetentionCancelation.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Detained Student Report", "rpt_Detaind_student.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (ddlScheme.SelectedIndex > 0 && ddlSem.SelectedIndex > 0)
            {
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else if (ddlSem.SelectedIndex > 0)
            {
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=0,@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
            else
            {
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=0,@P_SEMESTERNO=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            }
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

    private void getStudentList(int sesionno, int degreeno, int branchno, int semno, string DetainStatus)
    {
        StudentController scontroller = new StudentController();
        DataSet ds = null;
        try
        {
            ds = scontroller.GetDetainedList(sesionno, degreeno, branchno, semno,DetainStatus);
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
                getStudentList(sesionno, degreeno, branchno,  semno,DetainStatus);

            }

            else if (rbtlDetaind.SelectedValue == "Final Detain")
            {
                DetainStatus = "Y";
                pnlDetained.Visible = true;
                // ''lvDetainedStudent.Visible = true;
                pnlDetained.Visible = true;
                getStudentList(sesionno, degreeno, branchno,  semno, DetainStatus);

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
        //    if(ddlDegree.SelectedIndex <=0  || ddlBranch.SelectedIndex <= 0)
        //        objCommon.FillDropDown
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

    
}
