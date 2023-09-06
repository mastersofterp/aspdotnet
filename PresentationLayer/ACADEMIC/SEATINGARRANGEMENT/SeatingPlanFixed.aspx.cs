using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

public partial class ADMINISTRATION_SeatingPlanFixed : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();  

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
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME+'('+ CODE +')' as COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
        objCommon.FillDropDownList(ddlslot, "ACD_EXAM_TT_SLOT WITH (NOLOCK)", "SLOTNO", "SLOTNAME", "", "SLOTNO");
        
    }   
  

    public void RoomDetails()
    {
        try
        {

            //DataSet dsroom = objCommon.FillDropDown("ACD_SEATING_PLAN SP INNER JOIN ACD_ROOM R ON(SP.ROOMNO=R.ROOMNO AND SP.COLLEGE_ID=R.COLLEGE_ID)", "SP.ROOMNO", "R.ROOMNAME,ROOMCAPACITY,ACTUALCAPACITY,SP.DISABLED_IDS", "SP.COLLEGE_ID = " + ddlCollege.SelectedValue, "R.ROOMNAME DESC"); // COMMENTED ON 13-03-2020 BY VAISHALI
            DataSet dsroom = objCommon.FillDropDown("ACD_SEATING_PLAN SP WITH (NOLOCK) INNER JOIN ACD_ROOM R WITH (NOLOCK) ON(SP.ROOMNO=R.ROOMNO)", "SP.ROOMNO", "R.ROOMNAME,ROOMCAPACITY,ACTUALCAPACITY,SP.DISABLED_IDS", "", "R.ROOMNAME"); // MODIFIED ON 13-03-2020 BY VAISHALI
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
           
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int college_Id = ddlCollege.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlCollege.SelectedValue);
            int semesterno = ddlSem.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlSem.SelectedValue);
            //DataSet ds = objSC.GetAllFixedSeatPlan(Convert.ToInt32(ddlSession.SelectedValue), semesterno,slotno, college_Id); //COMMENTED ON 13-03-2020 BY VAISHALI
            DataSet ds = objSC.GetAllFixedSeatPlan(Convert.ToInt32(ddlSession.SelectedValue), slotno); // modified ON 13-03-2020 BY VAISHALI
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
            int SemeterNo = Convert.ToInt16(ddlSem.SelectedValue);
            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int seatArr = rbOnBench.SelectedValue == "" ? 0 : Convert.ToInt32(rbOnBench.SelectedValue);
            int seatType = rbSeatingType.SelectedValue == "" ? 0 : Convert.ToInt32(rbSeatingType.SelectedValue);
            string SchemeSeq = "";
            string SchemeNos = "";
            string roomno = string.Empty;
            string roomSq = string.Empty;
            int countProgram = 0;
            int countRoom = 0;

            if (seatArr == 0)
            {
                objCommon.DisplayUserMessage(updplRoom, "Please select seating on bench", this.Page);
                return;
            }
            if (seatType == 0)
            {
                objCommon.DisplayUserMessage(updplRoom, "Please select seating arrangement type", this.Page);
                return;
            }
            if (ddlExamName.SelectedValue == "0")
            {
                objCommon.DisplayUserMessage(updplRoom, "Please Select Exam Name !!!!", this.Page);
                return;
            }

            foreach (ListViewDataItem daitem in lvProgramNames.Items)
            {
                CheckBox chk = daitem.FindControl("chckProgram") as CheckBox;
                if (chk.Checked)
                {
                    countProgram++;
                }
            }

            if (countProgram != 0)
            {
                foreach (ListViewDataItem dataitem in lvProgramNames.Items)
                {

                    TextBox chk = dataitem.FindControl("txtSrNo") as TextBox;
                    HiddenField lblSchemeno = dataitem.FindControl("hfSchemeno") as HiddenField;
                    CheckBox chkprog = dataitem.FindControl("chckProgram") as CheckBox;
                    if (chkprog.Checked == true && string.IsNullOrEmpty(chk.Text.Trim()))
                    {
                        objCommon.DisplayUserMessage(updplRoom, "Please enter scheme sequence.", this.Page);
                        chk.Focus();
                        return;
                    }

                    if (chkprog.Checked == true && !string.IsNullOrEmpty(chk.Text.Trim()))
                    {
                        SchemeNos += lblSchemeno.Value + ',';
                        SchemeSeq += chk.Text + ',';
                    }

                }
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "Please Select atleast one Program !!!!", this.Page);
                return;
            }

            foreach (ListViewDataItem daitem in lvRoomDetails.Items)
            {
                CheckBox chk = daitem.FindControl("chckroom") as CheckBox;
                if (chk.Checked)
                {
                    countRoom++;
                }
            }
            // get rooms
            if (countRoom != 0)
            {
                foreach (ListViewDataItem dataitem in lvRoomDetails.Items)
                {

                    HiddenField roommnos = dataitem.FindControl("hfRoom") as HiddenField;
                    CheckBox chkroom = dataitem.FindControl("chckroom") as CheckBox;
                    TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;

                    if (chkroom.Checked == true && string.IsNullOrEmpty(txtbox.Text.Trim()))
                    {
                        objCommon.DisplayUserMessage(updplRoom, "Please enter room sequence.", this.Page);
                        txtbox.Focus();
                        return;
                    }
                    if (chkroom.Checked == true && !string.IsNullOrEmpty(txtbox.Text.Trim()))
                    {
                        roomno += roommnos.Value + ',';
                        roomSq += txtbox.Text.Trim() + ',';
                    }
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "Please Select atleast one Block !!!!", this.Page);
                return;
            }

            //CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentFixed(sessionno, SemeterNo, slotno, Convert.ToInt32(ddlCollege.SelectedValue), seatArr, seatType, SchemeNos, SchemeSeq, roomno, roomSq, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue)); // COMMENTED ON 13-03-2020 BY VAISHALI
            CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentFixed(sessionno, slotno, seatArr, seatType, SchemeNos, SchemeSeq, roomno, roomSq, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue));  // MODIFIED ON 13-03-2020 BY VAISHALI
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(updplRoom, "Seating arrangement done successfully!", this.Page);
                BindSeatPlan();
                //lblroomcapacity.Text = "";
                //lbltotcount.Text = "";
                //hdStudCount.Value = "0";
                //ddlSession.SelectedValue = "0";
                //ddlslot.SelectedValue = "0";
                //ddlCollege.SelectedIndex = 0;
            }
            else if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayUserMessage(updplRoom, "Room is already Configured for the students !!!!", this.Page);
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

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT SR WITH (NOLOCK) INNER JOIN ACD_SEMESTER S WITH (NOLOCK) ON (S.SEMESTERNO=SR.SEMESTERNO)", "DISTINCT SR.SEMESTERNO", "SEMESTERNAME", "SR.SEMESTERNO > 0 AND SR.SESSIONNO=" + ddlSession.SelectedValue, "SEMESTERNAME ASC");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindProgrammes();
            this.RoomDetails();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void BindProgrammes()
    {
        try
        {

            //DataSet ds = objSC.Get_ProgramsNameForSeating(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue)); //COMMENTED ON 13-03-2020 BY VAISHALI
            DataSet ds = objSC.Get_ProgramsNameForSeating(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlExamName.SelectedValue)); // MODIFIED ON 13-03-2020 BY VAISHALI
            if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               lvProgramNames.DataSource = ds;
               lvProgramNames.DataBind();
               pnlProgramName.Visible = true;

           }
           else
           {
               lvProgramNames.DataSource = null;
               lvProgramNames.DataBind();
               pnlProgramName.Visible = false;
           }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlslot.SelectedIndex > 0)
        {
            try
            {
                BindSeatPlan();
            }
            catch (Exception ex)
            {
            }
           // this.RoomDetails();
        }
        else
        {
           // pnlProgramName.Visible = false;
            //pnlRoomDetails.Visible = false;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindProgrammes();
            this.RoomDetails();
        }
        catch (Exception ex)
        {
        }
    }
}