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

public partial class ODTracker_Placement_OD_Dashboard : System.Web.UI.Page
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

            GetAllPlacementEvents(1);
            GetPlacementStudentListForApproval(1);//for pending for approval
            GetDashboardDataForPlacementCoordinator(objODTracker);
        }
    }

    protected void btnSubmitPlacement_Click(object sender, EventArgs e)
    {
        try
        {
            objODTracker.PlacementName = txtPlacementName.Text;
            string[] sdates = txtStartDate.Text.Split('-');
            string[] edates = txtEndDate.Text.Split('-');
            objODTracker.Event_Start_Date = sdates[0] + "-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(sdates[1])).ToString().Substring(0, 3) + "-" + sdates[2];
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
            //end

            objODTracker.IsPublish = chkIsPublish.Checked;
            objODTracker.UANO = Convert.ToInt32(Session["userno"]);
            objODTracker.PlacementHeadNo = Convert.ToInt32(ddlPlacementHead.SelectedValue);
            objODTracker.EventComment = txtPlacementComment.Text;

            //int result = objODTrackerController.InsertODTrackerPlacementEvent(objODTracker);
            //if (result == 1)
            //{
            //    objCommon.DisplayMessage("Record added sucessfully.", this.Page);
            //    ClearControls();
            //    GetAllPlacementEvents();
            //}
            //else
            //{

            //}

            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                //Edit 
                objODTracker.Placement_SRNO = Convert.ToInt32(Session["Placement_SRNO"]);
                CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerPlacementEvent(objODTracker);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControls();
                    GetAllPlacementEvents(1);
                    objCommon.DisplayMessage("Record Updated sucessfully", this.Page);
                    Session["action"] = "add";
                }
            }
            else
            {
                //Add New
                CustomStatus cs = (CustomStatus)objODTrackerController.InsertODTrackerPlacementEvent(objODTracker);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage("Record Added sucessfully", this.Page);
                    ClearControls();
                    GetAllPlacementEvents(1);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage("Record Already Exist", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage( "Record Already Exist", this.Page);
                }
                //else if (cs.Equals(CustomStatus.TransactionFailed))                    
                //{
                //    objCommon.DisplayMessage(this.updSession, "Transaction Failed", this.Page);
                //}
            }

        }
        catch
        {
        }
    }

    private void ClearControls()
    {
        txtPlacementName.Text = "";
        chkIsPublish.Checked = false;
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        ddlPlacementHead.SelectedIndex = 0;
        txtPlacementComment.Text = "";
    }

    private void GetAllPlacementEvents(int ReqStatus)
    {
        lvPlacementEventOD.DataSource = null;
        lvPlacementEventOD.DataBind();
        MainView.ActiveViewIndex = 0;
        btnTabMyPlacementOD.CssClass = "nav-link active";
        ddlPlacementRequestStatus.SelectedValue = ReqStatus.ToString();
        int UserType = Convert.ToInt32(Session["usertype"]);
        ds = objODTrackerController.GetPlacementEventList(Convert.ToInt32(ddlPlacementRequestStatus.SelectedValue), UserType);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlacementEventOD.DataSource = ds;
                lvPlacementEventOD.DataBind();
            }
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlPlacementHead, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE = 16", "");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnMyPlacementDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ODTracker objODTracker = new ODTracker();
            ImageButton btnMyPlacementDelete = sender as ImageButton;
            objODTracker.Placement_SRNO = int.Parse(btnMyPlacementDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objODTrackerController.DeletePlacementEvent(objODTracker);

            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Entry Deleted Successfully !", this.Page);
                this.GetAllPlacementEvents(1);
                this.ClearControls();
            }
            else
            {
                objCommon.DisplayMessage( "some problem occured while deleting record!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GetPlacementEventsByID(int Placement_SRNO)
    {
        objODTracker.Placement_SRNO = Placement_SRNO;
        int UserType = Convert.ToInt32(Session["usertype"]);
        ds = objODTrackerController.GetPlacementEventByID(objODTracker);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPlacementName.Text = ds.Tables[0].Rows[0]["PLACEMENT_NAME"].ToString();
                txtStartDate.Text = ds.Tables[0].Rows[0]["START_DATE"].ToString();
                txtEndDate.Text = ds.Tables[0].Rows[0]["END_DATE"].ToString();
                ddlPlacementHead.SelectedValue = ds.Tables[0].Rows[0]["PLACEMENT_HEAD"].ToString();
                txtPlacementComment.Text = ds.Tables[0].Rows[0]["COMMENT"].ToString();
                chkIsPublish.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_PUBLISH"].ToString());
            }
        }
    }

    protected void btnMyPlacementEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnMyPlacementEdit = sender as ImageButton;
            int Placement_SRNO = int.Parse(btnMyPlacementEdit.CommandArgument);
            Session["Placement_SRNO"] = int.Parse(btnMyPlacementEdit.CommandArgument);
            ViewState["edit"] = "edit";
            Session["action"] = "edit";
            this.GetPlacementEventsByID(Placement_SRNO);
            //txtSLongName.Focus();
            //ddlCollege.Enabled = false;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#myModal_Create').modal('show')", true);
            btnSubmitPlacement.Text = "Save";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void GetPlacementStudentListForApproval(int RequestStatus)
    {
        lstStudentPlacement.DataSource = null;
        lstStudentPlacement.DataBind();

        objODTracker.UANO = Convert.ToInt32(Session["userno"]);
        int UserType = Convert.ToInt32(Session["usertype"]);
        objODTracker.RequestStatus = RequestStatus;
        ddlStudentPlacementOD.SelectedValue = RequestStatus.ToString();
        ds = objODTrackerController.GetPlacementCoordinatorStudentListForApproval(objODTracker);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstStudentPlacement.DataSource = ds;
                lstStudentPlacement.DataBind();
            }
        }
    }
    
    protected void btnTabStudentPlacementODApproval_Click(object sender, EventArgs e)
    {
        btnTabMyPlacementOD.CssClass = "nav-link";
        btnTabStudentPlacementODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        ddlPlacementRequestStatus.SelectedValue = "1";
        GetPlacementStudentListForApproval(1);//request status 1 for pending request for approval
    }
    
    protected void btnTabMyPlacementOD_Click(object sender, EventArgs e)
    {
        btnTabMyPlacementOD.CssClass = "nav-link active";
        btnTabStudentPlacementODApproval.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
        GetAllPlacementEvents(1);
    }
    
    protected void btnApproveStudentPlacementEvent_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        //int userType = Convert.ToInt32(Session["usertype"]);
        int RequestStatus = 2; //for Approve Status.
        int SelectCount = 0;
        int processCount = 0;
        int result = 0;

        //objODTracker.Coordinator_Id = lbl

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_PLACEMENT_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lstStudentPlacement.Items)
        {
            Label lblStudPlacementNo = (Label)item.FindControl("lblStudPlacementNo");
            
            CheckBox chkStudPlacementSelect = (CheckBox)item.FindControl("chkStudPlacementSelect");
            //objODTracker.coordi = Convert.ToInt32(lblStudPlacementNo.Text);
            
            if (chkStudPlacementSelect.Checked)
            {
                SelectCount++;
                DataRow dr = dtCourse.NewRow();
                dr["STUD_PLACEMENT_NO"] = lblStudPlacementNo.Text;
                dtCourse.Rows.Add(dr);
            }
        }

        objODTracker.Approved_By = Convert.ToInt32(Session["userno"]);

        if (SelectCount > 0)
        {
            result = objODTrackerController.UpdateStudentPlacementEventByPlacementCoordinator(objODTracker, OrgId, RequestStatus, dtCourse);
            if (result == 2)
            {
                processCount++;
            }
        }

        if (processCount > 0)
        {
            objCommon.DisplayMessage("Records Approved Sucessfully.", this.Page);
            GetPlacementStudentListForApproval(1);
        }
        btnTabStudentPlacementODApproval_Click(null, null);
    }
    
    protected void btnRejectStudentPlacementEvent_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        //int userType = Convert.ToInt32(Session["usertype"]);
        int RequestStatus = 3; //for Rejected Status.
        int SelectCount = 0;
        int processCount = 0;
        int result = 0;

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_PLACEMENT_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lstStudentPlacement.Items)
        {
            Label lblStudPlacementNo = (Label)item.FindControl("lblStudPlacementNo");

            CheckBox chkStudPlacementSelect = (CheckBox)item.FindControl("chkStudPlacementSelect");
            //objODTracker.coordi = Convert.ToInt32(lblStudPlacementNo.Text);

            if (chkStudPlacementSelect.Checked)
            {
                SelectCount++;
                DataRow dr = dtCourse.NewRow();
                dr["STUD_PLACEMENT_NO"] = lblStudPlacementNo.Text;
                dtCourse.Rows.Add(dr);
            }
        }

        objODTracker.Rejected_By = Convert.ToInt32(Session["userno"]);

        if (SelectCount > 0)
        {
            result = objODTrackerController.UpdateStudentPlacementEventByPlacementCoordinator(objODTracker, OrgId, RequestStatus, dtCourse);
            if (result == 2)
            {
                processCount++;
            }
        }

        if (processCount > 0)
        {
            objCommon.DisplayMessage("Records Rejected Sucessfully.", this.Page);
            GetPlacementStudentListForApproval(1);
        }
        btnTabStudentPlacementODApproval_Click(null, null);
    }
    
    protected void ddlPlacementRequestStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Convert.ToInt32(ddlPlacementRequestStatus.SelectedValue)>0)
        {
            GetAllPlacementEvents(Convert.ToInt32(ddlPlacementRequestStatus.SelectedValue));
        }
    }
    
    protected void ddlStudentPlacementOD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStudentPlacementOD.SelectedValue) > 0)
        {
            GetPlacementStudentListForApproval(Convert.ToInt32(ddlStudentPlacementOD.SelectedValue));
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnTabStudentPlacementODApproval.CssClass = "nav-link";
        lstStudentPlacement.DataSource = null;
        lstStudentPlacement.DataBind();
        lvPlacementEventOD.DataSource = null;
        lvPlacementEventOD.DataBind();
        GetAllPlacementEvents(1);
        Session["action"] = "add";
        ClearControls();
        btnSubmitPlacement.Text = "Create";
    }

    private void GetDashboardDataForPlacementCoordinator(ODTracker objODTracker)
    {
        ds = objODTrackerController.GetDashboardDataForPlacementCoordinator(objODTracker);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblStudPlaceReq.Text = ds.Tables[0].Rows[0]["STUDENT_OD_COUNT"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblMyPlacementPending.Text = ds.Tables[1].Rows[0]["PLACEMENT_EVENT_COUNT"].ToString();
            }                          
        }
    }
}