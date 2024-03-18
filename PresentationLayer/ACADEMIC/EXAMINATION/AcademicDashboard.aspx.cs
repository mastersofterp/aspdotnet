using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using EASendMail;

public partial class ACADEMIC_EXAMINATION_AcademicDashboard : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    AcademicDashboardEntity objADEntity = new AcademicDashboardEntity();
    AcademinDashboardController objADEController = new AcademinDashboardController();

    DataSet dsExamRegistrationDept = null;
    DataSet dsExamRegistrationScheme = null;
    DataSet dsExamRegistrationAll = null;

    static DataSet dsExamRegistrationPending = null;
    static DataSet dsResultProcessingPending = null;

    public static int ExamRegistrationDept = 0;
    public static int ExamSchemeBindStatus = 0;
    public static int ExamSchemeWiseAllStatus = 0;
    public static int ExamRegistrationPendingStatus = 0;
    public static int ResultProcessingPendingStatus = 0;

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
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            PopulateDropDownList();
        }
    }

    #region -----------User Define Function-----------------

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AcademicDashboard.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AcademicDashboard.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //DataSet ds = objADEController.Get_College_Session(2, Session["college_nos"].ToString());
            //ddlSession.Items.Clear();
            //ddlSession.Items.Add("Please Select");
            //ddlSession.SelectedItem.Value = "0";

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    ddlSession.DataSource = ds;
            //    ddlSession.DataValueField = ds.Tables[0].Columns[0].ToString();
            //    ddlSession.DataTextField = ds.Tables[0].Columns[4].ToString();
            //    ddlSession.DataBind();
            //    ddlSession.SelectedIndex = 0;
            //}
            objCommon.FillDropDownList(ddlexamSession, "ACD_SESSION", "SESSIONID", "SESSION_NAME", "SESSIONID>0 AND ISNULL(FLOCK,0)=1", "SESSIONID");

            //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC"); //ISNULL(FLOCK,0)=1
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindStatusBarDetail()
    {
        string evenOdd = string.Empty;

        int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
        if (ODDEVEN == 1)
        {
            evenOdd = "Odd Semester";
        }
        else if (ODDEVEN == 2)
        {
            evenOdd = "Even Semester";
        }

        string semester = string.Empty;//"1,3,5,7";//string.Empty;
        semester = hdnsemesterno.Value;
        ViewState["semester"] = hdnsemesterno.Value;

        divProgressbar.Visible = true;

        #region---------Course Registration-----------------

        objADEntity.Dashboard_Type = 1;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsCourseReg = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, semester);
        if (dsCourseReg.Tables.Count > 0 && dsCourseReg.Tables[0].Rows.Count > 0)
        {
            lblCourseRegIncompleteStatus.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsCourseReg.Tables[0].Rows[0]["COURSE_REG_PENDING_COUNT"]) + "<span>";
            lblCourseRegFullStatus.Text = "Total Course Registration " + Convert.ToString(dsCourseReg.Tables[0].Rows[0]["COURSE_REG_COMPLETE_COUNT"]) + " Out of " + Convert.ToString(dsCourseReg.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        }
        double ProgessPer = 0;
        if (Convert.ToDouble(dsCourseReg.Tables[0].Rows[0]["TOTAL_COUNT"]) > 0)
        {
            ProgessPer = (Convert.ToDouble(dsCourseReg.Tables[0].Rows[0]["COURSE_REG_COMPLETE_COUNT"]) * 100) / Convert.ToDouble(dsCourseReg.Tables[0].Rows[0]["TOTAL_COUNT"]);
        }

        if (ProgessPer > 0 && ProgessPer <= 17)
            ProgressCourseRegistration.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPer > 17 && ProgessPer <= 51)
            ProgressCourseRegistration.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPer > 51 && ProgessPer <= 92)
            ProgressCourseRegistration.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPer > 92)
            ProgressCourseRegistration.Attributes["class"] = "progress-bar progress-bar-success";



        lblCourseRegPercentage.Text = Convert.ToString(Math.Round((Double)ProgessPer, 2)) + "%";
        ProgessPer = Math.Round((Double)ProgessPer, 2);

        ProgressCourseRegistration.Style.Add("width", ProgessPer + "%");

        //ClientScript.RegisterStartupScript(this.GetType(), "updateCourseRegistrationProgress", "updateCourseRegistrationProgress('" + ProgessPer + "');", true);

        #endregion---------Course Registration-----------------

        #region---------Teacher Allotment-----------------

        objADEntity.Dashboard_Type = 2;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsTeacherAllotment = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, ViewState["semester"].ToString());
        lblIncompleteTeacherAllotment.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsTeacherAllotment.Tables[0].Rows[0]["PENDING_COUNT"]) + "<span>";
        lblTeacharAllotmentFullStatus.Text = "Total Teacher Allotment " + Convert.ToString(dsTeacherAllotment.Tables[0].Rows[0]["COMPLETED_COURSE_COUNT"]) + " Out of " + Convert.ToString(dsTeacherAllotment.Tables[0].Rows[0]["TOTAL_COURSE_COUNT"]) + " For " + evenOdd;
        double ProgessPerTeacherAllotment = 0;
        if (Convert.ToDouble(dsTeacherAllotment.Tables[0].Rows[0]["TOTAL_COURSE_COUNT"]) != 0)
        {
            ProgessPerTeacherAllotment = (Convert.ToDouble(dsTeacherAllotment.Tables[0].Rows[0]["COMPLETED_COURSE_COUNT"]) * 100) / Convert.ToDouble(dsTeacherAllotment.Tables[0].Rows[0]["TOTAL_COURSE_COUNT"]);
        }
        if (ProgessPerTeacherAllotment > 0 && ProgessPerTeacherAllotment <= 17)
            progressTeacherAllotment.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPerTeacherAllotment > 17 && ProgessPerTeacherAllotment <= 51)
            progressTeacherAllotment.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPerTeacherAllotment > 51 && ProgessPerTeacherAllotment <= 92)
            progressTeacherAllotment.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPerTeacherAllotment > 92)
            progressTeacherAllotment.Attributes["class"] = "progress-bar progress-bar-success";

        lblTeacherAllotmentPercentage.Text = Convert.ToString(Math.Round((Double)ProgessPerTeacherAllotment, 2)) + "%";

        ProgessPerTeacherAllotment = Math.Round((Double)ProgessPerTeacherAllotment, 2);

        progressTeacherAllotment.Style.Add("width", ProgessPerTeacherAllotment + "%");

        #endregion---------Teacher Allotment-----------------

        #region--------- Class Time Table---------

        objADEntity.Dashboard_Type = 3;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsClassTimeTable = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, ViewState["semester"].ToString());
        lblIncompleteClassTimeTable.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsClassTimeTable.Tables[0].Rows[0]["PENDING_COUNT"]) + "<span>";
        lblClassTimeTableFullStatus.Text = "Total Class Time Table Define " + Convert.ToString(dsClassTimeTable.Tables[0].Rows[0]["COMPLETE_COUNT"]) + " Out of " + Convert.ToString(dsClassTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        double ProgessPerdsClassTimeTable = 0;
        if (Convert.ToDouble(dsClassTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]) != 0)
        {
            ProgessPerdsClassTimeTable = (Convert.ToDouble(dsClassTimeTable.Tables[0].Rows[0]["COMPLETE_COUNT"]) * 100) / Convert.ToDouble(dsClassTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]);
        }
        if (ProgessPerdsClassTimeTable > 0 && ProgessPerdsClassTimeTable <= 17)
            progressClassTimeTable.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPerdsClassTimeTable > 17 && ProgessPerdsClassTimeTable <= 51)
            progressClassTimeTable.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPerdsClassTimeTable > 51 && ProgessPerdsClassTimeTable <= 92)
            progressClassTimeTable.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPerdsClassTimeTable > 92)
            progressClassTimeTable.Attributes["class"] = "progress-bar progress-bar-success";

        lblClassTimeTablePercentage.Text = Convert.ToString(Math.Round((Double)ProgessPerdsClassTimeTable, 2)) + "%";

        ProgessPerdsClassTimeTable = Math.Round((Double)ProgessPerdsClassTimeTable, 2);

        progressClassTimeTable.Style.Add("width", ProgessPerdsClassTimeTable + "%");

        #endregion-------- Class Time Table-------------

        #region-----------Exam Time Table-----------------

        objADEntity.Dashboard_Type = 4;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsExamTimeTable = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, ViewState["semester"].ToString());
        lblIncompleteExamTimeTable.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsExamTimeTable.Tables[0].Rows[0]["PENDDING_COUNT"]) + "<span>";
        lblExamTimeTableFullStatus.Text = "Total Exam Time Table Define " + Convert.ToString(dsExamTimeTable.Tables[0].Rows[0]["COMPLETE_COUNT"]) + " Out of " + Convert.ToString(dsExamTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        double ProgessPerExamTimeTable = 0;
        if (Convert.ToDouble(dsExamTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]) != 0)
        {
            ProgessPerExamTimeTable = (Convert.ToDouble(dsExamTimeTable.Tables[0].Rows[0]["COMPLETE_COUNT"]) * 100) / Convert.ToDouble(dsExamTimeTable.Tables[0].Rows[0]["TOTAL_COUNT"]);
        }
        if (ProgessPerExamTimeTable > 0 && ProgessPerExamTimeTable <= 17)
            progressExamTimeTable.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPerExamTimeTable > 17 && ProgessPerExamTimeTable <= 51)
            progressExamTimeTable.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPerExamTimeTable > 51 && ProgessPerExamTimeTable <= 92)
            progressExamTimeTable.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPerExamTimeTable > 92)
            progressExamTimeTable.Attributes["class"] = "progress-bar progress-bar-success";

        lblExamTimeTablePercentage.Text = Convert.ToString(Math.Round((Double)ProgessPerExamTimeTable, 2)) + "%";

        ProgessPerExamTimeTable = Math.Round((Double)ProgessPerExamTimeTable, 2);

        progressExamTimeTable.Style.Add("width", ProgessPerExamTimeTable + "%");

        #endregion ----------Exam Time Table -------------

        #region ------------Exam Registration Status-----------

        objADEntity.Dashboard_Type = 5;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsExamRegistration = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, ViewState["semester"].ToString());
        lblIncompleteExamRegistration.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["COURSE_EXAM_REG_PENDING_COUNT"]) + "<span>";
        lblExamRegistrationFullStatus.Text = "Total Exam Registration " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["COURSE_EXAM_REG_COMPLETE_COUNT"]) + " Out of " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;

        //Form Fill
        //lblIncompleteExamRegistrationFormFill.Text = "Total Incomplete :" + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["EXAM_FORM_FILLUP_PENDING_COUNT"]);
        //lblExamRegistrationFormFillFullStatus.Text = "Total Form Fill " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["EXAM_FORM_FILLUP_COUNT"]) + " Out of " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        ////Payment
        //lblIncompleteExamRegistrationPayment.Text = "Total Incomplete :" + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["PAYMENT_CONFIRMED_PENDING_COUNT"]);
        //lblExamRegistrationPaymentFullStatus.Text = "Total Payment " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["PAYMENT_CONFIRMED_COUNT"]) + " Out of " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        ////HOD Approval
        //lblIncompleteExamRegistrationHODApproval.Text = "Total Incomplete :" + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["HOD_APPROVAL_PENDING_COUNT"]);
        //lblExamRegistrationHODApprovalFullStatus.Text = "Total HOD Approval " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["HOD_APPROVAL_COUNT"]) + " Out of " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        ////Admit Card Download
        //lblIncompleteExamRegistrationHODApproval.Text = "Total Incomplete :" + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["ADMIT_CARD_DOWNLOAD_PENDING_COUNT"]);
        //lblExamRegistrationHODApprovalFullStatus.Text = "Total Admit Card Download " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["ADMIT_CARD_DOWNLOAD_COUNT"]) + " Out of " + Convert.ToString(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;

        double ProgessPerExamRegistration = 0;
        if (Convert.ToDouble(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]) != 0)
        {
            ProgessPerExamRegistration = (Convert.ToDouble(dsExamRegistration.Tables[0].Rows[0]["COURSE_EXAM_REG_COMPLETE_COUNT"]) * 100) / Convert.ToDouble(dsExamRegistration.Tables[0].Rows[0]["TOTAL_COUNT"]);
        }
        if (ProgessPerExamRegistration > 0 && ProgessPerExamRegistration <= 17)
            progressExamRegistration.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPerExamRegistration > 17 && ProgessPerExamRegistration <= 51)
            progressExamRegistration.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPerExamRegistration > 51 && ProgessPerExamRegistration <= 92)
            progressExamRegistration.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPerExamRegistration > 92)
            progressExamRegistration.Attributes["class"] = "progress-bar progress-bar-success";

        lblExamRegistrationPercentage.Text = Convert.ToString(Math.Round((Double)ProgessPerExamRegistration, 2)) + "%";

        ProgessPerExamRegistration = Math.Round((Double)ProgessPerExamRegistration, 2);

        progressExamRegistration.Style.Add("width", ProgessPerExamRegistration + "%");

        #endregion --------Exam Registration Status------------

        #region ---------Result Processing Status----------

        objADEntity.Dashboard_Type = 6;
        objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

        DataSet dsResultProcessing = objADEController.GetAcademicDashboardPrograssbarDetail(objADEntity, ViewState["semester"].ToString());
        lblIncompleteResultprocess.Text = "Total Incomplete : " + "<span class='labeltext'>" + Convert.ToString(dsResultProcessing.Tables[0].Rows[0]["PENDING_COUNT"]) + "<span>";
        lblResultProcessFullStatus.Text = "Total Result Processing " + Convert.ToString(dsResultProcessing.Tables[0].Rows[0]["COMPLETE_COUNT"]) + " Out of " + Convert.ToString(dsResultProcessing.Tables[0].Rows[0]["TOTAL_COUNT"]) + " For " + evenOdd;
        double ProgessPerEResultProcessing = 0;
        if (Convert.ToInt32(dsResultProcessing.Tables[0].Rows[0]["TOTAL_COUNT"]) != 0)
            ProgessPerEResultProcessing = (Convert.ToDouble(dsResultProcessing.Tables[0].Rows[0]["COMPLETE_COUNT"]) * 100) / Convert.ToDouble(dsResultProcessing.Tables[0].Rows[0]["TOTAL_COUNT"]);

        if (ProgessPerEResultProcessing > 0 && ProgessPerEResultProcessing <= 17)
            progressResultProcessing.Attributes["class"] = "progress-bar progress-bar-warning";
        else if (ProgessPerEResultProcessing > 17 && ProgessPerEResultProcessing <= 51)
            progressResultProcessing.Attributes["class"] = "progress-bar progress-bar-danger";
        else if (ProgessPerEResultProcessing > 51 && ProgessPerEResultProcessing <= 92)
            progressResultProcessing.Attributes["class"] = "progress-bar progress-bar-info";
        else if (ProgessPerEResultProcessing > 92)
            progressResultProcessing.Attributes["class"] = "progress-bar progress-bar-success";

        lblResultProcessPercentage.Text = Convert.ToString(Math.Round((Double)ProgessPerEResultProcessing, 2)) + "%";

        ProgessPerEResultProcessing = Math.Round((Double)ProgessPerEResultProcessing, 2);

        progressResultProcessing.Style.Add("width", ProgessPerEResultProcessing + "%");

        #endregion -----Result Processing Status-----------
    }

    #endregion---------User Define Function-----------------

    #region -- Ajax Toolkit Tab Container-------

    protected void tabContainerAcademicDashboard_ActiveTabChanged(object sender, EventArgs e)
    {
        Session["EmailCategory"] = string.Empty;

        divCourseRegistrationProgress.Visible = false;
        divHTeacherAllotmentStatus.Visible = false;
        divHClassTimeTableStatus.Visible = false;
        divHExamTimeTableStatus.Visible = false;
        divHExamRegistrationStatus.Visible = false;
        divHResultProcessingStatus.Visible = false;


        if (ddlSession.SelectedIndex > 0)
        {
            if (tabContainerAcademicDashboard.ActiveTabIndex == 0)  // Course Registration
            {
                #region ---------- Course Registration-----------------

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                //objADEntity.Dashboard_Type = 1;
                objADEntity.Dashboard_Type = 17;

                divAcademicDashboard.Visible = true;

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divCourseRegistrationProgress.Visible = true;
                    gvParent.DataSource = dr;
                    gvParent.DataBind();
                }
                else
                {
                    gvParent.DataSource = null;
                    gvParent.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion ---------- Course Registration-----------------
            }
            else if (tabContainerAcademicDashboard.ActiveTabIndex == 1) // Teacher Allotment Status
            {
                #region --------------Teacher Allotment Status -----------------

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                objADEntity.Dashboard_Type = 3;

                //DataSet ds_TeacherAllotement = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divHTeacherAllotmentStatus.Visible = true;

                    gvParent_TeacherAllotment.DataSource = dr; //ds_TeacherAllotement;
                    gvParent_TeacherAllotment.DataBind();
                }
                else
                {
                    gvParent_TeacherAllotment.DataSource = null;
                    gvParent_TeacherAllotment.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion--------------Teacher Allotment Status -----------------
            }
            else if (tabContainerAcademicDashboard.ActiveTabIndex == 2) // Class Time Table Status
            {
                #region----------Class Time Table Status -----------------

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                objADEntity.Dashboard_Type = 6;

                //DataSet ds_ClassTimeTable = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divHClassTimeTableStatus.Visible = true;

                    gvParent_ClassTimeTable.DataSource = dr;// ds_ClassTimeTable;
                    gvParent_ClassTimeTable.DataBind();
                }
                else
                {
                    gvParent_ClassTimeTable.DataSource = null;
                    gvParent_ClassTimeTable.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion----------Class Time Table Status -----------------
            }
            else if (tabContainerAcademicDashboard.ActiveTabIndex == 3) // Exam Time Table Status
            {
                #region----------Exam Time Table Status -----------------

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                objADEntity.Dashboard_Type = 8;

                //DataSet ds_ExamTimeTable = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divHExamTimeTableStatus.Visible = true;

                    gvParent_ExamTimeTable.DataSource = dr;// ds_ExamTimeTable;
                    gvParent_ExamTimeTable.DataBind();
                }
                else
                {
                    gvParent_ExamTimeTable.DataSource = null;
                    gvParent_ExamTimeTable.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion----------Exam Time Table Status -----------------
            }
            else if (tabContainerAcademicDashboard.ActiveTabIndex == 4) // Exam Registration Status
            {
                #region----------Exam Registration Status -----------------

                ExamRegistrationDept = 0;
                ExamSchemeBindStatus = 0;
                ExamSchemeWiseAllStatus = 0;
                ExamRegistrationPendingStatus = 0;

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                objADEntity.Dashboard_Type = 15;

                // DataSet ds_ExamRegistration = objADEController.GetAcademicDashboardDetail(objADEntity);

                SqlDataReader dr_ExamRegistration = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr_ExamRegistration != null)
                {
                    divHExamRegistrationStatus.Visible = true;

                    gvParent_ExamRegistration.DataSource = dr_ExamRegistration;
                    gvParent_ExamRegistration.DataBind();
                }
                else
                {
                    gvParent_ExamRegistration.DataSource = null;
                    gvParent_ExamRegistration.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion----------Exam Registration Status -----------------
            }
            else if (tabContainerAcademicDashboard.ActiveTabIndex == 5) // Result Processing Status
            {
                #region----------Result Processing Status -----------------

                ResultProcessingPendingStatus = 0;

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);

                objADEntity.Dashboard_Type = 16;

                //DataSet ds_ResultProcessing = objADEController.GetAcademicDashboardDetail(objADEntity);

                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divHResultProcessingStatus.Visible = true;
                    gvParent_ResultProcessing.DataSource = dr;
                    gvParent_ResultProcessing.DataBind();
                }
                else
                {
                    gvParent_ResultProcessing.DataSource = null;
                    gvParent_ResultProcessing.DataBind();
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                }

                #endregion----------Result Processing Status -----------------
            }
        }
        else
        {
            gvParent.DataSource = null;
            gvParent.DataBind();

            gvParent_TeacherAllotment.DataSource = null;
            gvParent_TeacherAllotment.DataBind();

            gvParent_ClassTimeTable.DataSource = null;
            gvParent_ClassTimeTable.DataBind();

            gvParent_ExamTimeTable.DataSource = null;
            gvParent_ExamTimeTable.DataBind();

            gvParent_ExamRegistration.DataSource = null;
            gvParent_ExamRegistration.DataBind();

            //gvChild_ExamRegistration.DataSource = null;
            //gvChild_ExamRegistration.DataBind();

            gvParent_ResultProcessing.DataSource = null;
            gvParent_ResultProcessing.DataBind();
        }
        //Session End Condition

    }

    #endregion --Ajax Toolkit Tab Container----

    #region ------------Controll Event---------------

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        if (hdnsemesterno.Value != string.Empty && hdnsemesterno.Value != null && hdnsemesterno.Value != "")
        {
            string sem = string.Empty;

            string semname = string.Empty;
            string[] values = hdnsemesterno.Value.Split(',');
            for (int i = 0; i < values.Length - 1; i++)
            {
                values[i] = values[i].Trim();
                semname = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + Convert.ToInt32(values[i]));
                sem = sem + semname + ",";
            }
            lblmag.Text = "Note : " + sem.Remove(sem.Length - 1) + " Semester data is display.";
        }

        try
        {
            Session["EmailCategory"] = string.Empty;
            Session["ODDEVEN"] = 0;
            ExamRegistrationDept = 0;
            ExamSchemeBindStatus = 0;
            ExamSchemeWiseAllStatus = 0;

            if (ddlSession.SelectedIndex > 0)
            {
                Session["ODDEVEN"] = objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue);

                divCourseRegistrationProgress.Visible = false;
                divHTeacherAllotmentStatus.Visible = false;
                divHClassTimeTableStatus.Visible = false;
                divHExamTimeTableStatus.Visible = false;
                divHExamRegistrationStatus.Visible = false;
                divHResultProcessingStatus.Visible = false;

                //Progress bar
                BindStatusBarDetail();

                #region ---------- Course Registration-----------------

                objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
                // objADEntity.Dashboard_Type = 1;
                objADEntity.Dashboard_Type = 17;


                divAcademicDashboard.Visible = true;

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    divCourseRegistrationProgress.Visible = true;

                    gvParent.DataSource = dr;
                    gvParent.DataBind();

                    tabContainerAcademicDashboard.ActiveTabIndex = 0;

                    gvParent_TeacherAllotment.DataSource = null;
                    gvParent_TeacherAllotment.DataBind();

                    gvParent_ClassTimeTable.DataSource = null;
                    gvParent_ClassTimeTable.DataBind();

                    gvParent_ExamTimeTable.DataSource = null;
                    gvParent_ExamTimeTable.DataBind();

                    gvParent_ResultProcessing.DataSource = null;
                    gvParent_ResultProcessing.DataBind();
                }
                else
                {
                    gvParent.DataSource = null;
                    gvParent.DataBind();


                    gvParent_TeacherAllotment.DataSource = null;
                    gvParent_TeacherAllotment.DataBind();

                    gvParent_ClassTimeTable.DataSource = null;
                    gvParent_ClassTimeTable.DataBind();

                    gvParent_ExamTimeTable.DataSource = null;
                    gvParent_ExamTimeTable.DataBind();

                    gvParent_ResultProcessing.DataSource = null;
                    gvParent_ResultProcessing.DataBind();
                }
                #endregion ---------- Course Registration-----------------
            }
            else
            {
                gvParent.DataSource = null;
                gvParent.DataBind();

                gvParent_TeacherAllotment.DataSource = null;
                gvParent_TeacherAllotment.DataBind();

                gvParent_ClassTimeTable.DataSource = null;
                gvParent_ClassTimeTable.DataBind();

                gvParent_ExamTimeTable.DataSource = null;
                gvParent_ExamTimeTable.DataBind();

                gvParent_ResultProcessing.DataSource = null;
                gvParent_ResultProcessing.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    #region------------Course Registration-----------------

    protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 1;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChildCourseRegistrationDepartment");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;//ds;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChildCourseRegistrationDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 2;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                // DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;
                    gvChildGrid.DataBind();


                    //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChildGrid.Columns[2].Visible = true;
                        gvChildGrid.Columns[3].Visible = true;
                        gvChildGrid.Columns[4].Visible = true;
                        gvChildGrid.Columns[5].Visible = true;
                        gvChildGrid.Columns[6].Visible = false;
                        gvChildGrid.Columns[7].Visible = false;
                        gvChildGrid.Columns[8].Visible = false;
                        gvChildGrid.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChildGrid.Columns[2].Visible = false;
                        gvChildGrid.Columns[3].Visible = false;
                        gvChildGrid.Columns[4].Visible = false;
                        gvChildGrid.Columns[5].Visible = false;
                        gvChildGrid.Columns[6].Visible = true;
                        gvChildGrid.Columns[7].Visible = true;
                        gvChildGrid.Columns[8].Visible = true;
                        gvChildGrid.Columns[9].Visible = true;
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 18;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_StudentList");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
                objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;//ds;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvChild_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion---------Course Registration-----------------

    #region--------------Teacher Allotment Status -----------------

    protected void gvParent_TeacherAllotment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 4;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_TeacherAllotment");

                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                //DataTable dt = new DataTable();
                //dt.Load(dr);
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;// ds;
                    gvChildGrid.DataBind();


                    //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChildGrid.Columns[2].Visible = true;
                        gvChildGrid.Columns[3].Visible = true;
                        gvChildGrid.Columns[4].Visible = true;
                        gvChildGrid.Columns[5].Visible = true;
                        gvChildGrid.Columns[6].Visible = false;
                        gvChildGrid.Columns[7].Visible = false;
                        gvChildGrid.Columns[8].Visible = false;
                        gvChildGrid.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChildGrid.Columns[2].Visible = false;
                        gvChildGrid.Columns[3].Visible = false;
                        gvChildGrid.Columns[4].Visible = false;
                        gvChildGrid.Columns[5].Visible = false;
                        gvChildGrid.Columns[6].Visible = true;
                        gvChildGrid.Columns[7].Visible = true;
                        gvChildGrid.Columns[8].Visible = true;
                        gvChildGrid.Columns[9].Visible = true;
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_TeacherAllotment_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void gvChild_TeacherAllotment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 5;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_TeacherAllotment_Subject");

                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
                objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

                //DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                //DataTable dt = new DataTable();
                //dt.Load(dr);
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr; //ds;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvChild_TeacherAllotment_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion--------------Teacher Allotment Status -----------------

    #region------------Class Time Table Status-----------------

    protected void gvParent_ClassTimeTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 7;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ClassTimeTable");

                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChildGrid.DataSource = ds;
                    gvChildGrid.DataBind();

                    //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChildGrid.Columns[2].Visible = true;
                        gvChildGrid.Columns[3].Visible = true;
                        gvChildGrid.Columns[4].Visible = true;
                        gvChildGrid.Columns[5].Visible = true;
                        gvChildGrid.Columns[6].Visible = false;
                        gvChildGrid.Columns[7].Visible = false;
                        gvChildGrid.Columns[8].Visible = false;
                        gvChildGrid.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChildGrid.Columns[2].Visible = false;
                        gvChildGrid.Columns[3].Visible = false;
                        gvChildGrid.Columns[4].Visible = false;
                        gvChildGrid.Columns[5].Visible = false;
                        gvChildGrid.Columns[6].Visible = true;
                        gvChildGrid.Columns[7].Visible = true;
                        gvChildGrid.Columns[8].Visible = true;
                        gvChildGrid.Columns[9].Visible = true;
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_ClassTimeTable_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion---------Class Time Table Status-----------------

    #region------------Exam Time Table Status-----------------

    protected void gvParent_ExamTimeTable_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 9;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ExamTimeTable");

                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                // DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr; //ds;
                    gvChildGrid.DataBind();

                    //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChildGrid.Columns[2].Visible = true;
                        gvChildGrid.Columns[3].Visible = true;
                        gvChildGrid.Columns[4].Visible = true;
                        gvChildGrid.Columns[5].Visible = true;
                        gvChildGrid.Columns[6].Visible = false;
                        gvChildGrid.Columns[7].Visible = false;
                        gvChildGrid.Columns[8].Visible = false;
                        gvChildGrid.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChildGrid.Columns[2].Visible = false;
                        gvChildGrid.Columns[3].Visible = false;
                        gvChildGrid.Columns[4].Visible = false;
                        gvChildGrid.Columns[5].Visible = false;
                        gvChildGrid.Columns[6].Visible = true;
                        gvChildGrid.Columns[7].Visible = true;
                        gvChildGrid.Columns[8].Visible = true;
                        gvChildGrid.Columns[9].Visible = true;
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_ClassTimeTable_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion---------Exam Time Table Status-----------------

    #region------------Exam Registration Status-----------------

    protected void gvParent_ExamRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ExamRegistration_Department");

                HiddenField hdfCollegeno = e.Row.FindControl("hdfCollegeno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objADEntity.CollegeId = hdfCollegeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeno.Value);

                GetExamRegDeptData();
                DataView dv = new DataView(dsExamRegistrationDept.Tables[0], "COLLEGE_ID =" + objADEntity.CollegeId, "COLLEGE_ID", DataViewRowState.CurrentRows);
                //SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity);
                if (dv.Count > 0)
                {
                    gvChildGrid.DataSource = dv;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_ExamRegistration_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_ExamRegistration_Department_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ExamRegistration");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                GetExamRegSchmewiseData();
                DataView dv = new DataView(dsExamRegistrationScheme.Tables[0], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo, "COLLEGE_ID,DEPTNO", DataViewRowState.CurrentRows);

                if (dv.Count > 0)
                {
                    gvChildGrid.DataSource = dv;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvChild_ExamRegistration_Department_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_ExamRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
            HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
            HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChild_ExamRegistration_Exam_Form_Fillup = (GridView)e.Row.FindControl("gvChild_ExamRegistration_Exam_Form_Fillup");

                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
                objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);
                objADEntity.ExamProcessView = 1;

                //DataSet ds_Exam_Form_Fillup = objADEController.GetAcademicDashboardDetail(objADEntity);
                //Exam Form Fillup
                GetExamRegSchmewiseDataAll();

                DataView dv_Exam_Form_Fillup = new DataView(dsExamRegistrationAll.Tables[0], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO", DataViewRowState.CurrentRows);
                if (dv_Exam_Form_Fillup.Count > 0)
                {
                    gvChild_ExamRegistration_Exam_Form_Fillup.DataSource = dv_Exam_Form_Fillup;//ds_Exam_Form_Fillup.Tables[0];
                    gvChild_ExamRegistration_Exam_Form_Fillup.DataBind();

                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[1].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[2].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[3].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[4].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[5].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[6].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[7].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[8].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[1].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[2].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[3].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[4].Visible = false;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[5].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[6].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[7].Visible = true;
                        gvChild_ExamRegistration_Exam_Form_Fillup.Columns[8].Visible = true;
                    }
                }
                else
                {
                    gvChild_ExamRegistration_Exam_Form_Fillup.DataSource = null;
                    gvChild_ExamRegistration_Exam_Form_Fillup.DataBind();
                }

                ////Payment Process
                GridView gvChild_ExamRegistration_ExamPayment = (GridView)e.Row.FindControl("gvChild_ExamRegistration_ExamPayment");

                DataView dv_PaymentProcess = new DataView(dsExamRegistrationAll.Tables[1], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO", DataViewRowState.CurrentRows);
                if (dv_PaymentProcess.Count > 0)
                {
                    gvChild_ExamRegistration_ExamPayment.DataSource = dv_PaymentProcess;
                    gvChild_ExamRegistration_ExamPayment.DataBind();

                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChild_ExamRegistration_ExamPayment.Columns[1].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[2].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[3].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[4].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[5].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[6].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[7].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[8].Visible = false;

                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChild_ExamRegistration_ExamPayment.Columns[1].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[2].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[3].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[4].Visible = false;
                        gvChild_ExamRegistration_ExamPayment.Columns[5].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[6].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[7].Visible = true;
                        gvChild_ExamRegistration_ExamPayment.Columns[8].Visible = true;

                    }
                }
                else
                {
                    gvChild_ExamRegistration_ExamPayment.DataSource = null;
                    gvChild_ExamRegistration_ExamPayment.DataBind();
                }

                //HOD APPROVAL
                GridView gvChild_ExamRegistration_HODApproval = (GridView)e.Row.FindControl("gvChild_ExamRegistration_HODApproval");

                DataView dv_HOD_Approval = new DataView(dsExamRegistrationAll.Tables[2], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO", DataViewRowState.CurrentRows);
                if (dv_HOD_Approval.Count > 0) //HOD Approval
                {
                    gvChild_ExamRegistration_HODApproval.DataSource = dv_HOD_Approval;
                    gvChild_ExamRegistration_HODApproval.DataBind();

                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChild_ExamRegistration_HODApproval.Columns[1].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[2].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[3].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[4].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[5].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[6].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[7].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[8].Visible = false;

                    }
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChild_ExamRegistration_HODApproval.Columns[1].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[2].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[3].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[4].Visible = false;
                        gvChild_ExamRegistration_HODApproval.Columns[5].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[6].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[7].Visible = true;
                        gvChild_ExamRegistration_HODApproval.Columns[8].Visible = true;

                    }
                }
                else
                {
                    gvChild_ExamRegistration_HODApproval.DataSource = null;
                    gvChild_ExamRegistration_HODApproval.DataBind();
                }

                //ADMIT CARD DOWNLOAD DETAIL
                GridView gvChild_ExamRegistration_AdmitCardDownload = (GridView)e.Row.FindControl("gvChild_ExamRegistration_AdmitCardDownload");

                DataView dv_Admit_Card = new DataView(dsExamRegistrationAll.Tables[3], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO", DataViewRowState.CurrentRows);
                if (dv_Admit_Card.Count > 0) //Admit Card
                {
                    gvChild_ExamRegistration_AdmitCardDownload.DataSource = dv_Admit_Card;//ds_Exam_Form_Fillup.Tables[3];
                    gvChild_ExamRegistration_AdmitCardDownload.DataBind();

                    // int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[1].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[2].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[3].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[4].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[5].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[6].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[7].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[8].Visible = false;

                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[1].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[2].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[3].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[4].Visible = false;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[5].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[6].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[7].Visible = true;
                        gvChild_ExamRegistration_AdmitCardDownload.Columns[8].Visible = true;

                    }
                }
                else
                {
                    gvChild_ExamRegistration_AdmitCardDownload.DataSource = null;
                    gvChild_ExamRegistration_AdmitCardDownload.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard_gvChild_ExamRegistration_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void GetExamRegDeptData()
    {
        if (ExamRegistrationDept == 0)
        {
            ExamRegistrationDept = 1;
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 10;

            //dsExamRegistrationDept = objADEController.GetAcademicDashboardDetail(objADEntity); // Commented by Sagar Mankar on Date 18032024 with Ticket No 56499

            string SP_Name = "PKG_GET_ACADEMIC_DASHBOARD"; // Added by Sagar Mankar on Date 18032024 with Ticket No 56499
            string SP_Parameters = "@P_DASHBOARD_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_DEPTNO,@P_SEMESTERNOS,@P_EXAMPROCESSVIEW";
            string Call_Values = "" + objADEntity.Dashboard_Type + "," + objADEntity.SessionNo + "," + objADEntity.CollegeId + "," + objADEntity.SchemeNo + "," + objADEntity.DeptNo + "," + ViewState["semester"].ToString() + "," + objADEntity.ExamProcessView + "";

            dsExamRegistrationDept = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        }
    }

    private void GetExamRegSchmewiseData()
    {
        //bind Scheme
        if (ExamSchemeBindStatus == 0)
        {
            ExamSchemeBindStatus = 1;
            dsExamRegistrationScheme = null;
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 11;

            //dsExamRegistrationScheme = objADEController.GetAcademicDashboardDetail(objADEntity); // Commented by Sagar Mankar on Date 18032024 with Ticket No 56499

            string SP_Name = "PKG_GET_ACADEMIC_DASHBOARD"; // Added by Sagar Mankar on Date 18032024 with Ticket No 56499
            string SP_Parameters = "@P_DASHBOARD_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_DEPTNO,@P_SEMESTERNOS,@P_EXAMPROCESSVIEW";
            string Call_Values = "" + objADEntity.Dashboard_Type + "," + objADEntity.SessionNo + "," + objADEntity.CollegeId + "," + objADEntity.SchemeNo + "," + objADEntity.DeptNo + "," + ViewState["semester"].ToString() + "," + objADEntity.ExamProcessView + "";

            dsExamRegistrationScheme = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        }
    }

    private void GetExamRegSchmewiseDataAll()
    {
        if (ExamSchemeWiseAllStatus == 0)
        {
            ExamSchemeWiseAllStatus = 1;
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 14;

            //dsExamRegistrationAll = objADEController.GetAcademicDashboardDetail(objADEntity); // Commented by Sagar Mankar on Date 18032024 with Ticket No 56499

            string SP_Name = "PKG_GET_ACADEMIC_DASHBOARD"; // Added by Sagar Mankar on Date 18032024 with Ticket No 56499
            string SP_Parameters = "@P_DASHBOARD_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_DEPTNO,@P_SEMESTERNOS,@P_EXAMPROCESSVIEW";
            string Call_Values = "" + objADEntity.Dashboard_Type + "," + objADEntity.SessionNo + "," + objADEntity.CollegeId + "," + objADEntity.SchemeNo + "," + objADEntity.DeptNo + "," + ViewState["semester"].ToString() + "," + objADEntity.ExamProcessView + "";

            dsExamRegistrationAll = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);
        }
    }

    #region Exam Form Pending Student List.

    protected void gvChild_ExamRegistrationExam_FormPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
        HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
        HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildExamFormFillupPending_StudentList = (GridView)e.Row.FindControl("gvChildExamFormFillupPending_StudentList");

            objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
            objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
            objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

            if (ExamRegistrationPendingStatus == 0)
            {
                ExamRegistrationPendingStatus = 1;
                objADEntity.Dashboard_Type = 19;

                //dsExamRegistrationPending = objADEController.GetAcademicDashboardDetail(objADEntity); // Commented by Sagar Mankar on Date 18032024 with Ticket No 56499

                string SP_Name = "PKG_GET_ACADEMIC_DASHBOARD"; // Added by Sagar Mankar on Date 18032024 with Ticket No 56499
                string SP_Parameters = "@P_DASHBOARD_TYPE,@P_SESSIONNO,@P_COLLEGE_ID,@P_SCHEMENO,@P_DEPTNO,@P_SEMESTERNOS,@P_EXAMPROCESSVIEW";
                string Call_Values = "" + objADEntity.Dashboard_Type + "," + objADEntity.SessionNo + "," + objADEntity.CollegeId + "," + objADEntity.SchemeNo + "," + objADEntity.DeptNo + "," + ViewState["semester"].ToString() + "," + objADEntity.ExamProcessView + "";

                dsExamRegistrationPending = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            }

            DataView dv_Exam_Form_FillupStudList = new DataView(dsExamRegistrationPending.Tables[0], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO,SEMESTERNO", DataViewRowState.CurrentRows);
            if (dv_Exam_Form_FillupStudList.Count > 0)
            {
                gvChildExamFormFillupPending_StudentList.DataSource = dv_Exam_Form_FillupStudList;
                gvChildExamFormFillupPending_StudentList.DataBind();
            }
        }
    }

    #endregion Exam Form Pending Student List.

    #region Exam Payment Pending Student List.

    protected void gvChild_ExamRegistration_ExamPaymentPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
        HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
        HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildExamPaymentPending_StudentList = (GridView)e.Row.FindControl("gvChildExamPaymentPending_StudentList");

            objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
            objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
            objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

            if (ExamRegistrationPendingStatus == 0)
            {
                ExamRegistrationPendingStatus = 1;
                objADEntity.Dashboard_Type = 19;
                dsExamRegistrationPending = objADEController.GetAcademicDashboardDetail(objADEntity);
            }

            DataView dv_Exam_Payment_PendingStudList = new DataView(dsExamRegistrationPending.Tables[1], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO,SEMESTERNO", DataViewRowState.CurrentRows);
            if (dv_Exam_Payment_PendingStudList.Count > 0)
            {
                gvChildExamPaymentPending_StudentList.DataSource = dv_Exam_Payment_PendingStudList;
                gvChildExamPaymentPending_StudentList.DataBind();
            }
        }
    }

    #endregion Exam Payment Pending Student List.

    #region Exam HOD Approval Pending Student List.

    protected void gvChild_ExamRegistration_HODApprovalPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
        HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
        HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildHODApprovalPending_StudentList = (GridView)e.Row.FindControl("gvChildHODApprovalPending_StudentList");

            objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
            objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
            objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

            if (ExamRegistrationPendingStatus == 0)
            {
                ExamRegistrationPendingStatus = 1;
                objADEntity.Dashboard_Type = 19;
                dsExamRegistrationPending = objADEController.GetAcademicDashboardDetail(objADEntity);
            }

            DataView dv_Exam_HODApproval_PendingStudList = new DataView(dsExamRegistrationPending.Tables[2], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO,SEMESTERNO", DataViewRowState.CurrentRows);
            if (dv_Exam_HODApproval_PendingStudList.Count > 0)
            {
                gvChildHODApprovalPending_StudentList.DataSource = dv_Exam_HODApproval_PendingStudList;
                gvChildHODApprovalPending_StudentList.DataBind();
            }
        }
    }

    #endregion Exam HOD Approval Pending Student List.

    #region Admit Card Download Pending

    protected void gvChild_ExamRegistration_AdmitCardPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
        HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
        HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildAdmitCardDownloadPending_StudentList = (GridView)e.Row.FindControl("gvChildAdmitCardDownloadPending_StudentList");

            objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
            objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
            objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

            if (ExamRegistrationPendingStatus == 0)
            {
                ExamRegistrationPendingStatus = 1;
                objADEntity.Dashboard_Type = 19;
                dsExamRegistrationPending = objADEController.GetAcademicDashboardDetail(objADEntity);
            }

            DataView dv_Exam_AdmitCard_PendingStudList = new DataView(dsExamRegistrationPending.Tables[3], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO,SEMESTERNO", DataViewRowState.CurrentRows);
            if (dv_Exam_AdmitCard_PendingStudList.Count > 0)
            {
                gvChildAdmitCardDownloadPending_StudentList.DataSource = dv_Exam_AdmitCard_PendingStudList;
                gvChildAdmitCardDownloadPending_StudentList.DataBind();
            }
        }
    }

    #endregion Admit Card Download Pending

    #endregion---------Exam Registration Status-----------------

    #region------------Result Processing Status-----------------

    protected void gvParent_ResultProcessing_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 12;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ResultProcessingDepartment");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;
                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);

                // DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;
                    gvChildGrid.DataBind();
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvParent_ResultProcessing_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_ResultProcessingDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objADEntity.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            objADEntity.Dashboard_Type = 13;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild_ResultProcessing");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);

                // DataSet ds = objADEController.GetAcademicDashboardDetail(objADEntity);
                SqlDataReader dr = objADEController.GetAcademicExamRegistrationDashboardDetail(objADEntity, ViewState["semester"].ToString());
                if (dr != null)
                {
                    gvChildGrid.DataSource = dr;
                    gvChildGrid.DataBind();

                    //int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
                    if (Convert.ToInt32(Session["ODDEVEN"]) == 1)
                    {
                        gvChildGrid.Columns[2].Visible = true;
                        gvChildGrid.Columns[3].Visible = true;
                        gvChildGrid.Columns[4].Visible = true;
                        gvChildGrid.Columns[5].Visible = true;
                        gvChildGrid.Columns[6].Visible = false;
                        gvChildGrid.Columns[7].Visible = false;
                        gvChildGrid.Columns[8].Visible = false;
                        gvChildGrid.Columns[9].Visible = false;
                    }
                    else if (Convert.ToInt32(Session["ODDEVEN"]) == 2)
                    {
                        gvChildGrid.Columns[2].Visible = false;
                        gvChildGrid.Columns[3].Visible = false;
                        gvChildGrid.Columns[4].Visible = false;
                        gvChildGrid.Columns[5].Visible = false;
                        gvChildGrid.Columns[6].Visible = true;
                        gvChildGrid.Columns[7].Visible = true;
                        gvChildGrid.Columns[8].Visible = true;
                        gvChildGrid.Columns[9].Visible = true;
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
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AcademicDashboard__gvChild_ResultProcessingDepartment_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_ResultProcessingPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // 
        HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
        HiddenField hdfDeptno = e.Row.FindControl("hdfDeptno") as HiddenField;
        HiddenField hdfSchemeno = e.Row.FindControl("hdfSchemeno") as HiddenField;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvChildResultPending_StudentList = (GridView)e.Row.FindControl("gvChildResultPending_StudentList");

            objADEntity.CollegeId = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
            objADEntity.DeptNo = hdfDeptno.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptno.Value);
            objADEntity.SchemeNo = hdfSchemeno.Value == string.Empty ? 0 : Convert.ToInt32(hdfSchemeno.Value);

            if (ResultProcessingPendingStatus == 0)
            {
                ResultProcessingPendingStatus = 1;
                objADEntity.Dashboard_Type = 20;
                dsResultProcessingPending = objADEController.GetAcademicDashboardDetail(objADEntity);
            }

            DataView dv_Exam_Result_PendingStudList = new DataView(dsResultProcessingPending.Tables[0], "COLLEGE_ID =" + objADEntity.CollegeId + " AND DEPTNO=" + objADEntity.DeptNo + " AND SCHEMENO=" + objADEntity.SchemeNo, "COLLEGE_ID,DEPTNO,SCHEMENO,SEMESTERNO", DataViewRowState.CurrentRows);
            if (dv_Exam_Result_PendingStudList.Count > 0)
            {
                gvChildResultPending_StudentList.DataSource = dv_Exam_Result_PendingStudList;
                gvChildResultPending_StudentList.DataBind();
            }
        }
    }

    #endregion---------Result Processing Status-----------------

    #region ---------------Mail Sending Process-----------

    protected void btnSendCourseRegistrationMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        //DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Course Registration Progress";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + divname + "');", true);
        }
    }

    protected void btnSendTeacherAllotmentMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        //DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Teacher Allotment Status";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div5" + divname + "');", true);
        }
    }

    protected void btnSendClassTimeTableMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        // DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Class Time Table Status";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div7" + divname + "');", true);
        }
    }

    protected void btnSendExamTimeTableMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        // DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Exam Time Table Status";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div8" + divname + "');", true);
        }
    }

    protected void btnSendExamRegistrationMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        //DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Exam Registration Status";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div9" + divname + "');", true);
        }
    }

    protected void btnSendResultProcessingMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        Button btn = sender as Button;

        int Dept = Convert.ToInt32(btn.CommandArgument);

        //DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND UA_DEPTNO=" + Dept + "", "UA_NO");
        DataSet ds = objCommon.FillDropDown("USER_ACC WITH (NOLOCK)", "ISNULL(UA_EMAIL,'') AS UA_EMAIL", "UA_NO", "UA_TYPE=8 AND " + Dept + " IN (SELECT VALUE FROM DBO.SPLIT(UA_DEPTNO,','))", "UA_NO");

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (ToEmail == string.Empty)
            {
                ToEmail = Convert.ToString(dr["UA_EMAIL"]);
            }
            else if (ToEmail != string.Empty)
            {
                ToEmail += "," + Convert.ToString(dr["UA_EMAIL"]);
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.CommandArgument;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            Session["EmailCategory"] = "Result Processing Status";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div10" + divname + "');", true);
        }

    }

    protected void btnSent_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsconfig = null;
            string COMPANY_EMAILSVCID = string.Empty;
            string SENDGRID_APIKEY = string.Empty;
            string CollegeId = string.Empty;
            string SrNo = string.Empty;
            string MailMultiple = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            byte[] bytes = null;


            if (fileUploadAttachement.HasFile)
            {
                Stream fs = fileUploadAttachement.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                fileName = fileUploadAttachement.FileName;
            }

            int SendingEmailStatus = 0;
            string EmailId = string.Empty;
            string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            Session["SENDGRID_APIKEY"] = string.Empty;
            if (dsconfig.Tables[0].Rows.Count > 0)
            {
                COMPANY_EMAILSVCID = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
                SENDGRID_APIKEY = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            }


            DataRow dr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("TOEMAILID", typeof(string));
            dt.Columns.Add("CC_MAILID", typeof(string));
            dt.Columns.Add("BCC_MAILID", typeof(string));
            dt.Columns.Add("FROMEMAILID", typeof(string));
            dt.Columns.Add("EMAIL_STATUS", typeof(string));
            dt.Columns.Add("EMAIL_TEXTMATTER", typeof(string));
            dt.Columns.Add("EMAIL_SUBJECT", typeof(string));
            dt.Columns.Add("EMAIL_CATEGORY", typeof(string));
            dt.Columns.Add("SEND_FILENAME", typeof(string));
            dt.Columns.Add("EMAILFROM_UA_NO", typeof(int));
            dt.Columns.Add("IPADDRESS", typeof(string));

            int status = 0;

            string[] mail = txt_emailid.Text.Split(',');
            for (int i = 0; i < mail.Length; i++)
            {
                if (i == 0)
                {
                    if (mail[i] != string.Empty)
                    {
                        EmailId = mail[i];
                    }
                }
                else
                {
                    if (mail[i] != string.Empty)
                    {
                        if (MailMultiple == string.Empty)
                        {
                            MailMultiple = mail[i];
                        }
                        else
                        {
                            MailMultiple += "," + mail[i];
                        }
                    }
                }
            }

            string email_type = string.Empty;
            DataSet ds = getModuleConfig();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            }
            if (email_type == "2" && email_type != "")
            {
                // Gridview Mail Sending Process.
                Task<int> task = Execute(txtBody.Text, EmailId, MailMultiple, txtCc.Text, txtBcc.Text, txtSubject.Text, COMPANY_EMAILSVCID, SENDGRID_APIKEY, bytes, fileName);
                //Task<int> task = Execute(txtBody.Text, EmailId, txtSubject.Text);
                status = task.Result;
            }
            if (email_type == "3" && email_type != "")
            {
                OutLook_Email(txtBody.Text, EmailId, txtSubject.Text, MailMultiple, txtCc.Text, txtBcc.Text, bytes, fileName);
            }
            dr = dt.NewRow();
            dr["TOEMAILID"] = txt_emailid.Text;
            dr["CC_MAILID"] = txtCc.Text;
            dr["BCC_MAILID"] = txtBcc.Text;
            dr["FROMEMAILID"] = COMPANY_EMAILSVCID;
            dr["EMAIL_STATUS"] = status == 1 ? "Delivered" : "Not Delivered";
            dr["EMAIL_TEXTMATTER"] = txtBody.Text;
            dr["EMAIL_SUBJECT"] = txtSubject.Text;
            dr["EMAIL_CATEGORY"] = Session["EmailCategory"];
            dr["SEND_FILENAME"] = fileName;
            dr["EMAILFROM_UA_NO"] = Convert.ToInt32(Session["userno"]);
            dr["IPADDRESS"] = ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            dt.Rows.Add(dr);

            if (status == 0)
            {
                SendingEmailStatus = 1;
                EmailId = txt_emailid.Text;
            }

            //for (int i = 0; i < mail.Length; i++)
            //{
            //    if (mail[i] != string.Empty)
            //    {
            //        //Gridview Mail Sending Process.
            //        Task<int> task = Execute(txtBody.Text, mail[i], txtSubject.Text, COMPANY_EMAILSVCID, SENDGRID_APIKEY, bytes, fileName);
            //        status = task.Result;

            //        dr = dt.NewRow();
            //        dr["TOEMAILID"] = mail[i];
            //        dr["FROMEMAILID"] = COMPANY_EMAILSVCID;
            //        dr["EMAIL_STATUS"] = status == 1 ? "Delivered" : "Not Delivered";
            //        dr["EMAIL_TEXTMATTER"] = txtBody.Text;
            //        dr["EMAIL_SUBJECT"] = txtSubject.Text;
            //        dr["EMAIL_CATEGORY"] = Session["EmailCategory"];
            //        dr["EMAILFROM_UA_NO"] = Convert.ToInt32(Session["userno"]);
            //        dr["IPADDRESS"] = ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"]; ;
            //        dt.Rows.Add(dr);

            //        if (status == 0)
            //        {
            //            SendingEmailStatus = 1;
            //            if (EmailId == string.Empty)
            //            {
            //                EmailId = mail[i];
            //            }
            //            else
            //            {
            //                EmailId += "," + mail[i];
            //            }
            //        }
            //    }
            //}

            objADEController.Insert_Academic_Dashboard_Email_Sending_Log(dt);

            if (SendingEmailStatus == 0)
            {
                objCommon.DisplayMessage(updAcademicDashboard, "Email Sent Successfully", this.Page);
                txt_emailid.Text = string.Empty;
                txtBody.Text = string.Empty;
                txtSubject.Text = string.Empty;
                string divname = Session["divname"].ToString();
                if (Convert.ToString(Session["EmailCategory"]) == "Course Registration Progress")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + divname + "');", true);
                }
                else if (Convert.ToString(Session["EmailCategory"]) == "Teacher Allotment Status")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div5" + divname + "');", true);
                }
                else if (Convert.ToString(Session["EmailCategory"]) == "Class Time Table Status")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div7" + divname + "');", true);
                }
                else if (Convert.ToString(Session["EmailCategory"]) == "Exam Time Table Status")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div8" + divname + "');", true);
                }
                else if (Convert.ToString(Session["EmailCategory"]) == "Exam Registration Status")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div9" + divname + "');", true);
                }
                else if (Convert.ToString(Session["EmailCategory"]) == "Result Processing Status")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div10" + divname + "');", true);
                }

                Session["divname"] = string.Empty;
            }
            else
            {
                objCommon.DisplayMessage(updAcademicDashboard, "Email Not Send Some Faculty like " + EmailId + ", Please Try Again !!!.", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + EmailId + "');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_btnSent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private int OutLook_Email(string Message, string toEmailId, string sub, string toMultipleEmailId, string Cc, string Bcc, byte[] bytes, string fileName)
    {

        int ret = 0;
        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oMail.To = toEmailId;
            oMail.Cc = Cc;
            oMail.Bcc = Bcc;
            oMail.Subject = sub;
            oMail.HtmlBody = Message;
            //To Multiple Mail ID
            //if (toMultipleEmailId != string.Empty)
            //{
            //    string[] mail = toMultipleEmailId.Split(',');
            //    for (int i = 0; i < mail.Length; i++)
            //    {
            //        if (mail[i] != string.Empty)
            //        {
            //            oMail.AddAttachment(Convert.ToString(mail[i]));
            //                //AddTo(Convert.ToString(mail[i]));
            //        }
            //    }
            //}

            ////Cc Mail Id
            //if (Cc != string.Empty)
            //{
            //    string[] mailCc = Cc.Split(',');
            //    for (int i = 0; i < mailCc.Length; i++)
            //    {
            //        if (mailCc[i] != string.Empty)
            //        {
            //           .AddCc(Convert.ToString(mailCc[i]));
            //        }
            //    }
            //}

            ////Bcc Mail ID
            //if (Bcc != string.Empty)
            //{
            //    string[] mailBcc = Bcc.Split(',');
            //    for (int i = 0; i < mailBcc.Length; i++)
            //    {
            //        if (mailBcc[i] != string.Empty)
            //        {
            //            msg.AddBcc(Convert.ToString(mailBcc[i]));
            //        }
            //    }
            //}
            if (fileName != string.Empty)
            {
                oMail.AddAttachment(fileName, bytes);
            }
            // SmtpServer oServer = new SmtpServer("smtp.live.com");
            SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
            oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            Console.WriteLine("start to send email over TLS...");
            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
            Console.WriteLine("email sent successfully!");
            ret = 1;
        }
        catch (Exception ep)
        {
            Console.WriteLine("failed to send email with the following error:");
            Console.WriteLine(ep.Message);
            ret = 0;
        }
        return ret;
    }

    private DataSet getModuleConfig()
    {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
    }

    static async Task<int> Execute(string Message, string toEmailId, string toMultipleEmailId, string Cc, string Bcc, string sub, string COMPANY_EMAILSVCID, string SENDGRID_APIKEY, byte[] bytes, string fileName)
    {
        int ret = 0;
        try
        {
            var fromAddress = new System.Net.Mail.MailAddress(COMPANY_EMAILSVCID, "MAKAUT");
            //var toAddress = new MailAddress(toEmailId, "");

            var apiKey = SENDGRID_APIKEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(COMPANY_EMAILSVCID, "MAKAUT");
            var subject = sub;

            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            //To Multiple Mail ID
            if (toMultipleEmailId != string.Empty)
            {
                string[] mail = toMultipleEmailId.Split(',');
                for (int i = 0; i < mail.Length; i++)
                {
                    if (mail[i] != string.Empty)
                    {
                        msg.AddTo(Convert.ToString(mail[i]));
                    }
                }
            }

            //Cc Mail Id
            if (Cc != string.Empty)
            {
                string[] mailCc = Cc.Split(',');
                for (int i = 0; i < mailCc.Length; i++)
                {
                    if (mailCc[i] != string.Empty)
                    {
                        msg.AddCc(Convert.ToString(mailCc[i]));
                    }
                }
            }

            //Bcc Mail ID
            if (Bcc != string.Empty)
            {
                string[] mailBcc = Bcc.Split(',');
                for (int i = 0; i < mailBcc.Length; i++)
                {
                    if (mailBcc[i] != string.Empty)
                    {
                        msg.AddBcc(Convert.ToString(mailBcc[i]));
                    }
                }
            }

            if (fileName != string.Empty)
            {
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment(fileName, file);
            }
            //msg.AddCc("umeshganorkar@iitms.co.in"); //roshan.pannase@gmail.com

            //var response = await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
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

    // added by shubham On 23/02/2023
    //static async Task<int> Execute(string Message, string toEmailId, string sub)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //        var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //        var client = new SendGridClient(apiKey);
    //        var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //        var subject = sub;
    //        var to = new EmailAddress(toEmailId, "");
    //        var plainTextContent = "";
    //        var htmlContent = Message;
    //        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //        //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //        string res = Convert.ToString(response.StatusCode);
    //        if (res == "Accepted")
    //        {
    //            ret = 1;
    //            Console.WriteLine("Email Sent successfully!");

    //        }
    //        else
    //        {
    //            ret = 0;
    //            Console.WriteLine("Fail to send Mail!");
    //        }
    //        //attachments.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        ret = 0;
    //    }
    //    return ret;
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_emailid.Text = string.Empty;
        txtBody.Text = string.Empty;
        txtSubject.Text = string.Empty;
    }

    #endregion ----------- Mail Sending Process-----------

    #endregion ------------Controll Event---------------

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        string spName = string.Empty; string spParamaters = string.Empty; string spValue = string.Empty;
        DataSet dscollege = null;
        spName = "PKG_ACD_FILL_SCHOOL";
        spParamaters = "@P_SESSIONID";
        spValue = "" + ddlexamSession.SelectedValue + "";

        dscollege = objCommon.DynamicSPCall_Select(spName, spParamaters, spValue);
        if (dscollege.Tables[0].Rows.Count > 0)
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            ddlSession.DataSource = dscollege;
            ddlSession.DataValueField = dscollege.Tables[0].Columns[0].ToString();
            ddlSession.DataTextField = dscollege.Tables[0].Columns[1].ToString();
            ddlSession.DataBind();
        }
        else
        {
            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
        }

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            showSemester.Visible = true;
            int ODDEVEN = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "ISNULL(ODD_EVEN,0)", "SESSIONNO=" + ddlSession.SelectedValue));
            if (ODDEVEN == 1)
            {
                objCommon.FillDropDownList(ddlInstMultiCheck, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ODD_EVEN=" + ODDEVEN, "SEMESTERNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlInstMultiCheck, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "ODD_EVEN=" + ODDEVEN, "SEMESTERNO");
            }
            divProgressbar.Visible = false;
            divAcademicDashboard.Visible = false;
            lblmag.Text = string.Empty;
        }
    }
}