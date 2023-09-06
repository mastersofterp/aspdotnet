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

public partial class ACADEMIC_SEATINGARRANGEMENT_SeatingPlanNew : System.Web.UI.Page
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
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME AS COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
    }

    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE WITH (NOLOCK)", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updplRoom, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";

        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED WITH (NOLOCK) INNER JOIN ACD_EXAM_TT_SLOT AEIS WITH (NOLOCK) ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    public void BindExamCourseList()
    {
        try
        {
             string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
             DataSet ds = objSC.GetExamCourseListByDate(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt16(ddlslot.SelectedValue), EXAMDATE);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
                    lvExamCoursesOnDate.DataSource = ds;
                    lvExamCoursesOnDate.DataBind();
                    pnlExamCourse.Visible = true;
               
               
            }
            else
            {
                lvExamCoursesOnDate.DataSource = null;
                lvExamCoursesOnDate.DataBind();
                objCommon.DisplayUserMessage(updplRoom, "No Courses Founds On Selected Date!", this.Page);
                return;
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

            DataSet dsroom = objCommon.FillDropDown("ACD_SEATING_PLAN SP WITH (NOLOCK) INNER JOIN ACD_ROOM R WITH (NOLOCK) ON(SP.ROOMNO=R.ROOMNO AND SP.COLLEGE_ID=R.COLLEGE_ID)", "SP.ROOMNO", "R.ROOMNAME,ROOMCAPACITY,ACTUALCAPACITY,SP.DISABLED_IDS", "SP.COLLEGE_ID = " + ddlCollege.SelectedValue, "R.ROOMNAME");
            if (dsroom != null && dsroom.Tables[0].Rows.Count > 0)
            {
                lvRoomDetails.DataSource = dsroom;
                lvRoomDetails.DataBind();
                pnlRoomDetails.Visible = true;
            }
            else
            {
                lvRoomDetails.DataSource = null;
                lvRoomDetails.DataBind();
                pnlRoomDetails.Visible = false;
            }
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
            DataSet ds = objSC.GetAllSeatPlanByExamDate(EXAMDATE, slotno, ddlCollege.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               
                    lvdetails.DataSource = ds;
                    lvdetails.DataBind();
                    pnldetails.Visible = true;
               
               
            }
            else
            {
                lvdetails.DataSource = null;
                lvdetails.DataBind();
               // objCommon.DisplayUserMessage(updplRoom, "No Students Founds On Selected Date!", this.Page);
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
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int seatArr = rbOnBench.SelectedValue == "" ? 0 : Convert.ToInt32(rbOnBench.SelectedValue);
            int seatType = rbSeatingType.SelectedValue == "" ? 0 : Convert.ToInt32(rbSeatingType.SelectedValue);
            string CSequence = "";
            string SeqBranchno = "";
            string SeqDegreeno = "";
            string Ccodes = "";
            string roomno = string.Empty;
            string roomSq = string.Empty;

            if (seatArr == 0)
            {
                objCommon.DisplayUserMessage(updplRoom, "Please select students on bench", this.Page);               
                return;
            }
            if (seatType == 0)
            {
                objCommon.DisplayUserMessage(updplRoom, "Please select students arrangement type", this.Page);
                return;
            }

            foreach (ListViewDataItem dataitem in lvExamCoursesOnDate.Items)
            {
                TextBox chk = dataitem.FindControl("txtSrNo") as TextBox;
                Label lblCcode = dataitem.FindControl("lblCcode") as Label;
                Label lblbranch = dataitem.FindControl("lblbranch") as Label;
                if (!string.IsNullOrEmpty(chk.Text.Trim()))
                {
                    Ccodes += lblCcode.Text.Trim() + ',';
                    SeqBranchno += lblbranch.ToolTip.Trim() + ',';
                    SeqDegreeno += lblCcode.ToolTip.Trim() + ',';
                    CSequence += chk.Text + ',';                    
                }
                else
                {
                    objCommon.DisplayUserMessage(updplRoom, "Please enter Course Sequence", this.Page);
                    chk.Focus();
                    return;
                }
                    
            }

            // get rooms
            foreach (ListViewDataItem dataitem in lvRoomDetails.Items)
            {

                HiddenField roommnos = dataitem.FindControl("hfRoom") as HiddenField;
                CheckBox chkroom = dataitem.FindControl("chckroom") as CheckBox;
                TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
                if (chkroom.Checked == true)
                {
                    roomno += roommnos.Value + ',';
                    roomSq += txtbox.Text.Trim() + ',';
                }
            }
                   
            //CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno, Convert.ToInt32(ddlCollege.SelectedValue));
            CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno, Convert.ToInt32(ddlCollege.SelectedValue), seatArr, seatType, Ccodes, CSequence, roomno, roomSq, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue), SeqBranchno, SeqDegreeno);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(updplRoom, "Seating arrangement done successfully!", this.Page);
                BindSeatPlan();
                lblroomcapacity.Text = "";
                lbltotcount.Text = "";
                hdStudCount.Value = "0";
                ddlSession.SelectedValue = "0";
                ddlslot.SelectedValue = "0";
                txtExamDate.Text = "";
                ddlCollege.SelectedIndex = 0;
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "Failed To Configure Seating arrangement", this.Page);
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
            string totalstud = string.Empty;
            string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            int IsMidSem_Or_EndSem = 0;
            IsMidSem_Or_EndSem = Convert.ToInt32(ddlExamName.SelectedValue);
            if (IsMidSem_Or_EndSem == 1)//mid sem
            {
                totalstud = objCommon.LookUp("ACD_EXAM_DATE AED WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT ASR WITH (NOLOCK) ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND AED.SUBID=1");
            }
            else if (IsMidSem_Or_EndSem == 2)//end sem
            {
                totalstud = objCommon.LookUp("ACD_EXAM_DATE AED WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT ASR WITH (NOLOCK) ON (ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO) INNER JOIN ACD_STUDENT S WITH (NOLOCK) ON(ASR.IDNO=S.IDNO)", "COUNT(DISTINCT ASR.IDNO)", "ISNULL(CANCEL,0)=0 AND ACCEPTED = 1 AND EXAM_REGISTERED = 1 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND S.COLLEGE_ID=" + ddlCollege.SelectedValue + " AND AED.SUBID=1");
            }
            else
            {
            }
            int actualcapacity = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_PLAN WITH (NOLOCK)", "SUM (ACTUAL_CAPACITY) ", "ROOMNO>0 AND COLLEGE_ID=" + ddlCollege.SelectedValue));
            int disabled = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_PLAN WITH (NOLOCK) CROSS APPLY DBO.SPLIT(DISABLED_IDS,',') A", "COUNT(A.VALUE)", "VALUE<>''"));

           // (lblroomcapacity.Text) = (actualcapacity - disabled).ToString();
            lblroomcapacity.Text = actualcapacity.ToString();
            lbltotcount.Text = totalstud;
            hdStudCount.Value = totalstud;
            BindSeatPlan();
            BindExamCourseList();
            this.RoomDetails();
        }
        else
        {
            pnlExamCourse.Visible = false;
            pnlRoomDetails.Visible = false;
        }
    }

    protected void rbOnBench_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rbSeatingType.Items.Clear();
            if (rbOnBench.SelectedValue == "1")
            {
                rbSeatingType.Items.Add(new ListItem("Continue Seating", "1"));
                rbSeatingType.Items.Add(new ListItem("Alternate Seating", "2"));
            }
            else if (rbOnBench.SelectedValue == "2")
            {
                rbSeatingType.Items.Add(new ListItem("Continue Seating", "1"));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlslot.SelectedIndex = 0;
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}