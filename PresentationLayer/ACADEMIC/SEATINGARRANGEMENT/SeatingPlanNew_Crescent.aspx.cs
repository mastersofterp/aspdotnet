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
using System.Drawing;
using System.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

using ClosedXML.Excel;
using DynamicAL_v2;

public partial class ACADEMIC_SEATINGARRANGEMENT_SeatingPlanNew : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingController objSC = new SeatingController();
    Seating objSeating = new Seating();

    int TwoStud = 0;

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
                    //this.CheckPageAuthorization();
                    Page.Title = Session["coll_name"].ToString();

                }
                PopulateDropDownList();



                if (Convert.ToInt32(hfdTabsVal.Value) == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", "$('#tab1X').click();", true);
                    ClearTab2();
                    ClearTab3();
                    ClearTab4();
                    updpnl_CancelPlan.Update();
                    updpnl_CopyPlan.Update();
                    updpnl_PlanReports.Update();
                }
                else if (Convert.ToInt32(hfdTabsVal.Value) == 2)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", "$('#tab2X').click();", true);
                    ClearTab1();
                    ClearTab3();
                    ClearTab4();
                    updpnl_CreatePlan.Update();
                    updpnl_CopyPlan.Update();
                    updpnl_PlanReports.Update();
                }
                else if (Convert.ToInt32(hfdTabsVal.Value) == 3)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", "$('#tab3X').click();", true);
                    ClearTab1();
                    ClearTab2();
                    ClearTab4();
                    updpnl_CreatePlan.Update();
                    updpnl_CancelPlan.Update();
                    updpnl_PlanReports.Update();
                }
                else if (Convert.ToInt32(hfdTabsVal.Value) == 4)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "key", "$('#tab4X').click();", true);
                    ClearTab1();
                    ClearTab2();
                    ClearTab3();
                    updpnl_CreatePlan.Update();
                    updpnl_CancelPlan.Update();
                    updpnl_CopyPlan.Update();
                }
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
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE EXAMDATE IS NOT NULL) AND IS_ACTIVE= 1 ", "S.SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession1, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE EXAMDATE IS NOT NULL) AND IS_ACTIVE= 1 ", "S.SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession2, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE EXAMDATE IS NOT NULL) AND IS_ACTIVE= 1 ", "S.SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession3, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE EXAMDATE IS NOT NULL) AND IS_ACTIVE= 1 ", "S.SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSession3_1, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER C ON (C.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.SESSIONNO", "SESSION_NAME+' - '+C.COLLEGE_NAME AS SESSION_NAME", "SESSIONNO > 0 AND S.SESSIONNO IN (SELECT DISTINCT SESSIONNO FROM ACD_EXAM_DATE WHERE EXAMDATE IS NOT NULL) AND IS_ACTIVE= 1", "S.SESSIONNO DESC");
    }

    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayUserMessage(updpnl_CreatePlan, "Please Select Session First.", this.Page);
            return;
        }

        int IntExt = 2;
        //if (rbInternal.Checked)
        //    IntExt = 1;
        //else if (rbExternal.Checked)
        //    IntExt = 2;

        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "' AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND EXAMTYPE = " + IntExt + "");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updpnl_CreatePlan, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot.SelectedValue = "0";
            ddlExamdate.SelectedItem.Text = string.Empty;
            //ddlExamdate.SelectedValue Focus();
        }
        else
        {
            objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    protected void ddlslot_SelectedIndexChanged(object sender, EventArgs e)
    {
        int examType = 2;        //rbInternal.Checked == true ? 1 : 2;
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlslot.SelectedValue) + "," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + ",0,0," + Convert.ToInt32(Session["userno"]) + "," + examType + ",2";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

        lbltotcountHead.Text = (ddlslot.SelectedIndex != 0) ? "Total Student :" : "";
        lbltotcount.Text = (ddlslot.SelectedIndex != 0) ? Convert.ToString(ds.Tables[0].Rows.Count) : "";

       // BindSeatPlan();

       // int Exam_Type = 2;
        //if (rbInternal.Checked)
        //    Exam_Type = 1;
        //else if (rbExternal.Checked)
        //    Exam_Type = 2;

        objCommon.FillDropDownList(ddlBlock, "ACD_BLOCK", "BLOCKNO", "BLOCKNAME", "ACTIVESTATUS=1", "BLOCKNO");
        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM AR INNER JOIN ACD_SEATING_PLAN asp ON (AR.ROOMNO = asp.ROOMNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SP ON (SP.ROOMNO = AR.ROOMNO AND SP.EXAM_DATE= '" + EXAMDATE + "' AND SP.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ")", "DISTINCT AR.ROOMNO", "AR.ROOMNAME", "ISNULL(AR.ACTIVESTATUS,0) = 1 AND ISNULL(SP.STATUS,0) = 0 AND ISNULL(AR.ROWS,0)!=0 AND ISNULL(AR.COLUMNS,0)!=0 AND asp.EXAM_TYPE = " + examType + "", "AR.ROOMNAME");

        divSeatingPlan.Visible = false;
        btnSave.Enabled = false;
        ddlslot.Focus();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Session');", true);
            return;
        }
        else if (ddlExamdate.SelectedItem.Text == string.Empty)   //else if (ddlExamdate.SelectedItem.Text == string.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Exam Date');", true);
            return;
        }
        else if (ddlslot.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Exam Slot');", true);
            return;
        }

        int examType = 2;     //rbInternal.Checked == true ? 1 : 2;

        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlslot.SelectedValue) + "," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + ",0,0," + Convert.ToInt32(Session["userno"]) + "," + examType + ",2";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);
        ViewState["BranchTable"] = ds;

        DataView view = new DataView(ds.Tables[0]);
        DataTable BranchNo = view.ToTable(true, "BRANCHNO", "LONGNAME", "SHORTNAME", "CCODE", "SEMESTERNO", "COURSE_TYPE", "COUNTX", "COURSENO", "CODE", "PREV_STATUS");

        int count = ds.Tables[0].Rows.Count;

        if (rbInternal.Checked)
        {
            DataView dv = new DataView(BranchNo);
            dv.RowFilter = "PREV_STATUS = 0";
            BranchNo = dv.ToTable();
        }

        rptBranchPref.DataSource = BranchNo;
        rptBranchPref.DataBind();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>$('#BranchPref').modal({backdrop: 'static', keyboard: false});$('#BranchPref').modal('show');</script>", false);
        upd_Branch.Update();
    }

    string GetAlreadyAllotedSeats(int Operation, int RoomNo, int SessionX, string ExamDateX, int SlotX)
    {
        int examType = 2;         //rbInternal.Checked == true ? 1 : 2;

        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + SessionX + "," + SlotX + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + "," + Operation + "";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

        DataView view = new DataView(ds.Tables[0]);
        DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCKNAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");
 
        var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

        string commaSeperatedValues = string.Join(",", Rooms);

        string DynamicString = "";
        string BlockName = "";
        string FloorName = "";
        string RoomName = "";
        int rColumns, rRow, rCapacity, raCapacity;
        string DummyBenches = "";
        int FlagD = 0;
        int ColNo = 0;
        int RowNo = 0;
        int TempI = 0;
        string ExamDate = "";
        string SlotName = "";
        double ColWidthX = 0;

        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

        for (int i = 0; i < RoomInfo.Rows.Count; i++)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
            DataTable dt = dv.ToTable();
            ColNo = 0;   // ADDED FOR CHECKING FOR THE ISSUE OF REPORT GENERATION ON DT 17032022


            RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
            rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
            rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
            rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
            raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]);
            DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
            BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCKNAME"]);
            FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
            ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
            SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

            DataRow[] dr = dt.Select("SLOTNAME='" + SlotName + "'");        // ADDED BY NARESH BEERLA FOR CHECKING AS PER THE SOLT WISE AND LENGTH ON DT 17032022
            string len = dr.Length.ToString();



            ColWidthX = Convert.ToDouble(100.00 / rColumns);

            TempI = 0;
            RowNo = 0;

            DynamicString += "<div class='col-md-12'><h3><b>" + RoomName + "</b>  <i>( " + FloorName + ", " + BlockName + " )</i></h3></div><div class='col-md-12'>";

            for (int j = 0; j < raCapacity; j++)
            {
                FlagD = 0;
                TempI++;

                for (int k = 0; k < DummyBenches.Split(',').Length; k++)
                {
                    if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
                    {
                        DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ff6666'><label class='HeaderValue'>" + Alphabets[RowNo] + TempI + "</label></div><br>";
                        DynamicString += "<b style='color:red' class='FoSty1'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
                        DynamicString += "<b style='color:red' class='FoSty1'>Not in Use</b></center></div>";

                        j--;
                        FlagD = 1;
                    }
                }

                if (j < Convert.ToInt32(len)) // ADDED THE FOR LOOP BY NARESH BEERLA ON DT 17032022
                {
                    if (FlagD == 0)
                    {
                        if ((dt.Rows[j]["REGNO"]) == "-1")
                        {
                            DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ff6666''><label class='HeaderValue'>" + dt.Rows[j]["SEATNO"] + "</label></div><br>";
                            DynamicString += "<span class='FoSty1'> Not Available </span><br>";
                            DynamicString += "<span class='FoSty1'>( Blank )</span></center></div>";
                        }
                        else
                        {
                            DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#3CB371'><label class='HeaderValue'>" + dt.Rows[j]["SEATNO"] + "</label></div><br>";
                            DynamicString += "<span class='FoSty1'>" + dt.Rows[j]["REGNO"] + "</span><br>";
                            DynamicString += "<span class='FoSty1'>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span></center></div>";
                        }
                    }
                }
                ColNo++;
                if (ColNo == rColumns)
                {
                    ColNo = 0;
                    RowNo++;
                    DynamicString += "</div><div class='col-md-12'>";
                }
            }
            DynamicString += "</div>";
        }

        return DynamicString;
    }

    #region Backup Commented by Naresh Beerla on DT 25062022 for the issue in Seating Allotment Report
    //    string GetAlreadyAllotedSeatsForPrint(int Operation, int RoomNo, int SessionX, string ExamDateX, int SlotX)
    //    {
    //       // int examType = rbInternal.Checked == true ? 1 : 2;
    //        int examType = Convert.ToInt32(ddlExamType.SelectedValue);

    //        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
    //        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
    //        string Call_Values = "0," + SessionX + "," + 2 + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + "," + Operation + "";

    //        DataTable DT = new DataTable();
    //        DT.Columns.Add("SESSIONNO");
    //        DT.Columns.Add("REGNO");
    //        DT.Columns.Add("ROOMNO");
    //        DT.Columns.Add("SEATNO");
    //        DT.Columns.Add("SEATNO_N");
    //        DT.Columns.Add("LOCK");
    //        DT.Columns.Add("EXAMDATE");
    //        DT.Columns.Add("SLOT");

    //        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

    //        DataView view = new DataView(ds.Tables[0]);
    //        DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCK_NAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");

    //        var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

    //        string commaSeperatedValues = string.Join(",", Rooms);

    //        string DynamicString = "";
    //        string BlockName = "";
    //        string FloorName = "";
    //        string RoomName = "";
    //        int rColumns, rRow, rCapacity, raCapacity;
    //        string DummyBenches = "";
    //        int FlagD = 0;
    //        int ColNo = 0;
    //        int RowNo = 0;
    //        int TempI = 0;
    //        string ExamDate = "";
    //        string SlotName = "";
    //        //string ColMd = "";
    //        double ColWidthX = 0;

    //        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

    //        for (int i = 0; i < RoomInfo.Rows.Count; i++)
    //        {
    //            DataView dv = new DataView(ds.Tables[0]);
    //            dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
    //            DataTable dt = dv.ToTable();
    //             ColNo = 0;   // ADDED FOR CHECKING FOR THE ISSUE OF REPORT GENERATION ON DT 17032022


    //            RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
    //            rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
    //            rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
    //            rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
    //            raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]); 
    //            DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
    //            BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCK_NAME"]);
    //            FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
    //            ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
    //            SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

    //            DataRow[] dr = dt.Select("SLOTNAME='" + SlotName+"'");
    //            string len= dr.Length.ToString();

    //            ColWidthX = Convert.ToDouble(100.00 / rColumns);

    //            TempI = 0;
    //            RowNo = 0;
    //            //<div style='position:absolute;z-index:1;margin-left:15%;margin-top:7.5%'> 2nd line

    //            DynamicString += @"<div style='margin-top:0.5%'></div><div class='row' style='min-height:1700px;position:relative'>
    //                            <div style='position:absolute;z-index:1;margin-left:15%;margin-top:7.5%'>
    //                                <img src='../../IMAGES/SVCE_logo%20(1).jpg' width='100' height='100'/>
    //                            </div>
    //                            <div class='col-md-12' style='border-bottom:solid 2px black;margin-top:8%'>
    //                                <p>
    //                                    <center style='font-family:Verdana;font-size:15pt;font-weight:bold'>
    //                                        Sri Venkateswara College of Engineering
    //                                    </center>
    //                                </p>
    //                                <p>
    //                                    <center style='font-family:Verdana;font-size:10pt;'>
    //                                        (An Autonomous Institution, Affiliated to Anna University, Chennai)
    //                                    </center>
    //                                </p>
    //                                <p>
    //                                    <center style='font-family:Verdana;font-size:12pt;'>
    //                                        Sriperumbudur Tk, Kancheepuram District, Tamilnadu, India - 602117
    //                                    </center>
    //                                </p>
    //                            </div>
    //                            <div class='col-md-12'>
    //                                <p>
    //                                    <center style='font-family:Verdana;font-size:12pt;font-weight:bold'>SEATING ARRANGEMENT - DEC-21</center>
    //                                </p>
    //                                <p>
    //                                    <center style='font-family:Verdana;font-size:11pt;font-weight:bold'>" + RoomName + " : " + BlockName + " " + FloorName + " | " + ExamDate + "("+SlotName+")</center></p></div><div class='col-md-12' style='margin-top:15px;'>";

    //            //DynamicString += "<div class='col-md-12'><h3><b>" + RoomName + "</b>  <i>( " + FloorName + ", " + BlockName + " )</i></h3></div><div class='col-md-12'>";

    //            for (int j = 0; j < raCapacity; j++)
    //            {
    //                FlagD = 0;
    //                TempI++;

    //                for (int k = 0; k < DummyBenches.Split(',').Length; k++)
    //                {
    //                    if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
    //                    {
    //                        DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ccc'>" + Alphabets[RowNo] + TempI + "</div><br>";
    //                        DynamicString += "<b style='color:red' class='FoSty1'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
    //                        DynamicString += "<b style='color:red' class='FoSty1'>Not in Use</b></center></div>";

    //                        j--;
    //                        FlagD = 1;
    //                    }
    //                }

    //                if (j < Convert.ToInt32(len))
    //                {
    //                    if (FlagD == 0)//==0
    //                    {
    //                        DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ccc'>" + dt.Rows[j]["SEATNO"] + "</div><br>";
    //                        DynamicString += "<span class='FoSty1'>" + dt.Rows[j]["REGNO"] + "</span><br>";
    //                        DynamicString += "<span class='FoSty1'>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span></center></div>";
    //                    }
    //                }              
    //                ColNo++;
    //                if (ColNo == rColumns)
    //                {
    //                    ColNo = 0;
    //                    RowNo++;
    //                    DynamicString += "</div><div class='col-md-12'>";
    //                }
    //            }
    //            DynamicString += "</div>";

    //            DynamicString += @"<div class='col-md-12' style='margin-top:30px'>
    //                                    <table class='table table-bordered'>
    //                                        <thead>
    //                                            <tr>
    //                                                <th class='FoSty1'><center>Department</center></th>
    //                                                <th class='FoSty1'><center>Sub. Code</center></th>
    //                                                <th class='FoSty1'><center>No. of Candidates</center></th>
    //                                                <th class='FoSty1'>Number Appeared</th>
    //                                                <th class='FoSty1'>No. of Absentees</th>
    //                                            </tr>
    //                                        </thead>
    //                                        <tbody>
    //                                ";
    //            DataView ViewBranch = new DataView(dt);
    //           // DataTable BranchTable = ViewBranch.ToTable(true, "CODE","CCODE","ROOMNO","SLOTNAME");
    //            DataTable BranchTable = ViewBranch.ToTable(true, "CODE", "COURSENO", "ROOMNO", "CCODE", "SLOTNAME");

    //            DataRow[] dr1 = BranchTable.Select("SLOTNAME='" + SlotName + "'");
    //            string len1 = dr1.Length.ToString();

    //            int TotalStud = 0;

    //            for (int x = 0; x < Convert.ToInt32(len1); x++) //BranchTable.Rows.Count
    //            {
    //                if (x < Convert.ToInt32(len1))
    //                {
    //                    //int cnt = ds.Tables[0].Select("CCODE = '" + BranchTable.Rows[x][1] + "' AND ROOMNO=" + BranchTable.Rows[x][2] + "").Length;
    //                    int cnt = ds.Tables[0].Select("COURSENO = '" + dr1[x][1] + "' AND ROOMNO=" + dr1[x][2] + "").Length;
    //                    TotalStud += cnt;
    //                    //Convert.ToInt32(ds.Tables[0].Compute("COUNT(SHORTNAME)", ""));
    //                    //DynamicString += "<tr><td class='FoSty1'><center>" + BranchTable.Rows[x][0] + "</center></td><td class='FoSty1'><center>" + BranchTable.Rows[x][1] + "</center></td><td class='FoSty1'><center>" + cnt + "</center></td><td></td><td></td></tr>";
    //                    DynamicString += "<tr><td class='FoSty1'><center>" + dr1[x][0] + "</center></td><td class='FoSty1'><center>" + dr1[x][3] + "</center></td><td class='FoSty1'><center>" + cnt + "</center></td><td></td><td></td></tr>";
    //                }
    //            }
    //            DynamicString += "</tbody></table></div>";
    //            DynamicString += @"<div class='col-md-12'>
    //                                <span class='pull-right FoSty2'>Total Students : " + TotalStud + "</span></div>";
    //            DynamicString += @"<div class='col-md-12' style='margin-top:30px;height:100px;'>
    //                                    <div class='col-md-4 FoSty2'><center>Name of the Invigilator</center></div>
    //                                    <div class='col-md-4 FoSty2'><center>Department of the Invigilator</center></div>
    //                                    <div class='col-md-4 FoSty2'><center>Signature of the Invigilator</center></div>
    //                               </div>";
    //            DynamicString += @"<div class='col-md-12' style='position:absolute;bottom:100px;'>
    //                                    <div class='col-md-12'>
    //                                        <span class='pull-right FoSty2'>Chief Superintendent</span>
    //                                    </div>
    //                                </div></div> <div style='margin-top:1%'> </div> <div style='margin:bottom:2%;'> </div>";
    //        }
    //        return DynamicString;
    //    }
    #endregion

    // RESOLVED THE ISSUE OF SEATING ALLOTMENT HTML DYNAMIC REPORT & ADDED THE BELOW METHOD BY NARESH BEERLA ON DT 25062022 

    string GetAlreadyAllotedSeatsForPrint(int Operation, string RoomNo, int SessionX, string ExamDateX, int SlotX)
    {
        // int examType = rbInternal.Checked == true ? 1 : 2;
        int examType = 2;   // Convert.ToInt32(ddlExamType.SelectedValue);

        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + SessionX + "," + SlotX + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + "," + Operation + "";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

        DataView view = new DataView(ds.Tables[0]);
        DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCKNAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");

        var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

        string commaSeperatedValues = string.Join(",", Rooms);

        string DynamicString = "";
        string BlockName = "";
        string FloorName = "";
        string RoomName = "";
        int rColumns, rRow, rCapacity, raCapacity;
        string DummyBenches = "";
        int FlagD = 0;
        int ColNo = 0;
        int RowNo = 0;
        int TempI = 0;
        string ExamDate = "";
        string SlotName = "";
        //string ColMd = "";
        double ColWidthX = 0;

        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

        for (int i = 0; i < RoomInfo.Rows.Count; i++)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
            DataTable dt = dv.ToTable();
            ColNo = 0;   // ADDED FOR CHECKING FOR THE ISSUE OF REPORT GENERATION ON DT 17032022


            RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
            if (RoomName == "MPH1")
            { }
            rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
            rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
            rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
            raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]);
            DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
            BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCKNAME"]);
            FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
            ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
            SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

            DataRow[] dr = dt.Select("SLOTNAME='" + SlotName + "'");
            string len = dr.Length.ToString();

            ColWidthX = Convert.ToDouble(100.00 / rColumns);

            TempI = 0;
            RowNo = 0;
           // string sessionname = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=" + SessionX);   //Added by Sachin A dt on 24012023

            DynamicString += @"<div style='margin-top:2%'></div><div class='row' style='min-height:1700px;position:relative'>
                            <div style='position:absolute;z-index:1;margin-left:15%;margin-top:7.5%'>
                                <img src='~/Images/Crescent_logo-main.png' width='100' height='100'/>
                            </div>
                            <div class='col-md-12' style='border-bottom:solid 2px black;margin-top:8%'>
                                <p>
                                    <center  style='font-family:Verdana;font-size:15pt; text-align:center; font-weight:bold'>
                                       Crescent 
                                    </h4>
                                </p>
                                <p>
                                    <center style='font-family:Verdana;font-size:10pt;'>
                                        Institute of Science & Technology
                                    </center>
                                </p>
                                <p>
                                    <center style='font-family:Verdana;font-size:12pt;'>
                                       Deemed to be University u/s of the UGC Act, 1956 GST Road, Vandalur, Chennai 600 048
                                    </center>
                                </p>
                            </div>
                            <div class='col-md-12'>
                                <p>
                                    <center style='font-family:Verdana;font-size:12pt;font-weight:bold'>SEATING ARRANGEMENT -DEC-22 </center>
                                </p> 
                                <p>
                                    <center style='font-family:Verdana;font-size:11pt;font-weight:bold'>" + RoomName + " : " + BlockName + " " + FloorName + " | " + ExamDate + "(" + SlotName + ")</center></p></div><div class='col-md-12' style='margin-top:15px;'>  ";

            //DynamicString += "<div class='col-md-12'><h3><b>" + RoomName + "</b>  <i>( " + FloorName + ", " + BlockName + " )</i></h3></div><div class='col-md-12'>";

            for (int j = 0; j < raCapacity; j++)
            {
                FlagD = 0;
                TempI++;

                for (int k = 0; k < DummyBenches.Split(',').Length; k++)
                {
                    if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
                    {
                        DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ccc'>" + Alphabets[RowNo] + TempI + "</div><br>";
                        DynamicString += "<b style='color:red' class='FoSty1'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
                        DynamicString += "<b style='color:red' class='FoSty1'>Not in Use</b></center></div>";

                        j--;
                        FlagD = 1;
                    }
                }

                if (j < Convert.ToInt32(len))
                {
                    if (FlagD == 0)//==0
                    {
                        DynamicString += "<div style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ccc'>" + dt.Rows[j]["SEATNO"] + "</div><br>";
                        DynamicString += "<span class='FoSty1'>" + dt.Rows[j]["REGNO"] + "</span><br>";
                        DynamicString += "<span class='FoSty1'>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span></center></div>";
                    }
                }
                ColNo++;
                if (ColNo == rColumns)
                {
                    ColNo = 0;
                    RowNo++;
                    DynamicString += "</div><div class='col-md-12'>";
                }
            }
            DynamicString += "</div>";

            DynamicString += @"<div class='col-md-12' style='margin-top:30px'>
                                    <table class='table table-bordered'>
                                        <thead>
                                            <tr>
                                                <th class='FoSty1'><center>Department</center></th>
                                                <th class='FoSty1'><center>Sub. Code</center></th>
                                                <th class='FoSty1'><center>No. of Candidates</center></th>
                                                <th class='FoSty1'>Number Appeared</th>
                                                <th class='FoSty1'>No. of Absentees</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                ";
            DataView ViewBranch = new DataView(dt);
            // DataTable BranchTable = ViewBranch.ToTable(true, "CODE","CCODE","ROOMNO","SLOTNAME");
            DataTable BranchTable = ViewBranch.ToTable(true, "CODE", "COURSENO", "ROOMNO", "CCODE", "SLOTNAME");

            DataRow[] dr1 = BranchTable.Select("SLOTNAME='" + SlotName + "'");
            string len1 = dr1.Length.ToString();

            int TotalStud = 0;

            for (int x = 0; x < Convert.ToInt32(len1); x++) //BranchTable.Rows.Count
            {
                if (x < Convert.ToInt32(len1))
                {
                    //int cnt = ds.Tables[0].Select("CCODE = '" + BranchTable.Rows[x][1] + "' AND ROOMNO=" + BranchTable.Rows[x][2] + "").Length;
                    int cnt = ds.Tables[0].Select("COURSENO = '" + dr1[x][1] + "' AND ROOMNO=" + dr1[x][2] + "").Length;
                    TotalStud += cnt;
                    //Convert.ToInt32(ds.Tables[0].Compute("COUNT(SHORTNAME)", ""));
                    //DynamicString += "<tr><td class='FoSty1'><center>" + BranchTable.Rows[x][0] + "</center></td><td class='FoSty1'><center>" + BranchTable.Rows[x][1] + "</center></td><td class='FoSty1'><center>" + cnt + "</center></td><td></td><td></td></tr>";
                    DynamicString += "<tr><td class='FoSty1'><center>" + dr1[x][0] + "</center></td><td class='FoSty1'><center>" + dr1[x][3] + "</center></td><td class='FoSty1'><center>" + cnt + "</center></td><td></td><td></td></tr>";
                }
            }
            DynamicString += "</tbody></table></div>";
            DynamicString += @"<div class='col-md-12'>
                                <span class='pull-right FoSty2'>Total Students : " + TotalStud + "</span></div>";
            DynamicString += @"<div class='col-md-12 offset-8' style='margin-top:30px;height:100px;'>
                                    <div class='col-md-4 FoSty2'><center> Name of the Invigilator</center></div>
                                    <div class='col-md-4 FoSty2'><center>Department of the Invigilator</center></div>
                                    <div class='col-md-4 FoSty2'><center>Signature of the Invigilator</center></div>
                               </div>";
            DynamicString += @"<div class='col-md-12' style='position:absolute;bottom:140px;'>
                                    <div class='col-md-12'>
                                        <span class='pull-right FoSty2 offset-9'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Chief Superintendent</span>
                                    </div>
                                </div> </div><div style='margin-top:2%'> <div style='margin:bottom:2%;'></div>";
        }
        return DynamicString;
    }

    // ENDS HERE THE ISSUE OF SEATING ALLOTMENT HTML DYNAMIC REPORT & ADDED THE BELOW METHOD BY NARESH BEERLA ON DT 25062022 

    string GetAlreadyAllotedSeatsForSeatingPlan(int Operation, int RoomNo, int SessionX, string ExamDateX, int SlotX)
    {
        int examType = 2;         //rbInternal.Checked == true ? 1 : 2;

        // int examType = Convert.ToInt32(ddlExamType.SelectedValue);
        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + SessionX + "," + SlotX + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + "," + Operation + "";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

        DataView view = new DataView(ds.Tables[0]);
        DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCKNAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");

        var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

        string commaSeperatedValues = string.Join(",", Rooms);

        string DynamicString = "";
        string BlockName = "";
        string FloorName = "";
        string RoomName = "";
        int rColumns, rRow, rCapacity, raCapacity;
        string DummyBenches = "";
        int FlagD = 0;
        int ColNo = 0;
        int RowNo = 0;
        int TempI = 0;
        string ExamDate = "";
        string SlotName = "";
        //string ColMd = "";
        int BlankSeat = 0;
        double ColWidthX = 0;

        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

        for (int i = 0; i < RoomInfo.Rows.Count; i++)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
            DataTable dt = dv.ToTable();

            RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
            rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
            rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
            rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
            raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]);
            DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
            BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCKNAME"]);
            FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
            ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
            SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

            ColWidthX = Convert.ToDouble(100.00 / rColumns);

            TempI = 0;
            RowNo = 0;

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                FlagD = 0;
                TempI++;

                for (int k = 0; k < DummyBenches.Split(',').Length; k++)
                {
                    if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
                    {
                        DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:#ff6666;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[RowNo] + TempI + "</p>";
                        DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                        DynamicString += "<div class='col-md-12 disabled' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                        DynamicString += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                        DynamicString += "<span style='display:none'>1</span></div></center></div>";

                        j--;
                        FlagD = 1;
                        BlankSeat++;
                    }
                }

                if (FlagD == 0)
                {
                    if (!Convert.ToString(Alphabets[RowNo] + TempI).Equals(dt.Rows[j]["SEATNO"]))
                    {
                        DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[RowNo] + TempI + "</p>";
                        DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                        DynamicString += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
                        DynamicString += "<span style='color:green;font-weight:bold'>Empty</span><br>";
                        DynamicString += "<span style='display:none'>2</span></div></center></div>";

                        j--;
                        BlankSeat++;
                    }
                    else
                    {
                        DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:#3CB371;width:100%;margin-bottom:0px;font-weight:bold'>" + dt.Rows[j]["SEATNO"] + "</p>";
                        DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                        DynamicString += "<div class='col-md-12 cellX disabled' style='padding:7px 0px;'><span>" + dt.Rows[j]["REGNO"] + "</span><br>";
                        DynamicString += "<span>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span><br>";
                        DynamicString += "<span style='display:none'>" + dt.Rows[j]["REGNO"] + "</span></div></center></div>";
                    }
                }
                ColNo++;
                if (ColNo == rColumns)
                {
                    ColNo = 0;
                    RowNo++;
                    TempI = 0;
                }
            }
        }
        DynamicString += "^" + Convert.ToString(ds.Tables[0].Rows.Count + BlankSeat) + "$" + BlankSeat;
        return DynamicString;
    }

    string GetAlreadyAllotedSeatsForSeatingPlan_Internal(int Operation, int RoomNo, int SessionX, string ExamDateX, int SlotX)
    {
        string DynamicString = "";

        try
        {
            int examType = 2;     // rbInternal.Checked == true ? 1 : 2;

            string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
            string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
            string Call_Values = "0," + SessionX + "," + SlotX + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + "," + Operation + "";

            DataTable DT = new DataTable();
            DT.Columns.Add("SESSIONNO");
            DT.Columns.Add("REGNO");
            DT.Columns.Add("ROOMNO");
            DT.Columns.Add("SEATNO");
            DT.Columns.Add("SEATNO_N");
            DT.Columns.Add("LOCK");
            DT.Columns.Add("EXAMDATE");
            DT.Columns.Add("SLOT");

            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

            DataView view = new DataView(ds.Tables[0]);
            DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCKNAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");

            var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

            string commaSeperatedValues = string.Join(",", Rooms);

            //string DynamicString = "";
            string BlockName = "";
            string FloorName = "";
            string RoomName = "";
            int rColumns, rRow, rCapacity, raCapacity;
            string DummyBenches = "";
            int FlagD = 0;
            int ColNo = 0;
            int RowNo = 0;
            int TempI = 0;
            string ExamDate = "";
            string SlotName = "";
            //string ColMd = "";
            int BlankSeat = 0;
            double ColWidthX = 0;

            string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

            for (int i = 0; i < RoomInfo.Rows.Count; i++)
            {
                DataView dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
                DataTable dt = dv.ToTable();

                RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
                rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
                rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
                rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
                raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]);
                DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
                BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCKNAME"]);
                FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
                ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
                SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

                ColWidthX = Convert.ToDouble(100.00 / rColumns);

                TempI = 0;
                RowNo = 0;

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    FlagD = 0;
                    TempI++;

                    for (int k = 0; k < DummyBenches.Split(',').Length; k++)
                    {
                        if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
                        {
                            DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:#ff6666;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[RowNo] + TempI + "</p>";
                            DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicString += "<div class='col-md-12 disabled' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                            DynamicString += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                            DynamicString += "<span style='display:none'>1</span></div></center></div>";

                            j--;
                            FlagD = 1;
                            BlankSeat++;
                        }
                    }

                    if (FlagD == 0)
                    {
                        if (!Convert.ToString(Alphabets[RowNo] + TempI).Equals(dt.Rows[j]["SEATNO"]))
                        {
                            DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[RowNo] + TempI + "</p>";
                            DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicString += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
                            DynamicString += "<span style='color:green;font-weight:bold'>Empty</span><br>";
                            DynamicString += "<span style='display:none'>2</span></div></center></div>";

                            j--;
                            BlankSeat++;
                        }
                        else
                        {
                            //DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            //DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:#3CB371;width:100%;margin-bottom:0px;font-weight:bold'>" + dt.Rows[j]["SEATNO"] + "</p>";
                            //DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            //DynamicString += "<div class='col-md-12 cellX disabled' style='padding:7px 0px;'><span>" + dt.Rows[j]["REGNO"] + "</span><br>";
                            //DynamicString += "<span>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span><br>";
                            //DynamicString += "<span style='display:none'>" + dt.Rows[j]["REGNO"] + "</span></div></center></div>";



                            TwoStud++;
                            if (TwoStud % 2 != 0)
                            {
                                DynamicString += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                                DynamicString += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + dt.Rows[j]["SEATNO"] + "</p></center>";
                            }

                            DynamicString += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + RoomNo + "," + Alphabets[RowNo] + TempI + "," + TempI + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicString += "<center><div class='col-md-6 cellX' style='padding:7px 0px;'><span>" + dt.Rows[j]["REGNO"] + "</span><br>";
                            DynamicString += "<span>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span><br>";
                            DynamicString += "<span style='display:none'>" + dt.Rows[j]["REGNO"] + "</span></div></center>";

                            if (TwoStud % 2 == 0)
                            {
                                DynamicString += "</div>";
                            }
                        }
                    }
                    ColNo++;
                    if (ColNo == rColumns)
                    {
                        ColNo = 0;
                        RowNo++;
                        TempI = 0;
                    }
                }
            }
            DynamicString += "^" + Convert.ToString(ds.Tables[0].Rows.Count + BlankSeat) + "$" + BlankSeat;
            return DynamicString;
        }
        catch (Exception ex)
        {
            return DynamicString;
        }

    }

    //void GenerateDynamicSeats(string ViewStateName, int BranchCount, int FullCount)    //Commented by Sachin A dt on 23012023
    //{
    //    if (rbExternal.Checked)
    //    {
    //        #region External Exam
    //        DataSet ds = objCommon.FillDropDown("ACD_ROOM ar INNER JOIN ACD_SEATING_PLAN asp ON (ar.ROOMNO = asp.ROOMNO) INNER JOIN	ACD_FLOOR af ON (AF.FLOORNO = ar.FLOORNO) INNER JOIN ACD_BLOCK aab ON(AAB.BLOCKNO = ar.BLOCKNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SL ON (SL.ROOMNO = AR.ROOMNO AND SL.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SL.EXAMDATE='" + ddlExamdate.SelectedItem.Text + "' AND SL.SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ")", "(CAST(ar.ROOMNO AS VARCHAR)+'#'+(aab.BLOCKNAME)+'#'+(af.FLOORNAME)+'#'+CAST(ar.ROOMNAME AS VARCHAR)+'$'+CAST(asp.DISABLED_IDS AS VARCHAR)) AS A", "(CAST(AR.ROWS AS VARCHAR)+'$'+CAST(AR.COLUMNS AS VARCHAR)+'$'+CAST(((AR.ROWS*AR.COLUMNS)-ISNULL(SL.FILLCOUNT,0)) AS VARCHAR)) AS B", "ISNULL(AR.STATUS,0) = 1 AND (AR.BLOCK_NO = " + ddlBlock.SelectedValue + " OR 0 = " + ddlBlock.SelectedValue + ") AND (AR.FLOORNO = " + ddlFloor.SelectedValue + " OR 0 = " + ddlFloor.SelectedValue + ") AND (AR.ROOMNO = " + ddlRoom.SelectedValue + " OR 0 = " + ddlRoom.SelectedValue + ")", "ar.ROOMNO");

    //        string[] TotalRooms = new string[ds.Tables[0].Rows.Count];
    //        string RoomNo = "";
    //        string RoomName = "";
    //        string Rows = "";
    //        string Columns = "";
    //        string Capacity = "";
    //        string BlockName = "";
    //        string FloorName = "";

    //        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (i == (ds.Tables[0].Rows.Count - 1))
    //            {
    //                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0];
    //                BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1];
    //                FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2];
    //                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3];
    //                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0];
    //                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1];
    //                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2];
    //            }
    //            else
    //            {
    //                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0] + "$";
    //                BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1] + ",";
    //                FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2] + ",";
    //                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3] + "!";
    //                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0] + ",";
    //                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1] + ",";
    //                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2] + ",";
    //            }
    //            TotalRooms[i] = "0";
    //        }

    //        int TempCapacity = 0;
    //        int TempRoomNo = 0;
    //        int TempColNo = 0;
    //        int TempRow = 0;
    //        int TempColumn = 0;

    //        string TempRoomName = "";
    //        string TempFloorName = "";
    //        string TempBlockName = "";
    //        int TempX = 0;
    //        int TempC = 0;              // Temporary Column
    //        int TempR = 0;              // Temporary Row
    //        int TempI = 0;              // Temporary Increment 
    //        int TempCS = 0;             // Temporary Column Starting Value
    //        string Disable_Ids = "";    // Storing Disabled IDs
    //        int FlagD = 0;              // If flag equals to 1 then that Bench is not in use.

    //        int SeatNo = 1;             // Seating no for all students those applied for Exam
    //        int RoomSeatNo = 1;           // Seat No for Room
    //        string DynamicSrting = "";
    //        int BlankDTcount = 1;
    //        //string ColMd = "";
    //        int LoopBreak = 1;
    //        int BBC = 0;
    //        double ColWidthX = 0;

    //        DataTable TempTable = new DataTable();
    //        TempTable.Columns.Add("SESSIONNO");
    //        TempTable.Columns.Add("REGNO");
    //        TempTable.Columns.Add("ROOMNO");
    //        TempTable.Columns.Add("SEATNO");
    //        TempTable.Columns.Add("SEATNO_N");
    //        TempTable.Columns.Add("LOCK");
    //        TempTable.Columns.Add("EXAMDATE");
    //        TempTable.Columns.Add("SLOT");

    //        //Get Max Branch Student Count
    //        int[] arrCount = new int[BranchCount];
    //        for (int i = 0; i < BranchCount; i++)
    //        {
    //            DataTable dt = (DataTable)ViewState[ViewStateName.Split(',')[i]];
    //            arrCount[i] = dt.Rows.Count;
    //        }
    //        int CountTill = arrCount.Max();
    //        //End Here

    //        for (int i = 0; i < CountTill; i++)
    //        {
    //            for (int j = 0; j < BranchCount; j++)
    //            {

    //                if (FlagD == 1)
    //                {
    //                    FlagD = 0;
    //                    if (j == 0)
    //                    {
    //                        j = BranchCount - 1;
    //                    }
    //                    else
    //                    {
    //                        j--;
    //                    }
    //                }

    //                try
    //                {
    //                    TempCapacity = Convert.ToInt32(Capacity.Split(',')[TempColNo]) - TempX;

    //                    if (TempCapacity > 0)
    //                    {
    //                        TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
    //                        TempBlockName = BlockName.Split(',')[TempColNo];
    //                        TempFloorName = FloorName.Split(',')[TempColNo];
    //                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
    //                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
    //                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
    //                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);

    //                        if (i == 0 && j == 0 && RoomSeatNo == 1)
    //                        {
    //                            DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";

    //                            string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));

    //                            if (AlreadyAlloted != "^0$0")
    //                            {
    //                                DynamicSrting += AlreadyAlloted.Split('^')[0];
    //                                ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);
    //                                BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
    //                                TempX = BBC;
    //                            }
    //                            else
    //                            {
    //                                ViewState["TempC"] = 0;
    //                            }

    //                            if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
    //                            {
    //                                TempC = Convert.ToInt32(ViewState["TempC"]);
    //                            }
    //                            else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
    //                            {
    //                                TempC = 0;
    //                                TempR++;
    //                            }
    //                            else
    //                            {
    //                                int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
    //                                int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

    //                                TempC = Rem;
    //                                TempR = quo;
    //                            }
    //                        }
    //                        ColWidthX = Convert.ToDouble(100.00 / TempColumn);
    //                        TempX++;
    //                        TotalRooms[TempColNo] = Convert.ToString(TempX - BBC + Convert.ToInt32(ViewState["TempC"]));
    //                        //TotalRooms[TempColNo] = Convert.ToString(TempX);
    //                    }
    //                    else
    //                    {
    //                        TempColNo++;
    //                        if (Capacity.Split(',').Length == TempColNo)
    //                        {
    //                            CountTill = 0;
    //                            BranchCount = 0;
    //                            break;
    //                        }
    //                        TempX = 1;
    //                        TempC = 0;
    //                        TempR = 0;
    //                        TempCS = 0;
    //                        TempI = 0;
    //                        RoomSeatNo = 1;
    //                        TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
    //                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
    //                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
    //                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
    //                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
    //                        DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";
    //                        ColWidthX = Convert.ToDouble(100.00 / TempColumn);
    //                        string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));
    //                        DynamicSrting += AlreadyAlloted.Split('^')[0];
    //                        ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);

    //                        if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
    //                        {
    //                            TempC = Convert.ToInt32(ViewState["TempC"]);
    //                        }
    //                        else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
    //                        {
    //                            TempC = 0;
    //                            TempR++;
    //                        }
    //                        else
    //                        {
    //                            int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
    //                            int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

    //                            TempC = Rem;
    //                            TempR = quo;
    //                        }

    //                        BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
    //                        TempX = BBC + 1;

    //                        TotalRooms[TempColNo] = Convert.ToString(TempX);
    //                    }

    //                    DataTable dt = new DataTable();

    //                    for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
    //                    {
    //                        int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
    //                        if (CheckDisableId == RoomSeatNo)
    //                        {
    //                            FlagD = 1;
    //                            TempC++;
    //                            break;
    //                        }
    //                    }
    //                    BlankDTcount = 0;
    //                    if (FlagD != 1)
    //                    {
    //                        if (TempC < TempColumn)
    //                        {
    //                        ComeHere:
    //                            if (TempI == BranchCount)
    //                            {
    //                                TempI = 0;
    //                            }

    //                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

    //                            if (dt.Rows.Count == 0)
    //                            {

    //                                if (BlankDTcount == BranchCount)
    //                                {
    //                                    BlankDTcount = 0;
    //                                    LoopBreak = 0;
    //                                    break;
    //                                }
    //                                else
    //                                {
    //                                    BlankDTcount++;
    //                                    TempI++;
    //                                    goto ComeHere;
    //                                }
    //                            }

    //                            TempC++;
    //                        }
    //                        else
    //                        {
    //                            DynamicSrting += "</div><div class='col-md-12 disabled'>";

    //                            TempC = 0;
    //                            TempCS++;
    //                        ComeHere:
    //                            if (TempCS == BranchCount)
    //                            {
    //                                TempCS = 0;
    //                                TempI = TempCS;
    //                            }
    //                            else
    //                            {
    //                                TempI = TempCS;
    //                            }

    //                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
    //                            if (dt.Rows.Count == 0)
    //                            {
    //                                if (BlankDTcount == BranchCount)
    //                                {
    //                                    BlankDTcount = 0;
    //                                    LoopBreak = 0;
    //                                    break;
    //                                }
    //                                else
    //                                {
    //                                    BlankDTcount++;
    //                                    TempCS++;
    //                                    goto ComeHere;
    //                                }
    //                            }

    //                            TempR++;
    //                            TempC++;
    //                        }

    //                        //TempTable.Rows.Add(ddlSession.SelectedValue, dt.Rows[0][0], TempRoomNo, Alphabets[TempR] + TempC, TempC, 1, Convert.ToString(Convert.ToDateTime(txtExamDate.Text).ToString("dd/MM/yyyy")), ddlslot.SelectedValue);

    //                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
    //                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                        DynamicSrting += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span>" + dt.Rows[0][0] + "</span><br>";
    //                        DynamicSrting += "<span>(" + dt.Rows[0]["CODE"].ToString().Trim() + ")</span><br>";
    //                        DynamicSrting += "<span style='display:none'>" + dt.Rows[0][0] + "</span></div></center></div>";


    //                        ViewState["LastRegNo"] = dt.Rows[0][0];

    //                        DataRow dr = dt.Rows[0];
    //                        dr.Delete();
    //                        dt.AcceptChanges();
    //                        ViewState[ViewStateName.Split(',')[TempI]] = dt;
    //                        #region Commented but VIMP
    //                        //Remove Completed Branches from Loop
    //                        //if (dt.Rows.Count == 0)
    //                        //{
    //                        //    string reOrder = ViewStateName.ToString();
    //                        //    if (TempI == BranchCount - 1)
    //                        //    {
    //                        //        reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
    //                        //    }
    //                        //    ViewStateName = reOrder;
    //                        //    BranchCount = BranchCount - 1;
    //                        //    if (TempI != 0) { TempI--; }
    //                        //    if (TempC == TempColumn)
    //                        //    {
    //                        //        if (TempCS != 0) { TempCS--; }
    //                        //    }
    //                        //    if (j != 0) { j--; }
    //                        //}
    //                        //End Here
    //                        #endregion
    //                        TempI++;
    //                    }
    //                    else
    //                    {
    //                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
    //                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                        DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
    //                        DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
    //                        DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

    //                        if (j == BranchCount - 1)
    //                        {
    //                            j--;
    //                        }
    //                    }
    //                    SeatNo++;
    //                    RoomSeatNo++;
    //                }
    //                catch (Exception ex)
    //                {
    //                    ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('" + ex.Message + "==" + SeatNo + "~~" + ViewState["LastRegNo"] + "');", true);
    //                }
    //            }
    //        }

    //        //for (int z = LoopBreak; z < TempCapacity; z++)
    //        int z = RoomSeatNo + Convert.ToInt32(ViewState["TempC"]);
    //        //LoopBreak;
    //        while (z <= (TempRow * TempColumn))
    //        {
    //            if (TempC < TempColumn)
    //            {
    //                TempC++;
    //                TempR++;
    //            }
    //            else
    //            {
    //                TempC = 0;
    //                TempR++;
    //                TempC++;
    //            }

    //            for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
    //            {
    //                int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
    //                if (CheckDisableId == RoomSeatNo)
    //                {
    //                    DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                    DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
    //                    DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                    DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
    //                    DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
    //                    DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

    //                    FlagD = 1;
    //                    break;
    //                }
    //            }
    //            if (FlagD != 1)
    //            {
    //                DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
    //                DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                DynamicSrting += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold;'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
    //                DynamicSrting += "<span style='color:green;font-weight:bold'>Empty</span><br>";
    //                DynamicSrting += "<span style='display:none'>2</span></div></center></div>";
    //            }
    //            else
    //            {
    //                FlagD = 0;
    //            }

    //            RoomSeatNo++;
    //            z++;
    //        }

    //        ViewState["DynamicString"] = DynamicSrting;
    //        DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
    //        updpnl_CreatePlan.Update();
    //        ViewState["SeatPlan"] = TempTable;
    //        ViewState["RoomNo"] = RoomNo;
    //        ViewState["FillCount"] = string.Join("$", TotalRooms);
    //        divSeatingPlan.Visible = true;
    //        btnSave.Enabled = true;
    //        #endregion
    //    }
    //    else if (rbInternal.Checked)
    //    {
    //        #region Internal Exam
    //        DataSet ds = objCommon.FillDropDown("ACD_ROOM ar INNER JOIN ACD_SEATING_PLAN asp ON (ar.ROOMNO = asp.ROOMNO) INNER JOIN	ACD_FLOOR af ON (AF.FLOORNO = ar.FLOORNO) INNER JOIN ACD_BLOCK aab ON(AAB.BLOCKNO = ar.BLOCKNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SL ON (SL.ROOMNO = AR.ROOMNO AND SL.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SL.EXAM_DATE='" + ddlExamdate.SelectedItem.Text + "')", "(CAST(ar.ROOMNO AS VARCHAR)+'#'+(aab.BLOCKNAME)+'#'+(af.FLOORNAME)+'#'+CAST(ar.ROOMNAME AS VARCHAR)+'$'+CAST(asp.DISABLED_IDS AS VARCHAR)) AS A", "(CAST(AR.ROWS AS VARCHAR)+'$'+CAST(AR.COLUMNS AS VARCHAR)+'$'+CAST(((AR.ROWS*AR.COLUMNS)*2-ISNULL(SL.FILLCOUNT,0)) AS VARCHAR)) AS B", "ISNULL(AR.ActiveStatus,0) = 1 AND (AR.BLOCKNO = " + ddlBlock.SelectedValue + " OR 0 = " + ddlBlock.SelectedValue + ") AND (AR.FLOORNO = " + ddlFloor.SelectedValue + " OR 0 = " + ddlFloor.SelectedValue + ") AND (AR.ROOMNO = " + ddlRoom.SelectedValue + " OR 0 = " + ddlRoom.SelectedValue + ")", "ar.ROOMNO");

    //        string[] TotalRooms = new string[ds.Tables[0].Rows.Count];
    //        string RoomNo = "";
    //        string RoomName = "";
    //        string Rows = "";
    //        string Columns = "";
    //        string Capacity = "";
    //        string BlockName = "";
    //        string FloorName = "";

    //        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (i == (ds.Tables[0].Rows.Count - 1))
    //            {
    //                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0];
    //                BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1];
    //                FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2];
    //                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3];
    //                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0];
    //                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1];
    //                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2];
    //            }
    //            else
    //            {
    //                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0] + "$";
    //                BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1] + ",";
    //                FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2] + ",";
    //                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3] + "!";
    //                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0] + ",";
    //                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1] + ",";
    //                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2] + ",";
    //            }
    //            TotalRooms[i] = "0";
    //        }

    //        int TempCapacity = 0;
    //        int TempRoomNo = 0;
    //        int TempColNo = 0;
    //        int TempRow = 0;
    //        int TempColumn = 0;

    //        string TempRoomName = "";
    //        string TempFloorName = "";
    //        string TempBlockName = "";
    //        int TempX = 0;
    //        int TempC = 0;              // Temporary Column
    //        int TempR = 0;              // Temporary Row
    //        int TempI = 0;              // Temporary Increment 
    //        int TempCS = 0;             // Temporary Column Starting Value
    //        string Disable_Ids = "";    // Storing Disabled IDs
    //        int FlagD = 0;              // If flag equals to 1 then that Bench is not in use.

    //        int SeatNo = 1;             // Seating no for all students those applied for Exam
    //        int RoomSeatNo = 1;           // Seat No for Room
    //        string DynamicSrting = "";
    //        int BlankDTcount = 1;
    //        //string ColMd = "";
    //        int LoopBreak = 1;
    //        int BBC = 0;
    //        double ColWidthX = 0;
    //        int HeaderCnt = 1;

    //        DataTable TempTable = new DataTable();
    //        TempTable.Columns.Add("SESSIONNO");
    //        TempTable.Columns.Add("REGNO");
    //        TempTable.Columns.Add("ROOMNO");
    //        TempTable.Columns.Add("SEATNO");
    //        TempTable.Columns.Add("SEATNO_N");
    //        TempTable.Columns.Add("LOCK");
    //        TempTable.Columns.Add("EXAMDATE");
    //        TempTable.Columns.Add("SLOT");

    //        //Get Max Branch Student Count
    //        int[] arrCount = new int[BranchCount];
    //        for (int i = 0; i < BranchCount; i++)
    //        {
    //            DataTable dt = (DataTable)ViewState[ViewStateName.Split(',')[i]];
    //            arrCount[i] = dt.Rows.Count;
    //        }
    //        int CountTill = arrCount.Max();
    //        //End Here

    //        for (int i = 0; i < CountTill; i++)
    //        {
    //            for (int j = 0; j < BranchCount; j++)
    //            {

    //                if (FlagD == 1)
    //                {
    //                    FlagD = 0;
    //                    if (j == 0)
    //                    {
    //                        j = BranchCount - 1;
    //                    }
    //                    else
    //                    {
    //                        j--;
    //                    }
    //                }

    //                try
    //                {
    //                    TempCapacity = (Convert.ToInt32(Capacity.Split(',')[TempColNo])) - TempX;

    //                    if (TempCapacity > 0)
    //                    {
    //                        TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
    //                        TempBlockName = BlockName.Split(',')[TempColNo];
    //                        TempFloorName = FloorName.Split(',')[TempColNo];
    //                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
    //                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
    //                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
    //                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);

    //                        if (i == 0 && j == 0 && RoomSeatNo == 1)
    //                        {
    //                            DynamicSrting += "<div class='row'><center><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></center></h3><br></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";

    //                            string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan_Internal(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));

    //                            if (AlreadyAlloted != "^0$0")
    //                            {
    //                                DynamicSrting += AlreadyAlloted.Split('^')[0];
    //                                ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);
    //                                BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
    //                                TempX = BBC;
    //                            }
    //                            else
    //                            {
    //                                ViewState["TempC"] = 0;
    //                            }

    //                            if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
    //                            {
    //                                TempC = Convert.ToInt32(ViewState["TempC"]);
    //                            }
    //                            else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
    //                            {
    //                                TempC = 0;
    //                                TempR++;
    //                            }
    //                            else
    //                            {
    //                                int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
    //                                int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

    //                                TempC = Rem;
    //                                TempR = quo;
    //                            }
    //                        }
    //                        ColWidthX = Convert.ToDouble(100.00 / TempColumn);
    //                        TempX++;
    //                        TotalRooms[TempColNo] = Convert.ToString(TempX - BBC + Convert.ToInt32(ViewState["TempC"]));
    //                        //TotalRooms[TempColNo] = Convert.ToString(TempX);
    //                    }
    //                    else
    //                    {
    //                        TempColNo++;
    //                        if (Capacity.Split(',').Length == TempColNo)
    //                        {
    //                            CountTill = 0;
    //                            BranchCount = 0;
    //                            break;
    //                        }
    //                        TempX = 1;
    //                        TempC = 0;
    //                        TempR = 0;
    //                        TempCS = 0;
    //                        TempI = 0;
    //                        RoomSeatNo = 1;
    //                        TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
    //                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
    //                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
    //                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
    //                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
    //                        DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";
    //                        ColWidthX = Convert.ToDouble(100.00 / TempColumn);
    //                        string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan_Internal(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));
    //                        DynamicSrting += AlreadyAlloted.Split('^')[0];
    //                        ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);

    //                        if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
    //                        {
    //                            TempC = Convert.ToInt32(ViewState["TempC"]);
    //                        }
    //                        else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
    //                        {
    //                            TempC = 0;
    //                            TempR++;
    //                        }
    //                        else
    //                        {
    //                            int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
    //                            int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

    //                            TempC = Rem;
    //                            TempR = quo;
    //                        }

    //                        BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
    //                        TempX = BBC + 1;

    //                        TotalRooms[TempColNo] = Convert.ToString(TempX);
    //                    }

    //                    DataTable dt = new DataTable();

    //                    for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
    //                    {
    //                        int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
    //                        if (CheckDisableId == RoomSeatNo)
    //                        {
    //                            FlagD = 1;
    //                            TempC += 2;
    //                            break;
    //                        }
    //                    }
    //                    BlankDTcount = 0;
    //                    if (FlagD != 1)
    //                    {
    //                        if (TempC < TempColumn * 2)
    //                        {
    //                        ComeHere:
    //                            if (TempI == BranchCount)
    //                            {
    //                                TempI = 0;
    //                            }

    //                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

    //                            if (dt.Rows.Count == 0)
    //                            {

    //                                if (BlankDTcount == BranchCount)
    //                                {
    //                                    BlankDTcount = 0;
    //                                    LoopBreak = 0;
    //                                    break;
    //                                }
    //                                else
    //                                {
    //                                    BlankDTcount++;
    //                                    TempI++;
    //                                    goto ComeHere;
    //                                }
    //                            }
    //                            //if (TempC % 2 == 0)
    //                            //{
    //                            //    TempC--;
    //                            //}

    //                            TempC++;
    //                        }
    //                        else
    //                        {
    //                            DynamicSrting += "</div><div class='col-md-12 disabled'>";

    //                            TempC = 0;
    //                            TempCS++;
    //                        ComeHere:
    //                            if (TempCS == BranchCount)
    //                            {
    //                                TempCS = 0;
    //                                TempI = TempCS;
    //                            }
    //                            else
    //                            {
    //                                TempI = TempCS;
    //                            }

    //                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
    //                            if (dt.Rows.Count == 0)
    //                            {
    //                                if (BlankDTcount == BranchCount)
    //                                {
    //                                    BlankDTcount = 0;
    //                                    LoopBreak = 0;
    //                                    break;
    //                                }
    //                                else
    //                                {
    //                                    BlankDTcount++;
    //                                    TempCS++;
    //                                    goto ComeHere;
    //                                }
    //                            }

    //                            TempR++;
    //                            TempC++;
    //                        }

    //                        //TempTable.Rows.Add(ddlSession.SelectedValue, dt.Rows[0][0], TempRoomNo, Alphabets[TempR] + TempC, TempC, 1, Convert.ToString(Convert.ToDateTime(txtExamDate.Text).ToString("dd/MM/yyyy")), ddlslot.SelectedValue);

    //                        TwoStud++;
    //                        if (TwoStud % 2 != 0)
    //                        {
    //                            DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                            DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p></center>";

    //                        }

    //                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                        DynamicSrting += "<center><div class='col-md-6 cellX' style='padding:7px 0px;'><span>" + dt.Rows[0][0] + "</span><br>";
    //                        DynamicSrting += "<span>(" + dt.Rows[0]["CODE"].ToString().Trim() + ")</span><br>";
    //                        DynamicSrting += "<span style='display:none'>" + dt.Rows[0][0] + "</span></div></center>";

    //                        if (TwoStud % 2 == 0)
    //                        {
    //                            DynamicSrting += "</div>";
    //                            //HeaderCnt++;
    //                        }

    //                        ViewState["LastRegNo"] = dt.Rows[0][0];

    //                        DataRow dr = dt.Rows[0];
    //                        dr.Delete();
    //                        dt.AcceptChanges();
    //                        ViewState[ViewStateName.Split(',')[TempI]] = dt;
    //                        #region Commented but VIMP
    //                        //Remove Completed Branches from Loop
    //                        //if (dt.Rows.Count == 0)
    //                        //{
    //                        //    string reOrder = ViewStateName.ToString();
    //                        //    if (TempI == BranchCount - 1)
    //                        //    {
    //                        //        reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
    //                        //    }
    //                        //    ViewStateName = reOrder;
    //                        //    BranchCount = BranchCount - 1;
    //                        //    if (TempI != 0) { TempI--; }
    //                        //    if (TempC == TempColumn)
    //                        //    {
    //                        //        if (TempCS != 0) { TempCS--; }
    //                        //    }
    //                        //    if (j != 0) { j--; }
    //                        //}
    //                        //End Here
    //                        #endregion
    //                        TempI++;

    //                        SeatNo++;
    //                        RoomSeatNo++;
    //                    }
    //                    else
    //                    {
    //                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p>";
    //                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                        DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
    //                        DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
    //                        DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

    //                        if (j == BranchCount - 1)
    //                        {
    //                            j--;
    //                        }

    //                        //TempC += 2;

    //                        SeatNo += 2;
    //                        RoomSeatNo += 2;
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('" + ex.Message + "==" + SeatNo + "~~" + ViewState["LastRegNo"] + "');", true);
    //                }
    //            }
    //        }

    //        //for (int z = LoopBreak; z < TempCapacity; z++)
    //        int z = RoomSeatNo + Convert.ToInt32(ViewState["TempC"]);
    //        //LoopBreak;
    //        while (z <= (TempRow * TempColumn * 2))
    //        {
    //            if (TempC < TempColumn * 2)
    //            {
    //                TempR++;
    //                TempC++;
    //            }
    //            else
    //            {
    //                TempC = 0;
    //                TempR++;
    //                TempC++;
    //            }

    //            for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
    //            {
    //                int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
    //                if (CheckDisableId == RoomSeatNo)
    //                {
    //                    DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                    DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p>";
    //                    DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                    DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
    //                    DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
    //                    DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

    //                    FlagD = 1;
    //                    break;
    //                }
    //            }
    //            if (FlagD != 1)
    //            {
    //                TwoStud++;
    //                if (TwoStud % 2 != 0)
    //                {
    //                    DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
    //                    DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p></center>";

    //                }

    //                DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
    //                DynamicSrting += "<center><div class='col-md-6 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold;'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
    //                DynamicSrting += "<span style='color:green;font-weight:bold'>Empty</span><br>";
    //                DynamicSrting += "<span style='display:none'>2</span></div></center>";

    //                if (TwoStud % 2 == 0)
    //                {
    //                    DynamicSrting += "</div>";
    //                }

    //                RoomSeatNo++;
    //                z++;
    //            }
    //            else
    //            {
    //                FlagD = 0;

    //                TempC += 2;
    //                RoomSeatNo += 2;
    //                z += 2;
    //            }
    //        }

    //        ViewState["DynamicString"] = DynamicSrting;
    //        DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
    //        updpnl_CreatePlan.Update();
    //        ViewState["SeatPlan"] = TempTable;
    //        ViewState["RoomNo"] = RoomNo;
    //        ViewState["FillCount"] = string.Join("$", TotalRooms);
    //        divSeatingPlan.Visible = true;
    //        btnSave.Enabled = true;
    //        #endregion
    //    }

    //}

    //Set Branch Preference from Here
    
    protected void btnBranchPref_Click(object sender, EventArgs e)
    {

        //if (ddlRoom.SelectedIndex == 0)
        //{
        //    string x = GetAlreadyAllotedSeats(5, 0, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(txtExamDate.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));
        //    DynamicSeating.Controls.Add(new LiteralControl(x));
        //    updpnl_CreatePlan.Update();
        //}

        //return;
        string[] BranchNo = (hfdPrefBranches.Value.Remove(hfdPrefBranches.Value.Length - 1, 1)).Split(',');
        string[] CourseNo = (hfdPrefCourses.Value.Remove(hfdPrefCourses.Value.Length - 1, 1)).Split(',');
        string[] BCount = (hfdPrefCount.Value.Remove(hfdPrefCount.Value.Length - 1, 1)).Split(',');
        string[] PrevStatus = (hfdPrevStatus.Value.Remove(hfdPrevStatus.Value.Length - 1, 1)).Split(',');
         
        //string BCount = "50";//txtActualStudCnt
        //for (int i = 0; i < BranchNo.Length; i++)
        //{
        //    foreach (RepeaterItem item in rptBranchPref.Items)
        //    {
        //        TextBox BCount[i] = item.FindControl("txtActualStudCnt") as TextBox;
        //    }           
        //}             

        //return;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Jquery", "$('#BranchPref').modal('toggle');", true);

        DataSet ds = (DataSet)ViewState["BranchTable"];

        string ViewStateName = "";
        for (int i = 0; i < BranchNo.Length; i++)
        {
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "BRANCHNO = " + BranchNo[i] + " AND COURSENO = " + CourseNo[i] + " AND PREV_STATUS = " + PrevStatus[i] + "";

            DataTable dt = dv.ToTable();
            DataTable dtTop = dt.Rows.Cast<DataRow>().Take(Convert.ToInt32(BCount[i])).CopyToDataTable();

            ViewState["Branch_" + i] = dtTop;
            ViewStateName += "Branch_" + i + ",";

            if (i == (BranchNo.Length - 1))
            {
                ViewStateName = ViewStateName.Remove(ViewStateName.Length - 1, 1);
                GenerateDynamicSeats(ViewStateName, BranchNo.Length, ds.Tables[0].Rows.Count);
            }
        }
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Exam_Type = 2;
        //if (rbInternal.Checked)
        //    Exam_Type = 1;
        //else
        //    Exam_Type = 2;

        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");

        objCommon.FillDropDownList(ddlFloor, "ACD_ROOM AR INNER JOIN ACD_FLOOR AF ON (AF.FLOORNO = AR.FLOORNO)", "DISTINCT AF.FLOORNO", "AF.FLOORNAME", "BLOCKNO=" + ddlBlock.SelectedValue + " AND AF.ACTIVESTATUS=1", "AF.FLOORNO");

        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM AR INNER JOIN ACD_SEATING_PLAN asp ON (AR.ROOMNO = asp.ROOMNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SP ON (SP.ROOMNO = AR.ROOMNO AND SP.EXAM_DATE='" + EXAMDATE + "' AND SP.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ")", "DISTINCT AR.ROOMNO", "AR.ROOMNAME", "ISNULL(AR.ACTIVESTATUS,0) = 1 AND ISNULL(SP.STATUS,0) = 0 AND ISNULL(AR.ROWS,0)!=0 AND ISNULL(AR.COLUMNS,0)!=0 AND AR.BLOCKNO=" + ddlBlock.SelectedValue + " AND asp.EXAM_TYPE=" + Exam_Type + "", "AR.ROOMNAME");

        lblroomcapacityHead.Text = (ddlBlock.SelectedIndex != 0) ? "" + ddlBlock.SelectedItem.Text + " Capacity :" : "";
        lblroomcapacity.Text = (ddlBlock.SelectedIndex != 0) ? objCommon.LookUp("ACD_ROOM", "SUM(ACTUALCAPACITY)", "BLOCKNO=" + ddlBlock.SelectedValue + "") : "";

        btnShow.Visible = btnSave.Visible = true;
        btnShow1.Visible = false;

        divSeatingPlan.Visible = false;
        btnSave.Enabled = false;   //2023
        ddlBlock.Focus();
    }

    protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Exam_Type = 2;
        //if (rbInternal.Checked)
        //    Exam_Type = 1;
        //else
        //    Exam_Type = 2;
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
        objCommon.FillDropDownList(ddlRoom, "ACD_ROOM AR INNER JOIN ACD_SEATING_PLAN asp ON (AR.ROOMNO = asp.ROOMNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SP ON (SP.ROOMNO = AR.ROOMNO AND SP.EXAM_DATE='" + EXAMDATE + "' AND SP.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SP.SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ")", "DISTINCT AR.ROOMNO", "AR.ROOMNAME", "ISNULL(AR.ACTIVESTATUS,0) = 1 AND ISNULL(SP.STATUS,0) = 0 AND ISNULL(AR.ROWS,0)!=0 AND ISNULL(AR.COLUMNS,0)!=0 AND AR.BLOCKNO=" + ddlBlock.SelectedValue + " AND AR.FLOORNO=" + ddlFloor.SelectedValue + " AND asp.EXAM_TYPE=" + Exam_Type + "", "AR.ROOMNAME");

        lblroomcapacityHead.Text = (ddlFloor.SelectedIndex != 0) ? "" + ddlBlock.SelectedItem.Text + " → " + ddlFloor.SelectedItem.Text + " Capacity :" : "";
        lblroomcapacity.Text = (ddlFloor.SelectedIndex != 0) ? objCommon.LookUp("ACD_ROOM", "SUM(ACTUALCAPACITY)", "BLOCKNO=" + ddlBlock.SelectedValue + " AND FLOORNO=" + ddlFloor.SelectedValue + "") : "";

        btnShow.Visible = btnSave.Visible = true;
        btnShow1.Visible = false;

        divSeatingPlan.Visible = false;
        btnSave.Enabled = false;  //2023
        ddlFloor.Focus();
    }

    protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
        string RoomStatus = objCommon.LookUp("ACD_SEATING_ARRANGEMENT_LOG X", "ISNULL(X.STATUS,0)", "X.SESSIONNO=" + ddlSession.SelectedValue + " AND X.EXAM_DATE='" + EXAMDATE + "' AND SLOTNO=" + ddlslot.SelectedValue + " AND X.ROOMNO=" + ddlRoom.SelectedValue + "");
        if (RoomStatus == "1")
        {
            ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('Room is Already Full !! Try another Room.');", true);
            btnShow.Visible = btnSave.Visible = false;   //2023
            btnShow1.Visible = true;
        }
        else
        {
            btnShow.Visible = btnSave.Visible = true;
            btnShow1.Visible = false;
        }

        string BlockName = (ddlBlock.SelectedIndex == 0) ? "" : ddlBlock.SelectedItem.Text + " → ";
        string FloorName = (ddlFloor.SelectedIndex == 0) ? "" : ddlFloor.SelectedItem.Text + " → ";

        if (ddlRoom.SelectedIndex != 0)
        {
            lblroomcapacityHead.Text = (ddlRoom.SelectedIndex != 0) ? "" + BlockName + FloorName + ddlRoom.SelectedItem.Text + " Capacity :" : "";

            lblroomcapacity.Text = (ddlRoom.SelectedIndex != 0) ? objCommon.LookUp("ACD_ROOM", "SUM(ACTUALCAPACITY)", "BLOCKNO=" + ddlBlock.SelectedValue + " AND FLOORNO=" + ddlFloor.SelectedValue + " AND ROOMNO=" + ddlRoom.SelectedValue + "") : "";
        }
        else
        {
            lblroomcapacityHead.Text = lblroomcapacity.Text = string.Empty;
        }

        if ((ddlBlock.SelectedIndex == 0 || ddlFloor.SelectedIndex == 0) && ddlRoom.SelectedIndex > 0)
        {
            string RCap = objCommon.LookUp("ACD_ROOM", "ACTUALCAPACITY", "ROOMNO=" + ddlRoom.SelectedValue + "");
            lblroomcapacityHead.Text = ddlRoom.SelectedItem.Text + " Capacity :";
            lblroomcapacity.Text = RCap;
        }

        divSeatingPlan.Visible = false;
        btnSave.Enabled = false;  //2023
        ddlRoom.Focus();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearTab1();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        string hfdVal = (hfdDataTable.Value.Remove(hfdDataTable.Value.Length - 1, 1));

        string SESSIONNO = "", REGNO = "", ROOMNO = "", SEATNO = "", SEATNO_N = "", LOCK = "", EXAMDATE = "", SLOT = "";

        for (int i = 0; i < hfdVal.Split('_').Length; i++)
        {
            SESSIONNO = hfdVal.Split('_')[i].Split(',')[1];
            REGNO = hfdVal.Split('_')[i].Split(',')[0];
            ROOMNO = hfdVal.Split('_')[i].Split(',')[2];
            SEATNO = hfdVal.Split('_')[i].Split(',')[3];
            SEATNO_N = hfdVal.Split('_')[i].Split(',')[4];
            LOCK = hfdVal.Split('_')[i].Split(',')[5];
            EXAMDATE = hfdVal.Split('_')[i].Split(',')[6];
            SLOT = hfdVal.Split('_')[i].Split(',')[7];

            DT.Rows.Add(SESSIONNO, REGNO, ROOMNO, SEATNO, SEATNO_N, LOCK, EXAMDATE, SLOT);
        }

        var target = DateTime.Parse(ddlExamdate.SelectedItem.Text);


        //DateTime fromdate = DateTime.Parse(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToShortDateString()); 

        //string date = DateTime.Now.ToString("dd-MMM-yy");
        //string dat = Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd-MM-yyyy");

        //DataTable DT = (DataTable)ViewState["SeatPlan"];

        int examType = 2; //rbInternal.Checked == true ? 1 : 2;
        
        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlslot.SelectedValue) + "," + Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy") + "," + ViewState["RoomNo"] + "," + Convert.ToString(ViewState["FillCount"]) + "," + Convert.ToInt32(Session["userno"]) + "," + examType + ",1";

        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, DT, true, 2);
        if (que_out == "1")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Seat Alloted Successfully.');", true);
            ddlslot.SelectedIndex = ddlRoom.SelectedIndex = 0;
            divSeatingPlan.Visible = false;
            btnSave.Enabled = false;
            lblroomcapacityHead.Text = lblroomcapacity.Text = lbltotcountHead.Text = lbltotcount.Text = string.Empty;
            ddlExamdate.SelectedIndex = 0;
            ddlBlock.SelectedIndex = 0;
            ddlFloor.SelectedIndex = 0;
            ddlslot.SelectedIndex = 0;
            ddlSession.SelectedIndex = 0;
        }
        else if (que_out == "-99")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Server Error.');", true);
        }
        DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
    }

    //protected void btnLock_Click(object sender, EventArgs e)
    //{
    //    DataTable DT = (DataTable)ViewState["SeatPlan"];

    //    foreach (DataRow dr in DT.Rows)
    //    {
    //        dr["LOCK"] = 1;
    //    }
    //    //DT.AcceptChanges();

    //    string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
    //    string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_OPERATION";
    //    string Call_Values = "0," + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlslot.SelectedValue) + "," + Convert.ToString(Convert.ToDateTime(txtExamDate.Text).ToString("dd/MM/yyyy")) + "," + ViewState["RoomNo"] + "," + Convert.ToString(ViewState["FillCount"]) + ",3";
    //    string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, DT, true, 2);
    //    ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Seat Allotement Updated Successfully.');", true);
    //    DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
    //}

    protected void btnShow1_Click(object sender, EventArgs e)
    {
        string x = GetAlreadyAllotedSeats(5, Convert.ToInt32(ddlRoom1.SelectedValue), Convert.ToInt32(ddlSession1.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot1.SelectedValue));
        DynamicSeating.Controls.Add(new LiteralControl(x));
        updpnl_CreatePlan.Update();
    }

    protected void txtExamDate1_TextChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updpnl_CancelPlan, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot1.SelectedValue = "0";
            ddlExamdate.SelectedItem.Text = string.Empty;
        }
        else
        {
            objCommon.FillDropDownList(ddlslot1, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    protected void ddlslot1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate1.SelectedItem.Text)).ToString("yyyy-MM-dd");
        objCommon.FillDropDownList(ddlRoom1, "ACD_SEATING_ARRANGEMENT SA INNER JOIN ACD_ROOM ar ON (SA.ROOMNO = AR.ROOMNO) INNER JOIN ACD_SEATING_ARRANGEMENT_LOG SP ON (SP.ROOMNO = SA.ROOMNO)", "DISTINCT AR.ROOMNO", "AR.ROOMNAME", "SA.SESSIONNO =" + ddlSession1.SelectedValue + " AND CONVERT(VARCHAR,SA.EXAMDATE,103)='" + ddlExamdate1.SelectedItem.Text + "' AND SA.SLOTNO=" + ddlslot1.SelectedValue + "", "AR.ROOMNAME"); //ISNULL(SP.STATUS,0) = 1 AND 
        ddlslot1.Focus();
    }

    protected void ddlRoom1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string x = GetAlreadyAllotedSeats(5, Convert.ToInt32(ddlRoom1.SelectedValue), Convert.ToInt32(ddlSession1.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate1.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot1.SelectedValue));
        CancelDynamicSeating.Controls.Add(new LiteralControl(x));
        updpnl_CancelPlan.Update();
        divSeatingPlan2.Visible = true;
        ddlRoom1.Focus();
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        int examType = 2;         //rbCancelInternal.Checked == true ? 1 : 2;

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + Convert.ToInt32(ddlSession1.SelectedValue) + "," + Convert.ToInt32(ddlslot1.SelectedValue) + "," + Convert.ToDateTime(ddlExamdate1.SelectedItem.Text).ToString("dd/MM/yyyy") + "," + Convert.ToInt32(ddlRoom1.SelectedValue) + ",0," + Convert.ToInt32(Session["userno"]) + "," + examType + ",6";
        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, DT, true, 2);

        if (que_out == "6")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CancelPlan, updpnl_CancelPlan.GetType(), "key", "alert('Seat DeAllocated Successfully for " + ddlRoom1.SelectedItem.Text + ".');",
true);
            ClearTab2();
        }
        else if (que_out == "-99")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CancelPlan, updpnl_CancelPlan.GetType(), "key", "alert('Server Error.');", true);
        }
    }

    protected void btnClear1_Click(object sender, EventArgs e)
    {
        ClearTab2();
    }

    protected void txtExamDate2_TextChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(txtExamDate2.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp(" ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updpnl_PlanReports, "No Exams Are Conducted on Selected Date", this.Page);
            txtExamDate2.Text = string.Empty;
        }
        else
        {
            objCommon.FillDropDownList(ddlSlot2, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    }

    void ClearTab1()
    {
        // //ddlslot.Items.Clear();
        // ddlBlock.Items.Clear();
        // ddlFloor.Items.Clear();
        // ddlRoom.Items.Clear();
        //// ddlslot.Items.Add(new ListItem("Please Select", "0"));
        // ddlBlock.Items.Add(new ListItem("Please Select", "0"));
        // ddlFloor.Items.Add(new ListItem("Please Select", "0"));
        // ddlRoom.Items.Add(new ListItem("Please Select", "0"));

        ddlSession.SelectedIndex = ddlslot.SelectedIndex = ddlBlock.SelectedIndex = ddlFloor.SelectedIndex = ddlRoom.SelectedIndex = 0;
        ddlExamdate.SelectedItem.Text = string.Empty;

        divSeatingPlan.Visible = false;
        btnSave.Enabled = false;

        lblroomcapacityHead.Text = lblroomcapacity.Text = lbltotcountHead.Text = lbltotcount.Text = string.Empty;
    }

    void ClearTab2()
    {
        ddlslot1.Items.Clear();
        ddlRoom1.Items.Clear();
        ddlslot1.Items.Add(new ListItem("Please Select", "0"));
        ddlRoom1.Items.Add(new ListItem("Please Select", "0"));
        ddlSession1.SelectedIndex = ddlslot1.SelectedIndex = ddlRoom1.SelectedIndex = 0;
        txtExamDate1.Text = string.Empty;
        divSeatingPlan2.Visible = false;
        ddlExamdate1.SelectedIndex = 0;
    }

    void ClearTab3()
    {
        ddlExamDate3.Items.Clear();
        ddlExamDate3_1.Items.Clear();
        ddlSlot3.Items.Clear();
        ddlSlot3_1.Items.Clear();

        ddlExamDate3.Items.Add(new ListItem("Please Select", "0"));
        ddlExamDate3_1.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot3.Items.Add(new ListItem("Please Select", "0"));
        ddlSlot3_1.Items.Add(new ListItem("Please Select", "0"));


        ddlSession3.SelectedIndex = ddlExamDate3.SelectedIndex = ddlSlot3.SelectedIndex = ddlSession3_1.SelectedIndex = ddlExamDate3_1.SelectedIndex = ddlSlot3_1.SelectedIndex = 0;
    }

    void ClearTab4()
    {
        ddlSession2.SelectedIndex = 0;
        txtExamDate2.Text = string.Empty;
    }

    void GenerateReports(string reportTitle, string rptFileName, int ReportNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession2.SelectedValue) + ",@P_EXAM_DATE=" + Convert.ToString(txtExamDate2.Text) + ",@P_SLOTNO=" + Convert.ToInt32(ddlSlot2.SelectedValue) + ",@P_REPORT_NO=" + ReportNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpnl_PlanReports, this.updpnl_PlanReports.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    void GenerateReports_Internal(string reportTitle, string rptFileName, int ReportNo)
    {
        try
        {
            string Exam_Name = "";
            string SemesterNo = "";
            string Final_Exam_Name = "";

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < ddlExamName.Items.Count; j++)
                {
                    if (ddlExamName.Items[j].Text.Contains("CAT " + i + "") && ddlExamName.Items[j].Selected)
                    {
                        if (!Exam_Name.Contains(ddlExamName.Items[j].Value.Split('^')[0]))
                        {
                            Exam_Name = " (EN.EXAMNAME_LIKE_XY" + ddlExamName.Items[j].Value.Split('^')[0] + "XZ";
                        }

                        SemesterNo += "" + ddlExamName.Items[j].Value.Split('^')[1] + "$";
                    }
                }
                if (Exam_Name != "")
                {
                    SemesterNo = (SemesterNo.Remove(SemesterNo.Length - 1, 1)).Trim();
                    Final_Exam_Name += Exam_Name + " AND ED.SEMESTERNO IN (" + SemesterNo + ")) OR ";
                    Exam_Name = SemesterNo = "";
                }
            }

            Final_Exam_Name = (Final_Exam_Name.Remove(Final_Exam_Name.Length - 3, 3)).Trim();

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession2.SelectedValue) + ",@P_EXAM_DATE=" + Convert.ToString(txtExamDate2.Text) + ",@P_EXAM_NAME=" + Final_Exam_Name + ",@P_SEMESTERNO=0,@P_REPORT_NO=" + ReportNo + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_BRANCHNO=" + ddlBranch2.SelectedValue + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpnl_PlanReports, this.updpnl_PlanReports.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    bool ReportTabValidation()
    {
        //if (ddlExamType.SelectedIndex == 0)   //Commented by Sachin A dt on 24012023 because only extermark Seat allotment requirement in future internal exam use
        //{
        //    ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Exam Type.');", true);
        //    return false;
        //}
        //else
        if (ddlSession2.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Session.');", true);
            return false;
        }
        else if (ddlExamType.SelectedValue != "1")
        {
            if (txtExamDate2.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Exam Date.');", true);
                return false;
            }
        }
        else if (ddlExamType.SelectedValue == "1")
        {
            int ListItemCount = 0;
            for (int i = 0; i < ddlExamName.Items.Count; i++)
            {
                if (ddlExamName.Items[i].Selected)
                {
                    ListItemCount++;
                }

            }

            if (ListItemCount == 0)
            {
                ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "key", "alert('Please Select Exam Name.');", true);
                return false;
            }
        }

        return true;
    }

    protected void btnSeatingPlanReport_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            //string x = GetAlreadyAllotedSeatsForPrint(5, 0, Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToString(Convert.ToDateTime(txtExamDate2.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlSlot2.SelectedValue));
            string x = GetAlreadyAllotedSeatsForPrint(5, ViewState["RoomNo"].ToString(), Convert.ToInt32(ddlSession2.SelectedValue), Convert.ToString(Convert.ToDateTime(txtExamDate2.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlSlot2.SelectedValue)); // ADDED BY NARESH BEERLA AS PER REQUIREMENT ON DT 12/07/2022 CHANGES DONE IN ROOMS.
            divSeatingPlanReport.Controls.Add(new LiteralControl(x));
            updpnl_PlanReports.Update();
            ScriptManager.RegisterStartupScript(updpnl_PlanReports, updpnl_PlanReports.GetType(), "Query", "ShowSeatingPlanReport()", true);
        }
    }

    protected void btnClassWiseHallAllotment_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports("DepartmentwiseHallAllotment", "DepartmentwiseHallAllotment.rpt", 1);
        }
    }

    protected void btnSubjectWiseHallAllotment_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports("SubjectWiseHallAllotment", "rptSubjectWiseHallAllotment.rpt", 2);
        }
    }

    protected void btnDateSessionWiseHallAllotment_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports("DateSessionWiseHallAllotment", "rptDateAndSessionWiseHallAllotment.rpt", 3);
        }
    }

    protected void btnEndSemReport_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports("AnswerScriptCollectionReport", "AnswerScriptCollectionReport.rpt", 4);
        }
    }

    protected void ddlExamDate3_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamDate3.SelectedValue)).ToString("dd/MM/yyyy");

        objCommon.FillDropDownList(ddlSlot3, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR,EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO");
    }
    protected void ddlSession3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlExamDate3, "ACD_EXAM_DATE", "DISTINCT EXAMDATE", "CONVERT(VARCHAR,EXAMDATE,103) AS DATE2", "SESSIONNO=" + Convert.ToInt32(ddlSession3.SelectedValue) + "", "EXAMDATE");
        objCommon.FillDropDownList(ddlExamDate3, "ACD_SEATING_ARRANGEMENT asa INNER JOIN ACD_SEATING_ARRANGEMENT_LOG asal ON (asa.SESSIONNO = asal.SESSIONNO AND asa.ROOMNO = asal.ROOMNO AND convert(varchar,asa.EXAMDATE,103) = asal.EXAMDATE)", "DISTINCT ASA.EXAMDATE", "asal.EXAMDATE AS DATE2", "asa.SESSIONNO=" + Convert.ToInt32(ddlSession3.SelectedValue) + " AND ISNULL(asal.STATUS,0)=1", "ASA.EXAMDATE");

        objCommon.FillDropDownList(ddlExamDate3_1, "ACD_EXAM_DATE", "DISTINCT EXAMDATE", "CONVERT(VARCHAR,EXAMDATE,103) AS DATE2", "SESSIONNO=" + Convert.ToInt32(ddlSession3.SelectedValue) + "", "EXAMDATE");

        if (ddlExamDate3.Items.Count == 1)
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CopyPlan, updpnl_CopyPlan.GetType(), "key", "alert('No Plan Found For Selected Session !!');", true);
            ddlSession3.SelectedIndex = ddlSession3_1.SelectedIndex = 0;
        }
        else
        {
            ddlSession3_1.SelectedIndex = ddlSession3.SelectedIndex;
        }
    }
    protected void ddlSlot3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSession3_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlExamDate3_1, "ACD_EXAM_DATE", "DISTINCT EXAMDATE", "CONVERT(VARCHAR,EXAMDATE,103) AS DATE2", "SESSIONNO=" + Convert.ToInt32(ddlSession3_1.SelectedValue) + "", "EXAMDATE");
    }
    protected void ddlExamDate3_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamDate3_1.SelectedValue)).ToString("dd/MM/yyyy");

        objCommon.FillDropDownList(ddlSlot3_1, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "CONVERT(VARCHAR,EXAMDATE,103)='" + EXAMDATE + "'", "SLOTNO");
    }
    protected void ddlSlot3_1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCopyPlan3_Click(object sender, EventArgs e)
    {
        int examType = 2;     //rbCopyInternal.Checked == true ? 1 : 2;

        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_EXAM_TYPE, @P_OPERATION";
        string Call_Values = "0," + Convert.ToInt32(ddlSession3.SelectedValue) + "," + Convert.ToInt32(ddlSlot3.SelectedValue) + "," + Convert.ToDateTime(ddlExamDate3.SelectedValue).ToString("dd/MM/yyyy") + "," + Convert.ToDateTime(ddlExamDate3_1.SelectedValue).ToString("dd/MM/yyyy") + "," + Convert.ToString(ddlSlot3_1.SelectedValue) + "," + Convert.ToInt32(Session["userno"]) + "," + examType + ",7";

        DataTable DT = new DataTable();
        DT.Columns.Add("SESSIONNO");
        DT.Columns.Add("REGNO");
        DT.Columns.Add("ROOMNO");
        DT.Columns.Add("SEATNO");
        DT.Columns.Add("SEATNO_N");
        DT.Columns.Add("LOCK");
        DT.Columns.Add("EXAMDATE");
        DT.Columns.Add("SLOT");

        string que_out = objCommon.DynamicSPCall_IUD(SP_Name, SP_Parameters, Call_Values, DT, true, 2);
        if (que_out == "1")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CopyPlan, updpnl_CopyPlan.GetType(), "key", "alert('Copy Seating Plan done Successfully.');", true);
            ClearTab3();
        }
        else if (que_out == "-1")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CopyPlan, updpnl_CopyPlan.GetType(), "key", "alert('Seating Plan is Already Exists for " + ddlExamDate3_1.SelectedItem.Text + " !!');", true);
            return;
        }
        else if (que_out == "-99")
        {
            ScriptManager.RegisterClientScriptBlock(updpnl_CopyPlan, updpnl_CopyPlan.GetType(), "key", "alert('Server Error.');", true);
        }
        DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
    }

    protected void btnClear3_Click(object sender, EventArgs e)
    {
        ClearTab3();
    }

    protected void btnInternalBranchWise_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports_Internal("InternalBranchWise", "InternalBranchWise.rpt", 1);
        }
    }
    protected void btnInternalRoomWise_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports_Internal("InternalRoomWise", "InternalRoomWise.rpt", 2);
        }
    }
    protected void btnInternalRoomAndSectionWise_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            GenerateReports_Internal("InternalRoomAndSectionWise", "InternalRoomAndSectionWise.rpt", 3);
        }
    }
    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamType.SelectedValue == "0")
        {
            reportExamName.Visible = false;
            reportExamDate.Visible = false;

            btnSeatingPlanReport.Visible = btnClassWiseHallAllotment.Visible = btnSubjectWiseHallAllotment.Visible = btnDateSessionWiseHallAllotment.Visible = btnEndSemReport.Visible = btnInternalBranchWise.Visible = btnInternalRoomWise.Visible = btnInternalRoomAndSectionWise.Visible = false;
        }
        else if (ddlExamType.SelectedValue == "1")
        {
            reportExamName.Visible = true;
            reportExamDate.Visible = false;

            btnSeatingPlanReport.Visible = btnClassWiseHallAllotment.Visible = btnSubjectWiseHallAllotment.Visible = btnDateSessionWiseHallAllotment.Visible = btnEndSemReport.Visible = false;
            btnInternalBranchWise.Visible = btnInternalRoomWise.Visible = btnInternalRoomAndSectionWise.Visible = btnInternalBranchWise_Exl.Visible = true;
        }
        else
        {
            reportExamName.Visible = false;
            reportExamDate.Visible = true;

            btnSeatingPlanReport.Visible = btnClassWiseHallAllotment.Visible = btnSubjectWiseHallAllotment.Visible = btnDateSessionWiseHallAllotment.Visible = btnEndSemReport.Visible = true;
            btnInternalBranchWise.Visible = btnInternalRoomWise.Visible = btnInternalRoomAndSectionWise.Visible = btnInternalBranchWise_Exl.Visible = false;
        }
    }
    protected void ddlSession2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlDegree2, " ACD_STUDENT_RESULT sr INNER JOIN ACD_SCHEME sc ON sr.SCHEMENO = sc.SCHEMENO INNER JOIN ACD_DEGREE ad ON sc.DEGREENO = ad.DEGREENO", "DISTINCT ad.DEGREENO", "ad.DEGREENAME", "sr.SESSIONNO = " + Convert.ToInt32(ddlSession2.SelectedValue) + "", "ad.DEGREENAME");
        objCommon.FillDropDownList(ddlDegree2, " ACD_STUDENT_RESULT sr INNER JOIN ACD_SCHEME sc ON sr.SCHEMENO = sc.SCHEMENO INNER JOIN ACD_DEGREE ad ON sc.DEGREENO = ad.DEGREENO", "DISTINCT ad.DEGREENO", "ad.DEGREENAME", "sr.SESSIONNO = " + Convert.ToInt32(ddlSession2.SelectedValue) + "", "ad.DEGREENAME");
        string dates = DateTime.Now.ToString("yyyy-MM-dd");

        //Added dt on 22012023
        if (ddlSession2.SelectedIndex > 0)
        {
            reportExamName.Visible = false;
            reportExamDate.Visible = true;

            btnSeatingPlanReport.Visible = btnClassWiseHallAllotment.Visible = btnSubjectWiseHallAllotment.Visible = btnDateSessionWiseHallAllotment.Visible = btnEndSemReport.Visible = true;
            btnInternalBranchWise.Visible = btnInternalRoomWise.Visible = btnInternalRoomAndSectionWise.Visible = btnInternalBranchWise_Exl.Visible = false;
        }         
    }
    protected void ddlDegree2_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch2, "ACD_STUDENT_RESULT sr INNER JOIN ACD_SCHEME sc ON sr.SCHEMENO = sc.SCHEMENO INNER JOIN ACD_BRANCH ab ON sc.BRANCHNO = ab.BRANCHNO", "DISTINCT ab.BRANCHNO", "ab.LONGNAME", "sr.SESSIONNO = " + Convert.ToInt32(ddlSession2.SelectedValue) + " AND sc.DEGREENO = " + Convert.ToInt32(ddlDegree2.SelectedValue) + "", "ab.LONGNAME");
    }

    protected void btnInternalBranchWise_Exl_Click(object sender, EventArgs e)
    {
        if (ReportTabValidation() == true)
        {
            //GenerateReports_Internal_Excel("InternalBranchWise", "InternalBranchWise_Exl.rpt", 1);

            string Exam_Name = "";
            string SemesterNo = "";
            string Final_Exam_Name = "";

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < ddlExamName.Items.Count; j++)
                {
                    if (ddlExamName.Items[j].Text.Contains("CAT " + i + "") && ddlExamName.Items[j].Selected)
                    {
                        if (!Exam_Name.Contains(ddlExamName.Items[j].Value.Split('^')[0]))
                        {
                            Exam_Name = " (EN.EXAMNAME_LIKE_XY" + ddlExamName.Items[j].Value.Split('^')[0] + "XZ";
                        }

                        SemesterNo += "" + ddlExamName.Items[j].Value.Split('^')[1] + "$";
                    }
                }
                if (Exam_Name != "")
                {
                    SemesterNo = (SemesterNo.Remove(SemesterNo.Length - 1, 1)).Trim();
                    Final_Exam_Name += Exam_Name + " AND ED.SEMESTERNO IN (" + SemesterNo + ")) OR ";
                    Exam_Name = SemesterNo = "";
                }
            }

            Final_Exam_Name = (Final_Exam_Name.Remove(Final_Exam_Name.Length - 3, 3)).Trim();


            DynamicAL_v2.DynamicControllerAL AL = new DynamicAL_v2.DynamicControllerAL();
            string SP = "PKG_SEATING_PLAN_REPORTS_INTERNAL"
                  , PR = "@P_SESSIONNO ,@P_EXAM_DATE ,@P_EXAM_NAME ,@P_BRANCHNO ,@P_SEMESTERNO ,@P_REPORT_NO"
                  , VL = "" + Convert.ToInt32(ddlSession2.SelectedValue) + ",'10/09/2020'," + Final_Exam_Name + "," + Convert.ToString(ddlBranch2.SelectedValue) + ",'0',11";
            DataSet ds = AL.DynamicSPCall_Select(SP, PR, VL);

            ExportToExcel(ds.Tables[0], "Internal Branch Wise");
        }
    }

    void ExportToExcel(DataTable dt, string FileName)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            //wb.Worksheets.Add(dt, "Table");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }


    void GenerateReports_Internal_Excel(string reportTitle, string rptFileName, int ReportNo)
    {
        try
        {
            string Exam_Name = "";
            string SemesterNo = "";
            string Final_Exam_Name = "";

            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < ddlExamName.Items.Count; j++)
                {
                    if (ddlExamName.Items[j].Text.Contains("CAT " + i + "") && ddlExamName.Items[j].Selected)
                    {
                        if (!Exam_Name.Contains(ddlExamName.Items[j].Value.Split('^')[0]))
                        {
                            Exam_Name = " (EN.EXAMNAME_LIKE_XY" + ddlExamName.Items[j].Value.Split('^')[0] + "XZ";
                        }

                        SemesterNo += "" + ddlExamName.Items[j].Value.Split('^')[1] + "$";
                    }
                }
                if (Exam_Name != "")
                {
                    SemesterNo = (SemesterNo.Remove(SemesterNo.Length - 1, 1)).Trim();
                    Final_Exam_Name += Exam_Name + " AND ED.SEMESTERNO IN (" + SemesterNo + ")) OR ";
                    Exam_Name = SemesterNo = "";
                }
            } 
            Final_Exam_Name = (Final_Exam_Name.Remove(Final_Exam_Name.Length - 3, 3)).Trim();
             
            string exporttype = "xls";
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle + ".xls";
             
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession2.SelectedValue) + ",@P_EXAM_DATE=" + Convert.ToString(txtExamDate2.Text) + ",@P_EXAM_NAME=" + Final_Exam_Name + ",@P_SEMESTERNO=0,@P_REPORT_NO=" + ReportNo + ",@P_BRANCHNO=" + ddlBranch2.SelectedValue + "";

            string Print_Val = @"window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this.updpnl_PlanReports, this.updpnl_PlanReports.GetType(), "key", Print_Val, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
     
    protected void ddlSlot2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSlot2.SelectedIndex > 0)
        {
            //string ExamDate = Convert.ToDateTime(txtExamDate2.Text).ToString("dd/MM/yyyy");
            string ExamDate = Convert.ToDateTime(txtExamDate2.Text).ToString("yyyy/MM/dd");
            //ddlRooms.Items.Clear();
            //ddlRooms.Items.Add(new ListItem("Please Select", "0"));
            //ddlRooms.SelectedIndex = 0;

            DataSet ds = objCommon.FillDropDown("ACD_SEATING_ARRANGEMENT ASA INNER JOIN ACD_ROOM AR ON (AR.ROOMNO = ASA.ROOMNO)", "DISTINCT(AR.ROOMNO)", "AR.ROOMNAME", "ASA.SESSIONNO =" + ddlSession2.SelectedValue + "	AND ASA.EXAMDATE ='" + ExamDate + "'	AND (ASA.SLOTNO	 =" + ddlSlot2.SelectedValue + "	OR	" + ddlSlot2.SelectedValue + " =0) ", "AR.ROOMNO");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ddlRooms.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
            }
            ddlRooms.Focus();
            // ddlRooms.ClearSelection();
        }
        else
        {
            ddlSlot2.SelectedIndex = 0;
            ddlSlot2.Focus();
        }
    }
    protected void ddlRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        string room = GetRooms();
        //  room = room.Replace('$', ',');

        ViewState["RoomNo"] = room;
        string[] RoomNo = room.Split(',');
    }

    private string GetRooms()
    {
        string roomNo = "";
        string roomno = string.Empty;
        updpnl_PlanReports.Update();
        foreach (ListItem item in ddlRooms.Items)
        {
            if (item.Selected == true)
            {
                roomNo += item.Value + '$';
            }
        }

        if (roomNo != "")
        {
            roomno = roomNo.Substring(0, roomNo.Length - 1);
            string[] degValue = roomno.Split('$');
        }
        if (roomno == "")
        {
            roomno = "0";
        }

        return roomno;

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dates = DateTime.Now.ToString("yyyy-MM-dd");
        objCommon.FillDropDownList(ddlExamdate, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession.SelectedValue + " AND EXAMDATE IS NOT NULL AND EXAMDATE >='" + dates + "'", "SLOTNO");
    }
    protected void ddlExamdate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExamdate.SelectedIndex > 0)
        {
            string EXAMDATE = (Convert.ToDateTime(ddlExamdate.SelectedItem.Text)).ToString("yyyy-MM-dd");
            string a = objCommon.LookUp("ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
            if (a.ToString() == "0")
            {
                objCommon.DisplayUserMessage(updpnl_CancelPlan, "No Exams Are Conducted on Selected Date", this.Page);
                ddlslot.SelectedValue = "0";
                ddlExamdate.SelectedValue = "0";
            }
            else
            {
                objCommon.FillDropDownList(ddlslot, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
            }
        }
    }
    protected void ddlExamdate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EXAMDATE = (Convert.ToDateTime(ddlExamdate1.SelectedItem.Text)).ToString("yyyy-MM-dd");
        string a = objCommon.LookUp("ACD_EXAM_DATE", "COUNT(1)", "EXAMDATE='" + EXAMDATE + "'");
        if (a.ToString() == "0")
        {
            objCommon.DisplayUserMessage(updpnl_CancelPlan, "No Exams Are Conducted on Selected Date", this.Page);
            ddlslot1.SelectedValue = "0";
            txtExamDate1.Text = string.Empty;
        }
        else
        {
            objCommon.FillDropDownList(ddlslot1, "ACD_EXAM_DATE AED INNER JOIN ACD_EXAM_TT_SLOT AEIS ON AEIS.SLOTNO=AED.SLOTNO", "distinct aed.SLOTNO", "SLOTNAME", "EXAMDATE='" + EXAMDATE + "'", "SLOTNO");
        }
    } 
    protected void ddlSession1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dates = DateTime.Now.ToString("yyyy-MM-dd");
        objCommon.FillDropDownList(ddlExamdate1, "ACD_EXAM_DATE", "EXDTNO", "CONVERT(VARCHAR(100),EXAMDATE,103) AS DATE", "SESSIONNO=" + ddlSession1.SelectedValue + " AND EXAMDATE IS NOT NULL AND EXAMDATE >='" + dates + "'", "SLOTNO");       
    }
      
    void GenerateDynamicSeats(string ViewStateName, int BranchCount, int FullCount)
    {
        rbExternal.Checked = true;

        if (rbExternal.Checked)
        {
            #region External Exam
            DataSet ds = objCommon.FillDropDown("ACD_ROOM ar INNER JOIN ACD_SEATING_PLAN asp ON (ar.ROOMNO = asp.ROOMNO) INNER JOIN	ACD_FLOOR af ON (AF.FLOORNO = ar.FLOORNO) INNER JOIN ACD_BLOCK aab ON(AAB.BLOCKNO = ar.BLOCKNO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SL ON (SL.ROOMNO = AR.ROOMNO AND SL.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CONVERT(VARCHAR,SL.EXAM_DATE,103)='" + ddlExamdate.SelectedItem.Text + "' AND SL.SLOTNO=" + Convert.ToInt32(ddlslot.SelectedValue) + ")", "(CAST(ar.ROOMNO AS VARCHAR)+'#'+(aab.BLOCKNAME)+'#'+(af.FLOORNAME)+'#'+CAST(ar.ROOMNAME AS VARCHAR)+'$'+CAST(asp.DISABLED_IDS AS VARCHAR)) AS A", "(CAST(AR.ROWS AS VARCHAR)+'$'+CAST(AR.COLUMNS AS VARCHAR)+'$'+CAST(((AR.ROWS*AR.COLUMNS)-ISNULL(SL.FILLCOUNT,0)) AS VARCHAR)) AS B", "ISNULL(AR.ACTIVESTATUS,0) = 1 AND (AR.BLOCKNO = " + ddlBlock.SelectedValue + " OR 0 = " + ddlBlock.SelectedValue + ") AND (AR.FLOORNO = " + ddlFloor.SelectedValue + " OR 0 = " + ddlFloor.SelectedValue + ") AND (AR.ROOMNO = " + ddlRoom.SelectedValue + " OR 0 = " + ddlRoom.SelectedValue + ")", "ar.ROOMNO");

            string[] TotalRooms = new string[ds.Tables[0].Rows.Count];
            string RoomNo = "";
            string RoomName = "";
            string Rows = "";
            string Columns = "";
            string Capacity = "";
            string BlockName = "";
            string FloorName = "";

            string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == (ds.Tables[0].Rows.Count - 1))
                {
                    RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0];
                    BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1];
                    FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2];
                    RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3];
                    Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0];
                    Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1];
                    Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2];
                }
                else
                {
                    RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0] + "$";
                    BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1] + ",";
                    FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2] + ",";
                    RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3] + "!";
                    Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0] + ",";
                    Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1] + ",";
                    Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2] + ",";
                }
                TotalRooms[i] = "0";
            }

            int TempCapacity = 0;
            int TempRoomNo = 0;
            int TempColNo = 0;
            int TempRow = 0;
            int TempColumn = 0;

            string TempRoomName = "";
            string TempFloorName = "";
            string TempBlockName = "";
            int TempX = 0;
            int TempC = 0;              // Temporary Column
            int TempR = 0;              // Temporary Row
            int TempI = 0;              // Temporary Increment 
            int TempCS = 0;             // Temporary Column Starting Value
            string Disable_Ids = "";    // Storing Disabled IDs
            int FlagD = 0;              // If flag equals to 1 then that Bench is not in use.

            int SeatNo = 1;             // Seating no for all students those applied for Exam
            int RoomSeatNo = 1;           // Seat No for Room
            string DynamicSrting = "";
            int BlankDTcount = 1;
            //string ColMd = "";
            int LoopBreak = 1;
            int BBC = 0;
            double ColWidthX = 0;

            DataTable TempTable = new DataTable();
            TempTable.Columns.Add("SESSIONNO");
            TempTable.Columns.Add("REGNO");
            TempTable.Columns.Add("ROOMNO");
            TempTable.Columns.Add("SEATNO");
            TempTable.Columns.Add("SEATNO_N");
            TempTable.Columns.Add("LOCK");
            TempTable.Columns.Add("EXAMDATE");
            TempTable.Columns.Add("SLOT");

            //Get Max Branch Student Count
            int[] arrCount = new int[BranchCount];
            for (int i = 0; i < BranchCount; i++)
            {
                DataTable dt = (DataTable)ViewState[ViewStateName.Split(',')[i]];
                arrCount[i] = dt.Rows.Count;
            }
            int CountTill = arrCount.Max();
            //End Here

            for (int i = 0; i < CountTill; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {

                    if (FlagD == 1)
                    {
                        FlagD = 0;
                        if (j == 0)
                        {
                            j = BranchCount - 1;
                        }
                        else
                        {
                            j--;
                        }
                    }

                    try
                    {
                        TempCapacity = Convert.ToInt32(Capacity.Split(',')[TempColNo]) - TempX;      //capacity minus 1 use 25012023

                        if (TempCapacity > 0)
                        {
                            TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
                            TempBlockName = BlockName.Split(',')[TempColNo];
                            TempFloorName = FloorName.Split(',')[TempColNo];
                            TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
                            Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
                            TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
                            TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);

                            if (i == 0 && j == 0 && RoomSeatNo == 1)
                            {
                                DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";

                                string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));

                                if (AlreadyAlloted != "^0$0")
                                {
                                    DynamicSrting += AlreadyAlloted.Split('^')[0];
                                    ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);
                                    BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
                                    TempX = BBC;
                                }
                                else
                                {
                                    ViewState["TempC"] = 0;
                                }

                                if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
                                {
                                    TempC = Convert.ToInt32(ViewState["TempC"]);
                                }
                                else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
                                {
                                    TempC = 0;
                                    TempR++;
                                }
                                else
                                {
                                    int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
                                    int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

                                    TempC = Rem;
                                    TempR = quo;
                                }
                            }
                            ColWidthX = Convert.ToDouble(100.00 / TempColumn);
                            TempX++;
                            TotalRooms[TempColNo] = Convert.ToString(TempX - BBC + Convert.ToInt32(ViewState["TempC"]));
                            //TotalRooms[TempColNo] = Convert.ToString(TempX);
                        }
                        else
                        {
                            TempColNo++;
                            if (Capacity.Split(',').Length == TempColNo)
                            {
                                CountTill = 0;
                                BranchCount = 0;
                                break;
                            }
                            TempX = 1;
                            TempC = 0;
                            TempR = 0;
                            TempCS = 0;
                            TempI = 0;
                            RoomSeatNo = 1;
                            TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
                            TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
                            Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
                            TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
                            TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
                            DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";
                            ColWidthX = Convert.ToDouble(100.00 / TempColumn);
                            string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));
                            DynamicSrting += AlreadyAlloted.Split('^')[0];
                            ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);

                            if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
                            {
                                TempC = Convert.ToInt32(ViewState["TempC"]);
                            }
                            else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
                            {
                                TempC = 0;
                                TempR++;
                            }
                            else
                            {
                                int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
                                int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

                                TempC = Rem;
                                TempR = quo;
                            }

                            BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
                            TempX = BBC + 1;

                            TotalRooms[TempColNo] = Convert.ToString(TempX);
                        }

                        DataTable dt = new DataTable();

                        for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
                        {
                            int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
                            if (CheckDisableId == RoomSeatNo)
                            {
                                FlagD = 1;
                                TempC++;
                                break;
                            }
                        }
                        BlankDTcount = 0;
                        if (FlagD != 1)
                        {
                            if (TempC < TempColumn)
                            {
                            ComeHere:
                                if (TempI == BranchCount)
                                {
                                    TempI = 0;
                                }

                                dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

                                if (dt.Rows.Count == 0)
                                {

                                    if (BlankDTcount == BranchCount)
                                    {
                                        BlankDTcount = 0;
                                        LoopBreak = 0;
                                        break;
                                    }
                                    else
                                    {
                                        BlankDTcount++;
                                        TempI++;
                                        goto ComeHere;
                                    }
                                }

                                TempC++;
                            }
                            else
                            {
                                DynamicSrting += "</div><div class='col-md-12 disabled'>";

                                TempC = 0;
                                TempCS++;
                            ComeHere:
                                if (TempCS == BranchCount)
                                {
                                    TempCS = 0;
                                    TempI = TempCS;
                                }
                                else
                                {
                                    TempI = TempCS;
                                }

                                dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
                                if (dt.Rows.Count == 0)
                                {
                                    if (BlankDTcount == BranchCount)
                                    {
                                        BlankDTcount = 0;
                                        LoopBreak = 0;
                                        break;
                                    }
                                    else
                                    {
                                        BlankDTcount++;
                                        TempCS++;
                                        goto ComeHere;
                                    }
                                }

                                TempR++;
                                TempC++;
                            }

                            //TempTable.Rows.Add(ddlSession.SelectedValue, dt.Rows[0][0], TempRoomNo, Alphabets[TempR] + TempC, TempC, 1, Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), ddlslot.SelectedValue);

                            DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
                            DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicSrting += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span>" + dt.Rows[0][0] + "</span><br>";
                            DynamicSrting += "<span>(" + dt.Rows[0]["CODE"].ToString().Trim() + ")</span><br>";
                            DynamicSrting += "<span style='display:none'>" + dt.Rows[0][0] + "</span></div></center></div>";


                            ViewState["LastRegNo"] = dt.Rows[0][0];

                            DataRow dr = dt.Rows[0];
                            dr.Delete();
                            dt.AcceptChanges();
                            ViewState[ViewStateName.Split(',')[TempI]] = dt;
                            #region Commented but VIMP
                            //Remove Completed Branches from Loop
                            //if (dt.Rows.Count == 0)
                            //{
                            //    string reOrder = ViewStateName.ToString();
                            //    if (TempI == BranchCount - 1)
                            //    {
                            //        reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
                            //    }
                            //    else
                            //    {
                            //        reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
                            //    }
                            //    ViewStateName = reOrder;
                            //    BranchCount = BranchCount - 1;
                            //    if (TempI != 0) { TempI--; }
                            //    if (TempC == TempColumn)
                            //    {
                            //        if (TempCS != 0) { TempCS--; }
                            //    }
                            //    if (j != 0) { j--; }
                            //}
                            //End Here
                            #endregion
                            TempI++;
                        }
                        else
                        {
                            DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
                            DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                            DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                            DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

                            if (j == BranchCount - 1)
                            {
                                j--;
                            }
                        }
                        SeatNo++;
                        RoomSeatNo++;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('" + ex.Message + "==" + SeatNo + "~~" + ViewState["LastRegNo"] + "');", true);
                    }
                }
            }

            //for (int z = LoopBreak; z < TempCapacity; z++)
            int z = RoomSeatNo + Convert.ToInt32(ViewState["TempC"]);
            //LoopBreak;
            while (z <= (TempRow * TempColumn))
            {
                if (TempC < TempColumn)
                {
                    TempC++;
                }
                else
                {
                    TempC = 0;
                    TempR++;
                    TempC++;
                }

                for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
                {
                    int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
                    if (CheckDisableId == RoomSeatNo)
                    {
                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                        DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                        DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                        DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

                        FlagD = 1;
                        break;
                    }
                }
                if (FlagD != 1)
                {
                    DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                    DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + TempC + "</p>";
                    DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + TempC + "," + TempC + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                    DynamicSrting += "<div class='col-md-12 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold;'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
                    DynamicSrting += "<span style='color:green;font-weight:bold'>Empty</span><br>";
                    DynamicSrting += "<span style='display:none'>2</span></div></center></div>";
                }
                else
                {
                    FlagD = 0;
                }

                RoomSeatNo++;
                z++;
            }

            ViewState["DynamicString"] = DynamicSrting;
            DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
            updpnl_CreatePlan.Update();
            ViewState["SeatPlan"] = TempTable;
            ViewState["RoomNo"] = RoomNo;
            ViewState["FillCount"] = string.Join("$", TotalRooms);
            divSeatingPlan.Visible = true;
            btnSave.Enabled = true;
            #endregion
        }
        else if (rbInternal.Checked)
        {
            #region Internal Exam
            DataSet ds = objCommon.FillDropDown("ACD_ROOM ar INNER JOIN ACD_SEATING_PLAN asp ON (ar.ROOMNO = asp.ROOMNO) INNER JOIN	ACD_FLOOR af ON (AF.FLOORNO = ar.FLOORNO) INNER JOIN ACD_ACADEMIC_BLOCK aab ON(AAB.BLOCK_NO = ar.BLOCK_NO) LEFT JOIN ACD_SEATING_ARRANGEMENT_LOG SL ON (SL.ROOMNO = AR.ROOMNO AND SL.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND SL.EXAMDATE='" + ddlExamdate.SelectedItem.Text + "')", "(CAST(ar.ROOMNO AS VARCHAR)+'#'+(aab.BLOCK_NAME)+'#'+(af.FLOORNAME)+'#'+CAST(ar.ROOMNAME AS VARCHAR)+'$'+CAST(asp.DISABLED_IDS AS VARCHAR)) AS A", "(CAST(AR.ROWS AS VARCHAR)+'$'+CAST(AR.COLUMNS AS VARCHAR)+'$'+CAST(((AR.ROWS*AR.COLUMNS)*2-ISNULL(SL.FILLCOUNT,0)) AS VARCHAR)) AS B", "ISNULL(AR.STATUS,0) = 1 AND (AR.BLOCK_NO = " + ddlBlock.SelectedValue + " OR 0 = " + ddlBlock.SelectedValue + ") AND (AR.FLOORNO = " + ddlFloor.SelectedValue + " OR 0 = " + ddlFloor.SelectedValue + ") AND (AR.ROOMNO = " + ddlRoom.SelectedValue + " OR 0 = " + ddlRoom.SelectedValue + ")", "ar.ROOMNO");

            string[] TotalRooms = new string[ds.Tables[0].Rows.Count];
            string RoomNo = "";
            string RoomName = "";
            string Rows = "";
            string Columns = "";
            string Capacity = "";
            string BlockName = "";
            string FloorName = "";

            string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == (ds.Tables[0].Rows.Count - 1))
                {
                    RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0];
                    BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1];
                    FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2];
                    RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3];
                    Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0];
                    Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1];
                    Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2];
                }
                else
                {
                    RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0] + "$";
                    BlockName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1] + ",";
                    FloorName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[2] + ",";
                    RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[3] + "!";
                    Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0] + ",";
                    Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1] + ",";
                    Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2] + ",";
                }
                TotalRooms[i] = "0";
            }

            int TempCapacity = 0;
            int TempRoomNo = 0;
            int TempColNo = 0;
            int TempRow = 0;
            int TempColumn = 0;

            string TempRoomName = "";
            string TempFloorName = "";
            string TempBlockName = "";
            int TempX = 0;
            int TempC = 0;              // Temporary Column
            int TempR = 0;              // Temporary Row
            int TempI = 0;              // Temporary Increment 
            int TempCS = 0;             // Temporary Column Starting Value
            string Disable_Ids = "";    // Storing Disabled IDs
            int FlagD = 0;              // If flag equals to 1 then that Bench is not in use.

            int SeatNo = 1;             // Seating no for all students those applied for Exam
            int RoomSeatNo = 1;           // Seat No for Room
            string DynamicSrting = "";
            int BlankDTcount = 1;
            //string ColMd = "";
            int LoopBreak = 1;
            int BBC = 0;
            double ColWidthX = 0;
            int HeaderCnt = 1;

            DataTable TempTable = new DataTable();
            TempTable.Columns.Add("SESSIONNO");
            TempTable.Columns.Add("REGNO");
            TempTable.Columns.Add("ROOMNO");
            TempTable.Columns.Add("SEATNO");
            TempTable.Columns.Add("SEATNO_N");
            TempTable.Columns.Add("LOCK");
            TempTable.Columns.Add("EXAMDATE");
            TempTable.Columns.Add("SLOT");

            //Get Max Branch Student Count
            int[] arrCount = new int[BranchCount];
            for (int i = 0; i < BranchCount; i++)
            {
                DataTable dt = (DataTable)ViewState[ViewStateName.Split(',')[i]];
                arrCount[i] = dt.Rows.Count;
            }
            int CountTill = arrCount.Max();
            //End Here

            for (int i = 0; i < CountTill; i++)
            {
                for (int j = 0; j < BranchCount; j++)
                {

                    if (FlagD == 1)
                    {
                        FlagD = 0;
                        if (j == 0)
                        {
                            j = BranchCount - 1;
                        }
                        else
                        {
                            j--;
                        }
                    }

                    try
                    {
                        TempCapacity = (Convert.ToInt32(Capacity.Split(',')[TempColNo])) - TempX;

                        if (TempCapacity > 0)
                        {
                            TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
                            TempBlockName = BlockName.Split(',')[TempColNo];
                            TempFloorName = FloorName.Split(',')[TempColNo];
                            TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
                            Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
                            TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
                            TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);

                            if (i == 0 && j == 0 && RoomSeatNo == 1)
                            {
                                DynamicSrting += "<div class='row'><center><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></center></h3><br></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";

                                string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan_Internal(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));

                                if (AlreadyAlloted != "^0$0")
                                {
                                    DynamicSrting += AlreadyAlloted.Split('^')[0];
                                    ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);
                                    BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
                                    TempX = BBC;
                                }
                                else
                                {
                                    ViewState["TempC"] = 0;
                                }

                                if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
                                {
                                    TempC = Convert.ToInt32(ViewState["TempC"]);
                                }
                                else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
                                {
                                    TempC = 0;
                                    TempR++;
                                }
                                else
                                {
                                    int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
                                    int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

                                    TempC = Rem;
                                    TempR = quo;
                                }
                            }
                            ColWidthX = Convert.ToDouble(100.00 / TempColumn);
                            TempX++;
                            TotalRooms[TempColNo] = Convert.ToString(TempX - BBC + Convert.ToInt32(ViewState["TempC"]));
                            //TotalRooms[TempColNo] = Convert.ToString(TempX);
                        }
                        else
                        {
                            TempColNo++;
                            if (Capacity.Split(',').Length == TempColNo)
                            {
                                CountTill = 0;
                                BranchCount = 0;
                                break;
                            }
                            TempX = 1;
                            TempC = 0;
                            TempR = 0;
                            TempCS = 0;
                            TempI = 0;
                            RoomSeatNo = 1;
                            TempRoomNo = Convert.ToInt32(RoomNo.Split('$')[TempColNo]);
                            TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
                            Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
                            TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
                            TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
                            DynamicSrting += "<div class='row'><h3><b>" + TempRoomName + "</b> <i>( " + TempFloorName + ", " + TempBlockName + " )</i></h3></div><div class='row sortableX grid'><div class='col-md-12 disabled'>";
                            ColWidthX = Convert.ToDouble(100.00 / TempColumn);
                            string AlreadyAlloted = GetAlreadyAllotedSeatsForSeatingPlan_Internal(4, TempRoomNo, Convert.ToInt32(ddlSession.SelectedValue), Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), Convert.ToInt32(ddlslot.SelectedValue));
                            DynamicSrting += AlreadyAlloted.Split('^')[0];
                            ViewState["TempC"] = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[0]);

                            if (Convert.ToInt32(ViewState["TempC"]) < TempColumn)
                            {
                                TempC = Convert.ToInt32(ViewState["TempC"]);
                            }
                            else if (Convert.ToInt32(ViewState["TempC"]) == TempColumn)
                            {
                                TempC = 0;
                                TempR++;
                            }
                            else
                            {
                                int Rem = Convert.ToInt32(ViewState["TempC"]) % TempColumn;
                                int quo = Convert.ToInt32(ViewState["TempC"]) / TempColumn;

                                TempC = Rem;
                                TempR = quo;
                            }

                            BBC = Convert.ToInt32(AlreadyAlloted.Split('^')[1].Split('$')[1]);
                            TempX = BBC + 1;

                            TotalRooms[TempColNo] = Convert.ToString(TempX);
                        }

                        DataTable dt = new DataTable();

                        for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
                        {
                            int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
                            if (CheckDisableId == RoomSeatNo)
                            {
                                FlagD = 1;
                                TempC += 2;
                                break;
                            }
                        }
                        BlankDTcount = 0;
                        if (FlagD != 1)
                        {
                            if (TempC < TempColumn * 2)
                            {
                            ComeHere:
                                if (TempI == BranchCount)
                                {
                                    TempI = 0;
                                }

                                dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

                                if (dt.Rows.Count == 0)
                                {

                                    if (BlankDTcount == BranchCount)
                                    {
                                        BlankDTcount = 0;
                                        LoopBreak = 0;
                                        break;
                                    }
                                    else
                                    {
                                        BlankDTcount++;
                                        TempI++;
                                        goto ComeHere;
                                    }
                                }
                                //if (TempC % 2 == 0)
                                //{
                                //    TempC--;
                                //}

                                TempC++;
                            }
                            else
                            {
                                DynamicSrting += "</div><div class='col-md-12 disabled'>";

                                TempC = 0;
                                TempCS++;
                            ComeHere:
                                if (TempCS == BranchCount)
                                {
                                    TempCS = 0;
                                    TempI = TempCS;
                                }
                                else
                                {
                                    TempI = TempCS;
                                }

                                dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
                                if (dt.Rows.Count == 0)
                                {
                                    if (BlankDTcount == BranchCount)
                                    {
                                        BlankDTcount = 0;
                                        LoopBreak = 0;
                                        break;
                                    }
                                    else
                                    {
                                        BlankDTcount++;
                                        TempCS++;
                                        goto ComeHere;
                                    }
                                }

                                TempR++;
                                TempC++;
                            }

                            //TempTable.Rows.Add(ddlSession.SelectedValue, dt.Rows[0][0], TempRoomNo, Alphabets[TempR] + TempC, TempC, 1, Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")), ddlslot.SelectedValue);

                            TwoStud++;
                            if (TwoStud % 2 != 0)
                            {
                                DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                                DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p></center>";

                            }

                            DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicSrting += "<center><div class='col-md-6 cellX' style='padding:7px 0px;'><span>" + dt.Rows[0][0] + "</span><br>";
                            DynamicSrting += "<span>(" + dt.Rows[0]["CODE"].ToString().Trim() + ")</span><br>";
                            DynamicSrting += "<span style='display:none'>" + dt.Rows[0][0] + "</span></div></center>";

                            if (TwoStud % 2 == 0)
                            {
                                DynamicSrting += "</div>";
                                //HeaderCnt++;
                            }

                            ViewState["LastRegNo"] = dt.Rows[0][0];

                            DataRow dr = dt.Rows[0];
                            dr.Delete();
                            dt.AcceptChanges();
                            ViewState[ViewStateName.Split(',')[TempI]] = dt;
                            #region Commented but VIMP
                            //Remove Completed Branches from Loop
                            //if (dt.Rows.Count == 0)
                            //{
                            //    string reOrder = ViewStateName.ToString();
                            //    if (TempI == BranchCount - 1)
                            //    {
                            //        reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
                            //    }
                            //    else
                            //    {
                            //        reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
                            //    }
                            //    ViewStateName = reOrder;
                            //    BranchCount = BranchCount - 1;
                            //    if (TempI != 0) { TempI--; }
                            //    if (TempC == TempColumn)
                            //    {
                            //        if (TempCS != 0) { TempCS--; }
                            //    }
                            //    if (j != 0) { j--; }
                            //}
                            //End Here
                            #endregion
                            TempI++;

                            SeatNo++;
                            RoomSeatNo++;
                        }
                        else
                        {
                            DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                            DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p>";
                            DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                            DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                            DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                            DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

                            if (j == BranchCount - 1)
                            {
                                j--;
                            }

                            //TempC += 2;

                            SeatNo += 2;
                            RoomSeatNo += 2;
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('" + ex.Message + "==" + SeatNo + "~~" + ViewState["LastRegNo"] + "');", true);
                    }
                }
            }

            //for (int z = LoopBreak; z < TempCapacity; z++)
            int z = RoomSeatNo + Convert.ToInt32(ViewState["TempC"]);
            //LoopBreak;
            while (z <= (TempRow * TempColumn * 2))
            {
                if (TempC < TempColumn * 2)
                {
                    TempC++;
                }
                else
                {
                    TempC = 0;
                    TempR++;
                    TempC++;
                }

                for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
                {
                    int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
                    if (CheckDisableId == RoomSeatNo)
                    {
                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p>";
                        DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                        DynamicSrting += "<div class='col-md-12 disabled cellX' style='padding:7px 0px;'><span style='color:red;font-weight:bold'><i class='fa fa-ban' aria-hidden='true'></i></span><br>";
                        DynamicSrting += "<span style='color:red;font-weight:bold'>Not in Use</span><br>";
                        DynamicSrting += "<span style='display:none'>1</span></div></center></div>";

                        FlagD = 1;
                        break;
                    }
                }
                if (FlagD != 1)
                {
                    TwoStud++;
                    if (TwoStud % 2 != 0)
                    {
                        DynamicSrting += "<div class='disabled' style='border:1px solid grey;padding:0px 5px;width:" + ColWidthX + "%;float:left'><center>";
                        DynamicSrting += "<p class='disabled' style='border:1px solid grey;background-color:silver;width:100%;margin-bottom:0px;font-weight:bold'>" + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "</p></center>";

                    }

                    DynamicSrting += "<span class='roominfoX' style='display:none'>" + ddlSession.SelectedValue + "," + TempRoomNo + "," + Alphabets[TempR] + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + "," + (int)Math.Ceiling(Convert.ToDecimal(TempC) / 2) + ",1," + Convert.ToString(Convert.ToDateTime(ddlExamdate.SelectedItem.Text).ToString("dd/MM/yyyy")) + "," + ddlslot.SelectedValue + "</span>";
                    DynamicSrting += "<center><div class='col-md-6 cellX' style='padding:7px 0px;'><span style='color:green;font-weight:bold;'><i class='fa fa-check-circle-o' aria-hidden='true'></i></span><br>";
                    DynamicSrting += "<span style='color:green;font-weight:bold'>Empty</span><br>";
                    DynamicSrting += "<span style='display:none'>2</span></div></center>";

                    if (TwoStud % 2 == 0)
                    {
                        DynamicSrting += "</div>";
                    }

                    RoomSeatNo++;
                    z++;
                }
                else
                {
                    FlagD = 0;

                    TempC += 2;
                    RoomSeatNo += 2;
                    z += 2;
                }
            }

            ViewState["DynamicString"] = DynamicSrting;
            DynamicSeating.Controls.Add(new LiteralControl(Convert.ToString(ViewState["DynamicString"])));
            updpnl_CreatePlan.Update();
            ViewState["SeatPlan"] = TempTable;
            ViewState["RoomNo"] = RoomNo;
            ViewState["FillCount"] = string.Join("$", TotalRooms);
            divSeatingPlan.Visible = true;
            btnSave.Enabled = true;
            #endregion
        }

    }
}


#region Commented but 1st Option
//try
//{
//    TempCapacity = Convert.ToInt32(Capacity.Split(',')[TempColNo]) - TempX;
//    if (TempCapacity > 0)
//    {
//        TempRoomNo = Convert.ToInt32(RoomNo.Split(',')[TempColNo]);
//        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
//        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
//        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
//        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
//        TempX++;

//        if (i == 0 && j == 0)
//        {
//            DynamicSrting += "<div class='col-md-12'><h3>" + TempRoomName + "</h3>";
//        }
//    }
//    else
//    {
//        TempColNo++;
//        TempX = 1;
//        TempC = 0;
//        TempR = 0; TempR1 = 0;
//        TempCS = 0;
//        RoomSeatNo = 1;
//        TempRoomNo = Convert.ToInt32(RoomNo.Split(',')[TempColNo]);
//        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
//        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
//        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
//        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
//        DynamicSrting += "</div><div class='col-md-12'><h3>" + TempRoomName + "</h3>";
//    }

//    DataTable dt = new DataTable();

//    for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
//    {
//        int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
//        if (CheckDisableId == RoomSeatNo)
//        {
//            FlagD = 1;
//            break;
//        }
//    }

//    if (TempC < TempColumn)
//    {
//        if (TempI == BranchCount)
//        {
//            TempI = 0;
//        }
//        if (TempR == 0 && TempR1 == 0)
//        {
//            TempI = 0;
//            TempR1++;
//        }

//        dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

//        TempC++;
//        TempI++;
//    }
//    else
//    {
//        if (FlagD != 2)
//        {
//            DynamicSrting += "</div><div class='col-md-12'>";
//            TempC = 1;
//        }
//        else
//        {
//            TempC = 2;
//        }
//        TempR++;
//        TempCS++;

//        if (TempCS == BranchCount)
//        {
//            TempCS = 0;
//            TempI = TempCS;
//        }
//        else
//        {
//            TempI = TempCS;
//        }

//        dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

//        TempI++;
//    }




//    if (FlagD == 1)
//    {
//        DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//        DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:#ff6666'>" + Alphabets[TempR] + TempC + "</div><br>";
//        DynamicSrting += "<b style='color:red'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
//        DynamicSrting += "<b style='color:red'>Not in Use</b></center></div>";

//        TempTable.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], SeatNo, TempRoomNo);

//        if (TempC == TempColumn)
//        {
//            DynamicSrting += "</div><div class='col-md-12'>";

//            DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//            DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//            DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//            DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";

//            FlagD = 2;
//        }
//        else
//        {
//            DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//            DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//            DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//            DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";

//            TempC++;
//            FlagD = 0;
//        }
//    }
//    else
//    {
//        TempTable.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], SeatNo, TempRoomNo);

//        DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//        DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//        DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//        DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";

//        FlagD = 0;
//    }

//    SeatNo++;
//    RoomSeatNo++;
//}
//catch (Exception ex)
//{
//    TempCapacity--; TempC--; TempR--; TempI--; TempCS--; TempR1--; SeatNo--; RoomSeatNo--;
//    FlagD = 0;
//}
#endregion

#region Commented but 2nd Option
//if (TempC < TempColumn)
//{
//    TempC++;

//    if (TempColumn % 2 == 0)
//    {
//        if (TempR % 2 == 0)
//        {
//            dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//        }
//        else
//        {
//            if (TempC == TempColumn)
//            {
//                dt = (DataTable)ViewState[ViewStateName.Split(',')[0]];
//            }
//            else
//            {
//                if (j == BranchCount - 1)
//                {
//                    dt = (DataTable)ViewState[ViewStateName.Split(',')[0]];
//                }
//                else
//                {
//                    dt = (DataTable)ViewState[ViewStateName.Split(',')[j + 1]];
//                }
//            }
//        }
//    }
//    else
//    {
//        dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    }

//    #region
//    //if()

//    //if(Flip==0)
//    //dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    //else
//    //{
//    //    if (TempColumn % 2 == 0)
//    //    {
//    //        if (Flip % 2 == 0)
//    //        {
//    //            dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    //        }
//    //        else
//    //        {
//    //            if (j == 0)
//    //                dt = (DataTable)ViewState[ViewStateName.Split(',')[j + 1]];
//    //            else
//    //                dt = (DataTable)ViewState[ViewStateName.Split(',')[j - 1]];
//    //        }
//    //    }
//    //    else
//    //    {
//    //        dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    //    }
//    //}
//    #endregion
//}
//else
//{
//    DynamicSrting += "</div><div class='col-md-12'>";
//    TempC = 1;
//    TempR++;

//    if (TempColumn % 2 == 0)
//    {
//        if (TempR % 2 == 0)
//        {
//            dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//        }
//        else
//        {
//            if (j == BranchCount - 1)
//            {
//                dt = (DataTable)ViewState[ViewStateName.Split(',')[0]];
//            }
//            else
//            {
//                dt = (DataTable)ViewState[ViewStateName.Split(',')[j + 1]];
//            }
//        }
//    }
//    else
//    {
//        dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    }

//    #region
//    //Flip++;
//    //if (TempColumn % 2 == 0)
//    //{
//    //    if (Flip % 2 == 0)
//    //    {
//    //        dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    //    }
//    //    else
//    //    {
//    //        if (j == 0)
//    //            dt = (DataTable)ViewState[ViewStateName.Split(',')[j + 1]];
//    //        else
//    //            dt = (DataTable)ViewState[ViewStateName.Split(',')[j - 1]];
//    //    }
//    //}
//    //else
//    //{
//    //    dt = (DataTable)ViewState[ViewStateName.Split(',')[j]];
//    //}
//    #endregion
//}
#endregion

#region Final Most Imp Working Backup
//void GenerateDynamicSeats(string ViewStateName, int BranchCount, int FullCount)
//    {
//        DataSet ds = objCommon.FillDropDown("ACD_ROOM ar INNER JOIN ACD_SEATING_PLAN asp ON (ar.ROOMNO = asp.ROOMNO)", "(CAST(ar.ROOMNO AS VARCHAR)+'#'+CAST(ar.ROOMNAME AS VARCHAR)+'$'+CAST(asp.DISABLED_IDS AS VARCHAR)) AS A", "(CAST(AR.ROWS AS VARCHAR)+'$'+CAST(AR.COLUMNS AS VARCHAR)+'$'+CAST((AR.ROWS*AR.COLUMNS) AS VARCHAR)) AS B", "ISNULL(AR.STATUS,0) = 1 AND (AR.BLOCK_NO = " + ddlBlock.SelectedValue + " OR 0 = " + ddlBlock.SelectedValue + ") AND (AR.FLOORNO = " + ddlFloor.SelectedValue + " OR 0 = " + ddlFloor.SelectedValue + ") AND (AR.ROOMNO = " + ddlRoom.SelectedValue + " OR 0 = " + ddlRoom.SelectedValue + ")", "ar.ROOMNO");

//        string RoomNo = "";
//        string RoomName = "";
//        string Rows = "";
//        string Columns = "";
//        string Capacity = "";

//        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

//        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
//        {
//            if (i == (ds.Tables[0].Rows.Count - 1))
//            {
//                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0];
//                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1];
//                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0];
//                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1];
//                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2];
//            }
//            else
//            {
//                RoomNo += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[0] + ",";
//                RoomName += Convert.ToString(ds.Tables[0].Rows[i][0]).Split('#')[1] + "!";
//                Rows += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[0] + ",";
//                Columns += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[1] + ",";
//                Capacity += Convert.ToString(ds.Tables[0].Rows[i][1]).Split('$')[2] + ",";
//            }
//        }


//        int TempCapacity = 0;
//        int TempRoomNo = 0;
//        int TempColNo = 0;
//        int TempRow = 0;
//        int TempColumn = 0;

//        string TempRoomName = "";
//        int TempX = 0;
//        int TempC = 0;              // Temporary Column
//        int TempR = 0;              // Temporary Row
//        int TempI = 0;              // Temporary Increment 
//        int TempCS = 0;             // Temporary Column Starting Value
//        int TempR1 = 0;             // Suppoting for Row Change
//        string Disable_Ids = "";    // Storing Disabled IDs
//        int FlagD = 0;              // If flag equals to 1 then that Bench is not in use.

//        int SeatNo = 1;             // Seating no for all students those applied for Exam
//        int RoomSeatNo = 1;           // Seat No for Room
//        string DynamicSrting = "";
//        DataTable TempTable = new DataTable();
//        TempTable.Columns.Add("IDNO");
//        TempTable.Columns.Add("SHORTNAME");
//        TempTable.Columns.Add("BRANCHNO");
//        TempTable.Columns.Add("SEATNO");
//        TempTable.Columns.Add("ROOMNO");

//        //Get Max Branch Student Count
//        int[] arrCount = new int[BranchCount];
//        for (int i = 0; i < BranchCount; i++)
//        {
//            DataTable dt = (DataTable)ViewState[ViewStateName.Split(',')[i]];
//            arrCount[i] = dt.Rows.Count;
//        }
//        int CountTill = arrCount.Max();
//        //End Here

//        for (int i = 0; i < CountTill; i++)
//        {
//            if (i == 70)
//            {
//                string zxc = "";
//            }
//            for (int j = 0; j < BranchCount; j++)
//            {

//                if (FlagD == 1)
//                {
//                    FlagD = 0;
//                    if (j == 0)
//                    {
//                        j = BranchCount - 1;
//                    }
//                    else
//                    {
//                        j--;
//                    }
//                }

//                try
//                {
//                    TempCapacity = Convert.ToInt32(Capacity.Split(',')[TempColNo]) - TempX;

//                    if (TempCapacity > 0)
//                    {
//                        TempRoomNo = Convert.ToInt32(RoomNo.Split(',')[TempColNo]);
//                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
//                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
//                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
//                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
//                        TempX++;

//                        if (i == 0 && j == 0 && RoomSeatNo == 1)
//                        {
//                            DynamicSrting += "<div class='col-md-12'><h3>" + TempRoomName + "</h3>";
//                        }
//                    }
//                    else
//                    {
//                        TempColNo++;
//                        TempX = 1;
//                        TempC = 0;
//                        TempR = 0; TempR1 = 0;
//                        TempCS = 0;
//                        RoomSeatNo = 1;
//                        TempRoomNo = Convert.ToInt32(RoomNo.Split(',')[TempColNo]);
//                        TempRoomName = RoomName.Split('!')[TempColNo].Split('$')[0];
//                        Disable_Ids = RoomName.Split('!')[TempColNo].Split('$')[1];
//                        TempRow = Convert.ToInt32(Rows.Split(',')[TempColNo]);
//                        TempColumn = Convert.ToInt32(Columns.Split(',')[TempColNo]);
//                        DynamicSrting += "</div><div class='col-md-12'><h3>" + TempRoomName + "</h3>";
//                    }

//                    DataTable dt = new DataTable();

//                    for (int k = 0; k < Disable_Ids.Split(',').Length; k++)
//                    {
//                        int CheckDisableId = Convert.ToString(Disable_Ids.Split(',')[k]) == string.Empty ? 0 : Convert.ToInt32(Disable_Ids.Split(',')[k]);
//                        if (CheckDisableId == RoomSeatNo)
//                        {
//                            FlagD = 1;
//                            TempC++;
//                            break;
//                        }
//                    }

//                    if (FlagD != 1)
//                    {
//                    //Outer:
//                        if (TempC < TempColumn)
//                        {
//                            if (TempI == BranchCount)
//                            {
//                                TempI = 0;
//                            }
//                            if (TempR == 0 && TempR1 == 0)
//                            {
//                                TempI = 0;
//                                TempR1++;
//                            }

//                            //dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];

//                            //Remove Completed Branches from Loop
//                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
//                            if (dt.Rows.Count - 1 == i)
//                            {
//                                string reOrder = ViewStateName.ToString();
//                                if (TempI == BranchCount - 1)
//                                {
//                                    reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
//                                }
//                                else
//                                {
//                                    reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
//                                }
//                                ViewStateName = reOrder;
//                                BranchCount = BranchCount - 1;
//                                TempI--;
//                                TempCS--;
//                            }
//                            //End Here

//                            TempC++;
//                            TempI++;
//                        }
//                        else
//                        {
//                            DynamicSrting += "</div><div class='col-md-12'>";

//                            TempC = 1;
//                            TempR++;
//                            TempCS++;

//                            if (TempCS == BranchCount)
//                            {
//                                TempCS = 0;
//                                TempI = TempCS;
//                            }
//                            else
//                            {
//                                TempI = TempCS;
//                            }

//                            //Remove Completed Branches from Loop
//                            dt = (DataTable)ViewState[ViewStateName.Split(',')[TempI]];
//                            if (dt.Rows.Count - 1 == i)
//                            {
//                                string reOrder = ViewStateName.ToString();
//                                if (TempI == BranchCount - 1)
//                                {
//                                    reOrder = reOrder.Replace("," + Convert.ToString(ViewStateName.Split(',')[TempI]), "");
//                                }
//                                else
//                                {
//                                    reOrder = reOrder.Replace(Convert.ToString(ViewStateName.Split(',')[TempI]) + ",", "");
//                                }
//                                ViewStateName = reOrder;
//                                BranchCount = BranchCount - 1;
//                                TempI--;
//                                TempCS--;
//                            }
//                            //End Here

//                            TempI++;
//                        }

//                        if (170601322 == Convert.ToInt32(dt.Rows[i][0]))
//                        {
//                            string x = "";
//                        }

//                    }

//                    if (FlagD == 1)
//                    {
//                        DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//                        DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:#ff6666'>" + Alphabets[TempR] + TempC + "</div><br>";
//                        DynamicSrting += "<b style='color:red'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
//                        DynamicSrting += "<b style='color:red'>Not in Use</b></center></div>";

//                        if (j == BranchCount - 1)
//                        {
//                            j--;
//                        }
//                    }
//                    else
//                    {
//                        int TempIx = 0;
//                        if (i != 0)
//                        {
//                            TempIx = i;
//                            i--;
//                        }

//                        DataRow[] foundRows = TempTable.Select("IDNO =" + dt.Rows[i][0].ToString() + "");

//                        if (foundRows.Length > 0)
//                        {
//                            i++;
//                            DataRow[] foundRows1 = TempTable.Select("IDNO =" + dt.Rows[i][0].ToString() + "");
//                            if (foundRows1.Length > 0)
//                            {
//                                i++;
//                                DataRow[] foundRows2 = TempTable.Select("IDNO =" + dt.Rows[i][0].ToString() + "");
//                                if (foundRows2.Length > 0)
//                                {
//                                    i++;
//                                }
//                                else
//                                {
//                                    TempTable.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], SeatNo, TempRoomNo);
//                                    DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//                                    //DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//                                    DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//                                    //DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";
//                                }
//                            }
//                            else
//                            {
//                                TempTable.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], SeatNo, TempRoomNo);
//                                DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//                                //DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//                                DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//                                //DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";
//                            }
//                        }
//                        else
//                        {
//                            TempTable.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][2], SeatNo, TempRoomNo);
//                            DynamicSrting += "<div class='col-md-1' style='border:1px solid grey;padding:0px 5px'><center>";
//                            //DynamicSrting += "<div class='col-md-12' style='border:1px solid grey;background-color:silver'>" + Alphabets[TempR] + TempC + "</div><br>";
//                            DynamicSrting += "<span>" + dt.Rows[i][0] + "</span><br>";
//                            //DynamicSrting += "<span>(" + dt.Rows[i][1] + ")</span></center></div>";
//                        }

//                        i = TempIx;
//                        ViewState["LastRegNo"] = dt.Rows[i][0];
//                    }

//                    SeatNo++;
//                    RoomSeatNo++;
//                }
//                catch (Exception ex)
//                {
//                    //TempCapacity--; 
//                    //TempC--; TempR--; TempI--; TempCS--; TempR1--; SeatNo--; RoomSeatNo--;
//                    //FlagD = 0;
//                    ScriptManager.RegisterStartupScript(updpnl_CreatePlan, updpnl_CreatePlan.GetType(), "Query", "alert('" + ex.Message + "==" + i + "~~" + ViewState["LastRegNo"] + "');", true);
//                }
//            }
//        }
//        DynamicSeating.Controls.Add(new LiteralControl(DynamicSrting));
//        updpnl_CreatePlan.Update();
//        ViewState["SeatPlan"] = TempTable;
//    }
#endregion

//string GetAlreadyAllotedSeats(int Operation, int RoomNo, int SessionX, string ExamDateX, int SlotX)
//    {

//        string SP_Name = "PKG_DYNAMIC_SEAT_ALLOTMENT";
//        string SP_Parameters = "@P_TBL, @P_SESSIONNO, @P_SLOTNO, @P_EXAM_DATE, @P_ROOMNO, @P_FILLCOUNT, @P_UA_NO, @P_OPERATION";
//        string Call_Values = "0," + SessionX + "," + SlotX + "," + ExamDateX + "," + RoomNo + ",0," + Convert.ToInt32(Session["userno"]) + "," + Operation + "";

//        DataTable DT = new DataTable();
//        DT.Columns.Add("SESSIONNO");
//        DT.Columns.Add("REGNO");
//        DT.Columns.Add("ROOMNO");
//        DT.Columns.Add("SEATNO");
//        DT.Columns.Add("SEATNO_N");
//        DT.Columns.Add("LOCK");
//        DT.Columns.Add("EXAMDATE");
//        DT.Columns.Add("SLOT");

//        DataSet ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values, DT);

//        DataView view = new DataView(ds.Tables[0]);
//        DataTable RoomInfo = view.ToTable(true, "ROOMNO", "ROOMNAME", "ROWS", "COLUMNS", "ROOMCAPACITY", "ACTUALCAPACITY", "DISABLED_IDS", "BLOCK_NAME", "FLOORNAME", "EXAMDATE", "SLOTNAME");

//        var Rooms = RoomInfo.AsEnumerable().Select(s => s.Field<int>("ROOMNO")).ToArray();

//        string commaSeperatedValues = string.Join(",", Rooms);

//        string DynamicString = "";
//        string BlockName = "";
//        string FloorName = "";
//        string RoomName = "";
//        int rColumns, rRow, rCapacity, raCapacity;
//        string DummyBenches = "";
//        int FlagD = 0;
//        int ColNo = 0;
//        int RowNo = 0;
//        int TempI = 0;
//        string ExamDate = "";
//        string SlotName = "";
//        string ColMd = "";

//        string[] Alphabets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };

//        for (int i = 0; i < RoomInfo.Rows.Count; i++)
//        {
//            DataView dv = new DataView(ds.Tables[0]);
//            dv.RowFilter = "ROOMNO = " + RoomInfo.Rows[i][0] + "";
//            DataTable dt = dv.ToTable();

//            RoomName = Convert.ToString(RoomInfo.Rows[i]["ROOMNAME"]);
//            rColumns = Convert.ToInt32(RoomInfo.Rows[i]["COLUMNS"]);
//            rRow = Convert.ToInt32(RoomInfo.Rows[i]["ROWS"]);
//            rCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ROOMCAPACITY"]);
//            raCapacity = Convert.ToInt32(RoomInfo.Rows[i]["ACTUALCAPACITY"]);
//            DummyBenches = Convert.ToString(RoomInfo.Rows[i]["DISABLED_IDS"]);
//            BlockName = Convert.ToString(RoomInfo.Rows[i]["BLOCK_NAME"]);
//            FloorName = Convert.ToString(RoomInfo.Rows[i]["FLOORNAME"]);
//            ExamDate = Convert.ToString(RoomInfo.Rows[i]["EXAMDATE"]);
//            SlotName = Convert.ToString(RoomInfo.Rows[i]["SLOTNAME"]);

//            if (rColumns == 1) ColMd = "12";
//            else if (rColumns == 2) ColMd = "6";
//            else if (rColumns == 3) ColMd = "4";
//            else if (rColumns == 4) ColMd = "3";
//            else if (rColumns == 5) ColMd = "2";
//            else if (rColumns == 6) ColMd = "2";
//            else if (rColumns > 6) ColMd = "1";

//            TempI = 0;
//            RowNo = 0;

//            //DynamicString += "<div class='col-md-12'><h3><b>" + RoomName + "</b>  <i>( " + FloorName + ", " + BlockName + " )</i></h3></div><div class='col-md-12'>";
//            DynamicString += "<div class='row'><h3><b>" + RoomName + "</b>  <i>( " + FloorName + ", " + BlockName + " )</i></h3></div>";
//            DynamicString += "<div id='responsive-grid' class='row'>";
//            for (int j = 0; j < raCapacity; j++)
//            {
//                FlagD = 0;
//                TempI++;

//                for (int k = 0; k < DummyBenches.Split(',').Length; k++)
//                {
//                    if (Convert.ToInt32(DummyBenches.Split(',')[k]) == TempI)
//                    {
//                        DynamicString += "<div class='col-md-" + ColMd + "' style='border:1px solid grey;padding:0px 5px'><center>";
//                        DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#ff6666'>" + Alphabets[RowNo] + TempI + "</div><br>";
//                        DynamicString += "<b style='color:red' class='FoSty1'><i class='fa fa-ban' aria-hidden='true'></i></b><br>";
//                        DynamicString += "<b style='color:red' class='FoSty1'>Not in Use</b></center></div>";

//                        j--;
//                        FlagD = 1;
//                    }
//                }

//                if (FlagD == 0)
//                {
//                    DynamicString += "<div class='col-md-" + ColMd + "' style='border:1px solid grey;padding:0px 5px'><center>";
//                    DynamicString += "<div class='col-md-12 FoSty1' style='border:1px solid grey;background-color:#3CB371'>" + dt.Rows[j]["SEATNO"] + "</div><br>";
//                    DynamicString += "<span class='FoSty1'>" + dt.Rows[j]["REGNO"] + "</span><br>";
//                    DynamicString += "<span class='FoSty1'>(" + dt.Rows[j]["CODE"].ToString().Trim() + ")</span></center></div>";
//                }
//                ColNo++;
//                if (ColNo == rColumns)
//                {
//                    ColNo = 0;
//                    RowNo++;
//                    //DynamicString += "</div><div class='col-md-12'>";
//                }
//            }
//            DynamicString += "</div>";
//        }

//        return DynamicString;
//    }