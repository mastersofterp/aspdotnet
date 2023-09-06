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
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.IO;

public partial class ACADEMIC_SEATINGARRANGEMENT_SeatingPlanManual : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();
    Seating objSeating = new Seating();

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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                }
                PopulateDropDownList();
                ddlCourse.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_RoomConfig.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {

        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
        }
    }
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");       
    }

    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        ddlExamName.SelectedIndex = 0;
        pnlProgramName.Visible = false;
        lvProgramNames.DataSource = null;
        lvProgramNames.DataBind();
        pnlFilteredStudent.Visible = false;
        lvFilteredStudent.DataSource = null;
        lvFilteredStudent.DataBind();
        ddlCourse.SelectedIndex = 0;
        ddlCourse.Enabled = true;
        ddlRoom.SelectedIndex = 0;
        txtSelectedStudStrengh.Text = "";
        txtSelctedRommCap.Text = "";
        txtRemRoomCapacity.Text = string.Empty;
        txtTSelected.Text = string.Empty;
        string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updplRoom, "No Exams Are Conducted on the Selected Date !!!!", this.Page);
            ddlslot.SelectedValue = "0";
            return;
        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    public void BindExamCourseList()
    {
        try
        {
            if (ddlExamName.SelectedIndex > 0)
            {
                string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
                DataSet ds = objSC.GetExamManualCourseListByDate(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlslot.SelectedValue), EXAMDATE, Convert.ToInt16(ddlExamName.SelectedValue));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lvProgramNames.DataSource = ds;
                    lvProgramNames.DataBind();
                    pnlProgramName.Visible = true;
                    ddlCourse.DataSource = ds;
                    ddlCourse.DataValueField = ds.Tables[0].Columns["COURSENO"].ToString();
                    ddlCourse.DataTextField = ds.Tables[0].Columns["COURSES"].ToString();
                    ddlCourse.DataBind();
                    pnlProgramName.Visible = true;
                }
                else
                {
                    lvProgramNames.DataSource = null;
                    lvProgramNames.DataBind();
                    objCommon.DisplayUserMessage(updplRoom, "No Courses Founds On Selected Date!", this.Page);
                    ddlCourse.DataSource = null;
                    ddlCourse.DataBind();
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                    pnlProgramName.Visible = false;
                    ddlSession.SelectedIndex = 0;
                    txtExamDate.Text = string.Empty;
                    ddlslot.SelectedIndex = 0;
                    ddlExamName.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                lvProgramNames.DataSource = null;
                lvProgramNames.DataBind();
                ddlCourse.Items.Clear();
                ddlCourse.Items.Add(new ListItem("Please Select", "0"));
                pnlProgramName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void RoomDetails()
    {
        try
        {

            //DataSet dsroom = objCommon.FillDropDown("ACD_SEATING_PLAN SP INNER JOIN ACD_ROOM R ON(SP.ROOMNO=R.ROOMNO)", "SP.ROOMNO", "R.ROOMNAME,ROOMCAPACITY,ACTUALCAPACITY,SP.DISABLED_IDS", "", "R.ROOMNAME");
            objCommon.FillDropDownList(ddlRoom, "ACD_SEATING_PLAN SP INNER JOIN ACD_ROOM R ON(SP.ROOMNO=R.ROOMNO)", "SP.ROOMNO", "R.ROOMNAME", string.Empty, "R.ROOMNAME");
            //if (dsroom != null && dsroom.Tables[0].Rows.Count > 0)
            //{
            //    //lvRoomDetails.DataSource = dsroom;
            //    //lvRoomDetails.DataBind();
            //    //pnlRoomDetails.Visible = true;
            //    //divDetails.Visible = true;
            //    //divbuttons.Visible = true;
            //    ddlRoom.DataSource = dsroom;
            //    ddlRoom.DataBind();
            //}
            //else
            //{
            //    //lvRoomDetails.DataSource = null;
            //    //lvRoomDetails.DataBind();
            //    //pnlRoomDetails.Visible = false;
            //    //divDetails.Visible = false;
            //    //divbuttons.Visible = false;
            //    ddlRoom.DataSource = null;
            //    ddlRoom.DataBind();
            //}
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void BindSeatPlan()
    {
        try
        {
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            DataSet ds = objSC.GetAllManualSeatPlanByExamDate(EXAMDATE, slotno,Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlExamName.SelectedValue),string.Empty,string.Empty);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //lvdetails.DataSource = ds;
                //lvdetails.DataBind();
                //pnldetails.Visible = true;
            }
            else
            {
                //lvdetails.DataSource = null;
                //lvdetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.BindSeatPlan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnConfigure_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRoom.SelectedIndex > 0)
            {
                int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
                int slotno = Convert.ToInt32(ddlslot.SelectedValue);
                int countStud = 0;

                string coursenos = string.Empty, branchnos = string.Empty, semesternos = string.Empty, degreenos = string.Empty, idnos = string.Empty;
                int count = 0;

                foreach (ListViewItem lvitem in lvProgramNames.Items)
                {
                    CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
                    if (chk.Checked)
                    {
                        count++;
                    }
                }

                if (ddlCourse.SelectedIndex == 0 && count == 0)
                {
                    objCommon.DisplayMessage(this.updplRoom, "Please Select Course !!!!", this.Page);
                    return;
                }
                else
                {
                    foreach (ListViewItem lvitem in lvProgramNames.Items)
                    {
                        CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
                        Label lblBranchno = lvitem.FindControl("lblSchemeName") as Label;
                        Label lblCourseno = lvitem.FindControl("lblExamCourse") as Label;
                        Label lblSemno = lvitem.FindControl("lblSem") as Label;
                        Label lblDegreeno = lvitem.FindControl("lblCcode") as Label;

                        if (chk.Checked)
                        {
                            coursenos += lblCourseno.ToolTip + ',';
                            branchnos += lblBranchno.ToolTip + ',';
                            semesternos += lblSemno.ToolTip + ',';
                            degreenos += lblDegreeno.ToolTip + ',';
                        }
                    }
                }
                coursenos = coursenos.TrimEnd(',');
                branchnos = branchnos.TrimEnd(',');
                semesternos = semesternos.TrimEnd(',');
                degreenos = degreenos.TrimEnd(',');


                foreach (ListViewItem lvitem in lvFilteredStudent.Items)
                {
                    CheckBox chk = lvitem.FindControl("chkFilteredStudent") as CheckBox;
                    if (chk.Checked)
                    {
                        countStud++;
                    }
                }

                foreach (ListViewItem lvitem in lvFilteredStudent.Items)
                {
                    CheckBox chk = lvitem.FindControl("chkFilteredStudent") as CheckBox;
                    Label lblidno = lvitem.FindControl("lblStudName") as Label;

                    if (chk.Checked)
                    {
                        idnos += lblidno.ToolTip + ',';
                    }
                }
                idnos = idnos.TrimEnd(',');
             
                if (countStud > 0)
                {

                    //DataSet dscheck = objSC.CheckDataForManualSeatingArrangement(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), EXAMDATE, roomno, Ccodes);
                    //if (dscheck != null && dscheck.Tables[0].Rows.Count > 0)
                    //{
                    //    objCommon.DisplayUserMessage(updplRoom, "Seating arrangement for the selection is aready Configured !!!!", this.Page);
                    //    //lvdetails.DataSource = null;
                    //    //lvdetails.DataBind();
                    //    return;
                    //}
                    //else
                    //{
                    CustomStatus cs = (CustomStatus)objSC.ConfigureManualSeatingArrangmentDateWise(sessionno, slotno, EXAMDATE, (ddlCourse.SelectedIndex > 0 ? ddlCourse.SelectedValue : coursenos), Convert.ToInt32(ddlRoom.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(Session["userno"]), (ddlCourse.SelectedIndex > 0 ? string.Empty : branchnos), (ddlCourse.SelectedIndex > 0 ? string.Empty : degreenos), (ddlCourse.SelectedIndex > 0 ? string.Empty : semesternos), idnos);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayUserMessage(updplRoom, "Manual Seating arrangement done successfully !!!!", this.Page);
                        BindSeatPlan();
                        txtSelectedStudStrengh.Text = "";
                        txtSelctedRommCap.Text = "";
                        txtRemRoomCapacity.Text = string.Empty;
                        txtTSelected.Text = string.Empty;
                        hdStudCount.Value = "0";
                        ddlSession.SelectedValue = "0";
                        ddlslot.SelectedValue = "0";
                        ddlExamName.SelectedIndex = 0;
                        txtExamDate.Text = "";
                        lvProgramNames.DataSource = null;
                        lvProgramNames.DataBind();
                        pnlFilteredStudent.Visible = false;
                        lvFilteredStudent.DataSource = null;
                        lvFilteredStudent.DataBind();
                        ddlCourse.SelectedIndex = 0;
                        ddlRoom.SelectedIndex = 0;
                        btnConfigure.Visible = false;
                        ddlCourse.Enabled = true;
                        //lvRoomDetails.DataSource = null;
                        //lvRoomDetails.DataBind();
                        pnlProgramName.Visible = false;
                        //pnlRoomDetails.Visible = false;
                        ViewState["ccodes"] = null;
                        //divDetails.Visible = false;
                        //divbuttons.Visible = false;
                        //lvdetails.DataSource = null;
                        //lvdetails.DataBind();
                        //pnldetails.Visible = false;
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(updplRoom, "Failed To Configure Seating arrangement !!!!", this.Page);
                    }
                    //}
                }
                else
                {
                    objCommon.DisplayUserMessage(updplRoom, "Please select atleast one student from the student list !!!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "Please Select Room !!!!", this.Page);
                return;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.BindSeatPlan() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlslot.SelectedIndex > 0)
        {
            txtSelectedStudStrengh.Text = "";
            txtSelctedRommCap.Text = "";
            txtRemRoomCapacity.Text = string.Empty;
            txtTSelected.Text = string.Empty;
            pnlFilteredStudent.Visible = false;
            lvFilteredStudent.DataSource = null;
            lvFilteredStudent.DataBind();
            ddlRoom.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlCourse.Enabled = true;
            btnDeallocate.Visible = false;
            btnConfigure.Visible = false;
            hdStudCount.Value = "0";
            ddlExamName.SelectedIndex = 0;
            lvProgramNames.DataSource = null;
            lvProgramNames.DataBind();
            pnlProgramName.Visible = false;
        } 
    }
   
    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlslot.SelectedIndex = 0;
        txtSelectedStudStrengh.Text = string.Empty;
        ddlCourse.DataSource = null;
        ddlCourse.DataBind();
        ddlCourse.Items.Clear();
        ddlCourse.Items.Add(new ListItem("Please Select", "0"));
        pnlFilteredStudent.Visible = false;
        lvFilteredStudent.DataSource = null;
        lvFilteredStudent.DataBind();
        txtSelctedRommCap.Text = string.Empty;
        txtRemRoomCapacity.Text = string.Empty;

        if (ddlslot.SelectedIndex > 0)
        {
            string totalstud = string.Empty;
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int IsMidSem_Or_EndSem = 0;
            IsMidSem_Or_EndSem = Convert.ToInt32(ddlExamName.SelectedValue);
            if (IsMidSem_Or_EndSem == 1)//mid sem
            {
                //totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND AED.SUBID=1");
                totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND AED.SUBID=1");
            }
            else if (IsMidSem_Or_EndSem == 2)//end sem
            {
                //totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND EXAM_REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND AED.SUBID=1");
                totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND EXAM_REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "  AND AED.SUBID=1");
            }

            //int actualcapacity = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0 AND COLLEGE_ID=" + ddlCollege.SelectedValue));
            int actualcapacity = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0"));
            int disabled = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_PLAN CROSS APPLY DBO.SPLIT(DISABLED_IDS,',') A", "COUNT(A.VALUE)", "VALUE<>''"));

            //lblroomcapacity.Text = actualcapacity.ToString();
            //lbltotcount.Text = totalstud;
            //hdStudCount.Value = totalstud;
            BindSeatPlan();
            BindExamCourseList();
            this.RoomDetails();

        }
        else
        {
            pnlProgramName.Visible = false;
            //pnlRoomDetails.Visible = false;
            //divbuttons.Visible = false;
            objCommon.DisplayUserMessage(updplRoom, "Please Select Exam Slot !!!!", this.Page);
        }
    }

    protected void btnDeallocate_Click(object sender, EventArgs e)
    {
        string courseno = string.Empty;
        string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        int slotno = Convert.ToInt32(ddlslot.SelectedValue);

        foreach (ListViewItem lvitem in lvProgramNames.Items)
        {
             CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
             Label lblCourseno = lvitem.FindControl("lblExamCourse") as Label;

             if (chk.Checked)
             {
                 courseno += lblCourseno.ToolTip + ',';
             }
         }
         courseno = courseno.TrimEnd(',');

        CustomStatus cs = (CustomStatus)objSC.DeallocateManualSeatingArrangment(Convert.ToInt32(ddlSession.SelectedValue), slotno, Convert.ToInt32(ddlExamName.SelectedValue), EXAMDATE, Convert.ToInt32(ddlRoom.SelectedValue), (ddlCourse.SelectedIndex > 0 ? ddlCourse.SelectedValue : courseno));
        if (cs.Equals(CustomStatus.RecordDeleted))
        {
            objCommon.DisplayUserMessage(updplRoom, "Deallocated Manual Seating arrangement successfully !!!!", this.Page);
            BindSeatPlan();
            txtSelectedStudStrengh.Text = "";
            txtSelctedRommCap.Text = "";
            txtRemRoomCapacity.Text = string.Empty;
            txtTSelected.Text = string.Empty;
            pnlFilteredStudent.Visible = false;
            lvFilteredStudent.DataSource = null;
            lvFilteredStudent.DataBind();
            ddlRoom.SelectedIndex = 0;
            ddlCourse.SelectedIndex = 0;
            ddlCourse.Enabled = true;
            btnDeallocate.Visible = false;
            btnConfigure.Visible = false;
            hdStudCount.Value = "0";
            ddlSession.SelectedValue = "0";
            ddlslot.SelectedValue = "0";
            ddlExamName.SelectedIndex = 0;
            txtExamDate.Text = "";
            lvProgramNames.DataSource = null;
            lvProgramNames.DataBind();
            pnlProgramName.Visible = false;
            ViewState["ccodes"] = null;
        }
        else
        {
            objCommon.DisplayUserMessage(updplRoom, "Failed To Deallocate Manual Seating arrangement !!!!", this.Page);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void chckProgram_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string ccodes = string.Empty;
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int count = 0;
            txtTSelected.Text = string.Empty;
            pnlFilteredStudent.Visible = false;
            lvFilteredStudent.DataSource = null;
            lvFilteredStudent.DataBind();

            foreach (ListViewDataItem lvitem in lvProgramNames.Items)
            {
                CheckBox chkprog = lvitem.FindControl("chckProgram") as CheckBox;
                if (chkprog.Checked)
                {
                    count++;
                }
            }

            if(count == 0)
                ddlCourse.Enabled = true;
            else
                ddlCourse.Enabled = false;

            foreach (ListViewDataItem lvitem in lvProgramNames.Items)
            {
                Label lblccodes = lvitem.FindControl("lblCcode") as Label;
                CheckBox chkprog = lvitem.FindControl("chckProgram") as CheckBox;
                TextBox txt = lvitem.FindControl("txtSrNo") as TextBox;

                if (chkprog.Checked)
                {
                    ccodes += lblccodes.Text.Trim() + ',';
                    txt.Enabled = true;                   
                }
                else
                {
                    txt.Enabled = false;
                }
            }
            ViewState["ccodes"] = ccodes;
            //DataSet ds = objSC.GetAllManualSeatPlanByExamDate(EXAMDATE, slotno, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), string.Empty, ccodes);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    //lvdetails.DataSource = ds;
            //    //lvdetails.DataBind();
            //    //pnldetails.Visible = true;
            //}
            //else
            //{
            //    //lvdetails.DataSource = null;
            //    //lvdetails.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.chckProgram_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chckroom_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            string roomnos = string.Empty;
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int count = 0;

            //foreach (ListViewDataItem lvitem in lvRoomDetails.Items)
            //{
            //    HiddenField roommnos = lvitem.FindControl("hfRoom") as HiddenField;
            //    CheckBox chkroom = lvitem.FindControl("chckroom") as CheckBox;
            //    TextBox txt = lvitem.FindControl("txtRoomSrNo") as TextBox;

            //    if (chkroom.Checked)
            //    {
            //        roomnos += roommnos.Value.Trim() + ',';
            //        txt.Enabled = true;
            //    }
            //    else
            //    {
            //        txt.Enabled = false;
            //    }
            //}
            DataSet ds = objSC.GetAllManualSeatPlanByExamDate(EXAMDATE, slotno, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), roomnos, ViewState["ccodes"].ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //lvdetails.DataSource = ds;
                //lvdetails.DataBind();
                //pnldetails.Visible = true;
                //foreach (ListViewDataItem lvitem in lvRoomDetails.Items)
                //{
                //    CheckBox chkroom = lvitem.FindControl("chckroom") as CheckBox;

                //    if (chkroom.Checked)
                //    {
                //        count++;
                //    }                   
                //}
                if(count > 0)
                    btnDeallocate.Visible = true;
                else
                    btnDeallocate.Visible = false;
            }
            else
            {
                //lvdetails.DataSource = null;
                //lvdetails.DataBind();
                btnDeallocate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.chckroom_CheckedChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_EXAMDATE=" + (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd") + ",@P_SLOTNO=" + ddlslot.SelectedValue + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IS_MIDS_ENDS="+ddlExamName.SelectedValue;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updplRoom, this.updplRoom.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(1)", "SESSIONNO = " + ddlSession.SelectedValue + " AND IS_MIDS_ENDS = " + ddlExamName.SelectedValue + " AND EXAMDATE = '" + Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") + "' AND SLOTNO = " + ddlslot.SelectedValue + ""));
            GridView gv = new GridView();

            if (count > 0)
            {
                //ShowReport("Manual_Seating_Arrangement", "rptManualSeatingArrangement.rpt");
                DataSet ds = objSC.GetDataForManualSeatingArrangementExcelReport(Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd"), Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                    string attachment = "attachment; filename=ManualSeatingArrangement.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.MS-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage(updplRoom, "No Record Found for this selection !!!!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "No Record Found for this selection !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_SeatingArrangement.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = lvProgramNames.FindControl("chckallProgram") as CheckBox;

        foreach (ListViewDataItem lvitem in lvProgramNames.Items)
        {
            CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
            
            if (ddlCourse.SelectedIndex > 0)
            {
                chk.Enabled = false;
                chkAll.Enabled = false;
            }
            else
            {
                chk.Enabled = true;
                chkAll.Enabled = true;
            }
        }
        txtSelectedStudStrengh.Text = string.Empty;
        pnlFilteredStudent.Visible = false;
        lvFilteredStudent.DataSource = null;
        lvFilteredStudent.DataBind();
        txtTSelected.Text = string.Empty;
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtTSelected.Text = string.Empty;
            string examdate = Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd");
            string coursenos = string.Empty, branchnos = string.Empty, semesternos = string.Empty, degreenos = string.Empty;
            int count = 0;

            foreach (ListViewItem lvitem in lvProgramNames.Items)
            {
                CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
                if (chk.Checked)
                {
                    count++;
                }
            }

            if (ddlCourse.SelectedIndex == 0 && count == 0)
            {
                objCommon.DisplayMessage(this.updplRoom, "Please Select Course !!!!", this.Page);
                return;
            }
            else
            {
                foreach (ListViewItem lvitem in lvProgramNames.Items)
                {
                    CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
                    Label lblBranchno = lvitem.FindControl("lblSchemeName") as Label;
                    Label lblCourseno = lvitem.FindControl("lblExamCourse") as Label;
                    Label lblSemno = lvitem.FindControl("lblSem") as Label;
                    Label lblDegreeno = lvitem.FindControl("lblCcode") as Label;

                    if (chk.Checked)
                    {
                        coursenos += lblCourseno.ToolTip + ',';
                        branchnos += lblBranchno.ToolTip + ',';
                        semesternos += lblSemno.ToolTip + ',';
                        degreenos += lblDegreeno.ToolTip + ',';
                    }
                }
                coursenos = coursenos.TrimEnd(',');
                branchnos = branchnos.TrimEnd(',');
                semesternos = semesternos.TrimEnd(',');
                degreenos = degreenos.TrimEnd(',');

                DataSet ds = objSC.GetStudentListForManualSeatingArrangement(examdate, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlslot.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), (ddlCourse.SelectedIndex > 0 ? ddlCourse.SelectedValue : coursenos), (ddlCourse.SelectedIndex > 0 ? string.Empty : branchnos), (ddlCourse.SelectedIndex > 0 ? string.Empty : semesternos), (ddlCourse.SelectedIndex > 0 ? string.Empty : degreenos));

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    pnlFilteredStudent.Visible = true;
                    lvFilteredStudent.DataSource = ds;
                    lvFilteredStudent.DataBind();
                    btnConfigure.Visible = true;
                    if (ddlCourse.SelectedIndex > 0)
                    {
                        txtSelectedStudStrengh.Text = ds.Tables[0].Rows.Count.ToString();
                    }
                }
                else
                {
                    pnlFilteredStudent.Visible = false;
                    lvFilteredStudent.DataSource = ds;
                    lvFilteredStudent.DataBind();
                    btnConfigure.Visible = false;
                    objCommon.DisplayMessage(this.updplRoom, "Students are already configured for this course(s) !!!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SeatingPlanManual.btnFilter_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string room_capacity = string.Empty, rem_room_capacity = string.Empty, courseno = string.Empty, coursenos = string.Empty;
            int total_rem_room_capacity = 0,check = 0;
            string examdate = Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd");
            if (ddlRoom.SelectedIndex > 0)
            {
                room_capacity = objCommon.LookUp("ACD_ROOM", "ROOMCAPACITY", "ROOMNO = " + ddlRoom.SelectedValue + "");
                txtSelctedRommCap.Text = room_capacity;
                rem_room_capacity = objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(1)", "SESSIONNO =" + ddlSession.SelectedValue + " AND IS_MIDS_ENDS = " + ddlExamName.SelectedValue + " AND ROOMNO = " + ddlRoom.SelectedValue + " AND EXAMDATE = '" + examdate + "' AND SLOTNO = " + ddlslot.SelectedValue);
                total_rem_room_capacity = Convert.ToInt32(room_capacity) - Convert.ToInt32(rem_room_capacity);
                txtRemRoomCapacity.Text = total_rem_room_capacity.ToString();
            }
            else
            {
                txtRemRoomCapacity.Text = total_rem_room_capacity.ToString();
                txtSelctedRommCap.Text = room_capacity;
            }

            foreach (ListViewItem lvitem in lvProgramNames.Items)
            {
                CheckBox chk = lvitem.FindControl("chckProgram") as CheckBox;
                Label lblCourseno = lvitem.FindControl("lblExamCourse") as Label;

                if (chk.Checked)
                {
                    courseno += lblCourseno.ToolTip + ',';
                }
            }
            courseno = courseno.TrimEnd(',');
            coursenos = (ddlCourse.SelectedIndex > 0 ? ddlCourse.SelectedValue : courseno);

            check = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(1)", "SESSIONNO =" + ddlSession.SelectedValue + " AND IS_MIDS_ENDS = " + ddlExamName.SelectedValue + " AND ROOMNO = " + ddlRoom.SelectedValue + " AND COURSENO IN (SELECT VALUE FROM DBO.SPLIT('" + coursenos + "',','))"));
            if (check > 0)
            {
                btnDeallocate.Visible = true;
            }
            else
                btnDeallocate.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SeatingPlanManual.ddlRoom_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chckallProgram_CheckedChanged(object sender, EventArgs e)
    {
        pnlFilteredStudent.Visible = false;
        lvFilteredStudent.DataSource = null;
        lvFilteredStudent.DataBind();
    }

    protected void btnPDFReport_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            count = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(1)", "SESSIONNO = " + ddlSession.SelectedValue + " AND IS_MIDS_ENDS = " + ddlExamName.SelectedValue + " AND EXAMDATE = '" + Convert.ToDateTime(txtExamDate.Text).ToString("yyyy-MM-dd") + "' AND SLOTNO = " + ddlslot.SelectedValue + ""));

            if (count > 0)
            {
                ShowReport("Manual_Seating_Arrangement", "rptManualSeatingArrangement.rpt");  
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "No Record Found for this selection !!!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_SeatingArrangement.btnPDFReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}