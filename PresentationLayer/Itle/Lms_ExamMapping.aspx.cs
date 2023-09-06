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
using System.Linq;
using System.Data.SqlClient;
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;



public partial class Itle_Lms_ExamMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    AssignmentController objAM = new AssignmentController();
    Assignment objAssign = new Assignment();


    protected void Page_Load(object sender, EventArgs e)
    {
      
   
        if (!Page.IsPostBack)
        {
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

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
                //Page Authorization
                if (Session["Page"] == null)
                {
                    CheckPageAuthorization();
                    Session["Page"] = 1;
                }
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                BindListView();
                GETEXAMTYPE();
                Session["RecTblExamMap"] = null;
                FILLDROPDOWN();
            }
        }

        // Used to get maximum size of file attachment
        txtSyllabus.Text = Session["ICourseName"].ToString();
        txtSyllabus.Enabled = false;
        //GetAttachmentSize();
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_SyllabusMaster.aspx");
        }
    }

    protected void FILLDROPDOWN()
    {
        objCommon.FillDropDownList(ddltest, "ACD_IASSIGNMASTER", "AS_NO", "SUBJECT", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND  SESSIONNO =" + Session["SessionNo"].ToString(), "");
    
    }
    protected void GETEXAMTYPE()
    {

        DataSet ds = null;

        try
        {

            ds = objCommon.FillDropDown("ACD_SUBEXAM_NAME A inner join ACD_SCHEME B ON (A.PATTERNNO=B.PATTERNNO)INNER JOIN acd_STUDENT_RESULT C ON (B.SCHEMENO=C.SCHEMENO)INNER JOIN ACD_COURSE D ON (C.COURSENO=D.COURSENO)", "DISTINCT SUBEXAMNO", "SUBEXAMNAME", "A.SUBEXAMNAME<>'' AND D.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND C.SESSIONNO =" + Convert.ToInt32(Session["SessionNo"]), "");
            
          
            if (ds != null)
            {

                ddlexam.DataSource = ds;
                ddlexam.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlexam.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlexam.DataBind();
                ddlexam.SelectedIndex = 0;

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_Itle_Allow_Retest.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void rbtTestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtTestType.SelectedValue == "A")
        {
            objCommon.FillDropDownList(ddltest, "ACD_IASSIGNMASTER", "AS_NO", "SUBJECT", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND  SESSIONNO =" + Session["SessionNo"].ToString(), "");
        }
        else if (rbtTestType.SelectedValue == "O")
        {
            objCommon.FillDropDownList(ddltest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND  SESSIONNO =" + Session["SessionNo"].ToString()+ " AND TEST_TYPE='"+ rbtTestType.SelectedValue.ToString()+"'", "");
        }
        else if(rbtTestType.SelectedValue == "D")
        {
            objCommon.FillDropDownList(ddltest, "ACD_ITESTMASTER", "TESTNO", "TESTNAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND  SESSIONNO =" + Session["SessionNo"].ToString() + " AND TEST_TYPE='" + rbtTestType.SelectedValue.ToString() + "'", "");
   
        }
        
    }
   
    private DataTable CreateTabelExamMap()
    {
        DataTable dtexammap = new DataTable();
       
        dtexammap.Columns.Add(new DataColumn("SUBEXAM_NO", typeof(int)));
        dtexammap.Columns.Add(new DataColumn("SUBEXAM_NAME", typeof(string)));
        dtexammap.Columns.Add(new DataColumn("TEST_TYPE", typeof(string)));
        dtexammap.Columns.Add(new DataColumn("TEST_CODE", typeof(string)));
        dtexammap.Columns.Add(new DataColumn("TEST", typeof(string)));
        dtexammap.Columns.Add(new DataColumn("TEST_NO", typeof(string)));
        dtexammap.Columns.Add(new DataColumn("WEIGHTAGE", typeof(decimal)));
        dtexammap.Columns.Add(new DataColumn("Exam_SRNO", typeof(int)));
      


        return dtexammap;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {


        try
        {
            lvexammapping.Visible = true;
            if (Session["RecTblExamMap"] != null && ((DataTable)Session["RecTblExamMap"]) != null)
            {
                int maxVals = 0;
                DataTable dtexammap = (DataTable)Session["RecTblExamMap"];
                DataRow dr = dtexammap.NewRow();

                
               
                if (dr != null)
                {
                    maxVals = Convert.ToInt32(dtexammap.AsEnumerable().Max(row => row["Exam_SRNO"]));
                }
                dr["Exam_SRNO"] = maxVals + 1;
                dr["SUBEXAM_NO"] = ddlexam.SelectedValue == null ? string.Empty : Convert.ToString(ddlexam.SelectedValue);
                dr["SUBEXAM_NAME"] = ddlexam.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlexam.SelectedItem.Text.Trim());
                dr["TEST_NO"] = ddltest.SelectedValue == null ? string.Empty : Convert.ToString(ddltest.SelectedValue);
                dr["TEST"] = ddltest.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddltest.SelectedItem.Text.Trim());
                dr["WEIGHTAGE"] = txtweightage.Text == null ? string.Empty : Convert.ToString(txtweightage.Text);
                dr["TEST_CODE"] = rbtTestType.SelectedValue == null ? string.Empty : Convert.ToString(rbtTestType.SelectedValue);

                if (rbtTestType.SelectedValue == "A")
                {
                    dr["TEST_TYPE"] = "Assignment";
                }
                if (rbtTestType.SelectedValue == "O")
                {
                    dr["TEST_TYPE"] = "Objective Test";
                }
                if (rbtTestType.SelectedValue == "D")
                {
                    dr["TEST_TYPE"] = "Descriptive Test";
                }

                foreach (ListViewDataItem dataitem in lvexammapping.Items)
                {
                     HiddenField hdntestno = dataitem.FindControl("hdntestno") as HiddenField;
                     HiddenField hdnsubexamno = dataitem.FindControl("hdnsubexamno") as HiddenField;

                     string testno = hdntestno.Value;
                     string examno = hdnsubexamno.Value;
                     if (testno == ddltest.SelectedValue && examno == ddlexam.SelectedValue)
                    {
                        objCommon.DisplayMessage(UpdExams, "This Exam Mapping Is Already Exist " , this.Page);
                        return;
                    }
                }




                dtexammap.Rows.Add(dr);
                Session["RecTblExamMap"] = dtexammap;
                EXAMMAPLIST.Visible = true;
                lvexammapping.DataSource = dtexammap;
                lvexammapping.DataBind();
                ClearFeilds();
                ddlexam.Enabled = false;
             
                
            }
            else
            {

                DataTable dtexammap = this.CreateTabelExamMap();
                DataRow dr = dtexammap.NewRow();
                dr["Exam_SRNO"] = Convert.ToInt32(ViewState["Exam_SRNO"]) + 1;
                dr["SUBEXAM_NO"] = ddlexam.SelectedValue == null ? string.Empty : Convert.ToString(ddlexam.SelectedValue);
                dr["SUBEXAM_NAME"] = ddlexam.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddlexam.SelectedItem.Text.Trim());
                dr["TEST_NO"] = ddltest.SelectedValue == null ? string.Empty : Convert.ToString(ddltest.SelectedValue);
                dr["TEST"] = ddltest.SelectedItem.Text.Trim() == null ? string.Empty : Convert.ToString(ddltest.SelectedItem.Text.Trim());
                dr["WEIGHTAGE"] = txtweightage.Text == null ? string.Empty : Convert.ToString(txtweightage.Text);
                dr["TEST_CODE"] = rbtTestType.SelectedValue == null ? string.Empty : Convert.ToString(rbtTestType.SelectedValue);


                if (rbtTestType.SelectedValue == "A")
                {
                    dr["TEST_TYPE"] = "Assignment";
                }
                if (rbtTestType.SelectedValue == "O")
                {
                    dr["TEST_TYPE"] = "Objective Test";
                }
                if (rbtTestType.SelectedValue == "D")
                {
                    dr["TEST_TYPE"] = "Descriptive Test";
                }

                ViewState["Exam_SRNO"] = Convert.ToInt32(ViewState["Exam_SRNO"]) + 1;
                dtexammap.Rows.Add(dr);
                Session["RecTblExamMap"] = dtexammap;
                EXAMMAPLIST.Visible = true;
                lvexammapping.DataSource = dtexammap;
                lvexammapping.DataBind();
                ClearFeilds();
                ddlexam.Enabled = false;
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


    protected void ClearFeilds()
    {

        ddltest.SelectedValue = "0";
        //ddlexam.SelectedValue = "0";
        txtweightage.Text = string.Empty;
        rbtTestType.SelectedValue = "A";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

        ClearFeilds();
        ddlexam.Enabled = true;
        ddlexam.SelectedValue = "0";
        Session["RecTblExamMap"] = null;
        EXAMMAPLIST.Visible = false;

    
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        double tot1 = 0;
        foreach (ListViewDataItem lv in lvexammapping.Items)
        {
            Label lblweightage = lv.FindControl("lblweightage") as Label;

            tot1 += Convert.ToDouble(lblweightage.Text);
           
        }

        if (tot1 == 100)
        {
            //objAssign.COURSENO = Session["SESSION_NAME"].ToString();
            objAssign.SESSIONNO = Convert.ToInt32(Session["SessionNo"]);
            objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAssign.OrganizationId = Convert.ToInt32(Session["OrgId"]);
            objAssign.ExamMapTbl = Session["RecTblExamMap"]as DataTable;

            int SR_NO = Convert.ToInt32(ViewState["SR_NO"]);

           CustomStatus cs = (CustomStatus)objAM.AddExamMapping(objAssign,SR_NO); 
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(UpdExams, "Record Saved  Successfully...", this.Page);
                ClearFeilds();
                BindListView();
                ddlexam.Enabled = true;
                ddlexam.SelectedValue = "0";
                Session["RecTblExamMap"] = null;
                EXAMMAPLIST.Visible = false;

            }

        }
        else
        {
            objCommon.DisplayUserMessage(UpdExams, "Weightage should be 100%.....", this.Page);
            return;
        }
    }


    private DataRow GetEditableDatarowFromStudentAttendance(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Exam_SRNO"].ToString() == value)
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

    protected void BindListView()
    {
        int sessionno = Convert.ToInt32(Session["SessionNo"]);
      int courseno =  Convert.ToInt32(Session["ICourseNo"]);

        DataSet ds = objAM.GetAllExamMappingByCourseNo(sessionno,courseno);
        MainMappingList.DataSource = ds;
        MainMappingList.DataBind();
    }
    
    protected void btnremovemapping_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnremovemapping = sender as ImageButton;
            DataTable  dtexamtable;
          
            dtexamtable = ((DataTable)Session["RecTblExamMap"]);

            DataRow dr = this.GetEditableDatarowFromStudentAttendance(dtexamtable, btnremovemapping.CommandArgument);

            dtexamtable.Rows.Remove(dr);
            Session["RecTblExamMap"] = dtexamtable;
            lvexammapping.DataSource = dtexamtable;
            lvexammapping.DataBind();
        }



        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "EMP_APPRAISAL_TRANSACTION_EmployeeAppraisalForm.btnEditResearchGuidance_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton btnEdit = sender as ImageButton;
        int SR_NO = int.Parse(btnEdit.CommandArgument);

        ViewState["SR_NO"] = SR_NO;

        DataSet ds = objAM.GetSingleExamMappingData(SR_NO, Convert.ToInt32(Session["sessionno"].ToString()), Convert.ToInt32(Session["ICourseNo"]));

        lvexammapping.DataSource = ds;
        lvexammapping.DataBind();
        ddlexam.SelectedValue = ds.Tables[0].Rows[0]["SUBEXAM_NO"].ToString();
        ddlexam.Enabled = false;

        Session["RecTblExamMap"] = ds.Tables[0];
        DataTable dtexamtable = (DataTable)Session["RecTblExamMap"];


        EXAMMAPLIST.Visible = true;
        lvexammapping.Visible = true;
        ViewState["action"] = "edit";
           

    }

    //protected void ShowDetail(int srno,int courseno,int sessionno)
    //{

    //   DataSet ds=  objAM.GetSingleExamMappingData(srno, courseno, sessionno);

    //   lvexammapping.DataSource = ds;
    //   lvexammapping.DataBind();
    //}

   
}