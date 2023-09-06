﻿using System;
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
    SeatingArrangementController objSC = new SeatingArrangementController();
    Seating objSeating = new Seating();
    int seatstatus;
    int coschno;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    #region
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
                getExamdate();
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -

                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
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

    #endregion

    #region
    private void PopulateDropDownList()
    {
        DataSet ds = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "COLLEGE_IDS,DEGREENO", "BRANCH,SEMESTER,SESSION_NO", "STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["College_ids"] = ds.Tables[0].Rows[0]["COLLEGE_IDS"].ToString();
            ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["Branchno"] = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            ViewState["Semesterno"] = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
        }//Added by lalit 16-01-2023

        int coschno = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO,COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])));
        DataSet ds1 = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(coschno));
        if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0] != null)
        {
            ViewState["degreeno"] = Convert.ToInt32(ds1.Tables[0].Rows[0]["DEGREENO"]).ToString();
            ViewState["branchno"] = Convert.ToInt32(ds1.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            ViewState["college_id"] = Convert.ToInt32(ds1.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            ViewState["schemeno"] = Convert.ToInt32(ds1.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
            //ddlSession.Focus();


        }


    }
    #endregion

    #region Comment
    //protected void txtExamDate_TextChanged(object sender, EventArgs e)
    //{
    //    string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
    //    string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
    //  // // string dates = DateTime.Now.ToString("yyyy-MM-dd");


    //    DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));          
    //    DateTime examDates = DateTime.Parse(EXAMDATE);

    //    if (examDates >= currentdate)
    //    {

    //        if (a.ToString() == "0")
    //        {
    //            objCommon.DisplayUserMessage(updplRoom, "No Exams Are Conducted on Selected Date", this.Page);
    //            ddlslot.SelectedValue = "0";

    //        }
    //        else
    //        {
    //            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this,"Previous Date are not Allowed!", this);           

    //    }
    //}
    #endregion

    #region BindSeatPlan
    public void BindSeatPlan()
    {
        try
        {
            // string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
            string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
            //string EXAMDA = ((ddlExamdate.Text));
            // string date = EXAMDAT.ToString();
            // string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");

            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            int collegeId = Convert.ToInt32(Convert.ToInt32(ViewState["college_id"]));

            DataSet ds = objSC.GetAllSeatPlanByExamDate_CRESCENT(EXAMDATE, slotno);   //, ExamType
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lvdetails.DataSource = ds;
                    lvdetails.DataBind();
                    pnldetails.Visible = true;
                    btnDeallocate.Visible = true;
                }
                else
                {
                    lvdetails.DataSource = null;
                    lvdetails.DataBind();
                    //   pnldetails.Visible = false;
                    btnDeallocate.Visible = false;
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

    #endregion

    #region configure
    protected void btnConfigure_Click(object sender, EventArgs e)
    {

        try
        {
            if (RadioButton1.Checked == true || RadioButton2.Checked == true)
            {
                if (RadioButton1.Checked == true)
                {
                    if (Convert.ToInt32(lblroomcapacity.Text) >= Convert.ToInt32(lbltotcount.Text))
                    {
                        //tring EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
                        //string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);           
                        //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");

                        string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
                        string EXAMDATES = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
                        string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

                        int slotno = Convert.ToInt32(ddlslot.SelectedValue);
                        int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

                        int uano = Convert.ToInt32(Session["userno"]);
                        int orgno = Convert.ToInt32(Session["OrgId"]);
                        int collegeid = Convert.ToInt32(Convert.ToInt32(ViewState["college_id"]));

                        if (RadioButton1.Checked == true)
                        {
                            seatstatus = 1;
                        }
                        else if (RadioButton2.Checked == true)
                        {
                            seatstatus = 2;
                        }
                        DataSet ds = null;
                        //  // ds = objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno);

                        // string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND CONVERT(NVARCHAR(50),EXAMDATE,103)='" + EXAMDATES + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1
                        string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE CAST(EXAMDATE AS DATE) ='" + EXAMDATE + "')");  //AND AED.TH_PR=1
                        string actualcapacity = objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0");
                        lblroomcapacity.Text = actualcapacity;
                        lbltotcount.Text = totalstud;
                        int stud = Convert.ToInt32(totalstud);
                        int examhall = Convert.ToInt32(actualcapacity);

                        string roomno = string.Empty;
                        string roomSq = string.Empty;
                        //string CheckConfigure = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1
                        
                        if (stud <= examhall)   // 65 <= 80                
                        {
                            int SeatingCheck = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(*)", "CAST(EXAMDATE AS DATE) ='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue));    //AND AED.TH_PR=1

                            if (SeatingCheck <= 0)
                            {
                                CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise_Crescent(EXAMDATE, slotno, 0, uano, Convert.ToInt32(seatstatus), orgno, collegeid);
                                //CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno, Convert.ToInt32(ddlCollege.SelectedValue), seatArr, seatType, Ccodes, CSequence, roomno, roomSq, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue), SeqBranchno, SeqDegreeno);

                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    objCommon.DisplayUserMessage(this.updplRoom, "Seating arrangement done Successfully!", this.Page);
                                    BindSeatPlan();
                                    lblroomcapacity.Text = "";
                                    lbltotcount.Text = "";
                                    ddlslot.SelectedValue = "0";
                                    ddlExamdate.SelectedValue = "0";
                                    ddlCollege.SelectedValue = "0";
                                    lvExamCoursesOnDate.Visible = false;
                                    lvRoomDetails.Visible = false;
                                    btnConfigure.Visible = false;
                                    pnlRoomDetails.Visible = false;
                                    pnlExamCourse.Visible = false;
                                    btnSave.Visible = false;
                                    btnClear.Visible = false;
                                    string totalstudent = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue);  //AND AED.TH_PR=1
                                    string actualcapacityNo = objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0");
                                    lblroomcapacity.Text = actualcapacityNo;
                                    lbltotcount.Text = totalstudent;

                                    lvdetails.Visible = true;
                                    RadioButton1.Checked = false;
                                }
                                else
                                {
                                    objCommon.DisplayUserMessage(this.updplRoom, "Failed To Configure Seating arrangement", this.Page);
                                }
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(this.updplRoom, "Seating Arrangement Already Configure for these Date Slot !", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(this.updplRoom, "Please Makes Sure Room Capacity Must Be Greater Than Students Count!", this.Page);

                           // ddlSession.SelectedValue = "0";
                            ddlslot.SelectedValue = "0";
                            //txtExamDate.Text = "";
                            ddlExamType.SelectedValue = "-1";
                            ddlCollege.SelectedValue = "0";
                           
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updplRoom, "Room Capacity Less as Per Student Count !", this.Page);
                    }

                }
                else if (RadioButton2.Checked == true)
                {
                    string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
                    string EXAMDATES = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
                    string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

                    int slotno = Convert.ToInt32(ddlslot.SelectedValue);
                    int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

                    int uano = Convert.ToInt32(Session["userno"]);
                    int orgno = Convert.ToInt32(Session["OrgId"]);
                    int collegeid = Convert.ToInt32(Convert.ToInt32(ViewState["college_id"]));

                    DataSet ds = null;
                    //  // ds = objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno);

                    // string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND CONVERT(NVARCHAR(50),EXAMDATE,103)='" + EXAMDATES + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1
                    string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE CAST(EXAMDATE AS DATE) ='" + EXAMDATE + "')");  //AND AED.TH_PR=1
                    string actualcapacity = objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0");
                    lblroomcapacity.Text = actualcapacity;
                    lbltotcount.Text = totalstud;
                    int stud = Convert.ToInt32(totalstud);
                    int examhall = Convert.ToInt32(actualcapacity);

                    string roomno = string.Empty;
                    string roomSq = string.Empty;
                    if (RadioButton1.Checked == true)
                    {
                        seatstatus = 1;
                    }
                    else if (RadioButton2.Checked == true)
                    {
                        seatstatus = 2;
                    }
                    //string CheckConfigure = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1

                    if (stud <= examhall)   // 65 <= 80                
                    {
                        int SeatingCheck = Convert.ToInt32(objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(*)", "CAST(EXAMDATE AS DATE)='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue));    //AND AED.TH_PR=1

                        if (SeatingCheck <= 0)
                        {
                            CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise_Double_Crescent(EXAMDATE, slotno, 0, uano, Convert.ToInt32(seatstatus), orgno);
                            //CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingArrangmentDateWise(sessionno, EXAMDATE, slotno, Convert.ToInt32(ddlCollege.SelectedValue), seatArr, seatType, Ccodes, CSequence, roomno, roomSq, Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlExamName.SelectedValue), SeqBranchno, SeqDegreeno);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayUserMessage(this.updplRoom, "Seating arrangement done Successfully!", this.Page);
                                BindSeatPlan();
                                lblroomcapacity.Text = "";
                                lbltotcount.Text = "";
                                ddlslot.SelectedValue = "0";
                                ddlExamdate.SelectedValue = "0";
                                ddlCollege.SelectedValue = "0";
                                lvExamCoursesOnDate.Visible = false;
                                lvRoomDetails.Visible = false;
                                btnConfigure.Visible = false;
                                pnlRoomDetails.Visible = false;
                                pnlExamCourse.Visible = false;
                                btnSave.Visible = false;
                                btnClear.Visible = false;
                                string totalstudent = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue);  //AND AED.TH_PR=1
                                string actualcapacityNo = objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0");
                                lblroomcapacity.Text = actualcapacityNo;
                                lbltotcount.Text = totalstudent;

                                lvdetails.Visible = true;
                                RadioButton1.Checked = false;
                            }
                            else
                            {
                                objCommon.DisplayUserMessage(this.updplRoom, "Failed To Configure Seating arrangement", this.Page);
                            }
                        }
                        else
                        {
                            objCommon.DisplayUserMessage(this.updplRoom, "Seating Arrangement Already Configure for these Date Slot !", this.Page);
                        }
                    }
                    else
                    {
                        objCommon.DisplayUserMessage(this.updplRoom, "Please Makes Sure Room Capacity Must Be Greater Than Students Count!", this.Page);

                        ddlslot.SelectedValue = "0";
                        ddlExamType.SelectedValue = "-1";
                        ddlCollege.SelectedValue = "0";
                    }
                }
                else
                {
                    //Triple Seating Plan is in Progress
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updplRoom, "Please Checked Seating Plan!", this.Page);

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
    #endregion

    #region ddlSlot
    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlslot.SelectedIndex) == 0)
        {
            ddlExamType.SelectedValue = "0";
            pnlExamCourse.Visible = false;
            pnlRoomDetails.Visible = false;
            pnldetails.Visible = false;
        }
        else
        {
            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            //string EXAMDATE = (Convert.ToDateTime(txtExamDate.Text)).ToString("yyyy-MM-dd");
            //string EXAMDATE = ddlExamdate.SelectedItem.ToString();
            //DateTime formatDate = DateTime.ParseExact(EXAMDATE, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //EXAMDATE = formatDate.ToString("yyyy-dd-MM");
            //string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND CONVERT(NVARCHAR(20),EXAMDATE,103)='" + ddlExamdate.SelectedItem.Text.Trim() + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1
            string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND EXAMDATE='" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + "' AND slotno=" + ddlslot.SelectedValue);  //AND AED.TH_PR=1

            string statusnew = objCommon.LookUp("ACD_SEATING_ARRANGEMENT SA inner join acd_session_master SM on (SA.sessionno=SM.sessionno)", "isnull(SEAT_STATUS,0)", " EXAMDATE='" + Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd") + "' AND slotno=" + ddlslot.SelectedValue);  //Regarding get Status added by lalit 
            if (statusnew == "1")
            {
                RadioButton1.Checked = true;
            }
            else if (statusnew == "2")
            {
                RadioButton2.Checked = true;
            }
            else
            {
                RadioButton1.Checked = false;
                RadioButton2.Checked = false;
            }
            string actualcapacity = objCommon.LookUp("ACD_SEATING_PLAN", "SUM(ACTUAL_CAPACITY) ", "roomno>0");
            lblroomcapacity.Text = actualcapacity;
            lbltotcount.Text = totalstud;
            hdStudCount.Value = totalstud;
            hfroomcapacity.Value = actualcapacity;


            string Seatcheck = objCommon.LookUp("ACD_SEATING_ARRANGEMENT SA inner join acd_session_master SM on (SA.sessionno=SM.sessionno) ", "COUNT(*)", "EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue);    //AND AED.TH_PR=1
            int countSeatarrang = Convert.ToInt32(Seatcheck);
            if (countSeatarrang == 0)
            {
                // BindSeatPlan();
                BindExamCourseList();
                this.RoomDetails();
                this.RoomMatrixDetails();

                btnConfigure.Visible = false;
                btnDeallocate.Visible = false;
                btnCancel.Visible = false;
                btnClear.Visible = true;
                btnSave.Visible = true;
                //lvdetails.Visible = false;
                ////lvRoomDetails.Visible = true;
                ////lvExamCoursesOnDate.Visible = true;
                //pnlExamCourse.Visible = true;
                //pnlRoomDetails.Visible = true;
                //pnldetails.Visible = false;

                lvRoomDetails.Visible = true;
                lvExamCoursesOnDate.Visible = true;
            }
            else
            {
                BindSeatPlan();
                this.RoomMatrixDetails();
                btnConfigure.Visible = false;
                btnDeallocate.Visible = true;
                btnCancel.Visible = true;
                btnClear.Visible = false;
                btnSave.Visible = false;
                //lvRoomDetails.Visible = false;
                //  lvExamCoursesOnDate.Visible = false;
                // pnldetails.Visible = true;

                // pnlRoomDetails.Visible = false;
                // pnlExamCourse.Visible = false;
                lvdetails.Visible = true;
            }
        }
    }
    #endregion

    #region
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlExamType.SelectedIndex) == 0)
        {
            objCommon.DisplayUserMessage(updplRoom, "Please select Regular/Repeater !!", this.Page);
        }
        else
        {
            string EXAMDATE = (Convert.ToDateTime(ddlslot.Text)).ToString("yyyy-MM-dd");

            string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND (ISNULL(ASR.PREV_STATUS,0)=" + Convert.ToInt32(ddlExamType.SelectedValue) + " OR " + Convert.ToInt32(ddlExamType.SelectedValue) + "=2)" + " AND ISNULL(AED.STATUS,0)=" + Convert.ToInt32(ddlExamType.SelectedValue) + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1
            string actualcapacity = objCommon.LookUp("ACD_SEATING_PLAN", "SUM (ACTUAL_CAPACITY) ", "roomno>0");
            lblroomcapacity.Text = actualcapacity;
            lbltotcount.Text = totalstud;
            //BindSeatPlan();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlCollege_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //   int college_code = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER", "COLLEGE_CODE", "COLLEGE_CODE=" + Convert.ToInt32(ddlCollege.SelectedValue + "")));
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_CODE = " + Convert.ToInt32(Session["colcode"]), "SESSION_PNAME ASC");

        if (ddlCollege.SelectedIndex > 0)
        {
            //DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", "@P_COLSCHEMENO", "" + Convert.ToInt32(ddlClgname.SelectedValue) + "");
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                // objCommon.FillDropDownList(ddlSem, "ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_SEMESTER SM WITH (NOLOCK) ON(S.SEMESTERNO=SM.SEMESTERNO) LEFT OUTER JOIN ACD_SEM_PROMOTION SP WITH (NOLOCK) ON (S.IDNO = SP.IDNO AND S.SEMESTERNO = SP.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME", "S.DEGREENO > 0 AND S.SCHEMENO=" + ViewState["schemeno"], "SM.SEMESTERNO");
                if (ddlCollege.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_ID = " + Convert.ToInt32(ViewState["college_id"]), "SESSIONNO DESC");
                    ddlSession.Focus();
                }
                else
                {
                    objCommon.DisplayMessage("Please select College/School Name.", this.Page);
                }
            }
        }

        lvdetails.Visible = false;
        btnDeallocate.Visible = false;
        btnCancel.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        string examno = string.Empty;
        int pt = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(PATTERNNO,0) as PATTERNNO", "SCHEMENO='" + ViewState["schemeno"] + "'")));
        DataSet ds = objCommon.FillDropDown("ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", "PATTERNNO=" + pt + " and ACTIVESTATUS=1 and fldname like'%EXTERMARK%'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                examno += ds.Tables[0].Rows[i]["EXAMNO"].ToString() + ",";

            }
            examno = examno.TrimEnd(',');
        }
        string dates = DateTime.Now.ToString("yyyy-MM-dd");
        //objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL" + " AND EXAM_TT_TYPE = 11", "SLOTNO");
        objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "  CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL" + " AND EXAMDATE >='" + dates + "' AND EXAM_TT_TYPE = " + examno + "", "SLOTNO");  // AND EXAM_TT_TYPE = 11" 

    }
    #endregion


    public void getExamdate()
    {
        string examno = string.Empty;
        // int pt = Convert.ToInt32((objCommon.LookUp("ACD_SCHEME", "isnull(PATTERNNO,0) as PATTERNNO", "SCHEMENO='" + ViewState["schemeno"] + "'")));
        DataSet ds1 = objCommon.FillDropDown("ACD_EXAM_NAME", "EXAMNO", "EXAMNAME", " ACTIVESTATUS=1 and fldname like'%EXTERMARK%'", "");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                examno += ds1.Tables[0].Rows[i]["EXAMNO"].ToString() + ",";

            }
            examno = examno.TrimEnd(',');
        }
        string dates = DateTime.Now.ToString("yyyy-MM-dd");
        //objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL" + " AND EXAM_TT_TYPE = 11", "SLOTNO");
        //objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE ED inner join acd_session_master SM on (ED.SESSIONNO=SM.SESSIONNO) ", "EXDTNO", "  CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", " AND EXAMDATE IS NOT NULL" + " AND EXAMDATE >='" + dates + "' AND EXAM_TT_TYPE = " + examno + "", "SLOTNO");  // AND EXAM_TT_TYPE = 11" 
        objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE ED INNER JOIN ACD_SESSION_MASTER SM ON ED.SESSIONNO=SM.SESSIONNO ", "Distinct ED.SESSIONNO", " CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", " EXAMDATE IS NOT NULL AND EXAMDATE >='" + dates + "' AND EXAM_TT_TYPE in( " + examno + ")", "ED.SESSIONNO");  // AND EXAM_TT_TYPE = 11" 

    }


    #region ExamDate
    protected void ddlExamdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlExamdate.SelectedIndex) == 0)
        {
            objCommon.DisplayUserMessage(updplRoom, "Please select Exam Date !!", this.Page);
        }
        else
        {
            //string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
            string EXAMDAT = Convert.ToString(ddlExamdate.SelectedItem);
            string EXAMDA = ((ddlExamdate.Text));
            string date = EXAMDAT.ToString();
            //string EXAMDATE = (Convert.ToDateTime(EXAMDAT)).ToString("dd/MM/yyyy");
            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            string dates = DateTime.Now.ToString("yyyy-MM-dd");

            DateTime currentdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime examDates = DateTime.Parse(EXAMDATE);

            // objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(NVARCHAR(20),EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO");
            // objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
            //objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'" + "AND EXAMDATE >='" + dates + "'", "SLOTNO");
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct AED.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");


            //objCommon.FillDropDownList(ddlSequence, "ACD_SEATING_PLAN AED INNER JOIN ACD_ROOM AEIS ON AEIS.ROOMNO=AED.ROOMNO", "AED.ROOMNO", "AED.ROOM_NAME", "", "AED.SEQUENCENO");

            string Seatcheck = objCommon.LookUp("ACD_SEATING_ARRANGEMENT", "COUNT(*)", "EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue);    //AND AED.TH_PR=1
            int countSeatarrang = Convert.ToInt32(Seatcheck);
            if (countSeatarrang <= 0)
            {
                pnlExamCourse.Visible = false;
                pnlRoomDetails.Visible = false;

                pnldetails.Visible = false;

                lvExamCoursesOnDate.DataSource = null;
                lvExamCoursesOnDate.DataBind();

            }
            else
            {

                btnCancel.Visible = false;
                //lvRoomDetails.Visible = false;
                //  lvExamCoursesOnDate.Visible = false;
                //pnldetails.Visible = false;

                lvExamCoursesOnDate.DataSource = null;
                lvExamCoursesOnDate.DataBind();
                pnlRoomDetails.Visible = false;
                pnlExamCourse.Visible = false;
            }
            btnConfigure.Visible = false;
            btnDeallocate.Visible = false;
            btnCancel.Visible = false;
            btnClear.Visible = false;
            btnSave.Visible = false;
        }
    }

    #endregion

    #region
    protected void btnDeallocate_Click(object sender, EventArgs e)
    {

        if (RadioButton1.Checked == true)
        {
            //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

            int uano = Convert.ToInt32(Session["userno"]);
            int orgno = Convert.ToInt32(Session["OrgId"]);
            int collegeid = Convert.ToInt32(Convert.ToInt32(ViewState["college_id"]));

            if (RadioButton1.Checked == true)
            {
                seatstatus = 1;
            }
            else if (RadioButton2.Checked == true)
            {
                seatstatus = 2;
            }
            else
            {

            }

            DataSet ds = null;
            // string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND CONVERT(NVARCHAR(50),EXAMDATE,103)='" + EXAMDATES + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1

            CustomStatus cs = (CustomStatus)objSC.DeallocateSeatingArrangment_Crescent(EXAMDATE, slotno, uano, Convert.ToInt32(seatstatus), orgno);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updplRoom, "Seating Arrangement Deallocate Successfully !", this.Page);
                BindSeatPlan();
                lblroomcapacity.Text = "";
                lbltotcount.Text = "";
                //ddlSession.SelectedValue = "0";
                ddlslot.SelectedValue = "0";
                ddlExamdate.SelectedValue = "0";
                ddlCollege.SelectedValue = "0";
                btnDeallocate.Visible = false;
                btnCancel.Visible = false;
                // btnClear.Visible = true;
            }
            else
            {
                objCommon.DisplayUserMessage(this.updplRoom, "Failed To Deallocate Seating arrangement Plan!", this.Page);
            }
        }
        else if (RadioButton2.Checked == true)
        {
            //int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            int slotno = Convert.ToInt32(ddlslot.SelectedValue);
            int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);

            int uano = Convert.ToInt32(Session["userno"]);
            int orgno = Convert.ToInt32(Session["OrgId"]);
            int collegeid = Convert.ToInt32(Convert.ToInt32(ViewState["college_id"]));

            if (RadioButton1.Checked == true)
            {
                seatstatus = 1;
            }
            else if (RadioButton2.Checked == true)
            {
                seatstatus = 2;
            }
            else
            {

            }

            DataSet ds = null;
            // string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND CONVERT(NVARCHAR(50),EXAMDATE,103)='" + EXAMDATES + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1

            CustomStatus cs = (CustomStatus)objSC.DeallocateSeatingArrangment_Double_CRESCENT(EXAMDATE, slotno, uano, Convert.ToInt32(seatstatus), orgno);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updplRoom, "Seating Arrangement Deallocate Successfully !", this.Page);
                BindSeatPlan();
                lblroomcapacity.Text = "";
                lbltotcount.Text = "";
               // ddlSession.SelectedValue = "0";
                ddlslot.SelectedValue = "0";
                ddlExamdate.SelectedValue = "0";
                ddlCollege.SelectedValue = "0";
                btnDeallocate.Visible = false;
                btnCancel.Visible = false;
                // btnClear.Visible = true;
            }
            else
            {
                objCommon.DisplayUserMessage(this.updplRoom, "Failed To Deallocate Seating arrangement Plan!", this.Page);
            }

        }
        else
        {


        }
    }

    #endregion

    #region

    public void BindExamCourseList()
    {
        try
        {
            //string EXAMDATE = (Convert.ToDateTime(ddlExamdate.Text)).ToString("yyyy-MM-dd");
            string EXAMDATE = Convert.ToDateTime(ddlExamdate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");

            DataSet ds = objSC.GetExamCourseListByDate_Crescent(Convert.ToInt16(ddlslot.SelectedValue), EXAMDATE);
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

            DataSet dsroom = objCommon.FillDropDown("ACD_SEATING_PLAN SP WITH (NOLOCK) INNER JOIN ACD_ROOM R WITH (NOLOCK) ON(SP.ROOMNO=R.ROOMNO) LEFT JOIN ACD_FLOOR F ON (F.FLOORNO=R.FLOORNO)", "SP.ROOMNO", "FLOORNAME AS BLOCKNAME,R.ROOMNAME,ROOMCAPACITY,ACTUAL_CAPACITY,SP.DISABLED_IDS,SP.SEQUENCENO", "", "R.ROOMNO");
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

            #region
            // if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
            //if (dsroom.Tables[0].Rows.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvRoomDetails.Items)
            //    {
            //        TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
            //         //Label txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;

            //        //while (Convert.ToBoolean(dsroom.Tables[0].Rows[0]["SEQUENCENO"].ToString()))
            //        //{
            //        HiddenField roommnos = dataitem.FindControl("hfRoom") as HiddenField;
            //        int roommo = Convert.ToInt32(roommnos.Value);

            //        //CheckBox chkroom = dataitem.FindControl("chckroom") as CheckBox;
            //        //TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
            //        txtbox.Text = dsroom.Tables[0].Rows[0]["SEQUENCENO"].ToString();
            //        //}
            //   // }
            //}
            #endregion
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void RoomMatrixDetails()
    {
        try
        {

            DataSet dsroomM = objCommon.FillDropDown("ACD_SEATING_PLAN", "ROOMNO", "ROOM_NAME,ROW_INDEX,COLUMN_INDEX,ACTUAL_CAPACITY", "", "ROOMNO");
            if (dsroomM != null && dsroomM.Tables[0].Rows.Count > 0)
            {
                lvMatrix.DataSource = dsroomM;
                lvMatrix.DataBind();
                PnlMatrix.Visible = true;
            }
            else
            {
                lvRoomDetails.DataSource = null;
                lvRoomDetails.DataBind();
                PnlMatrix.Visible = false;
            }

            #region
            // if (Convert.ToDecimal(dsStudent.Tables[0].Rows[0]["SMAX"]) > 0)
            //if (dsroom.Tables[0].Rows.Count > 0)
            //{
            //    foreach (ListViewDataItem dataitem in lvRoomDetails.Items)
            //    {
            //        TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
            //         //Label txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;

            //        //while (Convert.ToBoolean(dsroom.Tables[0].Rows[0]["SEQUENCENO"].ToString()))
            //        //{
            //        HiddenField roommnos = dataitem.FindControl("hfRoom") as HiddenField;
            //        int roommo = Convert.ToInt32(roommnos.Value);

            //        //CheckBox chkroom = dataitem.FindControl("chckroom") as CheckBox;
            //        //TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
            //        txtbox.Text = dsroom.Tables[0].Rows[0]["SEQUENCENO"].ToString();
            //        //}
            //   // }
            //}
            #endregion
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string roomno = string.Empty;
        string roomSq = string.Empty;
        int status = 0;
        // IsInCollection();
        string txt = string.Empty;
        //if (RadioButton1.Checked == true || RadioButton2.Checked == true)
        //{ 
        foreach (ListViewDataItem dataitem in lvRoomDetails.Items)
        {

            HiddenField roommnos = dataitem.FindControl("hfRoom") as HiddenField;
            CheckBox chkroom = dataitem.FindControl("chckroom") as CheckBox;
            TextBox txtbox = dataitem.FindControl("txtRoomSrNo") as TextBox;
            if (chkroom.Checked == true)
            {
                if (txtbox.Text == null)
                {
                    objCommon.DisplayUserMessage(updplRoom, "Please Select All Room Sequence!", this.Page);
                    return;
                }


                if (txtbox.Text != "")
                {
                    roomno = roommnos.Value;
                    int room = Convert.ToInt32(roomno);
                    roomSq = txtbox.Text.Trim();
                    int seq = Convert.ToInt32(roomSq);
                    CustomStatus cs = (CustomStatus)objSC.ConfigureSeatingPlanSequence(room, seq);
                    status = Convert.ToInt32(cs);
                    //statuss = Convert.ToInt32(ViewState["Status"].ToString());
                }
                else
                {
                    objCommon.DisplayUserMessage(updplRoom, "Please Select Room Sequence!", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(updplRoom, "Please Select Room Sequence Details!", this.Page);
                return;
            }
        }
        // int seatplan = Convert.ToInt32(ViewState["Status"]);
        if (status == 1)
        {
            //       if (cs.Equals(CustomStatus.RecordSaved))
            //{
            objCommon.DisplayUserMessage(this.updplRoom, "Room Sequence Saved Successfully !", this.Page);
            btnConfigure.Visible = true;
            btnDeallocate.Visible = false;
            btnCancel.Visible = true;
            //pnlRoomDetails.Visible = false;
            //  pnlExamCourse.Visible = false;
            //btnSave.Visible = false;
            //btnClear.Visible = false;
        }
        else
        {
            btnConfigure.Visible = false;
            btnDeallocate.Visible = false;
            btnCancel.Visible = false;
        }
        //}
        //else
        //{
        //    objCommon.DisplayUserMessage(this.Page, "Please Checked Seating Plan Status Single/Dual", this.Page);
        //}
    }

    #endregion

    #region txtTextchanged
    protected void txtRoomSrNo_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = sender as TextBox;
        ListViewDataItem item = txt.NamingContainer as ListViewDataItem;
        //and now get the TextBox:
        TextBox txtRoomSrNo = item.FindControl("txtRoomSrNo") as TextBox;
        HiddenField hfRoomsrno = item.FindControl("hfsrno") as HiddenField;

        //for (int i = 0; i < lvRoomDetails.Items.Count; i++)
        //{
        foreach (ListViewItem item1 in lvRoomDetails.Items)
        {
            HiddenField hfsrno = item1.FindControl("hfsrno") as HiddenField;
            CheckBox chkroom = item1.FindControl("chckroom") as CheckBox;
            TextBox txtbox = item1.FindControl("txtRoomSrNo") as TextBox;
            if (txtbox.Text != "")
            {
                if (txtbox.Text == txtRoomSrNo.Text && hfRoomsrno.Value != hfsrno.Value)
                {
                    //objCommon.DisplayUserMessage(this.updplRoom, "Room Sequence alfeady exist !", this.Page);
                    objCommon.DisplayMessage(this, "Room Sequence [" + txtbox.Text + "]  Already Exists!", this);
                    txtRoomSrNo.Text = string.Empty;
                    return;
                }
            }
        }

        //}

    }
    #endregion



}