using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using IITMS.UAIMS;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using System.Data;
using System.Globalization;

public partial class ODTracker_Faculty_Dashboard_OD : System.Web.UI.Page
{
    Common objCommon = new Common();
    ODTracker objODTracker = new ODTracker();
    ODTrackerController objODTrackerController = new ODTrackerController();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropDownList();
            GetAllEventsForFaculty();
            ddlMyEventStatus.SelectedValue = "1";
            ddlMyEventStatus_SelectedIndexChanged(null, null);
            BindFacultyDashboard();
        }
        else
        {
            //ddlMyEventStatus_SelectedIndexChanged(null, null);
             //btnTabMyEvents_Click(null,null);
             
            //else if (MainView.ActiveViewIndex == 1)
            //{
            //    btnTabODApproval_Click(null, null);
            //}
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCategory, "ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME", "EVENT_ID > 0 AND IS_ACTIVE = 1", "");
            //objCommon.FillDropDownList(ddlSubCategory, "ACD_OD_TRACKER_SUB_EVENT_MASTER", "SUB_EVENT_ID", "SUB_EVENTNAME", "SUB_EVENT_ID > 0 AND IS_ACTIVE = 1", "");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    private void GetAllEventsForFaculty()
    {
        //lvMyEvent = null;
        lvMyEvent.DataSource = null;
        lvMyEvent.DataBind();
        MainView.ActiveViewIndex = 0;
        btnTabMyEvents.CssClass = "nav-link active";
        //ddlMyEventStatus.SelectedValue="4";
        int UserType = Convert.ToInt32(Session["usertype"]);
        ds = objODTrackerController.GetFacultyEventList(Convert.ToInt32(ddlMyEventStatus.SelectedValue), UserType);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMyEvent.DataSource = ds;
                lvMyEvent.DataBind();
            }             
        }
    }

    protected void ddlODStatus_SelectedIndexChanged(object sender, EventArgs e)  
    {
        int ReqStatus = Convert.ToInt32(ddlODStatus.SelectedValue);
        ds = objODTrackerController.GetStudentEventODListByStatus(ReqStatus);
        //ds = objODTrackerController.GetMyEventList(org_id, ReqStatus,userType);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {                                  
                lstODEvent.DataSource = ds;
                lstODEvent.DataBind();
            }
            else
            {
                lstODEvent.DataSource = null;
                lstODEvent.DataBind();
            }
        }
        else
        {
            lstODEvent.DataSource = null;
            lstODEvent.DataBind();
        }
        //btnODApproval_Click(null, null);
    }

    protected void ddlMyEventStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnCreateEvent.CssClass = "nav-link active";
        //Tab2.CssClass = "nav-lin";
        MainView.ActiveViewIndex = 0;
        //lvMyEvent = null;
        lvMyEvent.DataSource = null;
        lvMyEvent.DataBind();

        MainView.ActiveViewIndex = 0;
        int UserType = Convert.ToInt32(Session["usertype"]);
        ds = objODTrackerController.GetFacultyEventList(Convert.ToInt32(ddlMyEventStatus.SelectedValue), UserType);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvMyEvent.DataSource = ds;
                lvMyEvent.DataBind();
            }

        }

         
        //int ReqStatus = Convert.ToInt32(ddlMyEventStatus.SelectedValue);
        //int org_id = Convert.ToInt32(Session["OrgId"]);
        //int userType = 3;
        //ds = objODTrackerController.GetMyEventList(org_id, ReqStatus, userType);
        //if (ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvMyEvent.DataSource = ds;
        //        lvMyEvent.DataBind();
        //    }
        //}
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        int RequestStatus = 2; //for Approve Status.
        int SelectCount = 0;
        int processCount = 0;
        int result = 0;

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_OD_COURSE_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lstODEvent.Items)
        {
            Label lblStudOdCourseNo = (Label)item.FindControl("lblStudOdCourseNo");
            Label lblStudOdNo = (Label)item.FindControl("lblStudOdNo");
            Label lblCourseNO = (Label)item.FindControl("lblCourseNO");

            CheckBox chkODEvent = (CheckBox)item.FindControl("chkODEvent");
            objODTracker.Stud_OD_NO = Convert.ToInt32(lblStudOdNo.Text);
            objODTracker.Coordinator_Id = 0;
            if (chkODEvent.Checked)
            {
                SelectCount++;
                DataRow dr = dtCourse.NewRow();                 
                dr["STUD_OD_COURSE_NO"] = lblStudOdCourseNo.Text;                
                dtCourse.Rows.Add(dr);
            }
        }

        objODTracker.Approved_By = Convert.ToInt32(Session["userno"]);

        if (SelectCount > 0)
        {
            result = objODTrackerController.Update_Student_OD_Tracker_Events_Status(objODTracker, OrgId, userType, RequestStatus, dtCourse);
            if (result == 2)
            {
                processCount++;
            }
        }

        if (processCount > 0)
        {
            objCommon.DisplayMessage("Records approved.", this.Page);
            GetAllEventsForFaculty();
        }
        btnTabODApproval_Click(null, null);
    }
     

    protected void btnReject_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        int RequestStatus = 3; //for Reject Status.
        int SelectCount = 0;
         
        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_OD_COURSE_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lstODEvent.Items)
        {
            Label lblStudOdCourseNo = (Label)item.FindControl("lblStudOdCourseNo");
            Label lblStudOdNo = (Label)item.FindControl("lblStudOdNo");
            Label lblCourseNO = (Label)item.FindControl("lblCourseNO");

            CheckBox chkODEvent = (CheckBox)item.FindControl("chkODEvent");
            objODTracker.Stud_OD_NO = Convert.ToInt32(lblStudOdNo.Text);
            objODTracker.Coordinator_Id = 0;
            if (chkODEvent.Checked)
            {
                SelectCount++;
                DataRow dr = dtCourse.NewRow();
                dr["STUD_OD_COURSE_NO"] = lblStudOdCourseNo.Text;
                dtCourse.Rows.Add(dr);
            }
        }
        objODTracker.Rejected_By = Convert.ToInt32(Session["userno"]);
        int result = 0;
        
        
        if (SelectCount < 1)
        {
            objCommon.DisplayMessage("Please select atleast one record", this.Page);
            return;
        }
        if (SelectCount > 0)
        {
            result = objODTrackerController.Update_Student_OD_Tracker_Events_Status(objODTracker, OrgId, userType, RequestStatus, dtCourse);

            if (result > 0)
            {
                objCommon.DisplayMessage("Records rejected successfully.", this.Page);
            }
        }

        //if (processCount > 0)
        //{
        //    string strMsg = "Records rejected.";
        //    if (processCount == 1)
        //    {
        //        strMsg = "Record rejected.";
        //    }
        //    objCommon.DisplayMessage(processCount + " " + strMsg, this.Page);
        //}
    }

    protected void btnMyEvent_Click(object sender, EventArgs e)
    {
    }

    protected void btnODApproval_Click(object sender, EventArgs e) { }
    //{
    //    //btnODApproval.CssClass = "nav-link active";
    //    //tab_2.Attributes.Add("class", "tab-pane fade active show");

    //    int ReqStatus = Convert.ToInt32(ViewState["odddlStatus"]);

    //    int org_id = Convert.ToInt32(Session["OrgId"]);
    //    int userType = 2;
    //    ds = objODTrackerController.GetMyEventList(org_id, ReqStatus, userType);
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            //tab_1.Attributes.Add("class", "active");
    //            lstODEvent.DataSource = ds;
    //            lstODEvent.DataBind();
    //        }
    //        else
    //        {
    //            lvMyEvent.DataSource = null;
    //            lvMyEvent.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        lvMyEvent.DataSource = null;
    //        lvMyEvent.DataBind();
    //    }
    //}

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstODEvent.DataSource = null;
        lstODEvent.DataBind();
        lvMyEvent.DataSource = null;
        lvMyEvent.DataBind();

        if (ddlCategory.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSubCategory, "ACD_OD_TRACKER_SUB_EVENT_MASTER", "SUB_EVENT_ID", "SUB_EVENTNAME", "EVENT_ID = " + ddlCategory.SelectedValue + " AND SUB_EVENT_ID > 0 AND IS_ACTIVE = 1", "");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
        }
    }
    
    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        objODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        objODTracker.EventID = Convert.ToInt32(ddlCategory.SelectedValue);
        objODTracker.Sub_EventID = Convert.ToInt32(ddlSubCategory.SelectedValue);
        objODTracker.EventName = txtEventName.Text;
        string[] sdates = txtStartDate.Text.Split('-');
        string[] edates = txtEndDate.Text.Split('-');
        objODTracker.Event_Start_Date = sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0,3) + "-" + sdates[2];
        objODTracker.Event_End_Date = edates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(edates[1])).ToString().Substring(0, 3) + "-" + edates[2];
        
        //checking date valid or not
        DateTime EventStartDate = new DateTime();
        DateTime EventEndDate = new DateTime();
        try
        {
            EventStartDate = Convert.ToDateTime(sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0, 3) + "-" + sdates[2]);            
        }
        catch
        {
            objCommon.DisplayMessage("Please Enter Valid Start Date.", this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
            return;
        }
        try
        {
            EventEndDate = Convert.ToDateTime(edates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(edates[1])).ToString().Substring(0, 3) + "-" + edates[2]);
        }
        catch
        {
            objCommon.DisplayMessage("Please Enter Valid End Date.", this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
            return;
        }


        if (EventStartDate > EventEndDate)
        {
            objCommon.DisplayMessage("Start Date should not greater than End Date.", this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
            return;
        }
        //checking date valid or not end.

        objODTracker.IsPublish = chkIsPublish.Checked;
        objODTracker.IsSpecialEvent = chkSpecialEvent.Checked;         
        objODTracker.EventComment = txtComment.Text;
        objODTracker.UANO = Convert.ToInt32(Session["userno"]);
        objODTracker.SessionNo = Convert.ToInt32(Session["currentsession"]);
         
        int userType = Convert.ToInt32(Session["usertype"]);
                 
        int MinimumDay = CheckEventDateIsValidOrNot();
        
        DateTime MinimumDate = DateTime.Today.AddDays(-MinimumDay);
        DateTime UserFromDate = DateTime.Parse(objODTracker.Event_Start_Date);
        Boolean IsValidForApply = false;

        if (MinimumDate <= UserFromDate)
        {
            IsValidForApply = true;
        }
        else if (MinimumDate > UserFromDate)
        {
            IsValidForApply = false;
        }
        
        //if (MinimumDate > UserFromDate)
        if(!IsValidForApply)
        {
            objCommon.DisplayMessage("Start date should not be less than " + MinimumDate.ToString("dd-MMM-yyyy"), this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
        }

        if (IsValidForApply)
        {
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                //Edit 
                objODTracker.FacultyEvent_SRNO = Convert.ToInt32(Session["EVENT_SRNO"]);
                CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerEventsBYFaculty(objODTracker);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControls();
                    GetAllEventsForFaculty();
                    objCommon.DisplayMessage("Record Updated sucessfully", this.Page);
                    Session["action"] = "add";
                }
            }
            else
            {
                //Add New
                CustomStatus cs = (CustomStatus)objODTrackerController.InsertODTrackerEventsBYFaculty(objODTracker);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage("Record Added sucessfully", this.Page);
                    ClearControls();
                    GetAllEventsForFaculty();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage("Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage("Record Already Exist", this.Page);
                }                 
            }
        }
        btnTabMyEvents.CssClass = "nav-link active";
        btnTabODApproval.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
        GetAllEventsForFaculty();
    }

    protected void btnTabMyEvents_Click(object sender, EventArgs e)
    {
        btnTabMyEvents.CssClass = "nav-link active";
        btnTabODApproval.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
        ddlMyEventStatus_SelectedIndexChanged(null, null);
    }

    protected void btnTabODApproval_Click(object sender, EventArgs e)
    { 
        btnTabMyEvents.CssClass = "nav-link";
        btnTabODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        ddlODStatus.SelectedValue = "1";
        GetStudentEventODList(1);//request status 1 for pending request for approval
    }
    
    protected void btnMyEventDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ODTracker objODTracker = new ODTracker();            
            ImageButton btnMyEventDelete = sender as ImageButton;             
            objODTracker.FacultyEvent_SRNO = int.Parse(btnMyEventDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objODTrackerController.DeleteFacultyEvent(objODTracker);

            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Entry Deleted Successfully !", this.Page);
                this.GetAllEventsForFaculty();
                this.ClearControls();
            }
            else
            {
                objCommon.DisplayMessage("some problem occured while deleting record!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        txtEventName.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtComment.Text = "";
        chkIsPublish.Checked = false;
        chkSpecialEvent.Checked = false;
    }
    
    protected void lvMyEvent_DataBinding(object sender, EventArgs e)
    {
        //ListViewDataItem dataItem = (ListViewDataItem)e.Item;

        //if (e.Item.ItemType == ListViewItemType.DataItem)
        //{
        //    YourDataSource yourDataSource = (YourDataSource)dataItem.DataItem;

        //}
    }
    
    protected void btnMyEventEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnMyEventEdit = sender as ImageButton;
            int EVENT_SRNO = int.Parse(btnMyEventEdit.CommandArgument);
            Session["EVENT_SRNO"] = int.Parse(btnMyEventEdit.CommandArgument);
            ViewState["edit"] = "edit";
            Session["action"] = "edit";
            GetMyEventsByID(EVENT_SRNO);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
            btnCreateEvent.Text = "Save";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GetMyEventsByID(int EVENT_SRNO)
    {
        objODTracker.FacultyEvent_SRNO = EVENT_SRNO;
        int UserType = Convert.ToInt32(Session["usertype"]);

        ds = objODTrackerController.GetFacultyEventByID(objODTracker);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["EVENT_CATEGORY_NO"].ToString();
                if (ddlCategory.SelectedIndex > 0)
                {
                    objCommon.FillDropDownList(ddlSubCategory, "ACD_OD_TRACKER_SUB_EVENT_MASTER", "SUB_EVENT_ID", "SUB_EVENTNAME", "EVENT_ID = " + ddlCategory.SelectedValue + " AND SUB_EVENT_ID > 0 AND IS_ACTIVE = 1", "");
                }
                ddlSubCategory.SelectedValue = ds.Tables[0].Rows[0]["SUB_EVENT_CATEGORY_NO"].ToString();
                txtEventName.Text = ds.Tables[0].Rows[0]["EVENT_NAME"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["END_DATE"].ToString();
                chkSpecialEvent.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_SPECIALEVENT"].ToString());
                chkIsPublish.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_PUBLISH"].ToString());
                txtComment.Text = ds.Tables[0].Rows[0]["COMMENT"].ToString();
                //btnCreateCollegeEvent_(null, null);
            }
        }
    }

    private void GetStudentEventODList(int RequestStatus)
    {             
        ds = objODTrackerController.GetStudentEventODListByStatus(RequestStatus);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstODEvent.DataSource = ds;
                lstODEvent.DataBind();
            }
            else
            {
                lstODEvent.DataSource = null;
                lstODEvent.DataBind();
            }
        }
        else
        {
            lstODEvent.DataSource = null;
            lstODEvent.DataBind();
        }
    }

    private void BindFacultyDashboard()
    {
        ODTracker objODTracker = new ODTracker();
        objODTracker.UANO = Convert.ToInt32(Session["userno"]);
        DataSet dsdata = new DataSet();
        dsdata = objODTrackerController.GetDashBoardDataForFaculty(objODTracker);

        if (dsdata.Tables.Count > 0)
        {
            if (dsdata.Tables[0].Rows.Count > 0)
            {
                lblDashboardmyEventCount.Text = dsdata.Tables[0].Rows[0]["MY_EVENT"].ToString();                 
            }
            if (dsdata.Tables[1].Rows.Count > 0)
            {
                lblDashboardStudentODCount.Text = dsdata.Tables[1].Rows[0]["STUDENT_OD"].ToString();                 
            }            
        }
    }
    
    protected void btnCreateCollegeEvent_Click(object sender, EventArgs e)
    {
        //mp1.Show();
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["action"] = "add";
        ClearControls();
        btnCreateEvent.Text = "Create";
        btnTabODApproval.CssClass = "nav-link";
        GetAllEventsForFaculty();
        ddlMyEventStatus.SelectedValue = "1";
        ddlMyEventStatus_SelectedIndexChanged(null, null);
    }
    
    protected void lvMyEvent_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

    private int CheckEventDateIsValidOrNot()
    {
        //User for which we want to check back days allowed.
        string sDays = objCommon.LookUp("ACD_OD_TRACKER_EVENT_CONFIG", "MINIMUM_DAYS_ALLOWED", "EVENT_FOR_FACULTY_STUDENT = 3");
        int Days = 0;
        if (!String.IsNullOrEmpty(sDays))
        {
            Days = Convert.ToInt32(sDays);
        }
        return Days;
    }
}