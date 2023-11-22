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
using System.IO;
using BusinessLogicLayer.BusinessLogic.Academic.MentorMentee;

public partial class ACADEMIC_MentorMentee_Comprehensive_Stud_Report_MM : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
   // StudentController objSc = new StudentController();
    StudentControllerDetails_MM objSc = new StudentControllerDetails_MM();

    int uano = 0;



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
                objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                BindStudentListview(uano);

                //bindlist(string category, string searchtext, int uano);
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


                if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
                {
                    myModal2.Visible = false;
                    //ddlSession.SelectedIndex = 1;
                    this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + Convert.ToInt32(Session["idno"]), "R.SESSIONNO DESC");
                    if (ddlSession.Items.Count > 1)
                    {
                        ddlSession.SelectedIndex = 1;
                    }
                    divStudent.Visible = true;
                    ShowDetails();
                   
                }
                else
                {
                    myModal2.Visible = true;
                    divStudent.Visible = false;
                }
            }
            if (ViewState["usertype"].ToString() != "2")
            {
                if ((ViewState["usertype"].ToString() != "14"))
                {
                    //Search Pannel Dropdown Added by Swapnil
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);
                }
            }
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
            {
                lifatheralive.Visible = true;
            }
            else
            {
                lifatheralive.Visible = false;
            }
            //End Search Pannel Dropdown
        }
        else
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());
            int count = 0;
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1],uano);
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
        //StudentController objSC = new StudentController();
        StudentControllerDetails_MM objSC = new StudentControllerDetails_MM(); 

        DataSet dsregistration, dsResult, dsFees, dsCertificate, dsRemark, dsRefunds, dsTestMarks, dsAttendance;
        FeeCollectionController feeController = new FeeCollectionController();

        if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                

        }

        Session["stuinfoidno"] = idno;
        int uano = Convert.ToInt32(Session["userno"]);
        try
        {
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

                        //lblPTaluka.Text = dtr["PTEHSIL"] == DBNull.Value ? "" : objCommon.LookUp("ACD_TALUKA", "TALUKANAME", "TALUKANO=" + dtr["PTEHSIL"].ToString());
                        lblPTaluka.Text = dtr["PTEHSIL"] == null ? string.Empty : dtr["PTEHSIL"].ToString();
                        lblPDistrict.Text = dtr["PDISTRICT"] == DBNull.Value ? "" : objCommon.LookUp("ACD_DISTRICT", "DISTRICTNAME", "DISTRICTNO=" + dtr["PDISTRICT"].ToString());
                        lblPCity.Text = dtr["PCITY"] == DBNull.Value ? "" : objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());

                        lblRollNo.Text = dtr["ROLLNO"] == null ? string.Empty : dtr["ROLLNO"].ToString();

                        lblAdmBatch.Text = dtr["BATCH"] == null ? string.Empty : dtr["BATCH"].ToString();
                        lblAcademicYear.Text = dtr["ACADEMIC_YEAR_NAME"] == null ? string.Empty : dtr["ACADEMIC_YEAR_NAME"].ToString();
                        lblAdmissionType.Text = dtr["IDTYPEDESCRIPTION"] == null ? string.Empty : dtr["IDTYPEDESCRIPTION"].ToString();
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
                        lblAllotedCategory.Text = dtr["ALLOTED_CATEGORY"].ToString();
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
                        lblFatherMobile.Text = dtr["FATHERMOBILE"] == null ? string.Empty : dtr["FATHERMOBILE"].ToString();
                        lblMotherMobile.Text = dtr["MOTHERMOBILE"] == null ? string.Empty : dtr["MOTHERMOBILE"].ToString();
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
                        //string sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "IS_ACTIVE=1 AND FLOCK=1 AND COLLEGE_ID=" + college_id + "");
                        string sessionno = objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(MAX(SESSIONNO),0) AS SESSIONNO", " IDNO=" + idno + "");
                        //string sessionno = ddlSession.SelectedValue;
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

                        if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
                        {
                            DataSet dsYearFees = objSC.RetrieveStudentYearWiseFeesDetails(idno);
                            if (dsYearFees != null && dsYearFees.Tables.Count > 0 && dsYearFees.Tables[0].Rows.Count > 0)
                            {
                                lvYearFeesDetails.DataSource = dsYearFees;
                                lvYearFeesDetails.DataBind();
                                divYearWiseFees.Visible = true;
                            }
                            else
                            {
                                lvYearFeesDetails.DataSource = null;
                                lvYearFeesDetails.DataBind();
                            }

                            DataSet dsOtherFees = objSc.RetrieveOtherFeeDetails(idno);
                            if (dsOtherFees != null && dsOtherFees.Tables.Count > 0 && dsOtherFees.Tables[0].Rows.Count > 0)
                            {
                                lvOtherFees.DataSource = dsOtherFees;
                                lvOtherFees.DataBind();
                                divOtherFees.Visible = true;
                            }
                            else
                            {
                                lvOtherFees.DataSource = null;
                                lvOtherFees.DataBind();
                            }
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
                        //Commented because not in use grid view also display none in designing
                        //dsTestMarks = objSC.RetrieveStudentClassTestMarks(idno);
                        //if (dsTestMarks.Tables[0].Rows.Count > 0)
                        //{


                        //    gvTestMark.DataSource = dsTestMarks;
                        //    gvTestMark.DataBind();
                        //}
                        //else
                        //{
                        //    gvTestMark.DataSource = null;
                        //    gvTestMark.DataBind();
                        //}

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
                        if (Convert.ToInt32(Session["OrgId"]) == 1)
                        {
                            DataSet dsStudentPro = objSC.Getpromotionstatus(idno);
                            if (dsStudentPro != null && dsStudentPro.Tables.Count > 0 && dsStudentPro.Tables[0].Rows.Count > 0)
                            {
                                lvProt.DataSource = dsStudentPro;
                                lvProt.DataBind();
                            }
                            else
                            {
                                lvProt.DataSource = null;
                                lvProt.DataBind();
                            }
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
            StudentControllerDetails_MM objSC = new StudentControllerDetails_MM(); 

           // StudentController objSC = new StudentController();
            int idno = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                idno = Convert.ToInt32(ViewState["idno"]);
                //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
                //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");
            }
            ShowDetails();
            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
            {
                getinternalmarks();
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 9)
            {

                DataSet dsInternal = objSC.GetDetailsOfGradeRealease(idno, Convert.ToInt32(ddlSession.SelectedValue));
                if (dsInternal != null && dsInternal.Tables.Count > 0 && dsInternal.Tables[0].Rows.Count > 0)
                {
                    lvGradeRealease.DataSource = dsInternal;
                    lvGradeRealease.DataBind();
                }
                else
                {
                    lvGradeRealease.DataSource = null;
                    lvGradeRealease.DataBind();
                }


            }
            else if (Convert.ToInt32(Session["OrgId"]) == 8)
            {

                divMITExcel.Visible = true;
            }
            else
            {

            }
            DataSet dsCourse = objSC.RetrieveRegDetailsByIdnoAndSession(idno, Convert.ToInt32(ddlSession.SelectedValue));
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
            Label lblstatus = dataitem.FindControl("lblstatus") as Label;
            int studtype = Convert.ToInt32(lblstatus.ToolTip);

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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);
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
                        // lvSession.Columns.RemoveAt(3);
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
            else if (orgid == 6)
            {

                DataSet ds = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                DataSet dsreval = objSc.GetSemesterHistoryDetailsForRevalResult(idno, Convert.ToInt32(ViewState["sessionno"]), Convert.ToInt32(rdolistSemester.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    pnlCollege.Visible = true;
                    lvSession.DataSource = ds;
                    lvSession.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#printreport').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#printreport').hide();$('td:nth-child(10)').hide();});", true);

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
                DataSet ds = objSc.GetSemesterHistoryDetails(idno, Convert.ToInt32(rdolistSemester.SelectedValue));
                DataSet dsreval = objSc.GetSemesterHistoryDetailsForRevalResult(idno, Convert.ToInt32(ViewState["sessionno"]), Convert.ToInt32(rdolistSemester.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {


                    pnlCollege.Visible = true;
                    lvSession.DataSource = ds;
                    lvSession.DataBind();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#printreport').hide();$('td:nth-child(10)').hide();var prm =Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#printreport').hide();$('td:nth-child(10)').hide();});", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#printreport').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('#printreport').hide();$('td:nth-child(10)').hide();});", true);

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
    public void getinternalmarks()
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                

        }
        StudentControllerDetails_MM objSC = new StudentControllerDetails_MM(); 

        //StudentController objSC = new StudentController();
        DataSet dsInternal = objSC.GetDetailsOfInternalMarksHeader(idno, Convert.ToInt32(ddlSession.SelectedValue));
        ViewState["dshead"] = dsInternal;
        DataTable dt = new DataTable();



        if (dsInternal != null && dsInternal.Tables.Count > 0 && dsInternal.Tables[0].Rows.Count > 0)
        {
            dt = dsInternal.Tables[0];
            DataRow[] dr = dt.Select("");
            string str = string.Empty;
            string str1 = string.Empty;
            int td = 0;
            int colcont = dsInternal.Tables[0].Columns.Count;
            ViewState["colcount"] = colcont.ToString();
            int rule1 = colcont + 2;//tbl_Rule1
            // int rule1 = colcont
            for (int i = 0; i < colcont; i++)
            {
                str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule1').attr('colspan'," + rule1 + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length=='null'?$('#th" + i + "').hide():$('#th" + i + "').show();";
            }
            int z = 4;
            for (int j = 0; j < colcont; j++)
            {

                str1 += "$('#th" + (j) + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
                z++;
            }

            string str3 = str + str1;
            ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);

        }

        else
        {
            lvInternalData.DataSource = null;
            lvInternalData.DataBind();
        }
        DataSet dsInternal1 = objSC.GetDetailsOfInternalMarks1(idno, Convert.ToInt32(ddlSession.SelectedValue));
        if (dsInternal1 != null && dsInternal1.Tables.Count > 0 && dsInternal1.Tables[0].Rows.Count > 0)
        {
            //dt = dsInternal1.Tables[0];
            //DataRow[] dr = dt.Select("");
            //string str = string.Empty;
            //string str1 = string.Empty;
            //int td = 0;
            //int colcont = dsInternal1.Tables[0].Columns.Count;
            //ViewState["colcount"] = colcont.ToString();
            //// int rule1 = colcont + 3;
            //int rule1 = colcont;
            //for (int i = 0; i < colcont; i++)
            //{
            //    str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('td:nth-child(3)').show();$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#td" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#td" + i + "').text.length=='null'?$('#td" + i + "').hide():$('#td" + i + "').show();";
            //}
            //int z = 4;
            //for (int j = 0; j < colcont; j++)
            //{

            //    str1 += "$('#td" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#td" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
            //    z++;
            //}

            //string str3 = str + str1;
            //ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
            //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);

            lvInternalData.DataSource = dsInternal1.Tables[0];
            lvInternalData.DataBind();
        }
        else
        {
            lvInternalData.DataSource = null;
            lvInternalData.DataBind();
        }

        int arrVal = 0;
        string[] arr_TextBox = new string[] { "Label5", "Label6", "Label7", "Label8", "Label9", "Label10", "Label11", "Label12", "Label13", "Label14", "Label15", "Label16", "Label17", "Label18", "Label19" };
        int k = 0;
        foreach (ListViewDataItem lvitem in lvInternalData.Items)
        {
            for (; k < dsInternal1.Tables[0].Rows.Count; )
            {
                for (int j = 5; j < dsInternal1.Tables[0].Columns.Count; j++)
                {
                    if (Convert.ToString(dsInternal1.Tables[0].Rows[k][j]) != "")
                    {
                        string CL = Convert.ToString(dsInternal1.Tables[0].Columns[j].ColumnName);
                        ((Label)lvitem.FindControl(arr_TextBox[arrVal])).Text = Convert.ToString(dsInternal1.Tables[0].Rows[k][j]) != "-1.00" ? Convert.ToString(dsInternal1.Tables[0].Rows[k][j]) : "";
                        // arrVal++;


                        if (j + 2 > Convert.ToInt32(dsInternal1.Tables[0].Columns.Count))
                        {
                            arrVal = 0;
                            break;
                        }
                    }
                    arrVal++;
                }
                k++;
                break;
            }
            arrVal = 0;

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

            divStudent.Visible = false;
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

                    }
                }
                else
                {
                    //    pnlLV.Visible = false;
                    //    lblNoRecords.Visible = false;
                    //    lvStudent.DataSource = null;
                    //    lvStudent.DataBind();
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }
    }


    private void BindStudentListview(int userno)
    {
        StudentControllerDetails_MM objSC = new StudentControllerDetails_MM();
        objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        int ua_userno = Convert.ToInt32(Session["userno"].ToString());
       // DataSet ds = objSC.RetrieveStudentDetailsAdmCancel('','',ua_userno);
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(string.Empty, string.Empty, ua_userno);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
        else
        {
            lvStudent.Visible = false;
           lvStudent.DataSource = null;
            lvStudent.DataBind();
        }

    }
    private void bindlist(string category, string searchtext, int uano)
    {
        StudentControllerDetails_MM objSC = new StudentControllerDetails_MM(); 


       // StudentController objSC = new StudentController();
        objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        DataSet ds = objSC.RetrieveStudentDetailsAdmCancel(searchtext, category, uano);

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
        int uano = Convert.ToInt32(Session["userno"].ToString());

        if (Convert.ToInt32(Session["OrgId"]) == 9)
        {

            divrealease.Visible = true;

        }
        else if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4 || Convert.ToInt32(Session["OrgId"]) == 8)
        {

            divInternalMarks.Visible = true;
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            DivStudentData.Visible = true;

        }
        else
        {
            divInternalMarks.Visible = false;
            divrealease.Visible = false;
        }
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

        bindlist(ddlSearch.SelectedItem.Text, value, uano);
        //ddlDropdown.ClearSelection();
        txtSearch.Text = string.Empty;

        divStudent.Visible = false;


    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
       
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
        int idno = 0;
        StudentController objSC = new StudentController();


        //  DataSet dsregistration, dsResult, dsFees, dsCertificate, dsRemark, dsRefunds, dsTestMarks, dsAttendance;
        // FeeCollectionController feeController = new FeeCollectionController();

        if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                

        }

        this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + Convert.ToInt32(Session["stuinfoidno"]), "R.SESSIONNO DESC");
        if (ddlSession.Items.Count > 1)
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

            LinkButton lnkRecieptNo = sender as LinkButton;
            //Session["CANCEL_REC"] = ;
            //lnkRecieptNo.CommandArgument=
            //int.Parse(lnkRecieptNo.CommandArgument);
            //lnkRecieptNo.ToolTip = (lnkRecieptNo.ToolTip);
            if (lnkRecieptNo.ToolTip == "True")
            {
                Session["CANCEL_REC"] = 1;
            }
            else if (lnkRecieptNo.ToolTip == "False")
            {
                Session["CANCEL_REC"] = 0;
            }
            else
            {
                Session["CANCEL_REC"] = 0;
            }
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
            LinkButton btnPrint = sender as LinkButton;
            if (btnPrint.CommandArgument != string.Empty)
            {
                if (Convert.ToInt32(Session["OrgId"]) == 1)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 2)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_crescent.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    ShowReportPrevious2("OnlineFeePayment", "FeeCollectionReceiptForCash_RCPIPER.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Convert.ToInt32(Session["CANCEL_REC"]));
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    ShowReportPrevious1("OnlineFeePayment", "FeeCollectionReceiptForCash_MIT_FEECOLL.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString());
                }
                else if (Convert.ToInt32(Session["OrgId"]) == 5)
                {
                    ShowReportPrevious("OnlineFeePayment", "FeeCollectionReceiptForCash_JECRC.rpt", Int32.Parse(btnPrint.CommandArgument), Convert.ToInt32(Session["stuinfoidno"]), Session["UAFULLNAME"].ToString(), Convert.ToInt32(Session["CANCEL_REC"]));
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void ShowReportPrevious(string reportTitle, string rptFileName, int dcrNo, int studentNo, string Username, int Cancel)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();

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


    //private void ShowReport_ForCash(string rptName, int dcrNo, int studentNo, string copyNo, int Cancel)
    //    {
    //    try
    //        {
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=Fee_Collection_Receipt";
    //        url += "&path=~,Reports,Academic," + rptName;
    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, copyNo) + ",username=" + Session["username"].ToString();
    //        divMsg.InnerHtml += " <script type='text/javascript' language='javascript'> try{ ";
    //        divMsg.InnerHtml += " window.open('" + url + "','Fee_Collection_Receipt','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " }catch(e){ alert('Error: ' + e.description);}</script>";

    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //ScriptManager.RegisterClientScriptBlock(this.updEdit, this.updEdit.GetType(), "controlJSScript", sb.ToString(), true);
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
    //        }
    //    catch (Exception ex)
    //        {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "Academic_FeeCollection.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //        }
    //    }

    private void ShowReportPrevious2(string reportTitle, string rptFileName, int dcrNo, int studentNo, int Cancel)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();


            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + "," + this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_UA_NAME=" + Session["username"].ToString() + "," + "@P_CANCEL=" + Convert.ToInt32(Session["CANCEL_REC"]) + ","+this.GetReportParameters(dcrNo, studentNo, "2") + ",username=" + Session["username"].ToString();

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

    private void ShowReport_rcpit(string reportTitle, string rptFileName, int MiscdcrNO)
    {

        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,REPORTS,ACADEMIC," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREE=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_BRANCH=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_MISCDCRNO=" + MiscdcrNO;
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport_rcpit() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReportPrevious1(string reportTitle, string rptFileName, int dcrNo, int studentNo, string Username)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DCRNO=" + dcrNo + ",@P_IDNO=" + studentNo + ",@P_UA_NAME=" + Username;//+ ",username=" + Session["username"].ToString();

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

        ImageButton imgbtn = sender as ImageButton;
        string session = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();


        //ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        //Label IoNO = dataitem.FindControl("lbIoNo") as Label;
        //Label lblSession = dataitem.FindControl("lblSession") as Label;
        ////Label lblsessionnm = dataitem.FindControl("lblSessionname") as Label;
        //int Semesterno = Convert.ToInt32(rdolistSemester.SelectedValue);
        //int idno = Convert.ToInt32(IoNO.ToolTip);
        //int sessionno = Convert.ToInt32(lblSession.ToolTip);

        int idno = 0;
        if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
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
        int studtype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT PREV_STATUS", " SESSIONNO=" + Convert.ToInt32(session) + " AND IDNO=" + idno + " "));
        int sem = Convert.ToInt32(rdolistSemester.SelectedValue);//Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + idno + " "));
        //int session = Convert.ToInt32(objCommon.LookUp("ACD_TRRESULT", "MAX(SESSIONNO)", "IDNO=" + idno + " AND SEMESTERNO=" + rdolistSemester.SelectedValue));

        int OrgId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "OrganizationId", "IDNO=" + idno));

        try
        {

            if (OrgId == 1)
            {
                //string reportTitle = "Gradesheet Report";
                //string rptFileName = "rptCourseWise_GradeSheet_Rcpit.rpt";
                //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                //url += "Reports/CommonReport.aspx?";
                //url += "pagetitle=" + reportTitle;
                //url += "&path=~,Reports,Academic," + rptFileName;
                //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(session) + ",@P_SCHEMENO=" + scheme + ",@P_BRANCHNO=" + branch + ",@P_DEGREENO=" + degree + ",@P_SEMESTERNO=" + sem + ",@P_IDNO=" + idno;

                ////url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

                ////divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
                ////divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ////divMSG.InnerHtml += " </script>";

                ////To open new window from Updatepanel
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                //sb.Append(@"window.open('" + url + "','','" + features + "');");

                //ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);



                string spec = string.Empty;
                string Result = string.Empty;
                string reportTitle = "Gradesheet Report";
                string rptFileName = "MarksGrade_RCPIT_withoutHeader.rpt";
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(session) + ",@P_RESULT=" + Result + ",@P_SPEC=" + spec + ",@P_STUDTYPE=" + studtype + ",@P_SEMESTERNO=" + sem + ",@DateofIssue=" + DateTime.Today.Date;


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
                url += "&param=@P_COLLEGE_ID=" + Convert.ToInt32(cid) + ",@P_SESSIONNO=" + Convert.ToInt32(session) + ",@P_SCHEMENO=" + scheme + ",@P_BRANCHNO=" + branch + ",@P_DEGREENO=" + degree + ",@P_SEMESTERNO=" + sem + ",@P_IDNO=" + idno;

                //url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + studentNo + ",@P_DCRNO=" + Convert.ToInt32(dcrNo);

                //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                //divMsg.InnerHtml += " </script>";

                //To open new window from Updatepanel
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updStudentInfo, this.updStudentInfo.GetType(), "controlJSScript", sb.ToString(), true);

            }
            else
            { }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow();</script>", false);

        }
        catch (Exception ex)
        {
            throw;
        }


    }
    protected void lnkRecno_Click(object sender, EventArgs e)
    {
        LinkButton lnkOtherFees = sender as LinkButton;
        int MISCDCRNO = int.Parse(lnkOtherFees.CommandArgument);
        ShowReport_rcpit("Miscellanious Fees", "rptMiscReport_RCPIT.rpt", MISCDCRNO);
    }
    protected void btnInternalMarks_Click(object sender, EventArgs e)
    {
        try
        {

            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;
            int idno = 0;
            if (ViewState["usertype"].ToString() == "2")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                idno = Convert.ToInt32(ViewState["idno"]);
                //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
                //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                

            }

            //int COLLEGE = Convert.ToInt32(ddlSchoolInstitite.SelectedValue);
            //int Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "DISTINCT SCHEMENO", "COSCHNO=" + Convert.ToInt32(COLLEGE)));

            DataSet ds = null;

            string proc_name = "PKG_GET_SUBEXAMNAME_BY_PARTICULATUR_IDNO_Subexam_MIT";

            string para_name = "@P_IDNO,@P_SESSIONNO";
            string call_values = "" + idno + "," + Convert.ToInt32(ddlSession.SelectedValue) + "";
            // string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO,@P_ORGID";
            // string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "," + ORG + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);


            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=MarksEntryDetailsReports.xls";
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
                // objCommon.DisplayMessage(updMarksEntryDetailReport, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUaimsCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport_rcpit() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void lnkccode_Click(object sender, EventArgs e)
    {
        int idno = 0;
        if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = Convert.ToInt32(ViewState["idno"]);
            //idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
            //this.objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER M ON(R.SESSIONNO=M.SESSIONNO)", "DISTINCT R.SESSIONNO", "M.SESSION_NAME", "IDNO = " + idno, "R.SESSIONNO DESC");                                
        }
        string regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + idno + " ");
        LinkButton lnkccode = sender as LinkButton;
        if (lnkccode.CommandArgument != string.Empty)
        {
            DataSet ds = null;

            string proc_name = "PKG_ACD_GET_REPORT_STUDENT_ATTENDANCE";

            string para_name = "@P_REGNO,@P_COURSENO";
            string call_values = "" + regno + "," + Int32.Parse(lnkccode.CommandArgument) + "";
            // string para_name = "@P_IDNO,@P_SESSIONNO,@P_SCHEMENO,@P_DEGREENO,@P_BRANCHNO,@P_ORGID";
            // string call_values = "" + idno + "," + sessionno + "," + Convert.ToInt32(lblScheme.ToolTip) + "," + degreeno + "," + branchno + "," + ORG + "";
            ds = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourseAtt.DataSource = ds;
                lvCourseAtt.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalCourse();", true);
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel2, "No Data Found.", this.Page);
            }
        }
    }
}


