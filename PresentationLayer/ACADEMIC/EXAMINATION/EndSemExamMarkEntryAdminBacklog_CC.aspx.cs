//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - MARK ENTRY BY ADMIN                                          
// CREATION DATE : 16-APRIL-2019                                                     
// CREATED BY    : ROHIT KUMAR TIWARI                                            
// MODIFIED BY   :                                                  
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



public partial class Academic_MarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

    // string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    //string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_MEContainerName"].ToString();
    //  string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();

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
                this.PopulateDropDown();
                string excelStatus = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ExcelMarkEntry", "");
                if (excelStatus == "1")
                {
                    lnkExcekImport.Visible = true;
                }
                else
                {
                    lnkExcekImport.Visible = false;
                }

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    btnUnlock.Visible = true;
                }
                else
                {
                    btnUnlock.Visible = false;
                }

                if (Request.QueryString["pageno"] != null)
                {
                    int ADMIN_LEVEL_MARKS_ENTRY_USER = Convert.ToInt32(objCommon.LookUp("REFF", "isnull(ADMIN_LEVEL_MARKS_ENTRY,0)", ""));

                    //if (Convert.ToInt32(Session["userno"]) == ADMIN_LEVEL_MARKS_ENTRY_USER) //Check Marks Entry Admin Level User.
                    //{
                    //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO desc");
                    //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");

                    //objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "", "SUBID");
                    //ddlSession.SelectedIndex = 1;
                    //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')COLLEGE_NAME","C.COLLEGE_ID > 0", "C.COLLEGE_ID");
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    if (ddlSession.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage("The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                        pnlMarkEntry.Visible = false;
                    }
                    //}
                    //else
                    //{
                    //    CheckPageAuthorization();
                    //}
                }
                else
                {
                    CheckPageAuthorization();

                }
            }
        }
        divMsg.InnerHtml = string.Empty;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //PopulateDropDown();
    }
    private void PopulateDropDown()
    {
        try
        {

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0", "SESSIONNO DESC");

            DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
                ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
            }
            //Term
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            //ddlSession.SelectedIndex = 1;
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //College Name
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
            }
            else
            {
                //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+SHORT_NAME +'-'+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND COLLEGE_ID IN (" + ViewState["College_ids"].ToString() + ")", "COLLEGE_ID");
                // objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
            }

            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            //ddlSession.Items.Clear();
            //ddlSession.Items.Add(new ListItem("Please Select", "0"));

            ddlbranch.Items.Clear();
            ddlbranch.Items.Add(new ListItem("Please Select", "0"));

            ddlscheme.Items.Clear();
            ddlscheme.Items.Add(new ListItem("Please Select", "0"));

            ddlsemester.Items.Clear();
            ddlsemester.Items.Add(new ListItem("Please Select", "0"));

            ddldegree.Items.Clear();
            ddldegree.Items.Add(new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_EndSemExamMarkEntry.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //0 - means - unlock
        SaveAndLock(0);
    }

    #region Private/Public Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry.aspx");
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
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            try
            {
                string examtype = string.Empty;
                string Subexam = string.Empty;
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
                    CheckBox chk;

                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    //Note : -100 for Marks will be converted as NULL           
                    //NULL means mark entry not done.                           
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;

                        if (lock_status == 0)
                        {

                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";

                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";

                        }
                        else if (lock_status == 1 || lock_status == 2)
                        {
                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";

                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                        }
                    }
                    studids = studids.TrimEnd(',');

                    if (studids == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                        return;
                    }

                    string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);


                    #region Comment by Mahesh on Dated 24/06/2021

                    //if (ddlExam.SelectedValue.StartsWith("S"))
                    //    examtype = "S";
                    //else if (ddlExam.SelectedValue.StartsWith("E"))
                    //    examtype = "E";
                    //string examname = string.Empty;
                    //if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                    //    examname = ddlExam.SelectedValue.Substring(2);
                    //else if (ddlExam.SelectedIndex > 0)
                    //    examname = ddlExam.SelectedValue;

                    #endregion Comment by Mahesh on Dated 24/06/2021
                    string[] Exam = ddlExam.SelectedValue.Split('-');



                    if (Exam[1].StartsWith("S"))
                        examtype = "S";
                    else if (Exam[1].StartsWith("E"))
                        examtype = "E";

                    string examname = string.Empty;

                    examname = Exam[1]; //Column Name like S1,S2.....EXTERMARK

                    string SubExamName = string.Empty;
                    string SubExamComponentName = string.Empty;


                    if (divSubExamName.Visible == true)
                    {
                        if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                        {
                            Subexam = ddlSubExamName.SelectedValue;
                            SubExamComponentName = ddlSubExamName.SelectedItem.Text;
                        }
                    }
                    else
                    {
                        SubExamComponentName = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNAME", "EXAMNO=" + Exam[0]); ;
                        Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);
                    }

                    //CustomStatus cs = (CustomStatus)objMarksEntry.UpdateMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype); Comment by Mahesh C. Malve On Dated 24/06/2021  //
                    //CustomStatus cs = (CustomStatus)objMarksEntry.AdminUpdateMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), SubExamName, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(Exam[0]), SubExamComponentName);
                    CustomStatus cs = 0;
                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                    {
                        cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), (ddlSubExamName.SelectedValue).Split('-')[1], Convert.ToInt32(Exam[0]), SubExamComponentName);
                    }
                    else
                    {
                        cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Subexam, Convert.ToInt32(Exam[0]), SubExamComponentName);
                    }

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (lock_status == 1)
                        {
                            objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        }
                        else if (lock_status == 2)
                        {
                            objCommon.DisplayMessage(updpnl, "Marks Unlocked Successfully!!!", this.Page);
                        }
                        else
                            objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);

                        //btnReport.Enabled = true;
                        ShowStudents();
                    }
                    else
                        objCommon.DisplayMessage(updpnl, "Error in Saving Marks!", this.Page);
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


        else
        {


            try
            {
                string examtype = string.Empty;
                string Subexam = string.Empty;
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
                    CheckBox chk;

                    string[] Exam = ddlExam.SelectedValue.Split('-');



                    if (Exam[0].StartsWith("S"))
                        examtype = "S";
                    else if (Exam[0].StartsWith("E"))
                        examtype = "E";

                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    //Note : -100 for Marks will be converted as NULL           
                    //NULL means mark entry not done.                           
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;

                        if (lock_status == 0)
                        {

                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";
                            if (examtype == "S")
                            {
                                //Gather Exam Marks 
                                txtMarks = gvStudent.Rows[i].FindControl("txtintMarks") as TextBox;
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            }
                            else
                            {
                                txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            }

                        }
                        else if (lock_status == 1 || lock_status == 2)
                        {
                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";

                            //Gather Exam Marks 

                            if (examtype == "S")
                            {
                                //Gather Exam Marks 
                                txtMarks = gvStudent.Rows[i].FindControl("txtintMarks") as TextBox;
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            }
                            else
                            {
                                txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                                marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                            }
                        }
                    }
                    studids = studids.TrimEnd(',');

                    if (studids == string.Empty)
                    {
                        objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                        return;
                    }

                    string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);


                    #region Comment by Mahesh on Dated 24/06/2021

                    //if (ddlExam.SelectedValue.StartsWith("S"))
                    //    examtype = "S";
                    //else if (ddlExam.SelectedValue.StartsWith("E"))
                    //    examtype = "E";
                    //string examname = string.Empty;
                    //if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                    //    examname = ddlExam.SelectedValue.Substring(2);
                    //else if (ddlExam.SelectedIndex > 0)
                    //    examname = ddlExam.SelectedValue;

                    #endregion Comment by Mahesh on Dated 24/06/2021


                    string examname = string.Empty;

                    examname = Exam[0]; //Column Name like S1,S2.....EXTERMARK

                    string SubExamName = string.Empty;
                    string SubExamComponentName = string.Empty;


                    if (divSubExamName.Visible == true)
                    {
                        if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                        {
                            Subexam = ddlSubExamName.SelectedValue;
                            SubExamComponentName = ddlSubExamName.SelectedItem.Text;
                        }
                        if (examtype == "S")
                        {
                            SubExamComponentName = ddlSubExamName.SelectedValue;
                            examname = ddlExam.SelectedValue;
                        }
                    }
                    else
                    {
                        SubExamComponentName = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNAME", "EXAMNO=" + Exam[1]); ;
                        Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[1]);
                    }

                    CustomStatus cs = 0;
                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                    {
                        if (examtype == "S")
                        {
                            //cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);

                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);
                        }
                        else
                        {
                            cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[1], Convert.ToInt32(Exam[1]), SubExamComponentName);
                        }
                    }
                    else
                    {

                        if (examtype == "S")
                        {
                            //cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);

                            cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);
                        }
                        else
                        {
                            cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
                        }
                    }

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        if (lock_status == 1)
                        {
                            objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                            lnkExcekImport.Visible = false;
                        }
                        else if (lock_status == 2)
                        {
                            objCommon.DisplayMessage(updpnl, "Marks Unlocked Successfully!!!", this.Page);
                        }
                        else
                            objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);

                        //btnReport.Enabled = true;
                        ShowStudents();
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        ShowStudents();
                    }
                    else
                        objCommon.DisplayMessage(updpnl, "Error in Saving Marks!", this.Page);
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
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[3].Visible == true) return flag;
        return false;
    }

    private bool CheckMarks(int lock_status)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1)
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
                                            objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                                                objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                                //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                        else if (Convert.ToDouble(txt.Text) < 0)
                                        {
                                            //Note : 401 for Absent and Not Eligible
                                            if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                            {
                                            }
                                            else
                                            {
                                                objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
                                                //ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }

                                    ////Check for Marks entered greater than Max Marks
                                    //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                                    //{
                                    //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                                    //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                                    //    {
                                    //    }
                                    //    else
                                    //    {
                                    //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                    //        txt.Focus();
                                    //        flag = false;
                                    //        break;
                                    //    }
                                    //}
                                }
                                else
                                {
                                    if (txt.Enabled == true)
                                    {
                                        if (lock_status == 1)
                                        {
                                            objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                            //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                            txt.Focus();
                                            flag = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        #region Not Needed Commented by Manish
                        //if (gvStudent.Columns[4].Visible == true)
                        //{
                        //    if (j == 3) //CT/FE MARKS
                        //    {
                        //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT1Marks") as Label;      //Max Marks 
                        //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT1Marks") as TextBox;    //Marks Entered 
                        //        maxMarks = lbl.Text.Trim();
                        //        marks = txt.Text.Trim();

                        //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                        //        {
                        //            if (txt.Text == "")
                        //            {
                        //                ShowMessage("Marks Entry Not Completed!!!");
                        //                txt.Focus();
                        //                flag = false;
                        //                break;
                        //            }
                        //            else
                        //            {
                        //                //Check for Marks entered greater than Max Marks
                        //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                        //                {
                        //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                        //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                        //                    {
                        //                    }
                        //                    else
                        //                    {
                        //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                        //                        txt.Focus();
                        //                        flag = false;
                        //                        break;
                        //                    }
                        //                }
                        //            }

                        //            ////Check for Marks entered greater than Max Marks
                        //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                        //            //{
                        //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                        //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                        //            //    {
                        //            //    }
                        //            //    else
                        //            //    {
                        //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                        //            //        txt.Focus();
                        //            //        flag = false;
                        //            //        break;
                        //            //    }
                        //            //}
                        //        }
                        //        else
                        //        {
                        //            if (txt.Enabled == true)
                        //            {
                        //                if (lock_status == 1)
                        //                {
                        //                    ShowMessage("Marks Entry Not Completed!!!");
                        //                    txt.Focus();
                        //                    flag = false;
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        //if (gvStudent.Columns[5].Visible == true)
                        //{

                        //    if (j == 4) //TA MARKS
                        //    {
                        //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT2Marks") as Label;      //Max Marks 
                        //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT2Marks") as TextBox;    //Marks Entered 
                        //        maxMarks = lbl.Text.Trim();
                        //        marks = txt.Text.Trim();

                        //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                        //        {
                        //            if (txt.Text == "")
                        //            {
                        //                ShowMessage("Marks Entry Not Completed!!!");
                        //                txt.Focus();
                        //                flag = false;
                        //                break;
                        //            }
                        //            else
                        //            {
                        //                //Check for Marks entered greater than Max Marks
                        //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                        //                {
                        //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                        //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                        //                    {
                        //                    }
                        //                    else
                        //                    {
                        //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                        //                        txt.Focus();
                        //                        flag = false;
                        //                        break;
                        //                    }
                        //                }
                        //            }

                        //            ////Check for Marks entered greater than Max Marks
                        //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                        //            //{
                        //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                        //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                        //            //    {
                        //            //    }
                        //            //    else
                        //            //    {
                        //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                        //            //        txt.Focus();
                        //            //        flag = false;
                        //            //        break;
                        //            //    }
                        //            //}
                        //        }
                        //        else
                        //        {
                        //            if (txt.Enabled == true)
                        //            {
                        //                if (lock_status == 1)
                        //                {
                        //                    ShowMessage("Marks Entry Not Completed!!!");
                        //                    txt.Focus();
                        //                    flag = false;
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //if (gvStudent.Columns[7].Visible == true)
                        //{
                        //    if (j == 6) //TA-Pr MARKS
                        //    {
                        //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblTAPrMarks") as Label;      //Max Marks 
                        //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtTAPrMarks") as TextBox;    //Marks Entered 
                        //        maxMarks = lbl.Text.Trim();
                        //        marks = txt.Text.Trim();

                        //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                        //        {
                        //            if (txt.Text == "")
                        //            {
                        //                ShowMessage("Marks Entry Not Completed!!!");
                        //                txt.Focus();
                        //                flag = false;
                        //                break;
                        //            }
                        //            else
                        //            {
                        //                //Check for Marks entered greater than Max Marks
                        //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                        //                {
                        //                    //Note : 401 for Absent
                        //                    if (Convert.ToInt16(txt.Text) == -1)
                        //                    {
                        //                    }
                        //                    else
                        //                    {
                        //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                        //                        txt.Focus();
                        //                        flag = false;
                        //                        break;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (txt.Enabled == true)
                        //            {
                        //                if (lock_status == 1)
                        //                {
                        //                    ShowMessage("Marks Entry Not Completed!!!");
                        //                    txt.Focus();
                        //                    flag = false;
                        //                    break;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #region comment
                        //}
                        //else
                        //{
                        //    if (txt.Enabled == true)
                        //    {
                        //        //Grade marks
                        //        if (txt.Text.Trim().Equals("A") || txt.Text.Trim().Equals("B") || txt.Text.Trim().Equals("C") || txt.Text.Trim().Equals("D"))
                        //        {
                        //        }
                        //        else
                        //        {
                        //            if (lock_status == 1)
                        //            {
                        //                ShowMessage("Marks Entry Not Completed!!!");
                        //                txt.Focus();
                        //                flag = false;
                        //                break;
                        //            }
                        //            //else
                        //            //{
                        //            //    ShowMessage("Please Enter Marks in Range of A to D!!");
                        //            //    txt.Focus();
                        //            //    flag = false;
                        //            //    break;
                        //            //}
                        //        }
                        //    }
                        //}
                        #endregion

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



        else
        {

            bool flag = true;
            try
            {
                Label lbl;
                TextBox txt;
                string marks = string.Empty;
                string maxMarks = string.Empty;
                string examtype = string.Empty;
                string[] Exam = ddlExam.SelectedValue.Split('-');



                if (Exam[0].StartsWith("S"))
                    examtype = "S";
                else if (Exam[0].StartsWith("E"))
                    examtype = "E";


                if (examtype == "S")
                {
                    for (int j = 4; j < gvStudent.Columns.Count; j++)    //columns
                    {
                        for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                        {
                            if (gvStudent.Columns[j].Visible == true)
                            {
                                if (j == 4) //TA MARKS
                                {
                                    lbl = gvStudent.Rows[i].Cells[j].FindControl("lblintMarks") as Label;      //Max Marks 
                                    txt = gvStudent.Rows[i].Cells[j].FindControl("txtintMarks") as TextBox;    //Marks Entered 
                                    maxMarks = lbl.Text.Trim();
                                    marks = txt.Text.Trim();

                                    if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                                    {
                                        if (txt.Text == "")
                                        {
                                            if (lock_status == 1)
                                            {
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                                    txt.Focus();
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                            else if (Convert.ToDouble(txt.Text) < 0)
                                            {
                                                //Note : 401 for Absent and Not Eligible
                                                if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                                {
                                                }
                                                else
                                                {
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.");
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
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                                //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            #region Not Needed Commented by Manish
                            //if (gvStudent.Columns[4].Visible == true)
                            //{
                            //    if (j == 3) //CT/FE MARKS
                            //    {
                            //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT1Marks") as Label;      //Max Marks 
                            //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT1Marks") as TextBox;    //Marks Entered 
                            //        maxMarks = lbl.Text.Trim();
                            //        marks = txt.Text.Trim();

                            //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            //        {
                            //            if (txt.Text == "")
                            //            {
                            //                ShowMessage("Marks Entry Not Completed!!!");
                            //                txt.Focus();
                            //                flag = false;
                            //                break;
                            //            }
                            //            else
                            //            {
                            //                //Check for Marks entered greater than Max Marks
                            //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                            //                {
                            //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                            //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                            //                    {
                            //                    }
                            //                    else
                            //                    {
                            //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            //                        txt.Focus();
                            //                        flag = false;
                            //                        break;
                            //                    }
                            //                }
                            //            }

                            //            ////Check for Marks entered greater than Max Marks
                            //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                            //            //{
                            //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                            //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                            //            //    {
                            //            //    }
                            //            //    else
                            //            //    {
                            //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            //            //        txt.Focus();
                            //            //        flag = false;
                            //            //        break;
                            //            //    }
                            //            //}
                            //        }
                            //        else
                            //        {
                            //            if (txt.Enabled == true)
                            //            {
                            //                if (lock_status == 1)
                            //                {
                            //                    ShowMessage("Marks Entry Not Completed!!!");
                            //                    txt.Focus();
                            //                    flag = false;
                            //                    break;
                            //                }
                            //            }
                            //        }
                            //    }
                            //}

                            //if (gvStudent.Columns[5].Visible == true)
                            //{

                            //    if (j == 4) //TA MARKS
                            //    {
                            //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblT2Marks") as Label;      //Max Marks 
                            //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtT2Marks") as TextBox;    //Marks Entered 
                            //        maxMarks = lbl.Text.Trim();
                            //        marks = txt.Text.Trim();

                            //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            //        {
                            //            if (txt.Text == "")
                            //            {
                            //                ShowMessage("Marks Entry Not Completed!!!");
                            //                txt.Focus();
                            //                flag = false;
                            //                break;
                            //            }
                            //            else
                            //            {
                            //                //Check for Marks entered greater than Max Marks
                            //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                            //                {
                            //                    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                            //                    if (Convert.ToInt16(txt.Text) == -1 || Convert.ToInt16(txt.Text) == -2 || Convert.ToInt16(txt.Text) == -3)
                            //                    {
                            //                    }
                            //                    else
                            //                    {
                            //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            //                        txt.Focus();
                            //                        flag = false;
                            //                        break;
                            //                    }
                            //                }
                            //            }

                            //            ////Check for Marks entered greater than Max Marks
                            //            //if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                            //            //{
                            //            //    //Note : 401 for Absent; 402 for Debar; 403 for Copy Case 
                            //            //    if (Convert.ToInt16(txt.Text) == 401 || Convert.ToInt16(txt.Text) == 402 || Convert.ToInt16(txt.Text) == 403)
                            //            //    {
                            //            //    }
                            //            //    else
                            //            //    {
                            //            //        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            //            //        txt.Focus();
                            //            //        flag = false;
                            //            //        break;
                            //            //    }
                            //            //}
                            //        }
                            //        else
                            //        {
                            //            if (txt.Enabled == true)
                            //            {
                            //                if (lock_status == 1)
                            //                {
                            //                    ShowMessage("Marks Entry Not Completed!!!");
                            //                    txt.Focus();
                            //                    flag = false;
                            //                    break;
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            //if (gvStudent.Columns[7].Visible == true)
                            //{
                            //    if (j == 6) //TA-Pr MARKS
                            //    {
                            //        lbl = gvStudent.Rows[i].Cells[j].FindControl("lblTAPrMarks") as Label;      //Max Marks 
                            //        txt = gvStudent.Rows[i].Cells[j].FindControl("txtTAPrMarks") as TextBox;    //Marks Entered 
                            //        maxMarks = lbl.Text.Trim();
                            //        marks = txt.Text.Trim();

                            //        if (!txt.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txt.Enabled == true)
                            //        {
                            //            if (txt.Text == "")
                            //            {
                            //                ShowMessage("Marks Entry Not Completed!!!");
                            //                txt.Focus();
                            //                flag = false;
                            //                break;
                            //            }
                            //            else
                            //            {
                            //                //Check for Marks entered greater than Max Marks
                            //                if (Convert.ToInt16(txt.Text) > Convert.ToInt16(lbl.Text))
                            //                {
                            //                    //Note : 401 for Absent
                            //                    if (Convert.ToInt16(txt.Text) == -1)
                            //                    {
                            //                    }
                            //                    else
                            //                    {
                            //                        ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            //                        txt.Focus();
                            //                        flag = false;
                            //                        break;
                            //                    }
                            //                }
                            //            }
                            //        }
                            //        else
                            //        {
                            //            if (txt.Enabled == true)
                            //            {
                            //                if (lock_status == 1)
                            //                {
                            //                    ShowMessage("Marks Entry Not Completed!!!");
                            //                    txt.Focus();
                            //                    flag = false;
                            //                    break;
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            #endregion

                            #region comment
                            //}
                            //else
                            //{
                            //    if (txt.Enabled == true)
                            //    {
                            //        //Grade marks
                            //        if (txt.Text.Trim().Equals("A") || txt.Text.Trim().Equals("B") || txt.Text.Trim().Equals("C") || txt.Text.Trim().Equals("D"))
                            //        {
                            //        }
                            //        else
                            //        {
                            //            if (lock_status == 1)
                            //            {
                            //                ShowMessage("Marks Entry Not Completed!!!");
                            //                txt.Focus();
                            //                flag = false;
                            //                break;
                            //            }
                            //            //else
                            //            //{
                            //            //    ShowMessage("Please Enter Marks in Range of A to D!!");
                            //            //    txt.Focus();
                            //            //    flag = false;
                            //            //    break;
                            //            //}
                            //        }
                            //    }
                            //}
                            #endregion

                            if (flag == false) break;
                        }
                    }
                }
                else
                {
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
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                                    txt.Focus();
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                            else if (Convert.ToDouble(txt.Text) < 0)
                                            {
                                                //Note : 401 for Absent and Not Eligible
                                                if (Convert.ToDouble(txt.Text) != 902 && Convert.ToDouble(txt.Text) != 903 && Convert.ToDouble(txt.Text) != 904 && Convert.ToDouble(txt.Text) != 905 && Convert.ToDouble(txt.Text) != 906)
                                                {
                                                }
                                                else
                                                {
                                                    objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.", this.Page);
                                                    //ShowMessage("Marks Entered [" + txt.Text + "] cant be Less 0 (zero). Only -1, -2, -3 and -4 are allowed.");
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
                                                objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
                                                //ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                                txt.Focus();
                                                flag = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            #region Not Needed Commented by Manish

                            #endregion

                            #region comment
                            //}

                            #endregion

                            if (flag == false) break;
                        }
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
    }
    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        SaveAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        //Clear();
        ////ddlDegree.SelectedIndex = 0;
        ////ddlBranch.SelectedIndex = 0;
        ////ddlSemester.SelectedIndex = 0;
        //ddlSubjectType.SelectedIndex = 0;
        //ddlCourse.SelectedIndex = 0;
        //ddlExam.SelectedIndex = 0;
    }

    private void Clear()
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
        //btnReport.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                if (ddlCourse.SelectedIndex > 0)
                {
                    int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                    if (ddlExam.SelectedIndex > 0)
                    {
                        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW_ADMIN.rpt");//rptMarksList1.rpt
                    }
                    else
                    {
                        if (ddlSubjectType.SelectedValue == "10")
                        {
                            this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW_ADMIN.rpt");//rptMarksList1.rpt
                        }
                        else
                        {
                            if (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1)
                            {
                                this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_NEW_ADMIN.rpt");//rptMarksList1.rpt
                            }
                            else
                            {
                                objCommon.DisplayMessage(updpnl, "Please Select Exam!", this.Page);
                                ddlExam.Focus();
                            }
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Please Select Course Name!", this.Page);
                    ddlCourse.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please Select Subject Type!", this.Page);
                ddlSubjectType.Focus();
            }
        }
        else
        {
            objCommon.DisplayMessage(updpnl, "Please Select Session!", this.Page);
            ddlSession.Focus();
        }
    }

    private void ShowReportMarksEntry(string reportTitle, string rptFileName)
    {
        string Exam1 = string.Empty;
        string[] Exam = null;
        string Subexam = string.Empty;
        int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
        string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
        {
            Exam1 = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
            Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
        }
        else
        {
            Exam = ddlExam.SelectedValue.Split('-');
            if ((ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 0) || ddlSubjectType.SelectedValue == "11")
            {
                Subexam = ddlSubExamName.SelectedValue;
            }
            else
            {
                Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);
            }
        }
        string Username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();

        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam1 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + ",@p_username=" + Username + "";
        }
        else
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam[1] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + ",@p_username=" + Username + "";
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND ISNULL(PREV_STATUS,0)=1 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");


    }



    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {

        string examtype = string.Empty;
        string[] Examname = ddlExam.SelectedValue.Split('-');



        if (Examname[0].StartsWith("S"))
            examtype = "S";
        else if (Examname[0].StartsWith("E"))
            examtype = "E";

        divSubExamName.Visible = false;
        pnlStudGrid.Visible = false;

        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {

            divSubExamName.Visible = false;
            pnlStudGrid.Visible = false;

            if (ddlExam.SelectedIndex > 0)
            {
                string[] Exam = ddlExam.SelectedValue.Split('-');

                if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                {
                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + "", "");



                    divSubExamName.Visible = true;
                }

                else
                {
                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + "", "");

                    //objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+" AND COURSENO="+Convert.ToInt32(ddlCourse.SelectedValue)+" AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + "", "");

                    ddlSubExamName.SelectedIndex = 1;

                    if (Exam[1].ToUpper() == "S1" || Exam[1].ToUpper().ToUpper() == "S3" || Exam[1].ToUpper() == "S2")
                    {
                        //DataSet dsSubExam = objMarksEntry.GetLevelMarksEntryExamDetail(Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["Pattern"]), Convert.ToInt32(Exam[0]), 2); //2 for Sub Exam type

                        DataSet dsSubExam = objCommon.FillDropDown("ACD_EXAM_NAME", " CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND EXAMTYPE=2 AND FLDNAME IN('EXTERMARK')", "EXAMNO");
                        MainSubExamBind(ddlSubExamName, dsSubExam);
                        divSubExamName.Visible = true;
                    }
                }
            }

            Clear();
        }


        else
        {

            if (examtype == "S")
            {
                if (ddlExam.SelectedIndex > 0)
                {

                    //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToInt32(ddlExam.SelectedValue.Split('-')[1]) + " )", "SESSIONNO DESC");

                    //if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                    //{
                    //    objCommon.DisplayMessage(this.updpnl, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    //    return;
                    //}


                    string[] Exam = ddlExam.SelectedValue.Split('-');

                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                    {
                        objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");

                        //objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + "", "");
                        divSubExamName.Visible = true;
                    }

                    else
                    {
                        //objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");


                        objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");

                        //  ddlSubExamName.SelectedIndex = 1;

                        if (Exam[0].ToUpper() == "S2" || Exam[0].ToUpper().ToUpper() == "S3" || Exam[0].ToUpper() == "S1" || Exam[0].ToUpper() == "S6")
                        {

                            DataSet dsSubExam = objCommon.FillDropDown("ACD_EXAM_NAME", " CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>''AND FLDNAME NOT IN ('EXTERMARK')", "EXAMNO");
                            //MainSubExamBind(ddlSubExamName, dsSubExam);
                            divSubExamName.Visible = true;
                        }
                    }
                }


                Clear();
            }
            else
            {

                divSubExamName.Visible = false;
                pnlStudGrid.Visible = false;

                if (ddlExam.SelectedIndex > 0)
                {
                    string[] Exam = ddlExam.SelectedValue.Split('-');

                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                    {
                        objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + "", "");
                        divSubExamName.Visible = true;
                    }

                    else
                    {
                        objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                        ddlSubExamName.SelectedIndex = 1;

                        if (Exam[1].ToUpper() == "S1" || Exam[1].ToUpper().ToUpper() == "S3" || Exam[1].ToUpper() == "S2")
                        {


                            DataSet dsSubExam = objCommon.FillDropDown("ACD_EXAM_NAME", " CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND EXAMTYPE=2 AND FLDNAME IN('EXTERMARK')", "EXAMNO");
                            MainSubExamBind(ddlSubExamName, dsSubExam);
                            divSubExamName.Visible = true;
                        }
                    }
                }

                Clear();

            }
        }


    }

    protected void ddlSubExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlStudGrid.Visible = false;
        Clear();
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

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSubjectType.SelectedIndex > 0)
        {
                lnkExcekImport.Visible = true;
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND ISNULL(PREV_STATUS,0)=1 AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                ddlCourse.Focus();
                ddlSubExamName.SelectedIndex = 0;
                divSubExamName.Visible = false;
                DIVEXAM.Visible = true;


                if (ddlSubjectType.SelectedValue == "10")
                {
                    DIVEXAM.Visible = false;
                }
                else
                {
                    DIVEXAM.Visible = true;
                }
           
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));

            ddlExam.Items.Clear();
            ddlExam.Items.Add(new ListItem("Please Select", "0"));

            ddlSubExamName.Items.Clear();
            ddlSubExamName.Items.Add(new ListItem("Please Select", "0"));
            divSubExamName.Visible = false;


        }
        Clear();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        ShowStudents();

    }

    private void ShowStudentsSpecialSubject()
    {
        try
        {

            string[] course = ddlCourse.SelectedItem.Text.Split('-');
            string Exam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));

            string SubExam = string.Empty;
            string SubExamName = string.Empty;

            if (divSubExamName.Visible == true)
            {
                SubExam = ddlSubExamName.SelectedValue;
                SubExamName = ddlSubExamName.SelectedItem.Text;
            }

            DataSet dsStudent = null;
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

            dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam, Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]));
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM                 
                    if (divSubExamName.Visible == false)
                    {
                        hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                        hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();


                        gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                    }
                    else
                    {

                        gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                    }

                    ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                    ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                    ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];

                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();


                    btnSave.Enabled = true;
                    btnLock.Enabled = true;
                    btnSave.Visible = true;
                    btnLock.Visible = true;
                    btnUnlock.Enabled = false;
                    btnGrade.Visible = false;

                    if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True" && dsStudent.Tables[0].Rows[0]["MARKTOT"].ToString() != "")
                    {
                        gvStudent.Columns[5].Visible = true;
                        gvStudent.Columns[3].Visible = true;
                        gvStudent.Columns[4].Visible = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnUnlock.Enabled = true;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnGrade.Enabled = true;
                        btnGrade.Visible = true;
                        // btnMarksModifyReport.Visible = true;
                    }
                    else
                    {
                        gvStudent.Columns[4].Visible = false;
                        gvStudent.Columns[3].Visible = true;
                        gvStudent.Columns[5].Visible = false;
                        //gvStudent.Columns[6].Visible = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnUnlock.Enabled = true;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnGrade.Enabled = true;
                        btnGrade.Visible = true;
                        //btnMarksModifyReport.Visible = true;
                    }

                    if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty)
                    {
                        gvStudent.Columns[6].Visible = true;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnUnlock.Enabled = false;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnGrade.Enabled = false;
                        btnGrade.Visible = false;
                        //btnMarksModifyReport.Visible = true;
                        //btnfinalmarkentry.Visible = true;
                        //btnmarkexcel.Visible = true;

                    }
                    else
                    {
                        gvStudent.Columns[6].Visible = false;
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnUnlock.Enabled = true;
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnGrade.Enabled = true;
                        btnGrade.Visible = true;
                        //btnMarksModifyReport.Visible = true;
                        //btnfinalmarkentry.Visible = false;
                        //btnmarkexcel.Visible = false;
                    }

                    pnlStudGrid.Visible = true;
                    //btnReport.Visible = true;
                    //btnReport.Enabled = true;
                    lblStudents.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
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

    private void ShowStudents()
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            try
            {
                string[] course = ddlCourse.SelectedItem.Text.Split('-');
                string[] Exam = ddlExam.SelectedValue.Split('-');
                string SubExam = string.Empty;
                string SubExamName = string.Empty;
                DataSet ds = null;
                Boolean LOCK1 = false;
                Boolean LOCK2 = false;
                int Subexamno = 0;
                if (divSubExamName.Visible == true)
                {
                    SubExam = ddlSubExamName.SelectedValue;
                    SubExamName = ddlSubExamName.SelectedItem.Text;
                }
                else
                {
                    if (ddlSubjectType.SelectedValue == "4")
                    {
                        SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND ISNULL(ACTIVESTATUS,0)=1");
                    }
                    else
                    {
                        SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0]);
                    }

                }
                if (ddlSubjectType.SelectedValue == "4")
                {
                    Subexamno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0] + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND ISNULL(ACTIVESTATUS,0)=1"));
                }
                else
                {
                    Subexamno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[0]));
                }

                if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                {

                    ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");
                }
                else
                {
                    ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Subexamno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
                    {
                        objCommon.DisplayMessage(this, "STOP !!! Rule 1 for End Sem Exam is not Defined", this.Page);
                        return;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
                    {
                        objCommon.DisplayMessage(this, "STOP !!! Rule 2 for End Sem Exam is not Defined", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "STOP !!! Exam Rule is not Defined", this.Page);
                    return;
                }

                DataSet dsStudent = null;
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);


                if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                {
                    dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[1], Convert.ToInt32(ViewState["schemeno"]), (ddlSubExamName.SelectedValue).Split('-')[1], SubExamName, Convert.ToInt32(ViewState["college_id"]));
                }
                else
                {
                    if (ddlSubjectType.SelectedValue == "4")
                    {
                        dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[1], Convert.ToInt32(ViewState["schemeno"]), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]));
                    }
                    else
                    {
                        dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[1], Convert.ToInt32(ViewState["schemeno"]), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]));
                    }
                }
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                        if (Convert.ToString(ddlExam.SelectedValue).Split('-')[1] == "EXTERMARK")
                        {
                            gvStudent.Columns[2].Visible = true;
                        }
                        else
                        {
                            gvStudent.Columns[2].Visible = true;
                        }
                        if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
                        {
                            if (divSubExamName.Visible == false)
                            {
                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();


                                gvStudent.Columns[5].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                            }
                            else
                            {
                                hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                                gvStudent.Columns[5].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                            }

                            gvStudent.Columns[4].Visible = false;
                            gvStudent.Columns[5].Visible = true;
                            gvStudent.Columns[6].Visible = false;
                            ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                            ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                            ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
                        }
                        else
                        {
                            gvStudent.Columns[5].Visible = false;
                        }
                        lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                        //Bind the Student List
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();
                        // added for absent student by prafull on dated 23072022
                        int z = 0;

                        foreach (GridViewRow rw in gvStudent.Rows)
                        {
                            TextBox txtmark = (TextBox)rw.FindControl("txtmarks");
                            string regno = (dsStudent.Tables[0].Rows[z]["REGNO"]).ToString();

                            if ((dsStudent.Tables[0].Rows[z]["SMARK"]) is DBNull)
                            {
                                txtmark.Enabled = true;
                            }
                            else if ((Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 902.00) || (Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 903.00))
                            {
                                txtmark.Enabled = false;
                            }
                            else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == true)
                            {
                                txtmark.Enabled = false;
                            }
                            else
                            {
                                txtmark.Enabled = true;
                            }


                            z++;
                        }
                        // end 
                        #region Comment Code



                        #endregion Comment Code

                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnSave.Visible = true;
                        btnLock.Visible = true;
                        btnUnlock.Enabled = false;
                        btnGrade.Visible = false;


                        int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
                        if (SESSION_TYPE == 1)
                        {
                            if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                            {
                                if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                                {
                                    LOCK1 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(INTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                                    LOCK2 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(EXTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));



                                    if (LOCK1 == LOCK2)
                                    {
                                        gvStudent.Columns[6].Visible = true;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        btnGrade.Enabled = true;
                                        btnGrade.Visible = true;
                                        //btnMarksModifyReport.Visible = true;
                                    }
                                    //else
                                    //{
                                    //    btnSave.Enabled = true;
                                    //    btnLock.Enabled = true;
                                    //    btnUnlock.Enabled = true;
                                    //    btnSave.Visible = true;
                                    //    btnLock.Visible = true;
                                    //    btnGrade.Enabled = false;
                                    //    btnGrade.Visible = false;
                                    //    btnMarksModifyReport.Visible = true;
                                    //}

                                    else if (LOCK1 == true)
                                    {
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                    }
                                    else if (LOCK2 == true)
                                    {
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnGrade.Enabled = false;
                                        btnGrade.Visible = false;
                                        //btnMarksModifyReport.Visible = true;
                                    }

                                }
                                else
                                {
                                    gvStudent.Columns[6].Visible = true;
                                    btnSave.Enabled = false;
                                    btnLock.Enabled = true;
                                    btnUnlock.Enabled = true;
                                    btnSave.Visible = false;
                                    btnLock.Visible = false;
                                    btnGrade.Enabled = true;
                                    btnGrade.Visible = true;
                                    //btnMarksModifyReport.Visible = true;
                                }

                            }
                        }

                        else if (SESSION_TYPE == 2)
                        {
                            if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                            {
                                if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                                {
                                    LOCK1 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(INTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                                    LOCK2 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(EXTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));



                                    if (LOCK1 == LOCK2)
                                    {
                                        gvStudent.Columns[6].Visible = true;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        btnGrade.Enabled = true;
                                        btnGrade.Visible = true;
                                        //btnMarksModifyReport.Visible = true;
                                    }

                                    else if (LOCK1 == true)
                                    {
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                    }
                                    else if (LOCK2 == true)
                                    {
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                    }
                                    else
                                    {
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnGrade.Enabled = false;
                                        btnGrade.Visible = false;
                                        //btnMarksModifyReport.Visible = true;
                                    }

                                }
                                else
                                {
                                    gvStudent.Columns[6].Visible = true;
                                    //btnSave.Enabled = false;
                                    btnSave.Enabled = true;
                                    btnLock.Enabled = true;
                                    btnUnlock.Enabled = true;
                                    //btnSave.Visible = false;
                                    btnSave.Visible = true;
                                    btnLock.Visible = true;
                                    btnGrade.Enabled = true;
                                    btnGrade.Visible = true;
                                    //btnMarksModifyReport.Visible = true;
                                }

                            }

                        }



                        if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty)
                        {
                            gvStudent.Columns[7].Visible = true;
                            //gvStudent.Columns[5].Visible = true;
                            btnSave.Enabled = false;
                            btnLock.Enabled = false;
                            btnUnlock.Enabled = false;
                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            btnGrade.Enabled = false;
                            btnGrade.Visible = false;
                            //btnMarksModifyReport.Visible = true;
                            //btnfinalmarkentry.Visible = true;
                            //btnmarkexcel.Visible = true;

                        }
                        else
                        {
                            gvStudent.Columns[7].Visible = false;
                            //btnfinalmarkentry.Visible = false;
                            //btnmarkexcel.Visible = false;
                        }

                        pnlStudGrid.Visible = true;
                        //btnReport.Visible = true;
                        //btnReport.Enabled = true;
                        lblStudents.Visible = true;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
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
        else
        {
            try
            {
                string[] course = ddlCourse.SelectedItem.Text.Split('-');
                string[] Exam = ddlExam.SelectedValue.Split('-');
                string SubExam = string.Empty;
                string SubExamName = string.Empty;
                DataSet ds = null;
                Boolean LOCK1 = false;
                Boolean LOCK2 = false;
                int Subexamno = 0;
                string examtype = string.Empty;


                if (Exam[0].StartsWith("S"))
                    examtype = "S";
                else if (Exam[0].StartsWith("E"))
                    examtype = "E";


                if (divSubExamName.Visible == true)
                {
                    SubExam = ddlSubExamName.SelectedValue;
                    SubExamName = ddlSubExamName.SelectedItem.Text;
                }
                else
                {

                    SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1]);

                }

                Subexamno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1]));



                ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");


                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) < 0)
                    {
                        objCommon.DisplayMessage(this, "STOP !!! Rule 1 for End Sem Exam is not Defined", this.Page);
                        return;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) < 0)
                    {
                        objCommon.DisplayMessage(this, "STOP !!! Rule 2 for End Sem Exam is not Defined", this.Page);
                        return;
                    }
                }
                else
                {

                    objCommon.DisplayMessage(this, "STOP !!! Exam Rule is not Defined", this.Page);
                    return;
                }

                DataSet dsStudent = null;
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);



                if (examtype == "S")
                {
                    dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]));
                }
                else
                {
                    dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]));
                }

                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                        if (Convert.ToString(ddlExam.SelectedValue).Split('-')[0] == "EXTERMARK")
                        {
                            gvStudent.Columns[2].Visible = true;
                        }
                        else
                        {
                            gvStudent.Columns[2].Visible = true;
                        }
                        if (examtype == "S")
                        {
                            if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
                            {

                                if (divSubExamName.Visible == false)
                                {
                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();


                                    gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                }
                                else
                                {
                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();
                                    gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                }


                                gvStudent.Columns[4].Visible = true;
                                gvStudent.Columns[5].Visible = false;
                                gvStudent.Columns[6].Visible = false;
                                gvStudent.Columns[3].Visible = false;
                                ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                                ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                                ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];


                            }
                            else
                            {
                                gvStudent.Columns[4].Visible = false;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
                            {

                                if (divSubExamName.Visible == false)
                                {
                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();

                                    //gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + "  " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]" + " - " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                    gvStudent.Columns[5].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                }
                                else
                                {
                                    hfdMaxMark.Value = dsStudent.Tables[0].Rows[0]["SMAX"].ToString();
                                    hfdMinMark.Value = dsStudent.Tables[0].Rows[0]["SMIN"].ToString();
                                    //gvStudent.Columns[4].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]" + " - " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                    gvStudent.Columns[5].HeaderText = ddlSubExamName.SelectedItem.Text + "  " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]";
                                }


                                gvStudent.Columns[5].Visible = true;
                                gvStudent.Columns[3].Visible = true;
                                gvStudent.Columns[4].Visible = false;
                                ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                                ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                                ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];


                            }
                            else
                            {
                                gvStudent.Columns[5].Visible = false;
                            }

                        }
                        lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                        //Bind the Student List
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();
                        // added for absent student by prafull on dated 23072022
                        int z = 0;

                        foreach (GridViewRow rw in gvStudent.Rows)
                        {
                            TextBox txtmark = (TextBox)rw.FindControl("txtmarks");
                            string regno = (dsStudent.Tables[0].Rows[z]["REGNO"]).ToString();

                            if ((dsStudent.Tables[0].Rows[z]["SMARK"]) is DBNull)
                            {
                                txtmark.Enabled = true;
                            }
                            else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == true)
                            {
                                txtmark.Enabled = false;
                            }
                            else
                            {
                                txtmark.Enabled = true;
                            }


                            z++;
                        }
                        // end 
                        #region Comment Code



                        #endregion Comment Code

                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnSave.Visible = true;
                        btnLock.Visible = true;
                        lnkExcekImport.Visible = true;
                        btnUnlock.Enabled = false;
                        btnGrade.Visible = false;

                        if (examtype == "S")
                        {
                            int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
                            if (SESSION_TYPE == 1)
                            {
                                if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
                                {

                                    btnSave.Enabled = false;
                                    btnLock.Enabled = false;
                                    btnUnlock.Enabled = false;
                                    btnSave.Visible = false;
                                    btnLock.Visible = false;
                                    btnGrade.Enabled = false;
                                    btnGrade.Visible = false;
                                    lnkExcekImport.Enabled = false;
                                    // btninterreport.Enabled = true;
                                    // btninterreport.Visible = true;

                                    //lnkExcekImport.Visible = false;
                                    //btnMarksModifyReport.Visible = false;


                                }
                                else
                                {
                                    gvStudent.Columns[5].Visible = false;
                                    gvStudent.Columns[6].Visible = false;

                                    btnSave.Enabled = true;
                                    btnLock.Enabled = true;
                                    btnUnlock.Enabled = true;
                                    btnSave.Visible = true;
                                    btnLock.Visible = true;
                                    lnkExcekImport.Enabled = true;
                                    //   btninterreport.Enabled = true;
                                    //   btninterreport.Visible = true;
                                    //lnkExcekImport.Visible = true;
                                    //btnGrade.Enabled = true;
                                    // btnGrade.Visible = true;
                                    //btnMarksModifyReport.Visible = true;
                                }

                            }
                            else if (SESSION_TYPE == 2)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {
                                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                                    {
                                        LOCK1 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(INTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                                        LOCK2 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(EXTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));



                                        if (LOCK1 == LOCK2)
                                        {
                                            gvStudent.Columns[5].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            //btnMarksModifyReport.Visible = true;
                                        }

                                        else if (LOCK1 == true)
                                        {
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else if (LOCK2 == true)
                                        {
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else
                                        {
                                            btnSave.Enabled = true;
                                            btnLock.Enabled = true;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = true;
                                            btnLock.Visible = true;
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            //btnMarksModifyReport.Visible = true;
                                        }

                                    }
                                    else
                                    {
                                        gvStudent.Columns[5].Visible = true;
                                        //btnSave.Enabled = false;
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;
                                        btnUnlock.Enabled = true;
                                        //btnSave.Visible = false;
                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnGrade.Enabled = true;
                                        btnGrade.Visible = true;
                                        //btnMarksModifyReport.Visible = true;
                                    }

                                }

                            }

                            pnlStudGrid.Visible = true;
                            //btnReport.Visible = true;
                            //btnReport.Enabled = true;
                            lblStudents.Visible = true;
                            //btninterreport.Enabled = true;
                            // btninterreport.Visible = true;
                            gvStudent.Columns[7].Visible = false;
                        }
                        else
                        {
                            int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
                            if (SESSION_TYPE == 1)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {
                                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                                    {
                                        LOCK1 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(INTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                                        LOCK2 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(EXTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));



                                        if (LOCK1 == LOCK2)
                                        {
                                            gvStudent.Columns[6].Visible = true;
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            //btnMarksModifyReport.Visible = true;
                                        }


                                        else if (LOCK1 == true)
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else if (LOCK2 == true)
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = true;
                                            btnLock.Enabled = true;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = true;
                                            btnLock.Visible = true;
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            //btnMarksModifyReport.Visible = true;
                                        }

                                    }
                                    else
                                    {
                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = true;
                                        btnUnlock.Enabled = true;
                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

                                        if (studentcount > 30)
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                        }
                                        //btnMarksModifyReport.Visible = true;
                                    }

                                }
                                else
                                {
                                    gvStudent.Columns[3].Visible = true;
                                    gvStudent.Columns[6].Visible = false;
                                    lnkExcekImport.Visible = true;


                                }
                            }

                            else if (SESSION_TYPE == 2)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {
                                    if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                                    {
                                        LOCK1 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(INTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                                        LOCK2 = Convert.ToBoolean(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "DISTINCT ISNULL(EXTER_LOCK,0)", "SUB_ID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));



                                        if (LOCK1 == LOCK2)
                                        {
                                            gvStudent.Columns[6].Visible = true;
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            //btnMarksModifyReport.Visible = true;
                                        }

                                        else if (LOCK1 == true)
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else if (LOCK2 == true)
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = false;
                                            btnLock.Enabled = false;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = false;
                                            btnLock.Visible = false;
                                        }
                                        else
                                        {
                                            gvStudent.Columns[3].Visible = true;
                                            btnSave.Enabled = true;
                                            btnLock.Enabled = true;
                                            btnUnlock.Enabled = true;
                                            btnSave.Visible = true;
                                            btnLock.Visible = true;
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            //btnMarksModifyReport.Visible = true;
                                        }

                                    }
                                    else
                                    {
                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //btnSave.Enabled = false;
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;
                                        btnUnlock.Enabled = true;
                                        //btnSave.Visible = false;
                                        btnSave.Visible = true;
                                        btnLock.Visible = true;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

                                        if (studentcount > 30)
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                        }

                                        //btnMarksModifyReport.Visible = true;
                                    }

                                }

                            }

                            pnlStudGrid.Visible = true;
                            //btnReport.Visible = true;
                            //btnReport.Enabled = true;
                            lblStudents.Visible = true;



                            if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty)
                            {
                                gvStudent.Columns[7].Visible = true;
                                //gvStudent.Columns[5].Visible = true;
                                btnSave.Enabled = false;
                                btnLock.Enabled = false;
                                btnUnlock.Enabled = false;
                                btnSave.Visible = false;
                                btnLock.Visible = false;
                                btnGrade.Enabled = false;
                                btnGrade.Visible = false;
                                lnkExcekImport.Visible = false;


                            }
                            else
                            {
                                gvStudent.Columns[7].Visible = false;


                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Students Not Found..!!", this.Page);
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
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        SaveAndLock(2);
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcollege.SelectedIndex > 0)
            {

                //DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", "@P_COLSCHEMENO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "");
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                    if (ddlcollege.SelectedIndex > 0)
                    {
                        int count = 0;
                        count = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COUNT(SESSIONNO)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
                        if (1 > 0)
                        {
                            ddldegree.Items.Clear();
                            objCommon.FillDropDownList(ddldegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "A.DEGREENAME");
                            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                            //ddlDegree.Focus();
                            ddlSession.Focus();
                        }
                        else
                        {
                            ddlSession.Focus();
                            objCommon.DisplayMessage(this.updpnl, "Session Activity not Created Or activity may not be Started!!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        ddldegree.Items.Clear();
                        ddldegree.Items.Add(new ListItem("Please Select", "0"));
                        objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "EndSemExamMarkEntry.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    //added mahesh malve on dated 26/06/2021
    protected void btnMarksModifyReport_Click(object sender, EventArgs e)
    {
        //int count = Convert.ToInt32(objCommon.LookUp("[dbo].[ACD_ADMINMARKSENTRYTRACK]", "Count(IDNO)", "Sessionno=" + ddlSession.SelectedValue + ""));
        //if (count > 0)
        //{
        this.ShowAdminMarksModifyReport("AdminMarksModifyReport", "AdminMarksEntryReport.rpt");
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updpnl, "Record not found for selected Session.", this.Page);
        //}
    }

    private void ShowAdminMarksModifyReport(string reportTitle, string rptFileName)
    {
        //string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        //string[] Exam = ddlExam.SelectedValue.Split('-');
        //string Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);
        //string Username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //url += "Reports/CommonReport.aspx?";
        //url += "pagetitle=" + reportTitle;
        //url += "&path=~,Reports,Academic," + rptFileName;
        ////url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam[1] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + ",@p_username=" + Username + "";



        string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        string[] Exam = ddlExam.SelectedValue.Split('-');
        string Exam1 = string.Empty;
        string Subexam = string.Empty;
        string Username = string.Empty;
        Username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
        int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));

        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
        {
            Exam1 = "EXTERMARK";
            //objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
            Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
        }
        else
        {
            Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);

        }
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;

        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam1 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + ",@p_username=" + Username + "";
        }
        else
        {
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam[1] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + ",@p_username=" + Username + "";
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnGrade_Click(object sender, EventArgs e)
    {
        try
        {
            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                string studids = string.Empty;

                MarksEntryController objMarksEntry = new MarksEntryController();
                Label lbl;
                CheckBox chk;
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;

                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";

                }
                if (studids == string.Empty)
                {
                    objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                    return;
                }

                //string SP_Name = "PKG_ACAD_GRADE_ALLOTMENT_NEW";
                //string SP_Parameters = "@P_SESSIONNO,  @P_COURSENO, @P_STUDIDS,@P_TH_PR,@P_UA_NO,@P_IPADDRESS,@P_SEMESTERNO, @P_SCHEMENO, @P_OP";
                //string Call_Values = "" + ddlSession.SelectedValue + "," + ddlCourse.SelectedValue + ",'" + studids + "'," + ddlSubjectType.SelectedValue + "," + Convert.ToInt32(Session["userno"].ToString()) + ",'" + ViewState["ipAddress"].ToString() + "'," + ddlsemester.SelectedValue + "," + ddlscheme.SelectedValue + ",1";
                //string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, true);
                //if (que_out != "0")
                //{
                //}

                CustomStatus cs = (CustomStatus)objMarksEntry.GradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
                    //btnReport.Enabled = true;
                    ShowStudents();
                }
            }
            else
            {
                int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));

                if (ddlSubjectType.SelectedValue == "10" || Is_Specialcase == 1)
                {
                    string examtype = string.Empty;
                    string Subexam = string.Empty;
                    int lock_status = 1;
                    string studids = string.Empty;

                    MarksEntryController objMarksEntry = new MarksEntryController();
                    Label lbl;
                    CheckBox chk;


                    string marks = string.Empty;
                    TextBox txtMarks;

                    for (int i = 0; i < gvStudent.Rows.Count; i++)
                    {
                        chk = gvStudent.Rows[i].FindControl("chkMarks") as CheckBox;
                        if (lock_status == 0)
                        {
                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";

                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                        }
                        else if (lock_status == 1 || lock_status == 2)
                        {
                            //Gather Student IDs 
                            lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                            studids += lbl.ToolTip + ",";

                            //Gather Exam Marks 
                            txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                            marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                        }
                    }


                    string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

                    string Exam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));


                    string examname = string.Empty;


                    string SubExamName = string.Empty;
                    string SubExamComponentName = string.Empty;

                    if (divSubExamName.Visible == true)
                    {
                        SubExamName = ddlSubExamName.SelectedValue;
                        SubExamComponentName = ddlSubExamName.SelectedItem.Text;
                    }

                    CustomStatus cs1 = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[0]), SubExamComponentName);
                    if (cs1.Equals(CustomStatus.RecordSaved))
                    {
                        if (lock_status == 1)
                        {
                            // objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                        }
                        else if (lock_status == 2)
                        {
                            objCommon.DisplayMessage(updpnl, "Marks Unlocked Successfully!!!", this.Page);
                        }
                        else
                            objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);


                        CustomStatus cs = (CustomStatus)objMarksEntry.GradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
                            //btnReport.Enabled = true;
                            ShowStudentsSpecialSubject();
                        }
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Please Select Exam Name.", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnfinalmarkentry_Click(object sender, EventArgs e)
    {
        if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
        {
            this.ShowFinalTH_PR_MarkEntryReport("Final Mark Entry Report for Practical", "FinalMarksEntryReport_PR.rpt");
        }
        else
        {
            this.ShowFinalTH_PR_MarkEntryReport("Final Mark Entry Report for Theory", "FinalMarksEntryReport_TH.rpt");
        }
    }

    private void ShowFinalTH_PR_MarkEntryReport(string reportTitle, string rptFileName)
    {

        string[] Exam = ddlExam.SelectedValue.Split('-');

        string Subexam = string.Empty;
        string Exam1 = string.Empty;
        string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
        {
            Exam1 = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
            Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
        }
        else
        {
            Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);
        }
        string Username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1) || (ddlSubjectType.SelectedValue == "11" && Is_Specialcase == 1))
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam1 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + "";
        }
        else
        {
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam[1] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + "";
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");
        ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnmarkexcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
            {
                this.ShowReport("xls", "FinalMarksEntryReport_PR.rpt");
            }
            else
            {
                this.ShowReport("xls", "FinalMarksEntryReport_TH.rpt");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EndSemExamMarkEntry.aspx.btnmarkexcel_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowReport(string exporttype, string rptFileName)
    {
        try
        {
            string[] Exam = ddlExam.SelectedValue.Split('-');
            string Subexam = string.Empty;
            string Exam1 = string.Empty;
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
            int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
            {
                Exam1 = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) SUBSTRING(FLDNAME,1,2) FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
                Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", "TOP(1) CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
            }
            else
            {
                Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[0]);
            }
            string Username = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + ddlSubjectType.SelectedItem.Text + "_MarkEntryReport" + ".xls";
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
            if (ddlSubjectType.SelectedValue == "10" || (ddlSubjectType.SelectedValue == "2" && Is_Specialcase == 1))
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam1 + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + "";
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + Exam[1] + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SUB_EXAM=" + Subexam + "";
            }

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " window.close();";
            divMsg.InnerHtml += " </script>";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Generate_Rollno.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND ISNULL(PREV_STATUS,0)=1 AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");
       
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        DIVEXAM.Visible = true;
        DataSet ds = objMarksEntry.GetLevelMarksEntryCourseDetail(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["Pattern"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PATTERNNO"]);
        }


        DataSet dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME", " FLDNAME+'-'+ CAST(EXAMNO AS NVARCHAR)", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND FLDNAME IN('EXTERMARK')  AND ISNULL(EXAMNAME,'')<>'' ", "EXAMNO");

        MainSubExamBind(ddlExam, dsMainExam);

        int TestMark = 0;
        TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));


    }


    //added by prafull on dt 23092022

    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {
        //if (ddlSubExam.SelectedIndex > 0)      //&& (ddlSubExam.SelectedIndex != 0)
        //{
        try
        {
            if (divSubExamName.Visible == true)
            {
                // SubExam = ddlSubExamName.SelectedValue;
                // SubExamName = ddlSubExamName.SelectedItem.Text;
            }
            string excelname = string.Empty;
            // string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;

            ViewState["StudCount"] = 0;
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

            int MExamNo = Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]);
            string subexamno = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "ISNULL(ACTIVESTATUS,0)=1 AND FLDNAME='" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + "'");


            // dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[1], SubExamName, Convert.ToInt32(ViewState["college_id"]));


            dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ddlsemester.SelectedValue), MExamNo, Convert.ToInt32(ddlCourse.SelectedValue), (ddlSubExamName.SelectedValue.Split('-')[1]));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    //excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");
                    //excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ViewState["CCODE"].ToString() + '_' + ddlSubExam.SelectedItem.Text + "_" + ddlSubExam.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");

                    excelname = Session["username"].ToString() + '_' + ddlSession.SelectedItem.Text + '_' + ccode + '_' + ddlSubExamName.SelectedItem.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy");

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

                    //   pnlSelection.Visible = false;
                    pnlMarkEntry.Visible = true;
                    pnlStudGrid.Visible = true;
                    lblStudents.Visible = true;
                    //btnBack.Visible = true;

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
        // }
        //else
        //{
        //    objCommon.DisplayMessage(updpanle1, "Please Select Exam!!", this.Page);
        //    ddlSubExam.Focus();
        //}
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

                        if (j == 13) // MARKS
                        {

                            maxMarks = dt.Rows[i]["MAXMARK"].ToString();
                            marks = dt.Rows[i]["MARKS"].ToString();
                            if (!marks.Equals(string.Empty) && !maxMarks.Equals(string.Empty))
                            {
                                if (marks == "")
                                {
                                    if (lock_status == 1)
                                    {
                                        objCommon.DisplayMessage(updpnl, "Marks Entry Not Completed!! Please Enter the Marks for all Students.", this.Page);
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
                                            ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");

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
                                            ShowMessage("Marks Entered [" + marks + "] can not be Greater than Max Marks[" + maxMarks + "].Also Marks can not be Less than 0 (zero).");

                                            flag = false;
                                            break;
                                        }
                                    }
                                }


                            }
                            else
                            {

                                ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                                flag = false;
                                break;

                            }
                        }

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
            int FlagReval = 0;
            string examname1 = (ddlExam.SelectedValue).Split('-')[0].ToString();
            string subExam_Name = (ddlSubExamName.Visible == true) ? (ddlSubExamName.SelectedValue) : "S10T1-19";




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
            int sectionno = 0;
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            // string[] course = lblCourse.Text.Split('~');
            //dstring ccode = course[0].Trim();
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
            examtype = "S";

            //return;
            if (!string.IsNullOrEmpty(studids))



                //cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname1, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["sem"]), sectionno);



                cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname1, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ddlsemester.SelectedValue), 0);



            ///TO SAVE BLOG LOG//////////////
            //   log = (CustomStatus)objMarksEntry.InsertMarkEntryBlobLog(Convert.ToInt32(ddlSession.SelectedValue), courseno, ccode, studids, marks, semno, lock_status, subExam, Convert.ToInt32(ViewState["examNo"]), sectionno, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, file_name);
            //////////////

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                if (lock_status == 1)
                {
                    objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Marks Saved Successfully.", this.Page);
                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();
                }
                //}
                //else
                //    objCommon.DisplayMessage("Error in Saving Marks!", this.Page);
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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddlSubExam.SelectedIndex != 0)
            //{
            if (FuBrowse.HasFile)           //  if (FuBrowse.HasFile) //(FuBrowse.PostedFile != null)
            {
                string path = MapPath("~/ExcelData/");
                ViewState["FileName"] = string.Empty;
                string filename = FuBrowse.FileName.ToString();
                string Extension = Path.GetExtension(filename);
                string Filepath = Server.MapPath("~/ExcelData/" + filename);
                //CreateBlobContainer(blob_ContainerName);
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
                        //int retval = Blob_Upload(blob_ConStr, blob_ContainerName, filename, FuBrowse);
                        //if (retval == 0)
                        //{
                        //}
                        //else
                        //{
                        //    File.Delete(Filepath); // To delete file from code folder after saved file in blob storage
                        //}
                        ShowStudents();
                        pnlUP.Visible = false;
                        objCommon.DisplayMessage(updpnl, "Mark Entry Uploaded Successfully !", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Only Excel Sheet is Allowed!", this);
                    return;
                }

            }

            else
            {
                objCommon.DisplayMessage(updpnl, "Select File to Upload!!!", this);
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
            case ".xlsx": //Excel 07 Excel07ConString
                //   conStr = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
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
                SaveExcelMarks(0, dt, Convert.ToInt32(ddlsemester.SelectedValue));            //04082022
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


    protected void lnkExcekImport_Click(object sender, EventArgs e)
    {
        //if ( ddlSubExam.SelectedIndex > 0)
        //{
        ViewState["markentrystatus"] = "0";
        if (ViewState["markentrystatus"].ToString() == "0")
        //if (ViewState["LOCK_STATUS"] == "")
        {
            pnlUP.Visible = true;
        }
        else
        {
            pnlUP.Visible = false;
            objCommon.DisplayMessage(updpnl, "Mark Entry is locked!", this.Page);
        }
        //}
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        pnlUP.Visible = false;                      //Added by Sachin A on 16082022
    }

    //protected void CreateBlobContainer(string Name)
    //{

    //    //Get the reference of the Storage Account
    //    CloudStorageAccount storageaccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString());
    //    //Get the reference of the Storage Blob

    //    CloudBlobClient client = storageaccount.CreateCloudBlobClient();

    //    //Get the reference of the Container. The GetConainerReference doesn't make a request to the Blob Storage but the Create() &CreateIfNotExists() method does. The method CreateIfNotExists() could be use whether the Container exists or not
    //    CloudBlobContainer container = client.GetContainerReference(Name);
    //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    
    //}

    //private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    //{
    //    CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
    //    CloudBlobClient client = account.CreateCloudBlobClient();
    //    CloudBlobContainer container = client.GetContainerReference(ContainerName);
    //    return container;
    //}

    //public void DeleteIFExits(string FileName)
    //{
    //    CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
    //    string FN = Path.GetFileNameWithoutExtension(FileName);
    //    try
    //    {
    //        Parallel.ForEach(container.ListBlobs(FN, true), y =>
    //        {
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            ((CloudBlockBlob)y).DeleteIfExists();
    //        });
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    //public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    //{
    //    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
    //    int retval = 1;
    //    string Ext = Path.GetExtension(FU.FileName);
    //    string FileName = DocName;
    //    try
    //    {
    //        DeleteIFExits(FileName);
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    
    //        container.SetPermissions(new BlobContainerPermissions
    //        {
    //            PublicAccess = BlobContainerPublicAccessType.Blob
    //        });

    //        CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
    //        cblob.UploadFromStream(FU.PostedFile.InputStream);
    //    }
    //    catch
    //    {
    //        retval = 0;
    //        return retval;
    //    }
    //    return retval;
    //}


    //private void ShowReportMarksEntryNew(string reportTitle, string rptFileName)
    //{

    //    string fldname = objCommon.LookUp("acd_exam_name", "DISTINCT FLDNAME", "EXAMNAME='" + Convert.ToString(ddlSubExamName.SelectedItem.Text) + "'");

    //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //    url += "Reports/CommonReport.aspx?";
    //    url += "pagetitle=" + reportTitle;
    //    url += "&path=~,Reports,Academic," + rptFileName;
    //    int section = 0;
    //    int ua_no= 0;
    //    string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

    //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=" + ua_no + ",@P_CCODE=" + ccode + ",@P_SECTIONNO=" + section + ",@P_SUBID=" + ddlSubjectType.SelectedValue + ",@P_EXAM=" + (ddlExam.SelectedValue).Split('-')[1] + ",@P_semesterno=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + ",@P_SUB_EXAM=" + ddlSubExamName.SelectedValue + "";

    //    divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //    divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //    divMsg.InnerHtml += " </script>";
    //    //update panel
    //    string Script = string.Empty;
    //    Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //    ScriptManager.RegisterClientScriptBlock(this.updpnl, updpnl.GetType(), "Report", Script, true);
    //}



    //protected void btninterreport_Click(object sender, EventArgs e)
    //{
    //    this.ShowReportMarksEntryNew("MarksListReport", "rptMarksList1_NEW_Atlas.rpt");//rptMarksList1.rpt
    //}
}

