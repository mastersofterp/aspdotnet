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

public partial class ODTracker_Principal_Dashboard_OD : System.Web.UI.Page
{
    Common objCommon = new Common();
    ODTracker objODTracker = new ODTracker();
    ODTrackerController objODTrackerController = new ODTrackerController();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllEventsListForPrincipal();
            objODTracker.UANO = Convert.ToInt32(Session["userno"]);
            GetDashboardDataForPrincipal(objODTracker);
            //GetAllFacultyEvents();
        }
    }

    private void GetAllEventsListForPrincipal()
    {
        MainView.ActiveViewIndex = 0;
        btnEventODApproval.CssClass = "nav-link active";
        int UserType = Convert.ToInt32(Session["usertype"]);
        ddlEventODStatus.SelectedValue = "1";

        ds = objODTrackerController.GetMyEventListForPrincipal(Convert.ToInt32(ddlEventODStatus.SelectedValue), UserType);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventOD.DataSource = ds;
                lvEventOD.DataBind();
            }
            else
            {
                lvEventOD.DataSource = null;
                lvEventOD.DataBind();
            }
        }
        else
        {
            lvEventOD.DataSource = null;
            lvEventOD.DataBind();
        }
    }

    protected void ddlEventODStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlMyEventStatus
        int ReqStatus = Convert.ToInt32(ddlEventODStatus.SelectedValue);
        int org_id = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);

        //ds = objODTrackerController.GetFacultyEventListForPrincipal(Convert.ToInt32(Session["OrgId"]), ReqStatus, userType);
        ds = objODTrackerController.GetMyEventListForPrincipal(ReqStatus, userType);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventOD.DataSource = ds;
                lvEventOD.DataBind();
            }
            else
            {
                lvEventOD.DataSource = null;
                lvEventOD.DataBind();
            }
        }
    }

    protected void btnEventODApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            int userType = Convert.ToInt32(Session["usertype"]);
            int RequestStatus = 2;  //Approved Status
            int SelectCount = 0;
            int processCount = 0;
            objODTracker.Final_request_approved_by = Convert.ToInt32(Session["userno"]);
            objODTracker.Final_request_rejected_by = Convert.ToInt32(Session["userno"]);

            DataTable dtData = new DataTable();
            DataColumn dc = new DataColumn("Event_SRNO");
            dtData.Columns.Add(dc);

            foreach (ListViewItem item in lvEventOD.Items)
            {
                Label lblEvent_SRNO = (Label)item.FindControl("lblEvent_SRNO");
                CheckBox chkEvents = (CheckBox)item.FindControl("chkEvents");

                if (chkEvents.Checked)
                {
                    DataRow dr = dtData.NewRow();
                    dr[0] = lblEvent_SRNO.Text;
                    dtData.Rows.Add(dr);
                    //processCount++;
                    SelectCount++;
                }
            }

            CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerEventsStatusByPrincipal(objODTracker, OrgId, userType, RequestStatus, dtData);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                processCount++;
            }

            if (SelectCount < 1)
            {
                objCommon.DisplayMessage(updPrincipal,"Please select atleast one record", this.Page);
            }

            if (processCount > 0)
            {
                string strMsg = "Records approved.";
                if (processCount == 1)
                {
                    strMsg = "Record approved.";
                }
                objCommon.DisplayMessage(updPrincipal,processCount + " " + strMsg, this.Page);
                GetAllEventsListForPrincipal();
            }
        }
        catch
        {
        }
    }

    protected void btnEventODReject_Click(object sender, EventArgs e)
    {
        try
        {
            int OrgId = Convert.ToInt32(Session["OrgId"]);
            int userType = Convert.ToInt32(Session["usertype"]);
            int RequestStatus = 3;  //Reject Status
            int SelectCount = 0;
            int processCount = 0;
            objODTracker.Final_request_approved_by = Convert.ToInt32(Session["userno"]);
            objODTracker.Final_request_rejected_by = Convert.ToInt32(Session["userno"]);

            DataTable dtData = new DataTable();
            DataColumn dc = new DataColumn("Event_SRNO");
            dtData.Columns.Add(dc);

            foreach (ListViewItem item in lvEventOD.Items)
            {
                Label lblEvent_SRNO = (Label)item.FindControl("lblEvent_SRNO");
                CheckBox chkEvents = (CheckBox)item.FindControl("chkEvents");

                if (chkEvents.Checked)
                {
                    DataRow dr = dtData.NewRow();
                    dr[0] = lblEvent_SRNO.Text;
                    dtData.Rows.Add(dr);
                    //processCount++;
                    SelectCount++;
                }
            }

            CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerEventsStatusByPrincipal(objODTracker, OrgId, userType, RequestStatus, dtData);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                processCount++;
            }

            if (SelectCount < 1)
            {
                objCommon.DisplayMessage(updPrincipal,"Please select atleast one record", this.Page);
            }

            if (processCount > 0)
            {
                string strMsg = "Records Rejected.";
                if (processCount == 1)
                {
                    strMsg = "Record Rejected.";
                }
                objCommon.DisplayMessage(updPrincipal, processCount + " " + strMsg, this.Page);
                GetAllEventsListForPrincipal();
            }
        }
        catch
        {

        }
    }

    private void GetDashboardDataForPrincipal(ODTracker objODTracker)
    {
        ds = objODTrackerController.GetDashBoardDataForPrincipal(objODTracker);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblDashboardClgEventCnt.Text = ds.Tables[0].Rows[0]["COLLEGE_EVENT_COUNT"].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblDashboardStudODCnt.Text = ds.Tables[1].Rows[0]["STUDENT_OD_COUNT"].ToString();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                lblDashboardPlacementEventCnt.Text = ds.Tables[2].Rows[0]["PLACEMENT_EVENT_COUNT"].ToString();
            }
        }
    }


    private void GetAllFacultyEvents()
    {
        int UserType = 1;
        ds = objODTrackerController.GetFacultyEventListForPrincipal(Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(ddlEventODStatus.SelectedValue), UserType);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEventOD.DataSource = ds;
                lvEventOD.DataBind();
            }
            else
            {
                lvEventOD.DataSource = null;
                lvEventOD.DataBind();
            }
        }
        else
        {
            lvEventOD.DataSource = null;
            lvEventOD.DataBind();
        }
    }

    protected void ddlStudentODStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ReqStatus = Convert.ToInt32(ddlStudentODStatus.SelectedValue);
        ViewState["odddlStatus"] = ReqStatus;

        int org_id = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        objODTracker.RequestStatus = ReqStatus;
        //ds = objODTrackerController.GetMyEventListForPrincipal(ReqStatus, userType);        
        ds = objODTrackerController.GetStudentEventListForPrincipalApproval(objODTracker);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //tab_1.Attributes.Add("class", "active");
                lvStudentOD.DataSource = ds;
                lvStudentOD.DataBind();
            }
            else
            {
                lvStudentOD.DataSource = null;
                lvStudentOD.DataBind();
            }
        }
        else
        {
            lvStudentOD.DataSource = null;
            lvStudentOD.DataBind();
        }
    }

    protected void btnStudentODApprove_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        objODTracker.RequestStatus = 2; //for Approve Status.
        
        int SelectCount = 0;
         
        objODTracker.Final_request_approved_by = Convert.ToInt32(Session["userno"]);
        objODTracker.Final_request_approved_by = Convert.ToInt32(Session["userno"]);

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_OD_COURSE_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lvStudentOD.Items)
        {
            Label lblEventId = (Label)item.FindControl("lblEventId");
            CheckBox chkODEvent = (CheckBox)item.FindControl("chkODEvent");
            Label lblCourseNo = (Label)item.FindControl("lblCourseNo");
            Label lblStudOdCourseNo = (Label)item.FindControl("lblStudOdCourseNo");
            Label lblIDNO = (Label)item.FindControl("lblIDNO");

            if (chkODEvent.Checked)
            {
                DataRow dr = dtCourse.NewRow();
                dr["STUD_OD_COURSE_NO"] = lblStudOdCourseNo.Text;
                dtCourse.Rows.Add(dr);
                SelectCount++;
            }
        }
        
        if (SelectCount < 1)
        {
            objCommon.DisplayMessage(updPrincipal, "Please select atleast one record.", this.Page);
            return;
        }

        if (SelectCount > 0)
        {
            int result = 0;
            result = objODTrackerController.UpdateODTrackerStudentEventsStatusByPrincipal(objODTracker,dtCourse);

            if (result > 0)
            {
                objCommon.DisplayMessage(updPrincipal, "Record Approved Successfully.", this.Page);
                btnTabStudentODApproval_Click(null, null);
            }
            else
            {
                objCommon.DisplayMessage(updPrincipal, "No Record Approved.", this.Page);
            }
        }
         
    }

    protected void btnStudentODReject_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        objODTracker.RequestStatus = 3; //for reject Status.
        int SelectCount = 0;
         
        objODTracker.Final_request_approved_by = Convert.ToInt32(Session["userno"]);
        objODTracker.Final_request_rejected_by = Convert.ToInt32(Session["userno"]);

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_OD_COURSE_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lvStudentOD.Items)
        {
            Label lblEventId = (Label)item.FindControl("lblEventId");
            CheckBox chkODEvent = (CheckBox)item.FindControl("chkODEvent");
            Label lblCourseNo = (Label)item.FindControl("lblCourseNo");
            Label lblIDNO = (Label)item.FindControl("lblIDNO");
            Label lblStudOdCourseNo = (Label)item.FindControl("lblStudOdCourseNo");
             

            if (chkODEvent.Checked)
            {
                DataRow dr = dtCourse.NewRow();
                dr["STUD_OD_COURSE_NO"] = lblStudOdCourseNo.Text;
                dtCourse.Rows.Add(dr);
                //processCount++;
                SelectCount++;
            }
        }

        if (SelectCount > 0)
        {
            int result = 0;
            result = objODTrackerController.UpdateODTrackerStudentEventsStatusByPrincipal(objODTracker, dtCourse);

            if (result > 0)
            {
                objCommon.DisplayMessage(updPrincipal, "Record Rejected Successfully.", this.Page);
                btnTabStudentODApproval_Click(null, null);
            }
            else
            {
                objCommon.DisplayMessage(updPrincipal, "No Record Rejected.", this.Page);
            }
        }

        //objODTrackerController.UpdateODTrackerEventsStatusByPrincipal(objODTracker, OrgId, userType, RequestStatus, dtCourse);

        //if (SelectCount < 1)
        //{
        //    objCommon.DisplayMessage("Please select atleast one record", this.Page);
        //}

        //if (processCount > 0)
        //{
        //    string strMsg = "Records Rejected.";
        //    if (processCount == 1)
        //    {
        //        strMsg = "Record Rejected.";
        //    }
        //    objCommon.DisplayMessage(processCount + " " + strMsg, this.Page);
        //}
    }

    protected void btnEventODApproval_Click(object sender, EventArgs e)
    {
        btnEventODApproval.CssClass = "nav-link active";
        btnTabStudentODApproval.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
        //GetStudentEventODList(1);
        ddlEventODStatus_SelectedIndexChanged(null, null);
    }

    protected void btnTabStudentODApproval_Click(object sender, EventArgs e)
    {
        btnEventODApproval.CssClass = "nav-link";
        btnTabStudentODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        ODTracker objODTracker = new ODTracker();
        objODTracker.RequestStatus = 1;
        GetStudentEventODListForPrincipalApproval(objODTracker);
    }

    private void GetStudentEventODListForPrincipalApproval(ODTracker objODTracker)
    {
        ViewState["odddlStatus"] = objODTracker.RequestStatus;

        int org_id = Convert.ToInt32(Session["OrgId"]);
        int userType = Convert.ToInt32(Session["usertype"]);
        ddlStudentODStatus.SelectedValue = objODTracker.RequestStatus.ToString();
        ds = objODTrackerController.GetStudentEventListForPrincipalApproval(objODTracker);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //tab_1.Attributes.Add("class", "active");
                lvStudentOD.DataSource = ds;
                lvStudentOD.DataBind();
            }
            else
            {
                lvStudentOD.DataSource = null;
                lvStudentOD.DataBind();
            }
        }
        else
        {
            lvStudentOD.DataSource = null;
            lvStudentOD.DataBind();
        }
    }
}