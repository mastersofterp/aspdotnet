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

public partial class ACADEMIC_SEATINGARRANGEMENT_SeatingArrangment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomConfigController objRC = new RoomConfigController();
    RoomConfig objRoom = new RoomConfig();
    SeatingController objSc = new SeatingController();
    //int roomno=0;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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

    private void PopulateDropDownList()
    {

        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");

        objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlSemester2, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlSemester3, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        //objCommon.FillDropDownList(ddlDay, "ACD_EXAM_DATE", "DISTINCT DAYNO", "DAYNO AS DAY", "", "DAYNO");
        //objCommon.FillDropDownList(ddlSlot, "ACD_EXAM_DATE", "DISTINCT SLOTNO", "SLOTNO AS SLOT", "", "SLOTNO");
        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM", "ROOMNO", "ROOMNAME", "ROOMNO > 0 AND ROWS IS NOT NULL AND COLUMNS	IS NOT NULL AND ACTUALCAPACITY IS NOT NULL", "ROOMNO");

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
        }
    }
    //SHOW REPORT 'ROOMWISE' & 'ROOM STASTICAL'
    private void ShowReport(string reportTitle, string rptFileName, string dayno, string slotno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO= " + dayno + ",@P_SLOTNO= " + slotno;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updplRoom, this.updplRoom.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CertificateMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //SHOW REPORT SEATING ARRNAGEMENT(SEATING POSITIONS) ROOM WISE
    private void ShowReportRoomSeats(string reportTitle, string rptFileName, string dayno, string slotno, string roomno,int exp)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            if (exp == 1) { url += "&exporttype=xls"; url += "&filename=seat"; }
            else
                url += "pagetitle=" + reportTitle;
            
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DAYNO= " + dayno + ",@P_SLOTNO= " + slotno + ",@P_ROOMNO= " + roomno;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updplRoom, this.updplRoom.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CertificateMaster.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion
    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoom.SelectedIndex > 0)
        {
            getRoomCapacity();
        }
        else
        {
            lblActualCapacity.Text = "";
            lbl1.Text = "";
            lbl2.Text = "";
            lbl3.Text = "";
            trRoomCapacity.Visible = false;
            trbranch1.Visible = false;
        }
    }
    protected void btnConfigure_Click(object sender, EventArgs e)
    {
        if (ddlBranch1.SelectedIndex > 0 && ddlSemester1.SelectedIndex > 0)
        {
            if (ddlBranch2.SelectedIndex > 0 && ddlSemester2.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updplRoom, "Please select semester for branch 2", this);
                return;
            }
            if (ddlBranch3.SelectedIndex > 0 && ddlSemester3.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updplRoom, "Please select semester for branch 3", this);
                return;
            }
            //if(ddlArrangement.SelectedIndex == 0)
            //{
            //    objCommon.DisplayMessage(updplRoom, "Please select Arrangement", this);
            //    return;
            //}
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int dayno = Convert.ToInt32(ddlDay.SelectedValue);
            //int slot = Convert.ToInt32(ddlSlot.SelectedValue);
            string branchno = string.Empty;
            string semesterno = string.Empty;
            int oddeven = 0;
            int position = 0;
           
            if (ddlBranch1.SelectedIndex > 0)
                branchno = branchno + ddlBranch1.SelectedValue + ",";

            if (ddlSemester1.SelectedIndex > 0)
                semesterno = semesterno + ddlSemester1.SelectedValue + ",";

            int roomno = Convert.ToInt32(ddlRoom.SelectedValue);

            if (ddlBranch2.SelectedIndex > 0  && chkAdd1.Checked)
            {
                if (ddlBranch2.SelectedIndex > 0)
                {
                    branchno = branchno + ddlBranch2.SelectedValue + ",";

                    if (ddlSemester2.SelectedIndex > 0)
                        semesterno = semesterno + ddlSemester2.SelectedValue + ",";
                }
                if (ddlArrangement.SelectedValue == "1")
                {
                    oddeven = 1;
                    position = 2;
                }
                else
                {
                    oddeven = 2;
                    position = 1;
                }

                if (chkAdd2.Checked)
                {
                    if (ddlBranch3.SelectedIndex > 0)
                    {
                        branchno = branchno + ddlBranch3.SelectedValue + ",";
                        if (ddlSemester3.SelectedIndex > 0)
                            semesterno = semesterno + ddlSemester3.SelectedValue + ",";
                    }
                    if (ddlArrangement.SelectedValue == "1")
                    {
                        oddeven = 1;
                        position = 2;
                    }
                    else
                    {
                        oddeven = 2;
                        position = 1;
                    }
                }



            }
            else
            {
                oddeven = 1;
                position = 1;
            }

          ///  int degreeno = 0;

            //int result = objSc.ConfigureSeatingArrangment(sessionno, oddeven, position, branchno, roomno, semesterno, degreeno);

            //if (result == Convert.ToInt32(CustomStatus.RecordSaved))
            //{
            //    objCommon.DisplayMessage(updplRoom, "Seating arrangement done successfully!", this.Page);
            //}

            getRoomCapacity();
        }
        else
            objCommon.DisplayMessage(updplRoom,"Please Select at least one Branch and semester",this);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //REPORT SEATING PLAN ROOMWISE
    protected void btnRoomwise_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        //IF EXAM SELECTION IS DONE ELSE 0
        if (ddlSession.SelectedIndex > 0 && ddlRoom.SelectedIndex > 0)
            ShowReport("Seating_Arrangement_Roomwise", "rptSeatingPlanRoomwise.rpt", "0", "0");
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session and Room", this);
    }

    //REPORT SEATING PLAN STASTICS FOR THE SEATS OF ROOMS
    protected void btnStastical_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0)
            ShowReport("Seating_Arrangement_Stastical", "rptSeatingPlanStatistical.rpt", "0", "0");
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session", this);
    }

    //REPORT SEATING PLAN STASTICS FOR THE SEATS OF ROOMS
    protected void btnConsolidateSeating_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0)
            ShowReport("Consolidate Seating Plan", "rptConsolidateSeatingPlanStatistical.rpt", "0", "0");
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session", this);
    }

    //ACTUAL SEATING ARRANGEMENT
    protected void btnRoomSeats_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0)
            ShowReportRoomSeats("Seating_Plan_RoomSeats", "rptSeatingPlanRoomSeats.rpt", "0", "0", ddlRoom.SelectedValue, 0);
        else
            objCommon.DisplayMessage(updplRoom,"Please Select Session",this);
    }

    //MASTER SEATING ARRANGEMENT
    protected void btnMasterSeating_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0)
            ShowReportRoomSeats("Master Seating Arrangement", "rptMasterSeatingPlan.rpt", "0", "0", ddlRoom.SelectedValue, 0);
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session", this);
    }

    protected void btnRoomSeatsEx_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;

        ShowReportRoomSeats("Seating_Plan_RoomSeats", "rptSeatingPlanRoomSeats.rpt", "0", "0", ddlRoom.SelectedValue,1);
    }
    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch1.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch2, "ACD_EXAM_DATE E INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = E.BRANCHNO)", "DISTINCT E.BRANCHNO", "B.LONGNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "E.BRANCHNO");
            chkAdd1.Enabled = true;
        }
        else
        {
            chkAdd1.Enabled = false;
            chkAdd1.Checked = false;
            trbranch2.Visible = false;
            ddlSemester1.SelectedIndex = 0;
        }
    }
    protected void ddlBranch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch2.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch3, "ACD_EXAM_DATE E INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = E.BRANCHNO)", "DISTINCT E.BRANCHNO", "B.LONGNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) , "E.BRANCHNO");
            chkAdd2.Enabled = true;
        }
        else
        {
            chkAdd2.Enabled = false;
            chkAdd2.Checked = false;
            trbranch3.Visible = false;
            ddlSemester2.SelectedIndex = 0;
        }
    }

    protected void dlSemester2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch1.SelectedValue == ddlBranch2.SelectedValue)
        {
            if (ddlSemester1.SelectedValue == ddlSemester2.SelectedValue)
            {
                objCommon.DisplayMessage(updplRoom, "This semester already selected for same branch", this);
                ddlSemester2.SelectedValue = "0";
                ddlSemester2.Focus();
            }
        }
    }

    protected void dlSemester3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch1.SelectedValue == ddlBranch3.SelectedValue)
        {
            if (ddlSemester1.SelectedValue == ddlSemester3.SelectedValue)
            {
                objCommon.DisplayMessage(updplRoom, "This semester already selected for same branch", this);
                ddlSemester3.SelectedValue = "0";
                ddlSemester3.Focus();
                ddlSemester3.SelectedIndex = 0;
            }
        }
        else if (ddlBranch2.SelectedValue == ddlBranch3.SelectedValue)
        {
            if (ddlSemester2.SelectedValue == ddlSemester3.SelectedValue)
            {
                objCommon.DisplayMessage(updplRoom, "This semester already selected for same branch", this);
                ddlSemester3.SelectedValue = "0";
                ddlSemester3.Focus();
            }
        }
    }

    protected void chkAdd1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdd1.Checked == true)
        {
            trbranch2.Visible = true;
           // trArrangement.Visible = true;
        }
        else
        {
            trbranch2.Visible = false;
            //trArrangement.Visible = false;
        }
    }
    protected void chkAdd2_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdd2.Checked == true)
            trbranch3.Visible = true;
        else
            trbranch3.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch1, "ACD_EXAM_DATE E INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = E.BRANCHNO)", "DISTINCT E.BRANCHNO", "B.LONGNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "E.BRANCHNO");
        }
    }
    private void getRoomCapacity()
    {
        DataSet ds = objCommon.FillDropDown("ACD_ROOM R INNER JOIN ACD_SEATING_PLAN P ON (R.ROOMNO = P.ROOMNO)LEFT OUTER JOIN ACD_SEATING_ARRANGEMENT S ON (R.ROOMNO = S.ROOMNO AND SESSIONNO = " + ddlSession.SelectedValue + "  )", "DISTINCT ( ACTUALCAPACITY + (CASE ISNULL(P.DISABLED_IDS,'') WHEN '' THEN 0  ELSE (SELECT COUNT(*) FROM  [DBO].[FN_SPLIT](P.DISABLED_IDS,',')) END)) ACTUAL,(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 1 AND SR.ROOMNO = R.ROOMNO)ONE,(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 2  AND SR.ROOMNO = R.ROOMNO)TWO", "(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 3 AND SR.ROOMNO = R.ROOMNO)THREE", "R.ROOMNO =" + Convert.ToInt32(ddlRoom.SelectedValue) + " GROUP BY  R.ROOMNO,ACTUALCAPACITY,DISABLED_IDS,ODDEVEN", "");
        //DataSet ds = objCommon.FillDropDown("ACD_ROOM R INNER JOIN ACD_SEATING_PLAN P ON (R.ROOMNO = P.ROOMNO)LEFT OUTER JOIN ACD_SEATING_ARRANGEMENT S ON (R.ROOMNO = S.ROOMNO AND SESSIONNO = " + ddlSession.SelectedValue + "  )", "DISTINCT ( ACTUALCAPACITY + (SELECT COUNT(*) FROM  [DBO].[FN_SPLIT](P.DISABLED_IDS,','))) ACTUAL,(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 1 AND SR.ROOMNO = R.ROOMNO)ONE,(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 2  AND SR.ROOMNO = R.ROOMNO)TWO", "(SELECT COUNT(DISTINCT SR.SEATNO ) FROM ACD_SEATING_ARRANGEMENT SR  WHERE ODDEVEN = 3 AND SR.ROOMNO = R.ROOMNO)THREE", "R.ROOMNO =" + Convert.ToInt32(ddlRoom.SelectedValue) + " GROUP BY  R.ROOMNO,ACTUALCAPACITY,DISABLED_IDS,ODDEVEN", "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblActualCapacity.Text = ds.Tables[0].Rows[0]["ACTUAL"].ToString();
            lbl1.Text = ds.Tables[0].Rows[0]["ONE"].ToString();
            lbl2.Text = ds.Tables[0].Rows[0]["TWO"].ToString();
            lbl3.Text = ds.Tables[0].Rows[0]["THREE"].ToString();
            trRoomCapacity.Visible = true;
            trbranch1.Visible = true;
        }
        else
        {
            lblActualCapacity.Text = "";
            lbl1.Text = "";
            lbl2.Text = "";
            lbl3.Text = "";
            trRoomCapacity.Visible = false;
            trbranch1.Visible = true;
        }
    }
    
}
