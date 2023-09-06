using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.SQLServer.SQLDAL;
//using System.Transactions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_TeachingPlan : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITeachingPlan objTPlan = new ITeachingPlan();
    ITeachingPlanController objPL = new ITeachingPlanController();

    #region Page Load

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
        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
            if (Session["ICourseNo"] == null)
            {
                Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
            }

            if (!Page.IsPostBack)
            {
                //Check page refresh
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;
                FillDropDown();
                BindListView();
                //GetStatusOnPageLoad();
            }
            //FillDropDown();
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Itle_TeachingPlan.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_TeachingPlan.aspx");
        }
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlSyllabus, "ACD_ISYLLABUS", "DISTINCT SYLLABUS_NAME AS SYLLABUS_ID", "SYLLABUS_NAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "SYLLABUS_NAME");
            objCommon.FillDropDownList(ddlUnit, "ACD_ISYLLABUS", "DISTINCT UNIT_NAME AS UNIT_ID", "UNIT_NAME", "SYLLABUS_NAME='" + ddlSyllabus.SelectedValue + "' AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "UNIT_NAME");
        }
        catch (Exception ex)
        {

        }
    }

    private void BindListView()
    {
        try
        {
            ITeachingPlan objTPlan = new ITeachingPlan();
            ITeachingPlanController objPL = new ITeachingPlanController();

            objTPlan.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objTPlan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objTPlan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            DataSet ds = objPL.GetAllTeachingPlanbyUaNo(objTPlan);
            lvTPlan.DataSource = ds;
            lvTPlan.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.BindListView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearControle()
    {

        //ddlSyllabus.SelectedValue = "0";
        txtMedia.Text = string.Empty;
        ddlUnit.SelectedValue = "0";
        ddlTopic.SelectedValue = "0";
        txtSubject.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        ftbDescription.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        txtEndTime.Text = string.Empty;

    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("TeachingPlan.aspx");
        }

    }

    private void GetTopicDescription(ITeachingPlan objSub)
    {

        try
        {
            objSub.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objSub.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objSub.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            //objSub.PLAN_NO= Convert.ToInt32(ddlTopic.SelectedValue);


            DataTableReader dtr = objPL.GetTopicDescription(objSub, Session["TOPIC_NAME"].ToString());

            if (dtr != null)
            {
                if (dtr.Read())
                {
                    ftbDescription.Text = dtr["DESCRIPTION"].ToString() == null ? "" : dtr["DESCRIPTION"].ToString();

                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {

        }

    }

    private void ShowDetails(int tPlanNo)
    {
        try
        {
            ITeachingPlanController objPL = new ITeachingPlanController();
            DataTableReader dtr = objPL.GetSinglePlanByPlanNo(tPlanNo);

            if (dtr != null)
            {
                if (dtr.Read())
                {


                    txtSubject.Text = dtr["SUBJECT"].ToString() == null ? "" : dtr["SUBJECT"].ToString();

                    ddlSyllabus.SelectedItem.Text = dtr["SYLLABUS_NAME"].ToString();
                    ddlUnit.SelectedItem.Text = dtr["UNIT_NAME"].ToString();
                    ddlTopic.SelectedItem.Text = dtr["TOPIC_NAME"].ToString();
                    ftbDescription.Text = dtr["DESCRIPTION"].ToString() == null ? "" : dtr["DESCRIPTION"].ToString();
                    txtMedia.Text = dtr["MEDIA"].ToString() == null ? "" : dtr["MEDIA"].ToString();
                    txtStartDate.Text = dtr["START_DATE"] == null ? "" : Convert.ToDateTime(dtr["START_DATE"]).ToString("dd/MM/yyyy");
                    txtStartTime.Text = dtr["START_DATE"] == null ? "" : Convert.ToDateTime(dtr["START_DATE"]).ToString("hh:mm tt");
                    txtEndDate.Text = dtr["END_DATE"] == null ? "" : Convert.ToDateTime(dtr["END_DATE"]).ToString("dd/MM/yyyy");
                    txtEndTime.Text = dtr["END_DATE"] == null ? "" : Convert.ToDateTime(dtr["END_DATE"]).ToString("hh:mm tt");


                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Itle," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);//",COURSE_NAME=" + Session["ICourseName"] +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "TeachingPlan.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        try
        {

            ITeachingPlan objTPlan = new ITeachingPlan();
            ITeachingPlanController objPL = new ITeachingPlanController();

            ImageButton btnEdit = sender as ImageButton;
            int an_no = int.Parse(btnEdit.CommandArgument);
            ViewState["an_no"] = an_no;

            objTPlan.PLAN_NO = Convert.ToInt32(ViewState["an_no"]);
            objTPlan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objTPlan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);

            CustomStatus cs = (CustomStatus)objPL.UpdateStatusToDelete(objTPlan);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayUserMessage(UpdTeachingPlan, "Record Deleted Sucsessfully", this.Page);
            //lblStatus.Text = "Teaching Plan Deleted Successfully...";

            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_TeachingPlan.btnDelete_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewTeachingPlan_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_TeachingPlan_Report", "Itle_TeachingPlan_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "TeachingPlan.btnViewTeachingPlan_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckPageRefresh();
        ITeachingPlan objTPlan = new ITeachingPlan();
        ITeachingPlanController objPL = new ITeachingPlanController();
        #region Date & Time Validation
        try
        {
            objTPlan.STARTDATE = Convert.ToDateTime(txtStartDate.Text.Trim() + (txtStartTime.Text.Trim() == "" ? " 00:00:00" : " " + txtStartTime.Text.Trim()));
        }
        catch
        {
            objCommon.DisplayMessage("Invalid start date or time value.", this);
            return;
        }
        try
        {
            objTPlan.ENDDATE = Convert.ToDateTime(txtEndDate.Text.Trim() + (txtEndTime.Text.Trim() == "" ? " 00:00:00" : " " + txtEndTime.Text.Trim()));
        }
        catch
        {
            objCommon.DisplayMessage("Invalid end date or time value.", this);
            return;
        }
        if (Convert.ToDateTime(objTPlan.STARTDATE) > Convert.ToDateTime(objTPlan.ENDDATE))
        {
            //objCommon.DisplayMessage("Start date cannot be greater than end date.", this);
            MessageBox("Start date-Time cannot be greater than end  date-Time.");
            return;
        }

        //if (Convert.ToDateTime(objTPlan.STARTDATE) == Convert.ToDateTime(objTPlan.ENDDATE))
        //{
        //    if (Convert.ToDateTime(txtStartTime.Text.Trim()) > Convert.ToDateTime(txtEndTime.Text.Trim())) 
        //    {
        //        objCommon.DisplayMessage("Start time cannot be greater than end time.", this);
        //        return;
        //    }
        //}
        #endregion

        try
        {
            objTPlan.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objTPlan.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objTPlan.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objTPlan.STARTDATE = Convert.ToDateTime(txtStartDate.Text.Trim() + (txtStartTime.Text.Trim() == "" ? " 00:00:00" : " " + txtStartTime.Text.Trim()));
            objTPlan.ENDDATE = Convert.ToDateTime(txtEndDate.Text.Trim() + (txtEndTime.Text.Trim() == "" ? " 00:00:00" : " " + txtEndTime.Text.Trim()));
            objTPlan.SUBJECT = txtSubject.Text.Trim();
            objTPlan.SYLLABUS_NAME = ddlSyllabus.SelectedValue;

            if (ddlUnit.SelectedIndex > 0)
            {
                objTPlan.UNIT_NAME = ddlUnit.SelectedValue;
            }
            else
            {
                objTPlan.UNIT_NAME = "0";
            }

            if (ddlTopic.SelectedIndex > 0)
            {
                objTPlan.TOPIC_NAME = ddlTopic.SelectedValue;
            }
            else
            {
                objCommon.DisplayUserMessage(UpdTeachingPlan, " Please Select Topic.", this.Page);
                return;

                
            }

            objTPlan.DESCRIPTION = ftbDescription.Text.Trim();
            objTPlan.MEDIA = txtMedia.Text.Trim();
            objTPlan.STATUS = (objTPlan.ENDDATE < DateTime.Now ? '0' : '1');
            objTPlan.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objTPlan.PLAN_NO = Convert.ToInt32(ViewState["an_no"]);

                CustomStatus cs = (CustomStatus)objPL.UpdateTeachingPlan(objTPlan);
                if (cs.Equals(CustomStatus.RecordUpdated))
                    objCommon.DisplayUserMessage(UpdTeachingPlan, "Teaching plan updated successfully", this.Page);
                    //objCommon.DisplayMessage("Record updated successfully.", this);
                else
                    objCommon.DisplayUserMessage(UpdTeachingPlan, "Unable to update the record.", this.Page);
                    //objCommon.DisplayMessage("Unable to update the record.", this);
            }
            else
            {
                CustomStatus cs = (CustomStatus)objPL.AddTeachingPlan(objTPlan);
                if (cs.Equals(CustomStatus.RecordSaved))
                    objCommon.DisplayUserMessage(UpdTeachingPlan, " Teaching plan created successfully.", this.Page);
                else
                    objCommon.DisplayUserMessage(UpdTeachingPlan, "Unable to Add the record.", this.Page);
            }
            ClearControle();
            BindListView();
            //Response.Redirect("TeachingPlan.aspx");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "TeachingPlan.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int an_no = int.Parse(btnEdit.CommandArgument);
            ViewState["an_no"] = an_no;
            ViewState["action"] = "edit";


            objCommon.FillDropDownList(ddlUnit, "ACD_ISYLLABUS", "DISTINCT UNIT_NAME AS UNIT_ID", "UNIT_NAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "UNIT_NAME");
            objCommon.FillDropDownList(ddlTopic, "ACD_ISYLLABUS", "DISTINCT TOPIC_NAME AS TOPIC_ID", "TOPIC_NAME", "COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "TOPIC_NAME");
            FillDropDown();
            ShowDetails(an_no);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TeachingPlan.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string GetStatus(object status)
    {
        DateTime DT = Convert.ToDateTime(status);
        if (DT < DateTime.Today)
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    #endregion

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    #region Selected Index Changed

    protected void ddlSyllabus_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlUnit, "ACD_ISYLLABUS", "DISTINCT UNIT_NAME AS UNIT_ID", "UNIT_NAME", "SYLLABUS_NAME='" + ddlSyllabus.SelectedValue + "' AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND UA_NO=" + Convert.ToInt32(Session["userno"]) + " AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "UNIT_NAME");
    }

    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlTopic, "ACD_ISYLLABUS", "DISTINCT TOPIC_NAME AS TOPIC_ID", "TOPIC_NAME", "SYLLABUS_NAME='" + ddlSyllabus.SelectedValue + "' AND UNIT_NAME='" + ddlUnit.SelectedValue + "' AND COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "AND UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND SESSIONNO=" + Convert.ToInt32(Session["SessionNo"].ToString()), "TOPIC_NAME");

    }

    protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["TOPIC_NAME"] = ddlTopic.SelectedValue;
        GetTopicDescription(objTPlan);
    }

    #endregion

    #region Commented code

    //private void GetStatusOnPageLoad()
    //{
    //    IAnnouncementController objAC = new IAnnouncementController();
    //    try
    //    {
    //        DateTime CurrentDate = DateTime.Today;
    //        objAC.GetStatus(CurrentDate);

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    #endregion
}