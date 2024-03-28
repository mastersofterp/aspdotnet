using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Net.NetworkInformation;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using SendGrid;

using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


public partial class ACADEMIC_REPORTS_MarksEntryDetailReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryDetailReportController objMEDC = new MarksEntryDetailReportController();
    string email = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Page Authorization
//CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            this.PopulateDropDown();
            //PopulateDropDownList();
            // GetDegree();
            //GetBranch();

            if ((Convert.ToInt32(Session["OrgId"]) == 7))// For Rajagiri Client
            {
                btnreportIE.Visible = true;
                btnIntWeightageReport.Visible = true;
                btnMarksReport.Visible = true;
            }
            else
            {
                btnreportIE.Visible = false;
                btnIntWeightageReport.Visible = false;
                btnMarksReport.Visible = false;
            }
            if ((Convert.ToInt32(Session["OrgId"]) == 3) || (Convert.ToInt32(Session["OrgId"]) == 4)) // Added by Lalit G on dated 24-02-2023 ticket no 39556
            {
                btnMarksEntry.Visible = true;
                btnOverallMarks.Visible = true;
            }
            else
            {

                btnMarksEntry.Visible = false;
                btnOverallMarks.Visible = false;
            }

            //btnMarksEntry.Visible = true;
            //btnOverallMarks.Visible = true;
        }


        if ((Convert.ToInt32(Session["OrgId"])) == 5)
        {
            btnExcel.Visible = true;
            btnSubjectWiseMarkEntry.Visible = true;
            btnQReports.Visible = true;
        }
        else
        {
            btnExcel.Visible = false;
            btnSubjectWiseMarkEntry.Visible = false;
            btnQReports.Visible = false;
        }
        //lblmsg.Text = "";


    }



    //Added by Suraj Y. on 11032024 for Faculty Login
    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");

                //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING WITH (NOLOCK)", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                if (Session["usertype"].ToString().Equals("1"))
                {
                    divSchoolInstitute.Visible = true;
                    divCourseType.Visible = true;
                }
                else
                {
                    divSchoolInstitute.Visible = false;
                    divCourseType.Visible = false;
                }


            }
            else
            {
                //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");

                //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING  CSM INNER JOIN ACD_COURSE_TEACHER CT ON (CSM.SCHEMENO=CT.SCHEMENO)", "CSM.COSCHNO", "CSM.COL_SCHEME_NAME", "CSM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND CSM.COSCHNO>0 AND CSM.COLLEGE_ID > 0  AND CSM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CSM.COLLEGE_ID DESC");
                string deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING  SC INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=SC.DEGREENO AND CDB.BRANCHNO=SC.BRANCHNO AND CDB.COLLEGE_ID=SC.COLLEGE_ID", "COSCHNO", "COL_SCHEME_NAME", "SC.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SC.COLLEGE_ID > 0 AND SC.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND CDB.DEPTNO IN (" + deptno + ")", "SC.COLLEGE_ID DESC");
                if (Session["usertype"].ToString().Equals("3"))
                {
                    divSchoolInstitute.Visible = true;
                    divCourseType.Visible = true;
                }
                else
                {
                    divSchoolInstitute.Visible = false;
                    divCourseType.Visible = false;
                }


            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarksEntryDetailReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarksEntryDetailReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING WITH (NOLOCK)", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_NAME");

            //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "");
            //objCommon.FillDropDownList(ddlSchoolInstitite, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlCourseType, "ACD_SUBJECTTYPE WITH (NOLOCK)", "SUBID", "SUBNAME", "SUBID>0", "SUBID");
            //objCommon.FillDropDownList(ddlSchoolInstitite, "USER_ACC C WITH (NOLOCK) CROSS APPLY (SELECT * FROM DBO.SPLIT(C.UA_COLLEGE_NOS,',')) AS A INNER JOIN ACD_COLLEGE_MASTER CT  WITH (NOLOCK) ON(A.VALUE=CT.COLLEGE_ID)", "A.VALUE AS COLLEGE_ID", "CT.COLLEGE_NAME AS COLLEGES", "COLLEGE_ID<>25 AND C.UA_NO=" + Session["userno"] + "", "CT.COLLEGE_ID");
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                divSchoolInstitute.Visible = true;
                divCourseType.Visible = true;
            }
            else
            {
                divSchoolInstitute.Visible = false;
                divCourseType.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_MarksEntryDetailReport.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void ddlSession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCourseType.SelectedIndex = 0;
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        gvParentGrid1.DataSource = null;
        gvParentGrid1.DataBind();
        divCourseMarksEntryDetail.Visible = false;

        int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO= '" + Convert.ToInt32(ddlSchoolInstitite.SelectedValue) + "'"));

        //objCommon.FillDropDownList(ddlCourseType, "ACD_SUBJECTTYPE S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT ASR ON S.SUBID=ASR.SUBID", "DISTINCT S.SUBID", "SUBNAME", "S.SUBID > 0 AND ISNULL(S.ACTIVESTATUS,0)=1 AND SCHEMENO=" + SCHEMENO + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "S.SUBID");
        //objCommon.FillDropDownList(ddlCourseType, "ACD_SUBJECTTYPE S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT ASR ON S.SUBID=ASR.SUBID", "DISTINCT S.SUBID", "SUBNAME", "S.SUBID > 0 AND ISNULL(S.ACTIVESTATUS,0)=1 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "S.SUBID");

        if (ddlSession.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R (NOLOCK) ON(R.SUBID=C.SUBID AND R.COURSENO=C.COURSENO) INNER JOIN ACD_SCHEME S(NOLOCK) ON(S.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_BRANCH B (NOLOCK) ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT  R.COURSENO", "COURSENAME  + ' ('+B.SHORTNAME+')'", "R.SUBID=" + Convert.ToInt32(ddlCourseType.SelectedValue) + " AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "R.COURSENO");
            // ddlcourse.Focus();
            objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + "AND ISNULL(CANCEL,0)=0 AND SR.SCHEMENO = " + SCHEMENO, "S.SEMESTERNO");
            ddlsemester.Focus();
        }
        //else
        //{
        //    ddlsemester.SelectedIndex = 0;
        //    ddlsemester.Focus();
        //}
    }

    protected void ddlSchoolInstitite_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlCourseType.SelectedIndex = 0;
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        gvParentGrid1.DataSource = null;
        gvParentGrid1.DataBind();
        if (ddlSchoolInstitite.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlSchoolInstitite.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }

            int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK) ", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID ='" + COLLEGEID + "' AND COLLEGE_ID > 0 ", "SESSIONNO desc");
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK) ", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID ='" + COLLEGE + "' AND COLLEGE_ID > 0 ", "SESSIONNO desc");
            ddlSession.Focus();
        }
        divCourseMarksEntryDetail.Visible = false;
    }

    protected void ddlCourseType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //divCourseMarksEntryDetail.Visible = false;
        //ddlsemester.Items.Clear();
        //ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        gvParentGrid1.DataSource = null;
        gvParentGrid1.DataBind();
        int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", " SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlSchoolInstitite.SelectedValue)));
        if (ddlCourseType.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R (NOLOCK) ON(R.SUBID=C.SUBID AND R.COURSENO=C.COURSENO) INNER JOIN ACD_SCHEME S(NOLOCK) ON(S.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_BRANCH B (NOLOCK) ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT  R.COURSENO", "COURSENAME  + ' ('+B.SHORTNAME+')'", "R.SUBID=" + Convert.ToInt32(ddlCourseType.SelectedValue) + " AND R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "R.COURSENO");
            // ddlcourse.Focus();
            //objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SUBID=" + Convert.ToInt32(ddlCourseType.SelectedValue) + " AND SR.SESSIONNO = " + ddlSession.SelectedValue + "AND ISNULL(CANCEL,0)=0 AND SR.SCHEMENO = " + SCHEMENO, "S.SEMESTERNO");
            objCommon.FillDropDownList(ddlcourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R (NOLOCK) ON(R.SUBID=C.SUBID AND R.COURSENO=C.COURSENO) INNER JOIN ACD_SCHEME S(NOLOCK) ON(S.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_BRANCH B (NOLOCK) ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT  R.COURSENO", " C.CCODE +'-'+ C.COURSE_NAME +'- '+ ' ('+B.SHORTNAME+')'", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.SUBID=" + Convert.ToInt32(ddlCourseType.SelectedValue) + "AND ISNULL(ACTIVESTATUS,0)=1 AND ISNULL(CANCEL,0)=0 AND R.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND R.SCHEMENO=" + SCHEMENO, "R.COURSENO");
            ddlcourse.Focus();
        }
        //else
        //{
        //    ddlsemester.SelectedIndex = 0;
        //    ddlsemester.Focus();
        //}
    }

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet ds = objMEDC.GetCollegeDetail(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(COLLEGEID), Convert.ToInt32(ddlCourseType.SelectedValue));
            //DataSet ds = objMEDC.GetCollegeDetail(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), Convert.ToInt32(COLLEGE), Convert.ToInt32(ddlCourseType.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                divCourseMarksEntryDetail.Visible = true;
                gvParentGrid1.DataSource = ds;
                gvParentGrid1.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Record Found", this.Page);
                gvParentGrid1.DataSource = null;
                gvParentGrid1.DataBind();
            }
        }
        else
        {
            gvParentGrid1.DataSource = null;
            gvParentGrid1.DataBind();
        }

    }

    protected void gvParentGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChildGrid");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;

                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;


                DataSet dsCourse = objMEDC.GetCourseDetail1(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfCollegeId.Value), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlCourseType.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlcourse.SelectedValue));
                if (dsCourse.Tables[0].Rows.Count > 0)
                {
                    gvChildGrid.DataSource = dsCourse;
                    gvChildGrid.DataBind();

                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        gvChildGrid.Columns[5].Visible = true;
                    }
                    else
                    {
                        gvChildGrid.Columns[5].Visible = false;
                    }
                }
                else
                {

                    gvChildGrid.DataSource = null;
                    gvChildGrid.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_gvParentGrid1_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChildGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid_1 = (GridView)e.Row.FindControl("gvChildGrid_1");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField childhdfCourseNo = e.Row.FindControl("childhdfCourseNo") as HiddenField;
                HiddenField childhdfSubType = e.Row.FindControl("childhdfSubType") as HiddenField;
                HiddenField childhdfUANO = e.Row.FindControl("childhdfUANO") as HiddenField;
                //HiddenField childhdfCourseNo = e.Row.FindControl("childhdfCourseNo") as HiddenField;

                HtmlGenericControl div = e.Row.FindControl("divcR1") as HtmlGenericControl;


                DataSet dsStud = objMEDC.GetMarksEntryStudentDetail(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(hdfCollegeId.Value), Convert.ToInt32(childhdfUANO.Value), Convert.ToInt32(childhdfCourseNo.Value), Convert.ToInt32(childhdfSubType.Value), Convert.ToInt32(Session["usertype"]));
                if (dsStud.Tables[0].Rows.Count > 0)
                {
                    gvChildGrid_1.Columns[3].Visible = false;  //S1T1 MARK
                    gvChildGrid_1.Columns[4].Visible = false;  //S1T2 MARK
                    gvChildGrid_1.Columns[5].Visible = false;  //S1T3 MARK
                    gvChildGrid_1.Columns[6].Visible = false;  //S1T4 MARK
                    gvChildGrid_1.Columns[7].Visible = false;  //S2 MARK
                    gvChildGrid_1.Columns[8].Visible = false; //EXTERNAL MARK
                    gvChildGrid_1.Columns[9].Visible = false; //EXTERNAL MARK
                    gvChildGrid_1.Columns[10].Visible = false; //EXTERNAL MARK

                    DataSet dsComponent = objMEDC.GetMarksEntryComponentDetail(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(childhdfCourseNo.Value), Convert.ToInt32(childhdfSubType.Value));
                    int COUNT = 0;
                    foreach (DataRow dr in dsComponent.Tables[0].Rows)
                    {
                        COUNT++;

                        if (COUNT == 1)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[3].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[3].Visible = true;
                            }
                        }
                        else if (COUNT == 2)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[4].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[4].Visible = true;
                            }
                        }
                        else if (COUNT == 3)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[5].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[5].Visible = true;
                            }
                        }
                        else if (COUNT == 4)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[6].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[6].Visible = true;
                            }
                        }
                        else if (COUNT == 5)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[7].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[7].Visible = true;
                            }
                        }
                        else if (COUNT == 6)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[8].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[8].Visible = true;
                            }
                        }
                        else if (COUNT == 7)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[9].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[9].Visible = true;
                            }
                        }
                        else if (COUNT == 8)
                        {
                            if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                            {
                                gvChildGrid_1.Columns[10].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                                gvChildGrid_1.Columns[10].Visible = true;
                            }
                        }

                        //if (COUNT == 1)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[4].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[4].Visible = true;
                        //    }
                        //}
                        //else if (COUNT == 2)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[5].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[5].Visible = true;
                        //    }
                        //}
                        //else if (COUNT == 3)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[3].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[3].Visible = true;
                        //    }
                        //}
                        //else if (COUNT == 4)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[6].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[6].Visible = true;
                        //    }
                        //}
                        //else if (COUNT == 5)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[7].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[7].Visible = true;
                        //    }
                        //}
                        //else if (COUNT == 6)
                        //{
                        //    if (Convert.ToString(dr["EXAMNAME"]) != string.Empty)
                        //    {
                        //        gvChildGrid_1.Columns[8].HeaderText = Convert.ToString(dr["EXAMNAME"]);
                        //        gvChildGrid_1.Columns[8].Visible = true;
                        //    }
                        //}
                    }

                    gvChildGrid_1.DataSource = dsStud;
                    gvChildGrid_1.DataBind();
                }
                else
                {
                    gvChildGrid_1.DataSource = null;
                    gvChildGrid_1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_gvChildGrid_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        int CourseNo = Convert.ToInt32(btn.CommandArgument);
        int CollegeId = Convert.ToInt32(btn.ToolTip);
        int SubID = Convert.ToInt32(btn.CommandName);
        int UA_NO = Convert.ToInt32(btn.ValidationGroup);

        if (SubID == 1)
            this.ShowReport("MarksDetail", "CourseWise_Marks_Entry_Detail_Report_Theory.rpt", CourseNo, CollegeId, SubID, UA_NO);
        else if (SubID == 2)
            this.ShowReport("MarksDetail", "CourseWise_Marks_Entry_Detail_Report_Practical.rpt", CourseNo, CollegeId, SubID, UA_NO);
        else
            this.ShowReport("MarksDetail", "CourseWise_Marks_Entry_Detail_Report_Other.rpt", CourseNo, CollegeId, SubID, UA_NO);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + CollegeId + "');", true);
    }

    private void ShowReport(string reportTitle, string rptFileName, int Courseno, int CollegeId, int SubID, int UA_NO)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue +
              ",@P_COURSENO=" + Courseno + ",@P_COLLEGE_ID=" + CollegeId +
              ",@P_UA_NO=" + UA_NO +
              ",@P_SUBTYPE=" + SubID + ",@P_UA_TYPE=" + Convert.ToInt32(Session["usertype"]);

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updMarksEntryDetailReport, this.updMarksEntryDetailReport.GetType(), "controlJSScript", sb.ToString(), true);
    }
    protected void btnSentEmail_Click(object sender, EventArgs e)
    {
        //divexpandcollapse('div4<%# Eval("SRNO") %>')
        Button btn = sender as Button;
        int ua_no = int.Parse(btn.CommandArgument);
        string divname = btn.ToolTip;
        Session["divname"] = divname;
        Session["CourseNo"] = btn.CommandName;


        string To_Email = objCommon.LookUp("USER_ACC", "ISNULL(UA_EMAIL,'')", "UA_NO=" + ua_no);
        Session["ToEmail"] = To_Email;
        if (To_Email != string.Empty)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + To_Email + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + divname + "');", true);
        }
        else
        {
            objCommon.DisplayMessage(this.updMarksEntryDetailReport, "Faculty e-mail id is not found", this.Page);
            return;
        }
    }

    protected void btnSent_Click(object sender, EventArgs e)
    {
        try
        {
            string mail = Session["ToEmail"].ToString();

            int To_ua_no = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_EMAIL= '" + mail + "'"));

            int From_Uano = Convert.ToInt32(Session["userno"]);

            int status = 0;
            CustomStatus cs = (CustomStatus)objMEDC.InsertMarkEntryMilSentLog(From_Uano, To_ua_no, txtSubject.Text, txtBody.Text, Convert.ToInt32(Session["CourseNo"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                Task<int> task = Execute(txtBody.Text, mail, txtSubject.Text);
                status = task.Result;
                if (status == 1)
                {
                    objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Sent Successfully", this.Page);

                    txt_emailid.Text = string.Empty;
                    txtBody.Text = string.Empty;
                    txtSubject.Text = string.Empty;
                }
                else
                {
                    objCommon.DisplayMessage(updMarksEntryDetailReport, "Email not sent, Please try again!!!.", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + Session["ToEmail"] + "');", true);
                }
            }

            string divname = Session["divname"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + divname + "');", true);
            Session["divname"] = string.Empty;
            Session["CourseNo"] = string.Empty;
            Session["ToEmail"] = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_btnSent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {

            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_emailid.Text = string.Empty;
        txtBody.Text = string.Empty;
        txtSubject.Text = string.Empty;
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        int subid = Convert.ToInt32(ddlCourseType.SelectedValue);
        if (subid == 1 || subid == 10)
        {
            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)//Added by lalit Dt 03-01-2023
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise_CPU.rpt";
                //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise.rpt";
                //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise_CPU_RCPIPER.rpt"; // SM
                //string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise_CPU.rpt";
                //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 10) // Added By Sagar Mankar On Date 17112023 For PRMITR
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise_PRMITR.rpt";
                //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory_Coursewise_CPU.rpt";
                //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
        }
        else
        {
            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)//Added by lalit Dt 03-01-2023
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_CPU.rpt";
                // string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_Coursewise.rpt";
                // string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_CPU_REPIPER.rpt";
                // string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else
            {
                string reportTitle = "SubjectWiseMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_CPU.rpt";
                // string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SECTIONNO=0,@P_SUBID=" + ddlCourseType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"].ToString()) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
        }
    }

    protected void ddlcourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlcourse.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue, "S.SEMESTERNO");
        //    ddlsemester.Focus();
        //}
        //else
        //{
        //    ddlsemester.SelectedIndex = 0;
        //    ddlcourse.Focus();
        //}
    }
    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        gvParentGrid1.DataSource = null;
        gvParentGrid1.DataBind();
        divCourseMarksEntryDetail.Visible = false;
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO=" + Convert.ToInt32(ddlSchoolInstitite.SelectedValue)));
        if (ddlsemester.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlcourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R (NOLOCK) ON(R.SUBID=C.SUBID AND R.COURSENO=C.COURSENO) INNER JOIN ACD_SCHEME S(NOLOCK) ON(S.SCHEMENO=C.SCHEMENO) INNER JOIN ACD_BRANCH B (NOLOCK) ON(B.BRANCHNO=S.BRANCHNO)", "DISTINCT  R.COURSENO", " C.CCODE +'-'+ C.COURSE_NAME +'- '+ ' ('+B.SHORTNAME+')'", "R.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND R.SUBID=" + Convert.ToInt32(ddlCourseType.SelectedValue) + "AND ISNULL(ACTIVESTATUS,0)=1 AND ISNULL(CANCEL,0)=0 AND R.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND R.SCHEMENO=" + Schemeno, "R.COURSENO");
            objCommon.FillDropDownList(ddlCourseType, "ACD_SUBJECTTYPE S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT ASR ON S.SUBID=ASR.SUBID", "DISTINCT S.SUBID", "SUBNAME", "S.SUBID > 0 AND ISNULL(S.ACTIVESTATUS,0)=1 AND SCHEMENO=" + Schemeno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND ASR.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "S.SUBID");
            ddlCourseType.Focus();
        }
    }

    protected void btnreportIE_Click(object sender, EventArgs e)
    {
        int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        int Org_Id = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER", "ORGANIZATIONID", "COLLEGE_ID='" + COLLEGEID + "'"));



        //int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "COLLEGE_ID=" + Convert.ToInt32(COLLEGEID)));
        //int Branchno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "DEGREENO=" + Convert.ToInt32(Degreeno)));
        //int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT SCHEMENO", "DEGREENO=" + Convert.ToInt32(Degreeno) + "AND BRANCHNO=" + Convert.ToInt32(Branchno)));

        int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT DEGREENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Branchno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT BRANCHNO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));


        int subid = Convert.ToInt32(ddlCourseType.SelectedValue);

        string reportTitle = "MarksListReport";
        //string rptFileName = "rptMarksList_Examwise.rpt";
        string rptFileName = "Consolidated_Mark_Result_Final_Semester_Result.rpt";
        //string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_cc.rpt";
        //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(COLLEGEID) + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(Degreeno) + ",@P_BRANCHNO=" + Convert.ToInt32(Branchno) + ",@P_SCHEMENO=" + Convert.ToInt32(Schemeno) + ",@P_UA_NO=0" + ",@P_COLLEGE_ID=" + Convert.ToInt32(COLLEGEID);
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);


    }

    protected void btnIntWeightageReport_Click(object sender, EventArgs e)
    {
        //ShowMarkEnry("MarkEntryReport", "rpt_CIA_Report_Weightage_Wise.rpt");

        int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT DEGREENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Branchno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT BRANCHNO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));

        string reportTitle = "MarksListReport";
        //string rptFileName = "rptMarksList_Examwise.rpt";
        string rptFileName = "rpt_CIA_Report_Weightage_Wise.rpt";
        //string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_cc.rpt";
        //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
        url += "&param=@P_COLLEGE_CODE=" + COLLEGEID + ",@P_COLLEGE_ID=" + COLLEGEID + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlsemester.SelectedValue + ",@P_COURSENO=" + ddlcourse.SelectedValue + ",@P_SCHEMENO=" + Schemeno;

        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
    }

    protected void btnMarksReport_Click(object sender, EventArgs e)
    {
        //ShowMarkEnry("MarkEntryReport", "rpt_CIA_Report_Marks_Wise.rpt");

        //int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        //int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        //int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "COLLEGE_ID=" + Convert.ToInt32(COLLEGEID)));
        //int Branchno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "DEGREENO=" + Convert.ToInt32(Degreeno)));
        //int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT SCHEMENO", "DEGREENO=" + Convert.ToInt32(Degreeno) + "AND BRANCHNO=" + Convert.ToInt32(Branchno)));


        int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        int Degreeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT DEGREENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Branchno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT BRANCHNO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));

        string reportTitle = "MarksListReport";
        //string rptFileName = "rptMarksList_Examwise.rpt";
        string rptFileName = "rpt_CIA_Report_Marks_Wise.rpt";
        //string rptFileName = "rptMarksList_ExamwiseNew_Coursewise_cc.rpt";
        //string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_DEGREETYPE=" + Convert.ToInt32(ddlDegreeType.SelectedValue);
        url += "&param=@P_COLLEGE_CODE=" + COLLEGEID + ",@P_COLLEGE_ID=" + COLLEGEID + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SEMESTERNO=" + ddlsemester.SelectedValue + ",@P_COURSENO=" + ddlcourse.SelectedValue + ",@P_SCHEMENO=" + Schemeno;

        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);

    }

    protected void btnMarksEntry_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;

            int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));

            DataSet ds = null;

            string proc_name = "PKG_CONSOLIDATED_MARK_ENTRY_COMPLETED&PENDING_REPORT";

            string para_name = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO,@P_SUBID";
            string call_values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(Schemeno) + "," + ddlCourseType.SelectedValue + "";
            // string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO,@P_ORGID";
            // string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "," + ORG + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);


            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=MarksEntryDetailsReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                //lvStudApplied.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnOverallMarks_Click(object sender, EventArgs e)
    {
        int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
        GridView GVDayWiseAtt = new GridView();
        string ContentType = string.Empty;

        // int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
        int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));

        DataSet ds = null;

        string proc_name = "PKG_CONSOLIDATED_MARK_ENTRY_COMPLETED_PENDING_REPORT";

        string para_name = "@P_COLLEGE_ID,@P_SESSIONNO,@P_SUBID";
        string call_values = "" + COLLEGEID + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlCourseType.SelectedValue + "";
        // string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO,@P_ORGID";
        // string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "," + ORG + "";
        ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);


        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.RemoveAt(3);
            GVDayWiseAtt.DataSource = ds;
            GVDayWiseAtt.DataBind();

            string attachment = "attachment; filename=OverallMarksEntry.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVDayWiseAtt.RenderControl(htw);
            //lvStudApplied.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
            DataSet ds = null;
            string proc_name = "PKG_SUBJECTWISE_MARK_ENTRY_REPORT_JECRC";
            string para_name = "@P_COLLEGE_ID,@P_SESSIONNO,@P_SUBID";
            string call_values = "" + COLLEGEID + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourseType.SelectedValue) + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=MarksEntryStatus.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlSchoolInstitite.SelectedIndex = 0;
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlCourseType.SelectedIndex = 0;

        ddlCourseType.Items.Clear();
        ddlCourseType.Items.Add(new ListItem("Please Select", "0"));
        ddlsemester.Items.Clear();
        ddlsemester.Items.Add(new ListItem("Please Select", "0"));
        ddlcourse.Items.Clear();
        ddlcourse.Items.Add(new ListItem("Please Select", "0"));
        divCourseMarksEntryDetail.Visible = false;
        gvParentGrid1.DataSource = null;
        gvParentGrid1.DataBind();

        ddlSchoolInstitite.Focus();
    }

    protected void btnSubjectWiseMarkEntry_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
            DataSet ds = null;
            string proc_name = "PKG_GET_COMPONENTWISE_MARK_DETAILS";
            string para_name = "@P_COLLEGEID,@P_SESSIONNO,@P_SUBJECTTYPE,@P_COURSENO";
            string call_values = "" + COLLEGEID + "," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourseType.SelectedValue) + "," + Convert.ToInt32(ddlcourse.SelectedValue) + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=MarksEntryStatus.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region blank student list report mark entry / Added by Injamam
    protected void btnblankmarkreport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "SCHEMENO", "COSCHNO= '" + Convert.ToInt32(ddlSchoolInstitite.SelectedValue) + "'"));
            string proc_name = "PKG_EXAM_GET_STUDENT_FOR_BLANKREPORT";
            string para_name = "@P_SESSIONNO,@P_COURSENO,@P_SEMESTERNO,@P_SCHEMENO";
            string call_values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlcourse.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + SCHEMENO + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string reportTitle = "BlankMarksListReport";
                string rptFileName = "rptBlankMarkListReport.rpt";
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_SCHEMENO=" + SCHEMENO + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlcourse.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_SCHEMENO=" + SCHEMENO + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", Print_Val, true);
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    protected void btnQReports_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            int COLLEGEID = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COLLEGE_ID", "COSCHNO= '" + COLLEGE + "'"));
            DataSet ds = null;
            string proc_name = "PKG_GET_QUESTIONWISE_MARKS_DETAILS";
            string para_name = "@P_SESSIONNO,@P_SUBID,@P_COURSENO";
            string call_values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourseType.SelectedValue) + "," + Convert.ToInt32(ddlcourse.SelectedValue) + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
            //if (ds.Tables[0].Rows.Count > 0)
            if (ds.Tables.Count > 0)
            {
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();
                string attachment = "attachment; filename=QuestionWiseMarksEntryStatus.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_Stud_BranchwiseReport.ShowReportExcel -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}