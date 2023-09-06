//=================================================================================
// PROJECT NAME  : U-AIMS                                                          
// MODULE NAME   : SEATING PLAN 
// CREATION DATE : 20-APR-2012                                                     
// CREATED BY    : PRIYANKA KABADE               
// MODIFIED DATE :               
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
using System.Data.SqlClient;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_NewSeatingPlan : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();
    Seating objSeating = new Seating();
    
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
        objCommon.FillDropDownList(ddlcollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID DESC");
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
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=RoomConfig.aspx");
            //}
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

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_DEGREENO=" + ddldegree.SelectedValue + ",@P_COLLEGEID=" + ddlcollege.SelectedValue;
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
    private void ShowReportRoomSeats(string reportTitle, string rptFileName, string dayno, string slotno, string roomno, int exp)
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
            divbranch1.Visible = false;
        }
        BindSeatPlan();
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
            if (ddlBranch2.SelectedIndex > 0)
            {
                if (TXTFORSLOT1.Text != TXTFORSLOT2.Text)
                {
                    objCommon.DisplayMessage(updplRoom, "The Examination Slots are different , Please choose Same Slot", this);



                    return;
                }
            }        
         
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int dayno = Convert.ToInt32(ddlDay.SelectedValue);
            //int slot = Convert.ToInt32(ddlSlot.SelectedValue);
            string branchno = string.Empty;
            string semesterno = string.Empty;
            int oddeven = 0;
            int position = 0;
            //aayushi
            int collegeno = Convert.ToInt32(ddlcollege.SelectedValue);
            int degreeno = Convert.ToInt32(ddldegree.SelectedValue);

            if (ddlBranch1.SelectedIndex > 0)
                branchno = branchno + ddlBranch1.SelectedValue + ",";

            if (ddlSemester1.SelectedIndex > 0)
                semesterno = semesterno + ddlSemester1.SelectedValue + ",";

            int roomno = Convert.ToInt32(ddlRoom.SelectedValue);

            if (ddlBranch2.SelectedIndex > 0 && chkAdd1.Checked)
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



            int result = objSC.ConfigureSeatingArrangment(sessionno, oddeven, position, branchno, roomno, semesterno);

            if (result == Convert.ToInt32(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updplRoom, "Seating arrangement done successfully!", this.Page);
            }
            if (ddlSemester1.SelectedIndex > 0)
            {
                DataSet ds = null;
                ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT", "8", "COUNT(distinct IDNO)[all stud]", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + " and SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtallotstud1.Text = ds.Tables[0].Rows[0]["all stud"].ToString();
                    txtremainstud1.Text = (int.Parse(txttotstudb1.Text) - int.Parse(txtallotstud1.Text)).ToString();
                }
            }
            if (ddlSemester2.SelectedIndex > 0)
            {
                DataSet ds = null;
                ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT", "8", "COUNT(distinct IDNO)[all stud]", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue) + " and SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue), "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtallotstud2.Text = ds.Tables[0].Rows[0]["all stud"].ToString();
                    txtremainstud2.Text = (int.Parse(txttotstudb2.Text) - int.Parse(txtallotstud2.Text)).ToString();
                }
            }

         

            getRoomCapacity();
            BindSeatPlan();
            clear();
            

        }
        else
            objCommon.DisplayMessage(updplRoom, "Please Select at least one Branch and semester", this);

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
        if (ddlSession.SelectedIndex > 0 )
            ShowReport("Seating_Arrangement_Roomwise", "rptSeatingPlanRoomwise.rpt", "0", "0");
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session ", this);
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
            objCommon.DisplayMessage(updplRoom, "Please Select Session", this);
    }

    public void clear()
    {
      
        ddldegree.SelectedIndex = 0;
        ddlcollege.SelectedIndex = 0;
        ddlRoom.SelectedIndex = 0;
       
        //aayushi allot student
        DataSet ds = null;
        //ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT", "8", "COUNT(IDNO)[all stud]", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue), "");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtallotstud1.Text = ;
        //}
        //txtremainstud1.Text = (int.Parse(txttotstudb1.Text) - int.Parse(txtallotstud1.Text)).ToString();

      
     


    }
    //MASTER SEATING ARRANGEMENT
    protected void btnMasterSeating_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0)
            ShowReportRoomSeats("Master Seating Arrangement", "rptMasterSeatingPlan.rpt", "0", "0", ddlRoom.SelectedValue, 0);
        else
        
            objCommon.DisplayMessage(updplRoom, "Please Select Session and Room", this);
    }

    protected void btnRoomSeatsEx_Click(object sender, EventArgs e)
    {
        string dayno = string.Empty;
        string slotno = string.Empty;

        ShowReportRoomSeats("Seating_Plan_RoomSeats", "rptSeatingPlanRoomSeats.rpt", "0", "0", ddlRoom.SelectedValue, 1);
    }
    protected void ddlBranch1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch1.SelectedIndex > 0)
        {
           
            //objCommon.FillDropDownList(ddlBranch2, "ACD_EXAM_DATE E INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = E.BRANCHNO)", "DISTINCT E.BRANCHNO", "B.LONGNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "E.BRANCHNO");
            chkAdd1.Enabled = true;
        }
        else
        {
            chkAdd1.Enabled = false;
            chkAdd1.Checked = false;
            divbranch2.Visible = false;
            ddlSemester1.SelectedIndex = 0;
        }
    }
    protected void ddlBranch2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch2.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch3, "ACD_EXAM_DATE E INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = E.BRANCHNO)", "DISTINCT E.BRANCHNO", "B.LONGNAME", "SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue), "E.BRANCHNO");
            chkAdd2.Enabled = true;
        }
        else
        {
            chkAdd2.Enabled = false;
            chkAdd2.Checked = false;
            divbranch3.Visible = false;
            ddlSemester2.SelectedIndex = 0;
        }
    }

    protected void dlSemester2_SelectedIndexChanged(object sender, EventArgs e)
    {

        int result = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_DATE", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue)));

        if (result == 0)
        {
            ddlSemester2.SelectedIndex = 0;
            objCommon.DisplayMessage(updplRoom, "Please configure Timetable for the same selection", this);
            //TXTFORSLOT2.Text = "";
            //txttotstudb2.Text = "";
            //txtremainstud2.Text = "";
            //txtallotstud2.Text = "";
            return;

        }
        DataSet ds = null;

        ds = objCommon.FillDropDown("acd_student_result acd inner join acd_scheme acs on acs.SCHEMENO=acd.SCHEMENO ", "8", " COUNT(DISTINCT ACD.idno)[NO OF STUDENT]", "ACD.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and ACS.BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue) + "and ACD.SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue) + "and acd.REGISTERED=1 " + "and acd.EXAM_REGISTERED=1 ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txttotstudb2.Text = ds.Tables[0].Rows[0]["NO OF STUDENT"].ToString();
        }

        ds = objCommon.FillDropDown(" ACD_EXAM_DATE AED inner join ACD_EXAM_TT_SLOT ATS on AED.SlotNo=ATS.SLOTNO", "8", "ATS.SLOTNAME", "AED.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and AED.BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue) + "and AED.DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + "and AED.SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue), "");
        if (ds.Tables[0].Rows.Count > 0 )
        {
            TXTFORSLOT2.Text = ds.Tables[0].Rows[0]["SLOTNAME"].ToString();
        }

        //ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT", "8", "COUNT(IDNO)[all stud]", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue ), "");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    txtallotstud2.Text = ds.Tables[0].Rows[0]["all stud"].ToString();
        //}

        //// txtremainstud1.Text = (Convert.ToInt32(txtallotstud1) - Convert.ToInt32(txtallotstud1).ToString());

        //txtremainstud2.Text = (int.Parse(txttotstudb2.Text) - int.Parse(txtallotstud2.Text)).ToString();

        if (TXTFORSLOT1.Text != TXTFORSLOT2.Text)
        {
            objCommon.DisplayMessage(updplRoom, "The Examination Slots are different", this);
            ddlSemester2.SelectedIndex = 0;
             return;
        }


       

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
        int result = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_DATE", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch3.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester3.SelectedValue)));

        if (result == 0)
        {
            ddlSemester3.SelectedIndex = 0;
            objCommon.DisplayMessage(updplRoom, "Please configure Timetable for the same selection", this);

            return;

        }
        DataSet ds = null;

        ds = objCommon.FillDropDown(" ACD_EXAM_DATE AED inner join ACD_TIME_SLOT ATS on AED.SlotNo=ATS.SLOTNO", "8", "ATS.SLOTNAME", "AED.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and AED.BRANCHNO=" + Convert.ToInt32(ddlBranch2.SelectedValue) + "and AED.DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + "and AED.SEMESTERNO=" + Convert.ToInt32(ddlSemester2.SelectedValue) + "and acd.REGISTERED=1 " + "and acd.EXAM_REGISTERED=1 ", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            TXTFORSLOT2.Text = ds.Tables[0].Rows[0]["SLOTNAME"].ToString();
        }


       
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
            divbranch2.Visible = true;
            // trArrangement.Visible = true;
        }
        else
        {
            divbranch2.Visible = false;
            //trArrangement.Visible = false;
        }
    }
    protected void chkAdd2_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAdd2.Checked == true)
            divbranch3.Visible = true;
        else
            divbranch3.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
      
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
            //trbranch1.Visible = true;
            //aayyushi
            divbranch1.Visible = true;
        }
        else
        {
            lblActualCapacity.Text = "";
            lbl1.Text = "";
            lbl2.Text = "";
            lbl3.Text = "";
            trRoomCapacity.Visible = false;
            //aayushi
            divbranch1.Visible = true;
        }
    }


    public void BindSeatPlan()
    {
        try
        {
            
            DataSet ds = objSC.GetAllSeatPlan(Convert.ToInt32(ddlRoom.SelectedValue));
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lvdetails.DataSource = ds;
                    lvdetails.DataBind();
                    pnldetails.Visible = true;
                }
                else
                {
                    lvdetails.DataSource = null;
                    lvdetails.DataBind();
                 //   pnldetails.Visible = false;
                }
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

    
        



    protected void ddlcollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcollege.SelectedIndex > 0)
        {

            objCommon.FillDropDownList(ddldegree, "ACD_COLLEGE_DEGREE C INNER JOIN ACD_DEGREE D ON (C.DEGREENO=D.DEGREENO)", "C.DEGREENO", "DEGREENAME", "C.DEGREENO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlcollege.SelectedValue), "DEGREENO");
        }
        else
        {
            objCommon.DisplayMessage("Please Select College Name!", this.Page);
            ddlcollege.Focus();
        }
    }
    //aayushi
    protected void ddldegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch1, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddldegree.SelectedValue, "A.LONGNAME");
            objCommon.FillDropDownList(ddlBranch2, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0  AND B.DEGREENO = " + ddldegree.SelectedValue, "A.LONGNAME");
            objCommon.FillDropDownList(ddlBranch3, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0  AND B.DEGREENO = " + ddldegree.SelectedValue, "A.LONGNAME");
          
          
        }
    }
  
    int oddeven = 0;
    int position = 0;



    protected void ddlSemester1_SelectedIndexChanged(object sender, EventArgs e)
    {


        int result = Convert.ToInt32(objCommon.LookUp("ACD_EXAM_DATE", "COUNT(1)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + " AND BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue)));

        if (result == 0)
        {
            ddlSemester1.SelectedIndex = 0;
            objCommon.DisplayMessage(updplRoom, "Please configure Timetable for the same selection", this);
            txtallotstud1.Text = "";
            TXTFORSLOT1.Text = "";
            txtremainstud1.Text = "";
            txttotstudb1.Text = "";
           

            return;
        }
        
        //aayushi total student
        DataSet ds = null;
        //ds = objCommon.FillDropDown("acd_student_result acd inner join acd_scheme acs on acs.SCHEMENO=acd.SCHEMENO ", "acd.idno", "count(acd.idno)[No Of Student] ,acs.branchno,acd.SESSIONNO,acd.SEMESTERNO", "ACD.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and ACS.BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + "and ACD.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + " GROUP BY ACS.BRANCHNO,ACD.SESSIONNO,ACD.SEMESTERNO ", "");
        ds = objCommon.FillDropDown("acd_student_result acd inner join acd_scheme acs on acs.SCHEMENO=acd.SCHEMENO ", "8", " COUNT( DISTINCT ACD.idno)[NO OF STUDENT]", "ACD.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and ACS.BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + "and ACD.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue) + "and acd.REGISTERED=1 " + "and acd.EXAM_REGISTERED=1 ", "");
       

       if (ds.Tables[0].Rows.Count > 0)
       {
           txttotstudb1.Text = ds.Tables[0].Rows[0]["NO OF STUDENT"].ToString();
          
       }
       //aayushi Slot
       ds = objCommon.FillDropDown(" ACD_EXAM_DATE AED inner join ACD_EXAM_TT_SLOT ATS on AED.SlotNo=ATS.SLOTNO", "8", "ATS.SLOTNAME", "AED.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "and AED.BRANCHNO=" + Convert.ToInt32(ddlBranch1.SelectedValue) + "and AED.DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + "and AED.SEMESTERNO=" + Convert.ToInt32(ddlSemester1.SelectedValue), "");
       if (ds.Tables[0].Rows.Count > 0)
       {
           TXTFORSLOT1.Text = ds.Tables[0].Rows[0]["SLOTNAME"].ToString();
       }
       //aayushi allot student
      
       

    }


    //aayushi student attendence
    protected void studAttenreport_Click(object sender, EventArgs e)
    {

        string dayno = string.Empty;
        string slotno = string.Empty;
        if (ddlSession.SelectedIndex > 0 && ddldegree.SelectedIndex > 0 && ddlRoom.SelectedIndex >0)
            ShowReportAttedence("Student_Attendance_List", "rptExamStudAttendanceSheetAccordRoom.rpt");
        else
            objCommon.DisplayMessage(updplRoom, "Please Select Session/Degree/Block", this);

    }

    private void ShowReportAttedence(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ddldegree.SelectedValue) + " ,@P_ROOMNO=" + Convert.ToInt32(ddlRoom.SelectedValue);
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
   
}

