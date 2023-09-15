using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AcademicCanlender : System.Web.UI.Page
{
    AcademicCalender objAcademicController = new AcademicCalender();
     Common objCommon = new Common();
     UAIMS_Common objUCommon = new UAIMS_Common();
     Attendance objAttendanceEntity = new Attendance();
     StudentAttendanceController objSAC = new StudentAttendanceController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if ((this.Session["userno"] == null) || ((this.Session["username"] == null) || ((this.Session["usertype"] == null) || (this.Session["userfullname"] == null))))
            {
                base.Response.Redirect("~/default.aspx");
            }
            else
            {
                this.CheckPageAuthorization();
                this.Page.Title = this.Session["coll_name"].ToString();
                string text1 = base.Request.QueryString["pageno"];
                this.FillDropdown();
                this.Clear();
                this.ViewState["SrNo"] = "0";
                this.ViewState["action"] = "add";
            }
        }
        this.divMsg.InnerHtml = string.Empty;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["masterpage"] != null)
        {
            this.objCommon.SetMasterPage(this.Page, this.Session["masterpage"].ToString());
        }
        else
        {
            this.objCommon.SetMasterPage(this.Page, "");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string str = (((base.Request.Url.ToString().Substring(0, base.Request.Url.ToString().ToLower().IndexOf("academic")) + "Reports/CommonReport.aspx?") + "pagetitle=" + reportTitle) + "&path=~,Reports,Academic," + rptFileName) + "&param=@P_COLLEGE_CODE=" + this.Session["colcode"].ToString();
            this.divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            string[] strArray = new string[] { this.divMsg.InnerHtml, " window.open('", str, "','", reportTitle, "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');" };
            this.divMsg.InnerHtml = string.Concat(strArray);
            this.divMsg.InnerHtml = this.divMsg.InnerHtml + " </script>";
            StringBuilder builder = new StringBuilder();
            builder.Append("window.open('" + str + "','','" + "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes" + "');");
            ScriptManager.RegisterClientScriptBlock(this.updSession, this.updSession.GetType(), "controlJSScript", builder.ToString(), true);
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListLiew();
    }

    private void FillDropdown()
    {
        try
        {
            this.objCommon.FillDropDownList(this.ddlAdm, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID > 0 AND ISNULL(IS_CURRENT_FY,0)=1 AND ISNULL(ACTIVE_STATUS,0)=1", "ACADEMIC_YEAR_ID DESC");
            this.objCommon.FillDropDownList(this.ddlDegree1, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "DEGREENO");
            this.objCommon.FillDropDownList(this.ddlActivityType, "ACD_CALENDER_ACTIVITY_TYPE", "CANO", "ACTIVITY_TYPE", "CANO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "CANO");
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void CheckPageAuthorization()
    {
        if (base.Request.QueryString["pageno"] == null)
        {
            base.Response.Redirect("~/notauthorized.aspx?page=AcademicCanlender.aspx");
        }
        else if (!Common.CheckPage(int.Parse(this.Session["userno"].ToString()), base.Request.QueryString["pageno"].ToString(), int.Parse(this.Session["loginid"].ToString()), 0))
        {
            base.Response.Redirect("~/notauthorized.aspx?page=AcademicCanlender.aspx");
        }
    }

    private void Clear()
    {
        this.ddlAdm.SelectedIndex = 0;
        this.ddlDegree1.SelectedIndex = 0;
        this.ddlActivityType.SelectedIndex = 0;
        this.txtActivity.Text = "";
        this.txtGenaralSchedule.Text = "";
        this.txtDateDuration.Text = "";
        this.txtFromDate.Text = "";
        this.txtTodate.Text = "";
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        this.ShowReport("ACADEMICCALENDAR", "rptAcademicCalandarSchedule.rpt");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if ((this.ViewState["SrNo"].ToString() != string.Empty) && (this.ViewState["SrNo"].ToString() == "0"))
            {
                if (((CustomStatus)this.objAcademicController.Add_AcademicCalendar(Convert.ToInt32(this.ddlAdm.SelectedValue), Convert.ToInt32(this.ddlDegree1.SelectedValue), this.ddlActivityType.SelectedValue, this.txtActivity.Text, this.txtGenaralSchedule.Text, Convert.ToDateTime(this.txtFromDate.Text), Convert.ToDateTime(this.txtTodate.Text), this.Session["colcode"].ToString())).Equals(CustomStatus.TransactionFailed))
                {
                    this.objCommon.DisplayMessage(this.updSession, "Error in Adding Plan !", this.Page);
                }
                else
                {
                    this.objCommon.DisplayMessage(this.updSession, "Record Added Successfully!", this.Page);
                    this.BindListLiew();
                    this.Clear();
                }
            }
            else if (((CustomStatus)this.objAcademicController.Update_AcademicCalendar(Convert.ToInt32(this.ViewState["SrNo"]), Convert.ToInt32(this.ddlAdm.SelectedValue), Convert.ToInt32(this.ddlDegree1.SelectedValue), this.ddlActivityType.SelectedValue, this.txtActivity.Text, this.txtGenaralSchedule.Text, Convert.ToDateTime(this.txtFromDate.Text), this.Session["colcode"].ToString(), Convert.ToDateTime(this.txtTodate.Text))).Equals(CustomStatus.TransactionFailed))
            {
                this.objCommon.DisplayMessage(this.updSession, "Error in  Updating Plan!", this.Page);
            }
            else
            {
                this.objCommon.DisplayMessage(this.updSession, "Record Updated Successfully!", this.Page);
                this.BindListLiew();
                this.Clear();
                this.ViewState["SrNo"] = "0";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton button = sender as ImageButton;
            DataSet set = this.objAcademicController.GetSingle_AcademicCalendarDetails(int.Parse(button.CommandArgument));
            if (((set != null) && (set.Tables.Count > 0)) && (set.Tables[0].Rows.Count > 0))
            {
                DataRow row = set.Tables[0].Rows[0];
                this.ViewState["SrNo"] = row["SRNO"].ToString();
                this.ddlAdm.SelectedValue = set.Tables[0].Rows[0]["ADMBATCH"].ToString();
                this.ddlDegree1.SelectedValue = set.Tables[0].Rows[0]["DEGREENO"].ToString();
                this.ddlActivityType.SelectedValue = set.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                this.txtActivity.Text = set.Tables[0].Rows[0]["ACTIVITY"].ToString();
                this.txtGenaralSchedule.Text = set.Tables[0].Rows[0]["GENERAL_SCHEDULE"].ToString();
                this.txtFromDate.Text = set.Tables[0].Rows[0]["DATE_DUTRATION"].ToString();
                this.txtTodate.Text = set.Tables[0].Rows[0]["TO_DATE_DURATION"].ToString();
                this.objAttendanceEntity.CollegeCode = this.Session["colcode"].ToString();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        base.Response.Redirect(base.Request.Url.ToString());
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        this.Clear();
    }
    private void BindListLiew()
    {
        DataSet set = new DataSet();
        set = this.objAcademicController.GetAll_AcademicCalendarDetails(this.ddlActivityType.SelectedValue);
        if (set.Tables[0].Rows.Count <= 0)
        {
            this.lvAcademicCalander.DataSource = null;
            this.lvAcademicCalander.DataBind();
            this.lvAcademicCalander.Visible = false;
        }
        else
        {
            this.lvAcademicCalander.DataSource = set;
            this.lvAcademicCalander.DataBind();
            this.lvAcademicCalander.Visible = true;
            this.objCommon.SetListViewLabel("0", Convert.ToInt32(HttpContext.Current.Session["OrgId"]), Convert.ToInt32(this.Session["userno"]), this.lvAcademicCalander);
        }
    }

}