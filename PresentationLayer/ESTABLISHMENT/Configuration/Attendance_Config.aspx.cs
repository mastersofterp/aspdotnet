using System;
using System.Collections;
using System.Configuration;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_Attendance_Config : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    Shifts objShift = new Shifts();


    #region PageEvents
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            FillCollege();
            FillDropDown();
            GetCurrentConfig();

            //tblShift.Visible = false;
            //tbllvShift.Visible = false;
            GetTime();
        }
        //divMsg.InnerHtml = string.Empty;

    }
    #endregion

    #region Actions

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ODdays = Convert.ToInt32(txtODDays.Text);
        int ODAppdays = Convert.ToInt32(txtODAppDays.Text);
        int lwp_no = 0;
        int compl_no = 0;
        int medical_no = 0;
        lwp_no = Convert.ToInt32(ddlLWP.SelectedValue);
        compl_no = Convert.ToInt32(ddlComp.SelectedValue);
        medical_no = Convert.ToInt32(ddlMedical.SelectedValue);
        int noofshift = Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_REF", "NOOFSHIFT_AVAILABLE", ""));
        objAttandance.UpdateLeaveRef("1", "1", "1", "1", ODdays, ODAppdays, lwp_no, compl_no, medical_no);
        this.updateConfig();
        objCommon.DisplayMessage("Record Updated Successfully", this);
        MessageBox("Record Updated Successfully");
        //
    }
    #endregion

    #region Methods
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }


    }
    protected void FillDropDown()
    {
        //objCommon.FillDropDownList(ddlLeaveType, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "LNO>0", "LEAVE");
        //objCommon.FillDropDownList(ddlShift, "PAYroll_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "SRNO>0", "SHIFTNO");
        objCommon.FillDropDownList(ddlstaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "STNO>0 AND ISNULL(ACTIVESTATUS,0) =" + 1, "STNO");
        objCommon.FillDropDownList(ddlLWP, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlComp, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
        objCommon.FillDropDownList(ddlMedical, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");

    }


    //Reload The Page
    protected void Clear()
    {
        Response.Redirect("~/ESTABLISHMENT/Configuration/Attendance_Config.aspx");
    }
    //Get Current Configuration
    protected void GetCurrentConfig()
    {
        DataSet ds;
        ds = objAttandance.GetCurrentConfig();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtODDays.Text = ds.Tables[0].Rows[0]["OD_DAYS"].ToString();
            txtODAppDays.Text = ds.Tables[0].Rows[0]["OD_DAYS_APP"].ToString();
            ddlLWP.SelectedValue = ds.Tables[0].Rows[0]["LWP_NO"].ToString();
            ddlComp.SelectedValue = ds.Tables[0].Rows[0]["COMP_OFF"].ToString();
            ddlMedical.SelectedValue = ds.Tables[0].Rows[0]["ML_HPL"].ToString();
        }


        BindListViewStatus();
        BindListView();
        //foreach (ListViewDataItem lvitem in lvStatus.Items)
        //{
        //    DropDownList ddllv = lvitem.FindControl("ddlLvType") as DropDownList;
        //    HiddenField hidlv = lvitem.FindControl("hidLvType") as HiddenField;
        //    ddllv.SelectedValue = hidlv.Value; 
        //}
    }
    //Bind The List of Shifts Available
    protected void BindListView()
    {
        DataSet ds;
        ds = objAttandance.GetShiftList();
        //lvShift.DataSource = ds;
        //lvShift.DataBind();
    }

    protected void BindListViewStatus()
    {
        DataSet ds = null;
        ds = objAttandance.GetStatus(Convert.ToInt32(ddlstaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
       // ds = objAttandance.GetStatus(Convert.ToInt32(ddlstaff.SelectedValue));
        lvStatus.DataSource = ds;
        lvStatus.DataBind();
        //fillLeavetype();
    }

    protected void GetTime()
    {
        //DataSet dstime = objCommon.FillDropDown("PAYROLL_CONFIG_TIMING_ENTRY","ISNULL(FROM_TIME,0) AS FROM_TIME","ISNULL(TO_TIME,0) AS TO_TIME","STATUSNO="+ddlCondn.SelectedValue,"");
        //string intime = dstime.Tables[0].Rows[0]["FROM_TIME"].ToString();
        //string outtime = dstime.Tables[0].Rows[0]["TO_TIME"].ToString();
        //txtFrom.Text = intime;
        //txtTo.Text = outtime;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }

    #endregion


    protected void ddlCondn_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTime();
    }


    protected void btnAddAnotherShift_Click(object sender, EventArgs e)
    {
        //tbllvShift.Visible = true;
        //tblShift.Visible = true;
        Response.Redirect("~/ESTABLISHMENT/LEAVES/Master/Shift_Config.aspx");
    }

    private void fillLeavetype()
    {
        foreach (ListViewDataItem lvitem in lvStatus.Items)
        {
            //DropDownList ddllv = lvitem .FindControl ("ddlLvType") as DropDownList;
            //objCommon.FillDropDownList(ddllv, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "LNO>0 and STNO="+ddlstaff.SelectedValue, "LEAVE");

            Label lblstatusno = lvitem.FindControl("lblStatusno") as Label;
            Label lblCreded = lvitem.FindControl("lblCrDed") as Label;


            if (lblstatusno.ToolTip == "2" || lblstatusno.ToolTip == "8" || lblstatusno.ToolTip == "11")
            {
                lblCreded.Text = "(Deduct Leave)";
            }
            else if (lblstatusno.ToolTip == "9" || lblstatusno.ToolTip == "10")
            {
                lblCreded.Text = "(In Hr)  (Credit Leave)";
            }

        }
    }

    private void updateConfig()
    {
        double allowed_up_to = 0;
        foreach (ListViewDataItem lvitem in lvStatus.Items)
        {
            Label lblstatusno = lvitem.FindControl("lblStatusno") as Label;
            TextBox txtFrom = lvitem.FindControl("txtFrom") as TextBox;
            TextBox txtTo = lvitem.FindControl("txtTo") as TextBox;
            TextBox txtNoLeave = lvitem.FindControl("txtNoOfLeaves") as TextBox;
            TextBox txtAllowedUpTo = lvitem.FindControl("txtAllowedUpTo") as TextBox;

            DropDownList ddllv = lvitem.FindControl("ddlLvType") as DropDownList;
            if (txtAllowedUpTo.Text.Trim() != string.Empty)
            {
                allowed_up_to = Convert.ToDouble(txtAllowedUpTo.Text);
            }
            else
            {
                allowed_up_to = 0;
            }
            //objAttandance.UpdateConfigTimingEntry(txtFrom.Text, txtTo.Text, Convert.ToDouble(txtNoLeave.Text), Convert.ToInt32(lblstatusno.ToolTip), allowed_up_to);
            objAttandance.UpdateConfigTimingEntry(txtFrom.Text, txtTo.Text, Convert.ToDouble(txtNoLeave.Text), Convert.ToInt32(lblstatusno.ToolTip), allowed_up_to);

        }
    }
    protected void ddlstaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewStatus();
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListViewStatus();

    }
    protected void btnRestrict_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ESTABLISHMENT/LEAVES/Master/Restrict_LeaveTaken.aspx");
    }
    protected void btnShiftReport_Click(object sender, System.EventArgs e)
    {
        ShowReport("Shift Master Report", "ESTB_ShiftMaster.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,SHIFT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "," + "@username=" + Session["userfullname"].ToString();
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@P_COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
