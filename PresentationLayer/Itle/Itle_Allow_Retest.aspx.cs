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

public partial class Itle_Itle_Allow_Retest : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TestResult objResult = new TestResult();

    ITestMasterController objTC = new ITestMasterController();
    ITestMaster objTM = new ITestMaster();

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

                //pnlRetestList.Visible = true;
            }
            fillSession();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_Allow_Retest.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_Allow_Retest.aspx");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain
        DataSet ds = null;

        try
        {

            ds = objTC.getDataforRetest(Convert.ToInt16(ddlTest.SelectedValue), Convert.ToInt16(ddlScheme.SelectedValue), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(Ddlsession.SelectedValue), Convert.ToInt16(ddlCourse.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvretest.DataSource = ds;
                lvretest.DataBind();
                DivRetestGrid.Visible = true;
                //pnlRetestList.Visible = true;
            }
            else
            {
                lvretest.DataSource = null;
                lvretest.DataBind();
                DivRetestGrid.Visible = false;
                //pnlRetestList.Visible = false;
            }

            //  objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S JOIN ACD_ITEST_RESULT TR ON (S.IDNO=TR.IDNO) JOIN ACD_STUDENT_RESULT SR ON (SR.IDNO=TR.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=" + ddlSem.SelectedValue + "AND SR.SESSIONNO=" + Ddlsession.SelectedValue + "AND TR.COURSENO=" + ddlCourse.SelectedValue + "AND TR.TESTNO=" + ddlTest.SelectedValue, "IDNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    #endregion

    #region Private Method

    protected void fillSession()
    {

        if (Convert.ToInt32(Session["usertype"]) == 2)
        {
            objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3)", "SESSIONNO DESC");
        }
        else if (Convert.ToInt32(Session["usertype"]) == 3 || (Convert.ToInt32(Session["usertype"]) == 5))
        {

            objCommon.FillDropDownList(Ddlsession, "ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)  ", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "SESSIONNO>0 AND EXAMTYPE IN (1,3) and B.COLLEGE_ID in (" + (Session["college_nos"]) + ")", "SESSIONNO DESC");

        }
        //DataSet dsSession = new DataSet();
        //dsSession = objCommon.FillDropDown("ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
        //Ddlsession.Items.Clear();
        //Ddlsession.Items.Add("Please Select");
        //Ddlsession.SelectedItem.Value = "0";
        //Ddlsession.DataTextField = "SESSION_NAME";
        //Ddlsession.DataValueField = "SESSIONNO";
        //Ddlsession.DataSource = dsSession.Tables[0];
        //Ddlsession.DataBind();
        //Ddlsession.SelectedIndex = 0;
    }

    protected void getMainCourse()
    {
       // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        objCommon.FillDropDownList(ddlDegree, "ACD_STUDENT_RESULT A INNER JOIN ACD_SCHEME S ON (A.SCHEMENO = S.SCHEMENO)INNER JOIN ACD_DEGREE B ON (S.DEGREENO = B.DEGREENO)", "DISTINCT B.DEGREENO  ", "DEGREENAME", " SESSIONNO= " + Ddlsession.SelectedValue + "", "");
    
       }

    private void BindListView()  
    {
        try
        {
            //CourseController objCourse = new CourseController();

            DataSet ds = null;

            if (Convert.ToInt32(Session["usertype"]) == 2) { }
            //  ds = objTC.GetStatusByStudent(Convert.ToInt32(ddlStudent.SelectedValue), Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]));

            //lvStudent.DataSource = ds;
            //lvStudent.DataBind();
            //trStudent.Visible = true;
            //  lblStudent.Text = ddlStudent.SelectedItem.Text.Trim();
            //
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_selectCourse.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Ddlsession.SelectedValue + ",@P_COURSENO=" + ddlCourse.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SEMESTERNO=" + ddlSem.SelectedValue + ",@P_TESTNO=" + ddlTest.SelectedValue + ",COURSE_NAME=" + "";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AnnouncementMaster.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    public void ClearControls()
    {

        Ddlsession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlTest.SelectedIndex = 0;
        // cbAllow.Checked = false;

        DivRetestGrid.Visible = false;
        //ddlStudent.SelectedIndex = 0;
        //lblID_Required.Text = string.Empty;
        //lblName_Required.Text = string.Empty;
    }

    #endregion

    #region Selected Index Changed

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

                //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO =" + ddlDegree.SelectedValue, "LONGNAME"); //Previously it was like this
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (DB.BRANCHNO = B.BRANCHNO)", "B.BRANCHNO", "B.LONGNAME ", "DB.DEGREENO =" + ddlDegree.SelectedValue, "B.LONGNAME ASC"); //Added on 03/07/2018
                ddlBranch.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO =" + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.ShowError(Page, "Registration_courseAllot.ddlScheme_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE", "DISTINCT COURSENO", "COURSE_NAME", "SCHEMENO =" + ddlScheme.SelectedValue + "AND SEMESTERNO=" + ddlSem.SelectedValue, "COURSENO");
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT R ON(R.COURSENO=C.COURSENO)", "DISTINCT C.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) AS COURSE_NAME", "R.SCHEMENO =" + ddlScheme.SelectedValue + "AND R.SEMESTERNO=" + ddlSem.SelectedValue, "COURSENO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        {
            try
            {

                objCommon.FillDropDownList(ddlTest, "ACD_ITESTMASTER TM JOIN ACD_ITEST_RESULT TR ON (TM.TESTNO=TR.TESTNO)", "DISTINCT TM.TESTNO", "TM.TESTNAME+'      '+(CASE WHEN TM.TEST_TYPE='O' THEN '(OBJECTIVE)' ELSE '(DESCRIPTIVE)' END)AS TESTNAME", "TR.COURSENO=" + ddlCourse.SelectedValue, "TESTNO");


            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }


        }
    }

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

        try
        {

            ds = objTC.getDataforRetest(Convert.ToInt16(ddlTest.SelectedValue), Convert.ToInt16(ddlScheme.SelectedValue), Convert.ToInt16(ddlSem.SelectedValue), Convert.ToInt16(Ddlsession.SelectedValue), Convert.ToInt16(ddlCourse.SelectedValue));

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvretest.DataSource = ds.Tables[0];
                lvretest.DataBind();
                DivRetestGrid.Visible = true;
                pnlRetestList.Visible = true;
                //fldallowretest.Visible = true;
                // ViewState["ua_no"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                //ViewState["count"] =ds.Tables[0].Rows[0]["count"].ToString()
            }
            else
            {
                lvretest.DataSource = null;
                lvretest.DataBind();
                DivRetestGrid.Visible = false;
                pnlRetestList.Visible = false;
                //fldallowretest.Visible = false;
            }

            //  objCommon.FillDropDownList(ddlStudent, "ACD_STUDENT S JOIN ACD_ITEST_RESULT TR ON (S.IDNO=TR.IDNO) JOIN ACD_STUDENT_RESULT SR ON (SR.IDNO=TR.IDNO)", "DISTINCT S.IDNO", "S.STUDNAME", "SR.SCHEMENO =" + ddlScheme.SelectedValue + "AND SR.SEMESTERNO=" + ddlSem.SelectedValue + "AND SR.SESSIONNO=" + Ddlsession.SelectedValue + "AND TR.COURSENO=" + ddlCourse.SelectedValue + "AND TR.TESTNO=" + ddlTest.SelectedValue, "IDNO");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }



    }

    #endregion

    #region Page Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int flag = 0;
        try
        {

           


            objTM.COURSENO = Convert.ToInt32(ddlCourse.SelectedValue);
            objTM.TEST_NO = Convert.ToInt32(ddlTest.SelectedValue);
            objTM.SESSIONNO = Convert.ToInt32(Ddlsession.SelectedValue);
         // objTM.UA_NO = Convert.ToInt32(ViewState["ua_no"]);
                  

            objTM.COLLEGE_CODE = Session["colcode"].ToString().Trim();

            //objTM.STUDNAME = ddlStudent.SelectedItem.Text.Trim();



            Label STUDNAME;
            Label COUNT;
            Label lblreqdate;
            CheckBox chkall;

            foreach (ListViewDataItem item in lvretest.Items)
            {

                STUDNAME = (Label)item.FindControl("lblstudname");
                COUNT = (Label)item.FindControl("lblcount");
                lblreqdate = (Label)item.FindControl("lblreqdate");
                chkall = (CheckBox)item.FindControl("chkstatus");

                if (chkall.Checked.Equals(true))
                {
                    objTM.Uano += COUNT.Text + ",";
                    objTM.STUDNAME += STUDNAME.Text + ",";
                    objTM.Studidno += STUDNAME.ToolTip + ",";
                    if (!string.IsNullOrEmpty(lblreqdate.ToolTip))
                    {
                        objTM.Reqno += lblreqdate.ToolTip + ",";
                    }
                    else
                    {
                        objTM.Reqno += "0" + ",";

                    }
                }

            }

            CustomStatus cs = (CustomStatus)objTC.AddStudInfo(objTM);

            //if (flag == 1)
            //{
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(UpdPnlRetest, "Retest Allowed Successfully!!", this.Page);
                //objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, Common.Message.Saved, Common.MessageType.Success);
                //BindListView();
                //  lblAllowRetest.Text = string.Empty;
                ClearControls();

            }
            else
                objCommon.DisplayUserMessage(this.UpdPnlRetest, "Error Occured!!", this.Page);

            ClearControls();
            //}





        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Allow_Retest.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearControls();

        //Response.Redirect("Itle_Allow_Retest.aspx");

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourse.SelectedValue != "0")
            {
                ShowReport("Itle_Allow_Retest_Report", "Itle_AllowRetest_Report.rpt");
            }
            else
            {
                objCommon.DisplayUserMessage(Page, "Please select Course", this.Page);

            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AnnouncementMaster.btnViewTeachingPlan_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #endregion

    #region Commented Code

    //private void FillDropdown()
    //{
    //    DataSet ds = null;
    //    try
    //    {
    //        objCommon.FillDropDownList(ddlStudent, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");
    //        ds = objCommon.FillDropDown("ACD_SESSION_MASTER", " Top 1 SESSION_NAME", "SESSIONNO", "SESSIONNO>0", "SESSIONNO DESC");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlStudent.SelectedValue = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
    //            BindListView();

    //            Session["SessionNo"] = Convert.ToInt32(ddlStudent.SelectedValue);
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        objUCommon.ShowError(Page, "selectCourse.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
    //    }
    //}

    #endregion

}
