using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS;

public partial class ACADEMIC_ODTracker_ODTrackerEventConfiguration : System.Web.UI.Page
{
    ODTrackerController ObjTrackerCon = new ODTrackerController();
    ODTracker ObjODTracker = new ODTracker();
    Common objCommon = new Common();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEventConfigData(3);
            MainView.ActiveViewIndex = 0;
            btnTabFacultyEventConfig.CssClass = "nav-link active";
            btnTabStudentEventConfig.CssClass = "nav-link";
            
        }
    }

    private void BindEventConfigData(int EventConfigFor) //EventConfigFor = UserType
    {
        lvEventConfigDetails.DataSource = null;
        lvEventConfigDetails.DataBind();
        lvEventConfigDetailsStudent.DataSource = null;
        lvEventConfigDetailsStudent.DataBind();

        DataSet dsData = new DataSet();
        dsData = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_CONFIG", "CONFIG_SRNO", "EVENT_TYPE,MINIMUM_DAYS_ALLOWED,EVENT_FOR_FACULTY_STUDENT,CASE WHEN IS_ACTIVE = 0 THEN 'NO' WHEN IS_ACTIVE = 1 THEN 'YES'  END IS_ACTIVE", "EVENT_FOR_FACULTY_STUDENT = " + EventConfigFor, "");
        if (dsData.Tables.Count > 0)
        {
            if (dsData.Tables[0].Rows.Count > 0)
            {
                if (EventConfigFor == 2)
                {
                    lvEventConfigDetailsStudent.DataSource = dsData;
                    lvEventConfigDetailsStudent.DataBind();
                }
                else if (EventConfigFor == 3)
                {
                    lvEventConfigDetails.DataSource = dsData;
                    lvEventConfigDetails.DataBind();
                }


            }
        }
    }

    protected void btnSubmitEventConfigFaculty_Click(object sender, EventArgs e)
    {
        ObjODTracker.UANO = Convert.ToInt32(Session["userno"]);
        ObjODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        int ActiveStatus = 0;

        if (hfdFacultyConfig.Value == "true")
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        int UserType = 3;  //for Student

        //check for only one record allowed for faculty config
        string RecordExists = "";
        RecordExists = objCommon.LookUp("ACD_OD_TRACKER_EVENT_CONFIG", "CONFIG_SRNO", "EVENT_FOR_FACULTY_STUDENT = " + UserType);

        Boolean IsforEdit = false;
        //check for only one record allowed for faculty config end       
        if (ViewState["edit"] != null)
        {
            if (ViewState["edit"].ToString().Equals("edit"))
            {
                IsforEdit = true;
            }
        }

        if (!String.IsNullOrEmpty(RecordExists) && !IsforEdit)
        {
            //objCommon.DisplayMessage(updEventConfig, "Configuration can only once please update for change in config.", this);
            objCommon.DisplayMessage(updEventConfig, "Configuration can insert only once, please update the existing configuration.", this);
            return;
        }
        

        if (ViewState["edit"] != null)
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.UpdateODTrackerEventConfig(Convert.ToInt32(ViewState["CONFIG_SRNO"]), Convert.ToInt32(txtMinDaysFaculty.Text), ActiveStatus);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearControl();
                BindEventConfigData(3);
                objCommon.DisplayMessage(updEventConfig, "Record updated sucessfully.", this.Page);
                ViewState["edit"] = null;
                btnSubmitEventConfigFaculty.Text = "Submit";
            }
        }
        else
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.InsertODTrackerEventConfig(Convert.ToInt32(ddlEventType.SelectedValue), Convert.ToInt32(txtMinDaysFaculty.Text), UserType, ObjODTracker, ActiveStatus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updEventConfig, "Record added sucessfully.", this.Page);
                ClearControl();
                BindEventConfigData(3);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updEventConfig, "Record Already Exist.", this.Page);
            }
            else
            {
                //msgLbl.Text = "Record already exist";
                objCommon.DisplayMessage(updEventConfig, "Record Already Exist.", this.Page);
            }
        }
                
    }

    private void ClearControl()
    {
        txtMinDaysFaculty.Text = "";
        ddlStudentEventODType.SelectedIndex = 0;
        ddlEventType.SelectedIndex = 0;
        txtStudentAllowDays.Text = "";
    }

    protected void btnTabFacultyEventConfig_Click(object sender, EventArgs e)
    {
        btnTabFacultyEventConfig.CssClass = "nav-link active";
        btnTabStudentEventConfig.CssClass = "nav-link";
        MainView.ActiveViewIndex = 0;
        BindEventConfigData(3);
        ViewState["edit"] = null;
        btnSubmitEventConfigFaculty.Text = "Submit";
    }

    protected void btnTabStudentEventConfig_Click(object sender, EventArgs e)
    {
        btnTabFacultyEventConfig.CssClass = "nav-link";
        btnTabStudentEventConfig.CssClass = "nav-link active";
        MainView.ActiveViewIndex = 1;
        BindEventConfigData(2);
        ViewState["edit"] = null;
        btnSubmitEventConfigStudent.Text = "Submit";
    }

    protected void btnSubmitEventConfigStudent_Click(object sender, EventArgs e)
    {
        ObjODTracker.UANO = Convert.ToInt32(Session["userno"]);
        ObjODTracker.OrganizationID = Convert.ToInt32(Session["OrgId"]);
        int ActiveStatus = 0;

        if (hfdStudentConfig.Value == "true")
        {
            ActiveStatus = 1;
        }
        else
        {
            ActiveStatus = 0;
        }

        int UserType = 2;  //for Student
        string RecordExists = "";
        RecordExists = objCommon.LookUp("ACD_OD_TRACKER_EVENT_CONFIG", "CONFIG_SRNO", "EVENT_FOR_FACULTY_STUDENT = " + UserType);
        Boolean IsforEdit = false;
        if (ViewState["edit"] != null)
        {
            if (ViewState["edit"].ToString().Equals("edit"))
            {
                IsforEdit = true;
            }
        }
        if (!String.IsNullOrEmpty(RecordExists) && !IsforEdit)
        {
            //objCommon.DisplayMessage(updEventConfig, "Configuration can only once please update for change in config.", this);
            objCommon.DisplayMessage(updEventConfig, "configuration can insert only once, please update the existing configuration.", this);
            return;
        }

        //////////
        if (ViewState["edit"] != null)
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.UpdateODTrackerEventConfig(Convert.ToInt32(ViewState["CONFIG_SRNO"]), Convert.ToInt32(txtStudentAllowDays.Text), ActiveStatus);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ClearControl();
                BindEventConfigData(UserType);
                objCommon.DisplayMessage(updEventConfig, "Record Updated sucessfully", this.Page);
                ViewState["edit"] = null;
                btnSubmitEventConfigStudent.Text = "Submit";
            }
        }
        else
        {
            CustomStatus cs = (CustomStatus)ObjTrackerCon.InsertODTrackerEventConfig(Convert.ToInt32(ddlStudentEventODType.SelectedValue), Convert.ToInt32(txtStudentAllowDays.Text), UserType, ObjODTracker, ActiveStatus);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updEventConfig, "Record Added sucessfully", this.Page);
                ClearControl();
                BindEventConfigData(UserType);
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(updEventConfig, "Record Already Exist", this.Page);
            }
            else
            {
                //msgLbl.Text = "Record already exist";
                objCommon.DisplayMessage(updEventConfig, "Record Already Exist", this.Page);
            }
        }
        ////////////////

        //int result = ObjTrackerCon.InsertODTrackerEventConfig(Convert.ToInt32(ddlStudentEventODType.SelectedValue), Convert.ToInt32(txtStudentAllowDays.Text), UserType, ObjODTracker, ActiveStatus);
        //if (result == 1)
        //{
        //    BindEventConfigData(2);
        //    objCommon.DisplayMessage(updEventConfig, "Record inserted sucessfully.", this);
        //    ClearControl();
        //}
        //else
        //{
        //    objCommon.DisplayMessage(updEventConfig, "something went wrong.", this);
        //}
    }

    protected void btnEditFacultyEventConfig_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmitEventConfigFaculty.Text = "Submit";
            ImageButton btnEditFacultyEventConfig = sender as ImageButton;
            int EventNo = int.Parse(btnEditFacultyEventConfig.CommandArgument);
            ViewState["CONFIG_SRNO"] = int.Parse(btnEditFacultyEventConfig.CommandArgument);
            ViewState["edit"] = "edit";

            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_CONFIG", "CONFIG_SRNO", "MINIMUM_DAYS_ALLOWED,EVENT_TYPE,IS_ACTIVE", "CONFIG_SRNO = " + ViewState["CONFIG_SRNO"].ToString(), "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMinDaysFaculty.Text = ds.Tables[0].Rows[0]["MINIMUM_DAYS_ALLOWED"].ToString();
                    ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["EVENT_TYPE"].ToString();

                    if (ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == "Active" || ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString().ToLower() == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetFacultyConfig(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetFacultyConfig(false);", true);
                    } 

                    btnSubmitEventConfigFaculty.Text = "Save";
                }
            } 
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    protected void btnEditStudentEvenConfig_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnSubmitEventConfigFaculty.Text = "Submit";
            ImageButton btnEditStudentEvenConfig = sender as ImageButton;
            ViewState["CONFIG_SRNO"] = int.Parse(btnEditStudentEvenConfig.CommandArgument);
            ViewState["edit"] = "edit";

            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("ACD_OD_TRACKER_EVENT_CONFIG", "CONFIG_SRNO", "MINIMUM_DAYS_ALLOWED,EVENT_TYPE,IS_ACTIVE", "CONFIG_SRNO = " + ViewState["CONFIG_SRNO"].ToString(), "");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtStudentAllowDays.Text = ds.Tables[0].Rows[0]["MINIMUM_DAYS_ALLOWED"].ToString();
                    ddlStudentEventODType.SelectedValue = ds.Tables[0].Rows[0]["EVENT_TYPE"].ToString();

                    if (ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString() == "Active" || ds.Tables[0].Rows[0]["IS_ACTIVE"].ToString().ToLower() == "true")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStudentConfig(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStudentConfig(false);", true);
                    } 

                    btnSubmitEventConfigStudent.Text = "Save";
                }
            }
        }
        catch
        {
            throw;
        }
    }
}