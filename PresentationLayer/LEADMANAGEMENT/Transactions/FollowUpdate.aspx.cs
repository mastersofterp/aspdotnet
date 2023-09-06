using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using ClosedXML.Excel;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
public partial class LEADMANAGEMENT_Transactions_FollowUpdate : System.Web.UI.Page
{
    LMController objLMC = new LMController();
    Common objCommon = new Common();
    string name = string.Empty; string emailID = string.Empty; string mobile = string.Empty; int complete = 0; DateTime follow_Date; int user = 0; CustomStatus cs = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                GetToDayListVIew();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                
                //GetUpcomingDate();
                //GetOverdueDate();
            }
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FollowUpdate.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FollowUpdate.aspx");
        }
    }
    protected void GetToDayListVIew()
    {
        DataSet dsTodaysLead = objLMC.Get_Todays_Lead(Convert.ToInt32(Session["userno"]));
        if (dsTodaysLead.Tables[0].Rows.Count > 0)
        {
            lvTodayFollow.DataSource = dsTodaysLead;
            lvTodayFollow.DataBind();
            lvTodayFollow.Visible = true;
            btnSubmit.Visible = true;
            //btnSendEmail.Visible = true;
        }
        else
        {
            lvTodayFollow.DataSource = dsTodaysLead;
            lvTodayFollow.DataBind();
            lvTodayFollow.Visible = true;
            btnSubmit.Visible = false;
            //btnSendEmail.Visible = false;
        }
    }
    protected void GetUpcomingDate()
    {
        DataSet dsUpcoming = objLMC.GetUpcomingDates(Convert.ToInt32(Session["userno"]));
        if (dsUpcoming.Tables[0].Rows.Count > 0)
        {
            lvUpcoming.DataSource = dsUpcoming;
            lvUpcoming.DataBind();
            //lvUpcoming.Visible = true;
            btnSubmit_Upcoming.Visible = true;
            //btnSendEmail_Upcoming.Visible = true;
        }
        else
        {
            lvUpcoming.DataSource = dsUpcoming;
            lvUpcoming.DataBind();
            //lvUpcoming.Visible = true;
            btnSubmit_Upcoming.Visible = false;
            //btnSendEmail_Upcoming.Visible = false;
        }
    }
    protected void GetOverdueDate()
    {
        DataSet dsOverdue = objLMC.GetOverdueDates(Convert.ToInt32(Session["userno"]));
        if (dsOverdue.Tables[0].Rows.Count > 0)
        {
            lvOverdue.DataSource = dsOverdue;
            lvOverdue.DataBind();
            lvOverdue.Visible = true;
            btnSubmit_Overdue.Visible = true;
            //btnSendEmail_Overdue.Visible = true;
        }
        else
        {
            lvOverdue.DataSource = dsOverdue;
            lvOverdue.DataBind();
            lvOverdue.Visible = true;
            btnSubmit_Overdue.Visible = false;
            //btnSendEmail_Overdue.Visible = false;
        }
    }
    protected void GetCompleteDate()
    {
        DataSet dsComplete = objLMC.GetCompleteDates(Convert.ToInt32(Session["userno"]));
        if (dsComplete.Tables[0].Rows.Count > 0)
        {
            lvComplete.DataSource = dsComplete;
            lvComplete.DataBind();
            lvComplete.Visible = true;
            btnSubmit_Complete.Visible = true;
            //btnSendEmail_Complete.Visible = true;
        }
        else
        {
            lvComplete.DataSource = dsComplete;
            lvComplete.DataBind();
            lvComplete.Visible = true;
            btnSubmit_Complete.Visible = false;
            //btnSendEmail_Complete.Visible = false;
        }
    }
    protected void GetAllDate()
    {
        DataSet dsAll = objLMC.GetAllDates(Convert.ToInt32(Session["userno"]));
        if (dsAll.Tables[0].Rows.Count > 0)
        {
            lvAll.DataSource = dsAll;
            lvAll.DataBind();
            lvAll.Visible = true;
            //btnSubmit_All.Visible = true;
            //btnSendEmail_All.Visible = true;
        }
        else
        {
            lvAll.DataSource = dsAll;
            lvAll.DataBind();
            lvAll.Visible = true;
            //btnSubmit_All.Visible = false;
            //btnSendEmail_All.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            foreach (ListViewDataItem lv in lvTodayFollow.Items)
            {
                CheckBox chkBox = lv.FindControl("chkCheck") as CheckBox;
                Label lblName = lv.FindControl("lblName_Today") as Label;
                Label lblEmail = lv.FindControl("lblEmail_Today") as Label;
                Label lblMobile = lv.FindControl("lblMobile_Today") as Label;
                Label lblRegDate = lv.FindControl("lblRegDate_Today") as Label;
                Label lblNextDate = lv.FindControl("lblNextDate_Today") as Label;
                HiddenField hdnUser = lv.FindControl("hdnUserNo") as HiddenField;
                if (chkBox.Checked)
                {
                    complete = 1;
                }
                else
                {
                    complete = 0;
                }
                name = lblName.Text.ToString();
                emailID = lblEmail.Text.ToString();
                mobile = lblMobile.Text.ToString();
                follow_Date = lblNextDate.Text.ToString().Equals(string.Empty) ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(lblNextDate.Text.ToString());

                //follow_Date = Convert.ToDateTime(lblNextDate.Text.ToString());
                user =Convert.ToInt32(hdnUser.Value);
                cs=(CustomStatus) objLMC.Add_Complete_Follow_Update(user, Convert.ToInt32(Session["userno"]), name, emailID, mobile, follow_Date, complete);
                
            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                GetToDayListVIew();
                objCommon.DisplayMessage(this.UpdToday, "Marked As Completed Successfully.", this.Page);                
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkFollowDate_Click(object sender, EventArgs e)
    {
        try
        {
            liToday.Attributes.Add("class", "active");
            liUpcoming.Attributes.Remove("class");
            liOverdue.Attributes.Remove("class");
            liComplete.Attributes.Remove("class");
            liAll.Attributes.Remove("class");
            GetToDayListVIew();

            todaydate.Visible = true;
            upcomindate.Visible = false;
            overduedate.Visible = false;
            completedate.Visible = false;
            alldate.Visible = false;

            lvUpcoming.DataSource = null;
            lvUpcoming.DataBind();

            lvOverdue.DataSource = null;
            lvOverdue.DataBind();

            lvComplete.DataSource = null;
            lvComplete.DataBind();

            lvAll.DataSource = null;
            lvAll.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkUpcoming_Click(object sender, EventArgs e)
    {
        try
        {
            liToday.Attributes.Remove("class");
            liOverdue.Attributes.Remove("class");
            liComplete.Attributes.Remove("class");
            liAll.Attributes.Remove("class");
            liUpcoming.Attributes.Add("class", "active");
            
            upcomindate.Visible = true;
            todaydate.Visible = false;
            overduedate.Visible = false;
            completedate.Visible = false;
            alldate.Visible = false;
            GetUpcomingDate();
          
            lvTodayFollow.DataSource = null;
            lvTodayFollow.DataBind();
            lvTodayFollow.Visible = false;
            lvOverdue.DataSource = null;
            lvOverdue.DataBind();
            lvOverdue.Visible = false;
            lvComplete.DataSource = null;
            lvComplete.DataBind();
            lvComplete.Visible = false;
            lvAll.DataSource = null;
            lvAll.DataBind();
            lvAll.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkOverdue_Click(object sender, EventArgs e)
    {
        try
        {
            liOverdue.Attributes.Add("class", "active");
            liToday.Attributes.Remove("class");
            liUpcoming.Attributes.Remove("class");
            liComplete.Attributes.Remove("class");
            liAll.Attributes.Remove("class");

            GetOverdueDate();
            upcomindate.Visible = false;
            todaydate.Visible = false;
            overduedate.Visible = true;
            completedate.Visible = false;
            alldate.Visible = false;

            lvTodayFollow.DataSource = null;
            lvTodayFollow.DataBind();

            lvUpcoming.DataSource = null;
            lvUpcoming.DataBind();

            lvComplete.DataSource = null;
            lvComplete.DataBind();

            lvAll.DataSource = null;
            lvAll.DataBind();
            
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkComplete_Click(object sender, EventArgs e)
    {
        try
        {
            liComplete.Attributes.Add("class", "active");
            liToday.Attributes.Remove("class");
            liUpcoming.Attributes.Remove("class");
            liOverdue.Attributes.Remove("class");
            liAll.Attributes.Remove("class");
            GetCompleteDate();
            upcomindate.Visible = false;
            todaydate.Visible = false;
            overduedate.Visible = false;
            completedate.Visible = true;
            alldate.Visible = false;

            lvTodayFollow.DataSource = null;
            lvTodayFollow.DataBind();

            lvUpcoming.DataSource = null;
            lvUpcoming.DataBind();

            lvOverdue.DataSource = null;
            lvOverdue.DataBind();

            lvAll.DataSource = null;
            lvAll.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkAll_Click(object sender, EventArgs e)
    {
        try
        {
            liAll.Attributes.Add("class", "active");
            liToday.Attributes.Remove("class");
            liUpcoming.Attributes.Remove("class");
            liOverdue.Attributes.Remove("class");
            liComplete.Attributes.Remove("class");
            GetAllDate();
            upcomindate.Visible = false;
            todaydate.Visible = false;
            overduedate.Visible = false;
            completedate.Visible = false;
            alldate.Visible = true;

            lvTodayFollow.DataSource = null;
            lvTodayFollow.DataBind();

            lvUpcoming.DataSource = null;
            lvUpcoming.DataBind();

            lvOverdue.DataSource = null;
            lvOverdue.DataBind();

            lvComplete.DataSource = null;
            lvComplete.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Upcoming_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lv in lvUpcoming.Items)
            {
                CheckBox chkBox = lv.FindControl("chkCheck_Upcoming") as CheckBox;
                Label lblName = lv.FindControl("lblName_Upcoming") as Label;
                Label lblEmail = lv.FindControl("lblEmail_Upcoming") as Label;
                Label lblMobile = lv.FindControl("lblMobile_Upcoming") as Label;
                Label lblRegDate = lv.FindControl("lblRegDate_Upcoming") as Label;
                Label lblNextDate = lv.FindControl("lblNextDate_Upcoming") as Label;
                HiddenField hdnUser = lv.FindControl("hdnUserNo_Upcoming") as HiddenField;
                if (chkBox.Checked)
                {
                    complete = 1;
                }
                else
                {
                    complete = 0;
                }
                name = lblName.Text.ToString();
                emailID = lblEmail.Text.ToString();
                mobile = lblMobile.Text.ToString();
                follow_Date = lblNextDate.Text.ToString().Equals(string.Empty) ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(lblNextDate.Text.ToString());

                //follow_Date = Convert.ToDateTime(lblNextDate.Text.ToString());
                user = Convert.ToInt32(hdnUser.Value);
                cs = (CustomStatus)objLMC.Add_Complete_Follow_Update(user, Convert.ToInt32(Session["userno"]), name, emailID, mobile, follow_Date, complete);

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                GetUpcomingDate();
                objCommon.DisplayMessage(this.UpdUpcoming, "Marked As Completed Successfully.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSendEmail_Upcoming_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
 
        }
    }
    protected void btnSubmit_Overdue_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lv in lvOverdue.Items)
            {
                CheckBox chkBox = lv.FindControl("chkCheck_Overdue") as CheckBox;
                Label lblName = lv.FindControl("lblName_Overdue") as Label;
                Label lblEmail = lv.FindControl("lblEmail_Overdue") as Label;
                Label lblMobile = lv.FindControl("lblMobile_Overdue") as Label;
                Label lblRegDate = lv.FindControl("lblRegDate_Overdue") as Label;
                Label lblNextDate = lv.FindControl("lblNextDate_Overdue") as Label;
                HiddenField hdnUser = lv.FindControl("hdnUserNo_Overdue") as HiddenField;
                if (chkBox.Checked)
                {
                    complete = 1;
                }
                else
                {
                    complete = 0;
                }
                name = lblName.Text.ToString().Equals(string.Empty) ? "NA" : lblName.Text.ToString();
                emailID = lblEmail.Text.ToString().Equals(string.Empty) ? "NA" : lblEmail.Text.ToString();
                mobile = lblMobile.Text.ToString().Equals(string.Empty) ? "NA" : lblMobile.Text.ToString();
                follow_Date =lblNextDate.Text.ToString().Equals(string.Empty)?Convert.ToDateTime("01/01/1753") :Convert.ToDateTime(lblNextDate.Text.ToString());
                user = Convert.ToInt32(hdnUser.Value);
                cs = (CustomStatus)objLMC.Add_Complete_Follow_Update(user, Convert.ToInt32(Session["userno"]), name, emailID, mobile, follow_Date, complete);

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                GetOverdueDate();
                objCommon.DisplayMessage(this.UpdOverdue, "Marked As Completed Successfully.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSendEmail_Overdue_Click(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Complete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewDataItem lv in lvComplete.Items)
            {
                CheckBox chkBox = lv.FindControl("chkCheck_Complete") as CheckBox;
                Label lblName = lv.FindControl("lblName_Comp") as Label;
                Label lblEmail = lv.FindControl("lblEmail_Comp") as Label;
                Label lblMobile = lv.FindControl("lblMobile_Comp") as Label;
                Label lblRegDate = lv.FindControl("lblRegDate_Comp") as Label;
                Label lblNextDate = lv.FindControl("lblNextDate_Comp") as Label;
                HiddenField hdnUser = lv.FindControl("hdnUserNo_Comp") as HiddenField;
                if (chkBox.Checked)
                {
                    complete = 1;
                }
                else
                {
                    complete = 0;
                }
                name = lblName.Text.ToString();
                emailID = lblEmail.Text.ToString();
                mobile = lblMobile.Text.ToString();
                follow_Date = lblNextDate.Text.ToString().Equals(string.Empty) ? Convert.ToDateTime("01/01/1753") : Convert.ToDateTime(lblNextDate.Text.ToString());

                // follow_Date = Convert.ToDateTime(lblNextDate.Text.ToString());
                user = Convert.ToInt32(hdnUser.Value);
                cs = (CustomStatus)objLMC.Add_Complete_Follow_Update(user, Convert.ToInt32(Session["userno"]), name, emailID, mobile, follow_Date, complete);

            }
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                GetCompleteDate();
                objCommon.DisplayMessage(this.UpdComplete, "Marked As Completed Successfully.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSendEmail_Complete_Click(object sender, EventArgs e)
    {

    }
    protected void btnSendEmail_All_Click(object sender, EventArgs e)
    {

    }
}