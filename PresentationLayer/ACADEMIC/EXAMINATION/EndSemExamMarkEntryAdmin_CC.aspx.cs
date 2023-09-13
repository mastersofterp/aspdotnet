//=================================================================================
// PROJECT NAME  : COMMON CODE                                                          
// MODULE NAME   : ACADEMIC-EXAMINATION - MARK ENTRY BY ADMIN                                          
// CREATION DATE :                                   
// CREATED BY    : PRAFULL MUKE                                       
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
                

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

             
                if (Request.QueryString["pageno"] != null)
                {
                    int ADMIN_LEVEL_MARKS_ENTRY_USER = Convert.ToInt32(objCommon.LookUp("REFF", "isnull(ADMIN_LEVEL_MARKS_ENTRY,0)", ""));
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    CheckPageAuthorization();
                }
            }
        }
        if (Convert.ToInt32(Session["OrgId"]) == 6)  //Added by lalit dt 21-07-23 regarding RCPIPER backlog
        {

            divStudentType.Visible = true;

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
         
            if (Session["usertype"].ToString().Equals("1"))
            {

                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID DESC");
            }
            else
            {
                string deptno = objCommon.LookUp("USER_ACC", "UA_DEPTNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_SCHEME_MAPPING  SC INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=SC.DEGREENO AND CDB.BRANCHNO=SC.BRANCHNO AND CDB.COLLEGE_ID=SC.COLLEGE_ID", "COSCHNO", "COL_SCHEME_NAME", "SC.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SC.COLLEGE_ID > 0 AND SC.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND CDB.DEPTNO IN (" + deptno + ")", "SC.COLLEGE_ID DESC");
            }
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

                    //added by prafull on dt 20012023 to check conversion Rule 

                    if (Convert.ToInt32(Session["OrgId"]) == 8)
                    {

                        int admbatch = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT ADMBATCH", "SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString())));

                        if (admbatch >= 17)
                        {

                            if (examtype == "E")
                            {
                                double intermark_master = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "MAXMARKS_I", "COURSENO=" + ddlCourse.SelectedValue));
                                for (int i = 0; i < gvStudent.Rows.Count; i++)
                                {
                                    TextBox intermark = gvStudent.Rows[i].FindControl("lblInternal") as TextBox;
                                    double markinter;
                                    string newvar = intermark.ToolTip;
                                    if (newvar == "" || newvar == string.Empty)
                                    {
                                        markinter = 0.00;
                                    }
                                    else
                                    {
                                        markinter = Convert.ToDouble(intermark.ToolTip);
                                    }
                                    if (markinter > intermark_master)
                                    {
                                        objCommon.DisplayMessage(updpnl, "Please Check The Conversion Rule For Internal Mark...!", this.Page);
                                        return;
                                    }

                                }
                            }
                        }
                        else
                        {
                            //no need for validation data;
                        }
                    }
                    else
                    {
                        if (examtype == "E")
                        {
                            double intermark_master = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "MAXMARKS_I", "COURSENO=" + ddlCourse.SelectedValue));
                            for (int i = 0; i < gvStudent.Rows.Count; i++)
                            {
                                TextBox intermark = gvStudent.Rows[i].FindControl("lblInternal") as TextBox;
                                double markinter;
                                string newvar = intermark.ToolTip;
                                if (newvar == "" || newvar == string.Empty)
                                {
                                    markinter = 0.00;
                                }
                                else
                                {
                                    markinter = Convert.ToDouble(intermark.ToolTip);
                                }
                                if (markinter > intermark_master)
                                {
                                    objCommon.DisplayMessage(updpnl, "Please Check The Conversion Rule For Internal Mark...!", this.Page);
                                    return;
                                }

                            }
                        }
                    }


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


                  
                    string examname = string.Empty;

                    examname = Exam[0]; //Column Name like S1,S2.....EXTERMARK

                    string SubExamName = string.Empty;
                    string SubExamComponentName = string.Empty;


                    if (divSubExamName.Visible == true)
                    {
                        //if (ddlSubjectType.SelectedValue == "2" || ddlSubjectType.SelectedValue == "11")
                        //{
                        //    Subexam = ddlSubExamName.SelectedValue;
                        //    SubExamComponentName = ddlSubExamName.SelectedItem.Text;
                        //}
                        if (examtype == "S")
                        {
                            SubExamComponentName = ddlSubExamName.SelectedValue;
                            examname = ddlExam.SelectedValue;
                        }
                    }
                    else
                    {
                        SubExamComponentName = objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNAME", "EXAMNO=" + Exam[1]); ;
                       // Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME", " CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Exam[1]);


                        Subexam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "CAST(FLDNAME AS NVARCHAR)+'-'+ CAST (SA.SUBEXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));

                    }

                       CustomStatus cs = 0; 
                        if (examtype == "S")
                        {
                             cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);
                        }
                        else
                        {
                             cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
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
    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        SaveAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
       
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



    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");

    }
    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        string stu1 = (ddlStudenttype.SelectedValue);
        string examtype = string.Empty;
        string[] Examname = ddlExam.SelectedValue.Split('-');



        if (Examname[0].StartsWith("S"))
            examtype = "S";
        else if (Examname[0].StartsWith("E"))
            examtype = "E";

        divSubExamName.Visible = false;
        pnlStudGrid.Visible = false;

  
            if (examtype == "S")
            {
                if (ddlExam.SelectedIndex > 0)
                {
                    btnReGrade.Enabled = false;
                    btnGrade.Visible = false;
                    string[] Exam = ddlExam.SelectedValue.Split('-');

                    #region RCPIPER
                    if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                    {
                        if (stu1 == "1")
                        {
                            if (Convert.ToInt32(Session["usertype"]) == 1)
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }
                            else
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Session["usertype"]) == 1)
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1  AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }
                            else
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }

                        }
                    }
                    #endregion
                    else
                    {
                        if (Convert.ToInt32(Session["usertype"]) == 1)
                        {
                            objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                        }
                        else
                        {
                            objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                        }

                    }
                  

                        //  ddlSubExamName.SelectedIndex = 1;

                    if (Exam[0].ToUpper() == "S2" || Exam[0].ToUpper().ToUpper() == "S3" || Exam[0].ToUpper() == "S1" || Exam[0].ToUpper() == "S6" || Exam[0].ToUpper() == "S10" || Exam[0].ToUpper() == "S5" || Exam[0].ToUpper() == "S4" || Exam[0].ToUpper() == "S7" || Exam[0].ToUpper() == "S8")
                        {

                            DataSet dsSubExam = objCommon.FillDropDown("ACD_EXAM_NAME", " CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>''AND FLDNAME NOT IN ('EXTERMARK')", "EXAMNO");
                            //MainSubExamBind(ddlSubExamName, dsSubExam);
                            divSubExamName.Visible = true;
                        }
                    }
                
                Clear();
            }
            else
            {
                if (Convert.ToInt32(Session["OrgId"]) == 3 || Convert.ToInt32(Session["OrgId"]) == 4)
                {
                    divSubExamName.Visible = true;
                }
                else
                {
                    divSubExamName.Visible = false;
                }
                pnlStudGrid.Visible = false;

                if (ddlExam.SelectedIndex > 0)
                {
                        string[] Exam = ddlExam.SelectedValue.Split('-');

                        if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                        {
                            if (stu1 == "1")
                            {
                                if (Convert.ToInt32(Session["usertype"]) == 1)
                                {
                                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(Session["usertype"]) == 1)
                                {
                                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1  AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                                }
                                else
                                {
                                    objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                                }

                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(Session["usertype"]) == 1)
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }
                            else
                            {
                                objCommon.FillDropDownList(ddlSubExamName, "ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "DISTINCT CAST(FLDNAME AS VARCHAR)+'-'+CAST(SA.SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0 AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "");
                            }

                        }

                        // ddlSubExamName.SelectedIndex = 1;
                        if (ddlSubExamName.Items.Count > 1)
                        {
                            ddlSubExamName.SelectedIndex = 1;
                        }
                        else
                        {
                            ddlSubExamName.SelectedIndex = 0;
                            ddlExam.SelectedIndex = 0;
                            objCommon.DisplayMessage(updpnl, "Subexam Or Assesment Component is not defined for selected exam..!!", this.Page);
                            return;
                        }

                          if (Exam[0].ToUpper() == "S2" || Exam[0].ToUpper().ToUpper() == "S3" || Exam[0].ToUpper() == "S1" || Exam[0].ToUpper() == "S6" || Exam[0].ToUpper() == "S10" || Exam[0].ToUpper() == "S5" || Exam[0].ToUpper() == "S4" || Exam[0].ToUpper() == "S7" || Exam[0].ToUpper() == "S8")
                  
                        {
                            DataSet dsSubExam = objCommon.FillDropDown("ACD_EXAM_NAME", " DISTINCT CAST(EXAMNO AS NVARCHAR)+'-'+ FLDNAME AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND EXAMTYPE=2 AND FLDNAME IN('EXTERMARK')", "EXAMNO");
                            MainSubExamBind(ddlSubExamName, dsSubExam);
                            divSubExamName.Visible = true;
                        }
                    
                }
                Clear();
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
              
            if (Session["usertype"] == "3")
            {

                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND (UA_NO=" + Convert.ToInt32(Session["userno"]) + " OR UA_NO_PRAC=" + Convert.ToInt32(Session["userno"]) + ") AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            }
            else
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ViewState["schemeno"] + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            }
                ddlCourse.Focus();
                ddlSubExamName.SelectedIndex = 0;
                divSubExamName.Visible = false;
                DIVEXAM.Visible = true;
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
        int Is_Specialcase = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "ISNULL(IS_SPECIAL,0)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));

        if (ddlExam.SelectedIndex > 0)
        {
            ShowStudents();
        }

    }

    private void ShowStudents()
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
                    if (Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                        if (ddlStudenttype.SelectedValue == "1")
                        {
                            SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
                        }
                        else
                        {
                            SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
                        }

                    }
                    else
                    {
                        SubExam = objCommon.LookUp("ACD_SUBEXAM_NAME SA INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT EC ON(EC.SUBEXAMNO=SA.SUBEXAMNO)", "SA.SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + " and  ISNULL(CANCLE,0)=0 and ISNULL(ACTIVESTATUS,0)=1 AND EC.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SUBEXAM_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue));
                    }
                    
                }

                    Subexamno = Convert.ToInt32(objCommon.LookUp("ACD_SUBEXAM_NAME", "SUBEXAMNO", "EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1]));

                    if (Convert.ToInt32(Session["OrgId"]) == 6)
                    {
                        if (ddlStudenttype.SelectedValue == "1")
                        {
                            ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

                        }
                        else
                        {
                            ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

                        }
                    }
                    else
                    {
                        ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExamName.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"].ToString()) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "", "");

                    }
                
                string extermark = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                if (extermark != "0.00")
                {
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
                }

                DataSet dsStudent = null;
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);

                if (Convert.ToInt32(Session["OrgId"]) == 6)
                {
                    if (ddlStudenttype.SelectedValue == "1")
                    {
                        if (examtype == "S")
                        {
                            dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]), Convert.ToInt32(ddlsemester.SelectedValue));
                        }
                        else
                        {
                            dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]),Convert.ToInt32(ddlsemester.SelectedValue));
                        }
                    }
                    else 
                    {
                        if (examtype == "S")
                        {
                            string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_ADMIN_CC_BACKLOG";
                            string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID,@P_EXAMNO,@P_SUBEXAMNO";
                            string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + (ddlSubExamName.SelectedValue).Split('-')[0] + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "," + Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]) + "," + Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]) + "";
                            dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                      
                        }
                        else
                        {

                           
                            string sp_procedure = "PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL_CC_Backlog";
                            string sp_parameters = "@P_SESSIONNO,@P_UA_NO,@P_CCODE,@P_SECTIONNO,@P_SUBID,@P_EXAM,@P_SCHEMENO,@P_SUBEXAM,@P_SUBEXAMNAME,@P_COLLEGE_ID";
                            string sp_callValues = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(Session["userno"].ToString()) + "," + ccode + "," + 0 + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + Exam[0] + "," + Convert.ToInt32(ViewState["schemeno"].ToString()) + "," + SubExam + "," + SubExamName + "," + Convert.ToInt32(ViewState["college_id"]) + "";
                            dsStudent = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                       
                        }

                    }
                   
                }
                else
                {
                    if (examtype == "S")
                    {

                        dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_new(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), (ddlSubExamName.SelectedValue).Split('-')[0], SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32((ddlExam.SelectedValue).Split('-')[1]), Convert.ToInt32((ddlSubExamName.SelectedValue).Split('-')[1]),Convert.ToInt32(ddlsemester.SelectedValue));
                    }
                    else
                    {
                        dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Exam[0], Convert.ToInt32(ViewState["schemeno"].ToString()), SubExam, SubExamName, Convert.ToInt32(ViewState["college_id"]), Convert.ToInt32(ddlsemester.SelectedValue));
                    }
                } 
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {

                       
                        string excelStatus = objCommon.LookUp("ACD_EXAM_CONFIGURATION", "ExcelMarkEntry", "");
                        if (excelStatus == "1")
                        {
                            lnkExcekImport.Visible = true;

                        }
                        else
                        {
                            lnkExcekImport.Visible = false;
                        }
                        if (Convert.ToInt32(Session["OrgId"]) == 6)
                        {
                            btnEndSemReport.Visible = true;
                        }
                        else
                        {
                            btnEndSemReport.Visible = false;
                        }


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
                            if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
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
                            string extermarkNEW = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                            if (extermarkNEW != "0.00")
                            {
                                //Convert.ToDecimal(Convert.ToString(dsStudent.Tables[0].Rows[0]["SMAX"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["SMAX"].ToString())
                                if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"].ToString() == string.Empty ? "0" : dsStudent.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
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
                            else
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
                                gvStudent.Columns[5].Visible = false;
                                gvStudent.Columns[3].Visible = true;
                                gvStudent.Columns[4].Visible = false;
                                //btnSave.Visible = false;
                                //btnLock.Visible = false;
                                ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                                ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                                ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
                            }

                        }
                        lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                        //Bind the Student List
                        gvStudent.DataSource = dsStudent;
                        gvStudent.DataBind();



                        //added by prafull on dt 13042023   

                        
                        int lockcount = 0;
                        int lockcount_test = 0;
                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["LOCK"]) == true)
                            {
                                lockcount++;
                            }
                        }

                        for (int i = 0; i < dsStudent.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dsStudent.Tables[0].Rows[i]["TESTLOCK"]) == true)
                            {
                                lockcount_test++;
                            }
                        }




                        // added for absent student by prafull on dated 23072022
                        int z = 0;

                        //commented by prafull on dt 10022023

                        //foreach (GridViewRow rw in gvStudent.Rows)
                        //{
                        //    TextBox txtmark = (TextBox)rw.FindControl("txtmarks");
                        //    string regno = (dsStudent.Tables[0].Rows[z]["REGNO"]).ToString();

                        //    if ((dsStudent.Tables[0].Rows[z]["SMARK"]) is DBNull)
                        //    {
                        //        txtmark.Enabled = true;
                        //    }
                        //    else if ((Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 902.00) || (Convert.ToDouble(dsStudent.Tables[0].Rows[z]["SMARK"]) == 903.00))
                        //    {
                        //        txtmark.Enabled = false;
                        //    }
                        //    else if (Convert.ToBoolean(dsStudent.Tables[0].Rows[z]["LOCK"]) == true)
                        //    {
                        //        txtmark.Enabled = false;
                        //    }
                        //    else
                        //    {
                        //        txtmark.Enabled = true;
                        //    }


                        //    z++;
                        //}
                        // prafull comment end 
                        #region Comment Code



                        #endregion Comment Code

                        btnSave.Enabled = true;
                        btnLock.Enabled = true;
                        btnSave.Visible = true;
                        btnLock.Visible = true;

                        btnExcelReport.Enabled = true;
                        btnExcelReport.Visible = true;
                        //lnkExcekImport.Visible = true;


                        int TestMark = 0;
                        TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                        string extermarmew = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));


                       
                        btnGrade.Visible = false;

                        if (examtype == "S")
                        {
                            int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
                               if (SESSION_TYPE == 1)
                               {
                              
                                    if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
                                    {
                                        #region
                                          if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                             {

                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;

                                        btnSave.Visible = false;
                                        btnLock.Visible = false;


                                        //TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));
                                        //extermarmew = Convert.ToString(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                                        if (TestMark > 0)
                                        {

                                            if (extermarmew == "0.00")
                                            {
                                                string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                if (dschk.Tables.Count > 0)
                                                {
                                                    if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                    {
                                                        string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                        if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                        {
                                                            // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

                                                            btnGrade.Enabled = false;
                                                            btnGrade.Visible = false;

                                                        }
                                                        else
                                                        {
                                                            if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                            {
                                                                btnReGrade.Enabled = true;
                                                                btnReGrade.Visible = true;
                                                                btnGrade.Enabled = false;
                                                                btnGrade.Visible = false;
                                                            }
                                                            else
                                                            {
                                                                btnReGrade.Enabled = false;
                                                                btnReGrade.Visible = false;
                                                                btnGrade.Enabled = true;
                                                                btnGrade.Visible = true;
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                btnGrade.Enabled = false;
                                                btnGrade.Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                        }
                                        lnkExcekImport.Enabled = false;

                                        //btnReGrade.Enabled = false;
                                        //btnReGrade.Visible = false;

                                    }
                                         else
                                          {

                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;

                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                       if (TestMark > 0)
                                        {

                                            if (extermarmew == "0.00")
                                            {
                                                string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                if (dschk.Tables.Count > 0)
                                                {
                                                    if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                    {
                                                        string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                        if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                        {
                                                            // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

                                                            btnGrade.Enabled = false;
                                                            btnGrade.Visible = false;

                                                        }
                                                        else
                                                        {
                                                            if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                            {
                                                                btnReGrade.Enabled = true;
                                                                btnReGrade.Visible = true;
                                                                btnGrade.Enabled = false;
                                                                btnGrade.Visible = false;
                                                            }
                                                            else
                                                            {
                                                                btnReGrade.Enabled = false;
                                                                btnReGrade.Visible = false;
                                                                btnGrade.Enabled = true;
                                                                btnGrade.Visible = true;
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                btnGrade.Enabled = false;
                                                btnGrade.Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                        }
                                        lnkExcekImport.Enabled = false;

                                        //btnReGrade.Enabled = false;
                                        //btnReGrade.Visible = false;
                                }
                               #endregion
                                    }
                                 else
                                {
                                    gvStudent.Columns[5].Visible = false;
                                    gvStudent.Columns[6].Visible = false;
                                    btnSave.Enabled = true;
                                    btnLock.Enabled = true;
                                    btnSave.Visible = true;
                                    btnLock.Visible = true;
                                    lnkExcekImport.Enabled = true;
                                    if (TestMark > 0)
                                    {
                                        if (extermarmew == "0.00")
                                        {
                                            string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                            string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                            string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                            DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                            if (dschk.Tables.Count > 0)
                                            {
                                                if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                {
                                                    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                    if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                    {
                                                        btnGrade.Enabled = false;
                                                        btnGrade.Visible = false;
                                                    }
                                                    else
                                                    {
                                                        if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                        {
                                                            btnReGrade.Enabled = true;
                                                            btnReGrade.Visible = true;
                                                            btnGrade.Enabled = false;
                                                            btnGrade.Visible = false;
                                                        }
                                                        else
                                                        {
                                                            btnReGrade.Enabled = false;
                                                            btnReGrade.Visible = false;
                                                            btnGrade.Enabled = true;
                                                            btnGrade.Visible = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        btnReGrade.Enabled = false;
                                        btnReGrade.Visible = false;
                                    }
                                }
                            }
                            else if (SESSION_TYPE == 2)
                               {

                                   if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
                                   {
                                       #region
                                       if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                       {

                                           btnSave.Enabled = false;
                                           btnLock.Enabled = false;

                                           btnSave.Visible = false;
                                           btnLock.Visible = false;
                                           if (TestMark > 0)
                                           {

                                               if (extermarmew == "0.00")
                                               {
                                                   string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                   string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                   string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                   DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                   if (dschk.Tables.Count > 0)
                                                   {
                                                       if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                       {
                                                           string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                           if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                           {
                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;

                                                           }
                                                           else
                                                           {
                                                               if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                               {
                                                                   btnReGrade.Enabled = true;
                                                                   btnReGrade.Visible = true;
                                                                   btnGrade.Enabled = false;
                                                                   btnGrade.Visible = false;
                                                               }
                                                               else
                                                               {
                                                                   btnReGrade.Enabled = false;
                                                                   btnReGrade.Visible = false;
                                                                   btnGrade.Enabled = true;
                                                                   btnGrade.Visible = true;
                                                               }

                                                           }
                                                       }
                                                   }
                                               }
                                               else
                                               {
                                                   btnGrade.Enabled = false;
                                                   btnGrade.Visible = false;
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                           lnkExcekImport.Enabled = false;

                                       }
                                       else
                                       {

                                           btnSave.Enabled = true;
                                           btnLock.Enabled = true;

                                           btnSave.Visible = true;
                                           btnLock.Visible = true;
                                           if (TestMark > 0)
                                           {

                                               if (extermarmew == "0.00")
                                               {
                                                   string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                   string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                   string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                   DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                   if (dschk.Tables.Count > 0)
                                                   {
                                                       if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                       {
                                                           string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                           if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                           {
                                                               

                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;

                                                           }
                                                           else
                                                           {
                                                               if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                               {
                                                                   btnReGrade.Enabled = true;
                                                                   btnReGrade.Visible = true;
                                                                   btnGrade.Enabled = false;
                                                                   btnGrade.Visible = false;
                                                               }
                                                               else
                                                               {
                                                                   btnReGrade.Enabled = false;
                                                                   btnReGrade.Visible = false;
                                                                   btnGrade.Enabled = true;
                                                                   btnGrade.Visible = true;
                                                               }

                                                           }
                                                       }
                                                   }
                                               }
                                               else
                                               {
                                                   btnGrade.Enabled = false;
                                                   btnGrade.Visible = false;
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                           lnkExcekImport.Enabled = false;
                                      }
                                       #endregion
                                   }
                                   else
                                   {
                                       gvStudent.Columns[5].Visible = false;
                                       gvStudent.Columns[6].Visible = false;

                                       btnSave.Enabled = true;
                                       btnLock.Enabled = true;

                                       btnSave.Visible = true;
                                       btnLock.Visible = true;
                                       lnkExcekImport.Enabled = true;
                                
                                       if (TestMark > 0)
                                       {
                                           if (extermarmew == "0.00")
                                           {
                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                               if (dschk.Tables.Count > 0)
                                               {
                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                   {
                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                       {
                                                           btnGrade.Enabled = false;
                                                           btnGrade.Visible = false;
                                                       }
                                                       else
                                                       {
                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                           {
                                                               btnReGrade.Enabled = true;
                                                               btnReGrade.Visible = true;
                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;
                                                           }
                                                           else
                                                           {
                                                               btnReGrade.Enabled = false;
                                                               btnReGrade.Visible = false;
                                                               btnGrade.Enabled = true;
                                                               btnGrade.Visible = true;
                                                           }
                                                       }
                                                   }
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                       }
                                       else
                                       {
                                           btnReGrade.Enabled = false;
                                           btnReGrade.Visible = false;
                                       }
                                   }

                               }

                               #region Session_type3

                               ///******************added by prafull on dt:12092023 For Remedial ExamType 2******************************
                               else if(SESSION_TYPE==3)
                               {

                                   if (dsStudent.Tables[0].Rows[0]["TESTLOCK"].ToString() == "True")
                                   {
                                       #region
                                       if (lockcount_test == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                       {

                                           btnSave.Enabled = false;
                                           btnLock.Enabled = false;

                                           btnSave.Visible = false;
                                           btnLock.Visible = false;
                                           if (TestMark > 0)
                                           {

                                               if (extermarmew == "0.00")
                                               {
                                                   string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                   string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                   string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                   DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                   if (dschk.Tables.Count > 0)
                                                   {
                                                       if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                       {
                                                           string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                           if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                           {
                                                               // objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to Generat the Grade marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);

                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;

                                                           }
                                                           else
                                                           {
                                                               if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                               {
                                                                   btnReGrade.Enabled = true;
                                                                   btnReGrade.Visible = true;
                                                                   btnGrade.Enabled = false;
                                                                   btnGrade.Visible = false;
                                                               }
                                                               else
                                                               {
                                                                   btnReGrade.Enabled = false;
                                                                   btnReGrade.Visible = false;
                                                                   btnGrade.Enabled = true;
                                                                   btnGrade.Visible = true;
                                                               }

                                                           }
                                                       }
                                                   }
                                               }
                                               else
                                               {
                                                   btnGrade.Enabled = false;
                                                   btnGrade.Visible = false;
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                           lnkExcekImport.Enabled = false;

                                       }
                                       else
                                       {

                                           btnSave.Enabled = true;
                                           btnLock.Enabled = true;

                                           btnSave.Visible = true;
                                           btnLock.Visible = true;
                                           if (TestMark > 0)
                                           {

                                               if (extermarmew == "0.00")
                                               {
                                                   string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                                   string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                                   string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                                   DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                                   if (dschk.Tables.Count > 0)
                                                   {
                                                       if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                       {
                                                           string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                           if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                           {


                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;

                                                           }
                                                           else
                                                           {
                                                               if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                               {
                                                                   btnReGrade.Enabled = true;
                                                                   btnReGrade.Visible = true;
                                                                   btnGrade.Enabled = false;
                                                                   btnGrade.Visible = false;
                                                               }
                                                               else
                                                               {
                                                                   btnReGrade.Enabled = false;
                                                                   btnReGrade.Visible = false;
                                                                   btnGrade.Enabled = true;
                                                                   btnGrade.Visible = true;
                                                               }

                                                           }
                                                       }
                                                   }
                                               }
                                               else
                                               {
                                                   btnGrade.Enabled = false;
                                                   btnGrade.Visible = false;
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                           lnkExcekImport.Enabled = false;
                                       }
                                       #endregion
                                   }
                                   else
                                   {
                                       gvStudent.Columns[5].Visible = false;
                                       gvStudent.Columns[6].Visible = false;

                                       btnSave.Enabled = true;
                                       btnLock.Enabled = true;

                                       btnSave.Visible = true;
                                       btnLock.Visible = true;
                                       lnkExcekImport.Enabled = true;
                              
                                       if (TestMark > 0)
                                       {
                                           if (extermarmew == "0.00")
                                           {
                                               string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                               string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                               string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                               DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                               if (dschk.Tables.Count > 0)
                                               {
                                                   if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                                   {
                                                       string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                                       if (islocked == "0" || islocked == string.Empty || islocked == null)
                                                       {
                                                           btnGrade.Enabled = false;
                                                           btnGrade.Visible = false;

                                                       }
                                                       else
                                                       {
                                                           if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                           {
                                                               btnReGrade.Enabled = true;
                                                               btnReGrade.Visible = true;
                                                               btnGrade.Enabled = false;
                                                               btnGrade.Visible = false;
                                                           }
                                                           else
                                                           {
                                                               btnReGrade.Enabled = false;
                                                               btnReGrade.Visible = false;
                                                               btnGrade.Enabled = true;
                                                               btnGrade.Visible = true;
                                                           }
                                                       }
                                                   }
                                               }
                                           }
                                           else
                                           {
                                               btnGrade.Enabled = false;
                                               btnGrade.Visible = false;
                                           }
                                       }
                                       else
                                       {
                                           btnReGrade.Enabled = false;
                                           btnReGrade.Visible = false;
                                       }
                                  
                                   }
                               }
                           
                            #endregion
                            pnlStudGrid.Visible = true;                         
                            lblStudents.Visible = true;

                          
                            if (TestMark > 0)
                            {
                                if (extermarmew == "0.00")
                                {
                                    string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                                    string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                                    string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                                    DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                                    if (dschk.Tables.Count > 0)
                                    {
                                        if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                        {
                                            string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                            if (islocked == "0" || islocked == string.Empty || islocked == null)
                                            {
                                                gvStudent.Columns[7].Visible = false;
                                                btnReGrade.Enabled = false;
                                                btnReGrade.Visible = false;

                                            }
                                            else
                                            {
                                                if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                                {
                                                    gvStudent.Columns[7].Visible = true;
                                                    btnReGrade.Enabled = true;
                                                    btnReGrade.Visible = true;
                                                }
                                                else
                                                {
                                                    gvStudent.Columns[7].Visible = false;
                                                    btnReGrade.Enabled = false;
                                                    btnReGrade.Visible = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    gvStudent.Columns[7].Visible = false;
                                    btnReGrade.Enabled = false;
                                    btnReGrade.Visible = false;
                                }
                            }
                            else
                            {
                                gvStudent.Columns[7].Visible = false;
                                btnReGrade.Enabled = false;
                                btnReGrade.Visible = false;
                            }
                        }
                        else
                        {
                            int SESSION_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + ddlSession.SelectedValue));
                            if (SESSION_TYPE == 1)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {

                                    if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;

                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }
                                    }
                                    else
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;

                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }

                                    }

                                    //end 

                                }
                                else
                                {
                                    gvStudent.Columns[3].Visible = true;
                                    gvStudent.Columns[6].Visible = false;
                                    //lnkExcekImport.Visible = true;
                                }
                            }

                            else if (SESSION_TYPE == 2)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {

                                    if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;

                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }
                                    }
                                    else
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;

                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }

                                    }

                                    //end 

                                }
                                else
                                {
                                    gvStudent.Columns[3].Visible = true;
                                    gvStudent.Columns[6].Visible = false;
                                    //lnkExcekImport.Visible = true;
                                }
                            }
                            else if (SESSION_TYPE == 3)
                            {
                                if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                                {

                                    if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = false;
                                        btnLock.Enabled = false;

                                        btnSave.Visible = false;
                                        btnLock.Visible = false;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));



                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }
                                    }
                                    else
                                    {

                                        gvStudent.Columns[3].Visible = true;
                                        gvStudent.Columns[6].Visible = true;
                                        //gvStudent.Columns[4].Enabled = false;
                                        btnSave.Enabled = true;
                                        btnLock.Enabled = true;

                                        btnSave.Visible = true;
                                        btnLock.Visible = true;
                                        btnExcelReport.Enabled = true;
                                        btnExcelReport.Visible = true;
                                        //lnkExcekImport.Visible = false;

                                        int studentcount = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(DISTINCT IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + " AND ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0)=1"));

                                        //added by prafull on dt 30032023  for grade button only for admin login

                                        if (Convert.ToInt32(Session["OrgId"]) != 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;

                                            btnEndSemReport.Visible = false;
                                        }
                                        else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                        {
                                            btnGrade.Enabled = true;
                                            btnGrade.Visible = true;
                                            btnEndSemReport.Visible = true;
                                        }
                                        else
                                        {
                                            btnGrade.Enabled = false;
                                            btnGrade.Visible = false;
                                            btnEndSemReport.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    gvStudent.Columns[3].Visible = true;
                                    gvStudent.Columns[6].Visible = false;
                                    //lnkExcekImport.Visible = true;
                                }
                            }
                            pnlStudGrid.Visible = true;                          
                            lblStudents.Visible = true;



                            if (dsStudent.Tables[0].Rows[0]["GRADE"].ToString() != string.Empty && dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                            {
                                if (lockcount == Convert.ToInt32(dsStudent.Tables[0].Rows.Count.ToString()))
                                {
                                    gvStudent.Columns[7].Visible = true;
                                    //gvStudent.Columns[5].Visible = true;
                                    btnSave.Enabled = false;
                                    btnLock.Enabled = false;

                                    btnSave.Visible = false;
                                    btnLock.Visible = false;
                                    btnGrade.Enabled = false;
                                    btnGrade.Visible = false;



                                    //added by prafull on dt 30032023  for grade button only for admin login

                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
                                    {
                                        btnReGrade.Enabled = true;
                                        btnReGrade.Visible = true;
                                    }
                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                    {
                                        btnReGrade.Enabled = true;
                                        btnReGrade.Visible = true;
                                    }
                                    else
                                    {
                                        btnReGrade.Enabled = false;
                                        btnReGrade.Visible = false;
                                    }
                                }
                                else
                                {

                                    gvStudent.Columns[7].Visible = false;
                                    gvStudent.Columns[6].Visible = false;
                                    //gvStudent.Columns[5].Visible = true;
                                    btnSave.Enabled = true;
                                    btnLock.Enabled = true;

                                    btnSave.Visible = true;
                                    btnLock.Visible = true;
                                    btnGrade.Enabled = false;
                                    btnGrade.Visible = false;



                                    //added by prafull on dt 30032023  for grade button only for admin login

                                    if (Convert.ToInt32(Session["OrgId"]) != 6)
                                    {
                                        btnReGrade.Enabled = false;
                                        btnReGrade.Visible = false;
                                    }
                                    else if (Convert.ToInt32(Session["usertype"]) == 1 && Convert.ToInt32(Session["OrgId"]) == 6)
                                    {
                                        btnReGrade.Enabled = false;
                                        btnReGrade.Visible = false;
                                    }
                                    else
                                    {
                                        btnReGrade.Enabled = false;
                                        btnReGrade.Visible = false;
                                    }

                                }
                             
                            }
                            else
                            {
                                gvStudent.Columns[7].Visible = false;
                                // btnSave.Enabled = true;
                                // btnLock.Enabled = true;
                                //   btnSave.Visible = true;
                                //  btnLock.Visible = true;
                                // btnGrade.Enabled = false;
                                //btnGrade.Visible = false;
                                btnReGrade.Enabled = false;
                                btnReGrade.Visible = false;

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
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlcollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            
                    if (ddlcollege.SelectedIndex > 0)
                    {
                        int count = 0;
                        count = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COUNT(SESSIONNO)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
                        //if (1 > 0)
                        //{
                            
                              objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");                           
                            ddlSession.Focus();
                        //}
                        //else
                        //{
                        //    ddlSession.Focus();
                        //    objCommon.DisplayMessage(this.updpnl, "Session Activity not Created Or activity may not be Started!!!", this.Page);
                        //    return;
                        //}
                    }
                    else
                    {
                         objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                         return;
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

                CustomStatus cs;
                if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    cs = (CustomStatus)objMarksEntry.GradeGenaerationNew_MIT(studids, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
                }
                else
                {
                    cs = (CustomStatus)objMarksEntry.GradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
                }
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
                            //ShowStudentsSpecialSubject();
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

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", " DISTINCT R.SUBID", "S.SUBNAME", "R.SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "R.SUBID");
        //objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
    }
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

            string stu = (ddlStudenttype.SelectedValue);
            if (ddlCourse.SelectedIndex <= 0)
            {
                ddlExam.SelectedIndex = 0;
                ddlExam.ClearSelection();
                return;
            }
            else
            {
                
                    DIVEXAM.Visible = true;
               
                DataSet dsMainExam=null;
                DataSet ds = objMarksEntry.GetLevelMarksEntryCourseDetail(Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["Pattern"] = Convert.ToInt32(ds.Tables[0].Rows[0]["PATTERNNO"]);
                }


                if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                {
                    if (stu == "1")
                    {
                        if (Convert.ToInt32(Session["usertype"]) == 1)
                        {
                            dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                        }
                        else
                        {
                            dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                        }
                    }
                    else
                    {

                        if (Convert.ToInt32(Session["usertype"]) == 1)
                        {
                            dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                        }
                        else
                        {
                            dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                        }


                    }

                }
                else
                {
                    if (Convert.ToInt32(Session["usertype"]) == 1)
                    {
                        dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                    }
                    else
                    {
                        dsMainExam = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                    }

                }


                MainSubExamBind(ddlExam, dsMainExam);
                int TestMark = 0;
                double maxmark_i = 0;
                double maxmark_e = 0;
                maxmark_i = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "MAXMARKS_I", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                maxmark_e = Convert.ToDouble(objCommon.LookUp("ACD_COURSE", "MAXMARKS_E", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
                TestMark = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_TEST_MARK", "COUNT(*)", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

                    if (maxmark_i > 0)
                    {
                        if (TestMark > 0)
                        {

                            string sp_procedure = "PKG_ACD_CHECK_SUB_EXAMS_LOCKED_FOR_ENDSEM_MARK_ENTRY_CC";
                            string sp_parameters = "@P_COURSENO,@P_SCHEMENO,@P_UA_NO,@P_SESSIONNO";
                            string sp_callValues = "" + (ddlCourse.SelectedValue) + "," + ViewState["schemeno"].ToString() + "," + (Session["userno"].ToString()) + "," + ddlSession.SelectedValue + "";
                            DataSet dsMainExamnew=null;
                            DataSet dschk = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
                            if (dschk.Tables.Count > 0)
                            {
                                if (dschk.Tables[0].Rows.Count > 0 && dschk.Tables != null)
                                {
                                    string islocked = dschk.Tables[0].Rows[0]["LOCK"].ToString();

                                    if (islocked == "0" || islocked == string.Empty || islocked == null)
                                    {
                                        objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to enter the external marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);
                                        ddlSubExamName.SelectedIndex = 0;
                                        divSubExamName.Visible = false;
                                        ddlExam.SelectedIndex = 0;
                                        gvStudent.DataSource = null;
                                        gvStudent.DataBind();
                                        pnlStudGrid.Visible = false;
                                        ddlExam.Items.Clear();
                                        ddlExam.Items.Add("Please Select");
                                        ddlExam.SelectedItem.Value = "0";


                                        if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                                        {
                                            if (stu == "1")
                                            {
                                                if (Convert.ToInt32(Session["usertype"]) == 1)
                                                {
                                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                                }
                                                else
                                                {
                                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                                }
                                            }
                                            else
                                            {

                                                if (Convert.ToInt32(Session["usertype"]) == 1)
                                                {
                                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                                }
                                                else
                                                {
                                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                                }


                                            }

                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(Session["usertype"]) == 1)
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }
                                            else
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }

                                        }

                                      
                                        MainSubExamBind(ddlExam, dsMainExamnew);
                                        return;
                                    }
                                }
                                else
                                {
                                    //DataSet dsMainExamnew=null;
                                    ddlSubExamName.SelectedIndex = 0;
                                    divSubExamName.Visible = false;
                                    ddlExam.SelectedIndex = 0;
                                    gvStudent.DataSource = null;
                                    gvStudent.DataBind();
                                    ddlExam.Items.Clear();
                                    ddlExam.Items.Add("Please Select");
                                    ddlExam.SelectedItem.Value = "0";




                                    if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                                    {
                                        if (stu == "1")
                                        {
                                            if (Convert.ToInt32(Session["usertype"]) == 1)
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK') GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }
                                            else
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK') GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }
                                        }
                                        else
                                        {

                                            if (Convert.ToInt32(Session["usertype"]) == 1)
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }
                                            else
                                            {
                                                dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                            }


                                        }

                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(Session["usertype"]) == 1)
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }
                                        else
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }

                                    }
                                    // DataSet dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME", "FLDNAME+'-'+CAST(EXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND FLDNAME NOT IN('EXTERMARK')", "EXAMNO");
                                    MainSubExamBind(ddlExam, dsMainExamnew);
                                }
                            }
                            else
                            {
                                ddlSubExamName.SelectedIndex = 0;
                                divSubExamName.Visible = false;
                                ddlExam.SelectedIndex = 0;
                                gvStudent.DataSource = null;
                                gvStudent.DataBind();
                                ddlExam.Items.Clear();
                                ddlExam.Items.Add("Please Select");
                                ddlExam.SelectedItem.Value = "0";


                                if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                                {
                                    if (stu == "1")
                                    {
                                        if (Convert.ToInt32(Session["usertype"]) == 1)
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }
                                        else
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }
                                    }
                                    else
                                    {

                                        if (Convert.ToInt32(Session["usertype"]) == 1)
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }
                                        else
                                        {
                                            dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                        }


                                    }

                                }
                                else
                                {
                                    if (Convert.ToInt32(Session["usertype"]) == 1)
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }
                                    else
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }

                                }
                                //   DataSet dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME", "FLDNAME+'-'+CAST(EXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>''", "EXAMNO");
                                MainSubExamBind(ddlExam, dsMainExamnew);

                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updpnl, "Internal marks are not locked,kindky lock the data to enter the external marks for " + ddlCourse.SelectedItem.Text.ToString() + " !", this.Page);
                            ddlSubExamName.SelectedIndex = 0;
                            divSubExamName.Visible = false;
                            ddlExam.SelectedIndex = 0;
                            gvStudent.DataSource = null;
                            gvStudent.DataBind();
                            pnlStudGrid.Visible = false;
                            ddlExam.Items.Clear();
                            ddlExam.Items.Add("Please Select");
                            ddlExam.SelectedItem.Value = "0";
                            DataSet dsMainExamnew=null;


                            if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                            {
                                if (stu == "1")
                                {
                                    if (Convert.ToInt32(Session["usertype"]) == 1)
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK') GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }
                                    else
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }
                                }
                                else
                                {

                                    if (Convert.ToInt32(Session["usertype"]) == 1)
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }
                                    else
                                    {
                                        dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                    }


                                }

                            }
                            else
                            {
                                if (Convert.ToInt32(Session["usertype"]) == 1)
                                {
                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                }
                                else
                                {
                                    dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 AND EN.FLDNAME NOT IN('EXTERMARK')  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                }

                            }
                            //  DataSet dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME", " FLDNAME+'-'+CAST(EXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND FLDNAME NOT IN('EXTERMARK')", "EXAMNO");
                            MainSubExamBind(ddlExam, dsMainExamnew);
                            return;
                        }
                    }
                    else
                    {
                        ddlSubExamName.SelectedIndex = 0;
                        divSubExamName.Visible = false;
                        ddlExam.SelectedIndex = 0;
                        gvStudent.DataSource = null;
                        gvStudent.DataBind();
                        ddlExam.Items.Clear();
                        ddlExam.Items.Add("Please Select");
                        ddlExam.SelectedItem.Value = "0";
                         DataSet dsMainExamnew=null;

                        //INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO)
                        //INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=955 AND SESSIONNO=238 AND AC.UA_NO=1 AND ISNULL(CANCLE,0)=0)

                        ////DataSet dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME", "FLDNAME+'-'+CAST(EXAMNO AS NVARCHAR) AS FLDNAME", "EXAMNAME", "PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>''", "EXAMNO");

                         if (Convert.ToInt32(Session["OrgId"]) == 6) //Added by lalit regarding RCPIPER BACKLOG exam TICKET
                         {
                             if (stu == "1")
                             {
                                 if (Convert.ToInt32(Session["usertype"]) == 1)
                                 {
                                     dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1 GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                 }
                                 else
                                 {
                                     dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                 }
                             }
                             else
                             {

                                 if (Convert.ToInt32(Session["usertype"]) == 1)
                                 {
                                     dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1   GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                 }
                                 else
                                 {
                                     dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1   GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                                 }


                             }

                         }
                         else
                         {
                             if (Convert.ToInt32(Session["usertype"]) == 1)
                             {
                                 dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                             }
                             else
                             {
                                 dsMainExamnew = objCommon.FillDropDown("ACD_EXAM_NAME EN INNER JOIN ACD_SUBEXAM_NAME SN ON(SN.EXAMNO=EN.EXAMNO AND SN.PATTERNNO=EN.PATTERNNO) INNER JOIN ACD_ASSESSMENT_EXAM_COMPONENT AC ON(AC.SUBEXAMNO=SN.SUBEXAMNO AND AC.COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND AC.UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND ISNULL(CANCLE,0)=0)", " EN.FLDNAME+'-'+ CAST(EN.EXAMNO AS NVARCHAR)", "EXAMNAME", "EN.PATTERNNO=" + Convert.ToInt32(Session["Pattern"]) + " AND ISNULL(EXAMNAME,'')<>'' AND ISNULL(EN.ACTIVESTATUS,0)=1  GROUP BY EN.EXAMNO,EN.FLDNAME,EN.EXAMNAME  ", "EN.EXAMNO");
                             }

                         }
                        MainSubExamBind(ddlExam, dsMainExamnew);

                    }
                }

            }

    //added by prafull on dt 23092022

    protected void btnBlankDownld_Click(object sender, EventArgs e)
    {

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



            dsStudent = objMarksEntry.GetStudentsForPracticalCourseMarkEntry_Admin_IA(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ddlsemester.SelectedValue), MExamNo, Convert.ToInt32(ddlCourse.SelectedValue), (ddlSubExamName.SelectedValue.Split('-')[1]));

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
            string subExam_Name = (ddlSubExamName.SelectedValue);




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
            // examtype = "S";
            string SubExamComponentName = string.Empty;
            string examname = string.Empty;
            string Subexam = string.Empty;
            string[] Exam = ddlExam.SelectedValue.Split('-');
            if (Exam[0].StartsWith("S"))
                examtype = "S";
            else if (Exam[0].StartsWith("E"))
                examtype = "E";




            if (divSubExamName.Visible == true)
            {

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





            //return;
            if (!string.IsNullOrEmpty(studids))
            {

                if (examtype == "S")
                {
                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, SubExamComponentName, Convert.ToInt32(ddlsemester.SelectedValue), 0);
                }
                else
                {
                    cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Subexam, Convert.ToInt32(Exam[1]), SubExamComponentName);
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                return;
            }

            //if (examtype == "S")
            //{
            //    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNewAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, examname1, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, 0, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ddlsemester.SelectedValue), 0);
            //}
            //else
            //{
            //    cs = (CustomStatus)objMarksEntry.InsertMarkEntrybyAdmin_cc(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), ccode, studids, marks, lock_status, ddlExam.SelectedValue, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), subExam_Name, Convert.ToInt32(Exam[1]), SubExamComponentName);
            //}
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
    //    container.CreateIfNotExists();
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
    //        container.CreateIfNotExists();
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
    protected void btnExcelReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 8)
        {


            GridView GVStudData = new GridView();

            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));

            string SP_Name = "PKG_ACD_EXAM_COMPONENTWISE_MARK_ENTRY_CC";
            string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO,@P_SCHEMENO";
            //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["userno"]) + "";

            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ViewState["schemeno"]) + "";

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
                objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
            }
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            if (ddlStudenttype.SelectedValue == "1")
            {
                GridView GVStudData = new GridView();

                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));

                string SP_Name = "PKG_INTERNAL_EXTERNAL_MARK_GRADE_EXCEL_REPORT";
                string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SUBID,@P_CCODE,@P_SCHEMENO";
                //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["userno"]) + "";

                string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + ccode + "," + Convert.ToInt32(ViewState["schemeno"]) + "";

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
                    objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
                }
            }
            else
            {
                GridView GVStudData = new GridView();

                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));

                string SP_Name = "PKG_INTERNAL_EXTERNAL_MARK_GRADE_EXCEL_REPORT_BACKLOG";
                string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SUBID,@P_CCODE,@P_SCHEMENO";
                //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["userno"]) + "";

                string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + ccode + "," + Convert.ToInt32(ViewState["schemeno"]) + "";

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
                    objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
                }

            }


        }
        else
        {
            GridView GVStudData = new GridView();

            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue));

            string SP_Name = "PKG_INTERNAL_EXTERNAL_MARK_GRADE_EXCEL_REPORT";
            string SP_Parameters = "@P_SESSIONNO, @P_COURSENO, @P_SEMESTERNO, @P_SUBID,@P_CCODE,@P_SCHEMENO";
            //string Call_Values = "" + Convert.ToInt32(ddlsessionforabsent.SelectedValue) + "," + ddlcourseforabset.SelectedValue.Split('-')[0] + ",0," + ddlcourseforabset.SelectedValue.Split('-')[1] + "," + ddlcourseforabset.SelectedItem.Text.Split('-')[0] + ",0," + Examname + "," + Subexamname + ", " + Convert.ToInt32(Session["userno"]) + "";

            string Call_Values = "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlCourse.SelectedValue) + "," + Convert.ToInt32(ddlsemester.SelectedValue) + "," + Convert.ToInt32(ddlSubjectType.SelectedValue) + "," + ccode + "," + Convert.ToInt32(ViewState["schemeno"]) + "";

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
                objCommon.DisplayMessage(this.updpnl, "No Data Found", this.Page);
            }

        }
    }
    protected void btnReGrade_Click(object sender, EventArgs e)
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

                CustomStatus cs;
                if (Convert.ToInt32(Session["OrgId"]) == 8)
                {
                    cs = (CustomStatus)objMarksEntry.GradeGenaerationNew_Regenerate_MIT(studids, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue));
                }
                else
                {
                    cs = (CustomStatus)objMarksEntry.ReGradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
                }
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


                        CustomStatus cs = (CustomStatus)objMarksEntry.ReGradeGenaerationNew(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ViewState["schemeno"].ToString()));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
                            //btnReport.Enabled = true;
                           // ShowStudentsSpecialSubject();
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


    protected void btnEndSemReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            if (ddlStudenttype.SelectedValue == "1")
            {
                this.ShowReportEndSemMarkEntry("End Sem Mark Entry Report", "rpt_End_Sem_Mark_Entry_RCPIPER.rpt");
            }
            else
            {
                this.ShowReportEndSemMarkEntry("End Sem Mark Entry Report", "rpt_End_Sem_Mark_Entry_RCPIPER_Backlog.rpt");
            }
        }
        else
        {
            this.ShowReportEndSemMarkEntry("End Sem Mark Entry Report", "rpt_End_Sem_Mark_Entry_RCPIPER.rpt");

        }
    }


    //private void ShowReportEndSemMarkEntry(string reportTitle, string rptFileName)
    //{
    //    int UANO = 0;


    //    //Schemeno = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "DISTINCT SCHEMENO", "DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ddlbranch.SelectedValue)));
    //    UANO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT UA_NO", "SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));

    //    try
    //    {
    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
    //        // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;
    //        url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
    //              ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) +
    //             ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
    //             ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
    //            ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) +
    //           ",@P_UA_NO=" + UANO +
    //        ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) +
    //        ",@P_COLLEGE_CODE=" + Convert.ToInt32(ViewState["college_id"]);
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        ScriptManager.RegisterStartupScript(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}


    //added by tejas on dt 04042023
    private void ShowReportEndSemMarkEntry(string reportTitle, string rptFileName)
    {

        try
        {
            int UANO = 0;
            //int UA_NO_PRAC = 0;

            if (ddlSubjectType.SelectedValue == "2")
            {
                UANO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT UA_NO_PRAC", "SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            }
            else
            {
                UANO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT UA_NO", "SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + "AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue)));
            }

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            // string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) +
                  ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) +
                 ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) +
                 ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) +
                ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) +
               ",@P_UA_NO=" + UANO +
            ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterStartupScript(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {

        }
    }

}

