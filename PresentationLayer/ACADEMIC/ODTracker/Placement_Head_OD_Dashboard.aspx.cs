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

public partial class ACADEMIC_ODTracker_Placement_Head_OD_Dashboard : System.Web.UI.Page
{

    Common objCommon = new Common();
    ODTracker objODTracker = new ODTracker();
    ODTrackerController objODTrackerController = new ODTrackerController();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllPlacementListForHead(1);
            objODTracker.UANO = Convert.ToInt32(Session["userno"]);
            objODTracker.RequestStatus = 1;
            GetDashboardDataForCoordinatorHead(objODTracker);
        }
    }

    private void GetAllPlacementListForHead(int ReqStatus)
    {
        lvPlacementOD.DataSource = null;
        lvPlacementOD.DataBind();
        MainView.ActiveViewIndex = 0;
        btnTabPlacementODApproval.CssClass = "nav-link active";
        int UserType = Convert.ToInt32(Session["usertype"]);
        ddlPlacementODStatus.SelectedValue = ReqStatus.ToString();

        ds = objODTrackerController.GetPlacementListForCoordinatorHead(Convert.ToInt32(ddlPlacementODStatus.SelectedValue), UserType,Convert.ToInt32(Session["userno"]));
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlacementOD.DataSource = ds;
                lvPlacementOD.DataBind();
            }             
        }
    }
    
    protected void btnPlacementODApproval_Click(object sender, EventArgs e)
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
            DataColumn dc = new DataColumn("PLACEMENT_SRNO");
            dtData.Columns.Add(dc);

            foreach (ListViewItem item in lvPlacementOD.Items)
            {
                Label lblPlacement_SRNO = (Label)item.FindControl("lblPlacement_SRNO");
                CheckBox chkEvents = (CheckBox)item.FindControl("chkPlacement");

                if (chkEvents.Checked)
                {
                    DataRow dr = dtData.NewRow();
                    dr[0] = lblPlacement_SRNO.Text;
                    dtData.Rows.Add(dr);                     
                    SelectCount++;
                }
            }

            CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerPlacementStatusByCoordinatorHead(objODTracker, OrgId, userType, RequestStatus, dtData);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                processCount++;
            }

            if (SelectCount < 1)
            {
                objCommon.DisplayMessage(updPlacementHead, "Please select atleast one record.", this.Page);
            }

            if (processCount > 0)
            {
                //string strMsg = "Records approved.";
                //if (processCount == 1)
                //{
                //    strMsg = "Record approved.";
                //}
                objCommon.DisplayMessage(updPlacementHead, "Record approved sucessfully.", this.Page);
                GetAllPlacementListForCoordinatorHead();
            }
            btnTabStudentODApproval.CssClass = "nav-link";
            btnTabPlacementODApproval.CssClass = "nav-link active";            
            MainView.ActiveViewIndex = 0;
            //ddlStudentODStatus.SelectedValue = "1";
            //GetPlacementStudentListForApproval(1);
        }
        catch
        {
        }
    }

    protected void btnPlacementODReject_Click(object sender, EventArgs e)
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
            DataColumn dc = new DataColumn("PLACEMENT_SRNO");
            dtData.Columns.Add(dc);

            foreach (ListViewItem item in lvPlacementOD.Items)
            {
                Label lblPlacement_SRNO = (Label)item.FindControl("lblPlacement_SRNO");
                CheckBox chkEvents = (CheckBox)item.FindControl("chkPlacement");

                if (chkEvents.Checked)
                {
                    DataRow dr = dtData.NewRow();
                    dr[0] = lblPlacement_SRNO.Text;
                    dtData.Rows.Add(dr);
                    //processCount++;
                    SelectCount++;
                }
            }

            CustomStatus cs = (CustomStatus)objODTrackerController.UpdateODTrackerPlacementStatusByCoordinatorHead(objODTracker, OrgId, userType, RequestStatus, dtData);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                processCount++;
            }

            if (SelectCount < 1)
            {
                objCommon.DisplayMessage(updPlacementHead,"Please select atleast one record.", this.Page);
            }

            if (processCount > 0)
            {
                //string strMsg = "Records Rejected.";
                //if (processCount > 0)
                //{
                //    strMsg = "Record Rejected successfully.";
                //}
                objCommon.DisplayMessage(updPlacementHead, "Record rejected sucessfully.", this.Page);
                GetAllPlacementListForCoordinatorHead();
            }
        }
        catch
        {

        }
    }

    private void GetAllPlacementListForCoordinatorHead()
    {
        lvPlacementOD.DataSource = null;
        lvPlacementOD.DataBind();
        MainView.ActiveViewIndex = 0;
        btnTabPlacementODApproval.CssClass = "nav-link active";
        int UserType = Convert.ToInt32(Session["usertype"]);
        ddlPlacementODStatus.SelectedValue = "1";

        ds = objODTrackerController.GetPlacementListForCoordinatorHead(Convert.ToInt32(ddlPlacementODStatus.SelectedValue), UserType, Convert.ToInt32(Session["userno"]));
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlacementOD.DataSource = ds;
                lvPlacementOD.DataBind();
            }             
        }         
    }

    private void GetDashboardDataForCoordinatorHead(ODTracker objODTracker)
    {
        ds = objODTrackerController.GetDashboardDataForCoordinatorHead(objODTracker);
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
    
    protected void btnTabStudentODApproval_Click(object sender, EventArgs e)
    {
        btnTabPlacementODApproval.CssClass = "nav-link";
        btnTabStudentODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        ddlStudentODStatus.SelectedValue = "1";
        GetPlacementStudentListForApproval(1);
         
    }
    
    protected void ddlPlacementODStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvPlacementOD.DataSource = null;
        lvPlacementOD.DataBind();
        if(ddlPlacementODStatus.SelectedIndex>0)
        {
            GetAllPlacementListForHead(Convert.ToInt32(ddlPlacementODStatus.SelectedValue));
        }
    }

    private void GetPlacementStudentListForApproval(int RequestStatus)
    {
        lvStudentOD.DataSource = null;
        lvStudentOD.DataBind();

        objODTracker.UANO = Convert.ToInt32(Session["userno"]);
        int UserType = Convert.ToInt32(Session["usertype"]);
        objODTracker.RequestStatus = RequestStatus;
        ddlStudentODStatus.SelectedValue = RequestStatus.ToString();
        ds = objODTrackerController.GetPlacementCoordinatorHeadStudentListForApproval(objODTracker);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudentOD.DataSource = ds;
                lvStudentOD.DataBind();
            }
        }
    }
    
    protected void btnStudentODApprove_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);         
        int RequestStatus = 2; //for Approve Status.
        int SelectCount = 0;
        int processCount = 0;
        int result = 0;

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_PLACEMENT_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lvStudentOD.Items)
        {
            Label lblStudPlacementNo = (Label)item.FindControl("lblStudPlacementNo");
            CheckBox chkStudPlacementSelect = (CheckBox)item.FindControl("chkStudPlacementSelect");

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
            result = objODTrackerController.UpdateStudentPlacementEventByPlacementCoordinatorHead(objODTracker, OrgId, RequestStatus, dtCourse);
            if (result == 2)
            {
                processCount++;
            }
        }
        
        if (SelectCount < 1)
        {
            objCommon.DisplayMessage(updPlacementHead, "Please select atleast single record.", this.Page);
             
        }
        

        if (processCount > 0)
        {
            objCommon.DisplayMessage(updPlacementHead, "Record approved sucessfully.", this.Page);
            GetPlacementStudentListForApproval(1);
        }
        //btnTabStudentPlacementODApproval_Click(null, null);
        btnTabPlacementODApproval.CssClass = "nav-link";
        btnTabStudentODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        ddlStudentODStatus.SelectedValue = "1";
        btnTabStudentODApproval_Click(null, null);
    }

    protected void btnStudentODReject_Click(object sender, EventArgs e)
    {
        int OrgId = Convert.ToInt32(Session["OrgId"]);
        int RequestStatus = 3; //for Reject Status.
        int SelectCount = 0;
        int processCount = 0;
        int result = 0;

        DataTable dtCourse = new DataTable();
        DataColumn dc = new DataColumn("STUD_PLACEMENT_NO");
        dtCourse.Columns.Add(dc);

        foreach (ListViewItem item in lvStudentOD.Items)
        {
            Label lblStudPlacementNo = (Label)item.FindControl("lblStudPlacementNo");
            CheckBox chkStudPlacementSelect = (CheckBox)item.FindControl("chkStudPlacementSelect");

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
            result = objODTrackerController.UpdateStudentPlacementEventByPlacementCoordinatorHead(objODTracker, OrgId, RequestStatus, dtCourse);
            if (result == 2)
            {
                processCount++;
            }
        }

        if (processCount > 0)
        {
            objCommon.DisplayMessage(updPlacementHead, "Records rejected sucessfully.", this.Page);
            GetPlacementStudentListForApproval(1);
        }
        //btnTabStudentPlacementODApproval_Click(null, null);
        btnTabStudentODApproval_Click(null, null);
    }
    
    protected void btnTabPlacementODApproval_Click(object sender, EventArgs e)
    {
        btnTabStudentODApproval.CssClass = "nav-link";
        btnTabPlacementODApproval.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 0;
    }
    
    protected void ddlStudentODStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetPlacementStudentListForApproval(Convert.ToInt32(ddlStudentODStatus.SelectedValue));
    }
}