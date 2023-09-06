using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Web.Services;

public partial class EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpApprEnt objEA = new EmpApprEnt();
    EmployeeAppraisal_Controller objAPPRController = new EmployeeAppraisal_Controller();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
   {
        try
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
                   // if (Request.QueryString["autho"] != "Y")
                    if (Session["APAR_autho"] != "Y")
                    {
                        this.CheckPageAuthorization();
                    }
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    ViewStatNull();
                    this.FillDropDown();

                    //CreateTabelPerformanceResults();
                    //CreateTabelProfessionalDevelopment();
                    //CreateTabelStudentsAttendance();
                    //CreateTabelResarchGuidance();
                    //CreateTabelResarchQualification();

                    FillDropDown();
                    SessionNull();
                    ViewState["action"] = null;
                    ViewState["APPRAISAL_EMPLOYEE_ID"] = "";
                    // ViewState["AUTHORITY_UANO"] = null;
                    // ViewState["AUTHOEMPTYPE"] = null;
                    ViewState["SRNO"] = null;
                    ViewState["EMPLOYEE_ID"] = null;
                    // int empid = 0;
                    int SessionNo = 0;
                   // if (Request.QueryString["autho"] == "Y")            //Authority
                    if (Session["APAR_autho"] == "Y")
                    {
                       // int empid = Convert.ToInt32(Request.QueryString["empid"]);
                        int empid = Convert.ToInt32(Session["APAR_empid"]);

                        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.UA_TYPE = 3", "  E.FNAME"); // + Convert.ToInt32(Session["UA_IDNO"]),
                        ddlEmployee.AppendDataBoundItems = true;

                       // if (Request.QueryString["AUTHORITYUANO"] != null)
                        if(Convert.ToInt32(Session["APAR_AUTHORITYUANO"])  != null)
                        {
                            //int authority_ua_no = Convert.ToInt32(Request.QueryString["AUTHORITYUANO"]);
                            int authority_ua_no = Convert.ToInt32(Session["APAR_AUTHORITYUANO"]);
                            //ViewState["AUTHORITY_UANO"] = Convert.ToInt32(Request.QueryString["AUTHORITYUANO"]);
                            ViewState["AUTHORITY_UANO"] = Convert.ToInt32(Session["APAR_AUTHORITYUANO"]);
                            ViewState["SRNO"] = Convert.ToInt32(Session["APAR_SRNO"]);
                            //  ViewState["AUTHOEMPTYPE"] = Request.QueryString["EmployeeType"].ToString();
                            PanelEnabled();
                        }
                       // if (Request.QueryString["empid"] != null)
                        if (Convert.ToInt32(Session["APAR_empid"]) != null)

                        {
                            LinkButton_PersonalInfo_Click(sender, e);
                            //empid = Convert.ToInt32(Request.QueryString["empid"]);
                            empid = Convert.ToInt32(Convert.ToInt32(Session["APAR_empid"]));
                            SessionNo = Convert.ToInt32(Request.QueryString["SessionNo"]);
                            ViewState["EMPLOYEE_ID"] = empid;
                            ddlEmployee.SelectedValue = empid.ToString();
                            ddlEmployee.Enabled = false;
                            GetEmpAppraisalDetails(empid);
                            //GetEmpAppraisalDetails(Convert.ToInt32(Session["empid"]));
                        }
                    }



                    else if (Convert.ToInt32(Session["usertype"]) == 3)  // faculty user                                                                                       
                    {
                        LinkButton_Reviewing_Officers.Visible = false;
                        //Label35.Visible = false;
                        // objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.STAFFNO=1", "  E.FNAME"); // + Convert.ToInt32(Session["UA_IDNO"]),
                        //objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.STAFFNO=1", "  E.FNAME" ); // + Convert.ToInt32(Session["UA_IDNO"]),

                        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.IDNO=" + Convert.ToInt32(Session["idno"]), "E.IDNO");
                        ddlEmployee.SelectedValue = Session["idno"].ToString();
                        // GetEmpAppraisalDetails(Convert.ToInt32(Session["idno"]));

                        // visible ON/ OFF links
                        ddlEmployee.Attributes.Add("readonly", "true");
                        VisibleDiv();
                        LinkButton_PersonalInfo_Click(sender, e);

                       // txtWeight.Text = "20";
                       // txtMaxWeight.Text = "20";
                        // BindlistData();   
                        //  BindConferencelistData();
                        // BindPatentListView();
                        TeachingLearningActivities();
                        EngagingLectures();
                        Duties_in_Excess_of_UGC_Norms();
                        MaterialResource();
                        InnovativeTeaching();
                        StudentsFeedback();
                        ExaminationWork();
                        CO_CURRICULAR_ACTIVITIES();
                        Administrative_Academic();
                        ProfessionalDevelopment();
                        GetEmpAppraisalDetails(Convert.ToInt32(ddlEmployee.SelectedValue));
                        // PanelHideForEmpFinalSubmit();
                        PanelDisabled();
                        PanelEnabled();
                    }
                    else
                    {
                        ViewState["EMPLOYEE_TYPE"] = "NT";
                        ddlEmployee.AppendDataBoundItems = false;
                        objCommon.FillDropDownList(ddlEmployee, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.IDNO=" + Convert.ToInt32(Session["idno"]), "E.IDNO"); // P.PSTATUS='Y' AND 
                        GetEmpAppraisalDetails(Convert.ToInt32(Session["idno"]));
                        ddlEmployee.Attributes.Add("readonly", "true");

                        // visible ON/ OFF links
                        NonVisibleDiv();
                    }

                    DataSet dsSubD = null;

                    //if (Request.QueryString["empid"] != null)
                    if(Session["APAR_empid"]  != null)
                    {
                        dsSubD = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "PAPNO", "PAPATH", "IDNO= " + Convert.ToInt32(Session["APAR_empid"]), "");
                    }
                    else
                    {
                        dsSubD = objCommon.FillDropDown("APPRAISAL_PASSING_AUTHORITY_PATH", "PAPNO", "PAPATH", "IDNO= " + Convert.ToInt32(Session["idno"]), "");
                    }
                    if (dsSubD.Tables[0].Rows.Count != 0)
                    {
                        txtPAPath.Text = dsSubD.Tables[0].Rows[0]["PAPATH"].ToString();
                        hdnPAPNO.Value = dsSubD.Tables[0].Rows[0]["PAPNO"].ToString();
                        // PanelShowForEmpFinalSubmit();
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updActivity, "Passing Path is not defined.", this.Page);
                        LinkButton_Print.Visible = false;
                        LinkButton_FinalSubmit.Visible = false;
                        PanelHideForEmpFinalSubmit();
                        //return;
                    }

                  //  if (Request.QueryString["SRNO"] != null)
                    if (Convert.ToInt32(Session["SRNO"]) != null)
                    {
                      //  if (Request.QueryString["SRNO"].ToString() == "1") // Reporting Authority
                        if (Convert.ToInt32(Session["SRNO"]).ToString() == "1") 
                        {
                            ViewState["REPORTING_AUTHORITY_ID"] = Convert.ToInt32(Session["userno"]);

                            //LinkButton_Reporting_Officer.Visible = true;
                            //divReporting.Visible = true;
                            LinkButton_Reviewing_Officers.Visible = true;
                           // Label35.Visible = true;
                            //LinkButton_CounterSign_Officer.Visible = false;
                            //btnReportingOfficerNext.Visible = false;
                            //btnReviewingOfficerNext.Visible = false;
                            PanelEnabled();
                            VisibleDiv();
                            //NonVisibleDiv();
                            DataSet dss = null;
                            dss = objCommon.FillDropDown("USER_ACC", "UA_NO, UA_IDNO, UA_FULLNAME AS NAME", "UA_DESIG AS SUBDESIG, '' AS SUBDEPT ", "UA_NO = " + Convert.ToInt32(ViewState["AUTHORITY_UANO"]), "");

                            if (dsSubD.Tables[0].Rows.Count != 0)
                            {
                                // txtReportingOfficersName.Text = dss.Tables[0].Rows[0]["NAME"].ToString();
                            }


                            if (ViewState["AUTHOEMPTYPE"].ToString() == "T")
                            {

                                VisibleDiv();
                                LinkButton_PersonalInfo_Click(sender, e);
                            }
                            else if (ViewState["AUTHOEMPTYPE"].ToString() == "NT")
                            {
                                VisibleDiv();
                            }
                        }

                       // else if (Request.QueryString["SRNO"].ToString() == "2")  
                        else if (Convert.ToInt32(Session["SRNO"]).ToString() == "2") // Reviewing Authority
                        {
                            LinkButton_Reviewing_Officers.Visible = true;
                            divReviewing.Visible = true;
                            //LinkButton_CounterSign_Officer.Visible = false;
                            // PanelHideForReportingOfficer();
                            //btnReportingOfficerNext.Visible = true;
                            //btnReviewingOfficerNext.Visible = false;


                            if (ViewState["AUTHOEMPTYPE"].ToString() == "T")
                            {
                                VisibleDiv();
                                LinkButton_PersonalInfo_Click(sender, e);
                                //lblLength.Visible = true; lblLengthNT.Visible = false;
                            }
                            else if (ViewState["AUTHOEMPTYPE"].ToString() == "NT")
                            {
                                NonVisibleDiv();
                                //lblLength.Visible = false; lblLengthNT.Visible = true;
                            }
                        }
                        else
                        {
                            if (ViewState["SRNO"] != null)
                            {
                                LinkButton_Reviewing_Officers.Visible = true;
                                divReviewing.Visible = true;

                            }
                            else
                            {
                                LinkButton_Reviewing_Officers.Visible = false;
                                divReviewing.Visible = false;
                            }
                            //if (ViewState["AUTHOEMPTYPE"].ToString() == "T")
                            //{
                            //    VisibleDiv();
                            //    LinkButton_PersonalInfo_Click(sender, e);
                            //}
                            //else if (ViewState["AUTHOEMPTYPE"].ToString() == "NT")
                            //{
                            //    NonVisibleDiv();
                            //}
                        }
                    }
                }
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void PanelEnabled()
    {
        PnlUGCNorms.Enabled = true; PnlMaterial.Enabled = true; PnlAttend.Enabled = true; PnlResult.Enabled = true; PnlStudCurricular.Enabled = true; PnlActivity.Enabled = true;
        PnlAdmin.Enabled = true; PnlDevelopment.Enabled = true; PanelJournal.Enabled = true; PanelBook.Enabled = true; PanelChapters.Enabled = true; PanelConference.Enabled = true;
        PanelAvishkar.Enabled = true; PnlProjects.Enabled = true; PanelPatent.Enabled = true; PnlReearchGuid.Enabled = true; PnlResQualification.Enabled = true;
        PanelInnovative.Enabled = true; PanelFeedback.Enabled = true; PnlExamination.Enabled = true; PnlLearning.Enabled = true; PnlLectures.Enabled = true;
    }

    private void PanelDisabled()
    {
        PnlUGCNorms.Enabled = false; PnlMaterial.Enabled = false; PnlAttend.Enabled = false; PnlResult.Enabled = false; PnlStudCurricular.Enabled = false; PnlActivity.Enabled = false;
        PnlAdmin.Enabled = false; PnlDevelopment.Enabled = false; PanelJournal.Enabled = false; PanelBook.Enabled = false; PanelChapters.Enabled = false; PanelConference.Enabled = false;
        PanelAvishkar.Enabled = false; PnlProjects.Enabled = false; PanelPatent.Enabled = false; PnlReearchGuid.Enabled = true; PnlResQualification.Enabled = true;
        PanelInnovative.Enabled = false; PanelFeedback.Enabled = false; PnlExamination.Enabled = false; PnlLearning.Enabled = false; PnlLectures.Enabled = false;
    }

    private void VisibleDiv()
    {
        divPersonalInfo.Visible = true; divTeaching.Visible = true; divEngaging.Visible = true; divPerformance.Visible = true;
        divExcess.Visible = true; divAcademic.Visible = true; divCurricular.Visible = true;
        divCommunity.Visible = true; divAdminisrative.Visible = true; divJournal.Visible = true; divConference.Visible = true;
        divResearch.Visible = true; divPatent.Visible = true;
    }

    private void NonVisibleDiv()
    {
        divPersonalInfo.Visible = false; divTeaching.Visible = false; divEngaging.Visible = false;
        divPerformance.Visible = false; divExcess.Visible = false; divAcademic.Visible = false; divCurricular.Visible = false;
        divCommunity.Visible = false; divAdminisrative.Visible = false;
        divJournal.Visible = false; divConference.Visible = false; divResearch.Visible = false; divPatent.Visible = false;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=EmployeeAppraisalForm.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=EmployeeAppraisalForm.aspx");
        }
    }

    // This method is used to fill drop down values.
    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlSession, "APPRAISAL_SESSION_MASTER", "SESSION_ID", "SESSION_NAME", "IS_ACTIVE=1", "");

        txtfromdate.Text = objCommon.LookUp("appraisal_session_master", "convert(varchar, FROM_DATE,103)", "IS_ACTIVE=1");
        txttodate.Text = objCommon.LookUp("appraisal_session_master", "convert(varchar, TO_DATE,103)", "IS_ACTIVE=1");
        objCommon.FillDropDownList(ddlDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO <> 0", "");
        objCommon.FillDropDownList(ddlCDesig, "PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG", "SUBDESIGNO <>0", "");
        objCommon.FillDropDownList(ddlInstitute, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "");

        // objCommon.FillDropDownList(ddlSemester,"APPRAISAL_TEACHING_LERNING_ACTIVIES_MASTER","SN_ID","SEMESTER","","SN_ID");
    }

    #region "Get Emp Personal Details"
    //  This method is used to get employee personal detail 
    private void GetEmployeeDetails()
    {
        DataSet ds = null;
        ds = objAPPRController.GetEmployeePersonalInfo(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {

            txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
            ddlDept.SelectedValue = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
            ddlCDesig.SelectedValue = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();
            ddlInstitute.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
            string txtfromdate = objCommon.LookUp("appraisal_session_master", "YEAR(FROM_DATE)", "IS_ACTIVE=1");
            string txttodate = objCommon.LookUp("appraisal_session_master", "YEAR(TO_DATE)", "IS_ACTIVE=1");
            txtassesmentyear.Text = txtfromdate.ToString() + " " + "-" + " " + txttodate.ToString();

        }


    }

    #endregion

    //#region "Get Emp Appraisal Details"
    //  This method is used to get employee personal detail 

    private void GetEmpAppraisalDetails(int empid)
    {
        DataSet ds = null;
        string appraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + empid + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        ViewState["APPRAISAL_EMPLOYEE_ID"] = appraisalEmp_Id;
        if (appraisalEmp_Id != "")
        {
            ds = objAPPRController.GetEmployeeInfo(appraisalEmp_Id, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlEmployee.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["EMPLOYEE_NAME"].ToString();
                ddlCDesig.SelectedValue = ds.Tables[0].Rows[0]["SUBDESIGNO"].ToString();
                ddlDept.SelectedValue = ds.Tables[0].Rows[0]["DEPARTMENT_ID"].ToString();
                ddlInstitute.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();

                string txtfromdate = objCommon.LookUp("appraisal_session_master", "YEAR(FROM_DATE)", "IS_ACTIVE=1");
                string txttodate = objCommon.LookUp("appraisal_session_master", "YEAR(TO_DATE)", "IS_ACTIVE=1");
                txtassesmentyear.Text = txtfromdate.ToString() + " " + "-" + " " + txttodate.ToString();
                txtsemI.Text = ds.Tables[0].Rows[0]["SEM1_TOTAL"].ToString();
                txtsemII.Text = ds.Tables[0].Rows[0]["SEM2_TOTAL"].ToString();

                // Published Journal
                if (Convert.ToInt32(ViewState["SRNO"]) != 1 && Convert.ToInt32(ViewState["SRNO"]) != 2)
                {
                    divverifys.Visible = false;
                    
                    int UATYPE = Convert.ToInt32(Session["usertype"]);
                    if (UATYPE == 3)
                    {
                        LinkButton_Reviewing_Officers.Visible = false;
                        divReviewing.Visible = false;
                        string lockByFaculty = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(USER_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                        if (lockByFaculty == "1")
                        {
                            lvTeachActivity.Enabled = false;
                            lvpublicationJournal.Enabled = false;
                            lvPublishedChapter.Enabled = false;
                            lvPublishedBooks.Enabled = false;
                            lvConference.Enabled = false;
                            lvAvishkar.Enabled = false;
                            lvProjects.Enabled = false;
                            lvPatent.Enabled = false;
                            lvQualification.Enabled = false;




                        }
                    }

                }
                else
                {
                    txtNumericalGrading.ReadOnly = false;
                    divverifys.Visible = true;
                    btnResarchGuidance.Enabled = false;
                    btnResarchQualification.Enabled=false;
                    diver.Visible = true;
                    divattendance.Visible = true;
                    divresult.Visible = true;
                    div55.Visible = true;
                    div32.Visible = true;
                    div61.Visible = true;
                    div65.Visible = true;
                    divpv.Visible = true;
                }


                //lvInnovative.DataSource = ds.Tables[17];
                //lvInnovative.DataBind();
                //hdRowCount.Value = ds.Tables[17].Rows.Count.ToString();

                if (ds.Tables[17].Rows.Count > 0)
                {
                    lvInnovative.DataSource = ds.Tables[17];
                    lvInnovative.DataBind();
                    hdnApiCount.Value = ds.Tables[17].Rows.Count.ToString();

                }
                else
                {

                    InnovativeTeaching();
                }


                foreach (ListViewDataItem item in lvInnovative.Items)
                {
                    TextBox txtAPIScore = item.FindControl("txtAPIScore") as TextBox;
                    TextBox txtAPIVerified = item.FindControl("txtAPIVerified") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtAPIScore.Enabled = false;
                        txtAPIVerified.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtAPIScore.Enabled = false;
                        txtAPIVerified.Enabled = false;

                    }

                    else
                    {
                        txtAPIScore.Visible = true;
                        txtAPIVerified.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvInnovative.FindControl("lblverifiedapiscore").Visible = false;
                        txtVerifiedApiS.Visible = false;
                        lblver.Visible = false;
                    }

                }


                // }


                if (ds.Tables[18].Rows.Count > 0)
                {
                    lvExamination.DataSource = ds.Tables[18];
                    lvExamination.DataBind();
                    hdnExamination.Value = ds.Tables[18].Rows.Count.ToString();



                }
                else
                {

                    ExaminationWork();
                }


                foreach (ListViewDataItem item in lvExamination.Items)
                {
                    TextBox txtAPIScores = item.FindControl("txtAPIScores") as TextBox;
                    TextBox txtAPIVerified = item.FindControl("txtAPIVerified") as TextBox;
                   


                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtAPIScores.Enabled = false;
                        txtAPIVerified.Enabled = true;
                        

                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtAPIScores.Enabled = false;
                        txtAPIVerified.Enabled = false;
                        
                    }

                    else
                    {
                        txtAPIScores.Visible = true;
                        txtAPIVerified.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvExamination.FindControl("lblver2").Visible = false;
                        txtApiVerify.Visible = false;
                        lblverify.Visible = false;
                    }

                }


                if (ds.Tables[19].Rows.Count > 0)
                {
                    lvFeedback.DataSource = ds.Tables[19];
                    lvFeedback.DataBind();
                    hdnFeedback.Value = ds.Tables[19].Rows.Count.ToString();

                }
                else
                {

                    StudentsFeedback();
                }


                foreach (ListViewDataItem item in lvFeedback.Items)
                {
                    TextBox txtAPIScores = item.FindControl("txtAPIScores") as TextBox;
                    TextBox txtAPIVerified = item.FindControl("txtAPIVerified") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtAPIScores.Enabled = false;
                        txtAPIVerified.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtAPIScores.Enabled = false;
                        txtAPIVerified.Enabled = false;

                    }

                    else
                    {
                        txtAPIScores.Visible = true;
                        txtAPIVerified.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvFeedback.FindControl("lblapi2").Visible = false;
                        txtVerified.Visible = false;
                        lblvapi1.Visible = false;
                    }

                }



                if (ds.Tables[20].Rows.Count > 0)
                {
                    lvCurricular.DataSource = ds.Tables[20];
                    lvCurricular.DataBind();
                    hdnCurricular.Value = ds.Tables[20].Rows.Count.ToString();

                }
                else
                {

                    CO_CURRICULAR_ACTIVITIES();
                }


                foreach (ListViewDataItem item in lvCurricular.Items)
                {
                    TextBox txtAPIS = item.FindControl("txtAPIS") as TextBox;
                    TextBox txtVerifyAPI = item.FindControl("txtVerifyAPI") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtAPIS.Enabled = false;
                        txtVerifyAPI.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtAPIS.Enabled = false;
                        txtVerifyAPI.Enabled = false;

                    }

                    else
                    {
                        txtAPIS.Visible = true;
                        txtVerifyAPI.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvCurricular.FindControl("lblverify").Visible = false;
                        txtVery.Visible = false;
                        lblveify.Visible = false;
                    }

                }


                if (ds.Tables[22].Rows.Count > 0)
                {
                    lvAcademic.DataSource = ds.Tables[22];
                    lvAcademic.DataBind();
                    hdnAdminAcademic.Value = ds.Tables[22].Rows.Count.ToString();
                }
                else
                {

                    Administrative_Academic();
                }


                foreach (ListViewDataItem item in lvAcademic.Items)
                {
                    TextBox txtAcadDevelopApi = item.FindControl("txtAcadDevelopApi") as TextBox;
                    TextBox txtAcadDevelopVerify = item.FindControl("txtAcadDevelopVerify") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtAcadDevelopApi.Enabled = false;
                        txtAcadDevelopVerify.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtAcadDevelopApi.Enabled = false;
                        txtAcadDevelopVerify.Enabled = false;

                    }

                    else
                    {
                        txtAcadDevelopApi.Visible = true;
                        txtAcadDevelopVerify.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvAcademic.FindControl("lblapive").Visible = false;
                        txtAcademicVerifiedApi.Visible = false;
                        lblver1.Visible = false;
                    }

                }


                if (ds.Tables[24].Rows.Count > 0)
                {
                    txtLengthOfService.Text = ds.Tables[24].Rows[0]["GIVE_REASON"].ToString();
                    txtPenPicture_Comment_reviewing.Text = ds.Tables[24].Rows[0]["OTHER_COMMENT"].ToString();
                    txtReviewingReason.Text = ds.Tables[24].Rows[0]["LENGTH_OF_SERVICE"].ToString();
                    txtNumericalGrading.Text = ds.Tables[24].Rows[0]["NUMERICALGRADING"].ToString();


                }


                if (ds.Tables[25].Rows.Count > 0)
                {

                    lvotheractivity.DataSource = ds.Tables[25];
                    lvotheractivity.DataBind();

                    Session["RecTblOtherActivity"] = ds.Tables[25];
                    DataTable PublishChapter = (DataTable)Session["RecTblOtherActivity"];
                    hdnotheractivity.Value = ds.Tables[25].Rows.Count.ToString();
                }




                foreach (ListViewDataItem item in  lvotheractivity.Items)
                {
                    TextBox txtapiscore = item.FindControl("txtapiscore") as TextBox;
                    TextBox txtapiver = item.FindControl("txtapiver") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtapiscore.Enabled = false;
                        txtapiver.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtapiscore.Enabled = false;
                        txtapiver.Enabled = false;

                    }

                    else
                    {
                        txtapiscore.Visible = true;
                        txtapiver.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvotheractivity.FindControl("lblapive").Visible = false;
                        txtJournalPub.Visible = false;
                        lblverapi.Visible = false;
                    }

                }



















                if (ds.Tables[23].Rows.Count > 0)
                {
                    lvDevelopment.DataSource = ds.Tables[23];
                    lvDevelopment.DataBind();
                    hdnDevelopment.Value = ds.Tables[23].Rows.Count.ToString();
                }
                else
                {

                    ProfessionalDevelopment();
                }

                foreach (ListViewDataItem item in lvDevelopment.Items)
                {
                    TextBox txtDevelopApiScore = item.FindControl("txtDevelopApiScore") as TextBox;
                    TextBox txtDevelopVerifiedApi = item.FindControl("txtDevelopVerifiedApi") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtDevelopApiScore.Enabled = false;
                        txtDevelopVerifiedApi.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtDevelopApiScore.Enabled = false;
                        txtDevelopVerifiedApi.Enabled = false;

                    }

                    else
                    {
                        txtDevelopApiScore.Visible = true;
                        txtDevelopVerifiedApi.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvDevelopment.FindControl("lblver").Visible = false;
                        txtDevelopmentVerify.Visible = false;
                        lblvs.Visible = false;
                    }

                }




                if (ds.Tables[1].Rows.Count > 0)
                {
                    lvTeachActivity.DataSource = ds.Tables[1];
                    lvTeachActivity.DataBind();
                    hdRowCount.Value = ds.Tables[1].Rows.Count.ToString();
                }
                else
                {

                    TeachingLearningActivities();
                }



                if (ds.Tables[2].Rows.Count > 0)
                {
                    lvpublicationJournal.DataSource = ds.Tables[2];
                    lvpublicationJournal.DataBind();
                    hdnPublication.Value = ds.Tables[2].Rows.Count.ToString();

                }
                else
                {
                    PnlJournal.Visible = false;
                    Session["TblPublishedJournal"] = null;
                    lvpublicationJournal.Visible = false;
                    lvpublicationJournal.DataSource = null;
                    lvpublicationJournal.DataBind();
                    lblempserbook.Visible = true;
                    hdnPublication.Value = ds.Tables[2].Rows.Count.ToString();

                }


                foreach (ListViewDataItem item in lvpublicationJournal.Items)
                {
                    TextBox txtJournalApiClaimed = item.FindControl("txtJournalApiClaimed") as TextBox;
                    TextBox txtJournalVerifiedApiScore = item.FindControl("txtJournalVerifiedApiScore") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtJournalApiClaimed.Enabled = false;
                        txtJournalVerifiedApiScore.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtJournalApiClaimed.Enabled = false;
                        txtJournalVerifiedApiScore.Enabled = false;

                    }

                    else
                    {
                        txtJournalApiClaimed.Visible = true;
                        txtJournalVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvpublicationJournal.FindControl("lblverapisc").Visible = false;
                        txtJournalPub.Visible = false;
                        lblverapi.Visible = false;
                    }

                }





                // Published Books
                if (ds.Tables[3].Rows.Count > 0)
                {
                    lvPublishedBooks.DataSource = ds.Tables[3];
                    lvPublishedBooks.DataBind();
                    Session["TblPublishedBooks"] = ds.Tables[3];
                    DataTable dtPublishBook = (DataTable)Session["TblPublishedBooks"];
                }
                else
                {
                    PnlBook.Visible = false;
                    Session["TblPublishedBooks"] = null;
                    lvPublishedBooks.Visible = false;
                    lvPublishedBooks.DataSource = null;
                    lvPublishedBooks.DataBind();
                }

                foreach (ListViewDataItem item in lvPublishedBooks.Items)
                {
                    TextBox txtbookApiClaimed = item.FindControl("txtbookApiClaimed") as TextBox;
                    TextBox txtbookVerifiedApiScore = item.FindControl("txtbookVerifiedApiScore") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtbookApiClaimed.Enabled = false;
                        txtbookVerifiedApiScore.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtbookApiClaimed.Enabled = false;
                        txtbookVerifiedApiScore.Enabled = false;

                    }

                    else
                    {
                        txtbookApiClaimed.Visible = true;
                        txtbookVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvPublishedBooks.FindControl("lblverapis").Visible = false;
                        txtbookpub.Visible = false;
                        lblvscore.Visible = false;
                    }

                }





                // Chapters in book
                if (ds.Tables[4].Rows.Count > 0)
                {
                    lvPublishedChapter.DataSource = ds.Tables[4];
                    lvPublishedChapter.DataBind();
                    Session["TblChaptersInBook"] = ds.Tables[4];
                    DataTable PublishChapter = (DataTable)Session["TblChaptersInBook"];
                }
                else
                {
                    PnlChapters.Visible = false;
                    Session["TblChaptersInBook"] = null;
                    lvPublishedChapter.Visible = false;
                    lvPublishedChapter.DataSource = null;
                    lvPublishedChapter.DataBind();
                }


                foreach (ListViewDataItem item in lvPublishedChapter.Items)
                {
                    TextBox txtChapterApiClaimed = item.FindControl("txtChapterApiClaimed") as TextBox;
                    TextBox txtChapterVerifiedApiScore = item.FindControl("txtChapterVerifiedApiScore") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtChapterApiClaimed.Enabled = false;
                        txtChapterVerifiedApiScore.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtChapterApiClaimed.Enabled = false;
                        txtChapterVerifiedApiScore.Enabled = false;

                    }

                    else
                    {
                        txtChapterApiClaimed.Visible = true;
                        txtChapterVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvPublishedChapter.FindControl("lblvescore").Visible = false;
                        txtChapterPub.Visible = false;
                        lvlvescore.Visible = false;
                    }

                }










                // Conference Proceeds
                if (ds.Tables[5].Rows.Count > 0)
                {
                    lvConference.DataSource = ds.Tables[5];
                    lvConference.DataBind();
                    Session["TblConferenceProceeds"] = ds.Tables[5];
                    DataTable dtConference = (DataTable)Session["TblConferenceProceeds"];
                    hdnConference.Value = ds.Tables[5].Rows.Count.ToString();
                }
                else
                {
                    PnlConference.Visible = false;
                    Session["TblConferenceProceeds"] = null;
                    lvConference.Visible = false;
                    lvConference.DataSource = null;
                    lvConference.DataBind();
                    lblconfempserbook.Visible = true;
                    hdnConference.Value = ds.Tables[5].Rows.Count.ToString();
                }

                foreach (ListViewDataItem item in lvConference.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApi = item.FindControl("txtVerifiedApi") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApi.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvConference.FindControl("lblvesco").Visible = false;
                        txtConferencePub.Visible = false;
                        lblverfy.Visible = false;
                    }

                }







                // Avishkar
                if (ds.Tables[6].Rows.Count > 0)
                {
                    lvAvishkar.DataSource = ds.Tables[6];
                    lvAvishkar.DataBind();
                    Session["TblAvishkarDetails"] = ds.Tables[6];
                    DataTable dtAvishkar = (DataTable)Session["TblAvishkarDetails"];
                }
                else
                {
                    PnlAvishkar.Visible = false;
                    Session["TblAvishkarDetails"] = null;
                    lvAvishkar.Visible = false;
                    lvAvishkar.DataSource = null;
                    lvAvishkar.DataBind();
                }


                foreach (ListViewDataItem item in lvAvishkar.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApi = item.FindControl("txtVerifiedApi") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApi.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvAvishkar.FindControl("lblvers").Visible = false;
                        txtAvishkarPubl.Visible = false;
                        lblver12.Visible = false;
                    }

                }









                // On going and completed project consultancies
                if (ds.Tables[7].Rows.Count > 0)
                {
                    lvProjects.DataSource = ds.Tables[7];
                    lvProjects.DataBind();
                    Session["TblConsultancies"] = ds.Tables[7];
                    DataTable dtProject = (DataTable)Session["TblConsultancies"];
                }
                else
                {
                    PnlCompletedProject.Visible = false;
                    Session["TblConsultancies"] = null;
                    lvProjects.Visible = false;
                    lvProjects.DataSource = null;
                    lvProjects.DataBind();
                }
                foreach (ListViewDataItem item in lvProjects.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApi = item.FindControl("txtVerifiedApi") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApi.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvProjects.FindControl("lblvscore").Visible = false;
                        txtConsultancyApi.Visible = false;
                        lblver13.Visible = false;
                    }

                }



                // Patent
                if (ds.Tables[8].Rows.Count > 0)
                {
                    lvPatent.DataSource = ds.Tables[8];
                    lvPatent.DataBind();
                    Session["TblPatentIPR"] = ds.Tables[8];
                    DataTable dtPatent = (DataTable)Session["TblPatentIPR"];
                    hdnPatent.Value = ds.Tables[8].Rows.Count.ToString();
                }
                else
                {
                    PnlPatent.Visible = false;
                    Session["TblPatentIPR"] = null;
                    lvPatent.Visible = false;
                    lvPatent.DataSource = null;
                    lvPatent.DataBind();
                    lblpatentempserbook.Visible = true;
                    hdnPatent.Value = ds.Tables[8].Rows.Count.ToString();
                }





                foreach (ListViewDataItem item in lvPatent.Items)
                {
                    TextBox txtApiPatent = item.FindControl("txtApiPatent") as TextBox;
                    TextBox txtVerifiedPatent = item.FindControl("txtVerifiedPatent") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiPatent.Enabled = false;
                        txtVerifiedPatent.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiPatent.Enabled = false;
                        txtVerifiedPatent.Enabled = false;

                    }

                    else
                    {
                        txtApiPatent.Visible = true;
                        txtVerifiedPatent.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvPatent.FindControl("lvlver").Visible = false;
                        txtPatentVerified.Visible = false;
                        lblveri.Visible = false;
                    }

                }



                if (ds.Tables[16].Rows.Count > 0)
                {
                    lvResources.DataSource = ds.Tables[16];
                    lvResources.DataBind();
                    hdnMaterial.Value = ds.Tables[16].Rows.Count.ToString();

                }
                else
                {

                    MaterialResource();
                }





                foreach (ListViewDataItem item in lvResources.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApi = item.FindControl("txtVerifiedApi") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApi.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApi.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvResources.FindControl("lblverify").Visible = false;
                        txtMaterialVerifiedApi.Visible = false;
                        lblverify1.Visible = false;
                    }

                }










                // Research Guidance
                if (ds.Tables[9].Rows.Count > 0)
                {
                    lvEngagingLectures.DataSource = ds.Tables[9];
                    lvEngagingLectures.DataBind();
                    Session["TblGuidance"] = ds.Tables[9];
                    //ViewState["SN_ID"] = Convert.ToInt32(ds.Tables[9].AsEnumerable().Max(row => row["SN_ID"]));
                    DataTable dtResarchGuidance = (DataTable)Session["TblGuidance"];
                    //txtmaxw.Text = "50";
                }
                else
                {
                    EngagingLectures();

                }
                if (ds.Tables[10].Rows.Count > 0)
                {
                    lvGuidance.DataSource = ds.Tables[10];
                    lvGuidance.DataBind();
                    Session["TblGuidance"] = ds.Tables[10];
                    //   ViewState["SRNO"] = Convert.ToInt32(ds.Tables[10].AsEnumerable().Max(row => row["SRNO"]));
                    DataTable dtResarchGuidance = (DataTable)Session["TblGuidance"];
                    hdnGuidance.Value = ds.Tables[10].Rows.Count.ToString();

                }
                else
                {
                    Session["TblGuidance"] = null;
                    lvGuidance.Visible = false;
                    lvGuidance.DataSource = null;
                    lvGuidance.DataBind();
                }



                foreach (ListViewDataItem item in lvGuidance.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApiScore = item.FindControl("txtVerifiedApiScore") as TextBox;
                    ImageButton btnEditResarchPaper = item.FindControl("btnEditResarchPaper") as ImageButton;


                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = true;
                        btnEditResarchPaper.Enabled = false;

                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = false;
                        btnEditResarchPaper.Enabled = false;
                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvGuidance.FindControl("lblvery").Visible = false;
                        txtVerifiedGuidApi.Visible = false;
                        lblverify14.Visible = false;
                    }

                }



                // Research Qualification
                if (ds.Tables[11].Rows.Count > 0)
                {
                    lvQualification.DataSource = ds.Tables[11];
                    lvQualification.DataBind();
                    Session["TblQualification"] = ds.Tables[11];
                    //ViewState["SRNO"] = Convert.ToInt32(ds.Tables[11].AsEnumerable().Max(row => row["SRNO"]));
                    DataTable dtResarchQualification = (DataTable)Session["TblQualification"];
                }
                else
                {
                    Session["TblQualification"] = null;
                    lvQualification.Visible = false;
                    lvQualification.DataSource = null;
                    lvQualification.DataBind();
                }

                // Performance in attendance of Students
                if (ds.Tables[12].Rows.Count > 0)
                {
                    lvAttendance.DataSource = ds.Tables[12];
                    lvAttendance.DataBind();
                    Session["RecTblAttendance"] = ds.Tables[12];
                    // ViewState["SR_NO"] = Convert.ToInt32(ds.Tables[12].AsEnumerable().Max(row => row["SR_NO"]));
                    DataTable dtStudentAttendance = (DataTable)Session["RecTblAttendance"];
                  
                    //txtWeight.Text = "20";
                }
                else
                {
                    Session["RecTblAttendance"] = null;
                    lvAttendance.Visible = false;
                    lvAttendance.DataSource = null;
                    lvAttendance.DataBind();
                }

                // Performance In Results
                if (ds.Tables[13].Rows.Count > 0)
                {
                    lvResults.DataSource = ds.Tables[13];
                    lvResults.DataBind();
                    Session["RecTblPerformanceResults"] = ds.Tables[13];
                    DataTable dtPResults = (DataTable)Session["RecTblPerformanceResults"];
                    txtMaxWeight.Text = "20";
                }
                else
                {
                    Session["RecTblPerformanceResults"] = null;
                    lvResults.Visible = false;
                    lvResults.DataSource = null;
                    lvResults.DataBind();
                }

                if (ds.Tables[14].Rows.Count > 0)
                {
                    lvAcadDuties.DataSource = ds.Tables[14];
                    lvAcadDuties.DataBind();

                }
                else
                {

                    Duties_in_Excess_of_UGC_Norms();
                }
                if (ds.Tables[15].Rows.Count > 0)
                {
                    txtapi1.Text = ds.Tables[15].Rows[0]["LECTURE_AND_ACADEMIC_DUTIES_APISCORE"].ToString();
                    txtMaterialApiScore.Text = ds.Tables[15].Rows[0]["STUDY_M_API_SCORE_CLAIME"].ToString();
                    txtMaterialVerifiedApi.Text = ds.Tables[15].Rows[0]["STUDY_M_API_SCORE_VERIFY"].ToString();
                    txtApiScoretotal.Text = ds.Tables[15].Rows[0]["INNOVATIVE_API_SCORE_CLAIME"].ToString();
                    txtVerifiedApiS.Text = ds.Tables[15].Rows[0]["INNOVATIVE_API_SCORE_VERIFY"].ToString();
                    txtApi.Text = ds.Tables[15].Rows[0]["STUDENT_FEEDBACK_API_CLAIME"].ToString();
                    txtVerified.Text = ds.Tables[15].Rows[0]["STUDENT_FEEDBACK_API_VERIFY"].ToString();
                    txtAi.Text = ds.Tables[15].Rows[0]["EXAMINATION_API_SCORE_CLAIME"].ToString();
                    txtApiVerify.Text = ds.Tables[15].Rows[0]["EXAMINATION_API_SCORE_VEREFY"].ToString();
                    txtclaim.Text = ds.Tables[15].Rows[0]["STUDENT_API_SCORE_CLAIME"].ToString();
                    txtVery.Text = ds.Tables[15].Rows[0]["STUDENT_API_SCORE_VERIFY"].ToString();
                    txtVery.Text = ds.Tables[15].Rows[0]["STUDENT_API_SCORE_VERIFY"].ToString();
                    txtAcademicApi.Text = ds.Tables[15].Rows[0]["ADMINISTARTIVE_API_SCORE_CLAIME"].ToString();
                    txtAcademicVerifiedApi.Text = ds.Tables[15].Rows[0]["ADMINISTARTIVE_API_SCORE_VERIFY"].ToString();
                    txtDevelopmentApi.Text = ds.Tables[15].Rows[0]["DEVELOPEMENTE_API_SCORE_CLAIME"].ToString();
                    txtDevelopmentVerify.Text = ds.Tables[15].Rows[0]["ADMINISTARTIVE_API_SCORE_VERIFY"].ToString();
                    txtAverage.Text = ds.Tables[15].Rows[0]["STUDENT_AVG_ATTENDENCE"].ToString();
                    txtFactor.Text = ds.Tables[15].Rows[0]["STUDENT_MULTI_FACTOR"].ToString();
                   // txtWeight.Text = ds.Tables[15].Rows[0]["STUDENT_MAX_WEIGHT"].ToString();
                    txtScore.Text = ds.Tables[15].Rows[0]["STUDENT_API_SCORE"].ToString();
                    txtAvg.Text = ds.Tables[15].Rows[0]["RESULT_AVG_ATTENDENCE"].ToString();
                    txtMulFactor.Text = ds.Tables[15].Rows[0]["RESULT_MULTI_FACTOR"].ToString();
                   // txtMaxWeight.Text = ds.Tables[15].Rows[0]["RESULT_MAX_WEIGHT"].ToString();
                    txtClaimed.Text = ds.Tables[15].Rows[0]["RESULT_API_SCORE"].ToString();
                    txtGuidApi.Text = ds.Tables[15].Rows[0]["Res_Guid_Quali_Api_Claime_Tot"].ToString();
                    txtVerifiedGuidApi.Text = ds.Tables[15].Rows[0]["Res_Guid_Quali_Api_Verify_Tot"].ToString();
                    txtPubJournal.Text = ds.Tables[15].Rows[0]["PUBLISH_JOURNAL_API_CLAIME"].ToString();
                    txtJournalPub.Text = ds.Tables[15].Rows[0]["PUBLISH_JOURNAL_API_VERIFY"].ToString();
                    txtPubBook.Text = ds.Tables[15].Rows[0]["BOOK_API_CLAIME"].ToString();
                    txtbookpub.Text = ds.Tables[15].Rows[0]["BOOK_API_VERIFY"].ToString();
                    txtPubChapter.Text = ds.Tables[15].Rows[0]["CHAPTER_API_CLAIME"].ToString();
                    txtChapterPub.Text = ds.Tables[15].Rows[0]["CHAPTER_API_VERIFY"].ToString();
                    txtPubConfer.Text = ds.Tables[15].Rows[0]["CONFERENCE_API_CLAIME"].ToString();
                    txtConferencePub.Text = ds.Tables[15].Rows[0]["CONFEREMCE_API_VERIFY"].ToString();
                    txtAvishkarPubl.Text = ds.Tables[15].Rows[0]["AVISHKAR_API_VERIFY"].ToString();
                    txtPubliAvishkar.Text = ds.Tables[15].Rows[0]["AVISHKAR_API_CLAIME"].ToString();
                    txtApiProject.Text = ds.Tables[15].Rows[0]["PROJECT_API_CLAIME"].ToString();
                    txtConsultancyApi.Text = ds.Tables[15].Rows[0]["PROJECT_API_VERIFY"].ToString();
                    txtPatentApiScore.Text = ds.Tables[15].Rows[0]["PATENT_API_CLAIME"].ToString();
                    txtPatentVerified.Text = ds.Tables[15].Rows[0]["PATENT_API_VERIFY"].ToString();
                    txtavg1.Text = ds.Tables[15].Rows[0]["AVG_OF_COL"].ToString();
                    
                    txtlbl.Text = ds.Tables[15].Rows[0]["MULFLECTURE"].ToString();
                   // txtmaxw.Text = ds.Tables[15].Rows[0]["MAX_WLEC"].ToString();
                    txtapic.Text = ds.Tables[15].Rows[0]["APIS_LEC"].ToString();
                    txtapiv.Text = ds.Tables[15].Rows[0]["APIS_VER"].ToString();
                    txtVerify.Text = ds.Tables[15].Rows[0]["RESULT_API_VERIFY"].ToString();
                    txtapiperverify.Text = ds.Tables[15].Rows[0]["STUDENT_API_VERIFY"].ToString();
                    txtUGCVerifiedapi.Text = ds.Tables[15].Rows[0]["LECTURE_AND_ACADEMIC_DUTIES_VER"].ToString();
                    txtcategorytot.Text = ds.Tables[15].Rows[0]["CATEGORY_I_TOTAL"].ToString();
                    txtveryf.Text = ds.Tables[15].Rows[0]["CATEGORY_II_TOTAL"].ToString();
                    txtcatII.Text = ds.Tables[15].Rows[0]["CATEGORY_I_TOTAL2"].ToString();
                    TXTCATIIV.Text = ds.Tables[15].Rows[0]["CATEGORY_II_TOTAL2"].ToString();
                    txttot1.Text = ds.Tables[15].Rows[0]["TOTAL1"].ToString();
                    txttot2.Text = ds.Tables[15].Rows[0]["TOTAL2"].ToString();
                    txtpclaim.Text = ds.Tables[15].Rows[0]["CATEGORY_III_TOTAL1"].ToString();
                    txtpver.Text = ds.Tables[15].Rows[0]["CATEGORY_III_TOTAL2"].ToString();



                }


                if (ds.Tables[21].Rows.Count > 0)
                {
                    txtActivity.Text = ds.Tables[21].Rows[0]["NAME_OF_ACTIVITY"].ToString();
                    txtAPIAlloted.Text = ds.Tables[21].Rows[0]["API_SCORE_ALLOTTED"].ToString();
                    txtAPIScoreClaimed.Text = ds.Tables[21].Rows[0]["API_SCORE_CLAIMED"].ToString();
                    txtVerifiedScore.Text = ds.Tables[21].Rows[0]["VERIFIED_API_SCORE"].ToString();


                }










                Showlistview();

                if (Convert.ToInt32(ViewState["SRNO"]) != 1 && Convert.ToInt32(ViewState["SRNO"]) != 2)
                {
                    string lockByEmp = objCommon.LookUp("Appraisal_employee", "isnull(USER_LOCK,0)", "EMPLOYEE_ID=" + empid + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    if (lockByEmp == "1")
                    {

                        PanelHideForEmpFinalSubmit();
                        //return;
                    }
                    //else
                    //{
                    //    PanelShowForEmpFinalSubmit();
                    //    //return;
                    //}
                }


                if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                {

                    string Authority1 = objCommon.LookUp("Appraisal_employee", "isnull(REPORT_LOCK,0)", "EMPLOYEE_ID=" + empid + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    if (Authority1 == "1")
                    {

                        PanelHideForEmpFinalSubmit();
                        //return;
                    }
                   // PnlAttendance.Enabled = false;
                   // PnlPerformingResult.Enabled = false;
                    FeildEnableF();
                   

                }

                 

                
                if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                {

                    string Authority2 = objCommon.LookUp("Appraisal_employee", "isnull(REVIEW_LOCK,0)", "EMPLOYEE_ID=" + empid + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    if (Authority2 == "1")
                    {

                        PanelHideForEmpFinalSubmit();
                        //return;
                    }
                    else
                    {
                        PanelHideForEmpFinalSubmit();
                        //return;
                    }

                }



            }

            else
            {
                clearAttendance();
                clearresarchpaper();
                clearsponserdelement();
                return;
            }

        }
        else
        {
            Session["SRNO"] = null;
            ViewState["SRNO_Guidance"] = null;
            Session["TblGuidance"] = null;
            lvGuidance.Visible = false;
            lvGuidance.DataSource = null;
            lvGuidance.DataBind();

            ViewState["SRNO_Qualification"] = null;
            Session["TblQualification"] = null;
            lvQualification.Visible = false;
            lvQualification.DataSource = null;
            lvQualification.DataBind();

            ViewState["SRNO_Performance"] = null;
            Session["RecTblPerformanceResults"] = null;
            lvResults.Visible = false;
            lvResults.DataSource = null;
            lvResults.DataBind();

            //ViewState["SRNO"] = null;
            //Session["TblGuidance"] = null;
            //lvGuidance.Visible = false;
            //lvGuidance.DataSource = null;
            //lvGuidance.DataBind();

            ViewState["SRNO_Attendance"] = null;
            Session["RecTblStudentAttendance"] = null;
            lvAttendance.Visible = false;
            lvAttendance.DataSource = null;
            lvAttendance.DataBind();

            GetEmployeeDetails();


            SessionNull();
            BindlistData();
            BindConferencelistData();
            BindPatentListView();
        }

    }

    private void FeildEnableF()
    {
        lvEngagingLectures.Enabled = false;
        txtmaxw.Enabled = false;
        txtapic.Enabled = false;
        PnlLearning.Enabled = false;
        btnStudentsAttendance.Enabled = false;
        lvAttendance.Enabled = false;
        txtWeight.Enabled = false;
        txtScore.Enabled = false;
        btnPerformanceInResult.Enabled = false;
        lvResults.Enabled = false;
        txtMaxWeight.Enabled = false;
        txtClaimed.Enabled = false;
    }
    //#endregion


    #region "LinkButton_Click For Emp"
    protected void LinkButton_PersonalInfo_Click(object sender, EventArgs e)
    {

        MultiView_Profile.ActiveViewIndex = -1;
        MultiView_Profile.SetActiveView(View_PersonalInfo);
        trSession.Visible = true;

    }

    protected void LinkButton_Teaching_Learning_Activities_Click(object sender, EventArgs e)
    {
        //MultiView_Profile.ActiveViewIndex = -1;
        //MultiView_Profile.SetActiveView(Teaching_Learning_Activities);
        //trSession.Visible = true;
    }

    protected void LinkButton_Published_Journal_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Published_Journal);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Papers_In_Conference_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Papers_In_Conference_Proceeds);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Patent_IPR_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Patent_IPR);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Research_Guidance_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Research_Guidance);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Corporate_Life_Community_Work_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Field_Based_Activity);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Attendance_Performance_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Performance_In_Attendance);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Academic_Lectures_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Innovative_Teaching);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Administrative_And_Academic_Click(object sender, EventArgs e)
    {
        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(View_Administrative_Academic);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Teacher_Learning_Activities_Click(object sender, EventArgs e)
    {

        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Teacher_Learning_Activities);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Performance_In_Engaging_Lectures_Click(object sender, EventArgs e)
    {

        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Performance_In_Engaging_Lectures);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Duties_In_Excess_Of_UGC_Norms_Click(object sender, EventArgs e)
    {

        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Lectures_Academic_Duties);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Student_Co_Curricular_Click(object sender, EventArgs e)
    {

        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(Field_Based_Activity);
            trSession.Visible = true;
        }
    }

    protected void LinkButton_Reviewing_Click(object sender, EventArgs e)
    {

        string Eid = objCommon.LookUp("APPRAISAL_EMPLOYEE", "EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (Eid == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Personal Information First.');", true);
            return;
        }
        else
        {

            int cat1 = 0;
            int cat2 = 0;
            cat1 = Convert.ToInt32(txttot2.Text);
            if (txtpver.Text == "")
            {
                cat2 = 0;
            }
            else
            {
                cat2 = Convert.ToInt32(txtpver.Text);

            }
            txtNumericalGrading.Text = Convert.ToString(cat1 + cat2);
            MultiView_Profile.ActiveViewIndex = -1;
            MultiView_Profile.SetActiveView(View_Reviewing);
            trSession.Visible = true;

            
        }
    }

    #endregion

    #region "View Activate for Emp"
    protected void View_PersonalInfo_Activate(object sender, EventArgs e)
    {
        LinkButton_PersonalInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");

    }

    protected void View_Published_Journal_Activate(object sender, EventArgs e)
    {
        LinkButton_Published_Journal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");

    }

    protected void View_Papers_In_Conference_Activate(object sender, EventArgs e)
    {
        LinkButton_Papers_In_Conference.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Patent_IPR_Activate(object sender, EventArgs e)
    {
        LinkButton_Patent_IPR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Research_Guidance_Activate(object sender, EventArgs e)
    {
        LinkButton_Research_Guidance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Student_Co_Curricular_Activate(object sender, EventArgs e)
    {
        LinkButton_Corporate_Life_Community_Work.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Performance_In_Attendance_Activate(object sender, EventArgs e)
    {
        LinkButton_Attendance_Performance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Innovative_Teaching_Activate(object sender, EventArgs e)
    {
        LinkButton_Academic_Lectures.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Teacher_Learning_Activities_Activate(object sender, EventArgs e)
    {
        LinkButton_Teacher_Learning_Activities.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Performance_In_Engaging_Lectures_Activate(object sender, EventArgs e)
    {
        LinkButton_Performance_In_Engaging_Lectures.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Lectures_Academic_Duties_Activate(object sender, EventArgs e)
    {
        LinkButton_Duties_In_Excess_Of_UGC_Norms.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Administrative_Academic_Activate(object sender, EventArgs e)
    {
        LinkButton_Administrative_And_Academic.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Field_Based_Activity_Activate(object sender, EventArgs e)
    {
        LinkButton_Student_Co_Curricular.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    protected void View_Reviewing_Activate(object sender, EventArgs e)
    {
        LinkButton_Reviewing_Officers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#820000");
    }

    #endregion

    #region "View DeActivate For Emp"

    protected void View_PersonalInfo_Deactivate(object sender, EventArgs e)
    {

        LinkButton_PersonalInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");

    }
    protected void View_Published_Journal_Deactivate(object sender, EventArgs e)
    {

        LinkButton_Published_Journal.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");

    }

    protected void View_Papers_In_Conference_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Papers_In_Conference.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Patent_IPR_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Patent_IPR.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Research_Guidance_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Research_Guidance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Student_Co_Curricular_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Corporate_Life_Community_Work.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Performance_In_Attendance_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Attendance_Performance.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Innovative_Teaching_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Academic_Lectures.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Teacher_Learning_Activities_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Teacher_Learning_Activities.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Performance_In_Engaging_Lectures_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Performance_In_Engaging_Lectures.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Administrative_Academic_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Administrative_And_Academic.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Lectures_Academic_Duties_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Duties_In_Excess_Of_UGC_Norms.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Field_Based_Activity_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Student_Co_Curricular.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    protected void View_Reviewing_Deactivate(object sender, EventArgs e)
    {
        LinkButton_Reviewing_Officers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
    }

    #endregion


    #region "SUBMIT DATE FOR EMPLOYEE"

    protected void btnPersonalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objEA.APPRAISAL_EMPLOYEE_ID = 0;
            objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
            objEA.EMPLOYEE_NAME = txtName.Text.Trim();
            objEA.DEPARTMENT_ID = Convert.ToInt32(ddlDept.SelectedValue); // == string.Empty ? 0 : Convert.ToInt32(ddlDept.SelectedValue); 
            objEA.DESIGNATION_ID = Convert.ToInt32(ddlCDesig.SelectedValue);// == string.Empty ? 0 : Convert.ToInt32(ddlCDesig.SelectedValue);
            objEA.NAME_OF_INSTITUTION = Convert.ToInt32(ddlInstitute.SelectedValue);
            objEA.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objEA.COLLEGE_NO = Convert.ToInt32(Session["college_nos"]);
            objEA.COLLEGE_CODE = Convert.ToInt32(Session["colcode"]);

            objEA.PAPNO = Convert.ToInt32(hdnPAPNO.Value.Trim() == "" ? "0" : hdnPAPNO.Value);

            if (ViewState["APPRAISAL_EMPLOYEE_ID"] == null)
            {

                CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePersonalDetails(objEA);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    // clearpersnol();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    // Response.Redirect("EmployeeAppraisalForm.aspx?pageno=2798", false);
                }
            }
            else
            {
                //if (ViewState["APPRAISAL_EMPLOYEE_ID"] != null)
                //    objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());
                //else
                //    objEA.APPRAISAL_EMPLOYEE_ID = 0;


                CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePersonalDetails(objEA);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    // clearpersnol();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    //  Response.Redirect("EmployeeAppraisalForm.aspx?pageno=2798", false);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPersonalSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnPublishedDetailsSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {







                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);



                    DataTable dtpublishjournal = new DataTable();

                    dtpublishjournal.Columns.Add(new DataColumn("TITLE_PAGENO", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("JOURNAL", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("ISBN", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("PEER_REVIEWED", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("IMPACTFACTORS", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("MAIN_AUTHOR", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("CO_AUTHOR", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                    dtpublishjournal.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                    DataRow dr = null;
                    foreach (ListViewItem i in lvpublicationJournal.Items)
                    {

                        Label lblTitle = (Label)i.FindControl("lblTitle");
                        Label lblJournal = (Label)i.FindControl("lblJournal");
                        Label lblISBN = (Label)i.FindControl("lblISBN");
                        Label lblPeerReviewed = (Label)i.FindControl("lblPeerReviewed");
                        Label lblImpactFactor = (Label)i.FindControl("lblImpactFactor");
                        Label lblMainAuthor = (Label)i.FindControl("lblMainAuthor");
                        Label lblCoAuthor = (Label)i.FindControl("lblCoAuthor");
                        TextBox txtJournalApiClaimed = (TextBox)i.FindControl("txtJournalApiClaimed");
                        TextBox txtJournalVerifiedApiScore = (TextBox)i.FindControl("txtJournalVerifiedApiScore");




                        dr = dtpublishjournal.NewRow();
                        dr["TITLE_PAGENO"] = lblTitle.Text;
                        dr["JOURNAL"] = lblJournal.Text;
                        dr["ISBN"] = lblISBN.Text;
                        dr["PEER_REVIEWED"] = lblPeerReviewed.Text;
                        dr["IMPACTFACTORS"] = lblImpactFactor.Text;
                        dr["MAIN_AUTHOR"] = lblMainAuthor.Text;
                        dr["CO_AUTHOR"] = lblCoAuthor.Text;
                        dr["API_SCORE_CLAIMED"] = txtJournalApiClaimed.Text;
                        dr["VERIFIED_API_SCORE"] = txtJournalVerifiedApiScore.Text;



                        dtpublishjournal.Rows.Add(dr);

                    }

                    objEA.PUBLISHEDJOURNAL = dtpublishjournal;







                    DataTable dtbook = new DataTable();

                    dtbook.Columns.Add(new DataColumn("Title_Of_Book", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("Publisher_Name", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("PUBLICATION_TYPE", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("ISBN", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("MAIN_AUTHOR", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("CO_AUTHOR", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                    dtbook.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                    DataRow dr1 = null;
                    foreach (ListViewItem i in lvPublishedBooks.Items)
                    {

                        Label lblbookTitle = (Label)i.FindControl("lblbookTitle");
                        Label lblPublisherName = (Label)i.FindControl("lblPublisherName");
                        Label lblPublicationTypes = (Label)i.FindControl("lblPublicationTypes");
                        Label lblISSNNo = (Label)i.FindControl("lblISSNNo");
                        Label lblMainAuthor = (Label)i.FindControl("lblMainAuthor");
                        Label lblCoAuthor = (Label)i.FindControl("lblCoAuthor");
                        TextBox txtbookApiClaimed = (TextBox)i.FindControl("txtbookApiClaimed");
                        TextBox txtbookVerifiedApiScore = (TextBox)i.FindControl("txtbookVerifiedApiScore");




                        dr1 = dtbook.NewRow();
                        dr1["Title_Of_Book"] = lblbookTitle.Text;
                        dr1["Publisher_Name"] = 0;
                        dr1["PUBLICATION_TYPE"] = lblPublicationTypes.Text;
                        dr1["ISBN"] = lblISSNNo.Text;
                        dr1["MAIN_AUTHOR"] = lblMainAuthor.Text;
                        dr1["CO_AUTHOR"] = lblCoAuthor.Text;
                        dr1["API_SCORE_CLAIMED"] = txtbookApiClaimed.Text;
                        dr1["VERIFIED_API_SCORE"] = txtbookVerifiedApiScore.Text;



                        dtbook.Rows.Add(dr1);

                    }

                    objEA.PUBLISHEDBOOKS = dtbook;







                    DataTable dtchapter = new DataTable();

                    dtchapter.Columns.Add(new DataColumn("Title_Of_Books", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("Publisher_Name", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("PUBLICATION_TYPE", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("ISBN", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("No_of_Chapters", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                    dtchapter.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                    DataRow dr2 = null;
                    foreach (ListViewItem i in lvPublishedChapter.Items)
                    {

                        Label lblbookTitle = (Label)i.FindControl("lblbookTitle");
                        Label lblPublisherName = (Label)i.FindControl("lblPublisherName");
                        Label lblPublicationTypes = (Label)i.FindControl("lblPublicationTypes");
                        Label lblISSNNo = (Label)i.FindControl("lblISSNNo");
                        Label lblNoChapters = (Label)i.FindControl("lblNoChapters");

                        TextBox txtChapterApiClaimed = (TextBox)i.FindControl("txtChapterApiClaimed");
                        TextBox txtChapterVerifiedApiScore = (TextBox)i.FindControl("txtChapterVerifiedApiScore");




                        dr2 = dtchapter.NewRow();
                        dr2["Title_Of_Books"] = lblbookTitle.Text;
                        dr2["Publisher_Name"] = 0;
                        dr2["PUBLICATION_TYPE"] = lblPublicationTypes.Text;
                        dr2["ISBN"] = lblISSNNo.Text;
                        dr2["No_of_Chapters"] = lblNoChapters.Text;
                        dr2["API_SCORE_CLAIMED"] = txtChapterApiClaimed.Text;
                        dr2["VERIFIED_API_SCORE"] = txtChapterVerifiedApiScore.Text;



                        dtchapter.Rows.Add(dr2);

                    }

                    objEA.CHAPTERINBOOK = dtchapter;

                    if (txtPubJournal.Text == "")
                    {
                        objEA.PUBLISH_JOURNAL_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.PUBLISH_JOURNAL_API_CLAIME = Convert.ToInt32(txtPubJournal.Text);
                    }
                   
                    if (txtJournalPub.Text == "")
                    {
                        objEA.PUBLISH_JOURNAL_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.PUBLISH_JOURNAL_API_VERIFY = Convert.ToInt32(txtJournalPub.Text);

                    }
                   

                    if (txtPubBook.Text == "")
                    {
                        objEA.BOOK_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.BOOK_API_CLAIME = Convert.ToInt32(txtPubBook.Text);
                    }

                    if (txtbookpub.Text == "")
                    {
                        objEA.BOOK_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.BOOK_API_VERIFY = Convert.ToInt32(txtbookpub.Text);

                    }
                   

                    if (txtPubChapter.Text == "")
                    {
                        objEA.CHAPTER_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.CHAPTER_API_CLAIME = Convert.ToInt32(txtPubChapter.Text);

                    }
                    if (txtChapterPub.Text == "")
                    {
                        objEA.CHAPTER_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.CHAPTER_API_VERIFY = Convert.ToInt32(txtChapterPub.Text);

                    }




                    //DataTable dtTP = (DataTable)Session["TblPublishedJournal"];
                    //objEA.PUBLISHEDJOURNAL = dtTP;


                    //DataTable dtTE = (DataTable)Session["TblPublishedBooks"];
                    //objEA.PUBLISHEDBOOKS = dtTE;


                    //DataTable dtTR = (DataTable)Session["TblChaptersInBook"];
                    //objEA.CHAPTERINBOOK = dtTR;


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePublicationDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePublicationDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(View_PersonalInfo);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPublishedDetailsSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnConferenceSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);


                    DataTable dtconference = new DataTable();

                    dtconference.Columns.Add(new DataColumn("TITLE_PAGENO", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("TYPE", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("CONFERENCE_PUBLICATION", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("ABSTRACT", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("ISBN", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("CO_AUTHOR", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("MAIN_AUTHOR", typeof(string)));
                    dtconference.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(int)));
                    dtconference.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(int)));






                    DataRow dr = null;
                    foreach (ListViewItem i in lvConference.Items)
                    {

                        Label lblTitle = (Label)i.FindControl("lblTitle");
                        Label lblPublicationType = (Label)i.FindControl("lblPublicationType");
                        Label lblPubConference = (Label)i.FindControl("lblPubConference");
                        Label lblAbstract = (Label)i.FindControl("lblAbstract");
                        Label lblImpactFactor = (Label)i.FindControl("lblImpactFactor");
                        Label lblCoAuthor = (Label)i.FindControl("lblCoAuthor");
                        Label lblISBN = (Label)i.FindControl("lblISBN");
                        TextBox txtApiScore = (TextBox)i.FindControl("txtApiScore");
                        TextBox txtVerifiedApi = (TextBox)i.FindControl("txtVerifiedApi");




                        dr = dtconference.NewRow();
                        dr["TITLE_PAGENO"] = lblTitle.Text;
                        dr["TYPE"] = lblPublicationType.Text;
                        dr["CONFERENCE_PUBLICATION"] = lblPubConference.Text;
                        dr["ABSTRACT"] = lblAbstract.Text;
                        dr["ISBN"] = lblISBN.Text;
                        dr["MAIN_AUTHOR"] = lblAbstract.Text;
                        dr["CO_AUTHOR"] = lblCoAuthor.Text;
                        dr["API_SCORE_CLAIMED"] = txtApiScore.Text;
                        if (txtVerifiedApi.Text == "")
                        {
                            dr["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            dr["VERIFIED_API_SCORE"] = txtVerifiedApi.Text;
                        }



                        dtconference.Rows.Add(dr);

                    }

                    //objEA.PUBLISHEDJOURNAL = dtavishkar;
                    objEA.PAPER_IN_CONFERENCE = dtconference;







                    DataTable dtavishkar = new DataTable();

                    dtavishkar.Columns.Add(new DataColumn("TITLE_PAPER", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("AVISHKAR", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("PRIZE_WON", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("CO_AUTHOR", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("MAIN_AUTHOR", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                    dtavishkar.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                    DataRow dravishkar = null;
                    foreach (ListViewItem i in lvAvishkar.Items)
                    {

                        Label lblTitlePaper = (Label)i.FindControl("lblTitlePaper");
                        Label lblAvishkar = (Label)i.FindControl("lblAvishkar");
                        Label lblPrizeWon = (Label)i.FindControl("lblPrizeWon");
                        Label lblMainAuthor = (Label)i.FindControl("lblMainAuthor");
                        Label lblImpactFactor = (Label)i.FindControl("lblImpactFactor");
                        Label lblCoAuthor = (Label)i.FindControl("lblCoAuthor");

                        TextBox txtApiScore = (TextBox)i.FindControl("txtApiScore");

                        TextBox txtVerifiedApi = (TextBox)i.FindControl("txtVerifiedApi");




                        dravishkar = dtavishkar.NewRow();
                        dravishkar["TITLE_PAPER"] = lblTitlePaper.Text;
                        dravishkar["AVISHKAR"] = lblAvishkar.Text;
                        dravishkar["PRIZE_WON"] = lblPrizeWon.Text;
                        dravishkar["MAIN_AUTHOR"] = lblMainAuthor.Text;
                        dravishkar["CO_AUTHOR"] = lblCoAuthor.Text;
                        dravishkar["API_SCORE_CLAIMED"] = txtApiScore.Text;

                        if (txtVerifiedApi.Text == "")
                        {
                            dravishkar["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {

                            dravishkar["VERIFIED_API_SCORE"] = txtVerifiedApi.Text;
                        }


                        dtavishkar.Rows.Add(dravishkar);

                    }

                    //objEA.PUBLISHEDJOURNAL = dtavishkar;
                    objEA.AVISHKAR_DETAILS = dtavishkar;





                    DataTable dtproject = new DataTable();

                    dtproject.Columns.Add(new DataColumn("TITLE", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("AGENCY", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("PERIOD", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("AMOUNT_MOBILIZED", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("PROJECT_TYPE", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("PRICIPAL_INVESTIGATOR", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("NO_OF_CO_INVESTOR", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                    dtproject.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                    DataRow drproject = null;
                    foreach (ListViewItem i in lvProjects.Items)
                    {

                        Label lblTitle = (Label)i.FindControl("lblTitle");
                        Label lblAgency = (Label)i.FindControl("lblAgency");
                        Label lblPeriod = (Label)i.FindControl("lblPeriod");
                        Label lblAmountM = (Label)i.FindControl("lblAmountM");
                        Label lblProjectType = (Label)i.FindControl("lblProjectType");
                        Label lblCoAuthor = (Label)i.FindControl("lblCoAuthor");
                        Label lblPrincipal = (Label)i.FindControl("lblPrincipal");
                        Label lblInvestor = (Label)i.FindControl("lblInvestor");
                        TextBox txtApiScore = (TextBox)i.FindControl("txtApiScore");
                        TextBox txtVerifiedApi = (TextBox)i.FindControl("txtVerifiedApi");




                        drproject = dtproject.NewRow();
                        drproject["TITLE"] = lblTitle.Text;
                        drproject["AGENCY"] = lblAgency.Text;
                        drproject["PERIOD"] = lblPeriod.Text;
                        drproject["AMOUNT_MOBILIZED"] = lblAmountM.Text;
                        drproject["PROJECT_TYPE"] = lblProjectType.Text;
                        drproject["PRICIPAL_INVESTIGATOR"] = lblPrincipal.Text;
                        drproject["NO_OF_CO_INVESTOR"] = lblInvestor.Text;
                        drproject["API_SCORE_CLAIMED"] = txtApiScore.Text;
                        if (txtVerifiedApi.Text == "")
                        {

                            drproject["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            drproject["VERIFIED_API_SCORE"] = txtVerifiedApi.Text;
                        }


                        dtproject.Rows.Add(drproject);

                    }

                    //objEA.PUBLISHEDJOURNAL = dtavishkar;
                    objEA.PROJECT_CONSULTANCIES = dtproject;



                    if (txtConferencePub.Text == "")
                    {
                        objEA.CONFEREMCE_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.CONFEREMCE_API_VERIFY = Convert.ToInt32(txtConferencePub.Text);
                    }


                    if (txtPubConfer.Text == "")
                    {

                        objEA.CONFERENCE_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.CONFERENCE_API_CLAIME = Convert.ToInt32(txtPubConfer.Text);
                    }

                    if (txtAvishkarPubl.Text == "")
                    {
                        objEA.AVISHKAR_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.AVISHKAR_API_VERIFY = Convert.ToInt32(txtAvishkarPubl.Text);
                    }
                    //objEA.AVISHKAR_API_CLAIME = Convert.ToInt32(txtPubliAvishkar.Text);

                    if (txtPubliAvishkar.Text == "")
                    {
                        objEA.AVISHKAR_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.AVISHKAR_API_CLAIME = Convert.ToInt32(txtPubliAvishkar.Text);
                    }


                    if (txtConsultancyApi.Text == "")
                    {
                        objEA.PROJECT_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.PROJECT_API_VERIFY = Convert.ToInt32(txtConsultancyApi.Text);

                    }
                    //objEA.PROJECT_API_CLAIME = Convert.ToInt32(txtApiProject.Text);

                    if (txtApiProject.Text == "")
                    {
                        objEA.PROJECT_API_CLAIME = 0;
                    }
                    else
                    {
                        objEA.PROJECT_API_CLAIME = Convert.ToInt32(txtApiProject.Text);
                    }






                    //DataTable dtTP = (DataTable)Session["TblConferenceProceeds"];
                    //objEA.PAPER_IN_CONFERENCE = dtTP;

                    ////

                    //DataTable dtTE = (DataTable)Session["TblAvishkarDetails"];
                    //objEA.AVISHKAR_DETAILS = dtTE;

                    ////

                    //DataTable dtTR = (DataTable)Session["TblConsultancies"];
                    //objEA.PROJECT_CONSULTANCIES = dtTR;


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateConferenceProceedDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateConferenceProceedDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(View_PersonalInfo);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnConferenceSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnPatentSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {

                if (Convert.ToInt32(ViewState["SRNO"]) != 1 && Convert.ToInt32(ViewState["SRNO"]) != 2)
                {
                    int publsum1 = 0;
                    int publsum2 = 0;
                    int publsum3 = 0;
                    int publsum4 = 0;
                    int guidsum5 = 0;
                    int patentsum6 = 0;
                    int categotyIIIc = 0;


                    if (txtPubJournal.Text == "")
                    {
                        publsum1 = 0;
                    }
                    else
                    {
                        publsum1 = Convert.ToInt32(txtPubJournal.Text);
                    }
                    if (txtPubBook.Text == "")
                    {
                        publsum2 = 0;
                    }
                    else
                    {
                        publsum2 = Convert.ToInt32(txtPubBook.Text);
                  
                    }
                    if (txtPubChapter.Text == "")
                    {
                        publsum3 = 0;
                    }
                    else
                    {
                        publsum3 = Convert.ToInt32(txtPubChapter.Text);
                    }
                    if (txtPubConfer.Text == "")
                    {
                         publsum4  = 0;
                    }
                    else
                    {
                    publsum4 = Convert.ToInt32(txtPubConfer.Text);
                    }
                    if (txtGuidApi.Text == "")
                    {
                        guidsum5 = 0;
                    }
                    else
                    {
                        guidsum5 = Convert.ToInt32(txtGuidApi.Text);
                    }
                    if (txtPatentApiScore.Text == "")
                    {
                        patentsum6 = 0;
                    }
                    else
                    {
                        patentsum6 = Convert.ToInt32(txtPatentApiScore.Text);

                    }
                  
                    categotyIIIc = publsum1 + publsum2 + publsum3 + publsum4 + guidsum5 + patentsum6;

                    txtpclaim.Text = Convert.ToString(categotyIIIc);
                }
                objEA.CAT_III_TOTAL1 = Convert.ToInt32(txtpclaim.Text);

                if (txtpver.Text == "")
                {
                    objEA.CAT_III_TOTAL2 = 0;
                }
                else
                {
                    objEA.CAT_III_TOTAL2 =Convert.ToInt32(txtpver.Text);
                }


                

                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                    #region
                    // First Add All Column Names.
                    Session["DtPatent"] = null;
                    DataTable DtPatent = new DataTable();
                    if (Session["DtPatent"] != null)
                        DtPatent = (DataTable)Session["DtPatent"];
                    if (!(DtPatent.Columns.Contains("PUBLISHED_NO")))
                        DtPatent.Columns.Add("PUBLISHED_NO");
                    if (!(DtPatent.Columns.Contains("TITLE")))
                        DtPatent.Columns.Add("TITLE");
                    if (!(DtPatent.Columns.Contains("REG_NO")))
                        DtPatent.Columns.Add("REG_NO");
                    if (!(DtPatent.Columns.Contains("SUBMITTED")))
                        DtPatent.Columns.Add("SUBMITTED");
                    if (!(DtPatent.Columns.Contains("GRANTED")))
                        DtPatent.Columns.Add("GRANTED");
                    if (!(DtPatent.Columns.Contains("API_SCORE_CLAIMED")))
                        DtPatent.Columns.Add("API_SCORE_CLAIMED");
                    if (!(DtPatent.Columns.Contains("VERIFIED_API_SCORE")))
                        DtPatent.Columns.Add("VERIFIED_API_SCORE");


                    //find column
                    foreach (ListViewItem itemRow in lvPatent.Items)
                    {
                        Label Label15 = (Label)itemRow.FindControl("Label15") as Label;
                        Label lblTitle = (Label)itemRow.FindControl("lblTitle") as Label;
                        Label lblRegNo = (Label)itemRow.FindControl("lblRegNo") as Label;
                        Label lblSubmitted = (Label)itemRow.FindControl("lblSubmitted") as Label;
                        Label lblGranted = (Label)itemRow.FindControl("lblGranted") as Label;
                        TextBox txtApiPatent = (TextBox)itemRow.FindControl("txtApiPatent") as TextBox;
                        TextBox txtVerifiedPatent = (TextBox)itemRow.FindControl("txtVerifiedPatent") as TextBox;


                        DataRow drPatent = DtPatent.NewRow();
                        drPatent["PUBLISHED_NO"] = Convert.ToInt32(Label15.Text);
                        drPatent["TITLE"] = lblTitle.Text;
                        drPatent["REG_NO"] = Convert.ToInt32(lblRegNo.Text);
                        drPatent["SUBMITTED"] = Convert.ToInt32(lblSubmitted.Text);
                        drPatent["GRANTED"] = Convert.ToInt32(lblGranted.Text);
                       


                        if (txtApiPatent.Text == "")
                        {
                            drPatent["API_SCORE_CLAIMED"] = 0;
                        }
                        else
                        {
                            drPatent["API_SCORE_CLAIMED"] = Convert.ToInt32(txtApiPatent.Text);

                        }
                        if (txtVerifiedPatent.Text == "")
                        {
                            drPatent["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            drPatent["VERIFIED_API_SCORE"] = Convert.ToInt32(txtVerifiedPatent.Text);


                        }

                        //insert row in datatable
                        DtPatent.Rows.Add(drPatent);

                    }
                    #endregion
                    objEA.PATENTDETAILS = DtPatent;


                    objEA.PATENT_API_CLAIME = Convert.ToInt32(txtPatentApiScore.Text);
                    if (txtPatentVerified.Text == "")
                    {
                        objEA.PATENT_API_VERIFY = 0;
                    }
                    else
                    {
                        objEA.PATENT_API_VERIFY = Convert.ToInt32(txtPatentVerified.Text);

                    }


                    // DataTable dtTP = (DataTable)Session["TblPatentIPR"];
                    // objEA.PATENTDETAILS = dtTP;

                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePatentDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePatentDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(Patent_IPR);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPatentSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnResearchSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                //string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                //string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                //if (ApprcEmp_Id != "")
                //{
                int maxVals = 0;

                objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);




                DataTable dtqualification = new DataTable();

                dtqualification.Columns.Add(new DataColumn("SRNO", typeof(int)));
                dtqualification.Columns.Add(new DataColumn("Research_Guidance", typeof(string)));
                dtqualification.Columns.Add(new DataColumn("Number_Enrolled", typeof(string)));
                dtqualification.Columns.Add(new DataColumn("Thesis_Submitted", typeof(string)));
                dtqualification.Columns.Add(new DataColumn("Degree_Awarded", typeof(string)));
                dtqualification.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(string)));
                dtqualification.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(string)));





                DataRow dr = null;
                foreach (ListViewItem i in lvGuidance.Items)
                {

                    Label lblRGuidance = (Label)i.FindControl("lblRGuidance");
                    Label lblNEnrolled = (Label)i.FindControl("lblNEnrolled");
                    Label lblThesis = (Label)i.FindControl("lblThesis");
                    Label lblAwarded = (Label)i.FindControl("lblAwarded");

                    TextBox txtApiScore = (TextBox)i.FindControl("txtApiScore");
                    TextBox txtVerifiedApiScore = (TextBox)i.FindControl("txtVerifiedApiScore");




                    dr = dtqualification.NewRow();
                    dr["SRNO"] = maxVals + 1;
                    dr["Research_Guidance"] = lblRGuidance.Text;
                    dr["Number_Enrolled"] = lblNEnrolled.Text;
                    dr["Thesis_Submitted"] = lblThesis.Text;
                    dr["Degree_Awarded"] = lblAwarded.Text;
                    dr["API_SCORE_CLAIMED"] = txtApiScore.Text;
                    dr["VERIFIED_API_SCORE"] = txtVerifiedApiScore.Text;




                    dtqualification.Rows.Add(dr);

                }

                objEA.RESEARCHGUIDANCE = dtqualification;


                objEA.GUIDANCE_QUALIFICATION_VERIFY_TOT = Convert.ToInt32(txtGuidApi.Text);
                if (txtVerifiedGuidApi.Text == "")
                {
                    objEA.GUIDANCE_QUALIFICATION_CLAIME_TOT = 0;
                }
                else
                {
                    objEA.GUIDANCE_QUALIFICATION_CLAIME_TOT = Convert.ToInt32(txtVerifiedGuidApi.Text);

                }












                //DataTable dtTP = (DataTable)Session["TblGuidance"];
                //objEA.RESEARCHGUIDANCE = dtTP;


                DataTable dtTE = (DataTable)Session["TblQualification"];
                objEA.RESEARCHQUALIFICATION = dtTE;


                if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                {
                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateResearchDetails(objEA);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        ClearRecFromResarchPaper();
                    }
                }
                else
                {
                    objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateResearchDetails(objEA);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    }
                }

            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
            //    MultiView_Profile.ActiveViewIndex = -1;
            //    MultiView_Profile.SetActiveView(Research_Guidance);
            //    trSession.Visible = true;

                //}


            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnResearchSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnPerformanceSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                ViewState["APPRAISAL_EMPLOYEE_ID"] = ApprcEmp_Id;
                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);
                    // DataTable dtTP = (DataTable)Session["RecTblStudentAttendance"];
                    //objEA.ATTENDANCEPERFORMANCE = (DataTable)Session["RecTblAttendance"]; ;
                    objEA.ATTENDANCEPERFORMANCE = (DataTable)Session["RecTblAttendance"];


                    // DataTable dtTE = (DataTable)Session["RecTblPerformanceResults"];
                    objEA.PERFORMANCERESULT = (DataTable)Session["RecTblPerformanceResults"];

                    objEA.PAavg_attendance = Convert.ToDouble(txtAverage.Text);
                    objEA.PAper_multiplying_factor = Convert.ToString(txtFactor.Text);
                    objEA.PAmax_Weight = Convert.ToInt32(txtWeight.Text);
                    objEA.PAapi_score_claim = Convert.ToInt32(txtScore.Text);
                    if (txtapiperverify.Text == "")
                    {
                        objEA.PAapi_score_Verify = 0;
                    }
                    else
                    {
                        objEA.PAapi_score_Verify = Convert.ToInt32(txtapiperverify.Text);

                    }
                    
                    objEA.PRavg_attendance = Convert.ToDouble(txtAvg.Text);
                    objEA.PRper_multiplying_factor = txtMulFactor.Text;
                    objEA.PRmax_Weight = Convert.ToInt32(txtMaxWeight.Text);
                    objEA.PRapi_score_claim = Convert.ToInt32(txtClaimed.Text);
                    if (txtVerify.Text == "")
                    {
                        objEA.PRapi_score_verify = 0;
                    }
                    else
                    {
                        objEA.PRapi_score_verify = Convert.ToInt32(txtVerify.Text);

                    }
                    


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePerformanceDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePerformanceDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(View_PersonalInfo);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPerformanceSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnInnovativeSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {

                if (Convert.ToInt32(txtApiScoretotal.Text) > 20)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Innovative teaching learning methods api score claime should not be greater than 20.');", true);
                    return;
                }

                if (Convert.ToInt32(txtApi.Text) > 20) 

                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Students Feedback api score claime should not be greater than 20.');", true);
                    return;
                }

                if (Convert.ToInt32(txtAi.Text) > 20) 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Examination Related Work api score claime should not be greater than 20.');", true);
                    return;
                }


                if (Convert.ToInt32(ViewState["SRNO"]) != 1 && Convert.ToInt32(ViewState["SRNO"]) != 2)
                {
                    int sum1 = 0;
                    int sum2 = 0;
                    int sum3 = 0;
                    int sum4 = 0;
                    int SUM5 = 0;
                    int SUM6 = 0;
                    int SUM7 = 0;
                    int SUM8 = 0;
                    int categoryI = 0;

                    sum1 = Convert.ToInt32(txtapic.Text);
                    sum2 = Convert.ToInt32(txtScore.Text);
                    sum3 = Convert.ToInt32(txtClaimed.Text);
                    sum4 = Convert.ToInt32(txtapi1.Text);
                    SUM5 = Convert.ToInt32(txtMaterialApiScore.Text);
                    SUM6 = Convert.ToInt32(txtApiScoretotal.Text);
                    SUM7 = Convert.ToInt32(txtApi.Text);
                    SUM8 = Convert.ToInt32(txtAi.Text);

                    categoryI = sum1 + sum2 + sum3 + sum4 + SUM5 + SUM6 + SUM7 + SUM8;


                    txtcategorytot.Text = Convert.ToString(categoryI);
                }
                else
                {
                    int sum11 = 0;
                    int sum22 = 0;
                    int sum33 = 0;
                    int sum44 = 0;
                    int SUM55 = 0;
                    int SUM66 = 0;
                    int SUM77 = 0;
                    int SUM88 = 0;
                    int categoryII = 0;

                    sum11 = Convert.ToInt32(txtapiv.Text);
                    sum22 = Convert.ToInt32(txtapiperverify.Text);
                    sum33 = Convert.ToInt32(txtVerify.Text);
                    sum44 = Convert.ToInt32(txtUGCVerifiedapi.Text);
                    SUM55 = Convert.ToInt32(txtMaterialVerifiedApi.Text);
                    SUM66 = Convert.ToInt32(txtVerifiedApiS.Text);
                    SUM77 = Convert.ToInt32(txtVerified.Text);
                    SUM88 = Convert.ToInt32(txtApiVerify.Text);

                    categoryII = sum11 + sum22 + sum22 + sum44 + SUM55 + SUM66 + SUM77 + SUM88;
                    txtveryf.Text = Convert.ToString(categoryII);
                }

                if (txtveryf.Text == "")
                {
                    objEA.CATEGORY_II_TOTAL = 0;
                }
                else
                {
                    objEA.CATEGORY_II_TOTAL = Convert.ToInt32(txtveryf.Text);
               
                }
                
                objEA.CATEGORY_I_TOTAL =  Convert.ToInt32(txtcategorytot.Text);


                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {
                        
                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                    #region
                    // First Add All Column Names.
                    Session["dtRate"] = null;
                    DataTable dtRate = new DataTable();
                    if (Session["dtRate"] != null)
                        dtRate = (DataTable)Session["dtRate"];
                    if (!(dtRate.Columns.Contains("SRNO")))
                        dtRate.Columns.Add("SRNO");
                    if (!(dtRate.Columns.Contains("Study_Material")))
                        dtRate.Columns.Add("Study_Material");
                    if (!(dtRate.Columns.Contains("API_Score_Claimed")))
                        dtRate.Columns.Add("API_Score_Claimed");
                    if (!(dtRate.Columns.Contains("Verified_API_Score")))
                        dtRate.Columns.Add("Verified_API_Score");

                    //find column
                    foreach (ListViewItem itemRow in lvInnovative.Items)
                    {
                        Label lblSRNO = (Label)itemRow.FindControl("lblSRNO") as Label;
                        Label lblMaterial = (Label)itemRow.FindControl("lblMaterial") as Label;
                        TextBox txtAPIScore = (TextBox)itemRow.FindControl("txtAPIScore") as TextBox;
                        TextBox txtAPIVerified = (TextBox)itemRow.FindControl("txtAPIVerified") as TextBox;

                        DataRow drRate = dtRate.NewRow();
                        drRate["SRNO"] = Convert.ToInt32(lblSRNO.Text);
                        drRate["Study_Material"] = (lblMaterial.Text);


                        if (txtAPIScore.Text == "")
                        {
                            drRate["API_Score_Claimed"] = 0;
                        }
                        else
                        {
                            drRate["API_Score_Claimed"] = Convert.ToInt32(txtAPIScore.Text);
                        }
                        if (txtAPIVerified.Text == "")
                        {
                            drRate["Verified_API_Score"] = 0;
                        }
                        else
                        {
                            drRate["Verified_API_Score"] = Convert.ToInt32(txtAPIVerified.Text);
                        }
                        //insert row in datatable
                        dtRate.Rows.Add(drRate);

                    }
                    #endregion


                    #region
                    Session["dtStudFB"] = null;
                    DataTable dtStudFB = new DataTable();
                    if (Session["dtStudFB"] != null)
                        dtStudFB = (DataTable)Session["dtStudFB"];
                    if (!(dtStudFB.Columns.Contains("SRNO")))
                        dtStudFB.Columns.Add("SRNO");
                    if (!(dtStudFB.Columns.Contains("Class")))
                        dtStudFB.Columns.Add("Class");
                    if (!(dtStudFB.Columns.Contains("No_Of_Stud_Involve_Feedback")))
                        dtStudFB.Columns.Add("No_Of_Stud_Involve_Feedback");
                    if (!(dtStudFB.Columns.Contains("Feedback_Frequency_Per_Course")))
                        dtStudFB.Columns.Add("Feedback_Frequency_Per_Course");
                    if (!(dtStudFB.Columns.Contains("Methodology")))
                        dtStudFB.Columns.Add("Methodology");
                    if (!(dtStudFB.Columns.Contains("Api_Score_Claime")))
                        dtStudFB.Columns.Add("Api_Score_Claime");
                    if (!(dtStudFB.Columns.Contains("Api_Score_Verified")))
                        dtStudFB.Columns.Add("Api_Score_Verified");

                    //find column
                    foreach (ListViewItem itemRow in lvFeedback.Items)
                    {
                        Label lblSrNo = (Label)itemRow.FindControl("lblSrNo") as Label;
                        TextBox txtClass = (TextBox)itemRow.FindControl("txtClass") as TextBox;
                        TextBox txtStudInvolved = (TextBox)itemRow.FindControl("txtStudInvolved") as TextBox;
                        TextBox txtFeedback = (TextBox)itemRow.FindControl("txtFeedback") as TextBox;
                        Label lblMethodology = (Label)itemRow.FindControl("lblMethodology") as Label;
                        TextBox txtAPIScores = (TextBox)itemRow.FindControl("txtAPIScores") as TextBox;
                        TextBox txtAPIVerified = (TextBox)itemRow.FindControl("txtAPIVerified") as TextBox;

                        DataRow drStudFB = dtStudFB.NewRow();

                        drStudFB["SRNO"] = Convert.ToInt32(lblSrNo.Text);
                        drStudFB["Class"] = (txtClass.Text);
                        drStudFB["No_Of_Stud_Involve_Feedback"] = (txtStudInvolved.Text);
                        drStudFB["Feedback_Frequency_Per_Course"] = (txtFeedback.Text);
                        drStudFB["Methodology"] = lblMethodology.Text;

                        if (txtAPIScores.Text == "")
                        {
                            drStudFB["Api_Score_Claime"] = 0;
                        }
                        else
                        {

                            drStudFB["Api_Score_Claime"] = Convert.ToInt32(txtAPIScores.Text);
                        }

                        if (txtAPIVerified.Text == "")
                        {
                            drStudFB["Api_Score_Verified"] = 0;
                        }
                        else
                        {
                            drStudFB["Api_Score_Verified"] = Convert.ToInt32(txtAPIVerified.Text);

                        }

                        //insert row in datatable
                        dtStudFB.Rows.Add(drStudFB);







                    }
                    #endregion



                    #region

                    Session["dtExamWork"] = null;
                    DataTable dtExamWork = new DataTable();
                    if (Session["dtExamWork"] != null)
                        dtExamWork = (DataTable)Session["dtExamWork"];
                    if (!(dtExamWork.Columns.Contains("SRNO")))
                        dtExamWork.Columns.Add("SRNO");
                    if (!(dtExamWork.Columns.Contains("TYPE_OF_EXAMINATION_WORK")))
                        dtExamWork.Columns.Add("TYPE_OF_EXAMINATION_WORK");
                    if (!(dtExamWork.Columns.Contains("Api_Score_Claime")))
                        dtExamWork.Columns.Add("Api_Score_Claime");
                    if (!(dtExamWork.Columns.Contains("Api_Score_Verified")))
                        dtExamWork.Columns.Add("Api_Score_Verified");


                    foreach (ListViewItem itemRow in lvExamination.Items)
                    {
                        Label lblSrNo = (Label)itemRow.FindControl("lblSrNo") as Label;
                        Label lblWork = (Label)itemRow.FindControl("lblWork") as Label;
                        TextBox txtAPIScores = (TextBox)itemRow.FindControl("txtAPIScores") as TextBox;
                        TextBox txtAPIVerified = (TextBox)itemRow.FindControl("txtAPIVerified") as TextBox;

                        DataRow drExamWork = dtExamWork.NewRow();

                        drExamWork["SRNO"] = Convert.ToInt32(lblSrNo.Text);
                        drExamWork["TYPE_OF_EXAMINATION_WORK"] = lblWork.Text;


                        if (txtAPIScores.Text == "")
                        {
                            drExamWork["Api_Score_Claime"] = 0;
                        }
                        else
                        {
                            drExamWork["Api_Score_Claime"] = Convert.ToInt32(txtAPIScores.Text);
                        }

                        
                        if (txtAPIVerified.Text == "")
                        {
                            drExamWork["Api_Score_Verified"] = 0;
                        }
                        else
                        {
                            drExamWork["Api_Score_Verified"] = Convert.ToInt32(txtAPIVerified.Text);

                        }
                        //insert row in datatable
                        dtExamWork.Rows.Add(drExamWork);
                    }
                    #endregion


                    objEA.INNOVATIVETEACHING = dtRate;
                    objEA.STUDENT_FEEDBACK = dtStudFB;
                    objEA.EXAMINATION_WORK = dtExamWork;
                    objEA.INNOVATIVE_API_SCORE_CLAIME = Convert.ToInt32(txtApiScoretotal.Text);
                    if (txtVerifiedApiS.Text == "")
                    {
                        objEA.INNOVATIVE_API_SCORE_VERIFY = 0;
                    }
                    else
                    {
                        objEA.INNOVATIVE_API_SCORE_VERIFY = Convert.ToInt32(txtVerifiedApiS.Text);

                    }
                    objEA.STUDENT_FEEDBACK_API_SCORE_CLAIME = Convert.ToInt32(txtApi.Text);

                    if (txtVerified.Text == "")
                    {
                        objEA.STUDENT_FEEDBACK_API_SCORE_VERIFY = 0;

                    }
                    else
                    {
                        objEA.STUDENT_FEEDBACK_API_SCORE_VERIFY = Convert.ToInt32(txtVerified.Text);

                    }
                    objEA.EXAMINATION_API_SCORE_CLAIME = Convert.ToInt32(txtAi.Text);

                    if (txtApiVerify.Text == "")
                    {
                        objEA.EXAMINATION_API_SCORE_VERIFY = 0;
                    }
                    else
                    {
                        objEA.EXAMINATION_API_SCORE_VERIFY = Convert.ToInt32(txtApiVerify.Text);

                    }

                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateInnovationDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateInnovationDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }


                   
           


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(Innovative_Teaching);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnInnovativeSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnTeachingActivitySubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                ViewState["APPRAISAL_EMPLOYEE_ID"] = ApprcEmp_Id;
                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                    DataTable TEACHINGACTIVITY = new DataTable("dtEstb");
                    TEACHINGACTIVITY.Columns.Add(new DataColumn("SN_ID", typeof(int)));
                    TEACHINGACTIVITY.Columns.Add(new DataColumn("SEMESTER", typeof(string)));
                    TEACHINGACTIVITY.Columns.Add(new DataColumn("SEM1", typeof(string)));
                    TEACHINGACTIVITY.Columns.Add(new DataColumn("SEM2", typeof(string)));



                    DataRow dr = null;
                    foreach (ListViewItem i in lvTeachActivity.Items)
                    {

                        Label lbSnId = (Label)i.FindControl("lblSRNO");
                        Label lbsemsester = (Label)i.FindControl("lblSemester");
                        TextBox txtSem1 = (TextBox)i.FindControl("txtSem1");
                        TextBox txtsem2 = (TextBox)i.FindControl("txtSem2");


                        dr = TEACHINGACTIVITY.NewRow();
                        dr["SN_ID"] = lbSnId.Text;
                        dr["SEMESTER"] = lbsemsester.Text;
                        dr["SEM1"] = txtSem1.Text;
                        dr["SEM2"] = txtsem2.Text;


                        TEACHINGACTIVITY.Rows.Add(dr);

                    }
                    objEA.TEACHINGACTIVITY = TEACHINGACTIVITY;


                    objEA.SEM1_TOTAL = Convert.ToDouble(txtsemI.Text);
                    objEA.SEM2_TOTAL = Convert.ToDouble(txtsemII.Text);
                    //DataTable dtTP = (DataTable)Session["TblTeachingActivity"];
                    //objEA.TEACHINGACTIVITY = dtTP;


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateTeachingActivityDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateTeachingActivityDetails(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(Teacher_Learning_Activities);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnTeachingActivitySubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnEngagingLecturesSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                if (txtmaxw.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Max Weight.');", true);
                    return;
                }

                if (txtapic.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter API Score Claimed.');", true);
                    return;
                }

                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);


                    DataTable ENGAGINGLECTURE = new DataTable("dtEstb");
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("SN_ID", typeof(int)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("CLASS", typeof(string)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("SUBJECT_TAUGHT", typeof(string)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("HRS_TARGETED", typeof(string)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("HRS_ENGAGED", typeof(string)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("PERCENT_TARGET_ACHIEVED", typeof(string)));
                    ENGAGINGLECTURE.Columns.Add(new DataColumn("Exra_HRS", typeof(string)));



                    DataRow dr = null;
                    foreach (ListViewItem i in lvEngagingLectures.Items)
                    {

                        Label lbSnId = (Label)i.FindControl("lblSRNO");
                        TextBox txtclass = (TextBox)i.FindControl("txtCourse");
                        Label lblSubTaught = (Label)i.FindControl("lblSubject");
                        TextBox txtHrsTargeted = (TextBox)i.FindControl("txtHrsTargeted");
                        TextBox txtHrsEngaged = (TextBox)i.FindControl("txtHrsEngaged");
                        TextBox txtPercent = (TextBox)i.FindControl("txtPercentTarget");
                        //Label lblPercentTarget = (Label)i.FindControl("lblPercentTarget");


                        dr = ENGAGINGLECTURE.NewRow();
                        dr["SN_ID"] = lbSnId.Text;
                        dr["CLASS"] = txtclass.Text;
                        dr["SUBJECT_TAUGHT"] = lblSubTaught.Text;
                        dr["HRS_TARGETED"] = txtHrsTargeted.Text;
                        dr["HRS_ENGAGED"] = txtHrsEngaged.Text;
                        dr["PERCENT_TARGET_ACHIEVED"] = txtPercent.Text;


                        if (txtHrsTargeted.Text != "" && txtHrsEngaged.Text != "")
                        {
                            if (Convert.ToDecimal(txtHrsEngaged.Text) > Convert.ToDecimal(txtHrsTargeted.Text))
                            {
                                decimal Extra_Hrs = (Convert.ToDecimal(txtHrsEngaged.Text) - Convert.ToDecimal(txtHrsTargeted.Text));
                                dr["Exra_HRS"] = Extra_Hrs;
                            }
                        }
                        else
                        {
                            dr["Exra_HRS"] = 0;
                        }
                        ENGAGINGLECTURE.Rows.Add(dr);

                        

                    }
                    objEA.ENGAGINGLECTURE = ENGAGINGLECTURE;

                    objEA.AverageL = Convert.ToDouble(txtavg1.Text);

                    objEA.MulFact = txtlbl.Text;
                    objEA.MaxWL = Convert.ToInt32(txtmaxw.Text);



                    objEA.apiCL = Convert.ToInt32(txtapic.Text);

                    if (txtapiv.Text == "")
                    {
                        objEA.apiVl = 0;
                    }
                    else
                    {
                        objEA.apiVl =  Convert.ToInt32(txtapiv.Text);
                    }
                    //DataTable dtTP = (DataTable)Session["TblTeachingActivity"];
                    //objEA.TEACHINGACTIVITY = dtTP;


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePerformanceInEngagingLectures(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                            GetEmpAppraisalDetails(objEA.EMPLOYEE_ID);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePerformanceInEngagingLectures(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                            GetEmpAppraisalDetails(objEA.EMPLOYEE_ID);
                        }
                    }

                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(Performance_In_Engaging_Lectures);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEngagingLecturesSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnFieldBasesActivitySubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {



                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                    // 12-04-2022

                    //DataTable dtTP = (DataTable)Session["RecTblInnovative"];
                    //objEA.CU_CURRICULAR_ACTIVITY = dtTP;

                    // Commented on 13-04-2022

                    //objEA.Study_Material = ddlMaterial.SelectedItem.Text;

                    //objEA.API_Score_Claimed = txtApi.Text.Trim() == string.Empty ? string.Empty : txtApi.Text.Trim();

                    //objEA.Verified_API_Score = txtVerified.Text.Trim() == string.Empty ? string.Empty : txtVerified.Text.Trim();


                    #region
                    // First Add All Column Names.
                    Session["dtStuCoCurricular"] = null;
                    DataTable dtStuCoCurricular = new DataTable();
                    if (Session["dtStuCoCurricular"] != null)
                        dtStuCoCurricular = (DataTable)Session["dtStuCoCurricular"];
                    if (!(dtStuCoCurricular.Columns.Contains("SRNO")))
                        dtStuCoCurricular.Columns.Add("SRNO");
                    if (!(dtStuCoCurricular.Columns.Contains("NAME_OF_ACTIVITY")))
                        dtStuCoCurricular.Columns.Add("NAME_OF_ACTIVITY");
                    if (!(dtStuCoCurricular.Columns.Contains("API_SCORE_ALLOTD")))
                        dtStuCoCurricular.Columns.Add("API_SCORE_ALLOTED");
                    if (!(dtStuCoCurricular.Columns.Contains("API_SCORE_CLAIMED")))
                        dtStuCoCurricular.Columns.Add("API_SCORE_CLAIMED");
                    if (!(dtStuCoCurricular.Columns.Contains("VERIFIED_API_SCORE")))
                        dtStuCoCurricular.Columns.Add("VERIFIED_API_SCORE");


                    //find column
                    foreach (ListViewItem itemRow in lvCurricular.Items)
                    {
                        Label lblSRNO = (Label)itemRow.FindControl("lblSRNO") as Label;
                        Label lblNActivity = (Label)itemRow.FindControl("lblNActivity") as Label;
                        Label lblAPIAlloted = (Label)itemRow.FindControl("lblAPIAlloted") as Label;
                        TextBox txtAPIS = (TextBox)itemRow.FindControl("txtAPIS") as TextBox;
                        TextBox txtVerifyAPI = (TextBox)itemRow.FindControl("txtVerifyAPI") as TextBox;

                        DataRow drcoCurricular = dtStuCoCurricular.NewRow();
                        drcoCurricular["SRNO"] = Convert.ToInt32(lblSRNO.Text);
                        drcoCurricular["NAME_OF_ACTIVITY"] = (lblNActivity.Text);
                        drcoCurricular["API_SCORE_ALLOTED"] = Convert.ToInt32(lblAPIAlloted.Text);

                        if (txtAPIS.Text == "")
                        {
                            drcoCurricular["API_SCORE_CLAIMED"] = 0;
                        }
                        else
                        {
                            drcoCurricular["API_SCORE_CLAIMED"] = Convert.ToInt32(txtAPIS.Text);

                        }
                        
                        if (txtVerifyAPI.Text == "")
                        {
                            drcoCurricular["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            drcoCurricular["VERIFIED_API_SCORE"] = Convert.ToInt32(txtVerifyAPI.Text);
                        }
                        //insert row in datatable
                        dtStuCoCurricular.Rows.Add(drcoCurricular);

                    }
                    #endregion





                    objEA.CU_CURRICULAR_ACTIVITY = dtStuCoCurricular;


                    if (txtclaim.Text == "")
                    {
                        objEA.STUDENT_SCORE_CLAIME = 0;
                    }
                    else
                    {
                        objEA.STUDENT_SCORE_CLAIME = Convert.ToInt32(txtclaim.Text);
                    }

                    if (txtVery.Text == "")
                    {
                        objEA.STUDENT_SCORE_VERIFY = 0;


                    }
                    else
                    {
                        objEA.STUDENT_SCORE_VERIFY = Convert.ToInt32(txtVery.Text);
                    }
                    objEA.Community_activity = txtActivity.Text;
                    objEA.Community_SCORE_ALLOTED = Convert.ToInt32(txtAPIAlloted.Text);
                    objEA.Community_SCORE_CLAIME = Convert.ToInt32(txtAPIScoreClaimed.Text);

                    if (txtVerifiedScore.Text == "")
                    {
                        objEA.Community_SCORE_VERIFY = 0;
                    }
                    else
                    {
                        objEA.Community_SCORE_VERIFY = Convert.ToInt32(txtVerifiedScore.Text);
                    }







                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateFieldBasedActivities(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateFieldBasedActivities(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(Field_Based_Activity);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnFieldBasesActivitySubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }

    protected void btnAdministrativeAcademicSubmit_Click(object sender, EventArgs e)
    {
        {
            try
            {
                if (Convert.ToInt32(ViewState["SRNO"]) != 1 && Convert.ToInt32(ViewState["SRNO"]) != 2)
                {
                    int Csum1 = 0;
                    int Csum2 = 0;
                    int Csum3 = 0;
                    int Csum4 = 0;

                    int CATEGOTYII = 0;

                    Csum1 = Convert.ToInt32(txtclaim.Text);
                    Csum2 = Convert.ToInt32(txtAPIScoreClaimed.Text);
                    Csum3 = Convert.ToInt32(txtAcademicApi.Text);
                    Csum4 = Convert.ToInt32(txtDevelopmentApi.Text);

                    CATEGOTYII = Csum1 + Csum2 + Csum3 + Csum4;
                    txtcatII.Text = Convert.ToString(CATEGOTYII);
                }
                else
                {
                    int Csum11 = 0;
                    int Csum22 = 0;
                    int Csum33 = 0;
                    int Csum44 = 0;

                    int CATEGOTYII = 0;

                    Csum11 = Convert.ToInt32(txtVerifiedScore.Text);
                    Csum22 = Convert.ToInt32(txtVery.Text);
                    Csum33 = Convert.ToInt32(txtDevelopmentVerify.Text);
                    Csum44 = Convert.ToInt32(txtAcademicVerifiedApi.Text);

                    CATEGOTYII = Csum11 + Csum22 + Csum33 + Csum44;
                    TXTCATIIV.Text = Convert.ToString(CATEGOTYII);
                    
                }

                int TOT1 =  Convert.ToInt32(txtcatII.Text);
                int TOT2 = Convert.ToInt32(txtcategorytot.Text);
                txttot1.Text = Convert.ToString(TOT1 + TOT2);

                int TOT3 = 0;
                int TOT4 = 0;

                if (TXTCATIIV.Text == "")
                {
                     TOT3 = 0;
                }
                else
                {
                     TOT3 = Convert.ToInt32(TXTCATIIV.Text);
                }


                if (txtVery.Text == "")
                {
                    TOT3 = 0;
                }
                else
                {
                    TOT4 = Convert.ToInt32(txtVery.Text);
                }

                txttot2.Text = Convert.ToString(TOT3 + TOT4);

                objEA.TOTAL1 =  Convert.ToInt32(txttot1.Text);

                if (txttot2.Text == "")
                {
                    objEA.TOTAL2 = 0;
                }
                else
                {
                    objEA.TOTAL2 = Convert.ToInt32(txttot2.Text);
                }
               

               objEA.CAT_I_TOTAL = Convert.ToInt32(txtcatII.Text);
               if (TXTCATIIV.Text == "")
                {
                    objEA.CAT_II_TOTAL = 0;
                }
                else
                {
                    objEA.CAT_II_TOTAL = Convert.ToInt32(TXTCATIIV.Text);
                }


                string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (ApprcEmp_Id != "")
                {

                    objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                    objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                    //DataTable dtTP = (DataTable)Session["TblAdministrativeAcad"];
                    //objEA.ADMINACADEMIC = dtTP;
                    //DataTable dtTE = (DataTable)Session["TblProDevelopment"];
                    //objEA.PROFESSIONALDEVELOP = dtTE;


                    #region
                    // First Add All Column Names.
                    Session["dtAdmAcd"] = null;
                    DataTable dtAdmAcd = new DataTable();
                    if (Session["dtAdmAcd"] != null)
                        dtAdmAcd = (DataTable)Session["dtStuCoCurricular"];
                    if (!(dtAdmAcd.Columns.Contains("SN_ID")))
                        dtAdmAcd.Columns.Add("SN_ID");
                    if (!(dtAdmAcd.Columns.Contains("NAME_OF_ACTIVITY")))
                        dtAdmAcd.Columns.Add("NAME_OF_ACTIVITY");
                    if (!(dtAdmAcd.Columns.Contains("API_SCORE_ALLOTTED")))
                        dtAdmAcd.Columns.Add("API_SCORE_ALLOTTED");
                    if (!(dtAdmAcd.Columns.Contains("API_SCORE_CLAIMED")))
                        dtAdmAcd.Columns.Add("API_SCORE_CLAIMED");
                    if (!(dtAdmAcd.Columns.Contains("VERIFIED_API_SCORE")))
                        dtAdmAcd.Columns.Add("VERIFIED_API_SCORE");


                    //find column
                    foreach (ListViewItem itemRow in lvAcademic.Items)
                    {
                        Label lblSN = (Label)itemRow.FindControl("lblSN") as Label;
                        Label lblNameActivity = (Label)itemRow.FindControl("lblNameActivity") as Label;
                        Label lblApiAlloted = (Label)itemRow.FindControl("lblApiAlloted") as Label;
                        TextBox txtAcadDevelopApi = (TextBox)itemRow.FindControl("txtAcadDevelopApi") as TextBox;
                        TextBox txtAcadDevelopVerify = (TextBox)itemRow.FindControl("txtAcadDevelopVerify") as TextBox;

                        DataRow drAdmAcd = dtAdmAcd.NewRow();
                        drAdmAcd["SN_ID"] = Convert.ToInt32(lblSN.Text);
                        drAdmAcd["NAME_OF_ACTIVITY"] = (lblNameActivity.Text);
                        drAdmAcd["API_SCORE_ALLOTTED"] = Convert.ToInt32(lblApiAlloted.Text);

                        if (txtAcadDevelopApi.Text == "")
                        {
                            drAdmAcd["API_SCORE_CLAIMED"] = 0;
                        }
                        else
                        {
                            drAdmAcd["API_SCORE_CLAIMED"] = Convert.ToInt32(txtAcadDevelopApi.Text);
                        }
                        
                        if (txtAcadDevelopVerify.Text == "")
                        {
                            drAdmAcd["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            drAdmAcd["VERIFIED_API_SCORE"] = Convert.ToInt32(txtAcadDevelopVerify.Text);
                        }
                        //insert row in datatable
                        dtAdmAcd.Rows.Add(drAdmAcd);

                    }
                    objEA.ADMINACADEMIC = dtAdmAcd;
                    #endregion

                    #region
                    // First Add All Column Names.
                    Session["dtprodevactivity"] = null;
                    DataTable dtprodevactivity = new DataTable();
                    if (Session["dtprodevactivity"] != null)
                        dtprodevactivity = (DataTable)Session["dtprodevactivity"];
                    if (!(dtprodevactivity.Columns.Contains("SN_ID")))
                        dtprodevactivity.Columns.Add("SN_ID");
                    if (!(dtprodevactivity.Columns.Contains("NAME_OF_ACTIVITY")))
                        dtprodevactivity.Columns.Add("NAME_OF_ACTIVITY");
                    if (!(dtprodevactivity.Columns.Contains("API_SCORE_ALLOTTED")))
                        dtprodevactivity.Columns.Add("API_SCORE_ALLOTTED");
                    if (!(dtprodevactivity.Columns.Contains("API_SCORE_CLAIMED")))
                        dtprodevactivity.Columns.Add("API_SCORE_CLAIMED");
                    if (!(dtprodevactivity.Columns.Contains("VERIFIED_API_SCORE")))
                        dtprodevactivity.Columns.Add("VERIFIED_API_SCORE");


                    //find column
                    foreach (ListViewItem itemRow in lvDevelopment.Items)
                    {
                        Label lblSNID = (Label)itemRow.FindControl("lblSNID") as Label;
                        Label lblactivity = (Label)itemRow.FindControl("lblactivity") as Label;
                        TextBox txtApiAlloted = (TextBox)itemRow.FindControl("txtApiAlloted") as TextBox;
                        TextBox txtDevelopApiScore = (TextBox)itemRow.FindControl("txtDevelopApiScore") as TextBox;
                        TextBox txtDevelopVerifiedApi = (TextBox)itemRow.FindControl("txtDevelopVerifiedApi") as TextBox;

                        DataRow drprodevactivity = dtprodevactivity.NewRow();
                        drprodevactivity["SN_ID"] = Convert.ToInt32(lblSNID.Text);
                        drprodevactivity["NAME_OF_ACTIVITY"] = (lblactivity.Text);

                        if (txtApiAlloted.Text == "")
                        {
                            drprodevactivity["API_SCORE_ALLOTTED"] = 0;
                        }
                        else
                        {
                            drprodevactivity["API_SCORE_ALLOTTED"] = Convert.ToInt32(txtApiAlloted.Text);
                        }
                       
                        if (txtDevelopApiScore.Text == "")
                        {
                            drprodevactivity["API_SCORE_CLAIMED"] = 0;
                        }
                        else
                        {
                            drprodevactivity["API_SCORE_CLAIMED"] = Convert.ToInt32(txtDevelopApiScore.Text);

                        }
                        
                        
                        if (txtDevelopVerifiedApi.Text == "")
                        {

                            drprodevactivity["VERIFIED_API_SCORE"] = 0;
                        }
                        else
                        {
                            drprodevactivity["VERIFIED_API_SCORE"] = Convert.ToInt32(txtDevelopVerifiedApi.Text);

                        }
                        //insert row in datatable
                        dtprodevactivity.Rows.Add(drprodevactivity);

                    }
                    // objEA.ADMINACADEMIC = dtAdmAcd;
                    objEA.PROFESSIONALDEVELOP = dtprodevactivity;

                    #endregion




                    #region
                    Session["dtotheractivity"] = null;
                    DataTable dtotheractivity = new DataTable();
                    if (Session["dtotheractivity"] != null)
                        dtotheractivity = (DataTable)Session["dtotheractivity"];
                    if (!(dtotheractivity.Columns.Contains("SN_ID")))
                        dtotheractivity.Columns.Add("SN_ID");
                    if (!(dtotheractivity.Columns.Contains("NameOfActivity")))
                        dtotheractivity.Columns.Add("NameOfActivity");
                    if (!(dtotheractivity.Columns.Contains("ApiScoreAllotted")))
                        dtotheractivity.Columns.Add("ApiScoreAllotted");
                    if (!(dtotheractivity.Columns.Contains("ApiScoreClaime")))
                        dtotheractivity.Columns.Add("ApiScoreClaime");
                    if (!(dtotheractivity.Columns.Contains("SR_NO")))
                        dtotheractivity.Columns.Add("SR_NO");
                    if (!(dtotheractivity.Columns.Contains("VERIFIED_API_SCORE")))
                        dtotheractivity.Columns.Add("VERIFIED_API_SCORE");


                    foreach (ListViewItem itemRow in lvotheractivity.Items)
                    {
                        Label lblnameofactivity = (Label)itemRow.FindControl("lblnameofactivity") as Label;
                        Label lblapiscoreallot = (Label)itemRow.FindControl("lblapiscoreallot") as Label;
                        TextBox txtapiscore = (TextBox)itemRow.FindControl("txtapiscore") as TextBox;
                        TextBox txtapiver = (TextBox)itemRow.FindControl("txtapiver") as TextBox;
                        HiddenField hdnsnid =(HiddenField)itemRow.FindControl("hdnsnid") as HiddenField;


                        DataRow drotheractivity = dtotheractivity.NewRow();
                        drotheractivity["SN_ID"] = hdnsnid.Value;
                        drotheractivity["NameOfActivity"] = (lblnameofactivity.Text);
                        drotheractivity["ApiScoreAllotted"] = (lblapiscoreallot.Text);
                        drotheractivity["SR_NO"] = 0;

                        if (txtapiscore.Text == "")
                        {
                            drotheractivity["ApiScoreClaime"] = 0;
                        }
                        else
                        {
                            drotheractivity["ApiScoreClaime"] = (txtapiscore.Text);
                       
                        }
                        if (txtapiver.Text == "")
                        {
                            drotheractivity["VERIFIED_API_SCORE"] = 0;

                        }
                        else
                        {
                            drotheractivity["VERIFIED_API_SCORE"] = (txtapiver.Text);
                        }


                       // dtstudymaterial.Rows.Add(drdtstudymaterial);
                        dtotheractivity.Rows.Add(drotheractivity);
                        

                    
                    }


                    objEA.OtherActivity = dtotheractivity;

                    #endregion

                    objEA.ADMINISTRATIVE_SCORE_CLAIME = Convert.ToInt32(txtAcademicApi.Text);
                    if (txtAcademicVerifiedApi.Text == "")
                    {
                        objEA.ADMINISTRATIVE_SCORE_VERIFY = 0;
                    }
                    else
                    {
                        objEA.ADMINISTRATIVE_SCORE_VERIFY = Convert.ToInt32(txtAcademicVerifiedApi.Text);

                    }

                    objEA.PROFESSIONAL_SCORE_CLAIME = Convert.ToInt32(txtDevelopmentApi.Text);

                    if (txtDevelopmentVerify.Text == "")
                    {
                        objEA.PROFESSIONAL_SCORE_VERIFY = 0;
                    }
                    else
                    {
                        objEA.PROFESSIONAL_SCORE_VERIFY = Convert.ToInt32(txtDevelopmentVerify.Text);
                    }


                    if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                    {
                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateAdministrativeAcademic(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }
                    else
                    {
                        objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                        CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateAdministrativeAcademic(objEA);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        }
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                    MultiView_Profile.ActiveViewIndex = -1;
                    MultiView_Profile.SetActiveView(View_PersonalInfo);
                    trSession.Visible = true;

                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPerformanceSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }


    #endregion

    //btnEngagingLecturesSubmit_Click


    //protected void btnPatentDetailsSubmit_Click(object sender, EventArgs e)
    //{
    //    {
    //        try
    //        {
    //            string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

    //            if (ApprcEmp_Id != "")
    //            {

    //                objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);

    //                DataTable dtTP = (DataTable)Session["TblPatentIPR"];
    //                objEA.PATENTDETAILS = dtTP;


    //                if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
    //                {
    //                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePatentDetails(objEA);
    //                    if (cs.Equals(CustomStatus.RecordSaved))
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
    //                    }
    //                }
    //                else
    //                {
    //                    objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

    //                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdatePublicationDetails(objEA);
    //                    if (cs.Equals(CustomStatus.RecordSaved))
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
    //                    }
    //                }

    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
    //                MultiView_Profile.ActiveViewIndex = -1;
    //                MultiView_Profile.SetActiveView(View_PersonalInfo);
    //                trSession.Visible = true;

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (Convert.ToBoolean(Session["error"]) == true)
    //                objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPublishedDetailsSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
    //            else
    //                objUCommon.ShowError(Page, "Server UnAvailable");
    //        }
    //    }       

    //}

    #region BindListView Teaching Learning Activities

    //private void BindListData()
    //{
    //    DataSet ds = null;
    //    ds = objAPPRController.GetTeachingActivity(Convert.ToInt32(ddlEmployee.SelectedValue));

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Session["TblTeachingActivity"] = ds.Tables[0];
    //        lvTeachActivity.DataSource = ds;
    //        lvTeachActivity.DataBind();
    //    }
    //    else
    //    {
    //        Session["TblTeachingActivity"] = null;
    //        lvTeachActivity.Visible = false;
    //        lvTeachActivity.DataSource = null;
    //        lvTeachActivity.DataBind();
    //    }
    //}

    #endregion

    #region BindListView PublicationDetails

    private void BindlistData()
    {
        DataSet ds = null;
        ds = objAPPRController.GetJournalPublication(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblPublishedJournal"] = ds.Tables[0];
            lvpublicationJournal.DataSource = ds;
            lvpublicationJournal.DataBind();
            hdnPublication.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Session["TblPublishedJournal"] = null;
            lvpublicationJournal.Visible = false;
            lvpublicationJournal.DataSource = null;
            lvpublicationJournal.DataBind();
            hdnPublication.Value = ds.Tables[0].Rows.Count.ToString();
        }

        ds = objAPPRController.GetBooksPublication(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblPublishedBooks"] = ds.Tables[0];
            lvPublishedBooks.DataSource = ds;
            lvPublishedBooks.DataBind();
            hdnPublication.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Session["TblPublishedBooks"] = null;
            lvPublishedBooks.Visible = false;
            lvPublishedBooks.DataSource = null;
            lvPublishedBooks.DataBind();
        }

        ds = objAPPRController.GetChaptersPublicationInBook(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblChaptersInBook"] = ds.Tables[0];
            lvPublishedChapter.DataSource = ds;
            lvPublishedChapter.DataBind();
            hdnPublication.Value = ds.Tables[0].Rows.Count.ToString();

        }
        else
        {
            Session["TblChaptersInBook"] = null;
            lvPublishedChapter.Visible = false;
            lvPublishedChapter.DataSource = null;
            lvPublishedChapter.DataBind();
        }

    }

    #endregion

    #region BindListView Conference Proceedings

    private void BindConferencelistData()
    {
        DataSet ds = null;

        ds = objAPPRController.GetConferenceProceedindsDetails(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblConferenceProceeds"] = ds.Tables[0];
            lvConference.DataSource = ds;
            lvConference.DataBind();
            hdnConference.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Session["TblConferenceProceeds"] = null;
            lvConference.Visible = false;
            lvConference.DataSource = null;
            lvConference.DataBind();
        }

        ds = objAPPRController.GetAvishkarDetails(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblAvishkarDetails"] = ds.Tables[0];
            lvAvishkar.DataSource = ds;
            lvAvishkar.DataBind();
            hdnConference.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Session["TblAvishkarDetails"] = null;
            lvAvishkar.Visible = false;
            lvAvishkar.DataSource = null;
            lvAvishkar.DataBind();
        }

        ds = objAPPRController.GetOngoingCompletedConsultancies(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblConsultancies"] = ds.Tables[0];
            lvProjects.DataSource = ds;
            lvProjects.DataBind();
            hdnConference.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            Session["TblConsultancies"] = null;
            lvProjects.Visible = false;
            lvProjects.DataSource = null;
            lvProjects.DataBind();
        }

    }

    //private

    #endregion

    #region BindListView Patent/IPR

    private void BindPatentListView()
    {
        DataSet ds = objAPPRController.GetPatentIPRDetails(Convert.ToInt32(ddlEmployee.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["TblPatentIPR"] = ds.Tables[0];
            lvPatent.DataSource = ds;
            lvPatent.DataBind();
            hdnPatent.Value = ds.Tables[0].Rows.Count.ToString();

        }
        else
        {
            Session["TblPatentIPR"] = null;
            lvPatent.Visible = false;
            lvPatent.DataSource = null;
            lvPatent.DataBind();
        }

    }

    #endregion

    #region  BindListView Lectures and Academic Duties in Excess of UGC Norms

    //private void BindAcademicDutiesListView()
    //{
    //    DataSet ds = objAPPRController.GetInnovativeDetails(Convert.ToInt32(ddlEmployee.SelectedValue));

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Session["RecTblInnovative"] = ds.Tables[0];
    //        lvInnovative.DataSource = ds;
    //        lvInnovative.DataBind();
    //    }
    //    else
    //    {
    //        Session["RecTblInnovative"] = null;
    //        lvInnovative.Visible = false;
    //        lvInnovative.DataSource = null;
    //        lvInnovative.DataBind();
    //    }

    //}

    #endregion

    //#region BindListView Technical WorkShop

    //private void BindTechnicalWorkshopListView()
    //{
    //    DataSet ds= objAPPRController.GetTechnicalWorkshopListView(Convert.ToInt32(ddlEmployee.SelectedValue));

    //        if()
    //}

    //#endregion


    #region Research Guidance

    private DataTable CreateTabelResarchGuidance()
    {
        DataTable dtResarchGuidance = new DataTable();
        //dtResarchGuidance.Columns.Add(new DataColumn("SRNO", typeof(int)));   
        dtResarchGuidance.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtResarchGuidance.Columns.Add(new DataColumn("Research_Guidance", typeof(string)));
        dtResarchGuidance.Columns.Add(new DataColumn("Number_Enrolled", typeof(string)));
        dtResarchGuidance.Columns.Add(new DataColumn("Thesis_Submitted", typeof(string)));
        dtResarchGuidance.Columns.Add(new DataColumn("Degree_Awarded", typeof(string)));
        dtResarchGuidance.Columns.Add(new DataColumn("API_SCORE_CLAIMED", typeof(int)));
        dtResarchGuidance.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(int)));


        return dtResarchGuidance;
    }
    protected void btnResarchGuidance_Click(object sender, EventArgs e)
    {
        try
        {
            lvGuidance.Visible = true;
            if (Session["TblGuidance"] != null && ((DataTable)Session["TblGuidance"]) != null)
            {
                int maxVals = 0;
                DataTable dtResarchGuidance = (DataTable)Session["TblGuidance"];
                DataRow dr = dtResarchGuidance.NewRow();
                //if (dr != null)
                //{
                //    // maxVals = Convert.ToInt32(dtResarchGuidance.AsEnumerable().Max(row => row["SRNO"]));  
                //    maxVals = Convert.ToInt32(dtResarchGuidance.AsEnumerable().Max(row => row["SRNO_Guidance"]));
                //}

                //dr["SRNO"] = maxVals + 1;
                dr["SRNO"] = maxVals + 1;
                dr["Research_Guidance"] = ddlGuidance.SelectedItem.Text;
                dr["Number_Enrolled"] = txtEnrolled.Text.Trim() == null ? string.Empty : Convert.ToString(txtEnrolled.Text.Trim()).Replace(',', ' ');
                dr["Thesis_Submitted"] = ddlThesis.SelectedItem.Text;
                dr["Degree_Awarded"] = ddlDegree.SelectedItem.Text;
                if (TextBox1.Text == "")
                {
                    dr["API_SCORE_CLAIMED"] = 0;
                }
                else
                {
                    dr["API_SCORE_CLAIMED"] = Convert.ToInt32(TextBox1.Text);
                }
                if (TextBox2.Text == "")
                {
                    dr["API_SCORE_CLAIMED"] = 0;
                }
                else
                {
                    dr["VERIFIED_API_SCORE"] = Convert.ToInt32(TextBox2.Text);
                }



                dtResarchGuidance.Rows.Add(dr);
                Session["TblGuidance"] = dtResarchGuidance;
                lvGuidance.DataSource = dtResarchGuidance;
                lvGuidance.DataBind();
                foreach (ListViewDataItem item in lvGuidance.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApiScore = item.FindControl("txtVerifiedApiScore") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvGuidance.FindControl("lblvery").Visible = false;
                        txtVerifiedGuidApi.Visible = false;
                        lblverify14.Visible = false;
                    }

                }
                hdnGuidance.Value = dtResarchGuidance.Rows.Count.ToString();

                ClearRecFromResarchPaper();
                // ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {

                DataTable dtResarchGuidance = this.CreateTabelResarchGuidance();
                DataRow dr = dtResarchGuidance.NewRow();

                //dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;    
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                dr["Research_Guidance"] = ddlGuidance.SelectedItem.Text;
                dr["Number_Enrolled"] = txtEnrolled.Text.Trim() == null ? string.Empty : Convert.ToString(txtEnrolled.Text.Trim()).Replace(',', ' ');
                dr["Thesis_Submitted"] = ddlThesis.SelectedItem.Text;
                dr["Degree_Awarded"] = ddlDegree.SelectedItem.Text;
                if (TextBox1.Text == "")
                {
                    dr["API_SCORE_CLAIMED"] = 0;
                }
                else
                {
                    dr["API_SCORE_CLAIMED"] = Convert.ToInt32(TextBox1.Text);
                }
                if (TextBox2.Text == "")
                {
                    dr["API_SCORE_CLAIMED"] = 0;
                }
                else
                {
                    dr["VERIFIED_API_SCORE"] = Convert.ToInt32(TextBox2.Text);
                }


                ViewState["TblGuidance"] = Convert.ToInt32(ViewState["TblGuidance"]) + 1;
                dtResarchGuidance.Rows.Add(dr);
                ClearRecFromResarchPaper();
                Session["TblGuidance"] = dtResarchGuidance;
                lvGuidance.DataSource = dtResarchGuidance;
                lvGuidance.DataBind();
                foreach (ListViewDataItem item in lvGuidance.Items)
                {
                    TextBox txtApiScore = item.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApiScore = item.FindControl("txtVerifiedApiScore") as TextBox;



                    if (Convert.ToInt32(ViewState["SRNO"]) == 1)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = true;


                    }
                    else if (Convert.ToInt32(ViewState["SRNO"]) == 2)
                    {
                        txtApiScore.Enabled = false;
                        txtVerifiedApiScore.Enabled = false;

                    }

                    else
                    {
                        txtApiScore.Visible = true;
                        txtVerifiedApiScore.Visible = false;


                        // lblapiscore.FindControl("lblapiscore").Visible = false;
                        lvGuidance.FindControl("lblvery").Visible = false;
                        txtVerifiedGuidApi.Visible = false;
                        lblverify14.Visible = false;
                    }

                }
                hdnGuidance.Value = dtResarchGuidance.Rows.Count.ToString();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnResarchGuidance_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearRecFromResarchPaper()
    {

        txtEnrolled.Text = string.Empty;
        //ddlAwarded.SelectedValue = "0";
        ddlDegree.SelectedValue = "0";
        ddlThesis.SelectedValue = "0";
        ddlGuidance.SelectedValue = "0";
        //txtpapertitle.Text = string.Empty;
        //txtDatereviewed.Text = string.Empty;  
        ViewState["SRNO_Guidance"] = null;
        // ViewState["SRNO"] = null;
    }

    protected void btnEditResearchGuidance_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditResarchPaper = sender as ImageButton;
            DataTable dtResarchGuidance;
            if (Session["TblGuidance"] != null && ((DataTable)Session["TblGuidance"]) != null)
            {
                dtResarchGuidance = ((DataTable)Session["TblGuidance"]);

                DataRow dr = this.GetEditableDatarowFromResarchPaper(dtResarchGuidance, btnEditResarchPaper.CommandArgument);


                //txtEnrolled.Text = dr["Number_Enrolled"].ToString();
                //txtpapertitle.Text = dr["Paper_title"].ToString();
                //txtDatereviewed.Text = dr["Date_Reviewed"].ToString();


                dtResarchGuidance.Rows.Remove(dr);
                Session["TblGuidance"] = dtResarchGuidance;
                lvGuidance.DataSource = dtResarchGuidance;
                lvGuidance.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditResearchGuidance_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarowFromResarchPaper(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                //if (dr["SRNO"].ToString() == value) 
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromResarchPaper -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    //protected void btnDeletedRes_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDel = sender as ImageButton;
    //        int SRNO = int.Parse(btnDel.CommandArgument);


    //        int APPR_RESEARCHID = Convert.ToInt32((objCommon.LookUp("APPRAISAL_RESARCH_PAPER_REVIEWED", "APPRAISAL_RESARCH_PAPER_REVIEWED_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "AND APPRAISAL_EMPLOYEE_ID= " + ViewState["APPRAISAL_EMPLOYEE_ID"] + " AND SRNO=" + SRNO)));




    //        int IDNO = Convert.ToInt32(Session["idno"]);
    //        DataTable dtResarchPaper;

    //        CustomStatus cs = (CustomStatus)objAPPRController.DeleteResarchPapered(APPR_RESEARCHID);
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //        {
    //            dtResarchPaper = ((DataTable)Session["RecTblResarchGuidance"]);

    //            DataRow dr = this.GetEditableDatarowFromResarchPaper(dtResarchPaper, btnDel.CommandArgument);
    //            dtResarchPaper.Rows.Remove(dr);
    //            Session["RecTblResarchGuidance"] = dtResarchPaper;
    //            lvGuidance.DataSource = dtResarchPaper;
    //            lvGuidance.DataBind();
    //            ClearRecFromResarchPaper();

    //            MessageBox("Record Deleted Successfully");
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    #endregion


    #region Research Qualification

    private DataTable CreateTabelResarchQualification()
    {
        DataTable dtResarchQualification = new DataTable();
        dtResarchQualification.Columns.Add(new DataColumn("SRNO", typeof(int)));
        // dtResarchQualification.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtResarchQualification.Columns.Add(new DataColumn("Qualification", typeof(string)));
        dtResarchQualification.Columns.Add(new DataColumn("Submitted", typeof(string)));
        dtResarchQualification.Columns.Add(new DataColumn("Awarded", typeof(string)));

        return dtResarchQualification;
    }

    protected void btnResarchQualification_Click(object sender, EventArgs e)
    {
        try
        {
            lvQualification.Visible = true;
            if (Session["TblQualification"] != null && ((DataTable)Session["TblQualification"]) != null)
            {
                int maxVals = 0;
                DataTable dtResarchGuidance = (DataTable)Session["TblQualification"];
                DataRow dr = dtResarchGuidance.NewRow();
                //if (dr != null)
                //{
                //   // maxVals = Convert.ToInt32(dtResarchGuidance.AsEnumerable().Max(row => row["SRNO"]));
                //    maxVals = Convert.ToInt32(dtResarchGuidance.AsEnumerable().Max(row => row["SRNO_Qualification"]));
                //}

                //dr["SRNO"] = maxVals + 1;
                dr["SRNO"] = maxVals + 1;

                dr["Qualification"] = ddlQualification.SelectedItem.Text;
                dr["Submitted"] = ddlSubmitted.SelectedItem.Text;
                dr["Awarded"] = ddlAwarded.SelectedItem.Text;


                dtResarchGuidance.Rows.Add(dr);
                Session["TblQualification"] = dtResarchGuidance;
                lvQualification.DataSource = dtResarchGuidance;
                lvQualification.DataBind();
                ClearRecFromResarchQualificationPaper();
                //ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {

                DataTable dtResarchGuidance = this.CreateTabelResarchQualification();
                DataRow dr = dtResarchGuidance.NewRow();

                //dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                dr["Qualification"] = ddlQualification.SelectedItem.Text;
                dr["Submitted"] = ddlSubmitted.SelectedItem.Text;
                dr["Awarded"] = ddlAwarded.SelectedItem.Text;

                ViewState["TblQualification"] = Convert.ToInt32(ViewState["TblQualification"]) + 1;
                dtResarchGuidance.Rows.Add(dr);
                ClearRecFromResarchQualificationPaper();
                Session["TblQualification"] = dtResarchGuidance;
                lvQualification.DataSource = dtResarchGuidance;
                lvQualification.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnResarchQualification_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearRecFromResarchQualificationPaper()
    {
        ddlQualification.SelectedValue = "0";
        ddlSubmitted.SelectedValue = "0";
        ddlAwarded.SelectedValue = "0";
        // ViewState["SRNO"] = null;   
        ViewState["SRNO_Qualification"] = null;
    }

    protected void btnEditResearchQualification_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditResarchQualifiPaper = sender as ImageButton;
            DataTable dtResarchQualification;
            if (Session["TblQualification"] != null && ((DataTable)Session["TblQualification"]) != null)
            {
                dtResarchQualification = ((DataTable)Session["TblQualification"]);

                DataRow dr = this.GetEditableDatarowFromResarchQualification(dtResarchQualification, btnEditResarchQualifiPaper.CommandArgument);


                //txtEnrolled.Text = dr["Number_Enrolled"].ToString();
                //txtpapertitle.Text = dr["Paper_title"].ToString();
                //txtDatereviewed.Text = dr["Date_Reviewed"].ToString();


                dtResarchQualification.Rows.Remove(dr);
                Session["TblQualification"] = dtResarchQualification;
                lvQualification.DataSource = dtResarchQualification;
                lvQualification.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditResearchQualification_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarowFromResarchQualification(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                //if (dr["SRNO"].ToString() == value)
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromResarchQualification -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    #endregion

    //#region Student Co-Curricular Avtivities

    //private DataTable CreateTabelCurricularActivity()
    //{
    //    DataTable dtActivity = new DataTable();
    //    dtActivity.Columns.Add(new DataColumn("SRNO", typeof(int)));
    //    dtActivity.Columns.Add(new DataColumn("Name_of_Activity", typeof(string)));
    //    dtActivity.Columns.Add(new DataColumn("API_Score_Alloted", typeof(string)));
    //    dtActivity.Columns.Add(new DataColumn("API_Score_Claimed", typeof(string)));
    //    dtActivity.Columns.Add(new DataColumn("Verified_API_Score", typeof(string)));

    //    return dtActivity;
    //}
    //protected void btnActivity_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lvActivity.Visible = true;
    //        if (Session["RecTblCurricularActivity"] != null && ((DataTable)Session["RecTblCurricularActivity"]) != null)
    //        {
    //            int maxVals = 0;
    //            DataTable dtActivity = (DataTable)Session["RecTblCurricularActivity"];
    //            DataRow dr = dtActivity.NewRow();
    //            if (dr != null)
    //            {
    //                maxVals = Convert.ToInt32(dtActivity.AsEnumerable().Max(row => row["SRNO"]));
    //            }




    //            dr["SRNO"] = maxVals + 1;
    //            dr["Name_of_Activity"] = ddlActivity.SelectedItem.Text;
    //            dr["API_Score_Alloted"] = ddlAlloted.SelectedItem.Text;
    //            dr["API_Score_Claimed"] = txtScoreClaimed.Text.Trim() == null ? string.Empty : Convert.ToString(txtScoreClaimed.Text.Trim()).Replace(',', ' ');
    //            dr["Verified_API_Score"] = txtVerifiedAPI.Text.Trim() == null ? string.Empty : Convert.ToString(txtVerifiedAPI.Text.Trim()).Replace(',', ' ');


    //            dtActivity.Rows.Add(dr);
    //            Session["RecTblCurricularActivity"] = dtActivity;
    //            lvActivity.DataSource = dtActivity;
    //            lvActivity.DataBind();
    //            ClearRecFromActivity();
    //            ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
    //        }
    //        else
    //        {

    //            DataTable dtActivity = this.CreateTabelCurricularActivity();
    //            DataRow dr = dtActivity.NewRow();

    //            dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
    //            dr["Name_of_Activity"] = ddlActivity.SelectedItem.Text;
    //            dr["API_Score_Alloted"] = ddlAlloted.SelectedItem.Text;
    //            dr["API_Score_Claimed"] = txtScoreClaimed.Text.Trim() == null ? string.Empty : Convert.ToString(txtScoreClaimed.Text.Trim()).Replace(',', ' ');
    //            dr["Verified_API_Score"] = txtVerifiedAPI.Text.Trim() == null ? string.Empty : Convert.ToString(txtVerifiedAPI.Text.Trim()).Replace(',', ' ');


    //            ViewState["RecTblCurricularActivity"] = Convert.ToInt32(ViewState["RecTblCurricularActivity"]) + 1;
    //            dtActivity.Rows.Add(dr);
    //            ClearRecFromActivity();
    //            Session["RecTblCurricularActivity"] = dtActivity;
    //            lvActivity.DataSource = dtActivity;
    //            lvActivity.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnActivity_Click -->" + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //protected void ClearRecFromActivity()
    //{

    //    txtScoreClaimed.Text = string.Empty;
    //    ddlActivity.SelectedValue = "0";
    //    ddlAlloted.SelectedValue = "0";
    //    txtVerifiedAPI.Text = string.Empty;
    //    ViewState["SRNO"] = null;
    //}

    //protected void btnEditActivities_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEditActivities = sender as ImageButton;
    //        DataTable dtActivity;
    //        if (Session["RecTblCurricularActivity"] != null && ((DataTable)Session["RecTblCurricularActivity"]) != null)
    //        {
    //            dtActivity = ((DataTable)Session["RecTblCurricularActivity"]);

    //            DataRow dr = this.GetEditableDatarowFromActivity(dtActivity, btnEditActivities.CommandArgument);


    //            //txtEnrolled.Text = dr["Number_Enrolled"].ToString();
    //            //txtpapertitle.Text = dr["Paper_title"].ToString();
    //            //txtDatereviewed.Text = dr["Date_Reviewed"].ToString();


    //            dtActivity.Rows.Remove(dr);
    //            Session["RecTblCurricularActivity"] = dtActivity;
    //            lvGuidance.DataSource = dtActivity;
    //            lvGuidance.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["erreor"]) == true)
    //            objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditResearchGuidance_Click -->" + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    //private DataRow GetEditableDatarowFromActivity(DataTable dt, string value)
    //{
    //    DataRow datRow = null;
    //    try
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            if (dr["SRNO"].ToString() == value)
    //            {
    //                datRow = dr;
    //                break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromResarchPaper -->" + ex.Message + "" + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //    return datRow;
    //}

    //#endregion    

    #region Teaching Learning Activities

    private void TeachingLearningActivities()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_TEACHING_LERNING_ACTIVIES_MASTER", "*", "", "", "SN_ID");
        lvTeachActivity.DataSource = ds;
        lvTeachActivity.DataBind();
        hdRowCount.Value = ds.Tables[0].Rows.Count.ToString();
    }

    #endregion

    #region Performance In Engaging Lectures

    private void EngagingLectures()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_PERFORMANCE_IN_ENGAGING_LECTURES_MASTER", "*", "", "", "SN_ID");
        lvEngagingLectures.DataSource = ds;
        lvEngagingLectures.DataBind();

        hdnEngagingLecture.Value = ds.Tables[0].Rows.Count.ToString();
          
    }

    #endregion

    #region Lectures And Academic Duties in Excess of UGC Norms

    private void Duties_in_Excess_of_UGC_Norms()
    {
       // DataSet ds1 = objCommon.FillDropDown("APPRAISAL_PERFORMANCE_IN_ENGAGING_LECTURES", "SUM(Extra_HRS) AS Extra_HRS", "", "SESSION_ID =" + Convert.ToInt32(ddlSession.SelectedValue) + "AND EMPLOYEE_ID= " + Convert.ToInt32(ddlEmployee.SelectedValue), "");

       // int ExtraHRS = Convert.ToInt32(ds1.Tables[0].Rows[0]["Extra_HRS"]);
        DataSet ds = objCommon.FillDropDown("APPRAISAL_LECTURES_ACADEMIC_DUTIES_MASTER", "*", "", "", "SN_ID");
        lvAcadDuties.DataSource = ds;
        lvAcadDuties.DataBind();
        hdnExcess.Value = ds.Tables[0].Rows.Count.ToString();
    }
    private void MaterialResource()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_PREPARATION_STUDY_MATERIAL_MASTER", "*", "", "", "SN_ID");
        lvResources.DataSource = ds;
        lvResources.DataBind();
        hdnMaterial.Value = ds.Tables[0].Rows.Count.ToString();
    }

    #endregion

    #region Administrative And Academic
    private void Administrative_Academic()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_ADMINISTATIVE_AND_ACADEMIC_MASTER", "*", "", "", "SN_ID");
        lvAcademic.DataSource = ds;
        lvAcademic.DataBind();
        hdnAdminAcademic.Value = ds.Tables[0].Rows.Count.ToString();
    }
    private void ProfessionalDevelopment()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_PROFESSIONAL_DEVELOPMENT_ACTIVITIES_MASTER", "*", "", "", "SN_ID");
        lvDevelopment.DataSource = ds;
        lvDevelopment.DataBind();
        hdnDevelopment.Value = ds.Tables[0].Rows.Count.ToString();
    }

    private void ReacherchGuidenceQualification()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_RESEARCH_GUIDANCE", "APPRAISAL_RESEARCH_GUIDANCE_ID as SRNO,RESEARCH_GUIDANCE,NUMBER_ENROLLED,THESIS_SUBMITTED,DEGREE_AWARDED,API_SCORE_CLAIMED,VERIFIED_API_SCORE", "", " EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "and SESSION_ID=" + Convert.ToInt32(ddlSession.SelectedValue), "");
        lvGuidance.DataSource = ds;
        lvGuidance.DataBind();
        hdnGuidance.Value = ds.Tables[0].Rows.Count.ToString();
    }
    #endregion

    #region Innovative Teaching Learning Methods

    private void InnovativeTeaching()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_INNOVATIVE_TEACHING_LEARNING_MASTER", "*", "", "", "SN_ID");
        lvInnovative.DataSource = ds;
        lvInnovative.DataBind();
        hdnApiCount.Value = ds.Tables[0].Rows.Count.ToString();
    }
    private void StudentsFeedback()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_STUDENTS_FEEDBACK_MASTER", "*", "", "", "SN_ID");
        lvFeedback.DataSource = ds;
        lvFeedback.DataBind();
        hdnFeedback.Value = ds.Tables[0].Rows.Count.ToString();
    }
    private void ExaminationWork()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_EXAMINATION_RELETED_WORK_MASTER", "*", "", "", "SN_ID");
        lvExamination.DataSource = ds;
        lvExamination.DataBind();
        hdnExamination.Value = ds.Tables[0].Rows.Count.ToString();
    }

    #endregion

    #region Performance In Attendance Of Students

    private DataTable CreateTabelStudentsAttendance()
    {
        DataTable dtStudentAttendance = new DataTable();
        dtStudentAttendance.Columns.Add(new DataColumn("SR_NO", typeof(int)));
        dtStudentAttendance.Columns.Add(new DataColumn("Course", typeof(string)));
        dtStudentAttendance.Columns.Add(new DataColumn("Subject_Taught", typeof(string)));
        dtStudentAttendance.Columns.Add(new DataColumn("PRESENT_STUDENTS", typeof(string)));
        dtStudentAttendance.Columns.Add(new DataColumn("LECTURES", typeof(string)));
        dtStudentAttendance.Columns.Add(new DataColumn("STUDENT_ROLL", typeof(string)));
        dtStudentAttendance.Columns.Add(new DataColumn("Attendance", typeof(string)));

        return dtStudentAttendance;
    }
    protected void btnStudentsAttendance_Click(object sender, EventArgs e)
    {



        try
        {
            lvAttendance.Visible = true;
            if (Session["RecTblAttendance"] != null && ((DataTable)Session["RecTblAttendance"]) != null)
            {
                int maxVals = 0;
                DataTable dtStudentAttendance = (DataTable)Session["RecTblAttendance"];
                DataRow dr = dtStudentAttendance.NewRow();
                if (dr != null)
                {
                    maxVals = Convert.ToInt32(dtStudentAttendance.AsEnumerable().Max(row => row["SR_NO"]));
                }

                dr["SR_NO"] = maxVals + 1;
                dr["Course"] = txtCourse.Text.Trim() == null ? string.Empty : Convert.ToString(txtCourse.Text.Trim());
                dr["Subject_Taught"] = txtSubject.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubject.Text.Trim());
                dr["PRESENT_STUDENTS"] = txtStudPresent.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudPresent.Text.Trim());
                dr["LECTURES"] = txtEngaged.Text.Trim() == null ? string.Empty : Convert.ToString(txtEngaged.Text.Trim());
                dr["STUDENT_ROLL"] = txtStudRoll.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudRoll.Text.Trim());
                int stup = Convert.ToInt32(txtStudPresent.Text) * 100;
                int tot = Convert.ToInt32(txtEngaged.Text) * Convert.ToInt32(txtStudRoll.Text);
                int avg = stup / tot;
                dr["Attendance"] = avg;

                dtStudentAttendance.Rows.Add(dr);
                Session["RecTblAttendance"] = dtStudentAttendance;
                lvAttendance.DataSource = dtStudentAttendance;
                lvAttendance.DataBind();
                clearAttendancetext();
                //ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                ViewState["SRNO_Performance"] = Convert.ToInt32(ViewState["SRNO_Performance"]) + 1;
            }
            else
            {

                DataTable dtStudentAttendance = this.CreateTabelStudentsAttendance();
                DataRow dr = dtStudentAttendance.NewRow();
                dr["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
                dr["Course"] = txtCourse.Text.Trim() == null ? string.Empty : Convert.ToString(txtCourse.Text.Trim());
                dr["Subject_Taught"] = txtSubject.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubject.Text.Trim());
                dr["PRESENT_STUDENTS"] = txtStudPresent.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudPresent.Text.Trim());
                dr["LECTURES"] = txtEngaged.Text.Trim() == null ? string.Empty : Convert.ToString(txtEngaged.Text.Trim());
                dr["STUDENT_ROLL"] = txtStudRoll.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudRoll.Text.Trim());

                int stup = Convert.ToInt32(txtStudPresent.Text) * 100;
                int tot = Convert.ToInt32(txtEngaged.Text) * Convert.ToInt32(txtStudRoll.Text);
                int avg = stup / tot;
                dr["Attendance"] = avg;

                ViewState["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
                dtStudentAttendance.Rows.Add(dr);
                clearAttendancetext();
                Session["RecTblAttendance"] = dtStudentAttendance;
                lvAttendance.DataSource = dtStudentAttendance;
                lvAttendance.DataBind();
            }


            double tot1 = 0;
            int count = 0;
            foreach (ListViewItem itemRow in lvAttendance.Items)
            {

                Label lvlavgattendance = (Label)itemRow.FindControl("lvlavgattendance") as Label;


                tot1 += Convert.ToDouble(lvlavgattendance.Text);
                count++;
                
            }
            txtAverage.Text = (tot1 / count).ToString("0.0000");
        
            
            if (Convert.ToDouble(txtAverage.Text) >= 75)
            {
                txtFactor.Text = "Excellent";
                txtScore.Text = Convert.ToString(20 * 1);
            }
            if (Convert.ToDouble(txtAverage.Text) >= 61 && Convert.ToDouble(txtAverage.Text) < 75)
            {
                txtFactor.Text = "Good";
                txtScore.Text = Convert.ToString(20 * 0.7);
            }
            if (Convert.ToDouble(txtAverage.Text) >= 40 && Convert.ToDouble(txtAverage.Text) < 60)
            {
                txtFactor.Text = "Poor";
                txtScore.Text = Convert.ToString(20 * 0.2);
            }

            if (Convert.ToDouble(txtAverage.Text) < 40)
            {
                txtFactor.Text = "Poor";
                txtScore.Text = Convert.ToString(20 * 0);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPerformanceInResult_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }




        #region
        //try
        //{
        //    lvAttendance.Visible = true;

        //    if (Session["RecTblAttendance"] != null && ((DataTable)Session["RecTblAttendance"]) != null)
        //    {


        //        DataTable dtStudentAttendance = (DataTable)Session["RecTblAttendance"];
        //        DataRow dr = dtStudentAttendance.NewRow();
        //        //if (CheckDuplicateDetails(dtDetails, txtDetails.Text))
        //        //{
        //        //    objCommon.DisplayMessage(this.updActivity, "This Information Already Exist.", this.Page);
        //        //    return;
        //        //}
        //        dr["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
        //        dr["Course"] = txtCourse.Text.Trim() == null ? string.Empty : Convert.ToString(txtCourse.Text.Trim());
        //        dr["Subject_Taught"] = txtSubject.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubject.Text.Trim());
        //        dr["PRESENT_STUDENTS"] = txtStudPresent.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudPresent.Text.Trim());
        //        dr["LECTURES"] = txtEngaged.Text.Trim() == null ? string.Empty : Convert.ToString(txtEngaged.Text.Trim());
        //        dr["STUDENT_ROLL"] = txtStudRoll.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudRoll.Text.Trim());



        //        dtStudentAttendance.Rows.Add(dr);
        //        Session["RecTblAttendance"] = dtStudentAttendance;
        //        lvAttendance.DataSource = dtStudentAttendance;
        //        lvAttendance.DataBind();
        //      //  ClearRecDetails();
        //        ViewState["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
        //    }


        //    else
        //    {
        //        DataTable dtStudentAttendance = this.CreateTabelStudentsAttendance();
        //        DataRow dr = dtStudentAttendance.NewRow();


        //        //if (CheckDuplicateDetails(dtDetails, txtDetails.Text))
        //        //{
        //        //    objCommon.DisplayMessage(this.updActivity, "This Information Already Exist.", this.Page);
        //        //    return;
        //        //}
        //        dr["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
        //        dr["Course"] = txtCourse.Text.Trim() == null ? string.Empty : Convert.ToString(txtCourse.Text.Trim());
        //        dr["Subject_Taught"] = txtSubject.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubject.Text.Trim());
        //        dr["PRESENT_STUDENTS"] = txtStudPresent.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudPresent.Text.Trim());
        //        dr["LECTURES"] = txtEngaged.Text.Trim() == null ? string.Empty : Convert.ToString(txtEngaged.Text.Trim());
        //        dr["STUDENT_ROLL"] = txtStudRoll.Text.Trim() == null ? string.Empty : Convert.ToString(txtStudRoll.Text.Trim());
        //        ViewState["SR_NO"] = Convert.ToInt32(ViewState["SR_NO"]) + 1;
        //        dtStudentAttendance.Rows.Add(dr);
        //        // ClearRecDetails();
        //        Session["RecTblAttendance"] = dtStudentAttendance;
        //        lvAttendance.DataSource = dtStudentAttendance;
        //        lvAttendance.DataBind();
        //    }


        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objCommon.ShowError(Page, "IQAC_Transaction_PBAS_Proforma.btnAddDetails_Click -->" + ex.Message + "" + ex.StackTrace);
        //    else
        //        objCommon.ShowError(Page, "Server Unavailable.");
        //}
        #endregion
    }

    protected void clearAttendancetext()
    {
        txtCourse.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtStudPresent.Text = string.Empty;
        txtEngaged.Text = string.Empty;
        txtStudRoll.Text = string.Empty;


    }

    protected void txtWeight_TextChanged(object sender, EventArgs e)
    {

        int avg = Convert.ToInt32(txtAverage.Text);
        int wgt = Convert.ToInt32(txtWeight.Text);

        int apis = avg * wgt;
        txtScore.Text = Convert.ToString(apis);
    }
    protected void ClearRecFromStudentAttendance()
    {

        txtCourse.Text = string.Empty;
        txtSubject.Text = string.Empty; txtStudPresent.Text = string.Empty;
        txtEngaged.Text = string.Empty; txtStudRoll.Text = string.Empty;

        //ViewState["SRNO"] = null;
        ViewState["SRNO_Attendance"] = null;
    }

    protected void btnEditStudentsAttendance_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditStudentsAttendance = sender as ImageButton;
            DataTable dtStudentAttendance;
            //if (Session["RecTblStudentAttendance"] != null && ((DataTable)Session["RecTblStudentAttendance"]) != null)
            //{
            dtStudentAttendance = ((DataTable)Session["RecTblAttendance"]);

            DataRow dr = this.GetEditableDatarowFromStudentAttendance(dtStudentAttendance, btnEditStudentsAttendance.CommandArgument);


            //txtEnrolled.Text = dr["Number_Enrolled"].ToString();
            //txtpapertitle.Text = dr["Paper_title"].ToString();
            //txtDatereviewed.Text = dr["Date_Reviewed"].ToString();


            dtStudentAttendance.Rows.Remove(dr);
            Session["RecTblAttendance"] = dtStudentAttendance;
            lvAttendance.DataSource = dtStudentAttendance;
            lvAttendance.DataBind();
            //}


              int tot1 = 0;
            int count = 0;
            foreach (ListViewItem itemRow in lvAttendance.Items)
            {

                Label lvlavgattendance = (Label)itemRow.FindControl("lvlavgattendance") as Label;


                tot1 += Convert.ToInt32(lvlavgattendance.Text);
                count++;
                
            }
            txtAverage.Text = Convert.ToString(tot1 / count);
        
            
            if (Convert.ToInt32(txtAverage.Text) >= 75)
            {
                txtFactor.Text = "Excellent";
                txtScore.Text = Convert.ToString(20 * 1);
            }
            if (Convert.ToInt32(txtAverage.Text) >= 61 && Convert.ToInt32(txtAverage.Text) < 75)
            {
                txtFactor.Text = "Good";
                txtScore.Text = Convert.ToString(20 * 0.7);
            }
            if (Convert.ToInt32(txtAverage.Text) >= 40 && Convert.ToInt32(txtAverage.Text) < 60)
            {
                txtFactor.Text = "Poor";
                txtScore.Text = Convert.ToString(20 * 0.2);
            }

            if (Convert.ToInt32(txtAverage.Text) < 40 )
            {
                txtFactor.Text = "Poor";
                txtScore.Text = Convert.ToString(20 * 0);
            }
        }
            //int alltotal = 0;
            //int total = 0;

            //int l_engage = 0;
            //int l_total = 0;
            //int L_Aengage = 0;

            //int studroll = 0;
            //int S_Total = 0;
            //int STUD_TOT = 0;

            ////  int StudPresent = 0;
            //// StudPresent = Convert.ToInt32(txtAverage.Text);
            //foreach (ListViewItem itemRow in lvAttendance.Items)
            //{
            //    Label lblStudPresent = (Label)itemRow.FindControl("lblStudPresent") as Label;
            //    Label lblLecturesEngaged = (Label)itemRow.FindControl("lblLecturesEngaged") as Label;
            //    Label lblStudentsRoll = (Label)itemRow.FindControl("lblStudentsRoll") as Label;

            //    if (lblStudPresent != null)
            //    {
            //        total = Convert.ToInt32(lblStudPresent.Text);
            //        alltotal = alltotal - Convert.ToInt32(total);  // +Convert.ToInt32(StudPresent);
            //    }

            //    if (lblLecturesEngaged != null)
            //    {
            //        l_engage = Convert.ToInt32(lblLecturesEngaged.Text);
            //        l_total = l_total - Convert.ToInt32(l_engage);
            //    }
            //    if (lblStudentsRoll != null)
            //    {
            //        studroll = Convert.ToInt32(lblStudentsRoll.Text);
            //        S_Total = S_Total - Convert.ToInt32(studroll);
            //    }
            //}
            //L_Aengage = l_total;
            //STUD_TOT = S_Total;
            //double tot = alltotal * 100;
            //int SUM = l_total * S_Total;

            //int T = Convert.ToInt32(tot) / Convert.ToInt32(SUM);
            //txtAverage.Text = Convert.ToString(T);





       
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditResearchGuidance_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarowFromStudentAttendance(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SR_NO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromStudentAttendance -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    //protected void btnDeletedRes_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDel = sender as ImageButton;
    //        int SRNO = int.Parse(btnDel.CommandArgument);


    //        int APPR_RESEARCHID = Convert.ToInt32((objCommon.LookUp("APPRAISAL_RESARCH_PAPER_REVIEWED", "APPRAISAL_RESARCH_PAPER_REVIEWED_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "AND APPRAISAL_EMPLOYEE_ID= " + ViewState["APPRAISAL_EMPLOYEE_ID"] + " AND SRNO=" + SRNO)));




    //        int IDNO = Convert.ToInt32(Session["idno"]);
    //        DataTable dtResarchPaper;

    //        CustomStatus cs = (CustomStatus)objAPPRController.DeleteResarchPapered(APPR_RESEARCHID);
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //        {
    //            dtResarchPaper = ((DataTable)Session["RecTblResarchGuidance"]);

    //            DataRow dr = this.GetEditableDatarowFromResarchPaper(dtResarchPaper, btnDel.CommandArgument);
    //            dtResarchPaper.Rows.Remove(dr);
    //            Session["RecTblResarchGuidance"] = dtResarchPaper;
    //            lvGuidance.DataSource = dtResarchPaper;
    //            lvGuidance.DataBind();
    //            ClearRecFromResarchPaper();

    //            MessageBox("Record Deleted Successfully");
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    #endregion

    #region Performance In Results

    private DataTable CreateTabelPerformanceResults()
    {
        DataTable dtPResults = new DataTable();
        dtPResults.Columns.Add(new DataColumn("SR_NO", typeof(int)));
        dtPResults.Columns.Add(new DataColumn("COURSE", typeof(string)));
        dtPResults.Columns.Add(new DataColumn("Subject_Taught", typeof(string)));
        dtPResults.Columns.Add(new DataColumn("LAST_YEAR_RESULT", typeof(string)));
        dtPResults.Columns.Add(new DataColumn("CURRENT_INSTITUTE_RESULT", typeof(string)));
        dtPResults.Columns.Add(new DataColumn("Average", typeof(string)));


        return dtPResults;
    }
    protected void btnPerformanceInResult_Click(object sender, EventArgs e)
    {
        try
        {
            lvResults.Visible = true;
            if (Session["RecTblPerformanceResults"] != null && ((DataTable)Session["RecTblPerformanceResults"]) != null)
            {
                int maxVals = 0;
                DataTable dtPResults = (DataTable)Session["RecTblPerformanceResults"];
                DataRow dr = dtPResults.NewRow();
                if (dr != null)
                {
                    maxVals = Convert.ToInt32(dtPResults.AsEnumerable().Max(row => row["SR_NO"]));
                }

                dr["SR_NO"] = maxVals + 1;
                dr["COURSE"] = txtClass.Text.Trim() == null ? string.Empty : Convert.ToString(txtClass.Text.Trim()).Replace(',', ' ');
                dr["Subject_Taught"] = txtSubjectT.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubjectT.Text.Trim()).Replace(',', ' ');
                dr["LAST_YEAR_RESULT"] = txtLstYrResult.Text.Trim() == null ? string.Empty : Convert.ToString(txtLstYrResult.Text.Trim()).Replace(',', ' ');
                dr["CURRENT_INSTITUTE_RESULT"] = txtInstitute.Text.Trim() == null ? string.Empty : Convert.ToString(txtInstitute.Text.Trim()).Replace(',', ' ');

                int stup = Convert.ToInt32(txtInstitute.Text);
                int tot = Convert.ToInt32(txtLstYrResult.Text);
                int avg = stup / tot;
                dr["Average"] = avg *100;

                dtPResults.Rows.Add(dr);
                Session["RecTblPerformanceResults"] = dtPResults;
                lvResults.DataSource = dtPResults;
                lvResults.DataBind();
                ClearRecFromPerformanceResults();
                //ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                ViewState["SRNO_Performance"] = Convert.ToInt32(ViewState["SRNO_Performance"]) + 1;
            }
            else
            {

                DataTable dtPResults = this.CreateTabelPerformanceResults();
                DataRow dr = dtPResults.NewRow();

                //dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["SR_NO"] = Convert.ToInt32(ViewState["SRNO_Performance"]) + 1;
                dr["COURSE"] = txtClass.Text.Trim() == null ? string.Empty : Convert.ToString(txtClass.Text.Trim()).Replace(',', ' ');
                dr["Subject_Taught"] = txtSubjectT.Text.Trim() == null ? string.Empty : Convert.ToString(txtSubjectT.Text.Trim()).Replace(',', ' ');
                dr["LAST_YEAR_RESULT"] = txtLstYrResult.Text.Trim() == null ? string.Empty : Convert.ToString(txtLstYrResult.Text.Trim()).Replace(',', ' ');
                dr["CURRENT_INSTITUTE_RESULT"] = txtInstitute.Text.Trim() == null ? string.Empty : Convert.ToString(txtInstitute.Text.Trim()).Replace(',', ' ');

                int stup = Convert.ToInt32(txtInstitute.Text);
                int tot = Convert.ToInt32(txtLstYrResult.Text);
                int avg = stup / tot;
                dr["Average"] = avg * 100;

                ViewState["RecTblPerformanceResults"] = Convert.ToInt32(ViewState["RecTblPerformanceResults"]) + 1;
                dtPResults.Rows.Add(dr);
                ClearRecFromPerformanceResults();
                Session["RecTblPerformanceResults"] = dtPResults;
                lvResults.DataSource = dtPResults;
                lvResults.DataBind();
            }


            double tot1 = 0;
            int count = 0;
            foreach (ListViewItem itemRow in lvResults.Items)
            {

                Label lblaverage = (Label)itemRow.FindControl("lblaverage") as Label;


                tot1 += Convert.ToDouble(lblaverage.Text);
                count++;

            }
            txtAvg.Text = (tot1 / count).ToString("0.0000"); ;


            if (Convert.ToDouble(txtAvg.Text) >= 75)
            {
                txtMulFactor.Text = "Excellent";
                txtClaimed.Text = Convert.ToString(20 * 1);

            }
            if (Convert.ToDouble(txtAvg.Text) > 61 && Convert.ToDouble(txtAvg.Text) < 75)
            {
                txtMulFactor.Text = "Good";
                txtClaimed.Text = Convert.ToString(20 * 0.7);

            }
            if (Convert.ToDouble(txtAvg.Text) >= 40 && Convert.ToDouble(txtAvg.Text) < 61)
            {
                txtMulFactor.Text = "Poor";
                txtClaimed.Text = Convert.ToString(20 * 0.2);

            }
            if (Convert.ToDouble(txtAvg.Text) < 40)
            {
                txtMulFactor.Text = "Poor";
                txtClaimed.Text = Convert.ToString(20 * 0);

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnPerformanceInResult_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void ClearRecFromPerformanceResults()
    {
        txtClass.Text = string.Empty;
        txtSubjectT.Text = string.Empty; txtLstYrResult.Text = string.Empty;
        txtInstitute.Text = string.Empty;
        //ViewState["SRNO"] = null;
        ViewState["SRNO_Performance"] = null;
    }

    protected void btnEditPerformanceResults_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditPerformanceResults = sender as ImageButton;
            DataTable dtResults;
            if (Session["RecTblPerformanceResults"] != null && ((DataTable)Session["RecTblPerformanceResults"]) != null)
            {
                dtResults = ((DataTable)Session["RecTblPerformanceResults"]);

                DataRow dr = this.GetEditableDatarowFromPerformanceResults(dtResults, btnEditPerformanceResults.CommandArgument);


                //txtEnrolled.Text = dr["Number_Enrolled"].ToString();
                //txtpapertitle.Text = dr["Paper_title"].ToString();
                //txtDatereviewed.Text = dr["Date_Reviewed"].ToString();


                dtResults.Rows.Remove(dr);
                Session["RecTblPerformanceResults"] = dtResults;
                lvResults.DataSource = dtResults;
                lvResults.DataBind();

                int tot1 = 0;
                int count = 0;
                foreach (ListViewItem itemRow in lvResults.Items)
                {

                    Label lblaverage = (Label)itemRow.FindControl("lblaverage") as Label;


                    tot1 += Convert.ToInt32(lblaverage.Text);
                    count++;

                }
                
                txtAvg.Text = Convert.ToString(tot1 / count);


                if (Convert.ToInt32(txtAvg.Text) >= 75)
                {
                    txtMulFactor.Text = "Excellent";
                    txtClaimed.Text = Convert.ToString(20 * 1);

                }
                if (Convert.ToInt32(txtAvg.Text) > 61 && Convert.ToInt32(txtAvg.Text) < 75)
                {
                    txtMulFactor.Text = "Good";
                    txtClaimed.Text = Convert.ToString(20 * 0.7);

                }
                if (Convert.ToInt32(txtAvg.Text) >= 40 && Convert.ToInt32(txtAvg.Text) < 61)
                {
                    txtMulFactor.Text = "Poor";
                    txtClaimed.Text = Convert.ToString(20 * 0.2);

                }
                if (Convert.ToInt32(txtAvg.Text) < 40)
                {
                    txtMulFactor.Text = "Poor";
                    txtClaimed.Text = Convert.ToString(20 * 0);

                }
                


               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditPerformanceResults_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarowFromPerformanceResults(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SR_NO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromPerformanceResults -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    //protected void btnDeletedRes_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnDel = sender as ImageButton;
    //        int SRNO = int.Parse(btnDel.CommandArgument);


    //        int APPR_RESEARCHID = Convert.ToInt32((objCommon.LookUp("APPRAISAL_RESARCH_PAPER_REVIEWED", "APPRAISAL_RESARCH_PAPER_REVIEWED_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "AND APPRAISAL_EMPLOYEE_ID= " + ViewState["APPRAISAL_EMPLOYEE_ID"] + " AND SRNO=" + SRNO)));




    //        int IDNO = Convert.ToInt32(Session["idno"]);
    //        DataTable dtResarchPaper;

    //        CustomStatus cs = (CustomStatus)objAPPRController.DeleteResarchPapered(APPR_RESEARCHID);
    //        if (cs.Equals(CustomStatus.RecordDeleted))
    //        {
    //            dtResarchPaper = ((DataTable)Session["RecTblResarchGuidance"]);

    //            DataRow dr = this.GetEditableDatarowFromResarchPaper(dtResarchPaper, btnDel.CommandArgument);
    //            dtResarchPaper.Rows.Remove(dr);
    //            Session["RecTblResarchGuidance"] = dtResarchPaper;
    //            lvGuidance.DataSource = dtResarchPaper;
    //            lvGuidance.DataBind();
    //            ClearRecFromResarchPaper();

    //            MessageBox("Record Deleted Successfully");
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Pay_publication_Details .btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}


    #endregion

    #region Student Related Co-Curricular, Extension And Field Based Activities

    private void CO_CURRICULAR_ACTIVITIES()
    {
        DataSet ds = objCommon.FillDropDown("APPRAISAL_STUDENT_RELETED_CO_CURRICULAR_MASTER", "*", "", "", "SN_ID");
        lvCurricular.DataSource = ds;
        lvCurricular.DataBind();
        hdnCurricular.Value = ds.Tables[0].Rows.Count.ToString();
    }

    #endregion

    #region Professional Development Activity
    private DataTable CreateTabelProfessionalDevelopment()
    {
        DataTable dtAdminAcademic = new DataTable();
        dtAdminAcademic.Columns.Add(new DataColumn("SN_ID", typeof(int)));
        dtAdminAcademic.Columns.Add(new DataColumn("NAME_OF_ACTIVITY", typeof(string)));
        dtAdminAcademic.Columns.Add(new DataColumn("API_SCORE_ALLOTTED", typeof(string)));

        return dtAdminAcademic;
    }
    #endregion




    #region FINAL SUBMIT
    protected void btnOkDel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["usertype"]) == 3)   //faculty user
            {
                //string lockByEmp = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)); // if final submit is done by employee then hide all panels
                string lockByEmp = objCommon.LookUp("APPRAISAL_EMPLOYEE", "", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

                if (lockByEmp == "1")
                {
                    PanelHideForEmpFinalSubmit();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Final Submission Is Done.');", true);
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objAPPRController.UpdateLock(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        PanelHideForEmpFinalSubmit();
                        LinkButton_FinalSubmit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submission Done Successfully.');", true);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnOkDel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void LinkButton_FinalSubmit_Click(object sender, EventArgs e)
    {
        // if (Request.QueryString["autho"] == "Y")



        //  if (Request.QueryString["SRNO"].ToString() == "1")
        //{
        // if (Request.QueryString["SRNO"].ToString() == "1")//Reporting Autohrity
        if (Convert.ToInt32(ViewState["SRNO"]) == 1)
        {

            DataSet ds = null;
            //ds = objCommon.FillDropDown("APPRAISAL_GENERAL_INFORMATION", "*", "", "EMP_ID='" + Convert.ToInt32(Request.QueryString["empid"]) + "' AND SESSIONNO='" + Convert.ToInt32(ddlSession.SelectedValue) + "'", "");
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    DisplayMessage("Please Fill General Information Details");
            //    return;
            //}
            string lockByReport = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(REPORT_LOCK, 0)", "APPRAISAL_EMPLOYEE_ID=" + Convert.ToInt32(ViewState["APPRAISAL_EMP_ID"]));
            if (lockByReport == "1")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submission Is Already Done.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objAPPRController.UpdateFinalLock(Convert.ToInt32(Session["APAR_empid"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SRNO"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submit Done Successfully.');", true);
                    PanelHideForEmpFinalSubmit();
                }
            }
        }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Fill Assessment Section.');", true);
        //    return;
        //}

        else
        {
            // if (Request.QueryString["SRNO"].ToString() == "2")// Reviewing Authority
            if (Convert.ToInt32(ViewState["SRNO"]) == 2)
            {
                string lockByReview = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(REVIEW_LOCK, 0)", "APPRAISAL_EMPLOYEE_ID=" + Convert.ToInt32(ViewState["APPRAISAL_EMP_ID"]));
                if (lockByReview == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submission Is Already Done.');", true);
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objAPPRController.UpdateFinalLock(Convert.ToInt32(Session["APAR_empid"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SRNO"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submit Done Successfully.');", true);
                        PnlReviewing.Enabled = false;
                        btnReviewing.Enabled = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Your Remark To Final Submit.');", true);
                        return;
                    }
                }
            }


            else
            {
                if (Convert.ToInt32(Session["usertype"]) == 3)  // faculty user  
                {
                    // string lockByFaculty = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(USER_LOCK, 0)", "APPRAISAL_EMPLOYEE_ID=" + Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"]));
                    string lockByFaculty = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(USER_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    //string Filled = objCommon.LookUp("APPRAISAL_EMPLOYEE", "ISNULL(APPRAISAL_EMPLOYEE_ID,0)", "EMPLOYEE_ID='" + Convert.ToInt32(Session["idno"]) + "' AND SESSIONNO='" + Convert.ToInt32(ddlSession.SelectedValue) + "'");
                    string Filled = objCommon.LookUp("APPRAISAL_EMPLOYEE", "ISNULL(APPRAISAL_EMPLOYEE_ID,0)", "EMPLOYEE_ID='" + Convert.ToInt32(ddlEmployee.SelectedValue) + "' AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                    // }
                    if (Filled != "0" && Filled != "")
                    {
                        // DataSet ds = null;
                        //DataSet dss = null;
                        //ds = objCommon.FillDropDown("APPRAISAL_TEACHING", "*", "", "EMP_ID='" + Convert.ToInt32(Session["idno"]) + "' AND SESSIONNO='" + Convert.ToInt32(ddlSession.SelectedValue) + "'", "");
                        //if (ds.Tables[0].Rows.Count == 0)
                        //{
                        //    DisplayMessage("Please Fill Academic Information Details");
                        //    return;
                        //}
                        //dss = objAPPRController.GetReserchGuidanceData(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSession.SelectedValue));
                        //if (dss.Tables[0].Rows.Count == 0 && dss.Tables[1].Rows.Count == 0 && dss.Tables[2].Rows.Count == 0 && dss.Tables[3].Rows.Count == 0 && dss.Tables[4].Rows.Count == 0)
                        //{
                        //    DisplayMessage("Please Fill Research Guidance Details");
                        //    return;
                        //}
                        if (lockByFaculty == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submission Is Already Done.');", true);
                            return;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objAPPRController.UpdateFinalLock(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["SRNO"]));
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Final Submit Done Successfully.');", true);
                            }

                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Fill Faculty Information Details.');", true);
                    return;
                }
            }
        }
    }

    private void DisplayMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }


    private void finalsubmitclick()
    {
        string AppraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "USER_LOCK", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

        if (AppraisalEmp_Id == "1")
        {
            LinkButton_FinalSubmit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
        }
        else
        {
            LinkButton_FinalSubmit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#990099");
        }
    }



    #endregion

    #region Panel Final Submit & Hide/Show ListView

    private void PanelShowForEmpFinalSubmit()
    {
        // PERSNOL VIEW
        Panel_View_PersonalInfo.Enabled = true;
        pnlPBAS.Enabled = true;

        PnlAttendance.Enabled = true;
        PnlLearningActivity.Enabled = true;
        PnlInnovative.Enabled = true;
        PnlFeedback.Enabled = true;
        PnlPerformingResult.Enabled = true;
        PnlDuties.Enabled = true;
        PnlEngaged.Enabled = true;
        PnlExam.Enabled = true;

        PnlResources.Enabled = true;
        PnlQualification.Enabled = true;

        PnlJournal.Enabled = true;
        PnlBook.Enabled = true;
        PnlChapters.Enabled = true;
        PnlConference.Enabled = true;
        PnlAvishkar.Enabled = true;
        PnlCompletedProject.Enabled = true;
        PnlGuidance.Enabled = true;
        PnlPatent.Enabled = true;

        //BUTTON SUBMIT 
        btnTeachingActivitySubmit.Enabled = true;
        btnPublishedDetailsSubmit.Enabled = true;
        btnConferenceSubmit.Enabled = true;
        btnPatentSubmit.Enabled = true;
        btnFieldBasesActivitySubmit.Enabled = true;
        btnResearchSubmit.Enabled = true;
        btnPerformanceResults.Enabled = true;
        btnInnovativeSubmit.Enabled = true;
        btnAcademicDutiesSubmit.Enabled = true;
        btnEngagingLecturesSubmit.Enabled = true;

        //BUTTON NEXT
        //btnNextSemester.Enabled = true;
        //btnPublishedNext.Enabled = true;
        //PnlConferenceNext.Enabled = true;
        //btnPatentNext.Enabled = true;
        //btnFieldBasesActivityNext.Enabled = true;
        //btnResearchNext.Enabled = true;
        //btnPerformanceNext.Enabled = true;
        //btnInnovativeNext.Enabled = true;
        //btnAcadNext.Enabled = true;
        //btnEngageNext.Enabled = true;
    }

    private void PanelHideForEmpFinalSubmit()
    {
        // PERSNOL VIEW

        Panel_View_PersonalInfo.Enabled = false;
        PnlLearningActivity.Enabled = false;
        lvTeachActivity.Enabled = false;
        PnlAttendance.Enabled = false;
        PnlLearning.Enabled = false;
        pnlPBAS.Enabled = false;
        lvEngagingLectures.Enabled = false;
        lvAcadDuties.Enabled = false;
        lvResources.Enabled = false;
        lvCurricular.Enabled = false;
        lvInnovative.Enabled = false;
        lvResources.Enabled = false;
        lvFeedback.Enabled = false;
        lvDevelopment.Enabled = false;
        lvAcademic.Enabled = false;
        lvExamination.Enabled = false;
        PnlAttendance.Enabled = false;
        PnlLearningActivity.Enabled = false;
        PnlInnovative.Enabled = false;
        PnlFeedback.Enabled = false;
        PnlPerformingResult.Enabled = false;
        PnlDuties.Enabled = false;
        PnlEngaged.Enabled = false;
        PnlExam.Enabled = false;
        PnlResources.Enabled = false;
        lvAttendance.Enabled = false;
        pnltest.Enabled = false;
        PnlQualification.Enabled = true;
        PnlGuidance.Enabled = true;

        PnlJournal.Enabled = false;
        PnlBook.Enabled = false;
        PnlChapters.Enabled = false;
        PnlConference.Enabled = false;
        PnlAvishkar.Enabled = false;
        PnlCompletedProject.Enabled = false;
        PnlPatent.Enabled = false;
        PnlAttendance.Enabled = false;
        divat.Disabled = true;
        //BUTTON SUBMIT 

        btnTeachingActivitySubmit.Enabled = false;
        btnPublishedDetailsSubmit.Enabled = false;
        btnConferenceSubmit.Enabled = false;
        btnPatentSubmit.Enabled = false;
        btnFieldBasesActivitySubmit.Enabled = false;
        btnResearchSubmit.Enabled = false;
        btnPerformanceResults.Enabled = false;
        btnInnovativeSubmit.Enabled = false;
        btnAcademicDutiesSubmit.Enabled = false;
        btnEngagingLecturesSubmit.Enabled = false;
        



        //BUTTON NEXT
        //btnStudentsAttendance.Enabled = false;
        //btnNextSemester.Enabled = true;
        //btnPublishedNext.Enabled = true;
        //PnlConferenceNext.Enabled = true;
        //btnPatentNext.Enabled = true;
        //btnFieldBasesActivityNext.Enabled = true;
        //btnResearchNext.Enabled = true;
        //btnPerformanceNext.Enabled = true;
        //btnInnovativeNext.Enabled = true;
        //btnAcadNext.Enabled = true;
        //btnEngageNext.Enabled = true;

    }

    private void hidelistview()
    {
        lvAttendance.Visible = false;
        lvResults.Visible = false;
        lvCurricular.Visible = false;
        lvActivity.Visible = false;

        lvpublicationJournal.Visible = false;
        lvPublishedBooks.Visible = false;
        lvPublishedChapter.Visible = false;
        lvConference.Visible = false;
        lvAvishkar.Visible = false;
        lvProjects.Visible = false;
        lvPatent.Visible = false;
        lvGuidance.Visible = false;
        lvQualification.Visible = false;

        lvInnovative.Visible = false;
        lvFeedback.Visible = false;
        lvExamination.Visible = false;
        lvTeachActivity.Visible = false;
        lvEngagingLectures.Visible = false;
        lvAcadDuties.Visible = false;
        lvResources.Visible = false;
    }

    private void Showlistview()
    {
        lvAttendance.Visible = true;
        lvResults.Visible = true;
        lvCurricular.Visible = true;
        lvActivity.Visible = true;

        lvpublicationJournal.Visible = true;
        lvPublishedBooks.Visible = true;
        lvPublishedChapter.Visible = true;
        lvConference.Visible = true;
        lvAvishkar.Visible = true;
        lvProjects.Visible = true;
        lvPatent.Visible = true;
        lvGuidance.Visible = true;
        lvQualification.Visible = true;

        lvInnovative.Visible = true;
        lvFeedback.Visible = true;
        lvExamination.Visible = true;
        lvTeachActivity.Visible = true;
        lvEngagingLectures.Visible = true;
        lvAcadDuties.Visible = true;
        lvResources.Visible = true;
    }

    #endregion

    #region Clear Methods

    private void clearAttendance()
    {
        lvAttendance.DataSource = null;
        lvAttendance.DataBind();

        lvResults.DataSource = null;
        lvResults.DataBind();

        lvCurricular.DataSource = null;
        lvCurricular.DataBind();

        lvActivity.DataSource = null;
        lvActivity.DataBind();
    }
    private void clearresarchpaper()
    {
        lvpublicationJournal.DataSource = null;
        lvpublicationJournal.DataBind();

        lvPublishedBooks.DataSource = null;
        lvPublishedBooks.DataBind();

        lvPublishedChapter.DataSource = null;
        lvPublishedChapter.DataBind();

        lvConference.DataSource = null;
        lvConference.DataBind();

        lvAvishkar.DataSource = null;
        lvAvishkar.DataBind();

        lvProjects.DataSource = null;
        lvProjects.DataBind();

        lvPatent.DataSource = null;
        lvPatent.DataBind();

        lvGuidance.DataSource = null;
        lvGuidance.DataBind();

        lvQualification.DataSource = null;
        lvQualification.DataBind();

    }
    private void clearsponserdelement()
    {
        lvInnovative.DataSource = null;
        lvInnovative.DataBind();

        lvFeedback.DataSource = null;
        lvFeedback.DataBind();

        lvExamination.DataSource = null;
        lvExamination.DataBind();

        lvTeachActivity.DataSource = null;
        lvTeachActivity.DataBind();

        lvEngagingLectures.DataSource = null;
        lvEngagingLectures.DataBind();

        lvAcadDuties.DataSource = null;
        lvAcadDuties.DataBind();

        lvResources.DataSource = null;
        lvResources.DataBind();
    }

    #endregion

    #region SessionNull
    private void SessionNull()
    {
        Session["TblPublishedJournal"] = null;
        Session["TblPublishedBooks"] = null;
        Session["TblChaptersInBook"] = null;
        Session["TblConferenceProceeds"] = null;
        Session["TblAvishkarDetails"] = null;
        Session["TblConsultancies"] = null;

        Session["TblPatentIPR"] = null;
        Session["TblGuidance"] = null;

        Session["TblPublishedJournal"] = null;
        Session["TblQualification"] = null;
        Session["RecTblStudentAttendance"] = null;

        Session["RecTblPerformanceResults"] = null;
        Session["RecTblAttendance"] = null;


    }
    private void ViewStatNull()
    {
        ViewState["SRNO_Qualification"] = null;
        ViewState["SRNO_Performance"] = 0;
        ViewState["SRNO_Attendance"] = 0;
        ViewState["SRNO_Guidance"] = 0;
        ViewState["SRNO"] = 0;
        ViewState["SR_NO"] = 0;
        ViewState["SRNO_Performance"] = 0;


    }
    #endregion


    #region Reviewing Button

    protected void btnReviewing_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            //ds = objCommon.FillDropDown("APPRAISAL_REVIEW_REMARK_TEACHING", "*", "", "EMPLOYEE_ID='" + Convert.ToInt32(Request.QueryString["empid"]) + "' AND SESSION_ID='" + Convert.ToInt32(ddlSession.SelectedValue) + "'", "");
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Save Assessment Details.');", true);
            //    return;
            //}
            objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());
            objEA.EMPLOYEE_ID = Convert.ToInt32(Session["APAR_empid"]);
            objEA.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue == string.Empty ? "0" : ddlSession.SelectedValue);

            //objEA.DO_YOU_AGREE_ASSESSMENT = Convert.ToInt32(RboReviewing.SelectedValue);
            objEA.GIVE_REASON = txtReviewingReason.Text;
            objEA.OTHER_COMMENT = txtPenPicture_Comment_reviewing.Text;
            objEA.LENGTH_OF_SERVICE = txtLengthOfService.Text;
            if (txtNumericalGrading.Text == "")
            {

                objEA.NUMERICALGRADING = 0;
            }
            else
            {
                objEA.NUMERICALGRADING = Convert.ToInt32(txtNumericalGrading.Text);
            }
            objEA.CREATED_BY = Convert.ToInt32(Session["userno"]);
            objEA.MODIFIED_BY = Convert.ToInt32(Session["userno"]);


            if (ViewState["APPRAISAL_EMPLOYEE_ID"] != null)
            {
                objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                CustomStatus cs = (CustomStatus)objAPPRController.AddUpdReviewingRemark(objEA);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);

                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);

                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnReviewing_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    #region Print Button
    protected void linkbutton_Print_Click(object sender, EventArgs e)
    {
        if (ViewState["SRNO"] == null)
        {
            // ShowReport("Employee-Appraisal", "rptTeachingFaculty.rpt"); // user
            ShowReport("Employee-Appraisal", "AppraisalFacultyReport.rpt");
        }
        else if (ViewState["SRNO"].ToString() == "1")
        {
            //ShowReport("Employee-Appraisal", "rptTeachingReportingAuthority.rpt");
            ShowReport("Employee-Appraisal", "AppraisalFacultyReport.rpt");
        }
        else if (ViewState["SRNO"].ToString() == "2")
        {
            //ShowReport("Employee-Appraisal", "rptTeachingReviewingAuthority.rpt");
            ShowReport("Employee-Appraisal", "AppraisalFacultyReport.rpt");
        }
    }

    #endregion

    //protected void LinkButton_FinalSubmit_Click(object sender, EventArgs e)
    //{
    //    if (ViewState["AUTHORITY_UANO"] == null)   // faculty user final submission.
    //    {
    //        trSession.Visible = true;
    //        string lockByEmp = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(USER_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)); // if final submit is done by employee then hide all panels

    //        if (lockByEmp == "1")
    //        {
    //            PanelHideForEmpFinalSubmit();
    //            Panel1_ModalPopupExtender.Hide();
    //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Final Submission Is Done.');", true);
    //            return;
    //        }
    //        else
    //        {
    //            Panel1_ModalPopupExtender.Show();
    //        }
    //    }
    //    else  // Reporting Channel final submission.
    //    {
    //        trSession.Visible = true;
    //        if (ViewState["AUTHORITY_UANO"] != null)
    //        {
    //            if (ViewState["SRNO"].ToString() == "1")
    //            {
    //                string lockByReportOfficer = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(REPORT_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)); // if final submit is done by Reporting Officer 

    //                if (lockByReportOfficer == "1")
    //                {
    //                    Panel1_ModalPopupExtender.Hide();
    //                    // PanelHideForReportingOfficer();
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Final Submission Is Done.');", true);
    //                    return;
    //                }
    //                else
    //                {
    //                    Panel1_ModalPopupExtender.Show();
    //                }
    //            }
    //            else if (ViewState["SRNO"].ToString() == "2")
    //            {
    //                string lockByReviewOfficer = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(REVIEW_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)); // if final submit is done by Reporting Officer 

    //                if (lockByReviewOfficer == "1")
    //                {
    //                    // PanelHideForReviewingOfficer();
    //                    Panel1_ModalPopupExtender.Hide();
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Final Submission Is Done.');", true);
    //                    return;
    //                }
    //                else
    //                {
    //                    Panel1_ModalPopupExtender.Show();
    //                }
    //            }
    //            else
    //            {
    //                string lockByCounterSignOfficer = objCommon.LookUp("APPRAISAL_EMPLOYEE", "isnull(COUNTERSIGN_LOCK, 0)", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)); // if final submit is done by Reporting Officer 

    //                if (lockByCounterSignOfficer == "1")
    //                {
    //                    //PanelHideForCounterSignOfficer();
    //                    Panel1_ModalPopupExtender.Hide();
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Final Submission Is Done.');", true);
    //                    return;
    //                }
    //                else
    //                {
    //                    Panel1_ModalPopupExtender.Show();
    //                }
    //            }
    //        }
    //    }

    //}


    #region Report
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string appraisalEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            ViewState["APPRAISAL_EMPLOYEE_ID"] = appraisalEmp_Id;
       

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("IQAC")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("EMP_APPRAISAL")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=SelfAppraisal.pdf";
            url += "&path=~,Reports,Emp_Appraisal," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_EMPLOYEE_ID=" + Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    #endregion

    #region Print proforma
    //protected void LinkButton_Print_Click(object sender, EventArgs e)
    //{

    //    string iqacEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
    //    if (iqacEmp_Id != "")
    //    {
    //        ModalPopupExtender1.Show();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Fill  Appraisal Detail .');", true);
    //    }

    //}

    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("APPRAISAL")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=APAR_Proforma.pdf";
    //        url += "&path=~,Reports,APPRAISAL," + rptFileName;
    //        url += "&param=@P_EMP_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    //}

    //protected void btnokyes_Click(object sender, EventArgs e)
    //{
    //    trSession.Visible = true;

    //    if (ddlEmployee.SelectedIndex >= 0 && ddlSession.SelectedIndex >= 0)
    //    {
    //        ShowReport("APAR Fill By Faculty Members", "rptAPAR_Proforma.rpt");

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Employee & Session.');", true);
    //        return;
    //    }
    //}

    #endregion

    protected void btnAcademicDutiesSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtapi1.Text != "")
            {
                if (Convert.ToDouble(txtapi1.Text) > 10)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Lectures And Academic Duties In Excess Of UGC Norms  should not be greater than 10 .');", true);
                    txtMaterialApiScore.Focus();
                    return;
                }
            }

            if (txtMaterialApiScore.Text != "")
            {
                if (Convert.ToInt32(txtMaterialApiScore.Text) > 20)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Preparation of study material and resources  should not be greater than 20 .');", true);
                    txtMaterialApiScore.Focus();
                    return;
                }
            }



            string ApprcEmp_Id = objCommon.LookUp("APPRAISAL_EMPLOYEE", "APPRAISAL_EMPLOYEE_ID", "EMPLOYEE_ID=" + Convert.ToInt32(ddlEmployee.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));

            if (ApprcEmp_Id != "")
            {

                objEA.EMPLOYEE_ID = Convert.ToInt32(ddlEmployee.SelectedValue);
                objEA.SESSION_ID = Convert.ToInt32(ddlSession.SelectedValue);

                #region
                // First Add All Column Names.
                Session["dtLandA"] = null;
                DataTable dtLandA = new DataTable();
                if (Session["dtLandA"] != null)
                    dtLandA = (DataTable)Session["dtLandA"];
                if (!(dtLandA.Columns.Contains("SN_ID")))
                    dtLandA.Columns.Add("SN_ID");
                if (!(dtLandA.Columns.Contains("TYPE_OF_ACTIVITY")))
                    dtLandA.Columns.Add("TYPE_OF_ACTIVITY");
                if (!(dtLandA.Columns.Contains("NO_OF_STUDENTS_BENIFITED")))
                    dtLandA.Columns.Add("NO_OF_STUDENTS_BENIFITED");
                if (!(dtLandA.Columns.Contains("NO_OF_HRS_ENGAGED_FOR_THE_ACTIVITY")))
                    dtLandA.Columns.Add("NO_OF_HRS_ENGAGED_FOR_THE_ACTIVITY");

                //find column
                foreach (ListViewItem itemRow in lvAcadDuties.Items)
                {
                    Label lblSN = (Label)itemRow.FindControl("lblSN") as Label;
                    Label lblActivity = (Label)itemRow.FindControl("lblActivity") as Label;
                    TextBox txtStudBenefited = (TextBox)itemRow.FindControl("txtStudBenefited") as TextBox;
                    TextBox txtHrsEngaged = (TextBox)itemRow.FindControl("txtHrsEngaged") as TextBox;

                    DataRow drLandA = dtLandA.NewRow();
                    drLandA["SN_ID"] = Convert.ToInt32(lblSN.Text);

                    drLandA["TYPE_OF_ACTIVITY"] = (lblActivity.Text);


                    if (txtStudBenefited.Text == "")
                    {
                        drLandA["NO_OF_STUDENTS_BENIFITED"] = 0;
                    }
                    else
                    {
                       
                        drLandA["NO_OF_STUDENTS_BENIFITED"] = Convert.ToInt32(txtStudBenefited.Text);
                    }

                    if (txtHrsEngaged.Text == "")
                    {
                        drLandA["NO_OF_HRS_ENGAGED_FOR_THE_ACTIVITY"] = 0;
                    }
                    else
                    {
                        drLandA["NO_OF_HRS_ENGAGED_FOR_THE_ACTIVITY"] = Convert.ToInt32(txtHrsEngaged.Text);
                    }
                    //insert row in datatable
                    dtLandA.Rows.Add(drLandA);

                }
                #endregion


                #region
                // First Add All Column Names.
                Session["dtstudymaterial"] = null;
                DataTable dtstudymaterial = new DataTable();
                if (Session["dtstudymaterial"] != null)
                    dtstudymaterial = (DataTable)Session["dtLandA"];
                if (!(dtstudymaterial.Columns.Contains("SN_ID")))
                    dtstudymaterial.Columns.Add("SN_ID");
                if (!(dtstudymaterial.Columns.Contains("STUDY_MATERIAL")))
                    dtstudymaterial.Columns.Add("STUDY_MATERIAL");
                if (!(dtstudymaterial.Columns.Contains("API_SCORED_CLAIMED")))
                    dtstudymaterial.Columns.Add("API_SCORED_CLAIMED");
                if (!(dtstudymaterial.Columns.Contains("VERIFIED_API_SCORE")))
                    dtstudymaterial.Columns.Add("VERIFIED_API_SCORE");

                //find column
                foreach (ListViewItem itemRow in lvResources.Items)
                {
                    Label lblSNID = (Label)itemRow.FindControl("lblSNID") as Label;
                    Label lblCourse = (Label)itemRow.FindControl("lblCourse") as Label;
                    TextBox txtApiScore = (TextBox)itemRow.FindControl("txtApiScore") as TextBox;
                    TextBox txtVerifiedApi = (TextBox)itemRow.FindControl("txtVerifiedApi") as TextBox;

                    DataRow drdtstudymaterial = dtstudymaterial.NewRow();
                    drdtstudymaterial["SN_ID"] = Convert.ToInt32(lblSNID.Text);
                    drdtstudymaterial["STUDY_MATERIAL"] = (lblCourse.Text);


                    if (txtApiScore.Text == "")
                    {
                        drdtstudymaterial["API_SCORED_CLAIMED"] = 0;
                    }
                    else
                    {

                        drdtstudymaterial["API_SCORED_CLAIMED"] = Convert.ToInt32(txtApiScore.Text);
                    }

                    if (txtVerifiedApi.Text == "")
                    {
                        drdtstudymaterial["VERIFIED_API_SCORE"] = 0;
                    }
                    else
                    {

                        drdtstudymaterial["VERIFIED_API_SCORE"] = Convert.ToInt32(txtVerifiedApi.Text);
                    }
                    //insert row in datatable


                    dtstudymaterial.Rows.Add(drdtstudymaterial);

                }


                #endregion

                //objEA.EXAMINATION_WORK=dtExamWork;
                objEA.LECTURE_AND_ADUTIES = dtLandA;
                objEA.STUDY_MATERIAL_RESOURSE = dtstudymaterial;

                objEA.API_Score_Claimed = txtapi1.Text;

                if (txtMaterialApiScore.Text == "")
                {
                    objEA.API_SCORE_CLAIME_STUDY = 0;
                }
                else
                {
                    objEA.API_SCORE_CLAIME_STUDY = Convert.ToInt32(txtMaterialApiScore.Text);
                }
                if (txtUGCVerifiedapi.Text == "")
                {
                    objEA.Verified_API_Score = 0;
                }
                else
                {
                    objEA.Verified_API_Score =  Convert.ToInt32(txtUGCVerifiedapi.Text);
                }
                
                if (txtMaterialVerifiedApi.Text == "")
                {
                    objEA.API_SCORE_VERIFIED_STUDY = 0;
                }
                else
                {
                    objEA.API_SCORE_VERIFIED_STUDY = Convert.ToInt32(txtMaterialVerifiedApi.Text);
                }





                if (ViewState["APPRAISAL_EMPLOYEE_ID"] == "")
                {
                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateLectureAndAcademicDuties(objEA);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    }
                }
                else
                {
                    objEA.APPRAISAL_EMPLOYEE_ID = Convert.ToInt32(ViewState["APPRAISAL_EMPLOYEE_ID"].ToString());

                    CustomStatus cs = (CustomStatus)objAPPRController.AddUpdateLectureAndAcademicDuties(objEA);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please save first Personal Info.');", true);
                MultiView_Profile.ActiveViewIndex = -1;
                MultiView_Profile.SetActiveView(Innovative_Teaching);
                trSession.Visible = true;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnInnovativeSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }






    //protected void lvEngagingLectures_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
          
     
    //    TextBox txtHrsTargeted = e.Item.FindControl("txtHrsTargeted") as TextBox;
    //    TextBox txtHrsEngaged = e.Item.FindControl("txtHrsEngaged") as TextBox;
    //    TextBox txtPercentTarget = e.Item.FindControl("txtPercentTarget") as TextBox;

    //    txtHrsEngaged.Text = txtHrsTargeted.Text;

    //}
    //protected void txtHrsEngaged_TextChanged(object sender, EventArgs e)
    //{
    //    int achive = 0;
    //    int target = 0;
    //    int avg = 0;
    //    foreach (ListViewDataItem item in lvEngagingLectures.Items)
    //    {

    //        TextBox txtHrsTargeted = item.FindControl("txtHrsTargeted") as TextBox;
    //        TextBox txtHrsEngaged = item.FindControl("txtHrsEngaged") as TextBox;
    //        TextBox txtPercentTarget = item.FindControl("txtPercentTarget") as TextBox;

           

    //        if (txtHrsTargeted.Text == "")
    //        {
    //             target =0;
    //        }
    //        else
    //        {
    //             target = Convert.ToInt32(txtHrsTargeted.Text);
    //        }
    //        if (txtHrsEngaged.Text == "")
    //        {
    //             achive = 0;
    //        }
    //        else
    //        {
    //             achive = Convert.ToInt32(txtHrsEngaged.Text);
    //        }
           


    //         //avg = (achive / target);


    //         txtPercentTarget.Text = "100";

    //    }
    //}
    protected void clearOtherActivity()
    {
        txtactivity1.Text = string.Empty;
        txtapial.Text = string.Empty;
        //  txtapicl.Text = string.Empty;
    }   

    private DataTable CreateTabelOtherActivity()
    {
        DataTable dtotheractivity = new DataTable();
        dtotheractivity.Columns.Add(new DataColumn("SN_ID", typeof(int)));
        dtotheractivity.Columns.Add(new DataColumn("NameOfActivity", typeof(string)));
        dtotheractivity.Columns.Add(new DataColumn("ApiScoreAllotted", typeof(string)));
        dtotheractivity.Columns.Add(new DataColumn("ApiScoreClaime", typeof(string)));
        dtotheractivity.Columns.Add(new DataColumn("SR_NO", typeof(int)));
        dtotheractivity.Columns.Add(new DataColumn("VERIFIED_API_SCORE", typeof(int)));

        return dtotheractivity;
    }


    protected void btnaddotheractivity_Click(object sender, EventArgs e)
    {

        if (txtactivity1.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Name Of Activity.');", true);
            return;
        }

        if (txtapial.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter API Score Allotted.');", true);
            return;
        }

        lvotheractivity.Visible = true;
        if (Session["RecTblOtherActivity"] != null && ((DataTable)Session["RecTblOtherActivity"]) != null)
        {
            int maxVals = 0;
            DataTable dtotheractivity = (DataTable)Session["RecTblOtherActivity"];
            DataRow dr = dtotheractivity.NewRow();
            if (dr != null)
            {
                maxVals = Convert.ToInt32(dtotheractivity.AsEnumerable().Max(row => row["SN_ID"]));
            }

            dr["SN_ID"] = maxVals + 1;
            dr["NameOfActivity"] = txtactivity1.Text.Trim() == null ? string.Empty : Convert.ToString(txtactivity1.Text.Trim());
            dr["ApiScoreAllotted"] = txtapial.Text.Trim() == null ? string.Empty : Convert.ToString(txtapial.Text.Trim());
            //dr["ApiScoreClaime"] = txtapiscore.Text.Trim() == null ? string.Empty : Convert.ToString(txtapiscore.Text.Trim());



            dr["SR_NO"] = 0;

            dtotheractivity.Rows.Add(dr);
            Session["RecTblOtherActivity"] = dtotheractivity;
            lvotheractivity.DataSource = dtotheractivity;
            lvotheractivity.DataBind();
            clearOtherActivity();

            ViewState["SRNO_Performance"] = Convert.ToInt32(ViewState["SRNO_Performance"]) + 1;
        }
        else
        {

            DataTable dtotheractivity = this.CreateTabelOtherActivity();
            DataRow dr = dtotheractivity.NewRow();
            dr["SN_ID"] = Convert.ToInt32(ViewState["SN_ID"]) + 1;
            dr["NameOfActivity"] = txtactivity1.Text.Trim() == null ? string.Empty : Convert.ToString(txtactivity1.Text.Trim());
            dr["ApiScoreAllotted"] = txtapial.Text.Trim() == null ? string.Empty : Convert.ToString(txtapial.Text.Trim());
            // dr["ApiScoreClaime"] = txtapicl.Text.Trim() == null ? string.Empty : Convert.ToString(txtapicl.Text.Trim());
            dr["SR_NO"] = 0;



            ViewState["SN_ID"] = Convert.ToInt32(ViewState["SN_ID"]) + 1;
            dtotheractivity.Rows.Add(dr);
            clearOtherActivity();
            Session["RecTblOtherActivity"] = dtotheractivity;
            lvotheractivity.DataSource = dtotheractivity;
            lvotheractivity.DataBind();
        }
    }

    private DataRow GetEditableDatarowotheractivity(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SN_ID"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.GetEditableDatarowFromStudentAttendance -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void btneditOtherActivity_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btneditOtherActivity = sender as ImageButton;
        DataTable dtotheractivity;

        dtotheractivity = ((DataTable)Session["RecTblOtherActivity"]);

        DataRow dr = this.GetEditableDatarowotheractivity(dtotheractivity, btneditOtherActivity.CommandArgument);


        dtotheractivity.Rows.Remove(dr);
        Session["RecTblOtherActivity"] = dtotheractivity;
        lvotheractivity.DataSource = dtotheractivity;
        lvotheractivity.DataBind();
    }
}
