//======================================================================================
// PROJECT NAME  : UIAMS
// MODULE NAME   : 
// PAGE NAME     : ShiftMaster.ASPX                                                    
// CREATION DATE : 
// CREATED BY    :                                           
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Text.RegularExpressions;


public partial class ESTABLISHMENT_LEAVES_Master_ShiftMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ShiftManagement objSME = new ShiftManagement();
    ShiftManagementController objSMC = new ShiftManagementController();
    string UsrStatus = string.Empty;

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               // CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                FillCollege();
                BindListView();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnback.Visible = false;

                pnlSelect.Visible = false;
                Panel1.Visible = true;

                btnAdd.Visible = true;
                btnReport.Visible = true;
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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            objSME.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
            objSME.SHIFTNAME = txtShiftName.Text.Trim();
            objSME.INTIME = txtShiftInTime.Text.Trim();
            objSME.OUTTIME = txtShiftOutTime.Text.Trim();
            objSME.INTIME_MID = txtShiftInTime_Mid.Text.Trim();
            objSME.OUTTIME_MID = txtShiftOutTime_Mid.Text.Trim();
            int count = 0;
            int shiftno = Convert.ToInt32(ViewState["lblSHIFTNO"]);
            objSME.IsNightShift = Convert.ToBoolean(chkNightShift.Checked);
            objSME.IsAllowCompOffLeave = Convert.ToBoolean(chkIsAllowCompOffLeave.Checked);
            objSME.IsDoubleDuty = Convert.ToBoolean(chkIsDoubleDuty.Checked);

            if (ViewState["lblSHIFTNO"] == null)
            {
                count = Convert.ToInt32(objCommon.LookUp("PAYROLL_SHIFTMASTER", "COUNT(1)", "SHIFTNAME='" + txtShiftName.Text.ToString().Trim() + "'"));
            }
            else
            {
                count = Convert.ToInt32(objCommon.LookUp("PAYROLL_SHIFTMASTER", "COUNT(1)", "SHIFTNAME='" + txtShiftName.Text.ToString().Trim() + "' AND SHIFTNO <>" + shiftno));
            }

            if (count == 0)
            {

                if (ViewState["lblSHIFTNO"] == null)
                {
                    objSME.SHIFTNO = 0;
                }
                else
                {
                    objSME.SHIFTNO = Convert.ToInt32(ViewState["lblSHIFTNO"]);
                }

                CustomStatus cs = (CustomStatus)objSMC.InsertUpdateShiftMaster(objSME);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    BindListView();
                    objCommon.DisplayMessage("Record Saved Successfully!", this.Page);
                    ViewState["lblSHIFTNO"] = null;
                    Clear();

                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    btnback.Visible = false;

                    btnAdd.Visible = true;
                    btnReport.Visible = true;

                    Panel1.Visible = true;
                    pnlSelect.Visible = false;
                }
                else
                    objCommon.DisplayMessage("Exception Occured!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Shift Name Already Exists!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_ShiftMaster.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //Get Details of Heads To be Edited on textbox
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblSHIFTNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblShiftName = lst.FindControl("lblShiftName") as Label;
            Label lblInTime = lst.FindControl("lblInTime") as Label;
            Label lblOutTime = lst.FindControl("lblOutTime") as Label;
            //lblNightShiftStatus
            Label lblNightShiftStatus = lst.FindControl("lblNightShiftStatus") as Label;
            Label lblIsDoubleDuty = lst.FindControl("lblIsDoubleDuty") as Label;
            if (lblIsDoubleDuty.Text == "Yes")
            {
                chkIsDoubleDuty.Checked = true;
            }
            else
            {
                chkIsDoubleDuty.Checked = false;
            }
            if (lblNightShiftStatus.Text == "Yes")
            {
                chkNightShift.Checked = true;
            }
            else
            {
                chkNightShift.Checked = false;
            }
            //lblAllowCompOffStatus
            Label lblAllowCompOffStatus = lst.FindControl("lblAllowCompOffStatus") as Label;
            if (lblAllowCompOffStatus.Text == "Yes")
            {
                chkIsAllowCompOffLeave.Checked = true;
            }
            else
            {
                chkIsAllowCompOffLeave.Checked = false;
            }
            //ddlCollege.SelectedValue = "1";

            //int college_no = 1;
            //HiddenField hdncollege_no = lst.FindControl("hdncollege_no") as HiddenField;
            //HiddenField hdncollege_no = lst.FindControl("hdncollege_no") as HiddenField;
            //if (hdncollege_no.Value == null)
            //{
            //    ddlCollege.SelectedValue = "1";
            //}
            //else
            //{
            //    ddlCollege.SelectedValue = hdncollege_no.Value.ToString();
            //}
            //HiddenField hdncollege_no =
            //ddlCollege.SelectedValue = hdncollege_no.Value.ToString();       
            //lblInTimeMid,lblOutTimeMid

            //HiddenField hdncollege_no = lst.FindControl("hdncollege_no") as HiddenField;

            //int collegeno = Convert.ToInt32(hdncollege_no.Value);
            //ddlCollege.SelectedValue = collegeno.ToString();

            HiddenField hdncollege_no = lst.FindControl("hdncollege_no") as HiddenField;
            ddlCollege.SelectedValue = hdncollege_no.Value.ToString();

            Label lblInTimeMid = lst.FindControl("lblInTimeMid") as Label;
            Label lblOutTimeMid = lst.FindControl("lblOutTimeMid") as Label;
            txtShiftInTime_Mid.Text = lblInTimeMid.Text.Trim();
            txtShiftOutTime_Mid.Text = lblOutTimeMid.Text.Trim();

            txtShiftName.Text = lblShiftName.Text.Trim();
            txtShiftInTime.Text = lblInTime.Text.Trim();
            txtShiftOutTime.Text = lblOutTime.Text.Trim();
            ViewState["action"] = "edit";

            pnlSelect.Visible = true;
            Panel1.Visible = false;

            btnAdd.Visible = false;
            btnReport.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnback.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    //Fetch Already Defined Shift Master From The Database
    private void BindListView()
    {
        try
        {
            DataSet ds = objSMC.GetShiftMasterDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvShiftMaster.DataSource = ds;
                lvShiftMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_ShiftMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //To clear Controls
    protected void Clear()
    {
        txtShiftName.Text = txtShiftOutTime_Mid.Text = txtShiftInTime_Mid.Text = string.Empty;
        txtShiftInTime.Text = string.Empty;
        txtShiftOutTime.Text = string.Empty;
        ViewState["lblSHIFTNO"] = null;
        chkNightShift.Checked = false;
        chkIsAllowCompOffLeave.Checked = chkIsDoubleDuty.Checked = false;
        ddlCollege.SelectedIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //btnReport_Click
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ShiftReport", "Estb_ShiftMasterReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("establishment")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,SHIFT," + rptFileName;

            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_CODE=0";
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["college_nos"].ToString();
            }

            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_leaves.ShowReport->" + ex.Message + ' ' + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void txtShiftInTime_TextChanged(object sender, EventArgs e)
    {
        //select FORMAT(CAST(DATEADD(MINUTE, datediff(MINUTE,cast(@intime as datetime) , cast(@outtime as datetime)) /2.00 , cast(@intime as datetime)) AS dateTIME),'hh:mm tt') AS 'SH_MIN_IN'
        //select format(CONVERT(DATETIME,DATEADD(MINUTE,10,CAST(DATEADD(MINUTE, datediff(MINUTE,cast(@intime as datetime) , cast(@outtime as datetime)) /2.00 , cast(@intime as datetime)) AS dateTIME))),'hh:mm tt') AS 'SH_MIN_OUT'
        try
        {
            string mid_in_time; string mid_out_time;
            string in_time; string out_time;
            if (txtShiftInTime.Text.ToString() != string.Empty && txtShiftInTime.Text.ToString() != "__:__:__ PM" && txtShiftInTime.Text.ToString() != "__:__:__ AM" && txtShiftOutTime.Text.ToString() != string.Empty && txtShiftOutTime.Text.ToString() != "__:__:__ PM" && txtShiftOutTime.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtShiftInTime.Text.ToString();
                out_time = txtShiftOutTime.Text.ToString();
                mid_in_time = objCommon.LookUp("payroll_leave_ref", "FORMAT(CAST(DATEADD(MINUTE, datediff(MINUTE,cast('" + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME),'hh:mm:ss tt')", "");
                mid_out_time = objCommon.LookUp("payroll_leave_ref", "format(CONVERT(DATETIME,DATEADD(MINUTE,10,CAST(DATEADD(MINUTE, datediff(MINUTE,cast('" + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME))),'hh:mm:ss tt')", "");
                txtShiftInTime_Mid.Text = mid_in_time;
                txtShiftOutTime_Mid.Text = mid_out_time;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_ShiftMaster.txtShiftInTime_TextChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void txtShiftOutTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string mid_in_time; string mid_out_time;
            string in_time; string out_time;
            if (txtShiftInTime.Text.ToString() != string.Empty && txtShiftInTime.Text.ToString() != "__:__:__ PM" && txtShiftInTime.Text.ToString() != "__:__:__ AM" && txtShiftOutTime.Text.ToString() != string.Empty && txtShiftOutTime.Text.ToString() != "__:__:__ PM" && txtShiftOutTime.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtShiftInTime.Text.ToString();
                out_time = txtShiftOutTime.Text.ToString();
                mid_in_time = objCommon.LookUp("payroll_leave_ref", "FORMAT(CAST(DATEADD(MINUTE, datediff(MINUTE,cast('" + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME),'hh:mm:ss tt')", "");
                mid_out_time = objCommon.LookUp("payroll_leave_ref", "format(CONVERT(DATETIME,DATEADD(MINUTE,10,CAST(DATEADD(MINUTE, datediff(MINUTE,cast('" + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME))),'hh:mm:ss tt')", "");
                txtShiftInTime_Mid.Text = mid_in_time;
                txtShiftOutTime_Mid.Text = mid_out_time;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_ShiftMaster.txtShiftOutTime_TextChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        //if (Session["username"].ToString() != "admin")
        if (Session["usertype"].ToString() != "1")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnAdd.Visible = false;
        btnReport.Visible = false;

        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnback.Visible = true;


        Panel1.Visible = false;
        pnlSelect.Visible = true;
        



    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        pnlSelect.Visible = false;

        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnback.Visible = false;

        btnAdd.Visible = true;
        btnReport.Visible = true;

        Clear();
       // FillCollege();
       // BindListView();
    }
}