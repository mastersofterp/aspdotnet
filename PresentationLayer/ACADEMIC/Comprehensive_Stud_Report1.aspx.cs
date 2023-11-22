//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : ASHISH DHAKATE
// MODIFIED DATE : 14/02/2012
// MODIFIED DESC : 
//======================================================================================
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_Comprehensive_Stud_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentController objSc = new StudentController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        imgPhoto.ImageUrl = "~/Images/nophoto.jpg";
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]) + " and ua_no=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;


                if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "14")
                {
                    myModal2.Visible = false;
                    //ddlSession.SelectedIndex = 1;
                    this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + Convert.ToInt32(Session["idno"]), "R.SESSIONNO DESC");
                    ddlSession.SelectedIndex = 1;
                    divStudent.Visible = true;
                    ShowDetails();
                }
                else
                {
                    myModal2.Visible = true;
                    divStudent.Visible = false;
                }
            }
            //Search Pannel Dropdown Added by Swapnil
            this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
           // ddlSearch.SelectedIndex = 0;
            ddlSearch.SelectedIndex = 1;
            txtSearch.Text = string.Empty;
            ddlSearch_SelectedIndexChanged(sender, e);
            //divpanel.Visible = true;
            //pnltextbox.Visible = true;
            //divtxt.Visible = true;
            //End Search Pannel Dropdown
        }
        else
        {
            int count = 0;
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {

                    lvRegStatus.DataSource = null;
                    lvRegStatus.DataBind();
                    lvFees.DataSource = null;
                    lvFees.DataBind();
                    lvCertificate.DataSource = null;
                    lvCertificate.DataBind();
                    lblMsg.Text = string.Empty;

                }
                //if (Convert.ToInt32(ViewState["count"]) == 0)
                //{
                //    int id = 0;
                //    if (ViewState["usertype"].ToString() == "2")
                //    {
                //        id = Convert.ToInt32(Session["userno"].ToString());
                //        this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + id, "R.SESSIONNO DESC");
                //        ddlSession.SelectedIndex = 1;
                //        count++;
                //        ViewState["count"] = count;
                //    }
                //    //else  if (!txtEnrollmentSearch.Text.Trim().Equals(string.Empty))
                //    //{
                //    //    id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO= " + "'" + txtEnrollmentSearch.Text.Trim() + "'"));
                //    //}

                //}
            }
        }
        divMsg.InnerHtml = string.Empty;

    }

    private void ShowDetails()
    {

        Clear();
        int idno = 0;
        StudentController objSC = new StudentController();
        DataSet dsregistration, dsResult, dsFees, dsCertificate, dsRemark, dsRefunds, dsTestMarks, dsAttendance;
        FeeCollectionController feeController = new FeeCollectionController();

        if (ViewState["usertype"].ToString() == "2"  || ViewState["usertype"].ToString() == "14")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                

        }


        try
        {
            int uano = Convert.ToInt32(Session["userno"]);

            if (idno > 0)
            {
                DataTableReader dtr = objSC.GetStudentCompleteDetails(idno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        ViewState["admbatch"] = dtr["ADMBATCH"];
                        string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                        lblBranch.Text = branchname;
                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        //lblGender.Text = dtr["SEX"] == null ? string.Empty : dtr["SEX"].ToString();
                        lblGender.Text = (dtr["SEX"].ToString() == "M" && dtr["SEX"] != null) ? "Male" : "Female";

                        lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                        lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                        string caste = objCommon.LookUp("ACD_CASTE", "CASTE", "CASTENO=" + dtr["CASTE"].ToString());
                        lblCaste.Text = caste;
                        lblPaymentType.Text = dtr["PAYTYPENAME"] == null ? string.Empty : dtr["PAYTYPENAME"].ToString();
                        string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["CATEGORYNO"].ToString());
                        lblCategory.Text = category;
                        string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                        lblReligion.Text = religion;
                        string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                        lblNationality.Text = nation;
                        lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                        //string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                        //lblCity.Text = city;
                        // lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                        // lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString(); //
                        lblMobNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();

                        lblAlternateMobile.Text = dtr["STUDENTMOBILE_ALTERNATE"] == null ? string.Empty : dtr["STUDENTMOBILE_ALTERNATE"].ToString();

                        lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                        lblAadharNumber.Text = dtr["ADDHARCARDNO"] == null ? string.Empty : dtr["ADDHARCARDNO"].ToString();
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        lblApplicationId.Text = dtr["APPLICATIONID"] == null ? string.Empty : dtr["APPLICATIONID"].ToString();
                        string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        lblSemester.Text = semester;
                        //lblCurrentYear.Text = dtr["YEAR"] == null ? string.Empty : dtr["YEAR"].ToString();
                        lblCurrentYear.Text = dtr["YEAR"] == DBNull.Value ? "" : objCommon.LookUp("ACD_YEAR", "YEARNAME", "YEAR=" + dtr["YEAR"].ToString());
                        //city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());

                        lblPTaluka.Text = dtr["PTEHSIL"] == DBNull.Value ? "" : objCommon.LookUp("ACD_TALUKA", "TALUKANAME", "TALUKANO=" + dtr["PTEHSIL"].ToString());
                        lblPDistrict.Text = dtr["PDISTRICT"] == DBNull.Value ? "" : objCommon.LookUp("ACD_DISTRICT", "DISTRICTNAME", "DISTRICTNO=" + dtr["PDISTRICT"].ToString());
                        lblPCity.Text = dtr["PCITY"] == DBNull.Value ? "" : objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());

                        lblRollNo.Text = dtr["ROLLNO"] == null ? string.Empty : dtr["ROLLNO"].ToString();

                        lblAdmBatch.Text = dtr["BATCH"] == null ? string.Empty : dtr["BATCH"].ToString();
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=STUDENT";
                        //added by Aman
                        Session["IDNO_RESULT"] = dtr["IDNO"].ToString();

                        //Added By Dileep kare on 03.08.2021 as per ticket number 24052
                        lblMotherName.Text = dtr["MOTHERNAME"] == null ? string.Empty : dtr["MOTHERNAME"].ToString();
                        lblSchool.Text = dtr["COLLEGE"] == null ? string.Empty : dtr["COLLEGE"].ToString();
                        lblDegree.Text = dtr["DEGREE"] == null ? string.Empty : dtr["DEGREE"].ToString();
                        lblEmailID.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                        lblScheme.Text = dtr["SCHEME"] == null ? string.Empty : dtr["SCHEME"].ToString();
                        lblSection.Text = dtr["SECTION"] == null ? string.Empty : dtr["SECTION"].ToString();
                        lblAdmDate.Text = dtr["ADMDATE"].ToString();
                        lblGuardianName.Text = dtr["GUARDIANNAME"] == null ? string.Empty : dtr["GUARDIANNAME"].ToString();
                        lblFatherAlive.Text = dtr["FATHER_ALIVE"] == "1" && dtr["FATHER_ALIVE"] != null ? "No" : "Yes";

                        lblFatherOccupation.Text = dtr["OCCUPATIONNO"] == DBNull.Value ? "" : objCommon.LookUp("ACD_OCCUPATION", "OCCNAME", "OCCUPATION=" + dtr["OCCUPATIONNO"].ToString());
                        lblFatherIncome.Text = dtr["ANNUAL_INCOME"] == null ? string.Empty : dtr["ANNUAL_INCOME"].ToString();
                        lblMotherOccupation.Text = dtr["MOTHER_OCCUPATIONNO"] == DBNull.Value ? "" : objCommon.LookUp("ACD_OCCUPATION", "OCCNAME", "OCCUPATION=" + dtr["MOTHER_OCCUPATIONNO"].ToString());

                        lblBankName.Text = dtr["BANKNO"] == DBNull.Value ? "" : objCommon.LookUp("ACD_BANK", "BANKNAME", "BANKNO=" + dtr["BANKNO"].ToString());
                        //lblBankName.Text = dtr["BANKNO"] == null ? string.Empty : dtr["BANKNO"].ToString();
                        lblIfscCode.Text = dtr["IFSCCODE"] == null ? string.Empty : dtr["IFSCCODE"].ToString();
                        lblBankAccountNo.Text = dtr["ACC_NO"] == null ? string.Empty : dtr["ACC_NO"].ToString();
                        lblBankAddress.Text = dtr["BANKADDRESS"] == null ? string.Empty : dtr["BANKADDRESS"].ToString();
                        lblAdmStatus.Text = dtr["ADMCAN_1"] == null ? string.Empty : dtr["ADMCAN_1"].ToString();
                        if (dtr["ADMCAN_1"].ToString() == "CANCELLED")
                        {
                            lblAdmStatus.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblAdmStatus.ForeColor = System.Drawing.Color.Green;
                        }

                        //Students Current Registration Details

                        dsregistration = objSC.RetrieveStudentCurrentRegDetails(idno);
                        if (dsregistration.Tables[0].Rows.Count > 0)
                        {
                            lvRegStatus.DataSource = dsregistration;
                            lvRegStatus.DataBind();
                            lvRegStatus.Visible = false;
                        }
                        else
                        {
                            lvRegStatus.DataSource = null;
                            lvRegStatus.DataBind();
                        }


                        //End of Students Current Registration Details

                        //Students Attendance Details

                        string semesterno = dtr["SEMESTERNO"].ToString();
                        string schemeno = dtr["SCHEMENO"].ToString();
                        //string sessionno = Session["currentsession"].ToString();
                        int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno + ""));
                        string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "IS_ACTIVE=1 AND FLOCK=1 AND COLLEGE_ID=" + college_id + "");
                        dsAttendance = objSC.RetrieveStudentAttendanceDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno);


                        if (dsAttendance.Tables[0].Rows.Count > 0)
                        {
                            lvAttendance.DataSource = dsAttendance;
                            lvAttendance.DataBind();
                        }
                        else
                        {
                            lvAttendance.DataSource = null;
                            lvAttendance.DataBind();
                        }


                        DataSet dssession = objCommon.FillDropDown("ACD_TRRESULT T INNER JOIN ACD_SEMESTER SM ON T.SEMESTERNO=SM.SEMESTERNO", "DISTINCT T.SEMESTERNO", "SEMESTERNAME ", "IDNO=" + Convert.ToInt32(idno) + "AND T.RESULTDATE IS NOT NULL", "T.SEMESTERNO");
                        if (dssession.Tables[0].Rows.Count > 0)
                        {
                            rdolistSemester.Items.Clear();

                            for (int i = 0; i < dssession.Tables[0].Rows.Count; i++)
                            {
                                string value = dssession.Tables[0].Rows[i]["SEMESTERNO"].ToString().Trim();
                                string item = dssession.Tables[0].Rows[i]["SEMESTERNAME"].ToString().Trim();
                                rdolistSemester.Items.Add(new ListItem(item, value));
                            }

                        }
                        else
                        {
                            rdolistSemester.Items.Clear();
                        }

                        // End of Students Attendance Details


                        //Student Result Details

                        //dsResult = objSC.RetrieveStudentCurrentResult(idno);
                        //if (dsResult.Tables[0].Rows.Count > 0)
                        //{
                        //    lvResult.DataSource = dsResult;
                        //    lvResult.DataBind();
                        //}
                        //else
                        //{
                        //    lvResult.DataSource = null;
                        //    lvResult.DataBind();
                        //}

                        //End of Student Result Details

                        //Student Result Details New

                        try
                        {
                            //string sscmark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=1");
                            //string Intermark = objCommon.LookUp("ACD_STU_LAST_QUALEXM", "MARKS_OBTAINED", "IDNO=" + idno + " AND QUALIFYNO=2");
                            //DataSet dsSemester = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST", "DISTINCT DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SEMESTERNO,IDNO", "IDNO=" + idno, "SEMESTERNO DESC");
                            //DataSet dsSemester = objSC.RetrieveStudentSemesterMark(idno, Convert.ToInt32(ViewState["SEMESTERNO"]));
                            DataSet dsSemester = objSC.RetrieveStudentSemesterNumberResult(idno);
                            if (dsSemester != null && dsSemester.Tables.Count > 0 && dsSemester.Tables[0].Rows.Count > 0)
                            {

                                //gvParentGrid.DataSource = dsSemester;
                                //gvParentGrid.DataBind();
                                //gvParentGrid.Visible = true;

                            }
                            else
                            {
                                //gvParentGrid.DataSource = null;
                                //gvParentGrid.DataBind();
                                //gvParentGrid.Visible = false;
                            }
                        }


                        catch
                        {
                        }

                        //End of Student Result Details New

                        //Students Fees Details

                        //dsFees = objSC.RetrieveStudentFeesDetails(idno);
                        dsFees = objSC.RetrieveStudentFeesDetails(idno, uano);

                        if (dsFees.Tables[0].Rows.Count > 0)
                        {
                            lvFees.DataSource = dsFees;
                            lvFees.DataBind();
                        }
                        else
                        {
                            lvFees.DataSource = null;
                            lvFees.DataBind();
                        }

                        //End of Students Fees Details

                        //Students Certificate issued Details

                        dsCertificate = objSC.RetrieveStudentCertificateDetails(idno);
                        if (dsCertificate.Tables[0].Rows.Count > 0)
                        {
                            lvCertificate.DataSource = dsCertificate;
                            lvCertificate.DataBind();
                        }
                        else
                        {
                            lvCertificate.DataSource = null;
                            lvCertificate.DataBind();
                        }

                        //End of Students Certificate issued Details

                        //Students class Test Details

                        dsTestMarks = objSC.RetrieveStudentClassTestMarks(idno);
                        if (dsTestMarks.Tables[0].Rows.Count > 0)
                        {


                            gvTestMark.DataSource = dsTestMarks;
                            gvTestMark.DataBind();
                        }
                        else
                        {
                            gvTestMark.DataSource = null;
                            gvTestMark.DataBind();
                        }

                        //End of Students class Test Details

                        //Remark

                        dsRemark = objSC.GetAllRemarkDetails(idno);
                        if (dsRemark != null && dsRemark.Tables.Count > 0 && dsRemark.Tables[0].Rows.Count > 0)
                        {
                            lvRemark.DataSource = dsRemark;
                            lvRemark.DataBind();
                        }
                        else
                        {
                            lvRemark.DataSource = null;
                            lvRemark.DataBind();
                        }

                        // End of Remark


                        //Refund details

                        dsRefunds = objSC.GetStudentRefunds(idno);
                        if (dsRefunds != null && dsRefunds.Tables.Count > 0 && dsRefunds.Tables[0].Rows.Count > 0)
                        {
                            lvRefund.DataSource = dsRefunds;
                            lvRefund.DataBind();
                        }
                        else
                        {
                            lvRefund.DataSource = null;
                            lvRefund.DataBind();
                        }

                        // End of Refund Details

                        // Course Registered Added By Nikhil V.Lambe 
                        DataSet dsCourse = objSC.RetrieveRegDetailsByIdnoAndSession(idno, Convert.ToInt32(sessionno));
                        if (dsCourse != null && dsCourse.Tables.Count > 0 && dsCourse.Tables[0].Rows.Count > 0)
                        {
                            lvCourseReg.DataSource = dsCourse;
                            lvCourseReg.DataBind();
                        }
                        else
                        {
                            lvCourseReg.DataSource = null;
                            lvCourseReg.DataBind();
                        }

                        // End of Course Registered

                        // Internal Marks
                        DataSet dsInternal = objSC.GetDetailsOfInternalMarks(idno, Convert.ToInt32(ddlSession.SelectedValue));
                        if (dsInternal != null && dsInternal.Tables.Count > 0 && dsInternal.Tables[0].Rows.Count > 0)
                        {
                            lvInternalMarks.DataSource = dsInternal;
                            lvInternalMarks.DataBind();
                        }
                        else
                        {
                            lvInternalMarks.DataSource = null;
                            lvInternalMarks.DataBind();
                        }
                        // End of Internal Marks
                        // Attendance Details
                        DataSet dsAttendanceDetails = objSC.GetDetailsOfAttendanceByIdno(idno, Convert.ToInt32(sessionno));
                        if (dsAttendanceDetails != null && dsAttendanceDetails.Tables.Count > 0 && dsAttendanceDetails.Tables[0].Rows.Count > 0)
                        {
                            lvAttendanceDetails.DataSource = dsAttendanceDetails;
                            lvAttendanceDetails.DataBind();
                        }
                        else
                        {
                            lvAttendanceDetails.DataSource = null;
                            lvAttendanceDetails.DataBind();
                        }
                        // End of Attendance Details
                    }
                }
            }
            else
            {
                //objCommon.DisplayMessage("No student found having registration no.: " + txtEnrollmentSearch.Text.Trim(), this.Page);
                lblEnrollNo.Text = string.Empty;
                lblSemester.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        //////Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;
        string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;

        this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistar.rpt", sessionNo, schemeNo, semesterNo, IdNo);
    }

    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, string schemeNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + sessionNo + ",@P_SCHEMENO=" + schemeNo + ",@P_SEMESTERNO=" + semesterNo + ",@P_IDNO=" + idNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void gvParentGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        StudentController objSC = new StudentController();
    //        int admbacth = Convert.ToInt32(ViewState["admbatch"]);
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            GridView gv = (GridView)e.Row.FindControl("gvChildGrid");
    //            GridView gv1 = (GridView)e.Row.FindControl("gvChildGrid1");

    //            HiddenField idno = e.Row.FindControl("hdfIDNo") as HiddenField;
    //            HiddenField SemesterNo = e.Row.FindControl("hdfSemester") as HiddenField;
    //            HiddenField SessionNo = e.Row.FindControl("hdfSession") as HiddenField;
    //          //  HtmlControl htmlDivControl = (HtmlControl)Page.FindControl("aayushi");
    //            HtmlGenericControl div = e.Row.FindControl("divc1") as HtmlGenericControl;
    //            HtmlGenericControl div1 = e.Row.FindControl("divc2") as HtmlGenericControl;
    //            try
    //            {
    //                DataSet ds = objSC.RetrieveStudentCurrentResultFORGRADE(Convert.ToInt32(idno.Value), Convert.ToInt32(SemesterNo.Value), Convert.ToInt32(SessionNo.Value));
    //                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    //if (admbacth <= 4)
    //                    //{
    //                    //    gv.DataSource = ds;
    //                    //    gv.DataBind();
    //                    //    gv.Visible = true;
    //                    //    div1.Visible = false;
    //                    //    div.Visible = true;

    //                    //}
    //                    //else
    //                    //{
    //                    //    gv1.DataSource = ds;
    //                    //    gv1.DataBind();
    //                    //    gv1.Visible = true;
    //                    //    div1.Visible = true;
    //                    //    div.Visible = false;
    //                    //}

    //                    gv.DataSource = ds;
    //                    gv.DataBind();
    //                    gv.Visible = true;
    //                    div1.Visible = false;
    //                    div.Visible = true;
    //                }
    //                else
    //                {
    //                    gv.DataSource = null;
    //                    gv.DataBind();
    //                    gv.Visible = false;
    //                }


    //            }
    //            catch (Exception ex)
    //            {
    //                if (Convert.ToBoolean(Session["error"]) == true)
    //                    objUaimsCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.lvCollege_ItemDatabound() --> " + ex.Message + " " + ex.StackTrace);
    //                else
    //                    objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

    private void Clear()
    {
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblName.Text = string.Empty;
        lblMName.Text = string.Empty;
        lblDOB.Text = string.Empty;
        lblAdmDate.Text = string.Empty;
        lblCaste.Text = string.Empty;
        lblCategory.Text = string.Empty;
        lblReligion.Text = string.Empty;
        lblNationality.Text = string.Empty;
        lblLAdd.Text = string.Empty;
        //lblLLNo.Text = string.Empty;
        lblMobNo.Text = string.Empty;
        lblPAdd.Text = string.Empty;
        imgPhoto.ImageUrl = null;
        lvRegStatus.DataSource = null;
        lvRegStatus.DataBind();
        lvAttendance.DataSource = null;
        lvAttendance.DataBind();
        //gvParentGrid.DataSource = null;
        //gvParentGrid.DataBind();
        //lvResult.DataSource = null;
        //lvResult.DataBind();
        lvFees.DataSource = null;
        lvFees.DataBind();
        lvCertificate.DataSource = null;
        lvCertificate.DataBind();
        gvTestMark.DataSource = null;
        gvTestMark.DataBind();
        lvRemark.DataSource = null;
        lvRemark.DataBind();
        lvRefund.DataSource = null;
        lvRefund.DataBind();
        lblSemester.Text = string.Empty;

    }
    protected void gvTestMark_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

        }
    }
    protected void gvTestMark_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Examination Marks Details";
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            HeaderCell.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");

            gvTestMark.Controls[0].Controls.AddAt(0, HeaderGridRow);

            HeaderGrid.DataSource = null;
            HeaderGrid.DataBind();
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ShowDetails();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report1.ddlSession_TextChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvSession_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        Label lblSession = dataitem.FindControl("lblSession") as Label;
        //Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
        int Semesterno = Convert.ToInt32(rdolistSemester.SelectedValue);

        ViewState["semester"] = Semesterno;
        int idno = Convert.ToInt32(IoNO.ToolTip);
        ViewState["stuidno"] = idno;
        int sessionno = Convert.ToInt32(lblSession.ToolTip);
        ViewState["sessionno"] = sessionno;
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            DataSet ds = objSc.GetSemsesterwiseMarkDetails(idno, sessionno, Semesterno);
            lv.DataSource = ds;
            lv.DataBind();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.lvCollege_ItemDatabound() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void rdolistSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();

    }
    private void BindListView()
    {
        try
        {
            int idno = 0;
            if (Session["usertype"].ToString().Equals("2"))
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                idno = Convert.ToInt32(Session["stuinfoidno"]);
            }



            int orgid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "OrganizationId", "IDNO=" + idno));
            if (orgid == 1)
            {
                DataSet ds = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                DataSet dsreval = objSc.GetSemesterHistoryDetailsForRevalResult(idno, Convert.ToInt32(ViewState["sessionno"]), Convert.ToInt32(rdolistSemester.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlCollege.Visible = true;
                    lvSession.DataSource = ds;
                    lvSession.DataBind();

                }
                else
                {
                    objCommon.DisplayMessage(updStudentInfo, "No Result Found.", this.Page);
                    pnlCollege.Visible = false;
                    lvSession.DataSource = null;
                    lvSession.DataBind();

                }
                if (dsreval.Tables[0].Rows.Count > 0)
                {
                    pnlrevalresult.Visible = true;
                    lvRevalDetails.DataSource = dsreval;
                    lvRevalDetails.DataBind();
                }
                else
                {
                    // objCommon.DisplayMessage(updStudentInfo, "No.", this.Page);
                    pnlrevalresult.Visible = false;
                    lvRevalDetails.DataSource = null;
                    lvRevalDetails.DataBind();
                }
            }
            else if (orgid == 2)
            {
                DataSet ds1 = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lvSession.DataSource = ds1;
                    lvSession.DataBind();
                }
                int sessionno = Convert.ToInt32(ViewState["sessionno"]);   //Convert.ToInt32(rdolistSemester.SelectedValue);
                if (sessionno > 202)
                {
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "COUNT(*)", "SESSIONNO=" + sessionno + " AND ISNULL(RECON,0)=1 AND RECIEPT_CODE='EF' AND IDNO=" + idno));
                    if (count > 0)
                    {

                        DataSet ds = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                        DataSet dsreval = objSc.GetSemesterHistoryDetailsForRevalResult(idno, Convert.ToInt32(ViewState["sessionno"]), Convert.ToInt32(rdolistSemester.SelectedValue));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            pnlCollege.Visible = true;
                            lvSession.DataSource = ds;
                            lvSession.DataBind();

                        }
                        else
                        {
                            objCommon.DisplayMessage(updStudentInfo, "No Result Found.", this.Page);
                            pnlCollege.Visible = false;
                            lvSession.DataSource = null;
                            lvSession.DataBind();
                        }
                        if (dsreval.Tables[0].Rows.Count > 0)
                        {
                            pnlrevalresult.Visible = true;
                            lvRevalDetails.DataSource = dsreval;
                            lvRevalDetails.DataBind();
                        }
                        else
                        {
                            // objCommon.DisplayMessage(updStudentInfo, "No.", this.Page);
                            pnlrevalresult.Visible = false;
                            lvRevalDetails.DataSource = null;
                            lvRevalDetails.DataBind();
                        }
                    }
                    else
                    {
                        lvSession.DataSource = null;
                        lvSession.DataBind();
                        pnlrevalresult.Visible = false;
                        lvRevalDetails.DataSource = null;
                        lvRevalDetails.DataBind();

                    }
                }
                else
                {
                    DataSet ds = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                    DataSet dsreval = objSc.GetSemesterHistoryDetailsForRevalResult(idno, Convert.ToInt32(ViewState["sessionno"]), Convert.ToInt32(rdolistSemester.SelectedValue));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pnlCollege.Visible = true;
                        lvSession.DataSource = ds;
                        lvSession.DataBind();

                    }
                    else
                    {
                        objCommon.DisplayMessage(updStudentInfo, "No Result Found.", this.Page);
                        pnlCollege.Visible = false;
                        lvSession.DataSource = null;
                        lvSession.DataBind();

                    }
                    if (dsreval.Tables[0].Rows.Count > 0)
                    {
                        pnlrevalresult.Visible = true;
                        lvRevalDetails.DataSource = dsreval;
                        lvRevalDetails.DataBind();
                    }
                    else
                    {
                        // objCommon.DisplayMessage(updStudentInfo, "No.", this.Page);
                        pnlrevalresult.Visible = false;
                        lvRevalDetails.DataSource = null;
                        lvRevalDetails.DataBind();
                    }
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_Reports_Comprehensive_Stud_Report.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        FeeCollectionController objfees = new FeeCollectionController();
        Button btnView = sender as Button;
        string Receipt_code = btnView.CommandArgument;
        ListViewDataItem dataitem = btnView.NamingContainer as ListViewDataItem;
        HiddenField hdfSemester = dataitem.FindControl("hdfSemester") as HiddenField;

        int idno = Convert.ToInt32(ViewState["idno"]);
        int Semesterno = Convert.ToInt32(hdfSemester.Value);
        DataSet dsFees = objfees.GetReceiptInfoCompleteDetails(idno, Receipt_code, Semesterno);

        DataTable dtPaidFees = new DataTable();

        dtPaidFees.Columns.Add("Semster", typeof(string));
        dtPaidFees.Columns.Add("REC_NO", typeof(string));
        dtPaidFees.Columns.Add(new DataColumn("REC_DATE", typeof(DateTime)));
        dtPaidFees.Columns.Add("APPLIED_AMT", typeof(string));
        dtPaidFees.Columns.Add("PAID_AMT", typeof(float));
        dtPaidFees.Columns.Add("BAL_AMT", typeof(float));

        double TOTALPAID_AMT = 0;

        if (dsFees.Tables.Count > 0 && dsFees.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsFees.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dtPaidFees.NewRow();
                if (i > 0)
                {
                    if (dsFees.Tables[0].Rows[i - 1]["SEMESTERNAME"].ToString() == dsFees.Tables[0].Rows[i]["SEMESTERNAME"].ToString())
                    {
                        dr["Semster"] = dsFees.Tables[0].Rows[i]["SEMESTERNAME"];
                        dr["REC_NO"] = dsFees.Tables[0].Rows[i]["REC_NO"];
                        dr["REC_DATE"] = dsFees.Tables[0].Rows[i]["REC_DT"];
                        dr["APPLIED_AMT"] = dtPaidFees.Rows[i - 1]["BAL_AMT"];
                        //dr["APPLIED_AMT"] = ds.Tables[0].Rows[i - 1]["BAL_AMT"].ToString();
                        dr["PAID_AMT"] = dsFees.Tables[0].Rows[i]["PAID_AMOUNT"];
                        dr["BAL_AMT"] = Convert.ToDouble(dsFees.Tables[0].Rows[i]["APPLIED_AMOUNT"]) - (this.TOTALPAID_AMOUNT(i, dsFees, dsFees.Tables[0].Rows[i]["SEMESTERNAME"].ToString())); //(Convert.ToDouble(ds.Tables[0].Rows[i - 1]["PAID_AMOUNT"]) + Convert.ToDouble(ds.Tables[0].Rows[i]["PAID_AMOUNT"]));
                    }
                    else
                    {
                        dr["Semster"] = dsFees.Tables[0].Rows[i]["SEMESTERNAME"];
                        dr["REC_NO"] = dsFees.Tables[0].Rows[i]["REC_NO"];
                        dr["REC_DATE"] = dsFees.Tables[0].Rows[i]["REC_DT"];
                        dr["APPLIED_AMT"] = dsFees.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                        dr["PAID_AMT"] = dsFees.Tables[0].Rows[i]["PAID_AMOUNT"];
                        dr["BAL_AMT"] = dsFees.Tables[0].Rows[i]["BAL_AMT"];
                    }
                }
                else
                {
                    dr["Semster"] = dsFees.Tables[0].Rows[i]["SEMESTERNAME"];
                    dr["REC_NO"] = dsFees.Tables[0].Rows[i]["REC_NO"];
                    dr["REC_DATE"] = dsFees.Tables[0].Rows[i]["REC_DT"];
                    dr["APPLIED_AMT"] = dsFees.Tables[0].Rows[i]["APPLIED_AMOUNT"];
                    dr["PAID_AMT"] = dsFees.Tables[0].Rows[i]["PAID_AMOUNT"];
                    dr["BAL_AMT"] = dsFees.Tables[0].Rows[i]["BAL_AMT"];
                }
                dtPaidFees.Rows.Add(dr);
            }

            lvReceipt.DataSource = dtPaidFees;
            lvReceipt.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
        else
        {
            objCommon.DisplayMessage(this.updPopUP, "No Receipt Found.", this.Page);
        }
    }

    private double TOTALPAID_AMOUNT(int I, DataSet dstot, string semester)
    {
        double totpaid_amt = 0;
        for (int j = 0; j <= I; j++)
        {
            if (dstot.Tables[0].Rows[j]["SEMESTERNAME"].ToString() == semester)
            {
                totpaid_amt += Convert.ToDouble(dstot.Tables[0].Rows[j]["PAID_AMOUNT"]);
            }
        }
        return totpaid_amt;
    }

    #region SearchPannel

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;
                        divpanel.Visible = true;
                        
                      
                        //divDropDown.Attributes.Add("style", "display:none");                       
                        //divtxt.Attributes.Add("style", "display:block");
                        //divpanel.Attributes.Add("style", "display:block");
                        

                    }
                }
            }
            else
            {

                //pnltextbox.Visible = false;
                //pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }
    private void bindlist(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtSearch.Text;
        }

        //ddlSearch.ClearSelection();

        bindlist(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;
        divStudent.Visible = false;
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();

        //if (lnk.CommandArgument == null)
        //{
        //    string number = Session["StudId"].ToString();
        //    Session["stuinfoidno"] = Convert.ToInt32 (number);
        //}
        //else
        //{
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        //}
        ViewState["idno"] = Session["stuinfoidno"].ToString();

        this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + Convert.ToInt32(Session["stuinfoidno"]), "R.SESSIONNO DESC");
        if (ddlSession.SelectedIndex > 0)
        {
            ddlSession.SelectedIndex = 1;
        }
        divStudent.Visible = true;
        //Server.Transfer("PersonalDetails.aspx", false);
        divStudent.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        ShowDetails();

    }
    #endregion

    protected void lnkRecieptNo_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnPrint = sender as LinkButton;
            if (btnPrint.CommandArgument != string.Empty)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]));
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void ShowReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo)
    {
        try
        {


            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();

            //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private string GetReportParameters(int dcrNo, int studentNo, string copyNo)
    {
        string param = "@P_DCRNO=" + dcrNo.ToString() + "*MainRpt,@P_IDNO=" + studentNo.ToString() + "*MainRpt,CopyNo=" + copyNo + "*MainRpt";
        return param;
    }
    protected void imgbtnpreview_Click(object sender, ImageClickEventArgs e)
    {

        //StudentController objSc = new StudentController();
        //Label session=FindControl("lblSession")as Label;


        //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        //Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        //Label lblSession = dataitem.FindControl("lblSession") as Label;
        ////Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
        //int Semesterno = Convert.ToInt32(rdolistSemester.SelectedValue);
        //int idno = Convert.ToInt32(IoNO.ToolTip);
        //int sessionno = Convert.ToInt32(lblSession.ToolTip);

        int idno = 0;
        if (ViewState["usertype"].ToString() == "2" || ViewState["usertype"].ToString() == "14")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);

        }
        int degree = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno + " "));
        int cid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno + ""));
        int scheme = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SCHEMENO", "IDNO=" + idno + " "));
        int branch = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno + " "));
        int sem = Convert.ToInt32(rdolistSemester.SelectedValue);//Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno + " "));
        int session = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SESSIONNO)", "IDNO=" + idno + " AND SEMESTERNO=" + rdolistSemester.SelectedValue));

        int OrgId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "OrganizationId", "IDNO=" + idno));

        try
        {

            if (OrgId == 1)
            {
                string reportTitle = "Gradesheet Report";
                string rptFileName = "rptCourseWise_GradeSheet_Rcpit.rpt";
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + session + ",@P_SCHEMENO=" + scheme + ",@P_BRANCHNO=" + branch + ",@P_DEGREENO=" + degree + ",@P_SEMESTERNO=" + sem + ",@P_IDNO=" + idno;

                //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

                //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMSG.InnerHtml += " </script>";

                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else if (OrgId == 2)
            {

                string reportTitle = "Gradesheet Report";
                string rptFileName = "rptCourseWise_GradeSheet.rpt";
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_COLLEGE_ID=" + Convert.ToInt32(cid) + ",@P_SESSIONNO=" + session + ",@P_SCHEMENO=" + scheme + ",@P_BRANCHNO=" + branch + ",@P_DEGREENO=" + degree + ",@P_SEMESTERNO=" + sem + ",@P_IDNO=" + idno;

                //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

                //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMSG.InnerHtml += " </script>";

                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            { }
        }
        catch (Exception ex)
        {
            throw;
        }


    }
}
