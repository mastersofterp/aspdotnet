using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using System.Data;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.Text;
using System.Globalization;
using System.Collections;

public partial class ODTracker_Student_Dashboard_OD : System.Web.UI.Page
{
    Common objCommon = new Common();
    ODTracker objODTracker = new ODTracker();
    ODTrackerController objODTrackerController = new ODTrackerController();
    Boolean IsValidForApply = false;
    DateTime MinimumDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindCourseData();
            //PopulateDropDownList();
            GetStudentODEventData();
            lblCurrentSem.Text = objCommon.LookUp("acd_student", "semesterno", "idno = " + Session["IDNO"].ToString());

        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnApplyOD.ToolTip = "";
            btnApplyOD.Enabled = true;
            if (ddlCategory.SelectedValue == "1")  //event od
            {

                string[] sdates = txtDate.Text.Split('-');
                string EventDate = sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0, 3) + "-" + sdates[2];

                objCommon.FillDropDownList(ddlEvent, "ACD_OD_TRACKER_EVENT_LIST_BY_FACULTY", "EVENT_SRNO", "EVENT_NAME", "REQUEST_STATUS = 2 AND EVENT_SRNO > 0 and IS_PUBLISH = 1 AND START_DATE <= '" + EventDate + "' AND END_DATE >= '" + EventDate + "'", "EVENT_SRNO DESC");
            }
            else if (ddlCategory.SelectedValue == "2") //placement od
            {

                string[] sdates = txtDate.Text.Split('-');
                string EventDate = sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0, 3) + "-" + sdates[2];

                objCommon.FillDropDownList(ddlEvent, "ACD_OD_TRACKER_PLACEMENT_EVENT", "PLACEMENT_SRNO", "PLACEMENT_NAME", "REQUEST_STATUS = 2 AND PLACEMENT_SRNO > 0 and IS_PUBLISH = 1 AND START_DATE <= '" + EventDate + "' AND END_DATE >= '" + EventDate + "'", "PLACEMENT_SRNO DESC");
            }
            else if (ddlCategory.SelectedValue == "0")
            {
                objCommon.DisplayMessage(updStudent, "Please select od type.", this.Page);
                ddlCategory.Focus();
                return;
            }
        }
        catch { }
    }

    protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEventCoOrdinator.Text = "";
        if (ddlEvent.SelectedIndex > 0)
        {
            DataSet dsCoordinatorData = new DataSet();

            if (ddlCategory.SelectedValue == "1")
            {
                dsCoordinatorData = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_LIST_BY_FACULTY A INNER JOIN USER_ACC B ON A.INSERTED_BY = B.UA_NO", "TOP 1 UA_FULLNAME", "UA_NO,IS_SPECIALEVENT", "EVENT_SRNO = " + ddlEvent.SelectedValue, "");
            }
            else if (ddlCategory.SelectedValue == "2")
            {
                dsCoordinatorData = objCommon.FillDropDown("ACD_OD_TRACKER_PLACEMENT_EVENT A INNER JOIN USER_ACC B ON A.PLACEMENT_HEAD = B.UA_NO", "TOP 1 UA_FULLNAME", "B.UA_NO,1 IS_SPECIALEVENT", "PLACEMENT_SRNO = " + ddlEvent.SelectedValue, "");
            }


            if (dsCoordinatorData.Tables[0].Rows.Count > 0)
            {
                lblEventCoOrdinator.Text = dsCoordinatorData.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                lblEventCoOrdinatorId.Text = dsCoordinatorData.Tables[0].Rows[0]["UA_NO"].ToString();

                if (dsCoordinatorData.Tables[0].Rows[0]["IS_SPECIALEVENT"].ToString().ToLower() == "true" || dsCoordinatorData.Tables[0].Rows[0]["IS_SPECIALEVENT"].ToString().ToLower() == "1")
                {
                    lblIsSpecialReq.Text = "yes";
                }
                else
                {
                    lblIsSpecialReq.Text = "no";
                }

            }
            //
            //if (ddlCategory.SelectedValue == "1")
            //{
                BindTimeSlot();
                lstTiming.Enabled = true;
            //}
            //else
            //{
            //    lstTiming.Enabled = false;
            //}
            //
        }
        //Binding Time Slot
        //string ActiveSession = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "");

        //Binding Time Slot End
    }

    private void BindCourseData()
    {
        DataSet ds = new DataSet();
        ds = objCommon.FillDropDown("ACD_STUDENT_RESULT A INNER JOIN ACD_COURSE B ON A.COURSENO = B.COURSENO", "A.COURSENO", "A.COURSENAME,A.CCODE,B.CREDITS", "SESSIONNO = (SELECT MAX(SESSIONNO) FROM ACD_STUDENT_RESULT WHERE IDNO = A.IDNO) AND IDNO = " + Session["IDNO"].ToString(), "");
        rptCourse.DataSource = ds;
        rptCourse.DataBind();
    }

    protected void btnApplyOD_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "1")
        {
            //check od limit set or not 
            int ODLimit = 0;
            Boolean IsOdConfigDone = false;
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT A LEFT JOIN ACD_OD_TRACKER_STUDENT_OD_LIMIT_CONFIG B ON A.SCHEMENO = B.SCHEMENO AND B.ADM_BATCH_NO= A.ADMBATCH", "IDNO", "A.ADMBATCH ,A.SCHEMENO, CASE WHEN B.SCHEMENO IS NOT NULL AND B.ADM_BATCH_NO IS NOT NULL THEN '1' ELSE 0 END OD_CONFIG_DONE,ALLOWED_OD_DAYS", "IDNO = " + Session["IDNO"].ToString(), "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["OD_CONFIG_DONE"].ToString() == "1")
                    {
                        string days = ds.Tables[0].Rows[0]["ALLOWED_OD_DAYS"].ToString();
                        if (!String.IsNullOrEmpty(days))
                        {
                            ODLimit = Convert.ToInt32(days);
                        }
                        IsOdConfigDone = true;
                    }
                    else if (ds.Tables[0].Rows[0]["OD_CONFIG_DONE"].ToString() == "0")
                    {
                        string days = ds.Tables[0].Rows[0]["ALLOWED_OD_DAYS"].ToString();
                        IsOdConfigDone = false;
                        objCommon.DisplayMessage(updStudent, "Od configuration not done.", this.Page);
                        return;
                    }
                }
            }
            if (!IsOdConfigDone)
            {
                objCommon.DisplayMessage(updStudent, "Od configuration not done.", this.Page);
                return;
            }
            //SaveStudentEventOD(int ODLimit);
            SaveStudentEventOD(ODLimit);
        }
        else if (ddlCategory.SelectedValue == "2")
        {
            SavePlacementOD();
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlEventCategory, "ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME", "EVENT_ID>0", "EVENT_ID DESC");
            //objCommon.FillDropDownList(ddlEvent, "ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME", "EVENT_ID>0", "EVENT_ID DESC");
            //objCommon.FillListBox(lstTiming, "ACD_OD_TRACKER_TIME_SLOT", "TIME_SLOT_NO", "START_END_TIME", "TIME_SLOT_NO>0", "TIME_SLOT_NO");

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GetStudentODEventData()
    {
        try
        {
            lvStudentODRequest.DataSource = null;
            lvStudentODRequest.DataBind();
            divMyEvent.Visible = true;
            divStudPlacementEvent.Visible = false;
            DataSet ds = new DataSet();
            ds = objODTrackerController.GetStudentODEventData(Convert.ToInt32(Session["IDNO"]));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudentODRequest.DataSource = ds;
                    lvStudentODRequest.DataBind();
                }
            }

            if (ds.Tables.Count > 1)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ViewState["ODCourseCount"] = ds.Tables[1];
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "1")
        {
            lblCoordinator.Text = "Event Co-Ordinator";
            GetStudentODEventData();
        }
        else if (ddlCategory.SelectedValue == "2")
        {
            lblCoordinator.Text = "Placement Co-Ordinator";
            GetStudentPlacementDataList();
        }
    }

    private void SavePlacementOD()
    {
        objODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        objODTracker.PlacementNo = Convert.ToInt32(ddlEvent.SelectedValue);
        objODTracker.PlacementDate = txtDate.Text;
        objODTracker.StudPlacementComment = txtStdCommment.Text;
        objODTracker.Idno = Convert.ToInt32(Session["idno"]);
        objODTracker.UANO = Convert.ToInt32(Session["userno"].ToString());
        objODTracker.Coordinator_Id = Convert.ToInt32(lblEventCoOrdinatorId.Text);
        int OrgId = Convert.ToInt32(Session["OrgId"]);

        int result = objODTrackerController.InsertODTrackerStudentPlacementEvent(objODTracker);
        if (result == 1)
        {
            objCommon.DisplayMessage(updStudent, "Record saved sucessfully.", this.Page);
            GetStudentPlacementDataList();
        }
        else
        {
            objCommon.DisplayMessage(updStudent, "Record not saved.", this.Page);
        }
    }

    private void GetStudentPlacementDataList()
    {
        try
        {
            lvStudPlacementEvent.DataSource = null;
            lvStudPlacementEvent.DataBind();
            divMyEvent.Visible = false;
            divStudPlacementEvent.Visible = true;
            DataSet ds = new DataSet();
            ds = objODTrackerController.GetStudentPlacementEventData(Convert.ToInt32(Session["IDNO"]));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudPlacementEvent.DataSource = ds;
                    lvStudPlacementEvent.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void SaveStudentEventOD(int ODLimit)
    {
        //
        string[] sdates = txtDate.Text.Split('-');
        string EventDate = sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0, 3) + "-" + sdates[2];

        int MinimumDay = CheckEventDateIsValidOrNot();

        MinimumDate = DateTime.Today.AddDays(-MinimumDay);
        DateTime UserFromDate = DateTime.Parse(EventDate);

        if (MinimumDate <= UserFromDate)
        {
            IsValidForApply = true;
        }
        else if (MinimumDate > UserFromDate)
        {
            IsValidForApply = false;
        }

        //if (MinimumDate > UserFromDate)
        if (!IsValidForApply)
        {
            objCommon.DisplayMessage(updStudent, "Date should not be less than " + MinimumDate.ToString("dd-MMM-yyyy"), this.Page);
            btnApplyOD.ToolTip = "Date should not be less than " + MinimumDate.ToString("dd-MMM-yyyy");
            btnApplyOD.Enabled = false;
            return;
        }
        //

        int result = 0;
        objODTracker.FacultyEvent_SRNO = Convert.ToInt32(ddlEvent.SelectedValue);
        objODTracker.Student_OD_date = txtDate.Text;
        objODTracker.Student_Comment = txtStdCommment.Text;
        objODTracker.Idno = Convert.ToInt32(Session["idno"]);
        objODTracker.UANO = Convert.ToInt32(Session["userno"]);
        objODTracker.Faculty_Event_Uano = Convert.ToInt32(lblEventCoOrdinatorId.Text);
        objODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        objODTracker.SessionNo = Convert.ToInt32(Session["currentsession"]);

        //Getting all Applied OD 
        //DataSet dsStdAppData = objCommon.FillDropDown("ACD_OD_TRACKER_STUDENT_OD_LIST A INNER JOIN ACD_OD_TRACKER_STUD_OD_COURSE_LIST B ON A.STUD_OD_NO = B.STUD_OD_NO", "A.STUD_OD_NO", "FACULTY_EVENT_NO,STUD_OD_DATE,COURSENO", "A.IDNO = " + objODTracker.Idno + " AND FACULTY_EVENT_NO = " + objODTracker.Faculty_Event_Uano + "", "");
        //DataSet dsStdAppData = objCommon.FillDropDown("ACD_OD_TRACKER_STUDENT_OD_LIST A INNER JOIN ACD_OD_TRACKER_STUD_OD_COURSE_LIST B ON A.IDNO = B.IDNO", "DISTINCT A.STUD_OD_NO", "FACULTY_EVENT_NO,STUD_OD_DATE,COURSENO", "A.IDNO = " + objODTracker.Idno + "", "");
        DataSet dsStdAppData = objCommon.FillDropDown("ACD_OD_TRACKER_STUDENT_OD_LIST A INNER JOIN ACD_OD_TRACKER_STUD_OD_COURSE_LIST B ON A.IDNO = B.IDNO INNER JOIN ACD_COURSE C ON B.COURSENO = C.COURSENO", "DISTINCT B.STUD_OD_COURSE_NO", "B.STUD_OD_NO,B.COURSENO,B.IDNO,B.FACULTY_EVENT_UANO,COURSE_NAME", "A.IDNO = " + objODTracker.Idno + "", "");
        //
        //Raw data
        DataTable dtRaw = new DataTable();
        DataColumn col = new DataColumn("CourseNo");
        dtRaw.Columns.Add(col);
        col = new DataColumn("TimeSlotNo");
        dtRaw.Columns.Add(col);
        col = new DataColumn("IsReachMaxLimit");
        dtRaw.Columns.Add(col);
        col = new DataColumn("IsDuplicateRecord");
        dtRaw.Columns.Add(col);
        //
        
        DataTable dtForInsert = new DataTable();
        DataColumn dcForInsert = new DataColumn("CourseNo");
        dtForInsert.Columns.Add(dcForInsert);
        dcForInsert = new DataColumn("TimeSlotNo");
        dtForInsert.Columns.Add(dcForInsert);
                 
        StringBuilder sb = new StringBuilder();
        StringBuilder sbODLimitReachedCourses = new StringBuilder();

        Dictionary<string, string> crsStatus = new Dictionary<string, string>();
        Boolean IscntReach = false;
        Boolean IsODLimitReached = false;
        Boolean IsAtLeastOneRecSelected = false;

        foreach (ListItem listBoxItem in lstTiming.Items)
        {
            if (listBoxItem.Selected == true)
            {
                IsAtLeastOneRecSelected = true;
                string courseno = listBoxItem.Value.Split('-')[0];
                string timeSlot = listBoxItem.Value.Split('-')[1];
                DataRow drw = dtRaw.NewRow();
                if (lblIsSpecialReq.Text.ToLower() != "yes")
                {
                    //DataRow drw = new DataRow();

                    drw["CourseNo"] = courseno;
                    drw["TimeSlotNo"] = timeSlot;
                    drw["IsReachMaxLimit"] = IscntReach;

                    DataRow[] drTmp = dtRaw.Select("Convert(CourseNo, 'System.Int32') = " + courseno);

                    if (drTmp.Count() > 0)
                    {
                        drw["IsDuplicateRecord"] = 1;
                    }
                    else
                    {
                        drw["IsDuplicateRecord"] = 0;
                    }

                    dtRaw.Rows.Add(drw);

                    //IscntReach = IsCheckMaxLimitReach(Convert.ToInt32(courseno));
                    ////crsStatus.Add(courseno, IscntReach.ToString());                    
                    //drw["CourseNo"] = courseno;
                    //drw["TimeSlotNo"] = timeSlot;
                    //drw["IsReachMaxLimit"] = IscntReach;
                    //dtRaw.Rows.Add(drw);
                }
                else
                {
                    drw["CourseNo"] = courseno;
                    drw["TimeSlotNo"] = timeSlot;
                    drw["IsReachMaxLimit"] = "false";
                    dtRaw.Rows.Add(drw);
                    //crsStatus.Add(objODTracker.CourseNo.ToString(), IscntReach.ToString());
                }
            }
        }

        if (ddlCategory.SelectedValue == "1")  //ddl for Event OD
        {
            if (!IsAtLeastOneRecSelected)
            {
                objCommon.DisplayMessage(updStudent, "Please select atleast one time slot!", this.Page);
            }
        }

        int CourseInsertCount = 0;
        if (dtRaw.Rows.Count > 0)
        {
            //Dictionary<int,int> courseWithCount = new Dictionary<int,int>();
            List<clCourseCountOD> courseWithCount = new List<clCourseCountOD>();
            foreach (DataRow dr in dtRaw.Rows)
            {
                int DCourseNo = 0;
                int DCourseCount = 0;
                string sCourseNo = dr["CourseNo"].ToString();
                string sTimeSlot = dr["TimeSlotNo"].ToString();
                string IsReachMaxLimit = dr["IsReachMaxLimit"].ToString();
                string IsDuplicate = dr["IsDuplicateRecord"].ToString();
                int Count = dsStdAppData.Tables[0].AsEnumerable().Where(x => x["CourseNo"].ToString() == sCourseNo.ToString()).ToList().Count;
                if (IsDuplicate == "1" || Count>0)
                {
                    clCourseCountOD objData = new clCourseCountOD();
                    objData.CourseNo = Convert.ToInt32(sCourseNo);
                    objData.CourseCount = 1;
                    courseWithCount.Add(objData);

                    foreach (clCourseCountOD item in courseWithCount)
                    {
                        DCourseNo = item.CourseNo;
                        
                        if (Convert.ToInt32(sCourseNo) == DCourseNo)
                        {
                            DCourseCount++;
                        }
                    }
                }

                //int Count = dsStdAppData.Tables[0].AsEnumerable().Where(x => x["CourseNo"].ToString() == sCourseNo.ToString() && x["TIME_SLOT_NO"] == sTimeSlot && x["TIME_SLOT_NO"] == ddlEvent.SelectedValue).ToList().Count;
                
                Count = Count + DCourseCount;
                if (Count < ODLimit)
                {
                    DataRow drForInsert = dtForInsert.NewRow();
                    drForInsert["CourseNo"] = sCourseNo;
                    drForInsert["TimeSlotNo"] = sTimeSlot;
                    dtForInsert.Rows.Add(drForInsert);
                    CourseInsertCount++;
                }
                else
                {
                    IsODLimitReached = true;
                    DataRow[] drData = dsStdAppData.Tables[0].Select("Convert(CourseNo, 'System.Int32') = " + sCourseNo);
                    string coursename = drData[0]["course_name"].ToString();
                    sbODLimitReachedCourses.Append(coursename + ",");
                }
            }
        }

        if (IsValidForApply && CourseInsertCount > 0)
        {
            result = objODTrackerController.Insert_Student_OD_Tracker_Events(objODTracker, dtForInsert);
        }
        else if (!IsValidForApply)
        {
            objCommon.DisplayMessage(updStudent, "Start dateshould not be less than " + MinimumDate.ToString("dd-MMM-yyyy"), this.Page);
            return;
        }
        else if (CourseInsertCount < 1)
        {
            if (sbODLimitReachedCourses.Length > 0 && IsODLimitReached)
            {
                objCommon.DisplayMessage(updStudent, sbODLimitReachedCourses.ToString().TrimEnd(',') + " Course OD Limit Reached.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updStudent, "No record Inserted!", this.Page);
                return;
            }
        }
        if (result == 1)
        {
            objCommon.DisplayMessage(updStudent, "Record saved sucessfully.", this.Page);
            //if (sb.Length > 0)
            //{
            //    objCommon.DisplayMessage(updStudent, "following " + sb.ToString().TrimEnd(',') + " course not registered ", this.Page);
            //}
            //else
            //{
            //    objCommon.DisplayMessage(updStudent, "Record saved sucessfully.", this.Page);
            //}
            ClearControl();
        }

        GetStudentODEventData();
    }

    private void BindTimeSlot()
    {
        DataSet dsData = new DataSet();
        string EventDate = txtDate.Text.Split('-')[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(txtDate.Text.Split('-')[1])).ToString().Substring(0, 3) + "-" + txtDate.Text.Split('-')[2];

        dsData = objODTrackerController.GetTimeSlotDetailsFromTimetable(EventDate, Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(lblCurrentSem.Text));

        if (dsData.Tables.Count > 0)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                lstTiming.DataSource = dsData;
                lstTiming.DataValueField = "COURSENO_WITH_SLOTNO";
                lstTiming.DataTextField = "TimeSlot";

                lstTiming.DataBind();
            }
        }
    }

    private Boolean IsCheckMaxLimitReach(int CourseNo)
    {
        Boolean IsValid = false;
        DataTable dtcrs = (DataTable)ViewState["ODCourseCount"];

        if (dtcrs != null)
        {
            if (dtcrs.Rows.Count > 0)
            {
                int Count = dtcrs.AsEnumerable().Where(x => x["COURSENO"].ToString() == CourseNo.ToString() && x["IS_SPECIALEVENT"] != "1").ToList().Count;

                if (Count > 3)
                {
                    IsValid = true;
                }
            }
        }
        return IsValid;
    }

    private void ClearControl()
    {
        ddlEvent.SelectedIndex = 0;
        txtDate.Text = "";
        txtStdCommment.Text = "";
        ddlCategory.SelectedIndex = 0;
        lstTiming.ClearSelection();
    }

    private int CheckEventDateIsValidOrNot()
    {
        //User for which we want to check back days allowed.
        string sDays = objCommon.LookUp("ACD_OD_TRACKER_EVENT_CONFIG", "MINIMUM_DAYS_ALLOWED", "EVENT_FOR_FACULTY_STUDENT = 2");  //2 for student config details
        int Days = 0;
        if (!String.IsNullOrEmpty(sDays))
        {
            Days = Convert.ToInt32(sDays);
        }
        return Days;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        btnApplyOD.Enabled = true;
    }


}
public class clCourseCountOD
{
    public int CourseNo;
    public int CourseCount;
}