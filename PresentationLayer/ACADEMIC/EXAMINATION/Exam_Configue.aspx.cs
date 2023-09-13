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
                BindView();
            }
            else
            {
                // Clear message div
                //divMsg.InnerHtml = string.Empty;
            }



        }
        catch (Exception ex)
        {

        }
    }

    private void BindView()
    {

        DataSet ds = objCommon.FillDropDown("ACD_EXAM_CONFIGURATION", "EXAM_REGISTRATION", "EXAM_RULE,GRACE_RULE,LATE_FEE,IMPROVEMENT,EXAM_PATTERN,REVALUATION_PROCESS,RESULT_PUBLISH,CONDONATION,ISNULL(DECODE_NUMBER,0)AS DECODE_NUMBER,ISNULL(SEAT_NUMBER,0)AS SEAT_NUMBER,ISNULL(ExcelMarkEntry,0)AS ExcelMarkEntry,ISNULL(SEC_TIMETABLE,0) AS SEC_TIMETABLE,ISNULL(BATCH_TIMETABLE,0) AS BATCH_TIMETABLE,ISNULL(GRADE_ADMIN,0) AS GRADE_ADMIN,ISNULL(GRADE_FACULTY,0) AS GRADE_FACULTY,ISNULL(GRAPH,0) AS GRAPH,ISNULL(GRADE_RANGE,0) AS GRADE_RANGE,FEE_TYPE,PASS_RULE,MARK_ENTRY,ISNULL(FEES_PAID,0) AS FEES_PAID,Fee_type,PASS_RULE ", "", "");
        if (ds != null && ds.Tables.Count > 0)
        {
            string[] arr_rdIds = { "chk_Reg", "chk_ExamRule", "chk_GraceRule", "chk_LateFee", "chk_Improvement", "chk_ExamPattern", "chk_Revaluation_Process", "chk_ResultPublish", "chk_Condonation", "chk_Decode", "chk_SeatNumber", "chk_MarkEnrtyExcel", "chk_Section", "chk_Batch", "chk_grade_admin", "chk_grade_faculty", "chkGraph", "chk_chgrange" };
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

        }

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
        int Grade_Faculty=0;
        int graph = 0;
        int change_range = 0;

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
        if (hdfrange.Value == "true")
        {
            change_range = 1;
        }


        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype);

        //CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0);
        //added by Injamam For batch and section
        CustomStatus cs = (CustomStatus)exam.Add_ExamConfiguration(examrule, garcerule, latefee, Improvement, exampattern, revaluation, resultpublish, condonation, feetype, passrule, examreg, decode, seatno, 0, excelmark, sectnowise, batchwise, Grade_Admin, Grade_Faculty, graph, change_range);
        if (Convert.ToInt32(cs) == 1 || Convert.ToInt32(cs) == 2)
        {
            objCommon.DisplayMessage("Record Save Sucessfully.... !", this.Page);
            BindView();
        }
        else
        {
            objCommon.DisplayMessage("Something went wrong ..Please try again !", this.Page);
        }
      
        //BindSubjectType();
        clear();
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


        DataSet ds = objCommon.FillDropDown("ACD_EXAM_CONFIGURATION", "EXAM_REGISTRATION", "EXAM_RULE,GRACE_RULE,LATE_FEE,IMPROVEMENT,EXAM_PATTERN,REVALUATION_PROCESS,RESULT_PUBLISH,CONDONATION,FEE_TYPE,PASS_RULE	,MARK_ENTRY,ExcelMarkEntry,FEES_PAID,Fee_type,PASS_RULE ,SEC_TIMETABLE,BATCH_TIMETABLE", "", "");
        if (ds != null && ds.Tables.Count > 0)
        {
            string[] arr_rdIds = { "chk_Reg", "chk_ExamRule", "chk_GraceRule", "chk_LateFee", "chk_Improvement", "chk_ExamPattern", "chk_Revaluation_Process", "chk_ResultPublish", "chk_Condonation", "chk_Section", "chk_Batch" };
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