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
using System.Web;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Operatorallotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student_Acd objStudent = new Student_Acd();

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

                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

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

       
            objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_SESSION_MASTER SM ON (SR.SESSIONNO=SM.SESSIONNO)", "DISTINCT (SM.SESSIONNO)", "SM.SESSION_NAME", "SM.SESSIONNO > 0", " SM.SESSIONNO DESC");
           
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "C.COLLEGE_ID");
           
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlTeachertype, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID NOT IN (0,2) ", "USERTYPEID");
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

    private void FillTeacher()
    {
        try
        {
        objCommon.FillDropDownList(ddlTeacher, "ACD_COURSE_TEACHER CT INNER JOIN USER_ACC UA ON (CT.UA_NO=UA.UA_NO)", "CT.UA_NO", "UA.UA_FULLNAME", "CT.SESSIONNO = " + ddlSession.SelectedValue + " AND CT.COURSENO = " + ddlCourse.SelectedValue + " AND CT.SEMESTERNO =" + ddlSemester.SelectedValue + " AND CT.SECTIONNO =" + ddlSection.SelectedValue + " AND CT.SCHEMENO =" + ddlScheme.SelectedValue + " AND CT.TH_PR =" + ddlSubjectType.SelectedValue, "UA.UA_FULLNAME");
        ddlTeacher.Focus();
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

   
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //StudentController objSC = new StudentController();
            //Student_Acd objStudent = new Student_Acd();

                string chk = string.Empty;
                string chkstrg = string.Empty;    
                chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO)", "COUNT(1)", "A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND A.COLLEGE_ID=" +Convert.ToInt32(ddlColg.SelectedValue) + "AND a.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND A.OP_STATUS=" + Convert.ToInt32(ddlOprator.SelectedValue) + " AND A.INT_EXT=" + Convert.ToInt32(ddlInEx.SelectedValue) );

                if (ddlOprator.SelectedValue == "1")
                {
                    if (ddlInEx.SelectedValue == "1")
                    {
                        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" + 1 +" and sr.op_status="+1);
                    }
                    else
                    {
                        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" + 2 + " and sr.op_status=" + 1);
                    }

                }
                else
                {
                    if (ddlInEx.SelectedValue == "1")
                    {
                        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" + 1 + " and sr.op_status=" + 2);
                    }
                    else
                    {
                        chk = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "op_id", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" + 2 + " and sr.op_status=" + 2);
                    }
                }
           string chkexist = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "count(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" +Convert.ToInt32(ddlInEx.SelectedValue)+" and sr.op_id="+Convert.ToInt32(ddlTeacher.SelectedValue));
           if ((chkexist != null && chkexist != string.Empty) && chkexist != "0")
           {
               objCommon.DisplayMessage(upddetails,"Operator already allotted for above selection", this.Page);
               return;
           } 
                //if (ddlInEx.SelectedValue == "1")
                //{
                //    chkstrg = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "and (sr.[external]=1 or sr.internal=1) ");
                   
                //    if (ddlOprator.SelectedValue == "1")
                //    {
                //        string chkfac = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " and sr.i_op2=" + Convert.ToInt32(ddlTeacher.SelectedValue) + " and  sr.internal=1 ");
                //        if (chkfac != string.Empty && chkfac != "0")
                //        {
                //            objCommon.DisplayMessage( "This Teacher alloted for Operator2", this.Page);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        string chkfac = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " and sr.i_op1=" + Convert.ToInt32(ddlTeacher.SelectedValue) + " and  sr.internal=1 ");
                //        if (chkfac != string.Empty && chkfac != "0")
                //        {
                //            objCommon.DisplayMessage( "This Teacher alloted for Operator1", this.Page);
                //            return;
                //        }
                //    }
                //}
                //else
                //{
                //    chkstrg = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + "and (sr.[external]=1 or sr.internal=1) ");
                   
                //    if (ddlOprator.SelectedValue == "1")
                //    {
                //        string chkfac = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " and sr.E_op2=" + Convert.ToInt32(ddlTeacher.SelectedValue) + " and  sr.[EXTERNAL]=1 ");
                //        if (chkfac != string.Empty && chkfac != "0")
                //        {
                //            objCommon.DisplayMessage("This Teacher alloted for Operator2", this.Page);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        string chkfac = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "COUNT(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "and SR.SCHEMENO = " + ddlScheme.SelectedValue + "  AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "AND SR.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND SR.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " and sr.E_op1=" + Convert.ToInt32(ddlTeacher.SelectedValue) + " and  sr.[EXTERNAL]=1 ");
                //        if (chkfac != string.Empty && chkfac != "0")
                //        {
                //            objCommon.DisplayMessage( "This Teacher alloted for Operator1", this.Page);
                //            return;
                //        }
                //    }
                //}
            

                 if (chkover.Checked == true )
                {
                    string chkexists = objCommon.LookUp("ACD_EXAM_MARKENTRY_ALLOCATION SR", "count(1)", "sr.college_id=" + Convert.ToInt32(ddlColg.SelectedValue) + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " and sr.int_ext=" + Convert.ToInt32(ddlInEx.SelectedValue));// + " and sr.op_id=" + Convert.ToInt32(ddlTeacher.SelectedValue));
          
                    if ((chkexists == null && chkexists == string.Empty) || chkexists == "0")
                    {
                        objCommon.DisplayMessage(upddetails,"Above entry does not exist so can not be overwrite", this.Page);
                        return;
                    } 
                    objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                    objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                    objStudent.Sem = ddlSemester.SelectedValue;
                    objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                    objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
                    objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
                
                    objStudent.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                    objStudent.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                    objStudent.COLLEGE_ID = Convert.ToInt32(ddlColg.SelectedValue);
                    objStudent.INTEXT = Convert.ToInt32(ddlInEx.SelectedValue);

                    if (objSC.UpdateOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(upddetails, "Operator Updated Successfully..", this.Page);
                        DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "A.OPID,G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'Operator 1' ELSE 'Operator 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
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
                       // btnClear_Click(sender, e);
                      //  ClearControls();
                        return;
                    }
                    else
                        objCommon.DisplayMessage( "Server Error", this.Page);


                }

                 if ((chk != null && chk != string.Empty) && chk != "0")
                 {
                     objCommon.DisplayMessage(upddetails,"Operator already allotted", this.Page);
                     return;
                 }
                 else
                 {
                     objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                     objStudent.SchemeNo = Convert.ToInt32(ddlScheme.SelectedValue);
                     objStudent.Sem = ddlSemester.SelectedValue;
                     objStudent.CourseNo = Convert.ToInt32(ddlCourse.SelectedValue);
                     objStudent.UA_No = Convert.ToInt32(ddlTeacher.SelectedValue);
                     objStudent.Th_Pr = Convert.ToInt32(ddlSubjectType.SelectedValue);
                     objStudent.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
                     objStudent.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
                     objStudent.COLLEGE_ID = Convert.ToInt32(ddlColg.SelectedValue);
                     objStudent.INTEXT = Convert.ToInt32(ddlInEx.SelectedValue);

                     //if ((chkstrg != null || chkstrg != string.Empty) && chkstrg != "0")
                     //{
                     //    if (objSC.UpdateOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue), 2) == Convert.ToInt32(CustomStatus.RecordUpdated))
                     //    {
                     //        objCommon.DisplayMessage( "Operator Updated Sucessfully..", this.Page);
                     //        DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COLLEGE_MASTER B ON(A.COLLEGE_ID=B.COLLEGE_ID) INNER JOIN ACD_DEGREE C ON(A.DEGREENO=C.DEGREENO) INNER JOIN ACD_BRANCH D ON(A.BRANCHNO=D.BRANCHNO) INNER JOIN ACD_SCHEME E ON(A.SCHEMENO=E.SCHEMENO) INNER JOIN ACD_SEMESTER F ON(A.SEMESTERNO=F.SEMESTERNO) INNER JOIN ACD_COURSE G ON(A.COURSENO=G.COURSENO) LEFT OUTER JOIN USER_ACC H ON(A.I_OP1=H.UA_NO) LEFT OUTER JOIN USER_ACC I ON(A.I_OP2=I.UA_NO) LEFT OUTER JOIN USER_ACC J ON(A.E_OP1=J.UA_NO) LEFT OUTER JOIN USER_ACC K ON(A.E_OP2=K.UA_NO)", "B.COLLEGE_NAME,C.DEGREENAME,D.LONGNAME,E.SCHEMENAME,F.SEMESTERNAME,G.COURSE_NAME", "H.UA_FULLNAME AS INERTNALOP_1,I.UA_FULLNAME AS INERTNALOP_2, J.UA_FULLNAME AS EXTERNALOP_1,K.UA_FULLNAME AS EXTERNALOP_2", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (A.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (A.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (A.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (A.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (A.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
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
                     //        return;
                     //    }
                     //    else
                     //        objCommon.DisplayMessage( "Server Error", this.Page);

                     // }
                     //else
                     //{


                     if (objSC.InsertOperatorAllot(objStudent, Convert.ToInt32(ddlOprator.SelectedValue)) == Convert.ToInt32(CustomStatus.RecordSaved))
                     {
                         objCommon.DisplayMessage(upddetails,"Operator Allotted Successfully..", this.Page);
                         DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "A.OPID,G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);

                        // DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "A.OPID,G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
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

                     //}

                     // }
                 }

                 //ClearControls();
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
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue+ " and B.College_id="+ddlColg .SelectedValue, "A.LONGNAME");

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
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");

                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
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
            if (ddlSemester.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue, "C.SUBID");
                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO >0", "C.SUBID");
                ddlSubjectType.Focus();

                DataSet ds = objCommon.FillDropDown("ACD_EXAM_MARKENTRY_ALLOCATION A INNER JOIN ACD_COURSE B ON(A.COURSENO=B.COURSENO) INNER JOIN ACD_SCHEME C ON(B.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_DEGREE D ON(C.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH E ON(E.BRANCHNO=C.BRANCHNO) INNER JOIN USER_ACC F ON(F.UA_NO=A.OP_ID) INNER JOIN ACD_COLLEGE_MASTER G ON(G.COLLEGE_ID=A.COLLEGE_ID)", "A.OPID,G.COLLEGE_NAME,D.DEGREENAME,E.LONGNAME,C.SCHEMENAME,B.COURSE_NAME,B.SEMESTERNO,F.UA_FULLNAME", "(CASE WHEN OP_STATUS=1 THEN 'OPERATOR 1' ELSE 'OPERATOR 2' END)OPRTR, (CASE WHEN A.INT_EXT=1 THEN 'INTERNAL' ELSE 'EXTERNAL' END)STATUS", "(A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " OR " + Convert.ToInt32(ddlSession.SelectedValue) + "=0) AND (A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue) + " OR " + Convert.ToInt32(ddlColg.SelectedValue) + "=0) AND (C.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " OR " + Convert.ToInt32(ddlDegree.SelectedValue) + "=0) AND (C.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " OR " + Convert.ToInt32(ddlBranch.SelectedValue) + "=0) AND (C.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " OR " + Convert.ToInt32(ddlScheme.SelectedValue) + "=0)  AND (B.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " OR " + Convert.ToInt32(ddlSemester.SelectedValue) + "=0) AND (B.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " OR " + Convert.ToInt32(ddlCourse.SelectedValue) + "=0)", string.Empty);
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
                ddlSubjectType.Items.Clear();
                ddlSemester.SelectedIndex = 0;
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

        
        ddlSection.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
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

    protected void ddlCourse_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSection, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_STUDENT ST ON SR.IDNO = ST.IDNo INNER JOIN  ACD_SECTION S ON (ST.SECTIONNO = S.SECTIONNO)", "DISTINCT S.SECTIONNO", "S.SECTIONNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.COURSENO = " + ddlCourse.SelectedValue + " AND SR.PREV_STATUS = 0", "S.SECTIONNO");

                string cntmsg = objCommon.LookUp("ACD_STUDENT_RESULT SR inner join acd_student s on(sr.idno=s.idno)", "DISTINCT COUNT(sr.IDNO)", "SR.SCHEMENO = " + ddlScheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlSemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND sr.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + "and s.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue));
                if (cntmsg != string.Empty || cntmsg != null)
                {
                    lblmsg.Text = "Total students registered for this course are " + cntmsg;
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblmsg.Text = "No record found for this selection";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblmsg.Text = string.Empty;
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

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue, "SR.SEMESTERNO");
                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");//AND SR.PREV_STATUS = 0
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
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlColg.SelectedValue), "A.DEGREENAME");

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C, ACD_COLLEGE_DEGREE_BRANCH CD", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND CD.DEGREENO=D.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENO");
           
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
       // objCommon.FillDropDownList(ddlTeacher, "USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "UA_NO", "UA_NAME ", "UA_NO IS NOT NULL  AND UA_TYPE=" + ddlTeachertype.SelectedValue, "UA_NAME"); //+' - '+UA_FULLNAME as UA_FULLNAME
        objCommon.FillDropDownList(ddlTeacher, "USER_ACC A INNER JOIN USER_RIGHTS R ON A.UA_TYPE = R.USERTYPEID", "A.UA_NO", "UA_FULLNAME", "UA_NO IS NOT NULL  AND UA_TYPE=" + ddlTeachertype.SelectedValue, "A.UA_NAME");    
           
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("MarkEntryAllocationReport", "MarkEntryAllocationReport.rpt");

    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGEID=" + ddlColg.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + ddlScheme.SelectedValue.ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue.ToString();
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





    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            //sender = item.FindControl("cbRow") as CheckBox;
            int OPID = int.Parse(btnEdit.ToolTip);
            ViewState["OPID"] = int.Parse(btnEdit.ToolTip);
            ViewState["action"] = "edit";

            this.ShowDetails(OPID);

            //CheckBox chk = item.FindControl("cbRow") as CheckBox;
            //Label lblTotalclass = item.FindControl("lblTotalclass") as Label;
            //Label lblTotalattendance = item.FindControl("lblTotalattendance") as Label;
            //Label lblPercentage = item.FindControl("lblPercentage") as Label;
            //Label lblRegno = item.FindControl("lblRegno") as Label;



           // ListViewItem lst = btnEdit.NamingContainer as ListViewItem;
           // Label lblcollegename = lst.FindControl("lblcollegename") as Label;
           // Label lbldegree = lst.FindControl("lbldegree") as Label;
           // Label lblbranch = lst.FindControl("lblbranch") as Label;
           // Label lblschemename = lst.FindControl("lblschemename") as Label;
           // Label lblsemester = lst.FindControl("lblsemester") as Label;
           // Label lblcourse = lst.FindControl("lblcourse") as Label;
           // Label lbloperator = lst.FindControl("lbloperator") as Label;
           // Label lblstatus = lst.FindControl("lblstatus") as Label;
           // Label lbluafullname = lst.FindControl("lbluafullname") as Label;


           //// ddlSession.SelectedItem.Text = ddlSession.SelectedValue;
           // ddlColg.SelectedItem.Text = lblcollegename.Text.ToString().Trim();
           // ddlDegree.SelectedItem.Text = lbldegree.Text.ToString().Trim();
           // ddlBranch.SelectedItem.Text = lblbranch.Text.ToString().Trim();
           // ddlScheme.SelectedItem.Text = lblschemename.Text.ToString().Trim();
           // ddlSemester.SelectedItem.Text = lblsemester.Text.ToString().Trim();
           // ddlCourse.SelectedItem.Text = lblcourse.Text.ToString().Trim();

           // //Int32 Opretor =  Convert.ToInt32(ddlOprator.SelectedValue);
           // //ddlOprator.SelectedItem.Text = Opretor.ToString();
           // //Int32 Inex = Convert.ToInt32(ddlInEx.SelectedValue);
           // //ddlInEx.SelectedItem.Text = Inex.ToString();

           // ddlOprator.SelectedItem.Text = lbloperator.Text.ToString().Trim();
           // ddlInEx.SelectedItem.Text = lblstatus.Text.ToString().Trim();
           // ddlTeacher.SelectedItem.Text = lbluafullname.Text.ToString().Trim();


        


           // //ddlColg.Text = lblcollegename.Text.TrimEnd();
           // //ddlDegree.Text = lbldegree.Text.TrimEnd();
           // //ddlBranch.Text = lblbranch.Text.TrimEnd();
           // //ddlScheme.Text = lblschemename.Text.TrimEnd();
           // //ddlSemester.Text = lblsemester.Text.TrimEnd();
           // //ddlCourse.Text = lblcourse.Text.TrimEnd();
           // //ddlOprator.Text = lbloperator.Text.TrimEnd();
           // //ddlInEx.Text = lblstatus.Text.TrimEnd();
           // //ddlTeacher.Text = lbluafullname.Text.TrimEnd();



           // //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT SR INNER JOIN  ACD_SESSION_MASTER SM ON (SR.SESSIONNO=SM.SESSIONNO)", "DISTINCT (SM.SESSIONNO)", "SM.SESSION_NAME", "SM.SESSIONNO > 0", " SM.SESSIONNO DESC");
           // //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
           // //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0", "A.DEGREENAME");
           // //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0", "A.LONGNAME");
           // //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ddlDegree.SelectedValue, "SCHEMENO DESC");
           // //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SEMESTER S ON (SR.SEMESTERNO = S.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO = " + ddlScheme.SelectedValue + " ", "SR.SEMESTERNO");

           // //ddlSession.Items.Clear();
           // //ddlColg.Items.Clear();
           // //ddlDegree.Items.Clear();
           // //ddlBranch.Items.Clear();
           // //ddlScheme.Items.Clear();
           // //ddlSemester.Items.Clear(); 


           
        }


           


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }

         

    private void ClearControls()
    {
        //ddlColg.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlOprator.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlTeachertype.SelectedIndex = 0;
        ddlInEx.SelectedIndex = 0;
        ddlTeacher.SelectedIndex = 0;
       // ViewState["action"] = null;
       // chkover.Checked = false;
       
    }





    private void ShowDetails(int Opid)
    {
        try
        {
            SqlDataReader dr = objSC.GetDataForEditOperator(Opid);
            if (dr != null)
            {
                if (dr.Read())
                {
                    if (dr["SESSIONNAME"] == null | dr["SESSIONNAME"].ToString().Equals(""))
                        ddlSession.SelectedIndex = 0;
                    else
                        ddlSession.SelectedItem.Text = dr["SESSIONNAME"].ToString();
                       // ddlSession.Text = dr["SESSIONNAME"].ToString();
                       // if  (dr["SESSIONNAME"] ==  ddlSession.Text)
                       //   ddlSession.Text = dr["SESSIONNAME"].ToString();
                       //else
                       //  ddlSession.SelectedIndex = 0;
                            //    ddlSession.SelectedIndex = 0;

                    if (dr["COLLEGE_NAME"] == null | dr["COLLEGE_NAME"].ToString().Equals(""))
                        ddlColg.SelectedIndex = 0;
                    else
                        ddlColg.SelectedItem.Text = dr["COLLEGE_NAME"].ToString();

                    if (dr["DEGREENAME"] == null | dr["DEGREENAME"].ToString().Equals(""))
                        ddlDegree.SelectedIndex = 0;
                    else
                        ddlDegree.SelectedItem.Text = dr["DEGREENAME"].ToString();

                    if (dr["LONGNAME"] == null | dr["LONGNAME"].ToString().Equals(""))
                        ddlBranch.SelectedIndex = 0;
                    else
                        ddlBranch.SelectedItem.Text = dr["LONGNAME"].ToString();

                    if (dr["schemename"] == null | dr["schemename"].ToString().Equals(""))
                        ddlScheme.SelectedIndex = 0;
                    else
                        ddlScheme.SelectedItem.Text = dr["schemename"].ToString();

                    if (dr["SEMESTERNAME"] == null | dr["SEMESTERNAME"].ToString().Equals(""))
                        ddlSemester.SelectedIndex = 0;
                    else
                        ddlSemester.SelectedItem.Text = dr["SEMESTERNAME"].ToString();


                    if (dr["SUBID"] == null | dr["SUBID"].ToString().Equals(""))
                        
                        ddlSubjectType.SelectedIndex = 0;
                    else
                        //  ddlSubjectType.SelectedItem.Text = dr["SUBTYPE"].ToString();
                        ddlSubjectType.SelectedValue = dr["SUBID"].ToString();


                    //if (dr["COURSENO"] == null | dr["COURSENO"].ToString().Equals(""))
                    //    ddlCourse.SelectedIndex = 0;
                    //else
                    //    //ddlCourse.SelectedItem.Text = dr["COURSE_NAME"].ToString();
                    //     ddlCourse.SelectedValue = dr["COURSENO"].ToString();

                    if (dr["OP_STATUS"] == null | dr["OP_STATUS"].ToString().Equals(""))
                        ddlOprator.SelectedIndex = 0;
                    else
                        // ddlOprator.SelectedItem.Text = dr["OPRTR"].ToString();
                        ddlOprator.SelectedValue = dr["OP_STATUS"].ToString();

                    if (dr["INT_EXT"] == null | dr["INT_EXT"].ToString().Equals(""))
                        ddlInEx.SelectedIndex = 0;
                    else
                        // ddlInEx.SelectedItem.Text = dr["STATUS"].ToString();
                        ddlInEx.SelectedValue = dr["INT_EXT"].ToString();

                    if (dr["USERTYPEID"] == null | dr["USERTYPEID"].ToString().Equals(""))
                        ddlTeachertype.SelectedIndex = 0;
                        
                    else
                        // ddlTeachertype.SelectedItem.Text = dr["USER_TYPE"].ToString();
                        ddlTeachertype.SelectedValue = dr["USERTYPEID"].ToString();
                    
                    if (dr["UA_FULLNAME"] == null | dr["UA_FULLNAME"].ToString().Equals(""))
                        ddlTeacher.SelectedIndex = 0;
                    else
                        ddlTeacher.SelectedItem.Text = dr["UA_FULLNAME"].ToString();
                       // ddlTeacher.SelectedValue = dr["UA_FULLNAME"].ToString();
                        


                }
                if (dr != null) dr.Close();

                ViewState["action"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }    
}
