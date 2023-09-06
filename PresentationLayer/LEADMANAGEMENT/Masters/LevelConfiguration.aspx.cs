using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.IO;

public partial class LEADMANAGEMENT_Masters_LevelConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LevelConfigurationEntity objLCEntity = new LevelConfigurationEntity();
    LevelconfigurationController objLCController = new LevelconfigurationController();


    // Form Event
    #region Form Event

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
        try
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
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    DropDown();
                    RequiredFieldValidator1.Visible = false;
                    RequiredFieldValidator2.Visible = false;
                    RequiredFieldValidator3.Visible = false;
                    RequiredFieldValidator4.Visible = false;
                    RequiredFieldValidator5.Visible = false;
                }

            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSequenceNo.SelectedIndex = 0;
            txtReminderDay.Text = string.Empty;
            btnSave.Visible = true;

            collapse1.Attributes.Add("class", "panel-collapse collapse");
            collapse2.Attributes.Add("class", "panel-collapse collapse");
            collapse3.Attributes.Add("class", "panel-collapse collapse");
            collapse4.Attributes.Add("class", "panel-collapse collapse");
            collapse5.Attributes.Add("class", "panel-collapse collapse");
            ddlUserNameLevel1.Items.Clear();
            ddlUserNameLevel1.Items.Add("Load...");
            ddlUserNameLevel1.SelectedItem.Value = "0";
            txtReminderDayLevel1.Text = string.Empty;

            ddlUserNameLevel2.Items.Clear();
            ddlUserNameLevel2.Items.Add("Load...");
            ddlUserNameLevel2.SelectedItem.Value = "0";
            txtReminderDayLevel2.Text = string.Empty;

            ddlUserNameLevel3.Items.Clear();
            ddlUserNameLevel3.Items.Add("Load...");
            ddlUserNameLevel3.SelectedItem.Value = "0";
            txtReminderDayLevel3.Text = string.Empty;

            ddlUserNameLevel4.Items.Clear();
            ddlUserNameLevel4.Items.Add("Load...");
            ddlUserNameLevel4.SelectedItem.Value = "0";
            txtReminderDayLevel4.Text = string.Empty;

            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";
            txtReminderDayLevel5.Text = string.Empty;

            if (ddlUserName.SelectedIndex > 0)
            {
                DataSet dsLevelConfiguration = objLCController.GetSelectedOrAllLeveConfigurationDetail(Convert.ToInt32(ddlUserName.SelectedValue));
                if (dsLevelConfiguration.Tables[0].Rows.Count > 0)
                {
                    LevelConfigurationDetail();

                    //Can not Modify Lead if Transaction Available on ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT Table.
                    int TransCnt = Convert.ToInt32(objCommon.LookUp("ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT", "COUNT(*) AS CNT", "LEAD_UA_NO=" + ddlUserName.SelectedValue + ""));
                    if (TransCnt > 0)
                    {
                        btnSave.Visible = false;
                    }
                }
                else
                {
                    hdfLC_NO.Value = string.Empty;
                    hdfLC_NO1.Value = string.Empty;
                    hdfLC_NO2.Value = string.Empty;
                    hdfLC_NO3.Value = string.Empty;
                    hdfLC_NO4.Value = string.Empty;
                    hdfLC_NO5.Value = string.Empty;

                    //Check User Allready Assing to Another Lead or as a Lead.
                    if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserName.SelectedValue)))
                    {
                        objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserName.SelectedItem + " User Level is already define", this.Page);
                        ddlUserName.SelectedIndex = 0;
                    }
                    else
                    {
                        objCommon.FillDropDownList(ddlSequenceNo, "ACD_LEAD_SEQUENCE", "SEQUENCE_NO", "SEQUENCENAME", "SEQUENCE_NO NOT IN (SELECT DISTINCT SEQUENCE_NO FROM [DBO].[ACD_LEAD_LEVELCONFIGURATION])", "SEQUENCE_NO");
                        objCommon.FillDropDownList(ddlUserNameLevel1, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + ") ", "UA_FULLNAME");
                        collapse1.Attributes.Add("class", "panel-collapse");
                        collapse2.Attributes.Add("class", "panel-collapse collapse");
                        collapse3.Attributes.Add("class", "panel-collapse collapse");
                        collapse4.Attributes.Add("class", "panel-collapse collapse");
                        collapse5.Attributes.Add("class", "panel-collapse collapse");
                        txtReminderDayLevel1.Text = string.Empty;
                        txtReminderDayLevel2.Text = string.Empty;
                        txtReminderDayLevel3.Text = string.Empty;
                        txtReminderDayLevel4.Text = string.Empty;
                        txtReminderDayLevel5.Text = string.Empty;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.ddlUserName_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserNameLevel1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            collapse2.Attributes.Add("class", "panel-collapse collapse");
            collapse3.Attributes.Add("class", "panel-collapse collapse");
            collapse4.Attributes.Add("class", "panel-collapse collapse");
            collapse5.Attributes.Add("class", "panel-collapse collapse");

            ddlUserNameLevel2.Items.Clear();
            ddlUserNameLevel2.Items.Add("Load...");
            ddlUserNameLevel2.SelectedItem.Value = "0";
            txtReminderDayLevel2.Text = string.Empty;

            ddlUserNameLevel3.Items.Clear();
            ddlUserNameLevel3.Items.Add("Load...");
            ddlUserNameLevel3.SelectedItem.Value = "0";
            txtReminderDayLevel3.Text = string.Empty;

            ddlUserNameLevel4.Items.Clear();
            ddlUserNameLevel4.Items.Add("Load...");
            ddlUserNameLevel4.SelectedItem.Value = "0";
            txtReminderDayLevel4.Text = string.Empty;

            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";
            txtReminderDayLevel5.Text = string.Empty;

            if (ddlUserName.SelectedIndex > 0 && ddlUserNameLevel1.SelectedIndex > 0)
            {
                //Check User Allready Assing to Another Lead or as a Lead.
                RequiredFieldValidator1.Visible = true;
                if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserNameLevel1.SelectedValue)))
                {
                    objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserNameLevel1.SelectedItem + " User Level is already define", this.Page);
                    ddlUserNameLevel1.SelectedIndex = 0;
                }
                else
                {
                    objCommon.FillDropDownList(ddlUserNameLevel2, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + ") ", "UA_FULLNAME");
                    collapse2.Attributes.Add("class", "panel-collapse");
                    collapse3.Attributes.Add("class", "panel-collapse collapse");
                    collapse4.Attributes.Add("class", "panel-collapse collapse");
                    collapse5.Attributes.Add("class", "panel-collapse collapse");
                    txtReminderDayLevel2.Text = string.Empty;
                    txtReminderDayLevel3.Text = string.Empty;
                    txtReminderDayLevel4.Text = string.Empty;
                    txtReminderDayLevel5.Text = string.Empty;
                }
            }
            else
            {
                RequiredFieldValidator1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.ddlUserNameLevel1_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserNameLevel2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            collapse3.Attributes.Add("class", "panel-collapse collapse");
            collapse4.Attributes.Add("class", "panel-collapse collapse");
            collapse5.Attributes.Add("class", "panel-collapse collapse");

            ddlUserNameLevel3.Items.Clear();
            ddlUserNameLevel3.Items.Add("Load...");
            ddlUserNameLevel3.SelectedItem.Value = "0";

            ddlUserNameLevel4.Items.Clear();
            ddlUserNameLevel4.Items.Add("Load...");
            ddlUserNameLevel4.SelectedItem.Value = "0";

            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";

            if (ddlUserName.SelectedIndex > 0 && ddlUserNameLevel1.SelectedIndex > 0 && ddlUserNameLevel2.SelectedIndex > 0)
            {
                RequiredFieldValidator2.Visible = true;
                //Check User Allready Assing to Another Lead or as a Lead.
                if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserNameLevel2.SelectedValue)))
                {
                    objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserNameLevel2.SelectedItem + " User Level is already define", this.Page);
                    ddlUserNameLevel2.SelectedIndex = 0;
                }
                else
                {
                    objCommon.FillDropDownList(ddlUserNameLevel3, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + ") ", "UA_FULLNAME");
                    collapse3.Attributes.Add("class", "panel-collapse");
                    collapse4.Attributes.Add("class", "panel-collapse collapse");
                    collapse5.Attributes.Add("class", "panel-collapse collapse");
                    txtReminderDayLevel3.Text = string.Empty;
                    txtReminderDayLevel4.Text = string.Empty;
                    txtReminderDayLevel5.Text = string.Empty;
                }
            }
            else
            {
                RequiredFieldValidator2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.ddlUserNameLevel2_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserNameLevel3_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            collapse4.Attributes.Add("class", "panel-collapse collapse");
            collapse5.Attributes.Add("class", "panel-collapse collapse");

            ddlUserNameLevel4.Items.Clear();
            ddlUserNameLevel4.Items.Add("Load...");
            ddlUserNameLevel4.SelectedItem.Value = "0";

            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";

            if (ddlUserName.SelectedIndex > 0 && ddlUserNameLevel1.SelectedIndex > 0 && ddlUserNameLevel2.SelectedIndex > 0 && ddlUserNameLevel3.SelectedIndex > 0)
            {
                RequiredFieldValidator3.Visible = true;
                if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserNameLevel3.SelectedValue)))
                {
                    objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserNameLevel3.SelectedItem + " User Level is already define", this.Page);
                    ddlUserNameLevel3.SelectedIndex = 0;
                }
                else
                {
                    objCommon.FillDropDownList(ddlUserNameLevel4, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + "," + ddlUserNameLevel3.SelectedValue + ") ", "UA_FULLNAME");
                    collapse4.Attributes.Add("class", "panel-collapse");
                    collapse5.Attributes.Add("class", "panel-collapse collapse");
                    txtReminderDayLevel4.Text = string.Empty;
                    txtReminderDayLevel5.Text = string.Empty;
                }
            }
            else
            {
                RequiredFieldValidator3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.ddlUserNameLevel3_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserNameLevel4_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            collapse5.Attributes.Add("class", "panel-collapse collapse");
            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";

            if (ddlUserName.SelectedIndex > 0 && ddlUserNameLevel1.SelectedIndex > 0 && ddlUserNameLevel2.SelectedIndex > 0 && ddlUserNameLevel3.SelectedIndex > 0 && ddlUserNameLevel4.SelectedIndex > 0)
            {
                RequiredFieldValidator4.Visible = true;
                if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserNameLevel4.SelectedValue)))
                {
                    objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserNameLevel4.SelectedItem + " User Level is already define", this.Page);
                    ddlUserNameLevel4.SelectedIndex = 0;
                }
                else
                {
                    objCommon.FillDropDownList(ddlUserNameLevel5, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + "," + ddlUserNameLevel3.SelectedValue + "," + ddlUserNameLevel4.SelectedValue + ") ", "UA_FULLNAME");
                    collapse5.Attributes.Add("class", "panel-collapse");
                    txtReminderDayLevel5.Text = string.Empty;
                }
            }
            else
            {
                RequiredFieldValidator4.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.ddlUserNameLevel4_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlUserNameLevel5_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserNameLevel5.SelectedIndex > 0)
        {
            RequiredFieldValidator5.Visible = true;
            if (true == CheckUserAllReadyExitInLevel(Convert.ToInt32(ddlUserNameLevel5.SelectedValue)))
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, ddlUserNameLevel5.SelectedItem + " User Level is already define", this.Page);
                ddlUserNameLevel5.SelectedIndex = 0;
            }
        }
        else
        {
            RequiredFieldValidator5.Visible = true;
        }
    }

    protected void btnSave_Click(object Sender,EventArgs e)
    {
        try
        {
            objLCEntity.CreateModifyBy = Convert.ToInt32(Session["userno"].ToString());
            objLCEntity.IPAddress = Request.ServerVariables["REMOTE_ADDR"];

            DataRow dr = null;
            DataTable dtLevelConfiguration = new DataTable("LEADLEVELCONFIGURATIONTBL");
            dtLevelConfiguration.Columns.Add("LC_NO", typeof(int));
            dtLevelConfiguration.Columns.Add("LEAD_UA_NO", typeof(int));
            dtLevelConfiguration.Columns.Add("UA_NO", typeof(int));
            dtLevelConfiguration.Columns.Add("SEQUENCE_NO", typeof(int));
            dtLevelConfiguration.Columns.Add("LEVELNO", typeof(int));
            dtLevelConfiguration.Columns.Add("REMINDER_DAY", typeof(int));
            dtLevelConfiguration.Columns.Add("TOTAL_DAY", typeof(int));
            dtLevelConfiguration.Columns.Add("CREATED_MODIFY_BY", typeof(int));
            dtLevelConfiguration.Columns.Add("IPADDRESS", typeof(string));
            DataTable _dtLevelConfiguration = null;

            int LC_NO = Convert.ToInt32(objCommon.LookUp("ACD_LEAD_LEVELCONFIGURATION", "COUNT(*) AS CNT", ""));

            if (ddlUserName.SelectedIndex > 0 && txtReminderDay.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Reminder Day", this.Page);
                txtReminderDay.Focus();
                return;
            }

            if(ddlUserNameLevel1.SelectedIndex>0 && txtReminderDayLevel1.Text==string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Level 1 Reminder Day", this.Page);
                txtReminderDayLevel1.Focus();
                return;
            }


            if (ddlUserNameLevel2.SelectedIndex > 0 && txtReminderDayLevel2.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Level 2 Reminder Day", this.Page);
                txtReminderDayLevel2.Focus();
                return;
            }

            if (ddlUserNameLevel3.SelectedIndex > 0 && txtReminderDayLevel3.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Level 3 Reminder Day", this.Page);
                txtReminderDayLevel3.Focus();
                return;
            }

            if (ddlUserNameLevel4.SelectedIndex > 0 && txtReminderDayLevel4.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Level 4 Reminder Day", this.Page);
                txtReminderDayLevel4.Focus();
                return;
            }

            if (ddlUserNameLevel5.SelectedIndex > 0 && txtReminderDayLevel5.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Please Enter Level 5 Reminder Day", this.Page);
                txtReminderDayLevel5.Focus();
                return;
            }
            
            //Mean Lead
            if (ddlUserName.SelectedIndex > 0)
            {
                dr = dtLevelConfiguration.NewRow();

                if(hdfLC_NO.Value==string.Empty)
                    LC_NO +=1;
                 else
                    LC_NO=Convert.ToInt32(hdfLC_NO.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserName.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 0;
                dr["REMINDER_DAY"] = txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text;
                dr["TOTAL_DAY"] = txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text;
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            //Level 1
            if (ddlUserNameLevel1.SelectedIndex>0)
            {
                dr = dtLevelConfiguration.NewRow();

                if (hdfLC_NO1.Value == string.Empty)
                    LC_NO += 1;
                else
                    LC_NO = Convert.ToInt32(hdfLC_NO1.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserNameLevel1.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 1;
                dr["REMINDER_DAY"] = txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text;
                dr["TOTAL_DAY"] = Convert.ToInt32(txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text) + Convert.ToInt32(txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text);
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            //Level 2
            if (ddlUserNameLevel2.SelectedIndex>0)
            {
                dr = dtLevelConfiguration.NewRow();

                if (hdfLC_NO2.Value == string.Empty)
                    LC_NO += 1;
                else
                    LC_NO = Convert.ToInt32(hdfLC_NO2.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserNameLevel2.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 2;
                dr["REMINDER_DAY"] = txtReminderDayLevel2.Text == string.Empty ? "0" : txtReminderDayLevel2.Text;
                dr["TOTAL_DAY"] = Convert.ToInt32(txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text) + Convert.ToInt32(txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text) + Convert.ToInt32(txtReminderDayLevel2.Text == string.Empty ? "0" : txtReminderDayLevel2.Text);
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            //Level 3
            if (ddlUserNameLevel3.SelectedIndex > 0)
            {
                dr = dtLevelConfiguration.NewRow();

                if (hdfLC_NO3.Value == string.Empty)
                    LC_NO += 1;
                else
                    LC_NO = Convert.ToInt32(hdfLC_NO3.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserNameLevel3.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 3;
                dr["REMINDER_DAY"] = txtReminderDayLevel3.Text == string.Empty ? "0" : txtReminderDayLevel3.Text;
                dr["TOTAL_DAY"] = Convert.ToInt32(txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text) + Convert.ToInt32(txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text) + Convert.ToInt32(txtReminderDayLevel2.Text == string.Empty ? "0" : txtReminderDayLevel2.Text) + Convert.ToInt32(txtReminderDayLevel3.Text == string.Empty ? "0" : txtReminderDayLevel3.Text);
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            //Level 4
            if (ddlUserNameLevel3.SelectedIndex > 0)
            {
                dr = dtLevelConfiguration.NewRow();

                if (hdfLC_NO4.Value == string.Empty)
                    LC_NO += 1;
                else
                    LC_NO = Convert.ToInt32(hdfLC_NO4.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserNameLevel4.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 4;
                dr["REMINDER_DAY"] = txtReminderDayLevel4.Text == string.Empty ? "0" : txtReminderDayLevel4.Text;
                dr["TOTAL_DAY"] = Convert.ToInt32(txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text) + Convert.ToInt32(txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text) + Convert.ToInt32(txtReminderDayLevel2.Text == string.Empty ? "0" : txtReminderDayLevel2.Text) + Convert.ToInt32(txtReminderDayLevel3.Text == string.Empty ? "0" : txtReminderDayLevel3.Text) + Convert.ToInt32(txtReminderDayLevel4.Text == string.Empty ? "0" : txtReminderDayLevel4.Text);
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            //Level 5
            if (ddlUserNameLevel5.SelectedIndex > 0)
            {
                dr = dtLevelConfiguration.NewRow();

                if (hdfLC_NO5.Value == string.Empty)
                    LC_NO += 1;
                else
                    LC_NO = Convert.ToInt32(hdfLC_NO5.Value);

                dr["LC_NO"] = LC_NO;
                dr["LEAD_UA_NO"] = ddlUserName.SelectedValue;
                dr["UA_NO"] = ddlUserNameLevel5.SelectedValue;
                dr["SEQUENCE_NO"] = ddlSequenceNo.SelectedValue;
                dr["LEVELNO"] = 5;
                dr["REMINDER_DAY"] = txtReminderDayLevel5.Text == string.Empty ? "0" : txtReminderDayLevel5.Text;
                dr["TOTAL_DAY"] = Convert.ToInt32(txtReminderDay.Text == string.Empty ? "0" : txtReminderDay.Text) + Convert.ToInt32(txtReminderDayLevel1.Text == string.Empty ? "0" : txtReminderDayLevel1.Text) + Convert.ToInt32(txtReminderDayLevel2.Text == string.Empty ? "0" : txtReminderDayLevel2.Text) + Convert.ToInt32(txtReminderDayLevel3.Text == string.Empty ? "0" : txtReminderDayLevel3.Text) + Convert.ToInt32(txtReminderDayLevel4.Text == string.Empty ? "0" : txtReminderDayLevel4.Text) + Convert.ToInt32(txtReminderDayLevel5.Text == string.Empty ? "0" : txtReminderDayLevel5.Text);
                dr["CREATED_MODIFY_BY"] = objLCEntity.CreateModifyBy;
                dr["IPADDRESS"] = objLCEntity.IPAddress;
                dtLevelConfiguration.Rows.Add(dr);
            }

            _dtLevelConfiguration = dtLevelConfiguration;

            CustomStatus cs = (CustomStatus)objLCController.InsertUpdateLevelConfiguration(_dtLevelConfiguration);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Record saved successfully", this.Page);
                ddlUserName.SelectedIndex = 0;
                ddlUserName_OnSelectedIndexChanged(Sender, e);
            }
            else if (cs.Equals(CustomStatus.Error))
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Error Occured", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancel_Click(object Sender, EventArgs e)
    {
        try
        {
            ddlUserName.SelectedIndex = 0;
            ddlUserName_OnSelectedIndexChanged(Sender, e);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnShowReport_Click(object Sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet dsLevelConfiguration = objLCController.GetSelectedOrAllLeveConfigurationDetail(Convert.ToInt32(ddlUserName.SelectedValue));
            if (dsLevelConfiguration.Tables[0].Rows.Count > 0)
            {
                dsLevelConfiguration.Tables[0].Columns.Remove("LC_NO");
                dsLevelConfiguration.Tables[0].Columns.Remove("LEAD_UA_NO");
                dsLevelConfiguration.Tables[0].Columns.Remove("UA_NO");
                dsLevelConfiguration.Tables[0].Columns.Remove("UA_FULLNAME");
                dsLevelConfiguration.Tables[0].Columns.Remove("SEQUENCE_NO");
                dsLevelConfiguration.Tables[0].Columns.Remove("LEVELNO");
                dsLevelConfiguration.Tables[0].Columns.Remove("REMINDER_DAY");

                GV.DataSource = dsLevelConfiguration;
                GV.DataBind();
                string attachment = "attachment; filename=LevelConfiguration.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.upLevelConfiguration, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion Form Event


    // User Define Function
    #region User Define Function

    private bool CheckUserAllReadyExitInLevel(int UA_NO)
    {
        int ChkUserAllReadyExit = Convert.ToInt32(objCommon.LookUp("ACD_LEAD_LEVELCONFIGURATION", "CASE WHEN COUNT(UA_NO)>0 THEN 1 ELSE 0 END AS EXITSTCHECK", "UA_NO=" + UA_NO + ""));
        if (ChkUserAllReadyExit.Equals(1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=LevelConfiguration.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=LevelConfiguration.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.CheckPageAuthorization-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void DropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlUserName, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2", "UA_FULLNAME");
            objCommon.FillDropDownList(ddlSequenceNo, "ACD_LEAD_SEQUENCE", "SEQUENCE_NO", "SEQUENCENAME", "SEQUENCE_NO NOT IN (SELECT DISTINCT SEQUENCE_NO FROM [DBO].[ACD_LEAD_LEVELCONFIGURATION])", "SEQUENCE_NO");

            //Level 1
            ddlUserNameLevel1.Items.Clear();
            ddlUserNameLevel1.Items.Add("Load...");
            ddlUserNameLevel1.SelectedItem.Value = "0";

            //Level 2
            ddlUserNameLevel2.Items.Clear();
            ddlUserNameLevel2.Items.Add("Load...");
            ddlUserNameLevel2.SelectedItem.Value = "0";

            //Level 3
            ddlUserNameLevel3.Items.Clear();
            ddlUserNameLevel3.Items.Add("Load...");
            ddlUserNameLevel3.SelectedItem.Value = "0";

            //Level 4
            ddlUserNameLevel4.Items.Clear();
            ddlUserNameLevel4.Items.Add("Load...");
            ddlUserNameLevel4.SelectedItem.Value = "0";

            //Level 5
            ddlUserNameLevel5.Items.Clear();
            ddlUserNameLevel5.Items.Add("Load...");
            ddlUserNameLevel5.SelectedItem.Value = "0";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.DropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void LevelConfigurationDetail()
    {
        try
        {
                DataSet dsLevelConfiguration = objLCController.GetSelectedOrAllLeveConfigurationDetail(Convert.ToInt32(ddlUserName.SelectedValue));
                if (dsLevelConfiguration.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsLevelConfiguration.Tables[0].Rows)
                    {
                        if (dr["LEVELNO"].ToString().Equals("0"))
                        {
                            objCommon.FillDropDownList(ddlSequenceNo, "ACD_LEAD_SEQUENCE", "SEQUENCE_NO", "SEQUENCENAME", "", "SEQUENCE_NO");
                            ddlUserName.SelectedValue = dr["UA_NO"].ToString();
                            ddlSequenceNo.SelectedValue = dr["SEQUENCE_NO"].ToString();
                            txtReminderDay.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO.Value = dr["LC_NO"].ToString();

                            objCommon.FillDropDownList(ddlUserNameLevel1, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + ") ", "UA_FULLNAME");
                            collapse1.Attributes.Add("class", "panel-collapse");
                            collapse2.Attributes.Add("class", "panel-collapse collapse");
                            collapse3.Attributes.Add("class", "panel-collapse collapse");
                            collapse4.Attributes.Add("class", "panel-collapse collapse");
                            collapse5.Attributes.Add("class", "panel-collapse collapse");
                        }
                        else if (dr["LEVELNO"].ToString().Equals("1"))
                        {
                            ddlUserNameLevel1.SelectedValue = dr["UA_NO"].ToString();
                            txtReminderDayLevel1.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO1.Value = dr["LC_NO"].ToString();

                            objCommon.FillDropDownList(ddlUserNameLevel2, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + ") ", "UA_FULLNAME");
                            collapse2.Attributes.Add("class", "panel-collapse");
                            collapse3.Attributes.Add("class", "panel-collapse collapse");
                            collapse4.Attributes.Add("class", "panel-collapse collapse");
                            collapse5.Attributes.Add("class", "panel-collapse collapse");
                        }
                        else if (dr["LEVELNO"].ToString().Equals("2"))
                        {
                            ddlUserNameLevel2.SelectedValue = dr["UA_NO"].ToString();
                            txtReminderDayLevel2.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO2.Value=dr["LC_NO"].ToString();

                            objCommon.FillDropDownList(ddlUserNameLevel3, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + ") ", "UA_FULLNAME");
                            collapse3.Attributes.Add("class", "panel-collapse");
                            collapse4.Attributes.Add("class", "panel-collapse collapse");
                            collapse5.Attributes.Add("class", "panel-collapse collapse");
                        }
                        else if (dr["LEVELNO"].ToString().Equals("3"))
                        {
                            ddlUserNameLevel3.SelectedValue = dr["UA_NO"].ToString();
                            txtReminderDayLevel3.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO3.Value = dr["LC_NO"].ToString();

                            objCommon.FillDropDownList(ddlUserNameLevel4, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + "," + ddlUserNameLevel3.SelectedValue + ") ", "UA_FULLNAME");
                            collapse4.Attributes.Add("class", "panel-collapse");
                            collapse5.Attributes.Add("class", "panel-collapse collapse");
                        }
                        else if (dr["LEVELNO"].ToString().Equals("4"))
                        {
                            ddlUserNameLevel4.SelectedValue = dr["UA_NO"].ToString();
                            txtReminderDayLevel4.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO4.Value = dr["LC_NO"].ToString();

                            objCommon.FillDropDownList(ddlUserNameLevel5, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2 AND UA_NO NOT IN(" + ddlUserName.SelectedValue + "," + ddlUserNameLevel1.SelectedValue + "," + ddlUserNameLevel2.SelectedValue + "," + ddlUserNameLevel3.SelectedValue + "," + ddlUserNameLevel4.SelectedValue + ") ", "UA_FULLNAME");
                            collapse5.Attributes.Add("class", "panel-collapse");
                        }
                        else if (dr["LEVELNO"].ToString().Equals("5"))
                        {
                            ddlUserNameLevel5.SelectedValue = dr["UA_NO"].ToString();
                            txtReminderDayLevel5.Text = dr["REMINDER_DAY"].ToString();
                            hdfLC_NO5.Value = dr["LC_NO"].ToString();
                        }
                    }
             }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Masters_LevelConfiguration.LevelConfigurationDetail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion User Define Function
}
