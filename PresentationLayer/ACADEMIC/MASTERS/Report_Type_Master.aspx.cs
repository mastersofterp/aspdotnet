//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REPORT TYPE MASTER                                                    
// CREATION DATE : 04-April-2023                                                          
// CREATED BY    : NEHAL                                                                    
//======================================================================================
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
using mastersofterp_MAKAUAT;

public partial class ACADEMIC_MASTERS_Report_Type_Master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objSC = new AcdAttendanceController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["AuthFlag"] = 0;
            //Check Session
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
                ////Load Page Help
                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}
                BindListView();
            }
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Report_Type_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Report_Type_Master.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objSC.GetAllReportLsit(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlReport.Visible = true;
                lvReport.DataSource = ds;
                lvReport.DataBind();
            }
            else
            {
                pnlReport.Visible = false;
                lvReport.DataSource = null;
                lvReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            string ReportName = txtReportName.Text.Trim();
            bool IsActive, iscollege, issession, issemseter, iscoursetype, iscourse, issection, isfromdt, istodt, isoperator, 
                ispercentage, issubjecttype, istheory, isbtwpercentage, ismultiplecollege, isschoolinst, isdept, isfaculty,isExcelReport,isShowDeatils,isSessionValidation;

            if (hfdActive.Value == "true")
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }
            if (hfdCollegeScheme.Value == "true")
            {
                iscollege = true;
            }
            else
            {
                iscollege = false;
            }
            if (hfdSession.Value == "true")
            {
                issession = true;
            }
            else
            {
                issession = false;
            }
            if (hfdSemester.Value == "true")
            {
                issemseter = true;
            }
            else
            {
                issemseter = false;
            }
            if (hfdCousrseType.Value == "true")
            {
                iscoursetype = true;
            }
            else
            {
                iscoursetype = false;
            }
            if (hfdCousrse.Value == "true")
            {
                iscourse = true;
            }
            else
            {
                iscourse = false;
            }
            if (hfdSection.Value == "true")
            {
                issection = true;
            }
            else
            {
                issection = false;
            }
            if (hfdFromDate.Value == "true")
            {
                isfromdt = true;
            }
            else
            {
                isfromdt = false;
            }
            if (hfdToDate.Value == "true")
            {
                istodt = true;
            }
            else
            {
                istodt = false;
            }
            if (hfdOperator.Value == "true")
            {
                isoperator = true;
            }
            else
            {
                isoperator = false;
            }
            if (hfdPercent.Value == "true")
            {
                ispercentage = true;
            }
            else
            {
                ispercentage = false;
            }
            if (hfdSubjecttype.Value == "true")
            {
                issubjecttype = true;
            }
            else
            {
                issubjecttype = false;
            }
            if (hfdTheory.Value == "true")
            {
                istheory = true;
            }
            else
            {
                istheory = false;
            }
            if (hfdBtnPercent.Value == "true")
            {
                isbtwpercentage = true;
            }
            else
            {
                isbtwpercentage = false;
            }
            if (hfdCollegemultiple.Value == "true")
            {
                ismultiplecollege = true;
            }
            else
            {
                ismultiplecollege = false;
            }
            if (hfdSchoolinst.Value == "true")
            {
                isschoolinst = true;
            }
            else
            {
                isschoolinst = false;
            }
            if (hfdDepartment.Value == "true")
            {
                isdept = true;
            }
            else
            {
                isdept = false;
            }
            if (hfdFaculty.Value == "true")
            {
                isfaculty = true;
            }
            else
            {
                isfaculty = false;
            }

            if (hfdExcelReport.Value == "true")
            {
                isExcelReport = true;
            }
            else
            {
                isExcelReport = false;
            }

            if (hfdShowlist.Value == "true")
            {
                isShowDeatils = true;
            }
            else
            {
                isShowDeatils = false;
            }

            if (hfdSessionValidation.Value == "true")
            {
                isSessionValidation = true;
            }
            else
            {
                isSessionValidation = false;
            }

      

            //Check for add or edit
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                //Edit
                int reportid = Convert.ToInt32(Session["reportid"]);
                CustomStatus cs = (CustomStatus)objSC.Add_ReportTypeMaster(ReportName, IsActive, iscollege, issession, issemseter, iscoursetype, iscourse, issection, isfromdt, istodt, isoperator,
                    ispercentage, issubjecttype, istheory, isbtwpercentage, ismultiplecollege, isschoolinst, isdept, isfaculty, isExcelReport, isShowDeatils, isSessionValidation, reportid);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    txtReportName.Text = string.Empty;
                    BindListView();
                    Session["action"] = null;
                    objCommon.DisplayMessage(this.UPDROLE, "Record Updated sucessfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                }
            }
            else
            {
                //Add
                int reportid = 0;
                CustomStatus cs = (CustomStatus)objSC.Add_ReportTypeMaster(ReportName, IsActive, iscollege, issession, issemseter, iscoursetype, iscourse, issection, isfromdt, istodt, isoperator,
                    ispercentage, issubjecttype, istheory, isbtwpercentage, ismultiplecollege, isschoolinst, isdept, isfaculty, isExcelReport, isShowDeatils, isSessionValidation, reportid);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Added sucessfully", this.Page);
                    txtReportName.Text = string.Empty;
                    BindListView();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.UPDROLE, "Record Already Exist", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int reportid = int.Parse(btnEdit.CommandArgument);
        Session["reportid"] = int.Parse(btnEdit.CommandArgument);
        ViewState["edit"] = "edit";

        this.ShowDetails(reportid);
        txtReportName.Focus();
    }
    private void ShowDetails(int reportid)
    {
        try
        {
            DataSet ds = objSC.GetAllReportLsit(reportid);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtReportName.Text = ds.Tables[0].Rows[0]["REPORT_NAME"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["REPORT_NAME"].ToString();
                string IS_ACTIVE = ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString();
                string IS_COLLEGE_SCHEME_STATUS = ds.Tables[0].Rows[0]["IS_COLLEGE_SCHEME_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_COLLEGE_SCHEME_STATUS"].ToString();
                string IS_SESSION_STATUS = ds.Tables[0].Rows[0]["IS_SESSION_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SESSION_STATUS"].ToString();
                string IS_SEMESTER_STATUS = ds.Tables[0].Rows[0]["IS_SEMESTER_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SEMESTER_STATUS"].ToString();
                string IS_COURSE_TYPE_STATUS = ds.Tables[0].Rows[0]["IS_COURSE_TYPE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_COURSE_TYPE_STATUS"].ToString();
                string IS_COURSE_STATUS = ds.Tables[0].Rows[0]["IS_COURSE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_COURSE_STATUS"].ToString();
                string IS_SECTION_STATUS = ds.Tables[0].Rows[0]["IS_SECTION_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SECTION_STATUS"].ToString();
                string IS_FROM_DATE_STATUS = ds.Tables[0].Rows[0]["IS_FROM_DATE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_FROM_DATE_STATUS"].ToString();
                string IS_TO_DATE_STATUS = ds.Tables[0].Rows[0]["IS_TO_DATE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_TO_DATE_STATUS"].ToString();
                string IS_OPERATOR_STATUS = ds.Tables[0].Rows[0]["IS_OPERATOR_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_OPERATOR_STATUS"].ToString();
                string IS_PERCENTAGE_STATUS = ds.Tables[0].Rows[0]["IS_PERCENTAGE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_PERCENTAGE_STATUS"].ToString();
                string IS_SUBJECT_TYPE_STATUS = ds.Tables[0].Rows[0]["IS_SUBJECT_TYPE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SUBJECT_TYPE_STATUS"].ToString();
                string IS_THEORY_PRACTICAL_TUTORIAL_STATUS = ds.Tables[0].Rows[0]["IS_THEORY_PRACTICAL_TUTORIAL_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_THEORY_PRACTICAL_TUTORIAL_STATUS"].ToString();
                string IS_BETWEEN_PERCENTAGE_STATUS = ds.Tables[0].Rows[0]["IS_BETWEEN_PERCENTAGE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_BETWEEN_PERCENTAGE_STATUS"].ToString();
                string IS_MULTIPLE_COLLEGE_STATUS = ds.Tables[0].Rows[0]["IS_MULTIPLE_COLLEGE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_MULTIPLE_COLLEGE_STATUS"].ToString();
                string IS_SCHOOL_INSTITUTE_STATUS = ds.Tables[0].Rows[0]["IS_SCHOOL_INSTITUTE_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SCHOOL_INSTITUTE_STATUS"].ToString();
                string IS_DEPARTMENT_STATUS = ds.Tables[0].Rows[0]["IS_DEPARTMENT_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_DEPARTMENT_STATUS"].ToString();
                string IS_FACULTY_STATUS = ds.Tables[0].Rows[0]["IS_FACULTY_STATUS"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_FACULTY_STATUS"].ToString();

                string IS_EXCEL_STATUS = ds.Tables[0].Rows[0]["ISEXCEL_REPORT"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["ISEXCEL_REPORT"].ToString();
                string IS_SHOW_STATUS = ds.Tables[0].Rows[0]["IS_SHOW_REPORT"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_SHOW_REPORT"].ToString();
                string IS_SESSIONVALIDATION_STATUS = ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["IS_FROMDT_RFV"].ToString();


                if (IS_ACTIVE == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc", "SetStatActive(false);", true);
                }
                if (IS_COLLEGE_SCHEME_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertscript64103658111", "SetStatCollegeScheme(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertscript64103658111", "SetStatCollegeScheme(false);", true);
                }
                if (IS_SESSION_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1", "SetStatSession(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1", "SetStatSession(false);", true);
                }
                if (IS_SEMESTER_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12", "SetStatSemester(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12", "SetStatSemester(false);", true);
                }
                if (IS_COURSE_TYPE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc123", "SetStatCousrseType(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc123", "SetStatCousrseType(false);", true);
                }
                if (IS_COURSE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1234", "SetStatCousrse(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1234", "SetStatCousrse(false);", true);
                }
                if (IS_SECTION_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1235", "SetStatSection(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1235", "SetStatSection(false);", true);
                }
                if (IS_FROM_DATE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1236", "SetStatFromDate(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1236", "SetStatFromDate(false);", true);
                }
                if (IS_TO_DATE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1237", "SetStatToDate(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1237", "SetStatToDate(false);", true);
                }
                if (IS_OPERATOR_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1238", "SetStatOperator(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1238", "SetStatOperator(false);", true);
                }
                if (IS_PERCENTAGE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1239", "SetStatPercent(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc1239", "SetStatPercent(false);", true);
                }
                if (IS_SUBJECT_TYPE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12310", "SetStatSubjecttype(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12310", "SetStatSubjecttype(false);", true);
                }
                if (IS_THEORY_PRACTICAL_TUTORIAL_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12311", "SetStatTheory(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12312", "SetStatTheory(false);", true);
                }
                if (IS_BETWEEN_PERCENTAGE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12313", "SetStatBtnPercent(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12313", "SetStatBtnPercent(false);", true);
                }
                if (IS_MULTIPLE_COLLEGE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12314478", "SetStatCollegemultiple(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12314478", "SetStatCollegemultiple(false);", true);
                }
                if (IS_SCHOOL_INSTITUTE_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12315", "SetStatSchoolinst(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12315", "SetStatSchoolinst(false);", true);
                }
                if (IS_DEPARTMENT_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12316789", "SetStatDepartment(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12316789", "SetStatDepartment(false);", true);
                }
                if (IS_FACULTY_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12317", "SetStatFaculty(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc12317", "SetStatFaculty(false);", true);
                }

                if (IS_EXCEL_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc4849655", "SetStatExcelReport(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc4849655", "SetStatExcelReport(false);", true);
                }

                if (IS_SESSIONVALIDATION_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc8524757", "SetStatSessionValidation(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc8524757", "SetStatSessionValidation(false);", true);
                }

                if (IS_SHOW_STATUS == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc196385", "SetStatShowlist(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertsc196385", "SetStatShowlist(false);", true);
                }

               
                         
            }
            if (ds != null) ;

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() == "1")
        {
            Response.Redirect("~/principalHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "5")
        {
            Response.Redirect("~/homeNonFaculty.aspx", false);
        }
        else
        {
            Response.Redirect("~/home.aspx", false);
        }
    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
        string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
        string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);
        if (txtPass.Text.Trim() == db_pwd)
        {
            popup.Visible = false;
            Session["AuthFlag"] = 1;
            BindListView();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
        }
        else
            objCommon.DisplayMessage("Password does not match!", this.Page);
    }
}