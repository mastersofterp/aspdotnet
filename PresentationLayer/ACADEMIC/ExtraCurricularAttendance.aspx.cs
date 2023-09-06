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
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine; //crystal report
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization; //crystal report


public partial class ACADEMIC_ExtraCurricularAttendance : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentAttendanceController objAtt = new StudentAttendanceController();
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
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                this.PopulateDropDownList();
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=sectionWisePDL.aspx");
            }
            Common objCommon = new Common();
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=sectionWisePDL.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //  int usertype = Convert.ToInt32(Session["usertype"].ToString());

            if (Session["usertype"].ToString() == "1")
            {
                objCommon.FillDropDownList(ddlSession, "ACD_ATTENDANCE A INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=A.SESSIONNO", "DISTINCT A.SESSIONNO", "SM.SESSION_PNAME", "", "A.SESSIONNO DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlSession, "ACD_ATTENDANCE A INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=A.SESSIONNO", "DISTINCT A.SESSIONNO", "SM.SESSION_PNAME", "UA_NO=" + Session["userno"].ToString(), "A.SESSIONNO DESC");
            }
            ddlSession.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_TIMETABLE_TimeTable.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddldegree, "ACD_SCHEME SC INNER JOIN ACD_DEGREE D ON D.DEGREENO=SC.DEGREENO", "SC.DEGREENO", "D.DEGREENAME", "SCHEMENO IN(SELECT DISTINCT SCHEMENO FROM ACD_ATTENDANCE)", "");
        }
        else
        {
            objCommon.FillDropDownList(ddldegree, "ACD_SCHEME SC INNER JOIN ACD_DEGREE D ON D.DEGREENO=SC.DEGREENO", "SC.DEGREENO", "D.DEGREENAME", "SCHEMENO IN(SELECT DISTINCT SCHEMENO FROM ACD_ATTENDANCE WHERE UA_NO=" + Session["userno"].ToString() + ")", "");
        }
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_ATTENDANCE A INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=A.SCHEMENO", "DISTINCT A.SCHEMENO", " SC.SCHEMENAME", "SC.DEGREENO=" + ddldegree.SelectedValue, "A.SCHEMENO");
        }
        else
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_ATTENDANCE A INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=A.SCHEMENO", "DISTINCT A.SCHEMENO", " SC.SCHEMENAME", "SC.DEGREENO=" + ddldegree.SelectedValue + " AND A.UA_NO=" + Session["userno"].ToString(), "A.SCHEMENO");
        }
    }



    protected void cbHeadCourse_OnCheckChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem2 in lvCourse.Items)
        {
            CheckBox cbRow = lvitem2.FindControl("cbRow") as CheckBox;
            CheckBox cbHead = lvCourse.FindControl("cbHead") as CheckBox;
            HiddenField hdfIdNo = lvitem2.FindControl("hdfIdNo") as HiddenField;
            if (cbHead.Checked == true)
                cbRow.Checked = true;
            else
                cbRow.Checked = false;
        }
    }

    protected void cbHeadStudent_OnCheckChanged(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem2 in lvStudent.Items)
        {
            CheckBox cbRow = lvitem2.FindControl("cbRow") as CheckBox;
            CheckBox cbHead = lvStudent.FindControl("cbHead") as CheckBox;
            HiddenField hdfIdNo = lvitem2.FindControl("hdfIdNo") as HiddenField;
            if (cbHead.Checked == true)
                cbRow.Checked = true;
            else
                cbRow.Checked = false;
        }
    }

    protected void btnShowAllStudent_Click(object sender, EventArgs e)
    {
        DataSet ds, ds1;
        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (S.IDNO = SR.IDNO AND S.SCHEMENO = SR.SCHEMENO AND S.SEMESTERNO = SR.SEMESTERNO)", "top(1)S.SEMESTERNO", "S.SECTIONNO", "SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND S.FAC_ADVISOR =" + Session["userno"], "SR.IDNO");
        ds1 = objCommon.FillDropDown("ACD_STUDENT_RESULT SR INNER JOIN ACD_STUDENT S ON (S.IDNO = SR.IDNO AND S.SCHEMENO = SR.SCHEMENO AND S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT SR.IDNO", "DBO.FN_DESC('NAME', SR.IDNO) STUDNAME, DBO.FN_DESC('SCHEME', SR.SCHEMENO) SCHEMENAME, DBO.FN_DESC('SEMESTER', SR.SEMESTERNO) SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + "AND ISNULL(SR.CANCEL,0)=0 AND S.SEMESTERNO=" + ds.Tables[0].Rows[0]["SEMESTERNO"].ToString() + " AND S.SECTIONNO=" + ds.Tables[0].Rows[0]["SECTIONNO"].ToString(), "SR.IDNO");
        lvStudent.DataSource = ds1;
        lvStudent.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlsection, "ACD_ATTENDANCE A INNER JOIN ACD_SECTION SEC ON SEC.SECTIONNO=A.SECTIONNO", "DISTINCT A.SECTIONNO", "SEC.SECTIONNAME", "A.SECTIONNO>0", "A.SECTIONNO");
        }
        else
        {
            objCommon.FillDropDownList(ddlsection, "ACD_ATTENDANCE A INNER JOIN ACD_SECTION SEC ON SEC.SECTIONNO=A.SECTIONNO", "DISTINCT A.SECTIONNO", "SEC.SECTIONNAME", "A.UA_NO=" + Session["userno"].ToString() + " AND A.SECTIONNO>0", "A.SECTIONNO");
        }
        //TermWise();
    }

    private void TermWise()
    {
        StudentDetend sd = new StudentDetend();
        DataSet ds, dsStudent = null;

        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR with (nolock) INNER JOIN ACD_STUDENT S ON (S.IDNO = SR.IDNO  AND S.SEMESTERNO = SR.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=SR.SCHEMENO", "DISTINCT SC.SCHEMENAME,SC.SCHEMENO,COURSENO", "COURSENAME+'('+(CASE SUBID WHEN 1 THEN 'Theory' ELSE 'Practical' END)+')' COURSENAME, CCODE", "SR.ACCEPTED=1 AND ISNULL(CANCEL,0)=0 AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SUBID IN (1,2,9) AND isnull(prev_status,0)=0 and SR.SEMESTERNO =" + ddlTerm.SelectedValue + " and (" + ddlScheme.SelectedValue + "=0 or s.schemeno=" + ddlScheme.SelectedValue + ") AND s.degreeno=" + ddldegree.SelectedValue, "SCHEMENAME,COURSENO");
        lvCourse.DataSource = ds;
        lvCourse.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
        dsStudent = sd.getStudentListByCcode(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTerm.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlsection.SelectedValue), 4);
        lvStudent.DataSource = dsStudent;
        lvStudent.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            objCommon.FillDropDownList(ddlTerm, "ACD_ATTENDANCE A INNER JOIN ACD_SEMESTER SEM ON SEM.SEMESTERNO=A.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEM.SEMESTERNAME", "", "A.SEMESTERNO");
        }
        else
        {
            objCommon.FillDropDownList(ddlTerm, "ACD_ATTENDANCE A INNER JOIN ACD_SEMESTER SEM ON SEM.SEMESTERNO=A.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEM.SEMESTERNAME", "A.UA_NO=" + Session["userno"].ToString(), "A.SEMESTERNO");
        }
        // SchemeWise();
    }

    private void SchemeWise()
    {
        DataSet ds = null, ds1 = null;

        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT SR with (nolock) INNER JOIN ACD_STUDENT S ON (S.IDNO = SR.IDNO  AND S.SEMESTERNO = SR.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=SR.SCHEMENO", "DISTINCT SC.SCHEMENAME,SC.SCHEMENO,COURSENO", "COURSENAME+'('+(CASE SUBID WHEN 1 THEN 'Theory' ELSE 'Practical' END)+')' COURSENAME, CCODE", "SR.ACCEPTED=1  AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SUBID IN (1,2,9) and isnull(prev_status,0)=0 and s.schemeno=" + ddlScheme.SelectedValue, "SCHEMENAME,COURSENO");
        lvCourse.DataSource = ds;
        lvCourse.DataBind();

        ds1 = objCommon.FillDropDown("ACD_STUDENT_RESULT SR with (nolock) INNER JOIN ACD_STUDENT S ON (S.IDNO = SR.IDNO AND S.SCHEMENO = SR.SCHEMENO AND S.SEMESTERNO = SR.SEMESTERNO)", "DISTINCT SR.IDNO,SR.ROLL_NO", "DBO.FN_DESC('NAME', SR.IDNO) STUDNAME, DBO.FN_DESC('SCHEME', SR.SCHEMENO) SCHEMENAME, DBO.FN_DESC('SEMESTER', SR.SEMESTERNO) SEMESTERNAME", "SR.SESSIONNO =" + ddlSession.SelectedValue + " AND ISNULL(SR.CANCEL,0)=0 AND S.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue), "SR.ROLL_NO");
        lvStudent.DataSource = ds1;
        lvStudent.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label -
    }

    protected void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
    {
        SectionWise();
    }

    private void SectionWise()
    {
        StudentDetend sd = new StudentDetend();
        DataSet ds, dsStudent = null;

        if (Session["usertype"].ToString() == "1")
        {
            //ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=C.SCHEMENO", "DISTINCT C.COURSE_NAME AS COURSENAME,C.COURSENO,C.SCHEMENO", "SC.SCHEMENAME,C.CCODE", "COURSENO IN(SELECT DISTINCT COURSENO FROM ACD_ATTENDANCE WHERE SESSIONNO =" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlTerm.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SECTIONNO=" + ddlsection.SelectedValue + ")", "C.COURSENO");
            ds = objCommon.FillDropDown("ACD_ATTENDANCE CROSS APPLY DBO.SPLIT(STUDIDS,',') A CROSS APPLY DBO.SPLIT(EXTRA_CURR_STATUS,',') C", "DISTINCT COURSENO,SCHEMENO", "CCODE,DBO.FN_DESC('COURSENAME',COURSENO) AS COURSENAME,DBO.FN_DESC('SCHEMENAME',SCHEMENO) AS SCHEMENAME", "A.ID = C.ID AND A.ID = C.ID AND A.VALUE !='' AND CAST(C.VALUE AS INT)=1", "CCODE");
        }
        else
        {
             ds = objCommon.FillDropDown("ACD_ATTENDANCE CROSS APPLY DBO.SPLIT(STUDIDS,',') A CROSS APPLY DBO.SPLIT(EXTRA_CURR_STATUS,',') C", "DISTINCT COURSENO,SCHEMENO", "CCODE,DBO.FN_DESC('COURSENAME',COURSENO) AS COURSENAME,DBO.FN_DESC('SCHEMENAME',SCHEMENO) AS SCHEMENAME", "A.ID = C.ID AND A.ID = C.ID AND A.VALUE !='' AND CAST(C.VALUE AS INT)=1 AND UA_NO=" + Session["userno"].ToString(), "CCODE");
            //ds = objCommon.FillDropDown("ACD_COURSE C INNER JOIN ACD_SCHEME SC ON SC.SCHEMENO=C.SCHEMENO", "DISTINCT C.COURSE_NAME AS COURSENAME,C.COURSENO,C.SCHEMENO", "SC.SCHEMENAME,C.CCODE", "COURSENO IN(SELECT DISTINCT COURSENO FROM ACD_ATTENDANCE WHERE SESSIONNO =" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlTerm.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue + " AND SECTIONNO=" + ddlsection.SelectedValue + " AND UA_NO=" + Session["userno"].ToString() + ")", "C.COURSENO");
        }

        lvCourse.DataSource = ds;
        lvCourse.DataBind();

        //dsStudent = sd.getStudentListByCcode(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlTerm.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlsection.SelectedValue), 4);
        //lvStudent.DataSource = dsStudent;
        //lvStudent.DataBind();
    }

    private void ShowReport2(string reportTitle, string rptFileName, string studidno, int CondValue)
    {
        int branchno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "schemeno=" + ddlScheme.SelectedValue));
        string frmdate = txtFromDate.Text;
        string todate = txtTodate.Text;
        string Condition = ddlCondtion.SelectedValue;
        string courseno = string.Empty;

        foreach (ListViewDataItem lvitem in lvCourse.Items)
        {
            CheckBox cbRow = lvitem.FindControl("cbRow") as CheckBox;
            HiddenField hdfIdNo = lvitem.FindControl("hdfIdNo") as HiddenField;
            if (cbRow.Checked == true)
            {
                courseno = courseno + hdfIdNo.Value + "$";
            }
        }
        if(courseno=="")
        {
            courseno = "0";
        }
        DataSet ds = objAtt.GetExtraCurriAttendanceStud(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), branchno, Convert.ToInt32(ddlTerm.SelectedValue), frmdate, todate, Convert.ToInt32(txtPercentage.Text), Condition, CondValue, courseno.ToString());
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                    url += "Reports/CommonReport.aspx?";
                    url += "pagetitle=" + reportTitle;
                    url += "&path=~,Reports,Academic," + rptFileName;
                    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +
                        ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                        ",@P_DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) +
                        ",@P_BRANCHNO=" + branchno +
                        ",@P_SEMESTERNO=" + Convert.ToInt32(ddlTerm.SelectedValue) +
                        ",@P_FROMDATE=" + frmdate + ",@P_TODATE=" + todate +
                        ",@P_PERCENTAGE=" + txtPercentage.Text +
                        ",@P_CONDITION=" + Condition +
                        ",@P_CONDVALUE=" + CondValue +
                        ",@P_COURSENO=" + courseno.ToString();
                    //@P_IDNOS=" + studidno + ",

                    // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + ",@P_BRANCHNO=" + branchno + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlTerm.SelectedValue) + ",@P_FROMDATE=" + frmdate + ",@P_TODATE=" + todate + ",@P_PERCENTAGE=" + txtPercentage.Text + ",@P_IDNOS=" + studidno + ",@P_CONDITION=" + Condition + ",@P_CONDVALUE=" + CondValue + ",@P_COURSENO='" + courseno.ToString() + "'";
                    //To open new window from Updatepanel
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                    sb.Append(@"window.open('" + url + "','','" + features + "');");

                    ScriptManager.RegisterClientScriptBlock(this.updpnl_details, this.updpnl_details.GetType(), "controlJSScript", sb.ToString(), true);
                }
                catch (Exception ex)
                {
                    if (Convert.ToBoolean(Session["error"]) == true)
                        objUCommon.ShowError(Page, "ACADEMIC_Student_Result_list.ShowReportTR() --> " + ex.Message + " " + ex.StackTrace);
                    else
                        objUCommon.ShowError(Page, "Server Unavailable.");
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updpnl_details, "No Record Found.", this.Page);
            }
        }
        else
        {
            objCommon.DisplayUserMessage(updpnl_details, "No Record Found.", this.Page);
        }
    }

    protected void btnExtraAttendance_Click(object sender, EventArgs e)
    {
        int CondiValue = 0;
        if (ddlSession.SelectedValue == "0")
        {
            objCommon.ConfirmMessage(updpnl_details, "Select Session", this);
        }
        else if (ddldegree.SelectedValue == "0")
        {
            objCommon.ConfirmMessage(updpnl_details, "Select Degree", this);
        }
        else if (ddlScheme.SelectedValue == "0")
        {
            objCommon.ConfirmMessage(updpnl_details, "Select Scheme", this);
        }
        else if (ddlTerm.SelectedValue == "0")
        {
            objCommon.ConfirmMessage(updpnl_details, "Select Term", this);
        }
        else if (txtFromDate.Text == string.Empty)
        {
            objCommon.ConfirmMessage(updpnl_details, "Enter From Date", this);
        }
        else if (txtTodate.Text == "" && txtTodate.Text == string.Empty)
        {
            objCommon.ConfirmMessage(updpnl_details, "Enter To Date", this);
        }
        else if (ddlCondtion.SelectedValue == "0")
        {
            objCommon.ConfirmMessage(updpnl_details, "Select Condtion", this);
        }
        else if (txtPercentage.Text == string.Empty && txtPercentage.Text == "")
        {
            objCommon.ConfirmMessage(updpnl_details, "Enter Percentage", this);
        }
        else
        {
            string studid1 = string.Empty;
            foreach (ListViewDataItem lvitem in lvStudent.Items)
            {
                CheckBox cbRow = lvitem.FindControl("cbRow") as CheckBox;
                HiddenField hdfIdNo = lvitem.FindControl("hdfIdNo") as HiddenField;
                if (cbRow.Checked == true)
                {
                    studid1 = studid1 + hdfIdNo.Value + "$";
                    //ab_att = ab_att + "0$";
                }
            }

            if (ddlCondtion.SelectedValue == "<") { CondiValue = 1; }
            else if (ddlCondtion.SelectedValue == "<=") { CondiValue = 2; }
            else if (ddlCondtion.SelectedValue == ">") { CondiValue = 3; }
            else if (ddlCondtion.SelectedValue == ">=") { CondiValue = 4; }
            else if (ddlCondtion.SelectedValue == "=") { CondiValue = 5; }
            else { CondiValue = 0; }

            if (string.IsNullOrEmpty(studid1))
            {
                studid1 = "0";
            }
            this.ShowReport2("AttendanceWithExtraCurricular", "rptAttendanceWithExtraCurricular.rpt", studid1, CondiValue);
        }


    }

}
