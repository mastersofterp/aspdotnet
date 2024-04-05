using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ClosedXML.Excel;

public partial class ACADEMIC_MASTERS_ExamRule : System.Web.UI.Page
{

    Common objCommon = new Common();
    Common _objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objSReg = new ExamController();
    StudentRegist objSR = new StudentRegist();
    string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string deptno = string.Empty;

    static int chkCount = 0;

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
        //Get_ExamSubNames();
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["OrgId"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
                if (Session["usertype"].ToString() != "1")
                {
                    string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                    int Exam_rule = Convert.ToInt32(objCommon.LookUp("REFF", "EXAM_RULE", ""));

                    if (Exam_rule == 1)
                    {
                        DivSubtype.Visible = true;
                    }
                    else
                    {
                        DivSubtype.Visible = false;
                    }

                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ") AND B.DEPTNO =" + Session["userdeptno"].ToString(), "D.DEGREENO");
                    ViewState["DEPTNO"] = "0";

                }
                else
                {
                    ViewState["DEPTNO"] = "0";
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.COLLEGE_ID IN (" + Session["college_nos"].ToString() + ")", "D.DEGREENO");

                    int Exam_rule = Convert.ToInt32(objCommon.LookUp("REFF", "EXAM_RULE", ""));

                    if (Exam_rule == 1)
                    {
                        DivSubtype.Visible = true;
                    }
                    else
                    {
                        DivSubtype.Visible = false;
                    }

                }

                PopulateDropDownList();

                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
                    //ADDED BY GAURAV FOR SUBJECT WISE TAB
                    objCommon.FillDropDownList(ddlClgname2, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO = ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0) OR ISNULL(" + Convert.ToInt32(Session["userdeptno"]) + ",0)=0)", "");
                }
                else
                {

                    objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                    //ADDED BY GAURAV FOR SUBJECT WISE TAB
                    objCommon.FillDropDownList(ddlClgname2, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                }
                //  btnPrint.Enabled = false;
                //  BindListView();
            }
            Session["reportdata"] = null;
        }
        divMsg.InnerHtml = string.Empty;
        // ddlSession.Focus();

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=courseAllot.aspx");
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC INNER JOIN dbo.ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SC.SCHEMETYPE)", "CAST(SC.SCHEMENO AS VARCHAR)+' - '+CAST(2 AS VARCHAR)", "SC.SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");
                //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC INNER JOIN dbo.ACD_SCHEMETYPE ST ON (ST.SCHEMETYPENO = SC.SCHEMETYPE)", "SC.SCHEMENO", "SC.SCHEMENAME", " DEGREENO =" + ddlDegree.SelectedValue + " and BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO DESC");

            }
            else
            {
                ddlScheme.Items.Clear();
                ddlBranch.SelectedIndex = 0;

            }
            ddlBranch.Focus();
            dvExamRule.Visible = false;
            
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

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void PopulateDropDownList()
    {
        try
        {
            // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            //ddlSession.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlsubjecttype, "[ACD_SUBJECTTYPE] WITH (NOLOCK)", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
            objCommon.FillDropDownList(ddlsubjecttype2, "[ACD_SUBJECTTYPE] WITH (NOLOCK)", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Registration_teacherallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {

       // objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER sm INNER JOIN ACD_STUDENT_RESULT oc on (sm.sessionno=oc.sessionno) INNER JOIN  ACD_COURSE C ON	(C.COURSENO	=OC.COURSENO)", "distinct oc.SESSIONNO", "sm.SESSION_PNAME", " OC.SEMESTERNO=" + ddlSem.SelectedValue + " AND OC.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND sm.sessionno > " + ddlSession.SelectedValue, "oc.SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER sm ", "distinct sm.SESSIONNO", "sm.SESSION_PNAME", "sm.sessionno > " + ddlSession.SelectedValue +"AND SM.COLLEGE_ID="+Convert.ToInt16(ViewState["college_id"]), "sm.SESSIONNO DESC");
        btnCopy.Enabled = false;
        pnlCsession.Visible = false;
        //try
        //{
        //    dvExamRule.Visible = false;


        //    string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + ddlSession.SelectedValue);
        //    string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue);
        //    string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ViewState["degreeno"].ToString() + " AND BRANCHNO=" + ViewState["branchno"].ToString() + "");
        //    if (exam_type == "1" && odd_even != "3")
        //    {
        //        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
    protected void ddlSession2_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlsession2.SelectedIndex > 0)
        {
            btnReport2.Enabled = true;
            pnlCourses2.Visible = false;
            btnReport2.Enabled = true;
        }
        else {
            btnReport2.Enabled = false;
 
        }
        pnlCourses2.Visible = false;
        btnSubmit2.Enabled  = btnLock2.Enabled = false;
        //{
        //    dvExamRule.Visible = false;


        //    string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + ddlsession2.SelectedValue);
        //    string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlsession2.SelectedValue);
        //    string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ViewState["degreeno"].ToString() + " AND BRANCHNO=" + ViewState["branchno"].ToString() + "");
        //    if (exam_type == "1" && odd_even != "3")
        //    {
        //        objCommon.FillDropDownList(ddlsem2, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

        //    }
        //    else
        //    {
        //        objCommon.FillDropDownList(ddlsem2, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
        //    }
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server UnAvailable");
        //}
    }
    //    try
    //    {
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));

    //        if (ddlBranch.SelectedIndex > 0)
    //        {
    //            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT_HIST SR, ACD_SEMESTER S", "DISTINCT SR.SEMESTERNO", "S.SEMESTERNAME", "  SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "SEMESTERNO");
    //            ddlSem.SelectedIndex = 0;
    //        }
    //       // ddlStuType.SelectedIndex = 0;
    //      //  txtDateOfIssue.Text = string.Empty;
    //      //  txtDeclareDate.Text = string.Empty;
    //        lvCourseExamRule.DataSource = null;
    //        lvCourseExamRule.DataBind();
    //        pnlCourses.Visible = false;                 
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_LockMarksByScheme.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }


    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlSem.Items.Clear();
            string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ddlSession.SelectedValue));
            string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ddlDegree.SelectedValue + " AND BRANCHNO=" + ddlBranch.SelectedValue + "");
            if (exam_type == "1" && odd_even != "3")
            {
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

            }
            else
            {
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

            }
            ddlScheme.Focus();
            dvExamRule.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
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
                if (Session["usertype"].ToString() != "1")
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO = " + Session["userdeptno"].ToString(), "A.LONGNAME");

                }
                else
                {
                    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
                }

            }
            else
            {
                ddlBranch.Items.Clear();
                ddlDegree.SelectedIndex = 0;

            }
            ddlDegree.Focus();
            dvExamRule.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //BUTTON TO CLEAR ALL CONTROLS OF PAGE 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnCancel_Click2(object sender, EventArgs e)
    {
        Clear2();
    }
    // BUTTON TO SHOW LISTVIEWS OF COURSES  EXAM RULE
    protected void btnShow_Click(object sender, EventArgs e)
    {
        this.ShowCourses();
    }
    protected void btnShow2_Click(object sender, EventArgs e)
    {
        this.ShowCourses_SubjectWise();
    }
    private void ShowCourses_SubjectWise()
    {
        try
        {
            //if (Convert.ToInt32(ddlScheme.SelectedValue.Split('-')[1]) == 2)
            //{
            DataSet dshead1 = null;
            // dshead = objSReg.GetSubexamheader(Convert.ToInt32(Session["OrgId"]),Convert.ToInt32(ViewState["schemeno"].ToString()));
            string SP_Name = "PKG_ACD_SUBEXAM_NAME";
            string SP_Parameters = "@P_ORGANIZATIONID,@P_SCHEMENO,@P_SUBID";
            string Call_Values = "" + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + ", " + Convert.ToInt32(ddlsubjecttype2.SelectedValue) + "";            
            dshead1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            ViewState["dshead"] = dshead1;
            DataTable dt = new DataTable();
            if (dshead1 != null && dshead1.Tables.Count > 0 && dshead1.Tables[0].Rows.Count > 0)
            {
                dt = dshead1.Tables[0];
                DataRow[] dr = dt.Select("");
                string str = string.Empty;
                string str1 = string.Empty;
                int td = 0;
                int colcont = dshead1.Tables[0].Columns.Count;
                ViewState["colcount"] = colcont.ToString();
                //int rule1 = colcont + 3;
                int rule1 = colcont;
                for (int i = 0; i < colcont; i++)
                {
                    //str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule12').attr('colspan'," + colcont + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
                    str += "$('#tbl_Rule12').attr('colspan'," + colcont + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
                }
                int z = 1; //z = 4;
                for (int j = 0; j < colcont; j++)
                {

                    str1 += "$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
                    z++;
                }

                string str3 = str + str1;
                ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
                btnSubmit2.Enabled = btnLock2.Enabled = true;
            }
            else
            {
                lblmessageShow2.ForeColor = System.Drawing.Color.Red;
                lblmessageShow2.Text = "No Subexams are Created,Kindly Create and Define Rules";
                lblmessageShow.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
              //  objCommon.DisplayMessage(this.updpnl, "No Subexams are Created,Kindly Create and Define Rules", this.Page);
                lvSubjectWiseExamRule.DataSource = null;
                lvSubjectWiseExamRule.DataBind();
                lvSubjectWiseExamRule.Visible = false;
                btnSubmit2.Enabled = btnLock2.Enabled =btnReport2.Enabled= false;
                //btnReport2.Enabled=tru
                return;
            }

            #region Working with RULE 2

            //if (dshead != null && dshead.Tables.Count > 0 && dshead.Tables[0].Rows.Count > 0)
            //{
            //    dt = dshead.Tables[0];
            //    DataRow[] dr = dt.Select("");
            //    string str = string.Empty;
            //    string str1 = string.Empty;
            //    int td = 0;
            //    int colcont = dshead.Tables[0].Columns.Count;
            //    ViewState["colcount"] = colcont.ToString();
            //    int rule1 = colcont - 1;
            //    //if (colcont == 8)
            //    //{
            //    //    td = 12;
            //    //}
            //    //else
            //    //{
            //    //    td = 9;
            //    //}
            //    for (int i = 0; i < colcont; i++)
            //    {
            //        //$('#tbl_cat3').hide();$('#tbl_cat3_assign').hide();$('td:nth-child(10)').hide();$('td:nth-child(11)').hide();
            //        str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#tbl_Rule2').attr('colspan',1);$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
            //    }
            //    for (int j = 0; j < colcont; j++)
            //    {
            //        //$('#tbl_cat3').hide();$('#tbl_cat3_assign').hide();$('td:nth-child(10)').hide();$('td:nth-child(11)').hide();
            //        if (j > 0)
            //        {
            //            //ctl00_ContentPlaceHolder1_lvCourseExamRule_ctrl2_td0
            //            int z = (j + 3);
            //            if (j == (colcont) - 1)
            //            {
            //                z = z + 1;
            //            }
            //            str1 += "$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#tbl_Rule2').attr('colspan',1);$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();$('td:nth-child(10)').show();";
            //            //$('td:nth-child(10)').show();
            //        }
            //    }



            //    //str1 += "$(" + colcont + ").text==8?$('td:nth-child(11)').show():$('td:nth-child(10)').hide();$(" + colcont + ").text==8?$('td:nth-child(12)').show():$('td:nth-child(11)').hide();";
            //    //ViewState["headerscript"] = str.ToString();
            //    string str3 = str + str1;
            //    ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
            //}

            #endregion

            // }
            DataSet ds = null;
            //ds = objSReg.GetCourseExamRule(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue));
            ds = objSReg.GetCourseExamRuleSubjectwise(Convert.ToInt32(ddlsession2.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlsem2.SelectedValue), Convert.ToInt32(ddlsubjecttype2.SelectedValue));
            DataTable dt1 = new DataTable();
            
            if (ds != null && ds.Tables.Count > 0)
            {
                //if (ds.Tables[0].Columns["ISLOCK"].ColumnName)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt1 = ds.Tables[0];                   
                    lvSubjectWiseExamRule.DataSource = ds.Tables[0];
                    lvSubjectWiseExamRule.DataBind();
                    lvSubjectWiseExamRule.Visible = true;
                    pnlCourses2.Visible = true;
                    dvExamRule2.Visible = true;
                    btnReport2.Enabled = true;
                }
                else
                {
                    lvSubjectWiseExamRule.DataSource = null;
                    lvSubjectWiseExamRule.DataBind();
                    lvSubjectWiseExamRule.Visible = false;
                    //lblmessageShow.ForeColor = System.Drawing.Color.Red;
                    //lblmessageShow.Text = "No Record Found!";
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    lblmessageShow2.ForeColor = System.Drawing.Color.Red;
                    lblmessageShow2.Text = "No Record Found!";
                    lblmessageShow.Text = string.Empty;
                    btnSubmit2.Enabled = btnLock2.Enabled = btnReport2.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                }


                int arrVal = 0;               

                string[] arr_TextBox = new string[] { "txtCat12", "txtCat1asn2", "txtCat22", "txtCat2asn2", "txtCat32", "txtCat3asn2", "txt72", "txt82", "txt92", "txt102", "txt112", "txt122", "txt132", "txt142", "txt152", "txt162", "txt172", "txt182", "txt192", "txt202" };
                string[] arr_HiddenField = new string[] { "hdfCat12", "hdfCat1_Asign2", "hdfCat22", "hdfCat2_Asign2", "hdfCat32", "hdfCat3_Asign2", "hdf72", "hdf82", "hdf92", "hdf102", "hdf112", "hdf122", "hdf132", "hdf142", "hdf152", "hdf162", "hdf172", "hdf182", "hdf192", "hdf202" };

                int i = 0;
                foreach (ListViewDataItem lvitem in lvSubjectWiseExamRule.Items)
                {
                    for (; i < ds.Tables[0].Rows.Count; )
                    {
                        for (int j = 7; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[i][j]) != "")
                            {
                                int CL = Convert.ToInt32(ds.Tables[0].Columns[j].ColumnName);
                                ((TextBox)lvitem.FindControl(arr_TextBox[arrVal])).Text = Convert.ToString(ds.Tables[0].Rows[i][j]) != "-1.00" ? Convert.ToString(ds.Tables[0].Rows[i][j]) : "";
                                ((HiddenField)lvitem.FindControl(arr_HiddenField[arrVal])).Value = Convert.ToString(ds.Tables[0].Columns[j].ColumnName);
                                //      arrVal++;

                                //if (j + 2 < Convert.ToInt32(ds.Tables[0].Columns.Count)&& Convert.ToString(ds.Tables[0].Rows[i][j + 2]) == "")
                                //if (j+1 < ds.Tables[0].Columns.Count && Convert.ToString(ds.Tables[0].Rows[i][j+1]) == "")
                                if (j + 2 > Convert.ToInt32(ds.Tables[0].Columns.Count))
                                {
                                    arrVal = 0;
                                    break;
                                }
                            }
                            arrVal++;
                        }
                        i++;
                        break;
                    }
                    arrVal = 0;
                    
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamRule.ShowStudents --> " + ex.Message + " " + ex.StackTrace);

            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //METHOD TO SHOW COURSE FOR EXAM RULE
    private void ShowCourses()
    {
        try
        {
            //if (Convert.ToInt32(ddlScheme.SelectedValue.Split('-')[1]) == 2)
            //{
            DataSet dshead = null;
            // dshead = objSReg.GetSubexamheader(Convert.ToInt32(Session["OrgId"]),Convert.ToInt32(ViewState["schemeno"].ToString()));
            string SP_Name = "PKG_ACD_SUBEXAM_NAME";
            string SP_Parameters = "@P_ORGANIZATIONID,@P_SCHEMENO,@P_SUBID";
            string Call_Values = "" + Convert.ToInt32(Session["OrgId"]) + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + ", " + Convert.ToInt32(ddlsubjecttype.SelectedValue) + "";
            dshead = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            ViewState["dshead"] = dshead;
            DataTable dt = new DataTable();
            if (dshead != null && dshead.Tables.Count > 0 && dshead.Tables[0].Rows.Count > 0)
            {
                dt = dshead.Tables[0];
                DataRow[] dr = dt.Select("");
                string str = string.Empty;
                string str1 = string.Empty;
                int td = 0;
                int colcont = dshead.Tables[0].Columns.Count;
                ViewState["colcount"] = colcont.ToString();
                int rule1 = colcont + 3;

                for (int i = 0; i < colcont; i++)
                {
                    str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
                }
                int z = 4;
                for (int j = 0; j < colcont; j++)
                {

                    str1 += "$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
                    z++;
                }

                string str3 = str + str1;
                ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "No Subexams are Created,Kindly Create and Define Rules", this.Page);
                lvCourseExamRule.DataSource = null;
                lvCourseExamRule.DataBind();
                lvCourseExamRule.Visible = false;
                return;
            }

            #region Working with RULE 2

            //if (dshead != null && dshead.Tables.Count > 0 && dshead.Tables[0].Rows.Count > 0)
            //{
            //    dt = dshead.Tables[0];
            //    DataRow[] dr = dt.Select("");
            //    string str = string.Empty;
            //    string str1 = string.Empty;
            //    int td = 0;
            //    int colcont = dshead.Tables[0].Columns.Count;
            //    ViewState["colcount"] = colcont.ToString();
            //    int rule1 = colcont - 1;
            //    //if (colcont == 8)
            //    //{
            //    //    td = 12;
            //    //}
            //    //else
            //    //{
            //    //    td = 9;
            //    //}
            //    for (int i = 0; i < colcont; i++)
            //    {
            //        //$('#tbl_cat3').hide();$('#tbl_cat3_assign').hide();$('td:nth-child(10)').hide();$('td:nth-child(11)').hide();
            //        str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#tbl_Rule2').attr('colspan',1);$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
            //    }
            //    for (int j = 0; j < colcont; j++)
            //    {
            //        //$('#tbl_cat3').hide();$('#tbl_cat3_assign').hide();$('td:nth-child(10)').hide();$('td:nth-child(11)').hide();
            //        if (j > 0)
            //        {
            //            //ctl00_ContentPlaceHolder1_lvCourseExamRule_ctrl2_td0
            //            int z = (j + 3);
            //            if (j == (colcont) - 1)
            //            {
            //                z = z + 1;
            //            }
            //            str1 += "$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#tbl_Rule2').attr('colspan',1);$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();$('td:nth-child(10)').show();";
            //            //$('td:nth-child(10)').show();
            //        }
            //    }



            //    //str1 += "$(" + colcont + ").text==8?$('td:nth-child(11)').show():$('td:nth-child(10)').hide();$(" + colcont + ").text==8?$('td:nth-child(12)').show():$('td:nth-child(11)').hide();";
            //    //ViewState["headerscript"] = str.ToString();
            //    string str3 = str + str1;
            //    ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
            //}

            #endregion

            // }
            DataSet ds = null;
            //ds = objSReg.GetCourseExamRule(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue.Split('-')[0]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue));
            ds = objSReg.GetCourseExamRule(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlsubjecttype.SelectedValue));

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvCourseExamRule.DataSource = ds.Tables[0];
                    lvCourseExamRule.DataBind();
                    lvCourseExamRule.Visible = true;
                    pnlCourses.Visible = true;
                    dvExamRule.Visible = true;
                    btnCopy.Enabled = true;
                }
                else
                {
                    lvCourseExamRule.DataSource = null;
                    lvCourseExamRule.DataBind();
                    lvCourseExamRule.Visible = false;
                    lblmessageShow.ForeColor = System.Drawing.Color.Red;
                    lblmessageShow.Text = "No Record Found!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    pnlCsession.Visible = false;
                    btnCopy.Enabled = false;
                }


                int arrVal = 0;
                //string[] arr_TextBox = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10" };
                //string[] arr_HiddenField = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10" };

                string[] arr_TextBox = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                string[] arr_HiddenField = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10", "hdf11", "hdf12", "hdf13", "hdf14", "hdf15", "hdf16", "hdf17", "hdf18", "hdf19", "hdf20" };

                int i = 0;
                foreach (ListViewDataItem lvitem in lvCourseExamRule.Items)
                {
                    for (; i < ds.Tables[0].Rows.Count; )
                    {
                        for (int j = 7; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[i][j]) != "")
                            {
                                int CL = Convert.ToInt32(ds.Tables[0].Columns[j].ColumnName);
                                ((TextBox)lvitem.FindControl(arr_TextBox[arrVal])).Text = Convert.ToString(ds.Tables[0].Rows[i][j]) != "-1.00" ? Convert.ToString(ds.Tables[0].Rows[i][j]) : "";
                                ((HiddenField)lvitem.FindControl(arr_HiddenField[arrVal])).Value = Convert.ToString(ds.Tables[0].Columns[j].ColumnName);
                                arrVal++;

                                //if (j + 2 < Convert.ToInt32(ds.Tables[0].Columns.Count)&& Convert.ToString(ds.Tables[0].Rows[i][j + 2]) == "")
                                //if (j+1 < ds.Tables[0].Columns.Count && Convert.ToString(ds.Tables[0].Rows[i][j+1]) == "")
                                if (j + 2 > Convert.ToInt32(ds.Tables[0].Columns.Count))
                                {
                                    arrVal = 0;
                                    break;
                                }
                            }
                        }
                        i++;
                        break;
                    }
                    arrVal = 0;

                    CheckBox chk = (CheckBox)lvitem.FindControl("chkAccept");
                    Label lbl = (Label)lvitem.FindControl("lblLock");
                    if (chk.Enabled == false)
                    {
                        chk.Visible = false;
                        lbl.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamRule.ShowStudents --> " + ex.Message + " " + ex.StackTrace);

            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvCourseExamRule_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    //METHOD  TO CLEAR SELECTION
    private void Clear()
    {

        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;

        ddlScheme.SelectedIndex = 0;
        ddlClgname.SelectedIndex = 0;
        ddlsubjecttype.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        dvExamRule.Disabled = true;
        pnlCourses.Visible = false;
        pnlCsession.Visible = false;
        btnCopy.Enabled = false;
        pnlCsession.Visible = false;
        btnSubmit.Enabled=btnLock.Enabled = false;
    }
    private void Clear2()
    {

        ddlsession2.SelectedIndex = 0;
        // ddlDegree2.SelectedIndex = 0;
        //ddlBranch2.SelectedIndex = 0;

        // ddlScheme2.SelectedIndex = 0;
        ddlClgname2.SelectedIndex = 0;
        ddlsubjecttype2.SelectedIndex = 0;
        ddlsem2.SelectedIndex = 0;
        dvExamRule2.Disabled = true;
        pnlCourses2.Visible = false;
        btnSubmit2.Enabled = false;
        btnLock2.Enabled = false;
        btnReport2.Enabled = false;

    }

    // BUTTON FOR EXAM RULE SUBMIT
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlCsession.SelectedIndex > 0)
        {
            CopyRuleToSession();
           
        }
        else
        {
            InsertUpdateForAll(0, 1);
        }
        // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + ViewState["headerscript"].ToString() + "", true);
    }
    protected void btnSubmit_Click2(object sender, EventArgs e)
    {
        InsertUpdateForAll2(0, 1);
      //  updSchemeCreation1.Update();
        // ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + ViewState["headerscript"].ToString() + "", true);
    }
    private void CopyRuleToSession()
    {
        
         DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]
         if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
         {
             ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
         }

        int scheme = Convert.ToInt32(ViewState["schemeno"]);
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int subType = Convert.ToInt32(ddlsubjecttype.SelectedValue);
        int sem = Convert.ToInt32(ddlSem.SelectedValue);
        int CopySession = Convert.ToInt32(ddlCsession.SelectedValue);

        CustomStatus cs = (CustomStatus)objSReg.CopyExamRuleToSession(scheme, session, subType, sem, CopySession);

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this,"Record Copied Successfully", this.Page);
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Record Insert Successfully", true);
           
            return;
        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            objCommon.DisplayMessage(this,"Record Already Exist", this.Page);  
        }

    }
    private void InsertUpdateForAll2(int isLock, int OType)
    {
        foreach (ListViewDataItem lvitem in lvSubjectWiseExamRule.Items)
        {          
            objSR.SESSIONNO = Convert.ToInt32(ddlsession2.SelectedValue);
            objSR.DEGREENO = Convert.ToInt32(ViewState["degreeno"].ToString());
            objSR.BRANCHNO = Convert.ToInt32(ViewState["branchno"].ToString());
            objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"].ToString());
            objSR.SEMESTERNO = Convert.ToInt32(ddlsem2.SelectedValue);
            objSR.SUBID = Convert.ToInt32(ddlsubjecttype2.SelectedValue);
            //LOCK STATUS VALUE
            objSR.START_NO = isLock;
            //OPERATION TYPE
            objSR.USERTTYPE = OType;

            string[] arr = new string[] { "txtCat12", "txtCat1asn2", "txtCat22", "txtCat2asn2", "txtCat32", "txtCat3asn2", "txt72", "txt82", "txt92", "txt102", "txt112", "txt122", "txt132", "txt142", "txt152", "txt162", "txt172", "txt182", "txt192", "txt202" };
            string[] arr_ExamSubName = new string[] { "hdfCat12", "hdfCat1_Asign2", "hdfCat22", "hdfCat2_Asign2", "hdfCat32", "hdfCat3_Asign2", "hdf72", "hdf82", "hdf92", "hdf102", "hdf112", "hdf122", "hdf132", "hdf142", "hdf152", "hdf162", "hdf172", "hdf182", "hdf192", "hdf202" };

            string Sp_name = "PKG_GET_EXAM_RULES_COURSE_WISE";
            string Sp_para = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_SUBID";
            string call_val = "" + objSR.SESSIONNO + "," + objSR.SCHEMENO + "," + objSR.SEMESTERNO + "," + objSR.SUBID + "";

            DataSet dscourses = objCommon.DynamicSPCall_Select(Sp_name, Sp_para, call_val);

            string courses = string.Empty;
            string ccodes = string.Empty;
            string coursenames = string.Empty;
            if (dscourses.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dscourses.Tables[0].Rows.Count; i++)
                {
                    courses  = dscourses.Tables[0].Rows[i]["COURSENO"].ToString();// +",";
                    ccodes  = dscourses.Tables[0].Rows[i]["CCODE"].ToString();// +",";
                    coursenames  = dscourses.Tables[0].Rows[i]["COURSE_NAME"].ToString();// +",";
                 //   int z = 0;
                    for (int j = 0; j < Convert.ToInt32(ViewState["colcount"].ToString()); j++)
                    {
                       
                        if (j == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                        {
                            //   i = i + 3;
                        }
                        TextBox txtRule_1 = (TextBox)lvitem.FindControl(arr[j]);
                        //if (txtRule_1.Text != string.Empty)
                        //{
                            objSR.COURSENNO += courses +",";
                            objSR.CCODE += ccodes +","; 
                            objSR.COURSENAME += coursenames+","; 
                            objSR.CATEGORY3 += ((HiddenField)lvitem.FindControl(arr_ExamSubName[j])).Value + ",";
                            //   objSR.CATEGORY3 += cl() + ",";

                           // objSR.Rule11 += Convert.ToString(Convert.ToDecimal(txtRule_1.Text) + ",");// commented by gaurav 19-03-2024

                            objSR.Rule11 += string.IsNullOrEmpty(txtRule_1.Text) ? "0," : Convert.ToString(Convert.ToDecimal(txtRule_1.Text) + ",");

                            // return;
                        //}
                        //else
                        //{
                        //    if (isLock == 1)
                        //    {
                        //        objCommon.DisplayMessage(this.updpnl, "Failed !! Unable to Lock Subject untill all Exam Percent Marks are not entered.", this.Page);
                               
                        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        //        txtRule_1.Focus();
                        //        return;
                        //    }                           
                        //    objSR.CATEGORY3 += ((HiddenField)lvitem.FindControl(arr_ExamSubName[j])).Value + ",";
                        //    objSR.Rule11 += Convert.ToString(Convert.ToDecimal("0") + ",");
                        //}

                        // For Rule 2
                        TextBox txtRule_2 = (TextBox)lvitem.FindControl("txtrule22");
                        if (txtRule_2.Text != string.Empty && Convert.ToDecimal(txtRule_2.Text) != 0)
                        {
                            objSR.Rule22 += Convert.ToString(Convert.ToDecimal(txtRule_2.Text) + ",");
                        }
                        else
                        {
                            txtRule_2.Text = "";
                            objSR.Rule22 += Convert.ToString(Convert.ToDecimal("0") + ",");                           
                            //return;
                        }
                        // }

                    }


                
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnl, "No Records Found", this.Page);
                return;
            }
        }

        //return;
     
        CustomStatus cs = (CustomStatus)objSReg.AddCourseExamRuleforcoursewise(objSR, Convert.ToInt32(Session["OrgId"]));

        if (cs.Equals(CustomStatus.DuplicateRecord))
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;           
            objCommon.DisplayMessage("Record already Exist", this.Page);           
        }
        else if (cs.Equals(CustomStatus.RecordSaved))
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;            
            ShowCourses_SubjectWise();         
            lblmessageShow2.ForeColor = System.Drawing.Color.Green;
            lblmessageShow2.Text = "Record Saved  successfully !";
            lblmessageShow.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;            
            ShowCourses_SubjectWise();     
            lblmessageShow2.ForeColor = System.Drawing.Color.Green;
            lblmessageShow2.Text = "Record Updated successfully!";
            lblmessageShow.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (cs.Equals(CustomStatus.Others))
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;            
            ShowCourses_SubjectWise();           
            lblmessageShow2.ForeColor = System.Drawing.Color.Green;
            lblmessageShow2.Text = "Record Locked successfully!";
            lblmessageShow.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;               
            lblmessageShow2.ForeColor = System.Drawing.Color.Red;
            lblmessageShow2.Text = "Unable to save Exam Rule !";
            lblmessageShow.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }

    private void InsertUpdateForAll(int isLock, int OType)
    {
        foreach (ListViewDataItem lvitem in lvCourseExamRule.Items)
        {

            CheckBox chk = ((CheckBox)lvitem.FindControl("chkAccept"));
            if (chk.Checked)
            {
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.DEGREENO = Convert.ToInt32(ViewState["degreeno"].ToString());
                objSR.BRANCHNO = Convert.ToInt32(ViewState["branchno"].ToString());
                objSR.SCHEMENO = Convert.ToInt32(ViewState["schemeno"].ToString());
                objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
                objSR.SUBID = Convert.ToInt32(ddlsubjecttype.SelectedValue);
                //LOCK STATUS VALUE
                objSR.START_NO = isLock;
                //OPERATION TYPE
                objSR.USERTTYPE = OType;

                // For Rule 1 -Short Method Less Code. -- Created By Abhinay Lad [09-07-2019]
                //string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10" };
                //string[] arr_ExamSubName = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10" };

                string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                string[] arr_ExamSubName = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10", "hdf11", "hdf12", "hdf13", "hdf14", "hdf15", "hdf16", "hdf17", "hdf18", "hdf19", "hdf20" };


                for (int i = 0; i < Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                {
                    if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                    {
                        //   i = i + 3;
                    }
                    TextBox txtRule_1 = (TextBox)lvitem.FindControl(arr[i]);

                    if (txtRule_1.Text != string.Empty)
                    {
                        objSR.COURSENNO += Convert.ToString(chk.ToolTip) + ",";
                        objSR.CCODE += ((Label)lvitem.FindControl("lblccode")).Text.Trim() + ",";
                        objSR.COURSENAME += ((Label)lvitem.FindControl("lblcname")).Text.Trim() + ",";
                        objSR.CATEGORY3 += ((HiddenField)lvitem.FindControl(arr_ExamSubName[i])).Value + ",";
                        //   objSR.CATEGORY3 += cl() + ",";

                        objSR.Rule11 += Convert.ToString(Convert.ToDecimal(txtRule_1.Text) + ",");
                    }
                    else
                    {
                        if (isLock == 1)
                        {
                            lblmessageShow.ForeColor = System.Drawing.Color.Red;
                            lblmessageShow.Text = "Failed !! Unable to Lock Subject untill all Exam Percent Marks are not entered.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                            txtRule_1.Focus();
                            return;
                        }
                        objSR.COURSENNO += Convert.ToString(chk.ToolTip) + ",";
                        objSR.CCODE += ((Label)lvitem.FindControl("lblccode")).Text.Trim() + ",";
                        objSR.COURSENAME += ((Label)lvitem.FindControl("lblcname")).Text.Trim() + ",";
                        objSR.CATEGORY3 += ((HiddenField)lvitem.FindControl(arr_ExamSubName[i])).Value + ",";

                        objSR.Rule11 += Convert.ToString(Convert.ToDecimal("0") + ",");
                    }

                    // For Rule 2
                    TextBox txtRule_2 = (TextBox)lvitem.FindControl("txtrule2");
                    if (txtRule_2.Text != string.Empty && Convert.ToDecimal(txtRule_2.Text) != 0)
                    {
                        objSR.Rule22 += Convert.ToString(Convert.ToDecimal(txtRule_2.Text) + ",");
                    }
                    else
                    {
                        txtRule_2.Text = "";
                        objSR.Rule22 += Convert.ToString(Convert.ToDecimal("0") + ",");
                        //lblmessageShow.ForeColor = System.Drawing.Color.Red;
                        //lblmessageShow.Text = "Please Enter Rule 2 Percentage Marks !!";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                        //((TextBox)lvitem.FindControl("txtrule2")).Focus();
                        //return;
                    }
                }
            }
        }

        //    return;

        CustomStatus cs = (CustomStatus)objSReg.AddCourseExamRule(objSR, Convert.ToInt32(Session["OrgId"]));

        if (cs.Equals(CustomStatus.DuplicateRecord))
        {
            btnSubmit.Enabled = btnLock.Enabled = false;
            chkCount = 0;
            lblmessageShow.ForeColor = System.Drawing.Color.Red;
            lblmessageShow.Text = "Record already Exist";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (cs.Equals(CustomStatus.RecordSaved))
        {
            btnSubmit.Enabled = btnLock.Enabled = false;
            chkCount = 0;
            ShowCourses();
            lblmessageShow.ForeColor = System.Drawing.Color.Green;
            lblmessageShow.Text = "Record Saved  successfully !";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (cs.Equals(CustomStatus.RecordUpdated))
        {
            btnSubmit.Enabled = btnLock.Enabled = false;
            chkCount = 0;
            ShowCourses();
            lblmessageShow.ForeColor = System.Drawing.Color.Green;
            lblmessageShow.Text = "Record Updated successfully!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else if (cs.Equals(CustomStatus.Others))
        {
            btnSubmit.Enabled = btnLock.Enabled = false;
            chkCount = 0;
            ShowCourses();
            lblmessageShow.ForeColor = System.Drawing.Color.Green;
            lblmessageShow.Text = "Record Locked successfully!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            btnSubmit.Enabled = btnLock.Enabled = false;
            chkCount = 0;
            lblmessageShow.ForeColor = System.Drawing.Color.Red;
            lblmessageShow.Text = "Unable to save Exam Rule !";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + ViewState["headerscript"].ToString() + "", true);
        CheckBox chk = sender as CheckBox;
        if (chk.Checked == true)
        {
            chkCount++;
            btnSubmit.Enabled = btnLock.Enabled = true;
        }
        else
        {
            chkCount--;
            if (chkCount == 0)
            {
                btnSubmit.Enabled = btnLock.Enabled = false;
            }
        }

        foreach (ListViewDataItem lvitem in lvCourseExamRule.Items)
        {

            CheckBox chk1 = ((CheckBox)lvitem.FindControl("chkAccept"));

            TextBox txtrule2 = ((TextBox)lvitem.FindControl("txtrule2"));

            if (chk1.Checked)
            {
                //  txtrule2.Enabled = true;
                // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20", "txtrule2" };
                for (int i = 0; i < Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                {
                    if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                    {
                        //   i = i + 3;
                    }
                    TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                    txt.Enabled = true;
                }

            }
            else
            {
                //  txtrule2.Enabled = false;
                // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20", "txtrule2" };
                for (int i = 0; i < Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                {
                    if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                    {
                        //  i = i + 3;
                    }
                    TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                    txt.Enabled = false;
                }
            }

        }
    }
    protected void chkAccept_CheckedChanged2(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + ViewState["headerscript"].ToString() + "", true);
        CheckBox chk = sender as CheckBox;
        if (chk.Checked == true)
        {
            chkCount++;
            btnSubmit2.Enabled = btnLock2.Enabled = true;
        }
        else
        {
            chkCount--;
            if (chkCount == 0)
            {
                btnSubmit2.Enabled = btnLock2.Enabled = false;
            }
        }

        foreach (ListViewDataItem lvitem in lvSubjectWiseExamRule.Items)
        {

            CheckBox chk1 = ((CheckBox)lvitem.FindControl("chkAccept2"));

            TextBox txtrule2 = ((TextBox)lvitem.FindControl("txtrule22"));

            if (chk1.Checked)
            {
                //  txtrule2.Enabled = true;
                // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20", "txtrule2" };
                for (int i = 0; i < Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                {
                    if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                    {
                        //   i = i + 3;
                    }
                    TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                    txt.Enabled = true;
                }

            }
            else
            {
                //  txtrule2.Enabled = false;
                // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20", "txtrule2" };
                for (int i = 0; i < Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                {
                    if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                    {
                        //  i = i + 3;
                    }
                    TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                    txt.Enabled = false;
                }
            }

        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        ddlSem.Focus();
        dvExamRule.Visible = false;
        
            //to copy session drop down
           // btnCopy.Enabled = true;
           // objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER sm INNER JOIN ACD_STUDENT_RESULT oc on (sm.sessionno=oc.sessionno) INNER JOIN  ACD_COURSE C ON	(C.COURSENO	=OC.COURSENO)", "distinct oc.SESSIONNO", "sm.SESSION_NAME", " OC.SEMESTERNO=" + ddlSem.SelectedValue + " AND OC.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND sm.sessionno > " + ddlSession.SelectedValue, "oc.SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER sm INNER JOIN ACD_OFFERED_COURSE oc on (sm.sessionno=oc.sessionno) INNER JOIN  ACD_COURSE C	ON	(C.COURSENO	=OC.COURSENO)", "distinct OC.SESSIONNO", "sm.SESSION_NAME", "sm.sessionno > " + ddlSession.SelectedValue, "OC.SESSIONNO DESC");

            btnCopy.Enabled = false;
            pnlCsession.Visible = false;
        
    }
    protected void ddlSem_SelectedIndexChanged2(object sender, EventArgs e)
    {
        if (ddlsem2.SelectedIndex > 0)
        {
            ddlsem2.Focus();
            dvExamRule2.Visible = false;
            btnReport2.Enabled = btnSubmit2.Enabled = btnLock2.Enabled = false;
            btnReport2.Enabled = true;
        }
        else
        {
            btnSubmit2.Enabled = btnLock2.Enabled = false;
            btnReport2.Enabled = true;
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        InsertUpdateForAll(1, 2);
    }
    protected void btnLock2_Click(object sender, EventArgs e)
    {
        InsertUpdateForAll2(1, 2);
    }
    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");

               

                //string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ViewState["schemeno"].ToString()));
                //string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ViewState["schemeno"].ToString()));
                //string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ViewState["degreeno"].ToString() + " AND BRANCHNO=" + ViewState["branchno"].ToString() + "");
                //if (exam_type == "1" && odd_even != "3")
                //{
                //    objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

                //}
                //else
                //{
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO >0", "SM.SEMESTERNO");
                //}

                // For hide 2nd tab index and list view
              //lvSubjectWiseExamRule.Visible = false;
              //dvExamRule2.Visible = false;
              //pnlCourses2.Visible = false;
              //ddlClgname2.SelectedIndex = 0;
              //ddlsem2.SelectedIndex = 0;
              //ddlsubjecttype2.SelectedIndex = 0;
              //ddlsem2.SelectedIndex = 0;
                Clear2();
            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }
        dvExamRule.Visible = false;
    }
    protected void ddlClgname2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsem2.Items.Clear();
        ddlsem2.Items.Add(new ListItem("Please Select", "0"));

        if (ddlClgname2.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname2.SelectedValue));
            //ViewState["degreeno"]
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

                objCommon.FillDropDownList(ddlsession2, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO DESC");

                //string odd_even = objCommon.LookUp("acd_session_master", "odd_Even", "sessionno=" + Convert.ToInt32(ViewState["schemeno"].ToString()));
                //string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ViewState["schemeno"].ToString()));
                //string semCount = objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "CAST(DURATION AS INT)*2 AS DURATION", "DEGREENO=" + ViewState["degreeno"].ToString() + " AND BRANCHNO=" + ViewState["branchno"].ToString() + "");
                //if (exam_type == "1" && odd_even != "3")
                //{
                //    objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

                //}
                //else
                //{
                //objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
                // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO >0", "SM.SEMESTERNO");
                //}
                objCommon.FillDropDownList(ddlsem2, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO >0", "SM.SEMESTERNO");
                Clear();
                //Clear2();
                pnlCourses2.Visible = false;
                btnSubmit2.Enabled = btnReport2.Enabled=btnLock2.Enabled= false;
               
            }
        }
        else
        {
            //ddlSession.SelectedIndex = 0;
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname2.Focus();
            btnReport2.Enabled = false;
        }
        //dvExamRule2.Visible = false;
        //dvExamRule2.Visible = true;


    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {

        ShowCoursesRuleReport("Rule_Report", "rptRuleReport.rpt");
        //pnlCourses2.Visible = true;

    }


    private void ShowCoursesRuleReport(string reportTitle, string rptFileName)
    {
        try
        {
            string collegecod = objCommon.LookUp("REFF", "College_code", "OrganizationId=" + Session["OrgId"].ToString());
            string subjecttype = objCommon.LookUp("acd_subjecttype", "SUBNAME", "SUBID=" + Convert.ToInt32(ddlsubjecttype2.SelectedValue));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
          //  url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlsession2.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsem2.SelectedValue) + ",@P_SUBID =" + Convert.ToInt32(ddlsubjecttype2.SelectedValue) + ",@P_ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(collegecod);    



           url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlsession2.SelectedValue) + ",@P_SCHEMENO=0,@P_SEMESTERNO=" + Convert.ToInt32(ddlsem2.SelectedValue) + ",@P_SUBID=0,@P_COLLEGE_CODE=" + Convert.ToInt32(collegecod);    
           // url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlsession2.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsem2.SelectedValue) + ",@P_SUBID=" + Convert.ToInt32(ddlsubjecttype2.SelectedValue) + ",@P_ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(collegecod)+",@P_SUBJECTTYPE="+subjecttype;    

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");            
           // ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
            ScriptManager.RegisterStartupScript(this, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);

            pnlCourses2.Visible = false;
            dvExamRule2.Visible = true;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamRule.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");

            objCommon.DisplayMessage(this.updpnl, "Failed !! Unable to Lock Subject untill all Exam Percent Marks are not entered.", this.Page);
          
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //Verifies that the control is rendered
    }
    #region Abhijit Sir Code
    // protected void btnExcel_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        //GetSchemeWiseExamRule(int session)

    //        // paramaert 0 set kela aahe 
    //        //{
    //        DataSet dshead1 = null;
        
    //        // dshead = objSReg.GetSubexamheader(Convert.ToInt32(Session["OrgId"]),Convert.ToInt32(ViewState["schemeno"].ToString()));
    //        string SP_Name = "PKG_GET_EXAM_RULES_COURSE_WISE_subject_type_wise_excel";
    //        string SP_Parameters = "@P_SESSIONNO";
    //        string Call_Values = "0";
    //        dshead1 = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

    //        ViewState["dshead"] = dshead1;
    //        // return;

    //        DataTable dt1=new DataTable();
    //        dt1=dshead1.Tables[0];

    //        Response.ContentType = "application/vnd.ms-excel";
    //        string headerTable = "";
    //        Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:12.0pt; font-family:Calibri; background:white;'> <TR>");

    //        Response.Write("</TR>");
    //        Response.Write("<TR>");
    //        StringWriter stringWrite = new StringWriter();
    //        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //        // dshead1.Tables[0].RenderControl(htmlWrite);
    //        Response.Write(headerTable);
    //        string tab = "";
    //        Response.Write("\n");
    //        foreach (DataColumn dc in dt1.Columns)
    //        {
    //            Response.Write("<Td>");
    //            Response.Write("<B>");
    //            Response.Write(tab + dc.ColumnName);
    //            Response.Write("</B>");
    //            Response.Write("</Td>");
    //            tab = "\t";
    //        }
    //        Response.Write("\n");
    //        Response.Write("</TR>");
    //        int i;
    //        foreach (DataRow dr in dt1.Rows)
    //        {
    //            Response.Write("<TR>");
    //            tab = "";
    //            for (i = 0; i < dt1.Columns.Count; i++)
    //            {
    //                Response.Write("<Td>");
    //                Response.Write(tab + dr[i].ToString());
    //                tab = "\t";
    //                Response.Write("</Td>");
    //            }
    //            Response.Write("\n");
    //            Response.Write("</TR>");
    //        }
    //        Response.Write(stringWrite.ToString());
    //        Response.Write("</Table>");
    //        //Response.End();
    //       Response.Flush();
    //    }

    //    catch (Exception ex)
    //    {

    //    }
    //}

#endregion
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            GridView GVExcel = new GridView();
            ResultProcessing objRes = new ResultProcessing();
            string SP_Name = "PKG_GET_EXAM_RULES_COURSE_WISE_subject_type_wise_excel";
            string SP_Parameters = "@P_SESSIONNAME";
            string Call_Values = ddlsession2.SelectedItem.Text;
            if (Convert.ToInt32(ddlsession2.SelectedValue) > 0)
            {
                Call_Values = ddlsession2.SelectedItem.Text;

            }
            else {
                Call_Values = '0'.ToString();
            }
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            DataTable dst = ds.Tables[0];
            DataGrid dg = new DataGrid();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string[] selectedColumns = new[] { "COLLEGE_NAME", "DEGREENAME", "BRANCH_NAME", "SCHEME_NAME", "SESSION_NAME","SEMESTER_NAME", "STATUS" };
                  DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
               //SCHEMENO	COLLEGE_NAME	DEGREENAME	BRANCH_NAME	SCHEME_NAME	SESSION_NAME	SEMESTERNO	STATUS
//146	School of Engineering and Technology	Bachelor of Technology	Computer Science and Engineering	SET - B. Tech-Computer Science and Engineering-2020-21-Department of Computer Engineering (Old)	Jan-June 2021-2022	2	SAVE AND LOCK
                // dt.Columns["13"].ColumnName = "END-TERM %"; // change column names
                // dt.Columns["14"].ColumnName = "Assignment %"; // change column names
               

                   using (XLWorkbook wb = new XLWorkbook())
                    {
                       
                        wb.Worksheets.Add(dt);
                      //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=" + "AllSchemeExamRule" + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                           // Response.End();
                        }
                    }
               }


                   
                }
            }
        
        catch (Exception ex)
        {

        }
    }



    //protected void btnBlankDownld_Click(object sender, EventArgs e)
    //{
    //    if (ddlExam.SelectedIndex > 0)      //&& (ddlSubExam.SelectedIndex != 0)
    //    {
    //        try
    //        {
    //            string excelname = string.Empty;
    //            string[] course = lblCourse.Text.Split('-');
    //            DataSet dsStudent = null;

    //            ViewState["StudCount"] = 0;
    //            int MExamNo = Convert.ToInt32(ViewState["examNo"].ToString());
    //            string subexamno = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "ISNULL(ACTIVESTATUS,0)=1 AND EXAMNO=" + MExamNo + "AND FLDNAME='" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0] + "'");

    //            //dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToInt32(Examno[0]), Convert.ToInt32(ViewState["COURSENO"]));
    //            dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), MExamNo, Convert.ToInt32(ViewState["COURSENO"]), subexamno);

    //            if (dsStudent != null && dsStudent.Tables.Count > 0)
    //            {
    //                if (dsStudent.Tables[0].Rows.Count > 0)
    //                {
    //                    //excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");
    //                    excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + ddlSubExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");

    //                    ViewState["StudCount"] = dsStudent.Tables[0].Rows.Count;
    //                    //Bind the Student List
    //                    DataTable dst = dsStudent.Tables[0];
    //                    DataGrid dg = new DataGrid();
    //                    if (dsStudent != null && dsStudent.Tables.Count > 0)
    //                    {
    //                        if (dsStudent.Tables[0].Rows.Count > 0)
    //                        {
    //                            string[] selectedColumns = new[] { "IDNO", "STUDNAME", "REGNO1", "CCODE", "COURSENAME", "DEGREENAME", "BRANCHNAME", "SCHEMENAME", "SEMESTERNAME", "SESSIONNAME", "EXAMNAME", "SUBEXAMNAME", "SECTIONNAME", "MAXMARK" };

    //                            DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
    //                            dt.Columns["REGNO1"].ColumnName = "REGNO / ROLL_NO"; // change column names
    //                            //dt.Columns["SMAX"].ColumnName = "MAX MARKS"; // change column names
    //                            dt.Columns.Add("MARKS");

    //                            using (XLWorkbook wb = new XLWorkbook())
    //                            {
    //                                //foreach (System.Data.DataTable dtt in dsStudent.Tables)
    //                                //{
    //                                //Add System.Data.DataTable as Worksheet.
    //                                wb.Worksheets.Add(dt);
    //                                //}

    //                                //Export the Excel file.
    //                                Response.Clear();
    //                                Response.Buffer = true;
    //                                Response.Charset = "";
    //                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //                                Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");
    //                                using (MemoryStream MyMemoryStream = new MemoryStream())
    //                                {
    //                                    wb.SaveAs(MyMemoryStream);
    //                                    MyMemoryStream.WriteTo(Response.OutputStream);
    //                                    Response.Flush();
    //                                    Response.End();
    //                                }
    //                            }
    //                        }
    //                    }

    //                    pnlSelection.Visible = false;
    //                    pnlMarkEntry.Visible = true;
    //                    pnlStudGrid.Visible = true;
    //                    lblStudents.Visible = true;
    //                    btnBack.Visible = true;

    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(updpanle1, "Students Not Found..!!", this.Page);
    //                }
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server Unavailable.");
    //            objCommon.DisplayMessage(ex.ToString(), this.Page);
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(updpanle1, "Please Select Exam!!", this.Page);
    //        ddlExam.Focus();
    //    }
    //}

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        pnlCsession.Visible = true;
       
    }
    protected void ddlCsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCsession.SelectedIndex > 0)
        {
            btnSubmit.Enabled = true;
        }
        else
        {
            btnSubmit.Enabled = false;
        }
    }
}
