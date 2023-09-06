using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Itle_Itle_Subject_Wise_Result_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TestResult objResult = new TestResult();

    string TestType = string.Empty;

    #region Page Load

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
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }


                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

            }

            //lblSession.Text = Session["ISessionName"].ToString();
            //lblSession.ToolTip = Session["ISessionNo"].ToString();
            ////lblCourseName.Text = Session["ICourseName"].ToString();
            ////trStud.Visible = false;
            fillSession();
            TestType = "O";
        }

        //lblSession.Text = Session["ISessionName"].ToString();
        //lblSession.ToolTip = Session["ISessionNo"].ToString();
        //lblCourseName.Text = Session["ICourseName"].ToString();
        pnlReport.Visible = false;
        trExcelButton.Visible = false;
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_Subject_Wise_Result_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Subject_Wise_Result_Report.aspx");
        }
    }

    protected void fillSession()
    {
        // DataSet dsSession = new DataSet();
        //// dsSession = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        // dsSession = objCommon.FillDropDown("ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) inner join ACD_STUDENT_RESULT S on (A.SESSIONNO=S.SESSIONNO)", "DISTINCT A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME) as SESSION_NAME", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) AND IDNO=" + Convert.ToInt32(Session["idno"]), "A.SESSIONNO ");
        // Ddlsession.Items.Clear();
        // Ddlsession.Items.Add("Please Select");
        // Ddlsession.SelectedItem.Value = "0";
        // Ddlsession.DataTextField = "SESSION_NAME";
        // Ddlsession.DataValueField = "SESSIONNO";
        // Ddlsession.DataSource = dsSession.Tables[0];
        // Ddlsession.DataBind();
        // Ddlsession.SelectedIndex = 0;

        DataSet ds = null;
       
        {
           
            if (Convert.ToInt32(Session["usertype"]) == 2)
            {
                 objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID) inner join ACD_STUDENT_RESULT S on (A.SESSIONNO=S.SESSIONNO)", "DISTINCT A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "A.SESSIONNO>0 AND EXAMTYPE IN (1,3) AND IDNO=" + Convert.ToInt32(Session["idno"]), "A.SESSIONNO ");
            }
            else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
            {

                objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

            }

        }

    }
    protected void getMainCourse()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTTYPE=" + rbtTestType.SelectedValue;
            //url += "&param=username=" + Session["userfullname"].ToString() + ",SESSIONNAME=" + Ddlsession.SelectedItem + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_TESTTYPE=" + rbtTestType.SelectedValue + ""; //+ ",COURSENAME=" + ddlCourse.SelectedItem
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "Itle_Subject_Wise_Result_Report.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    private void ShowReportAll(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ITLE_Result_Sub_Wise.aspx")));
            url += "../Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=username=" + Session["username"].ToString() + ",IP_ADDRESS=" + Session["IPADDR"].ToString() + ",MAC_ADDRESS=" + Session["Database"].ToString() + ",SESSIONNAME=" + Session["ISessionName"].ToString() + ",COURSENAME=" + Session["ICourseName"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "Itle_Subject_Wise_Result_Report.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void Ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Ddlsession.SelectedIndex > 0)
        {
            getMainCourse();
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {

                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "", "LONGNAME");
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH  CDB INNER JOIN  ACD_BRANCH B ON (CDB.BRANCHNO = B.BRANCHNO)", "CDB.BRANCHNO", "B.LONGNAME", "DEGREENO="+ddlDegree.SelectedValue, "LONGNAME");
                

                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Subject_Wise_Result_Report.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");//DEGREENO = " + ddlDegree.SelectedValue + " AND 
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Subject_Wise_Result_Report.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR INNER JOIN ACD_Semester SC ON SR.SEmesterNO = SC.SEmesterNO", "DISTINCT SR.SEMESTERNO", "SC.SEMESTERNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue, "SR.SEMESTERNO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Subject_Wise_Result_Report.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R ON(R.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) AS COURSE_NAME", "R.SCHEMENO =" + ddlScheme.SelectedValue + "AND R.SEMESTERNO=" + ddlSem.SelectedValue, "COURSENO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Subject_Wise_Result_Report.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedIndex > 0)
            {
                ShowReport("Itle_Subject_wise_Student_Result", "Itle_Student_Result_Sub_Wise_Report.rpt");
            }
            else
            {
                objCommon.DisplayUserMessage(updTestResult, "Please select mendatory fields !", this.Page);
                //ShowReportAll("Itle_Student_Result", "Itle_Student_Result_forAll.rpt");
            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "Itle_Subject_Wise_Result_Report.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    //protected void btnShowGrid_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //DataSet ds = objAC.GetTestAllForResult(Convert.ToInt32(ddSubject.SelectedValue));
    //        grdResultReport.DataSource = ds;
    //        grdResultReport.DataBind();
    //        if (ds.Tables[0].Rows.Count > 0)
    //            pnlReport.Visible = true;
    //        trExcelButton.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        objCommon.DisplayUserMessage(UpdatePanel1, "ITLE_StudentResultReport.btnReport_Click->  " + ex.Message + ex.StackTrace, this.Page);
    //    }
    //}

    //protected void imgbutExporttoexcel_Click(object sender, EventArgs e)
    //{
    //    string filename = string.Empty;
    //    string ContentType = string.Empty;
    //    filename = "Result.xls";
    //    ContentType = "ms-excel";
    //    string attachment = "attachment; filename=" + filename;
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/" + ContentType;
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    grdResultReport.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Refresh Page url
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_Subject_Wise_Result_Report.btnCancel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion
}
