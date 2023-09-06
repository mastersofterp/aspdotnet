//======================================================================================
// PROJECT NAME  : RFCAMPUS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : REVALUATION MARKS CHANGE 
// CREATION DATE : 16-06-2015
// ADDED BY      : MR.MANISH WALDE
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

public partial class ACADEMIC_EXAMINATION_RevalMarkChange : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    Exam objExam = new Exam();
    ActivityController objActController = new ActivityController();

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
                this.CheckPageAuthorization();

                //Set the Page Title
                this.Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDDL();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearStudGrid();
        btnShow.Focus();
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;

        btnLock.Visible = false;
        btnUnlock.Visible = false;
        btnSave.Visible = false;

        if (ddlCourse.SelectedIndex <= 0)
        {
            ddlCourse.SelectedIndex = 0;
            ClearStudGrid();
        }
        else
        {
            ddlExam.Focus();
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;

        if (ddlScheme.SelectedIndex <= 0)
        {
            ddlScheme.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ClearStudGrid();
        }
        else
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER SE INNER JOIN ACD_REVAL_RESULT REV ON (SE.SEMESTERNO = REV.SEMESTERNO)", "DISTINCT SE.SEMESTERNO", "SE.SEMESTERNAME", "SE.SEMESTERNO > 0 AND ISNULL(REV.REV_APPROVE_STAT,0)=1 AND ISNULL(REV.CANCEL,0)=0 AND REV.SESSIONNO=" + ddlSession.SelectedValue + " AND REV.SCHEMENO=" + ddlScheme.SelectedValue, "SE.SEMESTERNO");
            objCommon.FillDropDownList(ddlExam, "ACD_EXAM_NAME E INNER JOIN ACD_SCHEME S ON (E.PATTERNNO = S.PATTERNNO)", "EXAMNO", "EXAMNAME", "FLDNAME='EXTERMARK' AND S.SCHEMENO=" + ddlScheme.SelectedValue, "EXAMNAME");
            ddlExam.SelectedIndex = 1;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        CheckActivityandShow();
    }

    //protected void ddlValuer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    CheckActivityandShow();
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveMarksAndLock(0);
    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        SaveMarksAndLock(1);
    }

    protected void btnCancel2_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

       /// ResetPage();
    }

    #region Private Methods

    private void PopulateDDL()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = '" + Request.QueryString["pageno"].ToString() + "')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 0;

        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC INNER JOIN ACD_REVAL_RESULT REV ON (SC.SCHEMENO = REV.SCHEMENO)", "DISTINCT SC.SCHEMENO", "SC.SCHEMENAME", "ISNULL(REV.REV_APPROVE_STAT,0)=1 AND ISNULL(REV.CANCEL,0)=0 AND SC.SCHEMENO > 0 AND REV.SESSIONNO=" + ddlSession.SelectedValue, "SC.SCHEMENO DESC");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntry_Admin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntry_Admin.aspx");
        }
    }
    private void CheckActivityandShow()
    {
        if (CheckActivity() == true)
        {
            ShowStudents();
        }
        else
            ShowMessage("Exam for the Selected Course may not be Started or may be Locked.");

        //rblRevalStatus.Enabled = false;
    }

    private void ShowStudents()
    {
        try
        {
            int lockv = 0;
            //int lockV2 = 0;
            string parameter = string.Empty;
            string[] course;
            course = ddlCourse.SelectedItem.ToString().Split('~');

            //DataSet dsStudent = objMarksEntry.GetStudentsForRevalMarksEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), course[0], ddlExam.SelectedValue, rblRevalStatus.SelectedItem.Value, ddlValuer.SelectedValue);
            DataSet dsStudent = objMarksEntry.GetStudentsForRevalMarksEntry(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), course[0], ddlExam.SelectedValue);//MODIFIED BY SRIKANTH 

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
                    {
                        gvStudent.Columns[4].HeaderText = "Valuer Marks" + " <br/> <br/> " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]"; // +" <br/><br/> " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";
                        //gvStudent.Columns[5].HeaderText = "Valuer2 Marks" + " <br/> <br/> " + "[Max : " + dsStudent.Tables[0].Rows[0]["SMAX"].ToString() + "]"; // +" <br/><br/> " + "[Min : " + dsStudent.Tables[0].Rows[0]["SMIN"].ToString() + "]";

                        gvStudent.Columns[3].Visible = true;
                        gvStudent.Columns[4].Visible = true;

                        ////Show Valuer 2 Only for Revaluation
                        //if (rblRevalStatus.SelectedValue == "0")
                        //{
                        //    gvStudent.Columns[5].Visible = true;
                        //}

                        lockv = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["lockv"]);
                        //lockV2 = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["LOCKV2"]);
                    }
                    else
                    {
                        gvStudent.Columns[3].Visible = false;
                        gvStudent.Columns[4].Visible = false;
                        //gvStudent.Columns[5].Visible = false;
                    }

                    //Bind the Student List
                    gvStudent.DataSource = dsStudent;
                    gvStudent.DataBind();
                    pnlStudGrid.Visible = true;

                    //Check for All Exams On or Off
                    if (CheckExamON() == false)
                    {
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnUnlock.Visible = false;
                        objCommon.DisplayMessage("Selected Exam Not Applicable for Course.", this.Page);
                        return;
                    }

                    if (lockv == 1)
                    {
                        btnSave.Visible = false;
                        btnLock.Visible = false;
                        btnUnlock.Visible = true;

                        for (int i = 0; i < gvStudent.Rows.Count; i++)
                        {
                            TextBox txtVMrk = gvStudent.Rows[i].FindControl("txtVMrk") as TextBox;
                            //TextBox txtV2Mrk = gvStudent.Rows[i].FindControl("txtV2Mrk") as TextBox;
                            txtVMrk.Enabled = false;
                           
                            //txtV2Mrk.Enabled = false;

                            //HiddenField hdnMarkDiffPer = gvStudent.Rows[i].FindControl("hdnMarkDiffPer") as HiddenField;

                            //if (Convert.ToDouble(hdnMarkDiffPer.Value) >= 20.00)
                            //{
                            //    txtV2Mrk.Visible = true;
                            //}
                            //else
                            //{
                            //    txtV2Mrk.Visible = false;
                            //}
                        }
                    }
                    else
                    {
                        btnSave.Visible = true;
                        btnLock.Visible = true;
                        btnUnlock.Visible = false;

                        ////Enable Valuer 2 Only for Revaluation
                        //if (select == 0)
                        //{
                        //    for (int i = 0; i < gvStudent.Rows.Count; i++)
                        //    {
                        //        HiddenField hdnMarkDiffPer = gvStudent.Rows[i].FindControl("hdnMarkDiffPer") as HiddenField;
                        //        TextBox txtV2Mrk = gvStudent.Rows[i].FindControl("txtV2Mrk") as TextBox;

                        //        if (Convert.ToDouble(hdnMarkDiffPer.Value) >= 20.00)
                        //        {
                        //            txtV2Mrk.Visible = true;
                        //            txtV2Mrk.Enabled = true;
                        //        }
                        //        else
                        //        {
                        //            txtV2Mrk.Visible = false;
                        //            txtV2Mrk.Enabled = false;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    objCommon.DisplayMessage("No Students for Selected Course & Exam !!", this.Page);
                    pnlStudGrid.Visible = false;
                    btnSave.Visible = false;
                    btnLock.Visible = false;
                    btnUnlock.Visible = false;
                    gvStudent.DataSource = null;
                    gvStudent.DataBind();
                }
            }
            else
            {
                objCommon.DisplayMessage("No Students for Selected Course & Exam !!", this.Page);
                pnlStudGrid.Visible = false;
                btnSave.Visible = false;
                btnLock.Visible = false;
                btnUnlock.Visible = false;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.ShowStudentsForRevalEntry --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckExamON()
    {
        bool flag = true;
        if (gvStudent.Columns[4].Visible == true) return flag;
        return false;
    }

    private void ClearStudGrid()
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        btnSave.Visible = false;
        btnLock.Visible = false;
        btnUnlock.Visible = false;
    }

    private void ResetPage()
    {
        ClearStudGrid();
        ddlScheme.SelectedIndex = -1;
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add("Please Select");
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add("Please Select");
      //  rblRevalStatus.Enabled = true;
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    private bool CheckMarks(int lock_status)
    {
        bool flag = true;
        try
        {
            Label lbl;
            TextBox txtV;
            //TextBox txtV2;
            string marks = string.Empty;
            string maxMarks = string.Empty;

            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                lbl = gvStudent.Rows[i].FindControl("lblMarks") as Label;      //Max Marks 
                txtV = gvStudent.Rows[i].FindControl("txtVMrk") as TextBox;    //Valuer Marks Entered 
                //txtV2 = gvStudent.Rows[i].FindControl("txtV2Mrk") as TextBox;    //valuer2 Marks Entered 

                maxMarks = lbl.Text.Trim();
                ////VALIDATION FOR Valuer
                if (!txtV.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txtV.Enabled == true)
                {
                    if (txtV.Text.Trim() == "")
                    {
                        if (lock_status == 1)
                        {
                            ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                            txtV.Focus();
                            flag = false;
                            break;
                        }
                    }
                    else
                    {
                        //Check for Marks entered greater than Max Marks
                        if (Convert.ToDouble(txtV.Text) > Convert.ToDouble(lbl.Text))
                        {
                            ShowMessage("Marks Entered [" + txtV.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                            txtV.Focus();
                            flag = false;
                            break;
                        }
                        else if (Convert.ToDouble(txtV.Text) < 0)
                        {
                            //Note : -1 for Absent and -2 Not Eligible
                            if (Convert.ToDouble(txtV.Text) != -1 || Convert.ToDouble(txtV.Text) != -2)
                            {
                                ShowMessage("Marks Entered [" + txtV.Text + "] cant be Less 0 (zero). Only -1 and -2 are allowed.");
                                txtV.Focus();
                                flag = false;
                                break;
                            }
                        }
                    }
                }

                //////VALIDATION FOR Valuer 2
                //if (!txtV2.Text.Trim().Equals(string.Empty) && !lbl.Text.Trim().Equals(string.Empty) || txtV2.Enabled == true)
                //{
                //    if (txtV2.Text.Trim() == "")
                //    {
                //        if (lock_status == 1)
                //        {
                //            ShowMessage("Marks Entry Not Completed!! Please Enter the Marks for all Students.");
                //            txtV2.Focus();
                //            flag = false;
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        //Check for Marks entered greater than Max Marks
                //        if (Convert.ToDouble(txtV2.Text) > Convert.ToDouble(lbl.Text))
                //        {
                //            ShowMessage("Marks Entered [" + txtV2.Text + "] cant be Greater than Max Marks[" + lbl.Text + "]");
                //            txtV2.Focus();
                //            flag = false;
                //            break;
                //        }
                //        else if (Convert.ToDouble(txtV2.Text) < 0)
                //        {
                //            //Note : -1 for Absent and -2 Not Eligible
                //            if (Convert.ToDouble(txtV2.Text) != -1 || Convert.ToDouble(txtV2.Text) != -2)
                //            {
                //                ShowMessage("Marks Entered [" + txtV2.Text + "] cant be Less 0 (zero). Only -1 and -2 are allowed.");
                //                txtV2.Focus();
                //                flag = false;
                //                break;
                //            }
                //        }
                //    }
                //}

                if (flag == false) break;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

            flag = false;
        }
        return flag;
    }

    private void SaveMarksAndLock(int lock_status)
    {
        try
        {
            //int select = Convert.ToInt32(rblRevalStatus.SelectedItem.Value);
            //check for if any exams on
            if (ddlExam.SelectedIndex > 0)
            {
                string examtype = string.Empty;
                string studids = string.Empty;
                string oldmarks = string.Empty;
                string vMarks = string.Empty;
                //string v2Marks = string.Empty;
                int count = 0;
                //Check for lock and null marks
                if (CheckMarks(lock_status) == false)
                {
                    return;
                }

                MarksEntryController objMarksEntry = new MarksEntryController();
                Label lbl;
                Label lblOldMarks;
                //TextBox txtMarks;
                TextBox txtVMrk;
                //TextBox txtV2Mrk;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //Note : -100 for Marks will be converted as NULL           
                //NULL means mark entry not done.                           
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < gvStudent.Rows.Count; i++)
                {
                    txtVMrk = gvStudent.Rows[i].FindControl("txtVMrk") as TextBox;
                    //txtV2Mrk = gvStudent.Rows[i].FindControl("txtV2Mrk") as TextBox;

                    if (txtVMrk.Text != "")
                    {
                        //Student IDs 
                        lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                        studids += lbl.ToolTip + ",";

                        //Exam Marks 
                        lblOldMarks = gvStudent.Rows[i].FindControl("lblOldMarks") as Label;
                        oldmarks += lblOldMarks.Text.Trim() == string.Empty ? "-100," : lblOldMarks.Text.Trim() + ",";

                        //New Marks
                        vMarks += txtVMrk.Text.Trim() == string.Empty ? "-100," : txtVMrk.Text.Trim() + ",";
                        //v2Marks += txtV2Mrk.Text.Trim() == string.Empty ? "-100," : txtV2Mrk.Text.Trim() + ",";
                        count++;
                    }

                    
                }
                if (count == 0)
                {
                    ShowMessage("Please enter marks.");
                    return;
                }
                else
                {
                    oldmarks = oldmarks.TrimEnd(',');
                    studids = studids.TrimEnd(',');
                    vMarks = vMarks.TrimEnd(',');
                    //v2Marks = v2Marks.TrimEnd(',');
                }

                int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
                string[] course = ddlCourse.SelectedItem.Text.Split('-');
                string ccode = course[0].Trim();


                //if (ddlExam.SelectedValue.StartsWith("S"))
                //    examtype = "S";
                //else if (ddlExam.SelectedValue.StartsWith("E"))
                //    examtype = "E";

                //CustomStatus cs = (CustomStatus)objMarksEntry.ChangeRevalMarks(Convert.ToInt32(ddlSession.SelectedValue), courseno, studids, oldmarks, vMarks, lock_status, Convert.ToInt32(Session["userno"]), Request.ServerVariables["REMOTE_ADDR"].ToString(), string.Empty, rblRevalStatus.SelectedValue, ddlValuer.SelectedValue);
                CustomStatus cs = (CustomStatus)objMarksEntry.ChangeRevalMarks(Convert.ToInt32(ddlSession.SelectedValue), courseno, studids, oldmarks, vMarks, lock_status, Convert.ToInt32(Session["userno"]), Request.ServerVariables["REMOTE_ADDR"].ToString(), string.Empty); //modified by srikanth 14-03-2020
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    if (lock_status == 1)
                        objCommon.DisplayMessage("Marks Locked Successfully!!!", this.Page);

                    else
                        objCommon.DisplayMessage("Marks Saved Successfully!!!", this.Page);

                    objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 2);
                    ShowStudents();

                }
                else
                    objCommon.DisplayMessage("Error in Saving Marks!", this.Page);
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.SaveRevalMarksAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckActivity()
    {
        bool ret = true;
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                ret = false;
            }

            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    //private bool CheckExamActivity()
    //{
    //    //GET THE EXAMS THAT ARE ON 
    //    DataSet dsExams = objMarksEntry.GetONExams(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["userno"]), int.Parse(Request.QueryString["pageno"].ToString()));

    //    string exams = string.Empty;

    //    if (dsExams != null && dsExams.Tables.Count > 0)
    //    {
    //        if (dsExams.Tables[0].Rows.Count <= 0)
    //        {
    //            return false;
    //        }
    //        else
    //            return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    #endregion

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        try
        {
            string idnos = string.Empty;
            int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
            int examno = Convert.ToInt32(ddlExam.SelectedValue);
            //int opNo = Convert.ToInt32(rblRevalStatus.SelectedValue);

            for (int i = 0; i < gvStudent.Rows.Count; i++)
            {
                // TextBox txtVMrk = gvStudent.Rows[i].FindControl("txtVMrk") as TextBox;
                if (examno != 0)
                {
                    Label lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    idnos += lbl.ToolTip + ",";
                }
            }
            idnos = idnos.Remove(idnos.Length - 1);

            //CustomStatus cs = (CustomStatus)objMarksEntry.UnlockRevalMark(Convert.ToInt32(ddlSession.SelectedValue), courseno, idnos, examno, rblRevalStatus.SelectedValue, ddlValuer.SelectedValue);
            CustomStatus cs = (CustomStatus)objMarksEntry.UnlockRevalMark(Convert.ToInt32(ddlSession.SelectedValue), courseno, idnos, examno); //MODIFIED BY SRIKANTH 14-03-2020

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage("Mark Unlocked Successfully!!!", this.Page);
                ShowStudents();
            }
            else
                objCommon.DisplayMessage("Error in Saving ..!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntry_Admin.ddlDegree_SelectedIndexChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Revaluation Result", "rptRevalScrutiny_Result.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        string[] course = ddlCourse.SelectedItem.Text.Split('~');
        string ccode = course[0].Trim();
        int courseno = Convert.ToInt32(ddlCourse.SelectedValue);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + sessionno + ",@P_SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + ",@P_CCODE=" + ccode + ",@P_COURSENO=" + courseno + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue);// +",@P_APP_TYPE=" + rblRevalStatus.SelectedValue;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ExamRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;

        if (ddlSemester.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT REV ON (C.COURSENO = REV.COURSENO)", "DISTINCT C.COURSENO", "(DBO.FN_DESC('CCODE',C.COURSENO)+' ~ '+DBO.FN_DESC('COURSENAME',C.COURSENO)) COURSENAME", "ISNULL(REV.REV_APPROVE_STAT,0)=1 AND ISNULL(REV.CANCEL,0)=0 AND REV.SCHEMENO=" + Convert.ToInt32(ddlScheme.SelectedValue) + " AND REV.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue) + " AND REV.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "C.COURSENO");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStudent.DataSource = null;
        gvStudent.DataBind();
        pnlStudGrid.Visible = false;
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME SC INNER JOIN ACD_REVAL_RESULT REV ON (SC.SCHEMENO = REV.SCHEMENO)", "DISTINCT SC.SCHEMENO", "SC.SCHEMENAME", "ISNULL(REV.REV_APPROVE_STAT,0)=1 AND ISNULL(REV.CANCEL,0)=0 AND SC.SCHEMENO > 0 AND REV.SESSIONNO=" + ddlSession.SelectedValue, "SC.SCHEMENO DESC");
        }
    }
}