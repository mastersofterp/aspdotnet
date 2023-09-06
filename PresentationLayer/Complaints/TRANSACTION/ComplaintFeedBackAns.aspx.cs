//=================================================================================
// PROJECT NAME  : UAIMS                                                          
// MODULE NAME   : Academic                                                                
// PAGE NAME     : StudentFeedBackAns.aspx                                               
// CREATION DATE : 26-03-2014                                                   
// CREATED BY    : Sheru Kumar Gaur                           
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;

public partial class Complaints_TRANSACTION_ComplaintFeedBackAns : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //StudentFeedBackController objSFBC = new StudentFeedBackController();
    Complaint objCEnt = new Complaint();
    ComplaintController objCC = new ComplaintController();
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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                //Commented by Nokhlal for designing purpose.....22-Feb-2018
               // string chkactivity = objCommon.LookUp("SESSION_ACTIVITY A INNER JOIN ACD_SESSION_MASTER B ON (A.SESSION_NO = B.SESSIONNO)", "TOP 1 B.SESSIONNO,B.SESSION_NAME", "ACTIVITY_NO = 10 AND A.STARTED = 1 AND CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112) AND CAST(GETDATE() AS TIME) BETWEEN CAST(STARTTIME AS TIME) AND CAST(ENDTIME AS TIME)");


                if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "4")
                {
                    divstud.Visible = true;
                    pnlchkinfo.Visible = true;
                    pnlStudInfo.Visible = true;
                    FillLabel();
                    return;

                }
                else
                {
                    if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "4")
                    //if (Session["usertype"].ToString() == "2" )
                    {
                        //stuInfo.Visible = true;
                        //GetFeedbackDetails(Convert.ToInt32(Session["idno"]));
                        divstud.Visible = true;
                        pnlchkinfo.Visible = true;
                        pnlStudInfo.Visible = true;
                        FillLabel();
                        //objCommon.DisplayMessage("1.If your class was distributed into sections,please fill the feedback only for the faculty(s) who taught you (i.e. your section).\\n 2.Your identity (roll number/name) will be detached from your feedback responses.So, please fill your feedback fairly and responsibly.\\n 3.Trouble? Wrong list of courses? Failed to submit feedback? Contact MIS Administrator at mis.lnmiit@iitms.co.in (Extension: 228).", this.Page);
                        return;

                        // FillCourseQuestion();
                        // FillTeacherQuestion();
                        // FillOtherQuestion();
                        //pnlFeedback.Visible = true;
                    }
                    //else
                    //{
                    //    pnlchkinfo.Visible = true;
                    //    pnlSearch.Visible = true;
                    //    pnlStudInfo.Visible = false;
                    //    //stuInfo.Visible = false;
                    //}
                }

                //FillLabel();
                //FillCourseQuestion();
                //FillTeacherQuestion();
                //FillOtherQuestion();
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }

    private void FillLabel()
    {
        try
        {

            Course objCourse = new Course();
            //CourseController objCC = new CourseController();
            SqlDataReader dr = null;
            if (Session["usertype"].ToString() == "2")
                dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(Session["idno"]));
            else
                dr = objCC.GetShemeSemesterByUser(Convert.ToInt32(ViewState["Id"]));
            if (dr != null)
            {
                if (dr.Read())
                {

                    ViewState["id"] = dr["IDNO"].ToString();
                    string chkactivitysesion = objCommon.LookUp("SESSION_ACTIVITY A INNER JOIN ACD_SESSION_MASTER B ON (A.SESSION_NO = B.SESSIONNO)", " B.SESSIONNO", "ACTIVITY_NO = 10 AND A.STARTED = 1 AND CONVERT(NVARCHAR(10),GETDATE(),112) BETWEEN CONVERT(NVARCHAR(10),[START_DATE],112) AND CONVERT(NVARCHAR(10),END_DATE,112) AND CAST(GETDATE() AS TIME) BETWEEN CAST(STARTTIME AS TIME) AND CAST(ENDTIME AS TIME)");

                    DataSet dsinfo = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN ACD_SESSION_MASTER B ON(A.SESSIONNO=B.SESSIONNO) INNER JOIN ACD_SEMESTER C ON(A.SEMESTERNO=C.SEMESTERNO)  INNER JOIN ACD_STUDENT D ON(A.IDNO=D.IDNO)INNER JOIN ACD_DEGREE E ON(D.DEGREENO=E.DEGREENO)INNER JOIN ACD_BRANCH F ON(D.BRANCHNO=F.BRANCHNO)", "TOP 1 A.SEMESTERNO", "A.SESSIONNO,B.SESSION_NAME,C.SEMESTERNAME,E.DEGREENAME,F.LONGNAME", "A.IDNO=" + Convert.ToInt32(ViewState["id"]) + "and a.sessionno=" + Convert.ToInt32(chkactivitysesion), "A.SESSIONNO DESC");

                    if (dsinfo.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                        lblName.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
                        lblSession.Text = dsinfo.Tables[0].Rows[0]["session_name"].ToString();
                        lblSession.ToolTip = dsinfo.Tables[0].Rows[0]["sessionno"].ToString();
                        lblSemester.Text = dsinfo.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsinfo.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblDegree.Text = dsinfo.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        lblBranch.Text = dsinfo.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblScheme.Text = dr["CURRICULUMNAME"] == null ? string.Empty : dr["CURRICULUMNAME"].ToString();
                        lblScheme.ToolTip = dr["SCHEMENO"] == null ? string.Empty : dr["SCHEMENO"].ToString();
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                    }
                    else
                    {
                        objCommon.DisplayMessage("Student not registered for this session", this.Page);
                        pnlchkinfo.Visible = false;
                        return;

                    }


                }
            }
            if (dr != null) dr.Close();
            if (lblScheme.ToolTip != "" || lblScheme.ToolTip == "")
            {
                //DataSet dsc = objCC.GetCOMPLAINTforfeedback(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"]));
                DataSet dsc = objCC.GetCOMPLAINTforfeedback(76, Convert.ToInt32(Session["userno"]));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));

                if (dsc.Tables[0].Rows.Count > 0)
                {
                    ddlCourse.DataSource = dsc;
                    ddlCourse.DataValueField = dsc.Tables[0].Columns[0].ToString();
                    //ddlCourse.DataTextField = dsc.Tables[0].Columns[1].ToString();
                    ddlCourse.DataBind();
                }
                else
                {
                    ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                }
                DataSet dsslist = objCC.GetCOURSESforfeedbacklist(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"]));
                //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT", "distinct COURSENO", "COURSENAME", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "  and (cancel=0 or cancel is null)", "COURSENO");
                // DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT", "distinct COURSENO,electiveno", "COURSENAME", "IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + "  and (cancel=0 or cancel is null)", "COURSENO");
                lvSelected.DataSource = dsslist;
                lvSelected.DataBind();
                this.CheckSubjectAssign();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillLabel-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void CheckSubjectAssign()
    {
        try
        {
            SqlDataReader dr = null;
            foreach (ListViewDataItem item in lvSelected.Items)
            {
                Label lblComplete = item.FindControl("lblComplete") as Label;
                lblComplete.ForeColor = System.Drawing.Color.Red;
                lblComplete.Text = "Incompleted";
            }
            if (Session["usertype"].ToString() == "2")
                dr = objCC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["idno"]));
            else
                dr = objCC.GetCourseSelected(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(ViewState["Id"]));
            if (dr != null)
            {
                while (dr.Read())
                {
                    foreach (ListViewDataItem item in lvSelected.Items)
                    {
                        CheckBox chkCourseNO = item.FindControl("chkCourseNO") as CheckBox;
                        CheckBox chkElectiveNO = item.FindControl("chkElectiveNO") as CheckBox;
                        Label lblComplete = item.FindControl("lblComplete") as Label;
                        Label lblfac = item.FindControl("lblfac") as Label;
                        if (Convert.ToInt32(chkCourseNO.ToolTip) == Convert.ToInt32(dr["COURSENO"].ToString()) && Convert.ToInt32(chkElectiveNO.ToolTip) == Convert.ToInt32(dr["ELECTIVENO"].ToString()) && lblfac.Text.ToString().Trim() == dr["faculty"].ToString().Trim())
                        {

                            chkCourseNO.Checked = true;
                            if (chkCourseNO.Checked == true)
                            {
                                lblComplete.ForeColor = System.Drawing.Color.Green;
                                //lblComplete.Text = "Completed for "+dr["faculty"].ToString();
                                lblComplete.Text = "Completed ";
                            }
                            //bool chklock = true;
                            //if (chklock == Convert.ToBoolean(dr["answerlock"]))
                            //{
                            //    lblComplete.ForeColor = System.Drawing.Color.Green;
                            //    lblComplete.Text = "Completed";
                            //}
                            //if (Convert.ToInt32(chkElectiveNO.ToolTip) == Convert.ToInt32(dr["ELECTIVENO"].ToString()))
                            //{
                            //    chkElectiveNO.Checked = true;
                            //    if (chkElectiveNO.Checked == true)
                            //    {
                            //        lblComplete.ForeColor = System.Drawing.Color.Red;
                            //        lblComplete.Text = "Completed";
                            //    }
                            //    break;
                            //}
                        }

                    }
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.CheckSubjectAssign-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    private void FillTeacherQuestion()
    {
        try
        {
          //  StudentFeedBack objSEB = new StudentFeedBack();
            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
            InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
            if (InstcourseNo > 0 && InstelectiveNo == 0)
            {
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "COURSENO=" + InstcourseNo);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            if (InstcourseNo == 0 && InstelectiveNo > 0)
            {
                string electgrp = objCommon.LookUp("ACD_ELECTIVELIST", "ELECTGROUPNO", "ELECTIVENO=" + InstelectiveNo);
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "ELECTIVENO=" + electgrp);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            objCEnt.CTID = 2;

            //DataSet ds = objSFBC.GetFeedBackQuestionForMaster(objSEB);
            //if (ds != null)
            //{
            //    lvTeacher.DataSource = ds;
            //    lvTeacher.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.FillTeacherQuestion-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //By Sheru
    // get student details
    private void GetFeedbackDetails()
    {
        try
        {
            DataSet ds = objCC.GetFeedbackInfo(Convert.ToInt32(Session["userno"]), Convert.ToString(ddlCourse.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblComplaintDate.Text = ds.Tables[0].Rows[0]["COMPLAINTDATE"].ToString();
                lblComplaint.Text = ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                lblCompName.Text = ds.Tables[0].Rows[0]["COMPLAINTEE_NAME"].ToString();
                lblfaculty.Text = ds.Tables[0].Rows[0]["FACULTY"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Estt_complaint.GetStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCourseQuestion()
    {
        try
        {
            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
            InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
            if (InstcourseNo > 0 && InstelectiveNo == 0)
            {
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "COURSENO=" + InstcourseNo);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            if (InstcourseNo == 0 && InstelectiveNo > 0)
            {
                string electgrp = objCommon.LookUp("ACD_ELECTIVELIST", "ELECTGROUPNO", "ELECTIVENO=" + InstelectiveNo);
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "ELECTIVENO=" + electgrp);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            objCEnt.CTID = 1;

            DataSet ds = objCC.GetFeedBackQuestionForMaster(objCEnt);
            if (ds != null)
            {
                //    if (objSEB.SubId == 1)
                //    {
                //        divtheory.Visible = true;
                //        divpract.Visible = false;
                //    }
                //    else if (objSEB.SubId == 2)
                //    {
                //        divtheory.Visible = false;
                //        divpract.Visible = true;
                //    }
                //    else
                //    {
                //        divtheory.Visible = true;
                //        divpract.Visible = false;
                //    }
                //    lvCourse.DataSource = ds;
                //    lvCourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.fillCourseQuestion-> " + ex.Message + "" + ex.StackTrace);
            }
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    private void FillOtherQuestion()
    {
        try
        {
            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
            InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
            if (InstcourseNo > 0 && InstelectiveNo == 0)
            {
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "COURSENO=" + InstcourseNo);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            if (InstcourseNo == 0 && InstelectiveNo > 0)
            {
                string electgrp = objCommon.LookUp("ACD_ELECTIVELIST", "ELECTGROUPNO", "ELECTIVENO=" + InstelectiveNo);
                string substatus = objCommon.LookUp("ACD_COURSE", "SUBJECTTYPE", "ELECTIVENO=" + electgrp);
                objCEnt.SubId = Convert.ToInt32(substatus);
            }
            objCEnt.CTID = 3;

            DataSet ds = objCC.GetFeedBackQuestionForMaster(objCEnt);
            if (ds != null)
            {
                //lvOther.DataSource = ds;
                //lvOther.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.fillOtherQuestion-> " + ex.Message + "" + ex.StackTrace);
            }
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
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //string QuesType=string.Empty;
    //        //string quesremark = string.Empty;
    //        if (Session["usertype"].ToString() == "2")
    //        {
    //            char[] sp = { '-' };
    //            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
    //            int InstcourseNo = 0; int InstelectiveNo = 0;
    //            if (ddlCourse.SelectedIndex > 0)
    //            {
    //                InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
    //                InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
    //            }
    //            objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
    //            objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
    //            objSEB.Date = DateTime.Now;
    //            objSEB.CollegeCode = Session["colcode"].ToString();
    //            objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
    //            objSEB.CourseNo = Convert.ToInt32(InstcourseNo);
    //            objSEB.ElectiveNo = Convert.ToInt32(InstelectiveNo);
    //            objSEB.UA_NO = Convert.ToInt32(ddlTecher.SelectedValue);

    //            //objSEB.UA_NO = Convert.ToInt32(lblTecher.ToolTip);
    //            objSEB.FB_Status = true;
    //            if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();

    //            string QuesType = string.Empty;
    //            string quesremark = string.Empty;
    //            if (Session["usertype"].ToString() == "2")
    //            {
    //                foreach (ListViewDataItem dataitem in lvCourse.Items)
    //                {
    //                    RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
    //                    HiddenField hdncourse=dataitem.FindControl("hdnCoursequeid")as  HiddenField;
    //                    Label lblCourse = dataitem.FindControl("lblCourse") as Label;
    //                    TextBox txtcremark = dataitem.FindControl("txtCremark") as TextBox;
    //                    if (Convert.ToInt32(lblCourse.ToolTip) == 1)
    //                    {
    //                        if (rblCourse.SelectedValue == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblCourse.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblCourse.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdncourse.Value + "$";
    //                            QuesType += (lblCourse.ToolTip.ToString()) + "$";
    //                            quesremark += txtcremark.Text + "$";
    //                        }
    //                    }
    //                    if (Convert.ToInt32(lblCourse.ToolTip) == 2)
    //                    {
    //                        if (txtcremark.Text == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblCourse.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblCourse.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdncourse.Value + "$";
    //                            QuesType += (lblCourse.ToolTip.ToString()) + "$";
    //                            quesremark += txtcremark.Text + "$";
    //                        }
    //                    }
    //                }

    //                foreach (ListViewDataItem dataitem in lvTeacher.Items)
    //                {
    //                    RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
    //                    Label lblTeacher = dataitem.FindControl("lblTeacher") as Label;
    //                    HiddenField hdnteacher = dataitem.FindControl("hdnTeacherqueid") as HiddenField;
    //                    TextBox txttremark = dataitem.FindControl("txtTremark") as TextBox;
    //                    if (Convert.ToInt32(lblTeacher.ToolTip) == 1)
    //                    {
    //                        if (rblTeacher.SelectedValue == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblTeacher.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblTeacher.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdnteacher.Value + "$";
    //                            QuesType += (lblTeacher.ToolTip.ToString()) + "$";
    //                            quesremark += txttremark.Text + "$";
    //                        }
    //                    }
    //                    if (Convert.ToInt32(lblTeacher.ToolTip) == 2)
    //                    {
    //                        if (txttremark.Text == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblTeacher.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblTeacher.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdnteacher.Value + "$";
    //                            QuesType += (lblTeacher.ToolTip.ToString()) + "$";
    //                            quesremark += txttremark.Text + "$";
    //                        }
    //                    }
    //                }

    //                foreach (ListViewDataItem dataitem in lvOther.Items)
    //                {
    //                    RadioButtonList rblOther = dataitem.FindControl("rblOther") as RadioButtonList;
    //                    Label lblOther = dataitem.FindControl("lblOther") as Label;
    //                    HiddenField hdnother = dataitem.FindControl("hdnOtherqueid") as HiddenField;
    //                    TextBox txtoremark = dataitem.FindControl("txtOremark") as TextBox;
    //                    if (Convert.ToInt32(lblOther.ToolTip) == 1)
    //                    {
    //                        if (rblOther.SelectedValue == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblOther.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblOther.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdnother.Value + "$";
    //                            QuesType += (lblOther.ToolTip.ToString()) + "$";
    //                            quesremark += txtoremark.Text + "$";
    //                        }
    //                    }
    //                    if (Convert.ToInt32(lblOther.ToolTip) == 2)
    //                    {
    //                        if (txtoremark.Text == "")
    //                        {
    //                            objCommon.DisplayMessage("Please answer Question No." + lblOther.Text , this.Page);
    //                            return;
    //                        }
    //                        else
    //                        {
    //                            objSEB.AnswerIds += rblOther.SelectedValue + "$";
    //                            objSEB.QuestionIds += hdnother.Value + "$";
    //                            QuesType += (lblOther.ToolTip.ToString()) + "$";
    //                            quesremark += txtoremark.Text + "$";
    //                        }
    //                    }
    //                }
    //            }
    //            int ret = objSFBC.AddStudentFeedBackAns(objSEB, QuesType, quesremark);
    //            if (ret == 1)
    //            {
    //                objCommon.DisplayMessage("Your FeedBack Saved Successfully", this.Page);

    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Your FeedBack Not Saved Successfully", this.Page);
    //            }
    //            this.CheckSubjectAssign();

    //            //this.ClearControls();
    //            string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + InstcourseNo + " AND ELECTIVENO=" + InstelectiveNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
    //            if (Convert.ToInt32(count) != 0)
    //            {
    //                lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed For " + ddlTecher.SelectedItem.Text;
    //                lblMsg.ForeColor = System.Drawing.Color.Green;
    //                lblMsg.Visible = false;
    //                pnlFeedback.Visible = false;
    //                btnSave.Enabled = false;
    //                ddlCourse.SelectedValue = "0";
    //                ddlTecher.SelectedValue = "0";
    //                //btnLock.Enabled = true;
    //            }
    //            //string chklock = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "count(1)", "answerlock='true' and courseno=" + InstcourseNo + "and electiveno =" + InstelectiveNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
    //            //if (chklock != string.Empty)
    //            //    if (chklock != "0")
    //            //    {
    //            //        lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed & Locked For " + ddlTecher.SelectedItem.Text;
    //            //        lblMsg.ForeColor = System.Drawing.Color.Green;
    //            //        lblMsg.Visible = true;
    //            //        pnlFeedback.Visible = false;
    //            //        //objCommon.DisplayMessage("Your FeedBack was already Locked ", this.Page);
    //            //        btnSave.Enabled = false;
    //            //        //btnLock.Enabled = false;
    //            //    }

    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Only Students fills this form!!", this.Page);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {

        int output = 0;
        output = objCC.ComplaintFeedbackUpdate(Convert.ToInt32(Session["userno"]), Convert.ToString(ddlCourse.SelectedValue), Convert.ToString(lblComplaint.Text), Convert.ToString(lblfaculty.Text), Convert.ToString(lblCompName.Text), Convert.ToString(rblist1.SelectedValue), txtRemark.Text.ToString());
        if (output != -99)
        {

            objCommon.DisplayMessage("Your FeedBack Update Successfully", this.Page);

        }
        else
        {

            objCommon.DisplayMessage("Complaint Failed", this.Page);
        }
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "2")
        {
            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            if (ddlCourse.SelectedIndex > 0)
            {
                InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
                InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
            }
            objCEnt.SessionNo = Convert.ToInt32(lblSession.ToolTip);



            objCEnt.Idno = Convert.ToInt32(lblName.ToolTip);
            objCEnt.CourseNo = Convert.ToInt32(InstcourseNo);
            objCEnt.ElectiveNo = Convert.ToInt32(InstelectiveNo);
            objCEnt.UA_NO = Convert.ToInt32(ddlTecher.SelectedValue);

            string chkalready = objCommon.LookUp("acd_online_feedback", "Count(1)", "sessionno=" + Convert.ToInt32(lblSession.ToolTip) + "and courseno=" + Convert.ToInt32(InstcourseNo) + "and electiveno=" + Convert.ToInt32(InstelectiveNo) + "and idno=" + Convert.ToInt32(lblName.ToolTip) + "AND ISNULL(ANSWERLOCK,0)=0");
            if (chkalready == string.Empty || chkalready == "0")
            {
                objCommon.DisplayMessage("Please save the feedback status first", this.Page);
                return;

            }
            //int ret = objSFBC.AddStudentFeedBackLock(objSEB);
            //if (ret == 2)
            //{
            //    objCommon.DisplayMessage("Your FeedBack Locked Successfully", this.Page);
            //    this.ClearControls();
            //    this.CheckSubjectAssign();
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Your FeedBack Not Locked Successfully", this.Page);
            //}
        }
    }

    private void ClearControls()
    {
        FillCourseQuestion();
        FillTeacherQuestion();
        lblMsg.Text = string.Empty;
        //lblTecher.Text = string.Empty;
        ddlTecher.SelectedIndex = 0;
        txtRemark.Text = string.Empty;
        ddlCourse.SelectedIndex = 0;
        // pnlFeedback.Visible = false;
    }

    //private void FillAnswers()
    //{
    //     char[] sp = { '-' };
    //        string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
    //        int InstcourseNo = 0; int InstelectiveNo = 0;
    //        if (ddlCourse.SelectedIndex > 0)
    //        {
    //            InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
    //            InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
    //        }
    //    objSEB.SessionNo = Convert.ToInt32(lblSession.ToolTip);
    //    objSEB.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
    //    objSEB.Date = DateTime.Now;
    //    objSEB.CollegeCode = Session["colcode"].ToString();
    //    objSEB.Idno = Convert.ToInt32(lblName.ToolTip);
    //    objSEB.CourseNo = Convert.ToInt32(InstcourseNo);
    //    objSEB.ElectiveNo = Convert.ToInt32(InstelectiveNo);
    //    objSEB.UA_NO = Convert.ToInt32(ddlTecher.SelectedValue);

    //    //objSEB.UA_NO = Convert.ToInt32(lblTecher.ToolTip);
    //    objSEB.FB_Status = true;
    //    if (!txtRemark.Text.Equals(string.Empty)) objSEB.Remark = txtRemark.Text.ToString();

    //        string QuesType=string.Empty;
    //        string quesremark = string.Empty;
    //        if (Session["usertype"].ToString() == "2")
    //        {
    //            foreach (ListViewDataItem dataitem in lvCourse.Items)
    //            {
    //                RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
    //                Label lblCourse = dataitem.FindControl("lblCourse") as Label;
    //                TextBox txtcremark = dataitem.FindControl("txtCremark") as TextBox;
    //                if (Convert.ToInt32(lblCourse.ToolTip) == 1)
    //                {
    //                    if (rblCourse.SelectedValue == "")
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblCourse.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblCourse.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblCourse.Text + ",";
    //                        QuesType += (lblCourse.ToolTip.ToString()) + ",";
    //                        quesremark += txtcremark.Text + ",";
    //                    }
    //                }
    //                if (Convert.ToInt32(lblCourse.ToolTip) == 2)
    //                {
    //                    if (txtcremark.Text == "")
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblCourse.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblCourse.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblCourse.Text + ",";
    //                        QuesType += (lblCourse.ToolTip.ToString()) + ",";
    //                        quesremark += txtcremark.Text + ",";
    //                    }
    //                }
    //            }

    //            foreach (ListViewDataItem dataitem in lvTeacher.Items)
    //            {
    //                RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
    //                Label lblTeacher = dataitem.FindControl("lblTeacher") as Label;
    //                TextBox txttremark = dataitem.FindControl("txtTremark") as TextBox;
    //                if (Convert.ToInt32(lblTeacher.ToolTip) == 1)
    //                {
    //                    if (rblTeacher.SelectedValue == "")
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblTeacher.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblTeacher.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblTeacher.Text + ",";
    //                        QuesType += (lblTeacher.ToolTip.ToString()) + ",";
    //                        quesremark += txttremark.Text + ",";
    //                    }
    //                }
    //                if (Convert.ToInt32(lblTeacher.ToolTip) == 2)
    //                {
    //                    if (txttremark.Text == "")
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblTeacher.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblTeacher.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblTeacher.Text + ",";
    //                        QuesType += (lblTeacher.ToolTip.ToString()) + ",";
    //                        quesremark += txttremark.Text + ",";
    //                    }
    //                }
    //            }

    //            foreach (ListViewDataItem dataitem in lvOther.Items)
    //            {
    //                RadioButtonList rblOther = dataitem.FindControl("rblOther") as RadioButtonList;
    //                Label lblOther = dataitem.FindControl("lblOther") as Label;
    //                TextBox txtoremark = dataitem.FindControl("txtOremark") as TextBox;
    //                if (Convert.ToInt32(lblOther.ToolTip) == 1)
    //                {
    //                    if (rblOther.SelectedValue == "" )
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblOther.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblOther.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblOther.Text + ",";
    //                        QuesType += (lblOther.ToolTip.ToString()) + ",";
    //                        quesremark += txtoremark.Text + ",";
    //                    }
    //                }
    //                if (Convert.ToInt32(lblOther.ToolTip) == 2)
    //                {
    //                    if (txtoremark.Text == "")
    //                    {
    //                        objCommon.DisplayMessage("Question No." + lblOther.Text + " must be select", this.Page);
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        objSEB.AnswerIds += rblOther.SelectedValue + ",";
    //                        objSEB.QuestionIds += lblOther.Text + ",";
    //                        QuesType += (lblOther.ToolTip.ToString()) + ",";
    //                        quesremark += txtoremark.Text + ",";
    //                    }
    //                }
    //            }
    //        }
    //    int ret = objSFBC.AddStudentFeedBackAns(objSEB,QuesType,quesremark);
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string adteacher = string.Empty;
            string chk = string.Empty;
            string subid = string.Empty;//= objCommon.LookUp("ACD_STUDENT_RESULT", "SUBID", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "  and (cancel=0 or cancel is null)");
            lblMsg.Text = string.Empty;
            // pnlFeedback.Visible = true;

            stuInfo.Visible = true;
            GetFeedbackDetails();

            //if (ddlCourse.SelectedIndex > 0)
            //{
            //    objCommon.FillDropDownList(ddlTecher, "ACD_STUDENT_RESULT R INNER JOIN USER_ACC U ON (R.UA_NO=U.UA_NO)", "DISTINCT R.UA_NO", "UA_FULLNAME", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "   and (cancel=0 or cancel is null)", "UA_FULLNAME");
            //}
            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            FillCourseQuestion();
            FillTeacherQuestion();
            FillOtherQuestion();
            //int InstrcourseNo = 0;
            if (ddlCourse.SelectedIndex > 0)
            {
                InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
                InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());
                //InstrcourseNo = Convert.ToInt32(Instcourses[2].Trim());
                string strUA_NO = "0";
                //if (InstrcourseNo > 0)
                //{
                //   strUA_NO = objCommon.LookUp("ACD_READINGINFO", "UA_NO", "SESSIONNO = '" + lblSession.ToolTip + "' AND RCOURSENO = '" + InstrcourseNo + "'");//Doubt ?


                //}
                if (InstelectiveNo > 0)
                {
                    strUA_NO = objCommon.LookUp("ACD_OFFEREDCOURSE", "UA_NO", "SESSIONNO = '" + lblSession.ToolTip + "' AND ELECTIVENO = '" + InstelectiveNo + "'");//Doubt ?

                }
                if (InstcourseNo > 0)
                {
                    strUA_NO = objCommon.LookUp("ACD_OFFEREDCOURSE", "UA_NO", "SESSIONNO = '" + lblSession.ToolTip + "' AND COURSENO = '" + InstcourseNo + "'");
                }
                if (strUA_NO == "")
                {
                    objCommon.DisplayMessage("No faculty for this course", this.Page);
                    ddlTecher.Items.Clear();
                    ddlTecher.Items.Insert(0, "Please Select");
                    return;
                }
                objCommon.FillDropDownList(ddlTecher, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_NO in (" + strUA_NO + ")", "UA_NO");
            }
            else
            {
                ddlTecher.Items.Clear();
                ddlTecher.Items.Insert(0, "Please Select");
                //ddlTecher.SelectedValue = "0";
            }
            //}
            //else
            //{
            //    //objCommon.FillDropDownList(ddlTecher, "ACD_STUDENT_RESULT R INNER JOIN USER_ACC U ON (R.UA_NO_PRAC=U.UA_NO)", "DISTINCT R.UA_NO_PRAC AS UA_NO", "UA_FULLNAME", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "  and (cancel=0 or cancel is null)", "UA_FULLNAME");
            //    //adteacher = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN USER_ACC U ON (R.UA_NO_PRAC=U.UA_NO)", "DISTINCT AD_TEACHER_PR", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + "  and (cancel=0 or cancel is null)");
            //}

            // pnlFeedback.Visible = false;
            //if (subid == "1")
            // chk = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN USER_ACC U ON (R.UA_NO=U.UA_NO)", "COUNT(DISTINCT R.UA_NO)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
            //else
            //    chk = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN USER_ACC U ON (R.UA_NO_PRAC=U.UA_NO)", "COUNT(DISTINCT R.UA_NO_PRAC)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));

            //if (chk == "0")
            //    pnlMsg.Visible = true;
            //else
            //    pnlMsg.Visible = false;

            //DataSet ds = objCommon.FillDropDown("ACD_COURSE_TEACHER T INNER JOIN USER_ACC U ON (T.UA_NO=U.UA_NO)", "T.UA_NO", "UA_FULLNAME", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue), "COURSENO");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    //lblTecher.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            //    //lblTecher.ToolTip = ds.Tables[0].Rows[0]["UA_NO"].ToString();
            //    if (Session["usertype"].ToString() == "2")
            //        pnlFeedback.Visible = true;
            //    else
            //        pnlFeedback.Visible = false;
            //    pnlMsg.Visible = false;
            //}
            //else
            //{
            //    //lblTecher.Text = "Teacher Not Allot!!";
            //    //lblTecher.ToolTip = "0";
            //    pnlFeedback.Visible = false;
            //    if (Session["usertype"].ToString() == "2")
            //        pnlMsg.Visible = true;
            //    else
            //        pnlMsg.Visible = false;
            //}
            //string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)+" AND IDNO="+Convert.ToInt32(lblName.ToolTip));
            //if (Convert.ToInt32(count) != 0)
            //{
            //    lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed";
            //    lblMsg.ForeColor = System.Drawing.Color.Red;
            //    lblMsg.Visible = true;
            //}
            //else
            //{
            //    lblMsg.Text = "";
            //    lblMsg.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ddlCourse_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));
        if (Convert.ToInt32(count) != 0)
            ShowReport("Student_FeedBack", "StudentFeedBackAns.rpt");
        else
            objCommon.DisplayMessage("Record Not Found", this.Page);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblName.ToolTip) + ",@P_SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + ",@P_SCHEMENO=" + Convert.ToInt32(lblScheme.ToolTip) + ",@P_SEMESTERNO=" + Convert.ToInt32(lblSemester.ToolTip) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //string idno = objCommon.LookUp("ACD_ONLINE_FEEDBACK FB INNER JOIN ACD_STUDENT S ON(FB.IDNO=S.IDNO)", "DISTINCT FB.IDNO", "S.REGNO='"+txtSearch.Text.Trim()+"'");
            string idno = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT IDNO", "REGNO='" + txtSearch.Text.Trim() + "'");
            if (idno != "")
            {
                ViewState["Id"] = Convert.ToInt32(idno);
                FillLabel();
                pnlStudInfo.Visible = true;
                //btnReport.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.btnSearch_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlTecher_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            char[] sp = { '-' };
            string[] Instcourses = ddlCourse.SelectedValue.Split(sp);
            int InstcourseNo = 0; int InstelectiveNo = 0;
            InstcourseNo = Convert.ToInt32(Instcourses[0].Trim());
            InstelectiveNo = Convert.ToInt32(Instcourses[1].Trim());

            //for checking course to particuler subject type.
            //----------------------------------------------

            FillCourseQuestion();
            FillTeacherQuestion();
            FillOtherQuestion();

            //----------------------------------------------

            //string chkans = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "count(1)", "status='true' and courseno=" + InstcourseNo + "and electiveno =" + InstelectiveNo);
            //if (chkans != string.Empty)
            //    if (chkans != "0")
            //    {
            //        btnSave.Enabled = false;
            //        btnLock.Enabled = true;
            //    }
            //    else
            //    {
            //        btnSave.Enabled = true;
            //        btnLock.Enabled = false;
            //    }



            if (Session["usertype"].ToString() == "2")
            {
                if (ddlTecher.SelectedValue == "0")
                {
                    // pnlFeedback.Visible = false;
                    return;
                }

                lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Incompleted For " + ddlTecher.SelectedItem.Text;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                //pnlFeedback.Visible = true;
                btnSave.Enabled = true;
                // btnLock.Enabled = true;
                //DataSet ds = objCommon.FillDropDown("ACD_ONLINE_FEEDBACK a inner join ACD_FEEDBACK_QUESTION b on(a.questionid=b.questionid)", "*", "electiveno", "electiveno=" + InstelectiveNo + "and courseno = " + InstcourseNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + "and b.ctid=1 and a.idno=" + Convert.ToInt32(Session["idno"]), string.Empty);
                //string comment = objCommon.LookUp("ACD_ONLINE_FEEDBACK_REMARK", "remark", "electiveno=" + InstelectiveNo + "and courseno = " + InstcourseNo + "and sessionno =" + Convert.ToInt32(lblSession.ToolTip) + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + "and idno=" + Convert.ToInt32(Session["idno"]));
                //if (comment != string.Empty)
                //{
                //    txtRemark.Text = comment;
                //}
                //else
                //{
                //    txtRemark.Text = string.Empty;
                //}
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    int i = 0;
                //    //txtRemark.Text=ds.Tables[0].Rows
                //    foreach (ListViewDataItem dataitem in lvCourse.Items)
                //    //for (int i = 0; i <= lvCourse.Items.Count; i++)
                //    {
                //        if (i >= ds.Tables[0].Rows.Count)
                //        {
                //            return;
                //        }

                //        RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                //        TextBox txtcremark = dataitem.FindControl("txtCremark") as TextBox;

                //        if (ds.Tables[0].Rows[dataitem.DataItemIndex]["questype"].ToString() == "1")
                //        {
                //            rblCourse.SelectedValue = ds.Tables[0].Rows[dataitem.DataItemIndex]["answerid"].ToString();
                //        }
                //        if (ds.Tables[0].Rows[dataitem.DataItemIndex]["questype"].ToString() == "2")
                //        {
                //            txtcremark.Text = ds.Tables[0].Rows[dataitem.DataItemIndex]["quesremark"].ToString();
                //        }
                //        i++;
                //    }
                //}
                //else
                //{
                //    foreach (ListViewDataItem dataitem in lvCourse.Items)
                //    //for (int i = 0; i <= lvCourse.Items.Count; i++)
                //    {


                //        RadioButtonList rblCourse = dataitem.FindControl("rblCourse") as RadioButtonList;
                //        TextBox txtcremark = dataitem.FindControl("txtCremark") as TextBox;

                //        rblCourse.SelectedValue = null;

                //        txtcremark.Text = "";


                //    }
                //}
                //DataSet dsteacher = objCommon.FillDropDown("ACD_ONLINE_FEEDBACK a inner join ACD_FEEDBACK_QUESTION b on(a.questionid=b.questionid)", "*", "electiveno", "electiveno=" + InstelectiveNo + "and courseno = " + InstcourseNo + "and b.ctid=2 and a.idno=" + Convert.ToInt32(Session["idno"]), string.Empty);
                //if (dsteacher.Tables[0].Rows.Count > 0)
                //{
                //    int i = 0;
                //    foreach (ListViewDataItem dataitem in lvTeacher.Items)
                //    //for (int i = 0; i <= lvCourse.Items.Count; i++)
                //    {

                //        if (i >= dsteacher.Tables[0].Rows.Count)
                //        {
                //            return;
                //        }
                //        RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
                //        TextBox txttremark = dataitem.FindControl("txtTremark") as TextBox;

                //        if (dsteacher.Tables[0].Rows[i]["questype"].ToString() == "1")
                //        {
                //            rblTeacher.SelectedValue = dsteacher.Tables[0].Rows[i]["answerid"].ToString();
                //        }
                //        if (dsteacher.Tables[0].Rows[i]["questype"].ToString() == "2")
                //        {
                //            txttremark.Text = dsteacher.Tables[0].Rows[i]["quesremark"].ToString();
                //        }
                //        i++;
                //    }
                //}
                //else
                //{
                //    foreach (ListViewDataItem dataitem in lvTeacher.Items)
                //    //for (int i = 0; i <= lvCourse.Items.Count; i++)
                //    {


                //        RadioButtonList rblTeacher = dataitem.FindControl("rblTeacher") as RadioButtonList;
                //        TextBox txttremark = dataitem.FindControl("txtTremark") as TextBox;

                //        rblTeacher.SelectedValue = null;

                //        txttremark.Text = "";


                //    }
                //}
                //string chkother = objCommon.LookUp("ACD_ONLINE_FEEDBACK a inner join ACD_FEEDBACK_QUESTION b on(a.questionid=b.questionid)", "Count(1)", "b.ctid=3");
                //if (Convert.ToInt32(chkother) <= 0)
                //{
                //DataSet dsother = objCommon.FillDropDown("ACD_ONLINE_FEEDBACK a inner join ACD_FEEDBACK_QUESTION b on(a.questionid=b.questionid)", "*", "electiveno", "a.idno=" + Convert.ToInt32(ViewState["id"]) + "and b.ctid=3", string.Empty);
                //if (dsother.Tables[0].Rows.Count > 0)
                //{
                //    int i = 0;
                //    foreach (ListViewDataItem dataitem in lvOther.Items)
                //    //for (int i = 0; i <= lvCourse.Items.Count; i++)
                //    {

                //        if (i >= dsother.Tables[0].Rows.Count)
                //        {
                //            return;
                //        }
                //        RadioButtonList rblOther = dataitem.FindControl("rblOther") as RadioButtonList;
                //        TextBox txtoremark = dataitem.FindControl("txtOremark") as TextBox;


                //        if (dsother.Tables[0].Rows[i]["questype"].ToString() != null && dsother.Tables[0].Rows[i]["questype"].ToString() != "")
                //        {
                //            if (dsother.Tables[0].Rows[i]["questype"].ToString() == "1")
                //            {
                //                rblOther.SelectedValue = dsother.Tables[0].Rows[i]["answerid"].ToString();
                //            }
                //            if (dsother.Tables[0].Rows[i]["questype"].ToString() == "2")
                //            {
                //                txtoremark.Text = dsother.Tables[0].Rows[i]["quesremark"].ToString();
                //            }
                //        }
                //        i++;
                //    }

                //}
                //else
                //{
                //    foreach (ListViewDataItem dataitem in lvOther.Items)
                //    {

                //        RadioButtonList rblOther = dataitem.FindControl("rblOther") as RadioButtonList;
                //        TextBox txtoremark = dataitem.FindControl("txtOremark") as TextBox;

                //        rblOther.SelectedValue = null;
                //        txtoremark.Text = "";
                //    }
                //}


                string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + InstcourseNo + " AND ELECTIVENO=" + InstelectiveNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
                if (Convert.ToInt32(count) != 0)
                {
                    lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed For " + ddlTecher.SelectedItem.Text;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Visible = true;
                    // pnlFeedback.Visible = true;
                    //btnSave.Enabled = true;
                    //btnLock.Enabled = true;
                }
                string chklock = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "count(1)", "answerlock='true' and courseno=" + InstcourseNo + "and electiveno =" + InstelectiveNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
                if (chklock != string.Empty)
                    if (chklock != "0")
                    {
                        lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed & Locked For " + ddlTecher.SelectedItem.Text;
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Visible = true;
                        //pnlFeedback.Visible = false;
                        //objCommon.DisplayMessage("Your FeedBack was already Locked ", this.Page);
                        //btnSave.Enabled = false;
                        //btnLock.Enabled = false;
                    }
            }
            else
            {
                lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Incompleted For " + ddlTecher.SelectedItem.Text;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
                //pnlFeedback.Visible = false;
                string count = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "COUNT(*)", "SESSIONNO=" + Convert.ToInt32(lblSession.ToolTip) + " AND COURSENO=" + InstcourseNo + " AND ELECTIVENO=" + InstelectiveNo + " AND UA_NO=" + Convert.ToInt32(ddlTecher.SelectedValue) + " AND IDNO=" + Convert.ToInt32(lblName.ToolTip));
                if (Convert.ToInt32(count) != 0)
                {
                    lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed For " + ddlTecher.SelectedItem.Text;
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    lblMsg.Visible = true;
                    //pnlFeedback.Visible = false;
                    //btnSave.Enabled = true;
                    //btnLock.Enabled = true;
                }
                string chklock = objCommon.LookUp("ACD_ONLINE_FEEDBACK", "count(1)", "answerlock='true' and courseno=" + InstcourseNo + "and electiveno =" + InstelectiveNo);
                if (chklock != string.Empty)
                    if (chklock != "0")
                    {
                        lblMsg.Text = ddlCourse.SelectedItem.Text + " Course FeedBack Completed & Locked For " + ddlTecher.SelectedItem.Text;
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Visible = true;
                        //pnlFeedback.Visible = false;
                        //objCommon.DisplayMessage("Your FeedBack was already Locked ", this.Page);
                        //btnSave.Enabled = false;
                        //btnLock.Enabled = false;
                    }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentFeedBackAns.ddlTecher_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }



    protected void txtRemark_TextChanged(object sender, EventArgs e)
    {

    }
}