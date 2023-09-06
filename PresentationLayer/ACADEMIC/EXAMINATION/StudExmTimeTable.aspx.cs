using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Web;
using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class ACADEMIC_EXAMINATION_StudExmTimeTable : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                   Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //if (Session["usertype"].ToString() == "2")
                //{
                //    divScheme.Visible = false;
                //    rfvScheme.Enabled = false;
                //}
                //else
                //{
                //    divScheme.Visible = true;
                //    rfvScheme.Enabled = true;
                //}
                ddlExam.SelectedIndex = 0;
                lvExamTimeTable.Visible = false;

                if (Session["usertype"].ToString() == "2")
                {
                    divCollScheme.Visible = false;
                    BindSession();
                }



                PopulateDropDownList();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }


    }
    private void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlCollegeSheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");

        //objCommon.FillDropDownList(ddlCollegeSheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "SCHEMENO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");

        if (Session["usertype"].ToString().Equals("2"))
        {
            objCommon.FillDropDownList(ddlCollegeSheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollegeSheme, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON (SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"].ToString() + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

        }



        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        //if (Session["usertype"].ToString().Equals("2"))
        //{
        //    objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (SC.PATTERNNO=E.PATTERNNO) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON S.SCHEMENO=SC.SCHEMENO", "DISTINCT EXAMNO", "EXAMNAME", "IDNO=" + Convert.ToInt32(Session["idno"]) + " AND E.FLDNAME IN ('EXTERMARK')", "");
        //    ddlExam.SelectedIndex = 1;
        //}
        //else if (Session["usertype"].ToString().Equals("3"))
        //{
        //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON CT.SCHEMENO=S.SCHEMENO", "DISTINCT S.SCHEMENO", "SCHEMENAME", "(UA_NO=" + Convert.ToInt32(Session["userno"]) + " OR ADTEACHER=" + Convert.ToInt32(Session["userno"]) + ")", "S.SCHEMENO");

        //}
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindExamTimeTable();
    }
    private void BindExamTimeTable()
    {
        DataSet ds = null;
        {


            //string schemeno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "SM.SCHEMENO", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND SM.COSCHNO=" + ddlCollegeSheme.SelectedValue);

            if (Session["usertype"].ToString().Equals("2"))
            {

                ds = objExamController.GetStudentExamTimeTable(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue), Convert.ToInt32(Session["idno"]));
                //ViewState["College_id"] = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            }
            else //if (Session["usertype"].ToString().Equals("3"))
            {
                string schemeno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "SM.SCHEMENO", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND SM.COSCHNO=" + ddlCollegeSheme.SelectedValue);
                //ds = objExamController.GetFacultyExamTimeTable(Convert.ToInt32(ddlCollegeSheme.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue), Convert.ToInt32(Session["userno"]));
                ds = objExamController.GetFacultyExamTimeTable(Convert.ToInt32(schemeno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue), Convert.ToInt32(Session["userno"]));
            }
            //else
            //{
            //    string schemeno = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "SM.SCHEMENO", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND SM.COSCHNO=" + ddlCollegeSheme.SelectedValue);
            //    //ds = objExamController.GetFacultyExamTimeTable(Convert.ToInt32(ddlCollegeSheme.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue), Convert.ToInt32(Session["userno"]));
            //    ds = objExamController.GetAdminExamTimeTable(Convert.ToInt32(schemeno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExam.SelectedValue));//, Convert.ToInt32(Session["userno"]));
            //}
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvExamTimeTable.DataSource = ds;
                    lvExamTimeTable.DataBind();
                    lvExamTimeTable.Visible = true;
                }
                else
                {
                    objCommon.DisplayUserMessage(updtime, "No Data found", this.Page);
                    lvExamTimeTable.DataSource = null;
                    lvExamTimeTable.DataBind();
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updtime, "No Data found", this.Page);
                lvExamTimeTable.DataSource = null;
                lvExamTimeTable.DataBind();
            }
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E WITH (NOLOCK) INNER JOIN ACD_SCHEME SC WITH (NOLOCK) ON (SC.PATTERNNO=E.PATTERNNO)", "DISTINCT EXAMNO", "EXAMNAME", "SC.SCHEMENO=" +  + " AND  E.FLDNAME IN ('EXTERMARK')", "");
        ddlExam.SelectedIndex = 1;
    }

    protected void ddlCollegeSheme_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCollegeSheme.SelectedIndex > 0)
        {
            //Common objCommon = new Common();
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollegeSheme.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
            }


        }

    }

    private void BindSession()
    {
        //int Schememo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT AT INNER JOIN  ACD_STUDENT_RESULT AR ON (AT.IDNO=AR.IDNO)", "AT.SCHEMENO", "AT.IDNO=" + Convert.ToInt32(Session["idno"])));
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER AE INNER JOIN ACD_COLLEGE_SCHEME_MAPPING AM ON (AE.COLLEGE_ID=AM.COLLEGE_ID)", "AE.SESSIONNO", "AE.SESSION_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND AM.SCHEMENO=" + Schememo, "AE.SESSION_NAME DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER AE INNER JOIN ACD_COLLEGE_SCHEME_MAPPING AM ON (AE.COLLEGE_ID=AM.COLLEGE_ID)INNER JOIN ACD_STUDENT S ON (AE.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT AE.SESSIONNO", "AE.SESSION_NAME", "AM.SCHEMENO=" + Schememo, "AE.SESSIONNO DESC");
        // added By shubham For bind session without schemeno condition on 12/12/2022
        objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE ED ON (ED.SESSIONNO = SR.SESSIONNO) INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO = ED.SESSIONNO)", "DISTINCT SR.SESSIONNO", "SM.SESSION_NAME", "SR.IDNO=" + Convert.ToInt32(Session["idno"]) + "AND SR.CANCEL = 0", "SR.SESSIONNO DESC");  

    }


    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME", "DISTINCT EXAMNO", "EXAMNAME", "EXAMNAME<>''", "EXAMNO");
        //objCommon.FillDropDownList(ddlExam, "ACD_SCHEME AC INNER JOIN ACD_EXAM_NAME AE ON (AC.PATTERNNO=AE.PATTERNNO)", "Distinct Ac.SCHEMENO", "EXAMNAME", "EXAMNAME<>'' AND Ac.SCHEMENO="+ddlCollegeSheme.SelectedValue, "EXAMNO");
        //objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S INNER JOIN ACD_COURSE_TEACHER CT ON CT.SCHEMENO=S.SCHEMENO", "SESSIONNO", "SCHEMENAME", "SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENO desc");
        //objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME AE INNER JOIN ACD_SESSION_MASTER AD ON (AE.OrganizationId=AD.OrganizationId)", "DISTINCT AE.EXAMNO", "AE.EXAMNAME", "EXAMNAME<>'' AND AD.SESSIONNO=" + ddlSession.SelectedValue + "AND ACTIVESTATUS=1", "EXAMNO ASC");
        if (Session["usertype"].ToString() == "2")
        {
            int Schememo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT AT INNER JOIN  ACD_STUDENT_RESULT AR ON (AT.IDNO=AR.IDNO)", "AT.SCHEMENO", "AT.IDNO=" + Convert.ToInt32(Session["idno"])));
            //int Schememo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT AT INNER JOIN  ACD_STUDENT_RESULT AR ON (AT.IDNO=AR.IDNO)", "AT.SCHEMENO", "AT.IDNO=" + Convert.ToInt32(Session["idno"])));
            objCommon.FillDropDownList(ddlExam, "ACD_SCHEME AC Inner join  ACD_EXAM_NAME AE ON(AC.PATTERNNO=AE.PATTERNNO)", "DISTINCT AE.EXAMNO", "AE.EXAMNAME", "AE.EXAMNAME<>'' AND AC.SCHEMENO=" + Schememo, "AE.EXAMNO");
        }
        else
        {
            int schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "SM.SCHEMENO", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + "AND SM.COSCHNO=" + ddlCollegeSheme.SelectedValue));
            objCommon.FillDropDownList(ddlExam, "ACD_SCHEME AC Inner join  ACD_EXAM_NAME AE ON(AC.PATTERNNO=AE.PATTERNNO)", "DISTINCT AE.EXAMNO", "AE.EXAMNAME", "AE.EXAMNAME<>'' AND AC.SCHEMENO=" + schemeno, "AE.EXAMNO");
        }

    }
}