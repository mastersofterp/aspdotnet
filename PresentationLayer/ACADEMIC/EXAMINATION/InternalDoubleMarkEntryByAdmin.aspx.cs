//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : InternalDoubleMarkEntry                                     
// CREATION DATE : 15-OCT-2011
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

public partial class ACADEMIC_InternalDoubleMarkEntryByAdmin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objmec = new MarksEntryController();
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //Binddata();
                ViewState["Lock"] = 0; this.PopulateDropDownList();
            }
        } divMsg.InnerHtml = string.Empty;
        ViewState["ipaddress"] = Request.ServerVariables["REMOTE_ADDR"];
    }
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
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

    protected void btnReportS_Click(object sender, EventArgs e)
    {
        ImageButton imgbutton = sender as ImageButton;
        int opid = Convert.ToInt32(imgbutton.AlternateText);
        int courseno = Convert.ToInt32(imgbutton.CommandArgument);
        ShowReport("Internal_Mark_entry_Report", "rptInternalMarkEntry.rpt", courseno, opid);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //divcourses.Visible = true;
        //Binddata();
        //btnClear.Visible = false;

    }

    int i = 0;
    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks1 = e.Row.FindControl("txtTotMarks") as TextBox;
            TextBox txtMarks2 = e.Row.FindControl("txtTAMarks") as TextBox;
            TextBox txtMarks3 = e.Row.FindControl("txtESPRMarks") as TextBox;
            TextBox txtMarks4 = e.Row.FindControl("txts4mark") as TextBox;
            TextBox txtMarks5 = e.Row.FindControl("txts5mark") as TextBox; 
            TextBox txtMarks6 = e.Row.FindControl("txts6mark") as TextBox;
            TextBox txtMarks7 = e.Row.FindControl("txts7mark") as TextBox;
            TextBox txtMarks8 = e.Row.FindControl("txts8mark") as TextBox;
            TextBox txtMarks9 = e.Row.FindControl("txts9mark") as TextBox;
            TextBox txtMarks10 = e.Row.FindControl("txts10mark") as TextBox;
            // string ii=  txtMarks1.Text.ToString();
            HiddenField hdnlock = e.Row.FindControl("hdnlock") as HiddenField;
            if (hdnlock.Value == "1")
            {
                txtMarks1.Enabled = false;
                txtMarks2.Enabled = false;
                txtMarks3.Enabled = false;
                txtMarks4.Enabled = false;
                txtMarks5.Enabled = false;
                txtMarks6.Enabled = false;
                txtMarks7.Enabled = false;
                txtMarks8.Enabled = false;
                txtMarks9.Enabled = false;
                txtMarks10.Enabled = false;
            }
            else
            {
                txtMarks1.Enabled = true;

                txtMarks2.Enabled = true;

                txtMarks3.Enabled = true;

                txtMarks4.Enabled = true;

                txtMarks5.Enabled = true; 
                txtMarks6.Enabled = true;
                txtMarks7.Enabled = true;
                txtMarks8.Enabled = true;
                txtMarks9.Enabled = false;
                txtMarks10.Enabled = true;

            }
        }
    }
  
    private void ShowMark()
    {
        DataSet dsallo = objCommon.FillDropDown("ACD_MARKS_ENTRY_OPERTOR A INNER JOIN ACD_EXAM_MARKENTRY_ALLOCATION B ON (A.OPID=B.OPID)", "A.*", "ENTRYDATE", "A.OPID=" + Convert.ToInt32(ViewState["opid"]) + " AND B.INT_EXT=1", string.Empty);

        if (dsallo.Tables[0].Rows.Count > 0)
        {
            btnLock.Visible = true;
            // btnReport.Visible = true;
            DataSet dsinfo = objmec.GetStudentmarkDetails(Convert.ToInt32(ViewState["opid"]));
            gvStudent.DataSource = dsinfo.Tables[0];
            gvStudent.DataBind();
        }
        else
        {
            btnLock.Visible = false;
            // btnReport.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet opid = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION", "opid", "move_status", "college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=1", string.Empty);
            for (int ss = 0; ss < opid.Tables[0].Rows.Count; ss++)
            {
                string studids = string.Empty;
                string examname = string.Empty;
                string marks = string.Empty;
                //string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(ViewState["opid"]));
                //if (chkexist != string.Empty && chkexist != "0")
                //{
                //    objCommon.DisplayMessage(updinternal,"Record already Saved and Locked...", this.Page);
                //    return;
                //}
                int k = 0;
                int columns = 0;
                for (int i = 3; i < gvStudent.Columns.Count; i++)
                {
                    if (gvStudent.Columns[i].Visible == true)
                    {
                        columns++;
                    }
                }
                int count = columns * (gvStudent.Rows.Count);
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    Label lblidnos = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    for (int j = 3; j < gvStudent.Columns.Count; j++)
                    {
                        if (gvStudent.Columns[j].Visible == true)
                        {
                            TableCell tc = gvStudent.Rows[i].Cells[j];
                            TextBox myControl = null;
                            foreach (Control c in tc.Controls)
                            {
                                if (c is TextBox)
                                {
                                    myControl = (TextBox)c;
                                    break;
                                }
                            }
                            string value = myControl.Text.Trim();
                            if (value != "" || value != string.Empty)
                            {
                                marks += value + ',';
                            }
                            else
                            {
                                marks += "-100" + ',';
                                k++;
                            }
                        }
                    }
                    studids += lblidnos.ToolTip.ToString() + ',';
                }
                for (int j = 3; j < gvStudent.Columns.Count; j++)
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        examname += gvStudent.Columns[j].FooterText + "MARK" + ',';
                    }
                }
                if (marks != "")
                {
                    if (k == count)
                    {
                        objCommon.DisplayMessage(updinternal,"Please Enter marks of atleast one student", this.Page);
                        return;
                    }
                    else
                    {
                        if (marks.Substring(marks.Length - 1) == ",")
                            marks = marks.Substring(0, marks.Length - 1);
                    }
                }
                if (studids != "")
                {
                    if (studids.Substring(studids.Length - 1) == ",")
                        studids = studids.Substring(0, studids.Length - 1);
                }
                if (examname != "")
                {
                    if (examname.Substring(examname.Length - 1) == ",")
                        examname = examname.Substring(0, examname.Length - 1);
                }

                int cs = objmec.MarkEntrybyOperator(Convert.ToInt32(opid.Tables[0].Rows[ss]["opid"].ToString()), studids.ToString().TrimEnd(','), examname.ToString().TrimEnd(','), marks.ToString().TrimEnd(','));
                if (cs == 1)
                {
                    if (ViewState["Lock"].ToString() != "1")
                    {
                        objCommon.DisplayMessage(updinternal,"Record saved successfully", this.Page);
                    }
                    //ShowMark();
                    //return;
                }
                else
                {
                    objCommon.DisplayMessage(updinternal,"Error occured while submitting data.Please contact MIS!!.", this.Page);
                    return;
                }
            } ShowMark();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet opid = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION", "opid", "move_status", "college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=1", string.Empty);
            for (int ss = 0; ss < opid.Tables[0].Rows.Count; ss++)
            {
                //string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION", "Count(1)", "lock=1 and opid=" + Convert.ToInt32(opid.Tables[0].Rows[ss]["opid"].ToString()));
                //if (chkexist != string.Empty && chkexist != "0")
                //{
                //    objCommon.DisplayMessage(updinternal,"Already locked...", this.Page);
                //    return;
                //}
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    for (int j = 3; j < gvStudent.Columns.Count; j++)
                    {
                        if (gvStudent.Columns[j].Visible == true)
                        {
                            TableCell tc = gvStudent.Rows[i].Cells[j];
                            TextBox myControl = null;
                            foreach (Control c in tc.Controls)
                            {
                                if (c is TextBox)
                                {
                                    myControl = (TextBox)c;
                                    break;
                                }
                            }
                            string value = myControl.Text.Trim();
                            if (value == "" || value == string.Empty)
                            {
                                objCommon.DisplayMessage(updinternal,"Mark entry could not be locked...!! Please enter marks of all students", this.Page);
                                return;
                            }

                        }
                    }
                }
                //ViewState["Lock"] = 1;
                //btnSave_Click(sender, e);
                int chklock = objmec.LockEntrybyOperator(Convert.ToInt32(opid.Tables[0].Rows[ss]["opid"].ToString()));
                if (chklock == 2)
                {
                    objCommon.DisplayMessage(updinternal,"Record locked successfully", this.Page);

                    objmec.InsertMarkEntryLock(Convert.ToInt32(opid.Tables[0].Rows[ss]["opid"].ToString()), Convert.ToInt32(Session["userno"]), ViewState["ipaddress"].ToString());
                    // return;
                }
                else
                {
                    objCommon.DisplayMessage("Error...", this.Page);
                    return;
                }
            } this.ShowMark();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE CD ON (D.DEGREENO = CD.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.DEGREENO=D.DEGREENO)", "DISTINCT D.DEGREENO", "D.DEGREENAME", "CD.COLLEGE_ID=" + ddlColg.SelectedValue + "AND CDB.UGPGOT IN (" + Session["ua_section"] + ")", "D.DEGREENO");
            ddlDegree.Focus();
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

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");

            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;
            }

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
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;
            }

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

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSemester.SelectedIndex = 0;
            }



            //ddlSubjectType.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;


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

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                ddlCourse.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }



            ddlCourse.SelectedIndex = 0;

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
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0

            }
            else
            {
                ddlSemester.Items.Clear();
                ddlScheme.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowReport(string reportTitle, string rptFileName, int Courseno, int opid)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_opid=" + opid + ",@P_COURSENO=" + Courseno;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "StudentReport1.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {

            //Show the Student List with Exams that are ON
            //=============================================
            //LinkButton lnk = sender as LinkButton;
            title.InnerText = "Enter Marks for following Students of" + "   " + ddlCourse.SelectedItem.ToString();

            DataSet opid = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION", "opid", "move_status", "college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and sessionno=" + Convert.ToInt32(ddlSession.SelectedValue) + "and courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and int_ext=1", string.Empty);
            if (opid.Tables[0].Rows.Count > 0)
            {
                btnSave.Enabled = true;

                btnLock.Enabled = true;
                for (int ss = 0; ss < opid.Tables[0].Rows.Count; ss++)
                {

                    //string[] sec_batch = lnk.CommandArgument.ToString().Split('+');
                    //string s1 = sec_batch[0].ToString();
                    //ViewState["courseno"] = lnk.ToolTip;
                    //ViewState["colg"] = s1;
                    ViewState["opid"] = opid.Tables[0].Rows[ss]["opid"].ToString();
                    int j = 2;
                    DataSet dsk = objCommon.FillDropDown("ACD_STUDENT_RESULT a inner join acd_student b on(a.idno=b.idno) inner join acd_scheme c on (a.schemeno=c.schemeno) inner join acd_course d on(d.courseno=a.courseno)", "distinct a.idno,a.regno,*", "b.studname,'" + ViewState["lock"] + "' as lock", "a.courseno=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND B.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.idno");
                    if (dsk.Tables[0].Rows.Count > 0)
                    {
                        pnlStudGrid.Visible = true;
                        btnSave.Visible = true;
                        btnClear.Visible = true;
                        string patern = dsk.Tables[0].Rows[0]["PATTERNNO"].ToString();
                        //ViewState["pattern"] = patern;
                        //DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO,*,examname", "fldname", "patternno=" + Convert.ToInt32(ViewState["pattern"]) + "  AND EXAMNAME!='' AND FLDNAME!='EXTERMARK'", "examno");
                        //DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                        //for (j = 3; j < gvStudent.Columns.Count; j++)
                        //{
                        //    while (dtrExams.Read())
                        //    {
                        //        string s = dtrExams["FLDNAME"].ToString();
                        //        int examrow = (Convert.ToInt32(dtrExams["SRNO"].ToString()) - 1);
                        //        if (Convert.ToInt32(dsk.Tables[0].Rows[0][s + "MAX"]) > 0)
                        //        {
                        //            gvStudent.Columns[j].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString() + "[Max : " + dsk.Tables[0].Rows[0][s + "MAX"].ToString() + "]";
                        //            gvStudent.Columns[j].FooterText = s;
                        //            gvStudent.Columns[j].Visible = true;
                        //            break;
                        //        }
                        //    }
                        //}
                        DataSet dspattern = objCommon.FillDropDown("ACD_EXAM_NAME", "ROW_NUMBER() OVER(ORDER BY EXAMNO) SRNO", "EXAMNO, EXAMNAME, FLDNAME", "patternno=" + patern + "  AND EXAMNAME!='' AND FLDNAME!='EXTERMARK'", "examno");
                        DataTableReader dtrExams = dspattern.Tables[0].CreateDataReader();
                        //for (j = 3; j < gvStudent.Columns.Count; j++)
                        //{
                        while (dtrExams.Read())
                        {
                            string s = dtrExams["FLDNAME"].ToString();
                            int examrow = (Convert.ToInt32(dtrExams["SRNO"].ToString()) - 1);
                            int col = Convert.ToInt16(s.Substring(1, 1));
                            if (Convert.ToInt32(dsk.Tables[0].Rows[0][s + "MAX"]) > 0)
                            {
                                gvStudent.Columns[2 + col].HeaderText = dspattern.Tables[0].Rows[examrow]["examname"].ToString() + "[Max : " + dsk.Tables[0].Rows[0][s + "MAX"].ToString() + "]";
                                gvStudent.Columns[2 + col].FooterText = s;
                                gvStudent.Columns[2 + col].Visible = true;
                                //break;
                            }
                        }
                        gvStudent.DataSource = dsk.Tables[0];
                        gvStudent.DataBind();


                        ShowMark();

                    }
                }
            }
            else
            {
                pnlStudGrid.Visible = false;
                btnSave.Enabled = false;

                btnLock.Enabled = false;
                //divcourses.Visible = true ;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
}
