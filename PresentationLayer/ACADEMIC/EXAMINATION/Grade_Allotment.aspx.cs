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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Services;
using System.Collections.Generic;
using Newtonsoft.Json;

using ClosedXML.Excel;
using System.Data.OleDb;

using System.IO;
//using Microsoft.WindowsAzure.Storage;
////using Microsoft.WindowsAzure;
//using Microsoft.WindowsAzure.Storage.Blob;


public partial class ACADEMIC_EXAMINATION_Grade_Allotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    ResultProcessing objResultProcessing = new ResultProcessing();
    ExamController objexam = new ExamController();

    Exam objExam = new Exam();
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
                    // this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    int OrgId = Convert.ToInt32(objCommon.LookUp("REFF", "OrganizationId", ""));
                  
                    // test.Visible = false;


                     if (Convert.ToInt32(Session["OrgId"]) == 5)
                     {
                         divgradetype.Visible = true;
                         ddlgradetype.Visible = true;

                     }
                     else
                     {
                         divgradetype.Visible = false;
                         ddlgradetype.Visible = false;
                     }

                   
                   
              

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                        // lvGradeRange.DataSource = null;
                        //   lvGradeRange.DataSource = null;
                        //  lvGradeRange.DataBind();

                        //lvGradeRange.Visible = true;
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    this.FillDropdown();
                    // lvGrade.DataSource = null;
                    // lvGrade.Visible = true;
                    //   lvGradeAllotment.DataBind();
                    //lvGradeAllotment.Visible = true;
                    //listview();

                }
            }
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_Grade_Allotment.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=GradeAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=GradeAllotment.aspx");
        }
    }
    private void FillDropdown()
    {
        try
        {
         
            objCommon.FillDropDownList(ddlcolgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
            // COMMENT FOT testc DATABASE DATABASE mISS MATCH

            //ADDED BY PRAFULL ON DT 17032023 FOR SESSIONIDWISE
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION", "SESSIONID", "SESSION_NAME", "SESSIONID>0 AND IS_ACTIVE=1", "SESSIONID DESC");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_GradeAllotment.FillDropdown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #region COLLEGE NAME & SCHEMA SelectedIndexChanged
    protected void ddlcolgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcolgname.SelectedIndex > 0)
        {

            Common objCommon = new Common();

            if (ddlcolgname.SelectedIndex > 0)
            {
                //Common objCommon = new Common();
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcolgname.SelectedValue));
                //ViewState["degreeno"]

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                    hdfnscheme.Value=ViewState["schemeno"].ToString();
                   // objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");


                }

                objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON A.SESSIONNO=SM.SESSIONNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED=1 AND A.SCHEMENO="+Convert.ToInt32(ViewState["schemeno"])+" AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue), "S.SEMESTERNO");
            }
        }

        else
        {
            lvGradeAllotment.DataSource = null;
            lvGradeAllotment.DataBind();
            lvGradeAllotment.Visible = false;
            ddlCourse.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
        }





    }
    #endregion
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            int session = Convert.ToInt32(ddlSession.SelectedValue);


            objCommon.FillDropDownList(ddlcolgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID) INNER JOIN ACD_STUDENT_RESULT SR ON SR.SCHEMENO=SM.SCHEMENO", "DISTINCT COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SR.SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID="+ddlSession.SelectedValue+") AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");

            //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");

            objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_SESSION_MASTER SM ON A.SESSIONNO=SM.SESSIONNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED=1 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue), "S.SEMESTERNO");

            //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT", "distinct COURSENO", "(CCODE +' - '+CourseName)As name", "EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]), "COURSENO");

            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON (SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT CCODE", "(CCODE +' - '+CourseName)As name", "EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 AND SM.SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "CCODE");

        }
        else {

            lvGradeAllotment.DataSource = null;
            lvGradeAllotment.DataBind();
            lvGradeAllotment.Visible = false;
            ddlCourse.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;



        
        
        }
    }
    private void MainSubExamBind(DropDownList ddlList, DataSet ds)
    {
        ddlList.Items.Clear();
        ddlList.Items.Add("Please Select");
        ddlList.SelectedItem.Value = "0";

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlList.DataSource = ds;
            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlList.DataBind();
            ddlList.SelectedIndex = 0;
        }
    }
    protected void ddlsem_OnselectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            
            //objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT", "DISTINCT COURSENO", "(COURSENAME + ' - ('+CCODE+')') AS COURSENAME ", "EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0 and  SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SEMESTERNO=" + ddlSemester.SelectedValue, "");

            //added by prafull on dt 17032023

            if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
            {
                ViewState["schemeno"] = "0";
            }

            objCommon.FillDropDownList(ddlCourse, "ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON(SM.SESSIONNO=SR.SESSIONNO)", "DISTINCT CCODE", "(CCODE +' - '+CourseName)As name", "EXAM_REGISTERED=1 AND ISNULL(CANCEL,0)=0  AND SR.SEMESTERNO=" + ddlSemester.SelectedValue + " AND (SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " OR " + Convert.ToInt32(ViewState["schemeno"]) + "=0) AND SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "CCODE");
            
        }
        else {

            lvGradeAllotment.DataSource = null;
            lvGradeAllotment.DataBind();
            lvGradeAllotment.Visible = false;
            ddlCourse.SelectedIndex = 0;
        
        }

    }
    protected void OnClick_Grade_Allotment(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["OrgId"]) == 5)
            {
                int lockstatus = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT GP INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=GP.SESSIONNO", "DISTINCT ISNULL(LOCKED_STATUS,0)", "CCODE='" + ddlCourse.SelectedValue + "' AND SM.SESSIONID=" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue));

                if (lockstatus == 0)
                {
                    objCommon.DisplayMessage(updpnlExam, "Grade Range is Not Lock..!! Please Lock the Grade range Before Grade Allotment..!!", this.Page);
                    return;
                }
            }


            int count = 0;
            string ccode = string.Empty;
            if (lvGradeAllotment.Items.Count > 0)
            {
                foreach (ListViewDataItem item in lvGradeAllotment.Items)
                {

                    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = item.FindControl("lblCourseName") as Label;
                    if (CheckId.Checked == true)
                    {
                        count++;
                        //ccode += lblCCode.ToolTip.Replace(" ", string.Empty) + '$';
                        ccode += lblCCode.ToolTip + '$';
                        //ccode += lblCCode.ToolTip.Replace(" ", string.Empty);
                    }
                }
                if (count == 0)
                {
                    objCommon.DisplayMessage(updpnlExam,"Please Select Atleast one Subject from the list", this.Page);
                    return;
                }
            }

            if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
            {
                ViewState["schemeno"] = "0";
            }
            //PKG_ACAD_RELATIVE_GRADE_ALLOTMENT_ATLAS
            string SP_Name = "PKG_ACAD_RELATIVE_GRADE_ALLOTMENT_ATLAS_CC";
            string SP_Parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO,@P_OP";
            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + 0 + "," + ddlSemester.SelectedValue + "," + ddlCourse.SelectedValue + "," + 0;
            string retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            if (retStatus == "1")
            {
                // divgraph.Visible = false;
                objCommon.DisplayMessage(updpnlExam, "Grade Allot Sucessfully", this.Page);
                BindGradeView();
                BindStudentlist();
                btnReRange.Visible = false;
                //btnRangeLock.Visible = false;

            }
            else
            {
                objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);
                return;
            }

        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objUaimsCommon.ShowError(Page, "Academic_Examination_Grade_Allotment.btnsave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void OnClick_Cancle(object sender, EventArgs e)
    {

        ddlcolgname.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        lblTotalStudent.InnerText = string.Empty;
        lblFemaleCount.InnerText = string.Empty;
        lblMaleCount.InnerText = string.Empty;
        lvGradeAllotment.Visible = false;
        lvGradeRange.Visible = false;
        lvStudentDetails.Visible = false;      
        divpower.Visible = false;
        btnmodifypowerfactor.Visible = false;
        btnRangeLock.Visible = true;


    }
    protected void ddlCourse_OnselectedIndexChanged(object sender, EventArgs e)
    {
        lvGradeRange.Visible = false;
        lvGradeAllotment.Visible = false;
        if (ddlCourse.SelectedIndex > 0)
        {

            DataSet dsSubjects = null;
            DataSet dsGdpoints = null;
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcolgname.SelectedValue));
            //ViewState["degreeno"]

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            }
        
            string SubjectCount = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SR.SESSIONNO ", "COUNT(DISTINCT COURSENO)", "SESSIONID=" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + "and isnull(Cancel,0)=0 and isnull(EXAM_REGISTERED,0)=1");
            lblTotalStudent.InnerText = SubjectCount;
            string GradAllotmentDone = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SR.SESSIONNO ", "COUNT(ISNULL(GRADE,0))", "SESSIONID=" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND GRADE IS NOT NULL and isnull(Cancel,0)=0 and isnull(EXAM_REGISTERED,0)=1");

            lblFemaleCount.InnerText = GradAllotmentDone;
            string GradAllotmentNotDone = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SR.SESSIONNO ", "COUNT(ISNULL(GRADE,0))", "SESSIONID=" + ddlSession.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + "AND GRADE IS NULL and isnull(Cancel,0)=0 and isnull(EXAM_REGISTERED,0)=1");

            lblMaleCount.InnerText = GradAllotmentNotDone;
            StudentController objSC = new StudentController();
            dsGdpoints = objCommon.FillDropDown("ACD_GRADE_POINT GP INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=GP.SESSIONNO", "MINMARK,MAXMARK,POINT,GRADE_NAME", "", " SESSIONID=" + ddlSession.SelectedValue + "AND CCODE='" + ddlCourse.SelectedValue + "'", "");

            lvStudentDetails.DataSource = null;
            lvStudentDetails.DataBind();
            lvStudentDetails.Visible = false;
            lvGradeRange.DataSource = null;
            lvGradeRange.DataBind();
            lvGradeAllotment.Visible = false;


            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
            {
                divpower.Visible =false;
                btnmodifypowerfactor.Visible = false;
                btnRangeLock.Visible = true;
               
            }
            else
            {
                divpower.Visible = false;
                btnmodifypowerfactor.Visible = false;
                btnRangeLock.Visible = true;
                btnRangeLock.Enabled = true;
                btnReRange.Visible = false;
                btnRangeLock.Visible = true;
                btnRangrlock.Visible = false;
                btnRangrUnlock.Visible = false;

                
            }
        }
        else 
        {

            lvStudentDetails.DataSource = null;
            lvStudentDetails.DataBind();
            lvStudentDetails.Visible = false;

            lvGradeAllotment.DataSource = null;
            lvGradeAllotment.DataBind();
            lvGradeAllotment.Visible = false;

            lvGradeRange.DataSource = null;
            lvGradeRange.DataBind();
            lvGradeAllotment.Visible = false;
            //listview();OnClick_Grade_RangeLock
        }
    }
    protected void listview()
    {
       //DataSet ds = objCommon.FillDropDown("ACD_GRADE_POINT", "MINMARK,MAXMARK,GRADE_NAME,POINT", "", "","");
       //   lvGradeRange.DataSource = ds;
       //  lvGradeRange.DataBind();
          DataSet dsGdpoints = null;
        dsGdpoints = objCommon.FillDropDown("ACD_GRADE_POINT", "TOP 1  0 AS SESSIONNO, 0 AS CCODE,0 AS GRADE_NAME,0 AS GRADE_NAME1", "", "", "");
        lvGradeRange.DataSource = dsGdpoints;
        lvGradeRange.DataBind();
    }
    protected void OnClick_ShowCourse(object sender, EventArgs e)
    {
        DataSet dsSubjects = null;

        if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
        {
            ViewState["schemeno"] = "0";
        }

        //PKG_ACD_GRAD_ALLOTMENT_SHOW
         string proc_name = "PKG_ACD_GRAD_ALLOTMENT_SHOW_CC";
         string para_name = "@P_SESSIONNO,@P_SEMESTERNO,@P_SCHEMENO,@P_COURSENO ";
         string call_values = "" + ddlSession.SelectedValue + "," + ddlSemester.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + ddlCourse.SelectedValue;
         dsSubjects = objCommon.DynamicSPCall_Select(proc_name, para_name, call_values);
         if (dsSubjects.Tables[0].Rows.Count > 0)
         {
   
             lvGradeAllotment.DataSource = dsSubjects;
             lvGradeAllotment.DataBind();
             lvGradeAllotment.Visible = true;
          
         }
        #region
       #endregion
         BindGradeView();
         BindStudentlist();
}
    protected void OnClick_Grade_RangeLock(object sender, EventArgs e)
    {
        try
        {
            int marktotnotcal = 0;
            marktotnotcal = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(*)", "SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND CCODE='" + ddlCourse.SelectedValue + "' AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND ISNULL(CANCEL,0)=0 AND EXAM_REGISTERED=1 AND MARKTOT IS NULL"));
             
            if (marktotnotcal > 0)
            {
                objCommon.DisplayMessage(updpnlExam, "Please Check the Mark Entry is completed or not for selected Course", this.Page);
                return;
            }
            int count = 0;
     
            string ccode = string.Empty;
            if (lvGradeAllotment.Items.Count > 0)
            {
                foreach (ListViewDataItem item in lvGradeAllotment.Items)
                {
                    CheckBox CheckId = item.FindControl("chkAccept") as CheckBox;
                    Label lblCCode = item.FindControl("lblCourseName") as Label;
                    if (CheckId.Checked == true)
                    {
                        count++;
                        ccode += lblCCode.ToolTip + '$';
                    }
                }
                if (count == 0)
                {
                    objCommon.DisplayMessage(updpnlExam,"Please Select Atleast one Subject from the list", this.Page);
                    return;
                }
            }
            DataSet dsGdpoints = null;
    
            int gradecount= Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT","COUNT(*)","SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND SEMESTERNO="+Convert.ToInt32(ddlSemester.SelectedValue)+" AND CCODE='" + ddlCourse.SelectedValue + "'"));
            if (gradecount > 0)
            {
                btnRangeLock.Enabled = false;

            }
            else
            {
                btnRangeLock.Enabled = true;
            }
            
            string SP_Name = string.Empty;
            string SP_Parameters = string.Empty;
            string Call_Values = string.Empty;
            string retStatus = string.Empty;

            if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
            {
                ViewState["schemeno"] = "0";
            }

            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
            {
                 SP_Name = "PKG_EXAM_POWER_FACTOR_CALCULATION_CPU";
                 SP_Parameters = "@P_SESSIONID,@P_CCODE,@P_SCHEMENO,@P_SEMESTERNO,@P_OP";        
                 Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlCourse.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + 0 ;
                 retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            }
            else
            {
                //PKG_ACAD_GRADE_CUTTOFF_CAL_RELATIVE
                 SP_Name = "PKG_ACAD_GRADE_CUTTOFF_CAL_RELATIVE_CC";
                 SP_Parameters = "@P_SESIONNO,@P_CCODE,@P_SCHEMENO,@P_SEMESTERNO,@P_UA_NO,@P_PREV_STATUS,@P_GRADE_TYPE,@P_OUT";
                 Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlCourse.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + 0 + "," + ddlgradetype.SelectedValue + "," + 0;
                 retStatus = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
            }
            if (retStatus == "1")
            {
                objCommon.DisplayMessage(updpnlExam,"Grade Range Genearted Sucessfully", this.Page);
                BindGradeView();
                BindStudentlist();
            }
            else if (retStatus == "0")
            {
                objCommon.DisplayMessage(updpnlExam, "Mark Entry is Not Completed or Grade is not defined...", this.Page);
                return;
            }
            else if (retStatus == "4")
            {
                objCommon.DisplayMessage(updpnlExam, "Mark Entry is Not Completed or Grade is not defined...", this.Page);
                return;
            }
            else 
            {
                objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objUaimsCommon.ShowError(Page, "Academic_Examination_Grade_Allotment.btnsave_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnLockGrade_Click(object sender, EventArgs e)
    {
        
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int semester = Convert.ToInt32(ddlSemester.SelectedValue);
        int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        int schemeno = Convert.ToInt32(ViewState["schemeno"]);
        //}
    }
    protected void btnReRange_Click(object sender, EventArgs e)
    {

        int lockstatus = 0;
        string maxmark = string.Empty;
        string minmark = string.Empty;
        string grade = string.Empty;

        string ccode = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SR.SESSIONNO", "DISTINCT CCODE", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "  AND CCODE='" + ddlCourse.SelectedValue + "'");




        foreach (ListViewItem dataitem in lvGradeRange.Items)
        {

            TextBox txtmax = dataitem.FindControl("txtmax") as TextBox;
            TextBox txtmin = dataitem.FindControl("txtmin") as TextBox;
            Label lblgrade = dataitem.FindControl("lblGrade") as Label;


            if (txtmax.Text == string.Empty || txtmax.Text == "" || txtmin.Text == string.Empty || txtmin.Text == "")
            {
                objCommon.DisplayMessage(updpnlExam, "Please Enter the Valid Range.", this.Page);
                return;
            }
            else if (Convert.ToDecimal(txtmax.Text) > 100 || Convert.ToDecimal(txtmin.Text) > 100)
            {
                objCommon.DisplayMessage(updpnlExam, "Grade Range Value Should Not Grater Than 100.", this.Page);
                return;
            }


            maxmark += txtmax.Text + ",";
            minmark += txtmin.Text + ",";
            grade += lblgrade.ToolTip + ",";



        }

        maxmark.TrimEnd(',');
        minmark.TrimEnd(',');
        grade.TrimEnd(',');
        //return;


        CustomStatus cs = (CustomStatus)objexam.UpdateGradeRange(Convert.ToInt32(ddlSession.SelectedValue), ddlCourse.SelectedValue, Convert.ToInt32(ddlSemester.SelectedValue), maxmark, minmark, grade, lockstatus, Convert.ToInt32(Session["userno"]));


        if (cs == CustomStatus.RecordSaved)
        {
            //divgradedetails.Visible = true;
            //divgraph.Visible = false;
            objCommon.DisplayMessage(updpnlExam, "Grade Range Change Sucessfully", this.Page);
            BindGradeView();
            BindStudentlist();

        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);

        }
    }
    protected void btnRangrlock_Click(object sender, EventArgs e)
    {
        int lockstatus = 1;

        string maxmark = string.Empty;
        string minmark = string.Empty;
        string grade = string.Empty;
        string ccode = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SR.SESSIONNO=SM.SESSIONNO", "DISTINCT CCODE", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CCODE='" + ddlCourse.SelectedValue + "'");

        foreach (ListViewItem dataitem in lvGradeRange.Items)
        {

            TextBox txtmax = dataitem.FindControl("txtmax") as TextBox;
            TextBox txtmin = dataitem.FindControl("txtmin") as TextBox;
            Label lblgrade = dataitem.FindControl("lblGrade") as Label;


            maxmark += txtmax.Text + ",";
            minmark += txtmin.Text + ",";
            grade += lblgrade.ToolTip + ",";

        }

        maxmark.TrimEnd(',');
        minmark.TrimEnd(',');
        grade.TrimEnd(',');

        CustomStatus cs = (CustomStatus)objexam.UpdateGradeRange(Convert.ToInt32(ddlSession.SelectedValue), ddlCourse.SelectedValue, Convert.ToInt32(ddlSemester.SelectedValue), maxmark, minmark, grade, lockstatus, Convert.ToInt32(Session["userno"]));
        if (cs == CustomStatus.RecordSaved)
        {

            DataSet dsGdpoints = null;
            //dsGdpoints = objCommon.FillDropDown("ACD_GRADE_POINT GP INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=GP.SESSIONNO", "MINMARK,MAXMARK,POINT,GRADE_NAME,TOTAL_STU", "", " SESSIONID=" + ddlSession.SelectedValue + " AND CCODE LIKE '%" + ddlCourse.SelectedValue + "%'", "");

            objCommon.DisplayMessage(updpnlExam, "Grade Range Lock Sucessfully", this.Page);

            //lvGradeRange.DataSource = dsGdpoints;
            //lvGradeRange.DataBind();
            //lvGradeRange.Visible = true;
            //btnRangeLock.Visible = false;
            //btnReRange.Enabled = false;
            //btnRangrlock.Enabled = false;
            BindGradeView();
            BindStudentlist();
            btnReRange.Visible = false;
            btnRangrlock.Visible = false;





        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);
            return;
        }
    }
    protected void BindGradeView()
    {
        DataSet dsGdpoints;

        if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
        {
            ViewState["schemeno"] = "0";
        }
        //PKG_GET_GRADE_RANGE_DETAILS
        string proc_name = "PKG_GET_GRADE_RANGE_DETAILS_CC";
        string pram_name = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO";
        string call_values = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + ddlSemester.SelectedValue + "," + ddlCourse.SelectedValue;
        dsGdpoints = objCommon.DynamicSPCall_Select(proc_name, pram_name, call_values);
        if (dsGdpoints != null && dsGdpoints.Tables.Count > 0)
        {
            if (dsGdpoints.Tables[0].Rows.Count > 0)
            {

                    lvGradeRange.DataSource = dsGdpoints;
                    lvGradeRange.DataBind();
                    lvGradeRange.Visible = true;
                    btnGradeAllotment.Visible = true;
                    btnReport.Visible = true;
                    test.Visible = true;



                    if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('.grade-range #studcount').hide();$('.grade-range #stdcounts').hide();var prm =                                    Sys.WebForms.PageRequestMa//ager.getInstance();prm.add_endRequest(function () { $('.grade-range #studcount').hide();$('.grade-range #stdcounts').hide();});", true);
                    }

                  

                    if (Convert.ToInt32(Session["OrgId"]) == 5)
                    {

                        int lock_status = Convert.ToInt32(objCommon.LookUp("ACD_GRADE_POINT", "DISTINCT ISNULL(LOCKED_STATUS,0)", "SESSIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND CCODE='" + ddlCourse.SelectedValue + "'"));

                        if (lock_status == 0)
                        {
                            btnReRange.Enabled = true;
                            btnReRange.Visible = true;
                            btnRangeLock.Enabled = false;
                            btnRangeLock.Visible = false;
                            btnRangrlock.Visible = true;
                            btnRangrUnlock.Visible = false;

                        }
                        else
                        {
                            btnReRange.Visible = false;
                            btnRangeLock.Enabled = false;
                            btnRangeLock.Visible = false;
                            btnRangrlock.Visible = false;
                            //btnupdscaledn.Visible = false;
                            btnRangrUnlock.Visible = true;
                        }
                    }
                
            }
            else
            {
                lvGradeRange.DataSource = null;
                lvGradeRange.DataBind();
                lvGradeRange.Visible = true;
                btnGradeAllotment.Visible = false;
                btnReport.Visible = false;

            }
        }
        else
        {
            lvGradeRange.DataSource = null;
            lvGradeRange.DataBind();
            lvGradeRange.Visible = true;
            btnReport.Visible = false;
        }

      
    }
    protected void BindStudentlist()
    {
        DataSet dsStudentDetails = null;
        //DataSet dsccode = objCommon.FillDropDown("ACD_STUDENT_RESULT", "CCODE", "", " SESSIONNO=" + ddlSession.SelectedValue + "AND schemeno=" + Convert.ToInt32(ViewState["schemeno"]) + "AND courseno=" + ddlCourse.SelectedValue, "");

        //string Ccode = dsccode.Tables[0].Rows[0]["ccode"].ToString(); 

        //string proc_nameSD = "PKG_SHOW_STUDMARKS_FROM_GRADE_POINT";
        //string pram_nameSD = "@P_SESSIONNO,@P_SCHEMENO,@P_CCODE,@P_SEMESTERNO ,@P_OUT";
        //string call_valuesSD = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + Ccode.ToString() + "," + ddlSemester.SelectedValue + "," + 0 + "";
        //dsStudentDetails = objCommon.DynamicSPCall_Select(proc_nameSD, pram_nameSD, call_valuesSD);

       
        string proc_nameSD = string.Empty;
        string pram_nameSD = string.Empty;
        string call_valuesSD = string.Empty;

        if (ViewState["schemeno"] == string.Empty || ViewState["schemeno"] == "")
        {
            ViewState["schemeno"] = "0";
        }
        //PKG_SHOW_STUDMARKS_FROM_GRADE_POINT
        proc_nameSD = "PKG_SHOW_STUDMARKS_FROM_GRADE_POINT_CC";
        pram_nameSD = "@P_SESSIONNO,@P_SCHEMENO,@P_CCODE,@P_SEMESTERNO ,@P_OUT";
        call_valuesSD = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["schemeno"]) + "," + ddlCourse.SelectedValue.ToString() + "," + ddlSemester.SelectedValue + "," + 0 + "";
        dsStudentDetails = objCommon.DynamicSPCall_Select(proc_nameSD, pram_nameSD, call_valuesSD);


        if (dsStudentDetails.Tables[0].Rows.Count > 0)
        {
            if (dsStudentDetails.Tables[0].Rows.Count > 0)
            {

                lvStudentDetails.DataSource = dsStudentDetails;
                lvStudentDetails.DataBind();
                lvStudentDetails.Visible = true;


                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                {
                    //divscale.Visible = true;
                    //string powerfactor = objCommon.LookUp("ACD_EXAM_GRADE_POINT_CALC_VALUES CV", "DISTINCT ISNULL(POWER_FACTOR_CPU,0)", "SESIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND CCODE='" + ddlCourse.SelectedValue + "' ");
                    //lblfactor.Text = powerfactor;

                    DataSet powerfactor = objCommon.FillDropDown("ACD_EXAM_GRADE_POINT_CALC_VALUES", "TOP 1 MAX_RANGE_CPU", "MIN_RANGE_CPU,POWER_FACTOR_CPU", "SESIONNO IN (SELECT SESIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + " AND CCODE='" + ddlCourse.SelectedValue + "')", "GDPOINTCALC_ID DESC");

                    if (powerfactor.Tables[0].Rows.Count > 0)
                    {
                        divpower.Visible = true;
                        lvpowerfactor.DataSource = powerfactor;
                        lvpowerfactor.DataBind();
                        lvpowerfactor.Visible = true;
                        btnmodifypowerfactor.Visible = false;
                        btnRangeLock.Visible = true;
                    }
                    else
                    {
                        divpower.Visible = false;
                        lvpowerfactor.DataSource = null;
                        lvpowerfactor.DataBind();
                        lvpowerfactor.Visible = false;
                        btnmodifypowerfactor.Visible = false;
                        btnRangeLock.Visible = true;
                    }
                }



            }
            else
            {
                lvStudentDetails.DataSource = null;
                lvStudentDetails.DataBind();
                lvStudentDetails.Visible = false;
                lvpowerfactor.DataSource = null;
                lvpowerfactor.DataBind();
                lvpowerfactor.Visible = false;
                divpower.Visible = false;
            }
        }
        else
        {
            lvStudentDetails.DataSource = null;
            lvStudentDetails.DataBind();
            lvStudentDetails.Visible = false;
            lvpowerfactor.DataSource = null;
            lvpowerfactor.DataBind();
            lvpowerfactor.Visible = false;
            divpower.Visible = false;
          
        }
        
    }
    //[WebMethod]
    //public static MarkDetails[] GetAllotment(int session, int sem, int course, int scheme)
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        DataTable dt = new DataTable();
    //        DataTable dt1 = new DataTable();
         
    //        List<MarkDetails> details = new List<MarkDetails>();
    //        List<MarksGrade> detailsnew = new List<MarksGrade>();
    //        SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[5];

    //        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

    //        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
    //        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
    //        objParams[3] = new SqlParameter("@P_COURSENO", course);
    //        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
    //        objParams[4].Direction = ParameterDirection.Output;

    //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RELATIVE_GRADE_ANALYSIS_PBI_ATLAS", objParams);
    //        dt = ds.Tables[0];
       
    //        foreach (DataRow dtrow in dt.Rows)
    //        {
    //            MarkDetails us = new MarkDetails();
    //            us.SRNO = dtrow["SRNO"].ToString();
    //            us.MARKS = dtrow["MARKS"].ToString();
    //            us.STUD_COUNT = dtrow["STUD_COUNT"].ToString();
    //            //us.STUDENT_COUNT = dtrow["STUDENT_COUNT"].ToString();
    //            //us.GRADE_NAME = dtrow["GRADE_NAME"].ToString();
    //            details.Add(us);

    //        }
          
    //        return details.ToArray();     
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}
    //[WebMethod]
    //public static MarksGrade[] GetGradeAllotment(int session, int sem, int course, int scheme)
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        DataTable dt1 = new DataTable();
    //       // DataTable dt1 = new DataTable();

    //       // List<MarkDetails> details = new List<MarkDetails>();
    //        List<MarksGrade> detailsnew = new List<MarksGrade>();
    //        SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

    //        SqlParameter[] objParams = null;
    //        objParams = new SqlParameter[5];

    //        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

    //        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
    //        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
    //        objParams[3] = new SqlParameter("@P_COURSENO", course);
    //        objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
    //        objParams[4].Direction = ParameterDirection.Output;

    //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RELATIVE_GRADE_ANALYSIS_PBI_ATLAS", objParams);
    //        dt1 = ds.Tables[1];

          
    //        foreach (DataRow dtrow in dt1.Rows)
    //        {
    //            MarksGrade us = new MarksGrade();
              
    //            us.STUDENT_COUNT = dtrow["STUDENT_COUNT"].ToString();
    //            us.GRADE_NAME = dtrow["GRADE_NAME"].ToString();
    //            detailsnew.Add(us);

    //        }
    //        return detailsnew.ToArray();
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }
    //}

    //added by prafull on dt-21062023


    [WebMethod]
    public static MarkDetails[] GetAllotment(int session, int sem, string course, int scheme)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            List<MarkDetails> details = new List<MarkDetails>();
            List<MarksGrade> detailsnew = new List<MarksGrade>();
            SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

            SqlParameter[] objParams = null;
            objParams = new SqlParameter[5];

            objParams[0] = new SqlParameter("@P_SESSIONNO", session);

            objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
            objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
            objParams[3] = new SqlParameter("@P_COURSENO", course);
            objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
            objParams[4].Direction = ParameterDirection.Output;

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RELATIVE_GRADE_ANALYSIS_PBI_ATLAS", objParams);
            dt = ds.Tables[0];

            foreach (DataRow dtrow in dt.Rows)
            {
                MarkDetails us = new MarkDetails();
                us.SRNO = dtrow["SRNO"].ToString();
                us.MARKS = dtrow["MARKS"].ToString();
                us.STUD_COUNT = dtrow["STUD_COUNT"].ToString();
                //us.STUDENT_COUNT = dtrow["STUDENT_COUNT"].ToString();
                //us.GRADE_NAME = dtrow["GRADE_NAME"].ToString();
                details.Add(us);

            }

            return details.ToArray();

            //return [details,detailsnew];

            // return {details,detailsnew};
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    [WebMethod]
    public static MarksGrade[] GetGradeAllotment(int session, int sem, string course, int scheme)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            // DataTable dt1 = new DataTable();

            // List<MarkDetails> details = new List<MarkDetails>();
            List<MarksGrade> detailsnew = new List<MarksGrade>();
            SQLHelper objSQLHelper = new SQLHelper(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);

            SqlParameter[] objParams = null;
            objParams = new SqlParameter[5];

            objParams[0] = new SqlParameter("@P_SESSIONNO", session);

            objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
            objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
            objParams[3] = new SqlParameter("@P_COURSENO", course);
            objParams[4] = new SqlParameter("@P_OP", SqlDbType.Int);
            objParams[4].Direction = ParameterDirection.Output;

            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RELATIVE_GRADE_ANALYSIS_PBI_ATLAS", objParams);
            dt1 = ds.Tables[1];

            foreach (DataRow dtrow in dt1.Rows)
            {
                MarksGrade us = new MarksGrade();

                us.STUDENT_COUNT = dtrow["STUDENT_COUNT"].ToString();
                us.GRADE_NAME = dtrow["GRADE_NAME"].ToString();
                detailsnew.Add(us);

            }
            return detailsnew.ToArray();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        foreach (ListViewDataItem lvitem in lvpowerfactor.Items)
        {

            CheckBox chk1 = ((CheckBox)lvitem.FindControl("chkAccept"));
            TextBox txtupper = ((TextBox)lvitem.FindControl("txtupper"));
            TextBox txtlower = ((TextBox)lvitem.FindControl("txtlower"));
            if (chk1.Checked)
            {
                txtlower.Enabled = true;
                txtupper.Enabled = true;
                btnmodifypowerfactor.Visible = true;
                btnRangeLock.Visible = false;

            }
            else
            {
                txtlower.Enabled = false;
                txtupper.Enabled = false;
                btnmodifypowerfactor.Visible = false;
                btnRangeLock.Visible = true;

                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                {
                    //divscale.Visible = true;
                    //string powerfactor = objCommon.LookUp("ACD_EXAM_GRADE_POINT_CALC_VALUES CV", "DISTINCT ISNULL(POWER_FACTOR_CPU,0)", "SESIONNO IN(SELECT SESSIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + ") AND CCODE='" + ddlCourse.SelectedValue + "' ");
                    //lblfactor.Text = powerfactor;

                    DataSet powerfactor = objCommon.FillDropDown("ACD_EXAM_GRADE_POINT_CALC_VALUES", "TOP 1 MAX_RANGE_CPU", "MIN_RANGE_CPU,POWER_FACTOR_CPU", "SESIONNO IN (SELECT SESIONNO FROM ACD_SESSION_MASTER WHERE SESSIONID=" + ddlSession.SelectedValue + " AND CCODE='" + ddlCourse.SelectedValue + "')", "GDPOINTCALC_ID DESC");

                    if (powerfactor.Tables[0].Rows.Count > 0)
                    {
                        lvpowerfactor.DataSource = powerfactor;
                        lvpowerfactor.DataBind();
                        lvpowerfactor.Visible = true;
                    }
                    else
                    {
                        lvpowerfactor.DataSource = null;
                        lvpowerfactor.DataBind();
                        lvpowerfactor.Visible = false;
                    }
                }


            }

        }

    }
    protected void btnRangrUnlock_Click(object sender, EventArgs e)
    {
        int lockstatus = 2;

        string maxmark = string.Empty;
        string minmark = string.Empty;
        string grade = string.Empty;
        string ccode = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SR.SESSIONNO=SM.SESSIONNO", "DISTINCT CCODE", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CCODE='" + ddlCourse.SelectedValue + "'");

        foreach (ListViewItem dataitem in lvGradeRange.Items)
        {

            TextBox txtmax = dataitem.FindControl("txtmax") as TextBox;
            TextBox txtmin = dataitem.FindControl("txtmin") as TextBox;
            Label lblgrade = dataitem.FindControl("lblGrade") as Label;


            maxmark += txtmax.Text + ",";
            minmark += txtmin.Text + ",";
            grade += lblgrade.ToolTip + ",";

        }

        maxmark.TrimEnd(',');
        minmark.TrimEnd(',');
        grade.TrimEnd(',');

        CustomStatus cs = (CustomStatus)objexam.UpdateGradeRange(Convert.ToInt32(ddlSession.SelectedValue), ddlCourse.SelectedValue, Convert.ToInt32(ddlSemester.SelectedValue), maxmark, minmark, grade, lockstatus, Convert.ToInt32(Session["userno"]));
        if (cs == CustomStatus.RecordSaved)
        {

            DataSet dsGdpoints = null;
            objCommon.DisplayMessage(updpnlExam, "Grade Range UnLock Sucessfully", this.Page);

            BindGradeView();
            BindStudentlist();
            btnReRange.Visible = true;
            btnRangrlock.Visible = true;
            btnRangrUnlock.Visible = false;





        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);
            return;
        }
    }
    protected void btnmodifypowerfactor_Click(object sender, EventArgs e)
    {


        string Upper_range = string.Empty; ;
        string Lower_range = string.Empty; ;
       

        string ccode = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_SESSION_MASTER SM ON SM.SESSIONNO=SR.SESSIONNO", "DISTINCT CCODE", "SESSIONID=" + Convert.ToInt32(ddlSession.SelectedValue) + "  AND CCODE='" + ddlCourse.SelectedValue + "'");


        foreach (ListViewItem dataitem in lvpowerfactor.Items)
        {
            TextBox txtupper = dataitem.FindControl("txtupper") as TextBox;
            TextBox txtlower = dataitem.FindControl("txtlower") as TextBox;
            Label lblpwerfactor = dataitem.FindControl("lblpwerfactor") as Label;

            
            if (txtupper.Text == string.Empty || txtupper.Text == "" || txtlower.Text == string.Empty || txtlower.Text == "")
            {
                objCommon.DisplayMessage(updpnlExam, "Please Enter the Valid Upper & Lower Range.", this.Page);
                return;
            }
            else if (Convert.ToDecimal(txtupper.Text) > 100 || Convert.ToDecimal(txtlower.Text) > 100)
            {
                objCommon.DisplayMessage(updpnlExam, "Grade Range Value Should Not Grater Than 100.", this.Page);
                return;
            }
            else if(Convert.ToDecimal(txtupper.Text) < Convert.ToDecimal(txtlower.Text) )
            {
                objCommon.DisplayMessage(updpnlExam, "Upper Range Value Should Not be Less Than Lower Range Value.", this.Page);
                return;
            }
            //maxmark += txtmax.Text + ",";
            //minmark += txtmin.Text + ",";
            //grade += lblgrade.ToolTip + ",";

            Upper_range = txtupper.Text+",";
            Lower_range = txtlower.Text+",";


        }
        Upper_range = Upper_range.TrimEnd(',');
        Lower_range = Lower_range.TrimEnd(',');

        CustomStatus cs = (CustomStatus)objexam.UpdatePowerFactor(Convert.ToInt32(ddlSession.SelectedValue), ddlCourse.SelectedValue, 0, Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToDecimal(Upper_range),Convert.ToDecimal(Lower_range), Convert.ToInt32(Session["userno"]));


        if (cs == CustomStatus.RecordSaved)
        {
            //divgradedetails.Visible = true;
            //divgraph.Visible = false;
            objCommon.DisplayMessage(updpnlExam, "Power Factor Update Sucessfully", this.Page);
            BindGradeView();
            BindStudentlist();

        }
        else
        {
            objCommon.DisplayMessage(updpnlExam, "Something Went Wrong,", this.Page);

        }
        
        //return;

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        GridView GVStudData = new GridView();
        string SP_Name = string.Empty;
        string SP_Parameters = string.Empty;
        string Call_Values = string.Empty;

      //  string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));


        if (Convert.ToInt32(Session["OrgId"]) == 5)
        {
            SP_Name = "PKG_GET_COMPONENTWISE_MARK_DETAILS_RELATIVE_GRADE_ALLOTMENT";
            SP_Parameters = "@P_SESSIONNO, @P_CCODE, @P_SEMESTERNO";

            Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlCourse.SelectedValue + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";
        }
        else
        {
            SP_Name = "PKG_ACD_RELATIVE_GRADE_ALLOTMENT_REPORT";
            SP_Parameters = "@P_SESSIONNO, @P_CCODE, @P_SEMESTERNO";

            Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + ddlCourse.SelectedValue + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "";
        }
        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStudData.DataSource = ds;
            GVStudData.DataBind();

            string attachment = "attachment;filename=InternalExternalMarkentryExcel.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(this.updpnlExam, "No Data Found", this.Page);
            return;
        }
    }
}