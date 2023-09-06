//======================================================================================
// PROJECT NAME  :RFC_CODE                                                                
// MODULE NAME   : Achievement_Basic_Details                     
// CREATION DATE : 11-03-2022                                                        
// CREATED BY    : DIKSHA NANDURKAR  
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                    
//=============================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;

public partial class ACADEMIC_StudentAchievement_Achievement_Basic_Details : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AchievementBasicDetailsController objBDC = new AchievementBasicDetailsController();
    AchievementBasicDetailsEntity objBDE = new AchievementBasicDetailsEntity();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindListView();
            EventNatureListView();
            EventCategoryListView();
            ActivityListView();
            AcadmicListView();
            EventLevelListView();
            TechinicalActivityListView();
            ParticipationTypeListView();
            MoocsPlatformListView();
            DurationListView();
            FillDropDown();
            ViewState["action"] = "add";

            ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        }
    }

    #region Tab Click Event
    //protected void btnTab1_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}

    //protected void btnTab2_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    protected void btnTab3_Click(object sender, EventArgs e)
    {
        FillDropDown();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    protected void btnTab4_Click(object sender, EventArgs e)
    {
        FillDropDown();
        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);
    }
    //protected void btnTab5_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    //protected void btnTab6_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    //protected void btnTab7_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    //protected void btnTab8_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    //protected void btnTab9_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    //protected void btnTab10_Click(object sender, EventArgs e)
    //{
    //    objCommon.DisplayMessage(this, "Record Called...", this.Page);
    //}
    #endregion

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlEventNature, "ACD_ACHIEVEMENT_EVENT_NATURE", "EVENT_NATURE_ID", "EVENT_NATURE", "EVENT_NATURE_ID > 0 AND ACTIVE_STATUS=1", "EVENT_NATURE_ID");
            objCommon.FillDropDownList(ddlEventCatergory, "ACD_ACHIEVEMENT_EVENT_CATEGORY", "EVENT_CATEGORY_ID", "EVENT_CATEGORY_NAME", "EVENT_CATEGORY_ID > 0  AND ACTIVE_STATUS=1", "EVENT_CATEGORY_ID");

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmitParticipationLevel_Click(object sender, EventArgs e)
    {

        objBDE.participation_level_name = txtParticipationLevelName.Text.Trim();


        if (hfdActive.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {

            //You can not inactive this record because of this is used in another one form.
            string refStatus = objBDC.CheckReferMasterTable(1, "Participation Level", Convert.ToInt32(ViewState["bdid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {


            objBDE.participation_level_id = Convert.ToInt32(ViewState["bdid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateBasicDetailsData(objBDE);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                clearData();
                BindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            }
        }




        else
        {
            //Edit 
            CustomStatus cs = (CustomStatus)objBDC.InsertBasicDetailsData(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                clearData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                clearData();
            }

            BindListView();

        }
    }






    protected void BindListView()
    {
        try
        {

            DataSet ds = objBDC.GetListview();

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlSession.Visible = true;
                lvBascicDetails.DataSource = ds.Tables[0];
                lvBascicDetails.DataBind();


            }
            else
            {
                pnlSession.Visible = true;
                lvBascicDetails.DataSource = null;
                lvBascicDetails.DataBind();

            }
            foreach (ListViewDataItem dataitem in lvBascicDetails.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnCancelParticipationLevel_Click(object sender, EventArgs e)
    {
        clearData();
    }
    public void clearData()
    {
        ViewState["action"] = null;
        txtParticipationLevelName.Text = "";

    }
    protected void btnParticipationLevel_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton btnParticipationLevel = sender as LinkButton;
            int PARTICIPATION_LEVEL_ID = Convert.ToInt32(btnParticipationLevel.CommandArgument);
            ViewState["bdid"] = Convert.ToInt32(btnParticipationLevel.CommandArgument);
            ShowDetails(PARTICIPATION_LEVEL_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int PARTICIPATION_LEVEL_ID)
    {
        DataSet ds = objBDC.EditBasicDetailsData(PARTICIPATION_LEVEL_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            //txtpationLevelId.Text = ds.Tables[0].Rows[0]["PARTICIPATION_LEVEL_ID"].ToString();
            txtParticipationLevelName.Text = ds.Tables[0].Rows[0]["PARTICIPATION_LEVEL_NAME"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(false);", true);
            }
        }


    }

    protected void lvBascicDetails_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void btnSubmitEventNature_Click(object sender, EventArgs e)
    {

        objBDE.event_nature_name = txtEventNature.Text.Trim();

        if (hfdEvent.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {

            //You can not inactive this record because of this is used in another one form.
            string refStatus = objBDC.CheckReferMasterTable(2, "Event Nature", Convert.ToInt32(ViewState["evid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            objBDE.event_nature_id = Convert.ToInt32(ViewState["evid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateEventNatureData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))

                ClearEventNatureData();
            EventNatureListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);


        }
        else
        {
            CustomStatus cs = (CustomStatus)objBDC.InsertAchievementEventNature(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);


                ClearEventNatureData();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }

            EventNatureListView();
        }

    }





    protected void btnCancelEventNature_Click(object sender, EventArgs e)
    {
        ClearEventNatureData();
    }

    public void ClearEventNatureData()
    {
        ViewState["action"] = null;
        txtEventNature.Text = "";
    }



    protected void EventNatureListView()
    {
        try
        {

            DataSet ds = objBDC.GetEventNatureList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Panel2.Visible = true;
                lvEvent.DataSource = ds.Tables[0];
                lvEvent.DataBind();


            }
            else
            {
                Panel2.Visible = true;
                lvEvent.DataSource = null;
                lvEvent.DataBind();

            }




            foreach (ListViewDataItem dataitem in lvEvent.Items)
            {
                Label Status = dataitem.FindControl("lblEStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvEvent_ItemEditing1(object sender, ListViewEditEventArgs e)
    {

    }
    protected void btnEventEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEventEdit = sender as LinkButton;
            int EVENT_NATURE_ID = Convert.ToInt32(btnEventEdit.CommandArgument);
            ViewState["evid"] = Convert.ToInt32(btnEventEdit.CommandArgument);
            ShowEventDetails(EVENT_NATURE_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowEventDetails(int EVENT_NATURE_ID)
    {
        DataSet ds = objBDC.EditEventNatureData(EVENT_NATURE_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtEventNature.Text = ds.Tables[0].Rows[0]["EVENT_NATURE"].ToString();


            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventNature(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventNature(false);", true);
            }
        }


    }
    protected void btnSubmitEventCategory_Click(object sender, EventArgs e)
    {

        objBDE.event_category_name = txtEventCategory.Text.Trim();
        objBDE.event_nature_id = Convert.ToInt32(ddlEventNature.SelectedValue);

        if (hfvCategory.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {

            //You can not inactive this record because of this is used in another one form.
            string refStatus = objBDC.CheckReferMasterTable(3, "Event Category", Convert.ToInt32(ViewState["caid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.event_category_id = Convert.ToInt32(ViewState["caid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateCategoryData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))
                ClearEventCategoryData();
            EventCategoryListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
        }
        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertEventCategoryData(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);


                ClearEventCategoryData();
            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearEventCategoryData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearEventCategoryData();
            }
            EventCategoryListView();

        }

    }

    protected void btnCancelEventCategory_Click(object sender, EventArgs e)
    {
        ClearEventCategoryData();
    }

    public void ClearEventCategoryData()
    {
        ViewState["action"] = null;
        txtEventCategory.Text = "";
        ddlEventNature.SelectedIndex = 0;
    }



    protected void EventCategoryListView()
    {
        try
        {

            DataSet ds = objBDC.GetCategoryList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelCategory.Visible = true;
                lvCategory.DataSource = ds.Tables[0];
                lvCategory.DataBind();


            }
            else
            {
                PanelCategory.Visible = true;
                lvCategory.DataSource = null;
                lvCategory.DataBind();

            }


            foreach (ListViewDataItem dataitem in lvCategory.Items)
            {
                Label Status = dataitem.FindControl("lblCStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btneditEventCategory_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btneditEventCategory = sender as LinkButton;
            int EVENT_CATEGORY_ID = Convert.ToInt32(btneditEventCategory.CommandArgument);
            ViewState["caid"] = Convert.ToInt32(btneditEventCategory.CommandArgument);
            ShowCategoryDetails(EVENT_CATEGORY_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowCategoryDetails(int EVENT_CATEGORY_ID)
    {
        DataSet ds = objBDC.EditCategoryData(EVENT_CATEGORY_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtEventCategory.Text = ds.Tables[0].Rows[0]["EVENT_CATEGORY_NAME"].ToString();

            ddlEventNature.SelectedValue = ds.Tables[0].Rows[0]["EVENT_NATURE_ID"].ToString();
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(true);", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(false);", true);
            }
        }


    }
    protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }

    protected void btnSubmitAcademicYear_Click(object sender, EventArgs e)
    {

        DataTable dt;
        /*if (date.InnerText.ToString().Length < 2)
        {
            objCommon.DisplayMessage(this, "Please select FROM Date TO Date !", this.Page);
            return;
        }*/


        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage("Please select FROM Date TO Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }
        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        //DateTime dateTime10 = Convert.ToDateTime(a);
        DateTime dtStartDate = DateTime.Parse(StartDate);
        objBDE.SDate = DateTime.Parse(StartDate);
        DateTime dtEndDate = DateTime.Parse(EndDate);
        objBDE.EDate = DateTime.Parse(EndDate);
        objBDE.status = chkDefault.Checked;


        objBDE.acadamic_year_name = txtAcademicYear.Text.Trim();

        if (hfvYear.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(5, "Academic Year", Convert.ToInt32(ViewState["ayid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.acadamic_year_id = Convert.ToInt32(ViewState["ayid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateAcadmic(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))

                ClearAcademicYearData();
            AcadmicListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);


        }

        else
        {
            CustomStatus cs = (CustomStatus)objBDC.InsertAcadmicYear(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {



                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearAcademicYearData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearAcademicYearData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearAcademicYearData();
            }
            AcadmicListView();


        }

    }

    protected void AcadmicListView()
    {
        try
        {

            DataSet ds = objBDC.AcadmicYearListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelAcademic.Visible = true;
                lvAcademic.DataSource = ds.Tables[0];
                lvAcademic.DataBind();


            }
            else
            {
                PanelAcademic.Visible = true;
                lvAcademic.DataSource = null;
                lvAcademic.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvAcademic.Items)
            {
                Label Status = dataitem.FindControl("lblAcademicStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnCancelAcademicYear_Click(object sender, EventArgs e)
    {
        ClearAcademicYearData();
    }
    public void ClearAcademicYearData()
    {
        ViewState["action"] = null;
        txtAcademicYear.Text = "";
        chkDefault.Checked = false;
        txtStartDate.Text = "";
        txtEndDate.Text = "";


    }


    protected void btnSubmitEventLevel_Click(object sender, EventArgs e)
    {

        objBDE.event_level_name = txtEventLevel.Text.Trim();


        if (hfvLevel.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(6, "Event Level", Convert.ToInt32(ViewState["elid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.event_level_id = Convert.ToInt32(ViewState["elid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateEventLevelData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))



                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            EventLevelListView();
            ClearEventLevelData();
            return;
        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertEventLevel(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearEventLevelData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            EventLevelListView();

        }

    }


    protected void btnCancelEventLevel_Click(object sender, EventArgs e)
    {
        ClearEventLevelData();

    }
    public void ClearEventLevelData()
    {
        ViewState["action"] = null;
        txtEventLevel.Text = "";

    }
    protected void EventLevelListView()
    {
        try
        {

            DataSet ds = objBDC.EventLevelListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                panelEvent.Visible = true;
                lvEventLevel.DataSource = ds.Tables[0];
                lvEventLevel.DataBind();

                btnSubmitParticipationLevel.Visible = true;
            }
            else
            {
                panelEvent.Visible = true;
                lvEventLevel.DataSource = null;
                lvEventLevel.DataBind();

            }




            foreach (ListViewDataItem dataitem in lvEventLevel.Items)
            {
                Label Status = dataitem.FindControl("lblEventStatus") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnEditEventLevel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditEventLevel = sender as LinkButton;
            int EVENT_LEVEL_ID = Convert.ToInt32(btnEditEventLevel.CommandArgument);
            ViewState["elid"] = Convert.ToInt32(btnEditEventLevel.CommandArgument);
            ShowEventLevel(EVENT_LEVEL_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }


    }
    private void ShowEventLevel(int EVENT_LEVEL_ID)
    {
        DataSet ds = objBDC.EditEventLevel(EVENT_LEVEL_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtEventLevel.Text = ds.Tables[0].Rows[0]["EVENT_LEVEL"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventLevel(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetEventLevel(false);", true);
            }
        }

    }
    protected void btnSubmitTechnicalActivity_Click(object sender, EventArgs e)
    {

        objBDE.technical_type = txtTechnicalActivity.Text.Trim();


        if (hfvTechnical.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(7, "Technical Activity Type", Convert.ToInt32(ViewState["taid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.technical_type_id = Convert.ToInt32(ViewState["taid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateTechnicalActivityData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))


                TechinicalActivityListView();


            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearTechnicalActivityData();

        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertTechnicalActivityType(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearTechnicalActivityData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            TechinicalActivityListView();


        }

    }




    protected void TechinicalActivityListView()
    {
        try
        {

            DataSet ds = objBDC.TecnicalActivityListView();
            if (ds.Tables[0].Rows.Count > 0)
            {
                panelTec.Visible = true;
                lvTechnical.DataSource = ds.Tables[0];
                lvTechnical.DataBind();


            }
            else
            {
                panelTec.Visible = true;
                lvTechnical.DataSource = null;
                lvTechnical.DataBind();

            }


            foreach (ListViewDataItem dataitem in lvTechnical.Items)
            {
                Label Status = dataitem.FindControl("lblTStatus") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancelTechnicalActivity_Click(object sender, EventArgs e)
    {
        ClearTechnicalActivityData();
    }
    public void ClearTechnicalActivityData()
    {
        ViewState["action"] = null;
        txtTechnicalActivity.Text = "";

    }

    protected void btnEditTechnical_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditTechnical = sender as LinkButton;
            int TECHNICALACTIVITY_TYPE_ID = Convert.ToInt32(btnEditTechnical.CommandArgument);
            ViewState["taid"] = Convert.ToInt32(btnEditTechnical.CommandArgument);
            ShowTechnicalActivity(TECHNICALACTIVITY_TYPE_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ShowTechnicalActivity(int TECHNICALACTIVITY_TYPE_ID)
    {
        DataSet ds = objBDC.EditTechnicalActivity(TECHNICALACTIVITY_TYPE_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtTechnicalActivity.Text = ds.Tables[0].Rows[0]["TECHNICALACTIVITY_TYPE"].ToString();


            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTechnical(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTechnical(false);", true);
            }
        }

    }
    protected void btnSubmitParticipation_Click(object sender, EventArgs e)
    {
        objBDE.participation_type = txtParticipation.Text.Trim();


        if (hfvParticipation.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(8, "Participation Type", Convert.ToInt32(ViewState["ptid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.participation_type_id = Convert.ToInt32(ViewState["ptid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateParticipationType(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))


                ParticipationTypeListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearParticipationTypeData();
            return;
        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertParticipationType(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearParticipationTypeData();
                ParticipationTypeListView();
            }

            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            ParticipationTypeListView();
        }

    }



    protected void ParticipationTypeListView()
    {
        try
        {

            DataSet ds = objBDC.ParticipationTypeListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                panelParticipation.Visible = true;
                lvParticipation.DataSource = ds.Tables[0];
                lvParticipation.DataBind();


            }
            else
            {
                panelParticipation.Visible = true;
                lvParticipation.DataSource = null;
                lvParticipation.DataBind();

            }


            foreach (ListViewDataItem dataitem in lvParticipation.Items)
            {
                Label Status = dataitem.FindControl("lblParticipationStatus") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowParticipationType(int PARTICIPATION_TYPE_ID)
    {
        DataSet ds = objBDC.EditParticipationType(PARTICIPATION_TYPE_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            txtParticipation.Text = ds.Tables[0].Rows[0]["PARTICIPATION_TYPE"].ToString();


            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setParticipation(true);", true);

            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setParticipation(false);", true);
            }
        }

    }
    protected void btnCancelParticipation_Click(object sender, EventArgs e)
    {
        ClearParticipationTypeData();
    }
    public void ClearParticipationTypeData()
    {
        ViewState["action"] = null;
        txtParticipation.Text = "";
    }
    protected void btnEditParticipation_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditParticipatipn = sender as LinkButton;
            int PARTICIPATION_TYPE_ID = Convert.ToInt32(btnEditParticipatipn.CommandArgument);
            ViewState["ptid"] = Convert.ToInt32(btnEditParticipatipn.CommandArgument);
            ShowParticipationType(PARTICIPATION_TYPE_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnSubmitMoocsPlatform_Click(object sender, EventArgs e)
    {

        objBDE.mooc_platform = txtMoocsPlatform.Text.Trim();

        if (hfvmooc.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(9, "Moocs Platform", Convert.ToInt32(ViewState["mpid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            objBDE.moocs_platform_id = Convert.ToInt32(ViewState["mpid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateMoocsPlatform(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))
                MoocsPlatformListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearMoocsPlateFormData();
            return;
        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertMoocsPlatformData(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearMoocsPlateFormData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
            }
            MoocsPlatformListView();

        }

    }

    protected void btnCancelMoocsPlatform_Click(object sender, EventArgs e)
    {
        ClearMoocsPlateFormData();
    }
    public void ClearMoocsPlateFormData()
    {
        ViewState["action"] = null;
        txtMoocsPlatform.Text = "";
    }
    protected void MoocsPlatformListView()
    {
        try
        {

            DataSet ds = objBDC.MoocsPlatformListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                panelMoocs.Visible = true;
                lvMoocs.DataSource = ds.Tables[0];
                lvMoocs.DataBind();


            }
            else
            {
                panelMoocs.Visible = true;
                lvMoocs.DataSource = null;
                lvMoocs.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvMoocs.Items)
            {
                Label Status = dataitem.FindControl("lblMoocsStatus") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEditMoocs_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditMoocs = sender as LinkButton;
            int MOOCS_PLATFORM_ID = Convert.ToInt32(btnEditMoocs.CommandArgument);
            ViewState["mpid"] = Convert.ToInt32(btnEditMoocs.CommandArgument);
            ShowMoocsPlatform(MOOCS_PLATFORM_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    private void ShowMoocsPlatform(int MOOCS_PLATFORM_ID)
    {
        DataSet ds = objBDC.EditMoocsPlatform(MOOCS_PLATFORM_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {


            txtMoocsPlatform.Text = ds.Tables[0].Rows[0]["MOOCS_PLATFORM"].ToString();


            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setMoocs(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setMoocs(false);", true);
            }
        }

    }
    protected void btnSubmitDuration_Click(object sender, EventArgs e)
    {

        objBDE.duration = txtDuration.Text.Trim();
        if (hfvDuration.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(10, "Duration", Convert.ToInt32(ViewState["duid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {
            objBDE.duration_id = Convert.ToInt32(ViewState["duid"]);

            CustomStatus cs = (CustomStatus)objBDC.UpdateDurationData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))

                DurationListView();
            objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
            ClearDurationData();

        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertDuration(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearDurationData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearDurationData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearDurationData();
            }
            DurationListView();


        }

    }


    protected void DurationListView()
    {
        try
        {

            DataSet ds = objBDC.DurationBindListview();
            if (ds.Tables[0].Rows.Count > 0)
            {
                panelDuration.Visible = true;
                lvDuration.DataSource = ds.Tables[0];
                lvDuration.DataBind();


            }
            else
            {
                panelDuration.Visible = true;
                lvDuration.DataSource = null;
                lvDuration.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvDuration.Items)
            {
                Label Status = dataitem.FindControl("lblDurationStatus") as Label;
                string Statuss = (Status.Text);
                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEditDuration_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditDuration = sender as LinkButton;
            int DURATION_ID = Convert.ToInt32(btnEditDuration.CommandArgument);
            ViewState["duid"] = Convert.ToInt32(btnEditDuration.CommandArgument);
            ShowDurationData(DURATION_ID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void ShowDurationData(int DURATION_ID)
    {
        DataSet ds = objBDC.EditDuration(DURATION_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtDuration.Text = ds.Tables[0].Rows[0]["DURATION"].ToString();

            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setDuration(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setDuration(false);", true);
            }
        }

    }

    protected void btnCancelDuration_Click(object sender, EventArgs e)
    {
        ClearDurationData();
    }
    public void ClearDurationData()
    {
        ViewState["action"] = null;
        txtDuration.Text = "";

    }
    protected void btnSubmitActivityCategory_Click1(object sender, EventArgs e)
    {
        objBDE.activity_category_name = txtActivityCategory.Text.Trim();
        objBDE.event_category_id = Convert.ToInt32(ddlEventCatergory.SelectedValue);

        if (hfvActivity.Value == "true")
        {
            objBDE.IsActive = true;
        }
        else
        {
            objBDE.IsActive = false;
        }

        objBDE.OrganizationId = Convert.ToInt32(Session["OrgId"]);


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit") && objBDE.IsActive == false)
        {
            string refStatus = objBDC.CheckReferMasterTable(4, "Activity Category", Convert.ToInt32(ViewState["acid"]));

            /*string[] commandArgs = refStatus.Split(new char[] { ',' });
            string statusCode = commandArgs[0];
            string statusName = commandArgs[1];*/

            if (refStatus.Equals("2"))
            {
                objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                return;
            }

        }


        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objBDE.activity_category_id = Convert.ToInt32(ViewState["acid"]);
            CustomStatus cs = (CustomStatus)objBDC.UpdateActivityData(objBDE);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ActivityListView();
                ClearActivityCategoryData();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);


            }
        }

        else
        {

            CustomStatus cs = (CustomStatus)objBDC.InsertActivityCategory(objBDE);
            if (cs.Equals(CustomStatus.RecordSaved))
            {

                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearActivityCategoryData();

            }
            else if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearActivityCategoryData();
            }
            else
            {

                objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                ClearActivityCategoryData();
            }
            ActivityListView();

        }
    }

    protected void btnCancelActivityCategory_Click(object sender, EventArgs e)
    {
        ClearActivityCategoryData();
    }

    public void ClearActivityCategoryData()
    {
        ViewState["action"] = null;
        txtActivityCategory.Text = "";
        ddlEventCatergory.SelectedIndex = 0;

    }


    protected void ActivityListView()
    {
        try
        {

            DataSet ds = objBDC.GetActivityCategoryList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelActivity.Visible = true;
                lvActivity.DataSource = ds.Tables[0];
                lvActivity.DataBind();


            }
            else
            {
                PanelActivity.Visible = true;
                lvActivity.DataSource = null;
                lvActivity.DataBind();

            }


            foreach (ListViewDataItem dataitem in lvActivity.Items)
            {
                Label Status = dataitem.FindControl("lblAStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss == "InActive")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void btnEditActivity_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEditActivity = sender as LinkButton;
            int ACTIVITY_CATEGORY_ID = Convert.ToInt32(btnEditActivity.CommandArgument);
            ViewState["acid"] = Convert.ToInt32(btnEditActivity.CommandArgument);
            ShowActivity(ACTIVITY_CATEGORY_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }

    }


    private void ShowActivity(int ACTIVITY_CATEGORY_ID)
    {
        DataSet ds = objBDC.EditActivityCategoryData(ACTIVITY_CATEGORY_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {


            txtActivityCategory.Text = ds.Tables[0].Rows[0]["ACTIVITY_CATEGORY_NAME"].ToString();

            ddlEventCatergory.SelectedValue = ds.Tables[0].Rows[0]["EVENT_CATEGORY_ID"].ToString();


            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActivity(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActivity(false);", true);
            }
        }

    }










    protected void btnEditAcademicYear_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton btnEditAcademicYear = sender as LinkButton;
            int ACADMIC_YEAR_ID = Convert.ToInt32(btnEditAcademicYear.CommandArgument);
            ViewState["ayid"] = Convert.ToInt32(btnEditAcademicYear.CommandArgument);
            ShowAcadmic(ACADMIC_YEAR_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowAcadmic(int ACADMIC_YEAR_ID)
    {
        DataSet ds = objBDC.EditAcadmic(ACADMIC_YEAR_ID);


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            txtAcademicYear.Text = ds.Tables[0].Rows[0]["ACADMIC_YEAR_NAME"].ToString();

            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy");
            txtEndDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
            if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString() == "Active")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');setAcademic(true);", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(true);", true);
                ////ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setAcademic(true);", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetParticipation(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');setAcademic(false);", true);
                //ScriptManager.RegisterClientScriptBlock(this, GetType(), "Src", "SetParticipation(false);", true);
                ////ScriptManager.RegisterStartupScript(this, GetType(), "Src", "setAcademic(false);", true);
            }

        }

    }


}









