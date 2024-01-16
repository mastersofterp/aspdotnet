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
using System.Data.SqlClient;
using System.Configuration;

public partial class ACADEMIC_EXAMINATION_Exam_Configue : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamController exam = new ExamController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    protected string a = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority                     
                    //   this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                }
                PopulateUserTypes();
                BindView();
            }
            else
            {
                // Clear message div
                //divMsg.InnerHtml = string.Empty;
            }


            //Response.Redirect(Request.Url.ToString());

        }
        catch (Exception ex)
        {

        }
    }
    private void PopulateUserTypes()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SUBJECTTYPE WITH (NOLOCK)", "SUBID", "SUBNAME", "SUBID>0 AND ACTIVESTATUS=1", "SUBID");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkListSubjectTypes.DataTextField = "SUBNAME";
                    chkListSubjectTypes.DataValueField = "SUBID";

                    chkListSubjectTypes.DataSource = ds.Tables[0];
                    chkListSubjectTypes.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Activity_ActivityMaster.PopulateUserTypes --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //private void BindView()
    //{

    //    DataSet ds = objCommon.FillDropDown("ACD_EXAM_CONFIGURATION", "EXAM_REGISTRATION", "EXAM_RULE,GRACE_RULE,LATE_FEE,IMPROVEMENT,EXAM_PATTERN,REVALUATION_PROCESS,RESULT_PUBLISH,CONDONATION,ISNULL(DECODE_NUMBER,0)AS DECODE_NUMBER,ISNULL(SEAT_NUMBER,0)AS SEAT_NUMBER,ISNULL(ExcelMarkEntry,0)AS ExcelMarkEntry,ISNULL(SEC_TIMETABLE,0) AS SEC_TIMETABLE,ISNULL(BATCH_TIMETABLE,0) AS BATCH_TIMETABLE,ISNULL(GRADE_ADMIN,0) AS GRADE_ADMIN,ISNULL(GRADE_FACULTY,0) AS GRADE_FACULTY,ISNULL(GRAPH,0) AS GRAPH,ISNULL(GRADE_RANGE,0) AS GRADE_RANGE, ISNULL(COLLEGE,0) AS COLLEGE,ISNULL(SESSION,0) AS SESSION,ISNULL(FEES_PAID,0) AS FEES_PAID,ISNULL(RELATIVE,0) AS RELATIVE,ISNULL(ABSOLUTE,0) AS ABSOLUTE, ISNULL(BARCODE,0) AS BARCODE, ISNULL(FEEDBACK,0) AS FEEDBACK,ISNULL(ATTENDANCE,0) AS ATTENDANCE,ISNULL(INTSUBEXAM,0) AS INTSUBEXAM,ISNULL(INTASSESSMENT,0) AS INTASSESSMENT,ISNULL(INTEXCELFORMAT,0) AS INTEXCELFORMAT,ISNULL(ADMISSION_STATUS,0) AS ADMISSION_STATUS,ISNULL(MULTIPALE_END_SEM_COMPONENT,0) AS MULTIPALE_END_SEM_COMPONENT,ISNULL(ADMIN_EX_APPROVAL,0) AS ADMIN_EX_APPROVAL,ISNULL(ADMIN_COMPONENT,0) AS ADMIN_COMPONENT,ISNULL(FACULTY_COMPONENT,0) AS FACULTY_COMPONENT", "", "");


    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        string[] arr_rdIds = { "chk_Reg", "chk_ExamRule", "chk_GraceRule", "chk_LateFee", "chk_Improvement", "chk_ExamPattern", "chk_Revaluation_Process", "chk_ResultPublish", "chk_Condonation", "chk_Decode", "chk_SeatNumber", "chk_MarkEnrtyExcel", "chk_Section", "chk_Batch", "chk_grade_admin", "chk_grade_faculty", "chkGraph", "chk_chgrange", "chk_college", "chk_session", "chk_feescollection", "chk_relative", "chk_absolute", "chk_barcode", "chk_feedback", "chk_attendance", "chk_intsubexam", "chk_intassessment", "chk_intexcelformat", "chk_admissionstatus", "chk_endsemcomponent", "chk_adminexapproval", "chk_admincomponent", "chk_facultycomponent" };
    //        int arr_val = 0;
    //        string str = "$(document).ready(function(){";
    //        string val;
    //        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
    //        {
    //            for (int j = 0; j <= arr_rdIds.Length - 1; j++)
    //            {
    //                if (Convert.ToInt32(ds.Tables[0].Rows[i][j]) == 1)
    //                {
    //                    val = "true";
    //                    txtAttendance.Visible = true;
    //                }
    //                else
    //                {
    //                    val = "false";

    //                }
    //                str += "$('#" + arr_rdIds[arr_val] + "').prop('checked'," + val + ");";
    //                arr_val++;
    //            }
    //            arr_val = 0;
    //        }
    //        str += "});";
    //        //string script = str.ToLower();
    //        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str + "", true);

    //    }

    //    //int Atten;
    //    int percent;
    //    percent = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ATTENDANCE_PERCENTAGE", ""));
    //    //Atten = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ATTENDANCE", ""));

    //    txtAttendance.Text = percent.ToString();
    //    //if (Atten == 1)
    //    //{
    //    //    txtAttendance.Visible = true;
    //    //}
    //    //else
    //    //{
    //    //    txtAttendance.Visible = false;
    //    //}

    //}
    private void BindView()
    {

        DataSet ds = objCommon.FillDropDown("ACD_EXAM_CONFIGURATION", "EXAM_REGISTRATION", "EXAM_RULE,GRACE_RULE,LATE_FEE,IMPROVEMENT,EXAM_PATTERN,REVALUATION_PROCESS,RESULT_PUBLISH,CONDONATION,ISNULL(DECODE_NUMBER,0)AS DECODE_NUMBER,ISNULL(SEAT_NUMBER,0)AS SEAT_NUMBER,ISNULL(ExcelMarkEntry,0)AS ExcelMarkEntry,ISNULL(SEC_TIMETABLE,0) AS SEC_TIMETABLE,ISNULL(BATCH_TIMETABLE,0) AS BATCH_TIMETABLE,ISNULL(GRADE_ADMIN,0) AS GRADE_ADMIN,ISNULL(GRADE_FACULTY,0) AS GRADE_FACULTY,ISNULL(GRAPH,0) AS GRAPH,ISNULL(GRADE_RANGE,0) AS GRADE_RANGE, ISNULL(COLLEGE,0) AS COLLEGE,ISNULL(SESSION,0) AS SESSION,ISNULL(FEES_PAID,0) AS FEES_PAID,ISNULL(RELATIVE,0) AS RELATIVE,ISNULL(ABSOLUTE,0) AS ABSOLUTE, ISNULL(BARCODE,0) AS BARCODE, ISNULL(FEEDBACK,0) AS FEEDBACK,ISNULL(ATTENDANCE,0) AS ATTENDANCE,ISNULL(INTSUBEXAM,0) AS INTSUBEXAM,ISNULL(INTASSESSMENT,0) AS INTASSESSMENT,ISNULL(INTEXCELFORMAT,0) AS INTEXCELFORMAT,ISNULL(ADMISSION_STATUS,0) AS ADMISSION_STATUS,ISNULL(MULTIPALE_END_SEM_COMPONENT,0) AS MULTIPALE_END_SEM_COMPONENT,ISNULL(ADMIN_EX_APPROVAL,0) AS ADMIN_EX_APPROVAL,ISNULL(ADMIN_COMPONENT,0) AS ADMIN_COMPONENT,ISNULL(FACULTY_COMPONENT,0) AS FACULTY_COMPONENT,ISNULL(EXCELUPLOAD_TIMETABLE,0) AS EXCELUPLOAD_TIMETABLE,ISNULL(INTMARKPUBLISH,0) AS INTMARKPUBLISH,ISNULL(COMPONENTWISE_EXCEL,0) AS COMPONENTWISE_EXCEL,ISNULL(SUBJECT_FOR_MARK,0) AS SUBJECT_FOR_MARK", "", "");
        if (ds != null && ds.Tables.Count > 0)
        {
            string[] arr_rdIds = { "chk_Reg", "chk_ExamRule", "chk_GraceRule", "chk_LateFee", "chk_Improvement", "chk_ExamPattern", "chk_Revaluation_Process", "chk_ResultPublish", "chk_Condonation", "chk_Decode", "chk_SeatNumber", "chk_MarkEnrtyExcel", "chk_Section", "chk_Batch", "chk_grade_admin", "chk_grade_faculty", "chkGraph", "chk_chgrange", "chk_college", "chk_session", "chk_feescollection", "chk_relative", "chk_absolute", "chk_barcode", "chk_feedback", "chk_attendance", "chk_intsubexam", "chk_intassessment", "chk_intexcelformat", "chk_admissionstatus", "chk_endsemcomponent", "chk_adminexapproval", "chk_admincomponent", "chk_facultycomponent", "chk_timetableexcel", "chk_intmarkpublish", "chk_compwiseexcel" };
            int arr_val = 0;
            string str = "$(document).ready(function(){";
            string val;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= arr_rdIds.Length - 1; j++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i][j]) == 1)
                    {
                        val = "true";
                        txtAttendance.Visible = true;
                    }
                    else
                    {
                        val = "false";

                    }
                    str += "$('#" + arr_rdIds[arr_val] + "').prop('checked'," + val + ");";
                    arr_val++;
                }
                arr_val = 0;
            }
            str += "});";
            //string script = str.ToLower();

            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str + "", true);


            DataRow dr = ds.Tables[0].Rows[0];
            string userTypes = dr["SUBJECT_FOR_MARK"] == DBNull.Value ? string.Empty : dr["SUBJECT_FOR_MARK"].ToString();
            string[] userTypeIds = userTypes.Split(',');

            for (int i = 0; i < userTypeIds.Length; i++)
            {
                foreach (ListItem item in chkListSubjectTypes.Items)
                {
                    if (item.Value == userTypeIds[i])
                        item.Selected = true;
                }
            }



        }

        //int Atten;
        int percent;
        percent = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ATTENDANCE_PERCENTAGE", ""));
        //Atten = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ATTENDANCE", ""));

        txtAttendance.Text = percent.ToString();
        //if (Atten == 1)
        //{
        //    txtAttendance.Visible = true;
        //}
        //else
        //{
        //    txtAttendance.Visible = false;
        //}

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Exam_Configue.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Exam_Configue.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        //int OrgId = Convert.ToInt32(Session["OrgId"]);
        //int orgid = Convert.ToInt32(Session["OrgId"]);
        int examreg = 0;
        int examrule = 0;
        int garcerule = 0;
        int latefee = 0;
        int Improvement = 0;
        int exampattern = 0;
        int revaluation = 0;
        int resultpublish = 0;
        int condonation = 0;
        int feetype = 0;
        int passrule = 0;
        int decode = 0;
        int seatno = 0;
        int excelmark = 0, sectnowise = 0, batchwise = 0;
        int Grade_Admin = 0;
        int Grade_Faculty = 0;
        int graph = 0;
        int change_range = 0;
        int college = 0;
        int session = 0;
        int feescollection = 0;
        int relative = 0;
        int absolute = 0;
        int barcode = 0;
        int feedback = 0;
        int attendance = 0;
        int attendance_percentage = 0;
        String atten = txtAttendance.Text;
        int intsubexam = 0;
        int intassessment = 0;
        int intexcelformat = 0;
        int admissionstatus = 0;
        int endsemcomponent = 0;
        int adminexapproval = 0;
        int admincomponent = 0;
        int facultycomponent = 0;
        int exceltimetable = 0;
        int intmarkpublish = 0;
        int compwiseexcel = 0;
        string subjecttype = string.Empty;


        foreach (ListItem item in chkListSubjectTypes.Items)
        {
            if (item.Selected)
            {
                if (subjecttype.Length > 0)
                    subjecttype += ",";

                subjecttype += item.Value;
            }
        }



        //examrule,garcerule,latefee,Improvement,exampattern,revaluation,resultpublish,condonation,feetype
        if (hdfexamregister.Value == "true")
        {
            examreg = 1;
        }
        //if (chk_Reg.Checked == true)
        //{
        //    examrule = 1;
        //}
        if (hdfexamrule.Value == "true")
        {
            examrule = 1;
            // pnlrule.Visible = true;
        }

        if (hdfgarcerule.Value == "true")
        {
            garcerule = 1;
        }
        if (hdflatefee.Value == "true")
        {
            latefee = 1;
        }
        if (hdfImprovement.Value == "true")
        {
            Improvement = 1;
        }
        if (hdfexampattern.Value == "true")
        {
            exampattern = 1;
        }

        if (hdfrevaluation.Value == "true")
        {
            revaluation = 1;
        }
        if (hdfresultpublish.Value == "true")
        {
            resultpublish = 1;
        }
        if (hdfcondonation.Value == "true")
        {
            condonation = 1;
        }

        if (hdfdecodenos.Value == "true")
        {
            decode = 1;
        }
        if (hdfSeatno.Value == "true")
        {
            seatno = 1;
        }

        if (hdfadmingrade.Value == "true")
        {
            Grade_Admin = 1;
        }
        if (hdfFacgrade.Value == "true")
        {
            Grade_Faculty = 1;
        }
        else
        {
            Grade_Faculty = 0;
        }


        if (hdfmarkentryexcel.Value == "true")
        {
            excelmark = 1;
        }
        if (hdfsection.Value == "true")
        {
            sectnowise = 1;
        }
        if (hdfbatch.Value == "true")
        {
            batchwise = 1;
        }
        if (hdfgraph.Value == "true")
        {
            graph = 1;
        }
        //else { 
        //    graph = 0;
        //}
        if (hdfrange.Value == "true")
        {
            change_range = 1;
        }
        else
        {
            change_range = 0;
        }
        if (hdfcollege.Value == "true")
        {
            college = 1;
        }
        if (hdfsession.Value == "true")
        {
            session = 1;
        }
        if (hdffeescollection.Value == "true")
        {
            feescollection = 1;
        }
        if (hdfrelative.Value == "true")
        {
            relative = 1;
        }
        if (hdfabsolute.Value == "true")
        {
            absolute = 1;
        }
        if (hdfbarcode.Value == "true")
        {
            barcode = 1;
        }
        if (hdffeedback.Value == "true")
        {
            feedback = 1;
        }
        if (hdfattendance.Value == "true")
        {
            attendance = 1;

            if (atten == "")
            {
                attendance_percentage = 0;
            }
            else
            {
                if (int.TryParse(txtAttendance.Text, out attendance_percentage))
                {
                    attendance_percentage = Convert.ToInt32(txtAttendance.Text);
                }
                else
                {
                    attendance_percentage = 0;
                }
            }



        }
        if (hdfintsubexam.Value == "true")
        {
            intsubexam = 1;
        }
        if (hdfintassessment.Value == "true")
        {
            intassessment = 1;
        }
        if (hdfintexcelformat.Value == "true")
        {
            intexcelformat = 1;
        }
        if (hdfadmissionstatus.Value == "true")
        {
            admissionstatus = 1;
        }
        if (hdfendsemcomponent.Value == "true")
        {
            endsemcomponent = 1;
        }
        if (hdfadminexapproval.Value == "true")
        {
            adminexapproval = 1;
        }
        if (hdfadmincomponent.Value == "true")
        {
            admincomponent = 1;
        }
        if (hdffacultycomponent.Value == "true")
        {
            facultycomponent = 1;
        }

        if (hdftimetableexcel.Value == "true")
        {
            exceltimetable = 1;
        }
        if (hdfintmarkpublish.Value == "true")
        {
            intmarkpublish = 1;
        }
        if (hdfcompwiseexcel.Value == "true")
        {
            compwiseexcel = 1;
        }

        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype);
        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0);
        //added by Injamam For batch and section
        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0, excelmark, sectnowise, batchwise, Grade_Admin, Grade_Faculty, graph, change_range, college, session, feescollection, relative, absolute, barcode, feedback, attendance, attendance_percentage, intsubexam, intassessment, intexcelformat, admissionstatus, endsemcomponent, adminexapproval);

        ////added by Hitesh 06122023
        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0, excelmark, sectnowise, batchwise, Grade_Admin, Grade_Faculty, graph, change_range, college, session, feescollection, relative, absolute, barcode, feedback, attendance, attendance_percentage, intsubexam, intassessment, intexcelformat, admissionstatus, endsemcomponent, adminexapproval, admincomponent, facultycomponent);

        //EXCEL TIMETABLE,INTERNAL MARK PUBLISH, COMPONENTWISE EXCEL FLAG ADDED AS PER THE REQUIREMENT.
        CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0, excelmark, sectnowise, batchwise, Grade_Admin, Grade_Faculty, graph, change_range, college, session, feescollection, relative, absolute, barcode, feedback, attendance, attendance_percentage, intsubexam, intassessment, intexcelformat, admissionstatus, endsemcomponent, adminexapproval, admincomponent, facultycomponent, exceltimetable, intmarkpublish, compwiseexcel, subjecttype);

        if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
        {
            objCommon.DisplayMessage("Record Save Sucessfully.... !", this.Page);
            //clear();
            BindView();
        }
        else
        {
            objCommon.DisplayMessage("Something went wrong ..Please try again !", this.Page);
        }
        //BindSubjectType();
        //clear();
    }

    private void BindSubjectType()
    {
        DataSet dslist = null;
        dslist = exam.GetSubjectType();
        if (dslist != null && dslist.Tables.Count > 0 && dslist.Tables[0].Rows.Count > 0)
        {
            lvBinddata.Visible = true;
            lvBinddata.DataSource = dslist;
            lvBinddata.DataBind();
            //btnuploadDocuments.Enabled = true;
        }
        else
        {
            lvBinddata.Visible = false;
            lvBinddata.DataSource = null;
            lvBinddata.DataBind();
            //btnuploadDocuments.Enabled = false;
        }

    }
    protected void rdbcomman_CheckedChanged(object sender, EventArgs e)
    {
        CHECK();
        pnlBind.Visible = false;
    }

    protected void clear()
    {
        pnlBind.Visible = false;
        // pnlChoice.Visible = false;
        //rdb_coursewise.Checked = false;
        //rdb_Creditwise.Checked = false;
        //rdb_nofee.Checked = false;
        // rdbyes.Checked = false;
        // rdbno.Checked = false;
        // rdbSbjecttype.Checked = false;
        // rdbcomman.Checked = false;
        hdfcondonation.Value = "";
        hdfexampattern.Value = "";
        hdfexamregister.Value = "";
        hdfexamrule.Value = "";
        hdfgarcerule.Value = "";
        hdfImprovement.Value = "";
        hdflatefee.Value = "";
        hdfresultpublish.Value = "";
        hdfrevaluation.Value = "";
        hdfSeatno.Value = "";
        hdfdecodenos.Value = "";
        hdfmarkentryexcel.Value = "";
        hdfsection.Value = "";
        hdfbatch.Value = "";
        hdfcollege.Value = "";
        hdfsession.Value = "";
        hdffeescollection.Value = "";
        hdfrelative.Value = "";
        hdfabsolute.Value = "";
        hdfbarcode.Value = "";
        hdffeedback.Value = "";
        hdfattendance.Value = "";
        hdfintsubexam.Value = "";
        hdfintassessment.Value = "";
        hdfintexcelformat.Value = "";
        hdfadmissionstatus.Value = "";
        hdfendsemcomponent.Value = "";
        hdfadmincomponent.Value = "";
        hdfadminexapproval.Value = "";
        hdffacultycomponent.Value = "";
        hdftimetableexcel.Value = "";
        hdfintmarkpublish.Value = "";
        hdfcompwiseexcel.Value = "";
        BindView();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlBind.Visible = false;
        //  pnlChoice.Visible = false;
        //rdb_coursewise.Checked = false;
        //rdb_Creditwise.Checked = false;
        //rdb_nofee.Checked = false;
        // rdbyes.Checked = false;
        //rdbno.Checked = false;
        // rdbSbjecttype.Checked = false;
        // rdbcomman.Checked = false;
        BindView();
    }

    private void CHECK()
    {
        //MULTIPALE_END_SEM_COMPONENT,ADMIN_COMPONENT,FACULTY_COMPONENT
        DataSet ds = objCommon.FillDropDown("ACD_EXAM_CONFIGURATION", "EXAM_REGISTRATION", "EXAM_RULE,GRACE_RULE,LATE_FEE,IMPROVEMENT,EXAM_PATTERN,REVALUATION_PROCESS,RESULT_PUBLISH,CONDONATION,FEE_TYPE,PASS_RULE,MARK_ENTRY,ExcelMarkEntry,FEES_PAID,Fee_type,PASS_RULE,SEC_TIMETABLE,BATCH_TIMETABLE,COLLEGE,SESSION,FEES_COLLECTION,RELATIVE,ABSOLUTE,BARCODE,ADMIN_EX_APPROVAL,MULTIPALE_END_SEM_COMPONENT,ADMIN_COMPONENT,FACULTY_COMPONENT ", "", "");
        if (ds != null && ds.Tables.Count > 0)
        {
            string[] arr_rdIds = { "chk_Reg", "chk_ExamRule", "chk_GraceRule", "chk_LateFee", "chk_Improvement", "chk_ExamPattern", "chk_Revaluation_Process", "chk_ResultPublish", "chk_Condonation", "chk_admincomponent", "chk_facultycomponent", "chk_endsemcomponent" };
            int arr_val = 0;
            string str = "$(document).ready(function(){";
            string val;
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i][j]) == 1)
                    {
                        val = "true";
                    }
                    else
                    {
                        val = "false";
                    }

                    str += "$('#" + arr_rdIds[arr_val] + "').prop('checked'," + val + ");";
                    arr_val++;
                }
                arr_val = 0;
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", ""+str+"", true);
            str += "});";
            //string script = str.ToLower();
            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str + "", true);
        }
    }

    protected void rdlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        CHECK();
    }
}