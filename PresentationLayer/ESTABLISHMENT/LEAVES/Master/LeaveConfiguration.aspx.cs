//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:07-09-2015
//PURPOSE: FOR LEAVE CONFIGURATION
//==============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_LeaveConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    LeavesController objLM = new LeavesController();
    Shifts objShift = new Shifts();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //FillDropDown();
                fillleave();                
                GetCurrentConfig();
                

               
                
                
            }

        }
    }
    //protected void FillDropDown()
    //{
    //    //objCommon.FillDropDownList(ddlLeaveType, "PAYROLL_LEAVE", "LNO", "LEAVENAME", "LNO>0", "LEAVE");
        
    //    //objCommon.FillDropDownList(ddlCasual, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
    //    //objCommon.FillDropDownList(ddlEarned, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
    //    //objCommon.FillDropDownList(ddlMedical, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
    //    //objCommon.FillDropDownList(ddlMaternity, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
    //    //objCommon.FillDropDownList(ddlPaternity, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");
    //    //objCommon.FillDropDownList(ddlLWP, "PAYROLL_LEAVE_NAME", "LVNO", "LEAVE_NAME", "LVNO>0", "LEAVE_NAME");

    //}
    protected void GetCurrentConfig()
    {
        
        DataSet ds;
        //ds = objCommon.FillDropDown("PAYROLL_LEAVE_REF", "isnull(OD_DAYS,0) AS OD_DAYS,isnull(OD_DAYS_APP,0) AS OD_DAYS_APP,isnull(COMPOFF_MIN_HR_FULLDAY,'') AS COMPOFF_MIN_HR_FULLDAY,isnull(COMPOFF_VALID_MONTH,0) AS COMPOFF_VALID_MONTH,isnull(NOTIFICATION_DAYS_FOR_LEAVE_APPROVAL_VALIDDATE,0)as NOTIFICATION_DAYS_FOR_LEAVE_APPROVAL_VALIDDATE,isnull(IsAutoApproved,0) AS IsAutoApproved", "isnull(isSMS,0) AS isSMS,isnull(IsEmail,0) AS IsEmail,isnull(IsLeaveWisePassingPath,0) AS IsLeaveWisePassingPath,isnull(DIRECTORMAILID,'')as DIRECTORMAILID,isnull(DAYS_FOR_AUTO_LEAVE_APPROVAL,0) as DAYS_FOR_AUTO_LEAVE_APPROVAL", "", "");
        ds = objLM.GetConfigurationDetail();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtODDays.Text = ds.Tables[0].Rows[0]["OD_DAYS"].ToString();
            txtODAppDays.Text = ds.Tables[0].Rows[0]["OD_DAYS_APP"].ToString();
            txtcomoffvalidmonth.Text = ds.Tables[0].Rows[0]["COMPOFF_VALID_MONTH"].ToString();
            txtminhoursfull.Text = Convert.ToString(ds.Tables[0].Rows[0]["COMPOFF_MIN_HR_FULLDAY"]);
            //txthourshalfday.Text = Convert.ToString(ds.Tables[0].Rows[0]["COMPOFF_MIN_HR_HALFDAY"]);
            Boolean ISMAIL = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEmail"]);
            Boolean isSMS = Convert.ToBoolean(ds.Tables[0].Rows[0]["isSMS"]);
            Boolean IsLeaveWisePassingPath = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLeaveWisePassingPath"]);
            txtmailid.Text = ds.Tables[0].Rows[0]["DIRECTORMAILID"].ToString();  
            Boolean ISAUTOAPPROVE = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAutoApproved"]);

            if (ISMAIL == true)
            {
                chkIsMAIL.Checked = true;
            }
            else
            {
                chkIsMAIL.Checked = false;
            }
            if (isSMS == true)
            {
                chkIsSMS.Checked = true;
            }
            else
            {
                chkIsSMS.Checked = false;
            }
            if (IsLeaveWisePassingPath == true)
            {
                chkLeavePath.Checked = true;
            }
            else
            {
                chkLeavePath.Checked = false;
            }

            txtLeaveValid.Text = ds.Tables[0].Rows[0]["DAYS_FOR_AUTO_LEAVE_APPROVAL"].ToString();

            txtnotify.Text = ds.Tables[0].Rows[0]["NOTIFICATION_DAYS_FOR_LEAVE_APPROVAL_VALIDDATE"].ToString();
            //ddlLWP.SelectedValue = ds.Tables[0].Rows[0]["LWP_NO"].ToString();

            if (ISAUTOAPPROVE == true)
            {
                chkAuto.Checked = true;
                divLeaveValid.Visible = true;
                divNotification.Visible = true;
            }
            else
            {
                chkAuto.Checked = false;
                divLeaveValid.Visible = false;
                divNotification.Visible = false;
            }
            Boolean IsApprovalOnDirectLeave = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalOnDirectLeave"]);
            if (IsApprovalOnDirectLeave == true)
            {
                chkDirect.Checked = true;
            }
            else
            {
                chkDirect.Checked = false;
            }
            ddlworkingday.SelectedValue = Convert.ToInt32(ds.Tables[0].Rows[0]["DayNumber"]).ToString();

            Boolean IsEmployeewiseSaturday = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEmployeeWiseSatWorking"]);
            if (IsEmployeewiseSaturday == true)
            {
                chkEmployeewiseSaturday.Checked = true;
            }
            else
            {
                chkEmployeewiseSaturday.Checked = false;
            }
            Boolean IsRequiredDocumentforOD = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsRequiredDocumentforOD"]);
            if (IsRequiredDocumentforOD)
            {
                CkhOdDocument.Checked = true;
            }
            else
            {
                CkhOdDocument.Checked = false;
            }
            //Added By Piyush Thakre on 28-02-2023
            txtAllowLate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Allow_Late"]);
            txtAllowEarly.Text = Convert.ToString(ds.Tables[0].Rows[0]["Allow_Early"]);
            txtpermissionvalid.Text = ds.Tables[0].Rows[0]["PERMISSIONINMONTH"].ToString();

            Boolean IsShowLWP = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsShowLWP"]);
            if (IsShowLWP)
            {
                ChkshowLwpLeave.Checked = true;
               
            }
            else
            {
                ChkshowLwpLeave.Checked = false;
                
            }

            Boolean IsChargeMail = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsChargeHandedMail"]);
            if (IsChargeMail)
            {
                chkChargeMail.Checked = true;
            }
            else
            {
                chkChargeMail.Checked = false;
            }

            Boolean IsValidatedLeaveComb = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsValidatedLeaveComb"]);
            if (IsValidatedLeaveComb)
            {
                ChkValidateLeaveComb.Checked = true;
            }
            else
            {
                ChkValidateLeaveComb.Checked = false;
            }
            // Added By Piyush Thakre on 20-09-2023
            Boolean IsNotAllowLeaveinCont = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsNotAllowLeaveinCont"]);
            if (IsNotAllowLeaveinCont)
            {
                chkLeaveincont.Checked = true;
            }
            else
            {
                chkLeaveincont.Checked = false;
            }

            // Added By Shrikant Bharne on 10/04/2023 
            Boolean ISLWPNOTALLOW = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISLWPNOTALLOW"]);
            if (ISLWPNOTALLOW)
            {
                chkLwpstop.Checked = true;
                divleavename.Visible = true;

                if (ds.Tables[0].Rows.Count > 0 && ds != null && ds.Tables.Count > 0)
                {
                    // domainCount = 1;                   
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string[] Leavevalue = ds.Tables[0].Rows[i]["Leavevalue"].ToString().Split(',');

                        // string val = ds.Tables[0].Rows[i]["AL_ASNO"].ToString();
                        for (int j = 0; j < Leavevalue.Length; j++)
                        {
                            foreach (ListItem item in ddlLeave.Items)
                            {
                                if (item.Value == Leavevalue[j])
                                {
                                    item.Selected = true;
                                    item.Enabled = true;
                                }
                            }
                        }
                    }

                }

            }
            else
            {
                chkLwpstop.Checked = false;
                ddlLeave.Items.Clear();
                divleavename.Visible = false;
            }

            // End


        }      
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Leaves objLeaves = new Leaves();

            if (chkIsSMS.Checked == true)
            {
                objLeaves.isSMS = true;
            }
            else
            {
                objLeaves.isSMS = false;
            }
            if (chkIsMAIL.Checked == true)
            {
                objLeaves.isEmail = true;
            }
            else
            {
                objLeaves.isEmail = false;
            }

            if (txtODDays.Text != string.Empty)
            {
                objLeaves.ODDAYS = Convert.ToInt32(txtODDays.Text);
            }
            else
            {
                objLeaves.ODDAYS = 0;
            }
            if (txtODAppDays.Text != string.Empty)
            {

                objLeaves.odApp = Convert.ToInt32(txtODAppDays.Text);
            }
            else
            {
                objLeaves.odApp = 0;
            }
            if (txtcomoffvalidmonth.Text != string.Empty)
            {
                objLeaves.ComoffvalidMonth = Convert.ToInt32(txtcomoffvalidmonth.Text);
            }
            else
            {
                objLeaves.ComoffvalidMonth = 0;
            }
            objLeaves.FULLDAYCOMOFF = txtminhoursfull.Text == string.Empty ? string.Empty : txtminhoursfull.Text;
      

            if (chkLeavePath.Checked == true)
            {
                objLeaves.IsLeaveWisePath = true;
            }
            else
            {
                objLeaves.IsLeaveWisePath = false;
            }

            if (txtmailid.Text != string.Empty)
            {
                objLeaves.DIRECTORMAILID = txtmailid.Text;
            }
            else
            {
                objLeaves.DIRECTORMAILID = null;
            }

            //if (txtLeaveValid.Text != string.Empty)
            //{
            //    objLeaves.LEAVEAPPROVALVALIDDAYS = Convert.ToInt32(txtLeaveValid.Text);
            //}
            //else
            //{
            //    objLeaves.LEAVEAPPROVALVALIDDAYS = 0;
            //}

            //if (txtnotify.Text != string.Empty)
            //{
            //    objLeaves.NOTIFICATIONDAYS = Convert.ToInt32(txtnotify.Text);
            //}
            //else
            //{
            //    objLeaves.NOTIFICATIONDAYS = 0;
            //}

            if (chkAuto.Checked == true)
            {
                objLeaves.IsAutoApprove = true;
                objLeaves.LEAVEAPPROVALVALIDDAYS = Convert.ToInt32(txtLeaveValid.Text);
                objLeaves.NOTIFICATIONDAYS = Convert.ToInt32(txtnotify.Text);
            }
            else
            {
                objLeaves.IsAutoApprove = false;
                objLeaves.LEAVEAPPROVALVALIDDAYS = 0;
                objLeaves.NOTIFICATIONDAYS = 0;
            }

            if (chkDirect.Checked == true)
            {
                objLeaves.IsApprovalOnDirectLeave = true;
            }
            else
            {
                objLeaves.IsApprovalOnDirectLeave = false;
            }

            objLeaves.Daynumber = Convert.ToInt32(ddlworkingday.SelectedValue);

            if (chkEmployeewiseSaturday.Checked == true)
            {
                objLeaves.IsEmployeewiseSaturday = true;
            }
            else
            {
                objLeaves.IsEmployeewiseSaturday = false;
            }

            if (CkhOdDocument.Checked)
            {
                objLeaves.IsRequiredDocumentforOD = true;
            }
            else
            {
                objLeaves.IsRequiredDocumentforOD = false;
            }
            //if (txtAllowLate.Text != string.Empty)
            //{
            //    objLeaves.AllowLate = Convert.ToDateTime(txtAllowLate.Text.Trim());
            //}
            //else
            //{
            //    objLeaves.AllowLate = Convert.ToDateTime(null);
            //}
            //if (txtAllowEarly.Text != string.Empty)
            //{
            //    objLeaves.AllowEarly = Convert.ToDateTime(txtAllowEarly.Text.Trim());
            //}
            //else
            //{
            //    objLeaves.AllowEarly = Convert.ToDateTime(null);
            //}
            objLeaves.AllowLate = txtAllowLate.Text == string.Empty ? string.Empty : txtAllowLate.Text;
            objLeaves.AllowEarly = txtAllowEarly.Text == string.Empty ? string.Empty : txtAllowEarly.Text;

            if (txtpermissionvalid.Text != string.Empty)
            {
                objLeaves.PERMISSIONINMONTH = Convert.ToInt32(txtpermissionvalid.Text);
            }
            else
            {
                objLeaves.PERMISSIONINMONTH = 0;
            }

            if (chkChargeMail.Checked)
            {
                objLeaves.IsChargeMail = true;
            }
            else
            {
                objLeaves.IsChargeMail = false;
            }

            if (ChkValidateLeaveComb.Checked)
            {
                objLeaves.IsValidatedLeaveComb = true;
            }
            else
            {
                objLeaves.IsValidatedLeaveComb = false;
            }

            if (chkLeaveincont.Checked)
            {
                objLeaves.IsNotAllowLeaveinCont = true;
            }
            else
            {
                objLeaves.IsNotAllowLeaveinCont = false;
            }

            if (ChkshowLwpLeave.Checked)
            {
                objLeaves.IsShowLWP = true;
            }
            else
            {
                objLeaves.IsShowLWP = false;
            }

            // Added By Shrikant Bharne on 10-04-2023
            if (chkLwpstop.Checked)
            {
                objLeaves.ISLWPNOTALLOW = true;
            }
            else
            {
                objLeaves.ISLWPNOTALLOW = false;
            }
            string Leavevalue = "";
            // string Leaveno ="";
            if (Convert.ToBoolean(objLeaves.ISLWPNOTALLOW))
            {
                for (int i = 0; i < ddlLeave.Items.Count; i++)
                {
                    if (ddlLeave.Items[i].Selected)
                    {
                        Leavevalue += ddlLeave.Items[i].Value + ",";
                        //Leavevalue = Leavevalue + "," + chkleave.Items[i].Value;                   
                    }
                }
                Leavevalue = Leavevalue.TrimEnd(',');
            }
            else
            {
                Leavevalue = string.Empty;
            }

            if (Convert.ToBoolean(objLeaves.ISLWPNOTALLOW) && Leavevalue == string.Empty)
            {
                MessageBox("Please Select Leave");                
                return;
            }
            //------------------------------------------------------------------//

            CustomStatus cs = (CustomStatus)objLM.AddUpdateLeaveConfiguration(objLeaves, Leavevalue);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Record Updated Successfully", this);
                 //Clear();
                GetCurrentConfig();

            }
       

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
       
    }
   
    private void ShowDetails()
    {
        DataSet ds = null;
        try
        {
           

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        chkIsSMS.Checked = false;
        chkIsMAIL.Checked = false;
        txtODDays.Text = string.Empty;
        txtODAppDays.Text = string.Empty;
        txtcomoffvalidmonth.Text = string.Empty;
        txtminhoursfull.Text = string.Empty;
        ddlworkingday.SelectedIndex = 0;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }



    protected void chkAuto_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAuto.Checked == true)
        {
            divLeaveValid.Visible = true;
            divNotification.Visible = true;
        }
        else
        {
            divLeaveValid.Visible = false;
            divNotification.Visible = false;
        }
    }
    protected void chkLwpstop_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkLwpstop.Checked)
            {
                divleavename.Visible = true;
                fillleave();

            }
            else
            {
                divleavename.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void fillleave()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Payroll_leave_name", "LVNO", "Leave_Name", "LVNO>0", "LVNO");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlLeave.DataTextField = "Leave_Name";
                    ddlLeave.DataValueField = "LVNO";
                    ddlLeave.DataSource = ds.Tables[0];
                    ddlLeave.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_PA_Path.Fill_Department ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

  
}
