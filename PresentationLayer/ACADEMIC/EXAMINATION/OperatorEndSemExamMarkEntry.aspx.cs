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

public partial class OperatorEndSemExamMarkEntry : System.Web.UI.Page
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
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    btnUnlock.Visible = false;

                }
                else
                {
                    btnUnlock.Visible = false;
                }

                if (Request.QueryString["pageno"] != null)
                    //  objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO desc");
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");

                objCommon.FillDropDownList(ddlSubjectType, "ACD_SUBJECTTYPE", "SUBID", "SUBNAME", "", "SUBID");
                ddlSession.SelectedIndex = 1;
                objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                if (ddlSession.SelectedValue == "0")
                {
                    objCommon.DisplayMessage("The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                    pnlMarkEntry.Visible = false;
                }

            }
        }
        divMsg.InnerHtml = string.Empty;
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
                string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);  /////changess

                if (ddlExam.SelectedValue.StartsWith("S"))
                    examtype = "S";
                else if (ddlExam.SelectedValue.StartsWith("E"))
                    examtype = "E";
                string examname = string.Empty;
                if (ddlExam.SelectedValue.Length > 2 && ddlExam.SelectedIndex > 0)
                    examname = ddlExam.SelectedValue.Substring(2);
                else if (ddlExam.SelectedIndex > 0)
                    examname = ddlExam.SelectedValue;

                CustomStatus cs = (CustomStatus)objMarksEntry.UpdateMarkEntry(Convert.ToInt32(ddlSession.SelectedValue), ccode, studids, marks, lock_status, examname, Convert.ToInt32(ddlSubjectType.SelectedValue), Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), examtype);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                    {
                        objCommon.DisplayMessage(updpnl, "Marks Locked Successfully!!!", this.Page);
                        //objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    }
                    else if (lock_status == 2)
                    {
                        objCommon.DisplayMessage(updpnl, "Marks Unlocked Successfully!!!", this.Page);
                    }
                    else
                        objCommon.DisplayMessage(updpnl, "Marks Saved Successfully. Please Click on Lock button to Final Submit the Marks", this.Page);
                    //objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);

                    btnReport.Enabled = true;
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

            for (int j = 1; j < gvStudent.Columns.Count; j++)    //columns
            {
                for (int i = 0; i < gvStudent.Rows.Count; i++)   //rows 
                {
                    if (gvStudent.Columns[j].Visible == true)
                    {
                        if (j == 1) //TA MARKS
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
                                        objCommon.DisplayMessage(updpnl, "Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]", this.Page);
                                        //ShowMessage("Marks Entered [" + txt.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                                        txt.Focus();
                                        flag = false;
                                        break;
                                    }
                                    else if (Convert.ToDouble(txt.Text) < 0)
                                    {
                                        //Note : 401 for Absent and Not Eligible
                                        if (Convert.ToDouble(txt.Text) == -1 || Convert.ToDouble(txt.Text) == -2 || Convert.ToDouble(txt.Text) == -3 || Convert.ToDouble(txt.Text) == -4)
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
    #endregion

    protected void btnLock_Click(object sender, EventArgs e)
    {
        //1 - means lock marks
        SaveAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        Clear();
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        ddlSubjectType.SelectedIndex = 0;
        ddlCourse.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
    }

    private void Clear()
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        lblStudents.Text = string.Empty;
        btnSave.Enabled = false;
        btnLock.Enabled = false;
        btnReport.Visible = false;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (ddlSubjectType.SelectedIndex > 0)
            {
                if (ddlCourse.SelectedIndex > 0)
                {
                    if (ddlExam.SelectedIndex > 0)
                    {
                        this.ShowReportMarksEntry("MarksListReport", "rptMarksList1_OPERATOR_NEW_ADMIN.rpt");//rptMarksList1.rpt
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnl, "Please Select Exam!", this.Page);
                        ddlExam.Focus();
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
        string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + ",@P_IDNO=" + idno.ToString();
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UA_NO=0,@P_CCODE=" + ccode + ",@P_SECTIONNO=0,@P_SUBID=" + Convert.ToInt32(ddlSubjectType.SelectedValue) + ",@P_EXAM=" + ddlExam.SelectedValue.ToString() + ",@P_SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + ",@P_USERNAME=" + Session["userfullname"].ToString();
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
        ddlcollege.Items.Clear();
        //ddlcollege.Items.Add(new ListItem("Please Select", "0"));
        //ddldegree.Items.Clear();
        ddldegree.Items.Add(new ListItem("Please Select", "0"));
        ddlbranch.Items.Clear();
        ddlbranch.Items.Add(new ListItem("Please Select", "0"));

        ddlsemester.Items.Clear();
        ddlsemester.Items.Add(new ListItem("Please Select", "0"));

        ddlscheme.Items.Clear();
        ddlscheme.Items.Add(new ListItem("Please Select", "0"));

        ddlSubjectType.Items.Clear();
        ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));

        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        ddlExam.Items.Clear();
        ddlExam.Items.Add(new ListItem("Please Select", "0"));
        Clear();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));
        string exams = string.Empty;
        if (dsExams != null && dsExams.Tables.Count > 0)
        {
            if (dsExams.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    if (Convert.ToInt32(Session["usertype"]) == 3)
                    {
                        if (dtr["FLDNAME"].ToString() == "EXTERMARK")
                        {
                            exams += dtr["FLDNAME"] == DBNull.Value ? string.Empty : dtr["FLDNAME"].ToString() + ",";
                        }
                    }
                    if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7 || Convert.ToInt32(Session["usertype"]) == 8)
                    {
                        exams += dtr["FLDNAME"] == DBNull.Value ? string.Empty : dtr["FLDNAME"].ToString() + ",";
                    }
                }
                dtr.Close();
            }
            else
                objCommon.DisplayMessage("Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);
        }
        else
            ////objCommon.DisplayMessage("Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);
            objCommon.DisplayMessage(updpnl, "Exam for the Selected Course may not be Started Or may be Locked!!!", this.Page);

        //If any exams are present then proceed
        if (exams.Length > 0)
        {
            //Store exams to viewstate to access later
            ViewState["exams"] = exams.Split(','); //store arrat
            ViewState["exam"] = exams;

            ddlExam.Items.Clear();
            ddlExam.Items.Add(new ListItem("Select Exam", "0"));

            DataTableReader dtr = dsExams.Tables[0].CreateDataReader();
            while (dtr.Read())
            {
                if (dtr["FLDNAME"] != DBNull.Value)
                {
                    #region COMMENTED CODE

                    #endregion

                    if (ddlSubjectType.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(Session["usertype"]) == 3)
                        {
                            if (dtr["FLDNAME"].ToString() == "EXTERMARK")
                            {
                                ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME"].ToString()));
                            }
                        }
                        if (Convert.ToInt32(Session["usertype"]) == 1 || Convert.ToInt32(Session["usertype"]) == 7 || Convert.ToInt32(Session["usertype"]) == 8)
                        {
                            ddlExam.Items.Add(new ListItem(dtr["EXAMNAME"].ToString(), dtr["FLDNAME"].ToString()));
                        }
                    }
                    else
                    {

                        ddlExam.Items.Clear();
                        ddlExam.Items.Add(new ListItem("Please Select", "0"));
                    }
                    Clear();
                }
            }
            dtr.Close();
        }
    }

    //protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Clear();
    //}

    protected void ddlSubjectType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSubjectType.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_STUDENT_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(SR.CCODE + ' - ' + SR.COURSENAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlscheme.SelectedValue + " AND SR.SUBID = " + ddlSubjectType.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
            ddlCourse.Focus();
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));
            ddlExam.Items.Clear();
            ddlExam.Items.Add(new ListItem("Please Select", "0"));
        }
        Clear();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlExam.SelectedIndex > 0)
        {
            ShowStudents();
            //  Page.ClientScript.RegisterStartupScript(this.GetType(), "DynamicMenuItem", "DynamicMenuItem();", true);
            //Page.ScriptManager.RegisterClientScriptBlock(this.GetType(), "FreezeGridHeader", "FreezeGridHeader();", true);
            
        }

    }

    private void ShowStudents()
    {
        try
        {
            string[] course = ddlCourse.SelectedItem.Text.Split('-');
            DataSet dsStudent = null;
            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
            dsStudent = objMarksEntry.GetStudentsForMarkEntryadmin_Operator(Convert.ToInt32(ddlSession.SelectedValue), 0, ccode, 0, Convert.ToInt32(ddlSubjectType.SelectedValue), ddlExam.SelectedValue.ToString(), Convert.ToInt32(ddlscheme.SelectedValue), Session["userfullname"].ToString());
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    ////HIDE STUDENT NAME COLUMN IF MARK ENTRY IS FROM EMDSEM
                    //if (ddlExam.SelectedValue == "EXTERMARK")
                    //{
                    //    gvStudent.Columns[2].Visible = true;
                    //}
                    //else
                    //{
                    //    gvStudent.Columns[2].Visible = false;
                    //}
                    gvStudent.Columns[2].Visible = false; // No Need this column Modify on Dated 16/12/2019 

                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        gvStudent.Columns[4].HeaderText = ddlExam.SelectedItem.Text + " " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]" + " " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";
                        gvStudent.Columns[4].Visible = true;

                        ViewState["maxmarks"] = dsStudent.Tables[0].Rows[0]["SMAX"];
                        ViewState["minmarks"] = dsStudent.Tables[0].Rows[0]["SMIN"];
                        ViewState["LockStatus"] = dsStudent.Tables[0].Rows[0]["LOCK"];
                    }
                    else
                        gvStudent.Columns[4].Visible = false;
                    lblStudents.Text = "Total Students : " + dsStudent.Tables[0].Rows.Count.ToString();

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();

                    //for (int i = 0; i < gvStudent.Rows.Count; i++)
                    //{
                    //    TextBox txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    //    Label lblMarks = gvStudent.Rows[i].FindControl("lblMarks") as Label;
                    //    if (lblMarks.ToolTip == "True")
                    //    {
                    //        txtMarks.Enabled = false;
                    //    }
                    //}

                    //Check for All Exams On or Off
                    //if (CheckExamON() == false)
                    //{
                    //    btnSave.Enabled = false;
                    //    btnLock.Enabled = false;
                    //    objCommon.DisplayMessage("Selected Exam Not Applicable for Course!!", this.Page);
                    //}
                    //else
                    //{
                    btnSave.Enabled = true;
                    btnLock.Enabled = true;
                    btnUnlock.Enabled = false;
                    //}

                    if (dsStudent.Tables[0].Rows[0]["LOCK"].ToString() == "True")
                    {
                        btnSave.Enabled = false;
                        btnLock.Enabled = false;
                        btnUnlock.Enabled = false;
                    }
                    pnlStudGrid.Visible = true;
                    btnSave.Visible = true;
                    btnLock.Visible = true;
                    btnReport.Visible = true;
                    btnReport.Enabled = true;
                    lblStudents.Visible = true;

                    ClientScript.RegisterStartupScript(this.GetType(), "FreezeGridHeader", "FreezeGridHeader();", true);
                }
                else
                {
                    objCommon.DisplayMessage(updpnl, "Students Not Found.Decode Number Is Not Generated!!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Students Not Found.Decode Number Is Not Generated!!", this.Page);
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
                objCommon.FillDropDownList(ddldegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlcollege.SelectedValue + "", "A.degreeno");
                ddldegree.Focus();
            }
            else
            {
                ddldegree.Items.Clear();
                ddldegree.Items.Add(new ListItem("Please Select", "0"));
                ddlbranch.Items.Clear();
                ddlbranch.Items.Add(new ListItem("Please Select", "0"));

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlscheme.Items.Clear();
                ddlscheme.Items.Add(new ListItem("Please Select", "0"));

                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));

            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue), "LONGNAME");
                ddlbranch.Focus();
            }
            else
            {

                ddlbranch.Items.Clear();
                ddlbranch.Items.Add(new ListItem("Please Select", "0"));

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlscheme.Items.Clear();
                ddlscheme.Items.Add(new ListItem("Please Select", "0"));

                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlbranch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlscheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddldegree.SelectedValue + " and BRANCHNO = " + ddlbranch.SelectedValue, "SCHEMENO DESC");
            }
            else
            {
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlscheme.Items.Clear();
                ddlscheme.Items.Add(new ListItem("Please Select", "0"));

                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }

    }
    protected void ddlscheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlscheme.SelectedIndex > 0)
            {
                //objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT SR, ACD_SEMESTER S", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "  SR.COLLEGEID = " + ddlCollege.SelectedValue + " AND SR.SEMESTERNO=S.SEMESTERNO AND S.SEMESTERNO > 0", "S.SEMESTERNO");
                objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue), "S.SEMESTERNO");
            }
            else
            {
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ReEvaluationAndScrutiny.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlsemester.SelectedIndex > 0)
            {

                objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlscheme.SelectedValue, "C.SUBID");
                //objCommon.FillDropDownList(ddlSubjectType, "ACD_COURSE C INNER JOIN ACD_SCHEME M ON (C.SCHEMENO = M.SCHEMENO) INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.SUBID", "S.SUBNAME", "C.SCHEMENO = " + ddlScheme.SelectedValue", "C.SUBID");
                ddlSubjectType.Focus();
            }
            else
            {
                ddlSubjectType.Items.Clear();
                ddlSubjectType.Items.Add(new ListItem("Please Select", "0"));
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                ddlExam.Items.Clear();
                ddlExam.Items.Add(new ListItem("Please Select", "0"));
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_courseAllot.ddlSem_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}