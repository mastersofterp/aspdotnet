//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : ACADEMIC - REVAL MARK ENTRY BY ADMIN                                          
// CREATION DATE : 28-MAY-2022                                                     
// CREATED BY    : SNEHA DOBLE                                  
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
using System.Configuration;

public partial class ACADEMIC_EXAMINATION_RevalMarkEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMarksEntry = new MarksEntryController();
    string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
                    objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(C.COLLEGE_NAME,'')COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    if (ddlSession.SelectedValue == "0")
                    {
                        objCommon.DisplayMessage("The Mark Entry activity may not be Started!!!, Please contact Admin", this.Page);
                        pnlMarkEntry.Visible = false;
                    }

                }
                else
                {
                    // CheckPageAuthorization();
                }

            }
        }
        divMsg.InnerHtml = string.Empty;
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowCourseDetails();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveAndLock(0);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["OrgId"]) == 1)
        {
            this.ShowMarkEntryReport("Reval Mark Entry Report", "rpt_Revel_mark_entry_report.rpt");
        }
        else if (Convert.ToInt32(Session["OrgId"]) == 6)
        {
            this.ShowMarkEntryReport("Reval Mark Entry Report", "rpt_Revel_mark_entry_report_Rcpiper.rpt");
        }
        else
        {
            this.ShowMarkEntryReport("Reval Mark Entry Report", "rpt_Revel_mark_entry_report_CC.rpt");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void ShowMarkEntryReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            if (Convert.ToInt32(Session["OrgId"]) == 1 || Convert.ToInt32(Session["OrgId"]) == 6)
            {
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + "";
            }
            else
            {
                string collegeid = ddlcollege.SelectedValue == "" ? Convert.ToString(Session["colcode"]) : ddlcollege.SelectedValue;
                url += "&param=@P_COLLEGE_CODE=" + collegeid + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + ",@P_COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + "";
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updpnl, this.updpnl.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        int Markscount = Convert.ToInt32(objCommon.LookUp("ACD_REVAL_RESULT", "COUNT(IDNO)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlsemester.SelectedValue) + " AND COURSENO=" + Convert.ToInt32(ddlCourse.SelectedValue) + " AND (APP_TYPE='REVAL' OR APP_TYPE ='REVAL_PHOTO') AND NEWMRKS IS NULL AND ISNULL(REV_APPROVE_STAT,0)=1"));
        if (Markscount > 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Save the Mark Entry first.", this.Page);
            return;
        }
        else
        {
            SaveAndLock(1);
        }
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
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.CheckMarks --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return flag;
    }

    private void SaveAndLock(int lock_status)
    {
        try
        {

            //Check for lock and null marks
            if (CheckMarks(lock_status) == false)
            {
                return;
            }
            string studids = string.Empty;
            string marks = string.Empty;
            string Actualmarks = string.Empty;

            MarksEntryController objMarksEntry = new MarksEntryController();
            Label lbl;
            TextBox txtMarks;
            Label lblExternal;
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
                    lblExternal = gvStudent.Rows[i].FindControl("lblExternal") as Label;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    // Actualmarks += lblExternal.Text.Trim() == string.Empty ? "-100," : lblExternal.Text + ",";

                }
                else if (lock_status == 1 || lock_status == 2)
                {
                    //Gather Student IDs 
                    lbl = gvStudent.Rows[i].FindControl("lblIDNO") as Label;
                    studids += lbl.ToolTip + ",";

                    //Gather Exam Marks 
                    txtMarks = gvStudent.Rows[i].FindControl("txtMarks") as TextBox;
                    lblExternal = gvStudent.Rows[i].FindControl("lblExternal") as Label;
                    marks += txtMarks.Text.Trim() == string.Empty ? "-100," : txtMarks.Text + ",";
                    //Actualmarks += lblExternal.Text.Trim() == string.Empty ? "-100," : lblExternal.Text + ",";
                }
            }
            studids = studids.TrimEnd(',');

            if (studids == string.Empty)
            {
                objCommon.DisplayMessage(updpnl, "Please Select Student!!!", this.Page);
                return;
            }

            string ccode = objCommon.LookUp("ACD_COURSE", "CCODE", "COURSENO=" + ddlCourse.SelectedValue);
            CustomStatus cs = 0;

            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntrybyAdmin(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue), studids, marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]));
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntrybyAdminRcpiper(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue), studids, marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]));
            }
            else
            {
                cs = (CustomStatus)objMarksEntry.InsertRevaluationMarkEntrybyAdmin_CC(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue), Convert.ToInt32(ddldegree.SelectedValue), Convert.ToInt32(ddlbranch.SelectedValue), studids, marks, lock_status, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["OrgId"]));
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

                btnReport.Enabled = true;
                ShowCourseDetails();
            }
            else
                objCommon.DisplayMessage(updpnl, "Error in Saving Marks!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_MarkEntry.SaveAndLock --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcollege.SelectedIndex > 0)
            {

                int count = 0;
                count = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "COUNT(SESSIONNO)", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')"));
                if (count > 0)
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "");
                    ddlSession.SelectedIndex = 1;

                    objCommon.FillDropDownList(ddldegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON A.DEGREENO=B.DEGREENO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON CDB.DEGREENO=A.DEGREENO", "DISTINCT A.DEGREENO", "A.DEGREENAME", "B.COLLEGE_ID=" + ddlcollege.SelectedValue + "", "A.degreeno");
                    ddldegree.Focus();
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
                ddlSession.Items.Clear();
                ddlSession.Items.Add(new ListItem("Please Select", "0"));
                ddldegree.Items.Clear();
                ddldegree.Items.Add(new ListItem("Please Select", "0"));
                ddlbranch.Items.Clear();
                ddlbranch.Items.Add(new ListItem("Please Select", "0"));

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlscheme.Items.Clear();
                ddlscheme.Items.Add(new ListItem("Please Select", "0"));


                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));


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

    private void ShowCourseDetails()
    {
        try
        {
            string sp_procedure = string.Empty;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                sp_procedure = "PKG_ACD_REVAL_MARK_ENTRY_STUDENT";
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                sp_procedure = "PKG_ACD_REVAL_MARK_ENTRY_STUDENT_RCPIPER";
            }
            else
            {
                sp_procedure = "PKG_ACD_REVAL_MARK_ENTRY_STUDENT_CC";
            }

            string sp_parameters = "@P_SESSIONNO,@P_SCHEMENO,@P_SEMESTERNO,@P_COURSENO";
            string sp_callValues = "" + (ddlSession.SelectedValue) + "," + ddlscheme.SelectedValue + "," + (ddlsemester.SelectedValue) + "," + ddlCourse.SelectedValue + "";
            DataSet dsCERT = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            {
                // gvStudent.Columns[4].HeaderText = string.Empty;
                if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
                {
                    hfdMaxMark.Value = dsCERT.Tables[0].Rows[0]["SMAX"].ToString();
                    gvStudent.Columns[4].HeaderText = "Reval Marks" + "  " + "[Max : " + hfdMaxMark.Value + "]";

                    gvStudent.DataSource = dsCERT;
                    gvStudent.DataBind();
                    pnlStudGrid.Visible = true;
                    btnSave.Enabled = true;
                    btnLock.Enabled = true;
                    btnSave.Visible = true;
                    btnLock.Visible = true;
                    btnReport.Visible = true;
                    if (Convert.ToDecimal(dsCERT.Tables[0].Rows[0]["SMAX"].ToString()) > 0)
                    {
                        hfdMaxMark.Value = dsCERT.Tables[0].Rows[0]["SMAX"].ToString();
                        gvStudent.Columns[4].HeaderText = "Reval Marks" + "  " + "[Max : " + dsCERT.Tables[0].Rows[0]["SMAX"].ToString() + "]";

                        #region PreviosMarkLockUnlock //Commeted dt on 27092023
                        //if (Convert.ToInt32(dsCERT.Tables[0].Rows[0]["LOCK"].ToString()) == 1)
                        //{
                        //    gvStudent.Columns[5].Visible = true;
                        //    gvStudent.Columns[6].Visible = false;
                        //    btnSave.Enabled = false;
                        //    btnLock.Enabled = false;
                        //    btngrade.Visible = true;
                        //}
                        //else
                        //{
                        //    gvStudent.Columns[5].Visible = false;
                        //    gvStudent.Columns[6].Visible = false;
                        //    btnSave.Enabled = true;
                        //    btnLock.Enabled = true;
                        //    btngrade.Visible = false;
                        //}
                        //if (dsCERT.Tables[0].Rows[0]["NEW_GRADE"].ToString() != string.Empty && Convert.ToInt32(dsCERT.Tables[0].Rows[0]["LOCK"].ToString())==1)
                        //{
                        //    gvStudent.Columns[6].Visible = true;
                        //    //gvStudent.Columns[5].Visible = true;
                        //    btnSave.Enabled = false;
                        //    btnLock.Enabled = false;

                        //    btnSave.Visible = false;
                        //    btnLock.Visible = false;
                        //    btngrade.Visible = false;
                        //    //btnMarksModifyReport.Visible = true;
                        //    //btnfinalmarkentry.Visible = true;
                        //    //btnmarkexcel.Visible = true;
                        //}
                        //else
                        //{
                        //    gvStudent.Columns[6].Visible = false;
                        //   // btngrade.Visible = true;
                        //    //btnfinalmarkentry.Visible = false;
                        //    //btnmarkexcel.Visible = false;
                        //}
                        #endregion

                        #region Checking the Marks lock for All Students
                        int lockcount = 0;
                        int gradecount = 0;
                        for (int i = 0; i < dsCERT.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dsCERT.Tables[0].Rows[i]["LOCK"]) == 1)
                            {
                                lockcount++;
                            }
                        }
                        for (int i = 0; i < dsCERT.Tables[0].Rows.Count; i++)
                        {
                            if (dsCERT.Tables[0].Rows[i]["NEW_GRADE"].ToString() != string.Empty)
                            {
                                gradecount++;
                            }
                        }

                        if (dsCERT.Tables[0].Rows.Count == Convert.ToInt32(gradecount) && dsCERT.Tables[0].Rows.Count == Convert.ToInt32(lockcount))
                        {
                            gvStudent.Columns[6].Visible = true;
                            gvStudent.Columns[5].Visible = true;
                            btngrade.Visible = true;
                            btnSave.Enabled = false;
                            btnLock.Enabled = false;

                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            btngrade.Text = "Re-Generate Grade";
                        }
                        else if (dsCERT.Tables[0].Rows.Count == Convert.ToInt32(lockcount))
                        {
                            gvStudent.Columns[5].Visible = true;
                            gvStudent.Columns[6].Visible = false;
                            btngrade.Visible = true;
                            btnSave.Enabled = false;
                            btnLock.Enabled = false;

                            btnSave.Visible = false;
                            btnLock.Visible = false;
                            btngrade.Text = "Generate Grade";
                        }
                        else
                        {
                            gvStudent.Columns[5].Visible = false;
                            gvStudent.Columns[6].Visible = false;
                            btnSave.Enabled = true;
                            btnLock.Enabled = true;
                            btngrade.Visible = false;

                            btnSave.Visible = true;
                            btnLock.Visible = true;
                        }
                        #endregion

                    }
                }
                else
                {
                    objCommon.DisplayMessage(this, "No Record Found", this.Page);
                    gvStudent.DataSource = null;
                    gvStudent.DataBind();
                    pnlStudGrid.Visible = false;
                    btnReport.Visible = false;
                    btngrade.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
        }
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
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));

        Clear();
    }

    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddldegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlbranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue), "LONGNAME");
                objCommon.FillDropDownList(ddlscheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", " DEGREENO =" + ddldegree.SelectedValue, "SCHEMENO DESC");
                ddlscheme.Focus();
            }
            else
            {
                ddlbranch.Items.Clear();
                ddlbranch.Items.Add(new ListItem("Please Select", "0"));

                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlscheme.Items.Clear();
                ddlscheme.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));

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

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));

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
                objCommon.FillDropDownList(ddlsemester, "ACD_REVAL_RESULT A INNER JOIN ACD_SEMESTER S ON (A.SEMESTERNO=S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND A.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND A.SCHEMENO=" + Convert.ToInt32(ddlscheme.SelectedValue), "S.SEMESTERNO");
            }
            else
            {
                ddlsemester.Items.Clear();
                ddlsemester.Items.Add(new ListItem("Please Select", "0"));

                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
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
        if (ddlsemester.SelectedIndex > 0)
        {
            if (Session["usertype"].ToString() == "3")
            {
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlscheme.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                ddlCourse.Focus();
            }
            else
            {
                //objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlscheme.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue) + " AND (APP_TYPE LIKE 'REVAL' OR APP_TYPE LIKE '%REVALUATION%' OR APP_TYPE LIKE '%REVAL_PHOTO%' ) ", "COURSE_NAME");
                objCommon.FillDropDownList(ddlCourse, "ACD_COURSE C INNER JOIN ACD_REVAL_RESULT SR ON C.COURSENO = SR.COURSENO", "DISTINCT SR.COURSENO", "(C.CCODE + ' - ' + C.COURSE_NAME) COURSE_NAME ", "SR.SCHEMENO = " + ddlscheme.SelectedValue + " AND SR.SEMESTERNO = " + ddlsemester.SelectedValue + "AND SR.SESSIONNO =" + Convert.ToInt32(ddlSession.SelectedValue), "COURSE_NAME");
                ddlCourse.Focus();
            }
        }
        else
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("Please Select", "0"));


        }
        Clear();
    }
    protected void btngrade_Click(object sender, EventArgs e)
    {
        try
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
            int subid = Convert.ToInt32(objCommon.LookUp("ACD_COURSE", "DISTINCT SUBID", "COURSENO=" + ddlCourse.SelectedValue));

            CustomStatus cs = 0;
            if (Convert.ToInt32(Session["OrgId"]) == 1)
            {
                cs = (CustomStatus)objMarksEntry.RevalGradeGeneration(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, subid, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
            }
            else if (Convert.ToInt32(Session["OrgId"]) == 6)
            {
                cs = (CustomStatus)objMarksEntry.RevalGradeGeneration_Rcpiper(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, subid, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
            }
            else
            {
                cs = (CustomStatus)objMarksEntry.RevalGradeGeneration_CC(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), studids, subid, Convert.ToInt32(Session["userno"]), ViewState["ipAddress"].ToString(), Convert.ToInt32(ddlscheme.SelectedValue), Convert.ToInt32(ddlsemester.SelectedValue));
            } 
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updpnl, "Grade Generated Successfully!!!", this.Page);
                btnReport.Enabled = true;
                ShowCourseDetails();
            }
            else
            {
                objCommon.DisplayMessage(updpnl, "Something Went Wrong..!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
   }