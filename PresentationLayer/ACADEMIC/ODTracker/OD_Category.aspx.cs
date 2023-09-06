using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS;
using System.Data;
using System.Data.SqlClient;

public partial class ACADEMIC_ODTracker_OD_Category : System.Web.UI.Page
{
    ODTrackerController ObjTrackerCon = new ODTrackerController();
    ODTracker ObjODTracker = new ODTracker();
    Common objCommon = new Common();
    int ActiveStatus = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropDownList();
            BindEventData();
            BindSubEventData();
        }
    }
    
    protected void btnSubmitEvent_Click(object sender, EventArgs e)
    {
        ObjODTracker.EventName = txtEventCategory.Text;
        int Organization_ID = Convert.ToInt32(Session["OrgId"]);

        if (hfdStatEvent.Value == "true")
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        

        if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
        {
            ObjODTracker.EventID = Convert.ToInt32(Session["OD_EventID"]);
            //duplicate Event check
            string MatchText = objCommon.LookUp("ACD_OD_TRACKER_EVENT_MASTER", "IS_ACTIVE = 1", "EVENT_ID != " + ObjODTracker.EventID + " AND  EVENTNAME = '" + txtEventCategory.Text + "'");
            if (!String.IsNullOrEmpty(MatchText))
            {
                objCommon.DisplayMessage(updEventCategory, "duplicate record not allowed.", this.Page);
                return;
            }
            //
            
            CustomStatus cs = (CustomStatus)ObjTrackerCon.UpdateODEvent(ObjODTracker, ActiveStatus, Organization_ID);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearSubEventControls();
                BindEventData();
                objCommon.DisplayMessage(updEventCategory,"Record Updated sucessfully", this.Page);
                Session["action"] = null;
            }
        }
        else
        {
            //duplicate Event check
            string MatchText = objCommon.LookUp("ACD_OD_TRACKER_EVENT_MASTER", "IS_ACTIVE = 1", "EVENTNAME = '" + txtEventCategory.Text + "'");
            if (!String.IsNullOrEmpty(MatchText))
            {
                objCommon.DisplayMessage(updEventCategory, "duplicate record not allowed.", this.Page);
                return;
            }
            //

            CustomStatus cs = (CustomStatus)ObjTrackerCon.InsertODEvent(ObjODTracker, ActiveStatus, Organization_ID);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updEventCategory,"Record Added sucessfully", this.Page);
                ClearEventControls();
                BindEventData();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updEventCategory,"Record Already Exist", this.Page);
            }
            else
            {
                //msgLbl.Text = "Record already exist";
                objCommon.DisplayMessage(updEventCategory,"Record Already Exist", this.Page);
            }
        }
        btnTabEventsCategory.CssClass = "nav-link active";
        btnTabSubEventCategory.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
    }

    private void PopulateDropDownList()
    {
        try
        {
            //objCommon.FillDropDownList(ddlEventCategory, "ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME", "EVENT_ID>0", "EVENT_ID DESC");
            objCommon.FillDropDownList(ddlEventCategory, "ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME", "EVENT_ID > 0 AND IS_ACTIVE = 1", "EVENT_ID DESC");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    protected void btnSubEventSubmit_Click(object sender, EventArgs e)
    {
        

        ObjODTracker.SubEventName = txtSubEventCategory.Text;
        ObjODTracker.EventID = Convert.ToInt32(ddlEventCategory.SelectedValue);

        int Organization_ID = Convert.ToInt32(Session["OrgId"]);

        if (hfdStatSubEvent.Value == "true")
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
        {
            ObjODTracker.Sub_EventID = Convert.ToInt32(Session["OD_SubEventID"]);
            //duplicate Sub Event check
            string MatchText = objCommon.LookUp("ACD_OD_TRACKER_SUB_EVENT_MASTER", "SUB_EVENT_ID", "SUB_EVENT_ID != " + ObjODTracker.Sub_EventID + " AND SUB_EVENTNAME = '" + txtSubEventCategory.Text + "'");
            if (!String.IsNullOrEmpty(MatchText))
            {
                objCommon.DisplayMessage(updEventCategory, "duplicate record not allowed.", this.Page);
                return;
            }
            //
            //ObjTrackerCon.UpdateODSubEvent(ObjODTracker, ActiveStatus, Organization_ID);

            CustomStatus cs = (CustomStatus)ObjTrackerCon.UpdateODSubEvent(ObjODTracker, ActiveStatus, Organization_ID);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearSubEventControls();
                BindEventData();
                BindSubEventData();
                objCommon.DisplayMessage(updEventCategory,"Record Updated sucessfully", this.Page);
                Session["action"] = null;
            }
        }
        else
        {
            //duplicate Sub Event check
            string MatchText = objCommon.LookUp("ACD_OD_TRACKER_SUB_EVENT_MASTER", "SUB_EVENT_ID", "SUB_EVENTNAME = '" + txtSubEventCategory.Text + "'");
            if (!String.IsNullOrEmpty(MatchText))
            {
                objCommon.DisplayMessage(updEventCategory, "duplicate record not allowed.", this.Page);
                return;
            }
            //
            CustomStatus cs = (CustomStatus)ObjTrackerCon.InsertODSubEvent(ObjODTracker, ActiveStatus, Organization_ID);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updEventCategory,"Record Added sucessfully", this.Page);
                ClearSubEventControls();
                BindSubEventData();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updEventCategory,"Record Already Exist", this.Page);
            }
            else
            {
                //msgLbl.Text = "Record already exist";
                objCommon.DisplayMessage(updEventCategory,"Record Already Exist", this.Page);
            }
        }
        btnTabEventsCategory.CssClass = "nav-link";
        btnTabSubEventCategory.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;        
    }

    private void BindEventData()
    {
        MainView.ActiveViewIndex = 0;
        btnTabEventsCategory.CssClass = "nav-link active";
        //DataSet dsData = objCommon.FillDropDown("acd_student_result", "coursename,SESSIONNO", "ccode", "idno=" + Convert.ToInt32(lblName.ToolTip) + "and semesterno=" + Convert.ToInt32(ds.Tables[0].Rows[0]["SEMESTERNO"]), string.Empty);
        DataSet dsData = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_MASTER", "EVENT_ID", "EVENTNAME,case when isnull(IS_ACTIVE,0)=0 then 'Inactive' else 'Active' end as IS_ACTIVE", "", "");
        if (dsData.Tables[0].Rows.Count > 0)
        {
            lvEventDetails.DataSource = dsData.Tables[0];
            lvEventDetails.DataBind();
        }
        else
        {
            lvEventDetails.DataSource = null;
            lvEventDetails.DataBind();
        }
    }

    private void BindSubEventData()
    {
        DataSet dsData = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_MASTER A INNER JOIN ACD_OD_TRACKER_SUB_EVENT_MASTER B ON A.EVENT_ID = B.EVENT_ID", "SUB_EVENT_ID", "B.EVENT_ID,EVENTNAME,SUB_EVENTNAME,CASE WHEN isnull(B.IS_ACTIVE,0)=0 THEN 'Inactive' ELSE 'Active' END AS IS_ACTIVE", "", "");
        if (dsData.Tables[0].Rows.Count > 0)
        {
            lvSubEventDetails.DataSource = dsData.Tables[0];
            lvSubEventDetails.DataBind();
        }
        else
        {
            lvSubEventDetails.DataSource = null;
            lvSubEventDetails.DataBind();
        }
    }

    protected void btnEditSubEvent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditSubEvent = sender as ImageButton;
            int SubEventNo = int.Parse(btnEditSubEvent.CommandArgument);
            Session["OD_SubEventID"] = int.Parse(btnEditSubEvent.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowSubEventDetails(SubEventNo);
            txtSubEventCategory.Focus();
        }
        catch (Exception ex)
        {
            //throw;
        }
    }

    private void ShowSubEventDetails(int SubEventNo)
    {
        try
        {
            ODTrackerController objSS = new ODTrackerController();
            SqlDataReader dr = objSS.GetSingleSubEvent(SubEventNo);
            if (dr != null)
            {
                if (dr.Read())
                {
                    ddlEventCategory.SelectedValue = dr["EVENT_ID"].ToString();
                    txtSubEventCategory.Text = dr["SUB_EVENTNAME"].ToString();
                    if (dr["IS_ACTIVE"].ToString() == "Active" || dr["IS_ACTIVE"].ToString().ToLower() == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSubEvent(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSubEvent(false);", true);
                    }
                    //ScriptManager.RegisterClientScriptBlock(upd, updS.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
                }

            }
            if (dr != null) dr.Close();

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            //throw;
            objCommon.DisplayMessage(updEventCategory, "Please Check Event Category Active or Not.", this.Page);
            txtSubEventCategory.Text = "";
            ddlEventCategory.SelectedValue = "0";
            Session["action"] = null;
        }
    }

    private void ShowEventDetails(int EventNo)
    {
        try
        {
            ODTrackerController objSS = new ODTrackerController();
            SqlDataReader dr = objSS.GetSingleEvent(EventNo);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtEventCategory.Text = dr["EVENTNAME"].ToString();
                    if (dr["IS_ACTIVE"].ToString() == "Active" || dr["IS_ACTIVE"].ToString().ToLower() == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEvent(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEvent(false);", true);
                    }                     
                }
            }
            if (dr != null) dr.Close();

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearSubEventControls()
    {
        ddlEventCategory.SelectedIndex = 0;
        txtSubEventCategory.Text = "";
    }
    
    private void ClearEventControls()
    {
        txtEventCategory.Text = "";
    }
     
    protected void btnEditEvent_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditEvent = sender as ImageButton;
            int EventNo = int.Parse(btnEditEvent.CommandArgument);
            Session["OD_EventID"] = int.Parse(btnEditEvent.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowEventDetails(EventNo);
            txtEventCategory.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnTabEventsCategory_Click(object sender, EventArgs e)
    {
        Session["action"] = null;
        btnTabEventsCategory.CssClass = "nav-link active";
        btnTabSubEventCategory.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
    }

    protected void btnTabSubEventCategory_Click(object sender, EventArgs e)
    {
        Session["action"] = null;
        btnTabEventsCategory.CssClass = "nav-link";
        btnTabSubEventCategory.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        PopulateDropDownList();
    }
    
    protected void btnCancelSubEventSubmit_Click(object sender, EventArgs e)
    {
        ClearSubEventControls();
        Session["OD_SubEventID"] = 0;
        ViewState["action"] = null;
        Session["action"] = null;
        ViewState["edit"] = "add";
    }
    protected void btnCancelSubmitEvent_Click(object sender, EventArgs e)
    {
        ClearEventControls();
        Session["OD_EventID"] = 0;
        ViewState["action"] = null;
        ViewState["edit"] = "add";
        Session["action"] = null;
    }
}