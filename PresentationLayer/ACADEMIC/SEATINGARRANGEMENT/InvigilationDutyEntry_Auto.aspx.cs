//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : INVIGILATOR DUTY ENTRY 
// CREATION DATE : 21-MAR-2012
// CREATED BY    : PRIYANKA KABADE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_InvigilationDutyEntry_Auto : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objEC = new ExamController();

    #region Page Events
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
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            PopulateDropDownList();
            FillText();
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignFacultyAdvisor.aspx");
        }
    }

    #endregion

    #region Other Events

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0")
            objCommon.FillDropDownList(ddlDay, "ACD_EXAM_DATE", "DISTINCT DAYNO AS DAY", "DAYNO", "SESSIONNO =" + ddlSession.SelectedValue, "DAYNO");
        else
            ClearControls();
    }
    protected void ddlSession2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession2.SelectedValue != "0")
            objCommon.FillDropDownList(ddlInvigilator, "ACD_INVIGilATION_DUTY D INNER JOIN ACD_EXAM_INVIGILATOR EX ON (D.UA_NO = EX.UA_NO AND EX.STATUS = 1) INNER JOIN USER_ACC U ON (D.UA_NO = U.UA_NO)", "DISTINCT U.UA_NO", "U.UA_FULLNAME ", "D.SESSIONNO=" + ddlSession2.SelectedValue, "U.UA_FULLNAME");
        else
        {
            lblExamDate2.Text = null;
            ddlSlot2.Items.Clear();
            ddlSlot2.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot2.Focus();
        }
    }
    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDay.SelectedValue != "0")
        {
            string sessionno = ddlSession.SelectedValue;
            string day_no = ddlDay.SelectedValue;
            string slot = ddlSlot.SelectedValue;
            lblExamDate.Text = objCommon.LookUp("ACD_EXAM_DATE", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103)+ ' - ' +DATENAME(DW,EXAMDATE)", "DAYNO = " + ddlDay.SelectedValue + " AND SESSIONNO = " + ddlSession.SelectedValue);
            objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE EX INNER JOIN ACD_EXAM_TT_SLOT S ON (S.SLOTNO = EX.SLOTNO)", "DISTINCT EX.SLOTNO", "S.SLOTNAME", " EX.SESSIONNO = " + ddlSession.SelectedValue + " AND EX.DAYNO = " + ddlDay.SelectedValue, "EX.SLOTNO");
            txtStudent.Text = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO)", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + "AND SR.EXAM_REGISTERED=1 AND (DETAIND = 0 OR DETAIND IS NULL )AND SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " ) AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " )");
        }
        else
        {
            lblExamDate.Text = null;
            txtStudent.Text = null;
            ddlSlot.Items.Clear();
            ddlSlot.Items.Add(new ListItem("Please Select", "0"));
            ddlSlot.Focus();
            txtStudent.Text = null;
            txtInvig.Text = null;
            txtTotal.Text = null;
        }
    }
    protected void ddlDay2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDay2.SelectedValue != "0")
        {
            lblExamDate2.Text = objCommon.LookUp("ACD_EXAM_DATE", "DISTINCT CONVERT(NVARCHAR,EXAMDATE,103) + ' - ' +DATENAME(DW,EXAMDATE)", "DAYNO = " + ddlDay2.SelectedValue + " AND SESSIONNO = " + ddlSession2.SelectedValue);
            objCommon.FillDropDownList(ddlSlot2, "ACD_INVIGILATION_DUTY I INNER JOIN ACD_EXAM_TT_SLOT S ON (S.SLOTNO = I.SLOTNO)", "DISTINCT I.SLOTNO", "S.SLOTNAME", " I.SESSIONNO = " + ddlSession2.SelectedValue + " AND I.DAYNO = " + ddlDay2.SelectedValue + " AND I.UA_NO = " + ddlInvigilator.SelectedValue, "I.SLOTNO");
        }
        else
        {
            lblExamDate2.Text = null;
            ddlSlot2.Items.Clear();
            ddlSlot2.Items.Add(new ListItem ("Please Select","0"));
            ddlSlot2.Focus();
            
        }
    }
    protected void ddlInvigilator_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInvigilator.SelectedValue != "0")
            objCommon.FillDropDownList(ddlDay2, "ACD_INVIGILATION_DUTY", "DISTINCT DAYNO AS DAY", "DAYNO", "SESSIONNO =" + ddlSession2.SelectedValue + " AND UA_NO=" + ddlInvigilator.SelectedValue, "DAYNO");
        else
        {
            ddlDay2.Items.Clear();
            ddlDay2.Items.Add(new ListItem ("Please Select","0"));
            ddlInvigilator.Focus();
            lblExamDate2.Text = null;
            ddlSlot2.Items.Clear();
            ddlSlot2.Items.Add(new ListItem ("Please Select","0"));
            ddlSlot2.Focus();
        }
    }
    protected void ddlSlot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSlot.SelectedValue != "0")
        {
            string sessionno = ddlSession.SelectedValue;
            string day_no = ddlDay.SelectedValue;
            string slot = ddlSlot.SelectedValue;
            txtStudent.Text = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO )", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + "AND SR.EXAM_REGISTERED=1 AND (DETAIND = 0 OR DETAIND IS NULL )AND SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ")");
            txtInvig.Text = (Math.Ceiling(decimal.Parse(txtStudent.Text.ToString()) / int.Parse(txtSeat.Text.ToString()) * 2)).ToString();
            txtTotal.Text = txtInvig.Text.ToString(); // + Convert.ToInt16("0")).ToString();
        }
        else
        {
            txtStudent.Text = null;
            txtInvig.Text = null;
            txtTotal.Text = null;
        }
    }
    #endregion

    #region Click Events
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtSeat.Text.Trim()))
            {
                //objExamController.UpdateSeatsPerBlock(int.Parse(txtSeat.Text));

                decimal cnt = 0.0m;
                decimal seats = 0.0m;
                decimal no_blocks = 0.0m;
                decimal total_reliever = 0.0m;
                // decimal no_reliever_block = 3.0m;
                //decimal extra_reliever = 3.0m;
                //decimal class3_reliever = 3.0m;
                decimal teaching_reliever = 3.0m;

                //IN GHRCE, SET SEATS AS TOP OF PREFERENCE LIST ROOM.
                //seats = decimal.Parse(objCommon.LookUp("ACD_ROOM", "AVG(ACTUALCAPACITY)", string.Empty));

                //AVG. SEATS PER BLOCK FROM TEXTBOX
                seats = int.Parse(txtSeat.Text);
                string sessionno = ddlSession.SelectedValue;
                //All Days & Slots 
                if (ddlDay.SelectedIndex <= 0)
                {
                    for (int i = 1; i < ddlDay.Items.Count; i++)
                    {
                        string day_no = ddlDay.Items[i].Value;
                        DataSet ds = objCommon.FillDropDown("ACD_EXAM_DATE", "DISTINCT SLOTNO", "DAYNO", "DAYNO = " + ddlDay.Items[i].Value + "AND SESSIONNO =" + sessionno, "SLOTNO");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                string slot = ds.Tables[0].Rows[j]["slotno"].ToString();

                                //SUM OF EXAM REGISTERED STUDENTS FOR THAT DAY'S COURSE PAPERS 
                                string tmpcnt = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO )", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + " AND EXAM_REGISTERED =1 AND (DETAIND IS NULL OR DETAIND = 0) AND  SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM  ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM  ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ")");

                                if (!string.IsNullOrEmpty(tmpcnt))
                                {
                                    cnt = decimal.Parse(tmpcnt); // TO DECIMAL

                                    //NO. OF ROOMS = NO.OF STUDENTS/ROOM CAPACITY..
                                    no_blocks = (int)Math.Ceiling((cnt / seats));

                                    //2 RELIVER'S / BLOCK OR AS ENTERED BY USER
                                    total_reliever = txtReliver.Text.ToString() == "" ? 0 : int.Parse(txtReliver.Text.ToString());

                                    //TOTAL INVIGILATORS DUTY FOR ONE DAY & SLOT
                                    teaching_reliever = no_blocks * 2 + total_reliever;

                                    //ASSIGN DUTY
                                    objEC.AddInvigilationDuty(Convert.ToInt32(sessionno), Convert.ToInt32(day_no), Convert.ToInt16(slot), (int)teaching_reliever, Session["colcode"].ToString());
                                }
                                #region Unused code
                                ////Extra Releiver..
                                //if (no_blocks <= 10)
                                //    extra_reliever = 2;
                                //else if (no_blocks > 10 && no_blocks <= 15)
                                //    extra_reliever = 3;
                                //else if (no_blocks > 15)
                                //    extra_reliever = 4;

                                //total_reliever = (int)no_blocks;
                                //NO.OF RELIEVER BLOCK...........WHY DIVIDE BY 3.0
                                //TOTAL RELIEVER = 2
                                //total_reliever += (int)Math.Ceiling(no_blocks / no_reliever_block);
                                //TOTAL RELIEVER = 2+1
                                //total_reliever += extra_reliever;
                                //TOTAL RELIEVER = 3+2 = 5

                                //IF STATUS = 1, THESE ARE THE AVAILABLE FACULTY FOR INVIGILATION.
                                //string cl_cnt1 = objCommon.LookUp("ACD_EXAM_INVIGILATOR", "COUNT(*) CNT", "STATUS = 1");
                                ////string t_cnt1 = objCommon.LookUp("ACD_EXAM_INVIGILATOR", "COUNT(*) CNT", "CLASSNO = 2 AND DISTANCENO = " + ddlSlot.Items[j].Value);

                                //decimal tot_cnt = int.Parse(cl_cnt1);// +int.Parse(t_cnt1);

                                //decimal cl_Per = (decimal.Parse(cl_cnt1) / tot_cnt) * 100;
                                //decimal t_Per = (decimal.Parse(t_cnt1) / tot_cnt) * 100;

                                //Teaching Staff..
                                //? WHY PERCANTAGE OF TEACHING RELIEVER.
                                //teaching_reliever = (tot_cnt / 100);// *total_reliever;
                                //teaching_reliever = (int)Math.Ceiling(tot_cnt / total_reliever);// * 100);


                                //Class3..
                                //class3_reliever = (int)Math.Floor(total_reliever / 2);
                                //class3_reliever = (cl_Per / 100) * total_reliever;
                                //objEC.AddInvigilationDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDay.Items[i].Value), Convert.ToInt32(ddlSlot.Items[j].Value), (int)class3_reliever, Session["colcode"].ToString());


                                ////Teaching Staff..
                                //teaching_reliever = (int)Math.Ceiling(total_reliever / 2);
                                //classno = 2;
                                //objEC.AddInvigilationDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDay.Items[i].Value), Convert.ToInt32(ddlSlot.Items[j].Value), (int)teaching_reliever, Session["colcode"].ToString());

                                ////Class3..
                                //class3_reliever = (int)Math.Floor(total_reliever / 2);
                                //classno = 1;
                                //objEC.AddInvigilationDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDay.Items[i].Value), Convert.ToInt32(ddlSlot.Items[j].Value), (int)class3_reliever, Session["colcode"].ToString());
                                #endregion
                            }
                        }
                    }

                    objCommon.DisplayMessage(this.updInvigAuto, "Invigilation Duty Generated Successfully!", this.Page);
                }
                else
                {
                    if (ddlSlot.SelectedIndex > 0)
                    {
                        //Dropdown Parameters
                        string day_no = ddlDay.SelectedValue;
                        string slot = ddlSlot.SelectedValue;

                        //IF TWO COURSES FOR THAT SESSION, DAY AND SLOT, THEN SUM OF THOSE TWO COURSES STUDENTS.
                        //Single Day/Slot
                        string tmp = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO )", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + " AND SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ")");
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            cnt = decimal.Parse(tmp);
                            no_blocks = (int)Math.Ceiling((cnt / seats));

                            ////Extra Releiver..
                            //if (no_blocks <= 10)
                            //    extra_reliever = 2;
                            //else if (no_blocks > 10 && no_blocks <= 15)
                            //    extra_reliever = 3;
                            //else if (no_blocks > 15)
                            //    extra_reliever = 4;

                            //2 RELIVER'S / BLOCK OR AS ENTERED BY USER
                            total_reliever = txtReliver.Text.ToString() == "" ? 0 : int.Parse(txtReliver.Text.ToString());

                            //TOTAL INVIGILATORS DUTY FOR ONE DAY & SLOT
                            teaching_reliever = (int)Math.Ceiling(no_blocks * 2 + total_reliever);
                            //total_reliever = (int)no_blocks;
                            //total_reliever += (int)Math.Ceiling(no_blocks / no_reliever_block);
                            //total_reliever += extra_reliever;

                            //TOTAL INVIGILATORS DUTY FOR ONE DAY & SLOT
                            // teaching_reliever += (int)Math.Ceiling(total_reliever);// / 2);

                            //ASSIGN DUTY
                            objEC.AddInvigilationDuty(Convert.ToInt32(sessionno), Convert.ToInt32(day_no), Convert.ToInt32(slot), (int)teaching_reliever, Session["colcode"].ToString());

                            ////Class3..
                            //class3_reliever = (int)Math.Floor(total_reliever / 2);
                            //objEC.AddInvigilationDuty(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDay.SelectedValue), Convert.ToInt32(ddlSlot.SelectedValue), (int)class3_reliever, Session["colcode"].ToString());

                        }
                        objCommon.DisplayMessage(this.updInvigAuto, "Invigilation Duty Generated Successfully!", this.Page);
                    }
                    else
                    {
                        string day_no = ddlDay.SelectedValue;

                        DataSet ds = objCommon.FillDropDown("ACD_EXAM_DATE", "DISTINCT SLOTNO", "DAYNO", "DAYNO = " + day_no + "AND SESSIONNO =" + sessionno, "SLOTNO");

                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                string slot = ds.Tables[0].Rows[j]["slotno"].ToString();

                                //IF TWO COURSES FOR THAT SESSION, DAY AND SLOT, THEN SUM OF THOSE TWO COURSES STUDENTS.
                                //Single Day/Slot
                                string tmp = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_EXAM_DATE DT ON (SR.SESSIONNO = DT.SESSIONNO AND SR.SEMESTERNO = DT.SEMESTERNO AND SR.SCHEMENO = DT.SCHEMENO AND SR.COURSENO = DT.COURSENO )", "COUNT(DISTINCT IDNO )", "SR.SESSIONNO = " + sessionno + " AND SR.SEMESTERNO IN (SELECT DISTINCT SEMESTERNO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.SCHEMENO IN (SELECT DISTINCT SCHEMENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ") AND SR.COURSENO IN (SELECT DISTINCT COURSENO FROM ACD_EXAM_DATE WHERE SESSIONNO = " + sessionno + " AND DAYNO = " + day_no + " AND SLOTNO = " + slot + ")");
                                if (!string.IsNullOrEmpty(tmp))
                                {
                                    cnt = decimal.Parse(tmp);
                                    no_blocks = (int)Math.Ceiling((cnt / seats));
                                    //2 RELIVER'S / BLOCK OR AS ENTERED BY USER
                                    total_reliever = txtReliver.Text.ToString() == "" ? 0 : int.Parse(txtReliver.Text.ToString());

                                    //TOTAL INVIGILATORS DUTY FOR ONE DAY & SLOT
                                    teaching_reliever = (int)Math.Ceiling(no_blocks * 2 + total_reliever);
                                    //ASSIGN DUTY
                                    objEC.AddInvigilationDuty(Convert.ToInt32(sessionno), Convert.ToInt32(day_no), Convert.ToInt32(slot), (int)teaching_reliever, Session["colcode"].ToString());
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.updInvigAuto, "No Student Forund!!", this.Page);
                                    return;
                                }
                            }
                            objCommon.DisplayMessage(this.updInvigAuto, "Invigilation Duty Generated Successfully!", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updInvigAuto, "Error!!", this.Page);
                            return;
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updInvigAuto, "Please Enter Seating Capacity Per Block!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.btnGenerate_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedValue != "0")
            this.ShowReportDayWise("Invigilation_Duty_Day_Wise", "rptInvigilation.rpt");
        else
            objCommon.DisplayMessage(this.updInvigAuto, "Please select Session!", this.Page);
    }
    protected void btnReport2_Click(object sender, EventArgs e)
    {
        this.ShowReportInvigilatorWise("Invigilation_Duty_Invigilator_Wise", "rptInvigilationWise.rpt");
    }
    #endregion

    #region User Methods
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND FLOCK=1 ", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession2, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND FLOCK=1 ", "SESSIONNO DESC");

    }
    private void FillText()
    {
        txtSeat.Text = objCommon.LookUp("ACD_ROOM", "AVG(ACTUALCAPACITY)", "ROOMNO != 0");
        
        }
   
    private void ShowReportDayWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO=" + ddlDay.SelectedValue+ ",@P_SLOTNO=" + ddlSlot.SelectedValue + ",@P_UA_NO=0,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            // url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DAYNO=" + Convert.ToInt32(ddlDay.SelectedValue) + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot.SelectedValue) + ",@P_UA_NO=" + (string.IsNullOrEmpty(ddlInvigilator.SelectedValue) == true ? "0" : ddlInvigilator.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigAuto, this.updInvigAuto.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
 
    private void ShowReportInvigilatorWise(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession2.SelectedValue + ",@P_DAYNO=" +ddlDay2.SelectedValue + ",@P_SLOTNO=" + ddlSlot2.SelectedValue + ",@P_UA_NO=" + (string.IsNullOrEmpty(ddlInvigilator.SelectedValue) == true ? "0" : ddlInvigilator.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_OrderNo=" + txtOrderNo.Text + ",@P_Date=" + txtDate.Text;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updInvigAuto, this.updInvigAuto.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_InvigilationDuty.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        ddlDay.SelectedIndex = 0;
        ddlSlot.SelectedIndex = 0;
        lblExamDate.Text = null;
        txtStudent.Text = null;
        txtInvig.Text = null;
        txtTotal.Text = null;

    }
    #endregion


   
}

