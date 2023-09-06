//Created By Nikhil on 17042020
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ACADEMIC_DashBoard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
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
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S inner join ACD_STUDENT_RESULT ST on S.SESSIONNO=ST.SESSIONNO", "Distinct(ST.SESSIONNO)", "S.SESSION_PNAME", "S.SESSIONNO > 0", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlExam, " ACD_EXAM_NAME EX INNER JOIN  ACD_SCHEME SC ON(SC.PATTERNNO=EX.PATTERNNO) ", "DISTINCT FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !=''", "FLDNAME");
            }
        }
        if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "DISTINCT SESSIONID", "SESSION_NAME", "SESSIONID > 0 AND ISNULL(IS_ACTIVE,0) = 1", "SESSION_NAME DESC");
            //objCommon.FillDropDownList(ddlPattern, "ACD_EXAM_PATTERN", "DISTINCT PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1", "PATTERN_NAME ASC ");

            //DataSet ds = objCommon.FillDropDown("ACD_EXAM_NAME EX INNER JOIN  ACD_SCHEME SC ON(SC.PATTERNNO=EX.PATTERNNO) ", "DISTINCT EXAMNO, FLDNAME", "EXAMNAME", " EXAMNAME IS NOT NULL AND EXAMNAME !=''", "EXAMNO");
            //DataSet ds = objCommon.FillDropDown("ACTIVITY_MASTER A WITH (NOLOCK) INNER JOIN SESSION_ACTIVITY S WITH (NOLOCK) ON (A.ACTIVITY_NO = S.ACTIVITY_NO) INNER JOIN ACD_EXAM_NAME E WITH (NOLOCK) ON (A.EXAMNO = E.EXAMNO) ", "DISTINCT A.EXAMNO,E.FLDNAME", "E.EXAMNAME", "ISNULL(E.FLDNAME,'')<>'' AND ISNULL(E.EXAMNAME,'')<>''", "E.EXAMNAME ASC");

            //ddlExam.Items.Clear();
            //ddlExam.Items.Add("Please Select");
            //ddlExam.SelectedItem.Value = "0";

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlExam.DataSource = ds;
            //    ddlExam.DataValueField = ds.Tables[0].Columns["EXAMNO"].ToString();
            //    ddlExam.DataTextField = ds.Tables[0].Columns["EXAMNAME"].ToString();
            //    ddlExam.DataBind();
            //    ddlExam.SelectedIndex = 0;
            //}
            divSubExam.Visible = false;
            ddlSession.Focus();

            //if (Session["usertype"].ToString() != "1")
            //{
            //    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER C WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (C.COLLEGE_ID = B.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "B.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0 AND B.DEPTNO =" + Session["userdeptno"].ToString(), "C.COLLEGE_ID");
            //}
            //else
            //{
            //    objCommon.FillDropDownList(ddlSchoolInstitute, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME ", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME ASC");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MarksEntryDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DashBoard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DashBoard.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            int Session = (ddlSession.SelectedIndex > 0 ? int.Parse(ddlSession.SelectedValue) : 0);

            DataSet ds = null;

            //DataSet ds = objMarksEntry.GetExamDashboardDetails(Convert.ToInt32(ddlSession.SelectedValue), ddlExam.SelectedValue, ddlSubExam.SelectedValue, Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //DataSet ds = objMarksEntry.GetExamDashboardDetails(Convert.ToInt32(ddlSession.SelectedValue), ddlExam.SelectedValue, ddlSubExam.SelectedValue, Convert.ToInt32(0));


            string sp_procedure = "PKG_EXAM_MARKENTRY_STATUS_CONSOLIDATED_DASHBOARD";
            string sp_parameters = "@P_SESSIONID,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAM,@P_PATTERNNO";
            string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlExam.SelectedValue) + "," + Convert.ToInt32(ddlSubExam.SelectedValue) + "," + Convert.ToInt32(ddlPattern.SelectedValue) + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            divExamMarksEntryStatus.Visible = false;

            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                objCommon.DisplayMessage(updPanel1, "Record Not Found.", this.Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                return;
            }
            else
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                divExamMarksEntryStatus.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "DashBoard.aspx --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

        //ddlSchoolInstitute.Focus();
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlPattern, "ACD_EXAM_PATTERN", "DISTINCT PATTERNNO", "PATTERN_NAME", "PATTERNNO > 0 AND ISNULL(ACTIVESTATUS,0) = 1", "PATTERN_NAME ASC ");
            ddlPattern.Focus();
        }
        else
        {
            ddlPattern.Items.Clear();
            ddlPattern.Items.Add("Please Select");
            ddlPattern.SelectedItem.Value = "0";
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divExamMarksEntryStatus.Visible = false;

        ddlExam.SelectedIndex = 0;

        divSubExam.Visible = false;
        ddlSubExam.SelectedIndex = 0;

        ddlExam.Items.Clear();
        ddlExam.Items.Add("Please Select");
        ddlExam.SelectedItem.Value = "0";
    }

    protected void ddlSchoolInstitute_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divExamMarksEntryStatus.Visible = false;

        //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "COLLEGE_ID='" + ddlSchoolInstitute.SelectedValue + "'", "A.SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0)
        {
            //if (ddlExam.SelectedValue == "S1")//Theory Purpose
            //{
            divSubExam.Visible = true;
            //objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SUBEXAMNAME", "ISNULL(EXAMNAME,'')<>'' AND ISNULL(SUBEXAMNAME,'')<>''" + " AND ED.EXAMNO = '" + ddlExam.SelectedValue + "'", "SE.SUBEXAMNO");
            //objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.FLDNAME", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = '" + ddlExam.SelectedValue.Split('-')[1].ToString() + "' AND EP.PATTERNNO='" + ddlPattern.SelectedValue + "' AND ISNULL(EP.ACTIVESTATUS,0) = 1", "SE.FLDNAME");

            objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SE.SUBEXAMNAME", "ED.EXAMNAME<>'' AND SE.SUBEXAMNAME<>'' AND ISNULL(SE.ACTIVESTATUS,0)=1  AND SE.EXAMNO = '" + ddlExam.SelectedValue + "' AND EP.PATTERNNO='" + ddlPattern.SelectedValue + "' AND ISNULL(EP.ACTIVESTATUS,0) = 1", "SE.SUBEXAMNO");

            //}
            //else if (ddlExam.SelectedValue == "S3") //Pracctical Marks Entry Purpose.
            //{
            //    divSubExam.Visible = true;
            //    objCommon.FillDropDownList(ddlSubExam, "ACD_SCHEME S INNER JOIN ACD_EXAM_NAME ED ON (ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO)", "DISTINCT SE.SUBEXAMNO", "SUBEXAMNAME", "ISNULL(EXAMNAME,'')<>'' AND ISNULL(SUBEXAMNAME,'')<>''" + " AND ED.EXAMNO = '" + ddlExam.SelectedValue + "'", "SE.SUBEXAMNO");
            //}
            //else
            //{
            //    divSubExam.Visible = false;
            //}
        }
        else
        {
            ddlSubExam.Items.Clear();
            ddlSubExam.Items.Add("Please Select");
            ddlSubExam.SelectedItem.Value = "0";

            divSubExam.Visible = false;
        }

        //divSubExam.Visible = false;

        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divExamMarksEntryStatus.Visible = false;
    }

    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        divExamMarksEntryStatus.Visible = false;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int Sessionno = (ddlSession.SelectedIndex > 0 ? Convert.ToInt32(ddlSession.SelectedValue) : 0);
            string Examno = ddlExam.SelectedValue;
            GridView GV = new GridView();

            DataSet ds = null;

            //DataSet ds = objMarksEntry.GetAllExamMarkEntryStatusforExcel(Convert.ToInt32(ddlSession.SelectedValue), ddlExam.SelectedValue, ddlSubExam.SelectedValue, Convert.ToInt32(ddlSchoolInstitute.SelectedValue));
            //DataSet ds = objMarksEntry.GetAllExamMarkEntryStatusforExcel(Convert.ToInt32(ddlSession.SelectedValue), ddlExam.SelectedValue, ddlSubExam.SelectedValue, Convert.ToInt32(0));

            string sp_procedure = "PKG_EXAM_MARKENTRY_STATUS_CONSOLIDATED_DASHBOARD_FOR_EXCEL";
            string sp_parameters = "@P_SESSIONID,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAM,@P_PATTERNNO";
            string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(0) + "," + Convert.ToInt32(ddlExam.SelectedValue) + "," + Convert.ToInt32(ddlSubExam.SelectedValue) + "," + Convert.ToInt32(ddlPattern.SelectedValue) + "";

            ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GV.DataSource = ds;
                    GV.DataBind();
                    string Attachment = "Attachment ; filename=AllExamMarkEntryStatus.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", Attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GV.HeaderStyle.Font.Bold = true;
                    GV.HeaderStyle.Font.Name = "Times New Roman";
                    GV.RowStyle.Font.Name = "Times New Roman";
                    GV.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(updPanel1, "Record Not Found.", this.Page);
                    //lvStudents.DataSource = null;
                    //lvStudents.DataBind();
                    //return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updPanel1, "Record Not Found.", this.Page);
                //lvStudents.DataSource = null;
                //lvStudents.DataBind();
                //return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objUCommon.ShowError(Page, "DashBoard.aspx --> " + ex.Message + " " + ex.StackTrace);
            }
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlPattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPattern.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlExam, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", " (ED.FLDNAME+'-'+ CAST(ED.EXAMNO AS VARCHAR(20)))FLDNAME ", "ED.EXAMNAME", " ED.EXAMNAME<>'' AND EP.PATTERNNO='" + ddlPattern.SelectedValue + "' AND ISNULL(EP.ACTIVESTATUS,0) = 1 group by ED.FLDNAME,ED.EXAMNO,ED.EXAMNAME,EP.PATTERNNO", "ED.EXAMNO");
            objCommon.FillDropDownList(ddlExam, " ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_EXAM_NAME ED WITH (NOLOCK) ON(ED.PATTERNNO=S.PATTERNNO) INNER JOIN ACD_SUBEXAM_NAME SE WITH (NOLOCK) ON (SE.EXAMNO = ED.EXAMNO AND ED.PATTERNNO=SE.PATTERNNO) INNER JOIN ACD_EXAM_PATTERN EP ON (EP.PATTERNNO=ED.PATTERNNO AND EP.PATTERNNO=SE.PATTERNNO)", "ED.EXAMNO", "ED.EXAMNAME", " ED.EXAMNAME<>'' AND EP.PATTERNNO='" + ddlPattern.SelectedValue + "' AND ISNULL(EP.ACTIVESTATUS,0) = 1 group by ED.FLDNAME,ED.EXAMNO,ED.EXAMNAME,EP.PATTERNNO", "ED.EXAMNAME");
            ddlExam.Focus();
        }
        else
        {
            ddlExam.Items.Clear();
            ddlExam.Items.Add("Please Select");
            ddlExam.SelectedItem.Value = "0";
        }

        divSubExam.Visible = false;
    }

} //END of Partial class