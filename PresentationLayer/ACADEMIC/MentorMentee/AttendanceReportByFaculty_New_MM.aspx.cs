using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using ClosedXML.Excel;
using BusinessLogicLayer.BusinessLogic.Academic.MentorMentee;

public partial class ACADEMIC_MentorMentee_AttendanceReportByFaculty_New_MM : System.Web.UI.Page
{
    #region Page Evnets
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController_MM objAttC = new AcdAttendanceController_MM();

    DateTime StartDate, EndDate;
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    //USED FOR INITIALSING THE MASTER PAGE
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    //USED FOR BYDEFAULT LOADING THE default PAGE
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                }
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 30/01/2022
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 30/01/2022
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //used for check requested page authorise or not OR requested page no is authorise or not. if page is authorise then display requested page . if page  not authorise then display bydefault not authorise page. 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AttendenceReportByFaculty.aspx");
        }
    }

    #endregion

    #region Form Methods
    //refresh current page or reload current page
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //refresh current page or reload current page
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    #endregion

    #region Private Methods

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }
    //bind scheme name in drop list
    private void FillScheme(int sessionNo, int uaNo, int subId, int batchNo)
    {
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[4];
            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
            objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
            objParams[2] = new SqlParameter("@P_SUBID", subId);
            objParams[3] = new SqlParameter("@P_BATCHNO", batchNo);
            DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_BY_UA_NO", objParams);

            ddlScheme.DataSource = ds;
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add("Please Select");
            ddlScheme.SelectedItem.Value = "0";
            ddlScheme.DataValueField = ds.Tables[0].Columns["SCHEMENO"].ToString();
            ddlScheme.DataTextField = ds.Tables[0].Columns["SCHEMENAME"].ToString();
            ddlScheme.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    ////bind DEGREE NAME SESSION_PNAME  in drop down list. 
    protected void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3", "SESSIONNO DESC");
            ddlSession.SelectedIndex = 0;
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            if (Session["usertype"].ToString() != "1" && Session["usertype"].ToString() != "3")
            {
                //objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "");
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");

                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "ISNULL(D.ACTIVESTATUS,0) = 1 AND D.DEGREENO>0", "D.DEGREENO");
            }
            else
            {              
                //objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID >0", "");
                objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_SCHEME SC ON(SC.SCHEMENO=SM.SCHEMENO)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COSCHNO");
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND ISNULL(D.ACTIVESTATUS,0) = 1", "D.DEGREENO");
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion
    //set index 0th Please Select for Branch,Scheme,Semester,SubjectType,Section drop down list
    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
    }

    //on selection of session name reset all the controllers
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            this.FillDatesDropDown(ddlSem, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]));
            ddlSem.Focus();
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    //On select of Degree bind Branch name in drop down list
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "DISTINCT B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + Convert.ToInt32(ViewState["degreeno"]), "B.BRANCHNO");

            //on faculty login to get only those dept which is related to logged in faculty
            //if (Convert.ToInt32(Session["BRANCH_FILTER"]) == 1)
            //{
            // objCommon.FilterDataByBranch(ddlBranch, Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userdeptno"]), Convert.ToInt32(Session["BRANCH_FILTER"]));
            //}

            ////bind branch name in drop down list faculty department wise start

            ////DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");
            ////string BranchNos = "";
            ////for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ////{
            ////    BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
            ////}
            ////DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            //////on faculty login to get only those dept which is related to logged in faculty
            ////objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
            ////end
            ddlBranch.Focus();


            ddlScheme.Items.Clear();

            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));

        }
        else
        {
            ClearControls();
        }
        ddlBranch.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "0";
        ddlBranch.Focus();
    }
    //On select of Branch bind Scheme name in drop down list
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //bind semester name in drop down list
    private void FillDatesDropDown(DropDownList ddlsemester, int sessionno, int degree)
    {
        
        try
           
        {
      
            DataSet ds = objAttC.GetSemesterDurationwise(sessionno, degree);

            ddlsemester.Items.Clear();
            ddlsemester.Items.Add("Please Select");
            ddlsemester.SelectedItem.Value = "0";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlsemester.DataSource = ds;
                ddlsemester.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlsemester.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlsemester.DataBind();
                ddlsemester.SelectedIndex = 0;
            }
        }
        catch
        {
            throw;
        }
    }


    ////On select of Scheme reset controlers
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "";

        ddlSem.Focus();
    }


    //On select of Semester name bind SECTION NAME , subject type in drop down list
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());
            objCommon.FillDropDownList(ddlSection, "ACD_COURSE_TEACHER SR INNER JOIN ACD_SECTION SC ON SR.SECTIONNO = SC.SECTIONNO", "DISTINCT SR.SECTIONNO", "SC.SECTIONNAME", "SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]) + " AND SR.SEMESTERNO =" + ddlSem.SelectedValue + " AND SR.SESSIONNO =" + ddlSession.SelectedValue + " AND SR.SECTIONNO > 0 AND SR.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SC.SECTIONNAME");
            objCommon.FillDropDownList(ddlSubjectType, "ACD_OFFERED_COURSE OC INNER JOIN ACD_COURSE C ON OC.COURSENO=C.COURSENO INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SUBID<>9 and OC.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND OC.SEMESTERNO = " + ddlSem.SelectedValue, "C.SUBID");

            if (ddlSem.SelectedIndex > 0)
            {
                //bind Subject name in drop down list
                objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue, "O.COURSENO");

            }
            ddlSubjectType.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlSection.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtTodate.Text = "";
            txtPercentage.Text = "0";
            ddlSubjectType.Focus();

            StartDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
            EndDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
            divDateDetails.Visible = true;
            lblTitleDate.Text = "Selected Session Start Date : " + StartDate.ToShortDateString() + " End Date : " + EndDate.ToShortDateString();

        }
        catch
        {
            lblTitleDate.Text = "Selected Session Start Date : - End Date : -";
        }
    }



    //on select of subject type bind Subject name in drop down list  
    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubject, "ACD_COURSE C INNER JOIN ACD_OFFERED_COURSE O ON (C.COURSENO = O.COURSENO AND C.SCHEMENO = O.SCHEMENO)", "DISTINCT O.COURSENO", "O.CCODE+' - '+C.COURSE_NAME AS COURSENAME", " O.SESSIONNO = " + ddlSession.SelectedValue + " AND O.SCHEMENO = " + Convert.ToInt32(ViewState["schemeno"]) + " AND O.SEMESTERNO  = " + ddlSem.SelectedValue + " AND C.SUBID  = " + ddlSubjectType.SelectedValue, "O.COURSENO");
            ddlSubject.Focus();
        }

        ddlSection.SelectedIndex = 0;
        // ddlSubject.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "0";
        ddlSubject.Focus();
    }


    //on select of Subject reset Section name , From date and To date , percentage
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSection.SelectedIndex = 0;
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "0";
        ddlSection.Focus();
    }

    //on select of Section reset  From date and To date , percentage
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = "";
        txtTodate.Text = "";
        txtPercentage.Text = "0";
        txtFromDate.Focus();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + " AND A.COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])));

            if (Convert.ToDateTime(txtFromDate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
            }
            else if (Convert.ToDateTime(txtFromDate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                txtFromDate.Text = string.Empty;
                txtFromDate.Focus();
            }
            else
            {
                txtTodate.Focus();
            }
        }
        catch
        {
            txtFromDate.Text = string.Empty;
            txtFromDate.Focus();
        }
    }

    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime SDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.START_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));
            DateTime EDate = Convert.ToDateTime(objCommon.LookUp("ACD_ATTENDANCE_CONFIG A INNER JOIN ACD_SCHEME S ON S.DEGREENO=A.DEGREENO AND A.SCHEMETYPE=S.SCHEMETYPE", "CONVERT(VARCHAR(10),A.END_DATE,103)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND A.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)));

            if (Convert.ToDateTime(txtTodate.Text) < SDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be greater than Session Start Date, Start of session is " + SDate.ToShortDateString(), this.Page);
                //  objCommon.DisplayMessage(this.Page, "End date should be greater than session start date", this.Page);
                txtTodate.Text = string.Empty;
                txtTodate.Focus();
            }
            else if (Convert.ToDateTime(txtTodate.Text) > EDate)
            {
                objCommon.DisplayMessage(this.Page, "Selected Date should be less than Session End Date, End of session is " + EDate.ToShortDateString(), this.Page);
                // objCommon.DisplayMessage(this.Page, "End date should be less than session end date", this.Page);
                txtTodate.Text = string.Empty;
                txtTodate.Focus();
            }
            else
            {
                ddlOperator.Focus();
            }
        }
        catch
        {
            txtTodate.Text = string.Empty;
            txtTodate.Focus();
        }
    }

    //showing the subject wise report in rptSubjectwiseAttendance.rpt file  in Exel or pdf formate.
    //protected void btnSubjectwise_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (rdoReportType.SelectedValue == "xls")
    //        //{
    //        //    ShowReportinFormate(rdoReportType.SelectedValue, "rptSubjectwiseAttendance_New.rpt");
    //        //}
    //        //else
    //        //{
    //        ShowReport("Subject Wise Attendance", "rptSubjectwiseAttendance_New_MM.rpt");
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    //function to show the report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToString(ViewState["college_id"]) 
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
                + ",username=" + Session["userfullname"].ToString() 
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) 
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) 
                + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") 
                + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") 
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) 
                + ",@P_CONDITIONS=" + ddlOperator.SelectedValue 
                + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() 
                + ",@P_SUBID=" + ddlSubjectType.SelectedValue 
                + ",@P_COURSENO=" + ddlSubject.SelectedValue 
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"]);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReportinFormate(string exporttype, string rptFileName)
    {
        try
        {
            string bname = objCommon.LookUp("acd_branch", "shortname", "branchno=" + Convert.ToInt32(ViewState["branchno"]));

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_COURSENO=" + ddlSubject.SelectedValue;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + bname + "_" + ddlSem.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_COURSENO=" + ddlSubject.SelectedValue + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ShowReportinFormateSubject(string exporttype, string rptFileName)
    {
        try
        {
            string bname = objCommon.LookUp("acd_branch", "shortname", "branchno=" + Convert.ToInt32(ViewState["branchno"]));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlDegree.SelectedItem.Text + "_" + bname + "_" + ddlSem.SelectedItem.Text + "_" + ddlSection.SelectedItem.Text + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",username=" + Session["userfullname"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) + ",@P_CONDITIONS=" + ddlOperator.SelectedValue + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_COURSENO=" + ddlSubject.SelectedValue + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    ////showing the Attendance wise report in rptAttendanceForDisplay.rpt file  in Exel or pdf formate.
    protected void btnSubjectwiseExpected_Click(object sender, EventArgs e)
    {
        try
        {
            //if (rdoReportType.SelectedValue == "xls")
            //{
            //    ShowReportinFormateSubject(rdoReportType.SelectedValue, "rptAttendanceForDisplay_Excel.rpt");
            //}
            //else
            //{
            ShowReport("Display Attendance", "rptAttendanceForDisplay_MM.rpt");
            //}
        }
        catch (Exception ex)
        {
            throw;
        }

    }

  //  showing the Attendance Details report on rptAttendanceDetails.rpt file .
    //protected void btnAttDetails_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.AttDetailsReport("Attendance Details", "rptAttendanceDetails.rpt");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void AttDetailsReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
                + ",username=" + Session["userfullname"].ToString() 
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) 
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) 
                + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") 
                + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") 
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) 
                + ",@P_CONDITIONS=" + ddlOperator.SelectedValue 
                + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim() 
                + ",@P_SUBID=" + ddlSubjectType.SelectedValue 
                + ",@P_COURSENO=" + ddlSubject.SelectedValue 
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"])
                + ",@P_ADTEACHER=" + Convert.ToInt32(Session["userno"]);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //showing the Cumulative Attendance Details report on rptAttendanceDetails.rpt file .
    //protected void btnCumulativeAttendance_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (rdoReportType.SelectedValue == "pdf")
    //        //{
    //        this.CumulativeAttDetailsReport("Cumulative Attendance Details", "rptTotalAttendanceDetails_MM.rpt");
    //        //}
    //        //else
    //        //{
    //        //    GridView GVStudData = new GridView();
    //        //  DataSet ds = objAttC.GetCumulativeAttDetails(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]),
    //        //        Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text),
    //        //         Convert.ToDateTime(txtTodate.Text), Convert.ToInt32(ViewState["college_id"]));

    //        //    if (ds.Tables[0].Rows.Count > 0)
    //        //    {
    //        //        GVStudData.DataSource = ds;
    //        //        GVStudData.DataBind();

    //        //        string attachment = "attachment;filename=CumulativeAttendanceDetails.xls";
    //        //        Response.ClearContent();
    //        //        Response.AddHeader("content-disposition", attachment);
    //        //        Response.Charset = "";
    //        //        Response.ContentType = "application/ms-excel";
    //        //        StringWriter sw = new StringWriter();
    //        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        //        GVStudData.RenderControl(htw);
    //        //        Response.Write(sw.ToString());
    //        //        Response.End();
    //        //    }
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void CumulativeAttDetailsReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]) 
                + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) 
                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) 
                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) 
                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue) 
                + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") 
                + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") 
                + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"])
                + ",@P_FAC_ADVISOR=" + Convert.ToInt32(Session["userno"]);
            
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //protected void btnAttReportWithOD_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (rdoReportType.SelectedValue == "pdf")
    //        //{
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + "AttReportWithOD";
    //        url += "&path=~,Reports,Academic," + "rptAttReportWithOD_MM.rpt";

    //        url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"])
    //            + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
    //            + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"])
    //            + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
    //            + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
    //            + ",@P_SUBID=" + ddlSubjectType.SelectedValue
    //            + ",@P_COURSENO=" + ddlSubject.SelectedValue
    //            + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
    //            + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd")
    //            + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd")
    //            + ",@P_CONDITIONS=" + ddlOperator.SelectedValue
    //            + ",@P_PERCENTAGE=" + txtPercentage.Text.Trim()
    //            + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + "AttReportWithOD" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";
    //        //}
    //        //else
    //        //{
    //        //    GridView GVStudData = new GridView();
    //        //    DataSet ds = objAttC.GetAttReportWithOD(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]),
    //        //        Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue),
    //        //         Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text),
    //        //         Convert.ToDateTime(txtTodate.Text), ddlOperator.SelectedValue, Convert.ToInt32(txtPercentage.Text), Convert.ToInt32(ViewState["college_id"]));

    //        //    if (ds.Tables[0].Rows.Count > 0)
    //        //    {
    //        //        GVStudData.DataSource = ds;
    //        //        GVStudData.DataBind();

    //        //        string attachment = "attachment;filename=ListOfOD_Applied_Students.xls";
    //        //        Response.ClearContent();
    //        //        Response.AddHeader("content-disposition", attachment);
    //        //        Response.Charset = "";
    //        //        Response.ContentType = "application/ms-excel";
    //        //        StringWriter sw = new StringWriter();
    //        //        HtmlTextWriter htw = new HtmlTextWriter(sw);
    //        //        GVStudData.RenderControl(htw);
    //        //        Response.Write(sw.ToString());
    //        //        Response.End();
    //        //    }
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    // MODIFIED BY SAFAL GUPTA ON 28042021

    //protected void btnFacultyIncompleteAttendance_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(txtTodate.Text))
    //        {
    //            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //            url += "Reports/CommonReport.aspx?";
    //            url += "pagetitle=" + "Faculty Incomplete Attendance";
    //            url += "&path=~,Reports,Academic," + "rptFacultyIncompleteAttendance_MM.rpt";

    //            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)
    //                + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"])
    //                + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue)
    //                + ",@P_SECTIONNO=" + Convert.ToInt32(ddlSection.SelectedValue)
    //                + ",@P_COURSENO=" + Convert.ToInt32(ddlSubject.SelectedValue)
    //                + ",@P_FROMDATE=" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd")
    //                + ",@P_TODATE=" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd")
    //                + ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);

    //            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //            divMsg.InnerHtml += " window.open('" + url + "','" + "Faculty Incomplete Attendance" + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //            divMsg.InnerHtml += " </script>";

    //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //            sb.Append(@"window.open('" + url + "','','" + features + "');");

    //            ScriptManager.RegisterClientScriptBlock(this.updSection, this.updSection.GetType(), "controlJSScript", sb.ToString(), true);

    //            // ",@P_COLLEGE_ID="+ Convert.ToInt32(ddlInstitute.SelectedValue) +
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(this.updSection, "Please Select Proper Date (From Date Should be less than To Date)!!", this.Page);
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected void btnODExcelReport_Click(object sender, EventArgs e)
    //{
    //    this.ShowODReportInExcel();
    //}

    //private void ShowODReportInExcel()
    //{
    //    try
    //    {
    //        GridView GVStudData = new GridView();
    //        DataSet ds = objAttC.GetODApproveStudentCount(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]),
    //             Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            GVStudData.DataSource = ds;
    //            GVStudData.DataBind();

    //            string attachment = "attachment;filename=StudentsODApprovedList.xls";
    //            Response.ClearContent();
    //            Response.AddHeader("content-disposition", attachment);
    //            Response.Charset = "";
    //            Response.ContentType = "application/ms-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            GVStudData.RenderControl(htw);
    //            Response.Write(sw.ToString());
    //            Response.End();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInstitute.SelectedIndex > 0)
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlInstitute.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
            ddlSession.Focus();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 and ODD_EVEN<>3 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            if (Session["usertype"].ToString() != "1")
            {
                string dec = objCommon.LookUp("USER_ACC WITH (NOLOCK)", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B  WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0 AND B.DEPTNO =" + Session["userdeptno"].ToString() + " AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
            else
            {
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]), "D.DEGREENO");
            }
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            ddlSubjectType.Items.Clear();
            ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
            ddlSubject.Items.Clear();
            ddlSubject.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    #region Subject Wise Details Excel Report

    //added by jay takalkhede on dated 21042023 for tkt 41327
    protected void btnSubjectwiseExpectedExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());
            GridView GVStudData = new GridView();
            DataSet ds = objAttC.GetSubjectWiseDetailsExcelReport(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtTodate.Text), ddlOperator.SelectedValue, Convert.ToInt32(txtPercentage.Text.Trim()), Convert.ToInt32(ddlSubject.SelectedValue), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count > 0)
            {

                ds.Tables[0].TableName = "SubjectWiseDetails";
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        if (dt != null && dt.Rows.Count > 0)
                            wb.Worksheets.Add(dt);
                    }

                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Subject_Wise_Details_Excel_Report.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updSection, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    #endregion
}
    
