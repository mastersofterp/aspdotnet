//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : OPRATOR ALLOTMENT                                      
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

public partial class ACADEMIC_BulkOperatorallotment : System.Web.UI.Page
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
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                //ddlSession.Items.Add(new ListItem(Session["sessionname"].ToString(), Session["currentsession"].ToString()));
                //objCommon.FillDropDownList("Please Select", ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "FLOCK = 1", "SESSIONNO DESC");
               
                this.PopulateDropDownList();
            }
        }
    }

    

    private void PopulateDropDownList()
    {
        try
        {

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO desc");
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

          //  objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");


            objCommon.FillDropDownList(ddlTeachertype, "USER_RIGHTS WITH (NOLOCK)", "USERTYPEID", "USERDESC", "USERTYPEID NOT IN (0,2) ", "USERTYPEID");
            ddlSession.SelectedIndex = 0;
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
            Student_Acd objStudent = new Student_Acd();

                int chkstatus = 0;
                string chk = string.Empty;
                string chkstrg = string.Empty;
                foreach (ListViewDataItem item in lvCurrentSubjects.Items)
                {
                    Label lblcourse = item.FindControl("lblCCode") as Label;
                    Label lblcoursename = item.FindControl("lblCourseName") as Label;
                    CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                    // lblcoursename.ForeColor = System.Drawing.Color.Blue;
                    if (chkRegister.Checked == true)
                    {
                        chkstatus = 1;

                        //if (chkover.Checked == true)
                        //{
                        //    chkstatus = 1;
                        //}
                        //else
                        //{
                        //    chkstatus = 0;
                        //}

                        if (chkover.Checked == true)
                        {
                            string chkexists = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR WITH (NOLOCK)", "COUNT(1)", "SR.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                                " and sr.int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));// + " and sr.op_id=" + Convert.ToInt32(ddlTeacher.SelectedValue));

                            if ((chkexists == null && chkexists == string.Empty) || chkexists == "0")
                            {
                                objCommon.DisplayMessage(upddetails, "Above entry does not exist so can not be overwrite", this.Page);
                                return;
                            }
                            objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                            objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                            objStudent.Sem = ddlSemester.SelectedValue;
                            objStudent.CourseNo = Convert.ToInt32(lblcourse.ToolTip.ToString());
                            objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
                            objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);

                            objStudent.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                            objStudent.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                            objStudent.COLLEGE_ID = Convert.ToInt32(ddlColg.SelectedValue);
                            objStudent.INTEXT = Convert.ToInt32(ddlInEx.SelectedValue);

                            if (objSC.UpdateOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                            {
                                objCommon.DisplayMessage(upddetails, "Operator Updated Successfully..", this.Page);
                                DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) and a.int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue)
                                    , "a.courseno");
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    lvDetails.DataSource = ds.Tables[0];
                                    lvDetails.DataBind();
                                    lvDetails.Visible = true;
                                }
                                else
                                {
                                    lvDetails.DataSource = null;
                                    lvDetails.DataBind();
                                }
                            }
                            else
                                objCommon.DisplayMessage("Server Error", this.Page);


                        }
                        else
                        {
                            chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION A WITH (NOLOCK) INNER JOIN ACD_COURSE B WITH (NOLOCK) ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C WITH (NOLOCK) ON(B.SCHEMENO=C.SCHEMENO)", "COUNT(1)", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND a.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                                " AND A.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue)); // ADDED ON 18-03-2020 BY VAISHALI

                            // COMMENTED ON 18-03-2020 BY VAISHALI
                            //if (ddlOprator.SelectedValue == "1")
                            //{
                            //    if (ddlInEx.SelectedValue == "1")
                            //    {
                            //        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                            //            " and sr.int_ext=" + 1 + " and sr.op_status=" + 1);
                            //    }
                            //    else
                            //    {
                            //        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                            //            " and sr.int_ext=" + 2 + " and sr.op_status=" + 1);
                            //    }

                            //}
                            //else
                            //{
                            //    if (ddlInEx.SelectedValue == "1")
                            //    {
                            //        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                            //            " and sr.int_ext=" + 1 + " and sr.op_status=" + 2);
                            //    }
                            //    else
                            //    {
                            //        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                            //            " and sr.int_ext=" + 2 + " and sr.op_status=" + 2);
                            //    }
                            //}

                             //chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                             //   " and sr.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue)); // modified ON 18-03-2020 BY VAISHALI  

                            int count = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION A WITH (NOLOCK) LEFT JOIN ACD_MARKS_ENTRY_OPERTOR B WITH (NOLOCK) ON A.OPID = B.OPID", "COUNT(1)", "A.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.COLLEGE_ID =" + Convert.ToInt32(ddlColg.SelectedValue) + " AND A.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip) +
                                " AND SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + " and A.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue) + " AND (A.OPID IN (SELECT OPID FROM ACD_MARKS_ENTRY_OPERTOR AA) OR B.MARKS IS NOT NULL)")); // modified ON 18-03-2020 BY VAISHALI

                            string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR WITH (NOLOCK)", "COUNT(1)", "SR.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(lblcourse.ToolTip.ToString()) +
                                "  AND SCHEMENO = " + Convert.ToInt32(ddlScheme.SelectedValue) + " AND SEMESTERNO = " + Convert.ToInt32(ddlSemester.SelectedValue) + " and sr.IS_MIDS_ENDS=" + Convert.ToInt32(ddlExamName.SelectedValue) + " and sr.op_id=" + Convert.ToInt32(ddlTeacher.SelectedValue)); // modified ON 18-03-2020 BY VAISHALI
                            
                            if ((chkexist != null && chkexist != string.Empty) && chkexist != "0")
                            {
                                objCommon.DisplayMessage(upddetails, "This Operator is already allotted for above selection for this course " + lblcoursename.Text.ToString(), this.Page);
                                return;
                            }

                            //if ((chk != null && chk != string.Empty) && chk != "0")
                            if(count != 0)
                            {
                                //objCommon.DisplayMessage(upddetails, "Operator already allotted for this course " + lblcoursename.Text.ToString(), this.Page);
                                objCommon.DisplayMessage(upddetails, "Marks Entry has already done for this course " + lblcoursename.Text.ToString() + "!!!!", this.Page);
                                return;
                            }
                            else
                            {
                                objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                                objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                                objStudent.Sem = ddlSemester.SelectedValue;
                                objStudent.CourseNo = Convert.ToInt32(lblcourse.ToolTip);
                                objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
                                objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
                                objStudent.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                                objStudent.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                                objStudent.COLLEGE_ID = Convert.ToInt32(ddlColg.SelectedValue);
                                objStudent.INTEXT = Convert.ToInt32(ddlInEx.SelectedValue);

                                if (objSC.InsertOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayMessage(upddetails, "Operator Allotted Successfully..", this.Page);

                                    //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) and a.int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue)
                                    //, "a.courseno");  // COMMENTED ON 18-03-2020 BY VAISHALI

                                    DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A WITH (NOLOCK) INNER JOIN ACD_COURSE B WITH (NOLOCK) ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C WITH (NOLOCK) ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E WITH (NOLOCK) ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F WITH (NOLOCK) ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G WITH (NOLOCK) ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS,CASE WHEN IS_MIDS_ENDS = 1 THEN 'MID SEM' WHEN IS_MIDS_ENDS = 2 THEN 'END SEM' END EXAMNAME", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0)  AND (A.IS_MIDS_ENDS = " + Convert.ToInt32(ddlExamName.SelectedValue) + " OR  " + Convert.ToInt32(ddlExamName.SelectedValue) + " = 0)"
                                    , "a.courseno");  // MODIFIED ON 18-03-2020 BY VAISHALI
                                   
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        lvDetails.DataSource = ds.Tables[0];
                                        lvDetails.DataBind();
                                        lvDetails.Visible = true;
                                    }
                                    else
                                    {
                                        lvDetails.DataSource = null;
                                        lvDetails.DataBind();
                                    } //return;
                                }
                                else if (objSC.InsertOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                                {
                                    objCommon.DisplayMessage(upddetails, "Operator Updated Successfully..", this.Page);

                                    DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A WITH (NOLOCK) INNER JOIN ACD_COURSE B WITH (NOLOCK) ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C WITH (NOLOCK) ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E WITH (NOLOCK) ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F WITH (NOLOCK) ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G WITH (NOLOCK) ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS,CASE WHEN IS_MIDS_ENDS = 1 THEN 'MID SEM' WHEN IS_MIDS_ENDS = 2 THEN 'END SEM' END EXAMNAME", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0)  AND (A.IS_MIDS_ENDS = " + Convert.ToInt32(ddlExamName.SelectedValue) + " OR  " + Convert.ToInt32(ddlExamName.SelectedValue) + " = 0)"
                                    , "A.COURSENO");  // MODIFIED ON 18-03-2020 BY VAISHALI

                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        lvDetails.DataSource = ds.Tables[0];
                                        lvDetails.DataBind();
                                        lvDetails.Visible = true;
                                    }
                                    else
                                    {
                                        lvDetails.DataSource = null;
                                        lvDetails.DataBind();
                                    } //return;
                                }
                                else
                                    objCommon.DisplayMessage("Server Error", this.Page);
                            }
                        }
                    }

                }
                    if(chkstatus==0)
                    {
                       objCommon.DisplayMessage("Please select atleast one course", this.Page);
                        return;
                    }
                
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_teacherallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
                ddlExamName.SelectedIndex = 0;
                //ddlBranch.Focus(); DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    lvDetails.DataSource = ds.Tables[0];
                //    lvDetails.DataBind();
                //}
                //else
                //{
                //    lvDetails.DataSource = null;
                //    lvDetails.DataBind();
                //}
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
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
            ddlExamName.SelectedIndex = 0;
            if (ddlBranch.SelectedIndex > 0)
            {

                 // CHANGES BY SUMIT-- 02112019
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");


               // objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                ddlScheme.Focus(); 

                //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    lvDetails.DataSource = ds.Tables[0];
                //    lvDetails.DataBind();
                //}
                //else
                //{
                //    lvDetails.DataSource = null;
                //    lvDetails.DataBind();
                //}
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
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
            ddlExamName.SelectedIndex = 0;
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_SCHEME M WITH (NOLOCK) ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S WITH (NOLOCK) ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
                ddlSubjectType.Focus();

                // added on 18-03-2020 by Vaishali
             //   DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO,sr.SEMESTERno as semester", "SR.COURSENAME as  COURSE_NAME,C.CCODE,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = 1 AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");

             //   if (dsCourse.Tables[0].Rows.Count > 0 && dsCourse != null)
             //   {
             //       lvCurrentSubjects.DataSource = dsCourse;
             //       lvCurrentSubjects.DataBind();
             //       lvCurrentSubjects.Visible = true;
             //       pnlcour.Visible = true;
             //   }
             //   else
             //   {
             //       lvCurrentSubjects.DataSource = null;
             //       lvCurrentSubjects.DataBind();
             //       lvCurrentSubjects.Visible = false;
             //       pnlcour.Visible = false;
             //   }

             //   //modified on 19-03-2020 by Vaishali
             //   DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS,CASE WHEN IS_MIDS_ENDS = 1 THEN 'MID SEM' WHEN IS_MIDS_ENDS = 2 THEN 'END SEM' END EXAMNAME", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (A.IS_MIDS_ENDS = " + Convert.ToInt32(ddlExamName.SelectedValue) + " OR " + Convert.ToInt32(ddlExamName.SelectedValue) + "= 0) "//AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)"
             //, string.Empty);
             //   if (ds.Tables[0].Rows.Count > 0)
             //   {
             //       lvDetails.DataSource = ds.Tables[0];
             //       lvDetails.DataBind();
             //   }
             //   else
             //   {
             //       lvDetails.DataSource = null;
             //       lvDetails.DataBind();
             //   }
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSemester.SelectedIndex = 0;
            }


            ddlSection.SelectedIndex = 0;
            ddlTeacher.SelectedIndex = 0;

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

    protected void ddlSubjectType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
        if (ddlSemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "COURSENO", "(CCODE + ' - ' + COURSE_NAME) COURSE_NAME", "OFFERED = 1 AND SCHEMENO = " + ddlScheme.SelectedValue + " AND SEMESTERNO = " + ddlSemester.SelectedValue + " AND SUBID = " + ddlSubjectType.SelectedValue, "CCODE");
            DataSet ds = objCommon.FillDropDown("ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO,SR.SEMESTERNO AS SEMESTER", "SR.COURSENAME AS COURSE_NAME,C.CCODE,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            lvCurrentSubjects.DataSource = ds;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;
            pnlcour.Visible = true;
        }
        else
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            pnlcour.Visible = false;
        }

        
        ddlSection.SelectedIndex = 0;
       // ddlCourse.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
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

  
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlExamName.SelectedIndex = 0;
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
                ddlSemester.Focus(); 
                //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    lvDetails.DataSource = ds.Tables[0];
                //    lvDetails.DataBind();
                //}
                //else
                //{
                //    lvDetails.DataSource = null;
                //    lvDetails.DataBind();
                //}
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

    //protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvDetails.DataSource = ds.Tables[0];
    //            lvDetails.DataBind();
    //        }
    //        else
    //        {
    //            lvDetails.DataSource = null;
    //            lvDetails.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //        {
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }
    //}
    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE B WITH (NOLOCK) ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue), "A.DEGREENAME");
            ddlExamName.SelectedIndex = 0;
          ////objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
          
            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lvDetails.DataSource = ds.Tables[0];
            //    lvDetails.DataBind();
               
            //}
            //else
            //{
            //    lvDetails.DataSource = null;
            //    lvDetails.DataBind();
            //}
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
    protected void ddlTeachertype_SelectedIndexChanged(object sender, EventArgs e)
    {
         ///objCommon.FillDropDownList(ddlTeacher, "USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO", "UA_NAME +' - '+UA_FULLNAME as UA_FULLNAME ", "UA_NO IS NOT NULL  AND UA_TYPE=" + ddlTeachertype.SelectedValue, "UA_NAME");
        objCommon.FillDropDownList(ddlTeacher, "USER_ACC A WITH (NOLOCK) INNER JOIN USER_RIGHTS R WITH (NOLOCK) ON A.UA_TYPE = R.USERTYPEID", "A.UA_NO", "UA_FULLNAME", "UA_NO IS NOT NULL  AND UA_TYPE=" + ddlTeachertype.SelectedValue, "A.UA_NAME"); 
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("MarkEntryAllocationReport", "MarkEntryAllocationReport.rpt",1);
    }

    private void ShowReport(string reportTitle, string rptFileName,int type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if(type == 1)
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlColg.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + ddlScheme.SelectedValue.ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue.ToString();
            else if(type == 2)
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_ID=" + ddlColg.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMTYPE=" + ddlExamName.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.upddetails, this.upddetails.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RandomStatusReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //added on 19-03-2020 by Vaishali
        if (ddlExamName.SelectedIndex > 0)
        {
            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS,CASE WHEN IS_MIDS_ENDS = 1 THEN 'MID SEM' WHEN IS_MIDS_ENDS = 2 THEN 'END SEM' END EXAMNAME", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (A.IS_MIDS_ENDS = " + ddlExamName.SelectedValue + " OR A.IS_MIDS_ENDS = 0) "//AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)"
            //, string.Empty);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    lvDetails.DataSource = ds.Tables[0];
            //    lvDetails.DataBind();
            //}
            //else
            //{
            //    lvDetails.DataSource = null;
            //    lvDetails.DataBind();
            //}

            DataSet dsCourse = objCommon.FillDropDown("ACD_COURSE C WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO,SR.SEMESTERNO AS SEMESTER", "SR.COURSENAME AS COURSE_NAME,C.CCODE,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS ", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = 1 AND ISNULL(REGISTERED,0) = 1 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(CANCEL,0) = 0  AND CASE WHEN " + ddlExamName.SelectedValue + " = 1 THEN 1 WHEN " + ddlExamName.SelectedValue + " = 2 THEN ISNULL(EXAM_REGISTERED,0) END = 1 AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");

            if (ddlScheme.SelectedIndex > 0 && ddlSemester.SelectedIndex > 0)
            {
                if (dsCourse.Tables[0].Rows.Count > 0 && dsCourse != null)
                {
                    lvCurrentSubjects.DataSource = dsCourse;
                    lvCurrentSubjects.DataBind();
                    lvCurrentSubjects.Visible = true;
                    pnlcour.Visible = true;
                }
                else
                {
                    lvCurrentSubjects.DataSource = null;
                    lvCurrentSubjects.DataBind();
                    lvCurrentSubjects.Visible = false;
                    pnlcour.Visible = false;
                    objCommon.DisplayMessage(this.upddetails, "No Courses Found for the selection !!!!", this.Page);
                    return;
                }
            }

            //modified on 19-03-2020 by Vaishali
            DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A WITH (NOLOCK) INNER JOIN ACD_COURSE B WITH (NOLOCK) ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C WITH (NOLOCK) ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D WITH (NOLOCK) ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E WITH (NOLOCK) ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F WITH (NOLOCK) ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G WITH (NOLOCK) ON(G.COLLEGE_ID=A.COLLEGE_ID)", "G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS,CASE WHEN IS_MIDS_ENDS = 1 THEN 'MID SEM' WHEN IS_MIDS_ENDS = 2 THEN 'END SEM' END EXAMNAME", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (A.IS_MIDS_ENDS = " + Convert.ToInt32(ddlExamName.SelectedValue) + " OR " + Convert.ToInt32(ddlExamName.SelectedValue) + "= 0) "//AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)"
         , string.Empty);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvDetails.DataSource = ds.Tables[0];
                lvDetails.DataBind();
            }
            else
            {
                lvDetails.DataSource = null;
                lvDetails.DataBind();
            }
        }
        else
        {
            lvDetails.DataSource = null;
            lvDetails.DataBind();
        }
    }

    protected void btnPendingReport_Click(object sender, EventArgs e)
    {
        ShowReport("PendingMarkEntryAllocationReport", "rptPendingOperatorAllocationReport.rpt",2);
    }
}
