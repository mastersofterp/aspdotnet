//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - MARK ENTRY                                           
// CREATION DATE :                                                      
// CREATED BY    : Lalit Gaikwad                                                
// MODIFIED BY   : 17-NOV-2009                                                     
// MODIFIED DESC : 
//=================================================================================

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
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;
using System.Net.NetworkInformation;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using ClosedXML.Excel;
using System.Data.OleDb;

using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;

using System.Threading.Tasks;
//using System.Activities.Statements;


using Microsoft.Win32;
//using Microsoft.WindowsAzure.Storage.Auth;
using DynamicAL_v2;



public partial class ACADEMIC_EXAMINATION_MarkEntryforIA_Admin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    int s;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
   // string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_MEContainerName"].ToString();
    //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["rcpitdoctesting"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameExam"].ToString();
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
        if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                }
                //Check for Panel
                if (ViewState["action"] == null)
                {
                    //selection panel
                    pnlSelection.Visible = true;
                    pnlMarkEntry.Visible = false;
                }
                else if (ViewState["action"].ToString().Equals("markentry"))
                {
                    //mark entry panel
                    pnlMarkEntry.Visible = true;
                    pnlSelection.Visible = false;
                }

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT TOP 4 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
                string colgno = Session["college_nos"].ToString();
                if (Session["usertype"].ToString().Equals("1"))
                {

                    objCommon.FillDropDownList(ddlscheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
        
                    ddlSession.SelectedIndex = 0;
                }
                else
                {
                    //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND COLLEGE_ID IN (" + ViewState["College_ids"].ToString() + ")", "COLLEGE_ID");
                    // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                    objCommon.FillDropDownList(ddlscheme, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
                    ddlSession.SelectedIndex = 0;
                }

                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");

                //objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "SUBID > 0", "SUBID");
                //ddlSession.SelectedIndex = 1;
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];

                if (ddlSession.SelectedValue == "0")
                {
                    // objCommon.DisplayMessage(this.updpanle1, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                }
                else
                {
                    this.GetExamWiseDates();
                }
                //Page.Form.Attributes.Add("enctype", "multipart/form-data");
                //this.Page.Form.Enctype = "multipart/form-data";
            }

            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //this.Page.Form.Enctype = "multipart/form-data";
        }
        divMsg.InnerHtml = string.Empty;

    }

    private void ShowCourses()
    {
        DataSet ds = objMarksEntry.GetCourseForTeacher_Admin_RCPIT(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCourse.DataSource = ds.Tables[0];
                lvCourse.DataBind();
            }
            else
            {
                lvCourse.DataSource = null;
                lvCourse.DataBind();
                lvCourse.Visible = false;
                objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);
            }
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page);
        }
    }

    protected void lnkbtnCourse_Click(object sender, EventArgs e)
    {
        try
        {
            //Show the Student List with Exams that are ON
            LinkButton lnk = sender as LinkButton;
            if (!lnk.ToolTip.Equals(string.Empty))
            {
                lblCourse.Text = lnk.Text;
                lblCourse.ToolTip = lnk.ToolTip;
                ViewState["COURSENO"] = lblCourse.ToolTip;
                ViewState["CCODE"] = (objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO='" + lblCourse.ToolTip + "'"));
                string[] sec_batch = lnk.CommandArgument.ToString().Split('+');


                //Added by gaurav For check Status for end Sem mark entry 07-09-2022
                ViewState["EXAMNO"] = sec_batch[2];
                string examno = objCommon.LookUp("acd_exam_name", "FLDNAME", "examno=" + ViewState["EXAMNO"]);

                int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO='" + lblCourse.ToolTip + "'"));
                string COURSENAME = (objCommon.LookUp("ACD_COURSE", "COURSE_NAME", "COURSENO='" + lblCourse.ToolTip + "'"));
                #region ADDED BY GAURAV S 11_01_2023 FOR CPUH/K FOR CHECK EXTERNAL MARK ENTRY CHECK
                //Added by gaurav For check Status for end Sem mark entry 07-09-2022

                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                {
                    if (examno == "EXTERMARK")
                    {
                        int TestMark = 0;
                        double maxmark = 0.00;
                        maxmark = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "MAXMARKS_I", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip)));
                        if (maxmark > 0)
                        {
                            TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                            if (TestMark > 0)
                            {
                                if (examno == "EXTERMARK")
                                {

                                    string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CPU_K_H";
                                    string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                    string sp_callValues = "" + ViewState["COURSENO"] + "," + SCHEMENO + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";

                                    DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                    if (dschk.Tables.Count > 0)
                                    {
                                        if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                        {
                                            string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                            if (islocked == "0" || islocked == string.Empty || islocked == null)
                                            {
                                                objCommon.DisplayMessage(this.updpanle1, "Kindly Check the Internal Mark Entry is not Completed or Not Locked for " + COURSENAME + " !", this.Page);
                                                return;

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {

                                objCommon.DisplayMessage(this.updpanle1, "Internal Mark Entry yet not start for " + COURSENAME + " !", this.Page);
                                return;
                            }
                        }
                        // }
                    }
                }
                #endregion
                
                
                //if (examno == "EXTERMARK")
                //{
                //    int status = GetStatusForMarkEntry();
                //    if (status == 1)
                //    {
                //        return;
                //    }
                //}


                //LinkButton btn = sender as LinkButton;
                //ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);
                //hdnsem.Value = Convert.ToString(ViewState["sem"]);                  //Added on 04082022           


                // Check Mark Enrty Activitity -- Added by Abhinay Lad [14-09-2019]
                DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[2]) + " )", "SESSIONNO DESC");
                //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[2]) + " )", "SESSIONNO DESC");

                if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayMessage(this.updpanle1, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    return;
                }

                //End

                hdfSection.Value = sec_batch[0].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";

                int CourseNo = 0;
                LinkButton btn = sender as LinkButton;
                CourseNo = Convert.ToInt32((btn.Parent.FindControl("hdnfld_courseno") as HiddenField).Value);
                ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);

                string excelStatus = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ExcelMarkEntry", "");
                if (excelStatus == "1")
                {
                    lnkExcekImport.Visible = true;
                }
                else
                {
                    lnkExcekImport.Visible = false;
                }


                if (lnk.CommandArgument.ToString().Split('+')[3].ToString().Equals("S10"))
                {
                    ViewState["S10"] = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[3]);
                    ViewState["MODEL_EXAM_NAME"] = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[4]);

                    string itemName = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[4]);
                    string itemValue = Convert.ToString(lnk.CommandArgument.ToString().Split('+')[3]) + "-" + Convert.ToString(lnk.CommandArgument.ToString().Split('+')[2]);



                    ddlExam.Items.Clear();
                    ddlExam.Items.Add(new ListItem("Select Exam", "0"));
                    ddlExam.Items.Add(new ListItem(itemName, itemValue));
                    ddlExam.SelectedIndex = 1;
                    ddlExam.Enabled = false;
                    ddlSubExam.Visible = false;
                    lblSubExamName.Visible = false;

                    if (ddlExam.Items.Count > 0)
                    {
                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = true;
                        pnlStudGrid.Visible = false;
                        //btnBack.Visible = false;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnPrintReport.Visible = false;
                        lblStudents.Visible = false;
                        ddlSubExam.SelectedIndex = 0;
                        ddlSubExam.Enabled = false;
                    }

                    DataSet dss = objCommon.FillDropDown("ACD_MARK_ENTRY_STATUS_CODES", "*", "", "", "");
                    rptMarkCodes.DataSource = dss;
                    rptMarkCodes.DataBind();
                }
                else
                {
                    ddlExam.Enabled = true;
                    ddlSubExam.Visible = true;
                    lblSubExamName.Visible = true;

                    DataSet dsExams = objMarksEntry.GetONExams_RCPIT_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                    string exams = string.Empty;
                    if (dsExams != null && dsExams.Tables.Count > 0 && dsExams.Tables[0].Rows.Count > 0)
                    {
                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                        while (dtr.Read())
                        {
                            exams += dtr["FLDNAME2"] == DBNull.Value ? string.Empty : dtr["FLDNAME2"].ToString() + ",";
                        }
                        dtr.Close();

                    }
                    else
                        objCommon.DisplayMessage(this.updpanle1, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);


                    ViewState["examNo"] = Convert.ToString(lnk.CommandArgument.Split('+')[2]);
                    //ViewState["fldname"] = Convert.ToString(lnk.CommandArgument.Split('+')[3]);

                    //for (int i = 0; i < dsExams.Tables[0].Rows.Count; i++)
                    //{
                    //    if (ViewState["examNo"] == dsExams.Tables[0].Rows[i]["EXAMNO"].ToString())
                    //    {
                    if (exams.Length > 0)
                    {
                        ViewState["exams"] = exams.Split(','); //store arrat
                        ViewState["exam"] = exams;

                        ddlExam.Items.Clear();
                        ddlExam.Items.Add(new ListItem("Select Exam", "0"));

                        DataTableReader dtr = dsExams.Tables[0].CreateDataReader();

                        while (dtr.Read())
                        {
                            if (ViewState["examNo"].ToString() == dtr["EXAMNO"].ToString())
                            {
                                if (dtr["FLDNAME2"] != DBNull.Value)
                                {
                                    if (ddlSubjectType.SelectedIndex > 0)
                                    {
                                        ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME2"].ToString()));
                                    }
                                }
                            }
                        }
                        dtr.Close();

                        if (ddlExam.Items.Count > 0)
                        {
                            pnlSelection.Visible = false;
                            pnlMarkEntry.Visible = true;
                            pnlStudGrid.Visible = false;
                            //btnBack.Visible = false;
                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            btnPrintReport.Visible = false;
                            lblStudents.Visible = false;
                            ddlSubExam.SelectedIndex = 0;
                            ddlSubExam.Enabled = false;
                        }

                        DataSet dss = objCommon.FillDropDown("ACD_MARK_ENTRY_STATUS_CODES", "*", "", "", "");
                        rptMarkCodes.DataSource = dss;
                        rptMarkCodes.DataBind();
                    }

                    else
                    {
                        objCommon.DisplayMessage(this.updpanle1, "No Exam for the Selected Course may be not be Started Or may be Locked!!!", this.Page);
                    }
                    //    }
                    //}

                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.lnkbtnCourse_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAndLock(0);
        //updpanle1.Update();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ViewState["action"] = null;
        pnlSelection.Visible = true;
        pnlMarkEntry.Visible = false;
        GetStatus();
        pnlUP.Visible = false;                      //Added by Sachin A on 16082022
        ddlSort.SelectedIndex = 0;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntryforIA _Admin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntryforIA _Admin.aspx");
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private void SaveAndLock(int lock_status)
    {
        try
        {
            string API_Output = "";
            string examtype = string.Empty;
            string examno = ViewState["examNo"].ToString();

            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                //Check for lock and null marks
                if (CheckMarks(lock_status) == false)
                {
                    return;
                }
                string studids = string.Empty;
                string marks = string.Empty;

                MarksEntryController objMarksEntry = new MarksEntryController();
                Label lbl;
                TextBox txtMarks;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //Note : -100 for Marks will be converted as NULL           
                //NULL means mark entry not done.                           
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";

                    //Gather Exam Marks 
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                }
                string[] course = lblCourse.Text.Split('~');
                string ccode = course[0].Trim();
                // Added By Abhinay Lad [17-07-2019]
                int courseNo = Convert.ToInt32(lblCourse.ToolTip);
                int FlagReval = 0;

                if (ddlExam.SelectedValue.StartsWith("S"))
                    examtype = "S";
                else if (ddlExam.SelectedValue.StartsWith("E"))
                    examtype = "E";

                string examname = ddlExam.SelectedValue;
                string subExam_Name = (ddlSubExam.Visible == true) ? ddlSubExam.SelectedValue : "S10T1-19";
                #region added by gaurav 
                string[] Exam = ddlExam.SelectedValue.Split('-');
                if (Exam[1].StartsWith("S"))
                    examtype = "S";
                else if (Exam[1].StartsWith("E"))
                    examtype = "E";
                string examnameExtern = string.Empty;
                #endregion


                CustomStatus cs;

                #region ADDED BY GAURAV S FOR CPU/H END SEM MARK ENTRY SAVE 11_01_2023
                string SubExamComponentName = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNAME", "EXAMNO=" + Convert.ToInt32(Exam[1])); ;
                string Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Convert.ToInt32(Exam[1]) + "AND  SUBEXAMNO=" + ddlSubExam.SelectedValue.Split('-')[1]);

                string examnoname = objCommon.LookUp("acd_exam_name", "fldname", "examno=" + Convert.ToInt32(ViewState["examNo"]));
                int SCHEMENO = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "SCHEMENO", "COURSENO='" + lblCourse.ToolTip + "'"));
                //return;             
                // CPUKOTA/HAMIRPUR
                if (examnoname == "EXTERMARK")
                {
                    examname = Exam[1];
                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                    {
                        cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_CPU_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(courseNo), ccode, studids, marks, lock_status, ddlExam.SelectedValue.ToString(), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ViewState["SemesterNo"].ToString()), SCHEMENO, Convert.ToInt32(hdfSection.Value), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
                    }
                    else
                    {
                        cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_CPU_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(courseNo), ccode, studids, marks, lock_status, ddlExam.SelectedValue.ToString(), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ViewState["SemesterNo"].ToString()), SCHEMENO, Convert.ToInt32(hdfSection.Value), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
                    }

                }
                #endregion 
                else
                {//added by gaurav 1
                    #region elsepart 



                    if (ViewState["markentryotp"] != null && ViewState["markentryotp"].ToString() == "1")
                    {
                        string smsmobile, to_email;
                        string sms_text = string.Empty;
                        string email_text = string.Empty;
                        string from_email = objCommon.LookUp("reff", "EMAILSVCID", "");

                        if (ViewState["to_email"].ToString() != string.Empty)
                            to_email = ViewState["to_email"].ToString();
                        else
                            to_email = string.Empty;

                        if (ViewState["smsmobile"].ToString() != string.Empty)
                            smsmobile = ViewState["smsmobile"].ToString();
                        else
                            smsmobile = string.Empty;

                        if (ViewState["sms_text"].ToString() != string.Empty)
                            sms_text = ViewState["sms_text"].ToString();
                        else
                            sms_text = string.Empty;

                        if (ViewState["email_text"].ToString() != string.Empty)
                            email_text = ViewState["email_text"].ToString();
                        else
                            email_text = string.Empty;


                        // return;
                        if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) //ADDED BY GAURAV FOR CPUK/H 21_01_2023
                        {
                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew_CPU_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, to_email, from_email, smsmobile, 1, sms_text, email_text, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
                        }
                        else
                        {
                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew1(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, to_email, from_email, smsmobile, 1, sms_text, email_text, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
                        }
                        }
                    else
                    {
                        if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) //ADDED BY GAURAV FOR CPUK/H 21_01_2023
                        {
                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew_CPU_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
                        }
                        else
                        {
                            //  return;
                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew1(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
                        }
                    }

                    #endregion
                }//added by gaurav 1
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(this.updpanle1, "Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        pnlUP.Visible = false;                      //Added by Sachin A on 06082022
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpanle1, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    }

                    //ShowStudents();
                    btnShow_Click(null, null);
                }


                else if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(this.updpanle1, "Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        pnlUP.Visible = false;                       //Added by Sachin A on 06082022
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updpanle1, "Marks Updated Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    }
                    //ShowStudents();
                    btnShow_Click(null, null);
                }
                else if (cs.Equals(CustomStatus.Others))
                {
                    objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined for Selected Subject.", this.Page);
                    //ShowStudents();
                    btnShow_Click(null, null);
                }

                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Error in Saving Marks!", this.Page);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[3].Visible == true) return flag;
        return false;
    }

    private bool CheckMarks(int lock_status)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;

            for (int j = 3; j < gvStudent.Columns.Count; j++)    //columns
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        if (j == 3) //TA MARKS
                        {
                            lbl = gvStudent.Rows[i].Cells[j].FindControl("lblMarks") as Label;      //Max Marks 
                            txt = gvStudent.Rows[i].Cells[j].FindControl("txtMarks") as TextBox;    //Marks Entered 
                            maxMarks = lbl.Text.Trim();
                            marks = txt.Text.Trim();

                            if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            {
                                if (txt.Text == "")
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(this.updpanle1, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                        //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToDouble(txt.Text) > Convert.ToDouble(lbl.Text))
                                    {
                                        if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                        {
                                            objCommon.DisplayMessage(this, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                    else if (Convert.ToDouble(txt.Text) < 0)
                                    {
                                        //Note : 401 for Absent and Not Eligible
                                        if (Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
                                        {
                                        }
                                        else
                                        {
                                            objCommon.DisplayMessage(this, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (txt.Enabled == true)
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(this, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (flag == false) break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    #region
    protected void btnLock_Click(object sender, EventArgs e)
    {
        SaveAndLock(1);
        #region Commented by Abhinay Lad [25-07-2019]
        //string markentryotp = objCommon.LookUp("ACD_ALERT_STATUS", "Confirm_Alert", "alertsno = 1 and Confirm_Alert=1");
        //ViewState["markentryotp"] = markentryotp;
        //DataSet ds = objCommon.FillDropDown("user_acc", "UA_MOBILE", "UA_FULLNAME,ua_email", "ua_no=" + Convert.ToInt32(Session["userno"]) + "", "");
        //if (markentryotp.ToString() == "1")
        //{
        //    DataSet Alert_status = objCommon.FillDropDown("ACD_ALERT_STATUS", "Send_Through", "Confirm_Alert", "AlertsNo=1", "");
        //    string OTP = GenerateOTP();
        //    Session["OTP"] = OTP;
        //    if (Alert_status != null && Alert_status.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow[] email = (Alert_status.Tables[0].Select("Send_Through=1 and Confirm_Alert=1"));
        //        if (email != null && email.Length > 0)
        //        {
        //            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0]["ua_email"].ToString()))
        //            {
        //                ViewState["to_email"] = ds.Tables[0].Rows[0]["ua_email"].ToString();
        //                bool chk = CheckMarks(1);
        //                if (chk == false)
        //                    return;
        //                else
        //                {
        //                    string msgbody = MessageBody(ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString(), ds.Tables[0].Rows[0]["ua_email"].ToString(), lblCourse.ToolTip.ToString(), Session["OTP"].ToString(), ddlExam.SelectedItem.Text);
        //                    objCommon.sendEmail(msgbody, ds.Tables[0].Rows[0]["ua_email"].ToString(), "One-Time Password to Lock Marks");
        //                    string email_text = "" + Session["OTP"].ToString() + " is your One-Time Password (OTP) to lock mark for " + ddlExam.SelectedItem.Text + " exam of " + lblCourse.ToolTip.ToString() + "";
        //                    ViewState["email_text"] = email_text;
        //                }
        //            }
        //            else
        //            {
        //                objCommon.DisplayMessage(this.updpanle1, "Your Email ID is not registered. Kindly register first.", this.Page);
        //            }
        //        }
        //        else
        //        {
        //            ViewState["to_email"] = string.Empty;
        //            ViewState["email_text"] = string.Empty;
        //        }
        //        DataRow[] sms = (Alert_status.Tables[0].Select("Send_Through=2 and Confirm_Alert=1"));
        //        if (sms != null && sms.Length > 0)
        //        {
        //            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0]["UA_MOBILE"].ToString()))
        //            {
        //                ViewState["smsmobile"] = ds.Tables[0].Rows[0]["UA_MOBILE"].ToString();
        //                bool chk = CheckMarks(1);
        //                if (chk == false)
        //                    return;
        //                else
        //                {
        //                    string text = " Dear " + ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString() + "," + Session["OTP"].ToString() + " is your One-Time Password (OTP) to lock marks for " + ddlExam.SelectedItem.Text + " Exam of " + lblCourse.Text.ToString() + " Course.";
        //                    ViewState["sms_text"] = text;
        //                    this.SendSMS(ds.Tables[0].Rows[0]["UA_MOBILE"].ToString(), text);
        //                }
        //            }
        //            else
        //            {
        //                objCommon.DisplayMessage(this.updpanle1, "Your Mobile No. is not registered. Kindly register first.", this.Page);
        //            }
        //        }
        //        else
        //        {
        //            ViewState["smsmobile"] = string.Empty;
        //            ViewState["sms_text"] = string.Empty;
        //        }
        //        lblOTP.Visible = true;
        //        lblOTP.Text = "OTP has been sent on your registered Email ID & Mobile No..."; ;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "$('#myModal33').modal('show')", true);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Show", "$('#myModal33').show()", true);
        //    }
        //}
        //else
        //{
        //    SaveAndLock(1);
        //}
        #endregion
    }
    #endregion

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Visible = false;
        btnLock.Visible = false;
        btnPrintReport.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW.rpt");//rptMarksList1.rpt
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value;

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }

    protected void btnTAReport_Click(object sender, EventArgs e)
    {
        string subid = objCommon.LookUp("ACD_COURSE", "SUBID", "COURSENO=" + lblCourse.ToolTip);

        if (Convert.ToInt32(subid) == 1)
        {
            this.ShowReportForMID("TAMarksListReport", "rptMarksListForMID.rpt");
        }
        else
        {
            this.ShowReportForMID("TAPracMarksListReport", "rptMarksListForMIDPrac.rpt");
        }
    }

    private void ShowReportForMID(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_COURSENO=" + lblCourse.ToolTip + ",@P_SECTIONNO=" + hdfSection.Value + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]);

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
    }

    protected void btnConsolidateReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("MarksListReport", "rptMarksList.rpt");
    }

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ShowCourses();
        this.GetStatus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlSubExam.Visible == true && ddlSubExam.SelectedIndex > 0)
        {
            ShowStudents();
        }
        else
        {
            if (ddlExam.Enabled == false && Convert.ToString(ViewState["S10"]) == "S10")
            {
                ShowStudents_For_Model_Exam();
            }
            else
            {
                objCommon.DisplayMessage(this.updpanle1, "Please Select Sub Exam!!", this.Page);
                ddlExam.Focus();
            }
        }
    }

    private void ShowStudents()
    {
        try
        {
            ViewState["LOCK_STATUS"] = string.Empty;
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            //DataSet ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + Convert.ToInt32(ViewState["COURSENO"]) + ") AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + "", "");

            DataSet ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + Convert.ToInt32(ViewState["COURSENO"]) + ") AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + "AND SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
                {
                    objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 1 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
                    return;
                }
                else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
                {
                    objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 2 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined", this.Page);
                return;
            }

            //dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ddlExam.SelectedValue).Split('-')[0], Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue));
            if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) //ADDED BY GAURAV FOR CPUK/H 21_01_2023
            {
                dsStudent = objMarksEntry.GetStudentsForMarkEntryNew_CPU_admin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ddlExam.SelectedValue).Split('-')[0], Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue), Convert.ToInt32(ddlSort.SelectedValue));
            }
            else
            {
                dsStudent = objMarksEntry.GetStudentsForMarkEntryNew_RCPIT_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ddlExam.SelectedValue).Split('-')[0], Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue), Convert.ToInt32(ddlSort.SelectedValue));
            }
                 int lockcount = 0;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ViewState["SemesterNo"] = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlExam.SelectedValue == "EXTERMARK")
                    {
                        if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LOCKS1"]) == 0)
                        {
                            objCommon.DisplayMessage(this.updpanle1, "Internal Mark Entry is not Done.", this.Page);
                            return;
                        }
                        gvStudent.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[2].Visible = true;
                    }
                    if (dsStudent.Tables[0].Rows[0]["SMAX"] != "0")
                    {
                        //gvStudent.Columns[4].HeaderText = "<center>" + ddlExam.SelectedItem.Text + "</center>" + "<span class='pull-left MaxMarks'>[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]</span>" + "<span class='pull-right'>[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]</span>";
                        gvStudent.Columns[4].HeaderText = "<center>" + ddlExam.SelectedItem.Text + "</center>" + "<center><span MaxMarks'>[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]</span></center>";
                        gvStudent.Columns[4].Visible = true;

                        hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                        hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];

                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                lockcount++;
                            }
                        }
                    }
                    else
                        gvStudent.Columns[4].Visible = false;
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Visible = false; btnLock.Visible = false;
                        objCommon.DisplayMessage(this.updpanle1, "Selected Exam Not Applicable for Mark Entry!!", this.Page);
                    }
                    else
                    {
                        btnSave.Visible = true; btnLock.Visible = true;
                    }

                    pnlSelection.Visible = false; pnlMarkEntry.Visible = true; pnlStudGrid.Visible = true; lblStudents.Visible = true;
                    //btnBack.Visible = true; 
                    btnSave.Visible = true; btnLock.Visible = true; btnPrintReport.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Students Not Found..!!", this.Page);
                }
                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                {
                    btnSave.Visible = false;
                    btnLock.Visible = false;
                    ViewState["LOCK_STATUS"] = "1";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    private void ShowStudents_For_Model_Exam()
    {
        try
        {
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;

            //DataSet ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + Convert.ToInt32(ViewState["COURSENO"]) + ") AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + "", "");

            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
            //    {
            //        objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 1 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
            //        return;
            //    }
            //    else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
            //    {
            //        objCommon.DisplayMessage(this.updpanle1, "STOP !!! Rule 2 for " + Convert.ToString(ddlSubExam.SelectedItem.Text) + " is not Defined", this.Page);
            //        return;
            //    }
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.updpanle1, "STOP !!! Exam Rule is not Defined", this.Page);
            //    return;
            //}

            string SP_Name = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_MODEL_EXAM";
            string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SECTIONNO, @P_SUBID, @P_UA_NO, @P_EXAM_NAME";
            string Call_Values = "" + ddlSession.SelectedValue + "," + Convert.ToInt32(ViewState["COURSENO"]) + "," + Convert.ToInt16(ViewState["sem"]) + "," + Convert.ToInt16(hdfSection.Value) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + Convert.ToString(ViewState["MODEL_EXAM_NAME"]) + "";

            dsStudent = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            int lockcount = 0;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ViewState["SemesterNo"] = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlExam.SelectedValue == "EXTERMARK")
                    {
                        gvStudent.Columns[2].Visible = false;
                    }
                    else
                    {
                        gvStudent.Columns[2].Visible = true;
                    }
                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        gvStudent.Columns[4].HeaderText = "<center>" + ddlExam.SelectedItem.Text + "</center>" + "<span class='pull-left MaxMarks'>[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]</span>" + "<span class='pull-right'>[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]</span>";
                        gvStudent.Columns[4].Visible = true;

                        hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                        hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];

                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                lockcount++;
                            }
                        }
                    }
                    else
                        gvStudent.Columns[4].Visible = false;
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Visible = false; btnLock.Visible = false;
                        objCommon.DisplayMessage(this.updpanle1, "Selected Exam Not Applicable for Mark Entry!!", this.Page);
                    }
                    else
                    {
                        btnSave.Visible = true; btnLock.Visible = true;
                    }

                    pnlSelection.Visible = false; pnlMarkEntry.Visible = true; pnlStudGrid.Visible = true; lblStudents.Visible = true;
                    //btnBack.Visible = true; 
                    btnSave.Visible = true; btnLock.Visible = true; btnPrintReport.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(this.updpanle1, "Students Not Found..!!", this.Page);
                }
                if (dsStudent.Tables[0].Rows.Count == Convert.ToInt32(lockcount)) // Checking the Marks lock for All Students
                {
                    btnSave.Visible = false;
                    btnLock.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    //methods to get marks entry status course wise..........added on [14-09-2016]
    private void GetStatus()
    {
        ViewState["Semester"] = string.Empty;
        if (Convert.ToString(Session["usertype"]) == "7")
        {
            ViewState["DEPTNO"] = (objCommon.LookUp("user_acc", "UA_DEPTNO", "ua_no='" + Convert.ToInt16(Session["userno"]) + "'"));
            ViewState["SCHEMENO"] = (objCommon.LookUp("ACD_SCHEME", "SCHEMENO", "DEPTNO='" + Convert.ToInt32(ViewState["DEPTNO"]) + "'"));
            DataSet ds = objMarksEntry.GetCourse_MarksEntryStatus_RECPIT_ADMIN(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO"]));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //GVEntryStatus.DataSource = ds;
                //GVEntryStatus.DataBind();
                ViewState["markentrystatus"] = ds.Tables[0].Rows[0]["MARK_ENTRY_STATUS"].ToString();                   //Added on 04082022
                ViewState["Semester"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                //Added on 04082022 

                rptExamName.DataSource = ds;
                rptExamName.DataBind();
                Div_ExamNameList.Visible = true;
            }
            else
            {
                //GVEntryStatus.DataSource = null;
                //GVEntryStatus.DataBind();
                rptExamName.DataSource = null;
                rptExamName.DataBind();
                lvCourse.Visible = false;
                Div_ExamNameList.Visible = false;
                objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page); //lblStatus.Visible = false;
            }
        }
        else
        {
            ViewState["SCHEMENO1"] = objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "schemeno", "COSCHNO=" + Convert.ToInt32(ddlscheme.SelectedValue));
            if (ViewState["SCHEMENO1"] == null || ViewState["SCHEMENO1"] == string.Empty)
            {
                ViewState["SCHEMENO1"] = 0;
            }
            DataSet ds = objMarksEntry.GetCourse_MarksEntryStatus_RECPIT_ADMIN(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(ViewState["SCHEMENO1"].ToString()));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //GVEntryStatus.DataSource = ds;
                //GVEntryStatus.DataBind();
                ViewState["markentrystatus"] = ds.Tables[0].Rows[0]["MARK_ENTRY_STATUS"].ToString();                   //Added on 04082022
                ViewState["Semester"] = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                //Added on 04082022 

                rptExamName.DataSource = ds;
                rptExamName.DataBind();
                Div_ExamNameList.Visible = true;
            }
            else
            {
                //GVEntryStatus.DataSource = null;
                //GVEntryStatus.DataBind();
                rptExamName.DataSource = null;
                rptExamName.DataBind();
                lvCourse.Visible = false;
                Div_ExamNameList.Visible = false;
                objCommon.DisplayMessage(this.updpanle1, "No Course Found For This Subject Type.", this.Page); //lblStatus.Visible = false;
            }
        }
    }

    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {

            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i == 0)
                    {
                        LinkButton cu = gridView.Rows[rowIndex].FindControl("lnkbtnCourse") as LinkButton;

                        LinkButton prev = gridView.Rows[rowIndex + 1].FindControl("lnkbtnCourse") as LinkButton;

                        if (cu.Text == prev.Text)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                   previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }


                    }
                    if (i == 4)
                    {
                        Button cu1 = gridView.Rows[rowIndex].FindControl("btnCourseWISE") as Button;
                        Button prev1 = gridView.Rows[rowIndex + 1].FindControl("btnCourseWISE") as Button;
                        if (cu1.ToolTip == prev1.ToolTip)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                                                              previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;
                        }
                    }
                }
            }
        }
    }

    //protected void GVEntryStatus_PreRender(object sender, EventArgs e)
    //{
    //    GridDecorator.MergeRows(GVEntryStatus);
    //}

    protected void GVEntryStatus_RowDataBound(object sender, GridViewRowEventArgs e)//..........added on [20-09-2016]
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[3].Text)
            {
                case "PENDING":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    e.Row.Cells[4].Enabled = false;
                    break;
                case "COMPLETED":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Green;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
                case "IN PROGRESS":
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Orange;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
                default:
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[3].BorderColor = System.Drawing.Color.Black;
                    break;
            };
        }
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false; lblStudents.Visible = false;
        btnSave.Visible = false; btnLock.Visible = false; btnPrintReport.Visible = false;
        if (ddlExam.SelectedIndex > 0)
        {
            ddlSubExam.Enabled = true;

            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "SUBID", "", "COURSENO=" + Convert.ToInt32(lblCourse.ToolTip) + "", "");

            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
            }
        }
        else if (ddlExam.SelectedIndex == 0)
        {
            ddlSubExam.SelectedIndex = 0;
            ddlSubExam.Enabled = false;
        }
    }

    protected void ddlSubExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false; lblStudents.Visible = false;
        btnSave.Visible = false; btnLock.Visible = false; btnPrintReport.Visible = false;

    }

    protected void gvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtMarks = (TextBox)e.Row.FindControl("txtMarks");

        }
    }

    protected void btncoursereport_Click(object sender, EventArgs e)
    {
        this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks.rpt");
    }

    private void ShowReportForcourse(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COURSENO=" + Convert.ToInt32(ViewState["Courseno"]) + ",@P_SECTIONNO=" + ViewState["section"].ToString() + ",@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_UANO=" + Convert.ToInt16(Session["userno"]);

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";

        string Script = string.Empty;
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.updpanle1, updpanle1.GetType(), "Report", Script, true);
    }

    protected void btnreportexamwise_Click(object sender, EventArgs e)
    {
        Button btnSelect = (sender as Button);
        ViewState["sec"] = btnSelect.CommandName;
        ViewState["CCODE"] = btnSelect.ToolTip;
        ViewState["Exam"] = btnSelect.CommandArgument;
        Button btn = sender as Button;
        ViewState["corseno"] = Convert.ToInt32((btn.Parent.FindControl("hdncorseno") as HiddenField).Value);
        ViewState["semester"] = Convert.ToInt32((btn.Parent.FindControl("hdnsemester") as HiddenField).Value);
        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW.rpt");//rptMarksList1.rpt
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {

        string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExam.SelectedItem.Text) + "'");
        string FULLMNAME = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Session["userno"]);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["CCODE"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_SUB_EXAM=" + ddlSubExam.SelectedValue + "";

        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        divMsg.InnerHtml += " </script>";
        //update panel
        string Script = string.Empty;
        Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        ScriptManager.RegisterClientScriptBlock(this.updpanle1, updpanle1.GetType(), "Report", Script, true);
    }

    protected void btnCourseWISE_Click(object sender, EventArgs e)
    {
        Button btnSelectcourse = (sender as Button);
        ViewState["section"] = btnSelectcourse.CommandArgument;

        ViewState["Courseno"] = btnSelectcourse.CommandName;
        this.ShowReportForcourse("CourseWiseMarks", "CourseWise_Marks.rpt");
    }

    private void GetExamWiseDates()
    {
        try
        {
            DataSet dsmemssage = objCommon.FillDropDown("ACTIVITY_MASTER A, SESSION_ACTIVITY S,ACD_EXAM_NAME E", "E.FLDNAME,E.EXAMNAME", "S.END_DATE", "A.ACTIVITY_NO = S.ACTIVITY_NO AND A.EXAMNO = E.EXAMNO AND CONVERT(DATE,GETDATE(),103) >= CONVERT(DATE,S.START_DATE,103) AND CONVERT(DATE,GETDATE(),103) <= CONVERT(DATE,S.END_DATE,103)AND S.STARTED = 1 AND UA_TYPE COLLATE DATABASE_DEFAULT LIKE '%" + Convert.ToString(Session["usertype"]) + "%' AND PAGE_LINK LIKE '%" + Convert.ToString(Request.QueryString["pageno"].ToString()) + "%' AND S.SESSION_NO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND E.EXAMNAME <> '' ", "");
            if (dsmemssage != null && dsmemssage.Tables[0].Rows.Count > 0)
            {
                string message = string.Empty;
                string stopDate = string.Empty;
                for (int i = 0; i < dsmemssage.Tables[0].Rows.Count; i++)
                {
                    string examname = dsmemssage.Tables[0].Rows[i]["EXAMNAME"].ToString();
                    string enddate = dsmemssage.Tables[0].Rows[i]["END_DATE"].ToString();
                    if (enddate != string.Empty || enddate != "")
                    {
                        DateTime statusdate = Convert.ToDateTime(enddate);
                        string status = statusdate.ToString("d");
                        if (status != string.Empty || status != "")
                        {
                            //divstatus.Visible = true;
                            //message += "  " + examname + " - " + status;
                            message += " " + examname + ",";
                            stopDate += " " + status + ",";
                        }
                    }
                }
                //lblsession.Text = ddlSession.SelectedItem.Text.Trim();
                //lblstatusmark.Text = message.Substring(0, message.Length - 1);
                //lblStopDate.Text = stopDate.Substring(0, stopDate.Length - 1);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.GetExamWiseDates --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
            objCommon.DisplayMessage(ex.ToString(), this.Page);
        }
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSession.SelectedValue) > 0)
        {
            objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", "DISTINCT R.SUBID", "SUBNAME", "S.SUBID > 0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "");
            this.GetExamWiseDates();
        }
        else
        {
            //lblsession.Text = string.Empty;
            //lblstatusmark.Text = string.Empty;
            //divstatus.Visible = false;
        }
        ddlSubjectType.SelectedIndex = 0;
        //GVEntryStatus.DataSource = null;
        //GVEntryStatus.DataBind();
        rptExamName.DataSource = null;
        rptExamName.DataBind();
    }

    //Patch For Adding Mark Entry Patch OTP
    private string GenerateOTP()
    {
        string allowedChars = "";

        allowedChars += "1,2,3,4,5,6,7,8,9,0";
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }

    public void SendSMS(string Mobile, string text)  //added by Raju.. send sms method
    {
        string status = "";
        try
        {
            string Message = string.Empty;

            DataSet ds = objCommon.FillDropDown("Reff", "SMSSVCID", "SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + "www.SMSnMMS.co.in/sms.aspx" + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }
        }
        catch
        {

        }
    }

    public string MessageBody(string FullName, string Email, string course_name, string otp, string exam_name)
    {
        const string EmailTemplate = "<html><body>" +
                              "<div align=\"center\">" +
                              "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                               "<tr>" +
                               "<td>" + "</tr>" +
                               "<tr>" +
                              "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                              "</tr>" +
                              "</table>" +
                              "</div>" +
                              "</body></html>";
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>Greetings !!</h1>");
        mailBody.AppendFormat("Dear" + " " + "<b>" + FullName + "," + "</b>");   //b
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<br />");

        mailBody.AppendFormat("<b>" + otp + "</b>" + " is your One-Time Password (OTP) to lock mark for <b>" + exam_name + "</b>");       //b

        mailBody.AppendFormat(" Exam of " + "<b>" + course_name + "." + "</b>" + "<br/><br/>");       //b               

        mailBody.AppendFormat("This is an auto generated response to your email. Please do not reply ");
        mailBody.AppendFormat("to this email " + "</br>" + " as it will not be received. For any discrepancy you may ");
        mailBody.AppendFormat("write to us at " + "<b>" + " jss.st.university@gmail.com" + "</b>");
        mailBody.AppendFormat("<br /><br /><br /><br />Regards,<br />");   //bb
        mailBody.AppendFormat("MIS JSS Team<br /><br />");   //bb

        string Mailbody = mailBody.ToString();
        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
        return nMailbody;
    }

    protected void btnOTPLockMarks_Click(object sender, EventArgs e)
    {
        pnlOTP.Visible = true;
        if (Session["OTP"].ToString() == txtOTP.Text.ToString())
        {
            SaveAndLock(1);
            txtOTP.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#myModal33').modal('hide')", true);
            Session["OTP"] = "";
        }

        else
        {
            objCommon.DisplayMessage(this.UpdOTP, "OTP is mismatched ! Please Enter Correct OTP", this.Page);
            txtOTP.Text = string.Empty;
        }
    }

    protected void btnPrintReport_Click(object sender, EventArgs e)
    {
        if (ddlSubExam.Visible == false)
        {
            string reportTitle = "MarksListReport";
            string rptFileName = "rptMarksList2_NEW.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(hdfSection.Value) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_EXAM_NAME=" + Convert.ToString(ViewState["MODEL_EXAM_NAME"]) + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpanle1, this.updpanle1.GetType(), "key", Print_Val, true);
        }
        else
        {
            this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW.rpt");//rptMarksList1.rpt
        }
    }

    protected void lbtnPrint_Click(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)(sender);
        ViewState["courseNo_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[0]);
        lbl_SubjectName.Text = lbtn.CommandArgument.Split(',')[1];
        ViewState["sem_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[2]);
        ViewState["sec_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[3]);
        ViewState["examNo_POP"] = Convert.ToInt32(lbtn.CommandArgument.Split(',')[4]);
        ViewState["examName_POP"] = Convert.ToString(lbtn.CommandArgument.Split(',')[5]);
        ViewState["fldname_POP"] = Convert.ToString(lbtn.CommandArgument.Split(',')[6]);

        ViewState["ccode_POP"] = lbl_SubjectName.Text.Split('~')[0];

        ddlExamPrint.Items.Clear();
        ddlExamPrint.Items.Add(new ListItem("Please Select", "0"));
        ddlExamPrint.Items.Add(new ListItem(ViewState["examName_POP"].ToString(), ViewState["examNo_POP"].ToString()));

        ddlSubExamPrint.Items.Clear();
        ddlSubExamPrint.Items.Add(new ListItem("Please Select", "0"));


        if (Convert.ToString(ViewState["fldname_POP"]) == "S10")
        {
            ddlSubExamPrint.Visible = false;
            lbl_SubExam_Print.Visible = false;
            btnPrintFront.Enabled = true;
        }
        else
        {
            ddlSubExamPrint.Visible = true;
            lbl_SubExam_Print.Visible = true;
            ddlSubExamPrint.Enabled = false;
            btnPrintFront.Enabled = false;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#PrintModal').modal('show');</script>", false);
        updPopUp.Update();
    }


    protected void ddlExamPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamPrint.SelectedIndex != 0)
        {
            DataSet ds = objCommon.FillDropDown("ACD_COURSE", "SUBID", "", "COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "", "");

            if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 1)
            {
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + "AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "");
                ddlSubExamPrint.Enabled = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME", "TOP(1) CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SUBID"].ToString()) + " AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "");
                ddlSubExamPrint.Enabled = true;
            }
            ddlSubExamPrint.Enabled = true;
        }
        else
        {
            ddlSubExamPrint.Enabled = false;
        }

    }

    protected void ddlSubExamPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubExamPrint.SelectedIndex != 0)
        {
            btnPrintFront.Enabled = true;
        }
        else
        {
            btnPrintFront.Enabled = false;
        }
    }

    #region btnPrintFront_Click
    protected void btnPrintFront_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#PrintModal').modal('hide');</script>", false);

        if (ddlSubExamPrint.Visible == false)
        {
            string reportTitle = "MarksListReport";
            string rptFileName = "rptMarksList2_NEW.rpt";

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_EXAM_NAME=" + ddlExamPrint.SelectedItem.Text + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
        }
        else
        {
            string reportTitle = "MarksListReport";
            string rptFileName = "rptMarksList1_NEW.rpt";

            string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_SUB_EXAM=" + ddlSubExamPrint.SelectedValue + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
        }
    }
    #endregion

    //protected void btnPrintAll_Click(object sender, EventArgs e)
    //{
    //    if (ddlExamPrint.SelectedIndex > 0)
    //    {
    //        string reportTitle = "CatMarksListReport";
    //        string rptFileName = "rptMarksList_Examwise.rpt";
    //        //string rptFileName = "rptMarksList_ExamwiseNew.rpt";
    //        string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;

    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_EXAM=" + fldname.ToString() + "";
    //        // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

    //        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
    //    }
    //    else
    //    {
    //        string reportTitle = "CatMarksListReport";
    //        // string rptFileName = "rptMarksList_Examwise.rpt";
    //        string rptFileName = "rptMarksList_ExamwiseNew.rpt";
    //        string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;

    //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";
    //        // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

    //        string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
    //    }
    //}

    protected void btnPrintAll_Click(object sender, EventArgs e)
    {
        if (ddlExamPrint.SelectedIndex > 0)
        {

            string reportTitle = "CatMarksListReport";
            string rptFileName = "rptMarksList_Examwise.rpt";
            //string rptFileName = "rptMarksList_ExamwiseNew.rpt";
            string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + ",@P_EXAM=" + fldname.ToString() + "";
            // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);

        }
        else
        {

            int subid = Convert.ToInt32(ddlSubjectType.SelectedValue);
            if (subid == 1)
            {

                string reportTitle = "CatMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew_ForTheory.rpt";
                string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";
               //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);
            }
            else
            {

                string reportTitle = "CatMarksListReport";
                // string rptFileName = "rptMarksList_Examwise.rpt";
                string rptFileName = "rptMarksList_ExamwiseNew.rpt";
                string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlExamPrint.SelectedItem.Text) + "'");
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;

                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UANO=" + Convert.ToInt32(Session["userno"]) + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";
                // url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"]) + ",@P_CCODE=" + ViewState["ccode_POP"].ToString() + ",@P_SECTIONNO=" + Convert.ToString(ViewState["sec_POP"]) + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + fldname.ToString() + ",@P_semesterno=" + Convert.ToInt32(ViewState["sem_POP"]) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["courseNo_POP"]) + "";

                string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                ScriptManager.RegisterClientScriptBlock(this.updPopUp, this.updPopUp.GetType(), "key", Print_Val, true);

            }
        }
    }


    //ADDED BY SACHIN A 03082022 TO SAVE MARK EXCEL SHEET THROUGH

    #region ExcelSheet MarkEntry
    protected void lnkExcekImport_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0 && ddlSubExam.SelectedIndex > 0)
        {          

            if (ViewState["LOCK_STATUS"] == "")
            {
                pnlUP.Visible = true;
            }
            else
            {
                pnlUP.Visible = false;
                objCommon.DisplayMessage(updpanle1, "Mark Entry is locked!", this.Page);
            }


        }
    }

    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0)      //&& (ddlSubExam.SelectedIndex != 0)
        {
            try
            {
                string excelname = string.Empty;
                string[] course = lblCourse.Text.Split('-');
                DataSet dsStudent = null;

                ViewState["StudCount"] = 0;
                int MExamNo = Convert.ToInt32(ViewState["examNo"].ToString());
                string subexamno = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "ISNULL(ACTIVESTATUS,0)=1 AND EXAMNO=" + MExamNo + "AND FLDNAME='" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0] + "'");

                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4) //ADDED BY GAURAV FOR CPUK/H 21_01_2023
                {
                    dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), MExamNo, Convert.ToInt32(ViewState["COURSENO"]), subexamno);

                }
                else
                {
                    //dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToInt32(Examno[0]), Convert.ToInt32(ViewState["COURSENO"]));
                    dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_IA_ADMIN_RCPIT(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), MExamNo, Convert.ToInt32(ViewState["COURSENO"]), subexamno);

                }

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        //excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");
                        excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + ddlSubExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");

                        ViewState["StudCount"] = dsStudent.Tables[0].Rows.Count;
                        //Bind the Student List
                        DataTable dst = dsStudent.Tables[0];
                        DataGrid dg = new DataGrid();
                        if (dsStudent != null && dsStudent.Tables.Count > 0)
                        {
                            if (dsStudent.Tables[0].Rows.Count > 0)
                            {
                                string[] selectedColumns = new[] { "IDNO", "STUDNAME", "REGNO1", "CCODE", "COURSENAME", "DEGREENAME", "BRANCHNAME", "SCHEMENAME", "SEMESTERNAME", "SESSIONNAME", "EXAMNAME", "SUBEXAMNAME", "SECTIONNAME", "MAXMARK" };

                                DataTable dt = new DataView(dst).ToTable(false, selectedColumns);
                                dt.Columns["REGNO1"].ColumnName = "REGNO / ROLL_NO"; // change column names
                                //dt.Columns["SMAX"].ColumnName = "MAX MARKS"; // change column names
                                dt.Columns.Add("MARKS");

                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    //foreach (System.Data.DataTable dtt in dsStudent.Tables)
                                    //{
                                    //Add System.Data.DataTable as Worksheet.
                                    wb.Worksheets.Add(dt);
                                    //}

                                    //Export the Excel file.
                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".xlsx");
                                    using (MemoryStream MyMemoryStream = new MemoryStream())
                                    {
                                        wb.SaveAs(MyMemoryStream);
                                        MyMemoryStream.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        Response.End();
                                    }
                                }
                            }
                        }

                        pnlSelection.Visible = false;
                        pnlMarkEntry.Visible = true;
                        pnlStudGrid.Visible = true;
                        lblStudents.Visible = true;
                        btnBack.Visible = true;

                    }
                    else
                    {
                        //objCommon.DisplayMessage(updpanle1, "Students Not Found..!!", this.Page);
                    }
                }

            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.ShowStudents --> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server Unavailable.");
                objCommon.DisplayMessage(ex.ToString(), this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(updpanle1, "Please Select Exam!!", this.Page);
            ddlExam.Focus();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubExam.SelectedIndex != 0)
            {
                if (FuBrowse.HasFile)           //  if (FuBrowse.HasFile) //(FuBrowse.PostedFile != null)
                {
                    string path = MapPath("~/ExcelData/");
                    ViewState["FileName"] = string.Empty;
                    string filename = FuBrowse.FileName.ToString();
                    string Extension = Path.GetExtension(filename);
                    string Filepath = Server.MapPath("~/ExcelData/" + filename);
                    // CreateBlobContainer(blob_ContainerName); 
                    CreateBlobContainer(blob_ContainerName);
                    if (filename.Contains(".xls") || filename.Contains(".xlsx"))
                    {
                        ViewState["FileName"] = filename;
                        //FuBrowse.SaveAs(path + filename + ".xls");
                        //FuBrowse.SaveAs(path + filename);
                        FuBrowse.SaveAs(path + filename);// To save file in code folder to validate marks.

                        //DataTable dt = AL.Blob_GetAllBlobs(blob_ConStr, blob_ContainerName);
                        //gdvBlobs.DataSource = dt;
                        //gdvBlobs.DataBind();                  


                        if (!CheckFormatAndImport(Extension, Filepath, "yes"))
                        {
                            File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                        }
                        else
                        {

                            int retval = Blob_Upload(blob_ConStr, blob_ContainerName, filename, FuBrowse);
                            if (retval == 0)
                            {
                            }
                            else
                            {
                                File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                            }
                            ShowStudents();
                            pnlUP.Visible = false;
                            objCommon.DisplayMessage(updpanle1, "Mark Entry Uploaded Successfully !", this);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpanle1, "Only Excel Sheet is Allowed!", this);
                        return;
                    }

                }

                else
                {
                    objCommon.DisplayMessage(updpanle1, "Select File to Upload!!!", this);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpanle1, "Please Select SubExam!!!", this);
                return;
            }

        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(this.Page, " ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private bool CheckFormatAndImport(string Extension, string FilePath, string isHDR)  //bool
    {
        string filename = ViewState["FileName"].ToString();
        Exam objExam = new Exam();
        string conStr = "";
        switch (Extension)
        {
            case ".xls": //Excel 97-03
                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07
                conStr = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;
        }

        conStr = String.Format(conStr, FilePath);

        string Message = string.Empty;
        int count = 0;
        OleDbConnection connExcel = new OleDbConnection(conStr);
        try
        {

            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataSet ds = null;
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            //Check for lock and null marks
            if (CheckExcelMarks(0, dt) == false)
            {
                return false;
            }
            else
            {
                SaveExcelMarks(0, dt, Convert.ToInt32(ViewState["Semester"].ToString()));            //04082022
                return true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Please Check if the data is saved in sheet1 of the file you are uploading or the file is in correct format!! ACADEMIC_StudentFileUpload->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(this.Page, "Server UnAvailable");
            return false;
        }
        finally
        {
            connExcel.Close();
            connExcel.Dispose();
        }
    }
    protected void CreateBlobContainer(string Name)
    {

        //Get the reference of the Storage Account
        CloudStorageAccount storageaccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString());
        //Get the reference of the Storage Blob

        CloudBlobClient client = storageaccount.CreateCloudBlobClient();

        //Get the reference of the Container. The GetConainerReference doesn't make a request to the Blob Storage but the Create() &CreateIfNotExists() method does. The method CreateIfNotExists() could be use whether the Container exists or not
        CloudBlobContainer container = client.GetContainerReference(Name);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public void DeleteIFExits(string FileName)
    {
        //CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        //string FN = Path.GetFileNameWithoutExtension(FileName);
        //try
        //{
        //    Parallel.ForEach(container.ListBlobs(FN, true), y =>
        //    {
        //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //        ((CloudBlockBlob)y).DeleteIfExists();
        //    });
        //}
        //catch (Exception)
        //{
        //}
    }

    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
           

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }

    private bool CheckExcelMarks(int lock_status, DataTable dt)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txt;
            string marks = string.Empty;
            string maxMarks = string.Empty;
            DataColumnCollection columns = dt.Columns;
            if (columns.Contains("MARKS"))
            {
                for (int j = 13; j < dt.Columns.Count; j++)    //columns
                {
                    for (int i = 0; i < dt.Rows.Count; i++)   //rows 
                    {

                        //if (gvStudent.Columns[j].Visible == true)
                        //{
                        if (j == 13) // MARKS
                        {
                            //lbl = gvStudent.Rows[i].Cells[j].FindControl("lblMarks") as Label;      //Max Marks 
                            //txt = gvStudent.Rows[i].Cells[j].FindControl("txtMarks") as TextBox;    //Marks Entered 
                            //maxMarks = maxMarks.Trim();
                            //marks = dt.Rows[i]["MARKS"].ToString();
                            maxMarks = dt.Rows[i]["MAXMARK"].ToString();
                            marks = dt.Rows[i]["MARKS"].ToString();
                            if (!marks.Equals(string.Empty) && !maxMarks.Equals(string.Empty))
                            {
                                if (marks == "")
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(updpanle1, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                        //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");

                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    //Check for Marks entered greater than Max Marks
                                    if (Convert.ToDouble(marks) > Convert.ToDouble(maxMarks))
                                    {

                                        if (Convert.ToDouble(marks) != 902 && Convert.ToDouble(marks) != 903 && Convert.ToDouble(marks) != 904 && Convert.ToDouble(marks) != 905 && Convert.ToDouble(marks) != 906)
                                        {
                                            ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero). Only -1, -2, -3 and -4 are allowed.");

                                            flag = false;
                                            break;
                                        }
                                    }
                                    else if (Convert.ToDouble(marks) < 0)
                                    {
                                        //Note : 401 for Absent and Not Eligible
                                        if (Convert.ToDouble(marks) == -1 || Convert.ToDouble(marks) == -2 || Convert.ToDouble(marks) == -3 || Convert.ToDouble(marks) == -4)
                                        {
                                        }
                                        else
                                        {
                                            ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero). Only -1, -2, -3 and -4 are allowed.");

                                            flag = false;
                                            break;
                                        }
                                    }
                                }

                                ////Check for Marks entered greater than Max Marks
                                //if (Convert.ToInt16(marks) > Convert.ToInt16(maxMarks))
                                //{
                                //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                                //    if (Convert.ToInt16(marks) == 401 || Convert.ToInt16(marks) == 402 || Convert.ToInt16(marks) == 403)
                                //    {
                                //    }
                                //    else
                                //    {
                                //        ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "]");
                                //        txt.Focus();
                                //        flag = false;
                                //        break;
                                //    }
                                //}
                            }
                            else
                            {

                                //if (lock_status == 1)
                                //{

                                ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                flag = false;
                                break;
                                //}

                            }
                        }
                        //}
                        if (flag == false)
                            break;
                    }
                }
            }
            else
            {
                ShowMessage("Invalid Excel File !!");
                flag = false;

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }
    private void SaveExcelMarks(int lock_status, DataTable dt, int semno)
    {
        try
        {
            string examtype = string.Empty;
            string subExam = string.Empty;
            CustomStatus cs = CustomStatus.Error;
            CustomStatus log = CustomStatus.Error;
            string file_name = ViewState["FileName"].ToString();

            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                //subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "SUBEXAMNO=" + ddlExam.SelectedValue + "");   //
                //subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "SUBEXAMNO=" + ViewState["examNo"].ToString() + "");
                subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "EXAMNO=" + ViewState["examNo"].ToString() + "AND FLDNAME='" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0] + "'");

                string studids = string.Empty;
                string marks = string.Empty;

                MarksEntryController objMarksEntry = new MarksEntryController();
                Label lbl;
                TextBox txtMarks;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //Note : -100 for Marks will be converted as NULL           
                //NULL means mark entry not done.                           
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    studids += dt.Rows[i]["IDNO"].ToString() + ",";
                    marks += dt.Rows[i]["MARKS"].ToString() == string.Empty ? "-100," : dt.Rows[i]["MARKS"].ToString() + ",";
                }
                int sectionno = Convert.ToInt32(hdfSection.Value);
                int courseno = Convert.ToInt32(lblCourse.ToolTip);
                string[] course = lblCourse.Text.Split('~');
                string ccode = course[0].Trim();
                examtype = "S";

                //return;
                if (!string.IsNullOrEmpty(studids))
                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryForSubExam_RCPIT_ADMIN(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["examNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

                ///TO SAVE BLOG LOG//////////////
                log = (CustomStatus)objMarksEntry.InsertMarkEntryBlobLog(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["examNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, file_name);
                //////////////

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(updpanle1, "Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        ShowStudents();
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpanle1, "Marks Saved Successfully.", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        ShowStudents();
                    }
                }
                else
                    objCommon.DisplayMessage("Error in Saving Marks!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region not in used
    private void SaveExcelData()
    {
        try
        {
            CustomStatus cs = CustomStatus.Error;
            string examtype = string.Empty;
            string subExam = string.Empty;
            //excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + ddlSubExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");
            string userfullname = Session["username"].ToString();
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int courseno = Convert.ToInt32(lblCourse.ToolTip);
            string[] course = lblCourse.Text.Split('~');
            string ccode = course[0].Trim();
            int exam = Convert.ToInt32(ViewState["ExamNo"]);
            int user = Convert.ToInt32(Session["userno"]);
            string ipaddress = ViewState["ipAddress"].ToString();
            int subtype = Convert.ToInt16(ddlSubjectType.SelectedValue);
            string filename = ViewState["FileName"].ToString();
            int sectionno = Convert.ToInt32(hdfSection.Value);
            if (ddlExam.SelectedIndex > 0)
            {
                // subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "SUBEXAMNO=" + ViewState["examNo"].ToString() + "");
                subExam = objCommon.LookUp("ACD_SUBEXAM_NAME WITH (NOLOCK)", "DISTINCT FLDNAME", "EXAMNO=" + ViewState["examNo"].ToString() + "AND FLDNAME='" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[0] + "'");
            }
            else
            {
            }
            if (!string.IsNullOrEmpty(filename))
                // cs = (CustomStatus)objMarksEntry.UpdateMarkEntryForSubExam(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["ExamNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updpanle1, "Marks Locked Successfully!!!", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(updpanle1, "Marks Saved Successfully.", this.Page);
                }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_PracticalMarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    #endregion

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        pnlUP.Visible = false;                      //Added by Sachin A on 16082022
    }

    #endregion

    //Added by Gaurav dt on 12092022 for to check mark entry lock status

    #region For JECRC Check For EndSem Mark Entry Done Or not
    private int GetStatusForMarkEntry()
    {
        int ret = 0;
        int courseno = Convert.ToInt32(ViewState["COURSENO"]);
        //ViewState["Semester"] = string.Empty;
        DataSet ds = objMarksEntry.GetCourse_MarksEntryStatusForMArkEntry(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue), courseno);


        int lockcount = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (Convert.ToInt32(ds.Tables[0].Rows[i]["LOCKSTATUS"]) == 1)
            {
                lockcount++;
            }
        }

        if (lockcount == 1 || lockcount == 0)
        {

            objUCommon.DisplayMessage(this.pnlSelection, "FIRST COMPLETE YOUE INTERNAL COMPONENT MARKS ENTRY..!!!", this);
            ret = 1;

        }
        return ret;

    }
    #endregion
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            lvCourse.Visible = false;
            Div_ExamNameList.Visible = false;
        }
        else
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");
            lvCourse.Visible = false;
            Div_ExamNameList.Visible = false;
        }
      
       
    }
}


