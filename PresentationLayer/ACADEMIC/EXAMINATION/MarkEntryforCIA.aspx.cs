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


public partial class ACADEMIC_EXAMINATION_MarkEntryforCIA : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();

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


                //string colgno = Session["college_nos"].ToString();
                //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT TOP 2 S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND C.COLLEGE_ID IN(" + colgno + ")", "SESSIONNO DESC");


                //objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "COLLEGE_NAME");

                // NEW LOGIC ADDED AS PER REQUIREMENT BY NARESH BEERLA ON 05032022
                string colgno = Session["college_nos"].ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN(SELECT DISTINCT SESSIONNO FROM ACD_COURSE_TEACHER WHERE UA_NO=" + Session["userno"].ToString() + " AND ISNULL(CANCEL,0)=0)", "SESSIONNO DESC");

                // NEW LOGIC ADDED AS PER REQUIREMENT BY NARESH BEERLA ON 05032022

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
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void ShowCourses()
    {
        DataSet ds = objMarksEntry.GetCourseForTeacher(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));

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


                //// Check Mark Enrty Activitity -- Added by Abhinay Lad [14-09-2019]
                //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[2]) + " )", "SESSIONNO DESC");

                //if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                //{
                //    objCommon.DisplayMessage(this.updpanle1, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                //    return;
                //}

                ////End

                // Added on 15/03/2022 by Sneha G.
                string semesterno = (lnk.Parent.FindControl("hdnsem") as HiddenField).Value;
                string branchno = (lnk.Parent.FindControl("hdnBranchno") as HiddenField).Value;

                // Check Mark Enrty Activitity -- Added by Abhinay Lad [14-09-2019]
                DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND SEMESTER LIKE '%" + semesterno + "%' AND BRANCH LIKE '%" + branchno + "%' AND EXAMNO=" + Convert.ToInt32(lnk.CommandArgument.ToString().Split('+')[2]) + " )", "SESSIONNO DESC");

                if (ds_CheckActivity.Tables[0].Rows.Count == 0)
                {
                    objCommon.DisplayMessage(this, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    return;
                }

                //END

                hdfSection.Value = sec_batch[0].ToString();
                ddlSession2.Items.Clear();
                ddlSession2.Items.Add(new ListItem(ddlSession.SelectedItem.Text, ddlSession.SelectedItem.Value));
                hdfBatch.Value = sec_batch.Length == 2 ? sec_batch[1].ToString() : "0";


                int CourseNo = 0;
                LinkButton btn = sender as LinkButton;
                CourseNo = Convert.ToInt32((btn.Parent.FindControl("hdnfld_courseno") as HiddenField).Value);
                ViewState["sem"] = Convert.ToInt32((btn.Parent.FindControl("hdnsem") as HiddenField).Value);

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

                    //DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(Request.QueryString["pageno"].ToString()));
                    DataSet dsExams = objCommon.FillDropDown("ACD_EXAM_NAME", "DISTINCT EXAMNO AS EXAMNO,FLDNAME,EXAMNAME", "CONCAT(FLDNAME,'-',EXAMNO) AS FLDNAME2 ", "EXAMNO=" + Convert.ToInt32(sec_batch[2].ToString()) + " AND ISNULL(ACTIVESTATUS,0)=1", "EXAMNO");
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
                                    //if (ddlSubjectType.SelectedIndex > 0)
                                    //{
                                    ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME2"].ToString()));
                                    //}
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
                    //}
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
        btnShow.Enabled = true;
        GetStatus();
    }

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
            string API_Output = "";
            string examtype = string.Empty;
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

                //if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                //    examname = ddlExam.SelectedValue.Substring(2);
                //else if (ddlExam.SelectedIndex > 0)
                //    examname = ddlExam.SelectedValue;

                CustomStatus cs;
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




                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, to_email, from_email, smsmobile, 1, sms_text, email_text, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));
                }
                else
                {
                    cs = (CustomStatus)objMarksEntry.UpdateMarkEntryNew(Convert.ToInt32(ddlSession.SelectedValue), courseNo, ccode, studids.Remove(studids.Length - 1, 1), marks.Remove(marks.Length - 1, 1), lock_status, examname, Convert.ToInt16(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype, FlagReval, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, subExam_Name, Convert.ToInt32(ViewState["SemesterNo"]), Convert.ToInt32(hdfSection.Value));

                }


                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(this.updpanle1, "Marks Locked Successfully!!!", this.Page);
                        objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
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
        if (ddlSubjectType.SelectedIndex > 0)
        {
            this.ShowCourses();
            this.GetStatus();
        }
        else
        {
            lvCourse.DataSource = null;
            lvCourse.DataBind();
            lvCourse.Visible = false;
            rptExamName.DataSource = null;
            rptExamName.DataBind();
            lvCourse.Visible = false;
            Div_ExamNameList.Visible = false;
        }
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
            string[] course = lblCourse.Text.Split('-');
            DataSet dsStudent = null;
            DataSet ds = objCommon.FillDropDown("ACAD_EXAM_RULE", "ISNULL(RULE1,0) AS RULE1", "ISNULL(RULE2,0) AS RULE2", "EXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=(select schemeno from acd_course where courseno=" + Convert.ToInt32(ViewState["COURSENO"]) + ") AND COURSENO=" + Convert.ToInt32(ViewState["COURSENO"]) + " AND SEMESTERNO=" + Convert.ToInt16(ViewState["sem"]) + "", "");

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

            dsStudent = objMarksEntry.GetStudentsForMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["CCODE"].ToString(), Convert.ToInt16(hdfSection.Value), Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt16(ViewState["sem"]), Convert.ToString(ddlExam.SelectedValue).Split('-')[0], Convert.ToInt32(ViewState["COURSENO"]), Convert.ToString(ddlSubExam.SelectedValue));
            int lockcount = 0;
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ViewState["SemesterNo"] = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SEMESTERNO"]);
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    if (ddlExam.SelectedValue.Contains("EXTERMARK"))
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
        DataSet ds = objMarksEntry.GetCourse_MarksEntryStatus(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(Session["userno"]), Convert.ToInt16(ddlSubjectType.SelectedValue));
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //GVEntryStatus.DataSource = ds;
            //GVEntryStatus.DataBind();
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
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString() + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "SUBEXAMNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExam, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString() + " AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + "", "SUBEXAMNO"); // TOP(1)  removed top 1 as per issue of not showing the subexams by naresh beerla on dt 24022022
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

        if (ddlSubExam.SelectedIndex > 0)
        {
            // Check Mark Enrty Activitity -- Added by Abhinay Lad [14-09-2019]
            //DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND COLLEGE_IDS IN (" + Session["college_nos"].ToString() + ") AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + ")", "SESSIONNO DESC");

            DataSet ds_CheckActivity = objCommon.FillDropDown("ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN (SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SESSIONNO IN (" + ddlSession.SelectedValue + ") AND SHOW_STATUS =1 AND ISNULL(ACTIVESTATUS,0)=1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' AND PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%' AND EXAMNO=" + Convert.ToString(ddlExam.SelectedValue).Split('-')[1] + ")", "SESSIONNO DESC");


            // AND SUBEXAMNO=" + Convert.ToString(ddlSubExam.SelectedValue).Split('-')[1] + " 
            // AND COLLEGE_IDS IN '%" + Session["college_nos"].ToString() + "%' AND
            if (ds_CheckActivity.Tables[0].Rows.Count == 0)
            {
                ddlSubExam.SelectedIndex = 0;
                btnShow.Enabled = false;
                lblStudents.Visible = false;
                pnlStudGrid.Visible = false;
                gvStudent.DataSource = null;
                gvStudent.DataBind();
                objCommon.DisplayMessage(this.updpanle1, "The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                return;
            }
            else
            {
                btnShow.Enabled = true;


            }

            //End
        }
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
            objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE S INNER JOIN ACD_STUDENT_RESULT R ON(R.SUBID=S.SUBID)", "DISTINCT R.SUBID", "SUBNAME", "S.SUBID > 0 AND (UA_NO=" + Convert.ToInt32(Session["userno"].ToString()) + " OR UA_NO_PRAC=" + Convert.ToInt32(Session["userno"].ToString()) + ") AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "", "");
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
        lvCourse.Visible = false;
        //  this.ShowCourses();
        //  this.GetStatus();
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
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString() + " AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "SUBEXAMNO");
                ddlSubExamPrint.Enabled = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlSubExamPrint, "ACD_SUBEXAM_NAME", "CAST(FLDNAME AS VARCHAR)+'-'+CAST(SUBEXAMNO AS VARCHAR) AS SUBEXAMNO", "SUBEXAMNAME", "ISNULL(ACTIVESTATUS,0)=1 AND SUBEXAM_SUBID=" + Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString() + " AND EXAMNO=" + Convert.ToString(ddlExamPrint.SelectedValue) + "", "SUBEXAMNO");
                // TOP(1)  removed top 1 as per issue of not showing the subexams by naresh beerla on dt 24022022
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

    //protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcollege.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT TOP 2 SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND COLLEGE_ID="+ Convert.ToInt32(ddlcollege.SelectedValue)+"", "SESSIONNO DESC");
    //        ddlSession.Focus();
    //    }
    //    else
    //    {
    //        ddlcollege.Focus();
    //        ddlcollege.SelectedIndex = 0;
    //        objCommon.DisplayMessage(this, "Please Select School/College", this.Page);
    //        return;
    //    }
    //}
}